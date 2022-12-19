namespace PDFiumDotNET.Components.Layout
{
    using System;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Page;

    /// <summary>
    /// Implementation of <see cref="IPageLayoutAdapter"/> for thumbnail page layout.
    /// </summary>
    internal class ThumbnailPageLayout : BasePageLayout
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailPageLayout"/> class.
        /// </summary>
        /// <param name="pageComponent"><see cref="PDFPageComponent"/> used to obtain information.</param>
        internal ThumbnailPageLayout(PDFPageComponent pageComponent)
            : base(pageComponent, PageLayoutType.Thumbnail)
        {
        }

        #endregion Constructors

        #region Protected virtual methods

        /// <summary>
        /// Returns the width of given page.
        /// </summary>
        /// <param name="page">Page to examine.</param>
        /// <returns>With of given page.</returns>
        /// <remarks>Used to determine which width should be used - width or thumbnail width.</remarks>
        protected override double PageWidth(IPDFPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return page.ThumbnailWidth;
        }

        /// <summary>
        /// Returns the height of given page.
        /// </summary>
        /// <param name="page">Page to examine.</param>
        /// <returns>With of given page.</returns>
        /// <remarks>Used to determine which height should be used - height or thumbnail height.</remarks>
        protected override double PageHeight(IPDFPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return page.ThumbnailHeight;
        }

        #endregion Protected virtual methods
    }
}
