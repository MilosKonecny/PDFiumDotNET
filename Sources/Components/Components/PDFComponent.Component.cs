namespace PDFiumDotNET.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PDFiumDotNET.Components.Bookmark;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;
    using PDFiumDotNET.Components.Page;
    using PDFiumDotNET.Components.Zoom;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFComponent"/>
    /// </summary>
    public sealed partial class PDFComponent
    {
        #region Implementation of IPDFComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFPageComponent PageComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = _childComponents.OfType<IPDFPageComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFPageComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFBookmarkComponent BookmarkComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = _childComponents.OfType<IPDFBookmarkComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFBookmarkComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFZoomComponent ZoomComponent
        {
            get
            {
                if (IsDisposed)
                {
                    return null;
                }

                var component = _childComponents.OfType<IPDFZoomComponent>().FirstOrDefault();
                if (component == null)
                {
                    component = new PDFZoomComponent();
                    Attach(component);
                }

                return component;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<IPDFChildComponent> ChildComponents
        {
            get
            {
                return _childComponents.OfType<IPDFChildComponent>();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsDocumentOpened
        {
            get;
            private set;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsDisposed
        {
            get;
            private set;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Attach(IPDFChildComponent childComponent)
        {
            if (childComponent == null)
            {
                throw new ArgumentNullException(nameof(childComponent));
            }

            _childComponents.Add(childComponent);
            childComponent.AttachedTo(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void OpenDocument(string file, string password)
        {
            OpenDocument(file, () => password);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void OpenDocument(string file, Func<string> getPassword = null)
        {
            _childComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpening(file));
            PDFiumDocument = PDFiumBridge.FPDF_LoadDocument(file, null);
            if (!PDFiumDocument.IsValid)
            {
                // Something went wrong. Check password...
                if (PDFiumBridge.FPDF_GetLastError() == FPDF_ERR_PASSWORD && getPassword != null)
                {
                    PDFiumDocument = PDFiumBridge.FPDF_LoadDocument(file, getPassword());
                }
            }

            IsDocumentOpened = PDFiumDocument.IsValid;
            InvokePropertyChangedEvent(nameof(IsDocumentOpened));
            if (IsDocumentOpened)
            {
                _childComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpened(file));
            }
            else
            {
                _childComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentOpenFailed(file));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CloseDocument()
        {
            if (!IsDocumentOpened)
            {
                // Nothing to close.
                return;
            }

            _childComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentClosing());
            PDFiumBridge.FPDF_CloseDocument(PDFiumDocument);
            PDFiumDocument = FPDF_DOCUMENT.InvalidHandle;
            IsDocumentOpened = false;
            InvokePropertyChangedEvent(nameof(IsDocumentOpened));
            _childComponents.OfType<IPDFDocumentObserver>().ToList().ForEach(a => a.DocumentClosed());
        }

        #endregion Implementation of IPDFComponent
    }
}
