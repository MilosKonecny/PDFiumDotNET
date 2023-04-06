# Libs/PDFium folder

PDFiumDotNET uses modified version of PDFium. The modification fixes error handling.

Compilation of PDFium is based on the solution from [bblanchon/pdfium-binaries](https://github.com/bblanchon)

Actually used PDFium version: [5640](https://pdfium.googlesource.com/pdfium/+/refs/heads/chromium/5640).

# Modifications in PDFium sources:

Changes in fx_system.h and fx_system.cpp:

Change the implementation so that functions FXSYS_SetLastError, FXSYS_GetLastError are the same for all operating systems.
