namespace PDFiumDotNET.Components.Page
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <inheritdoc cref="IPDFPageComponent"/>
    internal sealed partial class PDFPageComponent
    {
        #region Protected methods - overrides

        /// <inheritdoc/>
        protected override void ProcessDocumentOpening(string file)
        {
            SetDefaultValues();

            base.ProcessDocumentOpening(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentOpened(string file)
        {
            ScanDocument();

            base.ProcessDocumentOpened(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentOpenFailed(string file)
        {
            SetDefaultValues();

            base.ProcessDocumentOpenFailed(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentClosing()
        {
            SetDefaultValues();

            base.ProcessDocumentClosing();
        }

        #endregion Protected methods - overrides
    }
}
