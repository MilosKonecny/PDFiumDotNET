namespace PDFiumDotNET.Components.Page
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The class implements functionality defined by <see cref="IPDFPageComponent"/>.
    /// </summary>
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

        /// <inheritdoc/>
        protected override void InitializeComponentAfterAttachedTo()
        {
            if (MainComponent.IsDocumentOpen)
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
