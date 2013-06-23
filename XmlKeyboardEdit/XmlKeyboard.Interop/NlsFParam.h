#pragma once
#include "Stdafx.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    enum class NlsProcParamType : BYTE;

    /// <summary>NLS function parameter</summary>
    public ref class NlsFParam
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="VK_FPARAM"/> structure this instance wraps</summary>
        initonly PVK_FPARAM fParam;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="NlsFParam"/> class</summary>
        /// <param name="fParam">A pointer to unmanaged <see cref="VK_FPARAM"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="fParam"/> is null</exception>
        NlsFParam(PVK_FPARAM fParam);
    public:
        /// <summary>Gets value that identifies procedure parameter</summary>
        property NlsProcParamType Index{NlsProcParamType get();}
        /// <summary>Gets parameter value</summary>
        property ULONG Param{ULONG get();}
    };

    /// <summary>NLS function identifiers</summary>
    public enum class NlsProcParamType : BYTE{
        /// <summary>Invalid function</summary>
        Null = KBDNLS_NULL,
        /// <summary>Drop keyevent</summary>
        NoEvent= KBDNLS_NOEVENT,
        /// <summary>Send Base VK_xxx</summary>
        SendBaseVk = KBDNLS_SEND_BASE_VK,
        /// <summary>Send Parameter VK_xxx</summary>
        SendParamVk = KBDNLS_SEND_PARAM_VK,
        /// <summary>VK_KANA (with hardware lock)</summary>
        KanaLock = KBDNLS_KANALOCK,
        /// <summary>VK_DBE_ALPHANUMERIC</summary>
        AlphaNum = KBDNLS_ALPHANUM,
        /// <summary>VK_DBE_HIRAGANA</summary>
        Hiragana = KBDNLS_HIRAGANA,
        /// <summary>VK_DBE_KATAKANA</summary>
        Katakana = KBDNLS_KATAKANA,
        /// <summary>VK_DBE_SBCSCHAR/VK_DBE_DBCSCHAR</summary>
        SbcsDbcs = KBDNLS_SBCSDBCS,
        /// <summary>VK_DBE_ROMAN/VK_DBE_NOROMAN</summary>
        Roman = KBDNLS_ROMAN,
        /// <summary>VK_DBE_CODEINPUT/VK_DBE_NOCODEINPUT</summary>
        CodeInput = KBDNLS_CODEINPUT,
        /// <summary>VK_HELP or VK_END [NEC PC-9800 Only]</summary>
        HelpOrEnd = KBDNLS_HELP_OR_END,
        /// <summary>VK_HOME or VK_CLEAR [NEC PC-9800 Only]</summary>
        HomeOrClear = KBDNLS_HOME_OR_CLEAR,
        /// <summary>VK_NUMPAD? for Numpad key [NEC PC-9800 Only]</summary>
        Numpad = KBDNLS_NUMPAD,
        /// <summary>VK_KANA [Fujitsu FMV oyayubi Only]</summary>
        KanaEvent = KBDNLS_KANAEVENT
    };

}}}