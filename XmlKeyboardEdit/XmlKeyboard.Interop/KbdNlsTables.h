#pragma once
#include "stdafx.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {
    /// <summary>Contains varios tables that define extended far-east keyboard layout properties</summary>
    public ref class KbdNlsTables
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="KBDNLSTABLES"/> structure this instance wraps</summary>
        initonly PKBDNLSTABLES kbdNlsTables;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="KbdNlsTables"/> class</summary>
        /// <param name="kbdNlsTables">A pointer to unmanaged <see cref="KBDNLSTABLES"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="kbdNlsTables"/> is null</exception>
        KbdNlsTables(const PKBDNLSTABLES kbdNlsTables);
    public:
        //TODO:
    };

}}}