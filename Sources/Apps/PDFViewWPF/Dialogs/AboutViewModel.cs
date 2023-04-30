namespace PDFiumDotNET.Apps.PDFViewWPF.Dialogs
{
    using System.ComponentModel;
    using System.Windows;
    using PDFiumDotNET.Apps.Common;
    using PDFiumDotNET.Apps.PDFViewWPF.Base;

    /// <summary>
    /// View model class implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        #region Private fields

        private Licenses _licenses = new Licenses();

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
            : base()
        {
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets about text.
        /// </summary>
        public static string AboutText
        {
            get
            {
                return "WPF application provides information how to use PDFiumDotNET library";
            }
        }

        /// <summary>
        /// Gets my own license text.
        /// </summary>
        public string MyOwnLicense
        {
            get
            {
                string resourceName = "PDFiumDotNET.Apps.PDFViewWPF.Resources.Licenses.MyOwn.LICENSE";
                if (IsDesignTime)
                {
                    return resourceName;
                }

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
                string resourceName = "PDFiumDotNET.Apps.PDFViewWPF.Resources.Licenses.PDFium.LICENSE";
                if (IsDesignTime)
                {
                    return resourceName;
                }

                return _licenses.PDFiumLicense;
            }
        }

        #endregion Public properties

        #region Private properties

        private static bool IsDesignTime
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }

        #endregion Private properties
    }
}
