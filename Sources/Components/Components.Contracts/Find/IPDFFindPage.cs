namespace PDFiumDotNET.Components.Contracts.Find
{
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Interface defines properties and functionality for one page where the searched text was found.
    /// </summary>
    public interface IPDFFindPage
    {
        /// <summary>
        /// Gets related page where the searched text was found.
        /// </summary>
        IPDFPage RelatedPage { get; }

        /// <summary>
        /// Gets all found positions of searched text on this page.
        /// </summary>
        ObservableCollection<IPDFFindPosition> Positions { get; }
    }
}
