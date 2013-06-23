#include "NlsFParam.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

NlsFParam::NlsFParam(PVK_FPARAM fParam)
{
    if(fParam == NULL) throw gcnew ArgumentNullException("fParam");
    this->fParam = fParam;
}

NlsProcParamType NlsFParam::Index::get(){
    return (NlsProcParamType)fParam->NLSFEProcIndex;
}

ULONG NlsFParam::Param::get(){
    return fParam->NLSFEProcParam;
}