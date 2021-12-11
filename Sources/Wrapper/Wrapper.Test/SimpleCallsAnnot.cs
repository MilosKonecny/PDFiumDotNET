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
    /// Unit test class implements some unit test methods for functions defined in fpdf_annot.h.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class SimpleCallsAnnot
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
        public void PDFiumBridge_FPDFAnnotIsSupportedSubtype_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_IsSupportedSubtype(FPDF_ANNOTATION_SUBTYPE.FPDF_ANNOT_FREETEXT);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageCreateAnnot_SimpleCall_NoException()
        {
            _bridge.FPDFPage_CreateAnnot(FPDF_PAGE.InvalidHandle, FPDF_ANNOTATION_SUBTYPE.FPDF_ANNOT_FREETEXT);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageGetAnnotCount_SimpleCall_NoException()
        {
            _bridge.FPDFPage_GetAnnotCount(FPDF_PAGE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageGetAnnot_SimpleCall_NoException()
        {
            _bridge.FPDFPage_GetAnnot(FPDF_PAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageGetAnnotIndex_SimpleCall_NoException()
        {
            _bridge.FPDFPage_GetAnnotIndex(FPDF_PAGE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageCloseAnnot_SimpleCall_NoException()
        {
            _bridge.FPDFPage_CloseAnnot(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageRemoveAnnot_SimpleCall_NoException()
        {
            _bridge.FPDFPage_RemoveAnnot(FPDF_PAGE.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetSubtype_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetSubtype(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotIsObjectSupportedSubtype_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_IsObjectSupportedSubtype(FPDF_ANNOTATION_SUBTYPE.FPDF_ANNOT_FREETEXT);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotUpdateObject_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_UpdateObject(FPDF_ANNOTATION.InvalidHandle, FPDF_PAGEOBJECT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotAddInkStroke_SimpleCall_NoException()
        {
            var points = new FS_POINTF[2];
            _bridge.FPDFAnnot_AddInkStroke(FPDF_ANNOTATION.InvalidHandle, ref points, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotRemoveInkList_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_RemoveInkList(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotAppendObject_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_AppendObject(FPDF_ANNOTATION.InvalidHandle, FPDF_PAGEOBJECT.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetObjectCount_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetObjectCount(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetObject_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetObject(FPDF_ANNOTATION.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotRemoveObject_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_RemoveObject(FPDF_ANNOTATION.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetColor_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetColor(FPDF_ANNOTATION.InvalidHandle, FPDFANNOT_COLORTYPES.FPDFANNOT_COLORTYPE_Color, 0, 0, 0, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetColor_SimpleCall_NoException()
        {
            uint r, g, b, a;
            r = g = b = a = 0;
            _bridge.FPDFAnnot_GetColor(FPDF_ANNOTATION.InvalidHandle, FPDFANNOT_COLORTYPES.FPDFANNOT_COLORTYPE_Color, ref r, ref g, ref b, ref a);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotHasAttachmentPoints_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_HasAttachmentPoints(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetAttachmentPoints_SimpleCall_NoException()
        {
            var points = new FS_QUADPOINTSF();
            _bridge.FPDFAnnot_SetAttachmentPoints(FPDF_ANNOTATION.InvalidHandle, 0, ref points);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotAppendAttachmentPoints_SimpleCall_NoException()
        {
            var points = new FS_QUADPOINTSF();
            _bridge.FPDFAnnot_AppendAttachmentPoints(FPDF_ANNOTATION.InvalidHandle, ref points);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotCountAttachmentPoints_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_CountAttachmentPoints(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetAttachmentPoints_SimpleCall_NoException()
        {
            var points = new FS_QUADPOINTSF();
            _bridge.FPDFAnnot_GetAttachmentPoints(FPDF_ANNOTATION.InvalidHandle, 0, ref points);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetRect_SimpleCall_NoException()
        {
            var rect = new FS_RECTF();
            _bridge.FPDFAnnot_SetRect(FPDF_ANNOTATION.InvalidHandle, ref rect);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetRect_SimpleCall_NoException()
        {
            var rect = new FS_RECTF();
            _bridge.FPDFAnnot_GetRect(FPDF_ANNOTATION.InvalidHandle, ref rect);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetVertices_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetVertices(FPDF_ANNOTATION.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetInkListCount_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetInkListCount(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetInkListPath_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetInkListPath(FPDF_ANNOTATION.InvalidHandle, 0, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetLine_SimpleCall_NoException()
        {
            var start = new FS_POINTF();
            var end = new FS_POINTF();
            _bridge.FPDFAnnot_GetLine(FPDF_ANNOTATION.InvalidHandle, ref start, ref end);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetBorder_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetBorder(FPDF_ANNOTATION.InvalidHandle, 0, 0, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetBorder_SimpleCall_NoException()
        {
            float horizontal_radius, vertical_radius, border_width;
            horizontal_radius = vertical_radius = border_width = 0;
            _bridge.FPDFAnnot_GetBorder(FPDF_ANNOTATION.InvalidHandle, ref horizontal_radius, ref vertical_radius, ref border_width);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotHasKey_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_HasKey(FPDF_ANNOTATION.InvalidHandle, string.Empty);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetValueType_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetValueType(FPDF_ANNOTATION.InvalidHandle, string.Empty);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetStringValue_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetStringValue(FPDF_ANNOTATION.InvalidHandle, string.Empty, IntPtr.Zero);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetStringValue_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetStringValue(FPDF_ANNOTATION.InvalidHandle, string.Empty, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetNumberValue_SimpleCall_NoException()
        {
            var value = 0f;
            _bridge.FPDFAnnot_GetNumberValue(FPDF_ANNOTATION.InvalidHandle, string.Empty, ref value);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetAP_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetAP(FPDF_ANNOTATION.InvalidHandle, FPDF_ANNOT_APPEARANCEMODES.FPDF_ANNOT_APPEARANCEMODE_NORMAL, IntPtr.Zero);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetAP_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetAP(FPDF_ANNOTATION.InvalidHandle, FPDF_ANNOT_APPEARANCEMODES.FPDF_ANNOT_APPEARANCEMODE_NORMAL, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetLinkedAnnot_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetLinkedAnnot(FPDF_ANNOTATION.InvalidHandle, string.Empty);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFlags_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFlags(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetFlags_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetFlags(FPDF_ANNOTATION.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormFieldFlags_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormFieldFlags(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormFieldAtPoint_SimpleCall_NoException()
        {
            var point = new FS_POINTF();
            _bridge.FPDFAnnot_GetFormFieldAtPoint(FPDF_FORMHANDLE.InvalidHandle, FPDF_PAGE.InvalidHandle, ref point);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormFieldName_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormFieldName(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormFieldType_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormFieldType(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormFieldValue_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormFieldValue(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetOptionCount_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetOptionCount(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetOptionLabel_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetOptionLabel(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle, 0, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotIsOptionSelected_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_IsOptionSelected(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFontSize_SimpleCall_NoException()
        {
            var value = 0f;
            _bridge.FPDFAnnot_GetFontSize(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle, ref value);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotIsChecked_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_IsChecked(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetFocusableSubtypes_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetFocusableSubtypes(FPDF_FORMHANDLE.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFocusableSubtypesCount_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFocusableSubtypesCount(FPDF_FORMHANDLE.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFocusableSubtypes_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFocusableSubtypes(FPDF_FORMHANDLE.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetLink_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetLink(FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormControlCount_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormControlCount(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormControlIndex_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormControlIndex(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotGetFormFieldExportValue_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_GetFormFieldExportValue(FPDF_FORMHANDLE.InvalidHandle, FPDF_ANNOTATION.InvalidHandle, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> method.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFAnnotSetURI_SimpleCall_NoException()
        {
            _bridge.FPDFAnnot_SetURI(FPDF_ANNOTATION.InvalidHandle, string.Empty);
        }
    }
}
