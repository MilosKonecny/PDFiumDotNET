namespace PDFiumDotNET.Samples.SimpleWpf.Contracts
{
    using System.Windows;

    /// <summary>
    /// Interface defines base behaviour of view.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the view <see cref="Window"/>.
        /// </summary>
        Window Window { get; }

        /// <summary>
        /// Gets the actual width of the control where the PDF pages are rendered.
        /// </summary>
        double PDFActualWidth { get; }

        /// <summary>
        /// Gets the actual height of the control where the PDF pages are rendered.
        /// </summary>
        double PDFActualHeight { get; }

        /// <summary>
        /// Gets the margin between pages.
        /// </summary>
        Size PDFPageMargin { get; }

        /// <summary>
        /// Invalidates PDF control.
        /// </summary>
        void InvalidatePDFControl();
    }
}
