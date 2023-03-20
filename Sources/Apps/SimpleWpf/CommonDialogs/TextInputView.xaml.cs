namespace PDFiumDotNET.Samples.SimpleWpf.CommonDialogs
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for TextInputView.xaml.
    /// </summary>
    public partial class TextInputView : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputView"/> class.
        /// </summary>
        /// <param name="viewModel">View model to use.</param>
        public TextInputView(TextInputDialog viewModel)
        {
            InitializeComponent();

            DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            if (viewModel.UsePasswordInput)
            {
                _pwdBox.Focus();
            }
            else
            {
                _textBox.Focus();
            }
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets view model.
        /// </summary>
        public TextInputDialog ViewModel => DataContext as TextInputDialog;

        #endregion Public properties

        #region Private event handler methods

        private void HandlePasswordChangedEvent(object sender, RoutedEventArgs e)
        {
            ViewModel.InputText = _pwdBox.Password;
        }

        private void HandleButtonOkClickEvent(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void HandleButtonCancelClickEvent(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion Private event handler methods
    }
}
