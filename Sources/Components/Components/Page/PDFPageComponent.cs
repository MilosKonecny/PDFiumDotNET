namespace PDFiumDotNET.Components.Page
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Components.Adapters;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Adapters;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Contracts.EventArguments;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// <inheritdoc cref="IPDFPageComponent"/>
    /// </summary>
    internal sealed partial class PDFPageComponent : IPDFPageComponent, IPDFDocumentObserver
    {
        #region Private fields

        private readonly List<IPageLayoutAdapter> _pageLayoutAdapters = new List<IPageLayoutAdapter>();
        private PDFComponent _mainComponent;
        private StandardPageLayout _standardPageLayout;
        private ThumbnailPageLayout _thumbnailPageLayout;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPageComponent"/> class.
        /// </summary>
        public PDFPageComponent()
        {
            Pages = new ObservableCollection<IPDFPage>();

            _standardPageLayout = new StandardPageLayout(this);
            _pageLayoutAdapters.Add(_standardPageLayout);
            _thumbnailPageLayout = new ThumbnailPageLayout(this);
            _pageLayoutAdapters.Add(_thumbnailPageLayout);
        }

        #endregion Constructors

        #region Private methods

        private void SetDefaultValues()
        {
            PageCount = 0;
            Pages.Clear();
            SetCurrentInformation(null);
            _standardPageLayout.SetDefaultValues();
            _thumbnailPageLayout.SetDefaultValues();
            InvokePropertyChangedEvent(null);
        }

        /// <summary>
        /// Gets the page. In case the index is out of range, <c>null</c> is returned.
        /// </summary>
        /// <param name="pageIndex">Index of page to return.</param>
        /// <returns>Returns required page.
        /// <c>null</c> is returned in case the <paramref name="pageIndex"/> is out of range.</returns>
        private IPDFPage GetPage(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < PageCount)
            {
                return Pages[pageIndex];
            }

            return null;
        }

        #endregion Private methods

        #region Internal methods

        /// <summary>
        /// Sets all 'current' properties based on given page.
        /// </summary>
        /// <param name="page">Page to use as a source for 'current' properties.</param>
        internal void SetCurrentInformation(IPDFPage page)
        {
            var newPageIndex = 0;
            var newPageLabel = string.Empty;
            if (page != null)
            {
                newPageIndex = page.PageIndex + 1;
                newPageLabel = page.PageLabel;
            }

            if (CurrentPageIndex != newPageIndex)
            {
                CurrentPageIndex = newPageIndex;
                InvokePropertyChangedEvent(nameof(CurrentPageIndex));
            }

            if (!string.Equals(CurrentPageLabel, newPageLabel, StringComparison.Ordinal))
            {
                CurrentPageLabel = newPageLabel;
                InvokePropertyChangedEvent(nameof(CurrentPageLabel));
            }
        }

        #endregion Internal methods

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
        public int CurrentPageIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string CurrentPageLabel
        {
            get;
            private set;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPageLayoutAdapter this[PageLayoutType type]
        {
            get
            {
                return _pageLayoutAdapters.FirstOrDefault(adapter => adapter.LayoutType == type);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ObservableCollection<IPDFPage> Pages { get; private set; }

        /// <summary>
        /// Gets or sets the information whether the annotation objects ar to render.
        /// </summary>
        public bool IsAnnotationToRender { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void PerformAction(IPDFAction action)
        {
            if (action == null)
            {
                return;
            }

            if (action.ActionType != PDFActionType.Goto)
            {
                PerformOutsideAction?.Invoke(this, new PerformActionEventArgs(action));
                return;
            }

            if (action.Destination == null || action.Destination.PageIndex < 0 || action.Destination.PageIndex >= PageCount)
            {
                return;
            }

            var previousCurrentPageIndex = CurrentPageIndex;
            SetCurrentInformation(GetPage(action.Destination.PageIndex));
            NavigatedToPage?.Invoke(this, new NavigatedToPageEventArgs(previousCurrentPageIndex, CurrentPageIndex));
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

            var previousCurrentPageIndex = CurrentPageIndex;
            SetCurrentInformation(GetPage(destination.PageIndex));
            NavigatedToPage?.Invoke(this, new NavigatedToPageEventArgs(previousCurrentPageIndex, CurrentPageIndex));
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

            var previousCurrentPageIndex = CurrentPageIndex;
            SetCurrentInformation(GetPage(pageIndex - 1));
            NavigatedToPage?.Invoke(this, new NavigatedToPageEventArgs(previousCurrentPageIndex, CurrentPageIndex));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void NavigateToPage(string pageLabel)
        {
            var page = Pages.FirstOrDefault(p => string.Equals(p.PageLabel, pageLabel, StringComparison.OrdinalIgnoreCase));
            if (page != null)
            {
                var previousCurrentPageIndex = CurrentPageIndex;
                SetCurrentInformation(page);
                NavigatedToPage?.Invoke(this, new NavigatedToPageEventArgs(previousCurrentPageIndex, CurrentPageIndex));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event EventHandler<NavigatedToPageEventArgs> NavigatedToPage;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event EventHandler<PerformActionEventArgs> PerformOutsideAction;

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
