namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.WpfControls.Helper;
    using PDFiumDotNET.WpfControls.WritableBitmapExtension;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFView
    {
        #region Private methods - render related

        [Conditional("DEBUG")]
        private void RenderDebugInfo(DrawingContext drawingContext, IEnumerable<IPDFPageRenderInfo> pages = null)
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
            if (_renderInformation?.PagesToRender == null || !_renderInformation.PagesToRender.Any())
            {
                RenderEmptyArea(drawingContext);
                return;
            }

            // Improve quality by rendering to bigger image.
            // Prepare rendering data.
            var factor = 1.5d;
            var zoomFactor = factor * PDFPageComponent.ZoomComponent.CurrentZoomFactor;

            var intViewportWidthFactor = (int)(factor * ViewportWidth + 0.5d);
            var intViewportHeightFactor = (int)(factor * ViewportHeight + 0.5d);

            if (intViewportWidthFactor <= 0 || intViewportHeightFactor <= 0)
            {
                return;
            }

            // Check, initialize writable bitmap and buffer
            if (_renderBuffer == IntPtr.Zero
                || _renderBitmap == null
                || _renderBitmap.PixelWidth != intViewportWidthFactor
                || _renderBitmap.PixelHeight != intViewportHeightFactor)
            {
                _bufferSize = 4 * intViewportWidthFactor * intViewportHeightFactor;
                if (_renderBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(_renderBuffer);
                }

                _renderBuffer = Marshal.AllocHGlobal(_bufferSize);
                _renderBitmap = new WriteableBitmap(intViewportWidthFactor, intViewportHeightFactor, 96, 96, PixelFormats.Bgra32, null);
            }

            var format = BitmapFormatConverter.GetFormat(_renderBitmap.Format);

            using (var wbe = new WritableBitmapEx(_renderBitmap))
            {
                wbe.Clear();

                // Draw background
                drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ViewportWidth, ViewportHeight));

                // Iterate the pages, adjust some values, and draw them.
                foreach (var pageInfo in _renderInformation.PagesToRender)
                {
                    // Draw page background
                    drawingContext.DrawRectangle(
                        PDFPageBackground,
                        null,
                        new Rect(
                            pageInfo.RelativePositionInViewportArea.X,
                            pageInfo.RelativePositionInViewportArea.Y,
                            pageInfo.RelativePositionInViewportArea.Width,
                            pageInfo.RelativePositionInViewportArea.Height));

                    try
                    {
                        // Clear buffer
                        NativeMethods.SetMemory(_renderBuffer, 0, _bufferSize);

                        var visiblePart = new PDFRectangle<int>(
                            (int)(factor * pageInfo.VisiblePart.Left),
                            (int)(factor * pageInfo.VisiblePart.Top),
                            (int)(factor * pageInfo.VisiblePart.Width),
                            (int)(factor * pageInfo.VisiblePart.Height));

                        var visiblePartStride = 4 * visiblePart.Width;

                        // Render page content into buffer.
                        pageInfo.Page.RenderPageBitmap(
                            zoomFactor,
                            visiblePart.Left,
                            visiblePart.Top,
                            visiblePart.Right,
                            visiblePart.Bottom,
                            visiblePart.Width,
                            visiblePart.Height,
                            format,
                            _renderBuffer,
                            visiblePartStride);

                        // Copy buffer with rendered page into bitmap.
                        wbe.CopyImageBuffer(
                            _renderBuffer,
                            _bufferSize,
                            (int)(factor * pageInfo.VisiblePartInViewportArea.X + 0.5d),
                            (int)(factor * pageInfo.VisiblePartInViewportArea.Y + 0.5d),
                            visiblePartStride,
                            visiblePart.Height);
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch
                    {
                    }
#pragma warning restore CA1031 // Do not catch general exception types

                    if (ShowPageLabel)
                    {
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
                    }

                    // Draw page border - left
                    drawingContext.DrawLine(
                        pageInfo.IsClosestToCenter ? new Pen(PDFPageActiveBorderBrush, PDFPageActiveBorderThickness.Left) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Left),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Bottom));

                    // Draw page border - top
                    drawingContext.DrawLine(
                        pageInfo.IsClosestToCenter ? new Pen(PDFPageActiveBorderBrush, PDFPageActiveBorderThickness.Top) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Top));

                    // Draw page border - right
                    drawingContext.DrawLine(
                        pageInfo.IsClosestToCenter ? new Pen(PDFPageActiveBorderBrush, PDFPageActiveBorderThickness.Right) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Right),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Bottom));

                    // Draw page border - bottom
                    drawingContext.DrawLine(
                        pageInfo.IsClosestToCenter ? new Pen(PDFPageActiveBorderBrush, PDFPageActiveBorderThickness.Bottom) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Bottom),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Bottom),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Bottom));
                }

                // Draw background border - left
                drawingContext.DrawLine(
                    new Pen(BorderBrush, BorderThickness.Left),
                    new Point(0, 0),
                    new Point(0, ViewportHeight));

                // Draw background border - top
                drawingContext.DrawLine(
                    new Pen(BorderBrush, BorderThickness.Top),
                    new Point(0, 0),
                    new Point(ViewportWidth, 0));

                // Draw background border - right
                drawingContext.DrawLine(
                    new Pen(BorderBrush, BorderThickness.Right),
                    new Point(ViewportWidth, 0),
                    new Point(ViewportWidth, ViewportHeight));

                // Draw background border - bottom
                drawingContext.DrawLine(
                    new Pen(BorderBrush, BorderThickness.Bottom),
                    new Point(0, ViewportHeight),
                    new Point(ViewportWidth, ViewportHeight));
            }

            // Draw all pages into drawing context.
            drawingContext.DrawImage(_renderBitmap, new Rect(0, 0, ViewportWidth, ViewportHeight));

            RenderDebugInfo(drawingContext, _renderInformation.PagesToRender);
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
