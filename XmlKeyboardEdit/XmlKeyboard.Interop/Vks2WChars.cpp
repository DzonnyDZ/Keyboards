#include "Vks2WChars.h"
#include "Utils.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Vks2WChars::Vks2WChars(PVK_TO_WCHAR_TABLE vk2WChar)
{
    if(vk2WChar == NULL) throw gcnew ArgumentNullException("vk2WChar");
    this->vk2WChar = vk2WChar;
}

cli::array<Vk2WChar^>^ Vks2WChars::Mappings::get(){
    INITARRAY(mappings, vk2WChar->pVkToWchars, Vk2WChar^, (InitArrayUntilZero<VK_TO_WCHARS1, Vk2WChar, BYTE>(vk2WChar->pVkToWchars, vk2WChar->nModifications)));
    return mappings;
}
