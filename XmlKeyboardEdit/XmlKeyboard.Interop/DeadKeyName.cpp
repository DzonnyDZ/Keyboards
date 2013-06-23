#include "DeadKeyName.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

DeadKeyName::DeadKeyName(const WCHAR* keyName)
{
    if(keyName == NULL) throw gcnew ArgumentNullException("keyName");
    this->keyName = keyName;
}

Char DeadKeyName::Accent::get(){
    return keyName[0];
}
String^ DeadKeyName::Name::get(){
    if(name == nullptr){
        int i;
        for(i = 0; keyName[i] != 0; i++);
        name = gcnew String(keyName + i + 1);
    }
    return name;
}
