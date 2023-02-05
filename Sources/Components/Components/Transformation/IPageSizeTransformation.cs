namespace PDFiumDotNET.Components.Transformation
{
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// Interface defines the functionality of page size transformation.
    /// </summary>
    internal interface IPageSizeTransformation
    {
        /// <summary>
        /// Computes the transformed page width.
        /// </summary>
        /// <param name="page">Page to transform width for.</param>
        /// <returns>Transformed width.</returns>
        double Width(PDFPage page);

        /// <summary>
        /// Computes the transformed page height.
        /// </summary>
        /// <param name="page">Page to transform height for.</param>
        /// <returns>Transformed height.</returns>
        double Height(PDFPage page);
    }
}
