namespace PDFiumDotNET.Components.Page
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// <inheritdoc cref="IPDFPageRenderInfo"/>
    /// </summary>
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFPage Page { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Bottom { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsOnCenter { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double PagePositionOnCenter { get; set; }

        #endregion Implementation of IPDFPageRenderInfo
    }
}
