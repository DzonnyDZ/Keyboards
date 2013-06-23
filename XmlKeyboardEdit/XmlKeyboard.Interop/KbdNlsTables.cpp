#include "KbdNlsTables.h"


using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;

KbdNlsTables::KbdNlsTables(PKBDNLSTABLES kbdNlsTables)
{
    if(kbdNlsTables == NULL) throw gcnew ArgumentNullException("kbdNlsTables");
    this->kbdNlsTables = kbdNlsTables;
}
