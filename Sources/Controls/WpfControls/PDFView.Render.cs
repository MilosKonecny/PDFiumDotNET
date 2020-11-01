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
                text += Environment.NewLine + "Pages to draw: ";
                foreach (var pageInfo in pages)
                {
                    text += pageInfo.Page.PageIndex + ",";
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

            // Iterate pages, correct some values and draw them.
            foreach (var pageInfo in _renderedPages)
            {
                // Actual page width
                var actualPageWidth = pageInfo.Page.Width * PDFZoomComponent.ActualZoomFactor;
                if (ViewportWidth < _workArea.Width)
                {
                    // Take offset into considertion.
                    pageInfo.Left = -HorizontalOffset + PDFPageMargin;
                    pageInfo.Right = pageInfo.Left + actualPageWidth;
                }
                else
                {
                    // Center page.
                    pageInfo.Left = _viewport.Width / 2d - actualPageWidth / 2d;
                    pageInfo.Right = _viewport.Width / 2d + actualPageWidth / 2d;
                }

                // Determine all necessary information.
                var leftTop = new Point(pageInfo.Left, pageInfo.Top - VerticalOffset);
                var rightBottom = new Point(pageInfo.Right, pageInfo.Bottom - VerticalOffset);
                var drawWidth = (int)(rightBottom.X - leftTop.X);
                var drawHeight = (int)(rightBottom.Y - leftTop.Y);
                // Draw page background
                drawingContext.DrawRectangle(PDFPageBackground, null, new Rect(leftTop, rightBottom));

                // ToDo: Implement exception handling. WriteableBitmap ctor throws COMException: MILERR_WIN32ERROR (Exception from HRESULT: 0x88980003)
                // Prepare page content.
                var bitmap = new WriteableBitmap(drawWidth, drawHeight, 96, 96, PixelFormats.Bgra32, null);
                var format = BitmapFormatConverter.GetFormat(bitmap.Format);
                bitmap.Lock();
                var bmp = pageInfo.Page.CreatePageBitmap(drawWidth, drawHeight, format, bitmap.BackBuffer, bitmap.BackBufferStride);
                bitmap.AddDirtyRect(new Int32Rect(0, 0, drawWidth, drawHeight));
                bitmap.Unlock();

                // Draw page content.
                drawingContext.DrawImage(bitmap, new Rect(leftTop, rightBottom));
                bmp.Destroy();
            }

            // Draw border
            drawingContext.DrawRectangle(null, new Pen(BorderBrush, BorderThickness.Left), new Rect(0, 0, ViewportWidth, ViewportHeight));

            RenderDebugInfo(drawingContext, _renderedPages);
        }

        private void RenderEmptyArea(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, new Pen(BorderBrush, BorderThickness.Left), new Rect(0, 0, ViewportWidth, ViewportHeight));
            RenderDebugInfo(drawingContext);
        }

        #endregion Private methods - render related
    }
}
