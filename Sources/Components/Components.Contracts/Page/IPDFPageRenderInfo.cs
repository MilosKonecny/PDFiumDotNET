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
        /// Gets or sets the value indicating that this side is closest to the center of the viewport.
        /// </summary>
        bool IsNearestToCenter { get; }
    }
}
