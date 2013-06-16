Imports Tools.InteropT

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
        Console.WriteLine(vbTab & "layoutname - path of XML file to export layout to")
    End Sub

    Private Sub List()
        For Each layout In KeyboardLayoutInfo.GetInstalledLayouts
            Console.WriteLine("{0} {1} {2}", layout.LayoutFile, layout.LayoutDisplayName, layout.LayoutText)
        Next
    End Sub

    Private Sub MakeXml(dll$, xml$)
        Dim info = KeyboardDescriptor.LoadKeyboard(dll)
    End Sub

End Module
