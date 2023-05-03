namespace PDFiumDotNET.Apps.TestWPFControls.Contracts
{
    /// <summary>
    /// Interface defines base behavior of view model.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Called after view model was assigned to the view.
        /// </summary>
        /// <param name="view">View where is view model assigned.</param>
        void AssignedToView(IView view);
    }
}
