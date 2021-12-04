namespace PDFiumDotNET.Components.Bookmark
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Observers;

    /// <summary>
    /// <inheritdoc cref="IPDFBookmarkComponent"/>
    /// </summary>
    internal sealed partial class PDFBookmarkComponent : IPDFBookmarkComponent, IPDFDocumentObserver
    {
        #region Private fields

        private PDFComponent _mainComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFBookmarkComponent"/> class.
        /// </summary>
        public PDFBookmarkComponent()
        {
            Bookmarks = new ObservableCollection<IPDFBookmark>();
        }

        #endregion Constructors

        #region Private methods - invoke event

        private void InvokePropertyChangedEvent([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private methods - invoke event

        #region Implementation of IBookmarkProvider

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ObservableCollection<IPDFBookmark> Bookmarks { get; private set; }

        #endregion Implementation of IBookmarkProvider

        #region Implementation of IPDFChildComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFComponent MainComponent => _mainComponent;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void AttachedTo(IPDFComponent mainComponent)
        {
            var mc = mainComponent as PDFComponent;

            _mainComponent = mc ?? throw new ArgumentException(
                string.Format(CultureInfo.InvariantCulture, "The parameter {0} is not of expected type.", nameof(mainComponent)));
        }

        #endregion Implementation of IPDFChildComponent

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IDisposable

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
        }

        #endregion Implementation of IDisposable
    }
}
