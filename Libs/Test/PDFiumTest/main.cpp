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

HMODULE libraryHandle = nullptr;
VoidFuncDef InitLibrary = nullptr;
VoidFuncDef DestroyLibrary = nullptr;
LoadDocumentDef LoadDocument = nullptr;
CloseDocumentDef CloseDocument = nullptr;
GetPageCountDef GetPageCount = nullptr;
LoadPageDef LoadPage = nullptr;
ClosePageDef ClosePage = nullptr;

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

std::string GetPDFiumToLoad()
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
	libraryHandle = ::LoadLibraryA(GetPDFiumToLoad().c_str());
	assert(libraryHandle);
	InitLibrary = (VoidFuncDef)::GetProcAddress(libraryHandle, "FPDF_InitLibrary");
	assert(InitLibrary);
	DestroyLibrary = (VoidFuncDef)::GetProcAddress(libraryHandle, "FPDF_DestroyLibrary");
	assert(DestroyLibrary);
	LoadDocument = (LoadDocumentDef)::GetProcAddress(libraryHandle, "FPDF_LoadDocument");
	assert(LoadDocument);
	CloseDocument = (CloseDocumentDef)::GetProcAddress(libraryHandle, "FPDF_CloseDocument");
	assert(CloseDocument);
	GetPageCount = (GetPageCountDef)::GetProcAddress(libraryHandle, "FPDF_GetPageCount");
	assert(GetPageCount);
	LoadPage = (LoadPageDef)::GetProcAddress(libraryHandle, "FPDF_LoadPage");
	assert(LoadPage);
	ClosePage = (ClosePageDef)::GetProcAddress(libraryHandle, "FPDF_ClosePage");
	assert(ClosePage);

	InitLibrary();
}

void FreePDFium()
{
	DestroyLibrary();
	auto result = ::FreeLibrary(libraryHandle);
	assert(result);

	libraryHandle = nullptr;
	InitLibrary = nullptr;
	DestroyLibrary = nullptr;
	LoadDocument = nullptr;
	CloseDocument = nullptr;
	GetPageCount = nullptr;
	LoadPage = nullptr;
	ClosePage = nullptr;
}

int main()
{
	LoadPDFium();

	PrintMemoryUsageHeader();
	PrintMemoryUsage("Before test:");
	auto counter = 0;
	for (auto step = 0; step < 40; step++)
	{
		ProgressBar pb;
		pb.Start(38, 50);
		for (auto index = 0; index < 50; index++)
		{
			counter++;
			auto document = LoadDocument(".\\Precalculus.pdf", "");
			assert(document);
			auto page_count = GetPageCount(document);
			for (auto page_index = 0; page_index < page_count; page_index++)
			{
				auto page = LoadPage(document, page_index);
				ClosePage(page);
			}
			CloseDocument(document);
			pb.Step();
		}
		pb.Stop();

		std::string text("Step ");
		text += std::to_string(step + 1);
		text += " (";
		text += std::to_string(counter);
		text += "):";
		PrintMemoryUsage(text);
	}

	std::cout << "Press ENTER to exit" << std::endl;
	std::cin.get();
}
