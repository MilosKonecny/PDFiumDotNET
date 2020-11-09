namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// View class shows pages from opend PDF document.
    /// </summary>
    public partial class PDFView : Control, IScrollInfo
    {
        #region Private fields

        /// <summary>
        /// List of rendered pages. Used to examine click, touch, ...
        /// </summary>
        private List<IPDFPageRenderInfo> _renderedPages = new List<IPDFPageRenderInfo>();

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
        private Size _workArea = new Size(0, 0);

        /// <summary>
        /// The area visible to user.
        /// </summary>
        private Size _viewport = new Size(0, 0);

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

        private double _oldZoomFactor = -1d;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Static constructor to define metadata for the control (and link it to the style in Generic.xaml).
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            var newSize = new Size(
                double.IsInfinity(constraint.Width) ? 0d : constraint.Width,
                double.IsInfinity(constraint.Height) ? 0d : constraint.Height);

            UpdateViewportSize(newSize);

            return newSize;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(DesiredSize);

            UpdateWorkAreaSize(DeterminePageArea(arrangeBounds));

            UpdateViewportSize(arrangeBounds);

            return size;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (PDFPageComponent != null && PDFPageComponent.PageCount != 0)
            {
                RenderPages(drawingContext);
            }
            else
            {
                RenderEmptyArea(drawingContext);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            _zoomManipulationActive = false;
            _startManipulationZoom = PDFZoomComponent.ActualZoomFactor;
            _startManipulationHorizontalOffset = HorizontalOffset;
            _startManipulationVerticalOffset = VerticalOffset;
            e.Handled = true;
            base.OnManipulationStarted(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e.Manipulators.Count() == 1 && !_zoomManipulationActive)
            {
                HorizontalOffset = _startManipulationHorizontalOffset - e.CumulativeManipulation.Translation.X;
                VerticalOffset = _startManipulationVerticalOffset - e.CumulativeManipulation.Translation.Y;
            }
            else if (e.Manipulators.Count() == 2)
            {
                _zoomManipulationActive = true;
                var factor = (e.CumulativeManipulation.Scale.X + e.CumulativeManipulation.Scale.Y) / 2;
                var newZoom = _startManipulationZoom * factor;
                PDFZoomComponent.ActualZoomFactor = newZoom;
            }

            // e.Handled = true;
            base.OnManipulationDelta(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            // Decrease the velocity of the Rectangle's movement by
            // 10 inches per second every second.
            // (10 inches * 96 pixels per inch / 1000ms^2)
            e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);

            // Decrease the velocity of the Rectangle's resizing by
            // 0.1 inches per second every second.
            // (0.1 inches * 96 pixels per inch / (1000ms^2)
            e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);

            // Decrease the velocity of the Rectangle's rotation rate by
            // 2 rotations per second every second.
            // (2 * 360 degrees / (1000ms^2)
            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);

            // e.Handled = true;
            base.OnManipulationInertiaStarting(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            base.OnManipulationCompleted(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var point = e.GetPosition(this);

            var link = GetLinkOnPosition(point);
            if (link != null)
            {
                Cursor = Cursors.Hand;
                return;
            }

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
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

        #endregion Protected override methods

        #region Private methods

        private IPDFLink GetLinkOnPosition(Point point)
        {
            // ToDo: Optimize this place. Don't iterate throught all pages in some cases.
            foreach (var pageInfo in _renderedPages)
            {
                if (point.X > pageInfo.Left && point.X < pageInfo.Right
                    && point.Y > pageInfo.Top && point.Y < pageInfo.Bottom)
                {
                    // Mouse is over this page
                    // Transform the point to the page.
                    point.X -= pageInfo.Left;
                    point.Y -= pageInfo.Top;
                    // Eliminate zoom factor
                    point.X /= PDFZoomComponent.ActualZoomFactor;
                    point.Y /= PDFZoomComponent.ActualZoomFactor;
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
        private void UpdateViewportSize(Size newSize)
        {
            if (_viewport == newSize)
            {
                return;
            }

            _viewport = newSize;
            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Update the viewport size.
        /// </summary>
        /// <param name="newSize">New size to use for viewport.</param>
        private void UpdateWorkAreaSize(Size newSize)
        {
            if (_workArea == newSize)
            {
                return;
            }

            _workArea = newSize;
            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Datermines area where fit all pages of opened document.
        /// </summary>
        /// <param name="availableSize">Available size based on actual layout of application.</param>
        /// <returns>Required size to show all pages of opened</returns>
        private Size DeterminePageArea(Size availableSize)
        {
            if (PDFPageComponent == null
                || PDFZoomComponent == null
                || PDFPageComponent.PageCount == 0)
            {
                return availableSize;
            }

            var width = availableSize.Width;
            var height = availableSize.Height;
            PDFPageComponent.DeterminePageArea(ref width, ref height, PDFPageMargin, PDFZoomComponent.ActualZoomFactor);
            return new Size(width, height);
        }

        #endregion Private methods
    }
}
