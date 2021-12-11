namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;

    // Disable "Member 'xxxx' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_InitLibrary_Delegate();

        private static FPDF_InitLibrary_Delegate FPDF_InitLibraryStatic { get; set; }

        /// <summary>
        /// Initialize the FPDFSDK library.
        /// </summary>
        /// <remarks>
        /// Convenience function to call FPDF_InitLibraryWithConfig() for backwards compatibility purposes.
        /// This will be deprecated in the future.
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_InitLibrary();.
        /// </remarks>
        public void FPDF_InitLibrary()
        {
            lock (_syncObject)
            {
                FPDF_InitLibraryStatic();
            }
        }

        ////// Function: FPDF_InitLibraryWithConfig
        //////          Initialize the FPDFSDK library
        ////// Parameters:
        //////          config - configuration information as above.
        ////// Return value:
        //////          None.
        ////// Comments:
        //////          You have to call this function before you can call any PDF
        //////          processing functions.
        ////FPDF_EXPORT void FPDF_CALLCONV FPDF_InitLibraryWithConfig(const FPDF_LIBRARY_CONFIG* config);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_DestroyLibrary_Delegate();

        private static FPDF_DestroyLibrary_Delegate FPDF_DestroyLibraryStatic { get; set; }

        /// <summary>
        /// Release all resources allocated by the FPDFSDK library.
        /// </summary>
        /// <remarks>
        /// You can call this function to release all memory blocks allocated by the library.
        /// After this function is called, you should not call any PDF processing functions.
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_DestroyLibrary();.
        /// </remarks>
        public void FPDF_DestroyLibrary()
        {
            lock (_syncObject)
            {
                FPDF_DestroyLibraryStatic();
            }
        }

        ////// Function: FPDF_SetSandBoxPolicy
        //////          Set the policy for the sandbox environment.
        ////// Parameters:
        //////          policy -   The specified policy for setting, for example:
        //////                     FPDF_POLICY_MACHINETIME_ACCESS.
        //////          enable -   True to enable, false to disable the policy.
        ////// Return value:
        //////          None.
        ////FPDF_EXPORT void FPDF_CALLCONV FPDF_SetSandBoxPolicy(FPDF_DWORD policy, FPDF_BOOL enable);

        ////// Experimental API.
        ////// Function: FPDF_SetTypefaceAccessibleFunc
        //////          Set the function pointer that makes GDI fonts available in sandboxed
        //////          environments.
        ////// Parameters:
        //////          func -   A function pointer. See description above.
        ////// Return value:
        //////          None.
        ////FPDF_EXPORT void FPDF_CALLCONV FPDF_SetTypefaceAccessibleFunc(PDFiumEnsureTypefaceCharactersAccessible func);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_SetPrintTextWithGDI_Delegate(bool use_gdi);

        private static FPDF_SetPrintTextWithGDI_Delegate FPDF_SetPrintTextWithGDIStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set whether to use GDI to draw fonts when printing on Windows.
        /// </summary>
        /// <param name="use_gdi">Set to true to enable printing text with GDI.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_SetPrintTextWithGDI(FPDF_BOOL use_gdi);.
        /// </remarks>
        public void FPDF_SetPrintTextWithGDI(bool use_gdi)
        {
            lock (_syncObject)
            {
                FPDF_SetPrintTextWithGDIStatic(use_gdi);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_SetPrintMode_Delegate(FPDF_PRINTMODES mode);

        private static FPDF_SetPrintMode_Delegate FPDF_SetPrintModeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set printing mode when printing on Windows.
        /// </summary>
        /// <param name="mode">
        /// FPDF_PRINTMODE_EMF to output EMF (default)
        /// FPDF_PRINTMODE_TEXTONLY to output text only (for charstream devices)
        /// FPDF_PRINTMODE_POSTSCRIPT2 to output level 2 PostScript into EMF as a series of GDI comments.
        /// FPDF_PRINTMODE_POSTSCRIPT3 to output level 3 PostScript into EMF as a series of GDI comments.
        /// FPDF_PRINTMODE_POSTSCRIPT2_PASSTHROUGH to output level 2 PostScript via ExtEscape() in PASSTHROUGH mode.
        /// FPDF_PRINTMODE_POSTSCRIPT3_PASSTHROUGH to output level 3 PostScript via ExtEscape() in PASSTHROUGH mode.
        /// FPDF_PRINTMODE_EMF_IMAGE_MASKS to output EMF, with more efficient processing of documents containing image masks.
        /// FPDF_PRINTMODE_POSTSCRIPT3_TYPE42 to output level 3 PostScript with embedded Type 42 fonts, when applicable, into EMF as a series of GDI comments.
        /// FPDF_PRINTMODE_POSTSCRIPT3_TYPE42_PASSTHROUGH to output level 3 PostScript with embedded Type 42 fonts, when applicable, via ExtEscape() in PASSTHROUGH mode.
        /// </param>
        /// <returns>True if successful, false if unsuccessful (typically invalid input).</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_SetPrintMode(int mode);.
        /// </remarks>
        public bool FPDF_SetPrintMode(FPDF_PRINTMODES mode)
        {
            lock (_syncObject)
            {
                return FPDF_SetPrintModeStatic(mode);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DOCUMENT FPDF_LoadDocument_Delegate([MarshalAs(UnmanagedType.LPStr)] string file_path, [MarshalAs(UnmanagedType.LPStr)] string password);

        private static FPDF_LoadDocument_Delegate FPDF_LoadDocumentStatic { get; set; }

        /// <summary>
        /// Open and load a PDF document.
        /// </summary>
        /// <param name="file_path">Path to the PDF file (including extension).</param>
        /// <param name="password">A string used as the password for the PDF file. If no password is needed, empty or NULL can be used.
        /// See comments below regarding the encoding.</param>
        /// <returns>A handle to the loaded document, or NULL on failure.</returns>
        /// <remarks>
        /// Loaded document can be closed by FPDF_CloseDocument(). If this function fails, you can use FPDF_GetLastError() to retrieve the reason why it failed.
        /// The encoding for |password| can be either UTF-8 or Latin-1. PDFs, depending on the security handler revision, will only accept one or the other encoding.
        /// If |password|'s encoding and the PDF's expected encoding do not match, FPDF_LoadDocument() will automatically convert |password| to the other encoding.
        /// FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadDocument(FPDF_STRING file_path, FPDF_BYTESTRING password);.
        /// </remarks>
        public FPDF_DOCUMENT FPDF_LoadDocument([MarshalAs(UnmanagedType.LPStr)] string file_path, [MarshalAs(UnmanagedType.LPStr)] string password)
        {
            lock (_syncObject)
            {
                return FPDF_LoadDocumentStatic(file_path, password);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DOCUMENT FPDF_LoadMemDocument_Delegate(IntPtr data_buf, int size, [MarshalAs(UnmanagedType.LPStr)] string password);

        private static FPDF_LoadMemDocument_Delegate FPDF_LoadMemDocumentStatic { get; set; }

        /// <summary>
        /// Open and load a PDF document from memory.
        /// </summary>
        /// <param name="data_buf">Pointer to a buffer containing the PDF document.</param>
        /// <param name="size">Number of bytes in the PDF document.</param>
        /// <param name="password">A string used as the password for the PDF file. If no password is needed, empty or NULL can be used.</param>
        /// <returns>A handle to the loaded document, or NULL on failure.</returns>
        /// <remarks>
        /// The memory buffer must remain valid when the document is open.
        /// The loaded document can be closed by FPDF_CloseDocument.
        /// If this function fails, you can use FPDF_GetLastError() to retrieve the reason why it failed.
        /// See the comments for <see cref="FPDF_LoadDocument"/> regarding the encoding for |password|.
        /// If PDFium is built with the XFA module, the application should call FPDF_LoadXFA() function
        /// after the PDF document loaded to support XFA fields defined in the fpdfformfill.h file.
        /// FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadMemDocument(const void* data_buf, int size, FPDF_BYTESTRING password);.
        /// </remarks>
        public FPDF_DOCUMENT FPDF_LoadMemDocument(IntPtr data_buf, int size, [MarshalAs(UnmanagedType.LPStr)] string password)
        {
            lock (_syncObject)
            {
                return FPDF_LoadMemDocumentStatic(data_buf, size, password);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DOCUMENT FPDF_LoadMemDocument64_Delegate(IntPtr data_buf, int size, [MarshalAs(UnmanagedType.LPStr)] string password);

        private static FPDF_LoadMemDocument64_Delegate FPDF_LoadMemDocument64Static { get; set; }

        /// <summary>
        /// Experimental API.
        /// Open and load a PDF document from memory.
        /// </summary>
        /// <param name="data_buf">Pointer to a buffer containing the PDF document.</param>
        /// <param name="size">Number of bytes in the PDF document.</param>
        /// <param name="password">A string used as the password for the PDF file. If no password is needed, empty or NULL can be used.</param>
        /// <returns>A handle to the loaded document, or NULL on failure.</returns>
        /// <remarks>
        /// The memory buffer must remain valid when the document is open.
        /// The loaded document can be closed by FPDF_CloseDocument.
        /// If this function fails, you can use FPDF_GetLastError() to retrieve the reason why it failed.
        /// See the comments for FPDF_LoadDocument() regarding the encoding for |password|.
        /// If PDFium is built with the XFA module, the application should call FPDF_LoadXFA() function
        /// after the PDF document loaded to support XFA fields defined in the fpdfformfill.h file.
        /// FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadMemDocument64(const void* data_buf, size_t size, FPDF_BYTESTRING password);.
        /// </remarks>
        public FPDF_DOCUMENT FPDF_LoadMemDocument64(IntPtr data_buf, int size, [MarshalAs(UnmanagedType.LPStr)] string password)
        {
            lock (_syncObject)
            {
                return FPDF_LoadMemDocument64Static(data_buf, size, password);
            }
        }

        ////// Function: FPDF_LoadCustomDocument
        //////          Load PDF document from a custom access descriptor.
        ////// Parameters:
        //////          pFileAccess -   A structure for accessing the file.
        //////          password    -   Optional password for decrypting the PDF file.
        ////// Return value:
        //////          A handle to the loaded document, or NULL on failure.
        ////// Comments:
        //////          The application must keep the file resources |pFileAccess| points to
        //////          valid until the returned FPDF_DOCUMENT is closed. |pFileAccess|
        //////          itself does not need to outlive the FPDF_DOCUMENT.
        //////
        //////          The loaded document can be closed with FPDF_CloseDocument().
        //////
        //////          See the comments for FPDF_LoadDocument() regarding the encoding for
        //////          |password|.
        ////// Notes:
        //////          If PDFium is built with the XFA module, the application should call
        //////          FPDF_LoadXFA() function after the PDF document loaded to support XFA
        //////          fields defined in the fpdfformfill.h file.
        ////FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadCustomDocument(FPDF_FILEACCESS* pFileAccess, FPDF_BYTESTRING password);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_GetFileVersion_Delegate(FPDF_DOCUMENT doc, out int fileVersion);

        private static FPDF_GetFileVersion_Delegate FPDF_GetFileVersionStatic { get; set; }

        /// <summary>
        /// Get the file version of the given PDF document.
        /// </summary>
        /// <param name="doc">Handle to a document.</param>
        /// <param name="fileVersion">The PDF file version. File version: 14 for 1.4, 15 for 1.5, ...</param>
        /// <returns>True if succeeds, false otherwise.</returns>
        /// <remarks>
        /// If the document was created by FPDF_CreateNewDocument, then this function will always fail.
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_GetFileVersion(FPDF_DOCUMENT doc, int* fileVersion);.
        /// </remarks>
        public bool FPDF_GetFileVersion(FPDF_DOCUMENT doc, out int fileVersion)
        {
            lock (_syncObject)
            {
                return FPDF_GetFileVersionStatic(doc, out fileVersion);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_ERROR FPDF_GetLastError_Delegate();

        private static FPDF_GetLastError_Delegate FPDF_GetLastErrorStatic { get; set; }

        /// <summary>
        /// Get last error code when a function fails.
        /// </summary>
        /// <returns>A 32-bit integer indicating error code as defined above.</returns>
        /// <remarks>
        /// If the previous SDK call succeeded, the return value of this function is not defined.
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDF_GetLastError();.
        /// </remarks>
        public FPDF_ERROR FPDF_GetLastError()
        {
            lock (_syncObject)
            {
                return FPDF_GetLastErrorStatic();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_DocumentHasValidCrossReferenceTable_Delegate(FPDF_DOCUMENT document);

        private static FPDF_DocumentHasValidCrossReferenceTable_Delegate FPDF_DocumentHasValidCrossReferenceTableStatic { get; set; }

        /// <summary>
        /// Whether the document's cross reference table is valid or not. Experimental API.
        /// </summary>
        /// <param name="document">Handle to a document. Returned by FPDF_LoadDocument.</param>
        /// <returns>
        /// True if the PDF parser did not encounter problems parsing the cross reference table.
        /// False if the parser could not parse the cross reference table and the table had to be rebuild from other data within the document.</returns>
        /// <remarks>
        /// The return value can change over time as the PDF parser evolves.
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_DocumentHasValidCrossReferenceTable(FPDF_DOCUMENT document);.
        /// </remarks>
        public bool FPDF_DocumentHasValidCrossReferenceTable(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_DocumentHasValidCrossReferenceTableStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDF_GetTrailerEnds_Delegate(FPDF_DOCUMENT document, IntPtr buffer, ulong length);

        private static FPDF_GetTrailerEnds_Delegate FPDF_GetTrailerEndsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the byte offsets of trailer ends.
        /// </summary>
        /// <param name="document">Handle to document. Returned by FPDF_LoadDocument().</param>
        /// <param name="buffer">The address of a buffer that receives the byte offsets.</param>
        /// <param name="length">The size, in ints, of |buffer|.</param>
        /// <returns>Returns the number of ints in the buffer on success, 0 on error.</returns>
        /// <remarks>
        /// |buffer| is an array of integers that describes the exact byte offsets of the trailer ends in the document.
        /// If |length| is less than the returned length, or |document| or |buffer| is NULL, |buffer| will not be modified.
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDF_GetTrailerEnds(FPDF_DOCUMENT document, unsigned int* buffer, unsigned long length);.
        /// </remarks>
        public ulong FPDF_GetTrailerEnds(FPDF_DOCUMENT document, IntPtr buffer, ulong length)
        {
            lock (_syncObject)
            {
                return FPDF_GetTrailerEndsStatic(document, buffer, length);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_PERMISSIONS FPDF_GetDocPermissions_Delegate(FPDF_DOCUMENT document);

        private static FPDF_GetDocPermissions_Delegate FPDF_GetDocPermissionsStatic { get; set; }

        /// <summary>
        /// Get file permission flags of the document.
        /// </summary>
        /// <param name="document">Handle to a document. Returned by FPDF_LoadDocument.</param>
        /// <returns>A 32-bit integer indicating permission flags. Please refer to the PDF Reference for detailed descriptions.
        /// If the document is not protected, 0xffffffff will be returned.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDF_GetDocPermissions(FPDF_DOCUMENT document);.
        /// </remarks>
        public FPDF_PERMISSIONS FPDF_GetDocPermissions(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_GetDocPermissionsStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_GetSecurityHandlerRevision_Delegate(FPDF_DOCUMENT document);

        private static FPDF_GetSecurityHandlerRevision_Delegate FPDF_GetSecurityHandlerRevisionStatic { get; set; }

        /// <summary>
        /// Get the revision for the security handler.
        /// </summary>
        /// <param name="document">Handle to a document. Returned by FPDF_LoadDocument.</param>
        /// <returns>The security handler revision number. Please refer to the PDF Reference for a detailed description.
        /// If the document is not protected, -1 will be returned.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDF_GetSecurityHandlerRevision(FPDF_DOCUMENT document);.
        /// </remarks>
        public int FPDF_GetSecurityHandlerRevision(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_GetSecurityHandlerRevisionStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_GetPageCount_Delegate(FPDF_DOCUMENT document);

        private static FPDF_GetPageCount_Delegate FPDF_GetPageCountStatic { get; set; }

        /// <summary>
        /// Get total number of pages in the document.
        /// </summary>
        /// <param name="document">Handle to document. Returned by FPDF_LoadDocument.</param>
        /// <returns>Total number of pages in the document.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDF_GetPageCount(FPDF_DOCUMENT document);.
        /// </remarks>
        public int FPDF_GetPageCount(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageCountStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_PAGE FPDF_LoadPage_Delegate(FPDF_DOCUMENT document, int page_index);

        private static FPDF_LoadPage_Delegate FPDF_LoadPageStatic { get; set; }

        /// <summary>
        /// Load a page inside the document.
        /// </summary>
        /// <param name="document">Handle to document. Returned by FPDF_LoadDocument.</param>
        /// <param name="page_index">Index number of the page. 0 for the first page.</param>
        /// <returns>A handle to the loaded page, or NULL if page load fails.</returns>
        /// <remarks>
        /// The loaded page can be rendered to devices using FPDF_RenderPage.
        /// The loaded page can be closed using FPDF_ClosePage.
        /// FPDF_EXPORT FPDF_PAGE FPDF_CALLCONV FPDF_LoadPage(FPDF_DOCUMENT document, int page_index);.
        /// </remarks>
        public FPDF_PAGE FPDF_LoadPage(FPDF_DOCUMENT document, int page_index)
        {
            lock (_syncObject)
            {
                return FPDF_LoadPageStatic(document, page_index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float FPDF_GetPageWidthF_Delegate(FPDF_PAGE page);

        private static FPDF_GetPageWidthF_Delegate FPDF_GetPageWidthFStatic { get; set; }

        /// <summary>
        /// Get page width.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage().</param>
        /// <returns>Page width (excluding non-displayable area) measured in points. One point is 1/72 inch (around 0.3528 mm).</returns>
        /// <remarks>
        /// FPDF_EXPORT float FPDF_CALLCONV FPDF_GetPageWidthF(FPDF_PAGE page);.
        /// </remarks>
        public float FPDF_GetPageWidthF(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageWidthFStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate double FPDF_GetPageWidth_Delegate(FPDF_PAGE page);

        private static FPDF_GetPageWidth_Delegate FPDF_GetPageWidthStatic { get; set; }

        /// <summary>
        /// Get page width.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <returns>Page width (excluding non-displayable area) measured in points. One point is 1/72 inch (around 0.3528 mm).</returns>
        /// <remarks>
        /// Prefer FPDF_GetPageWidthF() above. This will be deprecated in the future.
        /// FPDF_EXPORT double FPDF_CALLCONV FPDF_GetPageWidth(FPDF_PAGE page);.
        /// </remarks>
        public double FPDF_GetPageWidth(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageWidthStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float FPDF_GetPageHeightF_Delegate(FPDF_PAGE page);

        private static FPDF_GetPageHeightF_Delegate FPDF_GetPageHeightFStatic { get; set; }

        /// <summary>
        /// Get page height.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage().</param>
        /// <returns>Page height (excluding non-displayable area) measured in points. One point is 1/72 inch (around 0.3528 mm).</returns>
        /// <remarks>
        /// FPDF_EXPORT float FPDF_CALLCONV FPDF_GetPageHeightF(FPDF_PAGE page);.
        /// </remarks>
        public float FPDF_GetPageHeightF(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageHeightFStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate double FPDF_GetPageHeight_Delegate(FPDF_PAGE page);

        private static FPDF_GetPageHeight_Delegate FPDF_GetPageHeightStatic { get; set; }

        /// <summary>
        /// Get page height.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <returns>Page height (excluding non-displayable area) measured in points. One point is 1/72 inch (around 0.3528 mm).</returns>
        /// <remarks>
        /// Prefer FPDF_GetPageHeightF() above. This will be deprecated in the future.
        /// FPDF_EXPORT double FPDF_CALLCONV FPDF_GetPageHeight(FPDF_PAGE page);.
        /// </remarks>
        public double FPDF_GetPageHeight(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageHeightStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_GetPageBoundingBox_Delegate(FPDF_PAGE page, out FS_RECTF rect);

        private static FPDF_GetPageBoundingBox_Delegate FPDF_GetPageBoundingBoxStatic { get; set; }

        /// <summary>
        /// Get the bounding box of the page. This is the intersection between its media box and its crop box.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <param name="rect">Pointer to a rect to receive the page bounding box. On an error, |rect| won't be filled.</param>
        /// <returns>True for success.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_GetPageBoundingBox(FPDF_PAGE page, FS_RECTF* rect);.
        /// </remarks>
        public bool FPDF_GetPageBoundingBox(FPDF_PAGE page, out FS_RECTF rect)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageBoundingBoxStatic(page, out rect);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_GetPageSizeByIndexF_Delegate(FPDF_DOCUMENT document, int page_index, out FS_SIZEF size);

        private static FPDF_GetPageSizeByIndexF_Delegate FPDF_GetPageSizeByIndexFStatic { get; set; }

        /// <summary>
        /// Get the size of the page at the given index.
        /// </summary>
        /// <param name="document">Handle to document. Returned by FPDF_LoadDocument().</param>
        /// <param name="page_index">Page index, zero for the first page.</param>
        /// <param name="size">Pointer to a FS_SIZEF to receive the page size. (in points).</param>
        /// <returns>Non-zero for success. 0 for error (document or page not found).</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_GetPageSizeByIndexF(FPDF_DOCUMENT document, int page_index, FS_SIZEF* size);.
        /// </remarks>
        public bool FPDF_GetPageSizeByIndexF(FPDF_DOCUMENT document, int page_index, out FS_SIZEF size)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageSizeByIndexFStatic(document, page_index, out size);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_GetPageSizeByIndex_Delegate(FPDF_DOCUMENT document, int page_index, ref double width, ref double height);

        private static FPDF_GetPageSizeByIndex_Delegate FPDF_GetPageSizeByIndexStatic { get; set; }

        /// <summary>
        /// Get the size of the page at the given index.
        /// </summary>
        /// <param name="document">Handle to document. Returned by FPDF_LoadDocument.</param>
        /// <param name="page_index">Page index, zero for the first page.</param>
        /// <param name="width">Pointer to a double to receive the page width (in points).</param>
        /// <param name="height">Pointer to a double to receive the page height (in points).</param>
        /// <returns>Non-zero for success. 0 for error (document or page not found).</returns>
        /// <remarks>
        /// Prefer FPDF_GetPageSizeByIndexF() above. This will be deprecated in the future.
        /// FPDF_EXPORT int FPDF_CALLCONV FPDF_GetPageSizeByIndex(FPDF_DOCUMENT document, int page_index, double* width, double* height);.
        /// </remarks>
        public int FPDF_GetPageSizeByIndex(FPDF_DOCUMENT document, int page_index, ref double width, ref double height)
        {
            lock (_syncObject)
            {
                return FPDF_GetPageSizeByIndexStatic(document, page_index, ref width, ref height);
            }
        }

        ////// Function: FPDF_RenderPage
        //////          Render contents of a page to a device (screen, bitmap, or printer).
        //////          This function is only supported on Windows.
        ////// Parameters:
        //////          dc          -   Handle to the device context.
        //////          page        -   Handle to the page. Returned by FPDF_LoadPage.
        //////          start_x     -   Left pixel position of the display area in
        //////                          device coordinates.
        //////          start_y     -   Top pixel position of the display area in device
        //////                          coordinates.
        //////          size_x      -   Horizontal size (in pixels) for displaying the page.
        //////          size_y      -   Vertical size (in pixels) for displaying the page.
        //////          rotate      -   Page orientation:
        //////                            0 (normal)
        //////                            1 (rotated 90 degrees clockwise)
        //////                            2 (rotated 180 degrees)
        //////                            3 (rotated 90 degrees counter-clockwise)
        //////          flags       -   0 for normal display, or combination of flags
        //////                          defined above.
        ////// Return value:
        //////          None.
        ////FPDF_EXPORT void FPDF_CALLCONV FPDF_RenderPage(HDC dc, FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, int flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_RenderPageBitmap_Delegate(FPDF_BITMAP bitmap, FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, FPDF_RENDERING_FLAGS flags);

        private static FPDF_RenderPageBitmap_Delegate FPDF_RenderPageBitmapStatic { get; set; }

        /// <summary>
        /// Render contents of a page to a device independent bitmap.
        /// </summary>
        /// <param name="bitmap">Handle to the device independent bitmap (as the output buffer).
        /// The bitmap handle can be created by FPDFBitmap_Create or retrieved from an image object by FPDFImageObj_GetBitmap.</param>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <param name="start_x">Left pixel position of the display area in bitmap coordinates.</param>
        /// <param name="start_y">Top pixel position of the display area in bitmap coordinates.</param>
        /// <param name="size_x">Horizontal size (in pixels) for displaying the page.</param>
        /// <param name="size_y">Vertical size (in pixels) for displaying the page.</param>
        /// <param name="rotate">Page orientation:
        /// 0 (normal),
        /// 1 (rotated 90 degrees clockwise),
        /// 2 (rotated 180 degrees),
        /// 3 (rotated 90 degrees counter-clockwise).</param>
        /// <param name="flags">0 for normal display, or combination of the Page Rendering flags defined above.
        /// With the FPDF_ANNOT flag, it renders all annotations that do not require user-interaction,
        /// which are all annotations except widget and popup annotations.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_RenderPageBitmap(FPDF_BITMAP bitmap, FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, int flags);.
        /// </remarks>
        public void FPDF_RenderPageBitmap(FPDF_BITMAP bitmap, FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, FPDF_RENDERING_FLAGS flags)
        {
            lock (_syncObject)
            {
                FPDF_RenderPageBitmapStatic(bitmap, page, start_x, start_y, size_x, size_y, rotate, flags);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_RenderPageBitmapWithMatrix_Delegate(FPDF_BITMAP bitmap, FPDF_PAGE page, ref FS_MATRIX matrix, ref FS_RECTF clipping, FPDF_RENDERING_FLAGS flags);

        private static FPDF_RenderPageBitmapWithMatrix_Delegate FPDF_RenderPageBitmapWithMatrixStatic { get; set; }

        /// <summary>
        /// Render contents of a page to a device independent bitmap.
        /// </summary>
        /// <param name="bitmap">Handle to the device independent bitmap (as the output buffer).
        /// The bitmap handle can be created by FPDFBitmap_Create or retrieved by FPDFImageObj_GetBitmap.</param>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <param name="matrix">The transform matrix, which must be invertible. See PDF Reference 1.7, 4.2.2 Common Transformations.</param>
        /// <param name="clipping">The rect to clip to in device coords.</param>
        /// <param name="flags">0 for normal display, or combination of the Page Rendering flags defined above.
        /// With the FPDF_ANNOT flag, it renders all annotations that do not require user-interaction,
        /// which are all annotations except widget and popup annotations.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_RenderPageBitmapWithMatrix(FPDF_BITMAP bitmap, FPDF_PAGE page, const FS_MATRIX* matrix, const FS_RECTF* clipping, int flags);.
        /// </remarks>
        public void FPDF_RenderPageBitmapWithMatrix(FPDF_BITMAP bitmap, FPDF_PAGE page, ref FS_MATRIX matrix, ref FS_RECTF clipping, FPDF_RENDERING_FLAGS flags)
        {
            lock (_syncObject)
            {
                FPDF_RenderPageBitmapWithMatrixStatic(bitmap, page, ref matrix, ref clipping, flags);
            }
        }

        //////
        //////
        //////
        //////
        //////
        //////
        //////
        //////
        //////
        ////FPDF_EXPORT FPDF_RECORDER FPDF_CALLCONV FPDF_RenderPageSkp(FPDF_PAGE page, int size_x, int size_y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_ClosePage_Delegate(FPDF_PAGE page);

        private static FPDF_ClosePage_Delegate FPDF_ClosePageStatic { get; set; }

        /// <summary>
        /// Close a loaded PDF page.
        /// </summary>
        /// <param name="page">Handle to the loaded page.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_ClosePage(FPDF_PAGE page);.
        /// </remarks>
        public void FPDF_ClosePage(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                FPDF_ClosePageStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDF_CloseDocument_Delegate(FPDF_DOCUMENT document);

        private static FPDF_CloseDocument_Delegate FPDF_CloseDocumentStatic { get; set; }

        /// <summary>
        /// Close a loaded PDF document.
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDF_CloseDocument(FPDF_DOCUMENT document);.
        /// </remarks>
        public void FPDF_CloseDocument(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                FPDF_CloseDocumentStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_DeviceToPage_Delegate(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, int device_x, int device_y, ref double page_x, ref double page_y);

        private static FPDF_DeviceToPage_Delegate FPDF_DeviceToPageStatic { get; set; }

        /// <summary>
        /// Convert the screen coordinates of a point to page coordinates.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <param name="start_x">Left pixel position of the display area in device coordinates.</param>
        /// <param name="start_y">Top pixel position of the display area in device coordinates.</param>
        /// <param name="size_x">Horizontal size (in pixels) for displaying the page.</param>
        /// <param name="size_y">Vertical size (in pixels) for displaying the page.</param>
        /// <param name="rotate">Page orientation:
        /// 0 (normal)
        /// 1 (rotated 90 degrees clockwise)
        /// 2 (rotated 180 degrees)
        /// 3 (rotated 90 degrees counter-clockwise).</param>
        /// <param name="device_x">X value in device coordinates to be converted.</param>
        /// <param name="device_y">Y value in device coordinates to be converted.</param>
        /// <param name="page_x">A pointer to a double receiving the converted X value in page coordinates.</param>
        /// <param name="page_y">A pointer to a double receiving the converted Y value in page coordinates.</param>
        /// <returns>Returns true if the conversion succeeds, and page_x and page_y successfully receives the converted coordinates.</returns>
        /// <remarks>
        /// The page coordinate system has its origin at the left-bottom corner of the page, with the X-axis on the bottom going to the right,
        /// and the Y-axis on the left side going up.
        /// NOTE: this coordinate system can be altered when you zoom, scroll, or rotate a page, however, a point on the page should always have
        /// the same coordinate values in the page coordinate system.
        /// The device coordinate system is device dependent. For screen device, its origin is at the left-top corner of the window. However this
        /// origin can be altered by the Windows coordinate transformation utilities.
        /// You must make sure the start_x, start_y, size_x, size_y and rotate parameters have exactly same values as you used in
        /// the FPDF_RenderPage() function call.
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_DeviceToPage(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, int device_x, int device_y, double* page_x, double* page_y);.
        /// </remarks>
        public bool FPDF_DeviceToPage(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, int device_x, int device_y, ref double page_x, ref double page_y)
        {
            lock (_syncObject)
            {
                return FPDF_DeviceToPageStatic(page, start_x, start_y, size_x, size_y, rotate, device_x, device_y, ref page_x, ref page_y);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_PageToDevice_Delegate(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, double page_x, double page_y, ref int device_x, ref int device_y);

        private static FPDF_PageToDevice_Delegate FPDF_PageToDeviceStatic { get; set; }

        /// <summary>
        /// Convert the page coordinates of a point to screen coordinates.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage.</param>
        /// <param name="start_x">Left pixel position of the display area in device coordinates.</param>
        /// <param name="start_y">Top pixel position of the display area in device coordinates.</param>
        /// <param name="size_x">Horizontal size (in pixels) for displaying the page.</param>
        /// <param name="size_y">Vertical size (in pixels) for displaying the page.</param>
        /// <param name="rotate">Page orientation:
        /// 0 (normal)
        /// 1 (rotated 90 degrees clockwise)
        /// 2 (rotated 180 degrees)
        /// 3 (rotated 90 degrees counter-clockwise).</param>
        /// <param name="page_x">X value in page coordinates.</param>
        /// <param name="page_y">Y value in page coordinate.</param>
        /// <param name="device_x">A pointer to an integer receiving the result X value in device coordinates.</param>
        /// <param name="device_y">A pointer to an integer receiving the result Y value in device coordinates.</param>
        /// <returns>Returns true if the conversion succeeds, and device_x and device_y successfully receives the converted coordinates.</returns>
        /// <remarks>
        /// See <see cref="FPDF_DeviceToPage"/>.
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_PageToDevice(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, double page_x, double page_y, int* device_x, int* device_y);.
        /// </remarks>
        public bool FPDF_PageToDevice(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, double page_x, double page_y, ref int device_x, ref int device_y)
        {
            lock (_syncObject)
            {
                return FPDF_PageToDeviceStatic(page, start_x, start_y, size_x, size_y, rotate, page_x, page_y, ref device_x, ref device_y);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_BITMAP FPDFBitmap_Create_Delegate(int width, int height, bool hasAlpha);

        private static FPDFBitmap_Create_Delegate FPDFBitmap_CreateStatic { get; set; }

        /// <summary>
        /// Create a device independent bitmap (FXDIB).
        /// </summary>
        /// <param name="width">The number of pixels in width for the bitmap. Must be greater than 0.</param>
        /// <param name="height">The number of pixels in height for the bitmap. Must be greater than 0.</param>
        /// <param name="hasAlpha">A flag indicating whether the alpha channel is used. Non-zero for using alpha, zero for not using.</param>
        /// <returns>The created bitmap handle, or NULL if a parameter error or out of memory.</returns>
        /// <remarks>
        /// The bitmap always uses 4 bytes per pixel. The first byte is always double word aligned.
        /// The byte order is BGRx (the last byte unused if no alpha channel) or BGRA.
        /// The pixels in a horizontal line are stored side by side, with the left most pixel stored first (with lower memory address).
        /// Each line uses width * 4 bytes.
        /// Lines are stored one after another, with the top most line stored first. There is no gap between adjacent lines.
        /// This function allocates enough memory for holding all pixels in the bitmap, but it doesn't initialize the buffer.
        /// Applications can use FPDFBitmap_FillRect() to fill the bitmap using any color.
        /// If the OS allows it, this function can allocate up to 4 GB of memory.
        /// FPDF_EXPORT FPDF_BITMAP FPDF_CALLCONV FPDFBitmap_Create(int width, int height, int alpha);.
        /// </remarks>
        public FPDF_BITMAP FPDFBitmap_Create(int width, int height, bool hasAlpha)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_CreateStatic(width, height, hasAlpha);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_BITMAP FPDFBitmap_CreateEx_Delegate(int width, int height, FPDFBitmapFormat format, IntPtr first_scan, int stride);

        private static FPDFBitmap_CreateEx_Delegate FPDFBitmap_CreateExStatic { get; set; }

        /// <summary>
        /// Create a device independent bitmap (FXDIB).
        /// </summary>
        /// <param name="width">The number of pixels in width for the bitmap. Must be greater than 0.</param>
        /// <param name="height">The number of pixels in height for the bitmap. Must be greater than 0.</param>
        /// <param name="format">A number indicating for bitmap format, as defined above.</param>
        /// <param name="first_scan">A pointer to the first byte of the first line if using an external buffer.
        /// If this parameter is NULL, then the a new buffer will be created.</param>
        /// <param name="stride">Number of bytes for each scan line, for external buffer only.</param>
        /// <returns>The bitmap handle, or NULL if parameter error or out of memory.</returns>
        /// <remarks>
        /// Similar to FPDFBitmap_Create function, but allows for more formats and an external buffer is supported.
        /// The bitmap created by this function can be used in any place that a FPDF_BITMAP handle is required.
        /// If an external buffer is used, then the application should destroy the buffer by itself.
        /// FPDFBitmap_Destroy function will not destroy the buffer.
        /// FPDF_EXPORT FPDF_BITMAP FPDF_CALLCONV FPDFBitmap_CreateEx(int width, int height, int format, void* first_scan, int stride);.
        /// </remarks>
        public FPDF_BITMAP FPDFBitmap_CreateEx(int width, int height, FPDFBitmapFormat format, IntPtr first_scan, int stride)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_CreateExStatic(width, height, format, first_scan, stride);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDFBitmapFormat FPDFBitmap_GetFormat_Delegate(FPDF_BITMAP bitmap);

        private static FPDFBitmap_GetFormat_Delegate FPDFBitmap_GetFormatStatic { get; set; }

        /// <summary>
        /// Get the format of the bitmap.
        /// </summary>
        /// <param name="bitmap">Handle to the bitmap. Returned by FPDFBitmap_Create or FPDFImageObj_GetBitmap.</param>
        /// <returns>The format of the bitmap.</returns>
        /// <remarks>
        /// Only formats supported by FPDFBitmap_CreateEx are supported by this function; see the list of such formats above.
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFBitmap_GetFormat(FPDF_BITMAP bitmap);.
        /// </remarks>
        public FPDFBitmapFormat FPDFBitmap_GetFormat(FPDF_BITMAP bitmap)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_GetFormatStatic(bitmap);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFBitmap_FillRect_Delegate(FPDF_BITMAP bitmap, int left, int top, int width, int height, FPDF_COLOR color);

        private static FPDFBitmap_FillRect_Delegate FPDFBitmap_FillRectStatic { get; set; }

        /// <summary>
        /// Fill a rectangle in a bitmap.
        /// </summary>
        /// <param name="bitmap">The handle to the bitmap. Returned by FPDFBitmap_Create.</param>
        /// <param name="left">The left position. Starting from 0 at the left-most pixel.</param>
        /// <param name="top">The top position. Starting from 0 at the top-most line.</param>
        /// <param name="width">Width in pixels to be filled.</param>
        /// <param name="height">Height in pixels to be filled.</param>
        /// <param name="color">A 32-bit value specifing the color, in 8888 ARGB format.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFBitmap_FillRect(FPDF_BITMAP bitmap, int left, int top, int width, int height, FPDF_DWORD color);.
        /// </remarks>
        public void FPDFBitmap_FillRect(FPDF_BITMAP bitmap, int left, int top, int width, int height, FPDF_COLOR color)
        {
            lock (_syncObject)
            {
                FPDFBitmap_FillRectStatic(bitmap, left, top, width, height, color);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr FPDFBitmap_GetBuffer_Delegate(FPDF_BITMAP bitmap);

        private static FPDFBitmap_GetBuffer_Delegate FPDFBitmap_GetBufferStatic { get; set; }

        /// <summary>
        /// Get data buffer of a bitmap.
        /// </summary>
        /// <param name="bitmap">Handle to the bitmap. Returned by FPDFBitmap_Create or FPDFImageObj_GetBitmap.</param>
        /// <returns>The pointer to the first byte of the bitmap buffer.</returns>
        /// <remarks>
        /// The stride may be more than width * number of bytes per pixel.
        /// Applications can use this function to get the bitmap buffer pointer, then manipulate any color and/or alpha values for any pixels in the bitmap.
        /// The data is in BGRA format. Where the A maybe unused if alpha was not specified.
        /// FPDF_EXPORT void* FPDF_CALLCONV FPDFBitmap_GetBuffer(FPDF_BITMAP bitmap);.
        /// </remarks>
        public IntPtr FPDFBitmap_GetBuffer(FPDF_BITMAP bitmap)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_GetBufferStatic(bitmap);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFBitmap_GetWidth_Delegate(FPDF_BITMAP bitmap);

        private static FPDFBitmap_GetWidth_Delegate FPDFBitmap_GetWidthStatic { get; set; }

        /// <summary>
        /// Get width of a bitmap.
        /// </summary>
        /// <param name="bitmap">Handle to the bitmap. Returned by FPDFBitmap_Create or FPDFImageObj_GetBitmap.</param>
        /// <returns>The width of the bitmap in pixels.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFBitmap_GetWidth(FPDF_BITMAP bitmap);.
        /// </remarks>
        public int FPDFBitmap_GetWidth(FPDF_BITMAP bitmap)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_GetWidthStatic(bitmap);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFBitmap_GetHeight_Delegate(FPDF_BITMAP bitmap);

        private static FPDFBitmap_GetHeight_Delegate FPDFBitmap_GetHeightStatic { get; set; }

        /// <summary>
        /// Get height of a bitmap.
        /// </summary>
        /// <param name="bitmap">Handle to the bitmap. Returned by FPDFBitmap_Create or FPDFImageObj_GetBitmap.</param>
        /// <returns>The height of the bitmap in pixels.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFBitmap_GetHeight(FPDF_BITMAP bitmap);.
        /// </remarks>
        public int FPDFBitmap_GetHeight(FPDF_BITMAP bitmap)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_GetHeightStatic(bitmap);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFBitmap_GetStride_Delegate(FPDF_BITMAP bitmap);

        private static FPDFBitmap_GetStride_Delegate FPDFBitmap_GetStrideStatic { get; set; }

        /// <summary>
        /// Get number of bytes for each line in the bitmap buffer.
        /// </summary>
        /// <param name="bitmap">Handle to the bitmap. Returned by FPDFBitmap_Create or FPDFImageObj_GetBitmap.</param>
        /// <returns>The number of bytes for each line in the bitmap buffer.</returns>
        /// <remarks>
        /// The stride may be more than width * number of bytes per pixel.
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFBitmap_GetStride(FPDF_BITMAP bitmap);.
        /// </remarks>
        public int FPDFBitmap_GetStride(FPDF_BITMAP bitmap)
        {
            lock (_syncObject)
            {
                return FPDFBitmap_GetStrideStatic(bitmap);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFBitmap_Destroy_Delegate(FPDF_BITMAP bitmap);

        private static FPDFBitmap_Destroy_Delegate FPDFBitmap_DestroyStatic { get; set; }

        /// <summary>
        /// Destroy a bitmap and release all related buffers.
        /// </summary>
        /// <param name="bitmap">Handle to the bitmap. Returned by FPDFBitmap_Create or FPDFImageObj_GetBitmap.</param>
        /// <remarks>
        /// This function will not destroy any external buffers provided when the bitmap was created.
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFBitmap_Destroy(FPDF_BITMAP bitmap);.
        /// </remarks>
        public void FPDFBitmap_Destroy(FPDF_BITMAP bitmap)
        {
            lock (_syncObject)
            {
                FPDFBitmap_DestroyStatic(bitmap);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_VIEWERREF_GetPrintScaling_Delegate(FPDF_DOCUMENT document);

        private static FPDF_VIEWERREF_GetPrintScaling_Delegate FPDF_VIEWERREF_GetPrintScalingStatic { get; set; }

        /// <summary>
        /// Whether the PDF document prefers to be scaled or not.
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <returns>True for yes.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_VIEWERREF_GetPrintScaling(FPDF_DOCUMENT document);.
        /// </remarks>
        public bool FPDF_VIEWERREF_GetPrintScaling(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetPrintScalingStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_VIEWERREF_GetNumCopies_Delegate(FPDF_DOCUMENT document);

        private static FPDF_VIEWERREF_GetNumCopies_Delegate FPDF_VIEWERREF_GetNumCopiesStatic { get; set; }

        /// <summary>
        /// Returns the number of copies to be printed.
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <returns>The number of copies to be printed.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDF_VIEWERREF_GetNumCopies(FPDF_DOCUMENT document);.
        /// </remarks>
        public int FPDF_VIEWERREF_GetNumCopies(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetNumCopiesStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_PAGERANGE FPDF_VIEWERREF_GetPrintPageRange_Delegate(FPDF_DOCUMENT document);

        private static FPDF_VIEWERREF_GetPrintPageRange_Delegate FPDF_VIEWERREF_GetPrintPageRangeStatic { get; set; }

        /// <summary>
        /// Page numbers to initialize print dialog box when file is printed.
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <returns>The print page range to be used for printing.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_PAGERANGE FPDF_CALLCONV FPDF_VIEWERREF_GetPrintPageRange(FPDF_DOCUMENT document);.
        /// </remarks>
        public FPDF_PAGERANGE FPDF_VIEWERREF_GetPrintPageRange(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetPrintPageRangeStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long FPDF_VIEWERREF_GetPrintPageRangeCount_Delegate(FPDF_PAGERANGE pagerange);

        private static FPDF_VIEWERREF_GetPrintPageRangeCount_Delegate FPDF_VIEWERREF_GetPrintPageRangeCountStatic { get; set; }

        /// <summary>
        /// Returns the number of elements in a FPDF_PAGERANGE. Experimental API.
        /// </summary>
        /// <param name="pagerange">Handle to the page range.</param>
        /// <returns>The number of elements in the page range. Returns 0 on error.</returns>
        /// <remarks>
        /// FPDF_EXPORT size_t FPDF_CALLCONV FPDF_VIEWERREF_GetPrintPageRangeCount(FPDF_PAGERANGE pagerange);.
        /// </remarks>
        public long FPDF_VIEWERREF_GetPrintPageRangeCount(FPDF_PAGERANGE pagerange)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetPrintPageRangeCountStatic(pagerange);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_VIEWERREF_GetPrintPageRangeElement_Delegate(FPDF_PAGERANGE pagerange, long index);

        private static FPDF_VIEWERREF_GetPrintPageRangeElement_Delegate FPDF_VIEWERREF_GetPrintPageRangeElementStatic { get; set; }

        /// <summary>
        /// Returns an element from a FPDF_PAGERANGE. Experimental API.
        /// </summary>
        /// <param name="pagerange">Handle to the page range.</param>
        /// <param name="index">Index of the element.</param>
        /// <returns>The value of the element in the page range at a given index. Returns -1 on error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDF_VIEWERREF_GetPrintPageRangeElement(FPDF_PAGERANGE pagerange, size_t index);.
        /// </remarks>
        public int FPDF_VIEWERREF_GetPrintPageRangeElement(FPDF_PAGERANGE pagerange, long index)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetPrintPageRangeElementStatic(pagerange, index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DUPLEXTYPE FPDF_VIEWERREF_GetDuplex_Delegate(FPDF_DOCUMENT document);

        private static FPDF_VIEWERREF_GetDuplex_Delegate FPDF_VIEWERREF_GetDuplexStatic { get; set; }

        /// <summary>
        /// Returns the paper handling option to be used when printing from the print dialog.
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <returns>The paper handling option to be used when printing.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_DUPLEXTYPE FPDF_CALLCONV FPDF_VIEWERREF_GetDuplex(FPDF_DOCUMENT document);.
        /// </remarks>
        public FPDF_DUPLEXTYPE FPDF_VIEWERREF_GetDuplex(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetDuplexStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint FPDF_VIEWERREF_GetName_Delegate(FPDF_DOCUMENT document, [MarshalAs(UnmanagedType.LPTStr)] string key, IntPtr buffer, uint length);

        private static FPDF_VIEWERREF_GetName_Delegate FPDF_VIEWERREF_GetNameStatic { get; set; }

        /// <summary>
        /// Gets the contents for a viewer ref, with a given key. The value must be of type "name".
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <param name="key">Name of the key in the viewer pref dictionary, encoded in UTF-8.</param>
        /// <param name="buffer">A string to write the contents of the key to.</param>
        /// <param name="length">Length of the buffer.</param>
        /// <returns>The number of bytes in the contents, including the NULL terminator.
        /// Thus if the return value is 0, then that indicates an error, such as when document is invalid or buffer is NULL.
        /// If length is less than the returned length, or buffer is NULL, buffer will not be modified.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDF_VIEWERREF_GetName(FPDF_DOCUMENT document, FPDF_BYTESTRING key, char* buffer, unsigned long length);.
        /// </remarks>
        public uint FPDF_VIEWERREF_GetName(FPDF_DOCUMENT document, [MarshalAs(UnmanagedType.LPTStr)] string key, IntPtr buffer, uint length)
        {
            lock (_syncObject)
            {
                return FPDF_VIEWERREF_GetNameStatic(document, key, buffer, length);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_CountNamedDests_Delegate(FPDF_DOCUMENT document);

        private static FPDF_CountNamedDests_Delegate FPDF_CountNamedDestsStatic { get; set; }

        /// <summary>
        /// Get the count of named destinations in the PDF document.
        /// </summary>
        /// <param name="document">Handle to a document.</param>
        /// <returns>The count of named destinations.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_DWORD FPDF_CALLCONV FPDF_CountNamedDests(FPDF_DOCUMENT document);.
        /// </remarks>
        public int FPDF_CountNamedDests(FPDF_DOCUMENT document)
        {
            lock (_syncObject)
            {
                return FPDF_CountNamedDestsStatic(document);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DEST FPDF_GetNamedDestByName_Delegate(FPDF_DOCUMENT document, string name);

        private static FPDF_GetNamedDestByName_Delegate FPDF_GetNamedDestByNameStatic { get; set; }

        /// <summary>
        /// Get a the destination handle for the given name.
        /// </summary>
        /// <param name="document">Handle to the loaded document.</param>
        /// <param name="name">The name of a destination.</param>
        /// <returns>The handle to the destination.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_DEST FPDF_CALLCONV FPDF_GetNamedDestByName(FPDF_DOCUMENT document, FPDF_BYTESTRING name);.
        /// </remarks>
        public FPDF_DEST FPDF_GetNamedDestByName(FPDF_DOCUMENT document, string name)
        {
            lock (_syncObject)
            {
                return FPDF_GetNamedDestByNameStatic(document, name);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DEST FPDF_GetNamedDest_Delegate(FPDF_DOCUMENT document, int index, IntPtr buffer, ref int buflen);

        private static FPDF_GetNamedDest_Delegate FPDF_GetNamedDestStatic { get; set; }

        /// <summary>
        /// Get the named destination by index.
        /// </summary>
        /// <param name="document">Handle to a document.</param>
        /// <param name="index">The index of a named destination.</param>
        /// <param name="buffer">The buffer to store the destination name, used as wchar_t*.</param>
        /// <param name="buflen">Size of the buffer in bytes on input, length of the result in bytes on output or -1 if the buffer is too small.</param>
        /// <returns>The destination handle for a given index, or NULL if there is no named destination corresponding to |index|.</returns>
        /// <remarks>
        /// Call this function twice to get the name of the named destination:
        /// 1) First time pass in |buffer| as NULL and get buflen.
        /// 2) Second time pass in allocated |buffer| and buflen to retrieve |buffer|, which should be used as wchar_t*.
        /// If buflen is not sufficiently large, it will be set to -1 upon return.
        /// FPDF_EXPORT FPDF_DEST FPDF_CALLCONV FPDF_GetNamedDest(FPDF_DOCUMENT document, int index, void* buffer, long* buflen);.
        /// </remarks>
        public FPDF_DEST FPDF_GetNamedDest(FPDF_DOCUMENT document, int index, IntPtr buffer, ref int buflen)
        {
            lock (_syncObject)
            {
                return FPDF_GetNamedDestStatic(document, index, buffer, ref buflen);
            }
        }

        private static void LoadDllViewPart()
        {
            LoadDllViewPart1();
            LoadDllViewPart2();
        }

        private static void LoadDllViewPart1()
        {
            FPDF_InitLibraryStatic = GetPDFiumFunction<FPDF_InitLibrary_Delegate>(nameof(FPDF_InitLibrary));
            ////FPDF_EXPORT void FPDF_CALLCONV FPDF_InitLibraryWithConfig(const FPDF_LIBRARY_CONFIG* config);
            FPDF_DestroyLibraryStatic = GetPDFiumFunction<FPDF_DestroyLibrary_Delegate>(nameof(FPDF_DestroyLibrary));
            ////FPDF_EXPORT void FPDF_CALLCONV FPDF_SetSandBoxPolicy(FPDF_DWORD policy, FPDF_BOOL enable);
            ////FPDF_EXPORT void FPDF_CALLCONV FPDF_SetTypefaceAccessibleFunc(PDFiumEnsureTypefaceCharactersAccessible func);
            FPDF_SetPrintTextWithGDIStatic = GetPDFiumFunction<FPDF_SetPrintTextWithGDI_Delegate>(nameof(FPDF_SetPrintTextWithGDI));
            FPDF_SetPrintModeStatic = GetPDFiumFunction<FPDF_SetPrintMode_Delegate>(nameof(FPDF_SetPrintMode));
            FPDF_LoadDocumentStatic = GetPDFiumFunction<FPDF_LoadDocument_Delegate>(nameof(FPDF_LoadDocument));
            FPDF_LoadMemDocumentStatic = GetPDFiumFunction<FPDF_LoadMemDocument_Delegate>(nameof(FPDF_LoadMemDocument));
            FPDF_LoadMemDocument64Static = GetPDFiumFunction<FPDF_LoadMemDocument64_Delegate>(nameof(FPDF_LoadMemDocument64));
            ////FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadCustomDocument(FPDF_FILEACCESS* pFileAccess, FPDF_BYTESTRING password);
            FPDF_GetFileVersionStatic = GetPDFiumFunction<FPDF_GetFileVersion_Delegate>(nameof(FPDF_GetFileVersion));
            FPDF_GetLastErrorStatic = GetPDFiumFunction<FPDF_GetLastError_Delegate>(nameof(FPDF_GetLastError));
            FPDF_DocumentHasValidCrossReferenceTableStatic = GetPDFiumFunction<FPDF_DocumentHasValidCrossReferenceTable_Delegate>(nameof(FPDF_DocumentHasValidCrossReferenceTable));
            FPDF_GetTrailerEndsStatic = GetPDFiumFunction<FPDF_GetTrailerEnds_Delegate>(nameof(FPDF_GetTrailerEnds));
            FPDF_GetDocPermissionsStatic = GetPDFiumFunction<FPDF_GetDocPermissions_Delegate>(nameof(FPDF_GetDocPermissions));
            FPDF_GetSecurityHandlerRevisionStatic = GetPDFiumFunction<FPDF_GetSecurityHandlerRevision_Delegate>(nameof(FPDF_GetSecurityHandlerRevision));
            FPDF_GetPageCountStatic = GetPDFiumFunction<FPDF_GetPageCount_Delegate>(nameof(FPDF_GetPageCount));
            FPDF_LoadPageStatic = GetPDFiumFunction<FPDF_LoadPage_Delegate>(nameof(FPDF_LoadPage));
            FPDF_GetPageWidthFStatic = GetPDFiumFunction<FPDF_GetPageWidthF_Delegate>(nameof(FPDF_GetPageWidthF));
            FPDF_GetPageWidthStatic = GetPDFiumFunction<FPDF_GetPageWidth_Delegate>(nameof(FPDF_GetPageWidth));
            FPDF_GetPageHeightFStatic = GetPDFiumFunction<FPDF_GetPageHeightF_Delegate>(nameof(FPDF_GetPageHeightF));
            FPDF_GetPageHeightStatic = GetPDFiumFunction<FPDF_GetPageHeight_Delegate>(nameof(FPDF_GetPageHeight));
            FPDF_GetPageBoundingBoxStatic = GetPDFiumFunction<FPDF_GetPageBoundingBox_Delegate>(nameof(FPDF_GetPageBoundingBox));
            FPDF_GetPageSizeByIndexFStatic = GetPDFiumFunction<FPDF_GetPageSizeByIndexF_Delegate>(nameof(FPDF_GetPageSizeByIndexF));
            FPDF_GetPageSizeByIndexStatic = GetPDFiumFunction<FPDF_GetPageSizeByIndex_Delegate>(nameof(FPDF_GetPageSizeByIndex));
            ////FPDF_EXPORT void FPDF_CALLCONV FPDF_RenderPage(HDC dc, FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, int rotate, int flags);
            FPDF_RenderPageBitmapStatic = GetPDFiumFunction<FPDF_RenderPageBitmap_Delegate>(nameof(FPDF_RenderPageBitmap));
            FPDF_RenderPageBitmapWithMatrixStatic = GetPDFiumFunction<FPDF_RenderPageBitmapWithMatrix_Delegate>(nameof(FPDF_RenderPageBitmapWithMatrix));
            ////FPDF_EXPORT FPDF_RECORDER FPDF_CALLCONV FPDF_RenderPageSkp(FPDF_PAGE page, int size_x, int size_y);
            FPDF_ClosePageStatic = GetPDFiumFunction<FPDF_ClosePage_Delegate>(nameof(FPDF_ClosePage));
            FPDF_CloseDocumentStatic = GetPDFiumFunction<FPDF_CloseDocument_Delegate>(nameof(FPDF_CloseDocument));
        }

        private static void LoadDllViewPart2()
        {
            FPDF_DeviceToPageStatic = GetPDFiumFunction<FPDF_DeviceToPage_Delegate>(nameof(FPDF_DeviceToPage));
            FPDF_PageToDeviceStatic = GetPDFiumFunction<FPDF_PageToDevice_Delegate>(nameof(FPDF_PageToDevice));
            FPDFBitmap_CreateStatic = GetPDFiumFunction<FPDFBitmap_Create_Delegate>(nameof(FPDFBitmap_Create));
            FPDFBitmap_CreateExStatic = GetPDFiumFunction<FPDFBitmap_CreateEx_Delegate>(nameof(FPDFBitmap_CreateEx));
            FPDFBitmap_GetFormatStatic = GetPDFiumFunction<FPDFBitmap_GetFormat_Delegate>(nameof(FPDFBitmap_GetFormat));
            FPDFBitmap_FillRectStatic = GetPDFiumFunction<FPDFBitmap_FillRect_Delegate>(nameof(FPDFBitmap_FillRect));
            FPDFBitmap_GetBufferStatic = GetPDFiumFunction<FPDFBitmap_GetBuffer_Delegate>(nameof(FPDFBitmap_GetBuffer));
            FPDFBitmap_GetWidthStatic = GetPDFiumFunction<FPDFBitmap_GetWidth_Delegate>(nameof(FPDFBitmap_GetWidth));
            FPDFBitmap_GetHeightStatic = GetPDFiumFunction<FPDFBitmap_GetHeight_Delegate>(nameof(FPDFBitmap_GetHeight));
            FPDFBitmap_GetStrideStatic = GetPDFiumFunction<FPDFBitmap_GetStride_Delegate>(nameof(FPDFBitmap_GetStride));
            FPDFBitmap_DestroyStatic = GetPDFiumFunction<FPDFBitmap_Destroy_Delegate>(nameof(FPDFBitmap_Destroy));
            FPDF_VIEWERREF_GetPrintScalingStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetPrintScaling_Delegate>(nameof(FPDF_VIEWERREF_GetPrintScaling));
            FPDF_VIEWERREF_GetNumCopiesStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetNumCopies_Delegate>(nameof(FPDF_VIEWERREF_GetNumCopies));
            FPDF_VIEWERREF_GetPrintPageRangeStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetPrintPageRange_Delegate>(nameof(FPDF_VIEWERREF_GetPrintPageRange));
            FPDF_VIEWERREF_GetPrintPageRangeCountStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetPrintPageRangeCount_Delegate>(nameof(FPDF_VIEWERREF_GetPrintPageRangeCount));
            FPDF_VIEWERREF_GetPrintPageRangeElementStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetPrintPageRangeElement_Delegate>(nameof(FPDF_VIEWERREF_GetPrintPageRangeElement));
            FPDF_VIEWERREF_GetDuplexStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetDuplex_Delegate>(nameof(FPDF_VIEWERREF_GetDuplex));
            FPDF_VIEWERREF_GetNameStatic = GetPDFiumFunction<FPDF_VIEWERREF_GetName_Delegate>(nameof(FPDF_VIEWERREF_GetName));
            FPDF_CountNamedDestsStatic = GetPDFiumFunction<FPDF_CountNamedDests_Delegate>(nameof(FPDF_CountNamedDests));
            FPDF_GetNamedDestByNameStatic = GetPDFiumFunction<FPDF_GetNamedDestByName_Delegate>(nameof(FPDF_GetNamedDestByName));
            FPDF_GetNamedDestStatic = GetPDFiumFunction<FPDF_GetNamedDest_Delegate>(nameof(FPDF_GetNamedDest));
        }
    }
}
