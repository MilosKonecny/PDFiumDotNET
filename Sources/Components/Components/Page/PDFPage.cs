namespace PDFiumDotNET.Components.Page
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Bitmap;
    using PDFiumDotNET.Components.Contracts.Bitmap;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Link;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFPage"/>
    /// </summary>
    internal class PDFPage : IPDFPage
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPage"/> class.
        /// </summary>
        /// <param name="mainComponent">Main component where this bookmark belongs.</param>
        /// <param name="pageIndex">Index of associated page.</param>
        public PDFPage(PDFComponent mainComponent, int pageIndex)
        {
            _mainComponent = mainComponent ?? throw new ArgumentNullException(nameof(mainComponent));
            PageIndex = pageIndex;
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

            Width = 0;
            Height = 0;
            if (_mainComponent.PDFiumBridge.FPDF_GetPageSizeByIndexF(_mainComponent.PDFiumDocument, PageIndex, out FS_SIZEF size))
            {
                Width = size.Width;
                Height = size.Height;
            }

            PageLabel = null;
            var requiredLen = _mainComponent.PDFiumBridge.FPDF_GetPageLabel(_mainComponent.PDFiumDocument, PageIndex, IntPtr.Zero, 0);
            if (requiredLen > 0)
            {
                var buffer = Marshal.AllocHGlobal(requiredLen);
                _mainComponent.PDFiumBridge.FPDF_GetPageLabel(_mainComponent.PDFiumDocument, PageIndex, buffer, (ulong)requiredLen);
                PageLabel = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);
            }
            else
            {
                PageLabel = (PageIndex + 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        #endregion Public methods

        #region Implementation of IPDFBookmark

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double ThumbnailHeight => Height / 6;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double ThumbnailWidth => Width / 6;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string PageLabel { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void RenderPageBitmap(double zoomFactor, int startX, int startY, int sizeX, int sizeY, int width, int height, BitmapFormat format, IntPtr buffer, int stride)
        {
            var bmp = new PDFBitmap(_mainComponent);
            bmp.Create(width, height, format, buffer, stride);

            var pageHandle = _mainComponent.PDFiumBridge.FPDF_LoadPage(_mainComponent.PDFiumDocument, PageIndex);
            bmp.RenderWithTransformation(pageHandle, zoomFactor, startX, startY, sizeX, sizeY);
            _mainComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            bmp.Destroy();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void RenderThumbnailBitmap(BitmapFormat format, IntPtr buffer, int stride)
        {
            var bmp = new PDFBitmap(_mainComponent);
            bmp.Create((int)ThumbnailWidth, (int)ThumbnailHeight, format, buffer, stride);

            var pageHandle = _mainComponent.PDFiumBridge.FPDF_LoadPage(_mainComponent.PDFiumDocument, PageIndex);
            bmp.RenderWithoutTransformation(pageHandle, 0, 0, (int)ThumbnailWidth, (int)ThumbnailHeight);
            _mainComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            bmp.Destroy();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFLink GetLinkFromPoint(double x, double y)
        {
            var pageHandle = _mainComponent.PDFiumBridge.FPDF_LoadPage(_mainComponent.PDFiumDocument, PageIndex);
            var linkHandle = _mainComponent.PDFiumBridge.FPDFLink_GetLinkAtPoint(pageHandle, x, y);
            _mainComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            if (linkHandle.IsValid)
            {
                return new PDFLink(_mainComponent, linkHandle);
            }

            return null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void NavigteTo()
        {
            _mainComponent.PageComponent.NavigateToPage(PageIndex + 1);
        }

        #endregion Implementation of IPDFBookmark
    }
}
