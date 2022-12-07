namespace PDFiumDotNET.Components.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var pageComponent = component.PageComponent;
            var zoomComponent = pageComponent.ZoomComponent;

            Assert.IsNotNull(zoomComponent as PDFZoomComponent);

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
            var pageComponent = component.PageComponent;
            var zoomComponent = pageComponent.ZoomComponent;
            Assert.AreEqual(1d, zoomComponent.CurrentZoomFactor);

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpened);
            Assert.AreEqual(1d, zoomComponent.CurrentZoomFactor);

            zoomComponent.CurrentZoomFactor = 2.1d;
            Assert.AreEqual(2.1d, zoomComponent.CurrentZoomFactor);

            component.CloseDocument();
            Assert.AreEqual(1d, zoomComponent.CurrentZoomFactor);

            component.CloseDocument();
            component.Dispose();
        }
    }
}
