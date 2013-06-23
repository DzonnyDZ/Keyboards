#pragma once
#include "Stdafx.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {
    /// <summary>Represents mapping from virtual scan code to virtual key code</summary>
    public ref class Vsc2Vk
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VSC_VK"/> structure this instance wraps</summary>
        initonly PVSC_VK vsc2Vk;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="Vsc2Vk"/> class</summary>
        /// <param name="vsc2Vk">A pointer to unmanaged <see cref="VSC_VK"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="vsc2Vk"/> is null</exception>
        Vsc2Vk(const PVSC_VK vsc2Vk);
    public: 
        /// <summary>Gets virtual scan code</summary>
        property BYTE ScanCode{BYTE get();}
        /// <summary>Gets virtual key code</summary>
        property USHORT VirtualKey{USHORT get();}
    };
}}}