using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Ionic.Zip;

namespace Bitmap_To_Zip
{

    class BitmapListToZip
    {

        /// <summary>
        /// Writes a list of bitmap images to a temporary directory which is then turned into a zip.
        /// </summary>
        /// <param name="bitmapList">The list of bitmap images.</param>
        /// <param name="outputPath">Where will the zip file be created..</param>
        /// <param name="zipName">Name of the outputted zip file.</param>
        /// <param name="zipPassword">Password that will be used to lock the zip file. If empty, then no password will be applied.</param>
        public void ConvertBitmapListToZip(List<Bitmap> bitmapList, string outputPath = "./", string zipName = "ImageZip", string zipPassword = "")
        {
            // Create the zip directory
            using (ZipFile zip = new ZipFile())
            {
                // Check if a zip password was provided
                if (!String.IsNullOrEmpty(zipPassword))
                {
                    zip.Password = zipPassword;
                }

                // Add all the bitmaps to the zip
                for (int i=0; i<bitmapList.Count; i++)
                {
                    zip.AddEntry(FormatIndex(i), ImageToByte(bitmapList[i]));
                }
                
                // Save the zip file
                zip.Save(Path.Combine(outputPath, zipName + ".zip"));
            }
        }

        /// <summary>
        /// Converts an image to bytes
        /// </summary>
        /// <returns></returns>
        private byte[] ImageToByte(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Formats the given index as a file name.
        /// </summary>
        /// <returns></returns>
        private string FormatIndex(int index)
        {
            string format = index.ToString();

            // Calculate how many digits need to be added
            int neededDigits = 7 - format.Length;
            if (neededDigits > 0)
            {
                for (int i=0; i<neededDigits; i++)
                {
                    format = "0" + format;
                }
            }

            return format + ".png";
        }
        
    }

}
