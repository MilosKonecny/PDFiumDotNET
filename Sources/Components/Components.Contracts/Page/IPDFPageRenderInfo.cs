namespace PDFiumDotNET.Components.Contracts.Page
{
    using PDFiumDotNET.Components.Contracts.Basic;

    /// <summary>
    /// Interface defines information about page to render.
    /// </summary>
    public interface IPDFPageRenderInfo
    {
        /// <summary>
        /// Gets the page estimated to render.
        /// </summary>
        IPDFPage Page { get; }

        /// <summary>
        /// Gets or sets the position of page in document area.
        /// </summary>
        PDFRectangle<double> PositionInDocumentArea { get; }

        /// <summary>
        /// Gets or sets the position of page in viewport area.
        /// Rectangle values are relative to the left-top point of viewport area.
        /// </summary>
        PDFRectangle<double> RelativePositionInViewportArea { get; }

        /// <summary>
        /// Gets the visible part of page in viewport area.
        /// </summary>
        PDFRectangle<double> VisiblePart { get; }

        /// <summary>
        /// Gets the position of visible part of page in viewport area.
        /// </summary>
        PDFRectangle<double> VisiblePartInViewportArea { get; }

        /// <summary>
        /// Gets or sets the value indicating that this page is closest to the center of the viewport.
        /// </summary>
        bool IsClosestToCenter { get; }

        /// <summary>
        /// Gets the row index where is the page rendered.
        /// First page is in the row 0.
        /// </summary>
        int PageRow { get; }

        /// <summary>
        /// Gets the column index where is the page rendered.
        /// First page in the row is in the column 0.
        /// </summary>
        int PageColumn { get; }
    }
}
