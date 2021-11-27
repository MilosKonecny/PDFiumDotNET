namespace PDFiumDotNET.Components.Contracts.Information
{
    using System;

    /// <summary>
    /// Interface defines all possible properties of opened PDF document.
    /// </summary>
    public interface IPDFInformation
    {
        /// <summary>
        /// Gets the document's title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the name of the person who created the document.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the subject of the document.
        /// </summary>
        string Subject { get; }

        /// <summary>
        /// Gets the keywords associated with the document.
        /// </summary>
        string Keywords { get; }

        /// <summary>
        /// Gets the original document's creator.
        /// If the document was converted to PDF from another format,
        /// the name of the application (for example, Adobe FrameMaker)
        /// that created the original document from which it was converted.
        /// </summary>
        string Creator { get; }

        /// <summary>
        /// Gets the document's converter.
        /// If the document was converted to PDF from another format,
        /// the name of the application (for example, Adobe FrameMaker)
        /// that converted it to PDF.
        /// </summary>
        string Producer { get; }

        /// <summary>
        /// Gets the date and time the document was created.
        /// </summary>
        DateTimeOffset CreationDate { get; }

        /// <summary>
        /// Gets the date the document was most recently modified.
        /// </summary>
        DateTimeOffset ModDate { get; }
    }
}
