using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Photoman.UserInterface.UIWidgets;
using System.Reflection;

namespace Photoman.UserInterface
{
    /// <summary>
    /// This class serves as the back end to MainGUI.cs
    /// and is heavily based on TestGUI_Controller.cs
    /// </summary>
    class MainGUI_Controller
    {
        //Controller and form references
        Controller.Controller m_Controller;
        MainGUI m_frmMain;

        uiwPreviewPopup m_ppSizeDimensions;
        uiwPreviewPopup m_ppColor;
        uiwPreviewPopup m_ppSpecial;

        private bool m_bAutoMode = false;
        public bool AutoMode
        {
            get { return m_bAutoMode; }
            set
            {
                m_bAutoMode = value;
                //Any other changes that need to be made go here
            }
        }

        //Ints for sliders
        int iRedSlider = 128;
        int iBlueSlider = 128;
        int iGreenSlider = 128;
        int iHueSlider = 180;
        int iLumSlider = 120;
        int iSatSlider = 5;
        int iBrightSlider = 128;

        //Variables for add text
        public string TextForAddText = "Your text goes here...";
        public string FontNameForAddText = "Arial";
        public string FontColorForAddText = "Black";
        public float FontSizeForAddText = 12;
        public bool BoldForAddText = false;
        public bool ItalicForAddText = false;
        public bool UnderlineForAddText = false;
        public bool StrikethroughForAddText = false;

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
                m_frmMain.mpbMain.WorkingImage = m_bmpOutput;
                m_frmMain.tlLoadedImages.UpdateElement(m_bmpOutput);
                m_frmMain.txtResizeWidth.Text = value.Width.ToString();
                m_frmMain.txtResizeHeight.Text = value.Height.ToString();

                m_frmMain.tslImageCount.Text = m_Controller.ImageCount() + Constants.strNumberImagesLoaded;
                m_frmMain.tslUndoRedoCount.Text = m_Controller.UndoCount() + Constants.strNumberOfUndos +
                                              m_Controller.RedoCount() + Constants.strNumberOfRedos;
            }
        }

        public MainGUI_Controller(Controller.Controller controller)
        {
            m_Controller = controller;
            m_frmMain = new MainGUI();
            m_frmMain.controller = this;

            //m_ppMain = new uiwPreviewPopup();
            //m_frmMain.Controls.Add(m_ppMain);
            //m_ppMain.BringToFront();
            m_ppSizeDimensions = m_frmMain.ppSizeDimensions;
            m_ppColor = m_frmMain.ppColor;
            m_ppSpecial = m_frmMain.ppSpecial;

            //run startup logic
            StartupLogic();
        }

        private void StartupLogic()
        {
            //Wire up thumbnail index change event
            m_frmMain.tlLoadedImages.IndexChanged += new IndexChangedHandler(SwitchImageFromList);
            //Wire up chromakey apply widget
            m_frmMain.ckMain.ApplyClicked += new Photoman.UserInterface.UIWidgets.ApplyClickedHandler(ChromaKey);
            //Wire up chromakey data drag event
            m_frmMain.ckMain.ValidImageDataDragged += new Photoman.UserInterface.UIWidgets.ValidImageDataDraggedHandler(Chromakey_DataDragged);

            //Wire up slider drag events
            m_frmMain.itRed.TrackbarDragged += new OnTrackbarDragged(Preveiw_RedSlider);
            m_frmMain.itGreen.TrackbarDragged += new OnTrackbarDragged(Preveiw_GreenSlider);
            m_frmMain.itBlue.TrackbarDragged += new OnTrackbarDragged(Preveiw_BlueSlider);
            m_frmMain.itBrightness.TrackbarDragged += new OnTrackbarDragged(Preveiw_BrightnessSlider);
            m_frmMain.itHue.TrackbarDragged += new OnTrackbarDragged(Preview_HueSlider);
            m_frmMain.itLuminosity.TrackbarDragged += new OnTrackbarDragged(Preview_LuminositySlider);
            m_frmMain.itSaturation.TrackbarDragged += new OnTrackbarDragged(Preview_SaturationSlider);

            //Wire up mouse handling events
            m_frmMain.mpbMain.ImageResized += new ImageResizedHandler(ResizeImageByMouse);

            //Populize Fonts in Add Text
            Global.WriteToLog("Loading FontFamilies for Add Text...", true, true);
            foreach (FontFamily ff in FontFamily.Families)
            {
                if(ff.IsStyleAvailable(FontStyle.Regular))
                    m_frmMain.cbFont.Items.Add(ff.Name);
            }

            //Populize Colors in Add Text
            Global.WriteToLog("Loading Colors for Add Text...", true, true);
            ArrayList alColorList = new ArrayList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                m_frmMain.cbTextColor.Items.Add(c.Name);
            }

            Global.WriteToLog("Wiring up Add Text drawing...", true, true);
            m_frmMain.cbFont.DrawItem += new DrawItemEventHandler(DrawAddTextFontSelection);
            m_frmMain.cbTextColor.DrawItem += new DrawItemEventHandler(DrawAddTextColorSelection);
            
            //Set text
            m_frmMain.cbFont.Text = "Arial";
            m_frmMain.cbTextColor.Text = "Black";

            Global.WriteToLog("Done setting up Add Text", true, true);

            Global.WriteToLog("Wiring up misc events...", true, true);
            //Wire up Disk cache status changed event
            Global.DiskCacheStatusChange += new OnDiskCacheStatusChange(DiskCacheStatusChange);
            Global.ExceptionCountChange += new OnExceptionCountChange(ExceptionCountChange);
            Global.WriteToLog("Done wiring up misc events", true, true);

            m_frmMain.Text += " - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            m_frmMain.tslImageCount.Text = m_Controller.ImageCount() + Constants.strNumberImagesLoaded;
            m_frmMain.tslUndoRedoCount.Text = m_Controller.UndoCount() + Constants.strNumberOfUndos +
                                              m_Controller.RedoCount() + Constants.strNumberOfRedos;

            Global.WriteToLog("Done with setup. Starting GUI", true, true);
            //show form
            m_frmMain.Show();
        }

        #region Image Handling

        internal void AddImage()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Title = "Select an image";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bmpStorage = new Bitmap(1, 1);

                    foreach (string Filename in ofd.FileNames)
                    {
                        try
                        {
                            //Gets file name without ext. for thumbnail list
                            System.IO.FileInfo fi = new System.IO.FileInfo(Filename);

                            m_frmMain.tlLoadedImages.AddElement((Bitmap)Image.FromFile(Filename), fi.Name);
                            bmpStorage = m_Controller.AddImage(Filename);
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "Out of memory.")
                            {
                                bool bWasCacheEnabled = m_Controller.ForceCacheCheck();
                                if (bWasCacheEnabled)
                                {
                                    //Try again now that cache is enabled
                                    //Gets file name without ext. for thumbnail list
                                    System.IO.FileInfo fi = new System.IO.FileInfo(Filename);

                                    m_frmMain.tlLoadedImages.AddElement((Bitmap)Image.FromFile(Filename), fi.Name);
                                    bmpStorage = m_Controller.AddImage(Filename);
                                }
                                else //Something has seriously gone wrong here.
                                    throw ex;
                            }
                            else
                                throw ex;                            
                        }

                    }

                    bmpOutput = bmpStorage;
                    m_frmMain.tlLoadedImages.RecalculateScrollbars();
                    m_frmMain.tlLoadedImages.SetIndex(m_frmMain.tlLoadedImages.Count - 1);

                    //Set image hue and sat slider
                    m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
                    m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);
                }

                m_frmMain.tlImageHistory.RemoveAllElements();
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

        }

        internal void RemoveImage()
        {
            bmpOutput = m_Controller.RemoveImage();
            m_frmMain.tlLoadedImages.RemoveElement();

            m_frmMain.tlImageHistory.RemoveAllElements();
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
            m_frmMain.tlLoadedImages.SetIndex(m_frmMain.tlLoadedImages.Index - 1);

            m_frmMain.tlImageHistory.RemoveAllElements();

            //bmpOutput = m_Controller.PreviousImage();
            //m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
            //m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);
        }

        internal void NextImage()
        {
            m_frmMain.tlLoadedImages.SetIndex(m_frmMain.tlLoadedImages.Index + 1);

            m_frmMain.tlImageHistory.RemoveAllElements();
            //bmpOutput = m_Controller.NextImage();
            //m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
            //m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);
        }

        internal void SwitchImageFromList()
        {
            bmpOutput = m_Controller.SwitchImage(m_frmMain.tlLoadedImages.Index);
            m_frmMain.itHue.ReInit(uiImageTrackbar.TrackbarColorType.Hue, bmpOutput);
            m_frmMain.itSaturation.ReInit(uiImageTrackbar.TrackbarColorType.Saturation, bmpOutput);

            m_frmMain.tlImageHistory.RemoveAllElements();
        }


        internal void SetCachePercentage()
        {
            m_Controller.ChangeCachePercentage((int)m_frmMain.nudCachePercentage.Value);
        }


        internal void SetSliderStart(uiImageTrackbar.TrackbarColorType tct)
        {
            switch (tct)
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
                    if (!m_bAutoMode)
                    {
                        bmpOutput = m_Controller.Gamma((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1,
                                                            1, 1);
                        m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Red");

                    }
                    else if (m_bAutoMode)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1);
                        ArgList.Add(1);
                        ArgList.Add(1);
                        m_Controller.Automation_AddOperation(Constants.Operations.Gamma, ArgList);
                        m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Red");                        
                    }
                    iRedSlider = m_frmMain.itRed.tbMain.Value;
                    break;
                case uiImageTrackbar.TrackbarColorType.Blue:
                    if (!m_bAutoMode)
                    {
                        bmpOutput = m_Controller.Gamma(1, 1, (iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1);
                        m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Blue");
                    }
                    else if (m_bAutoMode)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(1);
                        ArgList.Add(1);
                        ArgList.Add((iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1);

                        m_Controller.Automation_AddOperation(Constants.Operations.Gamma, ArgList);
                        m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Blue");
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Green:
                    if (!m_bAutoMode)
                    {
                        bmpOutput = m_Controller.Gamma(1, (iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1, 1);
                        m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Green");
                    }
                    else if (m_bAutoMode)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(1);
                        ArgList.Add((iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1);
                        ArgList.Add(1);

                        m_Controller.Automation_AddOperation(Constants.Operations.Gamma, ArgList);
                        m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Green");
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Hue:
                    if (!m_bAutoMode)
                    {
                        bmpOutput = m_Controller.Hue(m_frmMain.itHue.tbMain.Value - iHueSlider);
                        m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Hue");
                    }
                    else if (m_bAutoMode)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(m_frmMain.itHue.tbMain.Value - iHueSlider);
                        m_Controller.Automation_AddOperation(Constants.Operations.Hue, ArgList);
                        m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Hue");
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Luminosity:
                    if (!m_bAutoMode)
                    {
                        bmpOutput = m_Controller.Luminosity((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f);
                        m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Luminosity");
                    }
                    else if (m_bAutoMode)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f);
                        m_Controller.Automation_AddOperation(Constants.Operations.Luminosity, ArgList);
                        m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Luminosity");
                    }
                    break;
                case uiImageTrackbar.TrackbarColorType.Saturation:
                    if (!m_bAutoMode)
                    {
                        bmpOutput = m_Controller.Saturation(((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
                        m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Saturation");
                    }
                    else if (m_bAutoMode)
                    {
                        List<object> ArgList = new List<object>();
                        ArgList.Add(((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
                        m_Controller.Automation_AddOperation(Constants.Operations.Saturation, ArgList);
                        m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Saturation");
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

        internal void Grayscale()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Grayscale();
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Grayscale");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                m_Controller.Automation_AddOperation(Constants.Operations.Grayscale, ArgList);
                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Grayscale");
            }
        }

        internal void Dither()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Dither();
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Dither");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                m_Controller.Automation_AddOperation(Constants.Operations.Dither, ArgList);
                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Dither");
            }
        }

        internal void Sepia()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Sepia();
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Sepia");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                m_Controller.Automation_AddOperation(Constants.Operations.Sepia, ArgList);
                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Sepia");
            }
        }

        internal void Invert()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Invert();
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Invert");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                m_Controller.Automation_AddOperation(Constants.Operations.Invert, ArgList);
                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Invert");
            }
        }

        internal void Resize()
        {
            if (!m_bAutoMode)
            {
                if (m_frmMain.rbResizePixels.Checked)
                {
                    if (m_frmMain.cbMaintainAspectRatio.Checked)
                    {
                        Size sizNew = Core.BasicOps.MaintainAspectRatio_DontExcedeNewSize(
                                                                new Size(Convert.ToInt32(m_frmMain.txtResizeWidth.Text), 
                                                                         Convert.ToInt32(m_frmMain.txtResizeHeight.Text)),
                                                                bmpOutput.Size);

                        bmpOutput = m_Controller.ResizeByPixels(sizNew.Width, sizNew.Height);
                    }
                    else
                        bmpOutput = m_Controller.ResizeByPixels(Convert.ToInt32(m_frmMain.txtResizeWidth.Text),
                                                            Convert.ToInt32(m_frmMain.txtResizeHeight.Text));
                }
                else if (m_frmMain.rbResizePercentage.Checked)
                {
                    bmpOutput = m_Controller.ResizeByPercent(Convert.ToInt32(m_frmMain.txtResizeWidth.Text));
                }

                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Resize");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(Convert.ToInt32(m_frmMain.txtResizeWidth.Text));
                ArgList.Add(Convert.ToInt32(m_frmMain.txtResizeHeight.Text));

                if (m_frmMain.rbResizePixels.Checked)
                    m_Controller.Automation_AddOperation(Constants.Operations.ResizePixels, ArgList);
                else if (m_frmMain.rbResizePercentage.Checked)
                    m_Controller.Automation_AddOperation(Constants.Operations.ResizePercent, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Resize");

            }
        }

        internal void ResizeImageByMouse()
        {
            m_frmMain.txtResizeWidth.Text = m_frmMain.mpbMain.ImageSize.Width.ToString();
            m_frmMain.txtResizeHeight.Text = m_frmMain.mpbMain.ImageSize.Height.ToString();
            Resize();
            m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Resize");
        }

        internal void Crop()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Crop(m_frmMain.mpbMain.CropBounds);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Resize");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(m_frmMain.mpbMain.CropBounds);

                m_Controller.Automation_AddOperation(Constants.Operations.Crop, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Crop");
            }
        }

        internal void FlipL()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Flip(Photoman.Core.BasicOps.FlipAxis.X);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Flip");
            }

            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(Photoman.Core.BasicOps.FlipAxis.X);

                m_Controller.Automation_AddOperation(Constants.Operations.Flip, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Flip");
            }
        }

        internal void FlipR()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Flip(Photoman.Core.BasicOps.FlipAxis.Y);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Flip");
            }

            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(Photoman.Core.BasicOps.FlipAxis.Y);

                m_Controller.Automation_AddOperation(Constants.Operations.Flip, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Flip");
            }
        }

        internal void RotateL()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Rotate(Photoman.Core.BasicOps.RotateDirection.Left);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Rotate");
            }

            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Left);

                m_Controller.Automation_AddOperation(Constants.Operations.Rotate, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Rotate");
            }
        }

        internal void RotateR()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Rotate(Photoman.Core.BasicOps.RotateDirection.Right);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Rotate");
            }

            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Right);

                m_Controller.Automation_AddOperation(Constants.Operations.Rotate, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Rotate");
            }
        }

        internal void RedEye()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.RedEye(m_frmMain.mpbMain.CropBounds);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Red Eye Removal");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(m_frmMain.mpbMain.CropBounds);

                m_Controller.Automation_AddOperation(Constants.Operations.RemoveRedEye, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Red Eye Removal");
            }
        }

        internal void ChromaKey()
        {
            if (!m_bAutoMode)
            {
                bmpOutput = m_Controller.Chromakey(m_frmMain.ckMain.BackgroundImage, bmpOutput, m_frmMain.ckMain.Sensitivity);
                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Chromakey");
            }
            else if (m_bAutoMode)
            {
                List<object> ArgList = new List<object>();
                ArgList.Add(m_frmMain.ckMain.BackgroundImage);
                ArgList.Add(bmpOutput);
                ArgList.Add(m_frmMain.ckMain.Sensitivity);

                m_Controller.Automation_AddOperation(Constants.Operations.ChromaKey, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Chromakey");
            }
        }

        internal void AddText()
        {
            if (!m_bAutoMode)
            {
                //Generate Font
                FontStyle fsPreview = new FontStyle();

                if (BoldForAddText)
                    fsPreview = FontStyle.Bold;
                if (ItalicForAddText)
                    fsPreview = FontStyle.Italic;
                if (UnderlineForAddText)
                    fsPreview = FontStyle.Underline;
                if (StrikethroughForAddText)
                    fsPreview = FontStyle.Strikeout;

                Font fntPreview = new Font(FontNameForAddText, FontSizeForAddText, fsPreview);

                bmpOutput = m_Controller.AddText(m_frmMain.mpbMain.CropBounds, TextForAddText, fntPreview,
                                                                   new SolidBrush(Color.FromName(FontColorForAddText)));

                m_frmMain.tlImageHistory.AddElement(new Bitmap(1, 1), "Add Text");
            }
            else if (m_bAutoMode)
            {
                //Generate Font
                FontStyle fsPreview = new FontStyle();

                if (BoldForAddText)
                    fsPreview = FontStyle.Bold;
                if (ItalicForAddText)
                    fsPreview = FontStyle.Italic;
                if (UnderlineForAddText)
                    fsPreview = FontStyle.Underline;
                if (StrikethroughForAddText)
                    fsPreview = FontStyle.Strikeout;

                Font fntPreview = new Font(FontNameForAddText, FontSizeForAddText, fsPreview);

                List<object> ArgList = new List<object>();
                ArgList.Add(m_frmMain.mpbMain.CropBounds);
                ArgList.Add(TextForAddText);
                ArgList.Add(fntPreview);
                ArgList.Add(new SolidBrush(Color.FromName(FontColorForAddText)));

                m_Controller.Automation_AddOperation(Constants.Operations.AddText, ArgList);

                m_frmMain.tlAutomationActions.AddElement(new Bitmap(1, 1), "Add Text");
            }

            //Hide our selection rectangle now that we've added our text
            m_frmMain.mpbMain.HideSelectionRectangle();
        }

        internal void StartAutomation()
        {
            bmpOutput = m_Controller.Automation_Start();

            m_frmMain.tlAutomationActions.RemoveAllElements();
        }

        #endregion

        #region ImagePreview stuff

        internal void Preview_HueSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(m_frmMain.itHue.tbMain.Value - iHueSlider));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Hue, ArgList);
        }

        internal void Preview_LuminositySlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((m_frmMain.itLuminosity.tbMain.Value - iLumSlider) * 0.01f));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Luminosity, ArgList);
        }

        internal void Preview_SaturationSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((m_frmMain.itSaturation.tbMain.Value - iSatSlider) * 0.1f));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Saturation, ArgList);
        }

        internal void Preveiw_BrightnessSlider()
        {

            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((iBrightSlider - m_frmMain.itBrightness.tbMain.Value) * 1));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Brightness, ArgList);
        }

        internal void Preveiw_RedSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble((iRedSlider - m_frmMain.itRed.tbMain.Value) * 1));
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble(1));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preveiw_GreenSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble((iGreenSlider - m_frmMain.itGreen.tbMain.Value) * 1));
            ArgList.Add((float)Convert.ToDouble(1));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preveiw_BlueSlider()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble(1));
            ArgList.Add((float)Convert.ToDouble((iBlueSlider - m_frmMain.itBlue.tbMain.Value) * 1));
            m_ppColor.SetPreviewImage(bmpOutput, Constants.Operations.Gamma, ArgList);
        }

        internal void Preview_Grayscale()
        {
            List<object> ArgList = new List<object>();
            m_ppSpecial.SetPreviewImage(bmpOutput, Constants.Operations.Grayscale, ArgList);
        }

        internal void Preview_Dither()
        {
            List<object> ArgList = new List<object>();
            m_ppSpecial.SetPreviewImage(bmpOutput, Constants.Operations.Dither, ArgList);
        }

        internal void Preview_Sepia()
        {
            List<object> ArgList = new List<object>();
            m_ppSpecial.SetPreviewImage(bmpOutput, Constants.Operations.Sepia, ArgList);
        }

        internal void Preview_Invert()
        {
            List<object> ArgList = new List<object>();
            m_ppSpecial.SetPreviewImage(bmpOutput, Constants.Operations.Invert, ArgList);
        }

        internal void Preview_FlipL()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.FlipAxis.X);
            m_ppSizeDimensions.SetPreviewImage(bmpOutput, Constants.Operations.Flip, ArgList);
        }

        internal void Preview_FlipR()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.FlipAxis.Y);
            m_ppSizeDimensions.SetPreviewImage(bmpOutput, Constants.Operations.Flip, ArgList);
        }

        internal void Preview_RotateL()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Left);
            m_ppSizeDimensions.SetPreviewImage(bmpOutput, Constants.Operations.Rotate, ArgList);
        }

        internal void Preview_RotateR()
        {
            List<object> ArgList = new List<object>();
            ArgList.Add(Photoman.Core.BasicOps.RotateDirection.Right);
            m_ppSizeDimensions.SetPreviewImage(bmpOutput, Constants.Operations.Rotate, ArgList);
        }

        internal void Preview_AddText()
        {
            //Generate Font
            FontStyle fsPreview = new FontStyle();

            if(BoldForAddText)
                fsPreview = FontStyle.Bold;
            if(ItalicForAddText)
                fsPreview = FontStyle.Italic;
            if(UnderlineForAddText)
                fsPreview = FontStyle.Underline;
            if(StrikethroughForAddText)
                fsPreview = FontStyle.Strikeout;

            Font fntPreview = new Font(FontNameForAddText, FontSizeForAddText, fsPreview);
            m_frmMain.mpbMain.SetAddTextInfo(TextForAddText, fntPreview, new SolidBrush(Color.FromName(FontColorForAddText)));
        }

        internal void ClearPreview()
        {
            m_ppSizeDimensions.ClearPreview();
            m_ppColor.ClearPreview();
            m_ppSpecial.ClearPreview();
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
            ArgList.Add(m_frmMain.ckMain.Sensitivity);

            m_ppSpecial.SetPreviewImage(bmpOutput, Constants.Operations.ChromaKey, ArgList);

            
        }

        void DrawAddTextColorSelection(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 140/*110*/, rect.Y + 2/*5*/, rect.Width - 10, rect.Height - 4/*10*/);
            }
        }

        void DrawAddTextFontSelection(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font(n, 9f, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
            }
        }

        void DiskCacheStatusChange(bool Status)
        {
            if (Status)
            {
                //Chose purple because it stands out and isnt a "bad" color (ex. red, yellow)
                m_frmMain.tslCacheStatus.ForeColor = Color.Purple;
                m_frmMain.tslCacheStatus.Text = Constants.strCacheEnabled;
            }
            else
            {
                m_frmMain.tslCacheStatus.ForeColor = SystemColors.GrayText;
                m_frmMain.tslCacheStatus.Text = Constants.strCacheDisabled;
            }
        }

        void ExceptionCountChange(int ExceptionCount)
        {
            m_frmMain.tslExceptionCount.ForeColor = Color.Red;
            m_frmMain.tslExceptionCount.Text = ExceptionCount + Constants.strNumberOfExceptions;
        }

        #endregion
    }
}
