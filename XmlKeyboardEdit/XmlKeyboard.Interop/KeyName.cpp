#include "KeyName.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

KeyName::KeyName(PVSC_LPWSTR keyName)
{
    if(keyName == NULL) throw gcnew ArgumentNullException("keyName");
    this->keyName = keyName;
}

BYTE KeyName::ScanCode::get(){
    return keyName->vsc;
}

String^ KeyName::Name::get(){
    if(name == nullptr){
        name = gcnew String(keyName->pwsz);
    }
    return name;
}
