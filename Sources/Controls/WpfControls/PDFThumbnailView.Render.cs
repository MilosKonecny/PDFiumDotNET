namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.WpfControls.Helper;
    using PDFiumDotNET.WpfControls.WritableBitmapExtension;

    /// <summary>
    /// View class shows page thumbnails from opened PDF document.
    /// </summary>
    public partial class PDFThumbnailView
    {
        #region Private methods - render related

        private void RenderPages(DrawingContext drawingContext)
        {
            if (RenderInformation?.PagesToRender == null || !RenderInformation.PagesToRender.Any())
            {
                RenderEmptyArea(drawingContext);
                return;
            }

            var intViewportWidth = (int)(ViewportWidth + 0.5d);
            var intViewportHeight = (int)(ViewportHeight + 0.5d);

            if (intViewportWidth <= 0 || intViewportHeight <= 0)
            {
                return;
            }

            // Check, initialize writable bitmap and buffer
            if (RenderBuffer == IntPtr.Zero
                || RenderBitmap == null
                || RenderBitmap.PixelWidth != intViewportWidth
                || RenderBitmap.PixelHeight != intViewportHeight)
            {
                var bufferSize = 4 * intViewportWidth * intViewportHeight;
                InitializeRenderBuffer(bufferSize);
                InitializeRenderBitmap(intViewportWidth, intViewportHeight);
            }

            var format = BitmapFormatConverter.GetFormat(RenderBitmap.Format);

            using (var wbe = new WritableBitmapEx(RenderBitmap))
            {
                wbe.Clear();

                // Draw background
                drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ViewportWidth, ViewportHeight));

                // Iterate the pages, adjust some values, and draw them.
                foreach (var pageInfo in RenderInformation.PagesToRender)
                {
                    var zoomFactor = pageInfo.Page.TransformationZoom * PDFPageComponent.ZoomComponent.CurrentZoomFactor;

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
                        ClearRenderBuffer();

                        var visiblePart = new PDFRectangle<int>(
                            (int)pageInfo.VisiblePart.Left,
                            (int)pageInfo.VisiblePart.Top,
                            (int)pageInfo.VisiblePart.Width,
                            (int)pageInfo.VisiblePart.Height);

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
                            RenderBuffer,
                            visiblePartStride);

                        // Copy buffer with rendered page into bitmap.
                        wbe.CopyImageBuffer(
                            RenderBuffer,
                            RenderBufferSize,
                            (int)(pageInfo.VisiblePartInViewportArea.X + 0.5d),
                            (int)(pageInfo.VisiblePartInViewportArea.Y + 0.5d),
                            visiblePartStride,
                            visiblePart.Height);
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
                    drawingContext.DrawLine(
                        pageInfo.Page.PageIndex == FocusedPage ? new Pen(PDFFocusedPageBorderBrush, PDFFocusedPageBorderThickness.Top) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Bottom));

                    // Draw page border - top
                    drawingContext.DrawLine(
                        pageInfo.Page.PageIndex == FocusedPage ? new Pen(PDFFocusedPageBorderBrush, PDFFocusedPageBorderThickness.Top) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Top));

                    // Draw page border - right
                    drawingContext.DrawLine(
                        pageInfo.Page.PageIndex == FocusedPage ? new Pen(PDFFocusedPageBorderBrush, PDFFocusedPageBorderThickness.Top) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Bottom));

                    // Draw page border - bottom
                    drawingContext.DrawLine(
                        pageInfo.Page.PageIndex == FocusedPage ? new Pen(PDFFocusedPageBorderBrush, PDFFocusedPageBorderThickness.Top) : new Pen(PDFPageBorderBrush, PDFPageBorderThickness.Top),
                        new Point(pageInfo.RelativePositionInViewportArea.Left, pageInfo.RelativePositionInViewportArea.Bottom),
                        new Point(pageInfo.RelativePositionInViewportArea.Right, pageInfo.RelativePositionInViewportArea.Bottom));
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

            // Draw all pages into drawing context.
            drawingContext.DrawImage(RenderBitmap, new Rect(0, 0, ViewportWidth, ViewportHeight));
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
