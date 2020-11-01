namespace PDFiumDotNET.Components.Contracts.Bookmark
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interface defines functionality of bookmark component.
    /// </summary>
    public interface IPDFBookmarkComponent : IPDFChildComponent
    {
        /// <summary>
        /// Gets the top bookmarks of opened document.
        /// </summary>
        ObservableCollection<IPDFBookmark> Bookmarks { get; }
    }
}
