﻿#if WpfControls
namespace PDFiumDotNET.WpfControls
#else
namespace PDFiumDotNET.WpfCoreControls
#endif
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts.Adapters;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// View class shows page thumbnails from opened PDF document.
    /// </summary>
    public partial class PDFThumbnailView : Control, IScrollInfo
    {
        #region Private consts

        private const double _thumbnailZoomFactor = 1d;

        #endregion Private consts

        #region Private fields

        /// <summary>
        /// List of rendered pages. Used to examine click, touch, ...
        /// </summary>
        private List<IPDFPageRenderInfo> _renderedPages = new List<IPDFPageRenderInfo>();

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
        private Size _workArea = new Size(0, 0);

        /// <summary>
        /// The area visible to user.
        /// </summary>
        private Size _viewport = new Size(0, 0);

        /// <summary>
        /// Vertical offset at manipulation start.
        /// </summary>
        private double _startManipulationVerticalOffset;

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
        /// Initializes a new instance of the <see cref="PDFView"/> class.
        /// </summary>
        public PDFThumbnailView()
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
            if (drawingContext == null)
            {
                return;
            }

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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            _startManipulationVerticalOffset = VerticalOffset;

            base.OnManipulationStarted(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e != null && e.CumulativeManipulation != null)
            {
                VerticalOffset = _startManipulationVerticalOffset - e.CumulativeManipulation.Translation.Y;
            }

            base.OnManipulationDelta(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (e != null)
            {
                var point = e.GetPosition(this);
                var page = _renderedPages.FirstOrDefault(p => point.X > p.Left && point.X < p.Right && point.Y > p.Top && point.Y < p.Bottom);
                if (page != null)
                {
                    PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1);
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);

            if (e != null)
            {
                var point = e.GetTouchPoint(this).Position;
                _onTouchDownPage = _renderedPages.FirstOrDefault(p => point.X > p.Left && point.X < p.Right && point.Y > p.Top && point.Y < p.Bottom);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            if (e != null)
            {
                var point = e.GetTouchPoint(this).Position;
                var page = _renderedPages.FirstOrDefault(p => point.X > p.Left && point.X < p.Right && point.Y > p.Top && point.Y < p.Bottom);
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
        /// <param name="availableSize">Available size based on current layout of application.</param>
        /// <returns>Required size to show all pages of opened</returns>
        private Size DeterminePageArea(Size availableSize)
        {
            if (PDFPageComponent == null
                || PDFPageComponent.PageCount == 0)
            {
                return availableSize;
            }

            var width = availableSize.Width;
            var height = availableSize.Height;
            PDFPageComponent[PageLayoutType.Thumbnail].DetermineArea(ref width, ref height, 2d * FontSize, _thumbnailZoomFactor);
            return new Size(width, height);
        }

        /// <summary>
        /// Reset all relevat status fields.
        /// </summary>
        private void ResetStatus()
        {
            _verticalOffset = 0d;
            _workArea = new Size(0, 0);
            _renderedPages.Clear();
            _onTouchDownPage = null;
        }

        #endregion Private methods
    }
}
