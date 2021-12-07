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
    /// Test class for methods defined in 'PDFiumBridge.Doc.cs' file.
    /// </summary>
    [TestClass]
    public class PDFiumBridgeDocTest
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

        #region Methods for bookmark find

        private static string BookmarkTitle(PDFiumBridge bridge, FPDF_BOOKMARK bookmark)
        {
            var len = bridge.FPDFBookmark_GetTitle(bookmark, IntPtr.Zero, 0);
            var buffer = Marshal.AllocHGlobal(len);
            var returnedLen = bridge.FPDFBookmark_GetTitle(bookmark, buffer, (ulong)len);
            Assert.AreEqual(len, returnedLen);
            var title = Marshal.PtrToStringUni(buffer);
            Marshal.FreeHGlobal(buffer);
            return title;
        }

        private static void CheckBookmark(PDFiumBridge bridge, FPDF_DOCUMENT document, FPDF_BOOKMARK bookmark)
        {
            var title = BookmarkTitle(bridge, bookmark);
            Assert.IsFalse(string.IsNullOrEmpty(title));

            var action = bridge.FPDFBookmark_GetAction(bookmark);
            Assert.IsTrue(action.IsValid);
            var actionDestination = bridge.FPDFAction_GetDest(document, action);
            Assert.IsTrue(actionDestination.IsValid);
            var actionDestinationPage = bridge.FPDFDest_GetDestPageIndex(document, actionDestination);
            var resultA = bridge.FPDFDest_GetLocationInPage(actionDestination, out bool hasXValA, out bool hasYValA, out bool hasZoomValA, out float xA, out float yA, out float zoomA);
            Assert.IsTrue(resultA);

            var destination = bridge.FPDFBookmark_GetDest(document, bookmark);
            Assert.IsTrue(destination.IsValid);
            var destinationPage = bridge.FPDFDest_GetDestPageIndex(document, destination);
            var resultB = bridge.FPDFDest_GetLocationInPage(actionDestination, out bool hasXValB, out bool hasYValB, out bool hasZoomValB, out float xB, out float yB, out float zoomB);
            Assert.IsTrue(resultB);

            Assert.AreEqual(hasXValA, hasXValB);
            Assert.AreEqual(hasYValA, hasYValB);
            Assert.AreEqual(hasZoomValA, hasZoomValB);
            Assert.AreEqual(xA, xB);
            Assert.AreEqual(yA, yB);
            Assert.AreEqual(zoomA, zoomB);

            Assert.AreEqual(actionDestinationPage, destinationPage);
        }

        private static void FindFirstChild(
            PDFiumBridge bridge,
            FPDF_DOCUMENT document,
            FPDF_BOOKMARK bookmarkParent,
            ref int countOfBookmarks)
        {
            var firstChild = bridge.FPDFBookmark_GetFirstChild(document, bookmarkParent);
            if (firstChild.IsValid)
            {
                countOfBookmarks++;
                CheckBookmark(bridge, document, firstChild);

                FindFirstChild(bridge, document, firstChild, ref countOfBookmarks);
                FindSibling(bridge, document, firstChild, ref countOfBookmarks);
            }
        }

        private static void FindSibling(
            PDFiumBridge bridge,
            FPDF_DOCUMENT document,
            FPDF_BOOKMARK bookmarkPrevSibling,
            ref int countOfBookmarks)
        {
            var nextSibling = bridge.FPDFBookmark_GetNextSibling(document, bookmarkPrevSibling);
            if (nextSibling.IsValid)
            {
                countOfBookmarks++;
                CheckBookmark(bridge, document, nextSibling);

                FindFirstChild(bridge, document, nextSibling, ref countOfBookmarks);
                FindSibling(bridge, document, nextSibling, ref countOfBookmarks);
            }
        }

        #endregion Methods for bookmark find

        /// <summary>
        /// Base test for bookmark.
        /// </summary>
        [TestMethod]
        public void FPDFDOC_BookmarkGetChildAndSibling_FindAll_DataConsistent()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();
            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            var countOfBookmarks = 0;
            FindFirstChild(bridge, document, FPDF_BOOKMARK.InvalidHandle, ref countOfBookmarks);

            Assert.AreEqual(218, countOfBookmarks);

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }
    }
}
