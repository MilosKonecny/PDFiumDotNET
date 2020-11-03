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
                    text += Environment.NewLine + $"{pageInfo.Page.PageIndex}";
                    text += $" / x:{Math.Round(pageInfo.Left, 2)}=>{Math.Round(pageInfo.Right, 2)} / y:{Math.Round(pageInfo.Top, 2)}=>{Math.Round(pageInfo.Bottom, 2)}";
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

                // Determine all necessary information.
                var leftTop = new Point(pageInfo.Left, pageInfo.Top);
                var rightBottom = new Point(pageInfo.Right, pageInfo.Bottom);
                var drawWidth = (int)(rightBottom.X - leftTop.X);
                var drawHeight = (int)(rightBottom.Y - leftTop.Y);
                // Draw page background
                drawingContext.DrawRectangle(PDFPageBackground, null, new Rect(leftTop, rightBottom));

                // ToDo: Implement exception handling. WriteableBitmap ctor throws COMException: MILERR_WIN32ERROR (Exception from HRESULT: 0x88980003)
                var bitmap = new WriteableBitmap(drawWidth, drawHeight, 96, 96, PixelFormats.Bgra32, null);
                var format = BitmapFormatConverter.GetFormat(bitmap.Format);
                bitmap.Lock();
                var bmp = pageInfo.Page.CreatePageBitmap(
                    0, 0, drawWidth, drawHeight,
                    drawWidth, drawHeight, format, bitmap.BackBuffer, bitmap.BackBufferStride);
                bitmap.AddDirtyRect(new Int32Rect(0, 0, drawWidth, drawHeight));
                bitmap.Unlock();
                bmp.Destroy();

                // Draw page content.
                drawingContext.DrawImage(bitmap, new Rect(leftTop, rightBottom));
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
