namespace PDFiumDotNET.Components.Page
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Contracts.EventArguments;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;
    using PDFiumDotNET.Components.Find;
    using PDFiumDotNET.Components.Helper;
    using PDFiumDotNET.Components.Layout;
    using PDFiumDotNET.Components.Zoom;

    /// <inheritdoc cref="IPDFPageComponent"/>
    internal sealed partial class PDFPageComponent : PDFChildComponent, IPDFPageComponent
    {
        #region Private fields

        private readonly List<IPageLayoutAdapter> _pageLayoutAdapters = new List<IPageLayoutAdapter>();
        private readonly List<PDFRectangle> _selectionRectangles = new List<PDFRectangle>();
        private int _pageIndexWithSelections;
        private StandardPageLayout _standardPageLayout;
        private ThumbnailPageLayout _thumbnailPageLayout;
        private Func<int> _findSelectionBackgroundFunc;
        private Func<int> _findSelectionBorderFunc;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPageComponent"/> class.
        /// </summary>
        /// <param name="pageComponentName">Name of component. The name should be unique in <see cref="PDFComponent"/> context.</param>
        public PDFPageComponent(string pageComponentName)
        {
            // ToDo: Remove default value for pageComponentName parameter.
            Name = pageComponentName;

            Pages = new ObservableCollection<IPDFPage>();

            _standardPageLayout = new StandardPageLayout(this);
            _pageLayoutAdapters.Add(_standardPageLayout);
            _thumbnailPageLayout = new ThumbnailPageLayout(this);
            _pageLayoutAdapters.Add(_thumbnailPageLayout);
        }

        #endregion Constructors

        #region Internal properties

        /// <summary>
        /// Gets the name of page component.
        /// </summary>
        internal string Name { get; private set; }

        /// <summary>
        /// Gets the page index where are the selection rectangles defined.
        /// </summary>
        internal int PageIndexWithSelections
        {
            get
            {
                return _pageIndexWithSelections;
            }

            private set
            {
                _pageIndexWithSelections = value;
            }
        }

        /// <summary>
        /// Gets the list of selection rectangles to render on the page defined by <see cref="PageIndexWithSelections"/>.
        /// </summary>
        internal List<PDFRectangle> SelectionRectangles
        {
            get
            {
                return _selectionRectangles;
            }
        }

        #endregion Internal properties

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

        /// <summary>
        /// Clears the list of all selection rectangles.
        /// </summary>
        internal void ClearSelectionRectangles()
        {
            _pageIndexWithSelections = -1;
            _selectionRectangles?.Clear();
            TextSelectionsRemoved?.Invoke(this, EventArgs.Empty);
        }

        #endregion Internal methods

        #region Private methods

        private void SetDefaultValues()
        {
            ClearSelectionRectangles();
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

        /// <summary>
        /// Adds selection rectangle to the list to render with PDF content.
        /// </summary>
        /// <param name="left">Left position of rectangle.</param>
        /// <param name="top">Top position of rectangle.</param>
        /// <param name="right">Right position of rectangle.</param>
        /// <param name="bottom">Bottom position of rectangle.</param>
        private void AddSelectionRectangle(double left, double top, double right, double bottom)
        {
            _selectionRectangles.Add(new PDFRectangle(left, top, right, bottom));
        }

        #endregion Private methods

        #region Implementation of IPageComponent

        /// <inheritdoc/>
        public IPDFFindComponent FindComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = ChildComponents.OfType<IPDFFindComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFFindComponent(this);
                    Attach(component);
                }

                return component;
            }
        }

        /// <inheritdoc/>
        public IPDFZoomComponent ZoomComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = ChildComponents.OfType<IPDFZoomComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFZoomComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <inheritdoc/>
        public int CurrentPageIndex
        {
            get;
            private set;
        }

        /// <inheritdoc/>
        public string CurrentPageLabel
        {
            get;
            private set;
        }

        /// <inheritdoc/>
        public int PageCount { get; private set; }

        /// <inheritdoc/>
        public IPageLayoutAdapter this[PageLayoutType type]
        {
            get
            {
                return _pageLayoutAdapters.FirstOrDefault(adapter => adapter.LayoutType == type);
            }
        }

        /// <inheritdoc/>
        public ObservableCollection<IPDFPage> Pages { get; private set; }

        /// <summary>
        /// Gets or sets the information whether the annotation objects ar to render.
        /// </summary>
        public bool IsAnnotationToRender { get; set; }

        /// <summary>
        /// Gets or set the function to obtain color to use to draw find selection rectangle.
        /// </summary>
        public Func<int> FindSelectionBackgroundFunc
        {
            get
            {
                return _findSelectionBackgroundFunc;
            }

            set
            {
                if (_findSelectionBackgroundFunc != value)
                {
                    _findSelectionBackgroundFunc = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or set the function to obtain color to use to draw find selection rectangle.
        /// </summary>
        public Func<int> FindSelectionBorderFunc
        {
            get
            {
                return _findSelectionBorderFunc;
            }

            set
            {
                if (_findSelectionBorderFunc != value)
                {
                    _findSelectionBorderFunc = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void NavigateToFindPlace(IPDFFindPage page)
        {
            if (page == null)
            {
                return;
            }

            ClearSelectionRectangles();
            NavigateToPage(page.RelatedPage.PageIndex + 1);
        }

        /// <inheritdoc/>
        public void NavigateToFindPlace(IPDFFindPosition position)
        {
            if (position == null)
            {
                return;
            }

            ClearSelectionRectangles();
            if (position is PDFFindPosition ourPosition
                && position.Page.RelatedPage is PDFPage ourPage)
            {
                PageIndexWithSelections = ourPage.PageIndex;
                var positionX = -1;
                var positionY = -1;
                var pageHandle = PDFComponent.PDFiumBridge.FPDF_LoadPage(PDFComponent.PDFiumDocument, ourPosition.Page.RelatedPage.PageIndex);
                var textPageHandle = PDFComponent.PDFiumBridge.FPDFText_LoadPage(pageHandle);

                var rectCount = PDFComponent.PDFiumBridge.FPDFText_CountRects(textPageHandle, ourPosition.Position, ourPosition.Length);
                for (int i = 0; i < rectCount; i++)
                {
                    double left, top, right, bottom;
                    left = top = right = bottom = 0;
                    if (PDFComponent.PDFiumBridge.FPDFText_GetRect(textPageHandle, i, ref left, ref top, ref right, ref bottom))
                    {
                        AddSelectionRectangle(left, top, right, bottom);
                        if (positionX == -1)
                        {
                            positionX = (int)Math.Round(left, 0);
                            positionY = (int)Math.Round(top, 0);
                        }
                    }
                }

                PDFComponent.PDFiumBridge.FPDFText_ClosePage(textPageHandle);
                PDFComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

                var previousCurrentPageIndex = CurrentPageIndex;
                var currentPageIndex = position.Page.RelatedPage.PageIndex + 1;
                SetCurrentInformation(position.Page.RelatedPage);
                NavigatedToPage?.Invoke(this, new NavigatedToPageEventArgs(previousCurrentPageIndex, CurrentPageIndex, positionX, positionY));
            }
            else
            {
                NavigateToPage(position.Page.RelatedPage.PageIndex + 1);
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public event EventHandler<NavigatedToPageEventArgs> NavigatedToPage;

        /// <inheritdoc/>
        public event EventHandler<PerformActionEventArgs> PerformOutsideAction;

        /// <summary>
        /// Occurs whenever text selections were removed.
        /// </summary>
        /// <remarks>For example. It was some text selected to show found text.
        /// When the new find is started, this event is called.</remarks>
        public event EventHandler<EventArgs> TextSelectionsRemoved;

        #endregion Implementation of IPageComponent
    }
}
