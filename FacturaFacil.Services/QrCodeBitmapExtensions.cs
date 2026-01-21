using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace Net.Codecrete.QrCodeGenerator
{
    public static class QrCodeBitmapExtensions
    {
        public static Image ToBitmap(this QrCode qrCode, int scale, int border, Color foreground, Color background)
        {
            if (scale <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(scale), "Value out of range");
            }
            if (border < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(border), "Value out of range");
            }

            int size = qrCode.Size;
            int dim = (size + border * 2) * scale;

            var image = new Image<Rgb24>(dim, dim);
            image.Mutate(img =>
            {
                img.Fill(background);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (qrCode.GetModule(x, y))
                        {
                            img.Fill(foreground, new Rectangle((x + border) * scale, (y + border) * scale, scale, scale));
                        }
                    }
                }
            });

            return image;
        }

        public static byte[] ToPng(this QrCode qrCode, int scale, int border)
        {
            using (var stream = new MemoryStream())
            {
                var image = ToBitmap(qrCode, scale, border, Color.Black, Color.White);
                image.SaveAsPng(stream);
                return stream.ToArray();
            }
        }
    }
}
