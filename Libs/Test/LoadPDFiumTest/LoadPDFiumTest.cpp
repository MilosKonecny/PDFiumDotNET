// PDFiumTls.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <assert.h>
#include <tchar.h>
#include <windows.h>
#include <sysinfoapi.h>
#include <psapi.h>
#include <inttypes.h>
#include <string>

typedef void(__cdecl* VOIDFUNC)();
constexpr auto WIDTH = 12;
constexpr auto BASE_PATH = "..\\..\\..\\..\\..\\Libs\\PDFium\\";
constexpr auto DLLx86 = "x86\\pdfium.dll";
constexpr auto DLLx64 = "x64\\pdfium.dll";

void PrintMemoryUsage(int counter)
{
    PROCESS_MEMORY_COUNTERS_EX pmc;
    GetProcessMemoryInfo(GetCurrentProcess(), (PROCESS_MEMORY_COUNTERS*)&pmc, sizeof(pmc));
    size_t virtualMemUsedByMe = pmc.PrivateUsage / 1024;
    _tprintf(TEXT("%*" PRId32 ": Memory usage = %*" PRId32 " kB\n"), WIDTH, counter, WIDTH, (int)virtualMemUsedByMe);
}

std::string GetLibraryToLoad()
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

int main()
{
    auto counter = 0;
    PrintMemoryUsage(counter);
    while (true)
    {
        auto handle = ::LoadLibraryA(GetLibraryToLoad().c_str());
        assert(handle);
        auto FPDF_InitLibrary = (VOIDFUNC)::GetProcAddress(handle, "FPDF_InitLibrary");
        assert(FPDF_InitLibrary);
        auto FPDF_DestroyLibrary = (VOIDFUNC)::GetProcAddress(handle, "FPDF_DestroyLibrary");
        assert(FPDF_DestroyLibrary);
        FPDF_InitLibrary();
        FPDF_DestroyLibrary();
        auto result = ::FreeLibrary(handle);
        assert(result);

        counter++;
        PrintMemoryUsage(counter);
    }
}
