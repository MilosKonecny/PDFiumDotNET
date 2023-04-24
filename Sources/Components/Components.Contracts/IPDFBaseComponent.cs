namespace PDFiumDotNET.Components.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Base component for all PDF components.
    /// </summary>
    public interface IPDFBaseComponent : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Gets the main (top parent) component where this component belongs.
        /// <see cref="MainComponent"/> may be the same as <see cref="ParentComponent"/>.
        /// </summary>
        IPDFComponent MainComponent
        {
            get;
        }

        /// <summary>
        /// Gets the parent component where is this component attached.
        /// </summary>
        IPDFBaseComponent ParentComponent
        {
            get;
        }

        /// <summary>
        /// Gets all child components attached to this component.
        /// </summary>
        IEnumerable<IPDFChildComponent> ChildComponents { get; }

        /// <summary>
        /// Gets a value indicating whether the instance is disposed or not.
        /// </summary>
        bool IsDisposed
        {
            get;
        }

        /// <summary>
        /// Provides the information for child component he was added to another component as a child.
        /// </summary>
        /// <param name="parentComponent">Component where was the component attached.</param>
        void AttachedTo(IPDFBaseComponent parentComponent);

        /// <summary>
        /// Attaches new child component. This method calls <see cref="IPDFBaseComponent.AttachedTo(IPDFBaseComponent)"/> of child component.
        /// </summary>
        /// <param name="childComponent">Component to attach.</param>
        void Attach(IPDFChildComponent childComponent);
    }
}
