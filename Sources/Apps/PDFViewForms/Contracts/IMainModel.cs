namespace PDFiumDotNET.Apps.PDFViewForms.Contracts
{
    using System.ComponentModel;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The interface defines model functionality used by presenter.
    /// </summary>
    internal interface IMainModel : INotifyPropertyChanged
    {
        /// <summary>
        ///  Gets the file name of open file.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> used to view PDF document content in main view.
        /// </summary>
        IPDFPageComponent PDFPageComponentForView { get; }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> used to view PDF document content in thumbnail view.
        /// </summary>
        IPDFPageComponent PDFPageComponentForThumbnail { get; }

        /// <summary>
        /// The method initializes PDFiumDotNET components.
        /// </summary>
        void InitializeComponents();

        /// <summary>
        /// Gets the information indicating whether some file is open.
        /// </summary>
        bool IsFileOpen { get; }

        /// <summary>
        /// The method opens specified file and gives result of this operation back.
        /// </summary>
        /// <param name="path">File to open.</param>
        /// <returns>The method returns operation result - <see cref="OpenDocumentResult"/>.</returns>
        OpenDocumentResult OpenFile(string path);

        /// <summary>
        /// The method closes already opened file.
        /// </summary>
        void CloseFile();

        /// <summary>
        /// The method changes the layout of pages to one column.
        /// </summary>
        void ViewPagesInOneColumn();

        /// <summary>
        /// The method changes the layout of pages to two columns.
        /// </summary>
        void ViewPagesInTwoColumns();

        /// <summary>
        /// The method changes the layout of pages to two columns, where in the first row is only one page.
        /// </summary>
        void ViewPagesInTwoColumnsSpecial();
    }
}
