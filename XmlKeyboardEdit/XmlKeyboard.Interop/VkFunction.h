#pragma once
#include "Stdafx.h"
#include "NlsFParam.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    enum class NlsProcType:BYTE;
    enum class NlsProcIndex:BYTE;

    /// <summary>Mapping between Virtual Key and special functions</summary>
    public ref class VkFunction
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VK_F"/> structure this instance wraps</summary>
        initonly PVK_F vkf;

        cli::array<NlsFParam^>^ procs;
        cli::array<NlsFParam^>^ altProcs;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="VkFunction"/> class</summary>
        /// <param name="vkf">A pointer to unmanaged <see cref="VK_F"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="vkf"/> is null</exception>
        VkFunction(PVK_F vkf);
    public:
        /// <summary>Gets virtual key code</summary>
        property BYTE VirtualKey{BYTE get();}
        /// <summary>Gets function type</summary>
        property NlsProcType ProcType{NlsProcType get();}
        /// <summary>Gets function index</summary>
        property NlsProcIndex ProcCurrent{NlsProcIndex get();}
        /// <summary>Gets proc switch</summary>
        property BYTE ProcSwitch{BYTE get();}
        /// <summary>Gets base parameters.</summary>
        /// <returns>Array of 8 elements</returns>
        property cli::array<NlsFParam^>^ Procs{cli::array<NlsFParam^>^ get();}
        /// <summary>Gets ALt parameters</summary>
        /// <returns>Array of 8 elements</returns>
        property cli::array<NlsFParam^>^ AltProcs{cli::array<NlsFParam^>^ get();}
    };

    /// <summary>NLS function types</summary>
    public enum class NlsProcType : BYTE{
        /// <summary>Null type</summary>
        Null = KBDNLS_TYPE_NULL,
        /// <summary>Normal type</summary>
        Normal = KBDNLS_TYPE_NORMAL,
        /// <summary>Toggle type</summary>
        Toggle = KBDNLS_TYPE_TOGGLE
    };
    
    /// <summary>NLS function indexes</summary>
    public enum class NlsProcIndex : BYTE{
        /// <summary>Normal</summary>
        Normal = KBDNLS_INDEX_NORMAL,
        /// <summary>Alt</summary>
        Alt = KBDNLS_INDEX_ALT
    };

}}}