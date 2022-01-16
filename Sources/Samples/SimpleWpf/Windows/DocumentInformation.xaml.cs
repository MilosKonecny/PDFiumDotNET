namespace PDFiumDotNET.Samples.SimpleWpf.Windows
{
    using System;
    using System.Windows;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Information;

    /// <summary>
    /// Interaction logic for DocumentInformation.
    /// </summary>
    public partial class DocumentInformation : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentInformation"/> class.
        /// </summary>
        /// <param name="pdfComponent">Related pdf component.</param>
        public DocumentInformation(IPDFComponent pdfComponent)
        {
            if (pdfComponent == null)
            {
                throw new ArgumentNullException(nameof(pdfComponent));
            }

            InitializeComponent();

            DataContext = this;
            PDFFFileName = pdfComponent.FileName;
            PdfInformation = pdfComponent.DocumentInformation;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the file name of opened PDF document.
        /// </summary>
        public string PDFFFileName { get; }

        /// <summary>
        /// Gets document's information.
        /// </summary>
        public IPDFInformation PdfInformation { get; }

        /// <summary>
        /// Gets permission - print document.
        /// </summary>
        public string PermissionPrintDocument
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.PrintDocument) == PDFPermissions.PrintDocument);
            }
        }

        /// <summary>
        /// Gets permission - modify contents.
        /// </summary>
        public string PermissionModifyContents
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.ModifyContents) == PDFPermissions.ModifyContents);
            }
        }

        /// <summary>
        /// Gets permission - extract text and graphics.
        /// </summary>
        public string PermissionExtractTextAndGraphics
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.ExtractTextAndGraphics) == PDFPermissions.ExtractTextAndGraphics);
            }
        }

        /// <summary>
        /// Gets permission - add or modify text annotations.
        /// </summary>
        public string PermissionAddOrModifyTextAnnotations
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.AddOrModifyTextAnnotations) == PDFPermissions.AddOrModifyTextAnnotations);
            }
        }

        /// <summary>
        /// Gets permission - fill form fields.
        /// </summary>
        public string PermissionFillFormFields
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.FillFormFields) == PDFPermissions.FillFormFields);
            }
        }

        /// <summary>
        /// Gets permission - extract text and graphics disabilities.
        /// </summary>
        public string PermissionExtractTextAndGraphicsDisabilities
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.ExtractTextAndGraphicsDisabilities) == PDFPermissions.ExtractTextAndGraphicsDisabilities);
            }
        }

        /// <summary>
        /// Gets permission - assemble document.
        /// </summary>
        public string PermissionAssembleDocument
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.AssembleDocument) == PDFPermissions.AssembleDocument);
            }
        }

        /// <summary>
        /// Gets permission - print document high quality.
        /// </summary>
        public string PermissionPrintDocumentHighQuality
        {
            get
            {
                return Allowed((PdfInformation?.DocumentPermissions & PDFPermissions.PrintDocumentHighQuality) == PDFPermissions.PrintDocumentHighQuality);
            }
        }

        #endregion Public properties

        #region Private static methods

        private static string Allowed(bool allowed)
        {
            return allowed ? "Allowed" : "Not allowed";
        }

        #endregion Private static methods
    }
}
