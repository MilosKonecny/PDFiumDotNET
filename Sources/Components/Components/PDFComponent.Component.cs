﻿namespace PDFiumDotNET.Components
{
    using System;
    using System.IO;
    using System.Linq;
    using PDFiumDotNET.Components.Bookmark;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Information;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Helper;
    using PDFiumDotNET.Components.Information;
    using PDFiumDotNET.Components.Layout;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// The class implements the functionality defined by <see cref="IPDFComponent"/>.
    /// </summary>
    internal sealed partial class PDFComponent
    {
        #region Private fields

        private bool _isDocumentOpen;

        #endregion Private fields

        #region Implementation of IPDFComponent

        /// <inheritdoc/>
        public IPDFLayoutComponent LayoutComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = ChildComponents.OfType<IPDFLayoutComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFLayoutComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <inheritdoc/>
        public IPDFBookmarkComponent BookmarkComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = ChildComponents.OfType<IPDFBookmarkComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFBookmarkComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <inheritdoc/>
        public bool IsDocumentOpen
        {
            get
            {
                return _isDocumentOpen;
            }

            private set
            {
                if (_isDocumentOpen != value)
                {
                    _isDocumentOpen = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <inheritdoc/>
        public OpenDocumentResult OpenDocument(string file, string password)
        {
            // Abort in second call;
            var callCounter = 0;
            return OpenDocument(file, () => callCounter++ > 0 ? null : password);
        }

        /// <inheritdoc/>
        public OpenDocumentResult OpenDocument(string file, Func<string> getPassword = null)
        {
            var result = OpenDocumentResult.UnknownError;

            FileName = Path.GetFileName(file);
            FileWithPath = file;

            ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpening(file));
            PDFiumDocument = PDFiumBridge.FPDF_LoadDocument(file, null);
            if (!PDFiumDocument.IsValid)
            {
                // Something went wrong. Check password error ...
                if (PDFiumBridge.FPDF_GetLastError() == FPDF_ERROR.FPDF_ERR_PASSWORD && getPassword != null)
                {
                    while (!PDFiumDocument.IsValid)
                    {
                        var ret = getPassword();
                        if (ret == null)
                        {
                            // Aborted
                            return OpenDocumentResult.PasswordProtected;
                        }

                        PDFiumDocument = PDFiumBridge.FPDF_LoadDocument(file, ret);

                        if (PDFiumDocument.IsValid || PDFiumBridge.FPDF_GetLastError() != FPDF_ERROR.FPDF_ERR_PASSWORD)
                        {
                            break;
                        }
                    }
                }
            }

            IsDocumentOpen = PDFiumDocument.IsValid;

            if (IsDocumentOpen)
            {
                ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpened(file));
                result = OpenDocumentResult.Success;
            }
            else
            {
                ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpenFailed(file));
                result = (OpenDocumentResult)PDFiumBridge.FPDF_GetLastError();
            }

            return result;
        }

        /// <inheritdoc/>
        public void CloseDocument()
        {
            FileName = null;
            FileWithPath = null;

            if (!IsDocumentOpen)
            {
                // Nothing to close.
                return;
            }

            ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentClosing());
            PDFiumBridge.FPDF_CloseDocument(PDFiumDocument);
            PDFiumDocument = FPDF_DOCUMENT.InvalidHandle;
            IsDocumentOpen = false;
            ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentClosed());
        }

        /// <summary>
        /// Gets available document's information from opened PDF document.
        /// </summary>
        public IPDFInformation DocumentInformation
        {
            get
            {
                var retValue = new PDFInformation();
                if (!IsDocumentOpen)
                {
                    return retValue;
                }

                // Title
                retValue.Title = ReadMetaText(nameof(PDFInformation.Title));

                // Author
                retValue.Author = ReadMetaText(nameof(PDFInformation.Author));

                // Subject
                retValue.Subject = ReadMetaText(nameof(PDFInformation.Subject));

                // Keywords
                retValue.Keywords = ReadMetaText(nameof(PDFInformation.Keywords));

                // Creator
                retValue.Creator = ReadMetaText(nameof(PDFInformation.Creator));

                // Producer
                retValue.Producer = ReadMetaText(nameof(PDFInformation.Producer));

                // CreationDate
                retValue.CreationDate = DataConverter.Asn1DateTimeToToDateTime(ReadMetaText(nameof(PDFInformation.CreationDate)));

                // ModDate
                retValue.ModDate = DataConverter.Asn1DateTimeToToDateTime(ReadMetaText(nameof(PDFInformation.ModDate)));

                // Permissions
                retValue.DocumentPermissions = PDFPermissions.AllFlags & (PDFPermissions)PDFiumBridge.FPDF_GetDocPermissions(PDFiumDocument);

                return retValue;
            }
        }

        /// <summary>
        /// Gets opened file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets opened file with path.
        /// </summary>
        public string FileWithPath { get; private set; }

        #endregion Implementation of IPDFComponent
    }
}
