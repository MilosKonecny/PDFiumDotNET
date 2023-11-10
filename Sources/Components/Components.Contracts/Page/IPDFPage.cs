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
        /// Gets page width. Original value is defined in PDF document,
        /// but this value may be transformed. For example for thumbnail.
        /// </summary>
        /// <remarks>Check the Adobe Acrobat Reader and transformation of thumbnails.</remarks>
        double Width { get; }

        /// <summary>
        /// Gets page height. Original value is defined in PDF document,
        /// but this value may be transformed. For example for thumbnail.
        /// </summary>
        /// <remarks>Check the Adobe Acrobat Reader and transformation of thumbnails.</remarks>
        double Height { get; }

        /// <summary>
        /// Gets the transformation zoom if any transformation was applied.
        /// Return value is 1 if there is no transformation.
        /// </summary>
        double TransformationZoom { get; }

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
        /// <param name="zoomFactor">Zoom factor to use for transformation.</param>
        /// <param name="startX">Left pixel position of the page area to be rendered.</param>
        /// <param name="startY">Top pixel position of the page area to be rendered.</param>
        /// <param name="sizeX">Width of the page area to be rendered.</param>
        /// <param name="sizeY">Height of the page area to be rendered.</param>
        /// <param name="width">Width of bitmap to draw into.</param>
        /// <param name="height">Height of bitmap to draw into.</param>
        /// <param name="format">Pixel format for created bitmap.</param>
        /// <param name="buffer">Buffer for created bitmap.</param>
        /// <param name="stride">Stride of created buffer.</param>
        void RenderPageBitmap(double zoomFactor, int startX, int startY, int sizeX, int sizeY, int width, int height, BitmapFormat format, IntPtr buffer, int stride);

        /// <summary>
        /// Creates bitmap of whole page.
        /// </summary>
        /// <param name="format">Pixel format for created bitmap.</param>
        /// <param name="buffer">Buffer for created bitmap.</param>
        /// <param name="stride">Stride of created buffer.</param>
        void RenderWholePageBitmap(BitmapFormat format, IntPtr buffer, int stride);

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
