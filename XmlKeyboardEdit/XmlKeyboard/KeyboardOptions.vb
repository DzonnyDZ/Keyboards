Imports System.Globalization.CultureInfo
Imports <xmlns:http://dzonny.cz/Keyboard.xsd>

''' <summary>Possible combinations of locale flags</summary>
<Flags>
Public Enum LocaleFlags
    ''' <summary>No locale flags are specified</summary>
    None = 0
End Enum

''' <summary>Keyboard layoutversion</summary>
Public Enum LayoutVersion As UInt32
    ''' <summary>Version 0</summary>
    V0 = 0
    ''' <summary>Version 1</summary>
    V1 = 1
End Enum

Public Enum KbdType
    ''' <summary>AT&amp;T '301' &amp; '302'; Olivetti 83-key; PC-XT 84-key; etc.</summary>
    AtAndT = 1
    ''' <summary>Olivetti M24 102-key</summary>
    Olivetti = 2
    ''' <summary>HP Vectra (DIN); Olivetti 86-key; etc.</summary>
    HPVectra = 3
    ''' <summary>Enhanced 101/102-key; Olivetti A; etc.</summary>
    Enhanced101102 = 4
    ''' <summary>Nokia (Ericsson) type 5 (1050, etc.)</summary>
    NokiaEricssonType5 = 5
    ''' <summary>Nokia (Ericsson) type 6 (9140)</summary>
    NokiaEricssonType6 = 6
    ''' <summary>Japanese IBM type 002 keyboard.</summary>
    JapaneseIbm = 7
    ''' <summary>Japanese OADG (106) keyboard.</summary>
    JapaneseOadg = 8
    ''' <summary>Korean 101 (type A) keyboard.</summary>
    Korean101TypeA = 10
    ''' <summary>Korean 101 (type B) keyboard.</summary>
    Korean101TypeB = 11
    ''' <summary>Korean 101 (type C) keyboard.</summary>
    Korean101TypeC = 12
    ''' <summary>Korean 103 keyboard.</summary>
    Korean103 = 13
    ''' <summary>Japanese AX keyboard.</summary>
    JapaneseAx = 16
    ''' <summary>Fujitsu FMR JIS keyboard.</summary>
    FujitsuFmrJis = 20
    ''' <summary>Fujitsu FMR OYAYUBI keyboard.</summary>
    FujitsuFmrOyayubi = 21
    ''' <summary>Fujitsu FMV OYAYUBI keyboard.</summary>
    FujitsuFmvOyayubi = 22
    ''' <summary>NEC PC-9800 Normal Keyboard.</summary>
    NecPc9800Normal = 30
    ''' <summary>NEC PC-9800 for Hydra: PC-9800 Keyboard on Windows NT 5.0. / NEC PC-98NX for Hydra: PC-9800 Keyboard on Windows 95/NT.</summary>
    NecPc9800HydraNt50 = 33
    ''' <summary>NEC PC-9800 for Hydra: PC-9800 Keyboard on Windows NT 3.51/4.0.</summary>
    NecPc9800HydraNt35140 = 34
    ''' <summary>NEC PC-9800 for Hydra: PC-9800 Keyboard on Windows 95.</summary>
    NecPc9800HydraWindows95 = 37
    ''' <summary>DEC LK411-JJ (JIS  layout) keyboard</summary>
    DecLk411JjJis = 40
    ''' <summary>DEC LK411-AJ (ANSI layout) keyboard</summary>
    DcLk411AjAnsi = 41
End Enum

''' <summary>Specifies global keyboard options</summary>
Public Class KeyboardOptions
    Inherits XmlKeyboardObject
    ''' <summary>CTor - creates a new empty instance of the <see cref="Keyboard"/> class</summary>
    Public Sub New()
    End Sub

    ''' <summary>CTor - creates a new instance of the <see cref="KeyboardOptions"/> class and initializes it forkm <see cref="XElement"/></summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Sub New(element As XElement)
        LoadXml(element)
    End Sub

#Region "Keyboard properties"
    ''' <summary>Gets or sets lavale-specific flags</summary>
    Public Property LocaleFlags As LocaleFlags = LocaleFlags.None
    ''' <summary>Gets or sets API version of layout</summary>
    Public Property ApiVersion As LayoutVersion = LayoutVersion.V0
    ''' <summary>Gets or sets typeof keyboard to base thislayout's ScanCode-to-VirtualKey mapping on</summary>
    Public Property Type As KbdType = KbdType.Enhanced101102
    ''' <summary>Gets or sets keyboard subtype</summary>
    Public Property SubType As Short?
    ''' <summary>Gets or sets keyboard OEM ID</summary>
    Public Property OemId As Short?
    ''' <summary>Gets or sets file version for the layout</summary>
    Public Property FileVersion As Version
    ''' <summary>Gets or sets product version for the layout</summary>
    Public Property ProductVersion As Version
    ''' <summary>Gets or sets name of DLL for this keybord layout. Without .dll extension</summary>
    Public Property DllName As String
    ''' <summary>Gets or sets internal DLL name</summary>
    ''' <remarks>Only specify when differes from <see cref="DllName"/></remarks>
    Public Property InternalName As String
    ''' <summary>Gets or sets culture identifier of the keyboard</summary>
    Public Property Culture As String
    ''' <summary>Gets or sets name of company that produces this keyboard layout</summary>
    Public Property Company As String
    ''' <summary>Gets or sets description of the keyboard layout</summary>
    Public Property Description As String
    ''' <summary>Gets or sets copyringht notice</summary>
    Public Property Copyright As String
    ''' <summary>Gets or sets name of product</summary>
    Public Property Product As String
    ''' <summary>Gets or sets additional release information</summary>
    Public Property ReleaseInfo As String
    ''' <summary>Gets or sets layout display name</summary>
    Public Property DisplayName As String
    ''' <summary>Gets or sets language display name</summary>
    Public Property LanguageDisplayName As String


#End Region

#Region "XmlKeyboardObject"
    ''' <summary>Gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    Public NotOverridable Overrides ReadOnly Property Name As XName = <options/>.Name

    ''' <summary>Gets all child objects of this object</summary>
    ''' <returns>Child objects of this object</returns>
    Protected Overrides ReadOnly Property ChildObjects As IEnumerable(Of IXmlKeyboardObject) = Nothing

    ''' <summary>Populates this opbject form XML element</summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Overrides Sub LoadXml(element As XElement)
        If element Is Nothing Then Throw New ArgumentNullException(NameOf(element))
        If element.Name <> Name Then Throw New ArgumentException($"XML element must be {Name}")
        LocaleFlags = If(element.@<locale-flags> Is Nothing, LocaleFlags.None, Int32.Parse(element.@<locale-flags>, InvariantCulture))
        ApiVersion = If(element.@<api-version> Is Nothing, LayoutVersion.V0, UInt32.Parse(element.@<api-version>, InvariantCulture))
        Type = If(element.@type Is Nothing, KbdType.Enhanced101102, Integer.Parse(element.@type, InvariantCulture))
        SubType = If(element.@subtype Is Nothing, Nothing, Short.Parse(element.@subtype, InvariantCulture))
        OemId = If(element.@oemid Is Nothing, Nothing, Short.Parse(element.@oemid, InvariantCulture))
        FileVersion = If(element.@<file-version> Is Nothing, Nothing, Version.Parse(element.@<file-version>))
        ProductVersion = If(element.@<product-version> Is Nothing, Nothing, Version.Parse(element.@<product-version>))
        DllName = element.@<dll-name>
        InternalName = element.@<internal-name>
        Culture = element.@culture

        Company = element.<company>.SingleOrDefault?.Value
        Description = element.<description>.SingleOrDefault?.Value
        Copyright = element.<copyright>.SingleOrDefault?.Value
        Product = element.<product>.SingleOrDefault?.Value
        ReleaseInfo = element.<release-info>.SingleOrDefault?.Value
        DisplayName = element.<display-name>.SingleOrDefault?.Value
        LanguageDisplayName = element.<language-display-name>.SingleOrDefault?.Value
    End Sub

    ''' <summary>Determines if the object is valid</summary>
    ''' <returns>True if the object is valid - meaning all mandatory properties are set, and all properties have valid values. Fatlse otherwise.</returns>
    ''' <remarks>Checks recursively.</remarks>
    Public Overrides Function Isvalid() As Boolean
        Return LocaleFlags = LocaleFlags.None AndAlso [Enum].IsDefined(GetType(LayoutVersion), ApiVersion) AndAlso [Enum].IsDefined(GetType(KbdType), Type) AndAlso DllName <> "" AndAlso Culture <> ""
    End Function


    ''' <summary>Stores the object in XML</summary>
    ''' <returns>XML element representing the object</returns>
    Public Overrides Function ToXml() As XElement
        Return <<%= Name %>
                   locale-flags=<%= LocaleFlags.ToString("d") %>
                   api-version=<%= ApiVersion.ToString("d") %>
                   type=<%= Type.ToString("d") %>
                   subtype=<%= SubType?.ToString(InvariantCulture) %>
                   oemid=<%= OemId?.ToString(InvariantCulture) %>
                   file-version=<%= FileVersion?.ToString() %>
                   product-version=<%= Product?.ToString() %>
                   dll-name=<%= DllName %>
                   internal-name=<%= InternalName %>
                   culture=<%= Culture %>
                   >
                   <%= If(Company IsNot Nothing, <company><%= Company %></>, Nothing) %>
                   <%= If(Description IsNot Nothing, <description><%= Description %></>, Nothing) %>
                   <%= If(Copyright IsNot Nothing, <copyright><%= Copyright %></>, Nothing) %>
                   <%= If(Product IsNot Nothing, <product><%= Product %></>, Nothing) %>
                   <%= If(ReleaseInfo IsNot Nothing, <release-info><%= ReleaseInfo %></>, Nothing) %>
                   <%= If(DisplayName IsNot Nothing, <display-name><%= DisplayName %></>, Nothing) %>
                   <%= If(LanguageDisplayName IsNot Nothing, <language-display-name><%= LanguageDisplayName %></>, Nothing) %>
               </>
    End Function
#End Region
End Class
