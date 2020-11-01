namespace PDFiumDotNET.Components.Destination
{
    using System;
    using PDFiumDotNET.Components.Contracts.Destination;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFDestination"/>
    /// </summary>
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return $"Page='{PageIndex}' / X='{X}' / Y='{Y}' / Zoom='{Zoom}'";
        }

        #endregion Public override methods

        #region Implementation of IDestination

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public float? X
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                bool hasXVal, hasYVal, hasZoomVal;
                hasXVal = hasYVal = hasZoomVal = false;
                float x, y, zoom;
                x = y = zoom = 0f;

                var ret = _mainComponent.PDFiumBridge.FPDFDest_GetLocationInPage(
                    _destinationHandle, ref hasXVal, ref hasYVal, ref hasZoomVal, ref x, ref y, ref zoom);
                if (!ret || !hasXVal)
                {
                    return null;
                }

                return x;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public float? Y
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                bool hasXVal, hasYVal, hasZoomVal;
                hasXVal = hasYVal = hasZoomVal = false;
                float x, y, zoom;
                x = y = zoom = 0f;

                var ret = _mainComponent.PDFiumBridge.FPDFDest_GetLocationInPage(
                    _destinationHandle, ref hasXVal, ref hasYVal, ref hasZoomVal, ref x, ref y, ref zoom);
                if (!ret || !hasYVal)
                {
                    return null;
                }

                return y;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public float? Zoom
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                bool hasXVal, hasYVal, hasZoomVal;
                hasXVal = hasYVal = hasZoomVal = false;
                float x, y, zoom;
                x = y = zoom = 0f;

                var ret = _mainComponent.PDFiumBridge.FPDFDest_GetLocationInPage(
                    _destinationHandle, ref hasXVal, ref hasYVal, ref hasZoomVal, ref x, ref y, ref zoom);
                if (!ret || !hasZoomVal)
                {
                    return null;
                }

                return zoom;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Info => ToString();

        #endregion Implementation of IDestination
    }
}
