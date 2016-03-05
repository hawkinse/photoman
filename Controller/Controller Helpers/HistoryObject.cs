using System;
using System.Drawing;
using System.IO;

namespace Photoman.Controller
{
	public class HistoryObject
	{
		private Bitmap m_bmpBitmap;
		private bool m_bIsCached = false;
		private Constants.Operations m_Op;
		private string m_strCachePath = "";
		
		internal Bitmap ImageData
		{
			get
			{
				//Uncache image if cached
				if(m_bIsCached)
					UnCacheImage();
				//Return image data
				return m_bmpBitmap;
			}
			set
			{
				//Set image data and set cache flag to false
				m_bmpBitmap = value;
				m_bIsCached = false;
			}
		
		}
		
		internal Constants.Operations Operation
		{
			get { return m_Op;}
		}
		
		internal bool IsCached
		{
			get { return m_bIsCached;}
		}
		
		internal string CachePath
		{
			get { return m_strCachePath;}
		}
		
		internal HistoryObject(Bitmap bmpImage, Constants.Operations Op, string CachePath)
		{
			m_bmpBitmap = (Bitmap)bmpImage.Clone();
			m_bIsCached = false;
			m_Op = Op;	
			m_strCachePath = CachePath;
		}
		
		internal void CacheImage()
		{
			//Make sure we're not already cached
			if(!m_bIsCached)
			{
				try
				{
					//check if directory for cache path exists and create it if not
					if(!System.IO.Directory.Exists(Constants.strHistPath))
						System.IO.Directory.CreateDirectory(Constants.strHistPath);
			
					//Save off image
					m_bmpBitmap.Save(m_strCachePath);
					
					//Clear current image from ram
					m_bmpBitmap = null;
					m_bIsCached = true;	
				}
				catch(Exception ex)
				{
					Global.WriteToLog(ex);
				}
			}
		}
		
		private void UnCacheImage()
		{
			//Make sure we're not already loaded
			if(m_bIsCached)
			{
				try
				{
					//Load in image from cache
					FileStream fs = new FileStream(m_strCachePath, FileMode.Open, FileAccess.Read);
					Image imgTemp = Image.FromStream(fs);
					m_bmpBitmap = new Bitmap(imgTemp);
					imgTemp.Dispose();
					fs.Close();
				
					m_bIsCached = false;
				}
				catch(Exception ex)
				{
					Global.WriteToLog(ex);
				}
			}
		}
		
	}
}