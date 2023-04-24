namespace PDFiumDotNET.Components.Contracts.Link
{
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;

    /// <summary>
    /// Defines properties and functionality of PDF link.
    /// </summary>
    public interface IPDFLink
    {
        /// <summary>
        /// Gets associated action of link.
        /// </summary>
        IPDFAction Action { get; }

        /// <summary>
        /// Gets associated destination of bookmark.
        /// </summary>
        IPDFDestination Destination { get; }
    }
}
