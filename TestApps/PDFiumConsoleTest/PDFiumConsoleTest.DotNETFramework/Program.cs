using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var lastError = NativeMethods.FPDF_GetLastError();
            Console.WriteLine("Last error is {0}", lastError);

            NativeMethods.FPDF_DestroyLibrary();
            Console.WriteLine("Press any key to terminate!");
            Console.ReadKey();
        }
    }
}
