#pragma once
#include "Stdafx.h"

using namespace System;

namespace Dzonny { namespace XmlKeyboard { namespace Interop {
    /// <summary>Represents name of dead key</summary>
    public ref class DeadKeyName
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="WCHAR*"/> string this instance wraps</summary>
        initonly const WCHAR* keyName;
        String^ name;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="DeadKeyName"/> class</summary>
        /// <param name="keyName">A pointer to unmanaged <see cref="WCHAR*"/> string to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="keyName"/> is null</exception>
        DeadKeyName(const WCHAR* keyName);
    public:
        /// <summary>Gets character that represents a dead key</summary>
        property Char Accent{Char get();}
        /// <summary>Gets name of dead key for <see cref="Accent"/></summary>
        property String^ Name{String^ get();}
    };
}}}