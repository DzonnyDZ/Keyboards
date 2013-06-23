#include "Modifiers.h"
#include "Utils.h"

using namespace System;
using namespace Dzonny::XmlKeyboard::Interop;

Modifiers::Modifiers(PMODIFIERS modifiers)
{
    if(modifiers == NULL) throw gcnew ArgumentNullException("modifiers");
    this->modifiers = modifiers;
}

cli::array<Vk2Bit^>^ Modifiers::Vk2Bits::get(){
    INITARRAY(vk2Bits, modifiers->pVkToBit, Vk2Bit^, (InitArrayUntilZero<VK_TO_BIT, Vk2Bit>(modifiers->pVkToBit)))
    return vk2Bits;
}
cli::array<const BYTE>^ Modifiers::ModNumbers::get(){
    INITARRAY(modNumbers, modifiers->ModNumber, BYTE, (InitArrayCount<BYTE>(modifiers->ModNumber, modifiers->wMaxModBits + 1)))
    return modNumbers;
}
