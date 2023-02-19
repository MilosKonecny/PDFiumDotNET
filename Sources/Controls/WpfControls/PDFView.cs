namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFView : Control, IScrollInfo
    {
        #region Private fields

        /// <summary>
        /// List of rendered pages. Used to examine click, touch, ...
        /// </summary>
        private List<IPDFPageRenderInfo> _renderedPages = new ();

        /// <summary>
        /// Permission for horizontal scroll.
        /// </summary>
        private bool _canHorizontallyScroll;

        /// <summary>
        /// Permission for vertical scroll.
        /// </summary>
        private bool _canVerticallyScroll;

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
        private Size _documentArea = new Size(0, 0);

        /// <summary>
        /// The area visible to user.
        /// </summary>
        private Size _viewportArea = new Size(0, 0);

        /// <summary>
        /// Zoom at manipulation start.
        /// </summary>
        private double _startManipulationZoom;

        /// <summary>
        /// Horizontal offset at manipulation start.
        /// </summary>
        private double _startManipulationHorizontalOffset;

        /// <summary>
        /// Vertical offset at manipulation start.
        /// </summary>
        private double _startManipulationVerticalOffset;

        /// <summary>
        /// <c>true</c> if touch manipulation for zoom is active.
        /// </summary>
        private bool _zoomManipulationActive = false;

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
        /// Initializes static members of the <see cref="PDFView"/> class.
        /// </summary>
        static PDFView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(typeof(PDFView)));
            BackgroundProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(Brushes.DarkGray));
            BorderBrushProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(Brushes.Black));
            BorderThicknessProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(new Thickness(1)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFView"/> class.
        /// </summary>
        public PDFView()
        {
            IsManipulationEnabled = true;
        }

        #endregion Constructors

        #region Protected override methods

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size constraint)
        {
            var newSize = new Size(
                double.IsInfinity(constraint.Width) ? 0d : constraint.Width,
                double.IsInfinity(constraint.Height) ? 0d : constraint.Height);

            UpdateViewportAreaSize(newSize);

            return newSize;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(DesiredSize);

            UpdateDocumentAreaSize(DetermineDocumentAreaSize(arrangeBounds));

            UpdateViewportAreaSize(arrangeBounds);

            return size;
        }

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (drawingContext == null)
            {
                return;
            }

            if (PDFPageComponent != null)
            {
                // Check the position of viewport area in document area.
                HorizontalOffset = Math.Min(HorizontalOffset, _documentArea.Width - _viewportArea.Width);
                VerticalOffset = Math.Min(VerticalOffset, _documentArea.Height - _viewportArea.Height);

                // Determine pages to draw.
                var viewportInDocument = new PDFRectangle<double>(HorizontalOffset, VerticalOffset, ViewportWidth, ViewportHeight);
                _renderedPages.Clear();
                _renderedPages.AddRange(PDFPageComponent.RenderManager.PagesToRender(viewportInDocument));
            }

            if (_renderedPages.Count > 0)
            {
                RenderPages(drawingContext);
                var pageInfo = _renderedPages.FirstOrDefault(pageInfo => pageInfo.IsNearestToCenter);
                if (pageInfo != null)
                {
                    PDFPageComponent.SetCurrentPage(pageInfo.Page.PageIndex + 1);
                }
            }
            else
            {
                RenderEmptyArea(drawingContext);
            }
        }

        /// <inheritdoc/>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e == null)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    LineLeft();
                    break;
                case Key.Right:
                    LineRight();
                    break;
                case Key.Up:
                    LineUp();
                    break;
                case Key.Down:
                    LineDown();
                    break;
            }
        }

        /// <inheritdoc/>
        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            _zoomManipulationActive = false;
            _startManipulationZoom = PDFZoomComponent.CurrentZoomFactor;
            _startManipulationHorizontalOffset = HorizontalOffset;
            _startManipulationVerticalOffset = VerticalOffset;

            base.OnManipulationStarted(e);
        }

        /// <inheritdoc/>
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e != null && e.CumulativeManipulation != null)
            {
                if ((e.Manipulators.Count() == 1 || e.IsInertial) && !_zoomManipulationActive)
                {
                    HorizontalOffset = _startManipulationHorizontalOffset - e.CumulativeManipulation.Translation.X;
                    VerticalOffset = _startManipulationVerticalOffset - e.CumulativeManipulation.Translation.Y;
                }
                else if (e.Manipulators.Count() == 2)
                {
                    _zoomManipulationActive = true;
                    var factor = (e.CumulativeManipulation.Scale.X + e.CumulativeManipulation.Scale.Y) / 2;
                    var newZoom = _startManipulationZoom * factor;
                    PDFZoomComponent.CurrentZoomFactor = newZoom;
                }
            }

            base.OnManipulationDelta(e);
        }

        /// <inheritdoc/>
        protected override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            if (e != null)
            {
                if (e.TranslationBehavior != null)
                {
                    // Decrease the velocity of the Rectangle's movement by
                    // 10 inches per second every second.
                    // (10 inches * 96 pixels per inch / 1000ms^2)
                    e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);
                }

                if (e.ExpansionBehavior != null)
                {
                    // Decrease the velocity of the Rectangle's resizing by
                    // 0.1 inches per second every second.
                    // (0.1 inches * 96 pixels per inch / (1000ms^2)
                    e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);
                }

                if (e.RotationBehavior != null)
                {
                    // Decrease the velocity of the Rectangle's rotation rate by
                    // 2 rotations per second every second.
                    // (2 * 360 degrees / (1000ms^2)
                    e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);
                }
            }

            base.OnManipulationInertiaStarting(e);
        }

        /// <inheritdoc/>
        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            base.OnManipulationCompleted(e);
        }

        /// <inheritdoc/>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (_startDragPoint.X != -1 && _startDragPoint.Y != -1)
                    {
                        var newDragPoint = e.GetPosition(this);
                        HorizontalOffset = _startHorizontalOffset + _startDragPoint.X - newDragPoint.X;
                        VerticalOffset = _startVerticalOffset + _startDragPoint.Y - newDragPoint.Y;
                    }
                }
                else
                {
                    var point = e.GetPosition(this);
                    var link = GetLinkOnPosition(point);
                    if (link != null)
                    {
                        Cursor = Cursors.Hand;
                        return;
                    }
                }
            }

            Cursor = Cursors.Arrow;
        }

        /// <inheritdoc/>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (e != null)
            {
                _startHorizontalOffset = HorizontalOffset;
                _startVerticalOffset = VerticalOffset;
                _startDragPoint = e.GetPosition(this);
            }
        }

        /// <inheritdoc/>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            _startDragPoint = new Point(-1, -1);

            if (e != null)
            {
                var point = e.GetPosition(this);
                var link = GetLinkOnPosition(point);
                if (link != null)
                {
                    if (link.Action != null)
                    {
                        PDFPageComponent.PerformAction(link.Action);
                    }
                    else if (link.Destination != null)
                    {
                        PDFPageComponent.NavigateToDestination(link.Destination);
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            if (e != null)
            {
                var point = e.GetTouchPoint(this).Position;
                var link = GetLinkOnPosition(point);
                if (link != null)
                {
                    if (link.Action != null)
                    {
                        PDFPageComponent.PerformAction(link.Action);
                    }
                    else if (link.Destination != null)
                    {
                        PDFPageComponent.NavigateToDestination(link.Destination);
                    }
                }
            }
        }

        #endregion Protected override methods

        #region Private methods

        private IPDFLink GetLinkOnPosition(Point point)
        {
            // ToDo: Optimize this place. Don't iterate throught all pages in some cases.
            foreach (var pageInfo in _renderedPages)
            {
                if (point.X > pageInfo.RelativePositionInViewportArea.Left && point.X < pageInfo.RelativePositionInViewportArea.Right
                    && point.Y > pageInfo.RelativePositionInViewportArea.Top && point.Y < pageInfo.RelativePositionInViewportArea.Bottom)
                {
                    // Mouse is over this page
                    // Transform the point to the page.
                    point.X -= pageInfo.RelativePositionInViewportArea.Left;
                    point.Y -= pageInfo.RelativePositionInViewportArea.Top;

                    // Eliminate zoom factor
                    point.X /= PDFZoomComponent.CurrentZoomFactor;
                    point.Y /= PDFZoomComponent.CurrentZoomFactor;

                    // Transform y axis from top-left position to the bottom-left.
                    point.Y = pageInfo.Page.Height - point.Y;

                    // Get the link on this position.
                    return pageInfo.Page.GetLinkFromPoint(point.X, point.Y);
                }
            }

            return null;
        }

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
            ScrollOwner?.InvalidateScrollInfo();
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
            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Datermines area where fit all pages of opened document.
        /// </summary>
        /// <param name="availableSize">Available size based on current layout of application.</param>
        /// <returns>Required size to show all pages of opened document.</returns>
        private Size DetermineDocumentAreaSize(Size availableSize)
        {
            if (PDFPageComponent == null)
            {
                return availableSize;
            }

            return new Size(PDFPageComponent.RenderManager.DocumentArea.Width, PDFPageComponent.RenderManager.DocumentArea.Height);
        }

        /// <summary>
        /// Reset all relevat status fields.
        /// </summary>
        private void ResetStatus()
        {
            _horizontalOffset = 0d;
            _verticalOffset = 0d;
            _documentArea = new Size(0, 0);
            _renderedPages.Clear();
        }

        #endregion Private methods
    }
}
