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
            InvokePropertyChangedEvent(null);
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

            ActualPage = 0;
            WidestWidth = 0;
            HighestHeight = 0;
            CumulativeHeight = 0;
            PageCount = _mainComponent.PDFiumBridge.FPDF_GetPageCount(_mainComponent.PDFiumDocument);
            if (PageCount > 0)
            {
                ActualPage = 1;
            }

            InvokePropertyChangedEvent(nameof(ActualPage));
            InvokePropertyChangedEvent(nameof(PageCount));

            if (PageCount > 0)
            {
                CumulativeHeight = 0;
                for (var index = 0; index < PageCount; index++)
                {
                    var newPage = new PDFPage(_mainComponent, index);
                    newPage.Build();
                    Pages.Add(newPage);
                    CumulativeHeight += newPage.Height;
                    if (newPage.Width > WidestWidth)
                    {
                        WidestWidth = newPage.Width;
                    }

                    if (newPage.Height > HighestHeight)
                    {
                        HighestHeight = newPage.Height;
                    }
                }
            }

            InvokePropertyChangedEvent(nameof(Pages));
            InvokePropertyChangedEvent(nameof(WidestWidth));
            InvokePropertyChangedEvent(nameof(HighestHeight));
            InvokePropertyChangedEvent(nameof(CumulativeHeight));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpenFailed(string file)
        {
            SetDefaultValues();
            InvokePropertyChangedEvent(null);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentClosing()
        {
            SetDefaultValues();
            InvokePropertyChangedEvent(null);
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
