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
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// View class shows page thumbnails from opened PDF document.
    /// </summary>
    public partial class PDFThumbnailView : Control, IScrollInfo
    {
        #region Private fields

        /// <summary>
        /// List of rendered pages. Used to examine click, touch, ...
        /// </summary>
        private List<IPDFPageRenderInfo> _renderedPages = new ();

        /// <summary>
        /// Page with touch down.
        /// </summary>
        private IPDFPageRenderInfo _onTouchDownPage;

        /// <summary>
        /// Permission for horizontal scroll.
        /// </summary>
        private bool _canHorizontallyScroll;

        /// <summary>
        /// Permission for vertical scroll.
        /// </summary>
        private bool _canVerticallyScroll;

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
        /// Vertical offset at manipulation start.
        /// </summary>
        private double _startManipulationVerticalOffset;

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

        static PDFThumbnailView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PDFThumbnailView), new FrameworkPropertyMetadata(typeof(PDFThumbnailView)));
            BackgroundProperty.OverrideMetadata(typeof(PDFThumbnailView), new FrameworkPropertyMetadata(Brushes.White));
            BorderBrushProperty.OverrideMetadata(typeof(PDFThumbnailView), new FrameworkPropertyMetadata(Brushes.Black));
            BorderThicknessProperty.OverrideMetadata(typeof(PDFThumbnailView), new FrameworkPropertyMetadata(new Thickness(0.5d)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFThumbnailView"/> class.
        /// </summary>
        public PDFThumbnailView()
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
                // Determine pages to draw.
                var viewportInDocument = new PDFRectangle<double>(HorizontalOffset, VerticalOffset, ViewportWidth, ViewportHeight);
                _renderedPages.Clear();
                _renderedPages.AddRange(PDFPageComponent.RenderManager.PagesToRender(viewportInDocument));
            }

            if (_renderedPages.Count > 0)
            {
                RenderPages(drawingContext);
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
            _startManipulationVerticalOffset = VerticalOffset;

            base.OnManipulationStarted(e);
        }

        /// <inheritdoc/>
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e != null && e.CumulativeManipulation != null)
            {
                VerticalOffset = _startManipulationVerticalOffset - e.CumulativeManipulation.Translation.Y;
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
                        VerticalOffset = _startVerticalOffset + _startDragPoint.Y - newDragPoint.Y;
                    }
                }
            }
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
                var page = _renderedPages.FirstOrDefault(p => point.X > p.RelativePositionInViewportArea.Left && point.X < p.RelativePositionInViewportArea.Right && point.Y > p.RelativePositionInViewportArea.Top && point.Y < p.RelativePositionInViewportArea.Bottom);
                if (page != null)
                {
                    PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1);
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);

            if (e != null)
            {
                var point = e.GetTouchPoint(this).Position;
                _onTouchDownPage = _renderedPages.FirstOrDefault(p => point.X > p.RelativePositionInViewportArea.Left && point.X < p.RelativePositionInViewportArea.Right && point.Y > p.RelativePositionInViewportArea.Top && point.Y < p.RelativePositionInViewportArea.Bottom);
            }
        }

        /// <inheritdoc/>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            if (e != null)
            {
                var point = e.GetTouchPoint(this).Position;
                var page = _renderedPages.FirstOrDefault(p => point.X > p.RelativePositionInViewportArea.Left && point.X < p.RelativePositionInViewportArea.Right && point.Y > p.RelativePositionInViewportArea.Top && point.Y < p.RelativePositionInViewportArea.Bottom);
                if (page != null && _onTouchDownPage != null)
                {
                    if (_onTouchDownPage.Page.PageIndex == page.Page.PageIndex)
                    {
                        // Navigate to this page only if touch down and up was made on the same page.
                        PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1);
                    }
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
            _verticalOffset = 0d;
            _documentArea = new Size(0, 0);
            _renderedPages.Clear();
            _onTouchDownPage = null;
        }

        #endregion Private methods
    }
}
