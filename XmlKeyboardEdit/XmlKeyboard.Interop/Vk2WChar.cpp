#include "Vk2WChar.h"
#include "Utils.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Vk2WChar::Vk2WChar(const PVK_TO_WCHARS1 vk2WChars, BYTE modifications){
    if(vk2WChars == NULL) throw gcnew ArgumentNullException("vk2WChars");
    this->vk2WChars = vk2WChars;
    this->modifications = modifications;
}

BYTE Vk2WChar::VirtualKey::get(){
    return vk2WChars->VirtualKey;
}

Vk2CharAttributes Vk2WChar::Attributes::get(){
    return (Vk2CharAttributes)vk2WChars->Attributes;
}

cli::array<Char>^ Vk2WChar::Chars::get(){
    INITARRAY(chars, vk2WChars->wch, Char, (InitArrayCount<Char>(vk2WChars->wch, modifications)));
    return chars;
}