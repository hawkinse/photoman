using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Photoman.UserInterface
{
    public partial class TestGUI : Form
    {
        internal TestGUI_Controller controller;

        public TestGUI()
        {
            InitializeComponent();

            //Test code for thumbnail widget
            //Bitmap bmpTest = new Bitmap(5, 3);
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        bmpTest.SetPixel(i, j, Color.Red);
            //    }
            //}

            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
            //uitlMain.AddElement(bmpTest, "This is a test");
        }

        private void btnHue_Click(object sender, EventArgs e)
        {
            controller.Hue();
        }

        private void btnLuminosity_Click(object sender, EventArgs e)
        {
            controller.Luminosity();
        }

        private void btnSaturation_Click(object sender, EventArgs e)
        {
            controller.Saturation();
        }

        private void btnBrightness_Click(object sender, EventArgs e)
        {
            controller.Brightness();
        }

        private void btnGamma_Click(object sender, EventArgs e)
        {
            controller.Gamma();
        }

        private void btnGrayscale_Click(object sender, EventArgs e)
        {
            controller.Grayscale();
        }

        private void btnDither_Click(object sender, EventArgs e)
        {
            controller.Dither();
        }

        private void btnSepia_Click(object sender, EventArgs e)
        {
            controller.Sepia();
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            controller.Invert();
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            controller.Resize();
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            controller.Crop();
        }

        private void btnFlipL_Click(object sender, EventArgs e)
        {
            controller.FlipL();
        }

        private void btnFlipR_Click(object sender, EventArgs e)
        {
            controller.FlipR();
        }

        private void btnRotateL_Click(object sender, EventArgs e)
        {
            controller.RotateL();
        }

        private void btnRotateR_Click(object sender, EventArgs e)
        {
            controller.RotateR();
        }

        private void btnRedEye_Click(object sender, EventArgs e)
        {
            controller.RedEye();
        }

        private void txtText_Click(object sender, EventArgs e)
        {
            controller.AddText();
        }

        private void TestGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.ExitLogic();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            controller.AddImage();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            controller.RemoveImage();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            controller.PreviousImage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            controller.NextImage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            controller.SaveImage();
        }

        private void btnCachePercentage_Click(object sender, EventArgs e)
        {
            controller.SetCachePercentage();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            controller.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            controller.Redo();
        }
        
        void BtnStartAutoClick(object sender, EventArgs e)
        {
        	controller.StartAutomation();
        }

        private void btnHue_MouseLeave(object sender, EventArgs e)
        {
            controller.HidePreview();
        }

        private void btnHue_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Hue();
        }

        private void btnLuminosity_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Luminosity();
        }

        private void btnSaturation_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Luminosity();
        }

        private void btnBrightness_MouseEnter(object sender, EventArgs e)
        {
            controller.Preveiw_Brightness();
        }

        private void btnGamma_MouseEnter(object sender, EventArgs e)
        {
            controller.Preveiw_Gamma();
        }

        private void btnGrayscale_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Grayscale();
        }

        private void btnDither_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Dither();
        }

        private void btnSepia_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Sepia();
        }

        private void btnInvert_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_Invert();
        }

        private void btnFlipL_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_FlipL();
        }

        private void btnFlipR_Enter(object sender, EventArgs e)
        {
            controller.Preview_FlipR();
        }

        private void btnRotateL_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_RotateL();
        }

        private void btnRotateR_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_RotateR();
        }

        private void btnHue_MouseMove(object sender, MouseEventArgs e)
        {
            controller.AdjustPreviewPosition();
        }

        private void TestGUI_Load(object sender, EventArgs e)
        {
            itBrightness.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Brightness, null);
            itBlue.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Blue, null);
            itRed.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Red, null);
            itGreen.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Green, null);
            itLuminosity.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Luminosity, null);
            itHue.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Hue, null);
        }

        private void itRed_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Red);
        }

        private void itRed_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Red);
        }

        private void itGreen_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Green);
        }

        private void itGreen_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Green);
        }

        private void itBlue_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Blue);
        }

        private void itBlue_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Blue);
        }

        private void itHue_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Hue);
        }

        private void itHue_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Hue);
        }

        private void itLuminosity_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Luminosity);
        }

        private void itLuminosity_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Luminosity);
        }

        private void itSaturation_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Saturation);
        }

        private void itSaturation_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Saturation);
        }

        private void itBrightness_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Brightness);
        }

        private void itBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Brightness);
        }
    }
}
