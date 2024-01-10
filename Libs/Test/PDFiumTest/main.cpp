// PDFiumDotNET.Apps.TestPDFium.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <assert.h>
#include <tchar.h>
#include <windows.h>
#include <sysinfoapi.h>
#include <psapi.h>
#include <inttypes.h>
#include <iostream>
#include <iomanip>
#include <string>
#include "fpdfview.h"
#include "ProgressBar.h"

constexpr auto COLUMN_1_WIDTH = 20;
constexpr auto COLUMN_2_WIDTH = 10;
constexpr auto COLUMN_3_WIDTH = 10;
constexpr auto BASE_PATH = ".\\PDFium\\";
constexpr auto DLLx86 = "x86\\pdfium.dll";
constexpr auto DLLx64 = "x64\\pdfium.dll";

// FPDF_EXPORT void FPDF_CALLCONV FPDF_InitLibrary();
// FPDF_EXPORT void FPDF_CALLCONV FPDF_DestroyLibrary();
typedef void(__cdecl* VoidFuncDef)();
// FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadDocument(FPDF_STRING file_path, FPDF_BYTESTRING password);
typedef FPDF_DOCUMENT(__cdecl* LoadDocumentDef)(FPDF_STRING file_path, FPDF_BYTESTRING password);
// FPDF_EXPORT void FPDF_CALLCONV FPDF_CloseDocument(FPDF_DOCUMENT document);
typedef void(__cdecl* CloseDocumentDef)(FPDF_DOCUMENT document);
// FPDF_EXPORT int FPDF_CALLCONV FPDF_GetPageCount(FPDF_DOCUMENT document);
typedef int(__cdecl* GetPageCountDef)(FPDF_DOCUMENT document);
// FPDF_EXPORT FPDF_PAGE FPDF_CALLCONV FPDF_LoadPage(FPDF_DOCUMENT document, int page_index);
typedef FPDF_PAGE(__cdecl* LoadPageDef)(FPDF_DOCUMENT document, int page_index);
// FPDF_EXPORT void FPDF_CALLCONV FPDF_ClosePage(FPDF_PAGE page);
typedef void(__cdecl* ClosePageDef)(FPDF_PAGE page);
// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDF_GetPageSizeByIndexF(FPDF_DOCUMENT document, int page_index, FS_SIZEF* size);
typedef bool(__cdecl* GetPageSizeByIndexFDef)(FPDF_DOCUMENT document, int page_index, FS_SIZEF* size);
// FPDF_EXPORT FPDF_BITMAP FPDF_CALLCONV FPDFBitmap_CreateEx(int width, int height, int format, void* first_scan, int stride);
typedef FPDF_BITMAP(__cdecl* Bitmap_CreateExDef)(int width, int height, int format, void* first_scan, int stride);
// FPDF_EXPORT void FPDF_CALLCONV FPDFBitmap_Destroy(FPDF_BITMAP bitmap);
typedef void(__cdecl* Bitmap_DestroyDef)(FPDF_BITMAP bitmap);
// FPDF_EXPORT void FPDF_CALLCONV FPDF_RenderPageBitmapWithMatrix(FPDF_BITMAP bitmap, FPDF_PAGE page, const FS_MATRIX* matrix, const FS_RECTF* clipping, int flags);
typedef void(__cdecl* RenderPageBitmapWithMatrixDef)(FPDF_BITMAP bitmap, FPDF_PAGE page, const FS_MATRIX* matrix, const FS_RECTF* clipping, int flags);

HMODULE libraryHandle = nullptr;
VoidFuncDef PDFium_InitLibrary = nullptr;
VoidFuncDef PDFium_DestroyLibrary = nullptr;
LoadDocumentDef PDFium_LoadDocument = nullptr;
CloseDocumentDef PDFium_CloseDocument = nullptr;
GetPageCountDef PDFium_GetPageCount = nullptr;
LoadPageDef PDFium_LoadPage = nullptr;
ClosePageDef PDFium_ClosePage = nullptr;
GetPageSizeByIndexFDef PDFium_GetPageSizeByIndexF = nullptr;
Bitmap_CreateExDef PDFium_Bitmap_CreateEx = nullptr;
Bitmap_DestroyDef PDFium_Bitmap_Destroy = nullptr;
RenderPageBitmapWithMatrixDef PDFium_RenderPageBitmapWithMatrix = nullptr;

/// <summary>
/// Facet for defining numeric punctuation text
/// </summary>
class Punctuation : public std::numpunct<char>
{
protected:
	virtual char do_thousands_sep() const { return ','; }
	virtual std::string do_grouping() const { return "\03"; }
};

void PrintMemoryUsageHeader()
{
	std::cout << std::left << std::setw(COLUMN_1_WIDTH) << "Memory usage in KiB";
	std::cout << std::right << std::setw(COLUMN_2_WIDTH) << "Private";
	std::cout << std::right << std::setw(COLUMN_3_WIDTH) << "Physical" << std::endl;
}

void PrintMemoryUsage(std::string text)
{
	std::cout.imbue(std::locale(std::locale::classic(), new Punctuation));

	PROCESS_MEMORY_COUNTERS_EX pmc;
	GetProcessMemoryInfo(GetCurrentProcess(), (PROCESS_MEMORY_COUNTERS*)&pmc, sizeof(pmc));
	size_t privateMemoryUsage = pmc.PrivateUsage / 1024;
	size_t physicalMemoryUsage = pmc.WorkingSetSize / 1024;
	std::cout << std::left << std::setw(COLUMN_1_WIDTH) << text;
	std::cout << std::right << std::setw(COLUMN_2_WIDTH) << privateMemoryUsage;
	std::cout << std::right << std::setw(COLUMN_3_WIDTH) << physicalMemoryUsage << std::endl;
}

std::string DeterminePDFiumToLoad()
{
	std::string ret = BASE_PATH;
	SYSTEM_INFO si;
	::GetNativeSystemInfo(&si);
	if (si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_INTEL)
	{
		// 32 bit system
		ret += DLLx86;
	}
	else
	{
		// 64 bit system
		BOOL wow64Process;
		IsWow64Process(GetCurrentProcess(), &wow64Process);
		if (wow64Process)
		{
			// 32 bit process on 64 bit system
			ret += DLLx86;
		}
		else
		{
			// 64 bit process on 64 bit system
			ret += DLLx64;
		}
	}

	return ret;
}

void LoadPDFium()
{
	// Load DLL
	libraryHandle = ::LoadLibraryA(DeterminePDFiumToLoad().c_str());
	assert(libraryHandle);

	// Load all necessary functions
	PDFium_InitLibrary = (VoidFuncDef)::GetProcAddress(libraryHandle, "FPDF_InitLibrary");
	assert(PDFium_InitLibrary);
	PDFium_DestroyLibrary = (VoidFuncDef)::GetProcAddress(libraryHandle, "FPDF_DestroyLibrary");
	assert(PDFium_DestroyLibrary);
	PDFium_LoadDocument = (LoadDocumentDef)::GetProcAddress(libraryHandle, "FPDF_LoadDocument");
	assert(PDFium_LoadDocument);
	PDFium_CloseDocument = (CloseDocumentDef)::GetProcAddress(libraryHandle, "FPDF_CloseDocument");
	assert(PDFium_CloseDocument);
	PDFium_GetPageCount = (GetPageCountDef)::GetProcAddress(libraryHandle, "FPDF_GetPageCount");
	assert(PDFium_GetPageCount);
	PDFium_LoadPage = (LoadPageDef)::GetProcAddress(libraryHandle, "FPDF_LoadPage");
	assert(PDFium_LoadPage);
	PDFium_ClosePage = (ClosePageDef)::GetProcAddress(libraryHandle, "FPDF_ClosePage");
	assert(PDFium_ClosePage);
	PDFium_GetPageSizeByIndexF = (GetPageSizeByIndexFDef)::GetProcAddress(libraryHandle, "FPDF_GetPageSizeByIndexF");
	assert(PDFium_GetPageSizeByIndexF);
	PDFium_Bitmap_CreateEx = (Bitmap_CreateExDef)::GetProcAddress(libraryHandle, "FPDFBitmap_CreateEx");
	assert(PDFium_Bitmap_CreateEx);
	PDFium_Bitmap_Destroy = (Bitmap_DestroyDef)::GetProcAddress(libraryHandle, "FPDFBitmap_Destroy");
	assert(PDFium_Bitmap_Destroy);
	PDFium_RenderPageBitmapWithMatrix = (RenderPageBitmapWithMatrixDef)::GetProcAddress(libraryHandle, "FPDF_RenderPageBitmapWithMatrix");
	assert(PDFium_RenderPageBitmapWithMatrix);

	// Initialize PDFium DLL
	PDFium_InitLibrary();
}

void FreePDFium()
{
	// Destroy PDFium DLL
	PDFium_DestroyLibrary();

	// Free DLL
	auto result = ::FreeLibrary(libraryHandle);
	assert(result);

	// Reset loaded functions
	libraryHandle = nullptr;
	PDFium_InitLibrary = nullptr;
	PDFium_DestroyLibrary = nullptr;
	PDFium_LoadDocument = nullptr;
	PDFium_CloseDocument = nullptr;
	PDFium_GetPageCount = nullptr;
	PDFium_LoadPage = nullptr;
	PDFium_ClosePage = nullptr;
	PDFium_GetPageSizeByIndexF = nullptr;
	PDFium_Bitmap_CreateEx = nullptr;
	PDFium_Bitmap_Destroy = nullptr;
	PDFium_RenderPageBitmapWithMatrix = nullptr;
}

int main()
{
	// Load PDFium
	LoadPDFium();

	// Show memory information
	PrintMemoryUsageHeader();
	PrintMemoryUsage("Before test:");

	// Perform many tests
	for (auto step = 0; step < 100; step++)
	{
		// Open PDF document
		auto document = PDFium_LoadDocument(".\\Precalculus.pdf", "");
		assert(document);

		// Get page count
		auto page_count = PDFium_GetPageCount(document);

		// Prepare and start progress bar
		ProgressBar pb;
		pb.Start(38, page_count);

		// Iterate through all pages
		for (auto page_index = 0; page_index < page_count; page_index++)
		{
			// Load page
			auto page = PDFium_LoadPage(document, page_index);
			FS_SIZEF size;
			if (PDFium_GetPageSizeByIndexF(document, page_index, &size))
			{
				// Round up page size
				auto int_width = std::ceil(size.width);
				auto int_height = std::ceil(size.height);
				// Allocate buffer
				auto buffer = malloc(int_height * int_width * 4);
				// Create bitmap
				auto bitmap = PDFium_Bitmap_CreateEx(int_width, int_height, 4, buffer, int_width * 4);
				// Render page into bitmap
				FS_MATRIX matrix;
				matrix.a = 1; matrix.b = 0; matrix.c = 0; matrix.d = 1; matrix.e = 0; matrix.f = 0;
				FS_RECTF rect;
				rect.left = 0; rect.right = size.width; rect.top = 0; rect.bottom = size.height;
				PDFium_RenderPageBitmapWithMatrix(bitmap, page, &matrix, &rect, 0);
				// Free created bitmap
				PDFium_Bitmap_Destroy(bitmap);
				// Free allocated buffer
				free(buffer);
			}

			// Close page
			PDFium_ClosePage(page);
			// Step
			pb.Step();
		}

		// Close PDF document
		PDFium_CloseDocument(document);
		// Stop progress bar
		pb.Stop();

		// Show memory information
		std::string text("After test ");
		text += std::to_string(step + 1);
		text += ":";
		PrintMemoryUsage(text);
	}

	// Free PDFium
	FreePDFium();

	std::cout << "Press ENTER to exit" << std::endl;
	std::cin.get();
}
