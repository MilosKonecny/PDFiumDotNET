namespace PDFiumDotNET.Components.Contracts.Observers
{
    /// <summary>
    /// Defines possibility of one observer.
    /// Observer should always implements <see cref="IPDFChildComponent"/> and optionally <see cref="IPDFDocumentObserver"/>.
    /// Observer obtains all document related actions.
    /// </summary>
    public interface IPDFDocumentObserver
    {
        /// <summary>
        /// Called whenever PDF document will be opened.
        /// </summary>
        /// <param name="file">Opened PDF file.</param>
        void DocumentOpening(string file);

        /// <summary>
        /// Called whenever PDF document has opened.
        /// </summary>
        /// <param name="file">Opened PDF file.</param>
        void DocumentOpened(string file);

        /// <summary>
        /// Called whenever PDF document open has failed.
        /// </summary>
        /// <param name="file">Opened PDF file.</param>
        void DocumentOpenFailed(string file);

        /// <summary>
        /// Called whenever PDF document will be closed.
        /// </summary>
        void DocumentClosing();

        /// <summary>
        /// Called whenever PDF document was closed.
        /// </summary>
        void DocumentClosed();
    }
}
