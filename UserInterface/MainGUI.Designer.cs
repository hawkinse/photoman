namespace Photoman.UserInterface
{
    partial class MainGUI
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
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiMain = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.removeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideDebugOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcRibbon = new System.Windows.Forms.TabControl();
            this.tabSizeDimensions = new System.Windows.Forms.TabPage();
            this.ppSizeDimensions = new Photoman.UserInterface.UIWidgets.uiwPreviewPopup();
            this.gbCrop = new System.Windows.Forms.GroupBox();
            this.btnCrop = new System.Windows.Forms.Button();
            this.gbFlip = new System.Windows.Forms.GroupBox();
            this.btnFlipY = new System.Windows.Forms.Button();
            this.btnFlipX = new System.Windows.Forms.Button();
            this.gbRotate = new System.Windows.Forms.GroupBox();
            this.btnRotatePlus90 = new System.Windows.Forms.Button();
            this.btnRotateMinus90 = new System.Windows.Forms.Button();
            this.gbResize = new System.Windows.Forms.GroupBox();
            this.btnApplyResize = new System.Windows.Forms.Button();
            this.cbMaintainAspectRatio = new System.Windows.Forms.CheckBox();
            this.rbResizePercentage = new System.Windows.Forms.RadioButton();
            this.rbResizePixels = new System.Windows.Forms.RadioButton();
            this.txtResizeHeight = new System.Windows.Forms.TextBox();
            this.txtResizeWidth = new System.Windows.Forms.TextBox();
            this.lblResizeHeight = new System.Windows.Forms.Label();
            this.lblResizeWidth = new System.Windows.Forms.Label();
            this.tabColor = new System.Windows.Forms.TabPage();
            this.ppColor = new Photoman.UserInterface.UIWidgets.uiwPreviewPopup();
            this.gbColor = new System.Windows.Forms.GroupBox();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.lblSaturation = new System.Windows.Forms.Label();
            this.lblLuminosity = new System.Windows.Forms.Label();
            this.lblHue = new System.Windows.Forms.Label();
            this.itBrightness = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itBlue = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itGreen = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itRed = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itSaturation = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itLuminosity = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.itHue = new Photoman.UserInterface.UIWidgets.uiImageTrackbar();
            this.tabSpecial = new System.Windows.Forms.TabPage();
            this.ppSpecial = new Photoman.UserInterface.UIWidgets.uiwPreviewPopup();
            this.gbText = new System.Windows.Forms.GroupBox();
            this.txtAddText = new System.Windows.Forms.TextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.btnApplyText = new System.Windows.Forms.Button();
            this.cbTextColor = new System.Windows.Forms.ComboBox();
            this.btnStrikethrough = new System.Windows.Forms.Button();
            this.btnUnderline = new System.Windows.Forms.Button();
            this.btnItalic = new System.Windows.Forms.Button();
            this.btnBold = new System.Windows.Forms.Button();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.gbChromakey = new System.Windows.Forms.GroupBox();
            this.ckMain = new Photoman.UserInterface.UIWidgets.uiChromakey();
            this.gbSpecial = new System.Windows.Forms.GroupBox();
            this.btnRedEye = new System.Windows.Forms.Button();
            this.btnInvert = new System.Windows.Forms.Button();
            this.btnSepia = new System.Windows.Forms.Button();
            this.btnDither = new System.Windows.Forms.Button();
            this.btnGrayscale = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.cbxEnableVisualEffects = new System.Windows.Forms.CheckBox();
            this.cbxBatterySaveMode = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.nudCachePercentage = new System.Windows.Forms.NumericUpDown();
            this.lblDitherMethod = new System.Windows.Forms.Label();
            this.lblMemForCache = new System.Windows.Forms.Label();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tslImageCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslUndoRedoCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslCacheStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslExceptionCount = new System.Windows.Forms.ToolStripLabel();
            this.pnlListsContainer = new System.Windows.Forms.Panel();
            this.btnApplyActions = new System.Windows.Forms.Button();
            this.tlLoadedImages = new Photoman.UserInterface.UIWidgets.uiThumbnailList();
            this.btnAutomationList = new System.Windows.Forms.Button();
            this.btnHistoryList = new System.Windows.Forms.Button();
            this.btnImageList = new System.Windows.Forms.Button();
            this.tlImageHistory = new Photoman.UserInterface.UIWidgets.uiThumbnailList();
            this.tlAutomationActions = new Photoman.UserInterface.UIWidgets.uiThumbnailList();
            this.mpbMain = new Photoman.UserInterface.UIWidgets.uiMousePictureBox();
            this.msMain.SuspendLayout();
            this.tcRibbon.SuspendLayout();
            this.tabSizeDimensions.SuspendLayout();
            this.gbCrop.SuspendLayout();
            this.gbFlip.SuspendLayout();
            this.gbRotate.SuspendLayout();
            this.gbResize.SuspendLayout();
            this.tabColor.SuspendLayout();
            this.gbColor.SuspendLayout();
            this.tabSpecial.SuspendLayout();
            this.gbText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.gbChromakey.SuspendLayout();
            this.gbSpecial.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCachePercentage)).BeginInit();
            this.tsMain.SuspendLayout();
            this.pnlListsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMain,
            this.tsmiUndo,
            this.tsmiRedo,
            this.debugToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1008, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiMain
            // 
            this.tsmiMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.removeImageToolStripMenuItem,
            this.tsmiSaveOne,
            this.tsmiSaveAll,
            this.aboutToolStripMenuItem,
            this.tsmiExit});
            this.tsmiMain.Name = "tsmiMain";
            this.tsmiMain.Size = new System.Drawing.Size(50, 20);
            this.tsmiMain.Text = "Menu";
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(153, 22);
            this.tsmiOpen.Text = "Add Image";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // removeImageToolStripMenuItem
            // 
            this.removeImageToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.removeImageToolStripMenuItem.Name = "removeImageToolStripMenuItem";
            this.removeImageToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.removeImageToolStripMenuItem.Text = "Remove Image";
            this.removeImageToolStripMenuItem.Click += new System.EventHandler(this.removeImageToolStripMenuItem_Click);
            // 
            // tsmiSaveOne
            // 
            this.tsmiSaveOne.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tsmiSaveOne.Name = "tsmiSaveOne";
            this.tsmiSaveOne.Size = new System.Drawing.Size(153, 22);
            this.tsmiSaveOne.Text = "Save Single";
            this.tsmiSaveOne.Click += new System.EventHandler(this.tsmiSaveOne_Click);
            // 
            // tsmiSaveAll
            // 
            this.tsmiSaveAll.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tsmiSaveAll.Name = "tsmiSaveAll";
            this.tsmiSaveAll.Size = new System.Drawing.Size(153, 22);
            this.tsmiSaveAll.Text = "Save All";
            this.tsmiSaveAll.Click += new System.EventHandler(this.tsmiSaveAll_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // tsmiExit
            // 
            this.tsmiExit.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(153, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // tsmiUndo
            // 
            this.tsmiUndo.Name = "tsmiUndo";
            this.tsmiUndo.Size = new System.Drawing.Size(64, 20);
            this.tsmiUndo.Text = "=Undo=";
            this.tsmiUndo.Click += new System.EventHandler(this.tsmiUndo_Click);
            // 
            // tsmiRedo
            // 
            this.tsmiRedo.Name = "tsmiRedo";
            this.tsmiRedo.Size = new System.Drawing.Size(62, 20);
            this.tsmiRedo.Text = "=Redo=";
            this.tsmiRedo.Click += new System.EventHandler(this.tsmiRedo_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideDebugOutputToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // showHideDebugOutputToolStripMenuItem
            // 
            this.showHideDebugOutputToolStripMenuItem.Name = "showHideDebugOutputToolStripMenuItem";
            this.showHideDebugOutputToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.showHideDebugOutputToolStripMenuItem.Text = "Show/Hide debug output";
            this.showHideDebugOutputToolStripMenuItem.Click += new System.EventHandler(this.showHideDebugOutputToolStripMenuItem_Click);
            // 
            // tcRibbon
            // 
            this.tcRibbon.Controls.Add(this.tabSizeDimensions);
            this.tcRibbon.Controls.Add(this.tabColor);
            this.tcRibbon.Controls.Add(this.tabSpecial);
            this.tcRibbon.Controls.Add(this.tabSettings);
            this.tcRibbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcRibbon.Location = new System.Drawing.Point(0, 24);
            this.tcRibbon.Name = "tcRibbon";
            this.tcRibbon.SelectedIndex = 0;
            this.tcRibbon.Size = new System.Drawing.Size(1008, 120);
            this.tcRibbon.TabIndex = 1;
            // 
            // tabSizeDimensions
            // 
            this.tabSizeDimensions.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabSizeDimensions.Controls.Add(this.ppSizeDimensions);
            this.tabSizeDimensions.Controls.Add(this.gbCrop);
            this.tabSizeDimensions.Controls.Add(this.gbFlip);
            this.tabSizeDimensions.Controls.Add(this.gbRotate);
            this.tabSizeDimensions.Controls.Add(this.gbResize);
            this.tabSizeDimensions.Location = new System.Drawing.Point(4, 22);
            this.tabSizeDimensions.Name = "tabSizeDimensions";
            this.tabSizeDimensions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSizeDimensions.Size = new System.Drawing.Size(1000, 94);
            this.tabSizeDimensions.TabIndex = 0;
            this.tabSizeDimensions.Text = "Size and Dimensions";
            // 
            // ppSizeDimensions
            // 
            this.ppSizeDimensions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ppSizeDimensions.Dock = System.Windows.Forms.DockStyle.Right;
            this.ppSizeDimensions.Location = new System.Drawing.Point(869, 3);
            this.ppSizeDimensions.MouseoverPopup = false;
            this.ppSizeDimensions.Name = "ppSizeDimensions";
            this.ppSizeDimensions.Size = new System.Drawing.Size(128, 88);
            this.ppSizeDimensions.TabIndex = 4;
            // 
            // gbCrop
            // 
            this.gbCrop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbCrop.Controls.Add(this.btnCrop);
            this.gbCrop.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCrop.Location = new System.Drawing.Point(562, 3);
            this.gbCrop.Name = "gbCrop";
            this.gbCrop.Size = new System.Drawing.Size(78, 88);
            this.gbCrop.TabIndex = 3;
            this.gbCrop.TabStop = false;
            this.gbCrop.Text = "Crop";
            // 
            // btnCrop
            // 
            this.btnCrop.Location = new System.Drawing.Point(6, 14);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(66, 66);
            this.btnCrop.TabIndex = 0;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // gbFlip
            // 
            this.gbFlip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbFlip.Controls.Add(this.btnFlipY);
            this.gbFlip.Controls.Add(this.btnFlipX);
            this.gbFlip.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbFlip.Location = new System.Drawing.Point(413, 3);
            this.gbFlip.Name = "gbFlip";
            this.gbFlip.Size = new System.Drawing.Size(149, 88);
            this.gbFlip.TabIndex = 2;
            this.gbFlip.TabStop = false;
            this.gbFlip.Text = "Flip";
            // 
            // btnFlipY
            // 
            this.btnFlipY.Location = new System.Drawing.Point(78, 14);
            this.btnFlipY.Name = "btnFlipY";
            this.btnFlipY.Size = new System.Drawing.Size(66, 66);
            this.btnFlipY.TabIndex = 1;
            this.btnFlipY.Text = "Y Axis";
            this.btnFlipY.UseVisualStyleBackColor = true;
            this.btnFlipY.Click += new System.EventHandler(this.btnFlipY_Click);
            this.btnFlipY.MouseEnter += new System.EventHandler(this.btnFlipY_MouseEnter);
            this.btnFlipY.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // btnFlipX
            // 
            this.btnFlipX.Location = new System.Drawing.Point(6, 14);
            this.btnFlipX.Name = "btnFlipX";
            this.btnFlipX.Size = new System.Drawing.Size(66, 66);
            this.btnFlipX.TabIndex = 0;
            this.btnFlipX.Text = "X Axis";
            this.btnFlipX.UseVisualStyleBackColor = true;
            this.btnFlipX.Click += new System.EventHandler(this.btnFlipX_Click);
            this.btnFlipX.MouseEnter += new System.EventHandler(this.btnFlipX_MouseEnter);
            this.btnFlipX.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // gbRotate
            // 
            this.gbRotate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbRotate.Controls.Add(this.btnRotatePlus90);
            this.gbRotate.Controls.Add(this.btnRotateMinus90);
            this.gbRotate.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbRotate.Location = new System.Drawing.Point(267, 3);
            this.gbRotate.Name = "gbRotate";
            this.gbRotate.Size = new System.Drawing.Size(146, 88);
            this.gbRotate.TabIndex = 1;
            this.gbRotate.TabStop = false;
            this.gbRotate.Text = "Rotation";
            // 
            // btnRotatePlus90
            // 
            this.btnRotatePlus90.Location = new System.Drawing.Point(74, 14);
            this.btnRotatePlus90.Name = "btnRotatePlus90";
            this.btnRotatePlus90.Size = new System.Drawing.Size(66, 66);
            this.btnRotatePlus90.TabIndex = 1;
            this.btnRotatePlus90.Text = "+90";
            this.btnRotatePlus90.UseVisualStyleBackColor = true;
            this.btnRotatePlus90.Click += new System.EventHandler(this.btnRotatePlus90_Click);
            this.btnRotatePlus90.MouseEnter += new System.EventHandler(this.btnRotatePlus90_MouseEnter);
            this.btnRotatePlus90.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // btnRotateMinus90
            // 
            this.btnRotateMinus90.Location = new System.Drawing.Point(6, 14);
            this.btnRotateMinus90.Name = "btnRotateMinus90";
            this.btnRotateMinus90.Size = new System.Drawing.Size(66, 66);
            this.btnRotateMinus90.TabIndex = 0;
            this.btnRotateMinus90.Text = "-90";
            this.btnRotateMinus90.UseVisualStyleBackColor = true;
            this.btnRotateMinus90.Click += new System.EventHandler(this.btnRotateMinus90_Click);
            this.btnRotateMinus90.MouseEnter += new System.EventHandler(this.btnRotateMinus90_MouseEnter);
            this.btnRotateMinus90.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // gbResize
            // 
            this.gbResize.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbResize.Controls.Add(this.btnApplyResize);
            this.gbResize.Controls.Add(this.cbMaintainAspectRatio);
            this.gbResize.Controls.Add(this.rbResizePercentage);
            this.gbResize.Controls.Add(this.rbResizePixels);
            this.gbResize.Controls.Add(this.txtResizeHeight);
            this.gbResize.Controls.Add(this.txtResizeWidth);
            this.gbResize.Controls.Add(this.lblResizeHeight);
            this.gbResize.Controls.Add(this.lblResizeWidth);
            this.gbResize.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbResize.Location = new System.Drawing.Point(3, 3);
            this.gbResize.Name = "gbResize";
            this.gbResize.Size = new System.Drawing.Size(264, 88);
            this.gbResize.TabIndex = 0;
            this.gbResize.TabStop = false;
            this.gbResize.Text = "Resize";
            // 
            // btnApplyResize
            // 
            this.btnApplyResize.Location = new System.Drawing.Point(193, 14);
            this.btnApplyResize.Name = "btnApplyResize";
            this.btnApplyResize.Size = new System.Drawing.Size(66, 66);
            this.btnApplyResize.TabIndex = 7;
            this.btnApplyResize.Text = "Apply";
            this.btnApplyResize.UseVisualStyleBackColor = true;
            this.btnApplyResize.Click += new System.EventHandler(this.btnApplyResize_Click);
            this.btnApplyResize.MouseEnter += new System.EventHandler(this.btnApplyResize_MouseEnter);
            // 
            // cbMaintainAspectRatio
            // 
            this.cbMaintainAspectRatio.AutoSize = true;
            this.cbMaintainAspectRatio.Location = new System.Drawing.Point(10, 68);
            this.cbMaintainAspectRatio.Name = "cbMaintainAspectRatio";
            this.cbMaintainAspectRatio.Size = new System.Drawing.Size(130, 17);
            this.cbMaintainAspectRatio.TabIndex = 6;
            this.cbMaintainAspectRatio.Text = "Maintain Aspect Ratio";
            this.cbMaintainAspectRatio.UseVisualStyleBackColor = true;
            // 
            // rbResizePercentage
            // 
            this.rbResizePercentage.AutoSize = true;
            this.rbResizePercentage.Location = new System.Drawing.Point(125, 39);
            this.rbResizePercentage.Name = "rbResizePercentage";
            this.rbResizePercentage.Size = new System.Drawing.Size(62, 17);
            this.rbResizePercentage.TabIndex = 5;
            this.rbResizePercentage.Text = "Percent";
            this.rbResizePercentage.UseVisualStyleBackColor = true;
            this.rbResizePercentage.CheckedChanged += new System.EventHandler(this.rbResizePercentage_CheckedChanged);
            // 
            // rbResizePixels
            // 
            this.rbResizePixels.AutoSize = true;
            this.rbResizePixels.Checked = true;
            this.rbResizePixels.Location = new System.Drawing.Point(125, 16);
            this.rbResizePixels.Name = "rbResizePixels";
            this.rbResizePixels.Size = new System.Drawing.Size(52, 17);
            this.rbResizePixels.TabIndex = 4;
            this.rbResizePixels.TabStop = true;
            this.rbResizePixels.Text = "Pixels";
            this.rbResizePixels.UseVisualStyleBackColor = true;
            this.rbResizePixels.CheckedChanged += new System.EventHandler(this.rbResizePixels_CheckedChanged);
            // 
            // txtResizeHeight
            // 
            this.txtResizeHeight.Location = new System.Drawing.Point(48, 43);
            this.txtResizeHeight.Name = "txtResizeHeight";
            this.txtResizeHeight.Size = new System.Drawing.Size(71, 20);
            this.txtResizeHeight.TabIndex = 3;
            // 
            // txtResizeWidth
            // 
            this.txtResizeWidth.Location = new System.Drawing.Point(48, 17);
            this.txtResizeWidth.Name = "txtResizeWidth";
            this.txtResizeWidth.Size = new System.Drawing.Size(71, 20);
            this.txtResizeWidth.TabIndex = 2;
            // 
            // lblResizeHeight
            // 
            this.lblResizeHeight.AutoSize = true;
            this.lblResizeHeight.Location = new System.Drawing.Point(7, 46);
            this.lblResizeHeight.Name = "lblResizeHeight";
            this.lblResizeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblResizeHeight.TabIndex = 1;
            this.lblResizeHeight.Text = "Height";
            // 
            // lblResizeWidth
            // 
            this.lblResizeWidth.AutoSize = true;
            this.lblResizeWidth.Location = new System.Drawing.Point(7, 20);
            this.lblResizeWidth.Name = "lblResizeWidth";
            this.lblResizeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblResizeWidth.TabIndex = 0;
            this.lblResizeWidth.Text = "Width";
            // 
            // tabColor
            // 
            this.tabColor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabColor.Controls.Add(this.ppColor);
            this.tabColor.Controls.Add(this.gbColor);
            this.tabColor.Location = new System.Drawing.Point(4, 22);
            this.tabColor.Name = "tabColor";
            this.tabColor.Padding = new System.Windows.Forms.Padding(3);
            this.tabColor.Size = new System.Drawing.Size(1000, 94);
            this.tabColor.TabIndex = 1;
            this.tabColor.Text = "Color";
            // 
            // ppColor
            // 
            this.ppColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ppColor.Dock = System.Windows.Forms.DockStyle.Right;
            this.ppColor.Location = new System.Drawing.Point(869, 3);
            this.ppColor.MouseoverPopup = false;
            this.ppColor.Name = "ppColor";
            this.ppColor.Size = new System.Drawing.Size(128, 88);
            this.ppColor.TabIndex = 5;
            // 
            // gbColor
            // 
            this.gbColor.Controls.Add(this.lblBrightness);
            this.gbColor.Controls.Add(this.lblBlue);
            this.gbColor.Controls.Add(this.lblGreen);
            this.gbColor.Controls.Add(this.lblRed);
            this.gbColor.Controls.Add(this.lblSaturation);
            this.gbColor.Controls.Add(this.lblLuminosity);
            this.gbColor.Controls.Add(this.lblHue);
            this.gbColor.Controls.Add(this.itBrightness);
            this.gbColor.Controls.Add(this.itBlue);
            this.gbColor.Controls.Add(this.itGreen);
            this.gbColor.Controls.Add(this.itRed);
            this.gbColor.Controls.Add(this.itSaturation);
            this.gbColor.Controls.Add(this.itLuminosity);
            this.gbColor.Controls.Add(this.itHue);
            this.gbColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbColor.Location = new System.Drawing.Point(3, 3);
            this.gbColor.Name = "gbColor";
            this.gbColor.Size = new System.Drawing.Size(818, 88);
            this.gbColor.TabIndex = 0;
            this.gbColor.TabStop = false;
            this.gbColor.Text = "Color";
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(711, 62);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(56, 13);
            this.lblBrightness.TabIndex = 13;
            this.lblBrightness.Text = "Brightness";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(595, 62);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(28, 13);
            this.lblBlue.TabIndex = 12;
            this.lblBlue.Text = "Blue";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(479, 62);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(36, 13);
            this.lblGreen.TabIndex = 11;
            this.lblGreen.Text = "Green";
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(363, 62);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(27, 13);
            this.lblRed.TabIndex = 10;
            this.lblRed.Text = "Red";
            // 
            // lblSaturation
            // 
            this.lblSaturation.AutoSize = true;
            this.lblSaturation.Location = new System.Drawing.Point(249, 62);
            this.lblSaturation.Name = "lblSaturation";
            this.lblSaturation.Size = new System.Drawing.Size(55, 13);
            this.lblSaturation.TabIndex = 9;
            this.lblSaturation.Text = "Saturation";
            // 
            // lblLuminosity
            // 
            this.lblLuminosity.AutoSize = true;
            this.lblLuminosity.Location = new System.Drawing.Point(132, 62);
            this.lblLuminosity.Name = "lblLuminosity";
            this.lblLuminosity.Size = new System.Drawing.Size(56, 13);
            this.lblLuminosity.TabIndex = 8;
            this.lblLuminosity.Text = "Luminosity";
            // 
            // lblHue
            // 
            this.lblHue.AutoSize = true;
            this.lblHue.Location = new System.Drawing.Point(16, 62);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(27, 13);
            this.lblHue.TabIndex = 7;
            this.lblHue.Text = "Hue";
            // 
            // itBrightness
            // 
            this.itBrightness.Location = new System.Drawing.Point(702, 19);
            this.itBrightness.MinimumSize = new System.Drawing.Size(0, 56);
            this.itBrightness.Name = "itBrightness";
            this.itBrightness.Size = new System.Drawing.Size(110, 56);
            this.itBrightness.TabIndex = 6;
            this.itBrightness.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itBrightness_MouseDown);
            this.itBrightness.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itBrightness.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itBrightness_MouseUp);
            // 
            // itBlue
            // 
            this.itBlue.Location = new System.Drawing.Point(586, 19);
            this.itBlue.MinimumSize = new System.Drawing.Size(0, 56);
            this.itBlue.Name = "itBlue";
            this.itBlue.Size = new System.Drawing.Size(110, 56);
            this.itBlue.TabIndex = 5;
            this.itBlue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itBlue_MouseDown);
            this.itBlue.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itBlue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itBlue_MouseUp);
            // 
            // itGreen
            // 
            this.itGreen.Location = new System.Drawing.Point(470, 19);
            this.itGreen.MinimumSize = new System.Drawing.Size(0, 56);
            this.itGreen.Name = "itGreen";
            this.itGreen.Size = new System.Drawing.Size(110, 56);
            this.itGreen.TabIndex = 4;
            this.itGreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itGreen_MouseDown);
            this.itGreen.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itGreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itGreen_MouseUp);
            // 
            // itRed
            // 
            this.itRed.Location = new System.Drawing.Point(354, 19);
            this.itRed.MinimumSize = new System.Drawing.Size(0, 56);
            this.itRed.Name = "itRed";
            this.itRed.Size = new System.Drawing.Size(110, 56);
            this.itRed.TabIndex = 3;
            this.itRed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itRed_MouseDown);
            this.itRed.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itRed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itRed_MouseUp);
            // 
            // itSaturation
            // 
            this.itSaturation.Location = new System.Drawing.Point(238, 19);
            this.itSaturation.MinimumSize = new System.Drawing.Size(0, 56);
            this.itSaturation.Name = "itSaturation";
            this.itSaturation.Size = new System.Drawing.Size(110, 56);
            this.itSaturation.TabIndex = 2;
            this.itSaturation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itSaturation_MouseDown);
            this.itSaturation.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itSaturation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itSaturation_MouseUp);
            // 
            // itLuminosity
            // 
            this.itLuminosity.Location = new System.Drawing.Point(122, 19);
            this.itLuminosity.MinimumSize = new System.Drawing.Size(0, 56);
            this.itLuminosity.Name = "itLuminosity";
            this.itLuminosity.Size = new System.Drawing.Size(110, 56);
            this.itLuminosity.TabIndex = 1;
            this.itLuminosity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itLuminosity_MouseDown);
            this.itLuminosity.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itLuminosity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itLuminosity_MouseUp);
            // 
            // itHue
            // 
            this.itHue.Location = new System.Drawing.Point(6, 19);
            this.itHue.MinimumSize = new System.Drawing.Size(0, 56);
            this.itHue.Name = "itHue";
            this.itHue.Size = new System.Drawing.Size(110, 56);
            this.itHue.TabIndex = 0;
            this.itHue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itHue_MouseDown);
            this.itHue.MouseLeave += new System.EventHandler(this.ClearPreview);
            this.itHue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itHue_MouseUp);
            // 
            // tabSpecial
            // 
            this.tabSpecial.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabSpecial.Controls.Add(this.ppSpecial);
            this.tabSpecial.Controls.Add(this.gbText);
            this.tabSpecial.Controls.Add(this.gbChromakey);
            this.tabSpecial.Controls.Add(this.gbSpecial);
            this.tabSpecial.Location = new System.Drawing.Point(4, 22);
            this.tabSpecial.Name = "tabSpecial";
            this.tabSpecial.Size = new System.Drawing.Size(1000, 94);
            this.tabSpecial.TabIndex = 2;
            this.tabSpecial.Text = "Special";
            // 
            // ppSpecial
            // 
            this.ppSpecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ppSpecial.Dock = System.Windows.Forms.DockStyle.Right;
            this.ppSpecial.Location = new System.Drawing.Point(872, 0);
            this.ppSpecial.MouseoverPopup = false;
            this.ppSpecial.Name = "ppSpecial";
            this.ppSpecial.Size = new System.Drawing.Size(128, 94);
            this.ppSpecial.TabIndex = 8;
            // 
            // gbText
            // 
            this.gbText.Controls.Add(this.txtAddText);
            this.gbText.Controls.Add(this.lblText);
            this.gbText.Controls.Add(this.btnApplyText);
            this.gbText.Controls.Add(this.cbTextColor);
            this.gbText.Controls.Add(this.btnStrikethrough);
            this.gbText.Controls.Add(this.btnUnderline);
            this.gbText.Controls.Add(this.btnItalic);
            this.gbText.Controls.Add(this.btnBold);
            this.gbText.Controls.Add(this.nudFontSize);
            this.gbText.Controls.Add(this.cbFont);
            this.gbText.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbText.Location = new System.Drawing.Point(560, 0);
            this.gbText.Name = "gbText";
            this.gbText.Size = new System.Drawing.Size(272, 94);
            this.gbText.TabIndex = 7;
            this.gbText.TabStop = false;
            this.gbText.Text = "Add Text";
            // 
            // txtAddText
            // 
            this.txtAddText.Location = new System.Drawing.Point(38, 15);
            this.txtAddText.Name = "txtAddText";
            this.txtAddText.Size = new System.Drawing.Size(153, 20);
            this.txtAddText.TabIndex = 9;
            this.txtAddText.Text = "Your text goes here...";
            this.txtAddText.TextChanged += new System.EventHandler(this.txtAddText_TextChanged);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(4, 18);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(28, 13);
            this.lblText.TabIndex = 8;
            this.lblText.Text = "Text";
            // 
            // btnApplyText
            // 
            this.btnApplyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyText.Location = new System.Drawing.Point(196, 19);
            this.btnApplyText.Name = "btnApplyText";
            this.btnApplyText.Size = new System.Drawing.Size(66, 66);
            this.btnApplyText.TabIndex = 7;
            this.btnApplyText.Text = "Add Text";
            this.btnApplyText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnApplyText.UseVisualStyleBackColor = true;
            this.btnApplyText.Click += new System.EventHandler(this.btnApplyText_Click);
            // 
            // cbTextColor
            // 
            this.cbTextColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbTextColor.DropDownWidth = 200;
            this.cbTextColor.FormattingEnabled = true;
            this.cbTextColor.Location = new System.Drawing.Point(102, 41);
            this.cbTextColor.Name = "cbTextColor";
            this.cbTextColor.Size = new System.Drawing.Size(89, 21);
            this.cbTextColor.TabIndex = 6;
            this.cbTextColor.Text = "C";
            this.cbTextColor.TextChanged += new System.EventHandler(this.cbTextColor_TextChanged);
            // 
            // btnStrikethrough
            // 
            this.btnStrikethrough.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStrikethrough.Location = new System.Drawing.Point(70, 68);
            this.btnStrikethrough.Name = "btnStrikethrough";
            this.btnStrikethrough.Size = new System.Drawing.Size(20, 20);
            this.btnStrikethrough.TabIndex = 5;
            this.btnStrikethrough.Text = "S";
            this.btnStrikethrough.UseVisualStyleBackColor = true;
            this.btnStrikethrough.Click += new System.EventHandler(this.btnStrikethrough_Click);
            // 
            // btnUnderline
            // 
            this.btnUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnderline.Location = new System.Drawing.Point(49, 68);
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new System.Drawing.Size(20, 20);
            this.btnUnderline.TabIndex = 4;
            this.btnUnderline.Text = "U";
            this.btnUnderline.UseVisualStyleBackColor = true;
            this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItalic.Location = new System.Drawing.Point(28, 68);
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(20, 20);
            this.btnItalic.TabIndex = 3;
            this.btnItalic.Text = "I";
            this.btnItalic.UseVisualStyleBackColor = true;
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnBold
            // 
            this.btnBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBold.Location = new System.Drawing.Point(7, 68);
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(20, 20);
            this.btnBold.TabIndex = 2;
            this.btnBold.Text = "B";
            this.btnBold.UseVisualStyleBackColor = true;
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // nudFontSize
            // 
            this.nudFontSize.Location = new System.Drawing.Point(96, 68);
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(35, 20);
            this.nudFontSize.TabIndex = 1;
            this.nudFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // cbFont
            // 
            this.cbFont.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFont.DropDownWidth = 200;
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Location = new System.Drawing.Point(7, 41);
            this.cbFont.Name = "cbFont";
            this.cbFont.Size = new System.Drawing.Size(89, 21);
            this.cbFont.TabIndex = 0;
            this.cbFont.SelectedIndexChanged += new System.EventHandler(this.cbFont_SelectedIndexChanged);
            this.cbFont.TextChanged += new System.EventHandler(this.cbFont_TextChanged);
            // 
            // gbChromakey
            // 
            this.gbChromakey.Controls.Add(this.ckMain);
            this.gbChromakey.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbChromakey.Location = new System.Drawing.Point(371, 0);
            this.gbChromakey.Name = "gbChromakey";
            this.gbChromakey.Size = new System.Drawing.Size(189, 94);
            this.gbChromakey.TabIndex = 6;
            this.gbChromakey.TabStop = false;
            this.gbChromakey.Text = "Chromakey";
            // 
            // ckMain
            // 
            this.ckMain.Location = new System.Drawing.Point(6, 19);
            this.ckMain.Name = "ckMain";
            this.ckMain.Size = new System.Drawing.Size(174, 64);
            this.ckMain.TabIndex = 0;
            this.ckMain.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // gbSpecial
            // 
            this.gbSpecial.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbSpecial.Controls.Add(this.btnRedEye);
            this.gbSpecial.Controls.Add(this.btnInvert);
            this.gbSpecial.Controls.Add(this.btnSepia);
            this.gbSpecial.Controls.Add(this.btnDither);
            this.gbSpecial.Controls.Add(this.btnGrayscale);
            this.gbSpecial.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbSpecial.Location = new System.Drawing.Point(0, 0);
            this.gbSpecial.Name = "gbSpecial";
            this.gbSpecial.Size = new System.Drawing.Size(371, 94);
            this.gbSpecial.TabIndex = 5;
            this.gbSpecial.TabStop = false;
            this.gbSpecial.Text = "Special Operations";
            // 
            // btnRedEye
            // 
            this.btnRedEye.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedEye.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRedEye.Location = new System.Drawing.Point(296, 19);
            this.btnRedEye.Name = "btnRedEye";
            this.btnRedEye.Size = new System.Drawing.Size(66, 66);
            this.btnRedEye.TabIndex = 4;
            this.btnRedEye.Text = "Red Eye Removal";
            this.btnRedEye.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRedEye.UseVisualStyleBackColor = true;
            this.btnRedEye.Click += new System.EventHandler(this.btnRedEye_Click);
            // 
            // btnInvert
            // 
            this.btnInvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvert.Image = global::Photoman.Properties.Resources.Invert2;
            this.btnInvert.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInvert.Location = new System.Drawing.Point(224, 19);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(66, 66);
            this.btnInvert.TabIndex = 3;
            this.btnInvert.Text = "Invert";
            this.btnInvert.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            this.btnInvert.MouseEnter += new System.EventHandler(this.btnInvert_MouseEnter);
            this.btnInvert.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // btnSepia
            // 
            this.btnSepia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSepia.Image = global::Photoman.Properties.Resources.Sepia2;
            this.btnSepia.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSepia.Location = new System.Drawing.Point(152, 19);
            this.btnSepia.Name = "btnSepia";
            this.btnSepia.Size = new System.Drawing.Size(66, 66);
            this.btnSepia.TabIndex = 2;
            this.btnSepia.Text = "Sepia";
            this.btnSepia.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSepia.UseVisualStyleBackColor = true;
            this.btnSepia.Click += new System.EventHandler(this.btnSepia_Click);
            this.btnSepia.MouseEnter += new System.EventHandler(this.btnSepia_MouseEnter);
            this.btnSepia.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // btnDither
            // 
            this.btnDither.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDither.Image = global::Photoman.Properties.Resources.Dither2;
            this.btnDither.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDither.Location = new System.Drawing.Point(80, 19);
            this.btnDither.Name = "btnDither";
            this.btnDither.Size = new System.Drawing.Size(66, 66);
            this.btnDither.TabIndex = 1;
            this.btnDither.Text = "BW Dither";
            this.btnDither.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDither.UseVisualStyleBackColor = true;
            this.btnDither.Click += new System.EventHandler(this.btnDither_Click);
            this.btnDither.MouseEnter += new System.EventHandler(this.btnDither_MouseEnter);
            this.btnDither.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // btnGrayscale
            // 
            this.btnGrayscale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrayscale.Image = global::Photoman.Properties.Resources.Grayscale2;
            this.btnGrayscale.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGrayscale.Location = new System.Drawing.Point(8, 19);
            this.btnGrayscale.Name = "btnGrayscale";
            this.btnGrayscale.Size = new System.Drawing.Size(66, 66);
            this.btnGrayscale.TabIndex = 0;
            this.btnGrayscale.Text = "Grayscale";
            this.btnGrayscale.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGrayscale.UseVisualStyleBackColor = true;
            this.btnGrayscale.Click += new System.EventHandler(this.btnGrayscale_Click);
            this.btnGrayscale.MouseEnter += new System.EventHandler(this.btnGrayscale_MouseEnter);
            this.btnGrayscale.MouseLeave += new System.EventHandler(this.ClearPreview);
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabSettings.Controls.Add(this.gbSettings);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(1000, 94);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Settings";
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.cbxEnableVisualEffects);
            this.gbSettings.Controls.Add(this.cbxBatterySaveMode);
            this.gbSettings.Controls.Add(this.comboBox1);
            this.gbSettings.Controls.Add(this.nudCachePercentage);
            this.gbSettings.Controls.Add(this.lblDitherMethod);
            this.gbSettings.Controls.Add(this.lblMemForCache);
            this.gbSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbSettings.Location = new System.Drawing.Point(0, 0);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(395, 94);
            this.gbSettings.TabIndex = 0;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // cbxEnableVisualEffects
            // 
            this.cbxEnableVisualEffects.AutoSize = true;
            this.cbxEnableVisualEffects.Checked = true;
            this.cbxEnableVisualEffects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxEnableVisualEffects.Location = new System.Drawing.Point(209, 31);
            this.cbxEnableVisualEffects.Name = "cbxEnableVisualEffects";
            this.cbxEnableVisualEffects.Size = new System.Drawing.Size(126, 17);
            this.cbxEnableVisualEffects.TabIndex = 8;
            this.cbxEnableVisualEffects.Text = "Enable Visual Effects";
            this.cbxEnableVisualEffects.UseVisualStyleBackColor = true;
            this.cbxEnableVisualEffects.CheckedChanged += new System.EventHandler(this.cbEnableVisualEffects_CheckedChanged);
            // 
            // cbxBatterySaveMode
            // 
            this.cbxBatterySaveMode.AutoSize = true;
            this.cbxBatterySaveMode.Checked = true;
            this.cbxBatterySaveMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxBatterySaveMode.Location = new System.Drawing.Point(209, 56);
            this.cbxBatterySaveMode.Name = "cbxBatterySaveMode";
            this.cbxBatterySaveMode.Size = new System.Drawing.Size(181, 17);
            this.cbxBatterySaveMode.TabIndex = 7;
            this.cbxBatterySaveMode.Text = "Disable Visual Effects On Battery";
            this.cbxBatterySaveMode.UseVisualStyleBackColor = true;
            this.cbxBatterySaveMode.CheckedChanged += new System.EventHandler(this.cbxBatterySaveMode_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Atkinson",
            "Floyd",
            "Threshold"});
            this.comboBox1.Location = new System.Drawing.Point(103, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "Atkinson";
            // 
            // nudCachePercentage
            // 
            this.nudCachePercentage.Location = new System.Drawing.Point(158, 30);
            this.nudCachePercentage.Name = "nudCachePercentage";
            this.nudCachePercentage.Size = new System.Drawing.Size(45, 20);
            this.nudCachePercentage.TabIndex = 5;
            this.nudCachePercentage.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudCachePercentage.ValueChanged += new System.EventHandler(this.nudCachePercentage_ValueChanged);
            // 
            // lblDitherMethod
            // 
            this.lblDitherMethod.AutoSize = true;
            this.lblDitherMethod.Location = new System.Drawing.Point(8, 57);
            this.lblDitherMethod.Name = "lblDitherMethod";
            this.lblDitherMethod.Size = new System.Drawing.Size(88, 13);
            this.lblDitherMethod.TabIndex = 2;
            this.lblDitherMethod.Text = "Dithering Method";
            // 
            // lblMemForCache
            // 
            this.lblMemForCache.AutoSize = true;
            this.lblMemForCache.Location = new System.Drawing.Point(8, 32);
            this.lblMemForCache.Name = "lblMemForCache";
            this.lblMemForCache.Size = new System.Drawing.Size(150, 13);
            this.lblMemForCache.TabIndex = 1;
            this.lblMemForCache.Text = "Memory % Required for Cache";
            // 
            // tsMain
            // 
            this.tsMain.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslImageCount,
            this.toolStripSeparator1,
            this.tslUndoRedoCount,
            this.toolStripSeparator2,
            this.tslCacheStatus,
            this.toolStripSeparator3,
            this.tslExceptionCount});
            this.tsMain.Location = new System.Drawing.Point(0, 517);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1008, 25);
            this.tsMain.TabIndex = 2;
            this.tsMain.Text = "toolStrip1";
            // 
            // tslImageCount
            // 
            this.tslImageCount.Name = "tslImageCount";
            this.tslImageCount.Size = new System.Drawing.Size(106, 22);
            this.tslImageCount.Text = "No Images Loaded";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslUndoRedoCount
            // 
            this.tslUndoRedoCount.Name = "tslUndoRedoCount";
            this.tslUndoRedoCount.Size = new System.Drawing.Size(196, 22);
            this.tslUndoRedoCount.Text = "No Undos or Redos have been done";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslCacheStatus
            // 
            this.tslCacheStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.tslCacheStatus.Name = "tslCacheStatus";
            this.tslCacheStatus.Size = new System.Drawing.Size(124, 22);
            this.tslCacheStatus.Text = "Disk Cache is Disabled";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tslExceptionCount
            // 
            this.tslExceptionCount.ForeColor = System.Drawing.Color.DarkGreen;
            this.tslExceptionCount.Name = "tslExceptionCount";
            this.tslExceptionCount.Size = new System.Drawing.Size(72, 22);
            this.tslExceptionCount.Text = "0 Exceptions";
            // 
            // pnlListsContainer
            // 
            this.pnlListsContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlListsContainer.Controls.Add(this.btnApplyActions);
            this.pnlListsContainer.Controls.Add(this.tlLoadedImages);
            this.pnlListsContainer.Controls.Add(this.btnAutomationList);
            this.pnlListsContainer.Controls.Add(this.btnHistoryList);
            this.pnlListsContainer.Controls.Add(this.btnImageList);
            this.pnlListsContainer.Controls.Add(this.tlImageHistory);
            this.pnlListsContainer.Controls.Add(this.tlAutomationActions);
            this.pnlListsContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlListsContainer.Location = new System.Drawing.Point(0, 144);
            this.pnlListsContainer.Name = "pnlListsContainer";
            this.pnlListsContainer.Size = new System.Drawing.Size(200, 373);
            this.pnlListsContainer.TabIndex = 3;
            // 
            // btnApplyActions
            // 
            this.btnApplyActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnApplyActions.Location = new System.Drawing.Point(0, 300);
            this.btnApplyActions.Name = "btnApplyActions";
            this.btnApplyActions.Size = new System.Drawing.Size(196, 23);
            this.btnApplyActions.TabIndex = 7;
            this.btnApplyActions.Text = "Apply Actions";
            this.btnApplyActions.UseVisualStyleBackColor = true;
            this.btnApplyActions.Visible = false;
            this.btnApplyActions.Click += new System.EventHandler(this.btnApplyActions_Click);
            // 
            // tlLoadedImages
            // 
            this.tlLoadedImages.AutoScroll = true;
            this.tlLoadedImages.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.tlLoadedImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlLoadedImages.Location = new System.Drawing.Point(0, 23);
            this.tlLoadedImages.Name = "tlLoadedImages";
            this.tlLoadedImages.Size = new System.Drawing.Size(196, 300);
            this.tlLoadedImages.TabIndex = 4;
            this.tlLoadedImages.Thumbnail_Mode = Photoman.UserInterface.UIWidgets.uiThumbnailList.ThumbnailMode.LoadedImages;
            // 
            // btnAutomationList
            // 
            this.btnAutomationList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAutomationList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAutomationList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutomationList.Location = new System.Drawing.Point(0, 323);
            this.btnAutomationList.Name = "btnAutomationList";
            this.btnAutomationList.Size = new System.Drawing.Size(196, 23);
            this.btnAutomationList.TabIndex = 2;
            this.btnAutomationList.Text = "Selected Automation Actions";
            this.btnAutomationList.UseVisualStyleBackColor = false;
            this.btnAutomationList.Click += new System.EventHandler(this.btnAutomationList_Click);
            // 
            // btnHistoryList
            // 
            this.btnHistoryList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnHistoryList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnHistoryList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistoryList.Location = new System.Drawing.Point(0, 346);
            this.btnHistoryList.Name = "btnHistoryList";
            this.btnHistoryList.Size = new System.Drawing.Size(196, 23);
            this.btnHistoryList.TabIndex = 3;
            this.btnHistoryList.Text = "Image History";
            this.btnHistoryList.UseVisualStyleBackColor = false;
            this.btnHistoryList.Click += new System.EventHandler(this.btnHistoryList_Click);
            // 
            // btnImageList
            // 
            this.btnImageList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnImageList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImageList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImageList.Location = new System.Drawing.Point(0, 0);
            this.btnImageList.Name = "btnImageList";
            this.btnImageList.Size = new System.Drawing.Size(196, 23);
            this.btnImageList.TabIndex = 0;
            this.btnImageList.Text = "Loaded Images";
            this.btnImageList.UseVisualStyleBackColor = false;
            this.btnImageList.Click += new System.EventHandler(this.btnImageList_Click);
            // 
            // tlImageHistory
            // 
            this.tlImageHistory.AutoScroll = true;
            this.tlImageHistory.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.tlImageHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlImageHistory.Location = new System.Drawing.Point(0, 0);
            this.tlImageHistory.Name = "tlImageHistory";
            this.tlImageHistory.Size = new System.Drawing.Size(196, 369);
            this.tlImageHistory.TabIndex = 6;
            this.tlImageHistory.Thumbnail_Mode = Photoman.UserInterface.UIWidgets.uiThumbnailList.ThumbnailMode.ImageHistory;
            // 
            // tlAutomationActions
            // 
            this.tlAutomationActions.AutoScroll = true;
            this.tlAutomationActions.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.tlAutomationActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlAutomationActions.Location = new System.Drawing.Point(0, 0);
            this.tlAutomationActions.Name = "tlAutomationActions";
            this.tlAutomationActions.Size = new System.Drawing.Size(196, 369);
            this.tlAutomationActions.TabIndex = 8;
            this.tlAutomationActions.Thumbnail_Mode = Photoman.UserInterface.UIWidgets.uiThumbnailList.ThumbnailMode.ImageHistory;
            // 
            // mpbMain
            // 
            this.mpbMain.AllowAntMarching = true;
            this.mpbMain.AntMarching = true;
            this.mpbMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mpbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpbMain.Location = new System.Drawing.Point(200, 144);
            this.mpbMain.Mouse_Mode = Photoman.UserInterface.UIWidgets.MouseMode.Resize;
            this.mpbMain.Name = "mpbMain";
            this.mpbMain.Size = new System.Drawing.Size(808, 373);
            this.mpbMain.TabIndex = 4;
            this.mpbMain.WorkingImage = null;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1008, 542);
            this.Controls.Add(this.mpbMain);
            this.Controls.Add(this.pnlListsContainer);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.tcRibbon);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "MainGUI";
            this.Text = "Photoman by Elliot Hawkins";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainGUI_FormClosed);
            this.ResizeBegin += new System.EventHandler(this.MainGUI_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainGUI_ResizeEnd);
            this.Move += new System.EventHandler(this.MainGUI_Move);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tcRibbon.ResumeLayout(false);
            this.tabSizeDimensions.ResumeLayout(false);
            this.gbCrop.ResumeLayout(false);
            this.gbFlip.ResumeLayout(false);
            this.gbRotate.ResumeLayout(false);
            this.gbResize.ResumeLayout(false);
            this.gbResize.PerformLayout();
            this.tabColor.ResumeLayout(false);
            this.gbColor.ResumeLayout(false);
            this.gbColor.PerformLayout();
            this.tabSpecial.ResumeLayout(false);
            this.gbText.ResumeLayout(false);
            this.gbText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.gbChromakey.ResumeLayout(false);
            this.gbSpecial.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCachePercentage)).EndInit();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.pnlListsContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveOne;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.TabControl tcRibbon;
        private System.Windows.Forms.TabPage tabSizeDimensions;
        private System.Windows.Forms.TabPage tabColor;
        private System.Windows.Forms.TabPage tabSpecial;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel pnlListsContainer;
        private System.Windows.Forms.Button btnAutomationList;
        private System.Windows.Forms.Button btnHistoryList;
        private System.Windows.Forms.Button btnImageList;
        private System.Windows.Forms.GroupBox gbResize;
        private System.Windows.Forms.Label lblResizeWidth;
        private System.Windows.Forms.GroupBox gbRotate;
        private System.Windows.Forms.Button btnRotateMinus90;
        private System.Windows.Forms.Label lblResizeHeight;
        private System.Windows.Forms.GroupBox gbCrop;
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.GroupBox gbFlip;
        private System.Windows.Forms.Button btnFlipY;
        private System.Windows.Forms.Button btnFlipX;
        private System.Windows.Forms.Button btnRotatePlus90;
        private System.Windows.Forms.GroupBox gbColor;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblSaturation;
        private System.Windows.Forms.Label lblLuminosity;
        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.GroupBox gbText;
        private System.Windows.Forms.GroupBox gbChromakey;
        private System.Windows.Forms.GroupBox gbSpecial;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnSepia;
        private System.Windows.Forms.Button btnDither;
        private System.Windows.Forms.Button btnGrayscale;
        private System.Windows.Forms.Button btnStrikethrough;
        private System.Windows.Forms.Button btnUnderline;
        private System.Windows.Forms.Button btnItalic;
        private System.Windows.Forms.Button btnBold;
        private System.Windows.Forms.Button btnApplyText;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label lblDitherMethod;
        private System.Windows.Forms.Label lblMemForCache;
        private System.Windows.Forms.Button btnApplyActions;
        private System.Windows.Forms.Button btnApplyResize;
        internal Photoman.UserInterface.UIWidgets.uiThumbnailList tlLoadedImages;
        internal System.Windows.Forms.TextBox txtResizeWidth;
        internal Photoman.UserInterface.UIWidgets.uiThumbnailList tlImageHistory;
        internal Photoman.UserInterface.UIWidgets.uiThumbnailList tlAutomationActions;
        internal System.Windows.Forms.CheckBox cbMaintainAspectRatio;
        internal System.Windows.Forms.RadioButton rbResizePercentage;
        internal System.Windows.Forms.RadioButton rbResizePixels;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itHue;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itLuminosity;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itBrightness;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itBlue;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itGreen;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itRed;
        internal Photoman.UserInterface.UIWidgets.uiImageTrackbar itSaturation;
        internal System.Windows.Forms.ComboBox cbFont;
        internal System.Windows.Forms.NumericUpDown nudFontSize;
        internal System.Windows.Forms.ComboBox cbTextColor;
        internal System.Windows.Forms.NumericUpDown nudCachePercentage;
        internal System.Windows.Forms.ComboBox comboBox1;
        public Photoman.UserInterface.UIWidgets.uiMousePictureBox  mpbMain;
        internal Photoman.UserInterface.UIWidgets.uiwPreviewPopup ppSizeDimensions;
        internal Photoman.UserInterface.UIWidgets.uiwPreviewPopup ppColor;
        internal Photoman.UserInterface.UIWidgets.uiwPreviewPopup ppSpecial;
        internal System.Windows.Forms.TextBox txtResizeHeight;
        internal Photoman.UserInterface.UIWidgets.uiChromakey ckMain;
        private System.Windows.Forms.ToolStripMenuItem removeImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideDebugOutputToolStripMenuItem;
        private System.Windows.Forms.Label lblText;
        internal System.Windows.Forms.TextBox txtAddText;
        private System.Windows.Forms.Button btnRedEye;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbxBatterySaveMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripLabel tslCacheStatus;
        public System.Windows.Forms.ToolStripLabel tslImageCount;
        public System.Windows.Forms.ToolStripLabel tslUndoRedoCount;
        private System.Windows.Forms.CheckBox cbxEnableVisualEffects;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripLabel tslExceptionCount;
    }
}