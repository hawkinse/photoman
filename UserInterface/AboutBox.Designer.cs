namespace Photoman.UserInterface
{
    partial class AboutBox
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCredits = new System.Windows.Forms.Label();
            this.llRepoLink = new System.Windows.Forms.LinkLabel();
            this.lblLicenceInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 13);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(119, 13);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Photoman Version Here";
            // 
            // lblCredits
            // 
            this.lblCredits.AutoSize = true;
            this.lblCredits.Location = new System.Drawing.Point(13, 38);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(65, 13);
            this.lblCredits.TabIndex = 1;
            this.lblCredits.Text = "Credits Here";
            // 
            // llRepoLink
            // 
            this.llRepoLink.AutoSize = true;
            this.llRepoLink.Location = new System.Drawing.Point(13, 61);
            this.llRepoLink.Name = "llRepoLink";
            this.llRepoLink.Size = new System.Drawing.Size(207, 13);
            this.llRepoLink.TabIndex = 2;
            this.llRepoLink.TabStop = true;
            this.llRepoLink.Text = "https://bitbucket.org/hawkinse/photoman";
            // 
            // lblLicenceInfo
            // 
            this.lblLicenceInfo.AutoSize = true;
            this.lblLicenceInfo.Location = new System.Drawing.Point(13, 88);
            this.lblLicenceInfo.Name = "lblLicenceInfo";
            this.lblLicenceInfo.Size = new System.Drawing.Size(91, 13);
            this.lblLicenceInfo.TabIndex = 3;
            this.lblLicenceInfo.Text = "License Info Here";
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblLicenceInfo);
            this.Controls.Add(this.llRepoLink);
            this.Controls.Add(this.lblCredits);
            this.Controls.Add(this.lblVersion);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About Photoman";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.LinkLabel llRepoLink;
        private System.Windows.Forms.Label lblLicenceInfo;
    }
}