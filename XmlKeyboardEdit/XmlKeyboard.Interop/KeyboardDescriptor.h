#pragma once
#include "stdafx.h"

using namespace System;
using namespace Tools;

namespace Dzonny {
    namespace XmlKeyboard {

        /// <summary>Delegate to a function that returns keyboard layout description</summary>
        /// <returns>A pointer to <see cref="KBDTABLES"/> structure</returns>
        private delegate PKBDTABLES KbdLayerDescriptor();
        /// <summary>Delegate to a function that returns additional description of kayboard layout for fareast</summary>
        /// <returns>A pointer to <see cref="KBDNLSTABLES"/> structure</returns>
        private delegate PKBDNLSTABLES KbdNlsLayerDescriptor();

        /// <summary>Managed keyboard descriptor</summary>
        ref class KeyboardDescriptor
        {
        private:
            BYTE bvalue;
            /// <summary>Pointer to a <see cref="KBDTABLES"/> structure that contains basic keyboard description</summary>
            initonly PKBDTABLES kbdTables;
            /// <summary>Pointer to a <see cref="KBDNLSTABLES"/> structure that contains extended far-east keyboard features.</summary>
            /// <remarks>May be null</remarks>
            initonly PKBDNLSTABLES kbdNlsTables;
        internal:
            /// <summary>CTor - creates a new instance of the <see cref="KeyboardDescriptor"/> class</summary>
            /// <param name="kbdTables">Pointer to a <see cref="KBDTABLES"/> structure that contains basic keyboard description</param>
            /// <param name="kbdNlsTables">Pointer to a <see cref="KBDNLSTABLES"/> structure that contains extended far-east keyboard features. May be null.</param>
            /// <exception cref="ArgumentNullException"><paramref name="kbdTables"/> is null</exception>
            KeyboardDescriptor(PKBDTABLES kbdTables, PKBDNLSTABLES kbdNlsTables);
        public:
            /// <summary>Loads a keyboard layout from a DLL</summary>
            /// <param name="dllPath">Full path, or file name of DLL to load keyboard from</param>
            /// <returns>A new instance of the <see cref="KeyboardDescriptor"/> that provides description of a keyboard loaded from given DLL.</returns>
            /// <remarks>When <paramref name="dllPath"/> is just file name (i.e. neither relative nor absolute path) the DLL is looked for in Windows' System32 folder. Otherwise the relative or absolute path is used.</remarks>
            /// <exception cref="ArgumentNullException"><paramref name="dllPath"/> is null</exception>
            /// <exception cref="IO::FileNotFoundException">File identified by <paramref name="dllPath"/> cannot be found</exception>
            /// <exception cref="API::Win32ApiException">Failed to load unmanaged module identified by <paramref name="dllPath"/> or the module does not define the <c>KbdLayerDescriptor</c> function</exception>
            static KeyboardDescriptor^ LoadKeyboard(String^ dllPath);
        };
    }
}