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

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Static constructor to define metadata for the control (and link it to the style in Generic.xaml).
        /// </summary>
        static PDFView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PDFView), new FrameworkPropertyMetadata(typeof(PDFView)));
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
        protected override Size MeasureOverride(Size constraint)
        {
            Size infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Size childSize = base.MeasureOverride(infiniteSize);

            if (PDFPageComponent != null && PDFPageComponent.PageCount != 0)
            {
                var newSize = DetermineExtent();
                if (_workArea != newSize)
                {
                    _workArea = newSize;
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
            else if (childSize != _workArea)
            {
                // Use the size of the child as the un-scaled extent content.
                _workArea = childSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            // Update the size of the viewport onto the content based on the passed in 'constraint'.
            UpdateViewportSize(constraint);

            double width = constraint.Width;
            double height = constraint.Height;

            if (double.IsInfinity(width))
            {
                // Make sure we don't return infinity!
                width = childSize.Width;
            }

            if (double.IsInfinity(height))
            {
                // Make sure we don't return infinity!
                height = childSize.Height;
            }

            return new Size(width, height);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(DesiredSize);

            if (PDFPageComponent != null && PDFPageComponent.PageCount != 0)
            {
                var newSize = DetermineExtent();
                if (_workArea != newSize)
                {
                    _workArea = newSize;
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
            else if (DesiredSize != _workArea)
            {
                // Use the size of the child as the un-scaled extent content.
                _workArea = DesiredSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            // Update the size of the viewport onto the content based on the passed in 'arrangeBounds'.
            UpdateViewportSize(arrangeBounds);

            return size;
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
            if (e.Manipulators.Count() == 1)
            {
                HorizontalOffset = _startManipulationHorizontalOffset - e.CumulativeManipulation.Translation.X;
                VerticalOffset = _startManipulationVerticalOffset - e.CumulativeManipulation.Translation.Y;
            }
            else if (e.Manipulators.Count() == 2)
            {
                var factor = (e.CumulativeManipulation.Scale.X + e.CumulativeManipulation.Scale.Y) / 2;
                var newZoom = _startManipulationZoom * factor;
                PDFZoomComponent.ActualZoomFactor = newZoom;
            }

            e.Handled = true;
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

            e.Handled = true;
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
            //point.X += HorizontalOffset;
            point.Y += VerticalOffset;

            // ToDo: Remove this debug info.
            // Debug.WriteLine($"-----> Position on work area is: {point.X}, {point.Y}");

            // ToDo: Optimize this place. Don't iterate throught all pages in some cases.
            foreach (var pageInfo in _renderedPages)
            {
                // ToDo: Remove this debug info.
                // Debug.WriteLine($"-------------------------------------------------------------> Page: {pageInfo.Page.PageIndex}, l:{pageInfo.Left}, r:{pageInfo.Right}, t:{pageInfo.Top}, b:{pageInfo.Bottom}");
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
                    var link = pageInfo.Page.GetLinkFromPoint(point.X, point.Y);
                    if (link != null)
                    {
                        Cursor = Cursors.Hand;
                        return;
                    }
                }
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
            //point.X += HorizontalOffset;
            point.Y += VerticalOffset;

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
                    var link = pageInfo.Page.GetLinkFromPoint(point.X, point.Y);
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
        }

        #endregion Protected override methods

        #region Private methods

        /// <summary>
        /// Update the viewport size from the specified size.
        /// </summary>
        private void UpdateViewportSize(Size newSize)
        {
            if (_viewport == newSize)
            {
                // The viewport is already the specified size.
                return;
            }

            // Set the new value
            _viewport = newSize;

            // Tell that owning ScrollViewer that scrollbar data has changed.
            ScrollOwner?.InvalidateScrollInfo();
        }

        private Size DetermineExtent()
        {
            var width = PDFPageComponent.WidestWidth * PDFZoomComponent.ActualZoomFactor;
            var height = PDFPageComponent.CumulativeHeight * PDFZoomComponent.ActualZoomFactor;
            var marginWidth = 2d * PDFPageMargin;
            var marginHeight = (PDFPageComponent.PageCount + 1) * PDFPageMargin;
            return new Size(Math.Round(width + marginWidth, 2), Math.Round(height + marginHeight, 2));
        }

        #endregion Private methods
    }
}
