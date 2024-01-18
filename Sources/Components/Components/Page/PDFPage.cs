namespace PDFiumDotNET.Components.Page
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Annotation;
    using PDFiumDotNET.Components.Bitmap;
    using PDFiumDotNET.Components.Contracts.Annotation;
    using PDFiumDotNET.Components.Contracts.Bitmap;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Link;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// The class implements functionality defined by <see cref="IPDFPage"/>.
    /// </summary>
    internal class PDFPage : IPDFPage
    {
        #region Private fields

        private readonly PDFPageComponent _pageComponent;

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
            PageIndex = pageIndex;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the original height - height obtained from PDF document.
        /// </summary>
        public double OriginalHeight { get; private set; }

        /// <summary>
        /// Gets the original width - width obtained from PDF document.
        /// </summary>
        public double OriginalWidth { get; private set; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Builds page object.
        /// </summary>
        public void Build()
        {
            if (_pageComponent.PDFComponent.PDFiumBridge == null || !_pageComponent.PDFComponent.PDFiumDocument.IsValid)
            {
                return;
            }

            OriginalWidth = 0;
            OriginalHeight = 0;
            if (_pageComponent.PDFComponent.PDFiumBridge.FPDF_GetPageSizeByIndexF(_pageComponent.PDFComponent.PDFiumDocument, PageIndex, out FS_SIZEF size))
            {
                OriginalWidth = size.Width;
                OriginalHeight = size.Height;
            }

            PageLabel = null;
            var requiredLen = _pageComponent.PDFComponent.PDFiumBridge.FPDF_GetPageLabel(_pageComponent.PDFComponent.PDFiumDocument, PageIndex, IntPtr.Zero, 0);
            if (requiredLen > 0)
            {
                var buffer = Marshal.AllocHGlobal(requiredLen);
                _pageComponent.PDFComponent.PDFiumBridge.FPDF_GetPageLabel(_pageComponent.PDFComponent.PDFiumDocument, PageIndex, buffer, (ulong)requiredLen);
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
        public double TransformationZoom
        {
            get
            {
                return _pageComponent.PageSizeTransformation == null ? 1d : _pageComponent.PageSizeTransformation.TransformationZoom(this);
            }
        }

        /// <inheritdoc/>
        public string PageLabel { get; private set; }

        /// <inheritdoc/>
        public int PageIndex { get; private set; }

        /// <inheritdoc/>
        public IPDFPageAnnotations PageAnnotations
        {
            get
            {
                var pageHandle = _pageComponent.PDFComponent.PDFiumBridge.FPDF_LoadPage(_pageComponent.PDFComponent.PDFiumDocument, PageIndex);
                var count = _pageComponent.PDFComponent.PDFiumBridge.FPDFPage_GetAnnotCount(pageHandle);
                _pageComponent.PDFComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);
                return new PDFPageAnnotations(this, count);
            }
        }

        /// <inheritdoc/>
        public void RenderPageBitmap(double zoomFactor, int startX, int startY, int sizeX, int sizeY, int width, int height, BitmapFormat format, IntPtr buffer, int stride)
        {
            var bmp = new PDFBitmap(_pageComponent);
            bmp.Create(width, height, format, buffer, stride);

            var pageHandle = _pageComponent.PDFComponent.PDFiumBridge.FPDF_LoadPage(_pageComponent.PDFComponent.PDFiumDocument, PageIndex);
            bmp.RenderWithTransformation(
                pageHandle,
                zoomFactor,
                startX,
                startY,
                sizeX,
                sizeY,
                _pageComponent.IsAnnotationToRender ? FPDF_RENDERING_FLAGS.FPDF_ANNOT : FPDF_RENDERING_FLAGS.FPDF_NONE);
            _pageComponent.PDFComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            if (_pageComponent.PageIndexWithSelections == PageIndex && _pageComponent.SelectionRectangles.Count != 0)
            {
                foreach (var rect in _pageComponent.SelectionRectangles)
                {
                    bmp.RenderSelectionRectangle(zoomFactor, startX, startY, (int)(Height * zoomFactor), rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }

            bmp.Destroy();
        }

        /// <inheritdoc/>
        public void RenderWholePageBitmap(BitmapFormat format, IntPtr buffer, int stride)
        {
            var bmp = new PDFBitmap(_pageComponent);
            bmp.Create((int)Width, (int)Height, format, buffer, stride);

            var pageHandle = _pageComponent.PDFComponent.PDFiumBridge.FPDF_LoadPage(_pageComponent.PDFComponent.PDFiumDocument, PageIndex);
            bmp.RenderWithoutTransformation(
                pageHandle,
                0,
                0,
                (int)Width,
                (int)Height,
                FPDF_RENDERING_FLAGS.FPDF_NONE);
            _pageComponent.PDFComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            bmp.Destroy();
        }

        /// <inheritdoc/>
        public IPDFLink GetLinkFromPoint(double x, double y)
        {
            var pageHandle = _pageComponent.PDFComponent.PDFiumBridge.FPDF_LoadPage(_pageComponent.PDFComponent.PDFiumDocument, PageIndex);
            var linkHandle = _pageComponent.PDFComponent.PDFiumBridge.FPDFLink_GetLinkAtPoint(pageHandle, x, y);
            _pageComponent.PDFComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);

            if (linkHandle.IsValid)
            {
                return new PDFLink(_pageComponent.PDFComponent, linkHandle);
            }

            return null;
        }

        /// <inheritdoc/>
        public void NavigateTo()
        {
            _pageComponent.NavigateToPage(PageIndex + 1);
        }

        #endregion Implementation of IPDFPage
    }
}
