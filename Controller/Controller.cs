using System;
using System.Collections.Generic;
using System.Drawing;

namespace Photoman.Controller
{
    /// <summary>
    /// This class will act as a middleman between the Core namespace,
    /// the UserInterface namespace, and the ImageHandler class
    /// 
    /// Current implimentation is for demo purposes and most likely will not resemble final version
    /// </summary>
    class Controller
    {
        #region Controller stuff

        private ImageHandler m_ihPrimaryImageHandler;

        private HistoryHandler m_hhPrimaryHistoryHandler;
        
        private AutomationHandler m_ahPrimaryAutomationHandler;
        
        public Controller()
        {
            m_ihPrimaryImageHandler = new ImageHandler();
            m_hhPrimaryHistoryHandler = new HistoryHandler();
            m_ahPrimaryAutomationHandler = new AutomationHandler(this);
        }

        #endregion
        
        #region History stuff

        /// <summary>
        /// Updates the image in the Image Handler with content from the History Handler
        /// </summary>
        private void UpdateImageInIH()
        {
            if (m_ihPrimaryImageHandler.ImageCount > 0)
            {
                Global.WriteToLog("Updating ImageHandler image from History", true);
                m_ihPrimaryImageHandler.CurrentImage = m_hhPrimaryHistoryHandler.CurrentBitmap;
            }
        }

        /// <summary>
        /// Undoes the image
        /// </summary>
        /// <returns></returns>
        public Bitmap UndoImage()
        {
            Global.WriteToLog("Undoing image", true);
            m_hhPrimaryHistoryHandler.Undo();
            UpdateImageInIH();
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// Redoes the image
        /// </summary>
        /// <returns></returns>
        public Bitmap RedoImage()
        {
            Global.WriteToLog("Redoing image", true);
            m_hhPrimaryHistoryHandler.Redo();
            UpdateImageInIH();
            return m_ihPrimaryImageHandler.CurrentImage;
        }


        public int UndoCount()
        {
            int iUndoCount = m_hhPrimaryHistoryHandler.HistoryCount - 1 - RedoCount();
            if (iUndoCount < 0)
                return 0;
            else
                return iUndoCount;
        }

        public int RedoCount()
        {
            int iRedoCount = m_hhPrimaryHistoryHandler.HistoryCount - m_hhPrimaryHistoryHandler.Index - 1;
            if (iRedoCount < 0)
                return 0;
            else
                return iRedoCount;
        }


        #endregion

        #region Automation stuff
        
        public void Automation_AddOperation(Constants.Operations Op, List<object> Arguments)
        {
        	Global.WriteToLog("Adding operation" + Op.ToString() + " to automation list", true);
        	m_ahPrimaryAutomationHandler.AddOperation(Op, Arguments);
        }
         
        public void Automation_RemoveOperation(int Index)
        {
        	Global.WriteToLog("Removing operation at " + Index + "from automation list", true);
        	m_ahPrimaryAutomationHandler.RemoveOperation(Index);
        }
        
        public Bitmap Automation_Start()
        {
        	Global.WriteToLog("Starting automation", true);
        	//Start automation
        	m_ahPrimaryAutomationHandler.Start(m_ihPrimaryImageHandler.CurrentImageIndex);
        	//return current image
        	return m_ihPrimaryImageHandler.CurrentImage;
        }
        
        public Constants.Operations[] Automation_OperationList()
        {
        	Global.WriteToLog("Retriving operation list from autohandler", true);
        	return m_ahPrimaryAutomationHandler.OperationList;
        }
        
        public Bitmap BatchOp(Constants.Operations Op, List<object> Arguments)
        {
        	Global.WriteToLog("Performing batch operation " + Op.ToString(), true);
        	//Clear out previous stuff
        	m_ahPrimaryAutomationHandler.Clear();
        	//Add op to handler
        	m_ahPrimaryAutomationHandler.AddOperation(Op, Arguments);
        	//Start handler
        	m_ahPrimaryAutomationHandler.Start(m_ihPrimaryImageHandler.CurrentImageIndex);
        	//Clear handler
        	m_ahPrimaryAutomationHandler.Clear();
        	//Return current image.
        	return m_ihPrimaryImageHandler.CurrentImage;
        }
        
        #endregion

        #region ImageHandler

        public Bitmap AddImage(string FilePath)
        {
            UpdateImageInIH();
            if (m_hhPrimaryHistoryHandler == null)
                m_hhPrimaryHistoryHandler = new HistoryHandler(); //Create a new history handler
            else
                m_hhPrimaryHistoryHandler.Reset(); //Reset instead of recreate for speed
            //Original code
            //m_hhPrimaryHistoryHandler = new HistoryHandler(); //Destroy history handler and create a new one
            Global.WriteToLog("Adding image " + FilePath, true);
            m_ihPrimaryImageHandler.AddImage(FilePath);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.LoadImage, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;

        }

        public Bitmap RemoveImage()
        {
            UpdateImageInIH();
            Global.WriteToLog("Removing Current Image", true);
            return RemoveImage(m_ihPrimaryImageHandler.CurrentImageIndex);
        }

        public Bitmap RemoveImage(int index)
        {
            UpdateImageInIH();
            Global.WriteToLog("Removing Image at " + index, true);
            m_ihPrimaryImageHandler.RemoveImage(index);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        public void SaveImage(string FilePath)
        {
            UpdateImageInIH();
            Global.WriteToLog("Saving Image to " + FilePath, true);
            m_ihPrimaryImageHandler.SaveImage(FilePath);
        }

        public Bitmap SwitchImage(int iIndex)
        {
            UpdateImageInIH();
            Global.WriteToLog("Switching image to " + iIndex, true);
            m_ihPrimaryImageHandler.SwitchImages(iIndex);
            m_hhPrimaryHistoryHandler = new HistoryHandler();
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.SwitchImage, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        public Bitmap NextImage()
        {
            Global.WriteToLog("Switching to next image", true);
            SwitchImage(m_ihPrimaryImageHandler.CurrentImageIndex + 1);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        public Bitmap PreviousImage()
        {
            Global.WriteToLog("Switching to previous image", true);
            SwitchImage(m_ihPrimaryImageHandler.CurrentImageIndex - 1);
            return m_ihPrimaryImageHandler.CurrentImage;
        }
        
        public Bitmap CurrentImage()
        {
        	Global.WriteToLog("Retriving current image", true);
        	return m_ihPrimaryImageHandler.CurrentImage;
        }

        public Bitmap GetImageAtIndex(int Index)
        {
            Global.WriteToLog("Getting image at index " + Index, true);
            return m_ihPrimaryImageHandler.GetImageAtIndex(Index);            
        }

        public int CurrentImageIndex()
        {
        	Global.WriteToLog("Retriving current image index", false);
        	return m_ihPrimaryImageHandler.CurrentImageIndex;
        }
        public int ImageCount()
        {
        	Global.WriteToLog("Retriving image count", false);
        	return m_ihPrimaryImageHandler.ImageCount;
        }

        public void ChangeCachePercentage(int Percentage)
        {
            Global.WriteToLog("Changing cache percentage to " + Percentage, true);
            m_ihPrimaryImageHandler.iMemPercentageForCache = Percentage;
        }

        /// <summary>
        /// Forces a cache check to occure. Returns if the cache was turned on or not.
        /// </summary>
        /// <returns>Whether the image disk cache is enabled or disabled</returns>
        public bool ForceCacheCheck()
        {
            Global.WriteToLog("Forcing a disk cache check to see if it should be enabled\\disabled", true, false);
            m_ihPrimaryImageHandler.DetermineIfShouldCache();
            return m_ihPrimaryImageHandler.bCacheEnabled;
        }

        public int GetLoadedImageCount()
        {
            return m_ihPrimaryImageHandler.ImageCount;
        }

        public int GetSelectedIndex()
        {
            return m_ihPrimaryImageHandler.CurrentImageIndex;
        }

        #endregion

        #region HLS

        /// <summary>
        /// Pass hue operation to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Bitmap Hue(float value)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Hue to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.HLS.ChangeHue(m_ihPrimaryImageHandler.CurrentImage, value);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Hue, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// Pass saturation to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Bitmap Saturation(float value)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Saturation to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.HLS.ChangeSaturation(m_ihPrimaryImageHandler.CurrentImage, value);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Saturation, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// Pass luminosity to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Bitmap Luminosity(float value)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Luminosity to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.HLS.ChangeLuminosity(m_ihPrimaryImageHandler.CurrentImage, value);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Luminosity, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        #endregion

        #region RGB

        /// <summary>
        /// passes brightness to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Bitmap Brightnes(float value)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Brightness to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.RGB.Brightness(m_ihPrimaryImageHandler.CurrentImage, value);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Brightness, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass gamma to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Bitmap Gamma(float r, float g, float b)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Gamma to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.RGB.Gamma(m_ihPrimaryImageHandler.CurrentImage, r, g, b);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Gamma, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass grayscale to core
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap Grayscale()
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Grayscale to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.RGB.Grayscale(m_ihPrimaryImageHandler.CurrentImage);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Grayscale, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }
        
        /// <summary>
        /// pass atkinson dither to core
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap Dither()
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Atkinson Dither to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.RGB.Dither_Atkinson(m_ihPrimaryImageHandler.CurrentImage);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Dither, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }
        /// <summary>
        /// pass sepia to core
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap Sepia()
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Sepia to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.RGB.Sepia(m_ihPrimaryImageHandler.CurrentImage);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Sepia, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass invert to core
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap Invert()
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Invert to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.RGB.Invert(m_ihPrimaryImageHandler.CurrentImage);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Invert, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        #endregion

        #region Basic

        /// <summary>
        /// pass resize to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Bitmap ResizeByPixels(int x, int y)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Resize in Pixels to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.ResizeByPixels(m_ihPrimaryImageHandler.CurrentImage, x, y);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.ResizePixels, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass resize in inches to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Bitmap ResizeByInches(float x, float y)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Resize in Inches to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.ResizeByInches(m_ihPrimaryImageHandler.CurrentImage, x, y);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.ResizeInches, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass resize in centemeters to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Bitmap ResizeByCentimeters(float x, float y)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Resize in Centimeters to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.ResizeByCentimeters(m_ihPrimaryImageHandler.CurrentImage, x, y);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.ResizeCentimeters, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass resize by percent to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public Bitmap ResizeByPercent(int percent)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Resize in Percentage to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.ResizeByPercent(m_ihPrimaryImageHandler.CurrentImage, percent);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.ResizePercent, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass flip to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public Bitmap Flip(Core.BasicOps.FlipAxis axis)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Flip to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.Flip(m_ihPrimaryImageHandler.CurrentImage, axis);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Flip, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass rotation to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Bitmap Rotate(Core.BasicOps.RotateDirection dir)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Rotate to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.Rotate(m_ihPrimaryImageHandler.CurrentImage, dir);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Rotate, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass crop to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="CropArea"></param>
        /// <returns></returns>
        public Bitmap Crop(Rectangle CropArea)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Crop to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.BasicOps.Crop(m_ihPrimaryImageHandler.CurrentImage, CropArea);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.Crop, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        #endregion

        #region Other

        /// <summary>
        /// pass red eye to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="CropArea"></param>
        /// <returns></returns>
        public Bitmap RedEye(Rectangle CropArea)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Red Eye Removal to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.OtherOps.RedEyeRemoval(m_ihPrimaryImageHandler.CurrentImage, CropArea);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.RemoveRedEye, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        /// <summary>
        /// pass add text to core
        /// </summary>
        /// <param name="image"></param>
        /// <param name="TextArea"></param>
        /// <param name="Text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Bitmap AddText(Rectangle TextArea, string Text, Font font, Brush color)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Add Text to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.OtherOps.AddCaption(m_ihPrimaryImageHandler.CurrentImage, TextArea, Text, font, color);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.AddText, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        public Bitmap Chromakey(Bitmap background, Bitmap forground, float Sensitivity)
        {
            UpdateImageInIH();
            Global.WriteToLog("Applying Chromakey to image", true);
            m_ihPrimaryImageHandler.CurrentImage = Core.OtherOps.ChromaKey(forground, background, Sensitivity);
            m_hhPrimaryHistoryHandler.AddOperation(Constants.Operations.ChromaKey, m_ihPrimaryImageHandler.CurrentImage);
            return m_ihPrimaryImageHandler.CurrentImage;
        }

        #endregion
    }
}
