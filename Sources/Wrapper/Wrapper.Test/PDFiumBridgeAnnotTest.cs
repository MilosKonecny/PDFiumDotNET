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
    /// Test class for methods defined in 'PDFiumBridge.Annot.cs' file.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class PDFiumBridgeAnnotTest
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
        /// Test method for <see cref="PDFiumBridge.FPDFPage_GetAnnotCount"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageGetAnnotCount_TestCount_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Annotations.pdf");
            var bridge = new PDFiumBridge();

            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            var page = bridge.FPDF_LoadPage(document, 0);
            Assert.IsTrue(page.IsValid);

            var count = bridge.FPDFPage_GetAnnotCount(page);
            Assert.AreEqual(21, count);

            bridge.FPDF_ClosePage(page);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDFPage_GetAnnot"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_FPDFPageGetAnnot_TestForExistence_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Annotations.pdf");
            var bridge = new PDFiumBridge();

            var document = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsTrue(document.IsValid);

            var page = bridge.FPDF_LoadPage(document, 0);
            Assert.IsTrue(page.IsValid);

            var count = bridge.FPDFPage_GetAnnotCount(page);
            for (var index = 0; index < count; index++)
            {
                var annot = bridge.FPDFPage_GetAnnot(page, index);
                Assert.IsTrue(annot.IsValid);
                var realIndex = bridge.FPDFPage_GetAnnotIndex(page, annot);
                Assert.AreEqual(index, realIndex);
                var subtype = bridge.FPDFAnnot_GetSubtype(annot);
                Assert.IsTrue(subtype != FPDF_ANNOTATION_SUBTYPE.FPDF_ANNOT_UNKNOWN);

                var objectCount = bridge.FPDFAnnot_GetObjectCount(annot);
                for (var objectIndex = 0; objectIndex < objectCount; objectIndex++)
                {
                    var obj = bridge.FPDFAnnot_GetObject(annot, objectIndex);
                    Assert.IsTrue(obj.IsValid);
                }

                var pointsCount = bridge.FPDFAnnot_CountAttachmentPoints(annot);
                for (var pointsIndex = 0; pointsIndex < pointsCount; pointsIndex++)
                {
                    var points = new FS_QUADPOINTSF();
                    Assert.IsTrue(bridge.FPDFAnnot_GetAttachmentPoints(annot, pointsIndex, ref points));
                }

                var rect = new FS_RECTF();
                Assert.IsTrue(bridge.FPDFAnnot_GetRect(annot, ref rect));

                var verticesCount = bridge.FPDFAnnot_GetVertices(annot, IntPtr.Zero, 0);
                if (verticesCount != 0)
                {
                    var points = new FS_POINTF[verticesCount];
                    var pointsIntPtr = NativeMethods.StructureArrayToIntPtr(points);
                    Assert.AreEqual(verticesCount, bridge.FPDFAnnot_GetVertices(annot, pointsIntPtr, verticesCount));
                    Marshal.FreeHGlobal(pointsIntPtr);
                }

                float horizontal_radius, vertical_radius, border_width;
                horizontal_radius = vertical_radius = border_width = -1f;
                if (bridge.FPDFAnnot_GetBorder(annot, ref horizontal_radius, ref vertical_radius, ref border_width))
                {
                    Assert.IsTrue(horizontal_radius != -1f);
                    Assert.IsTrue(vertical_radius != -1f);
                    Assert.IsTrue(border_width != -1f);
                }

                bridge.FPDFPage_CloseAnnot(annot);
            }

            bridge.FPDF_ClosePage(page);
            bridge.FPDF_CloseDocument(document);
            bridge.Dispose();
        }
    }
}
