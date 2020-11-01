namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;

    /// <summary>
    /// Test class for methods defined in 'PDFiumBridge.Doc.cs' file.
    /// </summary>
    [TestClass]
    public class PDFiumBridgeDocTest
    {
        private static TestContext _testContext;
        private static string _pdfFilesFolder;

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

        private void FindFirstChild(PDFiumBridge bridge, PDFiumDelegates.FPDF_DOCUMENT document, PDFiumDelegates.FPDF_BOOKMARK bookmark, int indent)
        {
            var firstChild = bridge.FPDFBookmark_GetFirstChild(document, bookmark);
            if (firstChild.IsValid)
            {
                var firstChildLen = bridge.FPDFBookmark_GetTitle(firstChild, IntPtr.Zero, 0);
                var buffer = Marshal.AllocHGlobal(firstChildLen);
                var returnedLen = bridge.FPDFBookmark_GetTitle(firstChild, buffer, (ulong)firstChildLen);
                Assert.AreEqual(firstChildLen, returnedLen);
                var title = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);
                FindFirstChild(bridge, document, firstChild, indent + 1);
                FindSibling(bridge, document, firstChild, indent);
            }
        }

        private void FindSibling(PDFiumBridge bridge, PDFiumDelegates.FPDF_DOCUMENT document, PDFiumDelegates.FPDF_BOOKMARK bookmark, int indent)
        {
            var sibling = bridge.FPDFBookmark_GetNextSibling(document, bookmark);
            if (sibling.IsValid)
            {
                var siblingLen = bridge.FPDFBookmark_GetTitle(sibling, IntPtr.Zero, 0);
                var buffer = Marshal.AllocHGlobal(siblingLen);
                var returnedLen = bridge.FPDFBookmark_GetTitle(sibling, buffer, (ulong)siblingLen);
                Assert.AreEqual(siblingLen, returnedLen);
                var title = Marshal.PtrToStringUni(buffer);
                Marshal.FreeHGlobal(buffer);
                FindFirstChild(bridge, document, sibling, indent + 1);
                FindSibling(bridge, document, sibling, indent);
            }
        }

        /// <summary>
        /// Base test for bookmark.
        /// </summary>
        [TestMethod]
        public void Bookmark()
        {
            var indent = 1;
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            var bookmark = bridge.FPDFBookmark_GetFirstChild(document, new PDFiumDelegates.FPDF_BOOKMARK(IntPtr.Zero));
            Assert.IsTrue(bookmark.IsValid);
            var len = bridge.FPDFBookmark_GetTitle(bookmark, IntPtr.Zero, 0);
            var buffer = Marshal.AllocHGlobal(len);
            len = bridge.FPDFBookmark_GetTitle(bookmark, buffer, (ulong)len);
            var title = Marshal.PtrToStringUni(buffer);
            Marshal.FreeHGlobal(buffer);
            FindFirstChild(bridge, document, bookmark, indent + 1);
            FindSibling(bridge, document, bookmark, indent);

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }
    }
}
