namespace PDFiumDotNET.Components.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Observers;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class for <see cref="PDFComponent"/>.
    /// </summary>
    [TestClass]
    public class PDFComponentTest
    {
        #region Private consts

        private const string _constDiversePagesPDF = "DiversePages.pdf";

        #endregion Private consts

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
        /// Test for constructor.
        /// </summary>
        [TestMethod]
        public void PDFComponent_Constructor_Call_Success()
        {
            var component = new PDFComponent();
            Assert.IsFalse(component.IsDisposed);
            component.Dispose();
            Assert.IsTrue(component.IsDisposed);
        }

        /// <summary>
        /// Test for document open.
        /// </summary>
        [TestMethod]
        public void PDFComponent_OpenDocument_DocumentExists_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var component = new PDFComponent();
            component.OpenDocument(pdfFile);
            Assert.IsTrue(component.IsDocumentOpened);
            component.CloseDocument();
            Assert.IsFalse(component.IsDocumentOpened);
            component.CloseDocument();
            Assert.IsFalse(component.IsDocumentOpened);
            component.Dispose();
        }

        /// <summary>
        /// Test for document open.
        /// </summary>
        [TestMethod]
        public void PDFComponent_OpenDocument_DocumentDoesNotExist_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "FileDoesNotExist.pdf");

            var component = new PDFComponent();
            component.OpenDocument(pdfFile);
            Assert.IsFalse(component.IsDocumentOpened);
            component.Dispose();
        }

        /// <summary>
        /// Test for document open.
        /// </summary>
        [TestMethod]
        public void PDFComponent_OpenDocument_PasswordProtected_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "PwdProtected(Pwd is pwd).pdf");

            var component = new PDFComponent();
            component.OpenDocument(pdfFile);
            Assert.IsFalse(component.IsDocumentOpened);
            component.OpenDocument(pdfFile, "pwd");
            Assert.IsTrue(component.IsDocumentOpened);
            component.CloseDocument();
            component.Dispose();
        }

        /// <summary>
        /// Test for child components.
        /// </summary>
        [TestMethod]
        public void PDFComponent_ChildComponents_RequireThem_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var component = new PDFComponent();

            var pageComponent = component.PageComponent;
            Assert.IsNotNull(pageComponent);
            var findComponent = component.FindComponent;
            Assert.IsNotNull(findComponent);
            var bookmarkComponent = component.BookmarkComponent;
            Assert.IsNotNull(bookmarkComponent);

            component.OpenDocument(pdfFile);
            Assert.IsTrue(component.IsDocumentOpened);

            pageComponent = component.PageComponent;
            Assert.IsNotNull(pageComponent);
            findComponent = component.FindComponent;
            Assert.IsNotNull(findComponent);
            bookmarkComponent = component.BookmarkComponent;
            Assert.IsNotNull(bookmarkComponent);

            component.CloseDocument();

            pageComponent = component.PageComponent;
            Assert.IsNotNull(pageComponent);
            findComponent = component.FindComponent;
            Assert.IsNotNull(findComponent);
            bookmarkComponent = component.BookmarkComponent;
            Assert.IsNotNull(bookmarkComponent);

            component.Dispose();

            Assert.IsTrue(pageComponent.IsDisposed);
            Assert.IsTrue(findComponent.IsDisposed);
            Assert.IsTrue(bookmarkComponent.IsDisposed);

            pageComponent = component.PageComponent;
            Assert.IsNull(pageComponent);
            findComponent = component.FindComponent;
            Assert.IsNull(findComponent);
            bookmarkComponent = component.BookmarkComponent;
            Assert.IsNull(bookmarkComponent);
        }

        /// <summary>
        /// Test for attach of child component - IPDFDocumentObserver.
        /// </summary>
        [TestMethod]
        public void PDFComponent_Attach_NewComponent_Success()
        {
            // Prepare 'IPDFDocumentObserver'
            var childComponent = new Mock<IPDFChildComponent>();
            var documentObserver = childComponent.As<IPDFDocumentObserver>();
            var documentClosedCounter = 0;
            documentObserver.Setup(a => a.DocumentClosed()).Callback(() => documentClosedCounter++);
            var documentClosingCounter = 0;
            documentObserver.Setup(a => a.DocumentClosing()).Callback(() => documentClosingCounter++);
            var documentOpenedCounter = 0;
            var openedFile = string.Empty;
            documentObserver.Setup(a => a.DocumentOpened(It.IsAny<string>())).Callback<string>((file) =>
            {
                documentOpenedCounter++;
                openedFile = file;
            });
            var documentFailedCounter = 0;
            var failedFile = string.Empty;
            documentObserver.Setup(a => a.DocumentOpenFailed(It.IsAny<string>())).Callback<string>((file) =>
            {
                documentFailedCounter++;
                failedFile = file;
            });
            var documentOpeningCounter = 0;
            var openingFile = string.Empty;
            documentObserver.Setup(a => a.DocumentOpening(It.IsAny<string>())).Callback<string>((file) =>
            {
                documentOpeningCounter++;
                openingFile = file;
            });

            var pdfFileExists = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");
            var pdfFileDoesNotExist = Path.Combine(_pdfFilesFolder, "FileDoesNotExist.pdf");

            var component = new PDFComponent();
            component.Attach(childComponent.Object);

            // Check before any operation processed
            Assert.AreEqual(0, documentClosedCounter);
            Assert.AreEqual(0, documentClosingCounter);
            Assert.AreEqual(0, documentOpenedCounter);
            Assert.AreEqual(0, documentFailedCounter);
            Assert.AreEqual(0, documentOpeningCounter);
            Assert.AreEqual(string.Empty, openedFile);
            Assert.AreEqual(string.Empty, failedFile);
            Assert.AreEqual(string.Empty, openingFile);

            // Open pdf document
            component.OpenDocument(pdfFileExists);
            Assert.IsTrue(component.IsDocumentOpened);

            // Check values
            Assert.AreEqual(0, documentClosedCounter);
            Assert.AreEqual(0, documentClosingCounter);
            Assert.AreEqual(1, documentOpenedCounter);
            Assert.AreEqual(0, documentFailedCounter);
            Assert.AreEqual(1, documentOpeningCounter);
            Assert.AreEqual(pdfFileExists, openedFile);
            Assert.AreEqual(string.Empty, failedFile);
            Assert.AreEqual(pdfFileExists, openingFile);

            // Reset values
            openedFile = string.Empty;
            openingFile = string.Empty;

            // Close pdf document
            component.CloseDocument();
            Assert.IsFalse(component.IsDocumentOpened);

            // Check values
            Assert.AreEqual(1, documentClosedCounter);
            Assert.AreEqual(1, documentClosingCounter);
            Assert.AreEqual(1, documentOpenedCounter);
            Assert.AreEqual(0, documentFailedCounter);
            Assert.AreEqual(1, documentOpeningCounter);
            Assert.AreEqual(string.Empty, openedFile);
            Assert.AreEqual(string.Empty, failedFile);
            Assert.AreEqual(string.Empty, openingFile);

            // Reset values
            documentClosedCounter = 0;
            documentClosingCounter = 0;
            documentOpenedCounter = 0;
            documentFailedCounter = 0;
            documentOpeningCounter = 0;

            // Open pdf document that does not exist
            component.OpenDocument(pdfFileDoesNotExist);
            Assert.IsFalse(component.IsDocumentOpened);

            // Check values
            Assert.AreEqual(0, documentClosedCounter);
            Assert.AreEqual(0, documentClosingCounter);
            Assert.AreEqual(0, documentOpenedCounter);
            Assert.AreEqual(1, documentFailedCounter);
            Assert.AreEqual(1, documentOpeningCounter);
            Assert.AreEqual(string.Empty, openedFile);
            Assert.AreEqual(pdfFileDoesNotExist, failedFile);
            Assert.AreEqual(pdfFileDoesNotExist, openingFile);

            // Reset values
            failedFile = string.Empty;
            openingFile = string.Empty;

            // Close pdf document
            component.CloseDocument();
            Assert.IsFalse(component.IsDocumentOpened);

            // Check values
            Assert.AreEqual(0, documentClosedCounter);
            Assert.AreEqual(0, documentClosingCounter);
            Assert.AreEqual(0, documentOpenedCounter);
            Assert.AreEqual(1, documentFailedCounter);
            Assert.AreEqual(1, documentOpeningCounter);
            Assert.AreEqual(string.Empty, openedFile);
            Assert.AreEqual(string.Empty, failedFile);
            Assert.AreEqual(string.Empty, openingFile);

            // Reset values
            documentClosedCounter = 0;
            documentClosingCounter = 0;
            documentOpenedCounter = 0;
            documentFailedCounter = 0;
            documentOpeningCounter = 0;

            // Close not opened pdf document
            component.CloseDocument();
            Assert.IsFalse(component.IsDocumentOpened);

            // Check values
            Assert.AreEqual(0, documentClosedCounter);
            Assert.AreEqual(0, documentClosingCounter);
            Assert.AreEqual(0, documentOpenedCounter);
            Assert.AreEqual(0, documentFailedCounter);
            Assert.AreEqual(0, documentOpeningCounter);
            Assert.AreEqual(string.Empty, openedFile);
            Assert.AreEqual(string.Empty, failedFile);
            Assert.AreEqual(string.Empty, openingFile);

            component.Dispose();
        }

        /// <summary>
        /// Test for <see cref="IPDFComponent.DocumentInformation"/>.
        /// </summary>
        [TestMethod]
        public void PDFComponent_DocumentInformation_Get_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, _constDiversePagesPDF);
            var component = new PDFComponent();
            component.OpenDocument(pdfFile);
            var info = component.DocumentInformation;
            Assert.AreEqual("Diverse pages", info.Title);
            Assert.AreEqual("Miloš Konečný", info.Author);
            Assert.AreEqual("Diverse pages", info.Subject);
            Assert.AreEqual("-", info.Keywords);
            Assert.AreEqual("Miloš Konečný", info.Creator);
            Assert.AreEqual("PDFsharp 1.50.4740 (www.pdfsharp.com)", info.Producer);
            component.Dispose();
        }

        /// <summary>
        /// Test for <see cref="IPDFComponent.FileName"/>.
        /// </summary>
        [TestMethod]
        public void PDFComponent_FileName_Get_Success()
        {
            var pdfFile = _constDiversePagesPDF;
            var pdfPath = Path.Combine(_pdfFilesFolder, pdfFile);

            var component = new PDFComponent();

            Assert.IsNull(component.FileName);
            component.OpenDocument(pdfPath);
            Assert.AreEqual(pdfFile, component.FileName);
            component.CloseDocument();
            Assert.IsNull(component.FileName);

            component.Dispose();
        }

        /// <summary>
        /// Test for <see cref="IPDFComponent.FileWithPath"/>.
        /// </summary>
        [TestMethod]
        public void PDFComponent_FileWithPath_Get_Success()
        {
            var pdfFile = _constDiversePagesPDF;
            var pdfPath = Path.Combine(_pdfFilesFolder, pdfFile);

            var component = new PDFComponent();

            Assert.IsNull(component.FileWithPath);
            component.OpenDocument(pdfPath);
            Assert.AreEqual(pdfPath, component.FileWithPath);
            component.CloseDocument();
            Assert.IsNull(component.FileWithPath);

            component.Dispose();
        }
    }
}
