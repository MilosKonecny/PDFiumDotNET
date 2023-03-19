namespace PDFiumDotNET.Components.Bookmark
{
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFBookmarkComponent"/>
    internal sealed partial class PDFBookmarkComponent : PDFChildComponent, IPDFBookmarkComponent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFBookmarkComponent"/> class.
        /// </summary>
        public PDFBookmarkComponent()
        {
            Bookmarks = new ObservableCollection<IPDFBookmark>();
        }

        #endregion Constructors

        #region Private methods

        private void SetDefaultValues()
        {
            Bookmarks.Clear();
            InvokePropertyChangedEvent(nameof(Bookmarks));
        }

        private void ScanDocument()
        {
            if (PDFComponent.PDFiumBridge == null || !PDFComponent.PDFiumDocument.IsValid)
            {
                SetDefaultValues();
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
        }

        #endregion Private methods

        #region Implementation of IBookmarkProvider

        /// <inheritdoc/>
        public ObservableCollection<IPDFBookmark> Bookmarks { get; private set; }

        #endregion Implementation of IBookmarkProvider
    }
}
