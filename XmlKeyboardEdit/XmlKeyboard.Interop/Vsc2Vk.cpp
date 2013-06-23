#include "Vsc2Vk.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Vsc2Vk::Vsc2Vk(PVSC_VK vsc2Vk)
{
    if(vsc2Vk == NULL) throw gcnew ArgumentNullException("vsc2Vk");
    this->vsc2Vk = vsc2Vk;
}
