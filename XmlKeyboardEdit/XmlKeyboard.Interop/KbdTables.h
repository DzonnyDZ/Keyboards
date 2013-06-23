#pragma once
#include "Stdafx.h"
#include "Modifiers.h"
#include "Vks2WChars.h"
#include "KeyName.h"
#include "DeadKeyName.h"
#include "Vsc2Vk.h"
#include "Ligature.h"
#include "DeadKey.h"

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    enum class LocaleFlags : WORD;
    enum class KbdVersion : WORD;

    /// <summary>Contains various tables that contain basic definition of a keyboard layout</summary>
    public ref class KbdTables
    {
    private:
        /// <summary>A pointer to unmanaged <see cref="KBDTABLES"/> structure this instance wraps</summary>
        initonly PKBDTABLES kbdTables;

        Modifiers^ modifiers;
        cli::array<Vks2WChars^>^ vks2WChars;
        cli::array<DeadKey^>^ deadKeys;
        cli::array<KeyName^>^ keyNames;
        cli::array<KeyName^>^ keyNamesExt;
        cli::array<DeadKeyName^>^ deadKeyNames;
        cli::array<Vsc2Vk^>^ vsc2VkE0;
        cli::array<Vsc2Vk^>^ vsc2VkE1;
        cli::array<Ligature^>^ ligatures;
        cli::array<USHORT>^ scan2Vk;
    internal:
        /// <summary>CTor - creates a new instance of the <see cref="KbdTables"/> class</summary>
        /// <param name="kbdTables">A pointer to unmanaged <see cref="KBDTABLES"/> structure to wrap in a new instance</param>
        /// <exception cref="ArgumentNullException"><paramref name="kbdTables"/> is null</exception>
        KbdTables(const PKBDTABLES kbdTables);
    public:
        /// <summary>Gets modifier specification</summary>
        property Modifiers^ Modifiers{Dzonny::XmlKeyboard::Interop::Modifiers^ get();} 
        /// <summary>Gets mapping of Virtual keys to characters</summary>
        property cli::array<Vks2WChars^>^ Vks2WChars{cli::array<Dzonny::XmlKeyboard::Interop::Vks2WChars^>^ get();}
        /// <summary>Gets dead key definitions</summary>
        property cli::array<DeadKey^>^ DeadKeys{cli::array<DeadKey^>^ get();}
        /// <summary>Gets name of basic keys</summary>
        property cli::array<KeyName^>^ KeyNames{cli::array<KeyName^>^ get();}
        /// <summary>Gets names of extended keys</summary>
        property cli::array<KeyName^>^ KeyNamesExt{cli::array<KeyName^>^ get();}
        /// <summary>Kets names of dead keys</summary>
        property cli::array<DeadKeyName^>^ DeadKeyNames{cli::array<DeadKeyName^>^ get();}
        /// <summary>Gets mapping of Virtual Scan codes to Virtual Key codes for basic keys</summary>
        property cli::array<USHORT>^ Scan2Vk{cli::array<USHORT>^ get();}
        /// <summary>Gets mapping of Virtual Scan codes to Virtual Key codes for extended keys</summary>
        property cli::array<Vsc2Vk^>^ Vsc2VkE0{cli::array<Vsc2Vk^>^ get();}
        /// <summary>Gets mapping of Virtual Scan codes to Virtual Key codes for special key like Ctrl+Break</summary>
        property cli::array<Vsc2Vk^>^ Vsc2VkE1{cli::array<Vsc2Vk^>^ get();}
        /// <summary>Gets locale-specific flags</summary>
        property LocaleFlags LocaleFlags{Dzonny::XmlKeyboard::Interop::LocaleFlags get();}
        /// <summary>Gets version of keyboard layout API</summary>
        property KbdVersion KbdVersion{Dzonny::XmlKeyboard::Interop::KbdVersion get();}
        /// <summary>Gets ligatures definition</summary>
        property cli::array<Ligature^>^ Ligatures{cli::array<Ligature^>^ get();}
        /// <summary>Gets keboad type (if specified)</summary>
        property Nullable<DWORD> Type{Nullable<DWORD> get();}
        /// <summary>Gets keyboard sub-type  (if specified)</summary>
        property Nullable<WORD> SubType{Nullable<WORD> get();}
        /// <summary>Gets keyboard OEM id  (if specified)</summary>
        property Nullable<WORD> OemId{Nullable<WORD> get();}
    }; 

    /// <summary>Defines locale flasgs for keyboard layout</summary>
    [Flags]
    public enum class LocaleFlags : WORD{
        /// <summary>No locale flags specified</summary>
        none = 0,
        /// <summary>Right ALt is treated as Ctrl+Alt (AltGr)</summary>
        AltGr = KLLF_ALTGR,
        /// <summary>CapsLOck is turned off when Shift is pressed (ShiftLock)</summary>
        ShiftLock = KLLF_SHIFTLOCK,
        /// <summary>Left Shift + Backspace generates Left to Right marker (LRM), Right Shift + Backspace generates Right to Left marker (RLM)</summary>
        LrmRlm = KLLF_LRM_RLM,
        /// <summary>All values combined</summary>
        LayoutAttributes = KLLF_LAYOUT_ATTRS,
        /// <summary>Global attributes - equals <see cref="ShiftLock"/></summary>
        GlobalAttributes = KLLF_GLOBAL_ATTRS
    };

    /// <summary>Defines known keyboard layout API version</summary>
    public enum class KbdVersion : WORD{
        /// <summary>Previous version (0)</summary>
        zero = 0,
        /// <summary>Version 1</summary>
        one = 1
    };
}}}    