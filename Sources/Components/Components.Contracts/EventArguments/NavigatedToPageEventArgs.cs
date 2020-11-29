namespace PDFiumDotNET.Components.Contracts.EventArguments
{
    using System;

    /// <summary>
    /// Event arguments class used to inform that the current page was changed.
    /// </summary>
    public class NavigatedToPageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatedToPageEventArgs"/> class.
        /// </summary>
        /// <param name="previousCurrentPageIndex">Previous current page. First page has index 1.</param>
        /// <param name="currentPageIndex">New current page. First page has index 1.</param>
        public NavigatedToPageEventArgs(int previousCurrentPageIndex, int currentPageIndex)
        {
            PreviousCurrentPageIndex = previousCurrentPageIndex;
            CurrentPageIndex = currentPageIndex;
        }

        /// <summary>
        /// Gets previous current page. First page has index 1.
        /// </summary>
        public int PreviousCurrentPageIndex { get; }

        /// <summary>
        /// Gets current page. First page has index 1.
        /// </summary>
        public int CurrentPageIndex { get; }
    }
}
