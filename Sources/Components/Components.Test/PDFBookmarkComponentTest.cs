namespace PDFiumDotNET.Components.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Bookmark;
    using PDFiumDotNET.Components.Contracts.Bookmark;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class for <see cref="PDFBookmarkComponent"/>.
    /// </summary>
    [TestClass]
    public class PDFBookmarkComponentTest
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
        public void PDFBookmarkComponent_Component_CheckType_Success()
        {
            var component = new PDFComponent();
            var bookmarkComponent = component.BookmarkComponent;

            Assert.IsNotNull(bookmarkComponent as PDFBookmarkComponent);

            component.Dispose();
        }

        /// <summary>
        /// Test for correct count of bookmarks.
        /// </summary>
        [TestMethod]
        public void PDFBookmarkComponent_EnumerateBookmarks_CheckCount_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var component = new PDFComponent();
            var bookmarkComponent = component.BookmarkComponent;

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpen);

            var bookmarkCount = 0;
            Queue<IPDFBookmark> queue = new Queue<IPDFBookmark>(bookmarkComponent.Bookmarks);
            while (queue.Count != 0)
            {
                var bookmark = queue.Dequeue();
                Assert.IsNotNull(bookmark);
                Assert.IsFalse(string.IsNullOrEmpty(bookmark.Text));
                Assert.IsNotNull(bookmark.Action);
                Assert.IsNotNull(bookmark.Action.Destination);
                Assert.IsNotNull(bookmark.Destination);
                var destA = bookmark.Action.Destination;
                var destB = bookmark.Destination;
                Assert.AreEqual(destA.PageIndex, destB.PageIndex);
                Assert.AreEqual(destA.X, destB.X);
                Assert.AreEqual(destA.Y, destB.Y);
                Assert.AreEqual(destA.Zoom, destB.Zoom);

                bookmarkCount++;
                foreach (var childBookmark in bookmark.Bookmarks)
                {
                    queue.Enqueue(childBookmark);
                }
            }

            Assert.AreEqual(218, bookmarkCount);

            component.CloseDocument();
            component.Dispose();
        }

        /// <summary>
        /// Test for correct count of bookmarks.
        /// </summary>
        [TestMethod]
        public void PDFBookmarkComponent_Bookmarks_CheckState_Success()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "DiversePages.pdf");

            var component = new PDFComponent();
            var bookmarkComponent = component.BookmarkComponent;

            component.OpenDocument(pdfFile, string.Empty);
            Assert.IsTrue(component.IsDocumentOpen);

            var checkCount = 0;
            foreach (var bookmark in bookmarkComponent.Bookmarks)
            {
#if NET48
                if (bookmark.Text.Contains("opened"))
#else
                if (bookmark.Text.Contains("opened", StringComparison.InvariantCultureIgnoreCase))
#endif
                {
                    Assert.IsTrue(bookmark.IsOpened);
                    checkCount++;
                }

#if NET48
                if (bookmark.Text.Contains("closed"))
#else
                if (bookmark.Text.Contains("closed", StringComparison.InvariantCultureIgnoreCase))
#endif
                {
                    Assert.IsFalse(bookmark.IsOpened);
                    checkCount++;
                }
            }

            Assert.AreEqual(3, checkCount);

            component.CloseDocument();
            component.Dispose();
        }
    }
}
