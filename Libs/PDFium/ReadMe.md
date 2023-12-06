# Libs/PDFium folder

PDFiumDotNET uses modified version of PDFium.

Compilation of PDFium is based on the solution from [bblanchon/pdfium-binaries](https://github.com/bblanchon)

Actually used PDFium version: [6167](https://pdfium.googlesource.com/pdfium/+/refs/heads/chromium/6167).

# Modifications in PDFium sources

### fx_system.h, fx_system.cpp
Change the implementation so that functions FXSYS_SetLastError, FXSYS_GetLastError are the same for all operating systems.

### rand_util_win.cc
Use the implementation from version [5904](https://pdfium.googlesource.com/pdfium/+/refs/heads/chromium/5904)