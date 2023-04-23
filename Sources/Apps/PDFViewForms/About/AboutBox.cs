namespace PDFiumDotNET.Apps.PDFViewForms.About
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// Class implements about box for the 'PDFViewForms' example application.
    /// </summary>
    internal partial class AboutBox : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutBox"/> class.
        /// </summary>
        public AboutBox()
        {
            InitializeComponent();

            textBoxAbout.Text = MyOwnLicense;
            textBoxOSSLicenses.Text = PDFiumLicense;
        }

        /// <summary>
        /// Gets my own license text.
        /// </summary>
        public static string MyOwnLicense
        {
            get
            {
                string resourceName = "PDFiumDotNET.Apps.PDFViewForms.Resources.Licenses.MyOwn.LICENSE";
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
                string resourceName = "PDFiumDotNET.Apps.PDFViewForms.Resources.Licenses.PDFium.LICENSE";
                return ReadEmbededResource(resourceName);
            }
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
    }
}
