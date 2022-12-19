namespace PDFiumDotNET.Components.Contracts.Layout
{
    using System.Collections.Generic;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Interface defines functionality of adapter for page layout.
    /// </summary>
    /// <remarks>The basis of the layout is a grid.
    /// The grid can have one column and rows equal to the number of pages.
    /// In this case, the pages are displayed below each other.</remarks>
    public interface IPageLayoutAdapter
    {
        /// <summary>
        /// Gets the type of page layout adapter.
        /// </summary>
        PageLayoutType LayoutType { get; }

        /// <summary>
        /// Gets the width of widest grid cell.
        /// </summary>
        double WidestGridCellWidth { get; }

        /// <summary>
        /// Gets the height of highest grid cell.
        /// </summary>
        double HighestGridCellHeight { get; }

        /// <summary>
        /// Gets the cumulative height of all grid rows. This height contains no space between rows.
        /// </summary>
        double CumulativeHeight { get; }

        /// <summary>
        /// Gets the top line of page by applying distance between pages.
        /// </summary>
        /// <param name="pageIndex">Index of the page for which the top line needs to be determined.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <returns>Position of the top line for required page.</returns>
        double GetPageTopLine(int pageIndex, double pageMargin, double zoomFactor);

        /// <summary>
        /// Determine required size of area where fit all pages.
        /// </summary>
        /// <param name="width">In: available width. Out: required width.</param>
        /// <param name="height">In: available height. Out: required height.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for the computing of required area.</param>
        void DetermineArea(ref double width, ref double height, double pageMargin, double zoomFactor);

        /// <summary>
        /// Gets all pages to be drawn in specified region.
        /// </summary>
        /// <param name="topLine">Top line of viewport.</param>
        /// <param name="bottomLine">Bottom line of viewport.</param>
        /// <param name="pageMargin">Margin around the page.</param>
        /// <param name="zoomFactor">Zoom factor to use for pages. Not for page margin.</param>
        /// <param name="setCurrentPageIndex">If <c>true</c>, page on middle of height will be set as current page.</param>
        /// <returns>Pages to draw in required region.</returns>
        IList<IPDFPageRenderInfo> DeterminePagesToRender(double topLine, double bottomLine, double pageMargin, double zoomFactor, bool setCurrentPageIndex = false);

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
    }
}
