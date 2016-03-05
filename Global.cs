using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Management;
using Photoman;

namespace Photoman
{
    public delegate void OnDiskCacheStatusChange(bool Status);
    public delegate void OnExceptionCountChange(int Count);

    /// <summary>
    /// This class is for the storage of all static variables and methods.
    /// </summary>
    static class Global
    {
        //bools for in use progress bar functions
        static public bool bTackPixelProgress = false;
        static public bool bTrackImgCountProgress = false;
        static public bool bTrackOpCountProgress = false;

        //Integers for current pixel of pixel count. Used for progress of individual operations        
        static public int iCurrentOp_PixelCount = 0;
        static public int iCurrentOp_PixelTotal = 0;
        
        //Integers to keep track of image count in batch operations or automation
        static public int iCurrentOp_ImgCount = 0;
        static public int iCurrentOp_ImgTotal = 0;

        //Integers to keep track of operation count in automation
        static public int iCurrentOp_OpCount = 0;
        static public int iCurrentOp_OpTotal = 0;

        //Integer to keep track of the number of exceptions
        static public int iExceptionCount = 0;

        //Bool for determining if we shoudl shut off ant marching in widgets on battery
        static public bool bDisableAntsOnBattery = true;

        //Bool to stop painting. If true, should not paint. This fixes a bug in slow image loading.
        static public bool bDontPaint = false;

        static private StringBuilder m_strLog = new StringBuilder();
        static private string m_strLogPath = "log.txt";

        //Stores the total ammount of ram in the system
        //Stored since calculation is slow, this gives a noticable speedup to loading and switching images
        static private double m_dTotalSystemMemory = 0;

        static public DebugConsole DebugOutput;

        static public OnDiskCacheStatusChange DiskCacheStatusChange;
        static public OnExceptionCountChange ExceptionCountChange;

        /// <summary>
        /// Writes a message to log. Log will be saved on exception or application exit.
        /// </summary>
        /// <param name="Message">The message to write to the log</param>
        /// <param name="OutputToConsole">Whether or not to send message to console. For heavy operations this should NOT be done as the console has a heavy performance hit</param>
        static public void WriteToLog(string Message, bool OutputToConsole)
        {
            //if (OutputToConsole)
            //    DebugOutput.WriteToConsole("-" + Message + "\n\n");//Console.WriteLine("-" + Message + "\n\n");
            if (OutputToConsole)
                DebugOutput.WriteToConsole(Message);// + "\n");

            //m_strLog.Append("-" + Message + "\n\n");
            m_strLog.Append(Message + "\n");
        }
        
        /// <summary>
        /// Writes a message to log. Log will be saved on exception or application exit.
        /// </summary>
        /// <param name="Message">The message to write to the log</param>
        /// <param name="OutputToConsole">Whether or not to send message to console. For heavy operations this should NOT be done as the console has a heavy performance hit</param>
        /// <param name="DontFixForReadability">Dont add a "-" before and two new lines after message.</param>
        static public void WriteToLog(string Message, bool OutputToConsole, bool DontFixForReadability)
        {
            //Force DontFixForReadability... it just looks bad.
            DontFixForReadability = true;

        	if(!DontFixForReadability)
        	{
                if (OutputToConsole && DebugOutput != null)
                    DebugOutput.WriteToConsole("-" + Message + "\n\n");//Console.WriteLine("-" + Message + "\n\n");

            	m_strLog.Append("-" + Message + "\n\n");
        	}
        	else
        	{
                if (OutputToConsole && DebugOutput != null)
                    DebugOutput.WriteToConsole(Message);//Console.WriteLine(Message);

            	m_strLog.Append(Message);
        	}
        
        }

        /// <summary>
        /// Writes an exception to log and saves log to disk.
        /// </summary>
        /// <param name="ex">The exception to write to the log</param>
        static public void WriteToLog(Exception ex)
        {
            iExceptionCount++;
            if (ExceptionCountChange != null)
                ExceptionCountChange(iExceptionCount);

            string MessageToWrite = "\n=========================\n" +
                                      "An exception has occured!\n" +
                                      "=========================\n" +
                                      "Exception: " + ex.Message + "\n" +
                                      "Source:    " + ex.Source + "\n" +
                                      "Method:    " + ex.TargetSite.ToString() + "\n";
            m_strLog.Append(MessageToWrite);

            //Clear the console and write the output
            if(DebugOutput != null)
                DebugOutput.WriteToConsole(MessageToWrite);//Console.WriteLine(MessageToWrite);

            //Since we're logging an exception, save log off to file
            System.IO.File.WriteAllText(m_strLogPath, m_strLog.ToString());
        }

        /// <summary>
        /// Saves the log file
        /// </summary>
        static public void SaveLog()
        {
            m_strLog.Append("\nSaving contents of log to " + m_strLogPath);
            System.IO.File.WriteAllText(m_strLogPath, m_strLog.ToString());
        }

        /// <summary>
        /// Shows or hides the console
        /// </summary>
        static public void ToggleConsole()
        {
            if (DebugOutput.Visible)
                DebugOutput.Hide();
            else
                DebugOutput.Show();
        }

        /// <summary>
        /// Checks if a given pixel contains transparency.
        /// </summary>
        /// <param name="pixel">The pixel data to check against for transparency</param>
        /// <returns></returns>
        static public bool CheckPixelTransparency(Color pixel)
        {
            if (pixel.A != 255)//Original == 0
                return true;
            return false;
        }

        /// <summary>
        /// Checks for image transparency. Required for Indexed pixel check
        /// </summary>
        /// <param name="bmpImage">The bitmap to check pixels for any transparency in the alpha layer</param>
        /// <returns></returns>
        static public bool CheckImageTransparency(Bitmap bmpSource)
        {
            //This is the ONLY time that the Core namespace will be accessed outside of the controller.
            //This may be rerouted through the controller in the future once it is written. 

            try
            {
                Photoman.Core.UnsafeBitmap newUBMP = new Photoman.Core.UnsafeBitmap(bmpSource);
                newUBMP.LockImage();

                for (int i = 0; i < bmpSource.Size.Width; i++)
                    for (int j = 0; j < bmpSource.Size.Height; j++)
                    {
                        if (newUBMP.GetPixel(i, j).A != 255)//original == 0
                        {
                            newUBMP.UnlockImage();
                            return true;
                        }
                    }
                newUBMP.UnlockImage();
                return false;
            }
            catch (Exception ex)
            {                
                Global.WriteToLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Check if the given image is in an indexed format and convert it. This is because .NET chokes on
        /// indexed images. Perhaps this should be moved to the image handler?
        /// </summary>
        /// <param name="bmpSource"></param>
        /// <returns></returns>
        static public Bitmap CheckFor_ConvertIndexedPixels(Bitmap bmpSource)
        {
            try
            {
                if (bmpSource.PixelFormat == PixelFormat.Format1bppIndexed ||
                    bmpSource.PixelFormat == PixelFormat.Format4bppIndexed ||
                    bmpSource.PixelFormat == PixelFormat.Format8bppIndexed ||
                    bmpSource.PixelFormat == PixelFormat.Indexed)
                {
                    bool bIsTransparent = CheckImageTransparency(bmpSource);
                    Bitmap bmpTemp = bmpSource.Clone(new Rectangle(0, 0, bmpSource.Width, bmpSource.Height), PixelFormat.Format64bppArgb);
                    if (bIsTransparent)
                        bmpTemp.MakeTransparent();
                    return bmpTemp;
                }
                return bmpSource;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                Global.WriteToLog("Generating blank bitmap as substitute to stop app crash", true);
                return new Bitmap(1, 1);
            }
        }

        /// <summary>
        /// Returns the ammount of memory used by the system
        /// </summary>
        /// <returns></returns>
        static public double GetUsedMemoryBySys()
        {
            double dUsedMemory = GetTotalMemory() - GetFreeMemory();
            return dUsedMemory;
        }

        private static System.Diagnostics.PerformanceCounter m_pcRamCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
        /// <summary>
        /// Returns the ammount of memory not used at all in bytes
        /// </summary>
        /// <returns></returns>
        static public double GetFreeMemory()
        {
            
            //New code. Faster but causes Out of Memory exceptions when loading in large ammounts of images.
            //return (double)(m_pcRamCounter.NextValue() * 1048576f);

            //Old code. May be a bit slow, but it works

            //Disable thumbnail painting since the ManagementObjectSearcher invokes its paint event for some reason.
            bDontPaint = true;

            double totalFree = 0;
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher(objectQuery);
            ManagementObjectCollection vals = searcher.Get();

            foreach (ManagementObject val in vals)
            {
                totalFree += System.Convert.ToDouble(val.GetPropertyValue("FreePhysicalMemory"));
            }

            //Done. Turn painting back on.
            bDontPaint = false;

            //totalFree is in Kilobytes, so convert return value to bytes
            return totalFree * 1024d;                      
        }

        //code addapted from an example at
        //http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/b634cc1e-55e2-4f47-ab4d-aeca26dd26ce
        /// <summary>
        /// Gets the total ammount of ram in the machine in bytes
        /// </summary>
        /// <returns></returns>
        static public double GetTotalMemory()
        {
            //Check if value is stored. Since this value cant change
            //during runtime we can store it after the first calculation and
            //just return it later once we have it.

            if (m_dTotalSystemMemory == 0)
            {
                //Disable thumbnail painting since the ManagementObjectSearcher invokes its paint event for some reason.
                bDontPaint = true;

                double totalCapacity = 0;
                ObjectQuery objectQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
                ManagementObjectSearcher searcher = new
                ManagementObjectSearcher(objectQuery);
                ManagementObjectCollection vals = searcher.Get();

                foreach (ManagementObject val in vals)
                {
                    totalCapacity += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));
                }

                //Done. Turn painting back on.
                bDontPaint = false;

                m_dTotalSystemMemory = totalCapacity;
            }

            return m_dTotalSystemMemory;
            //System.Diagnostics.PerformanceCounter pcRam = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            
            //return pcRam.NextValue();
        }

        static public bool IsOnBattery()
        {
            if (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Offline)
                return true;

            return false;
        }
    }
}
