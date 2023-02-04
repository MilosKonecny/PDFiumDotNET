namespace PDFiumDotNET.Components.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Zoom;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class for <see cref="PDFZoomComponent"/>.
    /// </summary>
    [TestClass]
    public class PDFZoomComponentTest
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
        public void PDFZoomComponent_Component_CheckType_Success()
        {
            var component = new PDFComponent();
            var layoutComponent = component.LayoutComponent;
            Assert.IsNotNull(layoutComponent);

            var pageComponentStandard = layoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var pageComponentThumbnail = layoutComponent.CreatePageComponent("thumbnail", PageLayoutType.Thumbnail);

            var zoomComponentStandard = pageComponentStandard.ZoomComponent;
            var zoomComponentThumbnail = pageComponentThumbnail.ZoomComponent;

            Assert.IsNotNull(zoomComponentStandard as PDFZoomComponent);
            Assert.IsNotNull(zoomComponentThumbnail as PDFZoomComponent);

            component.Dispose();
        }

        /// <summary>
        /// Test for correct zoom.
        /// </summary>
        [TestMethod]
        public void PDFZoomComponent_StartZoom_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var component = new PDFComponent();
            var layoutComponent = component.LayoutComponent;
            Assert.IsNotNull(layoutComponent);

            var pageComponentStandard = layoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var pageComponentThumbnail = layoutComponent.CreatePageComponent("thumbnail", PageLayoutType.Thumbnail);

            var zoomComponentStandard = pageComponentStandard.ZoomComponent;
            var zoomComponentThumbnail = pageComponentThumbnail.ZoomComponent;

            Assert.AreEqual(1d, zoomComponentStandard.CurrentZoomFactor);
            Assert.AreEqual(1d, zoomComponentThumbnail.CurrentZoomFactor);

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpened);

            Assert.AreEqual(1d, zoomComponentStandard.CurrentZoomFactor);
            zoomComponentStandard.CurrentZoomFactor = 2.1d;
            Assert.AreEqual(2.1d, zoomComponentStandard.CurrentZoomFactor);

            Assert.AreEqual(1d, zoomComponentThumbnail.CurrentZoomFactor);
            zoomComponentThumbnail.CurrentZoomFactor = 2.1d;
            Assert.AreEqual(2.1d, zoomComponentThumbnail.CurrentZoomFactor);

            component.CloseDocument();

            Assert.AreEqual(1d, zoomComponentStandard.CurrentZoomFactor);
            Assert.AreEqual(1d, zoomComponentThumbnail.CurrentZoomFactor);

            component.CloseDocument();
            component.Dispose();
        }
    }
}
