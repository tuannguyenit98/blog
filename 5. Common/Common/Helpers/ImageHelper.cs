using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Common.Helpers
{
    public class ImageHelper
    {
        public static readonly string[] ImageType = { "jpg", "png", "jpeg", "jfif" };

        public static bool IsImage(string filename)
        {
            string fileEts = Path.GetExtension(filename).Replace(".", "").ToLower();
            return ImageType.Contains(fileEts);
        }

        public static bool IsImageInclude(string filename, string reg) // reg = 'jpg, png'
        {
            string fileEts = Path.GetExtension(filename).Replace(".", "").ToLower();
            return ImageType.Contains(fileEts) && reg.Contains(fileEts) ? true : false;
        }
    }
}
