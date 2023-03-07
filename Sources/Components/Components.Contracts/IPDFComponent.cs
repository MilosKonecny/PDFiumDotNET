namespace PDFiumDotNET.Components.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Information;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Defines functionality of main component used in namespace <see cref="PDFiumDotNET.Components"/>.
    /// This component is parent component for every <see cref="IPDFChildComponent"/>.
    /// </summary>
    public interface IPDFComponent : IPDFBaseComponent
    {
        /// <summary>
        /// Gets the layout component that provides multiple page components (<see cref="IPDFPageComponent"/>).
        /// </summary>
        IPDFLayoutComponent LayoutComponent { get; }

        /// <summary>
        /// Gets the bookmark component.
        /// </summary>
        IPDFBookmarkComponent BookmarkComponent { get; }

        /// <summary>
        /// Gets a value indicating whether pdf document is opened or not.
        /// </summary>
        bool IsDocumentOpened { get; }

        /// <summary>
        /// Closes pdf document.
        /// </summary>
        void CloseDocument();

        /// <summary>
        /// Opens given pdf document.
        /// </summary>
        /// <param name="file">Pdf file to open.</param>
        /// <param name="password">Password for protected document.</param>
        OpenDocumentResult OpenDocument(string file, string password);

        /// <summary>
        /// Opens given pdf document.
        /// </summary>
        /// <param name="file">Pdf file to open.</param>
        /// <param name="getPassword">The callback function is used when the PDF file to be opened is password-protected.
        /// The function is called until a correct password or <c>null</c> is returned. If <c>null</c> is returned, the open is aborted.</param>
        OpenDocumentResult OpenDocument(string file, Func<string> getPassword = null);

        /// <summary>
        /// Gets available document's information from opened PDF document.
        /// </summary>
        IPDFInformation DocumentInformation { get; }

        /// <summary>
        /// Gets opened file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets opened file with path.
        /// </summary>
        string FileWithPath { get; }
    }
}
