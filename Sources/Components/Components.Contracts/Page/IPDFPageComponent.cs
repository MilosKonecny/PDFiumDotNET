namespace PDFiumDotNET.Components.Contracts.Page
{
    using System;
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Adapters;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Contracts.EventArguments;

    /// <summary>
    /// Interface defines functionality of page component.
    /// Component provides all information related to pages of opened PDF document.
    /// </summary>
    public interface IPDFPageComponent : IPDFChildComponent
    {
        /// <summary>
        /// Gets index of current page of opened document. First page has index 1.
        /// </summary>
        int CurrentPageIndex { get; }

        /// <summary>
        /// Gets label of current page of opened document.
        /// </summary>
        string CurrentPageLabel { get; }

        /// <summary>
        /// Gets the page count of opened document.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the one of available layout adapters.
        /// </summary>
        /// <param name="type">Type of layout adapter to get.</param>
        /// <returns>Required adapter. Returned value is never <c>null</c>.</returns>
        IPageLayoutAdapter this[PageLayoutType type]
        {
            get;
        }

        /// <summary>
        /// Gets the pages of opened document.
        /// </summary>
        ObservableCollection<IPDFPage> Pages { get; }

        /// <summary>
        /// Gets or sets the information whether the annotation objects ar to render.
        /// </summary>
        bool IsAnnotationToRender { get; set; }

        /// <summary>
        /// Peforms the action defined in given <see cref="IPDFAction"/>.
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
        /// Navigates to the specified page based on its index.
        /// </summary>
        /// <param name="pageIndex">Index of page to navigate to. Index is 1 based.</param>
        void NavigateToPage(int pageIndex);

        /// <summary>
        /// Navigates to the specified page based on its label.
        /// </summary>
        /// <param name="pageLabel">Label of page to navigate to.
        /// In case the label not exists, text is converted to index and method <see cref="NavigateToPage(int)"/> will be used.</param>
        void NavigateToPage(string pageLabel);

        /// <summary>
        /// Occurs whenever some of 'navigate' / 'perform' methods was called and <see cref="CurrentPageIndex"/> was changed.
        /// </summary>
        event EventHandler<NavigatedToPageEventArgs> NavigatedToPage;

        /// <summary>
        /// Occurs whenever an action outside of the current pdf document is to perform.
        /// </summary>
        event EventHandler<PerformActionEventArgs> PerformOutsideAction;
    }
}
