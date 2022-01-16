namespace PDFiumDotNET.WpfControls.Helper
{
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts.Bitmap;

    /// <summary>
    /// Converter class converts <see cref="PixelFormat"/> to <see cref="BitmapFormat"/>.
    /// </summary>
    /// <remarks>
    /// Windows bitmap is created in <see cref="PixelFormat"/> and <see cref="BitmapFormat"/>
    /// is used to render PDF page by PDFium to the windows bitmap.
    /// </remarks>
    internal static class BitmapFormatConverter
    {
        /// <summary>
        /// Method converts <see cref="PixelFormat"/> to <see cref="BitmapFormat"/>.
        /// </summary>
        /// <param name="format">Format of windows bitmap where should be PDF content rendered.</param>
        /// <returns>Format to use in PDFium to render PDF content.</returns>
        public static BitmapFormat GetFormat(PixelFormat format)
        {
            if (format == PixelFormats.Gray8)
            {
                return BitmapFormat.BitmapGray;
            }

            if (format == PixelFormats.Bgr24)
            {
                return BitmapFormat.BitmapBGR;
            }

            if (format == PixelFormats.Bgr32)
            {
                return BitmapFormat.BitmapBGRx;
            }

            if (format == PixelFormats.Bgra32)
            {
                return BitmapFormat.BitmapBGRA;
            }

            return BitmapFormat.BitmapUnknown;
        }
    }
}
