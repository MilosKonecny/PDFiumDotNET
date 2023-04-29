namespace PDFiumDotNET.Apps.Common
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// The class provides all necessary license text used in applications.
    /// </summary>
    public class Licenses
    {
        #region Private fields

        private Assembly _resourceAssembly;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Licenses"/> class.
        /// </summary>
        public Licenses()
        {
            _resourceAssembly = Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Licenses"/> class.
        /// </summary>
        /// <param name="resourceAssembly">Assembly from which licenses should be read.</param>
        public Licenses(Assembly resourceAssembly)
        {
            _resourceAssembly = resourceAssembly ?? throw new ArgumentNullException(nameof(resourceAssembly));
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
                string resourceName = "PDFiumDotNET.Apps.Common.Resources.Licenses.MyOwn.LICENSE";
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
                string resourceName = "PDFiumDotNET.Apps.Common.Resources.Licenses.PDFium.LICENSE";
                return ReadEmbededResource(resourceName);
            }
        }

        #endregion Public properties

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

        private string ReadEmbededResource(string resourceName)
        {
            try
            {
                using (Stream stream = _resourceAssembly.GetManifestResourceStream(resourceName))
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
