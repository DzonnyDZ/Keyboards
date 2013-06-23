#pragma once
#include "Stdafx.h"

using namespace System;

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    /// <summary>Ligature - when a key press produces more than one character</summary>
    public ref class Ligature
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="LIGATURE1"/> structure this instance wraps</summary>
        /// <remarks>This can actually be pointer to <see cref="LIGATURE1"/>, <see cref="LIGATURE2"/>, <see cref="LIGATURE3"/>, <see cref="LIGATURE4"/> or <see cref="LIGATURE5"/>.</remarks>
        initonly PLIGATURE1 ligature;
        /// <summary>Maximum number of characters in <see cref="ligature"/>-><see cref="LIGATURE1::wch">wch</see></summary>
        initonly BYTE maxChars;

        String^ characters;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="Ligature"/> class</summary>
        /// <param name="ligature">
        /// A pointer to unmanaged <see cref="LIGATURE1"/> structure to wrap in a new instance.
        /// This can actually be pointer to <see cref="LIGATURE1"/>, <see cref="LIGATURE2"/>, <see cref="LIGATURE3"/>, <see cref="LIGATURE4"/> or <see cref="LIGATURE5"/>.
        /// </param>
        /// <param name="maxChars">Indicates maximum number of characters in <paramref name="ligature"/>-><see cref="LIGATURE1::wch">wch</see></param>
        /// <exception cref="ArgumentNullException"><paramref name="ligature"/> is null</exception>
        Ligature(const PLIGATURE1 ligature, BYTE maxChars);
    public:
        /// <summary>Gets a virtual key that, when pressed, produces this ligature</summary>
        property BYTE VirtualKey{BYTE get();}
        /// <summary>Gets modificators to be pressed along with <see cref="VirtualKey"/> to produce the ligature</summary>
        property WORD ModificationNumber{WORD get();}
        /// <summary>Gets characters for the ligature</summary>
        property String^ Characters{String^ get();}
    };

}}}