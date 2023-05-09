namespace PDFiumDotNET.Apps.PDFMerge
{
    using System.Windows;
    using PDFiumDotNET.Apps.PDFMerge.Contracts;

    /// <summary>
    /// Interaction logic for MainView.
    /// </summary>
    public partial class MainView : Window, IView
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

        #region Implementation of IView

        /// <inheritdoc/>
        public void ShowMessage(string title, string message)
        {
            MessageBox.Show(title, message);
        }

        #endregion Implementation of IView
    }
}
