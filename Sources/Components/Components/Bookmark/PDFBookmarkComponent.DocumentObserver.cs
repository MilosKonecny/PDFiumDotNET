namespace PDFiumDotNET.Components.Bookmark
{
    using PDFiumDotNET.Components.Contracts.Bookmark;

    /// <inheritdoc cref="IPDFBookmarkComponent"/>
    internal sealed partial class PDFBookmarkComponent
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
        protected override void ProcessDocumentClosing()
        {
            SetDefaultValues();
            base.ProcessDocumentClosing();
        }

        /// <inheritdoc/>
        protected override void InitializeComponentAfterAttachedTo()
        {
            if (MainComponent.IsDocumentOpened)
            {
                ScanDocument();
            }
            else
            {
                SetDefaultValues();
            }

            base.InitializeComponentAfterAttachedTo();
        }

        #endregion Protected methods - overrides
    }
}
