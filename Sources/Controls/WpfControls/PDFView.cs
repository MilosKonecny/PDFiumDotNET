namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;
    using PDFiumDotNET.WpfControls.Extensions;
    using PDFiumDotNET.WpfControls.Helper;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFView : Control, IScrollInfo
    {
        #region Private fields

        /// <summary>
        /// Render information with list of pages to render and other stuff. Used to examine click, touch, ...
        /// </summary>
        private IPDFRenderInfo _renderInformation;

        /// <summary>
        /// Point with mouse/touch down.
        /// </summary>
        private Point _downPoint;

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

        /// <summary>
        /// Timer is used to control the redraw memory usage.
        /// </summary>
        private DispatcherTimer _drawTimer = new DispatcherTimer();

        /// <summary>
        /// Variable used to control the redraw memory usage.
        /// </summary>
        private bool _isInvalidateFromTimer = false;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="PDFView"/> class.
        /// </summary>
        static PDFView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(typeof(PDFView)));
            BackgroundProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(Brushes.Black));
            BorderThicknessProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(new Thickness(1)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFView"/> class.
        /// </summary>
        public PDFView()
        {
            IsManipulationEnabled = true;
            _drawTimer.Interval = TimeSpan.FromMilliseconds(TimerInterval);
            if (!this.IsDesignTime())
            {
                _drawTimer.Tick += (s, e) =>
                {
                    System.Diagnostics.Debug.WriteLine("Draw timer tick.");
                    if (UseTimerForDraw && PDFPageComponent.MainComponent.IsDocumentOpen)
                    {
                        System.Diagnostics.Debug.WriteLine("Draw timer invalidate");
                        _isInvalidateFromTimer = true;
                        InvalidateVisual();
                        _drawTimer.Stop();
                    }
                };
            }
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
                SetOffsets(
                    Math.Min(HorizontalOffset, _documentArea.Width - _viewportArea.Width),
                    Math.Min(VerticalOffset, _documentArea.Height - _viewportArea.Height),
                    false);

                // Determine pages to draw.
                var viewportInDocument = new PDFRectangle<double>(HorizontalOffset, VerticalOffset, ViewportWidth, ViewportHeight);
                _renderInformation = PDFPageComponent.RenderManager.DetermineRenderInfo(viewportInDocument);
            }

            RenderPages(drawingContext);

            if (_renderInformation?.PagesToRender != null && ActivatePageInCenter)
            {
                var pageInfo = _renderInformation.PagesToRender.FirstOrDefault(pageInfo => pageInfo.IsClosestToCenter);
                if (pageInfo != null)
                {
                    PDFPageComponent.SetCurrentPage(pageInfo.Page.PageIndex + 1);
                }
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
            _startManipulationZoom = PDFPageComponent.ZoomComponent.CurrentZoomFactor;
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
                    SetOffsets(
                        _startManipulationHorizontalOffset - e.CumulativeManipulation.Translation.X,
                        _startManipulationVerticalOffset - e.CumulativeManipulation.Translation.Y,
                        true);
                }
                else if (e.Manipulators.Count() == 2)
                {
                    _zoomManipulationActive = true;
                    var factor = (e.CumulativeManipulation.Scale.X + e.CumulativeManipulation.Scale.Y) / 2;
                    var newZoom = _startManipulationZoom * factor;
                    SetZoom(newZoom);
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
                        SetOffsets(
                            _startHorizontalOffset + _startDragPoint.X - newDragPoint.X,
                            _startVerticalOffset + _startDragPoint.Y - newDragPoint.Y,
                            true);
                    }
                }
                else
                {
                    var point = e.GetPosition(this);
                    var link = GetLinkFromPoint(point);
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
                _downPoint = e.GetPosition(this);
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
                if (Distance(_downPoint, point) < 5)
                {
                    var link = GetLinkFromPoint(point);
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
                    else if (ActivatePageOnClick)
                    {
                        var page = PageFromPoint(point);
                        PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1);
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);

            if (e != null && _renderInformation?.PagesToRender != null)
            {
                _downPoint = e.GetTouchPoint(this).Position;
            }
        }

        /// <inheritdoc/>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            if (e != null)
            {
                var point = e.GetTouchPoint(this).Position;
                if (Distance(_downPoint, point) < 5)
                {
                    var link = GetLinkFromPoint(point);
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
                    else if (ActivatePageOnClick)
                    {
                        var page = PageFromPoint(point);
                        PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1);
                    }
                }
            }
        }

        #endregion Protected override methods

        #region Private methods

        private static double Distance(Point point1, Point point2)
        {
            var dX = point1.X - point2.X;
            var dY = point1.Y - point2.Y;
            var val = Math.Sqrt((dX * dX) + (dY * dY));
            return val;
        }

        private IPDFPageRenderInfo PageFromPoint(Point point)
        {
            return _renderInformation.PagesToRender.FirstOrDefault(p =>
            {
                return point.X > p.RelativePositionInViewportArea.Left
                && point.X < p.RelativePositionInViewportArea.Right
                && point.Y > p.RelativePositionInViewportArea.Top &&
                point.Y < p.RelativePositionInViewportArea.Bottom;
            });
        }

        private IPDFLink GetLinkFromPoint(Point point)
        {
            if (_renderInformation == null || _renderInformation.PagesToRender == null)
            {
                return null;
            }

            var pageInfo = PageFromPoint(point);
            if (pageInfo != null)
            {
                // Transform the point to the page.
                point.X -= pageInfo.RelativePositionInViewportArea.Left;
                point.Y -= pageInfo.RelativePositionInViewportArea.Top;

                // Eliminate zoom factor
                point.X /= PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                point.Y /= PDFPageComponent.ZoomComponent.CurrentZoomFactor;

                // Transform y axis from top-left position to the bottom-left.
                point.Y = pageInfo.Page.Height - point.Y;

                // Get the link on this position.
                return pageInfo.Page.GetLinkFromPoint(point.X, point.Y);
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

            return new Size(PDFPageComponent.RenderManager.DocumentArea.Width, PDFPageComponent.RenderManager.DocumentArea.Height);
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

        /// <summary>
        /// The method sets both offsets at the same time and invalidate is called only once if required.
        /// </summary>
        /// <param name="horizontalOffset">New horizontal offset.</param>
        /// <param name="verticalOffset">New vertical offset.</param>
        /// <param name="callInvalidate"><c>true</c> - call the invalidate if at least one of the offsets has changed. <c>false</c> - don't call invalidate.</param>
        private void SetOffsets(double horizontalOffset, double verticalOffset, bool callInvalidate)
        {
            horizontalOffset = Math.Max(0, Math.Min(horizontalOffset, ExtentWidth - ViewportWidth));
            verticalOffset = Math.Max(0, Math.Min(verticalOffset, ExtentHeight - ViewportHeight));

            if (!DoubleHelper.OffsetsAreEqual(horizontalOffset, HorizontalOffset)
                || !DoubleHelper.OffsetsAreEqual(verticalOffset, VerticalOffset))
            {
                _horizontalOffset = horizontalOffset;
                _verticalOffset = verticalOffset;

                if (callInvalidate)
                {
                    InvalidateVisual();
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
        }

        /// <summary>
        /// The method sets zoom factor in zoom component.
        /// </summary>
        /// <param name="newZoomFactor">New zoom factor to set in zoom component.</param>
        private void SetZoom(double newZoomFactor)
        {
            if (!DoubleHelper.ZoomsAreEqual(PDFPageComponent.ZoomComponent.CurrentZoomFactor, newZoomFactor))
            {
                PDFPageComponent.ZoomComponent.CurrentZoomFactor = newZoomFactor;
            }
        }

        #endregion Private methods
    }
}
