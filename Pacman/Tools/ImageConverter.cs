using System.IO;
using System.Windows.Media.Imaging;

namespace Pacman.Tools
{
    internal static class ImageConverter
    {
        public static BitmapImage ConvertToBitmapImage(this System.Drawing.Bitmap bitmap)
        {
            var bitmapImage = new BitmapImage();

            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }
    }
}