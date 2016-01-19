Imports <xmlns:http://dzonny.cz/Keyboard.xsd>

''' <summary>Defines entire keyboad</summary>
Public Class Keyboard
    Inherits XmlKeyboardObject
    ''' <summary>CTor - creates a new empty instance of the <see cref="Keyboard"/> class</summary>
    Public Sub New()
    End Sub

    ''' <summary>CTor - creates a new instance of the <see cref="Keyboard"/> class and initializes it forkm <see cref="XElement"/></summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Sub New(element As XElement)
        LoadXml(element)
    End Sub

#Region "Keyboard properties"
    ''' <summary>Gets or sets globalkeyboard options</summary>
    Public Property Options As KeyboardOptions
    ''' <summary>Gets or sets Scan Code to Virtual Key mapping. Overrides predefined mapping for selected keyboard type.</summary>
    Public Property Scan2VK As IXmlKeyboardObjectList(Of ScanToVK)
    ''' <summary>Gets or sets mappings of keys such as Ctrl, Shift and Alt to Virtual Keys</summary>
    Public Property Modifiers As IXmlKeyboardObjectList(Of VK2Modifier)
    ''' <summary>Gets or sets value indicating which modifier combinations are valid</summary>
    Public Property ModifierCombinations As IXmlKeyboardObjectList(Of ModifierCombination)
    ''' <summary>Gets or sets mappings between Virual Keys and characters</summary>
    Public Property Chars As IXmlKeyboardObjectList(Of VK2Char)
    ''' <summary>Gets or sets dead keys (diacritics)</summary>
    Public Property DeadKeys As IXmlKeyboardObjectList(Of DeadKey)
    ''' <summary>Gets or sets specification of special Far-East options</summary>
    Public Property NlsLayer As NlsLayer
#End Region

#Region "XmlKeyboardObject"
    ''' <summary>Gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    Public NotOverridable Overrides ReadOnly Property Name As XName = <keyboard/>.Name

    ''' <summary>Gets all child objects of this object</summary>
    ''' <returns>Child objects of this object</returns>
    Protected Overrides ReadOnly Iterator Property ChildObjects As IEnumerable(Of IXmlKeyboardObject)
        Get
            If Options IsNot Nothing Then Yield Options
            If Scan2VK IsNot Nothing Then Yield Scan2VK
            If Modifiers IsNot Nothing Then Yield Modifiers
            If ModifierCombinations IsNot Nothing Then Yield ModifierCombinations
            If Chars IsNot Nothing Then Yield Chars
            If DeadKeys IsNot Nothing Then Yield DeadKeys
            If NlsLayer IsNot Nothing Then Yield NlsLayer
        End Get
    End Property

    ''' <summary>Populates this opbject form XML element</summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Overrides Sub LoadXml(element As XElement)
        If element Is Nothing Then Throw New ArgumentNullException(NameOf(element))
        If element.Name <> Name Then Throw New ArgumentException($"XML element must be {Name}")
        Options = New KeyboardOptions(element.<options>)
        Scan2VK = New XmlKeyboardObjectList(Of ScanToVK)(element.<scan2vk>.Single)
        Modifiers = New XmlKeyboardObjectList(Of VK2Modifier)(element.<modifiers>.Single)
        ModifierCombinations = New XmlKeyboardObjectList(Of ModifierCombination)(element.<modifier-combonations>.Single)
        Chars = New XmlKeyboardObjectList(Of VK2Char)(element.<chars>.Single)
        DeadKeys = (From el In element.<deadkeys> Select New XmlKeyboardObjectList(Of DeadKey)(el)).SingleOrDefault
        NlsLayer = (From el In element.<deadkeys> Select New NlsLayer(el)).SingleOrDefault
    End Sub

    ''' <summary>Determines if the object is valid</summary>
    ''' <returns>True if the object is valid - meaning all mandatory properties are set, and all properties have valid values. Fatlse otherwise.</returns>
    ''' <remarks>Checks recursively.</remarks>
    Public Overrides Function Isvalid() As Boolean
        Return Options IsNot Nothing AndAlso Scan2VK IsNot Nothing AndAlso Modifiers IsNot Nothing AndAlso ModifierCombinations IsNot Nothing AndAlso Chars IsNot Nothing AndAlso MyBase.Isvalid()
    End Function
#End Region

End Class