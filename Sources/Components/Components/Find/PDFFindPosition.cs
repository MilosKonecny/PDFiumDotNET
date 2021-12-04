namespace PDFiumDotNET.Components.Find
{
    using System;
    using PDFiumDotNET.Components.Contracts.Find;

    /// <summary>
    /// Implements <see cref="IPDFFindPosition"/>.
    /// </summary>
    internal class PDFFindPosition : IPDFFindPosition
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFFindPosition"/> class.
        /// </summary>
        public PDFFindPosition(IPDFFindPage page)
        {
            Page = page ?? throw new ArgumentNullException(nameof(page));
        }

        #endregion Constructors

        #region Implementation of IPDFFindPosition

        /// <summary>
        /// Gets the page where this position belongs.
        /// </summary>
        public IPDFFindPage Page { get; private set; }

        /// <summary>
        /// Gets the position of first character of found text.
        /// </summary>
        public int Position { get; internal set; }

        /// <summary>
        /// Gets the length of found text.
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// Gets the text context where the searched text was found.
        /// </summary>
        public string Context { get; internal set; }

        #endregion Implementation of IPDFFindPosition
    }
}
