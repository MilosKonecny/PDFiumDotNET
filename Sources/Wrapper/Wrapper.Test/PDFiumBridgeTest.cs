namespace PDFiumDotNET.Wrapper.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

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
        public void PDFiumBridge_UsageCount_BridgesChanged_AlwaysCorrect_01()
        {
            // Because of 'Workaround for issue #105'
            // usage count is not decremented
            // ToDo: Change this test back after issue #105 is fixed.
            ////Assert.AreEqual(0, PDFiumBridge.UsageCount);
            var bridge1 = new PDFiumBridge();
            ////Assert.AreEqual(1, PDFiumBridge.UsageCount);
            var bridge2 = new PDFiumBridge();
            ////Assert.AreEqual(2, PDFiumBridge.UsageCount);
            bridge1.Dispose();
            ////Assert.AreEqual(1, PDFiumBridge.UsageCount);
            bridge2.Dispose();
            ////Assert.AreEqual(0, PDFiumBridge.UsageCount);
        }

        /// <summary>
        /// Test method  for <see cref="PDFiumBridge.UsageCount"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumBridge_UsageCount_BridgesChanged_AlwaysCorrect_02()
        {
            // Because of 'Workaround for issue #105'
            // usage count is not decremented
            // ToDo: Change this test back after issue #105 is fixed.
            ////Assert.AreEqual(0, PDFiumBridge.UsageCount);
            var bridge1 = new PDFiumBridge();
            ////Assert.AreEqual(1, PDFiumBridge.UsageCount);
            var bridge2 = new PDFiumBridge();
            ////Assert.AreEqual(2, PDFiumBridge.UsageCount);
            bridge2.Dispose();
            ////Assert.AreEqual(1, PDFiumBridge.UsageCount);
            bridge1.Dispose();
            ////Assert.AreEqual(0, PDFiumBridge.UsageCount);
        }
    }
}
