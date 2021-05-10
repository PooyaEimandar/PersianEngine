#pragma once

#pragma warning(disable : 4505 4710)
#pragma comment(lib, "version.lib")

#include <string>
#include <sstream>
#include <iomanip>
#include <exception>
#include <new>
#include <windows.h>


namespace version_nmsp
{
	struct language
	{
		WORD language_;
		WORD code_page_;

		language()
		{
			language_  = 0;
			code_page_ = 0;
		}
	};
}


class version
{
private:
	UCHAR* resource_data_;
	version_nmsp::language *language_information_;

	std::wstring get_value(const std::wstring &key) const
	{
		if(resource_data_)
		{
			UINT size   = 0;
			std::wstringstream t;
			LPVOID value  = 0;

			// Build query string
			t << L"\\StringFileInfo\\" << std::setw(4) << std::setfill(L'0') << std::hex
				<< language_information_->language_ << std::setw(4) << std::hex
				<< language_information_->code_page_ << L"\\" << key;

			if(VerQueryValue(static_cast<LPVOID>(resource_data_),
				const_cast<LPTSTR>(t.str().c_str()),
				static_cast<LPVOID*>(&value),
				&size) != FALSE)
			{
				return static_cast<LPTSTR>(value);
			}
		}

		return L"";
	}

public:
	version()
	{
		// Get application name
		TCHAR buf[MAX_PATH] = L"";

		if(GetModuleFileName(0, buf, sizeof(buf)))
		{
			std::wstring app_name = buf;
			app_name = app_name.substr(app_name.rfind(L"\\") + 1);

			// Get version info
			DWORD h = 0;

			DWORD resource_size = GetFileVersionInfoSize(const_cast<WCHAR*>(app_name.c_str()), &h);
			if(resource_size)
			{
				resource_data_ = new UCHAR[resource_size];
				if(resource_data_)
				{
					if(GetFileVersionInfo(const_cast<WCHAR*>(app_name.c_str()),
						0,
						resource_size,
						static_cast<LPVOID>(resource_data_)) != FALSE)
					{
						UINT size = 0;

						// Get language information
						if(::VerQueryValue(static_cast<LPVOID>(resource_data_),
							L"\\VarFileInfo\\Translation",
							reinterpret_cast<LPVOID*>(&language_information_),
							&size) == FALSE)
						{
							throw std::exception("Requested localized version information not available");
						}
					}
					else
					{
						std::stringstream exception;
						exception << "Could not get version information (Windows error: " << ::GetLastError() << ")";
						throw std::exception(exception.str().c_str());
					}
				}
				else
				{
					throw std::bad_alloc();
				}}
			else
			{
				std::stringstream exception;
				exception << "No version information found (Windows error: " << ::GetLastError() << ")";
				throw std::exception(exception.str().c_str());
			}
		}
		else
		{
			throw std::exception("Could not get application name");
		}
	}

	~version() 
	{ 
		delete [] resource_data_; 
	}

	std::wstring GetProductName() const { return get_value(L"ProductName"); }
	std::wstring GetInternalName() const { return get_value(L"InternalName"); }
	std::wstring GetProductVersion() const { return get_value(L"ProductVersion"); }
	std::wstring GetSpecialBuild() const { return get_value(L"SpecialBuild"); }
	std::wstring GetPrivateBuild() const { return get_value(L"PrivateBuild"); }
	std::wstring GetCopyright() const { return get_value(L"LegalCopyright"); }
	std::wstring GetTrademarks() const { return get_value(L"LegalTrademarks"); }
	std::wstring GetComments() const { return get_value(L"Comments"); }
	std::wstring GetCompanyName() const { return get_value(L"CompanyName"); }
	std::wstring GetFileVersion() const { return get_value(L"FileVersion"); }
	std::wstring GetFileDescription() const { return get_value(L"FileDescription"); }
};