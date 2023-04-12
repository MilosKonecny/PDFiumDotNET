namespace PDFiumDotNET.WinFormsControls
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Render;

    /// <summary>
    /// Class implements control to view thumbnails of PDF document.
    /// </summary>
    public partial class PDFThumbnailControl : Panel
    {
        #region Private fields

        /// <summary>
        /// Render information with list of pages to render and other stuff. Used to examine click, touch, ...
        /// </summary>
        private IPDFRenderInfo _renderInformation;

        /// <summary>
        /// Offset of viewport in x axis.
        /// </summary>
        private double _horizontalOffset;

        /// <summary>
        /// Offset of viewport in y axis.
        /// </summary>
        private double _verticalOffset;

        /// <summary>
        /// The area where all pages will fit.
        /// </summary>
        private Size _documentArea = default;

        /// <summary>
        /// The area visible to user.
        /// </summary>
        private Size _viewportArea = default;

        /// <summary>
        /// Variable used to support scroll through mouse and stylus dragging.
        /// </summary>
        private Point _startDragPoint = new Point(-1, -1);

        /// <summary>
        /// Variable used to support scroll through mouse and stylus dragging.
        /// </summary>
        private double _startHorizontalOffset;

        /// <summary>
        /// Variable used to support scroll through mouse and stylus dragging.
        /// </summary>
        private double _startVerticalOffset;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFThumbnailControl"/> class.
        /// </summary>
        public PDFThumbnailControl()
        {
            // UserPaint - The control paints itself rather than the operating system doing so.
            // ResizeRedraw - The control is redrawn when it is resized.
            // OptimizedDoubleBuffer - The control is first drawn to a buffer rather than directly to the screen, which can reduce flicker.
            // AllPaintingInWmPaint - The control ignores the window message WM_ERASEBKGND to reduce flicker.
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the vertical size of the viewport for this content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the vertical size of the viewport for this content.
        /// This property has no default value.</value>
        public double ViewportHeight => _viewportArea.Height;

        /// <summary>
        /// Gets the horizontal size of the viewport for this content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the horizontal size of the viewport for this content.
        /// This property has no default value.</value>
        public double ViewportWidth => _viewportArea.Width;

        /// <summary>
        /// Gets the horizontal offset of the scrolled content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the horizontal offset.
        /// Valid values are between zero and the document area width minus the <see cref="ViewportWidth"/>.
        /// This property has no default value.</value>
        public double HorizontalOffset
        {
            get => _horizontalOffset;
            private set
            {
                value = Math.Max(0, Math.Min(value, _documentArea.Width - ViewportWidth));
                if (_horizontalOffset != value)
                {
                    _horizontalOffset = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets the vertical offset of the scrolled content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the vertical offset of the scrolled content.
        /// Valid values are between zero and the document area height minus the <see cref="ViewportHeight"/>.
        /// This property has no default value.</value>
        public double VerticalOffset
        {
            get => _verticalOffset;
            private set
            {
                value = Math.Max(0, Math.Min(value, _documentArea.Height - ViewportHeight));
                if (_verticalOffset != value)
                {
                    _verticalOffset = value;
                    Invalidate();
                }
            }
        }

        #endregion Public properties

        #region Protected override methods

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // base.OnMouseWheel(e);
            if (e == null)
            {
                return;
            }

            if (Control.ModifierKeys != Keys.Control)
            {
                VerticalOffset -= e.Delta;
            }

            Invalidate();
        }

        /// <inheritdoc/>
        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se != null)
            {
                if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    VerticalOffset = se.NewValue;
                }
                else if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    HorizontalOffset = se.NewValue;
                }
            }

            base.OnScroll(se);
        }

        /// <inheritdoc/>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // base.OnPaintBackground(e);
        }

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (e?.Graphics == null)
            {
                return;
            }

            // base.OnPaint(e);
            if (PDFPageComponent != null)
            {
                // Determine pages to draw.
                var viewportInDocument = new PDFRectangle<double>(HorizontalOffset, VerticalOffset, ViewportWidth, ViewportHeight);
                _renderInformation = PDFPageComponent.RenderManager.DetermineRenderInfo(viewportInDocument);
            }

            RenderPages(e.Graphics);
        }

        /// <inheritdoc/>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            _viewportArea = new Size(Width, Height);
            Invalidate();
        }

        /// <inheritdoc/>
        protected override void OnSizeChanged(EventArgs e)
        {
            var newSize = new Size(Width, Height);
            UpdateViewportAreaSize(newSize);
            UpdateDocumentAreaSize(DetermineDocumentAreaSize(newSize));
            base.OnSizeChanged(e);
        }

        /// <inheritdoc/>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (_startDragPoint.X != -1 && _startDragPoint.Y != -1)
                    {
                        var newDragPoint = e.Location;
                        VerticalOffset = _startVerticalOffset + _startDragPoint.Y - newDragPoint.Y;
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e != null && e.Button == MouseButtons.Left)
            {
                _startHorizontalOffset = HorizontalOffset;
                _startVerticalOffset = VerticalOffset;
                _startDragPoint = e.Location;
            }
        }

        /// <inheritdoc/>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _startDragPoint = new Point(-1, -1);

            if (e != null && _renderInformation?.PagesToRender != null)
            {
                var point = e.Location;
                var page = _renderInformation.PagesToRender.FirstOrDefault(p => point.X > p.RelativePositionInViewportArea.Left && point.X < p.RelativePositionInViewportArea.Right && point.Y > p.RelativePositionInViewportArea.Top && point.Y < p.RelativePositionInViewportArea.Bottom);
                if (page != null)
                {
                    PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1);
                }
            }
        }

        #endregion Protected override methods

        #region Private methods

        /// <summary>
        /// Update the viewport size.
        /// </summary>
        /// <param name="newSize">New size to use for viewport.</param>
        private void UpdateViewportAreaSize(Size newSize)
        {
            if (_viewportArea == newSize)
            {
                return;
            }

            _viewportArea = newSize;
            Invalidate();
        }

        /// <summary>
        /// Update the viewport size.
        /// </summary>
        /// <param name="newSize">New size to use for viewport.</param>
        private void UpdateDocumentAreaSize(Size newSize)
        {
            if (_documentArea == newSize)
            {
                return;
            }

            _documentArea = newSize;
            AutoScrollMinSize = new Size(_documentArea.Width, _documentArea.Height);
            Invalidate();
        }

        /// <summary>
        /// Determines area where fit all pages of opened document.
        /// </summary>
        /// <param name="availableSize">Available size based on current layout of application.</param>
        /// <returns>Required size to show all pages of opened document.</returns>
        private Size DetermineDocumentAreaSize(Size availableSize)
        {
            if (PDFPageComponent == null)
            {
                return availableSize;
            }

            return new Size((int)PDFPageComponent.RenderManager.DocumentArea.Width, (int)PDFPageComponent.RenderManager.DocumentArea.Height);
        }

        /// <summary>
        /// Reset all relevant status fields.
        /// </summary>
        private void ResetStatus()
        {
            _horizontalOffset = 0d;
            _verticalOffset = 0d;
            _documentArea = new Size(0, 0);
            _renderInformation = null;
        }

        #endregion Private methods
    }
}
