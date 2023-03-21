namespace PDFiumDotNET.Apps.SimpleWpf.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using PDFiumDotNET.Apps.SimpleWpf.Base;

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

        #region Public static properties

        /// <summary>
        /// Gets about text.
        /// </summary>
        public static string AboutText
        {
            get
            {
                return "Simple Wpf application provides information how to use PDFiumDotNET library";
            }
        }

        /// <summary>
        /// Gets my own license text.
        /// </summary>
        public static string MyOwnLicense
        {
            get
            {
                string resourceName = "PDFiumDotNET.Apps.SimpleWpf.Resources.Licenses.MyOwn.LICENSE";
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
        public static string PDFiumLicense
        {
            get
            {
                string resourceName = "PDFiumDotNET.Apps.SimpleWpf.Resources.Licenses.PDFium.LICENSE";
                if (IsDesignTime)
                {
                    return resourceName;
                }

                return ReadEmbededResource(resourceName);
            }
        }

        #endregion Public static properties

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

        private static string CreateMessageFromException(Exception e)
        {
            var message = e.Message;
            e = e.InnerException;
            while (e != null)
            {
                message += Environment.NewLine;
                message += e.Message;
            }

            return message;
        }

        private static string ReadEmbededResource(string resourceName)
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
            catch (ArgumentNullException e1)
            {
                return CreateMessageFromException(e1);
            }
            catch (ArgumentException e2)
            {
                return CreateMessageFromException(e2);
            }
            catch (FileLoadException e3)
            {
                return CreateMessageFromException(e3);
            }
            catch (FileNotFoundException e4)
            {
                return CreateMessageFromException(e4);
            }
            catch (BadImageFormatException e5)
            {
                return CreateMessageFromException(e5);
            }
            catch (NotImplementedException e6)
            {
                return CreateMessageFromException(e6);
            }
        }

        #endregion Private methods
    }
}
