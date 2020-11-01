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
    }
}
