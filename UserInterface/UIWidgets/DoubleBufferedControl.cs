using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Photoman.UserInterface.UIWidgets
{
    public partial class DoubleBufferedControl : UserControl
    {
        public DoubleBufferedControl()
        {
            InitializeComponent();

            //Set up for double buffering
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, false);
            SetStyle(ControlStyles.UserPaint, true);
        }

        public void ManualMouseDown(MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        public void ManualMouseUp(MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
    }
}
