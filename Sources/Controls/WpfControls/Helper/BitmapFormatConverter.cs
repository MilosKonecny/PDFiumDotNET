using System.Windows.Media;
using PDFiumDotNET.Components.Contracts.Bitmap;

namespace PDFiumDotNET.WpfControls.Helper
{
    internal static class BitmapFormatConverter
    {
        public static BitmapFormat GetFormat(PixelFormat format)
        {
            if (format == PixelFormats.Gray8)
                return BitmapFormat.BitmapGray;
            if (format == PixelFormats.Bgr24)
                return BitmapFormat.BitmapBGR;
            if (format == PixelFormats.Bgr32)
                return BitmapFormat.BitmapBGRx;
            if (format == PixelFormats.Bgra32)
                return BitmapFormat.BitmapBGRA;

            return BitmapFormat.BitmapUnknown;
        }
    }
}
