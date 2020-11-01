namespace PDFiumDotNET.Components.Action
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Destination;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumDelegates;

    /// <summary>
    /// <inheritdoc cref="IPDFAction"/>
    /// </summary>
    internal class PDFAction : IPDFAction
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;
        private FPDF_ACTION _actionHandle;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFAction"/> class.
        /// </summary>
        /// <param name="mainComponent">Main component where this destination belongs.</param>
        /// <param name="actionHandle">Handle of associated action.</param>
        public PDFAction(PDFComponent mainComponent, FPDF_ACTION actionHandle)
        {
            _mainComponent = mainComponent ?? throw new ArgumentNullException(nameof(mainComponent));
            _actionHandle = actionHandle;
        }

        #endregion Constructors

        #region Public override methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return $"Type='{ActionType}' / Destination=({Destination}) / File='{FilePath}' / Uri='{UriPath}'";
        }

        #endregion Public override methods

        #region Implementation of IAction

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public PDFActionType ActionType
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return 0;
                }

                return (PDFActionType)_mainComponent.PDFiumBridge.FPDFAction_GetType(_actionHandle);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFDestination Destination
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var destination = _mainComponent.PDFiumBridge.FPDFAction_GetDest(_mainComponent.PDFiumDocument, _actionHandle);
                if (!destination.IsValid)
                {
                    return null;
                }

                return new PDFDestination(_mainComponent, destination);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FilePath
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var requiredLen = _mainComponent.PDFiumBridge.FPDFAction_GetFilePath(_actionHandle, IntPtr.Zero, 0);
                if (requiredLen <= 0)
                {
                    return null;
                }

                var buffer = Marshal.AllocHGlobal(requiredLen);
                _mainComponent.PDFiumBridge.FPDFAction_GetFilePath(_actionHandle, buffer, (ulong)requiredLen);
                var text = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);

                return text;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Uri UriPath
        {
            get
            {
                if (_mainComponent.PDFiumBridge == null || !_mainComponent.PDFiumDocument.IsValid)
                {
                    return null;
                }

                var requiredLen = _mainComponent.PDFiumBridge.FPDFAction_GetURIPath(_mainComponent.PDFiumDocument, _actionHandle, IntPtr.Zero, 0);
                if (requiredLen <= 0)
                {
                    return null;
                }

                var buffer = Marshal.AllocHGlobal(requiredLen);
                _mainComponent.PDFiumBridge.FPDFAction_GetURIPath(_mainComponent.PDFiumDocument, _actionHandle, buffer, (ulong)requiredLen);
                var text = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);

                return new Uri(text);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Info => ToString();

        #endregion Implementation of IAction
    }
}
