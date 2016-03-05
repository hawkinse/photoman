using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Photoman.UserInterface.UIData
{
    [Serializable()]

    class udImage_DragDrop
    {
        public string Indentifier { get; private set; }

        public static readonly string IdentifierCheck = "THIS IS VALID";
        public Bitmap BitmapData;
        public int Index = -1;

        public udImage_DragDrop()
        {
            this.Indentifier = IdentifierCheck;
        }

        public udImage_DragDrop(SerializationInfo info, StreamingContext ctxt)
        {
            this.Indentifier = (string)info.GetValue("Identifier", typeof(string));
            BitmapData = (Bitmap)info.GetValue("BitmapData", typeof(Bitmap));
            Index = (int)info.GetValue("ImageIndex", typeof(int));

            //Sanity check
            if (this.Indentifier != IdentifierCheck)
                throw new Exception("Identifier is not valid!");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Identifier", this.Indentifier);
            info.AddValue("BitmapData", BitmapData);
            info.AddValue("ImageIndex", Index);
        }
    }
}
