using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
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
            // Create the temp directory
            string tempPath = CreateTempFolder(zipName);

            // Write the bitmaps to the temp directory
            int index = 0;
            foreach (Bitmap bitmap in bitmapList)
            {
                bitmap.Save(Path.Combine(tempPath, FormatIndex(index)));
                index += 1;
            }

            // Zip up the temp directory and save to desired path
            using (ZipFile zip = new ZipFile())
            {
                // Check if a zip password was provided
                if (!String.IsNullOrEmpty(zipPassword))
                {
                    zip.Password = zipPassword;
                }

                zip.AddDirectory(tempPath);
                zip.Save(Path.Combine(outputPath, zipName + ".zip"));
            }

            // Delete the temp directory
            Directory.Delete(tempPath, true);
        }
        
        /// <summary>
        /// Creates a folder in the temporary directory location.
        /// </summary>
        /// <param name="folderName">Name of the temporary directory</param>
        /// <returns>Full path of the new temporary directory.</returns>
        private string CreateTempFolder(string folderName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), folderName);

            // Check if there is already a temp directory
            if (Directory.Exists(tempPath))
            {
                // Try and delete the temp directory
                try
                {
                    Directory.Delete(tempPath, true);
                } catch (IOException exp)
                {
                    Console.WriteLine(exp);
                }
                
            }

            Directory.CreateDirectory(tempPath);
            return tempPath;
        }
        
        /// <summary>
        /// Formats the given index as a file name.
        /// </summary>
        /// <param name="index">Index that will be formatted</param>
        /// <returns></returns>
        private string FormatIndex(int index)
        {
            string format = index.ToString();

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
