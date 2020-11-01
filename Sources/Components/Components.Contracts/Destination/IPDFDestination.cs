namespace PDFiumDotNET.Components.Contracts.Destination
{
    /// <summary>
    /// Interface defines destination within PDF document.
    /// </summary>
    public interface IPDFDestination
    {
        /// <summary>
        /// Gets the page index of this destination.
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Gets the x position of destination on the page in page coordinates.
        /// <c>null</c> if not defined.
        /// </summary>
        float? X { get; }

        /// <summary>
        /// Gets the y position of destination on the page in page coordinates.
        /// <c>null</c> if not defined.
        /// </summary>
        float? Y { get; }

        /// <summary>
        /// Gets the zoom factor to apply for destination page.
        /// <c>null</c> if not defined.
        /// </summary>
        float? Zoom { get; }

        /// <summary>
        /// Gets the simple info about destination for test purposes.
        /// </summary>
        string Info { get; }
    }
}
