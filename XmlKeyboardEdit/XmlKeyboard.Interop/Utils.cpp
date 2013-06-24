#include "Utils.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace Dzonny::XmlKeyboard::Interop;

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

#pragma region IsZero
    template <>
    bool IsZero<VK_TO_WCHAR_TABLE>(PVK_TO_WCHAR_TABLE item){
        return item == NULL || (item->pVkToWchars == NULL && item->nModifications == 0 && item->cbSize == 0);
    }

    template <>
    bool IsZero<DEADKEY>(PDEADKEY item){
        return item == NULL || (item->dwBoth == 0 && item->wchComposed == 0);
    }

    template <>
    bool IsZero<VSC_LPWSTR>(PVSC_LPWSTR item){
        return item == NULL || (item->vsc == 0 && item->pwsz == NULL);
    }

    template<>
    bool IsZero<VSC_VK>(PVSC_VK item){
        return item == NULL || (item->Vsc == 0 && item->Vk == 0);
    }

    template<>
    bool IsZero<LIGATURE1>(PLIGATURE1 item){
        return item == NULL || (item->VirtualKey == 0 && item->ModificationNumber == 0);
    }

    template<>
    bool IsZero<VK_TO_BIT>(PVK_TO_BIT item){
        return item == NULL || (item->Vk == 0 && item->ModBits == 0);
    }

    template<>
    bool IsZero<VK_TO_WCHARS1>(PVK_TO_WCHARS1 item){
        return item == NULL || (item->Attributes == 0 && item->VirtualKey == 0 && item->wch[0] == 0);
    }
#pragma endregion

    cli::array<Ligature^>^ InitArrayUntilZero(PLIGATURE1 pointer, BYTE ligatureMax, BYTE ligatureLength){
        List<Ligature^>^ ret = gcnew List<Ligature^>();
        if(ligatureLength > 0){
            for(auto ptr = pointer; !IsZero<LIGATURE1>(ptr); ptr = (PLIGATURE1)((BYTE*)ptr + ligatureLength)){
                ret->Add(gcnew Ligature((PLIGATURE1)ptr, ligatureMax));
            }
        }
        return ret->ToArray();
    }

}}}