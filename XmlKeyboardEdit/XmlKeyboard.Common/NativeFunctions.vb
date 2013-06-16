Imports System.Runtime.InteropServices

''' <summary>Contains defintions of WinAPI P/Invoke functions</summary>
Friend Module NativeFunctions
    ' ''' <summary>Retrieves the input locale identifiers (formerly called keyboard layout handles) corresponding to the current set of input locales in the system. The function copies the identifiers to the specified buffer.</summary>
    ' ''' <param name="nBuff">The maximum number of handles that the buffer can hold.</param>
    ' ''' <param name="lpList">A pointer to the buffer that receives the array of input locale identifiers.</param>
    ' ''' <returns>If the function succeeds, the return value is the number of input locale identifiers copied to the buffer or, if nBuff is zero, the return value is the size, in array elements, of the buffer needed to receive all current input locale identifiers.</returns>
    'Public Declare Function GetKeyboardLayoutList Lib "user32" (ByVal nBuff As Integer, <Out, MarshalAs(UnmanagedType.LPArray)> ByVal lpList As IntPtr()) As Integer
End Module
