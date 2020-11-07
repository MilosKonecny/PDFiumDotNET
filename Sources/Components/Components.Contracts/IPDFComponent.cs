namespace PDFiumDotNET.Components.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <summary>
    /// Defines functionality of main component used in namespace <see cref="PDFiumDotNET.Components"/>.
    /// This component is parent component for every <see cref="IPDFChildComponent"/>.
    /// </summary>
    public interface IPDFComponent : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Gets the page component.
        /// </summary>
        IPDFPageComponent PageComponent { get; }

        /// <summary>
        /// Gets the bookmark component.
        /// </summary>
        IPDFBookmarkComponent BookmarkComponent { get; }

        /// <summary>
        /// Gets the zoom component.
        /// </summary>
        IPDFZoomComponent ZoomComponent { get; }

        /// <summary>
        /// Gets all child components attached to this main component.
        /// </summary>
        IEnumerable<IPDFChildComponent> ChildComponents { get; }

        /// <summary>
        /// Gets a value indicating whether the instance is disposed or not.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets a value indicating whether pdf document is opened or not.
        /// </summary>
        bool IsDocumentOpened { get; }

        /// <summary>
        /// Attaches new child component. This method calls <see cref="IPDFChildComponent.AttachedTo(IPDFComponent)"/> of child component.
        /// </summary>
        /// <param name="childComponent">Component to attach.</param>
        void Attach(IPDFChildComponent childComponent);

        /// <summary>
        /// Closes pdf document.
        /// </summary>
        void CloseDocument();

        /// <summary>
        /// Opens given pdf document.
        /// </summary>
        /// <param name="file">Pdf file to open.</param>
        /// <param name="password">Password for protected document.</param>
        void OpenDocument(string file, string password);

        /// <summary>
        /// Opens given pdf document.
        /// </summary>
        /// <param name="file">Pdf file to open.</param>
        /// <param name="getPassword">Callback function used to get password in case the document is password protected.</param>
        void OpenDocument(string file, Func<string> getPassword = null);
    }
}
