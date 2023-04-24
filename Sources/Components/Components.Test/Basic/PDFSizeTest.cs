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
        /// Cleanup method called after every test method is completed.
        /// </summary>
        [TestCleanup]
        public void CleanupTest()
        {
        }

        #endregion Test context, initialization and cleanup

        /// <summary>
        /// Test <see cref="PDFSize{T}"/> constructor.
        /// Use constructor <see cref="PDFSize{T}.PDFSize()"/> and check default values in size.
        /// </summary>
        [TestMethod]
        public void PDFSize_Constructor1_Instantiation_IsEmpty()
        {
            var tested = new PDFSize<double>();

            tested.IsEmpty.Should().BeTrue();
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

        /// <summary>
        /// Test <see cref="PDFSize{T}.Equals(PDFSize{T})"/>.
        /// Create two instances with identical values and use <see cref="PDFSize{T}.Equals(PDFSize{T})"/>.
        /// </summary>
        [TestMethod]
        public void PDFSize_IEquatableEquals_UseEquals_AreEqual()
        {
            var size1 = new PDFSize<double>(2, 5);
            var size2 = new PDFSize<double>(2, 5);

            size1.Equals(size2).Should().BeTrue();
            size2.Equals(size1).Should().BeTrue();
        }

        /// <summary>
        /// Test <see cref="PDFSize{T}.Equals(PDFSize{T})"/>.
        /// Create two instances with identical values and use <see cref="PDFSize{T}.Equals(PDFSize{T})"/>.
        /// </summary>
        [TestMethod]
        public void PDFSize_IEquatableEquals_UseEquals_AreNotEqual()
        {
            var size1 = new PDFSize<double>(2, 5);
            var size2 = new PDFSize<double>(2.1, 5.1);

            size1.Equals(size2).Should().BeFalse();
            size2.Equals(size1).Should().BeFalse();
        }

        /// <summary>
        /// Test <see cref="PDFSize{T}.Equals(object)"/>.
        /// Create two instances with identical values and use <see cref="PDFSize{T}.Equals(object)"/>.
        /// </summary>
        [TestMethod]
        public void PDFSize_Equals_UseEquals_AreEqual()
        {
            var size1 = new PDFSize<double>(2, 5);
            var size2 = new PDFSize<double>(2, 5);

            size1.Equals((object)size2).Should().BeTrue();
            size2.Equals((object)size1).Should().BeTrue();
        }

        /// <summary>
        /// Test <see cref="PDFSize{T}.Equals(object)"/>.
        /// Create two instances with identical values and use <see cref="PDFSize{T}.Equals(object)"/>.
        /// </summary>
        [TestMethod]
        public void PDFSize_Equals_UseEquals_AreNotEqual()
        {
            var size1 = new PDFSize<double>(2, 5);
            var size2 = new PDFSize<double>(2.1, 5.1);

            size1.Equals((object)size2).Should().BeFalse();
            size2.Equals((object)size1).Should().BeFalse();
        }

        /// <summary>
        /// Test operator '==' of <see cref="PDFSize{T}"/>.
        /// </summary>
        [TestMethod]
        public void PDFSize_EqualityOperator_CheckEquality_AreEqual()
        {
            var size1 = new PDFSize<double>(3, 6);
            var size2 = new PDFSize<double>(3, 6);

            (size1 == size2).Should().BeTrue();
        }

        /// <summary>
        /// Test operator '!=' of <see cref="PDFSize{T}"/>.
        /// </summary>
        [TestMethod]
        public void PDFSize_IneEqualityOperator_CheckInequality_AreNotEqual()
        {
            var size1 = new PDFSize<double>(3, 6);
            var size2 = new PDFSize<double>(4, 7);

            (size1 != size2).Should().BeTrue();
        }
    }
}
