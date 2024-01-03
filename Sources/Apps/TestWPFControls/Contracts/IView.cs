namespace PDFiumDotNET.Apps.TestWPFControls.Contracts
{
    using System.Windows.Controls;
    using PDFiumDotNET.WpfControls;

    /// <summary>
    /// Interface defines base behavior of view.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the container for <see cref="PDFView"/> control.
        /// </summary>
        ScrollViewer PDFViewContainer { get; }
    }
}
