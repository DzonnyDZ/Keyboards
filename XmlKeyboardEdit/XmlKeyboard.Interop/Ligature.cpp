#include "Ligature.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

Ligature::Ligature(PLIGATURE1 ligature, BYTE maxChars)
{
    if(ligature == NULL) throw gcnew ArgumentException("ligature");
    this->ligature = ligature;
    this->maxChars = maxChars;
}

BYTE Ligature::VirtualKey::get(){
    return ligature->VirtualKey;
}

WORD Ligature::ModificationNumber::get(){
    return ligature->ModificationNumber;
}

String^ Ligature::Characters::get(){
    if(characters == nullptr){
        characters = gcnew String(ligature->wch);
    }
    return characters;
}
