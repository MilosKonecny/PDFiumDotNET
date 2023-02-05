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
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFPage"/>
    internal class PDFPage : IPDFPage
    {
        #region Private fields

        private readonly PDFPageComponent _pageComponent;
        private readonly PDFComponent _mainComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPage"/> class.
        /// </summary>
        /// <param name="pageComponent">Page component where this page belongs.</param>
        /// <param name="pageIndex">Index of associated page.</param>
        public PDFPage(PDFPageComponent pageComponent, int pageIndex)
        {
            _pageComponent = pageComponent ?? throw new ArgumentNullException(nameof(pageComponent));
            _mainComponent = _pageComponent.MainComponent as PDFComponent;
            PageIndex = pageIndex;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the original height - height obtained from pdf document.
        /// </summary>
        public double OriginalHeight { get; private set; }

        /// <summary>
        /// Gets the original width - width obtained from pdf document.
        /// </summary>
        public double OriginalWidth { get; private set; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Builds page object.
        /// </summary>
        public void Build()
        {
            if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
            {
                return;
            }

            OriginalWidth = 0;
            OriginalHeight = 0;
            if (_mainComponent.PDFiumBridge.FPDF_GetPageSizeByIndexF(_mainComponent.PDFiumDocument, PageIndex, out FS_SIZEF size))
            {
                OriginalWidth = size.Width;
                OriginalHeight = size.Height;
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

        #region Implementation of IPDFPage

        /// <inheritdoc/>
        public double Width
        {
            get
            {
                return _pageComponent.PageSizeTransformation == null ? OriginalWidth : _pageComponent.PageSizeTransformation.Width(this);
            }
        }

        /// <inheritdoc/>
        public double Height
        {
            get
            {
                return _pageComponent.PageSizeTransformation == null ? OriginalHeight : _pageComponent.PageSizeTransformation.Height(this);
            }
        }

        /// <inheritdoc/>
        public string PageLabel { get; private set; }

        /// <inheritdoc/>
        public int PageIndex { get; private set; }

        /// <inheritdoc/>
        public void RenderPageBitmap(double zoomFactor, int startX, int startY, int sizeX, int sizeY, int width, int height, BitmapFormat format, IntPtr buffer, int stride)
        {
            var bmp = new PDFBitmap(_pageComponent);
            bmp.Create(width, height, format, buffer, stride);

            var pageHandle = _mainComponent.PDFiumBridge.FPDF_LoadPage(_mainComponent.PDFiumDocument, PageIndex);
            bmp.RenderWithTransformation(
                pageHandle,
                zoomFactor,
                startX,
                startY,
                sizeX,
                sizeY,
                _pageComponent.IsAnnotationToRender ? FPDF_RENDERING_FLAGS.FPDF_ANNOT : FPDF_RENDERING_FLAGS.FPDF_NONE);
            _mainComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            if (_pageComponent is PDFPageComponent pageComponent
                && pageComponent.PageIndexWithSelections == PageIndex
                && pageComponent.SelectionRectangles.Count != 0)
            {
                foreach (var rect in pageComponent.SelectionRectangles)
                {
                    bmp.RenderSelectionRectangle(zoomFactor, startX, startY, sizeY, rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }

            bmp.Destroy();
        }

        /// <inheritdoc/>
        public void RenderWholePageBitmap(BitmapFormat format, IntPtr buffer, int stride)
        {
            var bmp = new PDFBitmap(_pageComponent);
            bmp.Create((int)Width, (int)Height, format, buffer, stride);

            var pageHandle = _mainComponent.PDFiumBridge.FPDF_LoadPage(_mainComponent.PDFiumDocument, PageIndex);
            bmp.RenderWithoutTransformation(
                pageHandle,
                0,
                0,
                (int)Width,
                (int)Height,
                FPDF_RENDERING_FLAGS.FPDF_NONE);
            _mainComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            bmp.Destroy();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void NavigteTo()
        {
            _pageComponent.NavigateToPage(PageIndex + 1);
        }

        #endregion Implementation of IPDFPage
    }
}
