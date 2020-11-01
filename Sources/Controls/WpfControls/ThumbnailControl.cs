using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PDFiumDotNET.Components.Contracts.Page;
using PDFiumDotNET.WpfControls.Helper;

namespace PDFiumDotNET.WpfControls
{
    /// <summary>
    /// Thumbnail control used to show small image of page.
    /// </summary>
    public class ThumbnailControl : Control
    {
        static ThumbnailControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThumbnailControl), new FrameworkPropertyMetadata(typeof(ThumbnailControl)));
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public IPDFPage Page
        {
            get { return (IPDFPage)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        /// <summary>
        /// Dependency property for 'Page' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(IPDFPage), typeof(ThumbnailControl), new PropertyMetadata(null));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            // background
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, Width, Height));
            // left
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(0, 0), new Point(0, Height));
            // top
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(Width, 0));
            // right
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(Width, 0), new Point(Width, Height));
            // bottom
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, Height), new Point(Width, Height));

            if (Page == null)
            {
                return;
            }

            var bitmap = new WriteableBitmap((int)Width, (int)Height, 96, 96, PixelFormats.Bgra32, null);
            var format = BitmapFormatConverter.GetFormat(bitmap.Format);
            bitmap.Lock();
            var bmp = Page.CreateThumbnailBitmap(format, bitmap.BackBuffer, bitmap.BackBufferStride);
            bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)Width, (int)Height));
            bitmap.Unlock();

            drawingContext.DrawImage(bitmap, new Rect(0, 0, Width, Height));
            bmp.Destroy();
        }
    }
}
