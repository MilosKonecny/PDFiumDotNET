namespace PDFiumDotNET.Components.Contracts.Page
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Annotation;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Contracts.EventArguments;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Render;
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <summary>
    /// Interface defines functionality of page component.
    /// Component provides all information related to pages of opened PDF document.
    /// </summary>
    public interface IPDFPageComponent : IPDFChildComponent
    {
        /// <summary>
        /// Gets or sets the margins between pages.
        /// </summary>
        PDFSize<double> PageMargin { get; set; }

        /// <summary>
        /// Gets the render manager helping to render PDF document.
        /// </summary>
        IPDFRenderManager RenderManager { get; }

        /// <summary>
        /// Gets the find component.
        /// </summary>
        IPDFFindComponent FindComponent { get; }

        /// <summary>
        /// Gets the zoom component.
        /// </summary>
        IPDFZoomComponent ZoomComponent { get; }

        /// <summary>
        /// Gets index of current page of opened document. First page has index 1.
        /// </summary>
        int CurrentPageIndex { get; }

        /// <summary>
        /// Gets label of current page of opened document.
        /// </summary>
        string CurrentPageLabel { get; }

        /// <summary>
        /// Gets the current page of opened document.
        /// </summary>
        IPDFPage CurrentPage { get; }

        /// <summary>
        /// Gets the page count of opened document.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the pages of opened document.
        /// </summary>
        ObservableCollection<IPDFPage> Pages { get; }

        /// <summary>
        /// Gets or sets the information whether the annotation objects are to render.
        /// </summary>
        bool IsAnnotationToRender { get; set; }

        /// <summary>
        /// Gets or sets the function to obtain color to use to draw find selection rectangle.
        /// </summary>
        Func<int> FindSelectionBackgroundFunc { get; set; }

        /// <summary>
        /// Gets or sets the function to obtain color to use to draw find selection rectangle.
        /// </summary>
        Func<int> FindSelectionBorderFunc { get; set; }

        /// <summary>
        /// Find all pages with annotation.
        /// </summary>
        /// <param name="progress">Callback method.
        /// Parameter is actual page index where is tried to find annotation. First page has index 0.
        /// Return <c>true</c> to continue and <c>false</c> to cancel find process.</param>
        /// <param name="addPage">Callback method.
        /// Called whenever next page was identified, where the annotation exists.
        /// Return <c>true</c> to continue and <c>false</c> to cancel find process.</param>
        /// <returns><c>true</c> at least one page with annotation was found.</returns>
        bool FindAnnotations(Func<int, bool> progress, Func<IPDFPageAnnotations, bool> addPage);

        /// <summary>
        /// Performs the action defined in given <see cref="IPDFAction"/>.
        /// </summary>
        /// <param name="action"><see cref="IPDFAction"/> defines action to perform.</param>
        /// <remarks>Only one type of action is performed: <see cref="PDFActionType.Goto"/>.</remarks>
        void PerformAction(IPDFAction action);

        /// <summary>
        /// Navigates to the specified page defined by given <see cref="IPDFDestination"/>.
        /// </summary>
        /// <param name="destination"><see cref="IPDFDestination"/> defines the destination.</param>
        /// <remarks>Navigation is performed only to the side.
        /// The position on the page is ignored even if it is defined.
        /// Zoom factor as well.</remarks>
        void NavigateToDestination(IPDFDestination destination);

        /// <summary>
        /// Sets the specified page as current page.
        /// </summary>
        /// <param name="pageIndex">Index of page to set as current page. Index is 1 based.</param>
        /// <remarks>This method is as the same as <see cref="NavigateToPage(int, bool)"/> / <see cref="NavigateToPage(string, bool)"/>, but event <see cref="NavigatedToPage"/> is not triggered.</remarks>
        void SetCurrentPage(int pageIndex);

        /// <summary>
        /// Navigates to the specified page based on its index.
        /// </summary>
        /// <param name="pageIndex">Index of page to navigate to. Index is 1 based.</param>
        /// <param name="forceNavigate">If <c>true</c>, navigation to the page is forced, even if the page is already active
        /// - <see cref="INotifyPropertyChanged.PropertyChanged"/> event will be invoked.</param>
        void NavigateToPage(int pageIndex, bool forceNavigate = false);

        /// <summary>
        /// Navigates to the specified page based on its label.
        /// </summary>
        /// <param name="pageLabel">Label of page to navigate to.
        /// In case the label not exists, text is converted to index and method <see cref="NavigateToPage(int, bool)"/> will be used.</param>
        /// <param name="forceNavigate">If <c>true</c>, navigation to the page is forced, even if the page is already active
        /// - <see cref="INotifyPropertyChanged.PropertyChanged"/> event will be invoked.</param>
        void NavigateToPage(string pageLabel, bool forceNavigate = false);

        /// <summary>
        /// Navigates to the place where the searched text was found.
        /// </summary>
        /// <param name="page">This definition contains only page where the searched text was found.</param>
        /// <param name="forceNavigate">If <c>true</c>, navigation to the page is forced, even if the page is already active
        /// - <see cref="INotifyPropertyChanged.PropertyChanged"/> event will be invoked.</param>
        void NavigateToFindPlace(IPDFFindPage page, bool forceNavigate = false);

        /// <summary>
        /// Navigates to the place where searched text was found.
        /// </summary>
        /// <param name="position">This definition contains whole information where the searched text was found.</param>
        /// <param name="forceNavigate">If <c>true</c>, navigation to the page is forced, even if the page is already active
        /// - <see cref="INotifyPropertyChanged.PropertyChanged"/> event will be invoked.</param>
        void NavigateToFindPlace(IPDFFindPosition position, bool forceNavigate = false);

        /// <summary>
        /// Occurs whenever some of 'navigate' / 'perform' methods was called and <see cref="CurrentPageIndex"/> was changed.
        /// </summary>
        event EventHandler<NavigatedToPageEventArgs> NavigatedToPage;

        /// <summary>
        /// Occurs whenever an action outside of the current PDF document is to perform.
        /// </summary>
        event EventHandler<PerformActionEventArgs> PerformOutsideAction;

        /// <summary>
        /// Occurs whenever text selections were removed.
        /// </summary>
        /// <remarks>For example. It was some text selected to show found text.
        /// When the new find is started, this event is called.</remarks>
        event EventHandler<EventArgs> TextSelectionsRemoved;
    }
}
