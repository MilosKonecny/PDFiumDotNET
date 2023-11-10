namespace PDFiumDotNET.Components.Transformation
{
    using System;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// Base Class transforms page size.
    /// In this moment is used only one type of transformation ant this transformation is used for thumbnail.
    /// </summary>
    /// <remarks>
    /// Transformation:
    /// ┌        ┐   ┌      ┐         ┌            ┐
    /// │ width  │   │ k  0 │         │ new width  │
    /// │        │ * │      │ * 200 = │            │
    /// │ height │   │ 0  k │         │ new height │
    /// └        ┘   └      ┘         └            ┘
    /// where k = 1 / Math.Max(width, height).
    /// </remarks>
    internal class PageSizeThumbnailTransformation : IPageSizeTransformation
    {
        #region Implementation of IPageSizeTransformation

        /// <inheritdoc/>
        public double Width(PDFPage page)
        {
            return (page.OriginalWidth * 200d) / Math.Max(page.OriginalWidth, page.OriginalHeight);
        }

        /// <inheritdoc/>
        public double Height(PDFPage page)
        {
            return (page.OriginalHeight * 200d) / Math.Max(page.OriginalWidth, page.OriginalHeight);
        }

        /// <inheritdoc/>
        public double TransformationZoom(PDFPage page)
        {
            var withZoom = Width(page) / page.OriginalWidth;
            var heightZoom = Height(page) / page.OriginalHeight;
            return Math.Max(withZoom, heightZoom);
        }

        #endregion Implementation of IPageSizeTransformation
    }
}
