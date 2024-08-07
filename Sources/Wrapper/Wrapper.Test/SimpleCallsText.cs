namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements some unit test methods for functions defined in fpdf_text.h.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class SimpleCallsText
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
        public void PDFiumBridge_FPDFBookmarkGetFirstChild_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_GetFirstChild(FPDF_DOCUMENT.InvalidHandle, FPDF_BOOKMARK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextLoadPage_SimpleCall_NoException()
        {
            _bridge.FPDFText_LoadPage(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextClosePage_SimpleCall_NoException()
        {
            _bridge.FPDFText_ClosePage(FPDF_TEXTPAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextCountChars_SimpleCall_NoException()
        {
            _bridge.FPDFText_CountChars(FPDF_TEXTPAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetUnicode_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetUnicode(FPDF_TEXTPAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetFontSize_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetFontSize(FPDF_TEXTPAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetFontInfo_SimpleCall_NoException()
        {
            int flags = 0;
            _bridge.FPDFText_GetFontInfo(FPDF_TEXTPAGE.InvalidHandle, 0, IntPtr.Zero, 0, ref flags);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetFontWeight_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetFontWeight(FPDF_TEXTPAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetFillColor_SimpleCall_NoException()
        {
            uint r, g, b, a;
            r = g = b = a = 0;
            _bridge.FPDFText_GetFillColor(FPDF_TEXTPAGE.InvalidHandle, 0, ref r, ref g, ref b, ref a);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetStrokeColor_SimpleCall_NoException()
        {
            ulong r, g, b, a;
            r = g = b = a = 0;
            _bridge.FPDFText_GetStrokeColor(FPDF_TEXTPAGE.InvalidHandle, 0, ref r, ref g, ref b, ref a);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetCharAngle_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetCharAngle(FPDF_TEXTPAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetCharBox_SimpleCall_NoException()
        {
            double left, right, bottom, top;
            left = right = bottom = top = 0d;
            _bridge.FPDFText_GetCharBox(FPDF_TEXTPAGE.InvalidHandle, 0, ref left, ref right, ref bottom, ref top);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetLooseCharBox_SimpleCall_NoException()
        {
            var rect = new FS_RECTF();
            _bridge.FPDFText_GetLooseCharBox(FPDF_TEXTPAGE.InvalidHandle, 0, ref rect);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetMatrix_SimpleCall_NoException()
        {
            var matrix = new FS_MATRIX();
            _bridge.FPDFText_GetMatrix(FPDF_TEXTPAGE.InvalidHandle, 0, ref matrix);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetCharOrigin_SimpleCall_NoException()
        {
            double x, y;
            x = y = 0d;
            _bridge.FPDFText_GetCharOrigin(FPDF_TEXTPAGE.InvalidHandle, 0, ref x, ref y);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetCharIndexAtPos_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetCharIndexAtPos(FPDF_TEXTPAGE.InvalidHandle, 0d, 0d, 0d, 0d);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetText_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetText(FPDF_TEXTPAGE.InvalidHandle, 0, 0, IntPtr.Zero);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextCountRects_SimpleCall_NoException()
        {
            _bridge.FPDFText_CountRects(FPDF_TEXTPAGE.InvalidHandle, 0, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetRect_SimpleCall_NoException()
        {
            double left, top, right, bottom;
            left = top = right = bottom = 0d;
            _bridge.FPDFText_GetRect(FPDF_TEXTPAGE.InvalidHandle, 0, ref left, ref top, ref right, ref bottom);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetBoundedText_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetBoundedText(FPDF_TEXTPAGE.InvalidHandle, 0d, 0d, 0d, 0d, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextFindStart_SimpleCall_NoException()
        {
            _bridge.FPDFText_FindStart(FPDF_TEXTPAGE.InvalidHandle, IntPtr.Zero, FPDF_FIND_FLAGS.FPDF_NONE, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextFindNext_SimpleCall_NoException()
        {
            _bridge.FPDFText_FindNext(FPDF_SCHHANDLE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextFindPrev_SimpleCall_NoException()
        {
            _bridge.FPDFText_FindPrev(FPDF_SCHHANDLE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextGetSchResultIndex_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetSchResultIndex(FPDF_SCHHANDLE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFText_GetSchCount_SimpleCall_NoException()
        {
            _bridge.FPDFText_GetSchCount(FPDF_SCHHANDLE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFTextFindClose_SimpleCall_NoException()
        {
            _bridge.FPDFText_FindClose(FPDF_SCHHANDLE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkLoadWebLinks_SimpleCall_NoException()
        {
            _bridge.FPDFLink_LoadWebLinks(FPDF_TEXTPAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkCountWebLinks_SimpleCall_NoException()
        {
            _bridge.FPDFLink_CountWebLinks(FPDF_PAGELINK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetURL_SimpleCall_NoException()
        {
            _bridge.FPDFLink_GetURL(FPDF_PAGELINK.InvalidHandle, 0, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkCountRects_SimpleCall_NoException()
        {
            _bridge.FPDFLink_CountRects(FPDF_PAGELINK.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetRect_SimpleCall_NoException()
        {
            double left, top, right, bottom;
            left = top = right = bottom = 0d;
            _bridge.FPDFLink_GetRect(FPDF_PAGELINK.InvalidHandle, 0, 0, ref left, ref top, ref right, ref bottom);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetTextRange_SimpleCall_NoException()
        {
            int start_char_index, char_count;
            start_char_index = char_count = 0;
            _bridge.FPDFLink_GetTextRange(FPDF_PAGELINK.InvalidHandle, 0, ref start_char_index, ref char_count);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkCloseWebLinks_SimpleCall_NoException()
        {
            _bridge.FPDFLink_CloseWebLinks(FPDF_PAGELINK.InvalidHandle);
        }
    }
}
