namespace PDFiumDotNET.Components.Bookmark
{
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFBookmarkComponent"/>
    internal sealed partial class PDFBookmarkComponent
    {
        #region Protected methods - overrides

        /// <inheritdoc/>
        protected override void ProcessDocumentOpening(string file)
        {
            Bookmarks.Clear();
            InvokePropertyChangedEvent(nameof(Bookmarks));

            base.ProcessDocumentOpening(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentOpened(string file)
        {
            if (PDFComponent.PDFiumBridge == null || !PDFComponent.PDFiumDocument.IsValid)
            {
                return;
            }

            var bookmarkHandle = PDFComponent.PDFiumBridge.FPDFBookmark_GetFirstChild(PDFComponent.PDFiumDocument, FPDF_BOOKMARK.InvalidHandle);
            while (bookmarkHandle.IsValid)
            {
                var newBookmark = new PDFBookmark(PDFComponent, bookmarkHandle);
                newBookmark.Build();
                Bookmarks.Add(newBookmark);
                bookmarkHandle = PDFComponent.PDFiumBridge.FPDFBookmark_GetNextSibling(PDFComponent.PDFiumDocument, bookmarkHandle);
            }

            InvokePropertyChangedEvent(nameof(Bookmarks));

            base.ProcessDocumentOpened(file);
        }

        /// <inheritdoc/>
        protected override void ProcessDocumentClosing()
        {
            Bookmarks.Clear();
            InvokePropertyChangedEvent(nameof(Bookmarks));

            base.ProcessDocumentClosing();
        }

        #endregion Protected methods - overrides
    }
}
