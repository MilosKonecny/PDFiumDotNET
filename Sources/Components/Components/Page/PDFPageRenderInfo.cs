namespace PDFiumDotNET.Components.Page
{
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
        public double Left { get; set; }

        /// <inheritdoc/>
        public double Right { get; set; }

        /// <inheritdoc/>
        public double Top { get; set; }

        /// <inheritdoc/>
        public double Bottom { get; set; }

        /// <inheritdoc/>
        public bool IsOnCenter { get; set; }

        /// <inheritdoc/>
        public double PagePositionOnCenter { get; set; }

        #endregion Implementation of IPDFPageRenderInfo
    }
}
