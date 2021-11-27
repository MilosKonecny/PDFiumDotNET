namespace PDFiumDotNET.Components
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFComponent"/>
    /// </summary>
    internal sealed partial class PDFComponent : IPDFComponent, IDisposable
    {
        #region Private fields

        private readonly List<IPDFChildComponent> _childComponents = new List<IPDFChildComponent>();

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

        #region Private methods - helper

        private string ReadMetaText(string key)
        {
            string retValue = null;
            var requiredLen = PDFiumBridge.FPDF_GetMetaText(PDFiumDocument, key, IntPtr.Zero, 0);
            if (requiredLen > 0)
            {
                var buffer = Marshal.AllocHGlobal(requiredLen);
                PDFiumBridge.FPDF_GetMetaText(PDFiumDocument, key, buffer, (ulong)requiredLen);
                retValue = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);
            }

            return retValue;
        }

        #endregion Private methods - helper

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
