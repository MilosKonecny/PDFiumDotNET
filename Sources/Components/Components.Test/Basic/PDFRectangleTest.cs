namespace PDFiumDotNET.Components.Test.Basic
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Contracts.Basic;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements unit test methods for class <see cref="PDFRectangle{T}"/>.
    /// </summary>
    /// <remarks>
    /// Unit testing best practices - https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices.
    /// The scenario under which it's being tested.
    /// </remarks>
    [TestClass]
    public class PDFRectangleTest
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
        /// Test <see cref="PDFRectangle{T}"/> constructor.
        /// Use constructor <see cref="PDFRectangle{T}.PDFRectangle()"/> and check default values in rectangle.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_Constructor1_Instantiation_IsEmpty()
        {
            var tested = new PDFRectangle<double>();

            tested.IsEmpty.Should().BeTrue();
            tested.X.Should().Be(default);
            tested.Y.Should().Be(default);
            tested.Width.Should().Be(default);
            tested.Height.Should().Be(default);
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}"/> constructor.
        /// Use constructor <see cref="PDFRectangle{T}.PDFRectangle(T, T, T, T)"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_Constructor2_Instantiation_ContainsSameValues()
        {
            var tested = new PDFRectangle<double>(2, 5, 3, 6);

            tested.X.Should().Be(2);
            tested.Y.Should().Be(5);
            tested.Width.Should().Be(3);
            tested.Height.Should().Be(6);
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}"/> constructor.
        /// Use constructor <see cref="PDFRectangle{T}.PDFRectangle(PDFPoint{T}, PDFSize{T})"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_Constructor3_Instantiation_ContainsSameValues()
        {
            var tested = new PDFRectangle<double>(new PDFPoint<double>(10, 20), new PDFSize<double>(30, 40));

            tested.X.Should().Be(10);
            tested.Y.Should().Be(20);
            tested.Width.Should().Be(30);
            tested.Height.Should().Be(40);
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}"/> constructor.
        /// Use constructor <see cref="PDFRectangle{T}.PDFRectangle(PDFRectangle{T})"/> and test equality.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_Constructor4_Instantiation_ContainsSameValues()
        {
            var rectangle = new PDFRectangle<double>
            {
                X = 1d,
                Y = 2d,
                Width = 3,
                Height = 4,
            };

            var tested = new PDFRectangle<double>(rectangle);

            tested.Should().BeEquivalentTo(rectangle);
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}.Equals(PDFRectangle{T})"/>.
        /// Create two instances with identical values and use <see cref="PDFRectangle{T}.Equals(PDFRectangle{T})"/>.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_IEquatableEquals_UseEquals_AreEqual()
        {
            var rectangle1 = new PDFRectangle<double>(2, 5, 32, 85);
            var rectangle2 = new PDFRectangle<double>(2, 5, 32, 85);

            rectangle1.Equals(rectangle2).Should().BeTrue();
            rectangle2.Equals(rectangle1).Should().BeTrue();
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}.Equals(PDFRectangle{T})"/>.
        /// Create two instances with identical values and use <see cref="PDFRectangle{T}.Equals(PDFRectangle{T})"/>.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_IEquatableEquals_UseEquals_AreNotEqual()
        {
            var rectangle1 = new PDFRectangle<double>(2, 5, 32, 85);
            var rectangle2 = new PDFRectangle<double>(2.1, 5.1, 32.1, 85.1);

            rectangle1.Equals(rectangle2).Should().BeFalse();
            rectangle2.Equals(rectangle1).Should().BeFalse();
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}.Equals(object)"/>.
        /// Create two instances with identical values and use <see cref="PDFRectangle{T}.Equals(object)"/>.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_Equals_UseEquals_AreEqual()
        {
            var rectangle1 = new PDFRectangle<double>(2, 5, 32, 85);
            var rectangle2 = new PDFRectangle<double>(2, 5, 32, 85);

            rectangle1.Equals((object)rectangle2).Should().BeTrue();
            rectangle2.Equals((object)rectangle1).Should().BeTrue();
        }

        /// <summary>
        /// Test <see cref="PDFRectangle{T}.Equals(object)"/>.
        /// Create two instances with identical values and use <see cref="PDFRectangle{T}.Equals(object)"/>.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_Equals_UseEquals_AreNotEqual()
        {
            var rectangle1 = new PDFRectangle<double>(2, 5, 32, 85);
            var rectangle2 = new PDFRectangle<double>(2.1, 5.1, 32.1, 85.1);

            rectangle1.Equals((object)rectangle2).Should().BeFalse();
            rectangle2.Equals((object)rectangle1).Should().BeFalse();
        }

        /// <summary>
        /// Test operator '==' of <see cref="PDFRectangle{T}"/>.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_EqualityOperator_CheckEquality_AreEqual()
        {
            var rectangle1 = new PDFRectangle<double>(3, 6, 33, 86);
            var rectangle2 = new PDFRectangle<double>(3, 6, 33, 86);

            (rectangle1 == rectangle2).Should().BeTrue();
        }

        /// <summary>
        /// Test operator '!=' of <see cref="PDFRectangle{T}"/>.
        /// </summary>
        [TestMethod]
        public void PDFRectangle_IneEqualityOperator_CheckInequality_AreNotEqual()
        {
            var rectangle1 = new PDFRectangle<double>(3, 6, 33, 86);
            var rectangle2 = new PDFRectangle<double>(4, 7, 34, 87);

            (rectangle1 != rectangle2).Should().BeTrue();
        }
    }
}
