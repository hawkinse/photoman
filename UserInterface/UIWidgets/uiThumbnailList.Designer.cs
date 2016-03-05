namespace Photoman.UserInterface.UIWidgets
{
    partial class uiThumbnailList
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.vsbMain = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.AutoSize = true;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(180, 297);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uc_MouseDown);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uc_MouseUp);
            // 
            // vsbMain
            // 
            this.vsbMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsbMain.LargeChange = 100;
            this.vsbMain.Location = new System.Drawing.Point(183, 0);
            this.vsbMain.Name = "vsbMain";
            this.vsbMain.Size = new System.Drawing.Size(17, 300);
            this.vsbMain.SmallChange = 45;
            this.vsbMain.TabIndex = 0;
            this.vsbMain.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbMain_Scroll);
            // 
            // uiThumbnailList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.vsbMain);
            this.Controls.Add(this.pnlMain);
            this.DoubleBuffered = true;
            this.Name = "uiThumbnailList";
            this.Size = new System.Drawing.Size(200, 300);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.uiThumbnailList_Scroll);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uiThumbnailList_KeyDown);
            this.SizeChanged += new System.EventHandler(this.uiThumbnailList_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uiThumbnailList_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.VScrollBar vsbMain;
    }
}
