namespace PDFiumDotNET.Components.Layout
{
    using PDFiumDotNET.Components.Contracts.Layout;

    /// <summary>
    /// Implementation class of <see cref="IPDFLayoutComponent"/>.
    /// </summary>
    internal sealed partial class PDFLayoutComponent
    {
        #region Protected methods - overrides

        /// <inheritdoc/>
        protected override void ProcessDocumentOpening(string file)
        {
            base.ProcessDocumentOpening(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentOpened(string file)
        {
            base.ProcessDocumentOpened(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentOpenFailed(string file)
        {
            base.ProcessDocumentOpenFailed(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentClosing()
        {
            base.ProcessDocumentClosing();
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentClosed()
        {
            base.ProcessDocumentClosed();
        }

        #endregion Protected methods - overrides
    }
}
