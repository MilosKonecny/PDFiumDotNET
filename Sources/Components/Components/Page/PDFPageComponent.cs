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
            ActualPage = 0;
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
        public int ActualPage { get; private set; }

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
        public IList<IPDFPageRenderInfo> PagesToRender(double topLine, double bottomLine, double pageMargin, double zoomFactor)
        {
            var list = new List<IPDFPageRenderInfo>();
            var actualPosition = pageMargin;

            // Iterate through all pages.
            foreach (var page in Pages)
            {
                if (actualPosition + (page.Height * zoomFactor) < topLine)
                {
                    // Page is above top line. Continue with next page.
                    actualPosition += pageMargin + (page.Height * zoomFactor);
                    continue;
                }

                if (actualPosition > bottomLine)
                {
                    // Pge is below bottom line. Terminate foreach.
                    break;
                }

                // Add this page to the list.
                list.Add(new PDFPageRenderInfo(page)
                {
                    Left = 0,
                    Right = page.Width,
                    Top = actualPosition,
                    Bottom = actualPosition + (page.Height * zoomFactor),
                });

                // Increment actual position.
                actualPosition += pageMargin + (page.Height * zoomFactor);
            }

            return list;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double GetPageTopLine(int pageIndex, double margin, double zoomFactor)
        {
            var actualPosition = margin;
            for (var index = 0; index < Pages.Count; index++)
            {
                if (index == pageIndex)
                {
                    break;
                }

                actualPosition += Pages[index].Height * zoomFactor;
                actualPosition += margin;
            }

            return actualPosition;
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

            ActualPage = 1 + action.Destination.PageIndex;
            InvokePropertyChangedEvent(nameof(ActualPage));
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

            ActualPage = 1 + destination.PageIndex;
            InvokePropertyChangedEvent(nameof(ActualPage));
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

            ActualPage = pageIndex;
            InvokePropertyChangedEvent(nameof(ActualPage));
        }

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
