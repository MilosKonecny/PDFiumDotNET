namespace PDFiumDotNET.Components.Contracts.Page
{
    using System;
    using PDFiumDotNET.Components.Contracts.Bitmap;
    using PDFiumDotNET.Components.Contracts.Link;

    /// <summary>
    /// Interface defines properties and functionality of page in PDF document.
    /// </summary>
    public interface IPDFPage
    {
        /// <summary>
        /// Gets page height defined in PDF document.
        /// </summary>
        double Height { get; }

        /// <summary>
        /// Gets page width defined in PDF document.
        /// </summary>
        double Width { get; }

        /// <summary>
        /// Gets predefined thumbnail height.
        /// </summary>
        double ThumbnailHeight { get; }

        /// <summary>
        /// Gets predefined thumbnail width.
        /// </summary>
        double ThumbnailWidth { get; }

        /// <summary>
        /// Gets page label defined in PDF document.
        /// </summary>
        string PageLabel { get; }

        /// <summary>
        /// Gets page index. First page has index 0.
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Creates page bitmap of page. Usable to get the scaled page.
        /// </summary>
        /// <param name="width">Width of bitmap to draw into.</param>
        /// <param name="height">Height of bitmap to draw into.</param>
        /// <param name="format">Pixel format for created bitmap.</param>
        /// <param name="buffer">Buffer for created bitmap.</param>
        /// <param name="stride">Stride of created buffer.</param>
        /// <returns>Create bitmap with thumbnail of page.</returns>
        IPDFBitmap CreatePageBitmap(int width, int height, BitmapFormat format, IntPtr buffer, int stride);

        /// <summary>
        /// Creates page bitmap of page. Width and height is used as defined in document.
        /// </summary>
        /// <param name="format">Pixel format for created bitmap.</param>
        /// <param name="buffer">Buffer for created bitmap.</param>
        /// <param name="stride">Stride of created buffer.</param>
        /// <returns>Create bitmap with thumbnail of page.</returns>
        IPDFBitmap CreatePageBitmap(BitmapFormat format, IntPtr buffer, int stride);

        /// <summary>
        /// Creates thumbnail bitmap of page.
        /// </summary>
        /// <param name="format">Pixel format for created bitmap.</param>
        /// <param name="buffer">Buffer for created bitmap.</param>
        /// <param name="stride">Stride of created buffer.</param>
        /// <returns>Create bitmap with thumbnail of page.</returns>
        IPDFBitmap CreateThumbnailBitmap(BitmapFormat format, IntPtr buffer, int stride);

        /// <summary>
        /// Gets the link on specified position.
        /// </summary>
        /// <param name="x">X position where to get the link from.</param>
        /// <param name="y">Y position where to get the link from.</param>
        /// <returns>Link on specified position. <c>null</c> if there don't exists any link.</returns>
        /// <remarks>Warning. Point [0,0] is at the bottom left.
        /// Therefore you need to transform screen coordinates where the point [0,0] is top left.</remarks>
        IPDFLink GetLinkFromPoint(double x, double y);

        /// <summary>
        /// Navigates to this page.
        /// </summary>
        void NavigteTo();
    }
}
