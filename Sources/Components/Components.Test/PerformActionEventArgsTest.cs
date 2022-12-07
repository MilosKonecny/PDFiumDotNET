namespace PDFiumDotNET.Components.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Action;
    using PDFiumDotNET.Components.Contracts.EventArguments;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test for <see cref="PerformActionEventArgs"/>.
    /// </summary>
    [TestClass]
    public class PerformActionEventArgsTest
    {
        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Constructor_Success()
        {
            var pdfComponent = new PDFComponent();
            var pdfAction = new PDFAction(pdfComponent, new Wrapper.Bridge.PDFiumBridge.FPDF_ACTION());
            var instance = new PerformActionEventArgs(pdfAction);
            Assert.IsNotNull(instance);
            pdfComponent.Dispose();
        }

        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PerformActionEventArgs_Constructor_ThrowException()
        {
            var pdfAction = new PDFAction(null, new Wrapper.Bridge.PDFiumBridge.FPDF_ACTION());
            var instance = new PerformActionEventArgs(pdfAction);
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Action_Equals()
        {
            var pdfComponent = new PDFComponent();
            var pdfAction = new PDFAction(pdfComponent, new Wrapper.Bridge.PDFiumBridge.FPDF_ACTION());
            var instance = new PerformActionEventArgs(pdfAction);
            Assert.IsNotNull(instance);
            Assert.AreEqual(pdfAction, instance.Action);
            pdfComponent.Dispose();
        }
    }
}
