namespace PDFiumDotNET.WinFormsControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Class implements control to view thumbnails of PDF document.
    /// </summary>
    public partial class PDFThumbnailControl
    {
        #region Private fields

        private Color _pdfPageBackground;
        private Color _pdfPageBorder;
        private Margins _pdfPageBorderThickness;
        private IPDFPageComponent _pdfPageComponent;

        #endregion Private fields

        #region Public properties

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
                    _pdfPageComponent.MainComponent.PropertyChanged -= HandlePDFComponentPropertyChangedEvent;
                }

                _pdfPageComponent = value;
                if (_pdfPageComponent != null)
                {
                    _pdfPageComponent.MainComponent.PropertyChanged += HandlePDFComponentPropertyChangedEvent;

                    // ToDo: Set margin if Font changes.
                    _pdfPageComponent.RenderManager.PageMargin = new PDFSize<double>(Font.SizeInPoints, 2d * Font.SizeInPoints);
                }
            }
        }

        #endregion Public properties

        #region Private event handler methods

        private void HandlePDFComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName)
                || string.Equals(
                    nameof(IPDFComponent.IsDocumentOpen),
                    e.PropertyName,
                    StringComparison.OrdinalIgnoreCase))
            {
                ResetStatus();
                Invalidate();
            }
        }

        #endregion Private event handler methods
    }
}
