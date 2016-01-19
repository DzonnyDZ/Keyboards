Imports <xmlns:http://dzonny.cz/Keyboard.xsd>

''' <summary></summary>
Public Class VK2Char
    Inherits XmlKeyboardObject
    ''' <summary>CTor - creates a new empty instance of the <see cref="Keyboard"/> class</summary>
    Public Sub New()
    End Sub

    ''' <summary>CTor - creates a new instance of the <see cref=""/> class and initializes it forkm <see cref="XElement"/></summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Sub New(element As XElement)
        LoadXml(element)
    End Sub

#Region "Keyboard properties"

#End Region

#Region "XmlKeyboardObject"
    ''' <summary>Gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    Public NotOverridable Overrides ReadOnly Property Name As XName = </>.Name

    ''' <summary>Gets all child objects of this object</summary>
    ''' <returns>Child objects of this object</returns>
    Protected Overrides ReadOnly Iterator Property ChildObjects As IEnumerable(Of IXmlKeyboardObject)
        Get
            'If xxx IsNot Nothing Then Yield xxx
        End Get
    End Property

    ''' <summary>Populates this opbject form XML element</summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Overrides Sub LoadXml(element As XElement)
        If element Is Nothing Then Throw New ArgumentNullException(NameOf(element))
        If element.Name <> Name Then Throw New ArgumentException($"XML element must be {Name}")
        'xxx = New KeyboardOptions(element.<>)
    End Sub

    ''' <summary>Determines if the object is valid</summary>
    ''' <returns>True if the object is valid - meaning all mandatory properties are set, and all properties have valid values. Fatlse otherwise.</returns>
    ''' <remarks>Checks recursively.</remarks>
    Public Overrides Function Isvalid() As Boolean
        Return xxx IsNot Nothing AndAlso MyBase.Isvalid()
    End Function
#End Region
End Class
