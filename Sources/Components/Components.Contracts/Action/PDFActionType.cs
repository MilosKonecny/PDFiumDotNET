namespace PDFiumDotNET.Components.Contracts.Action
{
    /// <summary>
    /// Definition of all action types.
    /// </summary>
    public enum PDFActionType
    {
        /// <summary>
        /// Unsupported action type.
        /// </summary>
        Unsupported = 0,

        /// <summary>
        /// Go to a destination within current document.
        /// </summary>
        Goto,

        /// <summary>
        /// Go to a destination within another document.
        /// </summary>
        RemoteGoto,

        /// <summary>
        /// Uri, including web pages and other Internet resources.
        /// </summary>
        Uri,

        /// <summary>
        /// Launch an application or open a file.
        /// </summary>
        Launch,
    }
}
