#include "DeadKey.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

DeadKey::DeadKey(PDEADKEY deadKey)
{
    if(deadKey == NULL) throw gcnew ArgumentException("deadKey");
    this->deadKey = deadKey;
}

Char DeadKey::Char::get(){
    return HIWORD(deadKey->dwBoth);
}
System::Char DeadKey::Accent::get(){
    return LOWORD(deadKey->dwBoth);
}
System::Char DeadKey::Composed::get(){
    return deadKey->wchComposed;
}
DeadKeyFlags DeadKey::Flags::get(){
    return (DeadKeyFlags)deadKey->uFlags;
}
