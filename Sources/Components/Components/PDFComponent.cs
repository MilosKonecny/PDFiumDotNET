namespace PDFiumDotNET.Components
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFComponent"/>
    internal sealed partial class PDFComponent : PDFBaseComponent, IPDFComponent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFComponent"/> class.
        /// </summary>
        public PDFComponent()
        {
            PDFiumBridge = new PDFiumBridge();
        }

        #endregion Constructors

        #region Internal properties

        /// <summary>
        /// Gets active bridge.
        /// </summary>
        internal PDFiumBridge PDFiumBridge { get; private set; }

        /// <summary>
        /// Gets active document.
        /// </summary>
        internal FPDF_DOCUMENT PDFiumDocument { get; private set; }

        #endregion Internal properties

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

        #region Protected override methods

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            PDFiumBridge?.Dispose();
            PDFiumBridge = null;
        }

        #endregion Protected override methods
    }
}
