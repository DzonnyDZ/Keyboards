Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports Microsoft.Win32
Imports Tools.InteropT

Public Class KeyboardLayoutInfo
    Public Shared Iterator Function GetInstalledLayouts() As IEnumerable(Of KeyboardLayoutInfo)
        Using hklm = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default)
            Using keyboardLayouts = hklm.OpenSubKey("SYSTEM\CurrentControlSet\Control\Keyboard Layouts")
                For Each subkeyname In keyboardLayouts.GetSubKeyNames
                    Using subkey = keyboardLayouts.OpenSubKey(subkeyname)
                        Try
                            Yield New KeyboardLayoutInfo(subkey)
                        Catch : End Try
                    End Using
                Next
            End Using
        End Using
    End Function

    Private ReadOnly _layoutFile As String
    Private ReadOnly _layoutText As String
    Private _layoutDisplayName As String
    Private ReadOnly _layoutId As String
    Private _customLanguageDisplayName As String
    Private ReadOnly _customLanguageName As String
    Private ReadOnly _layoutProductCode As String

    Private Sub New(key As RegistryKey)
        _layoutFile = key.GetValue("Layout File")
        _layoutText = key.GetValue("Layout Text")
        _layoutDisplayName = key.GetValue("Layout Display Name")
        _layoutId = key.GetValue("Layout Id")
        _customLanguageDisplayName = key.GetValue("Custom Language Display Name")
        _customLanguageName = key.GetValue("Custom Language Name")
        _layoutProductCode = key.GetValue("Layout Product Code")
    End Sub

    Public ReadOnly Property LayoutFile$
        Get
            Return _layoutFile
        End Get
    End Property

    Public ReadOnly Property LayoutText$
        Get
            Return _layoutText
        End Get
    End Property

    Public ReadOnly Property LayoutDisplayName$
        Get
            If _layoutDisplayName.StartsWith("@") Then
                Try
                    _layoutDisplayName = ExpandResource(_layoutDisplayName)
                Catch : End Try
            End If
            Return _layoutDisplayName
        End Get
    End Property

    Public ReadOnly Property LayoutId$
        Get
            Return _layoutId
        End Get
    End Property

    Public ReadOnly Property CustomLanguageDisplayName$
        Get
            If _customLanguageDisplayName.StartsWith("@") Then
                Try
                    _customLanguageDisplayName = ExpandResource(_customLanguageDisplayName)
                Catch : End Try
            End If
            Return _customLanguageDisplayName
        End Get
    End Property

    Public ReadOnly Property CustomLanguageName$
        Get
            Return _customLanguageName
        End Get
    End Property

    Public ReadOnly Property LayoutProductCode$
        Get
            Return _layoutProductCode
        End Get
    End Property

    Private Shared Function ExpandResource(layoutDisplayName As String) As String
        If layoutDisplayName.Length < 2 OrElse Not layoutDisplayName.Contains(","c) Then Return layoutDisplayName
        Dim fileName$
        Dim resourceId$
        If layoutDisplayName(1) = """"c Then
            fileName = layoutDisplayName.Substring(2, layoutDisplayName.IndexOf(""""c, 2) - 2)
            If layoutDisplayName(fileName.Length + 5) <> ","c Then Return layoutDisplayName
            resourceId = layoutDisplayName.Substring(fileName.Length + 6)
        Else
            fileName = layoutDisplayName.Substring(1, layoutDisplayName.IndexOf(","c) - 1)
            resourceId = layoutDisplayName.Substring(layoutDisplayName.IndexOf(","c) + 1)
        End If
        fileName = Environment.ExpandEnvironmentVariables(fileName)
        Dim id As Integer
        If Not Integer.TryParse(resourceId, Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, id) Then Return layoutDisplayName
        Using dll = UnmanagedModule.LoadLibraryAsDataFile(fileName)
            Dim ret = dll.LoadString(Math.Abs(id))
            Return ret
        End Using
    End Function

End Class
