Imports Dzonny.XmlKeyboard

''' <summary>Common nterface of all XML keyboard objects</summary>
Public Interface IXmlKeyboardObject
    ''' <summary>Gets all child objects of this object</summary>
    ''' <returns>Child objects of this object</returns>
    ReadOnly Property ChildObjects As IEnumerable(Of IXmlKeyboardObject)
    ''' <summary>Determines if the object is valid</summary>
    ''' <returns>True if the object is valid - meaning all mandatory properties are set, and all properties have valid values. Fatlse otherwise.</returns>
    ''' <remarks>For parent objects checks recursively</remarks>
    Function Isvalid() As Boolean
    ''' <summary>Populates this opbject form XML element</summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Sub LoadXml(element As XElement)
    ''' <summary>Gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    ReadOnly Property Name As XName
    ''' <summary>Stores the object in XML</summary>
    ''' <returns>XML element representing the object</returns>
    Function ToXml() As XElement
End Interface

''' <summary>Interface of list of <see cref="IXmlKeyboardObject"/>s which also acts as <see cref="IXmlKeyboardObject"/></summary>
''' <typeparam name="T">Actual type of <see cref="IXmlKeyboardObject"/>s in the list</typeparam>
Public Interface IXmlKeyboardObjectList(Of T As {IXmlKeyboardObject, New})
    Inherits IList(Of T)
    Inherits IXmlKeyboardObject
End Interface

''' <summary>Default implementation of <see cref="IXmlKeyboardObject"/> and common base class for XML keyboardefinition objects</summary>
Public MustInherit Class XmlKeyboardObject
    Implements IXmlKeyboardObject

    ''' <summary>When overriden in derived class populates this opbject form XML element</summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public MustOverride Sub LoadXml(element As XElement) Implements IXmlKeyboardObject.LoadXml

    ''' <summary>Stores the object in XML</summary>
    ''' <returns>XML element representing the object</returns>
    Public Overridable Function ToXml() As XElement Implements IXmlKeyboardObject.ToXml
        Return <<%= Name %>><%= From ch In ChildObjects Where ch IsNot Nothing Select x = ch.ToXml %></>
    End Function

    ''' <summary>When overriden in derived class gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    Public MustOverride ReadOnly Property Name As XName Implements IXmlKeyboardObject.Name

    ''' <summary>When overriden in derived class gets all child objects of this object</summary>
    ''' <returns>Child objects of this object</returns>
    Protected MustOverride ReadOnly Property ChildObjects As IEnumerable(Of IXmlKeyboardObject) Implements IXmlKeyboardObject.ChildObjects

    ''' <summary>When overriden in derived class determines if the object is valid</summary>
    ''' <returns>True if the object is valid - meaning all mandatory properties are set, and all properties have valid values. Fatlse otherwise.</returns>
    ''' <remarks>For parent objects checks recursively. This implementation only iterates over childobjects</remarks>
    Public Overridable Function Isvalid() As Boolean Implements IXmlKeyboardObject.Isvalid
        If ChildObjects IsNot Nothing Then
            Return ChildObjects.Count = 0 OrElse ChildObjects.All(Function(ch) ch.Isvalid())
        End If
        Return True
    End Function
End Class

''' <summary>Default implemnentation of <see cref="IXmlKeyboardObjectList(Of T)"/></summary>
''' <typeparam name="T">Type of <see cref="IXmlKeyboardObject"/>s in the list</typeparam>
Public Class XmlKeyboardObjectList(Of T As {IXmlKeyboardObject, New})
    Inherits List(Of T)
    Implements IXmlKeyboardObjectList(Of T)

    ''' <summary>CTor - creates a new empty insatnce of the <see cref="XmlKeyboardObjectList(Of T)"/> class specifying element name</summary>
    ''' <param name="name">Name of XML element representing this instance</param>
    ''' <exception cref="ArgumentNullException "><paramref name="name"/> is null</exception>
    Public Sub New(name As XName)
        If name Is Nothing Then Throw New ArgumentNullException(NameOf(name))
        Me.Name = name
    End Sub

    ''' <summary>CTor - creates a new instance of the <see cref="XmlKeyboardObjectList(Of T)"/> class initializing it from <see cref="XElement"/></summary>
    ''' <param name="element"><see cref="XElement"/> to initialize new instance from</param>
    ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
    Public Sub New(element As XElement)
        If element Is Nothing Then Throw New ArgumentNullException(NameOf(element))
        Name = element.Name
        LoadXml(element)
    End Sub

#Region "IXmlKeyboardObject"
    ''' <summary>Gets all child objects of this object</summary>
    ''' <returns>Child objects of this object</returns>
    Protected Overridable ReadOnly Property ChildObjects As IEnumerable(Of IXmlKeyboardObject) Implements IXmlKeyboardObject.ChildObjects
        Get
            Return Me
        End Get
    End Property

    ''' <summary>Gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    Public ReadOnly Property Name As XName Implements IXmlKeyboardObject.Name

    ''' <summary>Determines if the object is valid</summary>
    ''' <returns>True if the object is valid - meaning all mandatory properties are set, and all properties have valid values. Fatlse otherwise.</returns>
    ''' <remarks>For parent objects checks recursively</remarks>
    Public Overridable Function Isvalid() As Boolean Implements IXmlKeyboardObject.Isvalid
        Return Count = 0 OrElse Me.All(Function(x) x.Isvalid())
    End Function

    ''' <summary>When overriden in derived class populates this opbject form XML element</summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Overridable Sub LoadXml(element As XElement) Implements IXmlKeyboardObject.LoadXml
        If element Is Nothing Then Throw New ArgumentNullException(NameOf(element))
        If element.Name <> Name Then Throw New ArgumentException($"XML element must be {Name}")
        For Each el In element.Elements
            Dim itm = New T
            itm.LoadXml(el)
            Add(itm)
        Next
    End Sub

    ''' <summary>Stores the object in XML</summary>
    ''' <returns>XML element representing the object</returns>
    Public Overridable Function ToXml() As XElement Implements IXmlKeyboardObject.ToXml
        Return <<%= Name %>><%= From ch In ChildObjects Where ch IsNot Nothing Select x = ch.ToXml %></>
    End Function
#End Region

End Class
