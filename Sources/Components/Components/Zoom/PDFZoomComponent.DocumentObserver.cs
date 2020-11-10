namespace PDFiumDotNET.Components.Zoom
{
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <summary>
    /// <inheritdoc cref="IPDFZoomComponent"/>
    /// </summary>
    internal sealed partial class PDFZoomComponent
    {
        #region Implementation of IDocumentObserver

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpening(string file)
        {
            SetDefaultValues();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpened(string file)
        {
            if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
            {
                return;
            }

            SetDefaultValues();
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
            SetDefaultValues();
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
