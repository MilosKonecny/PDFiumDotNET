namespace PDFiumDotNET.Apps.PDFMerge.Contracts
{
    /// <summary>
    /// Interface defines functionality of view model visible to view.
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
