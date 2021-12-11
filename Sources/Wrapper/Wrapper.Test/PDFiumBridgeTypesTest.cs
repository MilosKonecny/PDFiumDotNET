namespace PDFiumDotNET.Wrapper.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements some unit test methods for types defined in <see cref="PDFiumBridge"/>.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class PDFiumBridgeTypesTest
    {
        /// <summary>
        /// Test for <see cref="FPDF_COLOR"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDFCOLOR_Constructor_Call_NoException_01()
        {
            _ = new FPDF_COLOR();
        }

        /// <summary>
        /// Test for <see cref="FPDF_COLOR"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDFCOLOR_Constructor_Call_NoException_02()
        {
            _ = new FPDF_COLOR(0);
        }

        /// <summary>
        /// Test for <see cref="FPDF_COLOR"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDFCOLOR_Constructor_Call_NoException_03()
        {
            _ = new FPDF_COLOR(1, 2, 3, 4);
        }

        /// <summary>
        /// Test for <see cref="FPDF_COLOR"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDFCOLOR_AllProperties_Test_Success_01()
        {
            var c = new FPDF_COLOR(0x0A0B0C0D);
            Assert.AreEqual(0x0B, c.Red);
            Assert.AreEqual(0x0C, c.Green);
            Assert.AreEqual(0x0D, c.Blue);
            Assert.AreEqual(0x0A, c.Alpha);
        }

        /// <summary>
        /// Test for <see cref="FPDF_COLOR"/> handle.
        /// </summary>
        [TestMethod]
        public void FPDFCOLOR_AllProperties_Test_Success_02()
        {
            var c = new FPDF_COLOR(1, 2, 3, 4);
            Assert.AreEqual(1, c.Red);
            Assert.AreEqual(2, c.Green);
            Assert.AreEqual(3, c.Blue);
            Assert.AreEqual(4, c.Alpha);
        }
    }
}
