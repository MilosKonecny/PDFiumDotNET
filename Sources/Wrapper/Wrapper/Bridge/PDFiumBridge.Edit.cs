namespace PDFiumDotNET.Wrapper.Bridge
{
    using System.Runtime.InteropServices;

    // Disable "Member 'xxxx' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_DOCUMENT FPDF_CreateNewDocument_Delegate();

        private static FPDF_CreateNewDocument_Delegate FPDF_CreateNewDocumentStatic { get; set; }

        /// <summary>
        /// Create a new PDF document.
        /// </summary>
        /// <returns>Returns a handle to a new document, or NULL on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_CreateNewDocument();.
        /// </remarks>
        public FPDF_DOCUMENT FPDF_CreateNewDocument()
        {
            lock (_syncObject)
            {
                return FPDF_CreateNewDocumentStatic();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_PAGE FPDFPage_New_Delegate(FPDF_DOCUMENT document, int page_index, double width, double height);

        private static FPDFPage_New_Delegate FPDFPage_NewStatic { get; set; }

        /// <summary>
        /// Create a new PDF page.
        /// </summary>
        /// <param name="document">Handle to document.</param>
        /// <param name="page_index">Suggested 0-based index of the page to create. If it is larger than document's current last index(L),
        /// the created page index is the next available index -- L+1.</param>
        /// <param name="width">The page width in points.</param>
        /// <param name="height">The page height in points.</param>
        /// <returns>Returns the handle to the new page or NULL on failure.</returns>
        /// <remarks>
        /// The page should be closed with FPDF_ClosePage() when finished as with any other page in the document.
        /// FPDF_EXPORT FPDF_PAGE FPDF_CALLCONV FPDFPage_New(FPDF_DOCUMENT document, int page_index, double width, double height);.
        /// </remarks>
        public FPDF_PAGE FPDFPage_New(FPDF_DOCUMENT document, int page_index, double width, double height)
        {
            lock (_syncObject)
            {
                return FPDFPage_NewStatic(document, page_index, width, height);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFPage_Delete_Delegate(FPDF_DOCUMENT document, int page_index);

        private static FPDFPage_Delete_Delegate FPDFPage_DeleteStatic { get; set; }

        /// <summary>
        /// Delete the page at |page_index|.
        /// </summary>
        /// <param name="document">Handle to document.</param>
        /// <param name="page_index">The index of the page to delete.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFPage_Delete(FPDF_DOCUMENT document, int page_index);.
        /// </remarks>
        public void FPDFPage_Delete(FPDF_DOCUMENT document, int page_index)
        {
            lock (_syncObject)
            {
                FPDFPage_DeleteStatic(document, page_index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFPage_GetRotation_Delegate(FPDF_PAGE page);

        private static FPDFPage_GetRotation_Delegate FPDFPage_GetRotationStatic { get; set; }

        /// <summary>
        /// Get the rotation of |page|.
        /// </summary>
        /// <param name="page">Handle to a page.</param>
        /// <returns>Returns one of the following indicating the page rotation:
        /// 0 - No rotation.
        /// 1 - Rotated 90 degrees clockwise.
        /// 2 - Rotated 180 degrees clockwise.
        /// 3 - Rotated 270 degrees clockwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFPage_GetRotation(FPDF_PAGE page);.
        /// </remarks>
        public int FPDFPage_GetRotation(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDFPage_GetRotationStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFPage_SetRotation_Delegate(FPDF_PAGE page, int rotate);

        private static FPDFPage_SetRotation_Delegate FPDFPage_SetRotationStatic { get; set; }

        /// <summary>
        /// Set rotation for |page|.
        /// </summary>
        /// <param name="page">Handle to a page.</param>
        /// <param name="rotate">The rotation value, one of:
        /// 0 - No rotation.
        /// 1 - Rotated 90 degrees clockwise.
        /// 2 - Rotated 180 degrees clockwise.
        /// 3 - Rotated 270 degrees clockwise.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFPage_SetRotation(FPDF_PAGE page, int rotate);.
        /// </remarks>
        public void FPDFPage_SetRotation(FPDF_PAGE page, int rotate)
        {
            lock (_syncObject)
            {
                FPDFPage_SetRotationStatic(page, rotate);
            }
        }

        private static void LoadDllEditPart()
        {
            // fpdf_edit.h exports 103 functions
            FPDF_CreateNewDocumentStatic = GetPDFiumFunction<FPDF_CreateNewDocument_Delegate>(nameof(FPDF_CreateNewDocument));
            FPDFPage_NewStatic = GetPDFiumFunction<FPDFPage_New_Delegate>(nameof(FPDFPage_New));
            FPDFPage_DeleteStatic = GetPDFiumFunction<FPDFPage_Delete_Delegate>(nameof(FPDFPage_Delete));
            FPDFPage_GetRotationStatic = GetPDFiumFunction<FPDFPage_GetRotation_Delegate>(nameof(FPDFPage_GetRotation));
            FPDFPage_SetRotationStatic = GetPDFiumFunction<FPDFPage_SetRotation_Delegate>(nameof(FPDFPage_SetRotation));
        }
    }
}
