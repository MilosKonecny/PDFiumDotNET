namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.WpfControls.Helper;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFView
    {
        #region Private methods - render related

        [Conditional("DEBUG")]
        private void RenderDebugInfo(DrawingContext drawingContext, IList<IPDFPageRenderInfo> pages = null)
        {
            var documentRectangle = new PDFRectangle<double>(0, 0, _documentArea.Width, _documentArea.Height);
            var viewportRectangle = new PDFRectangle<double>(HorizontalOffset, VerticalOffset, _viewportArea.Width, _viewportArea.Height);

            var text = $"Document area: {documentRectangle}{Environment.NewLine}";
            text += $"Viewport area: {viewportRectangle}{Environment.NewLine}";
            if (pages != null)
            {
                text += $"Pages to draw:{Environment.NewLine}";
                foreach (var pageInfo in pages)
                {
                    text += $"{pageInfo.Page.PageIndex}: {pageInfo.Page.Width} x {pageInfo.Page.Height} (zoomed to {pageInfo.RelativePositionInViewportArea.Width} x {pageInfo.RelativePositionInViewportArea.Height})";
                    text += Environment.NewLine;
                    text += $"        Position in document - {pageInfo.PositionInDocumentArea}";
                    text += Environment.NewLine;
                    text += $"        Position in viewport - {pageInfo.RelativePositionInViewportArea}";
                    text += Environment.NewLine;
                    text += $"        Visible part - {pageInfo.VisiblePart}";
                    text += Environment.NewLine;
                    text += $"        Draw position - {pageInfo.VisiblePartInViewportArea}";
                    text += Environment.NewLine;
                }
            }

            var ft = new FormattedText(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                Brushes.Blue,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            drawingContext.DrawText(ft, new Point(5, 5));
        }

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
                    var bitmap = new WriteableBitmap((int)pageInfo.VisiblePart.Width, (int)pageInfo.VisiblePart.Height, 96, 96, PixelFormats.Bgra32, null);
                    var format = BitmapFormatConverter.GetFormat(bitmap.Format);

                    bitmap.Lock();
                    pageInfo.Page.RenderPageBitmap(
                        PDFZoomComponent.CurrentZoomFactor,
                        (int)pageInfo.VisiblePart.Left,
                        (int)pageInfo.VisiblePart.Top,
                        (int)pageInfo.VisiblePart.Right,
                        (int)pageInfo.VisiblePart.Bottom,
                        (int)pageInfo.VisiblePart.Width,
                        (int)pageInfo.VisiblePart.Height,
                        format,
                        bitmap.BackBuffer,
                        bitmap.BackBufferStride);
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)pageInfo.VisiblePart.Width, (int)pageInfo.VisiblePart.Height));
                    bitmap.Unlock();

                    // Draw page content.
                    drawingContext.DrawImage(bitmap, new Rect(pageInfo.VisiblePartInViewportArea.X, pageInfo.VisiblePartInViewportArea.Y, pageInfo.VisiblePartInViewportArea.Width, pageInfo.VisiblePartInViewportArea.Height));
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch
                {
                }
#pragma warning restore CA1031 // Do not catch general exception types
            }

            // Draw background border - left
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(0, 0), new Point(0, ViewportHeight));

            // Draw background border - top
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(ViewportWidth, 0));

            // Draw background border - right
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(ViewportWidth, 0), new Point(ViewportWidth, ViewportHeight));

            // Draw background border - bottom
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, ViewportHeight), new Point(ViewportWidth, ViewportHeight));

            RenderDebugInfo(drawingContext, _renderedPages);
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

            RenderDebugInfo(drawingContext);
        }

        #endregion Private methods - render related
    }
}
