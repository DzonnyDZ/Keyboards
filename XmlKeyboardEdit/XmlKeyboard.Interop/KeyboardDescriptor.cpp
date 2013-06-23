#include "stdafx.h"
#include "KeyboardDescriptor.h"

using namespace Dzonny::XmlKeyboard::Interop;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::InteropT;

KeyboardDescriptor::KeyboardDescriptor(PKBDTABLES kbdTables, PKBDNLSTABLES kbdNlsTables)
{
    if(kbdTables == NULL) throw gcnew ArgumentNullException("kbdTables");
    this->kbdTables = kbdTables;
    this->kbdNlsTables = kbdNlsTables;
}

KeyboardDescriptor^ KeyboardDescriptor::LoadKeyboard(String^ dllPath){
    if(dllPath==nullptr) throw gcnew ArgumentNullException("dllPath");
    if(!dllPath->Contains(IO::Path::DirectorySeparatorChar.ToString()) && !dllPath->Contains(IO::Path::AltDirectorySeparatorChar.ToString()))
        dllPath = IO::Path::Combine(Environment::GetFolderPath(Environment::SpecialFolder::System), dllPath);
    if(!IO::File::Exists(dllPath)) throw gcnew IO::FileNotFoundException("File not found", dllPath);
    UnmanagedModule^ dllModule;
    try{
        dllModule= UnmanagedModule::LoadLibrary(dllPath); //Local object gets disposed automatically on method exit
        IntPtr kbdLayerDescriptor = dllModule->GetProcedureAddress(L"KbdLayerDescriptor");
        IntPtr kbdNlsLayerDescriptor = dllModule->TryGetProcedureAddress(L"KbdNlsLayerDescriptor");
        KbdLayerDescriptor^ kbdLayerDescriptorDelegate = (KbdLayerDescriptor^)Marshal::GetDelegateForFunctionPointer (kbdLayerDescriptor, KbdLayerDescriptor::typeid);
        KbdNlsLayerDescriptor^ kbdNlsLayerDescriptorDelegate =nullptr;
        if(kbdNlsLayerDescriptor!= IntPtr::Zero) 
            kbdNlsLayerDescriptorDelegate=(KbdNlsLayerDescriptor^)Marshal::GetDelegateForFunctionPointer(kbdNlsLayerDescriptor, KbdNlsLayerDescriptor::typeid);
        PKBDTABLES kbdTables = kbdLayerDescriptorDelegate();
        PKBDNLSTABLES kbdNlsTables = NULL;
        if(kbdNlsLayerDescriptorDelegate!=nullptr)
            kbdNlsTables = kbdNlsLayerDescriptorDelegate();
        return gcnew KeyboardDescriptor(kbdTables, kbdNlsTables);
    }finally{
        delete dllModule;
    }
}

KbdTables^ KeyboardDescriptor::KbdTables::get(){
    if(kbdTablesWrapper == nullptr) kbdTablesWrapper = gcnew Dzonny::XmlKeyboard::Interop::KbdTables(kbdTables);
    return kbdTablesWrapper;
}

KbdNlsTables^ KeyboardDescriptor::KbdNlsTables::get(){
    if(kbdNlsTables == NULL) return nullptr;
    if(kbdNlsTablesWrapper == nullptr) kbdNlsTablesWrapper = gcnew Dzonny::XmlKeyboard::Interop::KbdNlsTables(kbdNlsTables);
    return kbdNlsTablesWrapper;
}