namespace PDFiumDotNET.Components.Contracts
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines functionality of every child component used in namespace <see cref="PDFiumDotNET.Components"/>.
    /// This component is always child component to the <see cref="IPDFComponent"/>.
    /// </summary>
    public interface IPDFChildComponent : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Gets the main component where is this componnent attached.
        /// </summary>
        IPDFComponent MainComponent
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the instance is disposed or not.
        /// </summary>
        bool IsDisposed
        {
            get;
        }

        /// <summary>
        /// Provides the information for child component he was added to another component.
        /// </summary>
        /// <param name="mainComponent">Component where was the component attached.</param>
        void AttachedTo(IPDFComponent mainComponent);
    }
}
