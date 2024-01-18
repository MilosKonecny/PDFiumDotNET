namespace PDFiumDotNET.Components.Annotation
{
    using PDFiumDotNET.Components.Contracts.Annotation;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The class implements functionality defined by <see cref="IPDFPageAnnotations"/>.
    /// </summary>
    internal class PDFPageAnnotations : IPDFPageAnnotations
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPageAnnotations"/> class.
        /// </summary>
        public PDFPageAnnotations(IPDFPage page, int count)
        {
            RelatedPage = page;
            Count = count;
        }

        #endregion Constructors

        #region Implementation of IPDFPageAnnotations

        /// <inheritdoc/>
        public IPDFPage RelatedPage { get; private set; }

        /// <inheritdoc/>
        public int Count { get; private set; }

        #endregion Implementation of IPDFPageAnnotations
    }
}
