#if WpfControls
namespace PDFiumDotNET.WpfControls
#else
namespace PDFiumDotNET.WpfCoreControls
#endif
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Components.Contracts.Adapters;
#if WpfControls
    using PDFiumDotNET.WpfControls.Helper;
#else
    using PDFiumDotNET.WpfCoreControls.Helper;
#endif

    /// <summary>
    /// View class shows page thumbnails from opened PDF document.
    /// </summary>
    public partial class PDFThumbnailView
    {
        #region Private methods - render related

        private void RenderPages(DrawingContext drawingContext)
        {
            // Draw background
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ViewportWidth, ViewportHeight));

            var pageOnCenter = _renderedPages.FirstOrDefault(page => page.IsOnCenter);
            // Determine pages to draw.
            _renderedPages.Clear();
            _renderedPages.AddRange(PDFPageComponent[PageLayoutType.Thumbnail].DeterminePagesToRender(
                VerticalOffset,
                VerticalOffset + ViewportHeight,
                2d * FontSize,
                _thumbnailZoomFactor));

            // Determine viewport rectangle
            var viewportRectangle = new Rect(0, 0, _viewport.Width, _viewport.Height);

            // Iterate the pages, adjust some values, and draw them.
            foreach (var pageInfo in _renderedPages)
            {
                // Current page width
                var currentPageWidth = pageInfo.Page.ThumbnailWidth * _thumbnailZoomFactor;

                // Center the page horizontally
                pageInfo.Left = ViewportWidth / 2d - currentPageWidth / 2d;
                pageInfo.Right = ViewportWidth / 2d + currentPageWidth / 2d;

                // Take offsets into account
                pageInfo.Left -= HorizontalOffset;
                pageInfo.Right -= HorizontalOffset;
                pageInfo.Top -= VerticalOffset;
                pageInfo.Bottom -= VerticalOffset;

                var pageRect = new Rect(pageInfo.Left, pageInfo.Top, Math.Max(1d, pageInfo.Right - pageInfo.Left), Math.Max(0d, pageInfo.Bottom - pageInfo.Top));

                ////////var pageOnViewport = pageRect;
                ////////pageOnViewport.Intersect(viewportRectangle);
                ////////if (pageOnViewport.IsEmpty)
                ////////{
                ////////    continue;
                ////////}

                ////////pageOnViewport.Width = Math.Max(1d, pageOnViewport.Width);
                ////////pageOnViewport.Height = Math.Max(1d, pageOnViewport.Height);

                var pageRectForPDFium = pageRect;
                pageRectForPDFium.Y = pageRect.Y > 0d ? 0d : pageRect.Y;
                pageRectForPDFium.X = pageRect.X > 0d ? 0d : pageRect.X;
                pageRectForPDFium.Width = Math.Max(1d, pageRectForPDFium.Width);
                pageRectForPDFium.Height = Math.Max(1d, pageRectForPDFium.Height);

                // Draw page background
                drawingContext.DrawRectangle(PDFPageBackground, null, new Rect(pageRect.TopLeft, pageRect.BottomRight));

                try
                {
                    var bitmap = new WriteableBitmap((int)pageInfo.Page.ThumbnailWidth, (int)pageInfo.Page.ThumbnailHeight, 96, 96, PixelFormats.Bgra32, null);
                    var format = BitmapFormatConverter.GetFormat(bitmap.Format);

                    bitmap.Lock();
                    pageInfo.Page.RenderThumbnailBitmap(format, bitmap.BackBuffer, bitmap.BackBufferStride);
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)pageRect.Width, (int)pageRect.Height));
                    bitmap.Unlock();

                    // Draw page content.
                    drawingContext.DrawImage(bitmap, new Rect(pageRect.TopLeft, pageRect.BottomRight));
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch { }
#pragma warning restore CA1031 // Do not catch general exception types

                // Draw page label
                var ft = new FormattedText(
                    pageInfo.Page.PageLabel,
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                    FontSize,
                    Foreground,
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);
                var textLocation = new Point(pageInfo.Left + (pageInfo.Right - pageInfo.Left) / 2d - ft.WidthIncludingTrailingWhitespace / 2, pageInfo.Bottom);
                drawingContext.DrawText(ft, textLocation);

                // Draw page border - left
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Left), new Point(pageInfo.Left, pageInfo.Top), new Point(pageInfo.Left, pageInfo.Bottom));
                // Draw page border - top
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top), new Point(pageInfo.Left, pageInfo.Top), new Point(pageInfo.Right, pageInfo.Top));
                // Draw page border - right
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Right), new Point(pageInfo.Right, pageInfo.Top), new Point(pageInfo.Right, pageInfo.Bottom));
                // Draw page border - bottom
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Bottom), new Point(pageInfo.Left, pageInfo.Bottom), new Point(pageInfo.Right, pageInfo.Bottom));
            }

            // Draw background border - left
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(0, 0), new Point(0, ViewportHeight));
            // Draw background border - top
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(ViewportWidth, 0));
            // Draw background border - right
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(ViewportWidth, 0), new Point(ViewportWidth, ViewportHeight));
            // Draw background border - bottom
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, ViewportHeight), new Point(ViewportWidth, ViewportHeight));
        }

        private void RenderEmptyArea(DrawingContext drawingContext)
        {
            // Draw background
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ViewportWidth, ViewportHeight));
            // Draw page border - left
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(0, 0), new Point(0, ViewportHeight));
            // Draw page border - top
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(ViewportWidth, 0));
            // Draw page border - right
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(ViewportWidth, 0), new Point(ViewportWidth, ViewportHeight));
            // Draw page border - bottom
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, ViewportHeight), new Point(ViewportWidth, ViewportHeight));
        }

        #endregion Private methods - render related
    }
}
