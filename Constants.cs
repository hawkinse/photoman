using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Photoman
{
    /// <summary>
    /// This class is for the storage of all global constants
    /// </summary>
    static class Constants
    {
        /// <summary>
        /// The path of the cache directory
        /// </summary>
        public const string strCachePath = @"Cache\";

        /// <summary>
        /// The path of the history directory
        /// </summary>
        public const string strHistPath = @"TempHistory";

        /// <summary>
        /// The ammount of pixels that the popup preview is offset by
        /// </summary>
        public const int iPreviewMouseOffest = 5;

        /// <summary>
        /// The size of the mouse over preview. iPreviewSize x iPreviewSize, corrected for aspect ratio
        /// </summary>
        public const int iPreviewSize = 128; //128 is the best compromise between detail/visibility and speed on my machine, so its the default

        /// <summary>
        /// The size of the thumbnails in the image list. iThumbnailSize x iThumbnailSize, corrected for aspect ratio
        /// </summary>
        public const int iThumbnailSize = 32;

        /// <summary>
        /// The ammount to offset the slider image by
        /// </summary>
        public const int iSliderOffset = 10;

        /// <summary>
        /// The height of the slider images
        /// </summary>
        public const int iSliderImageHeight = 5;

        /// <summary>
        /// The ammount to offset the current memory usage percentage by when forcing cache checks to accomodate for future images.
        /// </summary>
        public const int iCachePercentageOffsetOnForce = 5;

        /// <summary>
        /// The ammount of bytes to add onto any cache calculations to account for program memory consumpion
        /// in addition to an image. For example, thumbnails.
        /// </summary>
        public const int iCacheProgramOverhead = 209715200; //Give 200mb of head room
        //Credits
        public const string strCredits = "Photoman Copyright (C) 2016 Elliot Hawkins\n";

        //Lience
        public const string strLicense = "Photoman is released under an MIT license, except where not applicable\n" +
                                         "due to existing licenses from code referenced.\n\n" +
                                         "These include the Code Project Open License 1.02 license for the marching\n" +
                                         "ant effect on mouse selections, and the Visual C# Kicks license for the\n" +
                                         "UnsafeBitmap.cs class.\n\n";

        //Tooltips
        #region Settings Tooltips
        public const string strDitheringMethod = "The style of Dithering to use.";
        public const string strDisableEffectsOnBattery = "Disables graphical effects when on the battery, such as the animated border on mouse operations. This is done to save battery life.";
        public const string strMemPercentForCache = "The percentage of total system memory that needs to be in use before Photoman will start using disk cache.";
        #endregion

        //Program strings
        #region GUIStrings

        public const string strCacheEnabled = "Disk Cache is Enabled";
        public const string strCacheDisabled = "Disk Cache is Disabled";
        public const string strNumberImagesLoaded = " images loaded";
        public const string strNumberOfUndos = " Undos, ";
        public const string strNumberOfRedos = " Redos";
        public const string strNumberOfExceptions = " Exceptions";

        #endregion
        /// <summary>
        /// Enum of all operations in program. 
        /// </summary>
        public enum Operations
        {
            /// <summary>
            /// ONLY for HistoryHandler use
            /// </summary>
            LoadImage,
            /// <summary>
            /// ONLY for Historyhandler use
            /// </summary>
            SaveImage,
            /// <summary>
            /// ONLY for Historyhandler use
            /// </summary>
            SwitchImage,
            ResizePixels,
            ResizePercent,
            /// <summary>
            /// Depreciated. To be removed in future versions
            /// </summary>
            ResizeInches,
            /// <summary>
            /// Depreciated. To be removed in future versions
            /// </summary>
            ResizeCentimeters,
            Crop,
            Flip,
            Rotate,
            Hue,
            Saturation,
            Luminosity,
            Brightness,
            Gamma,
            Grayscale,
            Dither,
            Sepia,
            Invert,
            RemoveRedEye,
            AddText,
            ChromaKey
        }
    }
}
