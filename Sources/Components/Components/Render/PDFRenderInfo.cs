namespace PDFiumDotNET.Components.Render
{
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;

    /// <summary>
    /// This class contains all necessary information to render current content of PDF document.
    /// </summary>
    public class PDFRenderInfo : IPDFRenderInfo
    {
        #region Implementation of IPDFRenderInfo

        /// <inheritdoc/>
        public double ZoomFactor { get; internal set; }

        /// <inheritdoc/>
        public PDFRectangle<double> ViewportArea { get; internal set; }

        /// <inheritdoc/>
        public IEnumerable<IPDFPageRenderInfo> PagesToRender { get; internal set; }

        #endregion Implementation of IPDFRenderInfo
    }
}
