namespace PDFiumDotNET.WinFormsControls
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using PDFiumDotNET.WpfControls.Helper;

    /// <summary>
    /// Class implements control to view thumbnails of PDF document.
    /// </summary>
    public partial class PDFThumbnailControl
    {
        #region Private methods - render related

        private void RenderPages(Graphics drawingContext)
        {
            if (_renderInformation?.PagesToRender == null || !_renderInformation.PagesToRender.Any())
            {
                RenderEmptyArea(drawingContext);
                return;
            }

            var brush = new SolidBrush(BackColor);
            drawingContext.FillRectangle(brush, new Rectangle(0, 0, (int)ViewportWidth, (int)ViewportHeight));
            brush.Dispose();

            var brushPage = new SolidBrush(PDFPageBackground);
            var penPageLeft = new Pen(PDFPageBorder, PDFPageBorderThickness.Left);
            var penPageTop = new Pen(PDFPageBorder, PDFPageBorderThickness.Top);
            var penPageRight = new Pen(PDFPageBorder, PDFPageBorderThickness.Right);
            var penPageBottom = new Pen(PDFPageBorder, PDFPageBorderThickness.Bottom);

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
                    var bitmap = new Bitmap((int)pageInfo.Page.Width, (int)pageInfo.Page.Height, PixelFormat.Format32bppArgb);
                    bitmap.SetResolution(96, 96);

                    var data = bitmap.LockBits(new Rectangle(0, 0, (int)pageInfo.Page.Width, (int)pageInfo.Page.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    var format = BitmapFormatConverter.GetFormat(bitmap.PixelFormat);

                    pageInfo.Page.RenderWholePageBitmap(format, data.Scan0, data.Stride);

                    bitmap.UnlockBits(data);

                    // Draw page content.
                    drawingContext.DrawImage(
                        bitmap,
                        new Rectangle(
                            (int)pageInfo.RelativePositionInViewportArea.X,
                            (int)pageInfo.RelativePositionInViewportArea.Y,
                            (int)pageInfo.RelativePositionInViewportArea.Width,
                            (int)pageInfo.RelativePositionInViewportArea.Height));

                    bitmap.Dispose();
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch
                {
                }
#pragma warning restore CA1031 // Do not catch general exception types

                // Draw page label
                var labelSize = drawingContext.MeasureString(pageInfo.Page.PageLabel, Font);
                var textLocation = new Point(
                    (int)(pageInfo.RelativePositionInViewportArea.Left + (pageInfo.RelativePositionInViewportArea.Width / 2) - (labelSize.Width / 2)),
                    2 + (int)pageInfo.RelativePositionInViewportArea.Bottom);
                drawingContext.DrawString(pageInfo.Page.PageLabel, Font, Brushes.Black, textLocation);

                // Draw page border - left
                drawingContext.DrawLine(penPageLeft, new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Top), new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Bottom));

                // Draw page border - top
                drawingContext.DrawLine(penPageTop, new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Top), new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Top));

                // Draw page border - right
                drawingContext.DrawLine(penPageRight, new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Top), new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Bottom));

                // Draw page border - bottom
                drawingContext.DrawLine(penPageBottom, new Point((int)pageInfo.RelativePositionInViewportArea.Left, (int)pageInfo.RelativePositionInViewportArea.Bottom), new Point((int)pageInfo.RelativePositionInViewportArea.Right, (int)pageInfo.RelativePositionInViewportArea.Bottom));
            }

            brushPage.Dispose();
            penPageLeft.Dispose();
            penPageTop.Dispose();
            penPageRight.Dispose();
            penPageBottom.Dispose();

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
        }

        #endregion Private methods - render related
    }
}
