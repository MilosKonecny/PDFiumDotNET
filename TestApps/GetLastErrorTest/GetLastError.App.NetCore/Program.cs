namespace GetLastError.App.NetCore
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Call FPDF_LoadDocument - parameter length is 4");
            NativeMethods.FPDF_LoadDocument("asdf", null);
            var lastError = NativeMethods.FPDF_GetLastError();
            Console.WriteLine("FPDF_GetLastError returned: {0}", lastError);

            Console.WriteLine();

            Console.WriteLine("Call FPDF_LoadDocument - parameter length is 11");
            NativeMethods.FPDF_LoadDocument("asdf - asdf", null);
            lastError = NativeMethods.FPDF_GetLastError();
            Console.WriteLine("FPDF_GetLastError returned: {0}", lastError);

            Console.WriteLine("Press any key!");
            Console.ReadKey();
        }
    }
}
