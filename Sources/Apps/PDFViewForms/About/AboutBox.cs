namespace PDFiumDotNET.Apps.PDFViewForms.About
{
    using System.Windows.Forms;
    using PDFiumDotNET.Apps.Common;

    /// <summary>
    /// Class implements about box for the 'PDFViewForms' example application.
    /// </summary>
    internal partial class AboutBox : Form
    {
        #region Private fields

        private Licenses _licenses = new Licenses();

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutBox"/> class.
        /// </summary>
        public AboutBox()
        {
            InitializeComponent();

            textBoxAbout.Text = MyOwnLicense;
            textBoxOSSLicenses.Text = PDFiumLicense;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets my own license text.
        /// </summary>
        public string MyOwnLicense
        {
            get
            {
                return _licenses.MyOwnLicense;
            }
        }

        /// <summary>
        /// Gets PDFium license text.
        /// </summary>
        public string PDFiumLicense
        {
            get
            {
                return _licenses.PDFiumLicense;
            }
        }

        #endregion Public properties
    }
}
