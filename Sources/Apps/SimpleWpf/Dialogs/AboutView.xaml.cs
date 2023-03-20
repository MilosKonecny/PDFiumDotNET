namespace PDFiumDotNET.Samples.SimpleWpf.Dialogs
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Navigation;

    /// <summary>
    /// The class implements simple grid window.
    /// </summary>
    public partial class AboutView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutView"/> class.
        /// </summary>
        public AboutView()
        {
            InitializeComponent();
            ViewModel = new AboutViewModel();
            DataContext = ViewModel;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the view model of this view.
        /// </summary>
        public AboutViewModel ViewModel
        {
            get;
        }

        #endregion Public properties

        #region Private event handler methods

        private void HandleCloseButtonClickEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HandleHyperlinkRequestNavigateEvent(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        }

        #endregion Private event handler methods
    }
}
