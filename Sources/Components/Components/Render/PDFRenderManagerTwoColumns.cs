namespace PDFiumDotNET.Components.Render
{
    using System;
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// The class <see cref="PDFRenderManagerTwoColumns"/> is derived from abstract <see cref="PDFRenderManager"/> class
    /// and implements specific render functionality.
    /// </summary>
    /// <remarks>Pages 0, 2, 4, 6, ... are on the left hand side. Pages 1, 3, 5, 7, ... on the right.</remarks>
    internal class PDFRenderManagerTwoColumns : PDFRenderManager
    {
        #region Private fields

        private readonly bool _isOnePageInFirstRow;
        private PDFSize<double> _requiredDocumentArea = default;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRenderManagerTwoColumns"/> class.
        /// </summary>
        /// <param name="isOnePageInFirstRow"><c>true</c> when the first row contains only one page; otherwise <c>false</c>.</param>
        public PDFRenderManagerTwoColumns(bool isOnePageInFirstRow)
        {
            _isOnePageInFirstRow = isOnePageInFirstRow;
        }

        #endregion Constructors

        #region Internal override methods

        /// <inheritdoc/>
        internal override PDFSize<double> RequiredDocumentArea => _requiredDocumentArea;

        /// <inheritdoc/>
        internal override void CalculateDocumentArea()
        {
            if (PageComponent == null || PageComponent.PageCount == 0)
            {
                _requiredDocumentArea = new PDFSize<double>(PageMargin);
                return;
            }

            var zoomFactor = PageComponent.ZoomComponent != null ? PageComponent.ZoomComponent.CurrentZoomFactor : 1d;

            // Determine height of all pages and widest page.
            var width = 0d;
            var height = 0d;
            var rows = 0;
            for (var index = 0; index < PageComponent.PageCount; index++, index++)
            {
                if (_isOnePageInFirstRow && index == 2)
                {
                    // Correction for first row with one page.
                    index--;
                }

                var leftPageHeight = PageComponent.Pages[index].Height * zoomFactor;
                var leftPageWidth = PageComponent.Pages[index].Width * zoomFactor;
                var rightPageHeight = ((_isOnePageInFirstRow && index == 0) || (index + 1 >= PageComponent.PageCount) ? 0 : PageComponent.Pages[index + 1].Height) * zoomFactor;
                var rightPageWidth = ((_isOnePageInFirstRow && index == 0) || (index + 1 >= PageComponent.PageCount) ? 0 : PageComponent.Pages[index + 1].Width) * zoomFactor;

                var bothPagesWidth = PageMargin.Width + (2d * Math.Max(leftPageWidth, rightPageWidth));

                height += Math.Max(leftPageHeight, rightPageHeight);
                if (width < bothPagesWidth)
                {
                    width = bothPagesWidth;
                }

                rows++;
            }

            // Add margins
            height += PageMargin.Height * (rows + 1);
            width += 2 * PageMargin.Width;

            // Set document area
            _requiredDocumentArea = new PDFSize<double>(width, height);
        }

        /// <inheritdoc/>
        internal override PDFRectangle<double> GetPagePosition(int pageIndex)
        {
            if (PageComponent == null || PageComponent.PageCount == 0)
            {
                return new PDFRectangle<double>();
            }

            var zoomFactor = PageComponent.ZoomComponent != null ? PageComponent.ZoomComponent.CurrentZoomFactor : 1d;

            // Determine the height of all pages above this page
            // and set the page position to the left or right of the center of the document area width.
            var pageRow = _isOnePageInFirstRow && pageIndex > 0 ? (pageIndex + 1) / 2 : pageIndex / 2;
            var top = PageMargin.Height;
            for (var rowIndex = 0; rowIndex < pageRow; rowIndex++)
            {
                var leftPageHeight = PageComponent.Pages[rowIndex * 2].Height * zoomFactor;
                var rightPageHeight = PageComponent.Pages[(rowIndex * 2) + 1].Height * zoomFactor;
                top += PageMargin.Height + Math.Max(leftPageHeight, rightPageHeight);
            }

            // Left for the pages on the right.
            var left = (_requiredDocumentArea.Width / 2d) + (PageMargin.Width / 2d);
            if (_isOnePageInFirstRow && pageIndex > 0 ? ((pageIndex + 1) % 2 == 0) : (pageIndex % 2 == 0))
            {
                // Left for the pages on the left.
                left = (_requiredDocumentArea.Width / 2d) - (PageMargin.Width / 2d) - PageComponent.Pages[pageIndex].Width;
            }

            return new PDFRectangle<double>(
                left,
                top,
                PageComponent.Pages[pageIndex].Width * zoomFactor,
                PageComponent.Pages[pageIndex].Height * zoomFactor);
        }

        /// <inheritdoc/>
        internal override IPDFRenderInfo GetPagesToRender(PDFRectangle<double> viewportArea)
        {
            // It should be mentioned. A viewport is a rectangle in the document area.
            var info = new PDFRenderInfo
            {
                ZoomFactor = PageComponent?.ZoomComponent != null ? PageComponent.ZoomComponent.CurrentZoomFactor : 1d,
                ViewportArea = viewportArea,
                PagesToRender = new List<IPDFPageRenderInfo>(),
            };

            if (PageComponent == null || PageComponent.PageCount == 0)
            {
                return info;
            }

            var zoomFactor = info.ZoomFactor;
            var top = PageMargin.Height;
            var viewportWidthIsWider = viewportArea.Width > DocumentArea.Width;
            var horizontalOffset = viewportWidthIsWider ? (viewportArea.Width - DocumentArea.Width) / 2d : 0d;
            var list = new List<IPDFPageRenderInfo>();

            PDFPageRenderInfo nearestPageToCenter = null;
            var distanceToCenter = double.MaxValue;

            for (var index = 0; index < PageComponent.PageCount; index++, index++)
            {
                if (_isOnePageInFirstRow && index == 2)
                {
                    // Correction for first row with one page.
                    index--;
                }

                var leftPage = PageComponent.Pages[index];
                var rightPage = ((_isOnePageInFirstRow && index == 0) || (index + 1 >= PageComponent.PageCount)) ? null : PageComponent.Pages[index + 1];
                var leftPageHeight = leftPage.Height * zoomFactor;
                var leftPageWidth = leftPage.Width * zoomFactor;
                var rightPageHeight = rightPage != null ? rightPage.Height * zoomFactor : 0d;
                var rightPageWidth = rightPage != null ? rightPage.Width * zoomFactor : 0d;

                var pageTop = top;
                var pageBottom = top + Math.Max(leftPageHeight, rightPageHeight);
                top = pageBottom + PageMargin.Height;

                if (pageTop > viewportArea.Bottom)
                {
                    // This page is bellow viewport.
                    break;
                }

                if (pageBottom < viewportArea.Top)
                {
                    // This page is above viewport.
                    continue;
                }

                for (var lr = 0; lr < 2; lr++)
                {
                    if (lr > 0 && rightPage == null)
                    {
                        break;
                    }

                    // Get width and height of left/right page.
                    var pageWidth = lr == 0 ? leftPageWidth : rightPageWidth;
                    var pageHeight = lr == 0 ? leftPageHeight : rightPageHeight;
                    var variablePart = lr == 0 ? -(PageMargin.Width / 2d) - pageWidth : PageMargin.Width / 2d;

                    // Page position in the viewport area relative to the top left point of the viewport area.
                    var relativePositionInViewportArea = new PDFRectangle<double>(
                        horizontalOffset - viewportArea.Left + (DocumentArea.Width / 2d) + variablePart,
                        pageTop - viewportArea.Top,
                        pageWidth,
                        pageHeight);

                    // 'Move' viewport area rectangle to page coordinate system to compute visible part of page in viewport area.
                    var viewportInPageCoordinates = new PDFRectangle<double>(-relativePositionInViewportArea.X, -relativePositionInViewportArea.Y, viewportArea.Width, viewportArea.Height);

                    // Intersection is the visible part of page
                    var visiblePart = viewportInPageCoordinates.Intersect(new PDFRectangle<double>(0, 0, relativePositionInViewportArea.Width, relativePositionInViewportArea.Height));

                    // Special behavior. Width and height have to be at least 1.
                    visiblePart.Width = Math.Max(1d, visiblePart.Width);
                    visiblePart.Height = Math.Max(1d, visiblePart.Height);

                    // Create page render info
                    var pageInfo = new PDFPageRenderInfo(lr == 0 ? leftPage : rightPage)
                    {
                        PositionInDocumentArea = new PDFRectangle<double>(
                            (DocumentArea.Width - pageWidth) / 2d,
                            pageTop,
                            pageWidth,
                            pageHeight),
                        RelativePositionInViewportArea = relativePositionInViewportArea,
                        VisiblePart = visiblePart,
                        VisiblePartInViewportArea = relativePositionInViewportArea.Intersect(new PDFRectangle<double>(0, 0, viewportArea.Width, viewportArea.Height)),
                        IsNearestToCenter = false,
                    };

                    if (pageInfo.VisiblePartInViewportArea.IsEmpty)
                    {
                        // It is not visible.
                        continue;
                    }

                    // Compute distance of this page to the center of viewport.
                    var x = (viewportArea.Width / 2d) - ((relativePositionInViewportArea.Left + relativePositionInViewportArea.Right) / 2);
                    var y = (viewportArea.Height / 2d) - ((relativePositionInViewportArea.Top + relativePositionInViewportArea.Bottom) / 2);
                    var distance = Math.Sqrt((x * x) + (y * y));

                    if (distance < distanceToCenter)
                    {
                        distanceToCenter = distance;
                        nearestPageToCenter = pageInfo;
                    }

                    list.Add(pageInfo);
                }
            }

            if (nearestPageToCenter != null)
            {
                nearestPageToCenter.IsNearestToCenter = true;
            }

            info.PagesToRender = list;
            return info;
        }

        /// <inheritdoc/>
        internal override double GetHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                throw new ArgumentNullException(nameof(renderInfo));
            }

            return renderInfo.ViewportArea.X;
        }

        /// <inheritdoc/>
        internal override double GetVerticalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                throw new ArgumentNullException(nameof(renderInfo));
            }

            return renderInfo.ViewportArea.X;
        }

        #endregion Internal override methods
    }
}
