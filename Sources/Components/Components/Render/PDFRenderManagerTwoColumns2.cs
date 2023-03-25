namespace PDFiumDotNET.Components.Render
{
    using System;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;

    /// <summary>
    /// The class <see cref="PDFRenderManagerTwoColumns2"/> is derived from abstract <see cref="PDFRenderManagerTwoColumnsBase"/> class
    /// and implements specific render functionality.
    /// First row contains two pages. On the left hand side are pages 0, 2, 4, 6... and on right hand side pages 1, 3, 5, 7...
    /// </summary>
    internal class PDFRenderManagerTwoColumns2 : PDFRenderManagerTwoColumnsBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRenderManagerTwoColumns2"/> class.
        /// </summary>
        public PDFRenderManagerTwoColumns2()
            : base(false)
        {
        }

        #endregion Constructors

        #region Protected override properties

        /// <inheritdoc/>
        protected override int PageRowCount
        {
            get
            {
                return (PageComponent.PageCount / 2) + (PageComponent.PageCount % 2);
            }
        }

        #endregion Protected override properties

        #region Protected override methods

        /// <inheritdoc/>
        protected override IPDFPage LeftPageInRow(int row)
        {
            return PageComponent.Pages[2 * row];
        }

        /// <inheritdoc/>
        protected override IPDFPage RightPageInRow(int row)
        {
            var pageIndex = (2 * row) + 1;
            if (pageIndex >= PageComponent.PageCount)
            {
                // Last line and odd number of pages - no page on right.
                return null;
            }

            return PageComponent.Pages[pageIndex];
        }

        /// <inheritdoc/>
        protected override double GetHorizontalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                throw new ArgumentNullException(nameof(renderInfo));
            }

            return renderInfo.ViewportArea.X;
        }

        /// <inheritdoc/>
        protected override double GetVerticalOffset(IPDFRenderInfo renderInfo, double newZoomFactor)
        {
            if (renderInfo == null)
            {
                throw new ArgumentNullException(nameof(renderInfo));
            }

            return renderInfo.ViewportArea.Y;
        }

        #endregion Protected override methods
    }
}
