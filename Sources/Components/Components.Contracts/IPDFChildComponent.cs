namespace PDFiumDotNET.Components.Contracts
{
    using PDFiumDotNET.Components.Contracts.Observers;

    /// <summary>
    /// Defines functionality of every child component used in namespace <see cref="PDFiumDotNET.Components"/>.
    /// This component is always child component to the <see cref="IPDFComponent"/>.
    /// </summary>
    public interface IPDFChildComponent : IPDFBaseComponent, IPDFDocumentObserver
    {
    }
}
