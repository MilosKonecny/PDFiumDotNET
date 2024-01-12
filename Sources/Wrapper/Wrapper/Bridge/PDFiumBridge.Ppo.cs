namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;

    // Disable "Member 'member' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    // Disable "Specify marshaling for P/Invoke string arguments."
#pragma warning disable CA2101

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_ImportPagesByIndex_Delegate(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, IntPtr page_indices, ulong length, int index);

        private static FPDF_ImportPagesByIndex_Delegate FPDF_ImportPagesByIndexStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDF_ImportPagesByIndex")]
        private static extern bool FPDF_ImportPagesByIndexStatic(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, IntPtr page_indices, ulong length, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API.
        /// Import pages to a FPDF_DOCUMENT.
        /// </summary>
        /// <param name="dest_doc">The destination document for the pages.</param>
        /// <param name="src_doc">The document to be imported.</param>
        /// <param name="page_indices">An array of page indices to be imported. The first page is zero.
        /// If |page_indices| is NULL, all pages from |src_doc| are imported.</param>
        /// <param name="length">The length of the |page_indices| array. NOT USED: automatically generated from page_indices.</param>
        /// <param name="index">The page index at which to insert the first imported page into |dest_doc|. The first page is zero.</param>
        /// <returns>Returns TRUE on success. Returns FALSE if any pages in |page_indices| is invalid.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_ImportPagesByIndex(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, const int* page_indices, unsigned long length, int index);.
        /// </remarks>
        public bool FPDF_ImportPagesByIndex(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, int[] page_indices, ulong length, int index)
        {
            lock (_syncObject)
            {
                var pagesIntPtr = NativeMethods.StructureArrayToIntPtr(page_indices);
                var ret = FPDF_ImportPagesByIndexStatic(dest_doc, src_doc, pagesIntPtr, (ulong)page_indices.Length, index);
                Marshal.FreeHGlobal(pagesIntPtr);
                return ret;
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_ImportPages_Delegate(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, [MarshalAs(UnmanagedType.LPStr)] string pagerange, int index);

        private static FPDF_ImportPages_Delegate FPDF_ImportPagesStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDF_ImportPages")]
        private static extern bool FPDF_ImportPagesStatic(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, [MarshalAs(UnmanagedType.LPStr)] string pagerange, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Import pages to a FPDF_DOCUMENT.
        /// </summary>
        /// <param name="dest_doc">The destination document for the pages.</param>
        /// <param name="src_doc">The document to be imported.</param>
        /// <param name="pagerange">A page range string, Such as "1,3,5-7". The first page is one. If |pagerange| is NULL, all pages from |src_doc| are imported.</param>
        /// <param name="index">The page index at which to insert the first imported page into |dest_doc|. The first page is zero.</param>
        /// <returns>Returns TRUE on success. Returns FALSE if any pages in |pagerange| is invalid or if |pagerange| cannot be read.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_ImportPages(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, FPDF_BYTESTRING pagerange, int index);.
        /// </remarks>
        public bool FPDF_ImportPages(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, string pagerange, int index)
        {
            lock (_syncObject)
            {
                return FPDF_ImportPagesStatic(dest_doc, src_doc, pagerange, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DOCUMENT FPDF_ImportNPagesToOne_Delegate(FPDF_DOCUMENT src_doc, float output_width, float output_height, int num_pages_on_x_axis, int num_pages_on_y_axis);

        private static FPDF_ImportNPagesToOne_Delegate FPDF_ImportNPagesToOneStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDF_ImportNPagesToOne")]
        private static extern FPDF_DOCUMENT FPDF_ImportNPagesToOneStatic(FPDF_DOCUMENT src_doc, float output_width, float output_height, int num_pages_on_x_axis, int num_pages_on_y_axis);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API.
        /// Create a new document from |src_doc|.
        /// The pages of |src_doc| will be combined to provide |num_pages_on_x_axis x num_pages_on_y_axis| pages per |output_doc| page.
        /// </summary>
        /// <param name="src_doc">The document to be imported.</param>
        /// <param name="output_width">The output page width in PDF "user space" units.</param>
        /// <param name="output_height">The output page height in PDF "user space" units.</param>
        /// <param name="num_pages_on_x_axis">The number of pages on X Axis.</param>
        /// <param name="num_pages_on_y_axis">The number of pages on Y Axis.</param>
        /// <returns>A handle to the created document, or NULL on failure.</returns>
        /// <remarks>
        /// number of pages per page = num_pages_on_x_axis * num_pages_on_y_axis
        /// FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_ImportNPagesToOne(FPDF_DOCUMENT src_doc, float output_width, float output_height, size_t num_pages_on_x_axis, size_t num_pages_on_y_axis);.
        /// </remarks>
        public FPDF_DOCUMENT FPDF_ImportNPagesToOne(FPDF_DOCUMENT src_doc, float output_width, float output_height, int num_pages_on_x_axis, int num_pages_on_y_axis)
        {
            lock (_syncObject)
            {
                return FPDF_ImportNPagesToOneStatic(src_doc, output_width, output_height, num_pages_on_x_axis, num_pages_on_y_axis);
            }
        }

        ////// Experimental API.
        ////// Create a template to generate form xobjects from |src_doc|'s page at
        ////// |src_page_index|, for use in |dest_doc|.
        //////
        ////// Returns a handle on success, or NULL on failure. Caller owns the newly
        ////// created object.
        ////FPDF_EXPORT FPDF_XOBJECT FPDF_CALLCONV FPDF_NewXObjectFromPage(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, int src_page_index);

        ////// Experimental API.
        ////// Close an FPDF_XOBJECT handle created by FPDF_NewXObjectFromPage().
        ////// FPDF_PAGEOBJECTs created from the FPDF_XOBJECT handle are not affected.
        ////FPDF_EXPORT void FPDF_CALLCONV FPDF_CloseXObject(FPDF_XOBJECT xobject);

        ////// Experimental API.
        ////// Create a new form object from an FPDF_XOBJECT object.
        //////
        ////// Returns a new form object on success, or NULL on failure. Caller owns the
        ////// newly created object.
        ////FPDF_EXPORT FPDF_PAGEOBJECT FPDF_CALLCONV FPDF_NewFormObjectFromXObject(FPDF_XOBJECT xobject);

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDF_CopyViewerPreferences_Delegate(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc);

        private static FPDF_CopyViewerPreferences_Delegate FPDF_CopyViewerPreferencesStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDF_CopyViewerPreferences")]
        private static extern bool FPDF_CopyViewerPreferencesStatic(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Copy the viewer preferences from |src_doc| into |dest_doc|.
        /// </summary>
        /// <param name="dest_doc">Document to write the viewer preferences into.</param>
        /// <param name="src_doc">Document to read the viewer preferences from.</param>
        /// <returns>Returns TRUE on success.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_CopyViewerPreferences(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc);.
        /// </remarks>
        public bool FPDF_CopyViewerPreferences(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc)
        {
            lock (_syncObject)
            {
                return FPDF_CopyViewerPreferencesStatic(dest_doc, src_doc);
            }
        }

        private static void LoadDllPpoPart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            // fpdf_ppo.h exports 7 functions
            FPDF_ImportPagesByIndexStatic = GetPDFiumFunction<FPDF_ImportPagesByIndex_Delegate>(nameof(FPDF_ImportPagesByIndex));
            FPDF_ImportPagesStatic = GetPDFiumFunction<FPDF_ImportPages_Delegate>(nameof(FPDF_ImportPages));
            FPDF_ImportNPagesToOneStatic = GetPDFiumFunction<FPDF_ImportNPagesToOne_Delegate>(nameof(FPDF_ImportNPagesToOne));
            FPDF_CopyViewerPreferencesStatic = GetPDFiumFunction<FPDF_CopyViewerPreferences_Delegate>(nameof(FPDF_CopyViewerPreferences));
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }

        private static void UnloadDllPpoPart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            // fpdf_ppo.h exports 7 functions
            FPDF_ImportPagesByIndexStatic = null;
            FPDF_ImportPagesStatic = null;
            FPDF_ImportNPagesToOneStatic = null;
            FPDF_CopyViewerPreferencesStatic = null;
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }
    }
}
