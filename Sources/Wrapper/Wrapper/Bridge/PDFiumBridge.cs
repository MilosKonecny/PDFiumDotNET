namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Wrapper.Exceptions;

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge : IDisposable
    {
        private static readonly object _syncObject = new object();
        private static IntPtr _libraryHandle;
        private static string _libraryName;

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
            var libraryNames = new List<string>();

            // Library is 'pdfium.dll' in sub-folder 'PDFium\x64' or 'PDFium\x86'
            libraryNames.Add(Environment.Is64BitProcess ? "PDFium\\x64\\pdfium.dll" : "PDFium\\x86\\pdfium.dll");

            // Library is in the same folder and name is 'pdfium.x64.dll' or 'pdfium.x86.dll'.
            libraryNames.Add(Environment.Is64BitProcess ? "pdfium.x64.dll" : "pdfium.x86.dll");

            // Library is in the same folder and name is 'pdfium.dll'
            libraryNames.Add("pdfium.dll");

            foreach (var libraryName in libraryNames)
            {
                if (File.Exists(libraryName))
                {
                    _libraryName = libraryName;
                    break;
                }
            }

            if (string.IsNullOrEmpty(_libraryName))
            {
                throw PDFiumLibraryNotLoadedException.CreateException(libraryNames);
            }

            // Load the library
            _libraryHandle = NativeMethods.LoadLibrary(_libraryName);
            if (_libraryHandle == IntPtr.Zero)
            {
                throw PDFiumLibraryNotLoadedException.CreateException(_libraryName, Marshal.GetLastWin32Error());
            }

            // Load the functions declared in view header - fpdfview.h.
            LoadDllViewPart();

            // Load the functions declared in view header - fpdf_ppo.h.
            LoadDllPpoPart();

            // Load the functions declared in view header - fpdf_edit.h.
            LoadDllEditPart();

            // Load the functions declared in view header - fpdf_save.h.
            LoadDllSavePart();

            // Load the functions declared in doc header - fpdf_doc.h.
            LoadDllDocPart();

            // Load the functions declared in text header - fpdf_text.h.
            LoadDllTextPart();

            // Load the functions declared in annot header - fpdf_annot.h.
            LoadDllAnnotPart();

            // Load the functions declared in annot header - fpdf_searchex.h.
            LoadDllSearchExPart();
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
                // ToDo: Workaround for issue #105
                // UsageCount--;
                if (UsageCount == 0)
                {
                    FPDF_DestroyLibraryStatic();
                    NativeMethods.FreeLibrary(_libraryHandle);
                    _libraryHandle = IntPtr.Zero;
                }
            }
        }

        private static T GetPDFiumFunction<T>(string functionName)
        {
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(_libraryName, functionName, Marshal.GetLastWin32Error());
            }

            return Marshal.GetDelegateForFunctionPointer<T>(address);
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
