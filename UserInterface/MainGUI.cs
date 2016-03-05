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
    public partial class MainGUI : Form
    {
        internal MainGUI_Controller controller;

        public MainGUI()
        {
            InitializeComponent();

            //Set up sliders
            itBrightness.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Brightness, null);
            itBlue.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Blue, null);
            itRed.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Red, null);
            itGreen.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Green, null);
            itLuminosity.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Luminosity, null);
            itHue.ReInit(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Hue, null);
            
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMainGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            controller.AddImage();
        }

        private void tsmiSaveOne_Click(object sender, EventArgs e)
        {
            controller.SaveImage();
        }

        private void tsmiSaveAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unimplimented. Redirecting to save single image");
            controller.SaveImage();
        }

        private void removeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.RemoveImage();
        }

        private void btnApplyResize_Click(object sender, EventArgs e)
        {
            //Ensure that we have valid values
            try
            {
                //Cast both values to ints. This way we can catch any exceptions before they do any damage
                Convert.ToInt32(txtResizeWidth.Text);
                Convert.ToInt32(txtResizeHeight.Text);

                controller.Resize();
            }
            catch (Exception ex)
            {
                Global.WriteToLog("Non-numeric values used in a resize.", true, true);
                Global.WriteToLog(ex);
            }
        }

        private void btnRotateMinus90_Click(object sender, EventArgs e)
        {
            controller.RotateL();
        }

        private void btnRotatePlus90_Click(object sender, EventArgs e)
        {
            controller.RotateR();
        }

        private void btnFlipX_Click(object sender, EventArgs e)
        {
            controller.FlipL();
        }

        private void btnFlipY_Click(object sender, EventArgs e)
        {
            controller.FlipR();
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            controller.Crop();
        }

        private void tsmiUndo_Click(object sender, EventArgs e)
        {
            controller.Undo();
        }

        private void tsmiRedo_Click(object sender, EventArgs e)
        {
            controller.Redo();
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

        private void itBrightness_MouseDown(object sender, MouseEventArgs e)
        {
            controller.SetSliderStart(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Brightness);
        }

        private void itBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            controller.SetSliderStop(Photoman.UserInterface.UIWidgets.uiImageTrackbar.TrackbarColorType.Brightness);
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

        private void btnApplyText_Click(object sender, EventArgs e)
        {
            controller.AddText();
        }

        private void nudCachePercentage_ValueChanged(object sender, EventArgs e)
        {
            controller.SetCachePercentage();
        }

        private void showHideDebugOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.ToggleConsole();
        }

        private void btnApplyResize_MouseEnter(object sender, EventArgs e)
        {
            //No preview
        }

        private void btnRotateMinus90_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_RotateL();
        }

        private void btnRotatePlus90_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_RotateR();
        }

        private void btnFlipX_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_FlipL();
        }

        private void btnFlipY_MouseEnter(object sender, EventArgs e)
        {
            controller.Preview_FlipR();
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

        private void cbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.FontNameForAddText = cbFont.Text;
            controller.Preview_AddText();
        }

        private void txtAddText_TextChanged(object sender, EventArgs e)
        {
            controller.TextForAddText = txtAddText.Text;
            controller.Preview_AddText();
        }

        private void cbFont_TextChanged(object sender, EventArgs e)
        {
            controller.FontNameForAddText = cbFont.Text;
            controller.Preview_AddText();
        }

        private void cbTextColor_TextChanged(object sender, EventArgs e)
        {
            controller.FontColorForAddText = cbTextColor.Text;
            controller.Preview_AddText();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            controller.BoldForAddText = !controller.BoldForAddText;
            if (controller.BoldForAddText)
                btnBold.BackColor = Color.LightYellow;
            else
                btnBold.BackColor = SystemColors.GradientInactiveCaption;
            controller.Preview_AddText();
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            controller.ItalicForAddText = !controller.ItalicForAddText;
            if (controller.ItalicForAddText)
                btnItalic.BackColor = Color.LightYellow;
            else
                btnItalic.BackColor = SystemColors.GradientInactiveCaption;

            controller.Preview_AddText();
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            controller.UnderlineForAddText = !controller.UnderlineForAddText;
            if (controller.UnderlineForAddText)
                btnUnderline.BackColor = Color.LightYellow;
            else
                btnUnderline.BackColor = SystemColors.GradientInactiveCaption;

            controller.Preview_AddText();
        }

        private void btnStrikethrough_Click(object sender, EventArgs e)
        {
            controller.StrikethroughForAddText = !controller.StrikethroughForAddText;
            if (controller.StrikethroughForAddText)
                btnStrikethrough.BackColor = Color.LightYellow;
            else
                btnStrikethrough.BackColor = SystemColors.GradientInactiveCaption;

            controller.Preview_AddText();
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            if (nudFontSize.Value > 0)
            {
                controller.FontSizeForAddText = (float)nudFontSize.Value;
                controller.Preview_AddText();
            }
            else
                nudFontSize.Value = 1;
        }

        private void btnRedEye_Click(object sender, EventArgs e)
        {
            controller.RedEye();
        }

        private void rbResizePixels_CheckedChanged(object sender, EventArgs e)
        {
            lblResizeWidth.Text = "Width";
            lblResizeHeight.Visible = true;
            txtResizeHeight.Visible = true;
            cbMaintainAspectRatio.Enabled = true;
        }

        private void rbResizePercentage_CheckedChanged(object sender, EventArgs e)
        {
            lblResizeWidth.Text = "    %";
            lblResizeHeight.Visible = false;
            txtResizeHeight.Visible = false;
            cbMaintainAspectRatio.Enabled = false;
        }

        private void btnImageList_Click(object sender, EventArgs e)
        {
            tlLoadedImages.BringToFront();

            btnImageList.Dock = DockStyle.Top;
            btnHistoryList.Dock = DockStyle.Bottom;
            btnAutomationList.Dock = DockStyle.Bottom;
            tlLoadedImages.Dock = DockStyle.Fill;
            btnApplyActions.Visible = false;

            //Block may not be well thought out. Written during brother's drum practice, addmittedly wasnt in best headspace
            pnlListsContainer.Controls.SetChildIndex(btnImageList, 3);
            pnlListsContainer.Controls.SetChildIndex(btnAutomationList, 1);
            pnlListsContainer.Controls.SetChildIndex(btnHistoryList, 2);

            //tell controller we're in normal mode
            controller.AutoMode = false;
        }

        private void btnAutomationList_Click(object sender, EventArgs e)
        {
            btnImageList.Dock = DockStyle.Top;
            btnAutomationList.Dock = DockStyle.Top;
            btnHistoryList.Dock = DockStyle.Bottom;
            btnApplyActions.Visible = true;
            btnApplyActions.Dock = DockStyle.Bottom;
            tlAutomationActions.BringToFront();
            tlAutomationActions.Dock = DockStyle.Fill;

            //Block may not be well thought out. Written during brother's drum practice, addmittedly wasnt in best headspace
            pnlListsContainer.Controls.SetChildIndex(btnImageList, 2);            
            pnlListsContainer.Controls.SetChildIndex(btnAutomationList, 0);
            pnlListsContainer.Controls.SetChildIndex(btnHistoryList, 1);
            pnlListsContainer.Controls.SetChildIndex(btnApplyActions, 0);
            pnlListsContainer.Controls.SetChildIndex(tlAutomationActions, 0);

            //Tell back end we are now in auto mode
            controller.AutoMode = true;

        }

        private void btnHistoryList_Click(object sender, EventArgs e)
        {
            //Hide Auto mode's apply actions button
            btnApplyActions.Visible = false;
            btnApplyActions.Dock = DockStyle.None;

            //btnImageList.Dock = DockStyle.None;
            //btnAutomationList.Dock = DockStyle.None;
            btnHistoryList.Dock = DockStyle.Bottom;
            
            tlImageHistory.BringToFront();
            tlImageHistory.Dock = DockStyle.Fill;

            btnImageList.Dock = DockStyle.Top;
            btnAutomationList.Dock = DockStyle.Top;
            btnHistoryList.Dock = DockStyle.Top;

            //Block may not be well thought out. Written during brother's drum practice, addmittedly wasnt in best headspace
            pnlListsContainer.Controls.SetChildIndex(btnImageList, 0);
            pnlListsContainer.Controls.SetChildIndex(btnAutomationList, 0);
            pnlListsContainer.Controls.SetChildIndex(btnHistoryList, 0);
            pnlListsContainer.Controls.SetChildIndex(tlImageHistory, 0);
            //Tell back end we are in normal mode
            controller.AutoMode = false;
        }

        private void btnApplyActions_Click(object sender, EventArgs e)
        {
            controller.StartAutomation();
        }

        private void ClearPreview(object sender, EventArgs e)
        {
            controller.ClearPreview();
        }

        private void cbxBatterySaveMode_CheckedChanged(object sender, EventArgs e)
        {
            Global.bDisableAntsOnBattery = cbxBatterySaveMode.Checked;
            mpbMain.Refresh();
        }

        private void MainGUI_ResizeBegin(object sender, EventArgs e)
        {
            mpbMain.AntMarching = false;
        }

        //This event also fires when we stop moving the form
        private void MainGUI_ResizeEnd(object sender, EventArgs e)
        {
            mpbMain.AntMarching = true;
        }

        private void MainGUI_Move(object sender, EventArgs e)
        {
            mpbMain.AntMarching = false;
        }

        private void cbEnableVisualEffects_CheckedChanged(object sender, EventArgs e)
        {
            mpbMain.AllowAntMarching = cbxEnableVisualEffects.Checked;
            mpbMain.Refresh();
        }
    }
}
