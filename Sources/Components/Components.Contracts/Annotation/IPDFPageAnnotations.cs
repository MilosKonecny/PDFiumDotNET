namespace PDFiumDotNET.Components.Contracts.Annotation
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The interface defines functionality of page's annotations.
    /// </summary>
    public interface IPDFPageAnnotations
    {
        /// <summary>
        /// Gets related page where the searched text was found.
        /// </summary>
        IPDFPage RelatedPage { get; }

        /// <summary>
        /// Gets the count of annotations on related page.
        /// </summary>
        int Count { get; }
    }
}
