using System;
using System.Drawing;

namespace Photoman.Core
{
    /// <summary>
    /// This class deals with basic operations such as Rotate, Flip, Resize and Crop
    /// </summary>
    class BasicOps
    {
        //Enum for rotation direction
        public enum RotateDirection { Left, Right };

        /// <summary>
        /// Rotates the image in the given direction
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Bitmap Rotate(Bitmap image, RotateDirection rotation)
        {
            try
            {
                image.RotateFlip(rotation == RotateDirection.Left ?
                                        RotateFlipType.Rotate90FlipNone :
                                        RotateFlipType.Rotate270FlipNone);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
            return image;
        }

        //Enum for flip axis
        public enum FlipAxis { X, Y };

        /// <summary>
        /// Flips the bitmap along the given axis
        /// </summary>
        public static Bitmap Flip(Bitmap image, FlipAxis Axis)
        {
            try
            {
                image.RotateFlip(Axis == FlipAxis.X ? RotateFlipType.RotateNoneFlipX :
                                                      RotateFlipType.RotateNoneFlipY);
            }            
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }

            return image;
        }

        /// <summary>
        /// Crops the given image to the area selected in the rectangle
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        public static Bitmap Crop(Bitmap image, Rectangle cropArea)
        {
            try
            {
                if ((cropArea.X > image.Width) ||
                    (cropArea.Width > image.Width) ||
                    (cropArea.Y > image.Height) ||
                    (cropArea.Height > image.Height) ||
                    cropArea.X < 0 ||
                    cropArea.Y < 0 ||
                    cropArea.Width < 0 ||
                    cropArea.Height < 0)
                    {
                        throw new Exception("Crop area is out of bounds");
                    }

                Bitmap bmpCrop = image.Clone(cropArea, image.PixelFormat);
                return bmpCrop;
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                //Return the source image as we cant crop it.
                return image;
            }
        }

        /// <summary>
        /// Generic resize in pixels
        /// </summary>
        /// <param name="sourceimage"></param>
        /// <param name="SizeX"></param>
        /// <param name="SizeY"></param>
        /// <returns></returns>
        public static Bitmap ResizeByPixels(Bitmap image, int SizeX, int SizeY)
        {
            try
            {
                return new Bitmap(image, SizeX, SizeY);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return image;
            }
        }

        /// <summary>
        /// Resizes by percentage
        /// </summary>
        /// <param name="sourceimage"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static Bitmap ResizeByPercent(Bitmap image, int percentage)
        {
            try
            {
                //Get percentage
                double NewHeight = image.Height * percentage * .01;
                double NewWidth = image.Width * percentage * .01;

                //Rounds since we cant have decimals in pixels
                int RoundedHeight = (int)Math.Round(NewHeight);
                int RoundedWidth = (int)Math.Round(NewWidth);

                //Makes new bitmap and return
                return new Bitmap(image, RoundedWidth, RoundedHeight);
            }
            catch(Exception ex)
            {
                Global.WriteToLog(ex);
                return image;
            }
        }

        /// <summary>
        /// resize by centimeters
        /// </summary>
        /// <param name="image"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static Bitmap ResizeByCentimeters(Bitmap image, float Width, float Height)
        {
            try
            {
                //Store original vertical and horizontal DPI
                float fltVertDPI = image.VerticalResolution;
                float fltHorDPI = image.HorizontalResolution;

                //new values
                float NewHeight = Height * fltHorDPI;
                float NewWidth = Width * fltVertDPI;

                return ResizeByPixels(image, (int)NewWidth, (int)NewHeight);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return image;
            }
        }

        /// <summary>
        /// Resizes the given image by inches
        /// </summary>
        /// <param name="image"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static Bitmap ResizeByInches(Bitmap image, float Width, float Height)
        {
            //Store original vertical and horizontal DPI
            float fltVertDPI = image.VerticalResolution;
            float fltHorDPI = image.HorizontalResolution;
            //Convert centimeters into inches
            float fltConvertedHorizontal = Height / 2.54f;
            float fltConvertedVertical = Width / 2.54f;
            //new values
            int NewHeight = (int)Math.Round((double)fltConvertedHorizontal * (double)fltHorDPI);
            int NewWidth = (int)Math.Round((double)fltConvertedVertical * (double)fltVertDPI);

            //For testing purposes only
            //MessageBox.Show("New values\n\nNew height = " + NewHeight.ToString() + "\n\nNew Width = " + NewWidth.ToString());

            return ResizeByPixels(image, (int)NewWidth, (int)NewHeight);
        }

        /// <summary>
        /// Returns a size with the same aspect ratio as the source
        /// </summary>
        /// <param name="image"></param>
        /// <param name="SizeX"></param>
        /// <param name="SizeY"></param>
        /// <returns></returns>
        public static Size MaintainAspectRatio(Size NewSize, Size OldSize)
        {
            try
            {
                if (NewSize.Width > NewSize.Height)
                {
                    //Removes incorrect ratio from coridenet
                    NewSize.Height = (int)Math.Round(((float)OldSize.Height * (float)NewSize.Width)
                                                / (float)OldSize.Width);

                }
                else
                {
                    NewSize.Width = (int)Math.Round(((float)OldSize.Width * (float)NewSize.Height)
                                                    / (float)OldSize.Height);
                }
                return new Size((int)NewSize.Width, (int)OldSize.Height);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return OldSize;
            }
        }

        public static Size MaintainAspectRatio_DontExcedeNewSize(Size NewSize, Size OldSize)
        {
            try
            {
                Size AdjustedSize1 = new Size(NewSize.Width, NewSize.Height);
                Size AdjustedSize2 = new Size(NewSize.Width, NewSize.Height);


                float fAspectRatio = (float)OldSize.Height / (float)OldSize.Width;

                //Change height of AS1                                
                AdjustedSize1.Height = (int)Math.Round((float)NewSize.Width * fAspectRatio);

                //Change height of AS2
                AdjustedSize2.Width = (int)Math.Round((float)NewSize.Height * fAspectRatio);

                //Compare
                if (AdjustedSize1.Width <= NewSize.Width && AdjustedSize1.Height <= NewSize.Height)
                    return AdjustedSize1;
                else
                    return AdjustedSize2;

                //
                //AdjustedSize.Width = (int)Math.Round((float)NewSize.Height * fAspectRatio);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
                return OldSize;
            }
        }
    }
}
