namespace PDFiumDotNET.Components.Information
{
    using System;
    using PDFiumDotNET.Components.Contracts.Information;

    /// <summary>
    /// Implementation of <see cref="IPDFInformation"/>.
    /// </summary>
    internal class PDFInformation : IPDFInformation
    {
        #region Implementation of IPDFInformation

        /// <summary>
        /// Gets the document's title.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets the name of the person who created the document.
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// Gets the subject of the document.
        /// </summary>
        public string Subject { get; internal set; }

        /// <summary>
        /// Gets the keywords associated with the document.
        /// </summary>
        public string Keywords { get; internal set; }

        /// <summary>
        /// Gets the original document's creator.
        /// If the document was converted to PDF from another format,
        /// the name of the application (for example, Adobe FrameMaker)
        /// that created the original document from which it was converted.
        /// </summary>
        public string Creator { get; internal set; }

        /// <summary>
        /// Gets the document's converter.
        /// If the document was converted to PDF from another format,
        /// the name of the application (for example, Adobe FrameMaker)
        /// that converted it to PDF.
        /// </summary>
        public string Producer { get; internal set; }

        /// <summary>
        /// Gets the date and time the document was created.
        /// </summary>
        public DateTimeOffset CreationDate { get; internal set; }

        /// <summary>
        /// Gets the date the document was most recently modified.
        /// </summary>
        public DateTimeOffset ModDate { get; internal set; }

        #endregion Implementation of IPDFInformation
    }
}
