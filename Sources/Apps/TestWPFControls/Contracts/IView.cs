namespace PDFiumDotNET.Apps.TestWPFControls.Contracts
{
    using PDFiumDotNET.WpfControls;

    /// <summary>
    /// Interface defines base behavior of view.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the <see cref="PDFView"/> control.
        /// </summary>
        PDFView PDFViewControl { get; }
    }
}
