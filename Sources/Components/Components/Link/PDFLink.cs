namespace PDFiumDotNET.Components.Link
{
    using System;
    using PDFiumDotNET.Components.Action;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Contracts.Link;
    using PDFiumDotNET.Components.Destination;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// The class implements functionality defined by <see cref="IPDFLink"/>.
    /// </summary>
    internal class PDFLink : IPDFLink
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;
        private FPDF_LINK _linkHandle;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFLink"/> class.
        /// </summary>
        /// <param name="mainComponent">Main component where this bookmark belongs.</param>
        /// <param name="linkHandle">Handle of this link.</param>
        public PDFLink(PDFComponent mainComponent, FPDF_LINK linkHandle)
        {
            _mainComponent = mainComponent ?? throw new ArgumentNullException(nameof(mainComponent));
            _linkHandle = linkHandle;
        }

        #endregion Constructors

        #region Implementation of IPDFLink

        /// <inheritdoc/>
        public IPDFAction Action
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var action = _mainComponent.PDFiumBridge.FPDFLink_GetAction(_linkHandle);
                if (!action.IsValid)
                {
                    return null;
                }

                return new PDFAction(_mainComponent, action);
            }
        }

        /// <inheritdoc/>
        public IPDFDestination Destination
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var destination = _mainComponent.PDFiumBridge.FPDFLink_GetDest(_mainComponent.PDFiumDocument, _linkHandle);
                if (!destination.IsValid)
                {
                    return null;
                }

                return new PDFDestination(_mainComponent, destination);
            }
        }

        #endregion Implementation of IPDFLink
    }
}
