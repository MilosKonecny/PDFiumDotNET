namespace PDFiumDotNET.Apps.SimpleWpf
{
    using System.Windows;
    using PDFiumDotNET.Apps.SimpleWpf.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;

    /// <summary>
    /// The class implements simple grid window.
    /// </summary>
    public partial class MainView : IView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                ViewModel = new MainViewModel();
                ViewModel.AssignedToView(this);
                DataContext = ViewModel;
            };
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the view model of this view.
        /// </summary>
        public MainViewModel ViewModel
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Private event handler methods

        private void HandleTreeViewSelectedItemChangedEvent(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is IPDFBookmark bookmark)
            {
                ViewModel.NavigateTo(bookmark);
            }
        }

        #endregion Private event handler methods

        #region Implementation of IView

        /// <summary>
        /// Gets the view <see cref="Window"/>.
        /// </summary>
        public Window Window => this;

        /// <summary>
        /// Gets the actual width of control, where are the PDF pages rendered.
        /// </summary>
        public double PDFActualWidth => _pdfView.ActualWidth;

        /// <summary>
        /// Gets the actual height of control, where are the PDF pages rendered.
        /// </summary>
        public double PDFActualHeight => _pdfView.ActualHeight;

        /// <summary>
        /// Gets the margin between pages.
        /// </summary>
        public Size PDFPageMargin => _pdfView.PDFPageMargin;

        /// <summary>
        /// Invalidates PDF control.
        /// </summary>
        public void InvalidatePDFControl()
        {
            _pdfView.InvalidateVisual();
            _pdfThumbnailView.InvalidateVisual();
        }

        #endregion Implementation of IView

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (ViewModel == null)
            {
                return;
            }

            ViewModel.SelectedFindObject = e?.NewValue;
        }
    }
}
