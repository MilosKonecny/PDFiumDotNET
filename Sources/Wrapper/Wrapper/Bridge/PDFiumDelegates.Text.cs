namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The class contains all delegates of methods in pdfium dll.
    /// </summary>
    internal partial class PDFiumDelegates
    {
        /// <summary>
        /// Prepare information about all characters in a page.
        /// </summary>
        /// <param name="page">Handle to the page. Returned by FPDF_LoadPage function (in FPDFVIEW module).</param>
        /// <returns>A handle to the text page information structure. NULL if something goes wrong.</returns>
        /// <remarks>
        /// Application must call FPDFText_ClosePage to release the text page information.
        /// FPDF_EXPORT FPDF_TEXTPAGE FPDF_CALLCONV FPDFText_LoadPage(FPDF_PAGE page);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate FPDF_TEXTPAGE FPDFText_LoadPage(FPDF_PAGE page);

        /// <summary>
        /// Release all resources allocated for a text page information structure.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFText_ClosePage(FPDF_TEXTPAGE text_page);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FPDFText_ClosePage(FPDF_TEXTPAGE text_page);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_CountChars(FPDF_TEXTPAGE text_page);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetUnicode(FPDF_TEXTPAGE text_page, int index);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double FPDFText_GetFontSize(FPDF_TEXTPAGE text_page, int index);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate ulong FPDFText_GetFontInfo(FPDF_TEXTPAGE text_page, int index, IntPtr buffer, ulong buflen, ref int flags);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetFontWeight(FPDF_TEXTPAGE text_page, int index);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate FPDF_TEXT_RENDERMODE FPDFText_GetTextRenderMode(FPDF_TEXTPAGE text_page, int index);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetFillColor(FPDF_TEXTPAGE text_page, int index, ref uint r, ref uint g, ref uint b, ref uint a);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetStrokeColor(FPDF_TEXTPAGE text_page, int index, ref ulong r, ref ulong g, ref ulong b, ref ulong a);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate float FPDFText_GetCharAngle(FPDF_TEXTPAGE text_page, int index);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetCharBox(FPDF_TEXTPAGE text_page, int index, ref double left, ref double right, ref double bottom, ref double top);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetLooseCharBox(FPDF_TEXTPAGE text_page, int index, ref FS_RECTF rect);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetMatrix(FPDF_TEXTPAGE text_page, int index, ref FS_MATRIX matrix);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetCharOrigin(FPDF_TEXTPAGE text_page, int index, ref double x, ref double y);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetCharIndexAtPos(FPDF_TEXTPAGE text_page, double x, double y, double xTolerance, double yTolerance);

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
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetText(FPDF_TEXTPAGE text_page, int start_index, int count, unsigned short* result);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetText(FPDF_TEXTPAGE text_page, int start_index, int count, IntPtr result);

        /// <summary>
        /// Count number of rectangular areas occupied by a segment of texts.
        /// </summary>
        /// <param name="text_page">Handle to a text page information structure. Returned by FPDFText_LoadPage function.</param>
        /// <param name="start_index">Index for the start characters.</param>
        /// <param name="count">Number of characters.</param>
        /// <returns>Number of rectangles. Zero for error.</returns>
        /// <remarks>
        /// This function, along with FPDFText_GetRect can be used by applications to detect the position on the page for a text segment,
        /// so proper areas can be highlighted. FPDFTEXT will automatically merge small character boxes into bigger one if those characters
        /// are on the same line and use same font settings.
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_CountRects(FPDF_TEXTPAGE text_page, int start_index, int count);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_CountRects(FPDF_TEXTPAGE text_page, int start_index, int count);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_GetRect(FPDF_TEXTPAGE text_page, int rect_index, ref double left, ref double top, ref double right, ref double bottom);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetBoundedText(FPDF_TEXTPAGE text_page, double left, double top, double right, double bottom, IntPtr buffer, int buflen);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate FPDF_SCHHANDLE FPDFText_FindStart(FPDF_TEXTPAGE text_page, IntPtr findwhat, FPDF_FIND_FLAGS flags, int start_index);

        /// <summary>
        /// Search in the direction from page start to end.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Whether a match is found.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_FindNext(FPDF_SCHHANDLE handle);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_FindNext(FPDF_SCHHANDLE handle);

        /// <summary>
        /// Search in the direction from page end to start.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Whether a match is found.</returns>
        /// <remarks>FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFText_FindPrev(FPDF_SCHHANDLE handle);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFText_FindPrev(FPDF_SCHHANDLE handle);

        /// <summary>
        /// Get the starting character index of the search result.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Index for the starting character.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetSchResultIndex(FPDF_SCHHANDLE handle);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetSchResultIndex(FPDF_SCHHANDLE handle);

        /// <summary>
        /// Get the number of matched characters in the search result.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <returns>Number of matched characters.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFText_GetSchCount(FPDF_SCHHANDLE handle);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFText_GetSchCount(FPDF_SCHHANDLE handle);

        /// <summary>
        /// Release a search context.
        /// </summary>
        /// <param name="handle">A search context handle returned by FPDFText_FindStart.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFText_FindClose(FPDF_SCHHANDLE handle);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FPDFText_FindClose(FPDF_SCHHANDLE handle);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate FPDF_PAGELINK FPDFLink_LoadWebLinks(FPDF_TEXTPAGE text_page);

        /// <summary>
        /// Count number of detected web links.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <returns>Number of detected web links.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFLink_CountWebLinks(FPDF_PAGELINK link_page);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFLink_CountWebLinks(FPDF_PAGELINK link_page);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFLink_GetURL(FPDF_PAGELINK link_page, int link_index, IntPtr buffer, int buflen);

        /// <summary>
        /// Count number of rectangular areas for the link.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <param name="link_index">Zero-based index for the link.</param>
        /// <returns>Number of rectangular areas for the link.  If |link_index| does not correspond to a valid link, then 0 is returned.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFLink_CountRects(FPDF_PAGELINK link_page, int link_index);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int FPDFLink_CountRects(FPDF_PAGELINK link_page, int link_index);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFLink_GetRect(FPDF_PAGELINK link_page, int link_index, int rect_index, ref double left, ref double top, ref double right, ref double bottom);

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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool FPDFLink_GetTextRange(FPDF_PAGELINK link_page, int link_index, ref int start_char_index, ref int char_count);

        /// <summary>
        /// Release resources used by weblink feature.
        /// </summary>
        /// <param name="link_page">Handle returned by FPDFLink_LoadWebLinks.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFLink_CloseWebLinks(FPDF_PAGELINK link_page);.
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FPDFLink_CloseWebLinks(FPDF_PAGELINK link_page);
    }
}
