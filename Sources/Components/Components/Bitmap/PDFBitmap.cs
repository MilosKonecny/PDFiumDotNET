namespace PDFiumDotNET.Components.Bitmap
{
    using System;
    using PDFiumDotNET.Components.Contracts.Bitmap;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFBitmap"/>
    /// </summary>
    internal class PDFBitmap : IPDFBitmap
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;
        private FPDF_BITMAP _bitmapHandle;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFBitmap"/> class.
        /// </summary>
        /// <param name="mainComponent">Main component where this bookmark belongs.</param>
        public PDFBitmap(PDFComponent mainComponent)
        {
            _mainComponent = mainComponent ?? throw new ArgumentNullException(nameof(mainComponent));
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Render the page to the bitmap.
        /// </summary>
        /// <param name="page">Page to render.</param>
        /// <param name="startX">Left pixel position of the page area to be rendered.</param>
        /// <param name="startY">Top pixel position of the page area to be rendered.</param>
        /// <param name="sizeX">Width of the page area to be rendered.</param>
        /// <param name="sizeY">Height of the page area to be rendered.</param>
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
        /// <param name="startX">Left pixel position of the page area to be rendered.</param>
        /// <param name="startY">Top pixel position of the page area to be rendered.</param>
        /// <param name="sizeX">Width of the page area to be rendered.</param>
        /// <param name="sizeY">Height of the page area to be rendered.</param>
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Destroy()
        {
            _mainComponent.PDFiumBridge.FPDFBitmap_Destroy(_bitmapHandle);
        }

        #endregion Implementation of IBitmap
    }
}
