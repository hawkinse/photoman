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
    public delegate void OnTrackbarScrolled();
    public delegate void OnTrackbarDragged();

    public partial class uiImageTrackbar : UserControl
    {
        public OnTrackbarScrolled TrackbarScrolled;
        public OnTrackbarDragged TrackbarDragged;

        private bool IsMouseDown;

        //Bitmap of colors for trackbar. Only one pixel high, should be stretched to appropriate height.
        private Bitmap m_bmpTrackbarImage;
                
        public enum TrackbarColorType
        {
            None,
            Red,
            Green,
            Blue,
            Hue,
            Luminosity,
            Saturation,
            Brightness
        }

        /// <summary>
        /// Constructor for uiImageTrackbar
        /// </summary>
        /// <param name="tct">The type of color bar to generate.</param>
        /// <param name="ImageForColorScape">The image to use if the color bar is Hue. Ignored otherwise, so safe to pass in null</param>
        public uiImageTrackbar()
        {
            InitializeComponent();
            //Default trackbar setup
            Setup(TrackbarColorType.None, null);

        }

        /// <summary>
        /// Reinitializes the image portion of the trackbar and sets to center. For Hue and Saturation, an Image is required.
        /// </summary>
        /// <param name="tct">The trackbar Color Type to use for the image bar</param>
        public void ReInit(TrackbarColorType tct)
        {
            Setup(tct, null);
            this.Invalidate();
        }

        /// <summary>
        /// Reinitializes the image portion of the trackbar and sets to center. For Hue and Saturation, an Image is required
        /// </summary>
        /// <param name="tct">The trackbar Color Type to use for the image bar</param>
        /// <param name="ImageForColorScape">In the case of Saturation and Hue, this is the image to generate off of</param>
        public void ReInit(TrackbarColorType tct, Bitmap ImageForColorScape)
        {
            Setup(tct, ImageForColorScape);
            this.Invalidate();
        }

        private void Setup(TrackbarColorType tct, Bitmap ImageForColorScape)
        {
            //Create color bar for hue. Easily the most complex color bar
            if (tct == TrackbarColorType.Hue)
            {
                tbMain.Maximum = 360;
                if (ImageForColorScape != null)
                {
                    //Shrink image and determine dominant color
                    Bitmap bmpTemp = new Bitmap(ImageForColorScape, Constants.iPreviewSize, Constants.iPreviewSize);

                    //Create a Color/Int dictionary where the int is the number of times the color is used.
                    Dictionary<Color, int> dicColorCount = new Dictionary<Color, int>();

                    Color cWorkingColor;

                    Core.HLS newHLS = new Photoman.Core.HLS();
                    
                    //Populate dictionary
                    for (int iX = 0; iX < bmpTemp.Width; iX++)
                    {
                        for (int iY = 0; iY < bmpTemp.Height; iY++)
                        {
                            cWorkingColor = bmpTemp.GetPixel(iX, iY);

                            float fLuminosity = cWorkingColor.GetBrightness();

                            newHLS.Color = cWorkingColor;
                            newHLS.Luminance = 0.5f;
                            newHLS.Saturation = 1f;

                            cWorkingColor = newHLS.Color;

                            if (dicColorCount.ContainsKey(cWorkingColor))
                                dicColorCount[cWorkingColor]++;
                            else
                            {
                                dicColorCount.Add(cWorkingColor, 0);
                            }
                        }
                    }

                    //Get most used color as base
                    KeyValuePair<Color, Int32> kvpMax = new KeyValuePair<Color, int>(Color.Transparent, 0);

                    foreach (KeyValuePair<Color, Int32> kvp in dicColorCount)
                    {
                        if (kvpMax.Value < kvp.Value)
                            kvpMax = kvp;
                    }

                    //Set trackbar length to 360 since hue is a 360 wheel
                    m_bmpTrackbarImage = new Bitmap(360, 1);


                    //Create 1 pixel bitmap to store most common color
                    Bitmap bmpHue = new Bitmap(1, 1);
                    bmpHue.SetPixel(0, 0, kvpMax.Key);                    
                    
                    //Increment hue by 180 so we can go through all 360 at once
                    bmpHue = Core.HLS.ChangeHue(bmpHue, 180);

                    for (int i = 0; i < 360; i++)
                    {
                        m_bmpTrackbarImage.SetPixel(i, 0, bmpHue.GetPixel(0, 0));
                        bmpHue = Core.HLS.ChangeHue(bmpHue, 1);
                    }

                    ////Fill in hue value center to right
                    //for (int i = 180; i < 360; i++) 
                    //{
                    //    m_bmpTrackbarImage.SetPixel(i, 0, bmpHue.GetPixel(0, 0));
                    //    bmpHue = Core.HLS.ChangeHue(bmpHue, 1);                        
                    //}

                    ////Fill in hue values left to center
                    //for (int i = 0; i < 180; i++)
                    //{
                    //    m_bmpTrackbarImage.SetPixel(i, 0, bmpHue.GetPixel(0, 0));
                    //    bmpHue = Core.HLS.ChangeHue(bmpHue, 1);                        
                    //}                    
                }
            }
            else if (tct == TrackbarColorType.Saturation)
            {
                tbMain.Maximum = 20;
                if (ImageForColorScape != null)
                {
                    //Shrink image and determine dominant color
                    Bitmap bmpTemp = new Bitmap(ImageForColorScape, Constants.iPreviewSize, Constants.iPreviewSize);

                    //Create a Color/Int dictionary where the int is the number of times the color is used.
                    Dictionary<Color, int> dicColorCount = new Dictionary<Color, int>();

                    Color cWorkingColor;


                    //Populate dictionary
                    for (int iX = 0; iX < bmpTemp.Width; iX++)
                    {
                        for (int iY = 0; iY < bmpTemp.Height; iY++)
                        {
                            cWorkingColor = bmpTemp.GetPixel(iX, iY);

                            if (dicColorCount.ContainsKey(cWorkingColor))
                                dicColorCount[cWorkingColor]++;
                            else
                            {
                                float fLuminosity = cWorkingColor.GetBrightness();
                                //Check if luminosity isnt too high or low
                                if (fLuminosity > 0.4 && fLuminosity < 0.6)
                                    dicColorCount.Add(cWorkingColor, 0);
                            }
                        }
                    }

                    //Get most used color as base
                    KeyValuePair<Color, Int32> kvpMax = new KeyValuePair<Color, int>(Color.Transparent, 0);

                    foreach (KeyValuePair<Color, Int32> kvp in dicColorCount)
                    {
                        if (kvpMax.Value < kvp.Value)
                            kvpMax = kvp;
                    }

                    //Set trackbar length to 20 since we start at 0.0 and can go from 0 to 1, need to go both ways
                    m_bmpTrackbarImage = new Bitmap(20, 1);


                    //Create 1 pixel bitmap to store most common color
                    Bitmap bmpSatHigher = new Bitmap(1, 1);
                    bmpSatHigher.SetPixel(0, 0, kvpMax.Key);
                    Bitmap bmpSatLower = (Bitmap)bmpSatHigher.Clone();

                    //Fill in hue value center to right
                    for (int i = 10; i < 20; i++)
                    {
                        bmpSatHigher = Core.HLS.ChangeSaturation(bmpSatHigher, 0.1f);
                        m_bmpTrackbarImage.SetPixel(i, 0, bmpSatHigher.GetPixel(0, 0));
                    }
                    
                    //Fill in hue values center to left
                    for (int i = 9; i > -1; i--)
                    {
                        bmpSatLower = Core.HLS.ChangeSaturation(bmpSatLower, -0.1f);
                        m_bmpTrackbarImage.SetPixel(i, 0, bmpSatLower.GetPixel(0, 0));
                    }
                }
            }
            else if (tct == TrackbarColorType.Luminosity)
            {
                tbMain.Maximum = 240;
                //Create bitmap band
                m_bmpTrackbarImage = new Bitmap(240, 1);

                //Create red color band
                for (int i = 0; i < 240; i++)
                    m_bmpTrackbarImage.SetPixel(i, 0, Color.FromArgb(i, i, i));
            }
            else if (tct == TrackbarColorType.Brightness)
            {
                tbMain.Maximum = 10;
                //Create bitmap band
                m_bmpTrackbarImage = new Bitmap(256, 1);

                //Create red color band
                for (int i = 0; i < 256; i++)
                    m_bmpTrackbarImage.SetPixel(i, 0, Color.FromArgb(i, i, i));
            }
            else if (tct == TrackbarColorType.Red)
            {
                tbMain.Maximum = 10;
                //Create bitmap band
                m_bmpTrackbarImage = new Bitmap(256, 1);

                //Create red color band
                for(int i = 0; i < 256; i++)
                    m_bmpTrackbarImage.SetPixel(i, 0, Color.FromArgb(i,0,0));
            }
            else if (tct == TrackbarColorType.Green)
            {
                tbMain.Maximum = 10;
                //Create bitmap band
                m_bmpTrackbarImage = new Bitmap(256, 1);

                //Create red color band
                for (int i = 0; i < 256; i++)
                    m_bmpTrackbarImage.SetPixel(i, 0, Color.FromArgb(0, i, 0));
            }
            else if (tct == TrackbarColorType.Blue)
            {
                tbMain.Maximum = 10;
                //Create bitmap band
                m_bmpTrackbarImage = new Bitmap(256, 1);

                //Create red color band
                for (int i = 0; i < 256; i++)
                    m_bmpTrackbarImage.SetPixel(i, 0, Color.FromArgb(0, 0, i));
            }
            else if (tct == TrackbarColorType.None)
            {
                //Fill bitmap band with black to test rendering
                m_bmpTrackbarImage = new Bitmap(tbMain.Maximum, 1);

                for (int i = 0; i < tbMain.Maximum; i++)
                {
                    m_bmpTrackbarImage.SetPixel(i, 0, Color.Black);
                }
            }     
       
            //Set slider to center
            tbMain.Value = tbMain.Maximum / 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {  
            //Perform base painting
            base.OnPaint(e);
            //Init graphics
            Graphics gfx = e.Graphics;
            //Draw image. Currently blurs due to stretching but works.
            gfx.DrawImage(m_bmpTrackbarImage, new Rectangle(tbMain.ClientRectangle.Left + Constants.iSliderOffset, tbMain.ClientRectangle.Bottom + 1, tbMain.Width - Constants.iSliderOffset * 2, Constants.iSliderImageHeight));
        }

        private void tbMain_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            this.OnMouseDown(e);
        }

        private void tbMain_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            this.OnMouseUp(e);
        }

        private void tbMain_Scroll(object sender, EventArgs e)
        {
            if (!IsMouseDown)
            {
                if (TrackbarScrolled != null)
                    TrackbarScrolled();
            }
            else
            {
                if (TrackbarDragged != null)
                    TrackbarDragged();
            }
                
        }
        
    }
}
