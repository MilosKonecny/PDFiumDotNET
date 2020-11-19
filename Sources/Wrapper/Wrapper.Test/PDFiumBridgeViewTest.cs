namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Test class for methods defined in 'PDFiumBridge.View.cs' file.
    /// </summary>
    [TestClass]
    public class PDFiumBridgeViewTest
    {
        #region Private fields

        private static TestContext _testContext;
        private static string _pdfFilesFolder;

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
        }

        /// <summary>
        /// Test class cleanup method.
        /// Use this method to run code after all tests in this class have run.
        /// </summary>
        [ClassCleanup]
        public static void MyClassCleanup()
        {
        }

        #endregion Test init and clean up

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_GetLastError"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_GetLastError_OpenPdf_Fail()
        {
#if NET48
            // The test will be successful for (net48), but fails for (netcoreapp3.1).
            // Here is such error described: https://bugs.chromium.org/p/pdfium/issues/detail?id=452
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(@"FileDoesNotExist.pdf", null);
            Assert.IsFalse(document.IsValid);
            var lastError = bridge.FPDF_GetLastError();
            Assert.AreEqual(PDFiumDelegates.FPDF_ERR_FILE, lastError);
            bridge.Dispose();
#endif // NET48
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_LoadDocument"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_LoadDocument_DocumentDoesNotExist_Fail()
        {
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(@"FileDoesNotExist.pdf", string.Empty);
            Assert.IsFalse(document.IsValid);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_LoadDocument"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_LoadDocument_DocumentExists_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_LoadDocument"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_LoadDocument_ProtectedDocument_Success()
        {
#if NET48
            var pdfFile = Path.Combine(_pdfFilesFolder, "PwdProtected(Pwd is pwd).pdf");
            var bridge = new PDFiumBridge();

            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsFalse(document.IsValid);

            var lastError = bridge.FPDF_GetLastError();
            Assert.AreEqual(PDFiumDelegates.FPDF_ERR_PASSWORD, lastError);

            document = bridge.FPDF_LoadDocument(pdfFile, "pwd");
            Assert.IsTrue(document.IsValid);

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
#endif // NET48
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_GetPageCount"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_GetPageCount_CheckCount_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.AreNotEqual(IntPtr.Zero, document);
            var count = bridge.FPDF_GetPageCount(document);
            Assert.AreEqual(1094, count);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_GetFileVersion"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_GetFileVersion_CheckVersion_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);
            var retVal = bridge.FPDF_GetFileVersion(document, out int version);
            Assert.IsTrue(retVal);
            Assert.AreEqual(15, version);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_DocumentHasValidCrossReferenceTable"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_DocumentHasValidCrossReferenceTable_Call_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.AreNotEqual(IntPtr.Zero, document);
            var retVal = bridge.FPDF_DocumentHasValidCrossReferenceTable(document);
            Assert.IsTrue(retVal);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_GetDocPermissions"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_GetDocPermissions_Call_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);
            var retVal = bridge.FPDF_GetDocPermissions(document);
            _testContext.WriteLine($"PDF file = '{pdfFile}' has permissions = '{retVal:X}'");
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_LoadPage"/> and <see cref="PDFiumBridge.FPDF_ClosePage"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_LoadPageCheckEveryPageProperties_Check_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);
            var pageCount = bridge.FPDF_GetPageCount(document);
            for (var index = 0; index < pageCount; index++)
            {
                var page = bridge.FPDF_LoadPage(document, index);
                Assert.IsTrue(page.IsValid);

                var widthF = bridge.FPDF_GetPageWidthF(page);
                Assert.AreEqual(612f, widthF);
                var width = bridge.FPDF_GetPageWidth(page);
                Assert.AreEqual(612d, width);
                var heightF = bridge.FPDF_GetPageHeightF(page);
                Assert.AreEqual(792f, heightF);
                var height = bridge.FPDF_GetPageHeight(page);
                Assert.AreEqual(792d, height);

                var retBool = bridge.FPDF_GetPageBoundingBox(page, out PDFiumDelegates.FS_RECTF rect);
                Assert.IsTrue(retBool);
                Assert.AreEqual(0f, rect.Left);
                Assert.AreEqual(0f, rect.Bottom);
                Assert.AreEqual(612f, rect.Right);
                Assert.AreEqual(792f, rect.Top);

                retBool = bridge.FPDF_GetPageSizeByIndexF(document, index, out PDFiumDelegates.FS_SIZEF size);
                Assert.IsTrue(retBool);
                Assert.AreEqual(612f, size.Width);
                Assert.AreEqual(792f, size.Height);
                var retInt = bridge.FPDF_GetPageSizeByIndex(document, index, ref width, ref height);
                Assert.AreEqual(612d, width);
                Assert.AreEqual(792d, height);

                bridge.FPDF_ClosePage(page);
            }

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_RenderPageBitmap"/> and <see cref="PDFiumBridge.FPDF_RenderPageBitmapWithMatrix"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_Render_Call()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);
            var page = bridge.FPDF_LoadPage(document, 0);
            Assert.IsTrue(page.IsValid);

            var bitmap = bridge.FPDFBitmap_Create(100, 200, false);
            Assert.IsTrue(bitmap.IsValid);
            bridge.FPDF_RenderPageBitmap(bitmap, page, 0, 0, 100, 200, 0, 0);
            bridge.FPDFBitmap_Destroy(bitmap);

            bitmap = bridge.FPDFBitmap_Create(1000, 2000, false);
            Assert.IsTrue(bitmap.IsValid);

            var matrix = new PDFiumDelegates.FS_MATRIX
            {
                A = 1,
                B = 0,
                C = 0,
                D = 1,
                E = 1,
                F = 1,
            };

            var rect = new PDFiumDelegates.FS_RECTF
            {
                Left = 0,
                Right = 612,
                Top = 792,
                Bottom = 0,
            };
            bridge.FPDF_RenderPageBitmapWithMatrix(bitmap, page, ref matrix, ref rect, 0);
            bridge.FPDFBitmap_Destroy(bitmap);

            bridge.FPDF_ClosePage(page);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_DeviceToPage"/> and <see cref="PDFiumBridge.FPDF_PageToDevice"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_DeviceToPageToDevice_Call_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);
            var page = bridge.FPDF_LoadPage(document, 0);
            Assert.IsTrue(page.IsValid);

            double pageX = 0;
            double pageY = 0;
            var retVal = bridge.FPDF_DeviceToPage(page, 0, 0, 612, 792, 0, 11, 12, ref pageX, ref pageY);
            Assert.IsTrue(retVal);
            Assert.AreEqual(11d, pageX);
            Assert.AreEqual(780d, pageY);

            int deviceX = 0;
            int deviceY = 0;
            retVal = bridge.FPDF_PageToDevice(page, 0, 0, 612, 792, 0, pageX, pageY, ref deviceX, ref deviceY);
            Assert.IsTrue(retVal);

            Assert.AreEqual(11, deviceX);
            Assert.AreEqual(12, deviceY);

            bridge.FPDF_ClosePage(page);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for some bitmap methods.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_BitmapOperation_Call_Success()
        {
            const int width = 612;
            const int height = 792;
            var bridge = new PDFiumBridge();

            var bitmap = bridge.FPDFBitmap_Create(width, height, false);
            Assert.IsTrue(bitmap.IsValid);
            bridge.FPDFBitmap_FillRect(bitmap, 0, 0, width, height, new PDFiumDelegates.FPDF_COLOR(10, 20, 30));
            var format = bridge.FPDFBitmap_GetFormat(bitmap);
            Assert.AreEqual(PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGRx, format);
            var buffer = bridge.FPDFBitmap_GetBuffer(bitmap);
            Assert.IsTrue(buffer != IntPtr.Zero);
            Assert.AreEqual(width, bridge.FPDFBitmap_GetWidth(bitmap));
            Assert.AreEqual(height, bridge.FPDFBitmap_GetHeight(bitmap));
            Assert.AreEqual(2448, bridge.FPDFBitmap_GetStride(bitmap));
            bridge.FPDFBitmap_Destroy(bitmap);

            bitmap = bridge.FPDFBitmap_CreateEx(width, height, PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_Gray, IntPtr.Zero, 0);
            Assert.IsTrue(bitmap.IsValid);
            bridge.FPDFBitmap_FillRect(bitmap, 0, 0, width, height, new PDFiumDelegates.FPDF_COLOR(10, 20, 30));
            format = bridge.FPDFBitmap_GetFormat(bitmap);
            Assert.AreEqual(PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_Gray, format);
            buffer = bridge.FPDFBitmap_GetBuffer(bitmap);
            Assert.IsTrue(buffer != IntPtr.Zero);
            Assert.AreEqual(width, bridge.FPDFBitmap_GetWidth(bitmap));
            Assert.AreEqual(height, bridge.FPDFBitmap_GetHeight(bitmap));
            Assert.AreEqual(612, bridge.FPDFBitmap_GetStride(bitmap));
            bridge.FPDFBitmap_Destroy(bitmap);

            bitmap = bridge.FPDFBitmap_CreateEx(width, height, PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGR, IntPtr.Zero, 0);
            Assert.IsTrue(bitmap.IsValid);
            bridge.FPDFBitmap_FillRect(bitmap, 0, 0, width, height, new PDFiumDelegates.FPDF_COLOR(10, 20, 30));
            format = bridge.FPDFBitmap_GetFormat(bitmap);
            Assert.AreEqual(PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGR, format);
            buffer = bridge.FPDFBitmap_GetBuffer(bitmap);
            Assert.IsTrue(buffer != IntPtr.Zero);
            Assert.AreEqual(width, bridge.FPDFBitmap_GetWidth(bitmap));
            Assert.AreEqual(height, bridge.FPDFBitmap_GetHeight(bitmap));
            Assert.AreEqual(1836, bridge.FPDFBitmap_GetStride(bitmap));
            bridge.FPDFBitmap_Destroy(bitmap);

            bitmap = bridge.FPDFBitmap_CreateEx(width, height, PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGRx, IntPtr.Zero, 0);
            Assert.IsTrue(bitmap.IsValid);
            bridge.FPDFBitmap_FillRect(bitmap, 0, 0, width, height, new PDFiumDelegates.FPDF_COLOR(10, 20, 30));
            format = bridge.FPDFBitmap_GetFormat(bitmap);
            Assert.AreEqual(PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGRx, format);
            buffer = bridge.FPDFBitmap_GetBuffer(bitmap);
            Assert.IsTrue(buffer != IntPtr.Zero);
            Assert.AreEqual(width, bridge.FPDFBitmap_GetWidth(bitmap));
            Assert.AreEqual(height, bridge.FPDFBitmap_GetHeight(bitmap));
            Assert.AreEqual(2448, bridge.FPDFBitmap_GetStride(bitmap));
            bridge.FPDFBitmap_Destroy(bitmap);

            bitmap = bridge.FPDFBitmap_CreateEx(width, height, PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGRA, IntPtr.Zero, 0);
            Assert.IsTrue(bitmap.IsValid);
            bridge.FPDFBitmap_FillRect(bitmap, 0, 0, width, height, new PDFiumDelegates.FPDF_COLOR(10, 20, 30));
            format = bridge.FPDFBitmap_GetFormat(bitmap);
            Assert.AreEqual(PDFiumDelegates.FPDFBitmapFormat.FPDFBitmap_BGRA, format);
            buffer = bridge.FPDFBitmap_GetBuffer(bitmap);
            Assert.IsTrue(buffer != IntPtr.Zero);
            Assert.AreEqual(width, bridge.FPDFBitmap_GetWidth(bitmap));
            Assert.AreEqual(height, bridge.FPDFBitmap_GetHeight(bitmap));
            Assert.AreEqual(2448, bridge.FPDFBitmap_GetStride(bitmap));
            bridge.FPDFBitmap_Destroy(bitmap);

            bridge.Dispose();
        }

        /// <summary>
        /// Test method for some view ref methods.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_ViewerRef()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            var retVal1 = bridge.FPDF_VIEWERREF_GetPrintScaling(document);
            var retVal2 = bridge.FPDF_VIEWERREF_GetNumCopies(document);
            var retVal3 = bridge.FPDF_VIEWERREF_GetPrintPageRange(document);
            var retVal4 = bridge.FPDF_VIEWERREF_GetPrintPageRangeCount(retVal3);
            var retVal5 = bridge.FPDF_VIEWERREF_GetPrintPageRangeElement(retVal3, 0);
            var retVal6 = bridge.FPDF_VIEWERREF_GetDuplex(document);

            var retVal7 = bridge.FPDF_VIEWERREF_GetName(document, "ViewArea", IntPtr.Zero, 0);
            retVal7 = bridge.FPDF_VIEWERREF_GetName(document, "Direction", IntPtr.Zero, 0);

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_GetNamedDest"/> and <see cref="PDFiumBridge.FPDF_GetNamedDestByName"/>.
        /// </summary>
        [TestMethod]
        public void FPDFVIEW_NamedDest()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            var dests = bridge.FPDF_CountNamedDests(document);
            for (var index = 0; index < dests; index++)
            {
                int len = 0;
                bridge.FPDF_GetNamedDest(document, index, IntPtr.Zero, ref len);
                IntPtr buffer = Marshal.AllocHGlobal(len);
                var dest = bridge.FPDF_GetNamedDest(document, index, buffer, ref len);
                var name = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);
                Assert.IsNotNull(name);
                Assert.IsTrue(dest.IsValid);

                var dest1 = bridge.FPDF_GetNamedDestByName(document, name);
                Assert.IsTrue(dest1.IsValid);
            }

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }
    }
}
