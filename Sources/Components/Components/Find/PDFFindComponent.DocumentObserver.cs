namespace PDFiumDotNET.Components.Find
{
    using PDFiumDotNET.Components.Contracts.Find;

    /// <summary>
    /// Implemeents <see cref="IPDFFindComponent"/>.
    /// </summary>
    internal sealed partial class PDFFindComponent
    {
        #region Implementation of IDocumentObserver

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpening(string file)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpened(string file)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpenFailed(string file)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentClosing()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentClosed()
        {
        }

        #endregion Implementation of IDocumentObserver
    }
}
