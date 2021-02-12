namespace PDFiumDotNET.Components.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.Adapters;
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
        /// Test for correct type of component.
        /// </summary>
        [TestMethod]
        public void PDFPageComponent_Component_CheckType_Success()
        {
            var component = new PDFComponent();
            var pageComponent = component.PageComponent;

            Assert.IsNotNull(pageComponent as PDFPageComponent);

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
            var pageComponent = component.PageComponent;
            Assert.IsNotNull(pageComponent);
            Assert.IsNotNull(pageComponent[PageLayoutType.Standard]);

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpened);

            Assert.AreEqual(1, pageComponent.CurrentPageIndex);
            Assert.AreEqual(1094, pageComponent.PageCount);

            // Test all layout adapters
            foreach (PageLayoutType type in Enum.GetValues(typeof(PageLayoutType)))
            {
                Assert.IsNotNull(pageComponent[type]);
            }

            // Standard
            Assert.IsTrue(866448 - pageComponent[PageLayoutType.Standard].CumulativeHeight <= 1, "Value: " + pageComponent[PageLayoutType.Standard].CumulativeHeight);
            Assert.IsTrue(792 - pageComponent[PageLayoutType.Standard].HighestGridCellHeight <= 1, "Value: " + pageComponent[PageLayoutType.Standard].HighestGridCellHeight);
            Assert.IsTrue(612 - pageComponent[PageLayoutType.Standard].WidestGridCellWidth <= 1, "Value: " + pageComponent[PageLayoutType.Standard].WidestGridCellWidth);

            // Thumbnail
            Assert.IsTrue(218800 - (int)pageComponent[PageLayoutType.Thumbnail].CumulativeHeight <= 1, "Value: " + (int)pageComponent[PageLayoutType.Thumbnail].CumulativeHeight);
            Assert.IsTrue(100 - (int)pageComponent[PageLayoutType.Thumbnail].HighestGridCellHeight <= 1, "Value: " + (int)pageComponent[PageLayoutType.Thumbnail].HighestGridCellHeight);
            Assert.IsTrue(77 - (int)pageComponent[PageLayoutType.Thumbnail].WidestGridCellWidth <= 1, "Value: " + (int)pageComponent[PageLayoutType.Thumbnail].WidestGridCellWidth);

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
            var pageComponent = component.PageComponent;

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpened);

            Assert.IsNotNull(pageComponent.Pages);

            var pageIndex = 0;
            foreach (var page in pageComponent.Pages)
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
