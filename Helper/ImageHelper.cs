using PosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.Helper
{
    public static class ImageHelper
    {
        public static string ImportImageToFolder(string folderName,string url)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;

            string destinationFolder = $"{folder}\\Assets\\Images\\{folderName}";

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string fileName = Path.GetFileName(url);
            string destinationFilePath = Path.Combine(destinationFolder, fileName);
            Debug.WriteLine(destinationFilePath);

            try
            {
                File.Copy(url, destinationFilePath, true);
                MessageBox.Show("Saved Successfully.");
                return destinationFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return "";
;            }
        }
    }
}
