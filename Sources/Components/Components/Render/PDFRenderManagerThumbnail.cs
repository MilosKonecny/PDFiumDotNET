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
    /// The class <see cref="PDFRenderManagerThumbnail"/> is derived from abstract <see cref="PDFRenderManager"/> class
    /// and implements specific render functionality.
    /// </summary>
    /// <remarks>ToDo: It appears that this class is identical to the <see cref="PDFRenderManagerStandard"/> class.</remarks>
    internal class PDFRenderManagerThumbnail : PDFRenderManager
    {
        #region Private fields

        private PDFSize<double> _requiredDocumentArea = default;

        #endregion Private fields

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

            // Determine height of all pages and widest page.
            var width = 0d;
            var height = 0d;
            foreach (var page in PageComponent.Pages)
            {
                height += page.Height * zoomFactor;
                if (width < page.Width * zoomFactor)
                {
                    width = page.Width * zoomFactor;
                }
            }

            // Add margins
            height += PageMargin.Height * (PageComponent.PageCount + 1);
            width += 2 * PageMargin.Width;

            // Set document area
            _requiredDocumentArea = new PDFSize<double>(width, height);
        }

        #endregion Internal override methods

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

            // Determine height of all pages above this page
            // and center this page on document area width.
            var top = PageMargin.Height;
            for (var index = 0; index < pageIndex; index++)
            {
                top += PageMargin.Height + (PageComponent.Pages[index].Height * zoomFactor);
            }

            return new PDFRectangle<double>(
                (_requiredDocumentArea.Width - PageComponent.Pages[pageIndex].Width) / 2d,
                top,
                PageComponent.Pages[pageIndex].Width * zoomFactor,
                PageComponent.Pages[pageIndex].Height * zoomFactor);
        }

        /// <inheritdoc/>
        protected override IPDFRenderInfo GetPagesToRender(PDFRectangle<double> viewportArea)
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

            foreach (var page in PageComponent.Pages)
            {
                var pageTop = top;
                var pageBottom = top + (page.Height * zoomFactor);
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

                // Page position in the viewport area relative to the top left point of the viewport area.
                var relativePositionInViewportArea = new PDFRectangle<double>(
                    horizontalOffset - viewportArea.Left + ((DocumentArea.Width - (page.Width * zoomFactor)) / 2d),
                    pageTop - viewportArea.Top,
                    page.Width * zoomFactor,
                    page.Height * zoomFactor);

                // 'Move' viewport area rectangle to page coordinate system to compute visible part of page in viewport area.
                var viewportInPageCoordinates = new PDFRectangle<double>(-relativePositionInViewportArea.X, -relativePositionInViewportArea.Y, viewportArea.Width, viewportArea.Height);

                // Intersection is the visible part of page
                var visiblePart = viewportInPageCoordinates.Intersect(new PDFRectangle<double>(0, 0, relativePositionInViewportArea.Width, relativePositionInViewportArea.Height));

                // Special behavior. Width and height have to be at least 1.
                visiblePart.Width = Math.Max(1d, visiblePart.Width);
                visiblePart.Height = Math.Max(1d, visiblePart.Height);

                // Create page render info
                var pageInfo = new PDFPageRenderInfo(page)
                {
                    PositionInDocumentArea = new PDFRectangle<double>(
                        (DocumentArea.Width - (page.Width * zoomFactor)) / 2d,
                        pageTop,
                        page.Width * zoomFactor,
                        page.Height * zoomFactor),
                    RelativePositionInViewportArea = relativePositionInViewportArea,
                    VisiblePart = visiblePart,
                    VisiblePartInViewportArea = relativePositionInViewportArea.Intersect(new PDFRectangle<double>(0, 0, viewportArea.Width, viewportArea.Height)),
                    IsClosestToCenter = false,
                    PageRow = page.PageIndex,
                    PageColumn = 0,
                };

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

            if (nearestPageToCenter != null)
            {
                nearestPageToCenter.IsClosestToCenter = true;
            }

            info.PagesToRender = list;
            return info;
        }

        /// <inheritdoc/>
        protected override double GetHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                return 0d;
            }

            var margins = PageMargin.Width;

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
