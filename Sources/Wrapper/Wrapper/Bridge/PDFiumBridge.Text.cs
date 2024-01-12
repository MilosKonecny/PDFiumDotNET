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
#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_TEXTPAGE FPDFText_LoadPage_Delegate(FPDF_PAGE page);

        private static FPDFText_LoadPage_Delegate FPDFText_LoadPageStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_LoadPage")]
        private static extern FPDF_TEXTPAGE FPDFText_LoadPageStatic(FPDF_PAGE page);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Prepare information about all characters in a page.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage function (in FPDFVIEW module).</param>
        /// <returns>A handle to the text page information structure. NULL if something goes wrong.</returns>
        /// <remarks>
        /// Application must call FPDFText_ClosePage to release the text page information.
        /// FPDF_EXPORT FPDF_TEXTPAGE FPDF_CALLCONV FPDFText_LoadPage(FPDF_PAGE page);.
        /// </remarks>
        public FPDF_TEXTPAGE FPDFText_LoadPage(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDFText_LoadPageStatic(page);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFText_ClosePage_Delegate(FPDF_TEXTPAGE text_page);

        private static FPDFText_ClosePage_Delegate FPDFText_ClosePageStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_ClosePage")]
        private static extern void FPDFText_ClosePageStatic(FPDF_TEXTPAGE text_page);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Release all resources allocated for a text page information structure.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFText_ClosePage(FPDF_TEXTPAGE text_page);.
        /// </remarks>
        public void FPDFText_ClosePage(FPDF_TEXTPAGE text_page)
        {
            lock (_syncObject)
            {
                FPDFText_ClosePageStatic(text_page);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_CountChars_Delegate(FPDF_TEXTPAGE text_page);

        private static FPDFText_CountChars_Delegate FPDFText_CountCharsStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_CountChars")]
        private static extern int FPDFText_CountCharsStatic(FPDF_TEXTPAGE text_page);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get number of characters in a page.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <returns>Number of characters in the page. Return -1 for error.
        /// Generated characters, like additional space characters, new line characters, are also counted.</returns>
        /// <remarks>
        /// Characters in a page form a "stream", inside the stream, each character has an index.
        /// We will use the index parameters in many of FPDFTEXT functions. The first character in the page has an index value of zero.
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_CountChars(FPDF_TEXTPAGE text_page);.
        /// </remarks>
        public int FPDFText_CountChars(FPDF_TEXTPAGE text_page)
        {
            lock (_syncObject)
            {
                return FPDFText_CountCharsStatic(text_page);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetUnicode_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_GetUnicode_Delegate FPDFText_GetUnicodeStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetUnicode")]
        private static extern int FPDFText_GetUnicodeStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get Unicode of a character in a page.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>The Unicode of the particular character.
        /// If a character is not encoded in Unicode and Foxit engine can't convert to Unicode, the return value will be zero.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned int FPDF_CALLCONV FPDFText_GetUnicode(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public int FPDFText_GetUnicode(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_GetUnicodeStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_IsGenerated_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_IsGenerated_Delegate FPDFText_IsGeneratedStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_IsGenerated")]
        private static extern int FPDFText_IsGeneratedStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get if a character in a page is generated by PDFium.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>1 if the character is generated by PDFium.
        /// 0 if the character is not generated by PDFium.
        /// -1 if there was an error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_IsGenerated(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public int FPDFText_IsGenerated(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_IsGeneratedStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_IsHyphen_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_IsHyphen_Delegate FPDFText_IsHyphenStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_IsHyphen")]
        private static extern int FPDFText_IsHyphenStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get if a character in a page is a hyphen.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by <see cref="FPDFText_LoadPage"/> function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>
        /// 1 if the character is a hyphen.
        /// 0 if the character is not a hyphen.
        /// -1 if there was an error.
        /// </returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_IsHyphen(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public int FPDFText_IsHyphen(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_IsHyphenStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_HasUnicodeMapError_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_HasUnicodeMapError_Delegate FPDFText_HasUnicodeMapErrorStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_HasUnicodeMapError")]
        private static extern int FPDFText_HasUnicodeMapErrorStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get if a character in a page has an invalid unicode mapping.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>1 if the character has an invalid unicode mapping.
        /// 0 if the character has no known unicode mapping issues.
        /// -1 if there was an error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_HasUnicodeMapError(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public int FPDFText_HasUnicodeMapError(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_HasUnicodeMapErrorStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate double FPDFText_GetFontSize_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_GetFontSize_Delegate FPDFText_GetFontSizeStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetFontSize")]
        private static extern double FPDFText_GetFontSizeStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get the font size of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>The font size of the particular character, measured in points (about 1/72 inch).
        /// This is the typographic size of the font (so called "em size").</returns>
        /// <remarks>
        /// FPDF_EXPORT double FPDF_CALLCONV FPDFText_GetFontSize(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public double FPDFText_GetFontSize(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_GetFontSizeStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFText_GetFontInfo_Delegate(FPDF_TEXTPAGE text_page, int index, IntPtr buffer, ulong buflen, ref int flags);

        private static FPDFText_GetFontInfo_Delegate FPDFText_GetFontInfoStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetFontInfo")]
        private static extern ulong FPDFText_GetFontInfoStatic(FPDF_TEXTPAGE text_page, int index, IntPtr buffer, ulong buflen, ref int flags);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get the font name and flags of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="buffer">A buffer receiving the font name.</param>
        /// <param name="buflen">The length of |buffer| in bytes.</param>
        /// <param name="flags">Optional pointer to an int receiving the font flags.
        /// These flags should be interpreted per PDF spec 1.7 Section 5.7.1 Font Descriptor Flags.</param>
        /// <returns>On success, return the length of the font name, including the trailing NUL character, in bytes.
        /// If this length is less than or equal to |length|, |buffer| is set to the font name, |flags| is set to the font flags.
        /// |buffer| is in UTF-8 encoding. Return 0 on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFText_GetFontInfo(FPDF_TEXTPAGE text_page, int index, void* buffer, unsigned long buflen, int* flags);.
        /// </remarks>
        public ulong FPDFText_GetFontInfo(FPDF_TEXTPAGE text_page, int index, IntPtr buffer, ulong buflen, ref int flags)
        {
            lock (_syncObject)
            {
                return FPDFText_GetFontInfoStatic(text_page, index, buffer, buflen, ref flags);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetFontWeight_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_GetFontWeight_Delegate FPDFText_GetFontWeightStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetFontWeight")]
        private static extern int FPDFText_GetFontWeightStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get the font weight of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>On success, return the font weight of the particular character.
        /// If |text_page| is invalid, if |index| is out of bounds, or if the character's text object is undefined, return -1.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetFontWeight(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public int FPDFText_GetFontWeight(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_GetFontWeightStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_TEXT_RENDERMODE FPDFText_GetTextRenderMode_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_GetTextRenderMode_Delegate FPDFText_GetTextRenderModeStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetTextRenderMode")]
        private static extern FPDF_TEXT_RENDERMODE FPDFText_GetTextRenderModeStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get text rendering mode of character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>On success, return the render mode value. A valid value is of type FPDF_TEXT_RENDERMODE.
        /// If |text_page| is invalid, if |index| is out of bounds, or if the text object is undefined, then return FPDF_TEXTRENDERMODE_UNKNOWN.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_TEXT_RENDERMODE FPDF_CALLCONV FPDFText_GetTextRenderMode(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public FPDF_TEXT_RENDERMODE FPDFText_GetTextRenderMode(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_GetTextRenderModeStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetFillColor_Delegate(FPDF_TEXTPAGE text_page, int index, ref uint r, ref uint g, ref uint b, ref uint a);

        private static FPDFText_GetFillColor_Delegate FPDFText_GetFillColorStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetFillColor")]
        private static extern bool FPDFText_GetFillColorStatic(FPDF_TEXTPAGE text_page, int index, ref uint r, ref uint g, ref uint b, ref uint a);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get the fill color of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="r">Pointer to an unsigned int number receiving the red value of the fill color.</param>
        /// <param name="g">Pointer to an unsigned int number receiving the green value of the fill color.</param>
        /// <param name="b">Pointer to an unsigned int number receiving the blue value of the fill color.</param>
        /// <param name="a">Pointer to an unsigned int number receiving the alpha value of the fill color.</param>
        /// <returns>Whether the call succeeded. If false, |R|, |G|, |B| and |A| are unchanged.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetFillColor(FPDF_TEXTPAGE text_page, int index, unsigned int* R, unsigned int* G, unsigned int* B, unsigned int* A);.
        /// </remarks>
        public bool FPDFText_GetFillColor(FPDF_TEXTPAGE text_page, int index, ref uint r, ref uint g, ref uint b, ref uint a)
        {
            lock (_syncObject)
            {
                return FPDFText_GetFillColorStatic(text_page, index, ref r, ref g, ref b, ref a);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetStrokeColor_Delegate(FPDF_TEXTPAGE text_page, int index, ref ulong r, ref ulong g, ref ulong b, ref ulong a);

        private static FPDFText_GetStrokeColor_Delegate FPDFText_GetStrokeColorStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetStrokeColor")]
        private static extern bool FPDFText_GetStrokeColorStatic(FPDF_TEXTPAGE text_page, int index, ref ulong r, ref ulong g, ref ulong b, ref ulong a);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get the stroke color of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="r">Pointer to an unsigned int number receiving the red value of the stroke color.</param>
        /// <param name="g">Pointer to an unsigned int number receiving the green value of the stroke color.</param>
        /// <param name="b">Pointer to an unsigned int number receiving the blue value of the stroke color.</param>
        /// <param name="a">Pointer to an unsigned int number receiving the alpha value of the stroke color.</param>
        /// <returns>Whether the call succeeded. If false, |R|, |G|, |B| and |A| are unchanged.</returns>
        /// <remarks>FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetStrokeColor(FPDF_TEXTPAGE text_page, int index, unsigned int* R, unsigned int* G, unsigned int* B, unsigned int* A);.</remarks>
        public bool FPDFText_GetStrokeColor(FPDF_TEXTPAGE text_page, int index, ref ulong r, ref ulong g, ref ulong b, ref ulong a)
        {
            lock (_syncObject)
            {
                return FPDFText_GetStrokeColorStatic(text_page, index, ref r, ref g, ref b, ref a);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float FPDFText_GetCharAngle_Delegate(FPDF_TEXTPAGE text_page, int index);

        private static FPDFText_GetCharAngle_Delegate FPDFText_GetCharAngleStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetCharAngle")]
        private static extern float FPDFText_GetCharAngleStatic(FPDF_TEXTPAGE text_page, int index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get character rotation angle.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <returns>On success, return the angle value in radian. Value will always be greater or equal to 0.
        /// If |text_page| is invalid, or if |index| is out of bounds, then return -1.</returns>
        /// <remarks>
        /// FPDF_EXPORT float FPDF_CALLCONV FPDFText_GetCharAngle(FPDF_TEXTPAGE text_page, int index);.
        /// </remarks>
        public float FPDFText_GetCharAngle(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncObject)
            {
                return FPDFText_GetCharAngleStatic(text_page, index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetCharBox_Delegate(FPDF_TEXTPAGE text_page, int index, ref double left, ref double right, ref double bottom, ref double top);

        private static FPDFText_GetCharBox_Delegate FPDFText_GetCharBoxStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetCharBox")]
        private static extern bool FPDFText_GetCharBoxStatic(FPDF_TEXTPAGE text_page, int index, ref double left, ref double right, ref double bottom, ref double top);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get bounding box of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="left">Pointer to a double number receiving left position of the character box.</param>
        /// <param name="right">Pointer to a double number receiving right position of the character box.</param>
        /// <param name="bottom">Pointer to a double number receiving bottom position of the character box.</param>
        /// <param name="top">Pointer to a double number receiving top position of the character box.</param>
        /// <returns>On success, return TRUE and fill in |left|, |right|, |bottom|, and |top|.
        /// If |text_page| is invalid, or if |index| is out of bounds, then return FALSE, and the out parameters remain unmodified.</returns>
        /// <remarks>
        /// All positions are measured in PDF "user space".
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetCharBox(FPDF_TEXTPAGE text_page, int index, double* left, double* right, double* bottom, double* top);.
        /// </remarks>
        public bool FPDFText_GetCharBox(FPDF_TEXTPAGE text_page, int index, ref double left, ref double right, ref double bottom, ref double top)
        {
            lock (_syncObject)
            {
                return FPDFText_GetCharBoxStatic(text_page, index, ref left, ref right, ref bottom, ref top);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetLooseCharBox_Delegate(FPDF_TEXTPAGE text_page, int index, ref FS_RECTF rect);

        private static FPDFText_GetLooseCharBox_Delegate FPDFText_GetLooseCharBoxStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetLooseCharBox")]
        private static extern bool FPDFText_GetLooseCharBoxStatic(FPDF_TEXTPAGE text_page, int index, ref FS_RECTF rect);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get a "loose" bounding box of a particular character, i.e.,
        /// covering the entire glyph bounds, without taking the actual glyph shape into account.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="rect">Pointer to a FS_RECTF receiving the character box.</param>
        /// <returns>On success, return TRUE and fill in |rect|. If |text_page| is invalid, or if |index| is out of bounds,
        /// then return FALSE, and the |rect| out parameter remains unmodified.</returns>
        /// <remarks>
        /// All positions are measured in PDF "user space".
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetLooseCharBox(FPDF_TEXTPAGE text_page, int index, FS_RECTF* rect);.
        /// </remarks>
        public bool FPDFText_GetLooseCharBox(FPDF_TEXTPAGE text_page, int index, ref FS_RECTF rect)
        {
            lock (_syncObject)
            {
                return FPDFText_GetLooseCharBoxStatic(text_page, index, ref rect);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetMatrix_Delegate(FPDF_TEXTPAGE text_page, int index, ref FS_MATRIX matrix);

        private static FPDFText_GetMatrix_Delegate FPDFText_GetMatrixStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetMatrix")]
        private static extern bool FPDFText_GetMatrixStatic(FPDF_TEXTPAGE text_page, int index, ref FS_MATRIX matrix);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Get the effective transformation matrix for a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage().</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="matrix">Pointer to a FS_MATRIX receiving the transformation matrix.</param>
        /// <returns>On success, return TRUE and fill in |matrix|. If |text_page| is invalid, or if |index| is out of bounds,
        /// or if |matrix| is NULL, then return FALSE, and |matrix| remains unmodified.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetMatrix(FPDF_TEXTPAGE text_page, int index, FS_MATRIX* matrix);.
        /// </remarks>
        public bool FPDFText_GetMatrix(FPDF_TEXTPAGE text_page, int index, ref FS_MATRIX matrix)
        {
            lock (_syncObject)
            {
                return FPDFText_GetMatrixStatic(text_page, index, ref matrix);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetCharOrigin_Delegate(FPDF_TEXTPAGE text_page, int index, ref double x, ref double y);

        private static FPDFText_GetCharOrigin_Delegate FPDFText_GetCharOriginStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetCharOrigin")]
        private static extern bool FPDFText_GetCharOriginStatic(FPDF_TEXTPAGE text_page, int index, ref double x, ref double y);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get origin of a particular character.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="index">Zero-based index of the character.</param>
        /// <param name="x">Pointer to a double number receiving x coordinate of the character origin.</param>
        /// <param name="y">Pointer to a double number receiving y coordinate of the character origin.</param>
        /// <returns>Whether the call succeeded. If false, x and y are unchanged.</returns>
        /// <remarks>
        /// All positions are measured in PDF "user space".
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetCharOrigin(FPDF_TEXTPAGE text_page, int index, double* x, double* y);.
        /// </remarks>
        public bool FPDFText_GetCharOrigin(FPDF_TEXTPAGE text_page, int index, ref double x, ref double y)
        {
            lock (_syncObject)
            {
                return FPDFText_GetCharOriginStatic(text_page, index, ref x, ref y);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetCharIndexAtPos_Delegate(FPDF_TEXTPAGE text_page, double x, double y, double xTolerance, double yTolerance);

        private static FPDFText_GetCharIndexAtPos_Delegate FPDFText_GetCharIndexAtPosStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetCharIndexAtPos")]
        private static extern int FPDFText_GetCharIndexAtPosStatic(FPDF_TEXTPAGE text_page, double x, double y, double xTolerance, double yTolerance);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get the index of a character at or nearby a certain position on the page.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="x">X position in PDF "user space".</param>
        /// <param name="y">Y position in PDF "user space".</param>
        /// <param name="xTolerance">An x-axis tolerance value for character hit detection, in point units.</param>
        /// <param name="yTolerance">A y-axis tolerance value for character hit detection, in point units.</param>
        /// <returns>The zero-based index of the character at, or nearby the point (x,y).
        /// If there is no character at or nearby the point, return value will be -1. If an error occurs, -3 will be returned.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetCharIndexAtPos(FPDF_TEXTPAGE text_page, double x, double y, double xTolerance, double yTolerance);.
        /// </remarks>
        public int FPDFText_GetCharIndexAtPos(FPDF_TEXTPAGE text_page, double x, double y, double xTolerance, double yTolerance)
        {
            lock (_syncObject)
            {
                return FPDFText_GetCharIndexAtPosStatic(text_page, x, y, xTolerance, yTolerance);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetText_Delegate(FPDF_TEXTPAGE text_page, int start_index, int count, IntPtr result);

        private static FPDFText_GetText_Delegate FPDFText_GetTextStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetText")]
        private static extern int FPDFText_GetTextStatic(FPDF_TEXTPAGE text_page, int start_index, int count, IntPtr result);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Extract unicode text string from the page.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="start_index">Index for the start characters.</param>
        /// <param name="count">Number of characters to be extracted.</param>
        /// <param name="result">A buffer (allocated by application) receiving the extracted unicodes.
        /// The size of the buffer must be able to hold the number of characters plus a terminator.</param>
        /// <returns>Number of characters written into the result buffer, including the trailing terminator.</returns>
        /// <remarks>
        /// This function ignores characters without unicode information.
        /// It returns all characters on the page, even those that are not visible when the page has a cropbox.
        /// To filter out the characters outside of the cropbox, use FPDF_GetPageBoundingBox() and FPDFText_GetCharBox().
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetText(FPDF_TEXTPAGE text_page, int start_index, int count, unsigned short* result);.
        /// </remarks>
        public int FPDFText_GetText(FPDF_TEXTPAGE text_page, int start_index, int count, IntPtr result)
        {
            lock (_syncObject)
            {
                return FPDFText_GetTextStatic(text_page, start_index, count, result);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_CountRects_Delegate(FPDF_TEXTPAGE text_page, int start_index, int count);

        private static FPDFText_CountRects_Delegate FPDFText_CountRectsStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_CountRects")]
        private static extern int FPDFText_CountRectsStatic(FPDF_TEXTPAGE text_page, int start_index, int count);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Counts number of rectangular areas occupied by a segment of text, and caches the result for subsequent FPDFText_GetRect() calls.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="start_index">Index for the start character.</param>
        /// <param name="count">Number of characters, or -1 for all remaining.</param>
        /// <returns>Number of rectangles, 0 if text_page is null, or -1 on bad start_index.</returns>
        /// <remarks>
        /// This function, along with FPDFText_GetRect can be used by applications to detect the position on the page for a text segment,
        /// so proper areas can be highlighted. The FPDFText_* functions will automatically merge small character boxes into bigger one if those
        /// characters are on the same line and use same font settings.
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_CountRects(FPDF_TEXTPAGE text_page, int start_index, int count);.
        /// </remarks>
        public int FPDFText_CountRects(FPDF_TEXTPAGE text_page, int start_index, int count)
        {
            lock (_syncObject)
            {
                return FPDFText_CountRectsStatic(text_page, start_index, count);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_GetRect_Delegate(FPDF_TEXTPAGE text_page, int rect_index, ref double left, ref double top, ref double right, ref double bottom);

        private static FPDFText_GetRect_Delegate FPDFText_GetRectStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetRect")]
        private static extern bool FPDFText_GetRectStatic(FPDF_TEXTPAGE text_page, int rect_index, ref double left, ref double top, ref double right, ref double bottom);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get a rectangular area from the result generated by FPDFText_CountRects.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="rect_index">Zero-based index for the rectangle.</param>
        /// <param name="left">Pointer to a double value receiving the rectangle left boundary.</param>
        /// <param name="top">Pointer to a double value receiving the rectangle top boundary.</param>
        /// <param name="right">Pointer to a double value receiving the rectangle right boundary.</param>
        /// <param name="bottom">Pointer to a double value receiving the rectangle bottom boundary.</param>
        /// <returns>On success, return TRUE and fill in |left|, |top|, |right|, and |bottom|.
        /// If |text_page| is invalid then return FALSE, and the out parameters remain unmodified.
        /// If |text_page| is valid but |rect_index| is out of bounds, then return FALSE and set the out parameters to 0.</returns>
        /// <remarks>FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_GetRect(FPDF_TEXTPAGE text_page, int rect_index, double* left, double* top, double* right, double* bottom);.
        /// </remarks>
        public bool FPDFText_GetRect(FPDF_TEXTPAGE text_page, int rect_index, ref double left, ref double top, ref double right, ref double bottom)
        {
            lock (_syncObject)
            {
                return FPDFText_GetRectStatic(text_page, rect_index, ref left, ref top, ref right, ref bottom);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetBoundedText_Delegate(FPDF_TEXTPAGE text_page, double left, double top, double right, double bottom, IntPtr buffer, int buflen);

        private static FPDFText_GetBoundedText_Delegate FPDFText_GetBoundedTextStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetBoundedText")]
        private static extern int FPDFText_GetBoundedTextStatic(FPDF_TEXTPAGE text_page, double left, double top, double right, double bottom, IntPtr buffer, int buflen);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Extract unicode text within a rectangular boundary on the page.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="left">Left boundary.</param>
        /// <param name="top">Top boundary.</param>
        /// <param name="right">Right boundary.</param>
        /// <param name="bottom">Bottom boundary.</param>
        /// <param name="buffer">A unicode buffer.</param>
        /// <param name="buflen">Number of characters (not bytes) for the buffer, excluding an additional terminator.</param>
        /// <returns>If buffer is NULL or buflen is zero, return number of characters (not bytes) of text present within the rectangle,
        /// excluding a terminating NUL.
        /// Generally you should pass a buffer at least one larger than this if you want a terminating NUL,
        /// which will be provided if space is available. Otherwise, return number of characters copied into the buffer,
        /// including the terminating NUL when space for it is available.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetBoundedText(FPDF_TEXTPAGE text_page, double left, double top, double right, double bottom, unsigned short* buffer, int buflen);.
        /// </remarks>
        public int FPDFText_GetBoundedText(FPDF_TEXTPAGE text_page, double left, double top, double right, double bottom, IntPtr buffer, int buflen)
        {
            lock (_syncObject)
            {
                return FPDFText_GetBoundedTextStatic(text_page, left, top, right, bottom, buffer, buflen);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_SCHHANDLE FPDFText_FindStart_Delegate(FPDF_TEXTPAGE text_page, IntPtr findwhat, FPDF_FIND_FLAGS flags, int start_index);

        private static FPDFText_FindStart_Delegate FPDFText_FindStartStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_FindStart")]
        private static extern FPDF_SCHHANDLE FPDFText_FindStartStatic(FPDF_TEXTPAGE text_page, IntPtr findwhat, FPDF_FIND_FLAGS flags, int start_index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Start a search.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="findwhat">A unicode match pattern.</param>
        /// <param name="flags">Option flags.</param>
        /// <param name="start_index">Start from this character. -1 for end of the page.</param>
        /// <returns>A handle for the search context. FPDFText_FindClose must be called to release this handle.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_SCHHANDLE FPDF_CALLCONV FPDFText_FindStart(FPDF_TEXTPAGE text_page, FPDF_WIDESTRING findwhat, unsigned long flags, int start_index);.
        /// </remarks>
        public FPDF_SCHHANDLE FPDFText_FindStart(FPDF_TEXTPAGE text_page, IntPtr findwhat, FPDF_FIND_FLAGS flags, int start_index)
        {
            lock (_syncObject)
            {
                return FPDFText_FindStartStatic(text_page, findwhat, flags, start_index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_FindNext_Delegate(FPDF_SCHHANDLE handle);

        private static FPDFText_FindNext_Delegate FPDFText_FindNextStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_FindNext")]
        private static extern bool FPDFText_FindNextStatic(FPDF_SCHHANDLE handle);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Search in the direction from page start to end.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Whether a match is found.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_FindNext(FPDF_SCHHANDLE handle);.
        /// </remarks>
        public bool FPDFText_FindNext(FPDF_SCHHANDLE handle)
        {
            lock (_syncObject)
            {
                return FPDFText_FindNextStatic(handle);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFText_FindPrev_Delegate(FPDF_SCHHANDLE handle);

        private static FPDFText_FindPrev_Delegate FPDFText_FindPrevStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_FindPrev")]
        private static extern bool FPDFText_FindPrevStatic(FPDF_SCHHANDLE handle);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Search in the direction from page end to start.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Whether a match is found.</returns>
        /// <remarks>FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_FindPrev(FPDF_SCHHANDLE handle);.
        /// </remarks>
        public bool FPDFText_FindPrev(FPDF_SCHHANDLE handle)
        {
            lock (_syncObject)
            {
                return FPDFText_FindPrevStatic(handle);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetSchResultIndex_Delegate(FPDF_SCHHANDLE handle);

        private static FPDFText_GetSchResultIndex_Delegate FPDFText_GetSchResultIndexStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetSchResultIndex")]
        private static extern int FPDFText_GetSchResultIndexStatic(FPDF_SCHHANDLE handle);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get the starting character index of the search result.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Index for the starting character.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetSchResultIndex(FPDF_SCHHANDLE handle);.
        /// </remarks>
        public int FPDFText_GetSchResultIndex(FPDF_SCHHANDLE handle)
        {
            lock (_syncObject)
            {
                return FPDFText_GetSchResultIndexStatic(handle);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFText_GetSchCount_Delegate(FPDF_SCHHANDLE handle);

        private static FPDFText_GetSchCount_Delegate FPDFText_GetSchCountStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_GetSchCount")]
        private static extern int FPDFText_GetSchCountStatic(FPDF_SCHHANDLE handle);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Get the number of matched characters in the search result.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Number of matched characters.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetSchCount(FPDF_SCHHANDLE handle);.
        /// </remarks>
        public int FPDFText_GetSchCount(FPDF_SCHHANDLE handle)
        {
            lock (_syncObject)
            {
                return FPDFText_GetSchCountStatic(handle);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFText_FindClose_Delegate(FPDF_SCHHANDLE handle);

        private static FPDFText_FindClose_Delegate FPDFText_FindCloseStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFText_FindClose")]
        private static extern void FPDFText_FindCloseStatic(FPDF_SCHHANDLE handle);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Release a search context.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFText_FindClose(FPDF_SCHHANDLE handle);.
        /// </remarks>
        public void FPDFText_FindClose(FPDF_SCHHANDLE handle)
        {
            lock (_syncObject)
            {
                FPDFText_FindCloseStatic(handle);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_PAGELINK FPDFLink_LoadWebLinks_Delegate(FPDF_TEXTPAGE text_page);

        private static FPDFLink_LoadWebLinks_Delegate FPDFLink_LoadWebLinksStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_LoadWebLinks")]
        private static extern FPDF_PAGELINK FPDFLink_LoadWebLinksStatic(FPDF_TEXTPAGE text_page);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Prepare information about weblinks in a page.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <returns>A handle to the page's links information structure, or NULL if something goes wrong.</returns>
        /// <remarks>
        /// FPDFLink_CloseWebLinks must be called to release resources.
        /// Weblinks are those links implicitly embedded in PDF pages. PDF also has a type of annotation called "link"
        /// (FPDFTEXT doesn't deal with that kind of link).
        /// FPDFTEXT weblink feature is useful for automatically detecting links in the page contents.
        /// For example, things like "https://www.example.com" will be detected, so applications can allow user to click on those characters
        /// to activate the link, even the PDF doesn't come with link annotations.
        /// FPDF_EXPORT FPDF_PAGELINK FPDF_CALLCONV FPDFLink_LoadWebLinks(FPDF_TEXTPAGE text_page);.
        /// </remarks>
        public FPDF_PAGELINK FPDFLink_LoadWebLinks(FPDF_TEXTPAGE text_page)
        {
            lock (_syncObject)
            {
                return FPDFLink_LoadWebLinksStatic(text_page);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFLink_CountWebLinks_Delegate(FPDF_PAGELINK link_page);

        private static FPDFLink_CountWebLinks_Delegate FPDFLink_CountWebLinksStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_CountWebLinks")]
        private static extern int FPDFLink_CountWebLinksStatic(FPDF_PAGELINK link_page);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Count number of detected web links.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <returns>Number of detected web links.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFLink_CountWebLinks(FPDF_PAGELINK link_page);.
        /// </remarks>
        public int FPDFLink_CountWebLinks(FPDF_PAGELINK link_page)
        {
            lock (_syncObject)
            {
                return FPDFLink_CountWebLinksStatic(link_page);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFLink_GetURL_Delegate(FPDF_PAGELINK link_page, int link_index, IntPtr buffer, int buflen);

        private static FPDFLink_GetURL_Delegate FPDFLink_GetURLStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_GetURL")]
        private static extern int FPDFLink_GetURLStatic(FPDF_PAGELINK link_page, int link_index, IntPtr buffer, int buflen);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Fetch the URL information for a detected web link.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <param name="link_index">Zero-based index for the link.</param>
        /// <param name="buffer">A unicode buffer for the result.</param>
        /// <param name="buflen">Number of 16-bit code units (not bytes) for the buffer, including an additional terminator.</param>
        /// <returns>If |buffer| is NULL or |buflen| is zero, return the number of 16-bit code units (not bytes) needed to buffer the result
        /// (an additional terminator is included in this count).
        /// Otherwise, copy the result into |buffer|, truncating at |buflen| if the result is too large to fit,
        /// and return the number of 16-bit code units actually copied into the buffer
        /// (the additional terminator is also included in this count).
        /// If |link_index| does not correspond to a valid link, then the result is an empty string.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFLink_GetURL(FPDF_PAGELINK link_page, int link_index, unsigned short* buffer, int buflen);.
        /// </remarks>
        public int FPDFLink_GetURL(FPDF_PAGELINK link_page, int link_index, IntPtr buffer, int buflen)
        {
            lock (_syncObject)
            {
                return FPDFLink_GetURLStatic(link_page, link_index, buffer, buflen);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFLink_CountRects_Delegate(FPDF_PAGELINK link_page, int link_index);

        private static FPDFLink_CountRects_Delegate FPDFLink_CountRectsStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_CountRects")]
        private static extern int FPDFLink_CountRectsStatic(FPDF_PAGELINK link_page, int link_index);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Count number of rectangular areas for the link.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <param name="link_index">Zero-based index for the link.</param>
        /// <returns>Number of rectangular areas for the link.  If |link_index| does not correspond to a valid link, then 0 is returned.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFLink_CountRects(FPDF_PAGELINK link_page, int link_index);.
        /// </remarks>
        public int FPDFLink_CountRects(FPDF_PAGELINK link_page, int link_index)
        {
            lock (_syncObject)
            {
                return FPDFLink_CountRectsStatic(link_page, link_index);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFLink_GetRect_Delegate(FPDF_PAGELINK link_page, int link_index, int rect_index, ref double left, ref double top, ref double right, ref double bottom);

        private static FPDFLink_GetRect_Delegate FPDFLink_GetRectStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_GetRect")]
        private static extern bool FPDFLink_GetRectStatic(FPDF_PAGELINK link_page, int link_index, int rect_index, ref double left, ref double top, ref double right, ref double bottom);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Fetch the boundaries of a rectangle for a link.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <param name="link_index">Zero-based index for the link.</param>
        /// <param name="rect_index">Zero-based index for a rectangle.</param>
        /// <param name="left">Pointer to a double value receiving the rectangle left boundary.</param>
        /// <param name="top">Pointer to a double value receiving the rectangle top boundary.</param>
        /// <param name="right">Pointer to a double value receiving the rectangle right boundary.</param>
        /// <param name="bottom">Pointer to a double value receiving the rectangle bottom boundary.</param>
        /// <returns>On success, return TRUE and fill in |left|, |top|, |right|, and |bottom|.
        /// If |link_page| is invalid or if |link_index| does not correspond to a valid link, then return FALSE,
        /// and the out parameters remain unmodified.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFLink_GetRect(FPDF_PAGELINK link_page, int link_index, int rect_index, double* left, double* top, double* right, double* bottom);.
        /// </remarks>
        public bool FPDFLink_GetRect(FPDF_PAGELINK link_page, int link_index, int rect_index, ref double left, ref double top, ref double right, ref double bottom)
        {
            lock (_syncObject)
            {
                return FPDFLink_GetRectStatic(link_page, link_index, rect_index, ref left, ref top, ref right, ref bottom);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFLink_GetTextRange_Delegate(FPDF_PAGELINK link_page, int link_index, ref int start_char_index, ref int char_count);

        private static FPDFLink_GetTextRange_Delegate FPDFLink_GetTextRangeStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_GetTextRange")]
        private static extern bool FPDFLink_GetTextRangeStatic(FPDF_PAGELINK link_page, int link_index, ref int start_char_index, ref int char_count);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Experimental API. Fetch the start char index and char count for a link.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <param name="link_index">Zero-based index for the link.</param>
        /// <param name="start_char_index">Pointer to int receiving the start char index.</param>
        /// <param name="char_count">Pointer to int receiving the char count.</param>
        /// <returns>On success, return TRUE and fill in |start_char_index| and |char_count|.
        /// if |link_page| is invalid or if |link_index| does not correspond to a valid link,
        /// then return FALSE and the out parameters remain unmodified.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFLink_GetTextRange(FPDF_PAGELINK link_page, int link_index, int* start_char_index, int* char_count);.
        /// </remarks>
        public bool FPDFLink_GetTextRange(FPDF_PAGELINK link_page, int link_index, ref int start_char_index, ref int char_count)
        {
            lock (_syncObject)
            {
                return FPDFLink_GetTextRangeStatic(link_page, link_index, ref start_char_index, ref char_count);
            }
        }

#if USE_DYNAMICALLY_LOADED_PDFIUM
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFLink_CloseWebLinks_Delegate(FPDF_PAGELINK link_page);

        private static FPDFLink_CloseWebLinks_Delegate FPDFLink_CloseWebLinksStatic { get; set; }
#else // USE_DYNAMICALLY_LOADED_PDFIUM
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [DllImport("pdfium.dll", EntryPoint = "FPDFLink_CloseWebLinks")]
        private static extern void FPDFLink_CloseWebLinksStatic(FPDF_PAGELINK link_page);
#endif // USE_DYNAMICALLY_LOADED_PDFIUM

        /// <summary>
        /// Release resources used by weblink feature.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFLink_CloseWebLinks(FPDF_PAGELINK link_page);.
        /// </remarks>
        public void FPDFLink_CloseWebLinks(FPDF_PAGELINK link_page)
        {
            lock (_syncObject)
            {
                FPDFLink_CloseWebLinksStatic(link_page);
            }
        }

        private static void LoadDllTextPart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            FPDFText_LoadPageStatic = GetPDFiumFunction<FPDFText_LoadPage_Delegate>(nameof(FPDFText_LoadPage));
            FPDFText_ClosePageStatic = GetPDFiumFunction<FPDFText_ClosePage_Delegate>(nameof(FPDFText_ClosePage));
            FPDFText_CountCharsStatic = GetPDFiumFunction<FPDFText_CountChars_Delegate>(nameof(FPDFText_CountChars));
            FPDFText_GetUnicodeStatic = GetPDFiumFunction<FPDFText_GetUnicode_Delegate>(nameof(FPDFText_GetUnicode));
            FPDFText_IsGeneratedStatic = GetPDFiumFunction<FPDFText_IsGenerated_Delegate>(nameof(FPDFText_IsGenerated));
            FPDFText_IsHyphenStatic = GetPDFiumFunction<FPDFText_IsHyphen_Delegate>(nameof(FPDFText_IsHyphen));
            FPDFText_HasUnicodeMapErrorStatic = GetPDFiumFunction<FPDFText_HasUnicodeMapError_Delegate>(nameof(FPDFText_HasUnicodeMapError));
            FPDFText_GetFontSizeStatic = GetPDFiumFunction<FPDFText_GetFontSize_Delegate>(nameof(FPDFText_GetFontSize));
            FPDFText_GetFontInfoStatic = GetPDFiumFunction<FPDFText_GetFontInfo_Delegate>(nameof(FPDFText_GetFontInfo));
            FPDFText_GetFontWeightStatic = GetPDFiumFunction<FPDFText_GetFontWeight_Delegate>(nameof(FPDFText_GetFontWeight));
            FPDFText_GetTextRenderModeStatic = GetPDFiumFunction<FPDFText_GetTextRenderMode_Delegate>(nameof(FPDFText_GetTextRenderMode));
            FPDFText_GetFillColorStatic = GetPDFiumFunction<FPDFText_GetFillColor_Delegate>(nameof(FPDFText_GetFillColor));
            FPDFText_GetStrokeColorStatic = GetPDFiumFunction<FPDFText_GetStrokeColor_Delegate>(nameof(FPDFText_GetStrokeColor));
            FPDFText_GetCharAngleStatic = GetPDFiumFunction<FPDFText_GetCharAngle_Delegate>(nameof(FPDFText_GetCharAngle));
            FPDFText_GetCharBoxStatic = GetPDFiumFunction<FPDFText_GetCharBox_Delegate>(nameof(FPDFText_GetCharBox));
            FPDFText_GetLooseCharBoxStatic = GetPDFiumFunction<FPDFText_GetLooseCharBox_Delegate>(nameof(FPDFText_GetLooseCharBox));
            FPDFText_GetMatrixStatic = GetPDFiumFunction<FPDFText_GetMatrix_Delegate>(nameof(FPDFText_GetMatrix));
            FPDFText_GetCharOriginStatic = GetPDFiumFunction<FPDFText_GetCharOrigin_Delegate>(nameof(FPDFText_GetCharOrigin));
            FPDFText_GetCharIndexAtPosStatic = GetPDFiumFunction<FPDFText_GetCharIndexAtPos_Delegate>(nameof(FPDFText_GetCharIndexAtPos));
            FPDFText_GetTextStatic = GetPDFiumFunction<FPDFText_GetText_Delegate>(nameof(FPDFText_GetText));
            FPDFText_CountRectsStatic = GetPDFiumFunction<FPDFText_CountRects_Delegate>(nameof(FPDFText_CountRects));
            FPDFText_GetRectStatic = GetPDFiumFunction<FPDFText_GetRect_Delegate>(nameof(FPDFText_GetRect));
            FPDFText_GetBoundedTextStatic = GetPDFiumFunction<FPDFText_GetBoundedText_Delegate>(nameof(FPDFText_GetBoundedText));
            FPDFText_FindStartStatic = GetPDFiumFunction<FPDFText_FindStart_Delegate>(nameof(FPDFText_FindStart));
            FPDFText_FindNextStatic = GetPDFiumFunction<FPDFText_FindNext_Delegate>(nameof(FPDFText_FindNext));
            FPDFText_FindPrevStatic = GetPDFiumFunction<FPDFText_FindPrev_Delegate>(nameof(FPDFText_FindPrev));
            FPDFText_GetSchResultIndexStatic = GetPDFiumFunction<FPDFText_GetSchResultIndex_Delegate>(nameof(FPDFText_GetSchResultIndex));
            FPDFText_GetSchCountStatic = GetPDFiumFunction<FPDFText_GetSchCount_Delegate>(nameof(FPDFText_GetSchCount));
            FPDFText_FindCloseStatic = GetPDFiumFunction<FPDFText_FindClose_Delegate>(nameof(FPDFText_FindClose));
            FPDFLink_LoadWebLinksStatic = GetPDFiumFunction<FPDFLink_LoadWebLinks_Delegate>(nameof(FPDFLink_LoadWebLinks));
            FPDFLink_CountWebLinksStatic = GetPDFiumFunction<FPDFLink_CountWebLinks_Delegate>(nameof(FPDFLink_CountWebLinks));
            FPDFLink_GetURLStatic = GetPDFiumFunction<FPDFLink_GetURL_Delegate>(nameof(FPDFLink_GetURL));
            FPDFLink_CountRectsStatic = GetPDFiumFunction<FPDFLink_CountRects_Delegate>(nameof(FPDFLink_CountRects));
            FPDFLink_GetRectStatic = GetPDFiumFunction<FPDFLink_GetRect_Delegate>(nameof(FPDFLink_GetRect));
            FPDFLink_GetTextRangeStatic = GetPDFiumFunction<FPDFLink_GetTextRange_Delegate>(nameof(FPDFLink_GetTextRange));
            FPDFLink_CloseWebLinksStatic = GetPDFiumFunction<FPDFLink_CloseWebLinks_Delegate>(nameof(FPDFLink_CloseWebLinks));
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }

        private static void UnloadDllTextPart()
        {
#if USE_DYNAMICALLY_LOADED_PDFIUM
            FPDFText_LoadPageStatic = null;
            FPDFText_ClosePageStatic = null;
            FPDFText_CountCharsStatic = null;
            FPDFText_GetUnicodeStatic = null;
            FPDFText_IsGeneratedStatic = null;
            FPDFText_IsHyphenStatic = null;
            FPDFText_HasUnicodeMapErrorStatic = null;
            FPDFText_GetFontSizeStatic = null;
            FPDFText_GetFontInfoStatic = null;
            FPDFText_GetFontWeightStatic = null;
            FPDFText_GetTextRenderModeStatic = null;
            FPDFText_GetFillColorStatic = null;
            FPDFText_GetStrokeColorStatic = null;
            FPDFText_GetCharAngleStatic = null;
            FPDFText_GetCharBoxStatic = null;
            FPDFText_GetLooseCharBoxStatic = null;
            FPDFText_GetMatrixStatic = null;
            FPDFText_GetCharOriginStatic = null;
            FPDFText_GetCharIndexAtPosStatic = null;
            FPDFText_GetTextStatic = null;
            FPDFText_CountRectsStatic = null;
            FPDFText_GetRectStatic = null;
            FPDFText_GetBoundedTextStatic = null;
            FPDFText_FindStartStatic = null;
            FPDFText_FindNextStatic = null;
            FPDFText_FindPrevStatic = null;
            FPDFText_GetSchResultIndexStatic = null;
            FPDFText_GetSchCountStatic = null;
            FPDFText_FindCloseStatic = null;
            FPDFLink_LoadWebLinksStatic = null;
            FPDFLink_CountWebLinksStatic = null;
            FPDFLink_GetURLStatic = null;
            FPDFLink_CountRectsStatic = null;
            FPDFLink_GetRectStatic = null;
            FPDFLink_GetTextRangeStatic = null;
            FPDFLink_CloseWebLinksStatic = null;
#endif // USE_DYNAMICALLY_LOADED_PDFIUM
        }
    }
}
