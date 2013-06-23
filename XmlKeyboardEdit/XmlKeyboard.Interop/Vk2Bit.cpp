#include "Vk2Bit.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Vk2Bit::Vk2Bit(const PVK_TO_BIT vk2Bit)
{
    if(vk2Bit == NULL) throw gcnew ArgumentException("vk2Bit");
    this->vk2Bit = vk2Bit;
}

BYTE Vk2Bit::VirtualKey::get(){
    return vk2Bit->Vk;
}
BYTE Vk2Bit::Modifiers::get(){
    return vk2Bit->ModBits;
}
