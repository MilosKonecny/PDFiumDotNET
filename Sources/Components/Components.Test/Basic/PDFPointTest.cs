namespace PDFiumDotNET.Components.Test.Basic
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.Basic;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements unit test methods for class <see cref="PDFPoint{T}"/>.
    /// </summary>
    /// <remarks>
    /// Unit testing best practices - https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices.
    /// The scenario under which it's being tested.
    /// </remarks>
    [TestClass]
    public class PDFPointTest
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
        /// Test <see cref="PDFPoint{T}"/> constructor.
        /// Use constructor <see cref="PDFPoint{T}.PDFPoint()"/> and check default values in point.
        /// </summary>
        [TestMethod]
        public void PDFPoint_Constructor1_Instantiation_IsEmpty()
        {
            var tested = new PDFPoint<double>();

            tested.IsEmpty.Should().BeTrue();
            tested.X.Should().Be(default);
            tested.Y.Should().Be(default);
        }

        /// <summary>
        /// Test <see cref="PDFPoint{T}"/> constructor.
        /// Use constructor <see cref="PDFPoint{T}.PDFPoint(T, T)"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFPoint_Constructor2_Instantiation_ContainsSameValues()
        {
            var tested = new PDFPoint<double>(2, 5);

            tested.X.Should().Be(2);
            tested.Y.Should().Be(5);
        }

        /// <summary>
        /// Test <see cref="PDFPoint{T}"/> constructor.
        /// Use constructor <see cref="PDFPoint{T}.PDFPoint(PDFPoint{T})"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFPoint_Constructor3_Instantiation_ContainsSameValues()
        {
            var point = new PDFPoint<double>
            {
                X = 1d,
                Y = 2d,
            };

            var tested = new PDFPoint<double>(point);

            tested.Should().BeEquivalentTo(point);
        }

        /// <summary>
        /// Test <see cref="PDFPoint{T}.Equals(PDFPoint{T})"/>.
        /// Create two instances with identical values and use <see cref="PDFPoint{T}.Equals(PDFPoint{T})"/>.
        /// </summary>
        [TestMethod]
        public void PDFPoint_IEquatableEquals_UseEquals_AreEqual()
        {
            var point1 = new PDFPoint<double>(2, 5);
            var point2 = new PDFPoint<double>(2, 5);

            point1.Equals(point2).Should().BeTrue();
            point2.Equals(point1).Should().BeTrue();
        }

        /// <summary>
        /// Test <see cref="PDFPoint{T}.Equals(PDFPoint{T})"/>.
        /// Create two instances with identical values and use <see cref="PDFPoint{T}.Equals(PDFPoint{T})"/>.
        /// </summary>
        [TestMethod]
        public void PDFPoint_IEquatableEquals_UseEquals_AreNotEqual()
        {
            var point1 = new PDFPoint<double>(2, 5);
            var point2 = new PDFPoint<double>(2.1, 5.1);

            point1.Equals(point2).Should().BeFalse();
            point2.Equals(point1).Should().BeFalse();
        }

        /// <summary>
        /// Test <see cref="PDFPoint{T}.Equals(object)"/>.
        /// Create two instances with identical values and use <see cref="PDFPoint{T}.Equals(object)"/>.
        /// </summary>
        [TestMethod]
        public void PDFPoint_Equals_UseEquals_AreEqual()
        {
            var point1 = new PDFPoint<double>(2, 5);
            var point2 = new PDFPoint<double>(2, 5);

            point1.Equals((object)point2).Should().BeTrue();
            point2.Equals((object)point1).Should().BeTrue();
        }

        /// <summary>
        /// Test <see cref="PDFPoint{T}.Equals(object)"/>.
        /// Create two instances with identical values and use <see cref="PDFPoint{T}.Equals(object)"/>.
        /// </summary>
        [TestMethod]
        public void PDFPoint_Equals_UseEquals_AreNotEqual()
        {
            var point1 = new PDFPoint<double>(2, 5);
            var point2 = new PDFPoint<double>(2.1, 5.1);

            point1.Equals((object)point2).Should().BeFalse();
            point2.Equals((object)point1).Should().BeFalse();
        }

        /// <summary>
        /// Test operator '==' of <see cref="PDFPoint{T}"/>.
        /// </summary>
        [TestMethod]
        public void PDFPoint_EqualityOperator_CheckEquality_AreEqual()
        {
            var point1 = new PDFPoint<double>(3, 6);
            var point2 = new PDFPoint<double>(3, 6);

            (point1 == point2).Should().BeTrue();
        }

        /// <summary>
        /// Test operator '!=' of <see cref="PDFPoint{T}"/>.
        /// </summary>
        [TestMethod]
        public void PDFPoint_IneEqualityOperator_CheckInequality_AreNotEqual()
        {
            var point1 = new PDFPoint<double>(3, 6);
            var point2 = new PDFPoint<double>(4, 7);

            (point1 != point2).Should().BeTrue();
        }
    }
}
