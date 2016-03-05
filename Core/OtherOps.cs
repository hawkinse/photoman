using System;
using System.Drawing;

namespace Photoman.Core
{
    /// <summary>
    /// This class handles operations that dont clearly fall into other catagories.
    /// This so far includes add text and red eye removal
    /// </summary>
    class OtherOps
    {
        /// <summary>
        /// returns the source bitmap with text added in the specified region
        /// </summary>
        public static Bitmap AddCaption(Bitmap image, Rectangle Positioning, string Text, Font Font, Brush color)
        {
            try
            {
                if ((image.Width < Positioning.X) || 
                    (image.Width < Positioning.Width) || 
                    (image.Height < Positioning.Y) || 
                    (image.Height < Positioning.Height))
                    throw new Exception("Text position is out of bounds");

                Graphics g = Graphics.FromImage(image);
                g.DrawString(Text, Font, color, Positioning);

            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
            return image;
        }

        /// <summary>
        /// Returns an image with red removed from the selected area
        /// </summary>
        /// <param name="sourceimage"></param>
        /// <param name="CropOut"></param>
        /// <returns></returns>
        public static Bitmap RedEyeRemoval(Bitmap sourceimage, Rectangle CropOut)
        {
            try
            {
                if ((sourceimage.Width < CropOut.X) || (sourceimage.Width < CropOut.Width) || (sourceimage.Height < CropOut.Y) || (sourceimage.Height < CropOut.Height))
                    throw new Exception("Text position is out of bounds");

                Bitmap bmpTemp = BasicOps.Crop(sourceimage, CropOut);
                return ReplaceRedEye(sourceimage, RemoveRed(bmpTemp), CropOut);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                //Return source image sicne we cant remove the red eye
                return sourceimage;
            }
        }

        /// <summary>
        /// helper method for RedEyeRemoval() that actualy removes the red eye
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Bitmap RemoveRed(Bitmap image)
        {
            try
            {
                Color cColor;
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++) // iterate thrugh the bitmap, pixel by pixe;
                    {

                        cColor = image.GetPixel(i, j);
                        float redIntensity = ((float)cColor.R / ((cColor.G + cColor.B) / 2));
                        if (redIntensity > 2f)  // so far, 2 appears to work better than original 1.5
                        {
                            // reduce red to the average of blue and green
                            image.SetPixel(i, j, Color.FromArgb((cColor.G + cColor.B) / 2, cColor.G, cColor.B));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return image;
        }

        /// <summary>
        /// helper method for RedEyeRemoval() that replaces the old eyes with new eyes from RemoveRed()
        /// </summary>
        /// <param name="image"></param>
        /// <param name="eyes"></param>
        /// <param name="Postion"></param>
        /// <returns></returns>
        private static Bitmap ReplaceRedEye(Bitmap image, Bitmap eyes, Rectangle Postion)
        {
            try
            {
                Graphics gObject = Graphics.FromImage(image);
                gObject.DrawImage(eyes, Postion.X, Postion.Y, Postion.Width, Postion.Height);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return image;
        }

        public static Bitmap ChromaKey(Bitmap ForgroundImage, Bitmap BackgroundImage, float Sensitivity)
        {
            Bitmap bmpTemp = ForgroundImage;

            try
            {                
                bmpTemp = (Bitmap)ForgroundImage.Clone();
                Bitmap bmpBackground = new Bitmap(BackgroundImage, bmpTemp.Width, bmpTemp.Height);

                //Flag to track transparency
                bool IsTransparent = false;

                //Set up progress
                Global.bTackPixelProgress = true;
                Global.iCurrentOp_PixelCount = 0;
                Global.iCurrentOp_PixelTotal = bmpTemp.Width * bmpTemp.Height;

                UnsafeBitmap ubmpForground = new UnsafeBitmap(bmpTemp);
                ubmpForground.LockImage();

                UnsafeBitmap ubmpBackground = new UnsafeBitmap(bmpBackground);
                ubmpBackground.LockImage();

                Color cWorkingColor;

                for (int iX = 0; iX < bmpTemp.Width; iX++)
                {
                    for (int iY = 0; iY < bmpTemp.Height; iY++)
                    {
                        cWorkingColor = ubmpForground.GetPixel(iX, iY);

                        float fIntensity = ((float)cWorkingColor.G / ((cWorkingColor.B + cWorkingColor.R) / 2));
                        if (fIntensity > Sensitivity)
                            ubmpForground.SetPixel(iX, iY, ubmpBackground.GetPixel(iX, iY));

                        if (!IsTransparent)
                            IsTransparent = Global.CheckPixelTransparency(cWorkingColor);

                        Global.iCurrentOp_PixelCount++;
                    }
                }

                ubmpForground.UnlockImage();
                ubmpBackground.UnlockImage();

                if (IsTransparent)
                    bmpTemp.MakeTransparent();

                Global.bTackPixelProgress = false;                
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return bmpTemp;
        }
    }
}
