using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Collections.Specialized;
using System.IO;

namespace Photoman.Controller
{
    /// <summary>
    /// This class handles all image management.
    /// This includes keeping track of all loaded images, which image is current,
    /// the cache, and storage of image history(does NOT include actual operations, just changes)
    /// </summary>
    class ImageHandler
    {
        //Dictionary to store loaded images. Int is an ID number, Bitmap is the actual image data.
        private OrderedDictionary m_ibmpDicLoadedBitmaps = new OrderedDictionary();        

        //Bool list for cache status - True is cached, false is loaded
        private List<bool> m_bLstBitmapCacheStatus = new List<bool>();

        //String list for base filepaths
        private List<String> m_strLstBitmapStrings = new List<String>();
        
        //String list for cache filepaths
        private List<String> m_strLstBitmapCachePath = new List<String>();

        //Int list for ID access order. Newer images are in the back
        private List<int> m_iLstIDAccessOrder = new List<int>();

        //The current image
        private int m_iCurrentImage = -1;

        //The current ID number to assign to another image
        private int m_iID = -1;
        private int iID
        {
            get
            {
                m_iID++;
                return m_iID;
            }
        }

        public ImageHandler()
        {
            //Delete old cache directory
            if (System.IO.Directory.Exists(Constants.strCachePath))
            {
                Global.WriteToLog("Emptying image cache", false);
                string[] files = System.IO.Directory.GetFiles(Constants.strCachePath);
                foreach (string file in files)
                    System.IO.File.Delete(file);                     
            }
        }

        ~ImageHandler()
        {
            //Delete old cache directory
            if (System.IO.Directory.Exists(Constants.strCachePath))
            {
                Global.WriteToLog("Emptying image cache", true);
                string[] files = System.IO.Directory.GetFiles(Constants.strCachePath);
                foreach (string file in files)
                    System.IO.File.Delete(file);
            }
        }

        //Percentage of ram in use required before we start caching. Defaults to 75%
        private int m_iMemPercentageForCache = 80;        
        /// <summary>
        /// Sets or gets the percentage of ram usage before caching begins
        /// </summary>
        internal int iMemPercentageForCache
        {
            get { return m_iMemPercentageForCache; }
            set
            {
                m_iMemPercentageForCache = value;
                //Code to check if we need to cache goes here
                DetermineIfShouldCache();
            }
        }

        //Bool for if cache is to be used or not
        private bool m_bUseCache = true;
        /// <summary>
        /// Bool for if cache is enabled. If switched after app start, cached images will not
        /// immediatly be loaded back in but nothing will be written back to the disk.
        /// </summary>
        internal bool bUseCache
        {
            get { return m_bUseCache; }
            set { m_bUseCache = value; }
        }

        //Bool for if the cache is enabled or not
        private bool m_bCacheEnabled = false;
        /// <summary>
        /// Returns whether or not the program is currently caching images
        /// </summary>
        internal bool bCacheEnabled
        {
            get { return m_bCacheEnabled; }
        }

        /// <summary>
        /// The number of loaded images. Read Only
        /// </summary>
        internal int ImageCount
        {
            get { return m_ibmpDicLoadedBitmaps.Count; }
        }

        /// <summary>
        /// The current working image index
        /// </summary>
        internal int CurrentImageIndex
        {
            get { return m_iCurrentImage; }
        }

        /// <summary>
        /// The current working image.
        /// </summary>
        internal Bitmap CurrentImage
        {
            get { return (Bitmap)m_ibmpDicLoadedBitmaps[m_iCurrentImage]; }
            set { m_ibmpDicLoadedBitmaps[m_iCurrentImage] = value; }
        }

        /// <summary>
        /// Sets the working image to the passed index
        /// </summary>
        /// <param name="iImageIndex">The index to switch to</param>
        internal void SwitchImages(int iIndexImage)
        {
            SwitchImages(iIndexImage, true);
        }

        /// <summary>
        /// Sets the working image to the passed index
        /// </summary>
        /// <param name="iImageIndex">The index to switch to</param>
        /// <param name="CheckDiskCache">Whether or not to check disk cache. Should always be true unless this is called as a result of file IO</param>
        internal void SwitchImages(int iImageIndex, bool CheckDiskCache)
        {
            try
            {
                //Ignore negative indexes
                if (iImageIndex < 0)
                    return;

                //Integrity check. Stops out of bounds loading
                if (iImageIndex >= 0 && iImageIndex < m_ibmpDicLoadedBitmaps.Count)
                {
                    //Load image if cached
                    if (m_ibmpDicLoadedBitmaps[iImageIndex] == null)
                    {   
                        Global.WriteToLog("Loading image ID# " + iImageIndex + " from cache", true);

                        //Use a filestream to avoid locking image
                        FileStream fs = new FileStream(m_strLstBitmapCachePath[iImageIndex],
                            FileMode.Open, FileAccess.Read);

                        //Check disk cache before loading file to ensure it wont kill the program
                        //Needs to be updated to use the actual file size of the image and not just offset the percentage by a fixed value
                        //DetermineIfShouldCache(true, Constants.iCachePercentageOffsetOnForce);
                        FileInfo fi = new FileInfo(m_strLstBitmapCachePath[iImageIndex]);
                        DetermineIfShouldCache(fi.Length);

                        //Use a temporary Image and then cast to Bitmap so save doesnt lock up later
                        Image imgTemp = Image.FromStream(fs);
                        m_ibmpDicLoadedBitmaps[iImageIndex] = new Bitmap(imgTemp);
                        imgTemp.Dispose();
                        fs.Close();
                        m_bLstBitmapCacheStatus[iImageIndex] = false;
                    }

                    m_iCurrentImage = iImageIndex;

                    //Remove all occurances of this image from the querey
                    int[] keys = new int[m_ibmpDicLoadedBitmaps.Keys.Count];
                    m_ibmpDicLoadedBitmaps.Keys.CopyTo(keys, 0);
                    do
                    {    
                        if (!m_iLstIDAccessOrder.Remove(keys[iImageIndex]))
                            break;
                    } while (true);

                    //Cache code
                    if (CheckDiskCache)
                    {
                        do
                        {
                            //Check if we need to cache and do it if needed
                            DetermineIfShouldCache();
                        } while ((m_bCacheEnabled) && (m_iLstIDAccessOrder.Count >= 1)); //repeat until cache flag turns off or only one element is in the cache queue
                    }

                    //Place on back of list.
                    m_ibmpDicLoadedBitmaps.Keys.CopyTo(keys, 0);
                    m_iLstIDAccessOrder.Add(keys[iImageIndex]);
                }
                else
                    SwitchImages(m_ibmpDicLoadedBitmaps.Count - 1);

            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
        }

        /// <summary>
        /// Removes the current working image
        /// </summary>
        internal void RemoveImage()
        {            
            RemoveImage(m_iCurrentImage);
        }

        /// <summary>
        /// Removes the image at the passed index.
        /// </summary>
        /// <param name="iImageIndex"></param>
        internal void RemoveImage(int iImageIndex)
        {
            try
            {
                //Remove the actual bitmap data
                int[] keys = new int[m_ibmpDicLoadedBitmaps.Count];
                m_ibmpDicLoadedBitmaps.Keys.CopyTo(keys, 0);
                m_ibmpDicLoadedBitmaps.Remove(keys[iImageIndex]);
                //Remove the file path
                m_strLstBitmapStrings.RemoveAt(iImageIndex);

                //Clean out cache path and remove
                if(System.IO.File.Exists(m_strLstBitmapCachePath[iImageIndex]))
                    System.IO.File.Delete(m_strLstBitmapCachePath[iImageIndex]);

                m_strLstBitmapCachePath.RemoveAt(iImageIndex);

                //Remove cache status
                m_bLstBitmapCacheStatus.RemoveAt(iImageIndex);

                //Fix any messed up indexes
                if (m_iCurrentImage > m_ibmpDicLoadedBitmaps.Count - 1)
                    m_iCurrentImage = m_ibmpDicLoadedBitmaps.Count - 1;
                else if (m_iCurrentImage < 0)
                    m_iCurrentImage = 0;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
        }

        /// <summary>
        /// Loads the given image and adds it to the image list
        /// </summary>
        /// <param name="FilePath"></param>
        internal void AddImage(string FilePath)
        {
            try
            {  

                //Add the bitmap after indexed pixel check
                //Use a filestream to avoid issue with bitmap locking
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                //Clone bitmap instead of using direct to stop save method from dying later.

                //Check disk cache before loading file to ensure it wont kill the program
                //Needs to be updated to use the actual file size of the image and not just offset the percentage by a fixed value
                //DetermineIfShouldCache(true, fs.Length);
                FileInfo fi = new FileInfo(FilePath);
                DetermineIfShouldCache(fi.Length);
                
                //Add the string file path
                m_strLstBitmapStrings.Add(FilePath);

                //Yes, I know the casting looks weird. But if we keep it as a bitmap something in System.Drawing chokes later on.
                Image imgTemp = (Image)Global.CheckFor_ConvertIndexedPixels((Bitmap)Image.FromStream(fs));
                m_ibmpDicLoadedBitmaps.Add(iID, new Bitmap(imgTemp) );
                //m_ibmpDicLoadedBitmaps.Add(iID, (Bitmap)imgTemp.Clone());

                imgTemp.Dispose();
                fs.Close();

                //Set cache status to false
                m_bLstBitmapCacheStatus.Add(false);
                //Cache path not yet determined
                m_strLstBitmapCachePath.Add("");
                
                //Add current index to the index accesss order list
                int[] keys = new int[m_ibmpDicLoadedBitmaps.Count];
                m_ibmpDicLoadedBitmaps.Keys.CopyTo(keys, 0);
                m_iLstIDAccessOrder.Add(keys.Last());

                //Switch the working image to the newly added image by jumping to the back.
                SwitchImages(m_ibmpDicLoadedBitmaps.Count - 1, false); 

            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
        }

        /// <summary>
        /// Gets the image at the specified index.
        /// </summary>
        /// <param name="ImageIndex">The index to pull from</param>
        /// <returns>The bitmap data at the specified index</returns>
        internal Bitmap GetImageAtIndex(int ImageIndex)
        {
            return (Bitmap)m_ibmpDicLoadedBitmaps[ImageIndex];
        }

        /// <summary>
        /// Gets the current image file path
        /// </summary>
        /// <returns></returns>
        internal string ImageFilePath()
        {
            return ImageFilePath(m_iCurrentImage);
        }

        /// <summary>
        /// Gets the image file path for the given index.
        /// </summary>
        /// <param name="iImageIndex"></param>
        /// <returns></returns>
        internal string ImageFilePath(int iImageIndex)
        {
            return m_strLstBitmapStrings[iImageIndex];
        }

        /// <summary>
        /// Saves the current image
        /// </summary>
        /// <returns>The return value is whether or not the save was successfull</returns>
        internal bool SaveImage(string FileName)
        {
            return SaveImage(FileName, m_iCurrentImage);
        }

        /// <summary>
        /// Saves the image at the requested index
        /// </summary>
        /// <param name="iImageIndex"></param>
        /// <returns>The return value is whether or not the save was successfull</returns>
        internal bool SaveImage(string FileName, int iImageIndex)
        {
            try
            {
                ((Bitmap)m_ibmpDicLoadedBitmaps[iImageIndex]).Save(FileName);
                return true;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Caches the current image to the disk for ram purposes. Return value is if caching is succesfull
        /// </summary>
        /// <returns>Exit status. True means success, false means it errored out and you need to check the log</returns>
        internal bool CacheImage()
        {
            try
            {
                if (m_bUseCache && m_bCacheEnabled && (m_iLstIDAccessOrder.Count >= 1))
                {
                    //KeyValuePair<int, Bitmap> ibmpToCache;
                    int iCurrentID = m_iLstIDAccessOrder.First();
                    m_iLstIDAccessOrder.RemoveAt(0);

                    //Get the index of the key in the dictionary. If key remains -1, its not in the dictionary
                    int[] keys = new int[m_ibmpDicLoadedBitmaps.Count];
                    m_ibmpDicLoadedBitmaps.Keys.CopyTo(keys, 0);
                    int iIndexOfKey = -1;
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        if (keys[i] == iCurrentID)
                        {
                            iIndexOfKey = i;
                            break;
                        }
                    }

                    if ((iIndexOfKey != -1) && !m_bLstBitmapCacheStatus[iCurrentID])
                    {

                        Global.WriteToLog("Caching image " + iCurrentID, true);

                        //Get the file name for the image
                        System.IO.FileInfo fiFileName = new System.IO.FileInfo(m_strLstBitmapStrings[iCurrentID]);

                        //generate cache path
                        string strCachePath = Constants.strCachePath + fiFileName.Name;


                        if (!System.IO.Directory.Exists(Constants.strCachePath))
                            System.IO.Directory.CreateDirectory(Constants.strCachePath);

                        //Commented because we've already deleted the contents at startup.
                        //This just slows us down.

                        //Delete anything occupying cache space
                        //if (System.IO.File.Exists(strCachePath))
                        //    System.IO.File.Delete(strCachePath);

                        //Set cache path
                        m_strLstBitmapCachePath[iCurrentID] = strCachePath;


                        //Save off image
                        //Not sure if object conversion of int to use as key is valid?
                        ((Bitmap)m_ibmpDicLoadedBitmaps[iIndexOfKey]).Save(strCachePath);

                        //Set image to cached
                        m_bLstBitmapCacheStatus[iIndexOfKey] = true;

                        //Clear out current bitmap
                        m_ibmpDicLoadedBitmaps[iIndexOfKey] = null;

                        //Cached successfully
                        return true;
                    }
                    else
                        throw new Exception("Key not in dictionary or attemped to cache an already cached image!");
                }

                //Return false since getting here meains we didnt cache
                return false;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Determines if we need to enable disk caching
        /// </summary>
        internal void DetermineIfShouldCache()
        {
            DetermineIfShouldCache(0);
        }

        /// <summary>
        /// Determines if we need to enable disk caching
        /// </summary>
        /// <param name="SizeOfFile">The size of the file to check against</param>
        internal void DetermineIfShouldCache(long SizeOfFile)//int RangeAwayFromPercentage)
        {
            //Disabled to test speed
            double dMemPercentage = ((Global.GetUsedMemoryBySys() + (double)SizeOfFile + Constants.iCacheProgramOverhead) / Global.GetTotalMemory()) * 100d;
            
            
            Global.WriteToLog("Percentage of System Ram Used: " + dMemPercentage, true);

            if (dMemPercentage >= m_iMemPercentageForCache)
            {
                Global.WriteToLog("Memory use higher than " + m_iMemPercentageForCache + "%, cache is enabled", true);
                m_bCacheEnabled = true;                

                //Actualy try to cache an image.
                CacheImage();
            }
            else
            {
                Global.WriteToLog("Memory use lower than " + m_iMemPercentageForCache + "%, cache is disabled", true);
                m_bCacheEnabled = false;
            }

            //Fire disk cache status change event
            if (Global.DiskCacheStatusChange != null)
                Global.DiskCacheStatusChange(m_bCacheEnabled);
        }
    }
}