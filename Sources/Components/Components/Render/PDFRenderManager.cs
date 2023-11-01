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
        protected abstract PDFSize<double> RequiredDocumentArea { get; }

        #endregion Protected abstract properties

        #region Protected abstract methods

        /// <summary>
        /// The method calculates the position of page in document area.
        /// </summary>
        /// <param name="pageIndex">Index of page to calculate the position for.</param>
        /// <returns>Position of page in document area.</returns>
        protected abstract PDFRectangle<double> GetPagePosition(int pageIndex);

        #endregion Protected abstract methods

        #region Internal abstract methods

        /// <summary>
        /// This method should be called whenever the document area is to be calculated.
        /// Typically when the margin or zoom changes.
        /// </summary>
        internal abstract void CalculateDocumentArea();

        #endregion Internal abstract methods

        #region Internal virtual methods

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
        /// The method detaches page component.
        /// </summary>
        internal virtual void DettachPageComponent()
        {
            if (PageComponent != null && PageComponent.ZoomComponent != null)
            {
                PageComponent.ZoomComponent.PropertyChanged -= HandleZoomComponentPropertyChangedEvent;
            }
        }

        #endregion Internal virtual methods

        #region Protected virtual methods

        /// <summary>
        /// The method returns all necessary information to render current content of PDF document.
        /// </summary>
        /// <param name="viewportArea">Viewport area of document area to draw.</param>
        /// <returns>Render information contains all pages to render and viewport area where the pages lie.</returns>
        protected virtual IPDFRenderInfo GetPagesToRender(PDFRectangle<double> viewportArea)
        {
            return new PDFRenderInfo
            {
                ZoomFactor = PageComponent?.ZoomComponent != null ? PageComponent.ZoomComponent.CurrentZoomFactor : 1d,
                ViewportArea = viewportArea,
                PagesToRender = new List<IPDFPageRenderInfo>(),
            };
        }

        /// <summary>
        /// The method calculates new horizontal offset to draw the page on center on the same position after zoom will change.
        /// </summary>
        /// <param name="renderInfo">Last render information used to render current content of PDF document.</param>
        /// <param name="newZoomFactor">New zoom to use for computing of new horizontal offset.</param>
        /// <returns>New horizontal offset.</returns>
        protected virtual double GetHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                return 0d;
            }

            return renderInfo.ViewportArea.X;
        }

        /// <summary>
        /// The method calculates new vertical offset to draw the page on center on the same position after zoom will change.
        /// </summary>
        /// <param name="renderInfo">Last render information used to render current content of PDF document.</param>
        /// <param name="newZoomFactor">New zoom to use for computing of new vertical offset.</param>
        /// <returns>New vertical offset.</returns>
        protected virtual double GetVerticalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                return 0d;
            }

            return renderInfo.ViewportArea.X;
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
        public double WidestPageRow { get; protected set; }

        /// <inheritdoc/>
        public PDFRectangle<double> DeterminePagePosition(int pageIndex) => GetPagePosition(pageIndex);

        /// <inheritdoc/>
        public IPDFRenderInfo DetermineRenderInfo(PDFRectangle<double> viewportArea) => GetPagesToRender(viewportArea);

        /// <inheritdoc/>
        public double DetermineHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor) => GetHorizontalOffset(renderInfo, newZoomFactor);

        /// <inheritdoc/>
        public double DetermineVerticalOffset(IPDFRenderInfo renderInfo, double newZoomFactor) => GetVerticalOffset(renderInfo, newZoomFactor);

        /// <inheritdoc/>
        public event EventHandler<EventArgs> DocumentAreaChanged;

        #endregion Implementation of IPDFRenderManager
    }
}
