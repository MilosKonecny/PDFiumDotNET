namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class implements some unit test methods methods defined in <see cref="NativeMethods"/>.
    /// </summary>
    /// <remarks>
    /// Naming convention:
    ///     - Tested class.
    ///     - Tested member (method/property) or name of tested block of members.
    ///     - Performed action.
    ///     - Expected result.
    /// </remarks>
    [TestClass]
    public class NativeMethodsTest
    {
        /// <summary>
        /// Test for <see cref="PDFiumDotNET.Wrapper.NativeMethods.StructureArrayToIntPtr{T}(T[])"/> method.
        /// </summary>
        [TestMethod]
        public void NativeMethods_StructureArrayToIntPtr_Call_AllocatedIntPtr()
        {
            var array = new FPDF_COLOR[] { new FPDF_COLOR(0, 1, 2, 3), new FPDF_COLOR(0, 1, 2, 3), new FPDF_COLOR(0, 1, 2, 3) };
            var intPtr = NativeMethods.StructureArrayToIntPtr(array);
            Assert.IsTrue(intPtr.ToInt64() != 0);
            Marshal.FreeHGlobal(intPtr);
        }

        /// <summary>
        /// Test for <see cref="PDFiumDotNET.Wrapper.NativeMethods.StructureArrayToIntPtr{T}(T[])"/> method.
        /// </summary>
        [TestMethod]
        public void NativeMethods_StructureArrayToIntPtr_Call2_AllocatedIntPtr()
        {
            var array = Array.Empty<FPDF_COLOR>();
            var intPtr = NativeMethods.StructureArrayToIntPtr<FPDF_COLOR>(array);
            Assert.IsTrue(intPtr.ToInt64() != 0);
            Marshal.FreeHGlobal(intPtr);
        }

        /// <summary>
        /// Test for <see cref="PDFiumDotNET.Wrapper.NativeMethods.StructureArrayToIntPtr{T}(T[])"/> method.
        /// </summary>
        [TestMethod]
        public void NativeMethods_StructureArrayToIntPtr_Call_NoException()
        {
            var intPtr = NativeMethods.StructureArrayToIntPtr<FPDF_COLOR>(null);
            Assert.AreEqual(IntPtr.Zero, intPtr);
            Marshal.FreeHGlobal(intPtr);
        }
    }
}
