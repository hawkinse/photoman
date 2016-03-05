namespace Photoman.UserInterface.UIWidgets
{
    partial class uiMousePictureBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uiMousePictureBox));
            this.tsTop = new System.Windows.Forms.ToolStrip();
            this.tlMouseMode = new System.Windows.Forms.ToolStripLabel();
            this.tcMouseMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlZoom = new System.Windows.Forms.ToolStripLabel();
            this.tbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tlZoomLevel = new System.Windows.Forms.ToolStripLabel();
            this.tbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlAspectRatio = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsNewDimensions = new System.Windows.Forms.ToolStripLabel();
            this.tmrAntMarchTimer = new System.Windows.Forms.Timer(this.components);
            this.tmrAutoZoomTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlMain = new Photoman.UserInterface.UIWidgets.DoubleBufferedControl();
            this.tsTop.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsTop
            // 
            this.tsTop.BackColor = System.Drawing.SystemColors.Control;
            this.tsTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlMouseMode,
            this.tcMouseMode,
            this.toolStripSeparator1,
            this.tlZoom,
            this.tbZoomOut,
            this.tlZoomLevel,
            this.tbZoomIn});
            this.tsTop.Location = new System.Drawing.Point(0, 0);
            this.tsTop.Name = "tsTop";
            this.tsTop.Size = new System.Drawing.Size(316, 25);
            this.tsTop.TabIndex = 0;
            this.tsTop.Text = "toolStrip1";
            // 
            // tlMouseMode
            // 
            this.tlMouseMode.Name = "tlMouseMode";
            this.tlMouseMode.Size = new System.Drawing.Size(80, 22);
            this.tlMouseMode.Text = "Mouse Mode:";
            // 
            // tcMouseMode
            // 
            this.tcMouseMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcMouseMode.Items.AddRange(new object[] {
            "Resize",
            "Crop",
            "Red Eye Removal",
            "Add Text"});
            this.tcMouseMode.Name = "tcMouseMode";
            this.tcMouseMode.Size = new System.Drawing.Size(115, 25);
            this.tcMouseMode.SelectedIndexChanged += new System.EventHandler(this.tcMouseMode_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlZoom
            // 
            this.tlZoom.Name = "tlZoom";
            this.tlZoom.Size = new System.Drawing.Size(39, 22);
            this.tlZoom.Text = "Zoom";
            // 
            // tbZoomOut
            // 
            this.tbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tbZoomOut.Image")));
            this.tbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbZoomOut.Name = "tbZoomOut";
            this.tbZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tbZoomOut.Text = "-";
            this.tbZoomOut.Click += new System.EventHandler(this.tbZoomOut_Click);
            // 
            // tlZoomLevel
            // 
            this.tlZoomLevel.Name = "tlZoomLevel";
            this.tlZoomLevel.Size = new System.Drawing.Size(18, 22);
            this.tlZoomLevel.Text = "/1";
            // 
            // tbZoomIn
            // 
            this.tbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tbZoomIn.Image")));
            this.tbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbZoomIn.Name = "tbZoomIn";
            this.tbZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tbZoomIn.Text = "+";
            this.tbZoomIn.Click += new System.EventHandler(this.tbZoomIn_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlSize,
            this.toolStripSeparator2,
            this.tlAspectRatio,
            this.toolStripSeparator3,
            this.tsNewDimensions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 211);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(316, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlSize
            // 
            this.tlSize.Name = "tlSize";
            this.tlSize.Size = new System.Drawing.Size(133, 22);
            this.tlSize.Text = "Image Dimensions Here";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tlAspectRatio
            // 
            this.tlAspectRatio.Name = "tlAspectRatio";
            this.tlAspectRatio.Size = new System.Drawing.Size(137, 22);
            this.tlAspectRatio.Text = "Image Aspect Ratio Here";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsNewDimensions
            // 
            this.tsNewDimensions.Name = "tsNewDimensions";
            this.tsNewDimensions.Size = new System.Drawing.Size(122, 15);
            this.tsNewDimensions.Text = "New Dimensions here";
            // 
            // tmrAntMarchTimer
            // 
            this.tmrAntMarchTimer.Enabled = true;
            this.tmrAntMarchTimer.Interval = 200;
            this.tmrAntMarchTimer.Tick += new System.EventHandler(this.tmrAntMarchTimer_Tick);
            // 
            // tmrAutoZoomTimer
            // 
            this.tmrAutoZoomTimer.Interval = 1000;
            this.tmrAutoZoomTimer.Tick += new System.EventHandler(this.tmrAutoZoomTimer_Tick);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(316, 186);
            this.pnlMain.TabIndex = 2;
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // uiMousePictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tsTop);
            this.DoubleBuffered = true;
            this.Name = "uiMousePictureBox";
            this.Size = new System.Drawing.Size(316, 236);
            this.SizeChanged += new System.EventHandler(this.uiMousePictureBox_SizeChanged);
            this.tsTop.ResumeLayout(false);
            this.tsTop.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTop;
        private System.Windows.Forms.ToolStripLabel tlMouseMode;
        private System.Windows.Forms.ToolStripComboBox tcMouseMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tlZoom;
        private System.Windows.Forms.ToolStripButton tbZoomOut;
        private System.Windows.Forms.ToolStripLabel tlZoomLevel;
        private System.Windows.Forms.ToolStripButton tbZoomIn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tlSize;
        private System.Windows.Forms.ToolStripLabel tlAspectRatio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmrAntMarchTimer;
        private DoubleBufferedControl pnlMain;
        private System.Windows.Forms.Timer tmrAutoZoomTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tsNewDimensions;
    }
}
