namespace PDFiumDotNET.Apps.PDFViewForms
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using PDFiumDotNET.Apps.PDFViewForms.Contracts;
    using PDFiumDotNET.Components.Contracts;

    /// <summary>
    /// The presenter for <see cref="MainForm"/> and implements two presenter interfaces: <see cref="IMainPresenterForView"/> and <see cref="IMainPresenterForModel"/>.
    /// </summary>
    internal class MainPresenter : IMainPresenterForView, IMainPresenterForModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPresenter"/> class.
        /// </summary>
        public MainPresenter()
        {
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the associated <see cref="IMainModel"/>.
        /// </summary>
        public IMainModel Model
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the associated <see cref="IMainView"/>.
        /// </summary>
        public IMainView View
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Private methods

        private void RunPresenter()
        {
            if (View == null || Model == null)
            {
                // We have wait until model and view are initialized.
                return;
            }

            Model.InitializeComponents();
        }

        private void UpdateGuiMenu()
        {
            View.EnableFileOpen(!Model.IsFileOpen);
            View.EnableFileClose(Model.IsFileOpen);
            View.EnableFileProperties(Model.IsFileOpen);
        }

        #endregion Private methods

        #region Private event handler methods

        private void HandleMainModelPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            UpdateGuiMenu();

            if (string.Equals(nameof(IMainModel.PDFPageComponentForView), e.PropertyName, StringComparison.OrdinalIgnoreCase))
            {
                View.SetPDFPageComponentForView(Model.PDFPageComponentForView);
            }
            else if (string.Equals(nameof(IMainModel.PDFPageComponentForThumbnail), e.PropertyName, StringComparison.OrdinalIgnoreCase))
            {
                View.SetPDFPageComponentForThumbnail(Model.PDFPageComponentForThumbnail);
            }
        }

        #endregion Private event handler methods

        #region Implementation of IMainPresenterForView

        /// <inheritdoc/>
        public void ViewInitialized(IMainView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            RunPresenter();
        }

        /// <inheritdoc/>
        public void OpenFile(string path)
        {
            var result = Model.OpenFile(path);
            UpdateGuiMenu();
            if (result != OpenDocumentResult.Success)
            {
                View.ShowError($"Document '{path}' not opened. Error: {result}");
            }
        }

        /// <inheritdoc/>
        public void CloseFile()
        {
            Model.CloseFile();
            UpdateGuiMenu();
        }

        /// <inheritdoc/>
        public void ShowInformation()
        {
        }

        /// <inheritdoc/>
        public void ShowAbout()
        {
        }

        #endregion Implementation of IMainPresenterForView

        #region Implementation of IMainPresenterForModel

        /// <inheritdoc/>
        public void ModelInitialized(IMainModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Model.PropertyChanged += HandleMainModelPropertyChangedEvent;
            RunPresenter();
        }

        #endregion Implementation of IMainPresenterForModel
    }
}
