namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.WpfControls.Helper;

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

            // Iterate the pages, adjust some values, and draw them.
            foreach (var pageInfo in _renderedPages)
            {
                // Draw page background
                drawingContext.DrawRectangle(PDFPageBackground, null, new Rect(pageInfo.RelativePositionInViewportArea.X, pageInfo.RelativePositionInViewportArea.Y, pageInfo.RelativePositionInViewportArea.Width, pageInfo.RelativePositionInViewportArea.Height));

                try
                {
                    var bitmap = new WriteableBitmap((int)pageInfo.Page.Width, (int)pageInfo.Page.Height, 96, 96, PixelFormats.Bgra32, null);
                    var format = BitmapFormatConverter.GetFormat(bitmap.Format);

                    bitmap.Lock();
                    pageInfo.Page.RenderWholePageBitmap(format, bitmap.BackBuffer, bitmap.BackBufferStride);
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)pageInfo.Page.Width, (int)pageInfo.Page.Height));
                    bitmap.Unlock();

                    // Draw page content.
                    drawingContext.DrawImage(bitmap, new Rect(pageInfo.RelativePositionInViewportArea.X, pageInfo.RelativePositionInViewportArea.Y, pageInfo.RelativePositionInViewportArea.Width, pageInfo.RelativePositionInViewportArea.Height));
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch
                {
                }
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
                var textLocation = new Point(pageInfo.RelativePositionInViewportArea.Left + ((pageInfo.RelativePositionInViewportArea.Right - pageInfo.RelativePositionInViewportArea.Left) / 2d) - (ft.WidthIncludingTrailingWhitespace / 2), pageInfo.RelativePositionInViewportArea.Bottom);
                drawingContext.DrawText(ft, textLocation);

                // Draw page border - left
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Left), new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Top), new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Bottom));

                // Draw page border - top
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top), new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Top), new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Top));

                // Draw page border - right
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Right), new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Top), new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Bottom));

                // Draw page border - bottom
                drawingContext.DrawLine(new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Bottom), new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Bottom), new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Bottom));
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
