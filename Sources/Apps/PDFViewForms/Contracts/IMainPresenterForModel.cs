namespace PDFiumDotNET.Apps.PDFViewForms.Contracts
{
    /// <summary>
    /// The interface defines presenter functionality used by model.
    /// </summary>
    internal interface IMainPresenterForModel
    {
        /// <summary>
        /// The method is called by the model at the moment the model is initialized and can be used in presenter.
        /// </summary>
        /// <param name="model">Initialized model to use in presenter.</param>
        void ModelInitialized(IMainModel model);
    }
}
