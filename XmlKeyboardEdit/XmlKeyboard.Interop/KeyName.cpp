#include "KeyName.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;
using namespace System::Runtime::InteropServices;

KeyName::KeyName(PVSC_LPWSTR keyName)
{
    if(keyName == NULL) throw gcnew ArgumentNullException("keyName");
    this->keyName = keyName;
}

BYTE KeyName::ScanCode::get(){
    return keyName->vsc;
}

String^ KeyName::Name::get(){
    if(name == nullptr && keyName->pwsz != NULL){
        name = Marshal::PtrToStringUni((IntPtr)(void*)keyName->pwsz);
    }
    return name;
}
