namespace PDFiumDotNET.Components.Bookmark
{
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Bookmark;

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

        #region Implementation of IBookmarkProvider

        /// <inheritdoc/>
        public ObservableCollection<IPDFBookmark> Bookmarks { get; private set; }

        #endregion Implementation of IBookmarkProvider
    }
}
