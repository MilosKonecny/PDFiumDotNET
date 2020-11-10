namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Wrapper.Exceptions;

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge : IDisposable
    {
        private static readonly object _syncObject = new object();
        private static IntPtr _libraryHandle;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumBridge"/> class.
        /// </summary>
        public PDFiumBridge()
        {
            LoadPDFium();
        }

        #endregion Constructors

        #region Internal static properties

        /// <summary>
        /// Gets the current count of <see cref="PDFiumBridge"/> classes that are using loaded PDFium dll.
        /// </summary>
        internal static int UsageCount { get; private set; }

        #endregion Internal static properties

        #region Private static methods

        private static void LoadDll()
        {
            // Determine and load pdfium library
            var libraryName = Environment.Is64BitProcess ? "PDFium\\x64\\pdfium.dll" : "PDFium\\x86\\pdfium.dll";
            _libraryHandle = NativeMethods.LoadLibrary(libraryName);
            if (_libraryHandle == IntPtr.Zero)
            {
                throw PDFiumLibraryNotLoadedException.CreateException(libraryName, Marshal.GetLastWin32Error());
            }

            // Load the functions declared in view header.
            LoadDllViewPart(libraryName);

            // Load the functions declared in doc header.
            LoadDllDocPart(libraryName);
        }

        /// <summary>
        /// Loads appropriate version of pdfium dll and extracts all pdfium methods currently supported in this project.
        /// </summary>
        private static void LoadPDFium()
        {
            lock (_syncObject)
            {
                if (UsageCount == 0)
                {
                    LoadDll();
                    FPDF_InitLibraryStatic();
                }

                UsageCount++;
            }
        }

        /// <summary>
        /// Unloads previously loaded pdfium dll.
        /// </summary>
        private static void UnloadPDFium()
        {
            lock (_syncObject)
            {
                UsageCount--;
                if (UsageCount == 0)
                {
                    FPDF_DestroyLibraryStatic();
                    NativeMethods.FreeLibrary(_libraryHandle);
                    _libraryHandle = IntPtr.Zero;
                }
            }
        }

        #endregion Private static methods

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            UnloadPDFium();
        }

        #endregion Implementation of IDisposable
    }
}
