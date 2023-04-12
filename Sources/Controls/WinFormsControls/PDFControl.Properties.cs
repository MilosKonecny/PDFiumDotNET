namespace PDFiumDotNET.WinFormsControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.EventArguments;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFControl
    {
        #region Private fields

        private Color _pdfFindSelectionBorder;
        private Color _pdfFindSelectionBackground;
        private Size _pdfPageMargin;
        private Color _pdfPageBackground;
        private Color _pdfPageActiveBorder;
        private Margins _pdfPageActiveBorderThickness;
        private Color _pdfPageBorder;
        private Margins _pdfPageBorderThickness;
        private IPDFPageComponent _pdfPageComponent;
        private bool _showPageLabel;
        private bool _activatePageOnClick;
        private bool _activatePageInCenter;

        #endregion Private fields

        #region Public properties

        /// <summary>
        /// Gets or sets the border of find selection.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Color PDFFindSelectionBorder
        {
            get
            {
                return _pdfFindSelectionBorder;
            }

            set
            {
                if (_pdfFindSelectionBorder != value)
                {
                    _pdfFindSelectionBorder = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background of find selection.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Color PDFFindSelectionBackground
        {
            get
            {
                return _pdfFindSelectionBackground;
            }

            set
            {
                if (_pdfFindSelectionBackground != value)
                {
                    _pdfFindSelectionBackground = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the margin on the left/right and top/bottom side of page.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Size PDFPageMargin
        {
            get
            {
                return _pdfPageMargin;
            }

            set
            {
                if (_pdfPageMargin != value)
                {
                    _pdfPageMargin = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the page background.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Color PDFPageBackground
        {
            get
            {
                return _pdfPageBackground;
            }

            set
            {
                if (_pdfPageBackground != value)
                {
                    _pdfPageBackground = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the border of active page.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Color PDFPageActiveBorder
        {
            get
            {
                return _pdfPageActiveBorder;
            }

            set
            {
                if (_pdfPageActiveBorder != value)
                {
                    _pdfPageActiveBorder = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the border thickness of active page.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Margins PDFPageActiveBorderThickness
        {
            get
            {
                return _pdfPageActiveBorderThickness;
            }

            set
            {
                if (_pdfPageActiveBorderThickness != value)
                {
                    _pdfPageActiveBorderThickness = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the border of page.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Color PDFPageBorder
        {
            get
            {
                return _pdfPageBorder;
            }

            set
            {
                if (_pdfPageBorder != value)
                {
                    _pdfPageBorder = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the border thickness of page.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public Margins PDFPageBorderThickness
        {
            get
            {
                return _pdfPageBorderThickness;
            }

            set
            {
                if (_pdfPageBorderThickness != value)
                {
                    _pdfPageBorderThickness = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the page component - source of PDF document information.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public IPDFPageComponent PDFPageComponent
        {
            get
            {
                return _pdfPageComponent;
            }

            set
            {
                if (_pdfPageComponent != null)
                {
                    _pdfPageComponent.FindSelectionBackgroundFunc = null;
                    _pdfPageComponent.FindSelectionBorderFunc = null;
                    _pdfPageComponent.MainComponent.PropertyChanged -= HandlePDFComponentPropertyChangedEvent;
                    _pdfPageComponent.NavigatedToPage -= HandlePDFPageComponentNavigatedToPageEvent;
                    _pdfPageComponent.TextSelectionsRemoved -= HandlePDFPageComponentTextSelectionsRemovedEvent;
                    _pdfPageComponent.ZoomComponent.ZoomChanged -= HandlePDFZoomComponentPropertyChangedEvent;
                }

                _pdfPageComponent = value;
                if (_pdfPageComponent != null)
                {
                    _pdfPageComponent.FindSelectionBackgroundFunc = () =>
                    BitConverter.ToInt32(new byte[] { PDFFindSelectionBackground.B, PDFFindSelectionBackground.G, PDFFindSelectionBackground.R, PDFFindSelectionBackground.A }, 0);
                    _pdfPageComponent.FindSelectionBorderFunc = () =>
                    BitConverter.ToInt32(new byte[] { PDFFindSelectionBorder.B, PDFFindSelectionBorder.G, PDFFindSelectionBorder.R, PDFFindSelectionBorder.A }, 0);
                    _pdfPageComponent.MainComponent.PropertyChanged += HandlePDFComponentPropertyChangedEvent;
                    _pdfPageComponent.NavigatedToPage += HandlePDFPageComponentNavigatedToPageEvent;
                    _pdfPageComponent.TextSelectionsRemoved += HandlePDFPageComponentTextSelectionsRemovedEvent;

                    _pdfPageComponent.ZoomComponent.ZoomChanged += HandlePDFZoomComponentPropertyChangedEvent;

                    _pdfPageComponent.PageMargin = new PDFSize<double>(PDFPageMargin.Width, PDFPageMargin.Height);
                }
            }
        }

        /// <summary>
        /// Gets or sets the information indicating whether the page label should be drawn.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        [DefaultValue(true)]
        public bool ShowPageLabel
        {
            get
            {
                return _showPageLabel;
            }

            set
            {
                if (_showPageLabel != value)
                {
                    _showPageLabel = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the information indicating whether page is activated after click on it.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public bool ActivatePageOnClick
        {
            get
            {
                return _activatePageOnClick;
            }

            set
            {
                if (_activatePageOnClick != value)
                {
                    _activatePageOnClick = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the information indicating whether page is activated in case page is in center of viewport.
        /// </summary>
        [Browsable(true)]
        [Category("PDFiumDotNET")]
        public bool ActivatePageInCenter
        {
            get
            {
                return _activatePageInCenter;
            }

            set
            {
                if (_activatePageInCenter != value)
                {
                    _activatePageInCenter = value;
                    Invalidate();
                }
            }
        }

        #endregion Public properties

        #region Private event handler methods

        private void HandlePDFComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName)
                || string.Equals(
                    nameof(IPDFComponent.IsDocumentOpened),
                    e.PropertyName,
                    StringComparison.OrdinalIgnoreCase))
            {
                ResetStatus();
                Invalidate();
            }
        }

        private void HandlePDFPageComponentNavigatedToPageEvent(object sender, NavigatedToPageEventArgs e)
        {
            if (ActivatePageOnClick)
            {
                // Don't scroll the content to display the current page at the top of the viewport.
                return;
            }

            // Current page is changed. Scroll to this page.
            var verticalOffset = PDFPageComponent.RenderManager.DeterminePagePosition(e.CurrentPageIndex - 1).Y;
            var horizontalOffset = double.NaN;
            if (e.IsDetailedNavigation)
            {
                // Get target page
                var page = PDFPageComponent.Pages[e.CurrentPageIndex - 1];

                // Center vertically
                var pageHeight = page.Height * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                var detailedPositionYFromTop = pageHeight - (e.DetailedPositionY * PDFPageComponent.ZoomComponent.CurrentZoomFactor);
                verticalOffset += detailedPositionYFromTop - (Height / 2);

                // Center horizontally
                var pageWidth = page.Width * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                var detailedPositionXFromLeft = e.DetailedPositionX * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                horizontalOffset = ((_documentArea.Width - pageWidth) / 2) + detailedPositionXFromLeft - (Width / 2);
            }

            VerticalOffset = verticalOffset;
            if (!double.IsNaN(horizontalOffset))
            {
                HorizontalOffset = horizontalOffset;
            }
        }

        private void HandlePDFPageComponentTextSelectionsRemovedEvent(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void HandlePDFZoomComponentPropertyChangedEvent(object sender, ZoomChangedEventArgs e)
        {
            _horizontalOffset = PDFPageComponent.RenderManager.DetermineHorizontalOffset(_renderInformation, e.NewZoomFactor);
            _verticalOffset = PDFPageComponent.RenderManager.DetermineVerticalOffset(_renderInformation, e.NewZoomFactor);
            Invalidate();
        }

        #endregion Private event handler methods
    }
}
