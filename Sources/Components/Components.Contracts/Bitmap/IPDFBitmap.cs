namespace PDFiumDotNET.Components.Contracts.Bitmap
{
    /// <summary>
    /// Interface defines functionality of bitmap object.
    /// </summary>
    public interface IPDFBitmap
    {
        /// <summary>
        /// Gets bitmap height in pixels.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets bitmap height in pixels.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Destroys any allocated resources.
        /// </summary>
        void Destroy();
    }
}
