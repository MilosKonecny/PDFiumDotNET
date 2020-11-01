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
        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_InitLibrary"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_InitLibrary FPDF_InitLibraryStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_InitLibrary"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_InitLibrary FPDF_InitLibrary => FPDF_InitLibraryStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_DestroyLibrary"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_DestroyLibrary FPDF_DestroyLibraryStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_DestroyLibrary"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_DestroyLibrary FPDF_DestroyLibrary => FPDF_DestroyLibraryStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_LoadDocument"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_LoadDocument FPDF_LoadDocumentStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_LoadDocument"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_LoadDocument FPDF_LoadDocument => FPDF_LoadDocumentStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetFileVersion"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetFileVersion FPDF_GetFileVersionStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetFileVersion"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetFileVersion FPDF_GetFileVersion => FPDF_GetFileVersionStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetLastError"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetLastError FPDF_GetLastErrorStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetLastError"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetLastError FPDF_GetLastError => FPDF_GetLastErrorStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable FPDF_DocumentHasValidCrossReferenceTableStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable FPDF_DocumentHasValidCrossReferenceTable => FPDF_DocumentHasValidCrossReferenceTableStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetDocPermissions"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetDocPermissions FPDF_GetDocPermissionsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetDocPermissions"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetDocPermissions FPDF_GetDocPermissions => FPDF_GetDocPermissionsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageCount FPDF_GetPageCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageCount FPDF_GetPageCount => FPDF_GetPageCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_LoadPage"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_LoadPage FPDF_LoadPageStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_LoadPage"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_LoadPage FPDF_LoadPage => FPDF_LoadPageStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageWidthF"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageWidthF FPDF_GetPageWidthFStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageWidthF"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageWidthF FPDF_GetPageWidthF => FPDF_GetPageWidthFStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageWidth"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageWidth FPDF_GetPageWidthStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageWidth"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageWidth FPDF_GetPageWidth => FPDF_GetPageWidthStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageHeightF"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageHeightF FPDF_GetPageHeightFStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageHeightF"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageHeightF FPDF_GetPageHeightF => FPDF_GetPageHeightFStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageHeight"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageHeight FPDF_GetPageHeightStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageHeight"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageHeight FPDF_GetPageHeight => FPDF_GetPageHeightStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageBoundingBox"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageBoundingBox FPDF_GetPageBoundingBoxStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageBoundingBox"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageBoundingBox FPDF_GetPageBoundingBox => FPDF_GetPageBoundingBoxStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageSizeByIndexF"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageSizeByIndexF FPDF_GetPageSizeByIndexFStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageSizeByIndexF"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageSizeByIndexF FPDF_GetPageSizeByIndexF => FPDF_GetPageSizeByIndexFStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageSizeByIndex"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetPageSizeByIndex FPDF_GetPageSizeByIndexStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetPageSizeByIndex"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetPageSizeByIndex FPDF_GetPageSizeByIndex => FPDF_GetPageSizeByIndexStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_RenderPageBitmap"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_RenderPageBitmap FPDF_RenderPageBitmapStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_RenderPageBitmap"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_RenderPageBitmap FPDF_RenderPageBitmap => FPDF_RenderPageBitmapStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix FPDF_RenderPageBitmapWithMatrixStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix FPDF_RenderPageBitmapWithMatrix => FPDF_RenderPageBitmapWithMatrixStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_ClosePage"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_ClosePage FPDF_ClosePageStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_ClosePage"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_ClosePage FPDF_ClosePage => FPDF_ClosePageStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_CloseDocument"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_CloseDocument FPDF_CloseDocumentStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_CloseDocument"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_CloseDocument FPDF_CloseDocument => FPDF_CloseDocumentStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_DeviceToPage"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_DeviceToPage FPDF_DeviceToPageStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_DeviceToPage"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_DeviceToPage FPDF_DeviceToPage => FPDF_DeviceToPageStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_PageToDevice"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_PageToDevice FPDF_PageToDeviceStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_PageToDevice"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_PageToDevice FPDF_PageToDevice => FPDF_PageToDeviceStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_Create"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_Create FPDFBitmap_CreateStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_Create"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_Create FPDFBitmap_Create => FPDFBitmap_CreateStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_CreateEx"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_CreateEx FPDFBitmap_CreateExStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_CreateEx"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_CreateEx FPDFBitmap_CreateEx => FPDFBitmap_CreateExStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetFormat"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_GetFormat FPDFBitmap_GetFormatStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetFormat"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_GetFormat FPDFBitmap_GetFormat => FPDFBitmap_GetFormatStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_FillRect"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_FillRect FPDFBitmap_FillRectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_FillRect"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_FillRect FPDFBitmap_FillRect => FPDFBitmap_FillRectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetBuffer"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_GetBuffer FPDFBitmap_GetBufferStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetBuffer"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_GetBuffer FPDFBitmap_GetBuffer => FPDFBitmap_GetBufferStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetWidth"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_GetWidth FPDFBitmap_GetWidthStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetWidth"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_GetWidth FPDFBitmap_GetWidth => FPDFBitmap_GetWidthStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetHeight"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_GetHeight FPDFBitmap_GetHeightStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetHeight"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_GetHeight FPDFBitmap_GetHeight => FPDFBitmap_GetHeightStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetStride"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_GetStride FPDFBitmap_GetStrideStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_GetStride"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_GetStride FPDFBitmap_GetStride => FPDFBitmap_GetStrideStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_Destroy"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFBitmap_Destroy FPDFBitmap_DestroyStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFBitmap_Destroy"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFBitmap_Destroy FPDFBitmap_Destroy => FPDFBitmap_DestroyStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling FPDF_VIEWERREF_GetPrintScalingStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling FPDF_VIEWERREF_GetPrintScaling => FPDF_VIEWERREF_GetPrintScalingStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies FPDF_VIEWERREF_GetNumCopiesStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies FPDF_VIEWERREF_GetNumCopies => FPDF_VIEWERREF_GetNumCopiesStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange FPDF_VIEWERREF_GetPrintPageRangeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange FPDF_VIEWERREF_GetPrintPageRange => FPDF_VIEWERREF_GetPrintPageRangeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount FPDF_VIEWERREF_GetPrintPageRangeCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount FPDF_VIEWERREF_GetPrintPageRangeCount => FPDF_VIEWERREF_GetPrintPageRangeCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement FPDF_VIEWERREF_GetPrintPageRangeElementStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement FPDF_VIEWERREF_GetPrintPageRangeElement => FPDF_VIEWERREF_GetPrintPageRangeElementStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetDuplex"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetDuplex FPDF_VIEWERREF_GetDuplexStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetDuplex"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetDuplex FPDF_VIEWERREF_GetDuplex => FPDF_VIEWERREF_GetDuplexStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetName"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_VIEWERREF_GetName FPDF_VIEWERREF_GetNameStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_VIEWERREF_GetName"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_VIEWERREF_GetName FPDF_VIEWERREF_GetName => FPDF_VIEWERREF_GetNameStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_CountNamedDests"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_CountNamedDests FPDF_CountNamedDestsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_CountNamedDests"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_CountNamedDests FPDF_CountNamedDests => FPDF_CountNamedDestsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetNamedDestByName"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetNamedDestByName FPDF_GetNamedDestByNameStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetNamedDestByName"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetNamedDestByName FPDF_GetNamedDestByName => FPDF_GetNamedDestByNameStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDF_GetNamedDest"/>.
        /// </summary>
        private static PDFiumDelegates.FPDF_GetNamedDest FPDF_GetNamedDestStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDF_GetNamedDest"/>.
        /// </summary>
        internal PDFiumDelegates.FPDF_GetNamedDest FPDF_GetNamedDest => FPDF_GetNamedDestStatic;

        private static void LoadDllViewPart(string libraryName)
        {
            LoadDllViewPartBase(libraryName);
            LoadDllViewPartNext(libraryName);
        }

        private static void LoadDllViewPartBase(string libraryName)
        {
            // FPDF_InitLibrary
            var functionName = nameof(PDFiumDelegates.FPDF_InitLibrary);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_InitLibraryStatic = (PDFiumDelegates.FPDF_InitLibrary)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_InitLibrary));

            // FPDF_DestroyLibrary
            functionName = nameof(PDFiumDelegates.FPDF_DestroyLibrary);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_DestroyLibraryStatic = (PDFiumDelegates.FPDF_DestroyLibrary)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_DestroyLibrary));

            // FPDF_LoadDocument
            functionName = nameof(PDFiumDelegates.FPDF_LoadDocument);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_LoadDocumentStatic = (PDFiumDelegates.FPDF_LoadDocument)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_LoadDocument));

            // FPDF_GetFileVersion
            functionName = nameof(PDFiumDelegates.FPDF_GetFileVersion);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetFileVersionStatic = (PDFiumDelegates.FPDF_GetFileVersion)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetFileVersion));

            // FPDF_GetLastError
            functionName = nameof(PDFiumDelegates.FPDF_GetLastError);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetLastErrorStatic = (PDFiumDelegates.FPDF_GetLastError)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetLastError));

            // FPDF_DocumentHasValidCrossReferenceTable
            functionName = nameof(PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_DocumentHasValidCrossReferenceTableStatic = (PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_DocumentHasValidCrossReferenceTable));

            // FPDF_GetDocPermissions
            functionName = nameof(PDFiumDelegates.FPDF_GetDocPermissions);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetDocPermissionsStatic = (PDFiumDelegates.FPDF_GetDocPermissions)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetDocPermissions));

            // FPDF_GetPageCount
            functionName = nameof(PDFiumDelegates.FPDF_GetPageCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageCountStatic = (PDFiumDelegates.FPDF_GetPageCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageCount));

            // FPDF_LoadPage
            functionName = nameof(PDFiumDelegates.FPDF_LoadPage);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_LoadPageStatic = (PDFiumDelegates.FPDF_LoadPage)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_LoadPage));

            // FPDF_GetPageWidthF
            functionName = nameof(PDFiumDelegates.FPDF_GetPageWidthF);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageWidthFStatic = (PDFiumDelegates.FPDF_GetPageWidthF)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageWidthF));

            // FPDF_GetPageWidth
            functionName = nameof(PDFiumDelegates.FPDF_GetPageWidth);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageWidthStatic = (PDFiumDelegates.FPDF_GetPageWidth)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageWidth));

            // FPDF_GetPageHeightF
            functionName = nameof(PDFiumDelegates.FPDF_GetPageHeightF);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageHeightFStatic = (PDFiumDelegates.FPDF_GetPageHeightF)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageHeightF));

            // FPDF_GetPageHeight
            functionName = nameof(PDFiumDelegates.FPDF_GetPageHeight);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageHeightStatic = (PDFiumDelegates.FPDF_GetPageHeight)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageHeight));

            // FPDF_GetPageBoundingBox
            functionName = nameof(PDFiumDelegates.FPDF_GetPageBoundingBox);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageBoundingBoxStatic = (PDFiumDelegates.FPDF_GetPageBoundingBox)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageBoundingBox));

            // FPDF_GetPageSizeByIndexF
            functionName = nameof(PDFiumDelegates.FPDF_GetPageSizeByIndexF);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageSizeByIndexFStatic = (PDFiumDelegates.FPDF_GetPageSizeByIndexF)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageSizeByIndexF));

            // FPDF_GetPageSizeByIndex
            functionName = nameof(PDFiumDelegates.FPDF_GetPageSizeByIndex);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetPageSizeByIndexStatic = (PDFiumDelegates.FPDF_GetPageSizeByIndex)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetPageSizeByIndex));

            // FPDF_RenderPageBitmap
            functionName = nameof(PDFiumDelegates.FPDF_RenderPageBitmap);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_RenderPageBitmapStatic = (PDFiumDelegates.FPDF_RenderPageBitmap)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_RenderPageBitmap));

            // FPDF_RenderPageBitmapWithMatrix
            functionName = nameof(PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_RenderPageBitmapWithMatrixStatic = (PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_RenderPageBitmapWithMatrix));

            // FPDF_ClosePage
            functionName = nameof(PDFiumDelegates.FPDF_ClosePage);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_ClosePageStatic = (PDFiumDelegates.FPDF_ClosePage)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_ClosePage));

            // FPDF_CloseDocument
            functionName = nameof(PDFiumDelegates.FPDF_CloseDocument);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_CloseDocumentStatic = (PDFiumDelegates.FPDF_CloseDocument)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_CloseDocument));
        }

        private static void LoadDllViewPartNext(string libraryName)
        {
            // FPDF_DeviceToPage
            var functionName = nameof(PDFiumDelegates.FPDF_DeviceToPage);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_DeviceToPageStatic = (PDFiumDelegates.FPDF_DeviceToPage)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_DeviceToPage));

            // FPDF_PageToDevice
            functionName = nameof(PDFiumDelegates.FPDF_PageToDevice);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_PageToDeviceStatic = (PDFiumDelegates.FPDF_PageToDevice)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_PageToDevice));

            // FPDFBitmap_Create
            functionName = nameof(PDFiumDelegates.FPDFBitmap_Create);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_CreateStatic = (PDFiumDelegates.FPDFBitmap_Create)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_Create));

            // FPDFBitmap_CreateEx
            functionName = nameof(PDFiumDelegates.FPDFBitmap_CreateEx);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_CreateExStatic = (PDFiumDelegates.FPDFBitmap_CreateEx)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_CreateEx));

            // FPDFBitmap_GetFormat
            functionName = nameof(PDFiumDelegates.FPDFBitmap_GetFormat);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_GetFormatStatic = (PDFiumDelegates.FPDFBitmap_GetFormat)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_GetFormat));

            // FPDFBitmap_FillRect
            functionName = nameof(PDFiumDelegates.FPDFBitmap_FillRect);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_FillRectStatic = (PDFiumDelegates.FPDFBitmap_FillRect)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_FillRect));

            // FPDFBitmap_GetBuffer
            functionName = nameof(PDFiumDelegates.FPDFBitmap_GetBuffer);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_GetBufferStatic = (PDFiumDelegates.FPDFBitmap_GetBuffer)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_GetBuffer));

            // FPDFBitmap_GetWidth
            functionName = nameof(PDFiumDelegates.FPDFBitmap_GetWidth);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_GetWidthStatic = (PDFiumDelegates.FPDFBitmap_GetWidth)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_GetWidth));

            // FPDFBitmap_GetHeight
            functionName = nameof(PDFiumDelegates.FPDFBitmap_GetHeight);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_GetHeightStatic = (PDFiumDelegates.FPDFBitmap_GetHeight)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_GetHeight));

            // FPDFBitmap_GetStride
            functionName = nameof(PDFiumDelegates.FPDFBitmap_GetStride);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_GetStrideStatic = (PDFiumDelegates.FPDFBitmap_GetStride)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_GetStride));

            // FPDFBitmap_Destroy
            functionName = nameof(PDFiumDelegates.FPDFBitmap_Destroy);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFBitmap_DestroyStatic = (PDFiumDelegates.FPDFBitmap_Destroy)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFBitmap_Destroy));

            // FPDF_VIEWERREF_GetPrintScaling
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetPrintScalingStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintScaling));

            // FPDF_VIEWERREF_GetNumCopies
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetNumCopiesStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetNumCopies));

            // FPDF_VIEWERREF_GetPrintPageRange
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetPrintPageRangeStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRange));

            // FPDF_VIEWERREF_GetPrintPageRangeCount
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetPrintPageRangeCountStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeCount));

            // FPDF_VIEWERREF_GetPrintPageRangeElement
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetPrintPageRangeElementStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetPrintPageRangeElement));

            // FPDF_VIEWERREF_GetDuplex
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetDuplex);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetDuplexStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetDuplex)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetDuplex));

            // FPDF_VIEWERREF_GetName
            functionName = nameof(PDFiumDelegates.FPDF_VIEWERREF_GetName);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_VIEWERREF_GetNameStatic = (PDFiumDelegates.FPDF_VIEWERREF_GetName)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_VIEWERREF_GetName));

            // FPDF_CountNamedDests
            functionName = nameof(PDFiumDelegates.FPDF_CountNamedDests);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_CountNamedDestsStatic = (PDFiumDelegates.FPDF_CountNamedDests)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_CountNamedDests));

            // FPDF_GetNamedDestByName
            functionName = nameof(PDFiumDelegates.FPDF_GetNamedDestByName);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetNamedDestByNameStatic = (PDFiumDelegates.FPDF_GetNamedDestByName)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetNamedDestByName));

            // FPDF_GetNamedDest
            functionName = nameof(PDFiumDelegates.FPDF_GetNamedDest);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDF_GetNamedDestStatic = (PDFiumDelegates.FPDF_GetNamedDest)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDF_GetNamedDest));
        }
    }
}
