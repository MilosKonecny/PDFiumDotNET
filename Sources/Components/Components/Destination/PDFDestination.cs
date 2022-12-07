namespace PDFiumDotNET.Components.Destination
{
    using System;
    using PDFiumDotNET.Components.Contracts.Destination;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <inheritdoc cref="IPDFDestination"/>
    internal class PDFDestination : IPDFDestination
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;
        private FPDF_DEST _destinationHandle;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFDestination"/> class.
        /// </summary>
        /// <param name="mainComponent">Main component where this destination belongs.</param>
        /// <param name="destinationHandle">Handle of associated destination.</param>
        public PDFDestination(PDFComponent mainComponent, FPDF_DEST destinationHandle)
        {
            _mainComponent = mainComponent ?? throw new ArgumentNullException(nameof(mainComponent));
            _destinationHandle = destinationHandle;
        }

        #endregion Constructors

        #region Public override methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Page='{PageIndex}' / X='{X}' / Y='{Y}' / Zoom='{Zoom}'";
        }

        #endregion Public override methods

        #region Implementation of IDestination

        /// <inheritdoc/>
        public int PageIndex
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                return _mainComponent.PDFiumBridge.FPDFDest_GetDestPageIndex(_mainComponent.PDFiumDocument, _destinationHandle);
            }
        }

        /// <inheritdoc/>
        public float? X
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                var ret = _mainComponent.PDFiumBridge.FPDFDest_GetLocationInPage(
                    _destinationHandle, out bool hasXVal, out bool hasYVal, out bool hasZoomVal, out float x, out float y, out float zoom);
                if (!ret || !hasXVal)
                {
                    return null;
                }

                return x;
            }
        }

        /// <inheritdoc/>
        public float? Y
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                var ret = _mainComponent.PDFiumBridge.FPDFDest_GetLocationInPage(
                    _destinationHandle, out bool hasXVal, out bool hasYVal, out bool hasZoomVal, out float x, out float y, out float zoom);
                if (!ret || !hasYVal)
                {
                    return null;
                }

                return y;
            }
        }

        /// <inheritdoc/>
        public float? Zoom
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                var ret = _mainComponent.PDFiumBridge.FPDFDest_GetLocationInPage(
                    _destinationHandle, out bool hasXVal, out bool hasYVal, out bool hasZoomVal, out float x, out float y, out float zoom);
                if (!ret || !hasZoomVal)
                {
                    return null;
                }

                return zoom;
            }
        }

        /// <inheritdoc/>
        public string Info => ToString();

        #endregion Implementation of IDestination
    }
}
