namespace PDFiumDotNET.Apps.PDFViewForms
{
    using System;
    using System.Windows.Forms;
    using PDFiumDotNET.Apps.PDFViewForms.Contracts;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Class implements main form of the 'PDFViewForms' example application.
    /// </summary>
    internal partial class MainForm : Form, IMainView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm(IMainPresenterForView presenter)
        {
            InitializeComponent();

            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the presenter of the MVP structural pattern.
        /// </summary>
        public IMainPresenterForView Presenter { get; private set; }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> used in PDF document view.
        /// </summary>
        public IPDFPageComponent PDFPageComponentForView
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> used in thumbnail PDF document view.
        /// </summary>
        public IPDFPageComponent PDFPageComponentForThumbnail
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Protected override methods

        /// <inheritdoc/>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Presenter.ViewInitialized(this);
        }

        #endregion Protected override methods

        #region Private methods
        #endregion Private methods

        #region Private event handler methods

        private void HandleOpenMenuItemClickEvent(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                // Disable "Method 'xxxx' passes a literal string as parameter 'yyyy' of a call to 'zzzz'."
#pragma warning disable CA1303
                // ToDo: Hard coded text
                Filter = "PDF file (*.pdf)|*.pdf|All files (*.*)|*.*",
                Title = "Select PDF file",
#pragma warning restore
                Multiselect = false,
                ValidateNames = true,
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Presenter.OpenFile(dlg.FileName);
            }

            dlg.Dispose();
        }

        private void HandleCloseMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.CloseFile();
        }

        private void HandlePropertiesMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ShowInformation();
        }

        private void HandleExitMenuItemClickEvent(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleAboutMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ShowAbout();
        }

        #endregion Private event handler methods

        #region Implementation of IMainView

        /// <inheritdoc/>
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }

        /// <inheritdoc/>
        public void SetPDFPageComponentForView(IPDFPageComponent pageComponent)
        {
            PDFPageComponentForView = pageComponent;
            pdfControl.PDFPageComponent = pageComponent;
        }

        /// <inheritdoc/>
        public void SetPDFPageComponentForThumbnail(IPDFPageComponent pageComponent)
        {
            PDFPageComponentForThumbnail = pageComponent;
            pdfThumbnailControl.PDFPageComponent = pageComponent;
        }

        /// <inheritdoc/>
        public void EnableFileOpen(bool enable)
        {
            toolStripMenuItemFileOpen.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableFileClose(bool enable)
        {
            toolStripMenuItemFileClose.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableFileProperties(bool enable)
        {
            toolStripMenuItemFileProperties.Enabled = enable;
        }

        #endregion Implementation of IMainView
    }
}
