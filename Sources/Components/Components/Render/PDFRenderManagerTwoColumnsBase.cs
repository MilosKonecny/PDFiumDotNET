namespace PDFiumDotNET.Components.Render
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// The class <see cref="PDFRenderManagerTwoColumnsBase"/> is derived from abstract <see cref="PDFRenderManager"/> class
    /// and it is used as basis class for classes <see cref="PDFRenderManagerTwoColumns1"/> and <see cref="PDFRenderManagerTwoColumns2"/>.
    /// </summary>
    internal abstract class PDFRenderManagerTwoColumnsBase : PDFRenderManager
    {
        #region Private fields

        private readonly bool _isOnePageInFirstRow;
        private PDFSize<double> _requiredDocumentArea = default;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRenderManagerTwoColumnsBase"/> class.
        /// </summary>
        /// <param name="isOnePageInFirstRow"><c>true</c> when the first row contains only one page; otherwise <c>false</c>.</param>
        protected PDFRenderManagerTwoColumnsBase(bool isOnePageInFirstRow)
        {
            _isOnePageInFirstRow = isOnePageInFirstRow;
        }

        #endregion Constructors

        #region Protected abstract properties

        /// <summary>
        /// Gets the count of page rows of PDF document to draw.
        /// </summary>
        protected abstract int PageRowCount { get; }

        #endregion Protected abstract properties

        #region Internal override methods

        /// <inheritdoc/>
        internal override void CalculateDocumentArea()
        {
            if (PageComponent == null || PageComponent.PageCount == 0)
            {
                _requiredDocumentArea = new PDFSize<double>(PageMargin);
                return;
            }

            var zoomFactor = PageComponent.ZoomComponent != null ? PageComponent.ZoomComponent.CurrentZoomFactor : 1d;
            WidestPageRow = 0d;

            // Determine height of all pages and widest row.
            var documentWidth = 0d;
            var documentHeight = 0d;
            var rows = 0;
            for (var index = 0; index < PageRowCount; index++)
            {
                var leftPage = LeftPageInRow(index);
                var rightPage = RightPageInRow(index);
                var leftPageHeight = leftPage == null ? 0d : leftPage.Height * zoomFactor;
                var leftPageWidth = leftPage == null ? 0d : leftPage.Width * zoomFactor;
                var rightPageHeight = rightPage == null ? 0d : rightPage.Height * zoomFactor;
                var rightPageWidth = rightPage == null ? 0d : rightPage.Width * zoomFactor;

                var rowWidth = PageMargin.Width + (2d * Math.Max(leftPageWidth, rightPageWidth));
                var pageRowWidth = (leftPage == null ? 0d : leftPage.Width) + PageMargin.Width + (rightPage == null ? 0d : rightPage.Width);

                documentHeight += Math.Max(leftPageHeight, rightPageHeight);
                if (documentWidth < rowWidth)
                {
                    documentWidth = rowWidth;
                }

                if (pageRowWidth > WidestPageRow)
                {
                    WidestPageRow = pageRowWidth;
                }

                rows++;
            }

            // Add margins
            documentHeight += PageMargin.Height * (rows + 1);
            documentWidth += 2 * PageMargin.Width;
            WidestPageRow = WidestPageRow + (2 * PageMargin.Width);

            // Set document area
            _requiredDocumentArea = new PDFSize<double>(documentWidth, documentHeight);
        }

        #endregion Internal override methods

        #region Protected abstract methods

        /// <summary>
        /// Gets the left page in row.
        /// </summary>
        /// <param name="row">Row to get the page from.</param>
        /// <returns>Required page, or <c>null</c> if there is no page.</returns>
        protected abstract IPDFPage LeftPageInRow(int row);

        /// <summary>
        /// Gets the right page in row.
        /// </summary>
        /// <param name="row">Row to get the page from.</param>
        /// <returns>Required page, or <c>null</c> if there is no page.</returns>
        protected abstract IPDFPage RightPageInRow(int row);

        #endregion Protected abstract methods

        #region Protected override methods

        /// <inheritdoc/>
        protected override PDFSize<double> RequiredDocumentArea => _requiredDocumentArea;

        /// <inheritdoc/>
        protected override PDFRectangle<double> GetPagePosition(int pageIndex)
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
        protected override IPDFRenderInfo GetPagesToRender(PDFRectangle<double> viewportArea)
        {
            // It should be mentioned. A viewport is a rectangle in the document area.
            var renderInfo = new PDFRenderInfo
            {
                ZoomFactor = PageComponent?.ZoomComponent != null ? PageComponent.ZoomComponent.CurrentZoomFactor : 1d,
                ViewportArea = viewportArea,
                PagesToRender = new List<IPDFPageRenderInfo>(),
            };

            if (PageComponent == null || PageComponent.PageCount == 0)
            {
                return renderInfo;
            }

            var zoomFactor = renderInfo.ZoomFactor;
            var top = PageMargin.Height;
            var viewportWidthIsWider = renderInfo.ViewportArea.Width > DocumentArea.Width;
            var horizontalOffset = viewportWidthIsWider ? (renderInfo.ViewportArea.Width - DocumentArea.Width) / 2d : 0d;
            var list = new List<IPDFPageRenderInfo>();

            PDFPageRenderInfo nearestPageToCenter = null;
            var distanceToCenter = double.MaxValue;

            for (var rowIndex = 0; rowIndex < PageRowCount; rowIndex++)
            {
                var leftPage = LeftPageInRow(rowIndex);
                var rightPage = RightPageInRow(rowIndex);
                var leftPageHeight = leftPage == null ? 0d : leftPage.Height * zoomFactor;
                var leftPageWidth = leftPage == null ? 0d : leftPage.Width * zoomFactor;
                var rightPageHeight = rightPage == null ? 0d : rightPage.Height * zoomFactor;
                var rightPageWidth = rightPage == null ? 0d : rightPage.Width * zoomFactor;

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

                if (leftPage != null)
                {
                    // Get width and height of left/right page.
                    var pageWidth = leftPageWidth;
                    var pageHeight = leftPageHeight;
                    var variablePart = -(PageMargin.Width / 2d) - pageWidth;

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
                    var pageInfo = new PDFPageRenderInfo(leftPage)
                    {
                        PositionInDocumentArea = new PDFRectangle<double>(
                            (DocumentArea.Width - pageWidth) / 2d,
                            pageTop,
                            pageWidth,
                            pageHeight),
                        RelativePositionInViewportArea = relativePositionInViewportArea,
                        VisiblePart = visiblePart,
                        VisiblePartInViewportArea = relativePositionInViewportArea.Intersect(new PDFRectangle<double>(0, 0, viewportArea.Width, viewportArea.Height)),
                        IsClosestToCenter = false,
                        PageRow = rowIndex,
                        PageColumn = 0,
                    };

                    if (!pageInfo.VisiblePartInViewportArea.IsEmpty)
                    {
                        // It is visible.
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

                if (rightPage != null)
                {
                    // Get width and height of left/right page.
                    var pageWidth = rightPageWidth;
                    var pageHeight = rightPageHeight;
                    var variablePart = PageMargin.Width / 2d;

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
                    var pageInfo = new PDFPageRenderInfo(rightPage)
                    {
                        PositionInDocumentArea = new PDFRectangle<double>(
                            (DocumentArea.Width - pageWidth) / 2d,
                            pageTop,
                            pageWidth,
                            pageHeight),
                        RelativePositionInViewportArea = relativePositionInViewportArea,
                        VisiblePart = visiblePart,
                        VisiblePartInViewportArea = relativePositionInViewportArea.Intersect(new PDFRectangle<double>(0, 0, viewportArea.Width, viewportArea.Height)),
                        IsClosestToCenter = false,
                        PageRow = rowIndex,
                        PageColumn = 1,
                    };

                    if (!pageInfo.VisiblePartInViewportArea.IsEmpty)
                    {
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
            }

            if (nearestPageToCenter != null)
            {
                nearestPageToCenter.IsClosestToCenter = true;
            }

            renderInfo.PagesToRender = list;

            return renderInfo;
        }

        /// <inheritdoc/>
        protected override double GetHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                return 0d;
            }

            var pageClosestToCenter = renderInfo.PagesToRender.FirstOrDefault(page => page.IsClosestToCenter);
            var pageColumn = pageClosestToCenter == null ? 0 : pageClosestToCenter.PageColumn;

            var margins = (pageColumn + 1) * PageMargin.Width;

            var newHorizontalOffset = renderInfo.ViewportArea.Left - margins;
            newHorizontalOffset += renderInfo.ViewportArea.Width / 2;

            newHorizontalOffset /= renderInfo.ZoomFactor;
            newHorizontalOffset *= newZoomFactor;

            newHorizontalOffset -= renderInfo.ViewportArea.Width / 2;
            newHorizontalOffset += margins;

            return newHorizontalOffset;
        }

        /// <inheritdoc/>
        protected override double GetVerticalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                return 0d;
            }

            var pageClosestToCenter = renderInfo.PagesToRender.FirstOrDefault(page => page.IsClosestToCenter);
            var pageRow = pageClosestToCenter == null ? 0 : pageClosestToCenter.PageRow;

            var margins = (pageRow + 1) * PageMargin.Height;

            var newVerticalOffset = renderInfo.ViewportArea.Top - margins;
            newVerticalOffset += renderInfo.ViewportArea.Height / 2;

            newVerticalOffset /= renderInfo.ZoomFactor;
            newVerticalOffset *= newZoomFactor;

            newVerticalOffset -= renderInfo.ViewportArea.Height / 2;
            newVerticalOffset += margins;

            return newVerticalOffset;
        }

        #endregion Protected override methods
    }
}
