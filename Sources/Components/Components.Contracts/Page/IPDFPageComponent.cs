namespace PDFiumDotNET.Components.Contracts.Page
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;

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
        /// Gets the page count of opened document.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the width of widest page.
        /// </summary>
        double WidestWidth { get; }

        /// <summary>
        /// Gets the height of highest page.
        /// </summary>
        double HighestHeight { get; }

        /// <summary>
        /// Gets the cumulative height of all pages. This height contains no space between pages.
        /// </summary>
        double CumulativeHeight { get; }

        /// <summary>
        /// Gets the pages of opened document.
        /// </summary>
        ObservableCollection<IPDFPage> Pages { get; }

        /// <summary>
        /// Determine required size of area where fit all pages.
        /// </summary>
        /// <param name="width">In: available width. Out: required width.</param>
        /// <param name="height">In: available height. Out: required height.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for the computing of required area.</param>
        void DeterminePageArea(ref double width, ref double height, double pageMargin, double zoomFactor);

        /// <summary>
        /// Gets all pages to be drawn in specified region.
        /// </summary>
        /// <param name="topLine">Top line of viewport.</param>
        /// <param name="bottomLine">Bottom line of viewport.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <returns>Pages to draw in required region.</returns>
        IList<IPDFPageRenderInfo> DeterminePagesToRender(double topLine, double bottomLine, double pageMargin, double zoomFactor);

        /// <summary>
        /// Gets all pages to be drawn in specified region.
        /// </summary>
        /// <param name="pageOnCenter">The page on which is center of vertical direction of the viewport.</param>
        /// <param name="topLine">Top line of viewport.</param>
        /// <param name="bottomLine">Bottom line of viewport.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <returns>Pages to draw in required region.</returns>
        IList<IPDFPageRenderInfo> DeterminePagesToRender(IPDFPageRenderInfo pageOnCenter, ref double topLine, ref double bottomLine, double pageMargin, double zoomFactor);

        /// <summary>
        /// Gets the top line of page by applying distance between pages.
        /// </summary>
        /// <param name="pageIndex">Index of the page for which the top line needs to be determined.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <returns>Position of the top line for required page.</returns>
        double GetPageTopLine(int pageIndex, double pageMargin, double zoomFactor);

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
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="pageIndex">Index of page to navigate into. Index is 1 based.</param>
        void NavigateToPage(int pageIndex);
    }
}
