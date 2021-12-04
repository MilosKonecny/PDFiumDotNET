namespace PDFiumDotNET.Components.Find
{
    using System;
    using System.Collections.ObjectModel;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Implements <see cref="IPDFFindPage"/>.
    /// </summary>
    internal class PDFFindPage : IPDFFindPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFFindPage"/> class.
        /// </summary>
        /// <param name="relatedPage">Page where was searched text found at least one time.</param>
        public PDFFindPage(IPDFPage relatedPage)
        {
            RelatedPage = relatedPage ?? throw new ArgumentNullException(nameof(relatedPage));
            Positions = new ObservableCollection<IPDFFindPosition>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Adds position into <see cref="Positions"/>.
        /// </summary>
        /// <param name="position">Position to add into <see cref="Positions"/>.</param>
        public void AddPosition(IPDFFindPosition position)
        {
            Positions.Add(position);
        }

        #endregion Public methods

        #region Implementation of IPDFFindPage

        /// <summary>
        /// Gets related page where the searched text was found.
        /// </summary>
        public IPDFPage RelatedPage { get; private set; }

        /// <summary>
        /// Gets all found positions of searched text on this page.
        /// </summary>
        public ObservableCollection<IPDFFindPosition> Positions { get; private set; }

        #endregion Implementation of IPDFFindPage
    }
}
