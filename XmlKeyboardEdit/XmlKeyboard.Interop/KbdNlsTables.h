#pragma once
#include "stdafx.h"
#include "VkFunction.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    /// <summary>Contains varios tables that define extended far-east keyboard layout properties</summary>
    public ref class KbdNlsTables
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="KBDNLSTABLES"/> structure this instance wraps</summary>
        initonly PKBDNLSTABLES kbdNlsTables;

        cli::array<VkFunction^>^ vkFunctions;
        cli::array<USHORT>^ mouseVKeys ;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="KbdNlsTables"/> class</summary>
        /// <param name="kbdNlsTables">A pointer to unmanaged <see cref="KBDNLSTABLES"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="kbdNlsTables"/> is null</exception>
        KbdNlsTables(const PKBDNLSTABLES kbdNlsTables);
    public:
        /// <summary>Gets OEM identifier</summary>
        property USHORT OemIdentifier{USHORT get();}
        /// <summary>Gets laoyut information</summary>
        property USHORT LayoutInformation{USHORT get();}
        /// <summary>Gets VK to functions mappings</summary>
        property cli::array<VkFunction^>^ VkFunctions{cli::array<VkFunction^>^ get();}
        /// <summary>Gets mouse virtual key translations</summary>
        property cli::array<USHORT>^ MouseVKeys{cli::array<USHORT>^ get();}
    };

}}}