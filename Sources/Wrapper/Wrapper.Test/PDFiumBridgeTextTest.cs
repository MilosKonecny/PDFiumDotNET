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
    /// Test class for methods defined in 'PDFiumBridge.Text.cs' file.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class PDFiumBridgeTextTest
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
        /// Test method for text methods.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFText_IteratePages_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Annotations.pdf");
            var bridge = new PDFiumBridge();

            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            int pageCount = bridge.FPDF_GetPageCount(document);
            Assert.IsTrue(pageCount > 0);

            for (var index = 0; index < pageCount; index++)
            {
                var page = bridge.FPDF_LoadPage(document, 0);
                Assert.IsTrue(page.IsValid);

                var textPage = bridge.FPDFText_LoadPage(page);
                Assert.IsTrue(textPage.IsValid);

                var charsCount = bridge.FPDFText_CountChars(textPage);
                var globalText = Marshal.AllocHGlobal((2 * charsCount) + 2);

                var writtenChars = bridge.FPDFText_GetText(textPage, 0, charsCount, globalText);
                Assert.AreEqual(charsCount, writtenChars - 1);

                var text = Marshal.PtrToStringUni(globalText);
                Assert.AreEqual(charsCount, text.Length);

                Marshal.FreeHGlobal(globalText);

                var rectCounts = bridge.FPDFText_CountRects(textPage, 0, charsCount);
                for (var rectIndex = 0; rectIndex < rectCounts; rectIndex++)
                {
                    double left, top, right, bottom;
                    left = top = right = bottom = 0d;
                    Assert.IsTrue(bridge.FPDFText_GetRect(textPage, rectIndex, ref left, ref top, ref right, ref bottom));
                }

                bridge.FPDFText_ClosePage(textPage);
                bridge.FPDF_ClosePage(page);
            }

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for text methods.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFText_IterateWebLinks_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var bridge = new PDFiumBridge();

            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            int pageCount = bridge.FPDF_GetPageCount(document);
            Assert.IsTrue(pageCount > 0);

            for (var index = 0; index < pageCount; index++)
            {
                var page = bridge.FPDF_LoadPage(document, 0);
                Assert.IsTrue(page.IsValid);

                var textPage = bridge.FPDFText_LoadPage(page);
                Assert.IsTrue(textPage.IsValid);

                var linkPage = bridge.FPDFLink_LoadWebLinks(textPage);
                Assert.IsTrue(linkPage.IsValid);

                var linkCount = bridge.FPDFLink_CountWebLinks(linkPage);

                for (var linkIndex = 0; linkIndex < linkCount; linkIndex++)
                {
                    var charsCount = bridge.FPDFLink_GetURL(linkPage, linkIndex, IntPtr.Zero, 0);
                    Assert.IsTrue(charsCount > 0);

                    var globalText = Marshal.AllocHGlobal(2 * charsCount);

                    var writtenChars = bridge.FPDFLink_GetURL(linkPage, linkIndex, globalText, charsCount);
                    Assert.AreEqual(charsCount, writtenChars);

                    var text = Marshal.PtrToStringUni(globalText);
                    Assert.AreEqual(charsCount, text.Length + 1);

                    Marshal.FreeHGlobal(globalText);

                    var rectCount = bridge.FPDFLink_CountRects(linkPage, linkIndex);
                    for (var rectIndex = 0; rectIndex < rectCount; rectIndex++)
                    {
                        double left, top, right, bottom;
                        left = top = right = bottom = 0d;
                        Assert.IsTrue(bridge.FPDFLink_GetRect(linkPage, linkIndex, rectIndex, ref left, ref top, ref right, ref bottom));
                    }
                }

                bridge.FPDFLink_CloseWebLinks(linkPage);
                bridge.FPDFText_ClosePage(textPage);
                bridge.FPDF_ClosePage(page);
            }

            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }
    }
}
