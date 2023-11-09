namespace PDFiumDotNET.WpfControls.WritableBitmapExtension
{
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Class implements additional functionality for <see cref="WriteableBitmap"/>.
    /// </summary>
    internal unsafe class WritableBitmapEx : IDisposable
    {
        #region Private fields

        private readonly WriteableBitmap _attachedBitmap;
        private readonly void* _buffer;
        private readonly int _stride;
        private readonly int _width;
        private readonly int _height;
        private readonly int _bufferSize;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WritableBitmapEx"/> class.
        /// </summary>
        /// <param name="bitmap"><see cref="WriteableBitmap"/> to associate with this object.</param>
        public WritableBitmapEx(WriteableBitmap bitmap)
        {
            _attachedBitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            _attachedBitmap.Lock();
            _buffer = _attachedBitmap.BackBuffer.ToPointer();
            _stride = _attachedBitmap.BackBufferStride;
            _width = _attachedBitmap.PixelWidth;
            _height = _attachedBitmap.PixelHeight;
            _bufferSize = _stride * _height;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Clear content of attached <see cref="WriteableBitmap"/>.
        /// </summary>
        public void Clear()
        {
            NativeMethods.SetMemory((IntPtr)_buffer, 0, _stride * _height);
        }

        /// <summary>
        /// The method copies image data from the buffer into associated <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <param name="buffer">Buffer to copy the image data from.</param>
        /// <param name="bufferLength">Length of <paramref name="buffer"/>.</param>
        /// <param name="x">X position in associated <see cref="WriteableBitmap"/>.</param>
        /// <param name="y">Y position in associated <see cref="WriteableBitmap"/>.</param>
        /// <param name="stride">Stride of the image in buffer.</param>
        /// <param name="height">Height of the image in buffer.</param>
        public void CopyImageBuffer(IntPtr buffer, int bufferLength, int x, int y, int stride, int height)
        {
            if (buffer == IntPtr.Zero)
            {
                return;
            }

            stride = stride >= 0 ? stride : 0;
            height = height >= 0 ? height : 0;

            // Copy pixel rows.
            for (var index = 0; index < height; index++)
            {
                var sourceOffset = index * stride;
                var destinationOffset = 4 * x + y * _stride + index * _stride;

                // Check negative offset.
                if (destinationOffset < 0)
                {
                    continue;
                }

                // Check the offsets pointing behind buffer.
                if (sourceOffset + stride > bufferLength || destinationOffset + stride > _bufferSize)
                {
                    continue;
                }

                // Copy one row of pixels
                NativeMethods.CopyMemory((byte*)buffer, sourceOffset, (byte*)_buffer, destinationOffset, stride);
            }
        }

        #endregion Public methods

        #region Implementation of IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            _attachedBitmap.AddDirtyRect(new Int32Rect(0, 0, _width, _height));
            _attachedBitmap.Unlock();
        }

        #endregion Implementation of IDisposable
    }
}
