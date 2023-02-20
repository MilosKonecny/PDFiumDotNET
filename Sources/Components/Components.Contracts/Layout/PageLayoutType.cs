namespace PDFiumDotNET.Components.Contracts.Layout
{
    /// <summary>
    /// Enumeration defines all available page layouts.
    /// </summary>
    public enum PageLayoutType
    {
        /// <summary>
        /// Layout of pages below each other - standard size.
        /// </summary>
        Standard,

        /// <summary>
        /// Layout of pages below each other - thumbnail size.
        /// </summary>
        Thumbnail,

        /// <summary>
        /// Layout of pages in two columns.
        /// </summary>
        TwoColumns,

        /// <summary>
        /// Layout of pages in two columns, but in first row is only one page.
        /// </summary>
        TwoColumnsSpecial,
    }
}
