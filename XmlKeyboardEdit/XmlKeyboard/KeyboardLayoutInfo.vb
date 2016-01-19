Imports Microsoft.Win32
Imports Tools.InteropT

''' <summary>Provides information about installed keyboard layout from registry</summary>
''' <remarks>This class is not part of XML keyboard definition</remarks>
Public Class KeyboardLayoutInfo
    ''' <summary>Gets all installed keyboard layouts from registry</summary>
    ''' <returns>All installed keyboard layouts from registry</returns>
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

    Private _layoutDisplayName As String
    Private _customLanguageDisplayName As String

    ''' <summary>CTor - creates a new instance of the <see cref="KeyboardLayoutInfo"/> class by reading given registry key</summary>
    ''' <param name="key">A registry key to read information about the layout from</param>
    Private Sub New(key As RegistryKey)
        LayoutFile = key.GetValue("Layout File")
        LayoutText = key.GetValue("Layout Text")
        _layoutDisplayName = key.GetValue("Layout Display Name")
        LayoutId = key.GetValue("Layout Id")
        _customLanguageDisplayName = key.GetValue("Custom Language Display Name")
        CustomLanguageName = key.GetValue("Custom Language Name")
        LayoutProductCode = key.GetValue("Layout Product Code")
    End Sub

    ''' <summary>Gets name (without path) of DLL file containing the layout</summary>
    Public ReadOnly Property LayoutFile$

    ''' <summary>Gets short name of the layout</summary>
    Public ReadOnly Property LayoutText$

    ''' <summary>Gets (localized) layout display name</summary>
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

    ''' <summary>Gets ID of the layout (if specified)</summary>
    Public ReadOnly Property LayoutId$

    ''' <summary>In case of custom layout gets custom layout language display name</summary>
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

    ''' <summary>In case of custom layout gets custom layout language name</summary>
    Public ReadOnly Property CustomLanguageName$

    ''' <summary>In case of custom layout gets custom layout product code</summary>
    Public ReadOnly Property LayoutProductCode$

    ''' <summary>Expands resource value</summary>
    ''' <param name="value">String, or expandable string to be loaded form resource</param>
    ''' <returns>In case <paramref name="value"/> represents expandable resource identifier, returns value of that resource, otherwise returns <paramref name="value"/>.</returns>
    Private Shared Function ExpandResource(value As String) As String
        If value.Length < 2 OrElse Not value.Contains(","c) Then Return value
        Dim fileName$
        Dim resourceId$
        If value(1) = """"c Then
            fileName = value.Substring(2, value.IndexOf(""""c, 2) - 2)
            If value(fileName.Length + 5) <> ","c Then Return value
            resourceId = value.Substring(fileName.Length + 6)
        Else
            fileName = value.Substring(1, value.IndexOf(","c) - 1)
            resourceId = value.Substring(value.IndexOf(","c) + 1)
        End If
        fileName = Environment.ExpandEnvironmentVariables(fileName)
        Dim id As Integer
        If Not Integer.TryParse(resourceId, Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, id) Then Return value
        Using dll = UnmanagedModule.LoadLibraryAsDataFile(fileName)
            Dim ret = dll.LoadString(Math.Abs(id))
            Return ret
        End Using
    End Function   
End Class        