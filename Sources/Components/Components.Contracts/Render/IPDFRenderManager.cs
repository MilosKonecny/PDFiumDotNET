namespace PDFiumDotNET.Components.Contracts.Render
{
    using System;
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The interface defines the functionality to provide all the necessary information to draw a PDF document
    /// in a specific <see cref="PageLayoutType"/> for which the <see cref="IPDFPageComponent"/> was created.
    /// </summary>
    public interface IPDFRenderManager
    {
        /// <summary>
        /// Gets or sets the margins between pages.
        /// </summary>
        PDFSize<double> PageMargin { get; set; }

        /// <summary>
        /// Gets the size of the document area in which all pages are rendered.
        /// </summary>
        PDFSize<double> DocumentArea { get; }

        /// <summary>
        /// The method determines where is particular page positioned.
        /// </summary>
        /// <param name="pageIndex">The index of the page for which the position is to be determined.</param>
        /// <returns>Position of the page.</returns>
        /// <remarks><paramref name="pageIndex"/> should be between 0 (inclusive) and <see cref="IPDFPageComponent.PageCount"/>.
        /// If this condition is not met, the returned rectangle has zero size.</remarks>
        PDFRectangle<double> PagePosition(int pageIndex);

        /// <summary>
        /// The method returns all pages that lie at least partially in the defined viewport area.
        /// </summary>
        /// <param name="viewportArea">Viewport area of document area to draw.</param>
        /// <returns>Render information contains all pages to render and viewport area where the pages lie.</returns>
        IList<IPDFPageRenderInfo> PagesToRender(PDFRectangle<double> viewportArea);

        /// <summary>
        /// Occurs when a document area changes.
        /// </summary>
        event EventHandler<EventArgs> DocumentAreaChanged;
    }
}
