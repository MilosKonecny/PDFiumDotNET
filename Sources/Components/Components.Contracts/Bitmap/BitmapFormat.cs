namespace PDFiumDotNET.Components.Contracts.Bitmap
{
    /// <summary>
    /// Enumeration defines format for supported formats.
    /// </summary>
    public enum BitmapFormat
    {
        /// <summary>
        /// Unknown or unsupported format.
        /// </summary>
        BitmapUnknown = 0,

        /// <summary>
        /// Gray scale bitmap, one byte per pixel.
        /// </summary>
        BitmapGray = 1,

        /// <summary>
        /// 3 bytes per pixel, byte order: blue, green, red.
        /// </summary>
        BitmapBGR = 2,

        /// <summary>
        /// 4 bytes per pixel, byte order: blue, green, red, unused.
        /// </summary>
        BitmapBGRx = 3,

        /// <summary>
        /// 4 bytes per pixel, byte order: blue, green, red, alpha.
        /// </summary>
        BitmapBGRA = 4,
    }
}
