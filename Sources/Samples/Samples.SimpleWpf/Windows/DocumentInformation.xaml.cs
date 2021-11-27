namespace PDFiumDotNET.Samples.SimpleWpf.Windows
{
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

        #endregion Public properties
    }
}
