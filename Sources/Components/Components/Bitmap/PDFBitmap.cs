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
        /// <param name="width">Width of bitmap to draw into.</param>
        /// <param name="height">Height of bitmap to draw into.</param>
        public void Render(FPDF_PAGE page, int width, int height)
        {
            _mainComponent.PDFiumBridge.FPDF_RenderPageBitmap(_bitmapHandle, page, 0, 0, width, height, 0, 0);
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
