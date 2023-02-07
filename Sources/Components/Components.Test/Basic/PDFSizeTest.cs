namespace PDFiumDotNET.Components.Test.Basic
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.Basic;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements unit test methods for class <see cref="PDFSize{T}"/>.
    /// </summary>
    /// <remarks>
    /// Unit testing best practices - https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices.
    /// The scenario under which it's being tested.
    /// </remarks>
    [TestClass]
    public class PDFSizeTest
    {
        #region Test context, initialization and cleanup

        private static TestContext _testContext;

        /// <summary>
        /// This context is the test context of every test method - set before every test starts.
        /// </summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        /// <summary>
        /// Unit test class initialization.
        /// </summary>
        /// <param name="ctx">Test context.</param>
        [ClassInitialize]
        public static void InitializeClass(TestContext ctx)
        {
            _testContext = ctx;
        }

        /// <summary>
        /// Unit test class cleanup.
        /// </summary>
        /// <remarks>This method is not called immediately after the last test is completed.</remarks>
        [ClassCleanup]
        public static void CleanupClass()
        {
        }

        /// <summary>
        /// Initialization for each test method. It is called before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
        }

        /// <summary>
        /// Clenaup method called after every test method is completed.
        /// </summary>
        [TestCleanup]
        public void CleanupTest()
        {
        }

        #endregion Test context, initialization and cleanup

        /// <summary>
        /// Test <see cref="PDFSize{T}"/> constructor.
        /// Use constructor <see cref="PDFSize{T}.PDFSize()"/> and check values in point.
        /// </summary>
        [TestMethod]
        public void PDFSize_Constructor1_Instantiation_IsEmpty()
        {
            var tested = new PDFSize<double>();

            tested.IsEmpty.Should().Be(true);
            tested.Width.Should().Be(default);
            tested.Height.Should().Be(default);
        }

        /// <summary>
        /// Test <see cref="PDFSize{T}"/> constructor.
        /// Use constructor <see cref="PDFSize{T}.PDFSize(T, T)"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFSize_Constructor2_Instantiation_ContainsSameValues()
        {
            var tested = new PDFSize<double>(2, 5);

            tested.Width.Should().Be(2);
            tested.Height.Should().Be(5);
        }

        /// <summary>
        /// Test <see cref="PDFSize{T}"/> constructor.
        /// Use constructor <see cref="PDFSize{T}.PDFSize(PDFSize{T})"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFSize_Constructor3_Instantiation_ContainsSameValues()
        {
            var size = new PDFSize<double>
            {
                Width = 1d,
                Height = 2d,
            };

            var tested = new PDFSize<double>(size);

            tested.Should().BeEquivalentTo(size);
        }
    }
}
