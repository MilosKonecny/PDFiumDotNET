namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;

    /// <summary>
    /// Test class for base functionality of the class <see cref="PDFiumBridge"/>.
    /// </summary>
    [TestClass]
    public class PDFiumBridgeTest
    {
        /// <summary>
        /// Test method  for <see cref="PDFiumBridge.UsageCount"/>.
        /// </summary>
        [TestMethod]
        public void UsageCount01()
        {
            Assert.AreEqual(0, PDFiumBridge.UsageCount);
            var bridge1 = new PDFiumBridge();
            Assert.AreEqual(1, PDFiumBridge.UsageCount);
            var bridge2 = new PDFiumBridge();
            Assert.AreEqual(2, PDFiumBridge.UsageCount);
            bridge1.Dispose();
            Assert.AreEqual(1, PDFiumBridge.UsageCount);
            bridge2.Dispose();
            Assert.AreEqual(0, PDFiumBridge.UsageCount);
        }

        /// <summary>
        /// Test method  for <see cref="PDFiumBridge.UsageCount"/>.
        /// </summary>
        [TestMethod]
        public void UsageCount02()
        {
            Assert.AreEqual(0, PDFiumBridge.UsageCount);
            var bridge1 = new PDFiumBridge();
            Assert.AreEqual(1, PDFiumBridge.UsageCount);
            var bridge2 = new PDFiumBridge();
            Assert.AreEqual(2, PDFiumBridge.UsageCount);
            bridge2.Dispose();
            Assert.AreEqual(1, PDFiumBridge.UsageCount);
            bridge1.Dispose();
            Assert.AreEqual(0, PDFiumBridge.UsageCount);
        }
    }
}
