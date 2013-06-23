#include "Vsc2Vk.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Vsc2Vk::Vsc2Vk(PVSC_VK vsc2Vk)
{
    if(vsc2Vk == NULL) throw gcnew ArgumentNullException("vsc2Vk");
    this->vsc2Vk = vsc2Vk;
}

BYTE Vsc2Vk::ScanCode::get(){
    return vsc2Vk->Vsc;
}

USHORT Vsc2Vk::VirtualKey::get(){
    return vsc2Vk->Vk;
}