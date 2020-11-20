This folder contains test applications created to test or to prove something.

***

### PDFiumConsoleTest
Shows the problem with FPDF_GetLastError function in PDFium dll.  

**PDFiumConsoleTest.DotNETCore**  
Simple console application for .NET Core 3.1 that uses PDFium dll.  
The application calls:
- *FPDF_InitLibrary*
- *FPDF_LoadDocument*, as parameter is passed document that does not exists
- *FPDF_GetLastError*  

In this application *FPDF_GetLastError* returns 0, but it should be 2 returned.

**PDFiumConsoleTest.DotNETFramework**  
Simple console application for .NET Framework 4.8 that uses PDFium dll.
This application calls exact the same methods as the previous application.  

In this application *FPDF_GetLastError* returns 2, as it should.

***

### GetLastErrorTest
Tests own dll with functions declared the same way as PDFium dll declares these functions.  

**GetLastError.Library**  
C/C++ DLL library that implements only two functions:
- *FPDF_LoadDocument* - the function sets the last error equal to the length of the passed string parameter
- *FPDF_GetLastError* - function returns last error  

Both functions are declared exactly the same way as functions in PDFium.

**GetLastError.App.NetCore**  
Simple console application for .NET Core 3.1 that uses GetLastError.Library dll.  
The application calls:
- *FPDF_LoadDocument*, with parameter "asdf"
- *FPDF_GetLastError*, returns 4
- *FPDF_LoadDocument*, with parameter "asdf - asdf"
- *FPDF_GetLastError*, returns 11


**GetLastError.App.NetFramework**  
Simple console application for .NET Framework 3.1 that uses GetLastError.Library dll.  
This application calls exact the same methods as the previous application with same result.  

***

