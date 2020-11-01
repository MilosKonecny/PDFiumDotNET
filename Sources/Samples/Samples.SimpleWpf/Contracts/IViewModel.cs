namespace PDFiumDotNET.Samples.SimpleWpf.Contracts
{
    /// <summary>
    /// Interface defines base behaviour of view model.
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
