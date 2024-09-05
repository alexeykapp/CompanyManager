using System.IO;
using System.Windows.Media.Imaging;

namespace CompanyManager.Converters
{
    public class ByteImage
    {
        public BitmapImage ConvertByteArrayToBitmapImage(byte[] bytes)
        {
            if (bytes == null) 
                return null!;

            using var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);

            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze();

            return image;
        }
    }
}
