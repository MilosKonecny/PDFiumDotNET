namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
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

            // Draw background
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ViewportWidth, ViewportHeight));

            // Iterate the pages, adjust some values, and draw them.
            foreach (var pageInfo in _renderInformation.PagesToRender)
            {
                // Draw page background
                drawingContext.DrawRectangle(PDFPageBackground, null, new Rect(pageInfo.RelativePositionInViewportArea.X, pageInfo.RelativePositionInViewportArea.Y, pageInfo.RelativePositionInViewportArea.Width, pageInfo.RelativePositionInViewportArea.Height));

                if (!UseTimerForDraw || (UseTimerForDraw && _isInvalidateFromTimer))
                {
                    Debug.WriteLine("Render draw page content");
                    try
                    {
                        // Improve quality by rendering to bigger image.
                        // Prepare rendering data.
                        var factor = 1.5d;
                        var zoomFactor = factor * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                        var visiblePart = new PDFRectangle<double>(factor * pageInfo.VisiblePart.Left, factor * pageInfo.VisiblePart.Top, factor * pageInfo.VisiblePart.Width, factor * pageInfo.VisiblePart.Height);

                        var bitmap = new WriteableBitmap((int)visiblePart.Width, (int)visiblePart.Height, 72, 72, PixelFormats.Bgra32, null);
                        var format = BitmapFormatConverter.GetFormat(bitmap.Format);

                        // Render page content into bitmap.
                        bitmap.Lock();
                        pageInfo.Page.RenderPageBitmap(
                            zoomFactor,
                            (int)visiblePart.Left,
                            (int)visiblePart.Top,
                            (int)visiblePart.Left + (int)visiblePart.Width,
                            (int)visiblePart.Top + (int)visiblePart.Height,
                            (int)visiblePart.Width,
                            (int)visiblePart.Height,
                            format,
                            bitmap.BackBuffer,
                            bitmap.BackBufferStride);
                        bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)visiblePart.Width, (int)visiblePart.Height));
                        bitmap.Unlock();

                        // Draw bitmap into drawing context.
                        drawingContext.DrawImage(bitmap, new Rect(pageInfo.VisiblePartInViewportArea.X, pageInfo.VisiblePartInViewportArea.Y, pageInfo.VisiblePartInViewportArea.Width, pageInfo.VisiblePartInViewportArea.Height));
                        if (UseGCCollect)
                        {
                            Debug.WriteLine("GC.Collect called");
                            GC.Collect();
                        }
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch
                    {
                    }
#pragma warning restore CA1031 // Do not catch general exception types
                }

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

            RenderDebugInfo(drawingContext, _renderInformation.PagesToRender);

            if (UseTimerForDraw)
            {
                Debug.WriteLine("Render use timer for draw");
                if (!_isInvalidateFromTimer)
                {
                    Debug.WriteLine("Render start draw timer");
                    _drawTimer.Start();
                    return;
                }

                Debug.WriteLine("Render reset _isInvalidateFromTimer");
                _isInvalidateFromTimer = false;
            }
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
