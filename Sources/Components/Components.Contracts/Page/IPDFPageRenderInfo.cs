namespace PDFiumDotNET.Components.Contracts.Page
{
    /// <summary>
    /// Interface defines information about page to render.
    /// </summary>
    public interface IPDFPageRenderInfo
    {
        /// <summary>
        /// Gets the page estimated to render.
        /// </summary>
        IPDFPage Page { get; }

        /// <summary>
        /// Gets or sets the left line of page.
        /// </summary>
        double Left { get; set; }

        /// <summary>
        /// Gets or sets the right line of page.
        /// </summary>
        double Right { get; set; }

        /// <summary>
        /// Gets or sets the top line of page.
        /// </summary>
        double Top { get; set; }

        /// <summary>
        /// Gets or sets the bottom line of page.
        /// </summary>
        double Bottom { get; set; }

        /// <summary>
        /// Gets or sets the value indicating that this page is in the center of vertical direction of viewport.
        /// </summary>
        bool IsOnCenter { get; set; }

        /// <summary>
        /// Gets or sets the position of center of vertical direction of viewport on this page.
        /// Valid only if <see cref="IsOnCenter"/> is <c>true</c>.
        /// </summary>
        double PagePositionOnCenter { get; set; }
    }
}
