using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Photoman.UserInterface.UIWidgets;

namespace Photoman.UserInterface
{
    /// <summary>
    /// This class is the back end for TestGUI.cs
    /// </summary>
    class TestGUI_Controller
    {
        //Controller and form references
        Controller.Controller m_Controller;
        TestGUI m_frmMain;
        uiwPreviewPopup m_ppMain;

        //Ints for sliders
        int iRedSlider = 128;
        int iBlueSlider = 128;
        int iGreenSlider = 128;
        int iHueSlider = 180;
        int iLumSlider = 120;
        int iSatSlider = 5;
        int iBrightSlider = 128;

        //Bitmap to do all editing on
        private Bitmap m_bmpOutput;
        private Bitmap bmpOutput
        {
            get
            {
                return m_bmpOutput;
            }

            set
            {
                m_bmpOutput = value;
                m_frmMain.pbOutput.Image = m_bmpOutput;
                m_frmMain.uitlMain.UpdateElement(m_bmpOutput);               

                
            }
        }

        public TestGUI_Controller(Controller.Controller controller)
        {
            m_Controller = controller;
            m_frmMain = new TestGUI();
            m_frmMain.controller = this;

            //m_ppMain = new uiwPreviewPopup();
            //m_frmMain.Controls.Add(m_ppMain);
            //m_ppMain.BringToFront();
            m_ppMain = m_frmMain.uippMain;

            //run startup logic
            StartupLogic();
        }

        private void StartupLogic()
        {            
            //Wire up thumbnail index change event
            m_frmMain.uitlMain.IndexChanged += new IndexChangedHandler(SwitchImageFromList);
            //Wire up chromakey apply widget
            m_frmMain.uiChromaKey.ApplyClicked += new Photoman.UserInterface.UIWidgets.ApplyClickedHandler(ChromaKey);
            //Wire up chromakey data drag event
            m_frmMain.uiChromaKey.ValidImageDataDragged += new Photoman.UserInterface.UIWidgets.ValidImageDataDraggedHandler(Chromakey_DataDragged);
            
            //Wire up slider drag events
            m_frmMain.itRed.TrackbarDragged += new OnTrackbarDragged(Preveiw_RedSlider);
            m_frmMain.itGreen.TrackbarDragged += new OnTrackbarDragged(Preveiw_GreenSlider);
            m_frmMain.itBlue.TrackbarDragged += new OnTrackbarDragged(Preveiw_BlueSlider);
            m_frmMain.itBrightness.TrackbarDragged += new OnTrackbarDragged(Preveiw_BrightnessSlider);
            m_frmMain.itHue.TrackbarDragged += new OnTrackbarDragged(Preview_HueSlider);
            m_frmMain.itLuminosity.TrackbarDragged += new OnTrackbarDragged(Preview_LuminositySlider);
            m_frmMain.itSaturation.TrackbarDragged += new OnTrackbarDragged(Preview_SaturationSlider);
            
            //show form

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                Global.WriteToLog("Control key held. Launching debug GUI", true, true);
                m_frmMain.Show();
            }
            else
            {
                Global.WriteToLog("Control key not held. Launching final GUI", true, true);

                MainGUI_Controller mgc = new MainGUI_Controller(m_Controller);
                MainGUI frmMainGUI = new MainGUI();
                frmMainGUI.controller = mgc;
            }

            //DialogResult dr = MessageBox.Show("Yes for Debug GUI, No for Final GUI", "Pick a GUI", MessageBoxButtons.YesNo);
            //if (dr == DialogResult.Yes)
            //    m_frmMain.Show();
            //else
            //{
            //    MainGUI_Controller mgc = new MainGUI_Controller(m_Controller);
            //    MainGUI frmMainGUI = new MainGUI();
            //    frmMainGUI.controller = mgc;

            //    //frmMainGUI.Show();
            //}
        }

        internal void ExitLogic()
        {
            //Quit application. Must be done this way due to origin as console app.
            Application.Exit();
        }
        
        #region Image Handling

        internal void AddImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select an image";
            ofd.Multiselect = true;            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string Filename in ofd.FileNames)
                {
                    //Gets file name without ext. for thumbnail list
                    System.IO.FileInfo fi = new System.IO.FileInfo(Filename);

                    m_frmMain.uitlMain.AddElement((Bitmap)Image.FromFile(Filename), fi.Name);
                    bmpOutput = m_Controller.AddImage(Filename);
                    m_frmMain.uitlMain.SetIndex(m_frmMain.uitlMain.Count - 1);

                    //Set image hue and sat slider
                    m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
                    m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);

                    //Add to thumbnail widget
                }
                
            }
        }

        internal void RemoveImage()
        {           
            bmpOutput = m_Controller.RemoveImage();
            m_frmMain.uitlMain.RemoveElement();
        }

        internal void SaveImage()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Select a path to save to";

            if (sfd.ShowDialog() == DialogResult.OK)
                m_Controller.SaveImage(sfd.FileName);
        }

        internal void PreviousImage()
        {
            m_frmMain.uitlMain.SetIndex(m_frmMain.uitlMain.Index - 1);

            //bmpOutput = m_Controller.PreviousImage();
            //m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
            //m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);
        }

        internal void NextImage()
        {
            m_frmMain.uitlMain.SetIndex(m_frmMain.uitlMain.Index + 1);
            //bmpOutput = m_Controller.NextImage();
            //m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
            //m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);
        }

        internal void SwitchImageFromList()
        {
            bmpOutput = m_Controller.SwitchImage(m_frmMain.uitlMain.Index);
            m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
            m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);
        }


        internal void SetCachePercentage()
        {
            m_Controller.ChangeCachePercentage(Convert.ToInt32(m_frmMain.txtCachePercentage.Text));
        }


        internal void SetSliderStart(uiImageTrackbar.TrackbarColorType tct)
        {
            switch(tct)
            {
                case uiImageTrackbar.TrackbarColorType.Red:
                    iRedSlider = m_frmMain.itRed.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Blue:
                    iBlueSlider = m_frmMain.itBlue.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Green:
                    iGreenSlider = m_frmMain.itGreen.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Hue:
                    iHueSlider = m_frmMain.itHue.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Luminosity:
                    iLumSlider = m_frmMain.itLuminosity.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Saturation:
                    iLumSlider = m_frmMain.itSaturation.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Brightness:
                    iBrightSlider = m_frmMain.itBrightness.tbMain.Value;
                    break;
                default:
                    break;
            }
        }

        internal void SetSliderStop(uiImageTrackbar.TrackbarColorType tct)
        {
            switch (tct)
            {
                case uiImageTrackbar.TrackbarColorType.Red:
                    if (m_frmMain.rbDoSingle.Checked)
                        bmpOutput = m_Controller.Gamma((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1,
                                                            1,1);
                    else if (m_frmMain.rbAddToAuto.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1);
                        ArgList.Add(1);
                        ArgList.Add(1);
                        m_Controller.Automation_AddOperation(Constants.Operations.Gamma, ArgList);
                    }
                    else if (m_frmMain.rbBatch.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1);
                        ArgList.Add(1);
                        ArgList.Add(1);
                        bmpOutput = m_Controller.BatchOp(Constants.Operations.Gamma, ArgList);
                    }
                    iRedSlider = m_frmMain.itRed.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Blue:
                    if (m_frmMain.rbDoSingle.Checked)
                        bmpOutput = m_Controller.Gamma(1,1,(iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1);
                    else if (m_frmMain.rbAddToAuto.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(1);
                        ArgList.Add(1);
                        ArgList.Add((iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1);
                        
                        m_Controller.Automation_AddOperation(Constants.Operations.Gamma, ArgList);
                    }
                    else if (m_frmMain.rbBatch.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(1);
                        ArgList.Add(1);
                        ArgList.Add((iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1);
                        
                        bmpOutput = m_Controller.BatchOp(Constants.Operations.Gamma, ArgList);
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Green:
                    if (m_frmMain.rbDoSingle.Checked)
                        bmpOutput = m_Controller.Gamma(1, (iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1, 1);
                    else if (m_frmMain.rbAddToAuto.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(1);                        
                        ArgList.Add((iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1);
                        ArgList.Add(1);

                        m_Controller.Automation_AddOperation(Constants.Operations.Gamma, ArgList);
                    }
                    else if (m_frmMain.rbBatch.Checked)
                    {
                        List<object> ArgList = new List<object>();                        
                        ArgList.Add(1);
                        ArgList.Add((iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1);
                        ArgList.Add(1);

                        bmpOutput = m_Controller.BatchOp(Constants.Operations.Gamma, ArgList);
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Hue:
                    if (m_frmMain.rbDoSingle.Checked)
                        bmpOutput = m_Controller.Hue(m_frmMain.itHue.tbMain.Value - iHueSlider);
                    else if (m_frmMain.rbAddToAuto.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(m_frmMain.itHue.tbMain.Value - iHueSlider);
                        m_Controller.Automation_AddOperation(Constants.Operations.Hue, ArgList);
                    }
                    else if (m_frmMain.rbBatch.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(m_frmMain.itHue.tbMain.Value - iHueSlider);
                        bmpOutput = m_Controller.BatchOp(Constants.Operations.Hue, ArgList);
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Luminosity:
                    if (m_frmMain.rbDoSingle.Checked)
                        bmpOutput = m_Controller.Luminosity((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f);
                    else if (m_frmMain.rbAddToAuto.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f);
                        m_Controller.Automation_AddOperation(Constants.Operations.Luminosity, ArgList);
                    }
                    else if (m_frmMain.rbBatch.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f);
                        bmpOutput = m_Controller.BatchOp(Constants.Operations.Luminosity, ArgList);
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Saturation:
                    if (m_frmMain.rbDoSingle.Checked)
                        bmpOutput = m_Controller.Saturation(((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
                    else if (m_frmMain.rbAddToAuto.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
                        m_Controller.Automation_AddOperation(Constants.Operations.Saturation, ArgList);
                    }
                    else if (m_frmMain.rbBatch.Checked)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
                        bmpOutput = m_Controller.BatchOp(Constants.Operations.Saturation, ArgList);
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Brightness:
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Image Operations

        internal void Hue()
        {
        	if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Hue( (float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        		m_Controller.Automation_AddOperation( Constants.Operations.Hue, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Hue, ArgList);
        	}
        }

        internal void Luminosity()
        {
        	if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Luminosity( (float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        		m_Controller.Automation_AddOperation( Constants.Operations.Luminosity, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Luminosity, ArgList);
        	}
        }

        internal void Saturation()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Saturation( (float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        		m_Controller.Automation_AddOperation( Constants.Operations.Saturation, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Saturation, ArgList);
        	}
        }

        internal void Brightness()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Brightnes( (float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
        		m_Controller.Automation_AddOperation( Constants.Operations.Brightness, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Brightness, ArgList);
        	}
        }

        internal void Gamma()
        {
           if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Gamma( (float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text),
                                                    (float)Convert.ToDouble(m_frmMain.txtRGB_Green.Text),
                                                    (float)Convert.ToDouble(m_frmMain.txtRGB_Blue.Text));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Green.Text));
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Blue.Text));
        		m_Controller.Automation_AddOperation( Constants.Operations.Gamma, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Green.Text));
        		ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Blue.Text));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Gamma, ArgList);
        	}
        }

        internal void Grayscale()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Grayscale();
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		m_Controller.Automation_AddOperation( Constants.Operations.Grayscale, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Grayscale, ArgList);
        	}
        }

        internal void Dither()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Dither();
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		m_Controller.Automation_AddOperation( Constants.Operations.Dither, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Dither, ArgList);
        	}
        }

        internal void Sepia()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Sepia();
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		m_Controller.Automation_AddOperation( Constants.Operations.Sepia, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Sepia, ArgList);
        	}
        }

        internal void Invert()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Invert();
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		m_Controller.Automation_AddOperation( Constants.Operations.Invert, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Invert, ArgList);
        	}
        }

        internal void Resize()
        {
        	if(m_frmMain.rbDoSingle.Checked)
            {
            	if (m_frmMain.rbPixels.Checked)
            	{
                	bmpOutput = m_Controller.ResizeByPixels(Convert.ToInt32(m_frmMain.txtSizeX.Text),
                                                            Convert.ToInt32(m_frmMain.txtSizeY.Text));
            	}
            	else if (m_frmMain.rbPercentage.Checked)
            	{
              	  bmpOutput = m_Controller.ResizeByPercent(Convert.ToInt32(m_frmMain.txtSizeX.Text));
            	}
            	else if (m_frmMain.rbInches.Checked)
            	{
                	bmpOutput = m_Controller.ResizeByInches(Convert.ToInt32(m_frmMain.txtSizeX.Text),
                                                            Convert.ToInt32(m_frmMain.txtSizeY.Text));
            	}
            	else if(m_frmMain.rbCentimeters.Checked)
            	{
                	bmpOutput = m_Controller.ResizeByCentimeters(Convert.ToInt32(m_frmMain.txtSizeX.Text),
                                                                 Convert.ToInt32(m_frmMain.txtSizeY.Text));
            	}
            }
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Convert.ToInt32(m_frmMain.txtSizeX.Text));
        		ArgList.Add(Convert.ToInt32(m_frmMain.txtSizeY.Text));
        		
        		if(m_frmMain.rbPixels.Checked)
        			m_Controller.Automation_AddOperation( Constants.Operations.ResizePixels, ArgList);
        		else if(m_frmMain.rbPercentage.Checked)
        			m_Controller.Automation_AddOperation(Constants.Operations.ResizePercent, ArgList);
        		else if(m_frmMain.rbInches.Checked)
        			m_Controller.Automation_AddOperation( Constants.Operations.ResizeInches, ArgList);
        		else if(m_frmMain.rbCentimeters.Checked)
        			m_Controller.Automation_AddOperation( Constants.Operations.ResizeCentimeters, ArgList);
        		
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Convert.ToInt32(m_frmMain.txtSizeX.Text));
        		ArgList.Add(Convert.ToInt32(m_frmMain.txtSizeY.Text));
        		
        		if(m_frmMain.rbPixels.Checked)
        			bmpOutput = m_Controller.BatchOp( Constants.Operations.ResizePixels, ArgList);
        		else if(m_frmMain.rbPercentage.Checked)
        			bmpOutput = m_Controller.BatchOp(Constants.Operations.ResizePercent, ArgList);
        		else if(m_frmMain.rbInches.Checked)
        			bmpOutput = m_Controller.BatchOp( Constants.Operations.ResizeInches, ArgList);
        		else if(m_frmMain.rbCentimeters.Checked)
        			bmpOutput = m_Controller.BatchOp( Constants.Operations.ResizeCentimeters, ArgList);
        	}
        }

        internal void Crop()
        {
        	if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.Crop(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtCropUL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropLR.Text)
                                                                   ));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtCropUL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropLR.Text)
                                                                   ));
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.Crop, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtCropUL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtCropLR.Text)
                                                                   ));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Crop, ArgList);
        	}
            
        }

        internal void FlipL()
        {
            if(m_frmMain.rbDoSingle.Checked)
            	bmpOutput = m_Controller.Flip(Photoman.Core.BasicOps.FlipAxis.X);
            
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.FlipAxis.X);
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.Flip, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.FlipAxis.X);
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Flip, ArgList);
        	}
        }

        internal void FlipR()
        {
            if(m_frmMain.rbDoSingle.Checked)
            	bmpOutput = m_Controller.Flip(Photoman.Core.BasicOps.FlipAxis.Y);
            
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.FlipAxis.Y);
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.Flip, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.FlipAxis.Y);
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Flip, ArgList);
        	}
        }

        internal void RotateL()
        {
           if(m_frmMain.rbDoSingle.Checked)
            	bmpOutput = m_Controller.Rotate(Photoman.Core.BasicOps.RotateDirection.Left);
            
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Left);
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.Rotate, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Left);
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Rotate, ArgList);
        	}
        }

        internal void RotateR()
        {
        	if(m_frmMain.rbDoSingle.Checked)
            	bmpOutput = m_Controller.Rotate(Photoman.Core.BasicOps.RotateDirection.Right);
            
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Right);
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.Rotate, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Right);
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.Rotate, ArgList);
        	}
        }

        internal void RedEye()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.RedEye(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLR.Text)
                                                                   ));
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLR.Text)
                                                                   ));
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.RemoveRedEye, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLR.Text)
                                                                   ));
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.RemoveRedEye, ArgList);
        	}
        }

        internal void ChromaKey()
        {
            if (m_frmMain.rbDoSingle.Checked)
            {
                bmpOutput = m_Controller.Chromakey(m_frmMain.uiChromaKey.BackgroundImage, bmpOutput, m_frmMain.uiChromaKey.Sensitivity);
            }
            else if (m_frmMain.rbAddToAuto.Checked)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(m_frmMain.uiChromaKey.BackgroundImage);
                ArgList.Add(bmpOutput);
                ArgList.Add(m_frmMain.uiChromaKey.Sensitivity);

                m_Controller.Automation_AddOperation(Constants.Operations.ChromaKey, ArgList);
            }
            else if (m_frmMain.rbBatch.Checked)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(m_frmMain.uiChromaKey.BackgroundImage);
                ArgList.Add(bmpOutput);
                ArgList.Add(m_frmMain.uiChromaKey.Sensitivity);

                m_Controller.BatchOp(Constants.Operations.ChromaKey, ArgList);
            }
        }

        internal void AddText()
        {
            if(m_frmMain.rbDoSingle.Checked)
        		bmpOutput = m_Controller.AddText(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtOtherUL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLR.Text)
                                                                   ), "Hello new caption!", new Font(new FontFamily("Courier New"),12),
                                                                   Brushes.Black);
        	else if(m_frmMain.rbAddToAuto.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtOtherUL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLR.Text)
                                                                   ));
        		ArgList.Add((string)"Hello new caption!");
        		ArgList.Add(new Font(new FontFamily("Courier New"), 12));
        		ArgList.Add((int)12);
        		
        		m_Controller.Automation_AddOperation( Constants.Operations.AddText, ArgList);
        	}
        	else if(m_frmMain.rbBatch.Checked)
        	{
        		List<object> ArgList = new List<object>();
        		ArgList.Add(new Rectangle(
                                                                    Convert.ToInt32(m_frmMain.txtOtherUL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherUR.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLL.Text),
                                                                    Convert.ToInt32(m_frmMain.txtOtherLR.Text)
                                                                   ));
        		ArgList.Add((string)"Hello new caption!");
        		ArgList.Add(new Font(new FontFamily("Courier New"), 12));
        		ArgList.Add((int)12);
        		bmpOutput = m_Controller.BatchOp( Constants.Operations.AddText, ArgList);
        	}
        }
        
        internal void StartAutomation()
        {
        	bmpOutput = m_Controller.Automation_Start();
        }
        
        #endregion

        #region ImagePreview stuff

        public void HidePreview()
        {
            if(m_ppMain.MouseoverPopup)
                m_ppMain.Hide();
        }

        internal void Preview_HueSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.itHue.tbMain.Value - iHueSlider));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Hue, ArgList);
        }

        internal void Preview_LuminositySlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Luminosity, ArgList);
        }

        internal void Preview_SaturationSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Saturation, ArgList);
        }

        internal void Preveiw_BrightnessSlider()
        {

            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((iBrightSlider - m_frmMain.itBrightness.tbMain.Value) * 1));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Brightness, ArgList);
        }

        internal void Preveiw_RedSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1));
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble(1));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preveiw_GreenSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble((iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1));
            ArgList.Add((float)Convert.ToDouble(1));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preveiw_BlueSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble((iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preview_Hue()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Hue, ArgList);
        }

        internal void Preview_Luminosity()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Luminosity, ArgList);
        }

        internal void Preview_Saturation()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtHLSValue.Text));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Saturation, ArgList);
        }

        internal void Preveiw_Brightness()
        {

            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Brightness, ArgList);
        }

        internal void Preveiw_Gamma()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Red.Text));
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Green.Text));
            ArgList.Add((float)Convert.ToDouble(m_frmMain.txtRGB_Blue.Text));
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preview_Grayscale()
        {
            List<object> ArgList = new List<object>();
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Grayscale, ArgList);
        }

        internal void Preview_Dither()
        {
            List<object> ArgList = new List<object>();
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Dither, ArgList);
        }

        internal void Preview_Sepia()
        {
            List<object> ArgList = new List<object>();
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Sepia, ArgList);
        }

        internal void Preview_Invert()
        {
            List<object> ArgList = new List<object>();
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Invert, ArgList);
        }

        internal void Preview_FlipL()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.FlipAxis.X);
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Flip, ArgList);
        }

        internal void Preview_FlipR()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.FlipAxis.Y);
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Flip, ArgList);
        }

        internal void Preview_RotateL()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Left);
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Rotate, ArgList);
        }

        internal void Preview_RotateR()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Right);
            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.Rotate, ArgList);
        }

        internal void AdjustPreviewPosition()
        {
            m_ppMain.MoveToMouse();
        }

        #endregion

        #region History handling

        internal void Undo()
        {
            bmpOutput = m_Controller.UndoImage();
        }

        internal void Redo()
        {
            bmpOutput = m_Controller.RedoImage();
        }

        #endregion

        #region Misc Functions

        internal void Chromakey_DataDragged(object sender, DragEventArgs e)
        {
            //Retrive data
            UIData.udImage_DragDrop udidd = (UIData.udImage_DragDrop)e.Data.GetData(typeof(UIData.udImage_DragDrop));

            //Replace low res data with full res data
            udidd.BitmapData = m_Controller.GetImageAtIndex(udidd.Index);
            e.Data.SetData(udidd);

            //Generate preview friendly background
            Size PreviewSize = new Size(Constants.iPreviewSize, Constants.iPreviewSize);
            Bitmap bmpScaledBackground = new Bitmap(udidd.BitmapData, Core.BasicOps.MaintainAspectRatio_DontExcedeNewSize(PreviewSize, udidd.BitmapData.Size));
                        
            //Generate preview
            List<object> ArgList = new List<object>();
            ArgList.Add(null);
            ArgList.Add(bmpScaledBackground);
            ArgList.Add(m_frmMain.uiChromaKey.Sensitivity);

            m_ppMain.SetPreviewImage(bmpOutput, Constants.Operations.ChromaKey, ArgList);
        }

        #endregion


    }
}
