namespace PDFiumDotNET.Samples.SimpleWpf
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Samples.SimpleWpf.Contracts;

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
        private void HandleTextBoxGotFocusEvent(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => (sender as TextBox)?.SelectAll()));
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
        public double PDFPageMargin => _pdfView.PDFPageMargin;

        #endregion Implementation of IView

    }
}
