﻿namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.WpfControls.Helper;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFView : PDFViewBase
    {
        #region Private fields

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
        }

        #endregion Constructors

        #region Protected override properties

        /// <inheritdoc/>
        protected override IPDFPageComponent ControlPDFPageComponent => PDFPageComponent;

        /// <inheritdoc/>
        protected override bool IsZoomSupported => true;

        #endregion Protected override properties

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
                    Math.Min(HorizontalOffset, DocumentArea.Width - ViewportArea.Width),
                    Math.Min(VerticalOffset, DocumentArea.Height - ViewportArea.Height),
                    false);

                // Determine pages to draw.
                var viewportInDocument = new PDFRectangle<double>(HorizontalOffset, VerticalOffset, ViewportWidth, ViewportHeight);
                RenderInformation = PDFPageComponent.RenderManager.DetermineRenderInfo(viewportInDocument);
            }

            RenderPages(drawingContext);

            if (RenderInformation?.PagesToRender != null && ActivatePageInCenter)
            {
                var pageInfo = RenderInformation.PagesToRender.FirstOrDefault(pageInfo => pageInfo.IsClosestToCenter);
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
            }
        }

        /// <inheritdoc/>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (e != null)
            {
                var point = e.GetPosition(this);
                if (Distance(_startDragPoint, point) < 5)
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
                        PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1, true);
                    }
                }
            }

            _startDragPoint = new Point(-1, -1);
        }

        /// <inheritdoc/>
        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);

            if (e != null && RenderInformation?.PagesToRender != null)
            {
                _startDragPoint = e.GetTouchPoint(this).Position;
            }
        }

        /// <inheritdoc/>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            if (e != null && RenderInformation?.PagesToRender != null)
            {
                var point = e.GetTouchPoint(this).Position;
                if (Distance(_startDragPoint, point) < 5)
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
                        PDFPageComponent.NavigateToPage(page.Page.PageIndex + 1, true);
                    }
                }
            }

            _startDragPoint = new Point(-1, -1);
        }

        #endregion Protected override methods

        #region Private methods

        private IPDFPageRenderInfo PageFromPoint(Point point)
        {
            return RenderInformation.PagesToRender.FirstOrDefault(p =>
            {
                return point.X > p.RelativePositionInViewportArea.Left
                && point.X < p.RelativePositionInViewportArea.Right
                && point.Y > p.RelativePositionInViewportArea.Top &&
                point.Y < p.RelativePositionInViewportArea.Bottom;
            });
        }

        private IPDFLink GetLinkFromPoint(Point point)
        {
            if (RenderInformation == null || RenderInformation.PagesToRender == null)
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
                HorizontalOffset = horizontalOffset;
                VerticalOffset = verticalOffset;

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
