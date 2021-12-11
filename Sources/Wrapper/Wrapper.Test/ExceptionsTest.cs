namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Exceptions;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements some unit test methods for class <see cref="PDFiumFunctionNotFoundException"/>
    /// and <see cref="PDFiumLibraryNotLoadedException"/>.
    /// </summary>
    [TestClass]
    public class ExceptionsTest
    {
        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Constructor1_Call_NoException()
        {
            _ = new PDFiumFunctionNotFoundException();
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Constructor2_Call_NoException()
        {
            _ = new PDFiumFunctionNotFoundException(null);
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Constructor3_Call_NoException()
        {
            _ = new PDFiumFunctionNotFoundException(string.Empty);
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Constructor4_Call_NoException()
        {
            _ = new PDFiumFunctionNotFoundException("PDFiumFunctionNotFoundException");
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Constructor5_Call_NoException()
        {
            _ = new PDFiumFunctionNotFoundException("PDFiumFunctionNotFoundException", null);
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Constructor6_Call_NoException()
        {
            _ = new PDFiumFunctionNotFoundException("PDFiumFunctionNotFoundException", new PDFiumFunctionNotFoundException("message"));
        }

        /// <summary>
        /// Unit test for property <see cref="Exception.Message"/> of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_Message_Call_NoException()
        {
            var message = "0\r\n1\tB";
            var e = new PDFiumFunctionNotFoundException(message);
            Assert.AreEqual(message, e.Message);
        }

        /// <summary>
        /// Unit test for method <see cref="PDFiumFunctionNotFoundException.CreateException(string, string, int)"/> of <see cref="PDFiumFunctionNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumFunctionNotFoundException_CreateException_Call_NoException()
        {
            var library = "l-i-b-r-a-r-y";
            var function = "f-u-n-c-t-i-o-n";
            var number = 5;
            var numberString = " 5 ";
            var e = PDFiumFunctionNotFoundException.CreateException(library, function, number);
#if NET48
            Assert.IsTrue(e.Message.Contains(library));
            Assert.IsTrue(e.Message.Contains(function));
            Assert.IsTrue(e.Message.Contains(numberString));
#else
            Assert.IsTrue(e.Message.Contains(library, StringComparison.InvariantCulture));
            Assert.IsTrue(e.Message.Contains(function, StringComparison.InvariantCulture));
            Assert.IsTrue(e.Message.Contains(numberString, StringComparison.InvariantCulture));
#endif
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Constructor1_Call_NoException()
        {
            _ = new PDFiumLibraryNotLoadedException();
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Constructor2_Call_NoException()
        {
            _ = new PDFiumLibraryNotLoadedException(null);
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Constructor3_Call_NoException()
        {
            _ = new PDFiumLibraryNotLoadedException(string.Empty);
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Constructor4_Call_NoException()
        {
            _ = new PDFiumLibraryNotLoadedException("PDFiumLibraryNotLoadedException");
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Constructor5_Call_NoException()
        {
            _ = new PDFiumLibraryNotLoadedException("PDFiumLibraryNotLoadedException", null);
        }

        /// <summary>
        /// Unit test for constructor of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Constructor6_Call_NoException()
        {
            _ = new PDFiumLibraryNotLoadedException("PDFiumLibraryNotLoadedException", new PDFiumLibraryNotLoadedException("message"));
        }

        /// <summary>
        /// Unit test for property <see cref="Exception.Message"/> of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_Message_Call_NoException()
        {
            var message = "0\r\n1\tB";
            var e = new PDFiumLibraryNotLoadedException(message);
            Assert.AreEqual(message, e.Message);
        }

        /// <summary>
        /// Unit test for method <see cref="PDFiumLibraryNotLoadedException.CreateException(string, int)"/> of <see cref="PDFiumLibraryNotLoadedException"/>.
        /// </summary>
        [TestMethod]
        public void PDFiumLibraryNotLoadedException_CreateException_Call_NoException()
        {
            var library = "l-i-b-r-a-r-y";
            var number = 5;
            var numberString = " 5 ";
            var e = PDFiumLibraryNotLoadedException.CreateException(library, number);
#if NET48
            Assert.IsTrue(e.Message.Contains(library));
            Assert.IsTrue(e.Message.Contains(numberString));
#else
            Assert.IsTrue(e.Message.Contains(library, StringComparison.InvariantCulture));
            Assert.IsTrue(e.Message.Contains(numberString, StringComparison.InvariantCulture));
#endif
        }
    }
}
