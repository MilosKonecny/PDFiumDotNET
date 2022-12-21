namespace PDFiumDotNET.Components.Layout
{
    using System;
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// Base class for all page layout adapters.
    /// </summary>
    internal abstract class BasePageLayout : IPageLayoutAdapter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePageLayout"/> class.
        /// </summary>
        /// <param name="pageComponent"><see cref="PDFPageComponent"/> used to obtain information.</param>
        /// <param name="layoutType">Type of supported layout.</param>
        internal BasePageLayout(PDFPageComponent pageComponent, PageLayoutType layoutType)
        {
            PageComponent = pageComponent ?? throw new ArgumentNullException(nameof(pageComponent));
            LayoutType = layoutType;
        }

        #endregion Constructors

        #region Protected properties

        /// <summary>
        /// Gets associated <see cref="PDFPageComponent"/>.
        /// </summary>
        protected PDFPageComponent PageComponent
        {
            get;
            private set;
        }

        #endregion Protected properties

        #region Internal virtual methods

        /// <summary>
        /// Sets default values in fields and properties.
        /// </summary>
        internal virtual void SetDefaultValues()
        {
            WidestGridCellWidth = 0;
            HighestGridCellHeight = 0;
            CumulativeHeight = 0;
        }

        /// <summary>
        /// Initializes layout.
        /// </summary>
        internal virtual void InitializeLayout()
        {
            SetDefaultValues();

            for (var index = 0; index < PageComponent.PageCount; index++)
            {
                var page = PageComponent.Pages[index];

                CumulativeHeight += PageHeight(page);
                if (PageWidth(page) > WidestGridCellWidth)
                {
                    WidestGridCellWidth = PageWidth(page);
                }

                if (PageHeight(page) > HighestGridCellHeight)
                {
                    HighestGridCellHeight = PageHeight(page);
                }
            }
        }

        #endregion Internal virtual methods

        #region Protected virtual methods

        /// <summary>
        /// Determine required size of area where fit all pages.
        /// </summary>
        /// <param name="width">In: available width. Out: required width.</param>
        /// <param name="height">In: available height. Out: required height.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for the computing of required area.</param>
        protected virtual void DetermineAreaVirtual(ref double width, ref double height, double pageMargin, double zoomFactor)
        {
            if (PageComponent.PageCount == 0)
            {
                return;
            }

            var newWidth = WidestGridCellWidth * zoomFactor;
            var newHeight = CumulativeHeight * zoomFactor;
            var marginWidth = 2d * pageMargin;
            var marginHeight = (PageComponent.PageCount + 1) * pageMargin;
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
        protected virtual IList<IPDFPageRenderInfo> DeterminePagesToRenderVirtual(double topLine, double bottomLine, double pageMargin, double zoomFactor, bool setCurrentPageIndex = false)
        {
            // List of all pages to render.
            var list = new List<IPDFPageRenderInfo>();

            // Current position on Y-axis.
            var currentPositionOnY = pageMargin;

            // Center line on viewport
            var centerLine = (topLine + bottomLine) / 2;
            var isCenterSet = false;

            // Iterate through all pages.
            foreach (var page in PageComponent.Pages)
            {
                // Filter out all pages above top line.
                if (currentPositionOnY + (PageHeight(page) * zoomFactor) < topLine)
                {
                    // Page is above top line.
                    // Adjust the current position on y and continue.
                    currentPositionOnY += pageMargin + (PageHeight(page) * zoomFactor);
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
                    Right = PageWidth(page) * zoomFactor,
                    Top = currentPositionOnY,
                    Bottom = currentPositionOnY + (PageHeight(page) * zoomFactor),
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
                        PageComponent.SetCurrentInformation(pageToAdd.Page);
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
                        PageComponent.SetCurrentInformation(pageToAdd.Page);
                    }
                }

                // Add the page to the list.
                list.Add(pageToAdd);

                // Adjust current position on y.
                currentPositionOnY += pageMargin + (PageHeight(page) * zoomFactor);
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
        protected virtual IList<IPDFPageRenderInfo> DeterminePagesToRenderVirtual(IPDFPageRenderInfo pageOnCenter, ref double topLine, ref double bottomLine, double pageMargin, double zoomFactor)
        {
            // Base check.
            if (pageOnCenter == null
                || !pageOnCenter.IsOnCenter
                || pageOnCenter.Page == null
                || pageOnCenter.Page.PageIndex >= PageComponent.PageCount)
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
                Right = PageWidth(pageOnCenter.Page) * zoomFactor,
                Top = -1d * pageOnCenter.PagePositionOnCenter * PageHeight(pageOnCenter.Page) * zoomFactor,
                Bottom = (1d - pageOnCenter.PagePositionOnCenter) * PageHeight(pageOnCenter.Page) * zoomFactor,
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
                var page = PageComponent.Pages[index];

                // Check the curreint position on y relative to the virtual top line.
                if (currentPositionOnY > virtualTopLine)
                {
                    // This page is still visible.
                    var nextPageToAdd = new PDFPageRenderInfo(page)
                    {
                        Left = 0,
                        Right = PageWidth(page) * zoomFactor,
                        Top = currentPositionOnY - (PageHeight(page) * zoomFactor),
                        Bottom = currentPositionOnY,
                    };

                    // Insert this page to the first position of list.
                    list.Insert(0, nextPageToAdd);
                }

                // Adjust current position on y.
                currentPositionOnY -= PageHeight(page) * zoomFactor;
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
                    pageRenderInfo.Bottom = currentPositionOnY + (PageHeight(pageRenderInfo.Page) * zoomFactor);

                    // Adjust current position on y axis.
                    currentPositionOnY += pageMargin + (PageHeight(pageRenderInfo.Page) * zoomFactor);
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
            for (var index = pageOnCenter.Page.PageIndex + 1; index < PageComponent.Pages.Count; index++)
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
                var nextPageToAdd = new PDFPageRenderInfo(PageComponent.Pages[index])
                {
                    Left = 0,
                    Right = PageWidth(PageComponent.Pages[index]) * zoomFactor,
                    Top = currentPositionOnY,
                    Bottom = currentPositionOnY + (PageHeight(PageComponent.Pages[index]) * zoomFactor),
                };

                // Add the page to the list.
                list.Add(nextPageToAdd);

                // Adjust current position on y axis.
                currentPositionOnY += pageMargin + (PageHeight(PageComponent.Pages[index]) * zoomFactor);
            }

            return list;
        }

        #endregion Protected virtual methods

        #region Protected abstract methods

        /// <summary>
        /// Returns the width of given page.
        /// </summary>
        /// <param name="page">Page to examine.</param>
        /// <returns>Width of given page.</returns>
        /// <remarks>Used to determine which width should be used - width or thumbnail width.</remarks>
        protected abstract double PageWidth(IPDFPage page);

        /// <summary>
        /// Returns the height of given page.
        /// </summary>
        /// <param name="page">Page to examine.</param>
        /// <returns>Height of given page.</returns>
        /// <remarks>Used to determine which height should be used - height or thumbnail height.</remarks>
        protected abstract double PageHeight(IPDFPage page);

        #endregion Protected abstract methods

        #region Implementation of IPageLayoutAdapter

        /// <inheritdoc/>
        public PageLayoutType LayoutType { get; protected set; }

        /// <inheritdoc/>
        public double WidestGridCellWidth { get; protected set; }

        /// <inheritdoc/>
        public double HighestGridCellHeight { get; protected set; }

        /// <inheritdoc/>
        public double CumulativeHeight { get; protected set; }

        /// <inheritdoc/>
        public double GetPageTopLine(int pageIndex, double pageMargin, double zoomFactor)
        {
            var currentPosition = pageMargin;
            for (var index = 0; index < PageComponent.Pages.Count; index++)
            {
                if (index == pageIndex)
                {
                    break;
                }

                currentPosition += PageHeight(PageComponent.Pages[index]) * zoomFactor;
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
