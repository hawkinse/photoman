using System;
using System.Collections.Generic;
using System.Drawing;

namespace Photoman.Controller
{
	/// <summary>
	/// Description of AutomationHandler.
	/// </summary>
	public class AutomationHandler
	{
		//hook to controller
		private Controller m_Controller;
		
		private List<Constants.Operations> m_OperationList = new List<Constants.Operations>();
		private List<List<object>> m_ArgumentList = new List<List<object>>();
		
		internal Constants.Operations[] OperationList
		{
			get { return m_OperationList.ToArray();}
		}
		
		internal AutomationHandler(Controller MainController)
		{
			m_Controller = MainController;
		}
		
		internal void AddOperation(Constants.Operations Op, List<object> Arguments )
		{
			m_OperationList.Add(Op);
			m_ArgumentList.Add(Arguments);
		}
		
		internal void RemoveOperation(int Index)
		{
			m_OperationList.RemoveAt(Index);
			m_ArgumentList.RemoveAt(Index);
		}	
		
		internal void Clear()
		{
			m_OperationList.Clear();
			m_ArgumentList.Clear();
		}
		
		internal void Start(int CurrentIndex)
		{
			try
			{
                if (m_ArgumentList.Count > 0)
                {
                    //get image count
                    int iImageCount = m_Controller.ImageCount();

                    //Keep track of image count and op count
                    Global.bTrackImgCountProgress = true;
                    Global.bTrackOpCountProgress = true;
                    Global.iCurrentOp_ImgTotal = iImageCount;
                    Global.iCurrentOp_OpTotal = m_OperationList.Count;
                    Global.iCurrentOp_ImgCount = 0;
                    Global.iCurrentOp_OpCount = 0;

                    //Loop through loaded images.
                    for (int i = 0; i < iImageCount; i++)
                    {
                        //Switch to the current image
                        m_Controller.SwitchImage(i);

                        //Incriment image counter
                        Global.iCurrentOp_ImgCount++;

                        //Loop through operations
                        for (int j = 0; j < m_OperationList.Count; j++)
                        {
                            //Incriment op counter
                            Global.iCurrentOp_OpCount++;

                            Global.WriteToLog("Performing operation " + (j + 1) + " of " + m_OperationList.Count +
                                              " on image " + (i + 1) + " of " + iImageCount, true);
                            #region operations switch
                            switch (m_OperationList[j])
                            {
                                case (Constants.Operations.AddText):
                                    m_Controller.AddText((Rectangle)(m_ArgumentList[j])[0], (string)(m_ArgumentList[j])[1],
                                                         (Font)(m_ArgumentList[j])[2], (Brush)(m_ArgumentList[j])[3]);
                                    break;
                                case (Constants.Operations.Brightness):
                                    m_Controller.Brightnes((float)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.Crop):
                                    m_Controller.Crop((Rectangle)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.Dither):
                                    m_Controller.Dither();
                                    break;
                                case (Constants.Operations.Flip):
                                    m_Controller.Flip((Core.BasicOps.FlipAxis)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.Gamma):
                                    m_Controller.Gamma((float)(m_ArgumentList[j])[0], (float)(m_ArgumentList[j])[1], (float)(m_ArgumentList[j])[2]);
                                    break;
                                case (Constants.Operations.Grayscale):
                                    m_Controller.Grayscale();
                                    break;
                                case (Constants.Operations.Hue):
                                    m_Controller.Hue((float)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.Invert):
                                    m_Controller.Invert();
                                    break;
                                case (Constants.Operations.Luminosity):
                                    m_Controller.Luminosity((float)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.RemoveRedEye):
                                    m_Controller.RedEye((Rectangle)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.ResizeCentimeters):
                                    m_Controller.ResizeByCentimeters((float)(m_ArgumentList[j])[0], (float)(m_ArgumentList[j])[1]);
                                    break;
                                case (Constants.Operations.ResizeInches):
                                    m_Controller.ResizeByInches((float)(m_ArgumentList[j])[0], (float)(m_ArgumentList[j])[1]);
                                    break;
                                case (Constants.Operations.ResizePercent):
                                    m_Controller.ResizeByInches((float)(m_ArgumentList[j])[0], (float)(m_ArgumentList[j])[1]);
                                    break;
                                case (Constants.Operations.ResizePixels):
                                    m_Controller.ResizeByPixels((int)(m_ArgumentList[j])[0], (int)(m_ArgumentList[j])[1]);
                                    break;
                                case (Constants.Operations.Rotate):
                                    m_Controller.Rotate((Core.BasicOps.RotateDirection)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.Saturation):
                                    m_Controller.Saturation((float)(m_ArgumentList[j])[0]);
                                    break;
                                case (Constants.Operations.Sepia):
                                    m_Controller.Sepia();
                                    break;
                                default:
                                    throw new Exception("Unsupported automated task attempted");
                            }
                            #endregion

                        }
                    }

                    //Turn off progress tracking flags
                    Global.bTrackImgCountProgress = true;
                    Global.bTrackOpCountProgress = true;

                    //Switch back to the last selected image.
                    m_Controller.SwitchImage(CurrentIndex);
                }
                else
                    Global.WriteToLog("Not performing automation since list is empty", true);
			}
			catch(Exception ex)
			{
				Global.WriteToLog(ex);
			}
		}
	}
}
