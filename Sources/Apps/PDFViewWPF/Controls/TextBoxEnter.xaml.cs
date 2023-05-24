namespace PDFiumDotNET.Apps.PDFViewWPF.Controls
{
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for TextBoxEnter control.
    /// </summary>
    public partial class TextBoxEnter : TextBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxEnter"/> class.
        /// </summary>
        public TextBoxEnter()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            Dispatcher.BeginInvoke(() => SelectAll, DispatcherPriority.SystemIdle);
        }

        /// <inheritdoc/>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e != null && e.Key == Key.Enter)
            {
                BindingOperations.GetBindingExpression(this, TextBox.TextProperty).UpdateSource();
            }
        }
    }
}
