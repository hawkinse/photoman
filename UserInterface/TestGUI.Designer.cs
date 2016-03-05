namespace Photoman.UserInterface
{
    partial class TestGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbHLS = new System.Windows.Forms.GroupBox();
            this.btnSaturation = new System.Windows.Forms.Button();
            this.btnLuminosity = new System.Windows.Forms.Button();
            this.btnHue = new System.Windows.Forms.Button();
            this.txtHLSValue = new System.Windows.Forms.TextBox();
            this.lblHLS = new System.Windows.Forms.Label();
            this.gbRGB = new System.Windows.Forms.GroupBox();
            this.txtRGB_Blue = new System.Windows.Forms.TextBox();
            this.txtRGB_Green = new System.Windows.Forms.TextBox();
            this.txtRGB_Red = new System.Windows.Forms.TextBox();
            this.lblRGB = new System.Windows.Forms.Label();
            this.btnInvert = new System.Windows.Forms.Button();
            this.btnSepia = new System.Windows.Forms.Button();
            this.btnDither = new System.Windows.Forms.Button();
            this.btnGrayscale = new System.Windows.Forms.Button();
            this.btnGamma = new System.Windows.Forms.Button();
            this.btnBrightness = new System.Windows.Forms.Button();
            this.gbBasic = new System.Windows.Forms.GroupBox();
            this.btnRotateR = new System.Windows.Forms.Button();
            this.btnRotateL = new System.Windows.Forms.Button();
            this.btnFlipR = new System.Windows.Forms.Button();
            this.btnFlipL = new System.Windows.Forms.Button();
            this.txtCropLR = new System.Windows.Forms.TextBox();
            this.txtCropLL = new System.Windows.Forms.TextBox();
            this.txtCropUR = new System.Windows.Forms.TextBox();
            this.txtCropUL = new System.Windows.Forms.TextBox();
            this.btnCrop = new System.Windows.Forms.Button();
            this.btnResize = new System.Windows.Forms.Button();
            this.rbPercentage = new System.Windows.Forms.RadioButton();
            this.rbCentimeters = new System.Windows.Forms.RadioButton();
            this.rbInches = new System.Windows.Forms.RadioButton();
            this.rbPixels = new System.Windows.Forms.RadioButton();
            this.txtSizeY = new System.Windows.Forms.TextBox();
            this.txtSizeX = new System.Windows.Forms.TextBox();
            this.gbOther = new System.Windows.Forms.GroupBox();
            this.lblText = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.Button();
            this.btnRedEye = new System.Windows.Forms.Button();
            this.txtOtherLR = new System.Windows.Forms.TextBox();
            this.txtOtherLL = new System.Windows.Forms.TextBox();
            this.txtOtherUR = new System.Windows.Forms.TextBox();
            this.txtOtherUL = new System.Windows.Forms.TextBox();
            this.pbOutput = new System.Windows.Forms.PictureBox();
            this.gbImageHandler = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCachePercentage = new System.Windows.Forms.TextBox();
            this.btnCachePercentage = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.gbHistory = new System.Windows.Forms.GroupBox();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.gbAutomation = new System.Windows.Forms.GroupBox();
            this.btnStartAuto = new System.Windows.Forms.Button();
            this.rbBatch = new System.Windows.Forms.RadioButton();
            this.rbDoSingle = new System.Windows.Forms.RadioButton();
            this.rbAddToAuto = new System.Windows.Forms.RadioButton();
            this.itHue = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.uippMain = new Photoman.UserInterface.UIWidgets.uiwPreviewPopup();
            this.uiChromaKey = new Photoman.UserInterface.UIWidgets.uiChromakey();
            this.itBrightness = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.uitlMain = new Photoman.UserInterface.UIWidgets.uiThumbnailList();
            this.itSaturation = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itLuminosity = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itBlue = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itGreen = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itRed = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.gbHLS.SuspendLayout();
            this.gbRGB.SuspendLayout();
            this.gbBasic.SuspendLayout();
            this.gbOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).BeginInit();
            this.gbImageHandler.SuspendLayout();
            this.gbHistory.SuspendLayout();
            this.gbAutomation.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbHLS
            // 
            this.gbHLS.Controls.Add(this.btnSaturation);
            this.gbHLS.Controls.Add(this.btnLuminosity);
            this.gbHLS.Controls.Add(this.btnHue);
            this.gbHLS.Controls.Add(this.txtHLSValue);
            this.gbHLS.Controls.Add(this.lblHLS);
            this.gbHLS.Location = new System.Drawing.Point(12, 231);
            this.gbHLS.Name = "gbHLS";
            this.gbHLS.Size = new System.Drawing.Size(381, 68);
            this.gbHLS.TabIndex = 0;
            this.gbHLS.TabStop = false;
            this.gbHLS.Text = "HLS Stuff";
            // 
            // btnSaturation
            // 
            this.btnSaturation.Location = new System.Drawing.Point(288, 35);
            this.btnSaturation.Name = "btnSaturation";
            this.btnSaturation.Size = new System.Drawing.Size(75, 23);
            this.btnSaturation.TabIndex = 4;
            this.btnSaturation.Text = "Saturation";
            this.btnSaturation.UseVisualStyleBackColor = true;
            this.btnSaturation.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnSaturation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnSaturation.Click += new System.EventHandler(this.btnSaturation_Click);
            this.btnSaturation.MouseEnter += new System.EventHandler(this.btnSaturation_MouseEnter);
            // 
            // btnLuminosity
            // 
            this.btnLuminosity.Location = new System.Drawing.Point(207, 35);
            this.btnLuminosity.Name = "btnLuminosity";
            this.btnLuminosity.Size = new System.Drawing.Size(75, 23);
            this.btnLuminosity.TabIndex = 3;
            this.btnLuminosity.Text = "Luminosity";
            this.btnLuminosity.UseVisualStyleBackColor = true;
            this.btnLuminosity.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnLuminosity.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnLuminosity.Click += new System.EventHandler(this.btnLuminosity_Click);
            this.btnLuminosity.MouseEnter += new System.EventHandler(this.btnLuminosity_MouseEnter);
            // 
            // btnHue
            // 
            this.btnHue.Location = new System.Drawing.Point(126, 34);
            this.btnHue.Name = "btnHue";
            this.btnHue.Size = new System.Drawing.Size(75, 23);
            this.btnHue.TabIndex = 2;
            this.btnHue.Text = "Hue";
            this.btnHue.UseVisualStyleBackColor = true;
            this.btnHue.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnHue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnHue.Click += new System.EventHandler(this.btnHue_Click);
            this.btnHue.MouseEnter += new System.EventHandler(this.btnHue_MouseEnter);
            // 
            // txtHLSValue
            // 
            this.txtHLSValue.Location = new System.Drawing.Point(10, 37);
            this.txtHLSValue.Name = "txtHLSValue";
            this.txtHLSValue.Size = new System.Drawing.Size(110, 20);
            this.txtHLSValue.TabIndex = 1;
            this.txtHLSValue.Text = "1";
            // 
            // lblHLS
            // 
            this.lblHLS.AutoSize = true;
            this.lblHLS.Location = new System.Drawing.Point(7, 20);
            this.lblHLS.Name = "lblHLS";
            this.lblHLS.Size = new System.Drawing.Size(113, 13);
            this.lblHLS.TabIndex = 0;
            this.lblHLS.Text = "Enter a multiplier value";
            // 
            // gbRGB
            // 
            this.gbRGB.Controls.Add(this.txtRGB_Blue);
            this.gbRGB.Controls.Add(this.txtRGB_Green);
            this.gbRGB.Controls.Add(this.txtRGB_Red);
            this.gbRGB.Controls.Add(this.lblRGB);
            this.gbRGB.Controls.Add(this.btnInvert);
            this.gbRGB.Controls.Add(this.btnSepia);
            this.gbRGB.Controls.Add(this.btnDither);
            this.gbRGB.Controls.Add(this.btnGrayscale);
            this.gbRGB.Controls.Add(this.btnGamma);
            this.gbRGB.Controls.Add(this.btnBrightness);
            this.gbRGB.Location = new System.Drawing.Point(12, 305);
            this.gbRGB.Name = "gbRGB";
            this.gbRGB.Size = new System.Drawing.Size(381, 122);
            this.gbRGB.TabIndex = 1;
            this.gbRGB.TabStop = false;
            this.gbRGB.Text = "RGB Stuff";
            // 
            // txtRGB_Blue
            // 
            this.txtRGB_Blue.Location = new System.Drawing.Point(242, 32);
            this.txtRGB_Blue.Name = "txtRGB_Blue";
            this.txtRGB_Blue.Size = new System.Drawing.Size(110, 20);
            this.txtRGB_Blue.TabIndex = 11;
            this.txtRGB_Blue.Text = "1";
            // 
            // txtRGB_Green
            // 
            this.txtRGB_Green.Location = new System.Drawing.Point(126, 32);
            this.txtRGB_Green.Name = "txtRGB_Green";
            this.txtRGB_Green.Size = new System.Drawing.Size(110, 20);
            this.txtRGB_Green.TabIndex = 10;
            this.txtRGB_Green.Text = "1";
            // 
            // txtRGB_Red
            // 
            this.txtRGB_Red.Location = new System.Drawing.Point(10, 32);
            this.txtRGB_Red.Name = "txtRGB_Red";
            this.txtRGB_Red.Size = new System.Drawing.Size(110, 20);
            this.txtRGB_Red.TabIndex = 9;
            this.txtRGB_Red.Text = "1";
            // 
            // lblRGB
            // 
            this.lblRGB.AutoSize = true;
            this.lblRGB.Location = new System.Drawing.Point(7, 16);
            this.lblRGB.Name = "lblRGB";
            this.lblRGB.Size = new System.Drawing.Size(254, 13);
            this.lblRGB.TabIndex = 8;
            this.lblRGB.Text = "Enter multiplier values for Gamma - Red, Green, Blue";
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(253, 87);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(75, 23);
            this.btnInvert.TabIndex = 7;
            this.btnInvert.Text = "Invert";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnInvert.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            this.btnInvert.MouseEnter += new System.EventHandler(this.btnInvert_MouseEnter);
            // 
            // btnSepia
            // 
            this.btnSepia.Location = new System.Drawing.Point(172, 87);
            this.btnSepia.Name = "btnSepia";
            this.btnSepia.Size = new System.Drawing.Size(75, 23);
            this.btnSepia.TabIndex = 6;
            this.btnSepia.Text = "Sepia";
            this.btnSepia.UseVisualStyleBackColor = true;
            this.btnSepia.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnSepia.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnSepia.Click += new System.EventHandler(this.btnSepia_Click);
            this.btnSepia.MouseEnter += new System.EventHandler(this.btnSepia_MouseEnter);
            // 
            // btnDither
            // 
            this.btnDither.Location = new System.Drawing.Point(91, 87);
            this.btnDither.Name = "btnDither";
            this.btnDither.Size = new System.Drawing.Size(75, 23);
            this.btnDither.TabIndex = 5;
            this.btnDither.Text = "Dither";
            this.btnDither.UseVisualStyleBackColor = true;
            this.btnDither.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnDither.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnDither.Click += new System.EventHandler(this.btnDither_Click);
            this.btnDither.MouseEnter += new System.EventHandler(this.btnDither_MouseEnter);
            // 
            // btnGrayscale
            // 
            this.btnGrayscale.Location = new System.Drawing.Point(10, 87);
            this.btnGrayscale.Name = "btnGrayscale";
            this.btnGrayscale.Size = new System.Drawing.Size(75, 23);
            this.btnGrayscale.TabIndex = 4;
            this.btnGrayscale.Text = "Grayscale";
            this.btnGrayscale.UseVisualStyleBackColor = true;
            this.btnGrayscale.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnGrayscale.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnGrayscale.Click += new System.EventHandler(this.btnGrayscale_Click);
            this.btnGrayscale.MouseEnter += new System.EventHandler(this.btnGrayscale_MouseEnter);
            // 
            // btnGamma
            // 
            this.btnGamma.Location = new System.Drawing.Point(145, 58);
            this.btnGamma.Name = "btnGamma";
            this.btnGamma.Size = new System.Drawing.Size(75, 23);
            this.btnGamma.TabIndex = 3;
            this.btnGamma.Text = "Gamma";
            this.btnGamma.UseVisualStyleBackColor = true;
            this.btnGamma.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnGamma.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnGamma.Click += new System.EventHandler(this.btnGamma_Click);
            this.btnGamma.MouseEnter += new System.EventHandler(this.btnGamma_MouseEnter);
            // 
            // btnBrightness
            // 
            this.btnBrightness.Location = new System.Drawing.Point(10, 58);
            this.btnBrightness.Name = "btnBrightness";
            this.btnBrightness.Size = new System.Drawing.Size(129, 23);
            this.btnBrightness.TabIndex = 0;
            this.btnBrightness.Text = "Brightness (Use R)";
            this.btnBrightness.UseVisualStyleBackColor = true;
            this.btnBrightness.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnBrightness.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnBrightness.Click += new System.EventHandler(this.btnBrightness_Click);
            this.btnBrightness.MouseEnter += new System.EventHandler(this.btnBrightness_MouseEnter);
            // 
            // gbBasic
            // 
            this.gbBasic.Controls.Add(this.btnRotateR);
            this.gbBasic.Controls.Add(this.btnRotateL);
            this.gbBasic.Controls.Add(this.btnFlipR);
            this.gbBasic.Controls.Add(this.btnFlipL);
            this.gbBasic.Controls.Add(this.txtCropLR);
            this.gbBasic.Controls.Add(this.txtCropLL);
            this.gbBasic.Controls.Add(this.txtCropUR);
            this.gbBasic.Controls.Add(this.txtCropUL);
            this.gbBasic.Controls.Add(this.btnCrop);
            this.gbBasic.Controls.Add(this.btnResize);
            this.gbBasic.Controls.Add(this.rbPercentage);
            this.gbBasic.Controls.Add(this.rbCentimeters);
            this.gbBasic.Controls.Add(this.rbInches);
            this.gbBasic.Controls.Add(this.rbPixels);
            this.gbBasic.Controls.Add(this.txtSizeY);
            this.gbBasic.Controls.Add(this.txtSizeX);
            this.gbBasic.Location = new System.Drawing.Point(12, 433);
            this.gbBasic.Name = "gbBasic";
            this.gbBasic.Size = new System.Drawing.Size(381, 186);
            this.gbBasic.TabIndex = 2;
            this.gbBasic.TabStop = false;
            this.gbBasic.Text = "Basic Stuff";
            // 
            // btnRotateR
            // 
            this.btnRotateR.Location = new System.Drawing.Point(172, 155);
            this.btnRotateR.Name = "btnRotateR";
            this.btnRotateR.Size = new System.Drawing.Size(75, 23);
            this.btnRotateR.TabIndex = 26;
            this.btnRotateR.Text = "RotateR";
            this.btnRotateR.UseVisualStyleBackColor = true;
            this.btnRotateR.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnRotateR.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnRotateR.Click += new System.EventHandler(this.btnRotateR_Click);
            this.btnRotateR.MouseEnter += new System.EventHandler(this.btnRotateR_MouseEnter);
            // 
            // btnRotateL
            // 
            this.btnRotateL.Location = new System.Drawing.Point(91, 155);
            this.btnRotateL.Name = "btnRotateL";
            this.btnRotateL.Size = new System.Drawing.Size(75, 23);
            this.btnRotateL.TabIndex = 25;
            this.btnRotateL.Text = "RotateL";
            this.btnRotateL.UseVisualStyleBackColor = true;
            this.btnRotateL.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnRotateL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnRotateL.Click += new System.EventHandler(this.btnRotateL_Click);
            this.btnRotateL.MouseEnter += new System.EventHandler(this.btnRotateL_MouseEnter);
            // 
            // btnFlipR
            // 
            this.btnFlipR.Location = new System.Drawing.Point(172, 126);
            this.btnFlipR.Name = "btnFlipR";
            this.btnFlipR.Size = new System.Drawing.Size(75, 23);
            this.btnFlipR.TabIndex = 24;
            this.btnFlipR.Text = "FlipR";
            this.btnFlipR.UseVisualStyleBackColor = true;
            this.btnFlipR.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnFlipR.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnFlipR.Click += new System.EventHandler(this.btnFlipR_Click);
            this.btnFlipR.MouseEnter += new System.EventHandler(this.btnFlipR_Enter);
            // 
            // btnFlipL
            // 
            this.btnFlipL.Location = new System.Drawing.Point(91, 126);
            this.btnFlipL.Name = "btnFlipL";
            this.btnFlipL.Size = new System.Drawing.Size(75, 23);
            this.btnFlipL.TabIndex = 23;
            this.btnFlipL.Text = "FlipL";
            this.btnFlipL.UseVisualStyleBackColor = true;
            this.btnFlipL.MouseLeave += new System.EventHandler(this.btnHue_MouseLeave);
            this.btnFlipL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHue_MouseMove);
            this.btnFlipL.Click += new System.EventHandler(this.btnFlipL_Click);
            this.btnFlipL.MouseEnter += new System.EventHandler(this.btnFlipL_MouseEnter);
            // 
            // txtCropLR
            // 
            this.txtCropLR.Location = new System.Drawing.Point(126, 100);
            this.txtCropLR.Name = "txtCropLR";
            this.txtCropLR.Size = new System.Drawing.Size(110, 20);
            this.txtCropLR.TabIndex = 22;
            this.txtCropLR.Text = "1";
            // 
            // txtCropLL
            // 
            this.txtCropLL.Location = new System.Drawing.Point(10, 100);
            this.txtCropLL.Name = "txtCropLL";
            this.txtCropLL.Size = new System.Drawing.Size(110, 20);
            this.txtCropLL.TabIndex = 21;
            this.txtCropLL.Text = "0";
            // 
            // txtCropUR
            // 
            this.txtCropUR.Location = new System.Drawing.Point(126, 74);
            this.txtCropUR.Name = "txtCropUR";
            this.txtCropUR.Size = new System.Drawing.Size(110, 20);
            this.txtCropUR.TabIndex = 20;
            this.txtCropUR.Text = "1";
            // 
            // txtCropUL
            // 
            this.txtCropUL.Location = new System.Drawing.Point(10, 74);
            this.txtCropUL.Name = "txtCropUL";
            this.txtCropUL.Size = new System.Drawing.Size(110, 20);
            this.txtCropUL.TabIndex = 19;
            this.txtCropUL.Text = "0";
            // 
            // btnCrop
            // 
            this.btnCrop.Location = new System.Drawing.Point(10, 126);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(75, 23);
            this.btnCrop.TabIndex = 18;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(10, 45);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(75, 23);
            this.btnResize.TabIndex = 17;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // rbPercentage
            // 
            this.rbPercentage.AutoSize = true;
            this.rbPercentage.Location = new System.Drawing.Point(242, 88);
            this.rbPercentage.Name = "rbPercentage";
            this.rbPercentage.Size = new System.Drawing.Size(118, 17);
            this.rbPercentage.TabIndex = 16;
            this.rbPercentage.Text = "Percentage (Use X)";
            this.rbPercentage.UseVisualStyleBackColor = true;
            // 
            // rbCentimeters
            // 
            this.rbCentimeters.AutoSize = true;
            this.rbCentimeters.Location = new System.Drawing.Point(243, 65);
            this.rbCentimeters.Name = "rbCentimeters";
            this.rbCentimeters.Size = new System.Drawing.Size(80, 17);
            this.rbCentimeters.TabIndex = 15;
            this.rbCentimeters.Text = "Centimeters";
            this.rbCentimeters.UseVisualStyleBackColor = true;
            // 
            // rbInches
            // 
            this.rbInches.AutoSize = true;
            this.rbInches.Location = new System.Drawing.Point(243, 42);
            this.rbInches.Name = "rbInches";
            this.rbInches.Size = new System.Drawing.Size(57, 17);
            this.rbInches.TabIndex = 14;
            this.rbInches.Text = "Inches";
            this.rbInches.UseVisualStyleBackColor = true;
            // 
            // rbPixels
            // 
            this.rbPixels.AutoSize = true;
            this.rbPixels.Checked = true;
            this.rbPixels.Location = new System.Drawing.Point(243, 19);
            this.rbPixels.Name = "rbPixels";
            this.rbPixels.Size = new System.Drawing.Size(52, 17);
            this.rbPixels.TabIndex = 13;
            this.rbPixels.TabStop = true;
            this.rbPixels.Text = "Pixels";
            this.rbPixels.UseVisualStyleBackColor = true;
            // 
            // txtSizeY
            // 
            this.txtSizeY.Location = new System.Drawing.Point(126, 19);
            this.txtSizeY.Name = "txtSizeY";
            this.txtSizeY.Size = new System.Drawing.Size(110, 20);
            this.txtSizeY.TabIndex = 12;
            this.txtSizeY.Text = "Y";
            // 
            // txtSizeX
            // 
            this.txtSizeX.Location = new System.Drawing.Point(10, 19);
            this.txtSizeX.Name = "txtSizeX";
            this.txtSizeX.Size = new System.Drawing.Size(110, 20);
            this.txtSizeX.TabIndex = 11;
            this.txtSizeX.Text = "X";
            // 
            // gbOther
            // 
            this.gbOther.Controls.Add(this.lblText);
            this.gbOther.Controls.Add(this.txtText);
            this.gbOther.Controls.Add(this.btnRedEye);
            this.gbOther.Controls.Add(this.txtOtherLR);
            this.gbOther.Controls.Add(this.txtOtherLL);
            this.gbOther.Controls.Add(this.txtOtherUR);
            this.gbOther.Controls.Add(this.txtOtherUL);
            this.gbOther.Location = new System.Drawing.Point(12, 625);
            this.gbOther.Name = "gbOther";
            this.gbOther.Size = new System.Drawing.Size(381, 93);
            this.gbOther.TabIndex = 3;
            this.gbOther.TabStop = false;
            this.gbOther.Text = "Other Stuff";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(7, 68);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(221, 13);
            this.lblText.TabIndex = 29;
            this.lblText.Text = "Add text defaults to black size 12 courier new";
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(253, 45);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(75, 23);
            this.txtText.TabIndex = 28;
            this.txtText.Text = "Add Text";
            this.txtText.UseVisualStyleBackColor = true;
            this.txtText.Click += new System.EventHandler(this.txtText_Click);
            // 
            // btnRedEye
            // 
            this.btnRedEye.Location = new System.Drawing.Point(253, 16);
            this.btnRedEye.Name = "btnRedEye";
            this.btnRedEye.Size = new System.Drawing.Size(75, 23);
            this.btnRedEye.TabIndex = 27;
            this.btnRedEye.Text = "Red Eye";
            this.btnRedEye.UseVisualStyleBackColor = true;
            this.btnRedEye.Click += new System.EventHandler(this.btnRedEye_Click);
            // 
            // txtOtherLR
            // 
            this.txtOtherLR.Location = new System.Drawing.Point(122, 45);
            this.txtOtherLR.Name = "txtOtherLR";
            this.txtOtherLR.Size = new System.Drawing.Size(110, 20);
            this.txtOtherLR.TabIndex = 26;
            this.txtOtherLR.Text = "1";
            // 
            // txtOtherLL
            // 
            this.txtOtherLL.Location = new System.Drawing.Point(6, 45);
            this.txtOtherLL.Name = "txtOtherLL";
            this.txtOtherLL.Size = new System.Drawing.Size(110, 20);
            this.txtOtherLL.TabIndex = 25;
            this.txtOtherLL.Text = "0";
            // 
            // txtOtherUR
            // 
            this.txtOtherUR.Location = new System.Drawing.Point(122, 19);
            this.txtOtherUR.Name = "txtOtherUR";
            this.txtOtherUR.Size = new System.Drawing.Size(110, 20);
            this.txtOtherUR.TabIndex = 24;
            this.txtOtherUR.Text = "1";
            // 
            // txtOtherUL
            // 
            this.txtOtherUL.Location = new System.Drawing.Point(6, 19);
            this.txtOtherUL.Name = "txtOtherUL";
            this.txtOtherUL.Size = new System.Drawing.Size(110, 20);
            this.txtOtherUL.TabIndex = 23;
            this.txtOtherUL.Text = "0";
            // 
            // pbOutput
            // 
            this.pbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbOutput.Location = new System.Drawing.Point(399, 238);
            this.pbOutput.Name = "pbOutput";
            this.pbOutput.Size = new System.Drawing.Size(508, 480);
            this.pbOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOutput.TabIndex = 4;
            this.pbOutput.TabStop = false;
            // 
            // gbImageHandler
            // 
            this.gbImageHandler.Controls.Add(this.btnSave);
            this.gbImageHandler.Controls.Add(this.txtCachePercentage);
            this.gbImageHandler.Controls.Add(this.btnCachePercentage);
            this.gbImageHandler.Controls.Add(this.btnNext);
            this.gbImageHandler.Controls.Add(this.btnPrev);
            this.gbImageHandler.Controls.Add(this.btnRemove);
            this.gbImageHandler.Controls.Add(this.btnAddImage);
            this.gbImageHandler.Location = new System.Drawing.Point(12, 151);
            this.gbImageHandler.Name = "gbImageHandler";
            this.gbImageHandler.Size = new System.Drawing.Size(381, 81);
            this.gbImageHandler.TabIndex = 5;
            this.gbImageHandler.TabStop = false;
            this.gbImageHandler.Text = "ImageHandler Stuff";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 47);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCachePercentage
            // 
            this.txtCachePercentage.Location = new System.Drawing.Point(253, 50);
            this.txtCachePercentage.Name = "txtCachePercentage";
            this.txtCachePercentage.Size = new System.Drawing.Size(75, 20);
            this.txtCachePercentage.TabIndex = 5;
            this.txtCachePercentage.Text = "80";
            // 
            // btnCachePercentage
            // 
            this.btnCachePercentage.Location = new System.Drawing.Point(91, 48);
            this.btnCachePercentage.Name = "btnCachePercentage";
            this.btnCachePercentage.Size = new System.Drawing.Size(156, 23);
            this.btnCachePercentage.TabIndex = 4;
            this.btnCachePercentage.Text = "Set Cache Percentage";
            this.btnCachePercentage.UseVisualStyleBackColor = true;
            this.btnCachePercentage.Click += new System.EventHandler(this.btnCachePercentage_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(253, 19);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(172, 19);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(91, 19);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(10, 19);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(75, 23);
            this.btnAddImage.TabIndex = 0;
            this.btnAddImage.Text = "Add";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // gbHistory
            // 
            this.gbHistory.Controls.Add(this.btnRedo);
            this.gbHistory.Controls.Add(this.btnUndo);
            this.gbHistory.Location = new System.Drawing.Point(12, 99);
            this.gbHistory.Name = "gbHistory";
            this.gbHistory.Size = new System.Drawing.Size(381, 46);
            this.gbHistory.TabIndex = 6;
            this.gbHistory.TabStop = false;
            this.gbHistory.Text = "HistoryHandler stuff";
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(87, 17);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(75, 23);
            this.btnRedo.TabIndex = 1;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(6, 17);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 0;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // gbAutomation
            // 
            this.gbAutomation.Controls.Add(this.rbBatch);
            this.gbAutomation.Controls.Add(this.btnStartAuto);
            this.gbAutomation.Controls.Add(this.rbDoSingle);
            this.gbAutomation.Controls.Add(this.rbAddToAuto);
            this.gbAutomation.Location = new System.Drawing.Point(12, 19);
            this.gbAutomation.Name = "gbAutomation";
            this.gbAutomation.Size = new System.Drawing.Size(271, 74);
            this.gbAutomation.TabIndex = 7;
            this.gbAutomation.TabStop = false;
            this.gbAutomation.Text = "Automation";
            // 
            // btnStartAuto
            // 
            this.btnStartAuto.Location = new System.Drawing.Point(7, 19);
            this.btnStartAuto.Name = "btnStartAuto";
            this.btnStartAuto.Size = new System.Drawing.Size(240, 23);
            this.btnStartAuto.TabIndex = 3;
            this.btnStartAuto.Text = "Start Automation";
            this.btnStartAuto.UseVisualStyleBackColor = true;
            this.btnStartAuto.Click += new System.EventHandler(this.BtnStartAutoClick);
            // 
            // rbBatch
            // 
            this.rbBatch.Location = new System.Drawing.Point(191, 44);
            this.rbBatch.Name = "rbBatch";
            this.rbBatch.Size = new System.Drawing.Size(70, 24);
            this.rbBatch.TabIndex = 2;
            this.rbBatch.Text = "Do Batch";
            this.rbBatch.UseVisualStyleBackColor = true;
            // 
            // rbDoSingle
            // 
            this.rbDoSingle.Checked = true;
            this.rbDoSingle.Location = new System.Drawing.Point(6, 44);
            this.rbDoSingle.Name = "rbDoSingle";
            this.rbDoSingle.Size = new System.Drawing.Size(84, 24);
            this.rbDoSingle.TabIndex = 1;
            this.rbDoSingle.TabStop = true;
            this.rbDoSingle.Text = "Do Single";
            this.rbDoSingle.UseVisualStyleBackColor = true;
            // 
            // rbAddToAuto
            // 
            this.rbAddToAuto.Location = new System.Drawing.Point(96, 44);
            this.rbAddToAuto.Name = "rbAddToAuto";
            this.rbAddToAuto.Size = new System.Drawing.Size(84, 24);
            this.rbAddToAuto.TabIndex = 0;
            this.rbAddToAuto.Text = "Add to Auto";
            this.rbAddToAuto.UseVisualStyleBackColor = true;
            // 
            // itHue
            // 
            this.itHue.Location = new System.Drawing.Point(289, 50);
            this.itHue.MinimumSize = new System.Drawing.Size(0, 56);
            this.itHue.Name = "itHue";
            this.itHue.Size = new System.Drawing.Size(150, 56);
            this.itHue.TabIndex = 14;
            this.itHue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itHue_MouseDown);
            this.itHue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itHue_MouseUp);
            // 
            // uippMain
            // 
            this.uippMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uippMain.Location = new System.Drawing.Point(778, 89);
            this.uippMain.MouseoverPopup = false;
            this.uippMain.Name = "uippMain";
            this.uippMain.Size = new System.Drawing.Size(128, 141);
            this.uippMain.TabIndex = 18;
            // 
            // uiChromaKey
            // 
            this.uiChromaKey.Location = new System.Drawing.Point(399, 129);
            this.uiChromaKey.Name = "uiChromaKey";
            this.uiChromaKey.Size = new System.Drawing.Size(167, 64);
            this.uiChromaKey.TabIndex = 17;
            // 
            // itBrightness
            // 
            this.itBrightness.Location = new System.Drawing.Point(399, 89);
            this.itBrightness.MinimumSize = new System.Drawing.Size(0, 56);
            this.itBrightness.Name = "itBrightness";
            this.itBrightness.Size = new System.Drawing.Size(150, 56);
            this.itBrightness.TabIndex = 8;
            this.itBrightness.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itBrightness_MouseDown);
            // 
            // uitlMain
            // 
            this.uitlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.uitlMain.AutoScroll = true;
            this.uitlMain.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.uitlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uitlMain.Location = new System.Drawing.Point(572, 89);
            this.uitlMain.Name = "uitlMain";
            this.uitlMain.Size = new System.Drawing.Size(200, 143);
            this.uitlMain.TabIndex = 16;
            this.uitlMain.Thumbnail_Mode = Photoman.UserInterface.UIWidgets.uiThumbnailList.ThumbnailMode.LoadedImages;
            // 
            // itSaturation
            // 
            this.itSaturation.Location = new System.Drawing.Point(601, 50);
            this.itSaturation.MinimumSize = new System.Drawing.Size(0, 56);
            this.itSaturation.Name = "itSaturation";
            this.itSaturation.Size = new System.Drawing.Size(150, 56);
            this.itSaturation.TabIndex = 15;
            this.itSaturation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itSaturation_MouseDown);
            this.itSaturation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itSaturation_MouseUp);
            // 
            // itLuminosity
            // 
            this.itLuminosity.Location = new System.Drawing.Point(445, 50);
            this.itLuminosity.MinimumSize = new System.Drawing.Size(0, 56);
            this.itLuminosity.Name = "itLuminosity";
            this.itLuminosity.Size = new System.Drawing.Size(150, 56);
            this.itLuminosity.TabIndex = 13;
            this.itLuminosity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itLuminosity_MouseDown);
            this.itLuminosity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itLuminosity_MouseUp);
            // 
            // itBlue
            // 
            this.itBlue.Location = new System.Drawing.Point(601, 19);
            this.itBlue.MinimumSize = new System.Drawing.Size(0, 56);
            this.itBlue.Name = "itBlue";
            this.itBlue.Size = new System.Drawing.Size(150, 56);
            this.itBlue.TabIndex = 11;
            this.itBlue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itBlue_MouseDown);
            this.itBlue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itBlue_MouseUp);
            // 
            // itGreen
            // 
            this.itGreen.Location = new System.Drawing.Point(445, 19);
            this.itGreen.MinimumSize = new System.Drawing.Size(0, 56);
            this.itGreen.Name = "itGreen";
            this.itGreen.Size = new System.Drawing.Size(150, 56);
            this.itGreen.TabIndex = 10;
            this.itGreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itGreen_MouseDown);
            this.itGreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itGreen_MouseUp);
            // 
            // itRed
            // 
            this.itRed.Location = new System.Drawing.Point(289, 19);
            this.itRed.MinimumSize = new System.Drawing.Size(0, 56);
            this.itRed.Name = "itRed";
            this.itRed.Size = new System.Drawing.Size(150, 56);
            this.itRed.TabIndex = 9;
            this.itRed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itRed_MouseDown);
            this.itRed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itRed_MouseUp);
            // 
            // TestGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 730);
            this.Controls.Add(this.gbHistory);
            this.Controls.Add(this.itHue);
            this.Controls.Add(this.gbAutomation);
            this.Controls.Add(this.uippMain);
            this.Controls.Add(this.uiChromaKey);
            this.Controls.Add(this.itBrightness);
            this.Controls.Add(this.uitlMain);
            this.Controls.Add(this.itSaturation);
            this.Controls.Add(this.itLuminosity);
            this.Controls.Add(this.itBlue);
            this.Controls.Add(this.itGreen);
            this.Controls.Add(this.itRed);
            this.Controls.Add(this.gbImageHandler);
            this.Controls.Add(this.pbOutput);
            this.Controls.Add(this.gbOther);
            this.Controls.Add(this.gbBasic);
            this.Controls.Add(this.gbRGB);
            this.Controls.Add(this.gbHLS);
            this.DoubleBuffered = true;
            this.Name = "TestGUI";
            this.Text = "Photoman Test GUI";
            this.Load += new System.EventHandler(this.TestGUI_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestGUI_FormClosed);
            this.gbHLS.ResumeLayout(false);
            this.gbHLS.PerformLayout();
            this.gbRGB.ResumeLayout(false);
            this.gbRGB.PerformLayout();
            this.gbBasic.ResumeLayout(false);
            this.gbBasic.PerformLayout();
            this.gbOther.ResumeLayout(false);
            this.gbOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).EndInit();
            this.gbImageHandler.ResumeLayout(false);
            this.gbImageHandler.PerformLayout();
            this.gbHistory.ResumeLayout(false);
            this.gbAutomation.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.RadioButton rbAddToAuto;
        internal System.Windows.Forms.RadioButton rbDoSingle;
        internal System.Windows.Forms.RadioButton rbBatch;
        private System.Windows.Forms.Button btnStartAuto;
        private System.Windows.Forms.GroupBox gbAutomation;

        #endregion

        private System.Windows.Forms.GroupBox gbHLS;
        private System.Windows.Forms.Button btnSaturation;
        private System.Windows.Forms.Button btnLuminosity;
        private System.Windows.Forms.Button btnHue;
        private System.Windows.Forms.Label lblHLS;
        private System.Windows.Forms.GroupBox gbRGB;
        private System.Windows.Forms.Label lblRGB;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnSepia;
        private System.Windows.Forms.Button btnDither;
        private System.Windows.Forms.Button btnGrayscale;
        private System.Windows.Forms.Button btnGamma;
        private System.Windows.Forms.Button btnBrightness;
        private System.Windows.Forms.GroupBox gbBasic;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.Button btnRotateR;
        private System.Windows.Forms.Button btnRotateL;
        private System.Windows.Forms.Button btnFlipR;
        private System.Windows.Forms.Button btnFlipL;
        private System.Windows.Forms.GroupBox gbOther;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button txtText;
        private System.Windows.Forms.Button btnRedEye;
        internal System.Windows.Forms.PictureBox pbOutput;
        internal System.Windows.Forms.TextBox txtHLSValue;
        internal System.Windows.Forms.TextBox txtRGB_Red;
        internal System.Windows.Forms.TextBox txtRGB_Green;
        internal System.Windows.Forms.TextBox txtRGB_Blue;
        internal System.Windows.Forms.TextBox txtSizeY;
        internal System.Windows.Forms.TextBox txtSizeX;
        internal System.Windows.Forms.RadioButton rbPercentage;
        internal System.Windows.Forms.RadioButton rbCentimeters;
        internal System.Windows.Forms.RadioButton rbInches;
        internal System.Windows.Forms.RadioButton rbPixels;
        internal System.Windows.Forms.TextBox txtCropLR;
        internal System.Windows.Forms.TextBox txtCropLL;
        internal System.Windows.Forms.TextBox txtCropUR;
        internal System.Windows.Forms.TextBox txtCropUL;
        internal System.Windows.Forms.TextBox txtOtherLR;
        internal System.Windows.Forms.TextBox txtOtherLL;
        internal System.Windows.Forms.TextBox txtOtherUR;
        internal System.Windows.Forms.TextBox txtOtherUL;
        private System.Windows.Forms.GroupBox gbImageHandler;
        private System.Windows.Forms.Button btnCachePercentage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddImage;
        internal System.Windows.Forms.TextBox txtCachePercentage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbHistory;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itHue;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itSaturation;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itBrightness;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itRed;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itGreen;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itBlue;
        public Photoman.UserInterface.UIWidgets.uiImageTrackbar itLuminosity;
        public Photoman.UserInterface.UIWidgets.uiThumbnailList uitlMain;
        internal Photoman.UserInterface.UIWidgets.uiChromakey uiChromaKey;
        internal Photoman.UserInterface.UIWidgets.uiwPreviewPopup uippMain;
    }
}