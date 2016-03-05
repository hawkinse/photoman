using System;
using System.Collections.Generic;
using System.Drawing;

namespace Photoman.Controller
{
	/// <summary>
	/// Description of HistoryHandler.
	/// </summary>
	public class HistoryHandler
	{
		private List<HistoryObject> m_hoHistory = new List<HistoryObject>();
		
		//Index. Use getter to ensure proper releasing of images.
		private int m_iIndex_DO_NOT_USE_DIRECTLY = -1;
		private int m_iIndex
		{
			get { return m_iIndex_DO_NOT_USE_DIRECTLY;}
			set
			{
				try
				{
					//Check to not go out of index
					if((value >= 0) && (value < m_hoHistory.Count))
					{	
						//Previous indicies
						int iOldPreviousIndex = m_iIndex_DO_NOT_USE_DIRECTLY - 1;
						int iOldCurrentIndex = m_iIndex_DO_NOT_USE_DIRECTLY;
						int iOldNextIndex = m_iIndex_DO_NOT_USE_DIRECTLY + 1;
						
						//Set value
						m_iIndex_DO_NOT_USE_DIRECTLY = value;
						
						//Check if any of the previous indicies are no longer in use. Caches them out if so.
						if(iOldPreviousIndex >= 0 && iOldPreviousIndex < m_hoHistory.Count - 1)
							if(!(value - 1 == iOldPreviousIndex || value == iOldPreviousIndex || value + 1 == iOldPreviousIndex))
								m_hoHistory[iOldPreviousIndex].CacheImage();
						
						if(iOldCurrentIndex >= 0 && iOldCurrentIndex < m_hoHistory.Count - 1)
							if(!(value - 1 == iOldCurrentIndex || value == iOldCurrentIndex || value + 1 == iOldCurrentIndex))
								m_hoHistory[iOldCurrentIndex].CacheImage();
						
						if(iOldNextIndex >= 0 && iOldNextIndex < m_hoHistory.Count - 1)
							if(!(value - 1 == iOldNextIndex || value == iOldNextIndex || value + 1 == iOldNextIndex))
								m_hoHistory[iOldNextIndex].CacheImage();
					}
				}
				catch(Exception ex)
				{
					Global.WriteToLog(ex);
				}
			}
		}
		
		internal int Index
		{
			get { return m_iIndex;}
		}

        internal int HistoryCount
        {
            get { return m_hoHistory.Count; }
        }

		internal Bitmap CurrentBitmap
		{
			get
			{
				//Return a clone of the image so modifying it doesnt wreck havoc on history state
                if (Index < m_hoHistory.Count && Index >= 0)
                    return (Bitmap)m_hoHistory[Index].ImageData.Clone();
                else
                {
                    Global.WriteToLog("History data does not exist at this index. Returning a blank 1x1 bitmap", true);
                    return new Bitmap(1, 1);
                }
			}
		}
		
		//ID for cache paths. use getter to always have a unique ID
		private static int m_iID_DO_NOT_USE_DIRECTLY = 0;
		private int m_iID
		{
			get
			{
				m_iID_DO_NOT_USE_DIRECTLY++;
				return m_iID_DO_NOT_USE_DIRECTLY;
			}
		}
		
		public HistoryHandler()
		{
			try
			{
				//Delete old cache directory
            	if (System.IO.Directory.Exists(Constants.strHistPath))
            	{
                	Global.WriteToLog("Emptying history cache", true);
               	 	string[] files = System.IO.Directory.GetFiles(Constants.strHistPath);
               	 	foreach (string file in files)
                	    System.IO.File.Delete(file);                     
            	}
			}
			catch(Exception ex)
			{
				Global.WriteToLog(ex);
			}
		}

        ~HistoryHandler()
        {
            //Delete old cache directory
            if (System.IO.Directory.Exists(Constants.strHistPath))
            {
                //Write to log but not console since constructors are on a different thread?
                Global.WriteToLog("Emptying history cache", false);
                string[] files = System.IO.Directory.GetFiles(Constants.strHistPath);
                foreach (string file in files)
                    System.IO.File.Delete(file);
            }
        }

		
		internal void AddOperation(Constants.Operations Op, Bitmap bmpData)
		{
			try
			{
			if(m_iIndex != -1)
			{					
					//Simple loop to remove all after current point
					while(true)
					{
						//Check that index is greater than 1 because if its 0 it'll remove the first object,
						//and below that is out of range
						if(m_hoHistory.Count - 1 > m_iIndex && (m_iIndex >= 0 && m_hoHistory.Count > 1))
							m_hoHistory.RemoveAt(m_hoHistory.Count - 1);
						else
							break;
					}
				}			
			
				m_hoHistory.Add(new HistoryObject(bmpData, Op, Constants.strHistPath + "\\" + m_iID.ToString()));
			
				m_iIndex = m_hoHistory.Count - 1;
			}
			catch(Exception ex)
			{
				Global.WriteToLog(ex);
			}
		}
		
		internal Bitmap Undo()
		{
			m_iIndex--;
			return CurrentBitmap;
		}
		
		internal Bitmap Redo()
		{
			m_iIndex++;	
			return CurrentBitmap;
		}

        /// <summary>
        /// Resets the history handler so we dont have to keep recreating it
        /// </summary>
        internal void Reset()
        {
            try
            {
                m_hoHistory.Clear();
                m_iIndex_DO_NOT_USE_DIRECTLY = -1;

                //Delete old cache directory
                if (System.IO.Directory.Exists(Constants.strHistPath))
                {
                    Global.WriteToLog("Emptying history cache", true);
                    string[] files = System.IO.Directory.GetFiles(Constants.strHistPath);
                    foreach (string file in files)
                        System.IO.File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
        }
	}
}
