namespace PDFiumDotNET.Components.Contracts
{
    using System;

    /// <summary>
    /// Enumeration defines all possible results of call <see cref="IPDFComponent.OpenDocument(string, string)"/> or <see cref="IPDFComponent.OpenDocument(string, Func{string})"/>.
    /// </summary>
    /// <remarks>These values are exactly the same values as defined in PDFium.</remarks>
    public enum OpenDocumentResult
    {
        /// <summary>
        /// Open the document was successful.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Open the document failed with unknown error.
        /// </summary>
        UnknownError = 1,

        /// <summary>
        /// File was not found or could not be opened.
        /// </summary>
        FileProblem = 2,

        /// <summary>
        /// File is not PDF format or corrupted.
        /// </summary>
        FormatError = 3,

        /// <summary>
        /// The password is required or password is incorrect.
        /// </summary>
        PasswordProtected = 4,

        /// <summary>
        /// File contains unsupported security scheme.
        /// </summary>
        SecurityError = 5,

        /// <summary>
        /// Page not found or content error.
        /// </summary>
        PageError = 6,

        /// <summary>
        /// Error during load of XFA.
        /// </summary>
        XFALoad = 7,

        /// <summary>
        /// Layout of XFA is unexpected.
        /// </summary>
        XFALayout = 8,
    }
}
