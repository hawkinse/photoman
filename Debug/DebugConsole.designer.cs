namespace Photoman
{
    partial class DebugConsole
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
            this.SuspendLayout();
            // 
            // DebugConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Name = "DebugConsole";
            this.Text = "DebugConsole";
            this.Load += new System.EventHandler(this.DebugConsole_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DebugConsole_Paint);
            this.SizeChanged += new System.EventHandler(this.DebugConsole_SizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DebugConsole_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion
    }
}