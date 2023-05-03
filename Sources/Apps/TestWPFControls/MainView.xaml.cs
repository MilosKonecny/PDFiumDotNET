namespace PDFiumDotNET.Apps.TestWPFControls
{
    using System.Windows;
    using PDFiumDotNET.Apps.TestWPFControls.Contracts;
    using PDFiumDotNET.WpfControls;

    /// <summary>
    /// Interaction logic for MainView.
    /// </summary>
    public partial class MainView : Window, IView
    {
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

        #region Implementation of IView

        /// <inheritdoc/>
        public PDFView PDFViewControl => _pdfView;

        #endregion Implementation of IView
    }
}
