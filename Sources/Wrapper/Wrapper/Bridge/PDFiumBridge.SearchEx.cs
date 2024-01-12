namespace PDFiumDotNET.Wrapper.Bridge
{
    using System.Runtime.InteropServices;

    // Disable "Member 'member' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetCharIndexFromTextIndex_Delegate(FPDF_TEXTPAGE text_page, int nTextIndex);

        private static FPDFText_GetCharIndexFromTextIndex_Delegate FPDFText_GetCharIndexFromTextIndexStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetCharIndexFromTextIndex")]
        private static extern int FPDFText_GetCharIndexFromTextIndexStatic(FPDF_TEXTPAGE text_page, int nTextIndex);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get the character index in |text_page| internal character list.
        /// </summary>
        /// <param name="text_page">A text page information structure.</param>
        /// <param name="nTextIndex">Index of the text returned from FPDFText_GetText().</param>
        /// <returns>Returns the index of the character in internal character list. -1 for error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetCharIndexFromTextIndex(FPDF_TEXTPAGE text_page, int nTextIndex);.
        /// </remarks>
        public int FPDFText_GetCharIndexFromTextIndex(FPDF_TEXTPAGE text_page, int nTextIndex)
        {
            lock (_syncObject)
            {
                return FPDFText_GetCharIndexFromTextIndexStatic(text_page, nTextIndex);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetTextIndexFromCharIndex_Delegate(FPDF_TEXTPAGE text_page, int nCharIndex);

        private static FPDFText_GetTextIndexFromCharIndex_Delegate FPDFText_GetTextIndexFromCharIndexStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetTextIndexFromCharIndex")]
        private static extern int FPDFText_GetTextIndexFromCharIndexStatic(FPDF_TEXTPAGE text_page, int nCharIndex);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get the text index in |text_page| internal character list.
        /// </summary>
        /// <param name="text_page">A text page information structure.</param>
        /// <param name="nCharIndex">Index of the character in internal character list.</param>
        /// <returns>Returns the index of the text returned from FPDFText_GetText(). -1 for error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetTextIndexFromCharIndex(FPDF_TEXTPAGE text_page, int nCharIndex);.
        /// </remarks>
        public int FPDFText_GetTextIndexFromCharIndex(FPDF_TEXTPAGE text_page, int nCharIndex)
        {
            lock (_syncObject)
            {
                return FPDFText_GetTextIndexFromCharIndexStatic(text_page, nCharIndex);
            }
        }

        private static void LoadDllSearchExPart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            FPDFText_GetCharIndexFromTextIndexStatic = GetPDFiumFunction<FPDFText_GetCharIndexFromTextIndex_Delegate>(nameof(FPDFText_GetCharIndexFromTextIndex));
            FPDFText_GetTextIndexFromCharIndexStatic = GetPDFiumFunction<FPDFText_GetTextIndexFromCharIndex_Delegate>(nameof(FPDFText_GetTextIndexFromCharIndex));
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }

        private static void UnloadDllSearchExPart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            FPDFText_GetCharIndexFromTextIndexStatic = null;
            FPDFText_GetTextIndexFromCharIndexStatic = null;
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }
    }
}
