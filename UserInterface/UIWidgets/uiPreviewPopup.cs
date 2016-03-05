using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * How to use:
 * 
 * Create an instance of this class in the GUI
 * 
 * On mouse over events for previews send this control the new image, operation and args. Will auto-show when image changed
 * 
 * On mouse off call the Hide function
 */

namespace Photoman.UserInterface.UIWidgets
{
    public partial class uiwPreviewPopup : UserControl
    {
        bool m_MouseoverPopup = false;
        public bool MouseoverPopup
        {
            get { return m_MouseoverPopup; }
            set { m_MouseoverPopup = value; }
        }

        /// <summary>
        /// The image to output to the preview.
        /// </summary>
        private Bitmap m_bmpOutput
        {
            get { return (Bitmap)pbOutput.Image; }
            set { pbOutput.Image = value; }
        }
        
        public uiwPreviewPopup()
        {            
            InitializeComponent();
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            //Move panel as mouse moves so that we're always under the mouse
            //Unsure if the mouse will follow if mouse is not over it?
            //this.Location = e.Location;
        }

        private void pnlMain_Load(object sender, EventArgs e)
        {
            pbOutput.BackColor = this.BackColor;

            //Set size
            this.Width = Constants.iPreviewSize;
            this.Height = Constants.iPreviewSize + 13; //13 is to accomodate for "Preview" text

            if (m_bmpOutput == null && m_MouseoverPopup)
                this.Hide();
            if(m_MouseoverPopup)
                this.Location = MousePosition;

            this.Show();

        }

        /// <summary>
        /// Resizes the input image to a size fast to parse through, applys the operation and shows as a preview. Will show to the user upon preview generation
        /// </summary>
        /// <param name="Input">The source image</param>
        /// <param name="Operation">The operation to perform. Resize and File IO operations not supported</param>
        /// <param name="Arguments"></param>
        internal void SetPreviewImage(Bitmap Input, Constants.Operations Operation, List<object> Arguments)
        {
            try
            {               
                //Sanity check
                if (Input == null)
                    return;

                Global.WriteToLog("Generating preview...", true);

                //For now stretch image. We can worry about scaling later
                //m_bmpOutput = new Bitmap(Input, Constants.iPreviewSize, Constants.iPreviewSize);//Core.BasicOps.MaintainAspectRatio(new Size(Constants.iPreviewSize, Constants.iPreviewSize), Input.Size));
                m_bmpOutput = new Bitmap(Input, Core.BasicOps.MaintainAspectRatio_DontExcedeNewSize(new Size(Constants.iPreviewSize, Constants.iPreviewSize), Input.Size));
                
                //Switch to apply operations on the input image. Since we dont want actions to apply to the loaded images,
                //this bypasses the controller and talks directly to the core.
                switch (Operation)
                {
                    case (Constants.Operations.AddText):
                        m_bmpOutput = Core.OtherOps.AddCaption(m_bmpOutput, (Rectangle)(Arguments)[0], (string)(Arguments)[1],
                                             (Font)(Arguments)[2], (Brush)(Arguments)[3]);
                        break;
                    case (Constants.Operations.Brightness):
                        m_bmpOutput = Core.RGB.Brightness(m_bmpOutput, (float)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Crop):
                        m_bmpOutput = Core.BasicOps.Crop(m_bmpOutput, (Rectangle)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Dither):
                        m_bmpOutput = Core.RGB.Dither_Atkinson(m_bmpOutput);
                        break;
                    case (Constants.Operations.Flip):
                        m_bmpOutput = Core.BasicOps.Flip(m_bmpOutput, (Core.BasicOps.FlipAxis)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Gamma):
                        m_bmpOutput = Core.RGB.Gamma(m_bmpOutput, (float)(Arguments)[0], (float)(Arguments)[1], (float)(Arguments)[2]);
                        break;
                    case (Constants.Operations.Grayscale):
                        m_bmpOutput = Core.RGB.Grayscale(m_bmpOutput);
                        break;
                    case (Constants.Operations.Hue):
                        m_bmpOutput = Core.HLS.ChangeHue(m_bmpOutput, (float)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Invert):
                        m_bmpOutput = Core.RGB.Invert(m_bmpOutput);
                        break;
                    case (Constants.Operations.Luminosity):
                        m_bmpOutput = Core.HLS.ChangeLuminosity(m_bmpOutput, (float)(Arguments)[0]);
                        break;
                    case (Constants.Operations.RemoveRedEye):
                        m_bmpOutput = Core.OtherOps.RedEyeRemoval(m_bmpOutput, (Rectangle)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Rotate):
                        m_bmpOutput = Core.BasicOps.Rotate(m_bmpOutput, (Core.BasicOps.RotateDirection)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Saturation):
                        m_bmpOutput = Core.HLS.ChangeSaturation(m_bmpOutput, (float)(Arguments)[0]);
                        break;
                    case (Constants.Operations.Sepia):
                        m_bmpOutput = Core.RGB.Sepia(m_bmpOutput);
                        break;
                    case Constants.Operations.ChromaKey:
                        m_bmpOutput = Core.OtherOps.ChromaKey(m_bmpOutput, (Bitmap)(Arguments)[1], (float)(Arguments)[2]);
                        break;
                    default:
                        throw new Exception("Unsupported preview task attempted");
                }

                if (m_MouseoverPopup)
                {
                    //Show popup
                    this.Show();

                    MoveToMouse();

                    //Set as frontmost control
                    this.BringToFront();
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                Global.WriteToLog("Not showing preview because of exception", true, false);

                m_bmpOutput = null;

                if(m_MouseoverPopup)
                    this.Hide();
            }
        }

        internal void ClearPreview()
        {
            m_bmpOutput = null;
        }

        internal void MoveToMouse()
        {
            if(m_MouseoverPopup)
                this.Location = this.FindForm().PointToClient(new Point(MousePosition.X + Constants.iPreviewMouseOffest, MousePosition.Y + Constants.iPreviewMouseOffest));
        }
    }
}
