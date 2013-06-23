#include "Vk2WChar.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Vk2WChar::Vk2WChar(PVK_TO_WCHAR_TABLE vk2WChar)
{
    if(vk2WChar == NULL) throw gcnew ArgumentNullException("vk2WChar");
    this->vk2WChar = vk2WChar;
}
