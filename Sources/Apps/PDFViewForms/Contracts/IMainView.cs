namespace PDFiumDotNET.Apps.PDFViewForms.Contracts
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// The interface defines view functionality used by presenter.
    /// </summary>
    internal interface IMainView
    {
        /// <summary>
        /// The method displays error message.
        /// </summary>
        /// <param name="error">Error message to display.</param>
        void ShowError(string error);

        /// <summary>
        /// The method sets the <see cref="IPDFPageComponent"/> for standard view.
        /// </summary>
        /// <param name="pageComponent"><see cref="IPDFPageComponent"/> to use.</param>
        void SetPDFPageComponentForView(IPDFPageComponent pageComponent);

        /// <summary>
        /// The method sets the <see cref="IPDFPageComponent"/> for standard view.
        /// </summary>
        /// <param name="pageComponent"><see cref="IPDFPageComponent"/> to use.</param>
        void SetPDFPageComponentForThumbnail(IPDFPageComponent pageComponent);

        /// <summary>
        /// The method enables or disables menu item 'file / open'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableFileOpen(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'file / close'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableFileClose(bool enable);

        /// <summary>
        /// The method enables or disables menu item 'file / properties'.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable menu item.</param>
        void EnableFileProperties(bool enable);
    }
}
