#pragma once
#include "Stdafx.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {
    /// <summary>Represents single mapping from Virtual Key to a single character</summary>
    public ref class Vk2WChar
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VK_TO_WCHAR_TABLE"/> structure this instance wraps</summary>
        initonly PVK_TO_WCHAR_TABLE vk2WChar;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="Vk2WChar"/> class</summary>
        /// <param name="vk2WChar">A pointer to unmanaged <see cref="VK_TO_WCHAR_TABLE"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="vk2WChar"/> is null</exception>
        Vk2WChar(const PVK_TO_WCHAR_TABLE vk2WChar);
    };

}}}