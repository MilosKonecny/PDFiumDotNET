namespace PDFiumDotNET.Components.Bitmap
{
    using System;
    using PDFiumDotNET.Components.Contracts.Bitmap;
    using PDFiumDotNET.Components.Page;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFBitmap"/>
    internal class PDFBitmap : IPDFBitmap
    {
        #region Private fields

        private readonly PDFPageComponent _pageComponent;
        private readonly PDFComponent _mainComponent;
        private FPDF_BITMAP _bitmapHandle;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFBitmap"/> class.
        /// </summary>
        /// <param name="pageComponent">Page component where this bitmap belongs.</param>
        public PDFBitmap(PDFPageComponent pageComponent)
        {
            _pageComponent = pageComponent ?? throw new ArgumentNullException(nameof(pageComponent));
            _mainComponent = _pageComponent.MainComponent as PDFComponent;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Render the rectangle to the bitmap.
        /// </summary>
        /// <param name="zoomFactor">Zoom factor to use for transformation.</param>
        /// <param name="startX">Left pixel position of the page area to render.</param>
        /// <param name="startY">Top pixel position of the page area to render.</param>
        /// <param name="sizeY">Height of the page area to render.</param>
        /// <param name="rectStartX">Left pixel position of the rectangle to render.</param>
        /// <param name="rectStartY">Top pixel position of the rectangle to render.</param>
        /// <param name="rectSizeX">Width of the rectangle to render.</param>
        /// <param name="rectSizeY">Height of the rectangle to render.</param>
        public void RenderSelectionRectangle(
            double zoomFactor,
            int startX,
            int startY,
            int sizeY,
            double rectStartX,
            double rectStartY,
            double rectSizeX,
            double rectSizeY)
        {
            int left = (int)Math.Round(rectStartX * zoomFactor, 0) + startX - 2;
            int top = sizeY - (int)Math.Round(rectStartY * zoomFactor, 0) + startY - 2;
            int width = (int)Math.Round(rectSizeX * zoomFactor, 0) + 4;
            int height = -(int)Math.Round(rectSizeY * zoomFactor, 0) + 4;

            // Colors
            var colorBackground = new FPDF_COLOR(0xFF, 0xFF, 0xA0, 0x3F);
            var colorBorder = new FPDF_COLOR(0x00, 0x00, 0xFF, 0xFF);
            if (_pageComponent.FindSelectionBackgroundFunc != null)
            {
                colorBackground = new FPDF_COLOR(_pageComponent.FindSelectionBackgroundFunc());
            }

            if (_pageComponent.FindSelectionBorderFunc != null)
            {
                colorBorder = new FPDF_COLOR(_pageComponent.FindSelectionBorderFunc());
            }

            // Background
            _mainComponent.PDFiumBridge.FPDFBitmap_FillRect(_bitmapHandle, left, top, width, height, colorBackground);

            // Bottom border
            _mainComponent.PDFiumBridge.FPDFBitmap_FillRect(_bitmapHandle, left - 2, top + height, width + 4, 2, colorBorder);

            // Top border
            _mainComponent.PDFiumBridge.FPDFBitmap_FillRect(_bitmapHandle, left - 2, top - 2, width + 2, 2, colorBorder);

            // Left border
            _mainComponent.PDFiumBridge.FPDFBitmap_FillRect(_bitmapHandle, left - 2, top - 2, 2, height + 2, colorBorder);

            // Right border
            _mainComponent.PDFiumBridge.FPDFBitmap_FillRect(_bitmapHandle, left + width, top - 2, 2, height + 2, colorBorder);
        }

        /// <summary>
        /// Render the page to the bitmap.
        /// </summary>
        /// <param name="page">Page to render.</param>
        /// <param name="startX">Left pixel position of the page area to render.</param>
        /// <param name="startY">Top pixel position of the page area to render.</param>
        /// <param name="sizeX">Width of the page area to render.</param>
        /// <param name="sizeY">Height of the page area to render.</param>
        /// <param name="flags">Rendering flags to use for rendering.</param>
        public void RenderWithoutTransformation(FPDF_PAGE page, int startX, int startY, int sizeX, int sizeY, FPDF_RENDERING_FLAGS flags)
        {
            _mainComponent.PDFiumBridge.FPDF_RenderPageBitmap(_bitmapHandle, page, startX, startY, sizeX, sizeY, 0, flags);
        }

        /// <summary>
        /// Render the page to the bitmap.
        /// </summary>
        /// <param name="page">Page to render.</param>
        /// <param name="zoomFactor">Zoom factor to use for transformation.</param>
        /// <param name="startX">Left pixel position of the page area to render.</param>
        /// <param name="startY">Top pixel position of the page area to render.</param>
        /// <param name="sizeX">Width of the page area to render.</param>
        /// <param name="sizeY">Height of the page area to render.</param>
        /// <param name="flags">Rendering flags to use for rendering.</param>
        public void RenderWithTransformation(FPDF_PAGE page, double zoomFactor, int startX, int startY, int sizeX, int sizeY, FPDF_RENDERING_FLAGS flags)
        {
            // Translation is performed with [1 0 0 1 tx ty].
            // Scaling is performed with [sx 0 0 sy 0 0].
            FS_MATRIX matrix = new FS_MATRIX { A = (float)zoomFactor, B = 0, C = 0, D = (float)zoomFactor, E = startX > 0f ? 0f : startX, F = startY > 0f ? 0f : startY };
            FS_RECTF rect = new FS_RECTF { Left = startX, Right = startX + sizeX, Top = startY, Bottom = startY + sizeY };
            _mainComponent.PDFiumBridge.FPDF_RenderPageBitmapWithMatrix(_bitmapHandle, page, ref matrix, ref rect, flags);
        }

        /// <summary>
        /// Create bitmap based on given parameters.
        /// </summary>
        /// <param name="width">Required width.</param>
        /// <param name="height">Required height.</param>
        /// <param name="format">Required format.</param>
        /// <param name="buffer">Required buffer.</param>
        /// <param name="stride">Required stride.</param>
        public void Create(int width, int height, BitmapFormat format, IntPtr buffer, int stride)
        {
            if (width <= 0)
            {
                throw new ArgumentException($"Parameter {nameof(width)} has wrong value - {width}");
            }

            if (height <= 0)
            {
                throw new ArgumentException($"Parameter {nameof(height)} has wrong value - {height}");
            }

            Width = width;
            Height = height;

            _bitmapHandle = _mainComponent.PDFiumBridge.FPDFBitmap_CreateEx(Width, Height, (FPDFBitmapFormat)format, buffer, stride);
        }

        #endregion Public methods

        #region Implementation of IBitmap

        /// <inheritdoc/>
        public int Width { get; private set; }

        /// <inheritdoc/>
        public int Height { get; private set; }

        /// <inheritdoc/>
        public void Destroy()
        {
            _mainComponent.PDFiumBridge.FPDFBitmap_Destroy(_bitmapHandle);
        }

        #endregion Implementation of IBitmap
    }
}
