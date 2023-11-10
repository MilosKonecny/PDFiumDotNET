namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;
    using PDFiumDotNET.WpfControls.Extensions;
    using PDFiumDotNET.WpfControls.WritableBitmapExtension;

    /// <summary>
    /// View base class implements some base functionality for rendering views.
    /// </summary>
    public abstract partial class PDFViewBase : Control, IScrollInfo
    {
        #region Protected fields

        /// <summary>
        /// Offset of viewport in x axis.
        /// </summary>
        private double _horizontalOffset;

        /// <summary>
        /// Offset of viewport in y axis.
        /// </summary>
        private double _verticalOffset;

        /// <summary>
        /// Permission for horizontal scroll.
        /// </summary>
        private bool _canHorizontallyScroll;

        /// <summary>
        /// Permission for vertical scroll.
        /// </summary>
        private bool _canVerticallyScroll;

        /// <summary>
        /// The area where all pages will fit.
        /// </summary>
        private Size _documentArea = new Size(0, 0);

        /// <summary>
        /// The area visible to user.
        /// </summary>
        private Size _viewportArea = new Size(0, 0);

        /// <summary>
        /// Variable used for optimized rendering of whole working area.
        /// </summary>
        private WriteableBitmap _renderBitmap = null;

        /// <summary>
        /// Variable used for rendering of one page.
        /// </summary>
        private IntPtr _renderBuffer = IntPtr.Zero;

        /// <summary>
        /// Variable contains size of allocated render buffer.
        /// </summary>
        private int _renderBufferSize;

        #endregion Protected fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFViewBase"/> class.
        /// </summary>
        protected PDFViewBase()
        {
            if (!this.IsDesignTime())
            {
                Unloaded += (s, e) =>
                {
                    if (_renderBuffer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(_renderBuffer);
                        _renderBuffer = IntPtr.Zero;
                    }
                };
            }
        }

        #endregion Constructors

        #region Protected abstract properties

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> associated with derived class.
        /// </summary>
        protected abstract IPDFPageComponent ControlPDFPageComponent { get; }

        /// <summary>
        /// Gets the value defining whether the zoom functionality in 'scroll' should be supported.
        /// </summary>
        protected abstract bool IsZoomSupported { get; }

        #endregion Protected abstract properties

        #region Protected properties

        /// <summary>
        /// Gets or sets the render information with list of pages to render and other stuff.
        /// Used to examine click, touch, ...
        /// </summary>
        protected IPDFRenderInfo RenderInformation { get; set; }

        /// <summary>
        /// Gets the area into which the entire document fits.
        /// </summary>
        protected Size DocumentArea => _documentArea;

        /// <summary>
        /// Gets the area in which to render the document.
        /// </summary>
        protected Size ViewportArea => _viewportArea;

        /// <summary>
        /// Gets the render bitmap used to render all pages.
        /// </summary>
        protected WriteableBitmap RenderBitmap => _renderBitmap;

        /// <summary>
        /// Gets the render buffer used to render one pages.
        /// </summary>
        protected IntPtr RenderBuffer => _renderBuffer;

        /// <summary>
        /// Gets the size of allocated render buffer.
        /// </summary>
        protected int RenderBufferSize => _renderBufferSize;

        #endregion Protected properties

        #region Protected methods

        /// <summary>
        /// Update the viewport size.
        /// </summary>
        /// <param name="newSize">New size to use for viewport.</param>
        protected void UpdateViewportAreaSize(Size newSize)
        {
            if (_viewportArea == newSize)
            {
                return;
            }

            _viewportArea = newSize;
            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Update the viewport size.
        /// </summary>
        /// <param name="newSize">New size to use for viewport.</param>
        protected void UpdateDocumentAreaSize(Size newSize)
        {
            if (_documentArea == newSize)
            {
                return;
            }

            _documentArea = newSize;
            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// The method creates and initializes <see cref="WriteableBitmap"/> based on given parameters.
        /// </summary>
        /// <param name="pixelWidth">The desired width of the bitmap.</param>
        /// <param name="pixelHeight">The desired height of the bitmap.</param>
        protected void InitializeRenderBitmap(int pixelWidth, int pixelHeight)
        {
            _renderBitmap = new WriteableBitmap(pixelWidth, pixelHeight, 96, 96, PixelFormats.Bgra32, null);
        }

        /// <summary>
        /// The method allocates global buffer based on given parameters.
        /// </summary>
        /// <param name="renderBufferSize">Size of buffer to allocate.</param>
        protected void InitializeRenderBuffer(int renderBufferSize)
        {
            if (_renderBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_renderBuffer);
            }

            _renderBufferSize = renderBufferSize;
            _renderBuffer = Marshal.AllocHGlobal(_renderBufferSize);
        }

        /// <summary>
        /// The method sets 0 in whole render buffer.
        /// </summary>
        protected void ClearRenderBuffer()
        {
            NativeMethods.SetMemory(RenderBuffer, 0, RenderBufferSize);
        }

        /// <summary>
        /// Reset all relevant status fields.
        /// </summary>
        protected void ResetStatus()
        {
            _horizontalOffset = 0d;
            _verticalOffset = 0d;
            _documentArea = new Size(0, 0);
            RenderInformation = null;
        }

        #endregion Protected methods
    }
}
