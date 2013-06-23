#include "KbdNlsTables.h"
#include "Utils.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

KbdNlsTables::KbdNlsTables(PKBDNLSTABLES kbdNlsTables)
{
    if(kbdNlsTables == NULL) throw gcnew ArgumentNullException("kbdNlsTables");
    this->kbdNlsTables = kbdNlsTables;
}

USHORT KbdNlsTables::OemIdentifier::get(){
    return kbdNlsTables->OEMIdentifier;
}

USHORT KbdNlsTables::LayoutInformation::get(){
    return kbdNlsTables->LayoutInformation;
}

cli::array<VkFunction^>^ KbdNlsTables::VkFunctions::get(){
    INITARRAY(vkFunctions, kbdNlsTables->pVkToF, VkFunction^, (InitArrayCount<VK_F, VkFunction>(kbdNlsTables->pVkToF, kbdNlsTables->NumOfVkToF)));
    return vkFunctions;
}

cli::array<USHORT>^ KbdNlsTables::MouseVKeys::get(){
    INITARRAY(mouseVKeys, kbdNlsTables->pusMouseVKey, USHORT, (InitArrayCount<USHORT>(kbdNlsTables->pusMouseVKey, kbdNlsTables->NumOfMouseVKey)));
    return mouseVKeys;
}
