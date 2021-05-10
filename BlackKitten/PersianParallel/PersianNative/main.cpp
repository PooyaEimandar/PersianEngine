#include "pch.h"

using namespace concurrency;

#define DllExport extern "C" __declspec( dllexport )

//Prepare WCHAR* for DllExport
const WCHAR* __stdcall GetWCHARPtr( const WCHAR* data, UINT size ) 
{
	ULONG  ulSize = (size * sizeof(wchar_t)) + sizeof(wchar_t);
	auto pwszReturn = (wchar_t*)::CoTaskMemAlloc(ulSize);
	wcscpy(pwszReturn, data);
	return pwszReturn;
}

DllExport const WCHAR* __stdcall Initialize()
{
	std::vector<accelerator> accs = accelerator::get_all();
	accelerator chosen_one;

	auto result = std::find_if(accs.begin(), accs.end(), [] (const accelerator& acc)
	{
		return !acc.is_emulated && acc.supports_double_precision && !acc.has_display;
	});

	if (result != accs.end())
	{
		chosen_one = *(result);
	}

	auto name = chosen_one.description;
	auto hr = accelerator::set_default(chosen_one.device_path);
	if (hr != true)
	{
		name = L"C++ AMP Error";
	}
#ifdef _DEBUG

	OutputDebugStringW(name.c_str());

#endif // DEBUG

	return  GetWCHARPtr(name.c_str(), name.size());
}

DllExport const WCHAR* __stdcall GetDLLVersion()
{
	version version;
	auto v = version.GetProductVersion();
	return GetWCHARPtr(v.c_str(), v.size());
}
