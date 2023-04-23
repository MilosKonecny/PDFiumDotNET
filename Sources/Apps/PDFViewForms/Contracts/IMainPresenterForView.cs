namespace PDFiumDotNET.Apps.PDFViewForms.Contracts
{
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.WinFormsControls;

    /// <summary>
    /// The interface defines presenter functionality used by view.
    /// </summary>
    internal interface IMainPresenterForView
    {
        /// <summary>
        /// The method is called by the view at the moment the view is initialized and can be used in presenter.
        /// </summary>
        /// <param name="view">Initialized view to use in presenter.</param>
        void ViewInitialized(IMainView view);

        /// <summary>
        /// The method opens the specified PDF file.
        /// </summary>
        /// <param name="path">File to open.</param>
        void OpenFile(string path);

        /// <summary>
        /// The method closes the actual open PDF file.
        /// </summary>
        void CloseFile();

        /// <summary>
        /// The method shows information about the open file.
        /// </summary>
        void ShowInformation();

        /// <summary>
        /// The method shows about information.
        /// </summary>
        void ShowAbout();

        /// <summary>
        /// The method increases zoom in opened PDF file.
        /// </summary>
        /// <param name="control">The control where is required to increase zoom.</param>
        void IncreaseZoom(PDFControl control);

        /// <summary>
        /// The method decreases zoom in opened PDF file.
        /// </summary>
        /// <param name="control">The control where is required to decrease zoom.</param>
        void DecreaseZoom(PDFControl control);

        /// <summary>
        /// The method performs action when a bookmark was activate in GUI.
        /// </summary>
        /// <param name="bookmark">The bookmark that has been activated and whose action is to be performed.</param>
        void BookmarkActivated(IPDFBookmark bookmark);

        /// <summary>
        /// The method changes the zoom so that the page width fits in the viewport.
        /// </summary>
        void SetPageWidthZoom();

        /// <summary>
        /// The method changes the zoom so that the page height fits in the viewport.
        /// </summary>
        void SetPageHeightZoom();

        /// <summary>
        /// The method navigates to the first page of the PDF document.
        /// </summary>
        void NavigateToFirstPage();

        /// <summary>
        /// The method navigates to the previous page of the PDF document.
        /// </summary>
        void NavigateToPreviousPage();

        /// <summary>
        /// The method navigates to the next page of the PDF document.
        /// </summary>
        void NavigateToNextPage();

        /// <summary>
        /// The method navigates to the last page of the PDF document.
        /// </summary>
        void NavigateToLastPage();

        /// <summary>
        /// The method changes the layout of pages to one column.
        /// </summary>
        void ViewPagesInOneColumn();

        /// <summary>
        /// The method changes the layout of pages to two columns.
        /// </summary>
        void ViewPagesInTwoColumns();

        /// <summary>
        /// The method changes the layout of pages to two columns, where in the first row is only one page.
        /// </summary>
        void ViewPagesInTwoColumnsSpecial();

        /// <summary>
        /// The method shows annotations of PDF document.
        /// </summary>
        void ShowAnnotations();
    }
}
