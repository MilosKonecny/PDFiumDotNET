namespace PDFiumDotNET.Samples.SimpleWpf.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using PDFiumDotNET.Samples.SimpleWpf.Base;

    /// <summary>
    /// View model class implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        #region Private fields
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
        public string AboutText
        {
            get
            {
                return "Simple Wpf application provides information how to use PDFiumDotNET library";
            }
        }


        /// <summary>
        /// Gets my own license text.
        /// </summary>
        public string MyOwnLicense
        {
            get
            {
                string resourceName = "PDFiumDotNET.Samples.SimpleWpf.Resources.Licenses.MyOwn.LICENSE";
                if (IsDesignTime)
                {
                    return resourceName;
                }

                return ReadEmbededResource(resourceName);
            }
        }

        /// <summary>
        /// Gets PDFium license text.
        /// </summary>
        public string PDFiumLicense
        {
            get
            {
                string resourceName = "PDFiumDotNET.Samples.SimpleWpf.Resources.Licenses.PDFium.LICENSE";
                if (IsDesignTime)
                {
                    return resourceName;
                }

                return ReadEmbededResource(resourceName);
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

        #region Private methods

        private string ReadEmbededResource(string resourceName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                ex = ex.InnerException;
                while (ex != null)
                {
                    message += Environment.NewLine;
                    message += ex.Message;
                }
                return message;
            }
        }

        #endregion Private methods
    }
}
