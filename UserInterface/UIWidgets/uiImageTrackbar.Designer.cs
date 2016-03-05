namespace Photoman.UserInterface.UIWidgets
{
    partial class uiImageTrackbar
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
            this.tbMain = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMain
            // 
            this.tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMain.AutoSize = false;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(150, 23);
            this.tbMain.TabIndex = 0;
            this.tbMain.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMain.Scroll += new System.EventHandler(this.tbMain_Scroll);
            this.tbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbMain_MouseDown);
            this.tbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbMain_MouseUp);
            // 
            // uiImageTrackbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbMain);
            this.MinimumSize = new System.Drawing.Size(0, 56);
            this.Name = "uiImageTrackbar";
            this.Size = new System.Drawing.Size(150, 56);
            ((System.ComponentModel.ISupportInitialize)(this.tbMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TrackBar tbMain;


    }
}
