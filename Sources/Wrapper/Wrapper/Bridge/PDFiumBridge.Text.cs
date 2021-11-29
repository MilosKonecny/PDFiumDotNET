namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Wrapper.Exceptions;

    // Disable "Member 'xxxx' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_LoadPage"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_LoadPage FPDFText_LoadPageStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_LoadPage"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_LoadPage FPDFText_LoadPage => FPDFText_LoadPageStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_ClosePage"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_ClosePage FPDFText_ClosePageStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_ClosePage"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_ClosePage FPDFText_ClosePage => FPDFText_ClosePageStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_CountChars"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_CountChars FPDFText_CountCharsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_CountChars"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_CountChars FPDFText_CountChars => FPDFText_CountCharsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetUnicode"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetUnicode FPDFText_GetUnicodeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetUnicode"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetUnicode FPDFText_GetUnicode => FPDFText_GetUnicodeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFontSize"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetFontSize FPDFText_GetFontSizeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFontSize"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetFontSize FPDFText_GetFontSize => FPDFText_GetFontSizeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFontInfo"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetFontInfo FPDFText_GetFontInfoStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFontInfo"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetFontInfo FPDFText_GetFontInfo => FPDFText_GetFontInfoStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFontWeight"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetFontWeight FPDFText_GetFontWeightStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFontWeight"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetFontWeight FPDFText_GetFontWeight => FPDFText_GetFontWeightStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetTextRenderMode"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetTextRenderMode FPDFText_GetTextRenderModeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetTextRenderMode"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetTextRenderMode FPDFText_GetTextRenderMode => FPDFText_GetTextRenderModeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFillColor"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetFillColor FPDFText_GetFillColorStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetFillColor"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetFillColor FPDFText_GetFillColor => FPDFText_GetFillColorStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetStrokeColor"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetStrokeColor FPDFText_GetStrokeColorStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetStrokeColor"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetStrokeColor FPDFText_GetStrokeColor => FPDFText_GetStrokeColorStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharAngle"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetCharAngle FPDFText_GetCharAngleStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharAngle"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetCharAngle FPDFText_GetCharAngle => FPDFText_GetCharAngleStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharBox"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetCharBox FPDFText_GetCharBoxStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharBox"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetCharBox FPDFText_GetCharBox => FPDFText_GetCharBoxStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetLooseCharBox"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetLooseCharBox FPDFText_GetLooseCharBoxStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetLooseCharBox"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetLooseCharBox FPDFText_GetLooseCharBox => FPDFText_GetLooseCharBoxStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetMatrix"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetMatrix FPDFText_GetMatrixStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetMatrix"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetMatrix FPDFText_GetMatrix => FPDFText_GetMatrixStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharOrigin"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetCharOrigin FPDFText_GetCharOriginStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharOrigin"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetCharOrigin FPDFText_GetCharOrigin => FPDFText_GetCharOriginStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharIndexAtPos"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetCharIndexAtPos FPDFText_GetCharIndexAtPosStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetCharIndexAtPos"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetCharIndexAtPos FPDFText_GetCharIndexAtPos => FPDFText_GetCharIndexAtPosStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetText"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetText FPDFText_GetTextStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetText"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetText FPDFText_GetText => FPDFText_GetTextStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_CountRects"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_CountRects FPDFText_CountRectsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_CountRects"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_CountRects FPDFText_CountRects => FPDFText_CountRectsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetRect"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetRect FPDFText_GetRectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetRect"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetRect FPDFText_GetRect => FPDFText_GetRectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetBoundedText"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetBoundedText FPDFText_GetBoundedTextStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetBoundedText"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetBoundedText FPDFText_GetBoundedText => FPDFText_GetBoundedTextStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_FindStart"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_FindStart FPDFText_FindStartStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_FindStart"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_FindStart FPDFText_FindStart => FPDFText_FindStartStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_FindNext"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_FindNext FPDFText_FindNextStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_FindNext"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_FindNext FPDFText_FindNext => FPDFText_FindNextStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_FindPrev"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_FindPrev FPDFText_FindPrevStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_FindPrev"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_FindPrev FPDFText_FindPrev => FPDFText_FindPrevStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetSchResultIndex"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetSchResultIndex FPDFText_GetSchResultIndexStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetSchResultIndex"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetSchResultIndex FPDFText_GetSchResultIndex => FPDFText_GetSchResultIndexStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_GetSchCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_GetSchCount FPDFText_GetSchCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_GetSchCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_GetSchCount FPDFText_GetSchCount => FPDFText_GetSchCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFText_FindClose"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFText_FindClose FPDFText_FindCloseStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFText_FindClose"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFText_FindClose FPDFText_FindClose => FPDFText_FindCloseStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_LoadWebLinks"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_LoadWebLinks FPDFLink_LoadWebLinksStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_LoadWebLinks"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_LoadWebLinks FPDFLink_LoadWebLinks => FPDFLink_LoadWebLinksStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_CountWebLinks"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_CountWebLinks FPDFLink_CountWebLinksStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_CountWebLinks"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_CountWebLinks FPDFLink_CountWebLinks => FPDFLink_CountWebLinksStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetURL"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetURL FPDFLink_GetURLStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetURL"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetURL FPDFLink_GetURL => FPDFLink_GetURLStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_CountRects"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_CountRects FPDFLink_CountRectsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_CountRects"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_CountRects FPDFLink_CountRects => FPDFLink_CountRectsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetRect"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetRect FPDFLink_GetRectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetRect"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetRect FPDFLink_GetRect => FPDFLink_GetRectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetTextRange"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetTextRange FPDFLink_GetTextRangeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetTextRange"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetTextRange FPDFLink_GetTextRange => FPDFLink_GetTextRangeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_CloseWebLinks"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_CloseWebLinks FPDFLink_CloseWebLinksStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_CloseWebLinks"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_CloseWebLinks FPDFLink_CloseWebLinks => FPDFLink_CloseWebLinksStatic;

        private static void LoadDllTextPart(string libraryName)
        {
            LoadDllTextPart1(libraryName);
            LoadDllTextPart2(libraryName);
        }

        private static void LoadDllTextPart1(string libraryName)
        {
            // FPDFText_LoadPage
            var functionName = nameof(PDFiumDelegates.FPDFText_LoadPage);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_LoadPageStatic = (PDFiumDelegates.FPDFText_LoadPage)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_LoadPage));

            // FPDFText_ClosePage
            functionName = nameof(PDFiumDelegates.FPDFText_ClosePage);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_ClosePageStatic = (PDFiumDelegates.FPDFText_ClosePage)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_ClosePage));

            // FPDFText_CountChars
            functionName = nameof(PDFiumDelegates.FPDFText_CountChars);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_CountCharsStatic = (PDFiumDelegates.FPDFText_CountChars)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_CountChars));

            // FPDFText_GetUnicode
            functionName = nameof(PDFiumDelegates.FPDFText_GetUnicode);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetUnicodeStatic = (PDFiumDelegates.FPDFText_GetUnicode)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetUnicode));

            // FPDFText_GetFontSize
            functionName = nameof(PDFiumDelegates.FPDFText_GetFontSize);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetFontSizeStatic = (PDFiumDelegates.FPDFText_GetFontSize)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetFontSize));

            // FPDFText_GetFontInfo
            functionName = nameof(PDFiumDelegates.FPDFText_GetFontInfo);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetFontInfoStatic = (PDFiumDelegates.FPDFText_GetFontInfo)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetFontInfo));

            // FPDFText_GetFontWeight
            functionName = nameof(PDFiumDelegates.FPDFText_GetFontWeight);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetFontWeightStatic = (PDFiumDelegates.FPDFText_GetFontWeight)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetFontWeight));

            // FPDFText_GetTextRenderMode
            functionName = nameof(PDFiumDelegates.FPDFText_GetTextRenderMode);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetTextRenderModeStatic = (PDFiumDelegates.FPDFText_GetTextRenderMode)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetTextRenderMode));

            // FPDFText_GetFillColor
            functionName = nameof(PDFiumDelegates.FPDFText_GetFillColor);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetFillColorStatic = (PDFiumDelegates.FPDFText_GetFillColor)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetFillColor));

            // FPDFText_GetStrokeColor
            functionName = nameof(PDFiumDelegates.FPDFText_GetStrokeColor);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetStrokeColorStatic = (PDFiumDelegates.FPDFText_GetStrokeColor)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetStrokeColor));

            // FPDFText_GetCharAngle
            functionName = nameof(PDFiumDelegates.FPDFText_GetCharAngle);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetCharAngleStatic = (PDFiumDelegates.FPDFText_GetCharAngle)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetCharAngle));

            // FPDFText_GetCharBox
            functionName = nameof(PDFiumDelegates.FPDFText_GetCharBox);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetCharBoxStatic = (PDFiumDelegates.FPDFText_GetCharBox)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetCharBox));

            // FPDFText_GetLooseCharBox
            functionName = nameof(PDFiumDelegates.FPDFText_GetLooseCharBox);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetLooseCharBoxStatic = (PDFiumDelegates.FPDFText_GetLooseCharBox)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetLooseCharBox));

            // FPDFText_GetMatrix
            functionName = nameof(PDFiumDelegates.FPDFText_GetMatrix);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetMatrixStatic = (PDFiumDelegates.FPDFText_GetMatrix)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetMatrix));

            // FPDFText_GetCharOrigin
            functionName = nameof(PDFiumDelegates.FPDFText_GetCharOrigin);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetCharOriginStatic = (PDFiumDelegates.FPDFText_GetCharOrigin)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetCharOrigin));

            // FPDFText_GetCharIndexAtPos
            functionName = nameof(PDFiumDelegates.FPDFText_GetCharIndexAtPos);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetCharIndexAtPosStatic = (PDFiumDelegates.FPDFText_GetCharIndexAtPos)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetCharIndexAtPos));

            // FPDFText_GetText
            functionName = nameof(PDFiumDelegates.FPDFText_GetText);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetTextStatic = (PDFiumDelegates.FPDFText_GetText)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetText));
        }

        private static void LoadDllTextPart2(string libraryName)
        {
            // FPDFText_CountRects
            var functionName = nameof(PDFiumDelegates.FPDFText_CountRects);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_CountRectsStatic = (PDFiumDelegates.FPDFText_CountRects)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_CountRects));

            // FPDFText_GetRect
            functionName = nameof(PDFiumDelegates.FPDFText_GetRect);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetRectStatic = (PDFiumDelegates.FPDFText_GetRect)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetRect));

            // FPDFText_GetBoundedText
            functionName = nameof(PDFiumDelegates.FPDFText_GetBoundedText);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetBoundedTextStatic = (PDFiumDelegates.FPDFText_GetBoundedText)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetBoundedText));

            // FPDFText_FindStart
            functionName = nameof(PDFiumDelegates.FPDFText_FindStart);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_FindStartStatic = (PDFiumDelegates.FPDFText_FindStart)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_FindStart));

            // FPDFText_FindNext
            functionName = nameof(PDFiumDelegates.FPDFText_FindNext);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_FindNextStatic = (PDFiumDelegates.FPDFText_FindNext)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_FindNext));

            // FPDFText_FindPrev
            functionName = nameof(PDFiumDelegates.FPDFText_FindPrev);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_FindPrevStatic = (PDFiumDelegates.FPDFText_FindPrev)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_FindPrev));

            // FPDFText_GetSchResultIndex
            functionName = nameof(PDFiumDelegates.FPDFText_GetSchResultIndex);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetSchResultIndexStatic = (PDFiumDelegates.FPDFText_GetSchResultIndex)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetSchResultIndex));

            // FPDFText_GetSchCount
            functionName = nameof(PDFiumDelegates.FPDFText_GetSchCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_GetSchCountStatic = (PDFiumDelegates.FPDFText_GetSchCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_GetSchCount));

            // FPDFText_FindClose
            functionName = nameof(PDFiumDelegates.FPDFText_FindClose);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFText_FindCloseStatic = (PDFiumDelegates.FPDFText_FindClose)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFText_FindClose));

            // FPDFLink_LoadWebLinks
            functionName = nameof(PDFiumDelegates.FPDFLink_LoadWebLinks);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_LoadWebLinksStatic = (PDFiumDelegates.FPDFLink_LoadWebLinks)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_LoadWebLinks));

            // FPDFLink_CountWebLinks
            functionName = nameof(PDFiumDelegates.FPDFLink_CountWebLinks);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_CountWebLinksStatic = (PDFiumDelegates.FPDFLink_CountWebLinks)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_CountWebLinks));

            // FPDFLink_GetURL
            functionName = nameof(PDFiumDelegates.FPDFLink_GetURL);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetURLStatic = (PDFiumDelegates.FPDFLink_GetURL)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetURL));

            // FPDFLink_CountRects
            functionName = nameof(PDFiumDelegates.FPDFLink_CountRects);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_CountRectsStatic = (PDFiumDelegates.FPDFLink_CountRects)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_CountRects));

            // FPDFLink_GetRect
            functionName = nameof(PDFiumDelegates.FPDFLink_GetRect);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetRectStatic = (PDFiumDelegates.FPDFLink_GetRect)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetRect));

            // FPDFLink_GetTextRange
            functionName = nameof(PDFiumDelegates.FPDFLink_GetTextRange);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetTextRangeStatic = (PDFiumDelegates.FPDFLink_GetTextRange)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetTextRange));

            // FPDFLink_CloseWebLinks
            functionName = nameof(PDFiumDelegates.FPDFLink_CloseWebLinks);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_CloseWebLinksStatic = (PDFiumDelegates.FPDFLink_CloseWebLinks)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_CloseWebLinks));
        }
    }
}
