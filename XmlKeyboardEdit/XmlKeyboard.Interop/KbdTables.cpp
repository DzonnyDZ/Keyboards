#include "KbdTables.h"
#include "Utils.h"
using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

KbdTables::KbdTables(PKBDTABLES kbdTables)
{
    if(kbdTables == NULL) throw gcnew ArgumentNullException("kbdTables");
    this->kbdTables = kbdTables;
}

Modifiers^ KbdTables::Modifiers::get(){
    if(kbdTables->pCharModifiers == NULL) return nullptr;
    if(modifiers == nullptr) modifiers = gcnew Dzonny::XmlKeyboard::Interop::Modifiers(kbdTables->pCharModifiers);
    return modifiers;
} 
cli::array<Vks2WChars^>^ KbdTables::Vks2WChars::get(){
    INITARRAY(vks2WChars, kbdTables->pVkToWcharTable, Dzonny::XmlKeyboard::Interop::Vks2WChars^, (InitArrayUntilZero<VK_TO_WCHAR_TABLE, Dzonny::XmlKeyboard::Interop::Vks2WChars>(kbdTables->pVkToWcharTable)));
    return vks2WChars;
}
cli::array<DeadKey^>^ KbdTables::DeadKeys::get(){
    INITARRAY(deadKeys, kbdTables->pDeadKey, DeadKey^, (InitArrayUntilZero<DEADKEY, DeadKey>(kbdTables->pDeadKey)));
    return deadKeys;
}
cli::array<KeyName^>^ KbdTables::KeyNames::get(){
    INITARRAY(keyNames, kbdTables->pKeyNames, KeyName^, (InitArrayUntilZero<VSC_LPWSTR, KeyName>(kbdTables->pKeyNames)));
    return keyNames;
}
cli::array<KeyName^>^ KbdTables::KeyNamesExt::get(){
    INITARRAY(keyNamesExt, kbdTables->pKeyNamesExt, KeyName^, (InitArrayUntilZero<VSC_LPWSTR, KeyName>(kbdTables->pKeyNamesExt)));
    return keyNamesExt;
}
cli::array<DeadKeyName^>^ KbdTables::DeadKeyNames::get(){
    INITARRAY(deadKeyNames, kbdTables->pKeyNamesDead, DeadKeyName^, (InitArrayUntilNull<WCHAR, DeadKeyName>(kbdTables->pKeyNamesDead))); 
    return deadKeyNames;
}
cli::array<USHORT>^ KbdTables::Scan2Vk::get(){
    INITARRAY(scan2Vk, kbdTables->pusVSCtoVK, USHORT, (InitArrayCount<USHORT>(kbdTables->pusVSCtoVK, kbdTables->bMaxVSCtoVK))); 
    return scan2Vk;
}
cli::array<Vsc2Vk^>^ KbdTables::Vsc2VkE0::get(){
    INITARRAY(vsc2VkE0, kbdTables->pVSCtoVK_E0, Vsc2Vk^, (InitArrayUntilZero<VSC_VK, Vsc2Vk>(kbdTables->pVSCtoVK_E0))); 
    return vsc2VkE0;
}
cli::array<Vsc2Vk^>^ KbdTables::Vsc2VkE1::get(){
    INITARRAY(vsc2VkE1, kbdTables->pVSCtoVK_E1, Vsc2Vk^, (InitArrayUntilZero<VSC_VK, Vsc2Vk>(kbdTables->pVSCtoVK_E1))); 
    return vsc2VkE1;
}
Dzonny::XmlKeyboard::Interop::LocaleFlags KbdTables::LocaleFlags::get(){
    return (Dzonny::XmlKeyboard::Interop::LocaleFlags)(KLL_ATTR_FROM_KLF(kbdTables->fLocaleFlags));
}
Dzonny::XmlKeyboard::Interop::KbdVersion KbdTables::KbdVersion::get(){
    return (Dzonny::XmlKeyboard::Interop::KbdVersion) GET_KBD_VERSION(kbdTables);
}
cli::array<Ligature^>^ KbdTables::Ligatures::get(){
    INITARRAY(ligatures, kbdTables->pLigature, Ligature^, (InitArrayUntilZero(kbdTables->pLigature, kbdTables->nLgMax, kbdTables->cbLgEntry)));
    return ligatures;
}
Nullable<DWORD> KbdTables::Type::get(){
    if(KbdVersion >= Dzonny::XmlKeyboard::Interop::KbdVersion::one)
        return kbdTables->dwType;
    return Nullable<DWORD>();
}

Nullable<UInt32> KbdTables::GetTypeValue() {
    return Type.HasValue ? (Nullable<UInt32>)(UInt32)Type.Value : Nullable<UInt32>();
}

Nullable<WORD> KbdTables::SubType::get(){
    if(KbdVersion >= Dzonny::XmlKeyboard::Interop::KbdVersion::one)
        return HIWORD(kbdTables->dwSubType);
    return Nullable<WORD>();
}
Nullable<WORD> KbdTables::OemId::get(){
    if(KbdVersion >= Dzonny::XmlKeyboard::Interop::KbdVersion::one)
        return LOWORD(kbdTables->dwSubType);
    return Nullable<WORD>();
}
