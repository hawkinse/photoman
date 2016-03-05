using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Photoman.Core;
using Photoman.UserInterface.UIData;

namespace Photoman.UserInterface.UIWidgets
{
    enum ChromakeySensitivity
    {
        Low,
        Medium_Default,
        High
    }

    public delegate void ApplyClickedHandler();
    public delegate void ValidImageDataDraggedHandler(object sender, DragEventArgs e);

    public partial class uiChromakey : UserControl
    {
        public ApplyClickedHandler ApplyClicked;
        public ValidImageDataDraggedHandler ValidImageDataDragged;

        private Bitmap m_bmpBackgroundImage;
        private ChromakeySensitivity m_cksSensitivity;
        private int m_iSelectedIndex = 0;

        public int SelectedIndex
        {
            get { return m_iSelectedIndex; }
        }

        //Hides base BackgroundImage, which returns Image. 
        //I apparently didn't think this particular part out well, not sure best approach to fix warning without breaking other parts of codebase
        public Bitmap BackgroundImage
        {
            get { return m_bmpBackgroundImage; }
            private set 
            { 
                m_bmpBackgroundImage = value;
                pnlBackgroundImg.Invalidate();
            }
        }

        public float Sensitivity
        {
            get
            {
                float fReturn = 1.5f;

                switch (m_cksSensitivity)
                {
                    case ChromakeySensitivity.Low:
                        fReturn = 2;
                        break;
                    case ChromakeySensitivity.Medium_Default:
                        fReturn = 1.5f;
                        break;
                    case ChromakeySensitivity.High:
                        fReturn = 1;
                        break;
                    default:
                        fReturn = 1.5f;
                        break;
                }

                return fReturn;
            }

        }

        public uiChromakey()
        {
            InitializeComponent();
        }

        private void pnlBackgroundImg_Paint(object sender, PaintEventArgs e)
        {
            if (m_bmpBackgroundImage != null)
            {
                //Create a temporary bitmap
                Bitmap bmpTemp = new Bitmap(pnlBackgroundImg.Width, pnlBackgroundImg.Height);
                Graphics gfx = Graphics.FromImage(bmpTemp);
                gfx.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, bmpTemp.Width, bmpTemp.Height));

                //Get scaled size for thumbnail
                Bitmap bmpResized = new Bitmap(m_bmpBackgroundImage, BasicOps.MaintainAspectRatio_DontExcedeNewSize(bmpTemp.Size, m_bmpBackgroundImage.Size));

                //Get Offest for thumbnail
                Point pntOffset = new Point((pnlBackgroundImg.Size.Width - bmpTemp.Size.Width) / 2, (pnlBackgroundImg.Size.Height - bmpTemp.Size.Height) / 2);

                //Render
                e.Graphics.DrawImage(bmpResized, pntOffset);
            }
            else
            {
                Graphics gfx = e.Graphics;
                Font fnt = new Font("Arial", 8);
                gfx.DrawString("Drag\nbackground\nhere", fnt, Brushes.Black, 0, 0);
            }
        }

        public void DEBUG_ChangeBackgroundImageExternally(Bitmap image)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                BackgroundImage = image;
            else
                throw new Exception("No debugger attached. Background image will not change");
        }

        private void tbSensitivity_ValueChanged(object sender, EventArgs e)
        {
            switch (tbSensitivity.Value)
            {
                case 0:
                    m_cksSensitivity = ChromakeySensitivity.Low;
                    break;
                case 1:
                    m_cksSensitivity = ChromakeySensitivity.Medium_Default;
                    break;
                case 2:
                    m_cksSensitivity = ChromakeySensitivity.High;
                    break;
                default:
                    m_cksSensitivity = ChromakeySensitivity.Medium_Default;
                    break;
            }
        }


        private void pnlBackgroundImg_DragDrop(object sender, DragEventArgs e)
        {
            //Needs reworking to match form1 drag start
            if (e.Data.GetDataPresent(typeof(udImage_DragDrop)))
            {
                try
                {
                    udImage_DragDrop iddData = (udImage_DragDrop)e.Data.GetData(typeof(udImage_DragDrop));

                    if (iddData != null && iddData.Indentifier == udImage_DragDrop.IdentifierCheck)
                    {
                        m_bmpBackgroundImage = iddData.BitmapData;
                        m_iSelectedIndex = iddData.Index;
                        pnlBackgroundImg.Invalidate();
                        if (ApplyClicked != null)
                            ApplyClicked();
                    }
                      
                }
                catch (Exception ex)
                {
                    if (ex.Message != "Identifier is not valid!")
                        throw ex;
                }
            }
        }

        private void pnlBackgroundImg_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                udImage_DragDrop iddData = (udImage_DragDrop)e.Data.GetData(typeof(udImage_DragDrop));

                if (iddData != null && iddData.Indentifier == udImage_DragDrop.IdentifierCheck)
                {
                    e.Effect = DragDropEffects.Copy;
                    if (ValidImageDataDragged != null)
                        ValidImageDataDragged(sender, e);
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void pnlBackgroundImg_Click(object sender, EventArgs e)
        {
            if (ApplyClicked != null)
                ApplyClicked();
        }

    }
}
