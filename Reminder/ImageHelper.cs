using ImageMagick;
using System.Drawing;
using System.IO;

namespace Reminder
{
    public class ImageHelper
    {
        public static Image LoadImageAuto(byte[] data)
        {
            if (IsWebp(data))
            {
                return ConvertWebpToDisplayImage(data);
            }

            using (var ms = new MemoryStream(data))
            {
                using (var img = Image.FromStream(ms))
                {
                    return new Bitmap(img);
                }
            }
        }
        public static bool IsWebp(byte[] data)
        {
            if (data == null || data.Length < 12) return false;

            return data[0] == 'R' &&
                   data[1] == 'I' &&
                   data[2] == 'F' &&
                   data[3] == 'F' &&
                   data[8] == 'W' &&
                   data[9] == 'E' &&
                   data[10] == 'B' &&
                   data[11] == 'P';
        }
        public static Image ConvertWebpToDisplayImage(byte[] data)
        {
            using (var collection = new MagickImageCollection())
            {
                // 1. 从内存读取 WebP（支持动画）
                collection.Read(data);

                bool isAnimated = collection.Count > 1;

                using (var ms = new MemoryStream())
                {
                    if (isAnimated)
                    {
                        // ⭐ 动画 → GIF
                        collection.Write(ms, MagickFormat.Gif);
                    }
                    else
                    {
                        // ⭐ 静态 → PNG
                        using (var image = (MagickImage)collection[0])
                        {
                            image.Format = MagickFormat.Png;
                            image.Write(ms);
                        }
                    }

                    ms.Position = 0;

                    // ⭐ 关键：Clone，避免流被释放导致异常/锁问题
                    using (var tmp = Image.FromStream(ms))
                    {
                        return new Bitmap(tmp);
                    }
                }
            }
        }
    }
}
