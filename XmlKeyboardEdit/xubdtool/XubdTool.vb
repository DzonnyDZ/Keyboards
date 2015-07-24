Imports Dzonny.XmlKeyboard.Interop
Imports Tools

''' <summary>Entry point of xubdtool</summary>
Friend Module XubdTool

    ''' <summary>Entry point method</summary>
    Public Sub Main()
        Console.OutputEncoding = System.Text.Encoding.Unicode

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

    ''' <summary>Prints program parameters documentation to command line</summary>
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
        Console.WriteLine(vbTab & "params: layoutname; prints information about given keyboard layout")
        Console.WriteLine(vbTab & "layoutname - name of layout to print (name of DLL file or full path to DLL file)")

    End Sub

    ''' <summary>Lists all installed keyboard layouts to command line</summary>
    Private Sub List()
        For Each layout In KeyboardLayoutInfo.GetInstalledLayouts
            Console.WriteLine("{0} {1} {2}", layout.LayoutFile, layout.LayoutDisplayName, layout.LayoutText)
        Next
    End Sub

    ''' <summary>Generates XML representation of one keyboard layout defined in a DLL</summary>
    ''' <param name="dll$">File name of installed layout DLL, or full path to layout DLL</param>
    ''' <param name="xml$">Path of XML file to write the layout to</param>
    ''' <exception cref="ArgumentNullException"><paramref name="dll"/> Is null</exception>
    ''' <exception cref="IO.FileNotFoundException">File identified by <paramref name="dll"/> cannot be found</exception>
    ''' <exception cref="API.Win32ApiException">Failed to load unmanaged module identified by <paramref name="dll"/> Or the module does Not define the <c>KbdLayerDescriptor</c> function</exception>
    Private Sub MakeXml(dll$, xml$)
        Using info = KeyboardDescriptor.LoadKeyboard(dll)
            'TODO:
        End Using
    End Sub

#Region "Print"
    ''' <summary>Current indentation</summary>
    Private levelIndicator As String

    ''' <summary>Writes indented text to console</summary>
    ''' <param name="text">Text to write (can be (<see cref="String.Format"/>-style pattern)</param>
    ''' <param name="params">Parameters for <see cref="String.Format"/></param>
    Private Sub Cv(text As String, ParamArray params As Object())
        If params Is Nothing OrElse params.Length = 0 Then
            Console.WriteLine(levelIndicator & text)
        Else
            Console.WriteLine(levelIndicator & String.Format(text, params))
        End If
    End Sub

    ''' <summary>Prints information about keyboard layout (specified by DLL file) to console</summary>
    ''' <param name="dll$">File name of installed layout DLL, or full path to layout DLL</param>
    ''' <exception cref="ArgumentNullException"><paramref name="dll"/> Is null</exception>
    ''' <exception cref="IO.FileNotFoundException">File identified by <paramref name="dll"/> cannot be found</exception>
    ''' <exception cref="API.Win32ApiException">Failed to load unmanaged module identified by <paramref name="dll"/> Or the module does Not define the <c>KbdLayerDescriptor</c> function</exception>
    Private Sub Print(dll$)
        Using info = KeyboardDescriptor.LoadKeyboard(dll)
            Print(info)
        End Using
    End Sub

    ''' <summary>Prints information about keyboard layout (specified by <see cref="KeyboardDescriptor"/>) to console</summary>
    ''' <param name="info">Object that represents the keyboard layout</param>
    Private Sub Print(info As KeyboardDescriptor)
        If info Is Nothing Then Console.WriteLine("null") : Return
        Cv("KBD tables")
        Using New RaisedLevel
            Print(info.KbdTables)
        End Using
        Cv("KBD NLS tables")
        Using New RaisedLevel
            Print(info.KbdNlsTables)
        End Using
    End Sub

    ''' <summary>Prints information about <see cref="KbdTables"/> to console</summary>
    ''' <param name="kbdTables">The <see cref="KbdTables"/> object</param>
    Private Sub Print(kbdTables As KbdTables)
        If kbdTables Is Nothing Then Cv("null") : Return
        Cv("Modifiers")
        Using New RaisedLevel
            Print(kbdTables.Modifiers)
        End Using
        Cv("VK 2 WChar")
        Using New RaisedLevel
            Print(kbdTables.Vks2WChars)
        End Using
        Cv("Dead keys")
        Using New RaisedLevel
            Print(kbdTables.DeadKeys)
        End Using
        Cv("Key names")
        Using New RaisedLevel
            Print(kbdTables.KeyNames)
        End Using
        Cv("Extended key names")
        Using New RaisedLevel
            Print(kbdTables.KeyNamesExt)
        End Using
        Cv("Dead key names")
        Using New RaisedLevel
            Print(kbdTables.DeadKeys)
        End Using
        Cv("Scan Code 2 VK")
        Using New RaisedLevel
            Print(kbdTables.Scan2Vk)
        End Using
        Cv("Scan 2 VK E0")
        Using New RaisedLevel
            Print(kbdTables.Vsc2VkE0)
        End Using
        Cv("Scan 2 VK E1")
        Using New RaisedLevel
            Print(kbdTables.Vsc2VkE1)
        End Using
        Cv("Locale flags {0}", kbdTables.LocaleFlags)
        Cv("API version {0}", kbdTables.KbdVersion)
        Cv("Ligatures")
        Using New RaisedLevel
            Print(kbdTables.Ligatures)
        End Using
        Cv("Keyboard type {0}", kbdTables.GetTypeValue())
        Cv("Keyboard sub-type {0}", kbdTables.SubType)
        Cv("OEM ID{0}", kbdTables.OemId)
    End Sub

    ''' <summary>Prints information about <see cref="KbdNlsTables"/> to console</summary>
    ''' <param name="kbdNlsTables">The <see cref="KbdNlsTables"/> object</param>
    Private Sub Print(kbdNlsTables As KbdNlsTables)
        If kbdNlsTables Is Nothing Then Cv("null") : Return
        Cv("OEM ID {0}", kbdNlsTables.OemIdentifier)
        Cv("Layout info {0}", kbdNlsTables.LayoutInformation)
        Cv("VK_F")
        Using New RaisedLevel
            Print(kbdNlsTables.VkFunctions)
        End Using
        Cv("Mouse")
        Using New RaisedLevel
            Print(kbdNlsTables.MouseVKeys)
        End Using
    End Sub

    ''' <summary>Prints <see cref="UShort()"/> to console</summary>
    ''' <param name="array">The array to print</param>
    Private Sub Print(array As UShort())
        If array Is Nothing Then Cv("null") : Return
        If array.Length = 0 Then Cv("empty") : Return
        Cv(String.Join(" ", array))
    End Sub

    ''' <summary>Prints information about <see cref="VkFunction"/>s to console</summary>
    ''' <param name="functions">The array of  <see cref="VkFunction"/>s</param>
    Private Sub Print(functions As VkFunction())
        If functions Is Nothing Then Cv("null") : Return
        If functions.Length = 0 Then Cv("empty") : Return
        For Each func In functions
            Print(func)
        Next
    End Sub

    ''' <summary>Prints information about <see cref="Ligature"/>s to console</summary>
    ''' <param name="ligatures">The array of  <see cref="Ligature"/>s</param>
    Private Sub Print(ligatures As Ligature())
        If ligatures Is Nothing Then Cv("null") : Return
        If ligatures.Length = 0 Then Cv("empty") : Return
        For Each ligature In ligatures
            Print(ligature)
        Next
    End Sub

    ''' <summary>Prints information about Virtual Scan Code to Virtual Key mappings to console</summary>
    ''' <param name="vsc2Vks">The array of Virtual Scan Code to Virtual Key mappings</param>
    Private Sub Print(vsc2Vks As Vsc2Vk())
        If vsc2Vks Is Nothing Then Cv("null") : Return
        If vsc2Vks.Length = 0 Then Cv("empty") : Return
        For Each vsc2Vk In vsc2Vks
            Print(vsc2Vk)
        Next
    End Sub

    ''' <summary>Prints information about dead keys to console</summary>
    ''' <param name="deadKeys">The dead keys</param>
    Private Sub Print(deadKeys As DeadKey())
        If deadKeys Is Nothing Then Cv("null") : Return
        If deadKeys.Length = 0 Then Cv("empty") : Return
        For Each deadKey In deadKeys
            Print(deadKey)
        Next
    End Sub

    ''' <summary>Prints information about key names to console</summary>
    ''' <param name="keyNames">The array of key names</param>
    Private Sub Print(keyNames As KeyName())
        If keyNames Is Nothing Then Cv("null") : Return
        If keyNames.Length = 0 Then Cv("empty") : Return
        For Each keyName In keyNames
            Print(keyName)
        Next
    End Sub

    ''' <summary>Prints information about virtual keys to characters mappings to console</summary>
    ''' <param name="vks2WChars">The array of virtual key to characters mappings</param>
    Private Sub Print(vks2WChars As Vks2WChars())
        If vks2WChars Is Nothing Then Cv("null") : Return
        If vks2WChars.Length = 0 Then Cv("empty") : Return
        For Each vks2WChar In vks2WChars
            Print(vks2WChar)
        Next
    End Sub

    ''' <summary>Prints information about <see cref="Modifiers"/>s to console</summary>
    ''' <param name="modifiers">The array of  <see cref="Modifiers"/>s</param>
    Private Sub Print(modifiers As Modifiers)
        If modifiers Is Nothing Then Cv("null") : Return
        Cv("VK 2 bit")
        Using New RaisedLevel
            Print(modifiers.Vk2Bits)
        End Using
        Cv("Mod numbers")
        Using New RaisedLevel
            Print(modifiers.ModNumbers)
        End Using
    End Sub

    ''' <summary>Prints information about <see cref="VkFunction"/> to console</summary>
    ''' <param name="func">The <see cref="VkFunction"/></param>
    Private Sub Print(func As VkFunction)
        If func Is Nothing Then Cv("null") : Return
        Cv("VK {0}", func.VirtualKey)
        Cv("Proc type {0}", func.ProcType)
        Cv("Proc current {0}", func.ProcCurrent)
        Cv("Proc switch {0}", func.ProcSwitch)
        Cv("Procs")
        Using New RaisedLevel
            Print(func.Procs)
        End Using
        Cv("Procs Alt")
        Using New RaisedLevel
            Print(func.AltProcs)
        End Using
    End Sub

    ''' <summary>Prints information about <see cref="Ligature"/> to console</summary>
    ''' <param name="ligature">The <see cref="Ligature"/></param>
    Private Sub Print(ligature As Ligature)
        If ligature Is Nothing Then Cv("null") : Return
        Cv("VK {0}", ligature.VirtualKey)
        Cv("Mod number {0}", ligature.ModificationNumber)
        Cv("Characters {0}", ligature.Characters)
    End Sub


    ''' <summary>Prints information about virtual scan code to virtual key mapping to console</summary>
    ''' <param name="vsc2Vk">The <see cref="Vsc2Vk"/> object</param>
    Private Sub Print(vsc2Vk As Vsc2Vk)
        If vsc2Vk Is Nothing Then Cv("null") : Return
        Cv("VSC {0}", vsc2Vk.ScanCode)
        Cv("VK {0}", vsc2Vk.VirtualKey)
    End Sub

    ''' <summary>Prints information about <see cref="DeadKey"/> to console</summary>
    ''' <param name="deadKey">The <see cref="DeadKey"/></param>
    Private Sub Print(deadKey As DeadKey)
        If deadKey Is Nothing Then Cv("null") : Return
        Cv("Accent {0}", deadKey.Accent)
        Cv("Char {0}", deadKey.Char)
        Cv("Composed {0}", deadKey.Composed)
        Cv("Flags {0}", deadKey.Flags)
    End Sub

    ''' <summary>Prints information about <see cref="KeyName"/> to console</summary>
    ''' <param name="keyName">The <see cref="KeyName"/></param>
    Private Sub Print(keyName As KeyName)
        If keyName Is Nothing Then Cv("null") : Return
        Cv("Scan code {0}", keyName.ScanCode)
        Cv("Name {0}", keyName.Name)
    End Sub

    ''' <summary>Prints information about virtual key to character mapping to console</summary>
    ''' <param name="vks2WChar">The <see cref="Vks2WChars"/> object</param>
    Private Sub Print(vks2WChar As Vks2WChars)
        If vks2WChar Is Nothing Then Cv("null") : Return
        Cv("Mappings")
        Using New RaisedLevel
            Print(vks2WChar.Mappings)
        End Using
    End Sub

    ''' <summary>Prints information about <see cref="Vk2Bit"/>s to console</summary>
    ''' <param name="vk2Bits">The array of  <see cref="Vk2Bit"/>s</param>
    Private Sub Print(vk2Bits As Vk2Bit())
        If vk2Bits Is Nothing Then Cv("null") : Return
        If vk2Bits.Length = 0 Then Cv("empty") : Return
        For Each vk2Bit In vk2Bits
            Print(vk2Bit)
        Next
    End Sub

    ''' <summary>Prints array of bytes to console</summary>
    ''' <param name="array">An array to be printed</param>
    Private Sub Print(array As Byte())
        If array Is Nothing Then Cv("null") : Return
        If array.Length = 0 Then Cv("empty") : Return
        Cv(String.Join(" ", array))
    End Sub

    ''' <summary>Prints information about <see cref="NlsFParam"/>s to console</summary>
    ''' <param name="nlsFParams">The array of <see cref="NlsFParam"/>s</param>
    Private Sub Print(nlsFParams As NlsFParam())
        If nlsFParams Is Nothing Then Cv("null") : Return
        If nlsFParams.Length = 0 Then Cv("empty") : Return
        For Each nlsFParam In nlsFParams
            Print(nlsFParam)
        Next
    End Sub

    ''' <summary>Prints information about virtual key to character mappings to console</summary>
    ''' <param name="vk2WChars">The array of <see cref="Vk2WChar"/> objects</param>
    Private Sub Print(vk2WChars As Vk2WChar())
        If vk2WChars Is Nothing Then Cv("null") : Return
        If vk2WChars.Length = 0 Then Cv("empty") : Return
        For Each vk2WChar In vk2WChars
            Print(vk2WChar)
        Next
    End Sub

    ''' <summary>Prints information about <see cref="Vk2Bit"/> to console</summary>
    ''' <param name="vk2Bit">The <see cref="Vk2Bit"/> object</param>
    Private Sub Print(vk2Bit As Vk2Bit)
        If vk2Bit Is Nothing Then Cv("null") : Return
        Cv("VK {0}", vk2Bit.VirtualKey)
        Cv("Modifiers {0}", vk2Bit.Modifiers)
    End Sub

    ''' <summary>Prints information about <see cref="NlsFParam"/> to console</summary>
    ''' <param name="nlsFParam">The <see cref="NlsFParam"/> object</param>
    Private Sub Print(nlsFParam As NlsFParam)
        If nlsFParam Is Nothing Then Cv("null") : Return
        Cv("Index {0}", nlsFParam.Index)
        Cv("Param {0}", nlsFParam.Param)
    End Sub

    ''' <summary>Prints information about virtual key to character mapping to console</summary>
    ''' <param name="vk2WChar">The <see cref="Vk2WChar"/> object</param>
    Private Sub Print(vk2WChar As Vk2WChar)
        If vk2WChar Is Nothing Then Cv("null") : Return
        Cv("VK {0}", vk2WChar.VirtualKey)
        Cv("Attributes {0}", vk2WChar.Attributes)
        Cv("chars")
        Using New RaisedLevel
            Print(vk2WChar.Chars)
        End Using
    End Sub

    ''' <summary>Prints array of characters to console</summary>
    ''' <param name="array">An array to be printed</param>
    Private Sub print(array As Char())
        If array Is Nothing Then Cv("null") : Return
        If array.Length = 0 Then Cv("empty") : Return
        Cv(String.Join(" ", array))
    End Sub
#End Region

    ''' <summary>Temporarily raises indentation level</summary>
    Private Class RaisedLevel : Implements IDisposable
        ''' <summary>Indicates that this instance has already been disposed</summary>
        Private disposed As Boolean
        ''' <summary>CTor - creates a new instance of the <see cref="RaisedLevel"/> class</summary>
        ''' <remarks>Calling this constructor immediately raises the indentation level</remarks>
        Public Sub New()
            levelIndicator &= vbTab
        End Sub

        ''' <summary>Decreases the indentation level</summary>
        Private Sub Dispose() Implements IDisposable.Dispose
            If Not disposed Then
                levelIndicator = levelIndicator.Substring(0, levelIndicator.Length - 1)
                disposed = True
            End If
        End Sub

    End Class
End Module
