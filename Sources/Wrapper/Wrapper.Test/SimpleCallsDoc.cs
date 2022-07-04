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
    /// Unit test class implements some unit test methods for functions defined in fpdf_doc.h.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class SimpleCallsDoc
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
        public void PDFiumBridge_FPDFBookmarkGetNextSibling_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_GetNextSibling(FPDF_DOCUMENT.InvalidHandle, FPDF_BOOKMARK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBookmarkGetTitle_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_GetTitle(FPDF_BOOKMARK.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBookmarkGetCount_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_GetCount(FPDF_BOOKMARK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBookmarkFind_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_Find(FPDF_DOCUMENT.InvalidHandle, IntPtr.Zero);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBookmarkGetDest_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_GetDest(FPDF_DOCUMENT.InvalidHandle, FPDF_BOOKMARK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFBookmarkGetAction_SimpleCall_NoException()
        {
            _bridge.FPDFBookmark_GetAction(FPDF_BOOKMARK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFActionGetType_SimpleCall_NoException()
        {
            _bridge.FPDFAction_GetType(FPDF_ACTION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFActionGetDest_SimpleCall_NoException()
        {
            _bridge.FPDFAction_GetDest(FPDF_DOCUMENT.InvalidHandle, FPDF_ACTION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFActionGetFilePath_SimpleCall_NoException()
        {
            _bridge.FPDFAction_GetFilePath(FPDF_ACTION.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFActionGetURIPath_SimpleCall_NoException()
        {
            _bridge.FPDFAction_GetURIPath(FPDF_DOCUMENT.InvalidHandle, FPDF_ACTION.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFDestGetDestPageIndex_SimpleCall_NoException()
        {
            _bridge.FPDFDest_GetDestPageIndex(FPDF_DOCUMENT.InvalidHandle, FPDF_DEST.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFDestGetView_SimpleCall_NoException()
        {
            ulong numParams = 0;
            _bridge.FPDFDest_GetView(FPDF_DEST.InvalidHandle, ref numParams, IntPtr.Zero);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFDestGetLocationInPage_SimpleCall_NoException()
        {
            _bridge.FPDFDest_GetLocationInPage(FPDF_DEST.InvalidHandle, out bool hasXVal, out bool hasYVal, out bool hasZoomVal, out float x, out float y, out float zoom);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetLinkAtPoint_SimpleCall_NoException()
        {
            _bridge.FPDFLink_GetLinkAtPoint(FPDF_PAGE.InvalidHandle, 0d, 0d);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetLinkZOrderAtPoint_SimpleCall_NoException()
        {
            _bridge.FPDFLink_GetLinkZOrderAtPoint(FPDF_PAGE.InvalidHandle, 0d, 0d);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetDest_SimpleCall_NoException()
        {
            _bridge.FPDFLink_GetDest(FPDF_DOCUMENT.InvalidHandle, FPDF_LINK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetAction_SimpleCall_NoException()
        {
            _bridge.FPDFLink_GetAction(FPDF_LINK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkEnumerate_SimpleCall_NoException()
        {
            int start_pos = 0;
            var link_annot = new FPDF_LINK();
            _bridge.FPDFLink_Enumerate(FPDF_PAGE.InvalidHandle, ref start_pos, ref link_annot);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetAnnot_SimpleCall_NoException()
        {
            _bridge.FPDFLink_GetAnnot(FPDF_PAGE.InvalidHandle, FPDF_LINK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetAnnotRect_SimpleCall_NoException()
        {
            var rect = new FS_RECTF();
            _bridge.FPDFLink_GetAnnotRect(FPDF_LINK.InvalidHandle, ref rect);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkCountQuadPoints_SimpleCall_NoException()
        {
            // ToDo: Unit test temporary deactivated
            // Some test is missing in PDFium
            // Exception thrown: read access violation.
            //      **std::__1::__tree < std::__1::__value_type<fxcrt::ByteString, fxcrt::RetainPtr<CPDF_Object>>,std::__1::__map_value_compare < fxcrt::ByteString,std::__1::__value_type<fxcrt::ByteString, fxcrt::RetainPtr<CPDF_Object>>,std::__1::less<fxcrt::ByteString>,1 >,std::__1::allocator < std::__1::__value_type < fxcrt::ByteString,fxcrt::RetainPtr < CPDF_Object > > > >::__end_node * *(...) returned 0x30.
            // Call stack
            //      pdfium.dll!std::__1::__tree < std::__1::__value_type<fxcrt::ByteString, fxcrt::RetainPtr<CPDF_Object>>,std::__1::__map_value_compare < fxcrt::ByteString,std::__1::__value_type<fxcrt::ByteString, fxcrt::RetainPtr<CPDF_Object>>,std::__1::less<fxcrt::ByteString>,1 >,std::__1::allocator < std::__1::__value_type < fxcrt::ByteString,fxcrt::RetainPtr < CPDF_Object >>>>::__root() Line 1079  C++
            //      pdfium.dll!std::__1::__tree < std::__1::__value_type<fxcrt::ByteString, fxcrt::RetainPtr<CPDF_Object>>,std::__1::__map_value_compare < fxcrt::ByteString,std::__1::__value_type<fxcrt::ByteString, fxcrt::RetainPtr<CPDF_Object>>,std::__1::less<fxcrt::ByteString>,1 >,std::__1::allocator < std::__1::__value_type < fxcrt::ByteString,fxcrt::RetainPtr < CPDF_Object >>>>::find<fxcrt::ByteString>(const fxcrt::ByteString &__v) Line 2477    C++
            //      pdfium.dll!std::__1::map < fxcrt::ByteString,fxcrt::RetainPtr<CPDF_Object>,std::__1::less<fxcrt::ByteString>,std::__1::allocator < std::__1::pair <const fxcrt::ByteString, fxcrt::RetainPtr < CPDF_Object >>>>::find(const fxcrt::ByteString &__k) Line 1393   C++
            //      pdfium.dll!CPDF_Dictionary::GetObjectFor(const fxcrt::ByteString &key) Line 87 C++
            //      pdfium.dll!CPDF_Dictionary::GetDirectObjectFor(const fxcrt::ByteString &key) Line 98   C++
            //      pdfium.dll!CPDF_Dictionary::GetDirectObjectFor(const fxcrt::ByteString &key) Line 103  C++
            //      pdfium.dll!CPDF_Dictionary::GetArrayFor(const fxcrt::ByteString &key) Line 173 C++
            //      pdfium.dll!GetQuadPointsArrayFromDictionary(CPDF_Dictionary * dict) Line 229 C++
            //      pdfium.dll!FPDFLink_CountQuadPoints(fpdf_link_t__ * link_annot) Line 394    C++
            ////_bridge.FPDFLink_CountQuadPoints(FPDF_LINK.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFLinkGetQuadPoints_SimpleCall_NoException()
        {
            var points = new FS_QUADPOINTSF();
            _bridge.FPDFLink_GetQuadPoints(FPDF_LINK.InvalidHandle, 0, ref points);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageAAction_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageAAction(FPDF_PAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetFileIdentifier_SimpleCall_NoException()
        {
            _bridge.FPDF_GetFileIdentifier(FPDF_DOCUMENT.InvalidHandle, FPDF_FILEIDTYPE.FILEIDTYPE_PERMANENT, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetMetaText_SimpleCall_NoException()
        {
            _bridge.FPDF_GetMetaText(FPDF_DOCUMENT.InvalidHandle, "tag", IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFGetPageLabel_SimpleCall_NoException()
        {
            _bridge.FPDF_GetPageLabel(FPDF_DOCUMENT.InvalidHandle, 0, IntPtr.Zero, 0);
        }
    }
}
