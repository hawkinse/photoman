using System;
using System.Drawing;

namespace Photoman.Core
{
    /// <summary>
    /// Handles all the operations dealing with HLS color
    /// </summary>
    class HLS
    {
        private byte m_byRed = 0;
        private byte m_byGreen = 0;
        private byte m_byBlue = 0;

        private float m_fHue = 0;
        private float luminance = 0;
        private float saturation = 0;

        public byte Red
        {
            get { return m_byRed; }
            set
            {
                m_byRed = value;
                ToHLS();
            }
        }
        public byte Green
        {
            get { return m_byGreen; }
            set
            {
                m_byGreen = value;
                ToHLS();
            }
        }
        public byte Blue
        {
            get { return m_byBlue; }
            set
            {
                m_byBlue = value;
                ToHLS();
            }
        }
        public float Luminance
        {
            get { return luminance; }
            set
            {
                if ((luminance < 0.0f) || (luminance > 1.0f))
                {
                    throw new ArgumentOutOfRangeException("Luminance", "Luminance must be between 0.0 and 1.0");
                }
                luminance = value;
                ToRGB();
            }
        }

        public float Hue
        {
            get { return m_fHue; }
            set
            {
                if ((m_fHue < 0.0f) || (m_fHue > 360.0f))
                {
                    throw new ArgumentOutOfRangeException("Hue", "Hue must be between 0.0 and 360.0");
                }
                m_fHue = value;
                ToRGB();
            }
        }

        public float Saturation
        {
            get { return saturation; }
            set
            {
                if ((saturation < 0.0f) || (saturation > 1.0f))
                {
                    throw new ArgumentOutOfRangeException("Saturation", "Saturation must be between 0.0 and 1.0");
                }
                saturation = value;
                ToRGB();
            }
        }

        public Color Color
        {
            get
            {
                Color cColor = Color.FromArgb(m_byRed, m_byGreen, m_byBlue);
                return (cColor);
            }
            set
            {
                m_byRed = value.R;
                m_byGreen = value.G;
                m_byBlue = value.B;
                ToHLS();
            }
        }

        public void LightenColor(float lightenBy)
        {
            luminance *= (1.0f + lightenBy);
            if (luminance > 1.0f)
                luminance = 1.0f;

            ToRGB();
        }

        public void DarkenColor(float darkenBy)
        {
            luminance *= darkenBy;
            ToRGB();
        }


        public void HLSRGB(Color cColor)
        {
            m_byRed = cColor.R;
            m_byGreen = cColor.G;
            m_byBlue = cColor.B;
            ToHLS();
        }

        public void HLSRGB(float fHue, float fLuminance, float fSaturation)
        {
            this.m_fHue = fHue;
            this.luminance = fLuminance;
            this.saturation = fSaturation;
            ToRGB();
        }

        public void HLSRGB(byte byRed, byte byGreen, byte byBlue)
        {
            this.m_byRed = byRed;
            this.m_byGreen = byGreen;
            this.m_byBlue = byBlue;
        }


        private void ToHLS()
        {
            byte minval = Math.Min(m_byRed, Math.Min(m_byGreen, m_byBlue));
            byte maxval = Math.Max(m_byRed, Math.Max(m_byGreen, m_byBlue));

            float mdiff = (float)(maxval - minval);
            float msum = (float)(maxval + minval);

            luminance = msum / 510.0f;

            if (maxval == minval)
            {
                saturation = 0.0f;
                m_fHue = 0.0f;
            }
            else
            {
                float rnorm = (maxval - m_byRed) / mdiff;
                float gnorm = (maxval - m_byGreen) / mdiff;
                float bnorm = (maxval - m_byBlue) / mdiff;

                saturation = (luminance <= 0.5f) ? (mdiff / msum) : (mdiff / (510.0f - msum));

                if (m_byRed == maxval)
                {
                    m_fHue = 60.0f * (6.0f + bnorm - gnorm);
                }

                if (m_byGreen == maxval)
                {
                    m_fHue = 60.0f * (2.0f + rnorm - bnorm);
                }

                if (m_byBlue == maxval)
                {
                    m_fHue = 60.0f * (4.0f + gnorm - rnorm);
                }

                if (m_fHue > 360.0f)
                {
                    m_fHue = m_fHue - 360.0f;
                }
            }
        }

        private void ToRGB()
        {
            if (saturation == 0.0)
            {
                m_byRed = (byte)(luminance * 255.0F);
                m_byGreen = m_byRed;
                m_byBlue = m_byRed;
            }
            else
            {
                float rm1;
                float rm2;

                if (luminance <= 0.5f)
                {
                    rm2 = luminance + luminance * saturation;
                }
                else
                {
                    rm2 = luminance + saturation - luminance * saturation;
                }
                rm1 = 2.0f * luminance - rm2;
                m_byRed = ToRGB1(rm1, rm2, m_fHue + 120.0f);
                m_byGreen = ToRGB1(rm1, rm2, m_fHue);
                m_byBlue = ToRGB1(rm1, rm2, m_fHue - 120.0f);
            }
        }

        private byte ToRGB1(float rm1, float rm2, float rh)
        {
            if (rh > 360.0f)
            {
                rh -= 360.0f;
            }
            else if (rh < 0.0f)
            {
                rh += 360.0f;
            }

            if (rh < 60.0f)
            {
                rm1 = rm1 + (rm2 - rm1) * rh / 60.0f;
            }
            else if (rh < 180.0f)
            {
                rm1 = rm2;
            }
            else if (rh < 240.0f)
            {
                rm1 = rm1 + (rm2 - rm1) * (240.0f - rh) / 60.0f;
            }

            return (byte)(rm1 * 255);
        }

        /// <summary>
        /// Returns an image with the Hue modified by the specified ammount
        /// </summary>
        /// <param name="image"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static Bitmap ChangeHue(Bitmap image, float h)
        {
            bool IsTransparent = false;
            try
            {

                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = image.Width * image.Height;

                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                Color cColor;
                for (int iWidth = 0; iWidth < image.Width; iWidth++)
                {
                    for (int iHeight = 0; iHeight < image.Height; iHeight++)
                    {
                        cColor = newUBMP.GetPixel(iWidth, iHeight);
                        HLS newHLS = new HLS();
                        newHLS.Red = cColor.R;
                        newHLS.Blue = cColor.B;
                        newHLS.Green = cColor.G;

                        newHLS.Hue += h;
                        newHLS.ToRGB();

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(cColor);

                        cColor = Color.FromArgb(newHLS.Red, newHLS.Green, newHLS.Blue);
                        newUBMP.SetPixel(iWidth, iHeight, cColor);

                        Global.iCurrentOp_PixelCount++;

                    }
                }

                newUBMP.UnlockImage();

                Global.bTackPixelProgress = false;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
            if (IsTransparent)
                image.MakeTransparent();
            return image;
        }

        /// <summary>
        /// Returns the source image with the luminosity modified by the specified ammount
        /// </summary>
        /// <param name="sourceimage"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public static Bitmap ChangeLuminosity(Bitmap image, float l)
        {
            bool IsTransparent = false;
            try
            {
                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = image.Width * image.Height;

                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                Color c;
                for (int iWidth = 0; iWidth < image.Width; iWidth++)
                {
                    for (int iHeight = 0; iHeight < image.Height; iHeight++)
                    {
                        c = newUBMP.GetPixel(iWidth, iHeight);
                        HLS newHLS = new HLS();
                        newHLS.Red = c.R;
                        newHLS.Blue = c.B;
                        newHLS.Green = c.G;

                        if (newHLS.Luminance + l >= 1f)
                        {
                            newHLS.Luminance = 1f;
                        }
                        else if (newHLS.Luminance + l <= 0.0f)
                        {
                            newHLS.Luminance = 0.0f;
                        }
                        else
                        {
                            newHLS.Luminance += l;
                        }
                        newHLS.ToRGB();

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(c);

                        c = Color.FromArgb(newHLS.Red, newHLS.Green, newHLS.Blue);
                        newUBMP.SetPixel(iWidth, iHeight, c);

                        Global.iCurrentOp_PixelCount++;
                    }
                }

                newUBMP.UnlockImage();

                Global.bTackPixelProgress = false;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
            if (IsTransparent)
                image.MakeTransparent();

            return image;
        }

        /// <summary>
        /// Returns the source image with the saturation modified by the specified ammount
        /// </summary>
        public static Bitmap ChangeSaturation(Bitmap image, float s)
        {
            bool IsTransparent = false;
            try
            {
                IsTransparent = false;// Utilites.CheckImageTransparency(sourceimage);

                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = image.Width * image.Height;

                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                Color cColor;
                for (int iWidth = 0; iWidth < image.Width; iWidth++)
                {
                    for (int iHeight = 0; iHeight < image.Height; iHeight++)
                    {
                        cColor = newUBMP.GetPixel(iWidth, iHeight);
                        HLS newHLS = new HLS();
                        newHLS.Red = cColor.R;
                        newHLS.Blue = cColor.B;
                        newHLS.Green = cColor.G;

                        if (newHLS.Saturation + s >= 1f)
                        {
                            newHLS.Saturation = 1f;
                        }
                        else if (newHLS.Saturation + s <= 0.0f)
                        {
                            newHLS.Saturation = 0.0f;
                        }
                        else
                        {
                            newHLS.Saturation += s;
                        }
                        newHLS.ToRGB();

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(cColor);

                        cColor = Color.FromArgb(newHLS.Red, newHLS.Green, newHLS.Blue);
                        newUBMP.SetPixel(iWidth, iHeight, cColor);

                        Global.iCurrentOp_PixelCount++;
                    }
                }

                newUBMP.UnlockImage();

                Global.bTackPixelProgress = false;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            if (IsTransparent)
                image.MakeTransparent();

            return image;
        }        
    }
}
