namespace PDFiumDotNET.Components.Contracts.EventArguments
{
    using System;

    /// <summary>
    /// Event arguments class used to inform that the current page was changed.
    /// </summary>
    public class NavigatedToPageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatedToPageEventArgs"/> class used for simple navigation.
        /// </summary>
        /// <param name="previousCurrentPageIndex">Previous current page. First page has index 1.</param>
        /// <param name="currentPageIndex">New current page. First page has index 1.</param>
        public NavigatedToPageEventArgs(int previousCurrentPageIndex, int currentPageIndex)
        {
            PreviousCurrentPageIndex = previousCurrentPageIndex;
            CurrentPageIndex = currentPageIndex;
            IsDetailedNavigation = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatedToPageEventArgs"/> class used for detailed navigation.
        /// </summary>
        /// <param name="previousCurrentPageIndex">Previous current page. First page has index 1.</param>
        /// <param name="currentPageIndex">New current page. First page has index 1.</param>
        /// <param name="positionX">X position for detailed navigation.</param>
        /// <param name="positionY">Y position for detailed navigation.</param>
        public NavigatedToPageEventArgs(int previousCurrentPageIndex, int currentPageIndex, int positionX, int positionY)
            : this(previousCurrentPageIndex, currentPageIndex)
        {
            IsDetailedNavigation = true;
            DetailedPositionX = positionX;
            DetailedPositionY = positionY;
        }

        /// <summary>
        /// Gets previous current page. First page has index 1.
        /// </summary>
        public int PreviousCurrentPageIndex { get; }

        /// <summary>
        /// Gets current page. First page has index 1.
        /// </summary>
        public int CurrentPageIndex { get; }

        /// <summary>
        /// Gets a value telling whether the detailed navigation is required.
        /// </summary>
        public bool IsDetailedNavigation { get; }

        /// <summary>
        /// Get the x position of detailed position. Valid only if <see cref="IsDetailedNavigation"/> is <c>true</c>.
        /// </summary>
        public int DetailedPositionX { get; }

        /// <summary>
        /// Get the y position of detailed position. Valid only if <see cref="IsDetailedNavigation"/> is <c>true</c>.
        /// </summary>
        public int DetailedPositionY { get; }
    }
}
