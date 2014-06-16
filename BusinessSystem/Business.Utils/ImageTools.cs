using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Business.Utils
{
    public static class ImageTools
    {
        public static string GetImgSizeBy(this string imageUrl, int width, int height)
        {
            Regex reg = new Regex(@"^(http://.+)(.jpg|.bmp|.png|.gif)$", RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                Match match = reg.Match(imageUrl);
                if (match.Success)
                {
                    string pathWithoutExtend = match.Groups[1].Value;
                    string extend = match.Groups[2].Value;
                    imageUrl = string.Format("{0}={2}x{3}{1}", pathWithoutExtend, extend, width, height);
                }
            }
            return imageUrl;
        }

        public static Image  GetImage(int Width, int Height,Stream imageStream)
        {
            Image img = Image.FromStream(imageStream);
            img = img.GetThumbnailImage(Width, Height, () => false, IntPtr.Zero);
            return img;
        }
    }
}



