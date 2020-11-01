namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Wrapper.Exceptions;

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        /*
        private static PDFiumDelegates.FPDF_NewFunction FPDF_NewFunctionStatic { get; set; }

            // FPDF_NewFunction
            functionName = nameof(PDFiumDelegates.FPDF_NewFunction);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }
            FPDF_NewFunction = (PDFiumDelegates.FPDF_NewFunction)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_NewFunction));

         */

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetFirstChild"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBookmark_GetFirstChild FPDFBookmark_GetFirstChildStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetFirstChild"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBookmark_GetFirstChild FPDFBookmark_GetFirstChild => FPDFBookmark_GetFirstChildStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetNextSibling"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBookmark_GetNextSibling FPDFBookmark_GetNextSiblingStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetNextSibling"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBookmark_GetNextSibling FPDFBookmark_GetNextSibling => FPDFBookmark_GetNextSiblingStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetTitle"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBookmark_GetTitle FPDFBookmark_GetTitleStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetTitle"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBookmark_GetTitle FPDFBookmark_GetTitle => FPDFBookmark_GetTitleStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_Find"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBookmark_Find FPDFBookmark_FindStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_Find"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBookmark_Find FPDFBookmark_Find => FPDFBookmark_FindStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetDest"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBookmark_GetDest FPDFBookmark_GetDestStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetDest"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBookmark_GetDest FPDFBookmark_GetDest => FPDFBookmark_GetDestStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetAction"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBookmark_GetAction FPDFBookmark_GetActionStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBookmark_GetAction"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBookmark_GetAction FPDFBookmark_GetAction => FPDFBookmark_GetActionStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetType"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAction_GetType FPDFAction_GetTypeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetType"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAction_GetType FPDFAction_GetType => FPDFAction_GetTypeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetDest"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAction_GetDest FPDFAction_GetDestStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetDest"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAction_GetDest FPDFAction_GetDest => FPDFAction_GetDestStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetFilePath"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAction_GetFilePath FPDFAction_GetFilePathStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetFilePath"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAction_GetFilePath FPDFAction_GetFilePath => FPDFAction_GetFilePathStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetURIPath"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAction_GetURIPath FPDFAction_GetURIPathStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAction_GetURIPath"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAction_GetURIPath FPDFAction_GetURIPath => FPDFAction_GetURIPathStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFDest_GetDestPageIndex"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFDest_GetDestPageIndex FPDFDest_GetDestPageIndexStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFDest_GetDestPageIndex"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFDest_GetDestPageIndex FPDFDest_GetDestPageIndex => FPDFDest_GetDestPageIndexStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFDest_GetLocationInPage"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFDest_GetLocationInPage FPDFDest_GetLocationInPageStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFDest_GetLocationInPage"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFDest_GetLocationInPage FPDFDest_GetLocationInPage => FPDFDest_GetLocationInPageStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetLinkAtPoint"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetLinkAtPoint FPDFLink_GetLinkAtPointStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetLinkAtPoint"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetLinkAtPoint FPDFLink_GetLinkAtPoint => FPDFLink_GetLinkAtPointStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint FPDFLink_GetLinkZOrderAtPointStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint FPDFLink_GetLinkZOrderAtPoint => FPDFLink_GetLinkZOrderAtPointStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetDest"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetDest FPDFLink_GetDestStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetDest"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetDest FPDFLink_GetDest => FPDFLink_GetDestStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetAction"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetAction FPDFLink_GetActionStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetAction"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetAction FPDFLink_GetAction => FPDFLink_GetActionStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_Enumerate"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_Enumerate FPDFLink_EnumerateStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_Enumerate"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_Enumerate FPDFLink_Enumerate => FPDFLink_EnumerateStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetAnnot"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetAnnot FPDFLink_GetAnnotStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetAnnot"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetAnnot FPDFLink_GetAnnot => FPDFLink_GetAnnotStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetAnnotRect"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetAnnotRect FPDFLink_GetAnnotRectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetAnnotRect"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetAnnotRect FPDFLink_GetAnnotRect => FPDFLink_GetAnnotRectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_CountQuadPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_CountQuadPoints FPDFLink_CountQuadPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_CountQuadPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_CountQuadPoints FPDFLink_CountQuadPoints => FPDFLink_CountQuadPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetQuadPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFLink_GetQuadPoints FPDFLink_GetQuadPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFLink_GetQuadPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFLink_GetQuadPoints FPDFLink_GetQuadPoints => FPDFLink_GetQuadPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetMetaText"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetMetaText FPDF_GetMetaTextStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetMetaText"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetMetaText FPDF_GetMetaText => FPDF_GetMetaTextStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageLabel"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageLabel FPDF_GetPageLabelStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageLabel"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageLabel FPDF_GetPageLabel => FPDF_GetPageLabelStatic;

        private static void LoadDllDocPart(string libraryName)
        {
            // FPDFBookmark_GetFirstChild
            var functionName = nameof(PDFiumDelegates.FPDFBookmark_GetFirstChild);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBookmark_GetFirstChildStatic = (PDFiumDelegates.FPDFBookmark_GetFirstChild)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBookmark_GetFirstChild));

            // FPDFBookmark_GetNextSibling
            functionName = nameof(PDFiumDelegates.FPDFBookmark_GetNextSibling);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBookmark_GetNextSiblingStatic = (PDFiumDelegates.FPDFBookmark_GetNextSibling)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBookmark_GetNextSibling));

            // FPDFBookmark_GetTitle
            functionName = nameof(PDFiumDelegates.FPDFBookmark_GetTitle);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBookmark_GetTitleStatic = (PDFiumDelegates.FPDFBookmark_GetTitle)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBookmark_GetTitle));

            // FPDFBookmark_Find
            functionName = nameof(PDFiumDelegates.FPDFBookmark_Find);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBookmark_FindStatic = (PDFiumDelegates.FPDFBookmark_Find)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBookmark_Find));

            // FPDFBookmark_GetDest
            functionName = nameof(PDFiumDelegates.FPDFBookmark_GetDest);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBookmark_GetDestStatic = (PDFiumDelegates.FPDFBookmark_GetDest)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBookmark_GetDest));

            // FPDFBookmark_GetAction
            functionName = nameof(PDFiumDelegates.FPDFBookmark_GetAction);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBookmark_GetActionStatic = (PDFiumDelegates.FPDFBookmark_GetAction)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBookmark_GetAction));

            // FPDFAction_GetType
            functionName = nameof(PDFiumDelegates.FPDFAction_GetType);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAction_GetTypeStatic = (PDFiumDelegates.FPDFAction_GetType)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAction_GetType));

            // FPDFAction_GetDest
            functionName = nameof(PDFiumDelegates.FPDFAction_GetDest);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAction_GetDestStatic = (PDFiumDelegates.FPDFAction_GetDest)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAction_GetDest));

            // FPDFAction_GetFilePath
            functionName = nameof(PDFiumDelegates.FPDFAction_GetFilePath);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAction_GetFilePathStatic = (PDFiumDelegates.FPDFAction_GetFilePath)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAction_GetFilePath));

            // FPDFAction_GetURIPath
            functionName = nameof(PDFiumDelegates.FPDFAction_GetURIPath);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAction_GetURIPathStatic = (PDFiumDelegates.FPDFAction_GetURIPath)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAction_GetURIPath));

            // FPDFDest_GetDestPageIndex
            functionName = nameof(PDFiumDelegates.FPDFDest_GetDestPageIndex);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFDest_GetDestPageIndexStatic = (PDFiumDelegates.FPDFDest_GetDestPageIndex)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFDest_GetDestPageIndex));

            // FPDFDest_GetLocationInPage
            functionName = nameof(PDFiumDelegates.FPDFDest_GetLocationInPage);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFDest_GetLocationInPageStatic = (PDFiumDelegates.FPDFDest_GetLocationInPage)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFDest_GetLocationInPage));

            // FPDFLink_GetLinkAtPoint
            functionName = nameof(PDFiumDelegates.FPDFLink_GetLinkAtPoint);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetLinkAtPointStatic = (PDFiumDelegates.FPDFLink_GetLinkAtPoint)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetLinkAtPoint));

            // FPDFLink_GetLinkZOrderAtPoint
            functionName = nameof(PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetLinkZOrderAtPointStatic = (PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetLinkZOrderAtPoint));

            // FPDFLink_GetDest
            functionName = nameof(PDFiumDelegates.FPDFLink_GetDest);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetDestStatic = (PDFiumDelegates.FPDFLink_GetDest)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetDest));

            // FPDFLink_GetAction
            functionName = nameof(PDFiumDelegates.FPDFLink_GetAction);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetActionStatic = (PDFiumDelegates.FPDFLink_GetAction)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetAction));

            // FPDFLink_Enumerate
            functionName = nameof(PDFiumDelegates.FPDFLink_Enumerate);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_EnumerateStatic = (PDFiumDelegates.FPDFLink_Enumerate)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_Enumerate));

            // FPDFLink_GetAnnot
            functionName = nameof(PDFiumDelegates.FPDFLink_GetAnnot);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetAnnotStatic = (PDFiumDelegates.FPDFLink_GetAnnot)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetAnnot));

            // FPDFLink_GetAnnotRect
            functionName = nameof(PDFiumDelegates.FPDFLink_GetAnnotRect);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetAnnotRectStatic = (PDFiumDelegates.FPDFLink_GetAnnotRect)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetAnnotRect));

            // FPDFLink_CountQuadPoints
            functionName = nameof(PDFiumDelegates.FPDFLink_CountQuadPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_CountQuadPointsStatic = (PDFiumDelegates.FPDFLink_CountQuadPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_CountQuadPoints));

            // FPDFLink_GetQuadPoints
            functionName = nameof(PDFiumDelegates.FPDFLink_GetQuadPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFLink_GetQuadPointsStatic = (PDFiumDelegates.FPDFLink_GetQuadPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFLink_GetQuadPoints));

            // FPDF_GetMetaText
            functionName = nameof(PDFiumDelegates.FPDF_GetMetaText);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetMetaTextStatic = (PDFiumDelegates.FPDF_GetMetaText)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetMetaText));

            // FPDF_GetPageLabel
            functionName = nameof(PDFiumDelegates.FPDF_GetPageLabel);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageLabelStatic = (PDFiumDelegates.FPDF_GetPageLabel)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageLabel));
        }
    }
}
