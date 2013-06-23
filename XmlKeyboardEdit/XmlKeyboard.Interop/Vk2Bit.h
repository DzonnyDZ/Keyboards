#pragma once

#include "Stdafx.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    /// <summary>Association between Virtual Key and modifier bits</summary>
    public ref class Vk2Bit
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VK_TO_BIT"/> structure this instance wraps</summary>
        initonly PVK_TO_BIT vk2Bit;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="Vk2Bit"/> class</summary>
        /// <param name="vk2Bit">A pointer to unmanaged <see cref="VK_TO_BIT"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="vk2Bit"/> is null</exception>
        Vk2Bit(const PVK_TO_BIT vk2Bit);
    public:
        /// <summary>Gets Virtual Key code</summary>
        property BYTE VirtualKey{BYTE get();}
        /// <summary>Gets modifiers produced by the <see cref="VirtualKey"/></summary>
        property BYTE Modifiers{BYTE get();}
    };

}}}