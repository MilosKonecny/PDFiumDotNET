namespace PDFiumDotNET.Components.Contracts.Find
{
    using System;

    /// <summary>
    /// Interface defines functionality of find text component.
    /// Component provides all possibilities to find some text.
    /// </summary>
    public interface IPDFFindComponent : IPDFChildComponent
    {
        /// <summary>
        /// Finds given text under defined circumstances.
        /// </summary>
        /// <param name="text">Text to find.</param>
        /// <param name="caseSensitive">Perform case sensitive find.</param>
        /// <param name="wholeWords">Perform whole words find.</param>
        /// <param name="progress">Callback method.
        /// Parameter is actual page index where is text searched. First page has index 0.
        /// Return <c>true</c> to continue and <c>false</c> to cancel find process.</param>
        /// <param name="addPage">Callback method.
        /// Called whenever next page was identified, where the searched text was found.
        /// Return <c>true</c> to continue and <c>false</c> to cancel find process.</param>
        /// <param name="addPosition">Callback method.
        /// Called whenever next position of searched text was found on the page.
        /// Return <c>true</c> to continue and <c>false</c> to cancel find process.</param>
        /// <returns>At least one position was found.</returns>
        bool FindText(
            string text,
            bool caseSensitive,
            bool wholeWords,
            Func<int, bool> progress,
            Func<IPDFFindPage, bool> addPage,
            Func<IPDFFindPage, IPDFFindPosition, bool> addPosition);

        /// <summary>
        /// Clears any allocations related to last <see cref="FindText"/>
        /// and all text selections on any page of opened PDF document.
        /// </summary>
        void ClearFindSelections();
    }
}
