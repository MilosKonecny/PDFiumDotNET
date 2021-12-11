namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements some unit test methods for all handles defined in <see cref="PDFiumBridge"/>.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class PDFiumBridgeHandlesTest
    {
        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_DOCUMENT_Constructor_Call1_Success()
        {
            var h = new FPDF_DOCUMENT();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_DOCUMENT_Constructor_Call2_Success()
        {
            var h = new FPDF_DOCUMENT(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_DOCUMENT_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_DOCUMENT.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGE_Constructor_Call1_Success()
        {
            var h = new FPDF_PAGE();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGE_Constructor_Call2_Success()
        {
            var h = new FPDF_PAGE(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGE_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_PAGE.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_BITMAP_Constructor_Call1_Success()
        {
            var h = new FPDF_BITMAP();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_BITMAP_Constructor_Call2_Success()
        {
            var h = new FPDF_BITMAP(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_BITMAP_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_BITMAP.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGERANGE_Constructor_Call1_Success()
        {
            var h = new FPDF_PAGERANGE();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGERANGE_Constructor_Call2_Success()
        {
            var h = new FPDF_PAGERANGE(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGERANGE_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_PAGERANGE.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_DEST_Constructor_Call1_Success()
        {
            var h = new FPDF_DEST();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_DEST_Constructor_Call2_Success()
        {
            var h = new FPDF_DEST(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_DEST_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_DEST.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_BOOKMARK_Constructor_Call1_Success()
        {
            var h = new FPDF_BOOKMARK();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_BOOKMARK_Constructor_Call2_Success()
        {
            var h = new FPDF_BOOKMARK(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_BOOKMARK_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_BOOKMARK.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_ACTION_Constructor_Call1_Success()
        {
            var h = new FPDF_ACTION();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_ACTION_Constructor_Call2_Success()
        {
            var h = new FPDF_ACTION(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_ACTION_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_ACTION.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_LINK_Constructor_Call1_Success()
        {
            var h = new FPDF_LINK();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_LINK_Constructor_Call2_Success()
        {
            var h = new FPDF_LINK(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_LINK_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_LINK.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_ANNOTATION_Constructor_Call1_Success()
        {
            var h = new FPDF_ANNOTATION();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_ANNOTATION_Constructor_Call2_Success()
        {
            var h = new FPDF_ANNOTATION(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_ANNOTATION_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_ANNOTATION.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_TEXTPAGE_Constructor_Call1_Success()
        {
            var h = new FPDF_TEXTPAGE();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_TEXTPAGE_Constructor_Call2_Success()
        {
            var h = new FPDF_TEXTPAGE(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_TEXTPAGE_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_TEXTPAGE.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_SCHHANDLE_Constructor_Call1_Success()
        {
            var h = new FPDF_SCHHANDLE();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_SCHHANDLE_Constructor_Call2_Success()
        {
            var h = new FPDF_SCHHANDLE(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_SCHHANDLE_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_SCHHANDLE.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGELINK_Constructor_Call1_Success()
        {
            var h = new FPDF_PAGELINK();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGELINK_Constructor_Call2_Success()
        {
            var h = new FPDF_PAGELINK(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGELINK_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_PAGELINK.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGEOBJECT_Constructor_Call1_Success()
        {
            var h = new FPDF_PAGEOBJECT();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGEOBJECT_Constructor_Call2_Success()
        {
            var h = new FPDF_PAGEOBJECT(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_PAGEOBJECT_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_PAGEOBJECT.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_FORMHANDLE_Constructor_Call1_Success()
        {
            var h = new FPDF_FORMHANDLE();
            Assert.IsFalse(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_FORMHANDLE_Constructor_Call2_Success()
        {
            var h = new FPDF_FORMHANDLE(new IntPtr(1));
            Assert.IsTrue(h.IsValid);
        }

        /// <summary>
        /// Test for <see cref="PDFiumBridge"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDF_FORMHANDLE_InvalidHandle_Call_CorrectHandle()
        {
            var h = FPDF_FORMHANDLE.InvalidHandle;
            Assert.IsFalse(h.IsValid);
        }
    }
}
