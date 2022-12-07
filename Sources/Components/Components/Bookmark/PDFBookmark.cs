namespace PDFiumDotNET.Components.Bookmark
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Action;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Destination;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFBookmark"/>
    internal class PDFBookmark : IPDFBookmark
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;
        private FPDF_BOOKMARK _bookmarkHandle;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFBookmark"/> class.
        /// </summary>
        /// <param name="mainComponent">Main component where this bookmark belongs.</param>
        /// <param name="bookmarkHandle">Handle of this bookmark.</param>
        public PDFBookmark(PDFComponent mainComponent, FPDF_BOOKMARK bookmarkHandle)
        {
            _mainComponent = mainComponent ?? throw new ArgumentNullException(nameof(mainComponent));
            _bookmarkHandle = bookmarkHandle;

            Bookmarks = new ObservableCollection<IPDFBookmark>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Builds bookmark object.
        /// </summary>
        public void Build()
        {
            if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
            {
                return;
            }

            var childBookmarkHandle = _mainComponent.PDFiumBridge.FPDFBookmark_GetFirstChild(_mainComponent.PDFiumDocument, _bookmarkHandle);
            while (childBookmarkHandle.IsValid)
            {
                var newBookmark = new PDFBookmark(_mainComponent, childBookmarkHandle);
                newBookmark.Build();
                Bookmarks.Add(newBookmark);
                childBookmarkHandle = _mainComponent.PDFiumBridge.FPDFBookmark_GetNextSibling(_mainComponent.PDFiumDocument, childBookmarkHandle);
            }

            IsOpened = _mainComponent.PDFiumBridge.FPDFBookmark_GetCount(_bookmarkHandle) > 0;
        }

        #endregion Public methods

        #region Implementation of IPDFBookmark

        /// <inheritdoc/>
        public IPDFAction Action
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var action = _mainComponent.PDFiumBridge.FPDFBookmark_GetAction(_bookmarkHandle);
                if (!action.IsValid)
                {
                    return null;
                }

                return new PDFAction(_mainComponent, action);
            }
        }

        /// <inheritdoc/>
        public ObservableCollection<IPDFBookmark> Bookmarks { get; private set; }

        /// <inheritdoc/>
        public bool IsOpened { get; private set; }

        /// <inheritdoc/>
        public IPDFDestination Destination
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var destination = _mainComponent.PDFiumBridge.FPDFBookmark_GetDest(_mainComponent.PDFiumDocument, _bookmarkHandle);
                if (!destination.IsValid)
                {
                    return null;
                }

                return new PDFDestination(_mainComponent, destination);
            }
        }

        /// <inheritdoc/>
        public string Text
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var requiredLen = _mainComponent.PDFiumBridge.FPDFBookmark_GetTitle(_bookmarkHandle, IntPtr.Zero, 0);
                if (requiredLen <= 0)
                {
                    return null;
                }

                var buffer = Marshal.AllocHGlobal(requiredLen);
                _mainComponent.PDFiumBridge.FPDFBookmark_GetTitle(_bookmarkHandle, buffer, (ulong)requiredLen);
                var text = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);

                return text;
            }
        }

        #endregion Implementation of IPDFBookmark
    }
}
