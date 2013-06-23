#pragma once
#include "Stdafx.h"

using namespace System;

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    /// <summary>Name of a key</summary>
    public ref class KeyName
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VSC_LPWSTR"/> structure this instance wraps</summary>
        initonly PVSC_LPWSTR keyName;

        String^ name;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="KeyName"/> class</summary>
        /// <param name="keyName">A pointer to unmanaged <see cref="VSC_LPWSTR"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="keyName"/> is null</exception>
        KeyName(const PVSC_LPWSTR keyName);
    public:
        /// <summary>Gets Scan Code of key this instance provides name of</summary>
        property BYTE ScanCode{BYTE get();}
        /// <summary>Gets name of the key identified by <see cref="ScanCode"/></summary>
        property String^ Name{String^ get();}
    };
}}}