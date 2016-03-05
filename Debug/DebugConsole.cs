using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoman
{
    public partial class DebugConsole : Form
    {
        public string m_strOutput;
        StringBuilder m_sbOutput;

        public DebugConsole()
        {
            InitializeComponent();
            //Set up for double buffering
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);
            m_sbOutput = new StringBuilder();
        }

        //Add string to output text and render
        public void WriteToConsole(string Text)
        {
            m_sbOutput.Append(System.DateTime.Now.ToString("[h:mm:ss] ") + Text + "\n");
            Invalidate();
            Update();
        }

        //clear console
        public void ClearConsole()
        {
            m_sbOutput = new StringBuilder();
            Invalidate();
            Update();
        }

        private void DebugConsole_Paint(object sender, PaintEventArgs e)
        {
            //Init window graphics
            Graphics gfxWindow = e.Graphics;

            //Fill window with black
            gfxWindow.FillRectangle(Brushes.Black, e.ClipRectangle);

            //Calculate size of text in pixels
            Size sizeBitmapSize = TextRenderer.MeasureText(m_sbOutput.ToString(), new Font("Courier New", 12, FontStyle.Regular, GraphicsUnit.Pixel));
            
            //Create a bitmap the size of the window
            Bitmap bmpRenderedText = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);

            //Init bitmap graphics
            Graphics gfxBitmap = Graphics.FromImage(bmpRenderedText);

            //Fill bitmap with black
            gfxBitmap.FillRectangle(Brushes.Black, 0, 0, sizeBitmapSize.Width, sizeBitmapSize.Height);
            
            //Draw text, setting offset based on total size so that last line is always at the bottom of the window making newest text always visible
            TextRenderer.DrawText(gfxBitmap, m_sbOutput.ToString(), new Font("Courier New", 12, FontStyle.Regular, GraphicsUnit.Pixel), new Point(0, e.ClipRectangle.Height - sizeBitmapSize.Height), Color.Green, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.WordEllipsis | TextFormatFlags.Left);

            //Draw bitmap to screen
            gfxWindow.DrawImage(bmpRenderedText, new Point(0, 0));
        }

        private void DebugConsole_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
            Update();
        }

        private void DebugConsole_Load(object sender, EventArgs e)
        {
            //Supposed to stop from showing up in designer. For some reason not working
            if (DesignMode)
                this.Hide();
        }

        private void DebugConsole_FormClosed(object sender, FormClosedEventArgs e)
        {   
            Application.Exit();
        }        
    }
}
