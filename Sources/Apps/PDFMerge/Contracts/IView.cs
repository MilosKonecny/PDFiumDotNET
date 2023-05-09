namespace PDFiumDotNET.Apps.PDFMerge.Contracts
{
    /// <summary>
    /// Interface defines functionality of view visible to view model.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Shows message in GUI.
        /// </summary>
        /// <param name="title">Title for the message box.</param>
        /// <param name="message">Message to show.</param>
        void ShowMessage(string title, string message);
    }
}
