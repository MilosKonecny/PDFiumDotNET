using System;
using System.Runtime.InteropServices;

namespace PDFiumConsoleTest.DotNETFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "asdf";
            Console.WriteLine("Is64BitProcess: {0}", Environment.Is64BitProcess);

            Console.WriteLine("Call FPDF_InitLibrary");
            NativeMethods.FPDF_InitLibrary();

            Console.WriteLine("Call FPDF_LoadDocument");
            var doc = NativeMethods.FPDF_LoadDocument(file, null);
            Console.WriteLine("Document {0} opened", doc == IntPtr.Zero ? "was not" : "was");

            Console.WriteLine("Last WIN32 error is {0}", Marshal.GetLastWin32Error());

            var lastError = NativeMethods.FPDF_GetLastError();
            Console.WriteLine("Last error is {0}", lastError);

            NativeMethods.FPDF_DestroyLibrary();
            Console.WriteLine("Press any key to terminate!");
            Console.ReadKey();
        }
    }
}
