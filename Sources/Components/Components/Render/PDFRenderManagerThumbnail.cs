namespace PDFiumDotNET.Components.Render
{
    using System;
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// The class <see cref="PDFRenderManagerThumbnail"/> is derived from abstract <see cref="PDFRenderManager"/> class
    /// and implements specific render functionality.
    /// </summary>
    internal class PDFRenderManagerThumbnail : PDFRenderManager
    {
        #region Private fields

        private PDFSize<double> _requiredDocumentArea = default;

        #endregion Private fields

        #region Internal override methods

        /// <inheritdoc/>
        internal override PDFSize<double> RequiredDocumentArea => _requiredDocumentArea;

        /// <inheritdoc/>
        internal override void CalculateDocumentArea()
        {
            if (PageComponent.PageCount == 0)
            {
                _requiredDocumentArea = new PDFSize<double>(Margin);
                return;
            }

            // Determine height of all pages and widest page.
            var width = 0d;
            var height = 0d;
            foreach (var page in PageComponent.Pages)
            {
                height += page.Height * PageComponent.ZoomComponent.CurrentZoomFactor;
                if (width < page.Width * PageComponent.ZoomComponent.CurrentZoomFactor)
                {
                    width = page.Width * PageComponent.ZoomComponent.CurrentZoomFactor;
                }
            }

            // Add margins
            height += Margin.Height * (PageComponent.PageCount + 1);
            width += 2 * Margin.Width;

            // Set document area
            _requiredDocumentArea = new PDFSize<double>(width, height);
        }

        /// <inheritdoc/>
        internal override PDFRectangle<double> GetPagePosition(int pageIndex)
        {
            // Determine height of all pages above this page
            // and center this page on document area width.
            var top = Margin.Height;
            for (var index = 0; index < pageIndex; index++)
            {
                top += Margin.Height + PageComponent.Pages[index].Height;
            }

            return new PDFRectangle<double>(
                (_requiredDocumentArea.Width - PageComponent.Pages[pageIndex].Width) / 2d,
                top,
                PageComponent.Pages[pageIndex].Width,
                PageComponent.Pages[pageIndex].Height);
        }

        /// <inheritdoc/>
        internal override IList<IPDFPageRenderInfo> GetPagesToRender(PDFRectangle<double> viewportArea)
        {
            // It should be mentioned. A viewport is a rectangle in the document area.
            var list = new List<IPDFPageRenderInfo>();

            var top = Margin.Height;

            var viewportWidthIsWider = viewportArea.Width > DocumentArea.Width;
            var horizontalOffset = viewportWidthIsWider ? (viewportArea.Width - DocumentArea.Width) / 2d : 0d;

            PDFPageRenderInfo nearestPageToCenter = null;
            var distanceToCenter = double.MaxValue;

            foreach (var page in PageComponent.Pages)
            {
                var pageTop = top;
                var pageBottom = top + (page.Height * PageComponent.ZoomComponent.CurrentZoomFactor);
                top = pageBottom + Margin.Height;

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
                    horizontalOffset - viewportArea.Left + ((DocumentArea.Width - (page.Width * PageComponent.ZoomComponent.CurrentZoomFactor)) / 2d),
                    pageTop - viewportArea.Top,
                    page.Width * PageComponent.ZoomComponent.CurrentZoomFactor,
                    page.Height * PageComponent.ZoomComponent.CurrentZoomFactor);

                // 'Move' viewport area rectangle to page coordinate system to compute visible part of page in viewport area.
                var viewportInPageCoordinates = new PDFRectangle<double>(-relativePositionInViewportArea.X, -relativePositionInViewportArea.Y, viewportArea.Width, viewportArea.Height);

                // Intersection is the visible part of page
                var visiblePart = viewportInPageCoordinates.Intersect(new PDFRectangle<double>(0, 0, relativePositionInViewportArea.Width, relativePositionInViewportArea.Height));

                // Special behavior. Width and height have to be at least 1.
                visiblePart.Width = Math.Max(1d, visiblePart.Width);
                visiblePart.Height = Math.Max(1d, visiblePart.Height);

                // Create page render info
                var info = new PDFPageRenderInfo(page)
                {
                    PositionInDocumentArea = new PDFRectangle<double>(
                        (DocumentArea.Width - (page.Width * PageComponent.ZoomComponent.CurrentZoomFactor)) / 2d,
                        pageTop,
                        page.Width * PageComponent.ZoomComponent.CurrentZoomFactor,
                        page.Height * PageComponent.ZoomComponent.CurrentZoomFactor),
                    RelativePositionInViewportArea = relativePositionInViewportArea,
                    VisiblePart = visiblePart,
                    VisiblePartInViewportArea = relativePositionInViewportArea.Intersect(new PDFRectangle<double>(0, 0, viewportArea.Width, viewportArea.Height)),
                    IsNearestToCenter = false,
                };

                // Compute distance of this page to the center of viewport.
                var x = (viewportArea.Width / 2d) - ((relativePositionInViewportArea.Left + relativePositionInViewportArea.Right) / 2);
                var y = (viewportArea.Height / 2d) - ((relativePositionInViewportArea.Top + relativePositionInViewportArea.Bottom) / 2);
                var distance = Math.Sqrt((x * x) + (y * y));

                if (distance < distanceToCenter)
                {
                    distanceToCenter = distance;
                    nearestPageToCenter = info;
                }

                list.Add(info);
            }

            if (nearestPageToCenter != null)
            {
                nearestPageToCenter.IsNearestToCenter = true;
            }

            return list;
        }

        #endregion Internal override methods
    }
}
