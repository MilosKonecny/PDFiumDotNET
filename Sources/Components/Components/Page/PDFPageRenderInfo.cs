namespace PDFiumDotNET.Components.Page
{
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <inheritdoc cref="IPDFPageRenderInfo"/>
    internal class PDFPageRenderInfo : IPDFPageRenderInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPageRenderInfo"/> class.
        /// </summary>
        /// <param name="page">Page to render.</param>
        public PDFPageRenderInfo(IPDFPage page)
        {
            Page = page;
        }

        #endregion Constructors

        #region Implementation of IPDFPageRenderInfo

        /// <inheritdoc/>
        public IPDFPage Page { get; private set; }

        /// <inheritdoc/>
        public PDFRectangle<double> PositionInDocumentArea { get; internal set; }

        /// <inheritdoc/>
        public PDFRectangle<double> RelativePositionInViewportArea { get; internal set; }

        /// <inheritdoc/>
        public PDFRectangle<double> VisiblePart { get; internal set; }

        /// <inheritdoc/>
        public PDFRectangle<double> VisiblePartInViewportArea { get; internal set; }

        /// <inheritdoc/>
        public bool IsClosestToCenter { get; internal set; }

        /// <inheritdoc/>
        public int PageRow { get; internal set; }

        /// <inheritdoc/>
        public int PageColumn { get; internal set; }

        #endregion Implementation of IPDFPageRenderInfo
    }
}
