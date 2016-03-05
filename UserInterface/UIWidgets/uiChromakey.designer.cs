namespace Photoman.UserInterface.UIWidgets
{
    partial class uiChromakey
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
            this.tbSensitivity = new System.Windows.Forms.TrackBar();
            this.pnlBackgroundImg = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSensitivity)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.tbSensitivity);
            this.pnlMain.Controls.Add(this.pnlBackgroundImg);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(150, 64);
            this.pnlMain.TabIndex = 0;
            // 
            // tbSensitivity
            // 
            this.tbSensitivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSensitivity.LargeChange = 2;
            this.tbSensitivity.Location = new System.Drawing.Point(60, 12);
            this.tbSensitivity.Maximum = 2;
            this.tbSensitivity.Name = "tbSensitivity";
            this.tbSensitivity.Size = new System.Drawing.Size(84, 45);
            this.tbSensitivity.TabIndex = 1;
            this.tbSensitivity.Value = 1;
            this.tbSensitivity.ValueChanged += new System.EventHandler(this.tbSensitivity_ValueChanged);
            // 
            // pnlBackgroundImg
            // 
            this.pnlBackgroundImg.AllowDrop = true;
            this.pnlBackgroundImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBackgroundImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackgroundImg.Location = new System.Drawing.Point(8, 8);
            this.pnlBackgroundImg.Name = "pnlBackgroundImg";
            this.pnlBackgroundImg.Size = new System.Drawing.Size(46, 46);
            this.pnlBackgroundImg.TabIndex = 0;
            this.pnlBackgroundImg.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBackgroundImg_Paint);
            this.pnlBackgroundImg.Click += new System.EventHandler(this.pnlBackgroundImg_Click);
            this.pnlBackgroundImg.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlBackgroundImg_DragDrop);
            this.pnlBackgroundImg.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlBackgroundImg_DragEnter);
            // 
            // uiChromakey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.DoubleBuffered = true;
            this.Name = "uiChromakey";
            this.Size = new System.Drawing.Size(150, 64);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSensitivity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBackgroundImg;
        private System.Windows.Forms.TrackBar tbSensitivity;
    }
}
