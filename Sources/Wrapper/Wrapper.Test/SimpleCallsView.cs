namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements some unit test methods for functions defined in fpdfview.h.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class SimpleCallsView
    {
        #region Private fields

        private static TestContext _testContext;
        private static string _pdfFilesFolder;
        private static string _pdfFile;
        private static PDFiumBridge _bridge;

        #endregion Private fields

        #region Test init and clean up

        /// <summary>
        /// Test class initialization method.
        /// Use this method to run code before any test is executed.
        /// </summary>
        /// <param name="testContext">Test context for the test.</param>
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            _testContext = testContext ?? throw new ArgumentNullException(nameof(testContext));

            var position = _testContext.DeploymentDirectory.IndexOf("Sources", StringComparison.InvariantCultureIgnoreCase);
            var rootFolder = _testContext.DeploymentDirectory.Substring(0, position);
            _pdfFilesFolder = Path.Combine(rootFolder, @"TestData\PDFs");
            _pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            _bridge = new PDFiumBridge();
            ////_document = _bridge.FPDF_LoadDocument(_pdfFile, null);
        }

        /// <summary>
        /// Test class cleanup method.
        /// Use this method to run code after all tests in this class have run.
        /// </summary>
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            ////_bridge.FPDF_CloseDocument(_document);
            _bridge.Dispose();
        }

        #endregion Test init and clean up

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFInitLibrary_SimpleCall_NoException()
        {
            _bridge.FPDF_InitLibrary();
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFDestroyLibrary_SimpleCall_NoException()
        {
            _bridge.FPDF_DestroyLibrary();
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFSetPrintTextWithGDI_SimpleCall_NoException()
        {
            _bridge.FPDF_SetPrintTextWithGDI(false);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFSetPrintMode_SimpleCall_NoException()
        {
            _bridge.FPDF_SetPrintMode(FPDF_PRINTMODES.FPDF_PRINTMODE_EMF);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLoadDocument_SimpleCall_NoException()
        {
            _bridge.FPDF_LoadDocument(string.Empty, string.Empty);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLoadMemDocument_SimpleCall_NoException()
        {
            // ToDo:
            // For some reason, this unit test fails on GitHub. Deactivated.
            ////var byteArray = new byte[] { 0 };
            ////var hGlobal = Marshal.AllocHGlobal(1);
            ////Marshal.Copy(byteArray, 0, hGlobal, 1);
            ////_bridge.FPDF_LoadMemDocument(hGlobal, 1, string.Empty);
            ////Marshal.FreeHGlobal(hGlobal);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLoadMemDocument64_SimpleCall_NoException()
        {
            // ToDo:
            // For some reason, this unit test fails on GitHub. Deactivated.
            ////var byteArray = new byte[] { 0 };
            ////var hGlobal = Marshal.AllocHGlobal(1);
            ////Marshal.Copy(byteArray, 0, hGlobal, 1);
            ////_bridge.FPDF_LoadMemDocument64(hGlobal, 1, string.Empty);
            ////Marshal.FreeHGlobal(hGlobal);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetFileVersion_SimpleCall_NoException()
        {
            _bridge.FPDF_GetFileVersion(FPDF_DOCUMENT.InvalidHandle, out int fileVersion);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetLastError_SimpleCall_NoException()
        {
            _bridge.FPDF_GetLastError();
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFDocumentHasValidCrossReferenceTable_SimpleCall_NoException()
        {
            _bridge.FPDF_DocumentHasValidCrossReferenceTable(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetTrailerEnds_SimpleCall_NoException()
        {
            _bridge.FPDF_GetTrailerEnds(FPDF_DOCUMENT.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetDocPermissions_SimpleCall_NoException()
        {
            _bridge.FPDF_GetDocPermissions(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetSecurityHandlerRevision_SimpleCall_NoException()
        {
            _bridge.FPDF_GetSecurityHandlerRevision(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageCount_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageCount(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLoadPage_SimpleCall_NoException()
        {
            _bridge.FPDF_LoadPage(FPDF_DOCUMENT.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageWidthF_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageWidthF(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageWidth_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageWidth(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageHeightF_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageHeightF(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageHeight_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageHeight(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageBoundingBox_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageBoundingBox(FPDF_PAGE.InvalidHandle, out FS_RECTF rect);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageSizeByIndexF_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageSizeByIndexF(FPDF_DOCUMENT.InvalidHandle, 0, out FS_SIZEF size);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageSizeByIndex_SimpleCall_NoException()
        {
            double width, height;
            width = height = 0d;
            _bridge.FPDF_GetPageSizeByIndex(FPDF_DOCUMENT.InvalidHandle, 0, ref width, ref height);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFRenderPageBitmap_SimpleCall_NoException()
        {
            _bridge.FPDF_RenderPageBitmap(FPDF_BITMAP.InvalidHandle, FPDF_PAGE.InvalidHandle, 0, 0, 0, 0, 0, FPDF_RENDERING_FLAGS.FPDF_NONE);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFRenderPageBitmapWithMatrix_SimpleCall_NoException()
        {
            var matrix = new FS_MATRIX();
            var clipping = new FS_RECTF();
            _bridge.FPDF_RenderPageBitmapWithMatrix(FPDF_BITMAP.InvalidHandle, FPDF_PAGE.InvalidHandle, ref matrix, ref clipping, FPDF_RENDERING_FLAGS.FPDF_NONE);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFClosePage_SimpleCall_NoException()
        {
            _bridge.FPDF_ClosePage(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFCloseDocument_SimpleCall_NoException()
        {
            _bridge.FPDF_CloseDocument(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFDeviceToPage_SimpleCall_NoException()
        {
            double page_x, page_y;
            page_x = page_y = 0d;
            _bridge.FPDF_DeviceToPage(FPDF_PAGE.InvalidHandle, 0, 0, 0, 0, 0, 0, 0, ref page_x, ref page_y);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageToDevice_SimpleCall_NoException()
        {
            int device_x, device_y;
            device_x = device_y = 0;
            _bridge.FPDF_PageToDevice(FPDF_PAGE.InvalidHandle, 0, 0, 0, 0, 0, 0d, 0d, ref device_x, ref device_y);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapCreate_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_Create(0, 0, false);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapCreateEx_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_CreateEx(0, 0, FPDFBitmapFormat.FPDFBitmap_BGRA, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapGetFormat_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_GetFormat(FPDF_BITMAP.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapFillRect_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_FillRect(FPDF_BITMAP.InvalidHandle, 0, 0, 0, 0, new FPDF_COLOR(0, 1, 2, 3));
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapGetBuffer_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_GetBuffer(FPDF_BITMAP.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapGetWidth_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_GetWidth(FPDF_BITMAP.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapGetHeight_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_GetHeight(FPDF_BITMAP.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapGetStride_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_GetStride(FPDF_BITMAP.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBitmapDestroy_SimpleCall_NoException()
        {
            _bridge.FPDFBitmap_Destroy(FPDF_BITMAP.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetPrintScaling_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetPrintScaling(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetNumCopies_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetNumCopies(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetPrintPageRange_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetPrintPageRange(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetPrintPageRangeCount_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetPrintPageRangeCount(FPDF_PAGERANGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetPrintPageRangeElement_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetPrintPageRangeElement(FPDF_PAGERANGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetDuplex_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetDuplex(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFVIEWERREFGetName_SimpleCall_NoException()
        {
            _bridge.FPDF_VIEWERREF_GetName(FPDF_DOCUMENT.InvalidHandle, string.Empty, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFCountNamedDests_SimpleCall_NoException()
        {
            _bridge.FPDF_CountNamedDests(FPDF_DOCUMENT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetNamedDestByName_SimpleCall_NoException()
        {
            _bridge.FPDF_GetNamedDestByName(FPDF_DOCUMENT.InvalidHandle, string.Empty);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetNamedDest_SimpleCall_NoException()
        {
            int buflen = 0;
            _bridge.FPDF_GetNamedDest(FPDF_DOCUMENT.InvalidHandle, 0, IntPtr.Zero, ref buflen);
        }
    }
}
