using System;
using System.Drawing;

namespace Photoman.Core
{
    /// <summary>
    /// Handles all the operations dealing with RGB color
    /// </summary>
    class RGB
    {
        /// <summary>
        /// Inverts the given image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Invert(Bitmap image)
        {
            try
            {
                //Flag to track transparency
                bool IsTransparent = false;

                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = image.Width * image.Height;

                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                //The color to set the pixel to.
                Color c;

                for (int i = 0; i < image.Width; i++) // interate  hru image pixel by pixel
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        c = newUBMP.GetPixel(i, j); // get the pixels color
                        newUBMP.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B)); // invert the colors

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(c);

                        Global.iCurrentOp_PixelCount++;
                    }
                }

                newUBMP.UnlockImage();

                if (IsTransparent)
                    image.MakeTransparent();

                Global.bTackPixelProgress = false;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return image;
        }

        /// <summary>
        /// Returns a grayscale version of the given image
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap Grayscale(Bitmap bmpSource)
        {
            try
            {
                //transparency check flag
                bool IsTransparent = false;

                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = bmpSource.Width * bmpSource.Height;

                UnsafeBitmap newUBMP = new UnsafeBitmap(bmpSource);
                newUBMP.LockImage();

                for (int i = 0; i < bmpSource.Width; i++)
                {
                    for (int j = 0; j < bmpSource.Height; j++)
                    {
                        //get the pixel from the original image
                        Color originalColor = newUBMP.GetPixel(i, j);

                        //create the grayscale version of the pixel
                        int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                            + (originalColor.B * .11));

                        //create the color object
                        Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(originalColor);
                        //set the new image's pixel to the grayscale version
                        newUBMP.SetPixel(i, j, newColor);

                        Global.iCurrentOp_PixelCount++;
                    }
                }
                newUBMP.UnlockImage();

                if (IsTransparent)
                    bmpSource.MakeTransparent();
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return bmpSource;
        }

        /// <summary>
        /// Makes gamma info for the Gamma function by getting RGB values between 5 and .2
        /// </summary>
        /// <param name="colortype"></param>
        /// <returns></returns>
        private static byte[] MakeGammainfo(double colortype)
        {
            byte[] gammaArray = new byte[256];

            try
            {
                for (int i = 0; i < 256; ++i)
                {
                    gammaArray[i] = (byte)Math.Min(255,
                                                   (int)((255.0 * Math.Pow(i / 255.0, 1.0 / colortype)) + 0.5));
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return gammaArray;
        }

        /// <summary>
        /// Returns a bitmap with the gamma modified according to the given rgb value
        /// </summary>
        /// <param name="bitmp"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static Bitmap Gamma(Bitmap image, double red, double green, double blue)
        {
            try
            {
                if (red <= 5 || red >= .2 || blue <= 5 || blue >= .2 || blue <= 5 || blue >= .2)
                {
                    byte[] redGamma = MakeGammainfo(red);
                    byte[] greenGamma = MakeGammainfo(green);
                    byte[] blueGamma = MakeGammainfo(blue);

                    bool IsTransparent = false;

                    //Set up progress
                    Global.bTackPixelProgress = true;
                    Global.iCurrentOp_PixelCount = 0;
                    Global.iCurrentOp_PixelTotal = image.Width * image.Height;

                    UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                    newUBMP.LockImage();

                    for (int i = 0; i < image.Width; i++)
                    {
                        for (int j = 0; j < image.Height; j++)
                        {
                            Color c = newUBMP.GetPixel(i, j);
                            newUBMP.SetPixel(i, j, Color.FromArgb(redGamma[c.R],
                               greenGamma[c.G], blueGamma[c.B]));

                            //if the image hasnt been determined as transparent, check it. 
                            if (!IsTransparent)
                                IsTransparent = Global.CheckPixelTransparency(c);

                            Global.iCurrentOp_PixelCount++;
                        }
                    }

                    newUBMP.UnlockImage();

                    if (IsTransparent)
                        image.MakeTransparent();

                    Global.bTackPixelProgress = false;

                    return image;
                }

                else
                {
                    return image;
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return image;
            }
        }

        /// <summary>
        /// Returns an image brightened by the specified ammount. Just a shortcut of Gamma with only 
        /// one parameter
        /// </summary>
        /// <param name="image"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static Bitmap Brightness(Bitmap image, float brightness)
        {
            return Gamma(image, brightness, brightness, brightness);
        }

        /// <summary>
        /// Returns a sepia toned image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Sepia(Bitmap image)
        {
            try
            {
                bool IsTransparent = false;

                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = image.Width * image.Height;

                try
                {
                    UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                    newUBMP.LockImage();

                    for (int i = 0; i < image.Width; i++)
                        for (int j = 0; j < image.Height; j++)
                        {
                            Color cOriginalPixelColor = newUBMP.GetPixel(i, j);

                            double dRed = (cOriginalPixelColor.R * .393) + (cOriginalPixelColor.G * .769) + (cOriginalPixelColor.B * .189);
                            double dBlue = (cOriginalPixelColor.R * .272) + (cOriginalPixelColor.G * .534) + (cOriginalPixelColor.B * .131);
                            double dGreen = (cOriginalPixelColor.R * .349) + (cOriginalPixelColor.G * .686) + (cOriginalPixelColor.B * .168);

                            if (dRed > 255) dRed = 255;
                            if (dGreen > 255) dGreen = 255;
                            if (dBlue > 255) dBlue = 255;

                            newUBMP.SetPixel(i, j, Color.FromArgb((int)cOriginalPixelColor.A, (int)Math.Round(dRed), (int)Math.Round(dGreen), (int)Math.Round(dBlue)));

                            if (!IsTransparent)
                                IsTransparent = Global.CheckPixelTransparency(cOriginalPixelColor);

                            Global.iCurrentOp_PixelCount++;
                        }

                    newUBMP.UnlockImage();

                }
                catch (Exception ex)
                {
                	Global.WriteToLog(ex);
                }

                if (IsTransparent)
                    image.MakeTransparent();

                Global.bTackPixelProgress = false;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return image;
        }

        /// <summary>
        /// Dithers an image threshold style, where no shading occures
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Dither_Threshold(Bitmap image)
        {
            bool IsTransparent = false;

            //Set up progress
            Global.bTackPixelProgress = true;
            Global.iCurrentOp_PixelCount = 0;
            Global.iCurrentOp_PixelTotal = image.Width * image.Height;

            try
            {
                
                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                for (int i = 0; i < image.Width; i++)
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color cOriginalPixelColor = newUBMP.GetPixel(i, j);

                        float fAddedPixels = cOriginalPixelColor.R + cOriginalPixelColor.B + cOriginalPixelColor.G;

                        //Dithering code goes here
                        Color newColor = Color.FromArgb((fAddedPixels >= 382.5 ? 255 : 0), (fAddedPixels >= 382.5 ? 255 : 0),
                                                    (fAddedPixels >= 382.5 ? 255 : 0));

                        newUBMP.SetPixel(i, j, newColor);

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(cOriginalPixelColor);

                        Global.iCurrentOp_PixelCount++;
                    }
                newUBMP.UnlockImage();
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            if (IsTransparent)
                image.MakeTransparent();

            Global.bTackPixelProgress = false;

            return image;
        }

        /// <summary>
        /// Dithers an image Floyd style. This produces a grainy image but is the industry standard
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Dither_Floyd(Bitmap image)
        {
            bool IsTransparent = false;

            //Set up progress
            Global.bTackPixelProgress = true;
            Global.iCurrentOp_PixelCount = 0;
            Global.iCurrentOp_PixelTotal = image.Width * image.Height;

            try
            {                
                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                for (int i = 0; i < image.Width; i++)
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color cOriginalPixelColor = newUBMP.GetPixel(i, j);

                        //create the grayscale version of the pixel - using code here so we only need to do math for one color value instead of 3
                        int grayScale = (int)((cOriginalPixelColor.R * .3) + (cOriginalPixelColor.G * .59)
                        + (cOriginalPixelColor.B * .11));


                        //Dithering code goes here
                        Color newColor = Color.FromArgb((grayScale >= 128 ? 255 : 0), (grayScale >= 128 ? 255 : 0),
                                                    (grayScale >= 128 ? 255 : 0));

                        float fQuantError = grayScale - newColor.R;//Color.FromArgb(1, 1, 1);

                        //Set our current pixel
                        newUBMP.SetPixel(i, j, newColor);

                        //Store color here and set to black to avoid compiler error
                        Color c1 = Color.Black;

                        try
                        {
                            //Set pixel at x+1,y if valid                        
                            if (i + 1 < image.Width)
                            {
                                c1 = newUBMP.GetPixel(i + 1, j);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 7d / 16d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i + 1, j, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //Set pixel at x-1, y+1
                            if (i - 1 > 0 && j + 1 < image.Height)
                            {

                                c1 = newUBMP.GetPixel(i - 1, j + 1);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 3d / 16d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i - 1, j + 1, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //set pixel at x, y + 1
                            if (j + 1 < image.Height)
                            {
                                c1 = newUBMP.GetPixel(i, j + 1);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 7d / 16d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i, j + 1, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //set pixel at x + 1, y + 1
                            if (i + 1 < image.Width && j + 1 < image.Height)
                            {
                                c1 = newUBMP.GetPixel(i + 1, j + 1);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 7d / 16d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i + 1, j + 1, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(cOriginalPixelColor);

                        Global.iCurrentOp_PixelCount++;
                    }

                newUBMP.UnlockImage();
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            if (IsTransparent)
                image.MakeTransparent();

            Global.bTackPixelProgress = false;

            return image;
        }

        public static Bitmap Dither_Atkinson(Bitmap image)
        {
            bool IsTransparent = false;

            //Set up progress
            Global.bTackPixelProgress = true;
            Global.iCurrentOp_PixelCount = 0;
            Global.iCurrentOp_PixelTotal = image.Width * image.Height;

            try
            {                
                UnsafeBitmap newUBMP = new UnsafeBitmap(image);
                newUBMP.LockImage();

                for (int i = 0; i < image.Width; i++)
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color cOriginalPixelColor = newUBMP.GetPixel(i, j);

                        //create the grayscale version of the pixel - using code here so we only need to do math for one color value instead of 3
                        int grayScale = (int)((cOriginalPixelColor.R * .3) + (cOriginalPixelColor.G * .59)
                        + (cOriginalPixelColor.B * .11));


                        //Dithering code goes here
                        Color newColor = Color.FromArgb((grayScale >= 128 ? 255 : 0), (grayScale >= 128 ? 255 : 0),
                                                    (grayScale >= 128 ? 255 : 0));

                        float fQuantError = grayScale - newColor.R;//Color.FromArgb(1, 1, 1);

                        //Set our current pixel
                        newUBMP.SetPixel(i, j, newColor);

                        //Store color here and set to black to avoid compiler error
                        Color c1 = Color.Black;

                        try
                        {
                            //Set pixel at x+1,y if valid                        
                            if (i + 1 < image.Width)
                            {
                                c1 = newUBMP.GetPixel(i + 1, j);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 1d / 8d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i + 1, j, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //Set pixel at x+2,y if valid                        
                            if (i + 2 < image.Width)
                            {
                                c1 = newUBMP.GetPixel(i + 2, j);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 1d / 8d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i + 2, j, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //Set pixel at x-1, y+1
                            if (i - 1 > 0 && j + 1 < image.Height)
                            {

                                c1 = newUBMP.GetPixel(i - 1, j + 1);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 1d / 8d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i - 1, j + 1, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //set pixel at x, y + 1
                            if (j + 1 < image.Height)
                            {
                                c1 = newUBMP.GetPixel(i, j + 1);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 1d / 8d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i, j + 1, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));
                            }

                            //set pixel at x + 1, y + 1
                            if (i + 1 < image.Width && j + 1 < image.Height)
                            {
                                c1 = newUBMP.GetPixel(i + 1, j + 1);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 1d / 8d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i + 1, j + 1, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));

                            }

                            //set pixel at x + 1, y + 2
                            if (i + 1 < image.Width && j + 2 < image.Height)
                            {
                                c1 = newUBMP.GetPixel(i + 1, j + 2);

                                grayScale = (int)((c1.R * .3) + (c1.G * .59)
                                + (c1.B * .11));

                                int iNewOffsetColor = (int)Math.Round((double)grayScale + 1d / 8d * fQuantError);

                                if (iNewOffsetColor > 255)
                                    iNewOffsetColor = 255;
                                else if (iNewOffsetColor < 0)
                                    iNewOffsetColor = 0;

                                newUBMP.SetPixel(i + 1, j + 2, Color.FromArgb(
                                                          iNewOffsetColor,
                                                          iNewOffsetColor,
                                                          iNewOffsetColor));

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(cOriginalPixelColor);

                        Global.iCurrentOp_PixelCount++;
                    }
                newUBMP.UnlockImage();
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            if (IsTransparent)
                image.MakeTransparent();

            Global.bTackPixelProgress = false;

            return image;
        }

    }
}
