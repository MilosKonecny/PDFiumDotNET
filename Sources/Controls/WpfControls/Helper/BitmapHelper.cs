namespace PDFiumDotNET.WpfControls.Helper
{
    using System.Globalization;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// The class defines extension methods for bitmap classes.
    /// </summary>
    internal static class BitmapHelper
    {
        private static string _filePathFormat1 = "C:\\GitHub\\bitmap-{0}.bmp";
        private static string _filePathFormat2 = "C:\\GitHub\\bitmap-{0}-{1}.bmp";

        /// <summary>
        /// The method saves the bitmap content into the file.
        /// </summary>
        /// <param name="bitmap">The bitmap to save.</param>
        /// <param name="pageNumber">Page number where is the bitmap from.</param>
        /// <param name="onlyPageNumber"><c>true</c> - use format 'bitmap-{pageNumber}.bmp'; otherwise 'bitmap-{pageNumber}-{nr}.bmp', where 'nr' is fixed number leading to a unique file name.</param>
        public static void Save(this WriteableBitmap bitmap, int pageNumber, bool onlyPageNumber)
        {
            var nr = 0;
            var filename = string.Format(CultureInfo.InvariantCulture, onlyPageNumber ? _filePathFormat1 : _filePathFormat2, pageNumber, nr);
            while (!onlyPageNumber && File.Exists(filename))
            {
                nr++;
                filename = string.Format(CultureInfo.InvariantCulture, onlyPageNumber ? _filePathFormat1 : _filePathFormat2, pageNumber, nr);
            }

            using (FileStream stream = new FileStream(filename, onlyPageNumber ? FileMode.Create : FileMode.CreateNew))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }
        }
    }
}
