namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.WpfControls.Helper;

    /// <summary>
    /// View class shows pages from opend PDF document.
    /// </summary>
    public partial class PDFView
    {
        #region Private methods - render related

        [Conditional("DEBUG")]
        private void RenderDebugInfo(DrawingContext drawingContext, IList<IPDFPageRenderInfo> pages = null)
        {
            var text = $"Area: {_workArea.Width} / {_workArea.Height}{Environment.NewLine}Viewport: {_viewport.Width} / {_viewport.Height}{Environment.NewLine}Offset: {HorizontalOffset} / {VerticalOffset}";
            if (pages != null)
            {
                var viewportRectangle = new Rect(0, 0, _viewport.Width, _viewport.Height);
                text += Environment.NewLine + "Pages to draw:";
                foreach (var pageInfo in pages)
                {
                    text += Environment.NewLine + $"{pageInfo.Page.PageIndex}: width={pageInfo.Page.Width} height={pageInfo.Page.Height}";

                    var pageRect = new Rect(pageInfo.Left, pageInfo.Top, Math.Max(1d, pageInfo.Right - pageInfo.Left), Math.Max(0d, pageInfo.Bottom - pageInfo.Top));
                    var pageOnViewport = pageRect;
                    pageOnViewport.Intersect(viewportRectangle);
                    pageOnViewport.Width = Math.Max(1d, pageOnViewport.Width);
                    pageOnViewport.Height = Math.Max(1d, pageOnViewport.Height);

                    var pageRectForPDFium = pageRect;
                    pageRectForPDFium.Y = pageRect.Y > 0d ? 0d : pageRect.Y;
                    pageRectForPDFium.X = pageRect.X > 0d ? 0d : pageRect.X;
                    pageRectForPDFium.Width = Math.Max(1d, pageRectForPDFium.Width);
                    pageRectForPDFium.Height = Math.Max(1d, pageRectForPDFium.Height);

                    text += Environment.NewLine + $"    Page rect / x:{Math.Round(pageRect.X, 2)} width:{Math.Round(pageRect.Width, 2)} / y:{Math.Round(pageRect.Y, 2)} height:{Math.Round(pageRect.Height, 2)}";
                    text += Environment.NewLine + $"    Page on viewort / x:{Math.Round(pageOnViewport.X, 2)} width:{Math.Round(pageOnViewport.Width, 2)} / y:{Math.Round(pageOnViewport.Y, 2)} height:{Math.Round(pageOnViewport.Height, 2)}";
                    text += Environment.NewLine + $"    Page for PDFium / x:{Math.Round(pageRectForPDFium.X, 2)} width:{Math.Round(pageRectForPDFium.Width, 2)} / y:{Math.Round(pageRectForPDFium.Y, 2)} height:{Math.Round(pageRectForPDFium.Height, 2)}";
                }
            }

            var ft = new FormattedText(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                Brushes.Red,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            drawingContext.DrawText(ft, new Point(5, 5));
        }

        private void RenderPages(DrawingContext drawingContext)
        {
            // Draw background
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ViewportWidth, ViewportHeight));

            // Determine bages to draw
            _renderedPages.Clear();
            _renderedPages.AddRange(PDFPageComponent.PagesToRender(
                VerticalOffset,
                VerticalOffset + ViewportHeight,
                PDFPageMargin,
                PDFZoomComponent.ActualZoomFactor));

            // Determine viewport rectangle
            var viewportRectangle = new Rect(0, 0, _viewport.Width, _viewport.Height);

            // Iterate the pages, adjust some values, and draw them.
            foreach (var pageInfo in _renderedPages)
            {
                // Actual page width
                var actualPageWidth = pageInfo.Page.Width * PDFZoomComponent.ActualZoomFactor;

                // Center the page horizontally
                if (ViewportWidth > _workArea.Width)
                {
                    pageInfo.Left = ViewportWidth / 2d - actualPageWidth / 2d;
                    pageInfo.Right = ViewportWidth / 2d + actualPageWidth / 2d;
                }
                else
                {
                    pageInfo.Left = _workArea.Width / 2d - actualPageWidth / 2d;
                    pageInfo.Right = _workArea.Width / 2d + actualPageWidth / 2d;
                }

                // Take offsets into account
                pageInfo.Left -= HorizontalOffset;
                pageInfo.Right -= HorizontalOffset;
                pageInfo.Top -= VerticalOffset;
                pageInfo.Bottom -= VerticalOffset;

                var pageRect = new Rect(pageInfo.Left, pageInfo.Top, Math.Max(1d, pageInfo.Right - pageInfo.Left), Math.Max(0d, pageInfo.Bottom - pageInfo.Top));

                var pageOnViewport = pageRect;
                pageOnViewport.Intersect(viewportRectangle);
                pageOnViewport.Width = Math.Max(1d, pageOnViewport.Width);
                pageOnViewport.Height = Math.Max(1d, pageOnViewport.Height);

                var pageRectForPDFium = pageRect;
                pageRectForPDFium.Y = pageRect.Y > 0d ? 0d : pageRect.Y;
                pageRectForPDFium.X = pageRect.X > 0d ? 0d : pageRect.X;
                pageRectForPDFium.Width = Math.Max(1d, pageRectForPDFium.Width);
                pageRectForPDFium.Height = Math.Max(1d, pageRectForPDFium.Height);

                // Draw page background
                drawingContext.DrawRectangle(PDFPageBackground, null, new Rect(pageRect.TopLeft, pageRect.BottomRight));

                try
                {
                    var bitmap = new WriteableBitmap((int)pageOnViewport.Width, (int)pageOnViewport.Height, 96, 96, PixelFormats.Bgra32, null);
                    var format = BitmapFormatConverter.GetFormat(bitmap.Format);

                    bitmap.Lock();
                    pageInfo.Page.RenderPageBitmap(
                        PDFZoomComponent.ActualZoomFactor,
                        (int)pageRectForPDFium.X, (int)pageRectForPDFium.Y, (int)pageRectForPDFium.Width, (int)pageRectForPDFium.Height,
                        (int)pageOnViewport.Width, (int)pageOnViewport.Height, format, bitmap.BackBuffer, bitmap.BackBufferStride);
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)pageOnViewport.Width, (int)pageOnViewport.Height));
                    bitmap.Unlock();

                    // Draw page content.
                    drawingContext.DrawImage(bitmap, new Rect(pageOnViewport.TopLeft, pageOnViewport.BottomRight));
                }
                catch { }
            }

            // Draw page border - left
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(0, 0), new Point(0, ViewportHeight));
            // Draw page border - top
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(ViewportWidth, 0));
            // Draw page border - right
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(ViewportWidth, 0), new Point(ViewportWidth, ViewportHeight));
            // Draw page border - bottom
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
