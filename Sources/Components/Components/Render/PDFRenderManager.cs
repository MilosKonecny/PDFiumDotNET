namespace PDFiumDotNET.Components.Render
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;
    using PDFiumDotNET.Components.Contracts.Zoom;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// The class <see cref="PDFRenderManager"/> is abstract class containing base implementation of <see cref="IPDFRenderManager"/>.
    /// Provides common functionality for all derived render managers.
    /// </summary>
    internal abstract class PDFRenderManager : IPDFRenderManager
    {
        #region Private fields

        private PDFSize<double> _pageMargin;

        #endregion Private fields

        #region Protected properties

        /// <summary>
        /// Gets <see cref="PDFPageComponent"/> that have to be rendered.
        /// </summary>
        protected PDFPageComponent PageComponent { get; private set; }

        #endregion Protected properties

        #region Protected abstract properties

        /// <summary>
        /// Gets the area into which all pages fit.
        /// </summary>
        internal abstract PDFSize<double> RequiredDocumentArea { get; }

        #endregion Protected abstract properties

        #region Protected abstract methods

        /// <summary>
        /// This method should be called whenever the document area is to be calculated.
        /// Typically when the margin or zoom changes.
        /// </summary>
        internal abstract void CalculateDocumentArea();

        /// <summary>
        /// The method calculates the position of page in document area.
        /// </summary>
        /// <param name="pageIndex">Index of page to calculate the position for.</param>
        /// <returns>Position of page in document area.</returns>
        internal abstract PDFRectangle<double> GetPagePosition(int pageIndex);

        #endregion Protected abstract methods

        #region Protected virtual methods

        /// <summary>
        /// The method attaches page component and initializes render manager.
        /// </summary>
        /// <param name="pageComponent"><see cref="PDFPageComponent"/> to attach and manage the render functionality.</param>
        internal virtual void AttachPageComponent(PDFPageComponent pageComponent)
        {
            PageComponent = pageComponent ?? throw new ArgumentNullException(nameof(pageComponent));

            if (PageComponent.ZoomComponent == null)
            {
                throw new InvalidOperationException();
            }

            PageComponent.ZoomComponent.PropertyChanged += HandleZoomComponentPropertyChangedEvent;
            PageMargin = PageComponent.PageMargin;
        }

        /// <summary>
        /// The method detattaches page component.
        /// </summary>
        internal virtual void DettachPageComponent()
        {
            if (PageComponent != null && PageComponent.ZoomComponent != null)
            {
                PageComponent.ZoomComponent.PropertyChanged -= HandleZoomComponentPropertyChangedEvent;
            }
        }

        /// <summary>
        /// The method returns all pages that lie at least partially in the defined viewport area.
        /// </summary>
        /// <param name="viewportArea">Viewport area of document area to draw.</param>
        /// <returns>Render information contains all pages to render and viewport area where the pages lie.</returns>
        internal virtual IList<IPDFPageRenderInfo> GetPagesToRender(PDFRectangle<double> viewportArea)
        {
            return new List<IPDFPageRenderInfo>();
        }

        #endregion Protected virtual methods

        #region Protected methods

        /// <summary>
        /// Method invokes <see cref="DocumentAreaChanged"/> event.
        /// </summary>
        protected void InvokeDocumentAreaChangedEvent()
        {
            DocumentAreaChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Protected methods

        #region Private event handler methods

        private void HandleZoomComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, nameof(IPDFZoomComponent.CurrentZoomFactor), StringComparison.OrdinalIgnoreCase))
            {
                CalculateDocumentArea();
            }
        }

        #endregion Private event handler methods

        #region Implementation of IPDFRenderManager

        /// <inheritdoc/>
        public PDFSize<double> PageMargin
        {
            get
            {
                return _pageMargin;
            }

            set
            {
                if (_pageMargin != value)
                {
                    _pageMargin = value;
                    CalculateDocumentArea();
                    InvokeDocumentAreaChangedEvent();
                }
            }
        }

        /// <inheritdoc/>
        public PDFSize<double> DocumentArea => RequiredDocumentArea;

        /// <inheritdoc/>
        public PDFRectangle<double> PagePosition(int pageIndex) => GetPagePosition(pageIndex);

        /// <inheritdoc/>
        public IList<IPDFPageRenderInfo> PagesToRender(PDFRectangle<double> viewportArea) => GetPagesToRender(viewportArea);

        /// <inheritdoc/>
        public event EventHandler<EventArgs> DocumentAreaChanged;

        #endregion Implementation of IPDFRenderManager
    }
}
