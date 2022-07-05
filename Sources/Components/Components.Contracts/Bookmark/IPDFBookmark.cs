namespace PDFiumDotNET.Components.Contracts.Bookmark
{
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;

    /// <summary>
    /// Interface defines functionality of bookmark.
    /// </summary>
    public interface IPDFBookmark
    {
        /// <summary>
        /// Gets associated action of bookmark.
        /// </summary>
        IPDFAction Action { get; }

        /// <summary>
        /// Gets the observable collection of child bookmarks on this bookmark level.
        /// </summary>
        ObservableCollection<IPDFBookmark> Bookmarks { get; }

        /// <summary>
        /// Gets the flag whether the bookmark is in opened state - child bookmarks are visible.
        /// </summary>
        bool IsOpened { get; }

        /// <summary>
        /// Gets associated destionation of bookmark.
        /// </summary>
        IPDFDestination Destination { get; }

        /// <summary>
        /// Gets the text of bookmark.
        /// </summary>
        string Text { get; }
    }
}
