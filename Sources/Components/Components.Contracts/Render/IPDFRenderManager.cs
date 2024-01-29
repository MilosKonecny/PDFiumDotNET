namespace PDFiumDotNET.Components.Contracts.Render
{
    using System;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <summary>
    /// The interface defines the functionality to provide all the necessary information to draw a PDF document
    /// in a specific <see cref="PageLayoutType"/> for which the <see cref="IPDFPageComponent"/> was created.
    /// </summary>
    public interface IPDFRenderManager
    {
        /// <summary>
        /// Gets or sets the margins between pages.
        /// </summary>
        /// <remarks>This property should be set from implementation of <see cref="IPDFPageComponent"/>.</remarks>
        PDFSize<double> PageMargin { get; set; }

        /// <summary>
        /// Gets the size of the document area in which all pages are rendered.
        /// Computed document area uses actual zoom factor. Zoom factor is obtained from <see cref="IPDFZoomComponent"/>.
        /// </summary>
        PDFSize<double> DocumentArea { get; }

        /// <summary>
        /// Gets the widest row of pages. Count of pages in row depends on layout - one page in row, or two pages in row.
        /// Determined width doesn't use actual zoom factor.
        /// </summary>
        double WidestPageRow { get; }

        /// <summary>
        /// Gets the highest row of pages. Count of pages in row depends on layout - one page in row, or two pages in row.
        /// Determined height doesn't use actual zoom factor.
        /// </summary>
        double HighestPageRow { get; }

        /// <summary>
        /// The method determines where is particular page positioned.
        /// </summary>
        /// <param name="pageIndex">The index of the page for which the position is to be determined.</param>
        /// <returns>Position of the page.</returns>
        /// <remarks><paramref name="pageIndex"/> should be between 0 (inclusive) and <see cref="IPDFPageComponent.PageCount"/>.
        /// If this condition is not met, the returned rectangle has zero size.</remarks>
        PDFRectangle<double> DeterminePagePosition(int pageIndex);

        /// <summary>
        /// The method determines all necessary information to render current content of PDF document.
        /// </summary>
        /// <param name="viewportArea">Viewport area of document area to draw.</param>
        /// <returns>Render information contains all pages to render and viewport area where the pages lie.</returns>
        IPDFRenderInfo DetermineRenderInfo(PDFRectangle<double> viewportArea);

        /// <summary>
        /// The method determines new horizontal offset to draw the page on center on the same position after zoom will change.
        /// </summary>
        /// <param name="renderInfo">Last render information used to render current content of PDF document.</param>
        /// <param name="newZoomFactor">New zoom to use for computing of new horizontal offset.</param>
        /// <returns>New horizontal offset.</returns>
        double DetermineHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor);

        /// <summary>
        /// The method determines new vertical offset to draw the page on center on the same position after zoom will change.
        /// </summary>
        /// <param name="renderInfo">Last render information used to render current content of PDF document.</param>
        /// <param name="newZoomFactor">New zoom to use for computing of new vertical offset.</param>
        /// <returns>New vertical offset.</returns>
        double DetermineVerticalOffset(IPDFRenderInfo renderInfo, double newZoomFactor);

        /// <summary>
        /// Occurs when a document area changes.
        /// </summary>
        event EventHandler<EventArgs> DocumentAreaChanged;
    }
}
