namespace PDFiumDotNET.Components.Page
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// <inheritdoc cref="IPDFPageComponent"/>
    /// </summary>
    internal sealed partial class PDFPageComponent
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

            PageCount = _mainComponent.PDFiumBridge.FPDF_GetPageCount(_mainComponent.PDFiumDocument);
            _standardPageLayout.SetDefaultValues();
            _thumbnailPageLayout.SetDefaultValues();
            if (PageCount > 0)
            {
                CurrentPageIndex = 1;
                for (var index = 0; index < PageCount; index++)
                {
                    var newPage = new PDFPage(_mainComponent, index);
                    newPage.Build();
                    Pages.Add(newPage);
                }

                _standardPageLayout.InitializeLayout();
                _thumbnailPageLayout.InitializeLayout();
            }

            InvokePropertyChangedEvent(null);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpenFailed(string file)
        {
            SetDefaultValues();
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
