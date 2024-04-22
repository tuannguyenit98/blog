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

        public static string GetPublicIdFromUrl(string url)
        {
            int idx = url.LastIndexOf('.');
            string publicId = string.Empty;

            if (idx != -1)
            {
                var a = url.Substring(0, idx); // "https://res.cloudinary.com/dibbsh3z8/image/upload/v1713799824/jxxiykyxay3eoordxngh"
                int idx1 = a.LastIndexOf('/');
                publicId = a.Substring(idx1 + 1);
            }
            return publicId;
        }
    }
}
