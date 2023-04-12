namespace PDFiumDotNET.WinFormsControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.WpfControls.Helper;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFControl
    {
        #region Private methods - render related

        [Conditional("DEBUG")]
        private void RenderDebugInfo(Graphics drawingGraphics, IEnumerable<IPDFPageRenderInfo> pages = null)
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

            drawingGraphics.DrawString(text, Font, Brushes.Blue, 0, 0);
        }

        private void RenderPages(Graphics drawingContext)
        {
            if (_renderInformation?.PagesToRender == null || !_renderInformation.PagesToRender.Any())
            {
                RenderEmptyArea(drawingContext);
                return;
            }

            // Draw background
            var brush = new SolidBrush(BackColor);
            drawingContext.FillRectangle(brush, new Rectangle(0, 0, (int)ViewportWidth, (int)ViewportHeight));
            brush.Dispose();

            var brushPage = new SolidBrush(PDFPageBackground);
            var penActivePageLeft = new Pen(PDFPageActiveBorder, PDFPageActiveBorderThickness.Left);
            var penInactivePageLeft = new Pen(PDFPageBorder, PDFPageBorderThickness.Left);
            var penActivePageTop = new Pen(PDFPageActiveBorder, PDFPageActiveBorderThickness.Top);
            var penInactivePageTop = new Pen(PDFPageBorder, PDFPageBorderThickness.Top);
            var penActivePageRight = new Pen(PDFPageActiveBorder, PDFPageActiveBorderThickness.Right);
            var penInactivePageRight = new Pen(PDFPageBorder, PDFPageBorderThickness.Right);
            var penActivePageBottom = new Pen(PDFPageActiveBorder, PDFPageActiveBorderThickness.Bottom);
            var penInactivePageBottom = new Pen(PDFPageBorder, PDFPageBorderThickness.Bottom);

            // Iterate the pages, adjust some values, and draw them.
            foreach (var pageInfo in _renderInformation.PagesToRender)
            {
                // Draw page background
                drawingContext.FillRectangle(
                    brushPage,
                    new Rectangle(
                        (int)pageInfo.RelativePositionInViewportArea.X,
                        (int)pageInfo.RelativePositionInViewportArea.Y,
                        (int)pageInfo.RelativePositionInViewportArea.Width,
                        (int)pageInfo.RelativePositionInViewportArea.Height));

                try
                {
                    var bitmap = new Bitmap((int)pageInfo.VisiblePart.Width, (int)pageInfo.VisiblePart.Height, PixelFormat.Format32bppArgb);
                    bitmap.SetResolution(96, 96);

                    var data = bitmap.LockBits(new Rectangle(0, 0, (int)pageInfo.VisiblePart.Width, (int)pageInfo.VisiblePart.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    var format = BitmapFormatConverter.GetFormat(bitmap.PixelFormat);

                    pageInfo.Page.RenderPageBitmap(
                        PDFPageComponent.ZoomComponent.CurrentZoomFactor,
                        (int)pageInfo.VisiblePart.Left,
                        (int)pageInfo.VisiblePart.Top,
                        (int)pageInfo.VisiblePart.Right,
                        (int)pageInfo.VisiblePart.Bottom,
                        (int)pageInfo.VisiblePart.Width,
                        (int)pageInfo.VisiblePart.Height,
                        format,
                        data.Scan0,
                        data.Stride);

                    bitmap.UnlockBits(data);

                    // Draw page content.
                    drawingContext.DrawImage(
                        bitmap,
                        new Rectangle(
                            (int)pageInfo.VisiblePartInViewportArea.X,
                            (int)pageInfo.VisiblePartInViewportArea.Y,
                            (int)pageInfo.VisiblePartInViewportArea.Width,
                            (int)pageInfo.VisiblePartInViewportArea.Height));

                    bitmap.Dispose();
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch
                {
                }
#pragma warning restore CA1031 // Do not catch general exception types

                if (ShowPageLabel)
                {
                    // Draw page label
                    var labelSize = drawingContext.MeasureString(pageInfo.Page.PageLabel, Font);
                    var textLocation = new Point(
                        (int)(pageInfo.RelativePositionInViewportArea.Left + (pageInfo.RelativePositionInViewportArea.Width / 2) - (labelSize.Width / 2)),
                        2 + (int)pageInfo.RelativePositionInViewportArea.Bottom);
                    drawingContext.DrawString(pageInfo.Page.PageLabel, Font, Brushes.Black, textLocation);
                }

                // Draw page border - left
                drawingContext.DrawLine(
                    pageInfo.IsClosestToCenter ? penActivePageLeft : penInactivePageLeft,
                    new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Top),
                    new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Bottom));

                // Draw page border - top
                drawingContext.DrawLine(
                    pageInfo.IsClosestToCenter ? penActivePageTop : penInactivePageTop,
                    new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Top),
                    new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Top));

                // Draw page border - right
                drawingContext.DrawLine(
                    pageInfo.IsClosestToCenter ? penActivePageRight : penInactivePageRight,
                    new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Top),
                    new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Bottom));

                // Draw page border - bottom
                drawingContext.DrawLine(
                    pageInfo.IsClosestToCenter ? penActivePageBottom : penInactivePageBottom,
                    new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Bottom),
                    new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Bottom));
            }

            brushPage.Dispose();
            penActivePageLeft.Dispose();
            penInactivePageLeft.Dispose();
            penActivePageTop.Dispose();
            penInactivePageTop.Dispose();
            penActivePageRight.Dispose();
            penInactivePageRight.Dispose();
            penActivePageBottom.Dispose();
            penInactivePageBottom.Dispose();

            // ToDo: Don't use SystemBrushes
            var pen = new Pen(SystemBrushes.InactiveBorder, 1);

            // Draw background border - left
            drawingContext.DrawLine(pen, new Point(0, 0), new Point(0, (int)ViewportHeight));

            // Draw background border - top
            drawingContext.DrawLine(pen, new Point(0, 0), new Point((int)ViewportWidth, 0));

            // Draw background border - right
            drawingContext.DrawLine(pen, new Point((int)ViewportWidth, 0), new Point((int)ViewportWidth, (int)ViewportHeight));

            // Draw background border - bottom
            drawingContext.DrawLine(pen, new Point(0, (int)ViewportHeight), new Point((int)ViewportWidth, (int)ViewportHeight));

            pen.Dispose();

            RenderDebugInfo(drawingContext, _renderInformation.PagesToRender);
        }

        private void RenderEmptyArea(Graphics drawingContext)
        {
            // Draw background
            var brush = new SolidBrush(BackColor);
            drawingContext.FillRectangle(brush, new Rectangle(0, 0, (int)ViewportWidth, (int)ViewportHeight));
            brush.Dispose();

            // ToDo: Don't use SystemBrushes
            var pen = new Pen(SystemBrushes.InactiveBorder, 1);

            // Draw page border - left
            drawingContext.DrawLine(pen, new Point(0, 0), new Point(0, (int)ViewportHeight));

            // Draw page border - top
            drawingContext.DrawLine(pen, new Point(0, 0), new Point((int)ViewportWidth, 0));

            // Draw page border - right
            drawingContext.DrawLine(pen, new Point((int)ViewportWidth, 0), new Point((int)ViewportWidth, (int)ViewportHeight));

            // Draw page border - bottom
            drawingContext.DrawLine(pen, new Point(0, (int)ViewportHeight), new Point((int)ViewportWidth, (int)ViewportHeight));

            pen.Dispose();

            RenderDebugInfo(drawingContext);
        }

        #endregion Private methods - render related
    }
}
