#pragma once
#include "Stdafx.h"

using namespace System;

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    enum class DeadKeyFlags : USHORT;

    /// <summary>Defines a dead key - when single key stroke does not produce a character immediatelly but rather modifies character produced by following key stroke.</summary>
    public ref class DeadKey
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="DEADKEY"/> structure this instance wraps</summary>
        initonly PDEADKEY deadKey;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="DeadKey"/> class</summary>
        /// <param name="deadKey">A pointer to unmanaged <see cref="DEADKEY"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="deadKey"/> is null</exception>
        DeadKey(const PDEADKEY deadKey);
    public:
        /// <summary>Gets a base character this instance defines dead key for</summary>
        /// <remarks>This is the bese character which is changed when a diacritics is applied on it.</remarks>
        property Char Char{System::Char get();}
        /// <summary>Gets combining character (diacritic, accent) for this dead key</summary>
        property System::Char Accent{System::Char get();}
        /// <summary>Gets result of dead key processing</summary>
        property System::Char Composed{System::Char get();}
        /// <summary>Gets dead key flags</summary>
        property DeadKeyFlags Flags{DeadKeyFlags get();}
    };

    /// <summary>Defines flags used for dead keys</summary>
    [Flags]
    public enum class DeadKeyFlags : USHORT{
        /// <summary>No flags specified</summary>
        none = 0,
        /// <summary>Dead key produces another dead key</summary>
        Dead = DKF_DEAD
    };
}}}