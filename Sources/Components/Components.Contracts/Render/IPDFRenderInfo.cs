namespace PDFiumDotNET.Components.Contracts.Render
{
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Interface defines object containing all necessary information to render current content of PDF document.
    /// </summary>
    public interface IPDFRenderInfo
    {
        /// <summary>
        /// Gets the zoom factor used to gather the render information.
        /// </summary>
        double ZoomFactor { get; }

        /// <summary>
        /// Viewport area for which was render information gathered.
        /// </summary>
        PDFRectangle<double> ViewportArea { get; }

        /// <summary>
        /// Gets all pages that lie at least partially in the defined viewport area.
        /// </summary>
        IEnumerable<IPDFPageRenderInfo> PagesToRender { get; }
    }
}
