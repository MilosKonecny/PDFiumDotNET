#include <windows.h>

#if defined(WIN32)
#if defined(FPDF_IMPLEMENTATION)
#define FPDF_EXPORT __declspec(dllexport)
#else
#define FPDF_EXPORT __declspec(dllimport)
#endif  // defined(FPDF_IMPLEMENTATION)
#else
#if defined(FPDF_IMPLEMENTATION)
#define FPDF_EXPORT __attribute__((visibility("default")))
#else
#define FPDF_EXPORT
#endif  // defined(FPDF_IMPLEMENTATION)
#endif  // defined(WIN32)

#if defined(WIN32) && defined(FPDFSDK_EXPORTS)
#define FPDF_CALLCONV __stdcall
#else
#define FPDF_CALLCONV
#endif

typedef struct fpdf_document_t__* FPDF_DOCUMENT;

typedef const char* FPDF_STRING;

typedef const char* FPDF_BYTESTRING;

unsigned long lastError;

FPDF_EXPORT FPDF_DOCUMENT FPDF_CALLCONV FPDF_LoadDocument(FPDF_STRING file_path, FPDF_BYTESTRING password)
{
	if (file_path == NULL)
	{
		lastError = 0;
		return NULL;
	}

	lastError = (unsigned long)strlen(file_path);
	return NULL;
}

FPDF_EXPORT unsigned long FPDF_CALLCONV FPDF_GetLastError()
{
	return lastError;
}