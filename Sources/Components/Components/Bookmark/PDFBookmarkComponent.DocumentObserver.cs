namespace PDFiumDotNET.Components.Bookmark
{
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// <inheritdoc cref="IPDFBookmarkComponent"/>
    /// </summary>
    internal sealed partial class PDFBookmarkComponent
    {
        #region Implementation of IDocumentObserver

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DocumentOpening(string file)
        {
            Bookmarks.Clear();
            InvokePropertyChangedEvent(nameof(Bookmarks));
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

            var bookmarkHandle = _mainComponent.PDFiumBridge.FPDFBookmark_GetFirstChild(_mainComponent.PDFiumDocument, FPDF_BOOKMARK.InvalidHandle);
            while (bookmarkHandle.IsValid)
            {
                var newBookmark = new PDFBookmark(_mainComponent, bookmarkHandle);
                newBookmark.Build();
                Bookmarks.Add(newBookmark);
                bookmarkHandle = _mainComponent.PDFiumBridge.FPDFBookmark_GetNextSibling(_mainComponent.PDFiumDocument, bookmarkHandle);
            }

            InvokePropertyChangedEvent(nameof(Bookmarks));
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
            Bookmarks.Clear();
            InvokePropertyChangedEvent(nameof(Bookmarks));
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
