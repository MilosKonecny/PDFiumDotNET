namespace PDFiumDotNET.Apps.PDFViewForms
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Apps.PDFViewForms.Contracts;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Factory;

    /// <summary>
    /// The class implements model of MVP structural pattern.
    /// </summary>
    internal class MainModel : IMainModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainModel"/> class.
        /// </summary>
        /// <param name="presenter">Associated presenter in MVP architectural pattern.</param>
        public MainModel(IMainPresenterForModel presenter)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

            Presenter.ModelInitialized(this);
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the presenter of the MVP structural pattern.
        /// </summary>
        public IMainPresenterForModel Presenter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IPDFComponent"/> containing PDF document.
        /// </summary>
        public IPDFComponent PDFComponent
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Private methods
        #endregion Private methods

        #region Private invoke event methods

        /// <summary>
        /// The method invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Parameter name used in <see cref="PropertyChangedEventArgs"/>.</param>
        private void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Protected invoke event methods

        #region Implementation of INotifyPropertyChanged

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IMainModel

        /// <inheritdoc/>
        public IPDFPageComponent PDFPageComponentForView
        {
            get;
            private set;
        }

        /// <inheritdoc/>
        public IPDFPageComponent PDFPageComponentForThumbnail
        {
            get;
            private set;
        }

        /// <inheritdoc/>
        public bool IsFileOpen
        {
            get
            {
                return PDFComponent != null ? PDFComponent.IsDocumentOpened : false;
            }
        }

        /// <inheritdoc/>
        public void InitializeComponents()
        {
            if (PDFComponent != null)
            {
                return;
            }

            PDFComponent = PDFFactory.PDFComponent;
            PDFPageComponentForView = PDFComponent.LayoutComponent.CreatePageComponent("Standard", Components.Contracts.Layout.PageLayoutType.Standard);
            PDFPageComponentForThumbnail = PDFComponent.LayoutComponent.CreatePageComponent("Thumbnail", Components.Contracts.Layout.PageLayoutType.Thumbnail);

            PDFPageComponentForThumbnail.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFPageComponent.CurrentPageIndex), e.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    PDFPageComponentForView.NavigateToPage(PDFPageComponentForThumbnail.CurrentPageIndex);
                }
            };

            InvokePropertyChangedEvent(nameof(PDFPageComponentForView));
            InvokePropertyChangedEvent(nameof(PDFPageComponentForThumbnail));
        }

        /// <inheritdoc/>
        public OpenDocumentResult OpenFile(string path)
        {
            var file = Path.GetFullPath(path);
            return PDFComponent.OpenDocument(path);
        }

        /// <inheritdoc/>
        public void CloseFile()
        {
            PDFComponent.CloseDocument();
        }

        #endregion Implementation of IMainModel
    }
}
