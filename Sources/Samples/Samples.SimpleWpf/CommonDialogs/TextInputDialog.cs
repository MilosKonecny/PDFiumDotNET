using System.Windows;

namespace PDFiumDotNET.Samples.SimpleWpf.CommonDialogs
{
    /// <summary>
    /// Class implements text input dialog.
    /// </summary>
    public sealed class TextInputDialog
    {
        /// <summary>
        /// Gets or sets the dialog title.
        /// </summary>
        public string InputDialogTitle { get; set; }

        /// <summary>
        /// Gets or sets the hint shown above input text box.
        /// </summary>
        public string InputTextHint { get; set; }

        /// <summary>
        /// Gets the input text or sets the predefined input text.
        /// </summary>
        public string InputText { get; set; }

        /// <summary>
        /// Gets or sets the value telling whether the password input should be used.
        /// </summary>
        public bool UsePasswordInput { get; set; }

        /// <summary>
        /// Shows the text input dialog.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <returns><c>true</c> in case OK button was pressed. Otherwise <c>false</c>.</returns>
        public bool ShowDialog(Window owner)
        {
            var view = new TextInputView(owner, this);
            if (true == view.ShowDialog())
            {
                return true;
            }
            return false;
        }
    }
}
