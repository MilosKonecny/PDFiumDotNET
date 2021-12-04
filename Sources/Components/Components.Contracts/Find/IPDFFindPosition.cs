namespace PDFiumDotNET.Components.Contracts.Find
{
    /// <summary>
    /// Interface defines properties and functionality for one found position of searched text.
    /// </summary>
    public interface IPDFFindPosition
    {
        /// <summary>
        /// Gets the page where this position belongs.
        /// </summary>
        IPDFFindPage Page { get; }

        /// <summary>
        /// Gets the position of first character of found text.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Gets the length of found text.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the text context where the searched text was found.
        /// </summary>
        string Context { get; }
    }
}
