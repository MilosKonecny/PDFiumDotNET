namespace PDFiumDotNET.Components.Page
{
    using System;
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <inheritdoc cref="IPDFPageComponent"/>
    internal sealed partial class PDFPageComponent
    {
        #region Private methods

        /// <summary>
        /// Initializes layout.
        /// </summary>
        private void InitializeLayout()
        {
            for (var index = 0; index < PageCount; index++)
            {
                var page = Pages[index];

                CumulativeHeight += page.Height;
                if (page.Width > WidestGridCellWidth)
                {
                    WidestGridCellWidth = page.Width;
                }

                if (page.Height > HighestGridCellHeight)
                {
                    HighestGridCellHeight = page.Height;
                }
            }
        }

        /// <summary>
        /// Determine required size of area where fit all pages.
        /// </summary>
        /// <param name="width">In: available width. Out: required width.</param>
        /// <param name="height">In: available height. Out: required height.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for the computing of required area.</param>
        private void DetermineAreaVirtual(ref double width, ref double height, double pageMargin, double zoomFactor)
        {
            if (PageCount == 0)
            {
                return;
            }

            var newWidth = WidestGridCellWidth * zoomFactor;
            var newHeight = CumulativeHeight * zoomFactor;
            var marginWidth = 2d * pageMargin;
            var marginHeight = (PageCount + 1) * pageMargin;
            width = Math.Round(newWidth + marginWidth, 2);
            height = Math.Round(newHeight + marginHeight, 2);
        }

        /// <summary>
        /// Gets all pages to be drawn in specified region.
        /// </summary>
        /// <param name="topLine">Top line of viewport.</param>
        /// <param name="bottomLine">Bottom line of viewport.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <param name="setCurrentPageIndex">If <c>true</c>, page on middle of height will be set as current page.</param>
        /// <returns>Pages to draw in required region.</returns>
        private IList<IPDFPageRenderInfo> DeterminePagesToRenderVirtual(double topLine, double bottomLine, double pageMargin, double zoomFactor, bool setCurrentPageIndex = false)
        {
            // List of all pages to render.
            var list = new List<IPDFPageRenderInfo>();

            // Current position on Y-axis.
            var currentPositionOnY = pageMargin;

            // Center line on viewport
            var centerLine = (topLine + bottomLine) / 2;
            var isCenterSet = false;

            // Iterate through all pages.
            foreach (var page in Pages)
            {
                // Filter out all pages above top line.
                if (currentPositionOnY + (page.Height * zoomFactor) < topLine)
                {
                    // Page is above top line.
                    // Adjust the current position on y and continue.
                    currentPositionOnY += pageMargin + (page.Height * zoomFactor);
                    continue;
                }

                // Filter out all pages below bottom line.
                if (currentPositionOnY > bottomLine)
                {
                    // Page is below bottom line.
                    // End foreach.
                    break;
                }

                // Part of this page is between top and bottom line.
                // Add this page to the list.
                var pageToAdd = new PDFPageRenderInfo(page)
                {
                    Left = 0,
                    Right = page.Width * zoomFactor,
                    Top = currentPositionOnY,
                    Bottom = currentPositionOnY + (page.Height * zoomFactor),
                };

                // Check if the middle point of height of viewport is over this page.
                if (pageToAdd.Top < centerLine && pageToAdd.Bottom > centerLine)
                {
                    // Middle point of height of viewport is over this page.
                    // Store thid information to use it later during zoom manipulation.
                    pageToAdd.IsOnCenter = true;
                    pageToAdd.PagePositionOnCenter = (centerLine - pageToAdd.Top) / (pageToAdd.Bottom - pageToAdd.Top);
                    isCenterSet = true;
                    if (setCurrentPageIndex)
                    {
                        SetCurrentInformation(pageToAdd.Page);
                    }
                }

                // Special behaviour. Middle point of height of viewport may be between to pages.
                // In this case use current page to add.
                if (!isCenterSet && pageToAdd.Top > centerLine)
                {
                    pageToAdd.IsOnCenter = true;
                    pageToAdd.PagePositionOnCenter = 0d;
                    isCenterSet = true;
                    if (setCurrentPageIndex)
                    {
                        SetCurrentInformation(pageToAdd.Page);
                    }
                }

                // Add the page to the list.
                list.Add(pageToAdd);

                // Adjust current position on y.
                currentPositionOnY += pageMargin + (page.Height * zoomFactor);
            }

            return list;
        }

        /// <summary>
        /// Gets all pages to be drawn in specified region.
        /// </summary>
        /// <param name="pageOnCenter">The page on which is center of vertical direction of the viewport.</param>
        /// <param name="topLine">Top line of viewport.</param>
        /// <param name="bottomLine">Bottom line of viewport.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <returns>Pages to draw in required region.</returns>
        private IList<IPDFPageRenderInfo> DeterminePagesToRenderVirtual(IPDFPageRenderInfo pageOnCenter, ref double topLine, ref double bottomLine, double pageMargin, double zoomFactor)
        {
            // Base check.
            if (pageOnCenter == null
                || !pageOnCenter.IsOnCenter
                || pageOnCenter.Page == null
                || pageOnCenter.Page.PageIndex >= PageCount)
            {
                // We don't have required information.
                return DeterminePagesToRenderVirtual(topLine, bottomLine, pageMargin, zoomFactor);
            }

            // Height of viewport.
            var height = bottomLine - topLine;

            // Let's say, the middle point of height is on position 0. Create virtual top line.
            var virtualTopLine = -1d * (bottomLine - topLine) / 2d;

            // List of all pages to render.
            var list = new List<IPDFPageRenderInfo>();

            // Let's take the page witch vertical center on it has this point on position 0.
            // We'll correct all top and bottom lines later.
            var pageOnMiddle = new PDFPageRenderInfo(pageOnCenter.Page)
            {
                Left = 0,
                Right = pageOnCenter.Page.Width * zoomFactor,
                Top = -1d * pageOnCenter.PagePositionOnCenter * pageOnCenter.Page.Height * zoomFactor,
                Bottom = (1d - pageOnCenter.PagePositionOnCenter) * pageOnCenter.Page.Height * zoomFactor,
                IsOnCenter = pageOnCenter.IsOnCenter,
                PagePositionOnCenter = pageOnCenter.PagePositionOnCenter,
            };

            // Add this page to the list.
            list.Add(pageOnMiddle);

            // Current position on Y-axis.
            var currentPositionOnY = pageOnMiddle.Top;
            currentPositionOnY -= pageMargin;

            // Iterate through pages from 'page on middle' to the first page.
            // Iterate through all of them, don't break on page above virtual top line.
            for (var index = pageOnCenter.Page.PageIndex - 1; index >= 0; index--)
            {
                // Get page to check.
                var page = Pages[index];

                // Check the curreint position on y relative to the virtual top line.
                if (currentPositionOnY > virtualTopLine)
                {
                    // This page is still visible.
                    var nextPageToAdd = new PDFPageRenderInfo(page)
                    {
                        Left = 0,
                        Right = page.Width * zoomFactor,
                        Top = currentPositionOnY - (page.Height * zoomFactor),
                        Bottom = currentPositionOnY,
                    };

                    // Insert this page to the first position of list.
                    list.Insert(0, nextPageToAdd);
                }

                // Adjust current position on y.
                currentPositionOnY -= page.Height * zoomFactor;
                currentPositionOnY -= pageMargin;
            }

            // Set new top and bottom line returned back.
            topLine = (-1d * currentPositionOnY) - (height / 2d);
            bottomLine = topLine + height;

            // A negative top line means that the first page is displayed and not positioned at the top.
            if (topLine < 0d)
            {
                topLine = 0d;
                bottomLine = topLine + height;

                currentPositionOnY = pageMargin;

                // Adjust top and bottom line of all already added pages to render.
                foreach (var pageRenderInfo in list)
                {
                    pageRenderInfo.Top = currentPositionOnY;
                    pageRenderInfo.Bottom = currentPositionOnY + (pageRenderInfo.Page.Height * zoomFactor);

                    // Adjust current position on y axis.
                    currentPositionOnY += pageMargin + (pageRenderInfo.Page.Height * zoomFactor);
                }
            }
            else
            {
                // Adjust top and bottom line of all already added pages to render.
                foreach (var pageRenderInfo in list)
                {
                    pageRenderInfo.Top += topLine + (height / 2d);
                    pageRenderInfo.Bottom += topLine + (height / 2d);
                    currentPositionOnY = pageRenderInfo.Bottom;
                }

                // Adjust current position on y axis.
                currentPositionOnY += pageMargin;
            }

            // Iterate through pages from 'page on middle' to the last page.
            for (var index = pageOnCenter.Page.PageIndex + 1; index < Pages.Count; index++)
            {
                // Filter out all pages below bottom line.
                if (currentPositionOnY > bottomLine)
                {
                    // Page is below bottom line.
                    // End foreach.
                    break;
                }

                // Part of this page is between top and bottom line.
                // Add this page to the list.
                var nextPageToAdd = new PDFPageRenderInfo(Pages[index])
                {
                    Left = 0,
                    Right = Pages[index].Width * zoomFactor,
                    Top = currentPositionOnY,
                    Bottom = currentPositionOnY + (Pages[index].Height * zoomFactor),
                };

                // Add the page to the list.
                list.Add(nextPageToAdd);

                // Adjust current position on y axis.
                currentPositionOnY += pageMargin + (Pages[index].Height * zoomFactor);
            }

            return list;
        }

        #endregion Private methods

        #region Implementation of IPageLayoutAdapter

        /// <inheritdoc/>
        public double WidestGridCellWidth { get; private set; }

        /// <inheritdoc/>
        public double HighestGridCellHeight { get; private set; }

        /// <inheritdoc/>
        public double CumulativeHeight { get; private set; }

        /// <inheritdoc/>
        public double GetPageTopLine(int pageIndex, double pageMargin, double zoomFactor)
        {
            var currentPosition = pageMargin;
            for (var index = 0; index < Pages.Count; index++)
            {
                if (index == pageIndex)
                {
                    break;
                }

                currentPosition += Pages[index].Height * zoomFactor;
                currentPosition += pageMargin;
            }

            return currentPosition;
        }

        /// <inheritdoc/>
        public void DetermineArea(ref double width, ref double height, double pageMargin, double zoomFactor)
        {
            DetermineAreaVirtual(ref width, ref height, pageMargin, zoomFactor);
        }

        /// <inheritdoc/>
        public IList<IPDFPageRenderInfo> DeterminePagesToRender(double topLine, double bottomLine, double pageMargin, double zoomFactor, bool setCurrentPageIndex = false)
        {
            return DeterminePagesToRenderVirtual(topLine, bottomLine, pageMargin, zoomFactor, setCurrentPageIndex);
        }

        /// <inheritdoc/>
        public IList<IPDFPageRenderInfo> DeterminePagesToRender(IPDFPageRenderInfo pageOnCenter, ref double topLine, ref double bottomLine, double pageMargin, double zoomFactor)
        {
            return DeterminePagesToRenderVirtual(pageOnCenter, ref topLine, ref bottomLine, pageMargin, zoomFactor);
        }

        #endregion Implementation of IPageLayoutAdapter
    }
}
