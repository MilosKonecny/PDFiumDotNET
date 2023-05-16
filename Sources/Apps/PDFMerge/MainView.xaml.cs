namespace PDFiumDotNET.Apps.PDFMerge
{
    using System;
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
            MessageBox.Show(this, message, title);
        }

        /// <inheritdoc/>
        public void ShowExceptionInfo(string title, Exception ex)
        {
            ShowMessage(title, ex?.Message);
        }

        #endregion Implementation of IView

        #region Private event handler methods

        private void HandleWindowPreviewDragEnterEvent(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void HandleWindowPreviewDragOverEvent(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void HandleWindowPreviewDragLeaveEvent(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void HandleWindowPreviewDropEvent(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ViewModel.AddFilesToList(files);
            }
        }

        #endregion Private event handler methods
    }
}
