namespace PDFiumDotNET.Components.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.EventArguments;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class for <see cref="NavigatedToPageEventArgs"/>.
    /// </summary>
    [TestClass]
    public class NavigatedToPageEventArgsTest
    {
        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Constructor1_Success()
        {
            var instance = new NavigatedToPageEventArgs(0, 0);
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Constructor2_Success()
        {
            var instance = new NavigatedToPageEventArgs(-1, -1);
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Constructor3_Success()
        {
            var instance = new NavigatedToPageEventArgs(0, 0, 0, 0);
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test for constructor with success.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Constructor4_Success()
        {
            var instance = new NavigatedToPageEventArgs(-1, -1, -1, -1);
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test for <see cref="NavigatedToPageEventArgs.IsDetailedNavigation"/> property.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_IsDetailedNavigation_CorrectValue()
        {
            var instance = new NavigatedToPageEventArgs(0, 0);
            Assert.IsNotNull(instance);
            Assert.AreEqual(false, instance.IsDetailedNavigation);

            instance = new NavigatedToPageEventArgs(0, 0, 0, 0);
            Assert.IsNotNull(instance);
            Assert.AreEqual(true, instance.IsDetailedNavigation);
        }

        /// <summary>
        /// Test for <see cref="NavigatedToPageEventArgs.PreviousCurrentPageIndex"/>,
        /// <see cref="NavigatedToPageEventArgs.CurrentPageIndex"/>,
        /// <see cref="NavigatedToPageEventArgs.DetailedPositionX"/> and
        /// <see cref="NavigatedToPageEventArgs.DetailedPositionY"/> properties.
        /// </summary>
        [TestMethod]
        public void PerformActionEventArgs_Properties_CorrectValue()
        {
            var instance = new NavigatedToPageEventArgs(1, 2);
            Assert.IsNotNull(instance);
            Assert.AreEqual(1, instance.PreviousCurrentPageIndex);
            Assert.AreEqual(2, instance.CurrentPageIndex);

            instance = new NavigatedToPageEventArgs(10, 20, 30, 40);
            Assert.IsNotNull(instance);
            Assert.AreEqual(10, instance.PreviousCurrentPageIndex);
            Assert.AreEqual(20, instance.CurrentPageIndex);
            Assert.AreEqual(30, instance.DetailedPositionX);
            Assert.AreEqual(40, instance.DetailedPositionY);
        }
    }
}
