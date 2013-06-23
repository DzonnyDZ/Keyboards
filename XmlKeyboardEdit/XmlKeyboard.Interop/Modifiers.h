#pragma once

#include "Stdafx.h"
#include "Vk2Bit.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    /// <summary>Maps all possible shifter key combinations to an enumerated shift state</summary>
    public ref class Modifiers
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="MODIFIERS"/> structure this instance wraps</summary>
        initonly PMODIFIERS modifiers;

        cli::array<Vk2Bit^>^ vk2Bits;
        cli::array<const BYTE>^ modNumbers;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="Modifiers"/> class</summary>
        /// <param name="modifiers">A pointer to unmanaged <see cref="MODIFIERS"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="modifiers"/> is null</exception>
        Modifiers(const PMODIFIERS modifiers);
    public:
        /// <summary>Gets mapping of Virtual Keys to modifier bits</summary>
        property cli::array<Vk2Bit^>^ Vk2Bits{cli::array<Vk2Bit^>^ get();}
        /// <summary>Gets array indicating accociations of modifier numbers to modifier bits</summary>
        property cli::array<const BYTE>^ ModNumbers{cli::array<const BYTE>^ get();}
    };

}}}