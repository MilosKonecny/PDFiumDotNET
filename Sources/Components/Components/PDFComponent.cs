namespace PDFiumDotNET.Components
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFComponent"/>
    /// </summary>
    public sealed partial class PDFComponent : IPDFComponent, IDisposable
    {
        #region Private fields

        private readonly List<Contracts.IPDFChildComponent> _childComponents = new List<Contracts.IPDFChildComponent>();

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFComponent"/> class.
        /// </summary>
        public PDFComponent()
        {
            PDFiumBridge = new PDFiumBridge();
        }

        #endregion Constructors

        #region Internal methods

        /// <summary>
        /// Gets active bridge.
        /// </summary>
        internal PDFiumBridge PDFiumBridge { get; private set; }

        /// <summary>
        /// Gets active document.
        /// </summary>
        internal FPDF_DOCUMENT PDFiumDocument { get; private set; }

        #endregion Internal methods

        #region Private methods - invoke event

        private void InvokePropertyChangedEvent([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private methods - invoke event

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
            foreach (var component in _childComponents)
            {
                component.Dispose();
            }

            _childComponents.Clear();
            IsDisposed = true;
            PDFiumBridge?.Dispose();
        }

        #endregion Implementation of IDisposable
    }
}
