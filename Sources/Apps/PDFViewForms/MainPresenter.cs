namespace PDFiumDotNET.Apps.PDFViewForms
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using PDFiumDotNET.Apps.PDFViewForms.Contracts;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.WinFormsControls;

    /// <summary>
    /// The presenter for <see cref="MainForm"/> and implements two presenter interfaces: <see cref="IMainPresenterForView"/> and <see cref="IMainPresenterForModel"/>.
    /// </summary>
    internal class MainPresenter : IMainPresenterForView, IMainPresenterForModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPresenter"/> class.
        /// </summary>
        public MainPresenter()
        {
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the associated <see cref="IMainModel"/>.
        /// </summary>
        public IMainModel Model
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the associated <see cref="IMainView"/>.
        /// </summary>
        public IMainView View
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Private methods

        private void RunPresenter()
        {
            if (View == null || Model == null)
            {
                // We have wait until model and view are initialized.
                return;
            }

            Model.InitializeComponents();
        }

        private void UpdateMenu()
        {
            View.EnableFileOpen(!Model.IsFileOpen);
            View.EnableFileClose(Model.IsFileOpen);
            View.EnableFileProperties(Model.IsFileOpen);

            View.EnableNavigateFirstPage(Model.IsFileOpen);
            View.EnableNavigatePreviousPage(Model.IsFileOpen);
            View.EnableNavigateNextPage(Model.IsFileOpen);
            View.EnableNavigateLastPage(Model.IsFileOpen);

            View.EnableZoomIncrease(Model.IsFileOpen);
            View.EnableZoomDecrease(Model.IsFileOpen);
            View.EnableZoomPageWidth(Model.IsFileOpen);
            View.EnableZoomPageHeight(Model.IsFileOpen);

            View.EnableViewPagesInOneColumn(Model.IsFileOpen);
            View.EnableViewPagesInTwoColumns(Model.IsFileOpen);
            View.EnableViewPagesInTwoColumnsSpecial(Model.IsFileOpen);
            View.EnableViewAnnotations(Model.IsFileOpen);
        }

        #endregion Private methods

        #region Private event handler methods

        private void HandleMainModelPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            UpdateMenu();

            if (string.IsNullOrEmpty(e.PropertyName))
            {
                View.InvalidatePDFView();
            }
            else if (string.Equals(nameof(IMainModel.PDFPageComponentForView), e.PropertyName, StringComparison.OrdinalIgnoreCase))
            {
                View.SetPDFPageComponentForView(Model.PDFPageComponentForView);
                View.InvalidatePDFView();
            }
            else if (string.Equals(nameof(IMainModel.PDFPageComponentForThumbnail), e.PropertyName, StringComparison.OrdinalIgnoreCase))
            {
                View.SetPDFPageComponentForThumbnail(Model.PDFPageComponentForThumbnail);
                View.InvalidatePDFView();
            }
        }

        #endregion Private event handler methods

        #region Implementation of IMainPresenterForView

        /// <inheritdoc/>
        public void ViewInitialized(IMainView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            RunPresenter();
        }

        /// <inheritdoc/>
        public void OpenFile(string path)
        {
            var result = Model.OpenFile(path);
            UpdateMenu();
            if (result == OpenDocumentResult.Success)
            {
                View.ClearBookmarks();
                View.PopulateBookmarks(Model.PDFPageComponentForView.MainComponent.BookmarkComponent.Bookmarks);
            }
            else
            {
                View.ShowError(Path.GetFileName(path), $"Document '{path}' not opened. Error: {result}");
            }
        }

        /// <inheritdoc/>
        public void CloseFile()
        {
            Model.CloseFile();
            UpdateMenu();
            View.ClearBookmarks();
        }

        /// <inheritdoc/>
        public void ShowInformation()
        {
            var information = Model?.PDFPageComponentForView?.MainComponent?.DocumentInformation;
            if (information != null)
            {
                View.ShowDocumentInformation(Model.FileName, information);
            }
        }

        /// <inheritdoc/>
        public void ShowAbout()
        {
            View.ShowAboutBox();
        }

        /// <inheritdoc/>
        public void IncreaseZoom(PDFControl control)
        {
            control?.PDFPageComponent?.ZoomComponent?.IncreaseZoom();
        }

        /// <inheritdoc/>
        public void DecreaseZoom(PDFControl control)
        {
            control?.PDFPageComponent?.ZoomComponent?.DecreaseZoom();
        }

        /// <inheritdoc/>
        public void BookmarkActivated(IPDFBookmark bookmark)
        {
            if (bookmark?.Destination == null)
            {
                return;
            }

            Model.PDFPageComponentForView.NavigateToDestination(bookmark.Destination);
        }

        /// <inheritdoc/>
        public void SetPageWidthZoom()
        {
            if (Model?.PDFPageComponentForView?.ZoomComponent == null
                || Model?.PDFPageComponentForView?.CurrentPage == null)
            {
                return;
            }

            Model.PDFPageComponentForView.ZoomComponent.CurrentZoomFactor
                = View.PDFActualWidth / (Model.PDFPageComponentForView.CurrentPage.Width + (2 * View.PDFPageMargin.Width));
        }

        /// <inheritdoc/>
        public void SetPageHeightZoom()
        {
            if (Model?.PDFPageComponentForView?.ZoomComponent == null
                || Model?.PDFPageComponentForView?.CurrentPage == null)
            {
                return;
            }

            Model.PDFPageComponentForView.ZoomComponent.CurrentZoomFactor
                = View.PDFActualHeight / (Model.PDFPageComponentForView.CurrentPage.Height + (2 * View.PDFPageMargin.Height));
        }

        /// <inheritdoc/>
        public void NavigateToFirstPage()
        {
            if (Model.PDFPageComponentForView == null)
            {
                return;
            }

            Model.PDFPageComponentForView.NavigateToPage(1);
        }

        /// <inheritdoc/>
        public void NavigateToPreviousPage()
        {
            if (Model.PDFPageComponentForView == null)
            {
                return;
            }

            Model.PDFPageComponentForView.NavigateToPage(Model.PDFPageComponentForView.CurrentPageIndex - 1);
        }

        /// <inheritdoc/>
        public void NavigateToNextPage()
        {
            if (Model.PDFPageComponentForView == null)
            {
                return;
            }

            Model.PDFPageComponentForView.NavigateToPage(Model.PDFPageComponentForView.CurrentPageIndex + 1);
        }

        /// <inheritdoc/>
        public void NavigateToLastPage()
        {
            if (Model.PDFPageComponentForView == null)
            {
                return;
            }

            Model.PDFPageComponentForView.NavigateToPage(Model.PDFPageComponentForView.PageCount);
        }

        /// <inheritdoc/>
        public void ViewPagesInOneColumn()
        {
            Model.ViewPagesInOneColumn();
        }

        /// <inheritdoc/>
        public void ViewPagesInTwoColumns()
        {
            Model.ViewPagesInTwoColumns();
        }

        /// <inheritdoc/>
        public void ViewPagesInTwoColumnsSpecial()
        {
            Model.ViewPagesInTwoColumnsSpecial();
        }

        /// <inheritdoc/>
        public void ShowAnnotations()
        {
            if (Model.PDFPageComponentForView == null)
            {
                return;
            }

            Model.PDFPageComponentForView.IsAnnotationToRender = !Model.PDFPageComponentForView.IsAnnotationToRender;
            View.InvalidatePDFView();
        }

        #endregion Implementation of IMainPresenterForView

        #region Implementation of IMainPresenterForModel

        /// <inheritdoc/>
        public void ModelInitialized(IMainModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Model.PropertyChanged += HandleMainModelPropertyChangedEvent;
            RunPresenter();
        }

        #endregion Implementation of IMainPresenterForModel
    }
}
