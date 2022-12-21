namespace PDFiumDotNET.Components
{
    using System;
    using System.IO;
    using System.Linq;
    using PDFiumDotNET.Components.Bookmark;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Information;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Find;
    using PDFiumDotNET.Components.Helper;
    using PDFiumDotNET.Components.Information;
    using PDFiumDotNET.Components.Layout;
    using PDFiumDotNET.Components.Page;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFComponent"/>
    internal sealed partial class PDFComponent
    {
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
        public IPDFPageComponent PageComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = ChildComponents.OfType<IPDFPageComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFPageComponent();
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

        /// <summary>
        /// Gets the find component.
        /// </summary>
        public IPDFFindComponent FindComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = ChildComponents.OfType<IPDFFindComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFFindComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <inheritdoc/>
        public bool IsDocumentOpened
        {
            get;
            private set;
        }

        /// <inheritdoc/>
        public void OpenDocument(string file, string password)
        {
            OpenDocument(file, () => password);
        }

        /// <inheritdoc/>
        public void OpenDocument(string file, Func<string> getPassword = null)
        {
            FileName = Path.GetFileName(file);
            FileWithPath = file;

            ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpening(file));
            PDFiumDocument = PDFiumBridge.FPDF_LoadDocument(file, null);
            if (!PDFiumDocument.IsValid)
            {
                // Something went wrong. Check password...
                if (PDFiumBridge.FPDF_GetLastError() == FPDF_ERROR.FPDF_ERR_PASSWORD && getPassword != null)
                {
                    PDFiumDocument = PDFiumBridge.FPDF_LoadDocument(file, getPassword());
                }
            }

            IsDocumentOpened = PDFiumDocument.IsValid;
            InvokePropertyChangedEvent(nameof(IsDocumentOpened));
            if (IsDocumentOpened)
            {
                ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpened(file));
            }
            else
            {
                ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpenFailed(file));
            }
        }

        /// <inheritdoc/>
        public void CloseDocument()
        {
            FileName = null;
            FileWithPath = null;

            if (!IsDocumentOpened)
            {
                // Nothing to close.
                return;
            }

            ChildComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentClosing());
            PDFiumBridge.FPDF_CloseDocument(PDFiumDocument);
            PDFiumDocument = FPDF_DOCUMENT.InvalidHandle;
            IsDocumentOpened = false;
            InvokePropertyChangedEvent(nameof(IsDocumentOpened));
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
                if (!IsDocumentOpened)
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
