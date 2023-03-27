namespace PDFiumDotNET.Components.Render
{
    using System;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Render;

    /// <summary>
    /// The class <see cref="PDFRenderManagerTwoColumns1"/> is derived from abstract <see cref="PDFRenderManagerTwoColumnsBase"/> class
    /// and implements specific render functionality.
    /// First row contains one pages. On the left hand side are pages -, 1, 3, 5... and on right hand side pages 0, 2, 4, 6...
    /// </summary>
    internal class PDFRenderManagerTwoColumns1 : PDFRenderManagerTwoColumnsBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRenderManagerTwoColumns1"/> class.
        /// </summary>
        public PDFRenderManagerTwoColumns1()
            : base(true)
        {
        }

        #endregion Constructors

        #region Protected override properties

        /// <inheritdoc/>
        protected override int PageRowCount
        {
            get
            {
                if (PageComponent.PageCount <= 2)
                {
                    return PageComponent.PageCount;
                }

                return (PageComponent.PageCount / 2) + 1;
            }
        }

        #endregion Protected override properties

        #region Protected override methods

        /// <inheritdoc/>
        protected override IPDFPage LeftPageInRow(int row)
        {
            if (row == 0)
            {
                // First row - no page on left.
                return null;
            }

            return PageComponent.Pages[(2 * (row - 1)) + 1];
        }

        /// <inheritdoc/>
        protected override IPDFPage RightPageInRow(int row)
        {
            var pageIndex = 2 * row;
            if (pageIndex >= PageComponent.PageCount)
            {
                // Last line and even number of pages - no page on right.
                return null;
            }

            return PageComponent.Pages[pageIndex];
        }

        #endregion Protected override methods
    }
}
