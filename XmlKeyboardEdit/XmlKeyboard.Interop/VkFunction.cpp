#include "VkFunction.h"
#include "Utils.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

VkFunction::VkFunction(PVK_F vkf)
{
    if(vkf == NULL) throw gcnew ArgumentNullException("vkf");
    this->vkf = vkf;
}


BYTE VkFunction::VirtualKey::get(){
    return vkf->Vk;
}

NlsProcType VkFunction::ProcType::get(){
    return (NlsProcType)vkf->NLSFEProcType;
}

NlsProcIndex VkFunction::ProcCurrent::get(){
    return (NlsProcIndex)vkf->NLSFEProcCurrent;
}

BYTE VkFunction::ProcSwitch::get(){
    return vkf->NLSFEProcSwitch;
}

cli::array<NlsFParam^>^ VkFunction::Procs::get(){
    INITARRAY(procs, vkf->NLSFEProc, NlsFParam^, (InitArrayCount<VK_FPARAM, NlsFParam>(vkf->NLSFEProc, 8)));
    return procs;
}

cli::array<NlsFParam^>^ VkFunction::AltProcs::get(){
    INITARRAY(altProcs, vkf->NLSFEProcAlt, NlsFParam^, (InitArrayCount<VK_FPARAM, NlsFParam>(vkf->NLSFEProcAlt, 8)));
    return altProcs;
}