namespace PDFiumDotNET.Apps.PDFViewForms.Contracts
{
    using System.Collections.ObjectModel;
    using System.Drawing;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Information;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The interface defines view functionality used by presenter.
    /// </summary>
    internal interface IMainView
    {
        /// <summary>
        /// Gets the actual width of the control where the PDF pages are rendered.
        /// </summary>
        int PDFActualWidth { get; }

        /// <summary>
        /// Gets the actual height of the control where the PDF pages are rendered.
        /// </summary>
        int PDFActualHeight { get; }

        /// <summary>
        /// Gets the margin between pages.
        /// </summary>
        Size PDFPageMargin { get; }

        /// <summary>
        /// The method invalidates PDF view.
        /// </summary>
        void InvalidatePDFView();

        /// <summary>
        /// The method displays error message.
        /// </summary>
        /// <param name="title">Text to use for dialog.</param>
        /// <param name="error">Error message to display.</param>
        void ShowError(string title, string error);

        /// <summary>
        /// The method shows PDF document information.
        /// </summary>
        /// <param name="title">Text to use for dialog.</param>
        /// <param name="information">PDF document information to show.</param>
        void ShowDocumentInformation(string title, IPDFInformation information);

        /// <summary>
        /// The method shows About Box.
        /// </summary>
        void ShowAboutBox();

        /// <summary>
        /// The method sets the <see cref="IPDFPageComponent"/> for standard view.
        /// </summary>
        /// <param name="pageComponent"><see cref="IPDFPageComponent"/> to use.</param>
        void SetPDFPageComponentForView(IPDFPageComponent pageComponent);

        /// <summary>
        /// The method sets the <see cref="IPDFPageComponent"/> for standard view.
        /// </summary>
        /// <param name="pageComponent"><see cref="IPDFPageComponent"/> to use.</param>
        void SetPDFPageComponentForThumbnail(IPDFPageComponent pageComponent);

        /// <summary>
        /// The method enables or disables menu item 'file / open'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableFileOpen(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'file / close'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableFileClose(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'file / properties'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableFileProperties(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'navigate / first page'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableNavigateFirstPage(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'navigate / previous page'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableNavigatePreviousPage(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'navigate / next page'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableNavigateNextPage(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'navigate / last page'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableNavigateLastPage(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'zoom / increase zoom'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableZoomIncrease(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'zoom / decrease zoom'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableZoomDecrease(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'zoom / page width'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableZoomPageWidth(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'zoom / page height'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableZoomPageHeight(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'view / one column'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableViewPagesInOneColumn(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'view / two columns'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableViewPagesInTwoColumns(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'view / two columns special'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableViewPagesInTwoColumnsSpecial(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'view / annotations'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableViewAnnotations(bool enable);

        /// <summary>
        /// The method removes all items from bookmark tree view.
        /// </summary>
        void ClearBookmarks();

        /// <summary>
        /// The method populates tree view with bookmarks.
        /// </summary>
        /// <param name="bookmarks">Bookmarks collection to show in tree view.</param>
        void PopulateBookmarks(ObservableCollection<IPDFBookmark> bookmarks);
    }
}
