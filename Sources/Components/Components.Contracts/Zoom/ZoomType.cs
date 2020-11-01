namespace PDFiumDotNET.Components.Contracts.Zoom
{
    /// <summary>
    /// Enumeration defines all possible zoom types.
    /// </summary>
    public enum ZoomType
    {
        /// <summary>
        /// Zoom is specific value defined by user.
        /// </summary>
        DefinedValue = 0,

        /// <summary>
        /// Zoom is calculated in way so that the width of page matches the width of the view.
        /// </summary>
        FitWidth,

        /// <summary>
        /// Zoom is calculated in way so that the height of page matches the height of the view.
        /// </summary>
        FitHeight,
    }
}
