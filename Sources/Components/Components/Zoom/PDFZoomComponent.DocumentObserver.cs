namespace PDFiumDotNET.Components.Zoom
{
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <inheritdoc cref="IPDFZoomComponent"/>
    internal sealed partial class PDFZoomComponent
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
            if (PDFComponent.PDFiumBridge != null && PDFComponent.PDFiumDocument.IsValid)
            {
                SetDefaultValues();
            }

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
