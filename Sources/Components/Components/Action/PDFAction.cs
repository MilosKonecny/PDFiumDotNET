namespace PDFiumDotNET.Components.Action
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using PDFiumDotNET.Components.Contracts.Action;
    using PDFiumDotNET.Components.Contracts.Destination;
    using PDFiumDotNET.Components.Destination;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// The class implements the functionality defined by <see cref="IPDFAction"/>.
    /// </summary>
    internal class PDFAction : IPDFAction
    {
        #region Private fields

        private readonly PDFComponent _mainComponent;
        private readonly FPDF_ACTION _actionHandle;

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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Type='{ActionType}' / Destination=({Destination}) / File='{FilePath}' / Uri='{UriPath}'";
        }

        #endregion Public override methods

        #region Private static methods

        private static string PtrToStringUTF8(IntPtr nativeUtf8)
        {
            // .net framework 4.8 doesn't know Marshal.PtrToStringUTF8
            int len = 0;
            while (Marshal.ReadByte(nativeUtf8, len) != 0)
            {
                ++len;
            }

            byte[] buffer = new byte[len];
            Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        #endregion Private static methods

        #region Implementation of IAction

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
                var text = PtrToStringUTF8(buffer);
                Marshal.FreeHGlobal(buffer);

                return text;
            }
        }

        /// <inheritdoc/>
        public string UriPath
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
                var text = PtrToStringUTF8(buffer);
                Marshal.FreeHGlobal(buffer);

                return text;
            }
        }

        /// <inheritdoc/>
        public string Info => ToString();

        #endregion Implementation of IAction
    }
}
