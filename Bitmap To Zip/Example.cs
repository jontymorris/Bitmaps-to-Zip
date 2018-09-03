using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace Bitmap_To_Zip
{
    class Example
    {

        static void Main(string[] args)
        {
            List<Bitmap> myBitmapList = new List<Bitmap>();

            // Psuedo code for loading bitmaps 
            for (int i = 0; i < 10; i++)
            {
                myBitmapList.Add(new Bitmap("bitmap" + i + ".png"));
            }

            // Example usage
            BitmapListToZip bitmapConverter = new BitmapListToZip();
            bitmapConverter.ConvertBitmapListToZip(myBitmapList, outputPath: "/my/output/folder", zipName: "MyZipFile", zipPassword: "Password123");
        }

    }
}
