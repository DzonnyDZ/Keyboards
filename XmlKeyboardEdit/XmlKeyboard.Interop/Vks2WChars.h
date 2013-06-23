#pragma once
#include "Stdafx.h"
#include "Vk2WChar.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {
    /// <summary>Holds mappings from Virtual Keys to a characters</summary>
    public ref class Vks2WChars
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VK_TO_WCHAR_TABLE"/> structure this instance wraps</summary>
        initonly PVK_TO_WCHAR_TABLE vk2WChar;

        cli::array<Vk2WChar^>^ mappings;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="Vks2WChars"/> class</summary>
        /// <param name="vk2WChar">A pointer to unmanaged <see cref="VK_TO_WCHAR_TABLE"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="vk2WChar"/> is null</exception>
        Vks2WChars(const PVK_TO_WCHAR_TABLE vk2WChar);
    public:
        /// <summary>Gets mappings the mappings</summary>
        property cli::array<Vk2WChar^>^ Mappings{cli::array<Vk2WChar^>^ get();}
    };

}}}