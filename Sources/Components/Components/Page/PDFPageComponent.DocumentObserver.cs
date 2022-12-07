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
            if (PDFComponent.PDFiumBridge != null && PDFComponent.PDFiumDocument.IsValid)
            {
                PageCount = PDFComponent.PDFiumBridge.FPDF_GetPageCount(PDFComponent.PDFiumDocument);
                _standardPageLayout.SetDefaultValues();
                _thumbnailPageLayout.SetDefaultValues();
                if (PageCount > 0)
                {
                    for (var index = 0; index < PageCount; index++)
                    {
                        var newPage = new PDFPage(PDFComponent, index);
                        newPage.Build();
                        Pages.Add(newPage);
                    }

                    _standardPageLayout.InitializeLayout();
                    _thumbnailPageLayout.InitializeLayout();
                    SetCurrentInformation(Pages[0]);
                }

                InvokePropertyChangedEvent(null);
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
