namespace PDFiumDotNET.Components.Page
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// <inheritdoc cref="IPDFPageComponent"/>
    /// </summary>
    internal sealed partial class PDFPageComponent : IPDFPageComponent, IPDFDocumentObserver
    {
        #region Private fields

        private PDFComponent _mainComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPageComponent"/> class.
        /// </summary>
        public PDFPageComponent()
        {
            Pages = new ObservableCollection<IPDFPage>();
        }

        #endregion Constructors

        #region Private methods

        private void SetDefaultValues()
        {
            CurrentPageIndex = 0;
            WidestWidth = 0;
            HighestHeight = 0;
            CumulativeHeight = 0;
            PageCount = 0;
            Pages.Clear();
            InvokePropertyChangedEvent(null);
        }

        #endregion Private methods

        #region Private methods - invoke event

        private void InvokePropertyChangedEvent([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private methods - invoke event

        #region Implementation of IPageComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int CurrentPageIndex { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double WidestWidth { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double HighestHeight { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double CumulativeHeight { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ObservableCollection<IPDFPage> Pages { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeterminePageArea(ref double width, ref double height, double pageMargin, double zoomFactor)
        {
            if (PageCount == 0)
            {
                return;
            }

            var newWidth = WidestWidth * zoomFactor;
            var newHeight = CumulativeHeight * zoomFactor;
            var marginWidth = 2d * pageMargin;
            var marginHeight = (PageCount + 1) * pageMargin;
            width = Math.Round(newWidth + marginWidth, 2);
            height = Math.Round(newHeight + marginHeight, 2);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IList<IPDFPageRenderInfo> DeterminePagesToRender(double topLine, double bottomLine, double pageMargin, double zoomFactor, bool setCurrentPageIndex = false)
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
                        CurrentPageIndex = pageToAdd.Page.PageIndex + 1;
                        InvokePropertyChangedEvent(nameof(CurrentPageIndex));
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
                        CurrentPageIndex = pageToAdd.Page.PageIndex + 1;
                        InvokePropertyChangedEvent(nameof(CurrentPageIndex));
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
        /// <inheritdoc/>
        /// </summary>
        public IList<IPDFPageRenderInfo> DeterminePagesToRender(IPDFPageRenderInfo pageOnCenter, ref double topLine, ref double bottomLine, double pageMargin, double zoomFactor)
        {
            // Base check.
            if (pageOnCenter == null
                || !pageOnCenter.IsOnCenter
                || pageOnCenter.Page == null
                || pageOnCenter.Page.PageIndex >= PageCount)
            {
                // We don't have required information.
                return DeterminePagesToRender(topLine, bottomLine, pageMargin, zoomFactor);
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double GetPageTopLine(int pageIndex, double margin, double zoomFactor)
        {
            var currentPosition = margin;
            for (var index = 0; index < Pages.Count; index++)
            {
                if (index == pageIndex)
                {
                    break;
                }

                currentPosition += Pages[index].Height * zoomFactor;
                currentPosition += margin;
            }

            return currentPosition;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void PerformAction(IPDFAction action)
        {
            if (action == null
                || action.ActionType != PDFActionType.Goto
                || action.Destination == null)
            {
                return;
            }

            if (action.Destination.PageIndex < 0 || action.Destination.PageIndex >= PageCount)
            {
                return;
            }

            CurrentPageIndex = 1 + action.Destination.PageIndex;
            InvokePropertyChangedEvent(nameof(CurrentPageIndex));
            NavigatedToPage?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void NavigateToDestination(IPDFDestination destination)
        {
            if (destination == null)
            {
                return;
            }

            if (destination.PageIndex < 0 || destination.PageIndex >= PageCount)
            {
                return;
            }

            CurrentPageIndex = 1 + destination.PageIndex;
            InvokePropertyChangedEvent(nameof(CurrentPageIndex));
            NavigatedToPage?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void NavigateToPage(int pageIndex)
        {
            if (pageIndex < 1 || pageIndex > PageCount)
            {
                return;
            }

            CurrentPageIndex = pageIndex;
            InvokePropertyChangedEvent(nameof(CurrentPageIndex));
            NavigatedToPage?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs whenever some of 'navigate' methods was called and <see cref="CurrentPageIndex"/> was changed.
        /// </summary>
        public event EventHandler<EventArgs> NavigatedToPage;

        #endregion Implementation of IPageComponent

        #region Implementation of IComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFComponent MainComponent => _mainComponent;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void AttachedTo(IPDFComponent mainComponent)
        {
            var mc = mainComponent as PDFComponent;

            _mainComponent = mc ?? throw new ArgumentException(
                string.Format(CultureInfo.InvariantCulture, "The parameter {0} is not of expected type.", nameof(mainComponent)));
        }

        #endregion Implementation of IComponent

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IDisposable

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
        }

        #endregion Implementation of IDisposable
    }
}
