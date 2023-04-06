# Libs folder

Folder contains all libraries used in the **PDFiumDotNET** project.

The pre-build / post-build process copies particular files to the appropriate folder.

Sub folder **Test** contains specific applications that show problems with used libraries.

Test applications:
- LoadPDFiumTest  
This application shows memory usage by repeatedly loading and freeing the PDFium library.
Various crashes occur on x86 and x64.
