namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;

    // Disable "Member 'member' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        /// <summary>
        /// Structure for custom file write.
        /// </summary>
        internal struct FPDF_FILEWRITE
        {
            /// <summary>
            /// Version number of the interface. Currently must be 1.
            /// </summary>
            public int Version;

            ////// Method: WriteBlock
            //////          Output a block of data in your custom way.
            ////// Interface Version:
            //////          1
            ////// Implementation Required:
            //////          Yes
            ////// Comments:
            //////          Called by function FPDF_SaveDocument
            ////// Parameters:
            //////          pThis       -   Pointer to the structure itself
            //////          pData       -   Pointer to a buffer to output
            //////          size        -   The size of the buffer.
            ////// Return value:
            //////          Should be non-zero if successful, zero for error.
            ////int (* WriteBlock) (struct FPDF_FILEWRITE_* pThis, const void* pData, unsigned long size);

            /// <summary>
            /// Delegate method for callback.
            /// </summary>
            /// <param name="pThis">Pointer to the structure itself.</param>
            /// <param name="pData">Pointer to a buffer to output.</param>
            /// <param name="size">The size of the buffer.</param>
            /// <returns><c>true</c> for success; otherwise <c>false</c>.</returns>
            public delegate bool WriteCallback(ref FPDF_FILEWRITE pThis, IntPtr pData, ulong size);

            /// <summary>
            /// Output a block of data in your custom way.
            /// </summary>
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WriteCallback WriteBlockMethod;
        }

        /// <summary>
        /// Enumeration for save functions.
        /// </summary>
        internal enum FPDF_SAVEFLAGS : int
        {
            /// <summary>
            /// Incremental flag.
            /// </summary>
            FPDF_INCREMENTAL = 1,

            /// <summary>
            /// No incremental flag.
            /// </summary>
            FPDF_NO_INCREMENTAL = 2,

            /// <summary>
            /// Remove security flag.
            /// </summary>
            FPDF_REMOVE_SECURITY = 3,
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_SaveAsCopy_Delegate(FPDF_DOCUMENT document, ref FPDF_FILEWRITE pFileWrite, FPDF_SAVEFLAGS flags);

        private static FPDF_SaveAsCopy_Delegate FPDF_SaveAsCopyStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDF_SaveAsCopy")]
        private static extern bool FPDF_SaveAsCopyStatic(FPDF_DOCUMENT document, ref FPDF_FILEWRITE pFileWrite, FPDF_SAVEFLAGS flags);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Saves the copy of specified document in custom way.
        /// </summary>
        /// <param name="document">Handle to document, as returned by FPDF_LoadDocument() or FPDF_CreateNewDocument().</param>
        /// <param name="pFileWrite">A pointer to a custom file write structure.</param>
        /// <param name="flags">The creating flags.</param>
        /// <returns>TRUE for succeed, FALSE for failed.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_SaveAsCopy(FPDF_DOCUMENT document, FPDF_FILEWRITE* pFileWrite, FPDF_DWORD flags);.
        /// </remarks>
        public bool FPDF_SaveAsCopy(FPDF_DOCUMENT document, ref FPDF_FILEWRITE pFileWrite, FPDF_SAVEFLAGS flags)
        {
            lock (_syncObject)
            {
                return FPDF_SaveAsCopyStatic(document, ref pFileWrite, flags);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_SaveWithVersion_Delegate(FPDF_DOCUMENT document, ref FPDF_FILEWRITE pFileWrite, FPDF_SAVEFLAGS flags, int fileVersion);

        private static FPDF_SaveWithVersion_Delegate FPDF_SaveWithVersionStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDF_SaveWithVersion")]
        private static extern bool FPDF_SaveWithVersionStatic(FPDF_DOCUMENT document, ref FPDF_FILEWRITE pFileWrite, FPDF_SAVEFLAGS flags, int fileVersion);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Same as FPDF_SaveAsCopy(), except the file version of the saved document can be specified by the caller.
        /// </summary>
        /// <param name="document">Handle to document.</param>
        /// <param name="pFileWrite">A pointer to a custom file write structure.</param>
        /// <param name="flags">The creating flags.</param>
        /// <param name="fileVersion">The PDF file version. File version: 14 for 1.4, 15 for 1.5, ...</param>
        /// <returns>TRUE if succeed, FALSE if failed.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_SaveWithVersion(FPDF_DOCUMENT document, FPDF_FILEWRITE* pFileWrite, int flags, int fileVersion);.
        /// </remarks>
        public bool FPDF_SaveWithVersion(FPDF_DOCUMENT document, ref FPDF_FILEWRITE pFileWrite, FPDF_SAVEFLAGS flags, int fileVersion)
        {
            lock (_syncObject)
            {
                return FPDF_SaveWithVersionStatic(document, ref pFileWrite, flags, fileVersion);
            }
        }

        private static void LoadDllSavePart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            // fpdf_save.h exports 2 functions
            FPDF_SaveAsCopyStatic = GetPDFiumFunction<FPDF_SaveAsCopy_Delegate>(nameof(FPDF_SaveAsCopy));
            FPDF_SaveWithVersionStatic = GetPDFiumFunction<FPDF_SaveWithVersion_Delegate>(nameof(FPDF_SaveWithVersion));
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }

        private static void UnloadDllSavePart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            // fpdf_save.h exports 2 functions
            FPDF_SaveAsCopyStatic = null;
            FPDF_SaveWithVersionStatic = null;
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }
    }
}
