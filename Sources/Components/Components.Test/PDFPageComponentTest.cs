namespace PDFiumDotNET.Components.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Page;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class for <see cref="PDFPageComponent"/>.
    /// </summary>
    [TestClass]
    public class PDFPageComponentTest
    {
        #region Private fields

        private static TestContext _testContext;
        private static string _pdfFilesFolder;

        #endregion Private fields

        #region Test initialization and clean up

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

        #endregion Test initialization and clean up

        /// <summary>
        /// Test for correct type of component.
        /// </summary>
        [TestMethod]
        public void PDFPageComponent_Component_CheckType_Success()
        {
            var component = new PDFComponent();
            var layoutComponent = component.LayoutComponent;
            Assert.IsNotNull(layoutComponent);

            var pageComponentStandard = layoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var pageComponentThumbnail = layoutComponent.CreatePageComponent("thumbnail", PageLayoutType.Thumbnail);

            Assert.IsNotNull(pageComponentStandard as PDFPageComponent);
            Assert.IsNotNull(pageComponentThumbnail as PDFPageComponent);

            component.CloseDocument();
            component.Dispose();
        }

        /// <summary>
        /// Test for enumeration of all pages.
        /// </summary>
        [TestMethod]
        public void PDFPageComponent_Properties_Test_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var component = new PDFComponent();
            var layoutComponent = component.LayoutComponent;
            Assert.IsNotNull(layoutComponent);

            var pageComponentStandard = layoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var pageComponentThumbnail = layoutComponent.CreatePageComponent("thumbnail", PageLayoutType.Thumbnail);

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpen);

            Assert.AreEqual(1, pageComponentStandard.CurrentPageIndex);
            Assert.AreEqual(1094, pageComponentStandard.PageCount);
            Assert.AreEqual(1, pageComponentThumbnail.CurrentPageIndex);
            Assert.AreEqual(1094, pageComponentThumbnail.PageCount);

            component.CloseDocument();
            component.Dispose();
        }

        /// <summary>
        /// Test for enumeration of all pages.
        /// </summary>
        [TestMethod]
        public void PDFPageComponent_Pages_Enumerate_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var component = new PDFComponent();
            var layoutComponent = component.LayoutComponent;
            Assert.IsNotNull(layoutComponent);

            var pageComponentStandard = layoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var pageComponentThumbnail = layoutComponent.CreatePageComponent("thumbnail", PageLayoutType.Thumbnail);

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpen);

            Assert.IsNotNull(pageComponentStandard.Pages);
            Assert.IsNotNull(pageComponentThumbnail.Pages);

            var pageIndex = 0;
            foreach (var page in pageComponentStandard.Pages)
            {
                Assert.IsNotNull(page);
                Assert.AreEqual(pageIndex++, page.PageIndex);
                Assert.IsNotNull(page.PageLabel);
                Assert.AreEqual(792, page.Height);
                Assert.AreEqual(612, page.Width);
            }

            component.CloseDocument();
            component.Dispose();
        }
    }
}
