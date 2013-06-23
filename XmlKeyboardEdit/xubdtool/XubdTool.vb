Imports Tools.InteropT
Imports Dzonny.XmlKeyboard.Interop

Friend Module XubdTool

    Public Sub Main()

        If My.Application.CommandLineArgs.Count = 0 Then
            Console.WriteLine("Error - no parameters specified")
            PrintParams()
            Environment.Exit(1)
        End If
        Select Case My.Application.CommandLineArgs(0)
            Case "-?", "-h", "/?", "/h", "h", "?", "help"
                PrintParams()
            Case "list"
                List()
            Case "makexml"
                If My.Application.CommandLineArgs.Count < 3 Then
                    Console.WriteLine("Error - insufficient number of parameters")
                    PrintParams()
                    Environment.Exit(1)
                End If
                MakeXml(My.Application.CommandLineArgs(1), My.Application.CommandLineArgs(2))
            Case "print"
                If My.Application.CommandLineArgs.Count < 2 Then
                    Console.WriteLine("Error - insufficient number of parameters")
                    PrintParams()
                    Environment.Exit(1)
                End If
                Print(My.Application.CommandLineArgs(1))
        End Select
    End Sub

    Private Sub PrintParams()
        Console.WriteLine("{0} mode params", My.Application.Info.AssemblyName)
        Console.WriteLine("mode:")
        Console.WriteLine("-h, -?, /h, /?, ?, help, h")
        Console.WriteLine(vbTab & "(no params), prints this info")
        Console.WriteLine("list")
        Console.WriteLine(vbTab & "(no params), lists all system keyboard layouts")
        Console.WriteLine("makexml")
        Console.WriteLine(vbTab & "params: layoutname outfile; exports given keyboard layout to XML file")
        Console.WriteLine(vbTab & "layoutname - name of layout to export (name of DLL file or full path to DLL file)")
        Console.WriteLine(vbTab & "outfile - path of XML file to export layout to")
        Console.WriteLine("print")
        Console.WriteLine(vbTab & "params: layoutname; prints information ybout given keyboard layout")
        Console.WriteLine(vbTab & "layoutname - name of layout to print (name of DLL file or full path to DLL file)")

    End Sub

    Private Sub List()
        For Each layout In KeyboardLayoutInfo.GetInstalledLayouts
            Console.WriteLine("{0} {1} {2}", layout.LayoutFile, layout.LayoutDisplayName, layout.LayoutText)
        Next
    End Sub

    Private Sub MakeXml(dll$, xml$)
        Using info = KeyboardDescriptor.LoadKeyboard(dll)
            'TODO:
        End Using
    End Sub

#Region "Print"
    Private level As Integer = 0
    Private Sub Cv(text As String, ParamArray params As Object())
        Console.WriteLine(New String(CChar(vbTab), level) & String.Format(text, params))
    End Sub
    Private Sub Print(dll$)
        Using info = KeyboardDescriptor.LoadKeyboard(dll)
            Print(info)
        End Using
    End Sub

    Private Sub Print(info As KeyboardDescriptor)
        If info Is Nothing Then Console.WriteLine("null") : Return
        Cv("KBD tables")
        Using rl As New RaisedLevel
            Print(info.KbdTables)
        End Using
        Cv("KBD NLS tables")
        Using rl As New RaisedLevel
            Print(info.KbdNlsTables)
        End Using
    End Sub

    Private Sub Print(kbdTables As KbdTables)
        If kbdTables Is Nothing Then Cv("null") : Return
        Cv("Modifiers")
        Using rl As New RaisedLevel
            Print(kbdTables.Modifiers)
        End Using
        Cv("VK 2 WChar")
        Using rl As New RaisedLevel
            Print(kbdTables.Vks2WChars)
        End Using
        Cv("Dead keys")
        Using rl As New RaisedLevel
            Print(kbdTables.DeadKeys)
        End Using
        Cv("Key names")
        Using rl As New RaisedLevel
            Print(kbdTables.KeyNames)
        End Using
        Cv("Extended key names")
        Using rl As New RaisedLevel
            Print(kbdTables.KeyNamesExt)
        End Using
        Cv("Dead key names")
        Using rl As New RaisedLevel
            Print(kbdTables.DeadKeys)
        End Using
        Cv("Scan Code 2 VK")
        Using rl As New RaisedLevel
            Print(kbdTables.Scan2Vk)
        End Using
        Cv("Scan 2 VK E0")
        Using rl As New RaisedLevel
            Print(kbdTables.Vsc2VkE0)
        End Using
        Cv("Scan 2 VK E1")
        Using rl As New RaisedLevel
            Print(kbdTables.Vsc2VkE1)
        End Using
        Cv("Locale flags {0}", kbdTables.LocaleFlags)
        Cv("API version {0}", kbdTables.KbdVersion)
        Cv("Ligatures")
        Using rl As New RaisedLevel
            Print(kbdTables.Ligatures)
        End Using
        Cv("Keyboard type {0}", kbdTables.Type)
        Cv("Keyboard sub-type {0}", kbdTables.SubType)
        Cv("OEM ID{0}", kbdTables.OemId)
    End Sub

    Private Sub Print(kbdNlsTables As KbdNlsTables)
        If kbdNlsTables Is Nothing Then Cv("null") : Return
        Cv("OEM ID {0}", kbdNlsTables.OemIdentifier)
        Cv("Layout info {0}", kbdNlsTables.LayoutInformation)
        Cv("VK_F")
        Using rl As New RaisedLevel
            Print(kbdNlsTables.VkFunctions)
        End Using
        Cv("Mouse")
        Using rl As New RaisedLevel
            Print(kbdNlsTables.MouseVKeys)
        End Using
    End Sub

    Private Sub Print(array As UShort())
        If array Is Nothing Then Cv("null") : Return
        If array.Length = 0 Then Cv("empty") : Return
        Cv(String.Join(" ", array))
    End Sub

    Private Sub Print(functions As VkFunction())
        If functions Is Nothing Then Cv("null") : Return
        If functions.Length = 0 Then Cv("empty") : Return
        For Each func In functions
            Print(func)
        Next
    End Sub

    Private Sub Print(ligatures As Ligature())
        If ligatures Is Nothing Then Cv("null") : Return
        If ligatures.Length = 0 Then Cv("empty") : Return
        For Each ligature In ligatures
            Print(ligature)
        Next
    End Sub

    Private Sub Print(vsc2Vks As Vsc2Vk())
        If vsc2Vks Is Nothing Then Cv("null") : Return
        If vsc2Vks.Length = 0 Then Cv("empty") : Return
        For Each vsc2Vk In vsc2Vks
            Print(vsc2Vk)
        Next
    End Sub

    Private Sub Print(deadKeys As DeadKey())
        If deadKeys Is Nothing Then Cv("null") : Return
        If deadKeys.Length = 0 Then Cv("empty") : Return
        For Each deadKey In deadKeys
            Print(deadKey)
        Next
    End Sub

    Private Sub Print(keyNames As KeyName())
        If keyNames Is Nothing Then Cv("null") : Return
        If keyNames.Length = 0 Then Cv("empty") : Return
        For Each keyName In keyNames
            Print(keyName)
        Next
    End Sub

    Private Sub Print(vks2WChars As Vks2WChars())
        If vks2WChars Is Nothing Then Cv("null") : Return
        If vks2WChars.Length = 0 Then Cv("empty") : Return
        For Each vks2WChar In vks2WChars
            Print(vks2WChar)
        Next
    End Sub

    Private Sub Print(modifiers As Modifiers)
        If modifiers Is Nothing Then Cv("null") : Return
        Cv("VK 2 bit")
        Using rl As New RaisedLevel
            Print(modifiers.Vk2Bits)
        End Using
        Cv("Mod numbers")
        Using rl As New RaisedLevel
            Print(modifiers.ModNumbers)
        End Using
    End Sub

    Private Sub Print(func As VkFunction)
        If func Is Nothing Then Cv("null") : Return
        Cv("VK {0}", func.VirtualKey)
        Cv("Proc type {0}", func.ProcType)
        Cv("Proc current {0}", func.ProcCurrent)
        Cv("Proc switch {0}", func.ProcSwitch)
        Cv("Procs")
        Using rl As New RaisedLevel
            Print(func.Procs)
        End Using
        Cv("Procs Alt")
        Using rl As New RaisedLevel
            Print(func.AltProcs)
        End Using
    End Sub

    Private Sub Print(ligature As Ligature)
        If ligature Is Nothing Then Cv("null") : Return
        Cv("VK {0}", ligature.VirtualKey)
        Cv("Mod number {0}", ligature.ModificationNumber)
        Cv("Characters {0}", ligature.Characters)
    End Sub

    Private Sub Print(vsc2Vk As Vsc2Vk)
        If vsc2Vk Is Nothing Then Cv("null") : Return
        Cv("VSC {0}", vsc2Vk.ScanCode)
        Cv("VK {0}", vsc2Vk.VirtualKey)
    End Sub

    Private Sub Print(deadKey As DeadKey)
        If deadKey Is Nothing Then Cv("null") : Return
        Cv("Accent {0}", deadKey.Accent)
        Cv("Char {0}", deadKey.Char)
        Cv("Composed {0}", deadKey.Composed)
        Cv("Flags {0}", deadKey.Flags)
    End Sub

    Private Sub Print(keyName As KeyName)
        If keyName Is Nothing Then Cv("null") : Return
        Cv("Scan code {0}", keyName.ScanCode)
        Cv("Name {0}", keyName.Name)
    End Sub

    Private Sub Print(vks2WChar As Vks2WChars)
        If vks2WChar Is Nothing Then Cv("null") : Return
        Cv("Mappings")
        Using rl As New RaisedLevel
            Print(vks2WChar.Mappings)
        End Using
    End Sub

    Private Sub Print(vk2Bits As Vk2Bit())
        If vk2Bits Is Nothing Then Cv("null") : Return
        If vk2Bits.Length = 0 Then Cv("empty") : Return
        For Each vk2Bit In vk2Bits
            Print(vk2Bit)
        Next
    End Sub

    Private Sub Print(array As Byte())
        If array Is Nothing Then Cv("null") : Return
        If array.Length = 0 Then Cv("empty") : Return
        Cv(String.Join(" ", array))
    End Sub

    Private Sub Print(nlsFParams As NlsFParam())
        If nlsFParams Is Nothing Then Cv("null") : Return
        If nlsFParams.Length = 0 Then Cv("empty") : Return
        For Each nlsFParam In nlsFParams
            Print(nlsFParam)
        Next
    End Sub

    Private Sub Print(vk2WChars As Vk2WChar())
        If vk2WChars Is Nothing Then Cv("null") : Return
        If vk2WChars.Length = 0 Then Cv("empty") : Return
        For Each vk2WChar In vk2WChars
            Print(vk2WChar)
        Next
    End Sub

    Private Sub Print(vk2Bit As Vk2Bit)
        If vk2Bit Is Nothing Then Cv("null") : Return
        Cv("VK {0}", vk2Bit.VirtualKey)
        Cv("Modifiers {0}", vk2Bit.Modifiers)
    End Sub

    Private Sub Print(nlsFParam As NlsFParam)
        If nlsFParam Is Nothing Then Cv("null") : Return
        Cv("Index {0}", nlsFParam.Index)
        Cv("Param {0}", nlsFParam.Param)
    End Sub

    Private Sub Print(vk2WChar As Vk2WChar)
        If vk2WChar Is Nothing Then Cv("null") : Return
        Cv("VK {0}", vk2WChar.VirtualKey)
        Cv("Attributes {0}", vk2WChar.Attributes)
        Cv("chars")
        Using rl As New RaisedLevel
            Print(vk2WChar.Chars)
        End Using
    End Sub

    Private Sub print(array As Char())
        If array Is Nothing Then Cv("null") : Return
        If array.Length = 0 Then Cv("empty") : Return
        Cv(String.Join(" ", array))
    End Sub
#End Region

    Private Class RaisedLevel : Implements IDisposable
        Private disposed As Boolean
        Public Sub New()
            level += 1
        End Sub
        Private Sub Dispose() Implements IDisposable.Dispose
            If Not disposed Then
                level -= 1
                disposed = True
            End If
        End Sub

    End Class
End Module
