namespace PDFiumDotNET.Apps.PDFViewForms.Contracts
{
    /// <summary>
    /// The interface defines presenter functionality used by view.
    /// </summary>
    internal interface IMainPresenterForView
    {
        /// <summary>
        /// The method is called by the view at the moment the view is initialized and can be used in presenter.
        /// </summary>
        /// <param name="view">Initialized view to use in presenter.</param>
        void ViewInitialized(IMainView view);

        /// <summary>
        /// The method opens the specified file.
        /// </summary>
        /// <param name="path">File to open.</param>
        void OpenFile(string path);

        /// <summary>
        /// The method closes the file if there is any open file.
        /// </summary>
        void CloseFile();

        /// <summary>
        /// The method shows information about the open file.
        /// </summary>
        void ShowInformation();

        /// <summary>
        /// The method shows about information.
        /// </summary>
        void ShowAbout();
    }
}
