﻿Imports System.Runtime.InteropServices
Imports System.Text
Imports <xmlns:http://dzonny.cz/Keyboard.xsd>

''' <summary>Represents possible name of Scan Code Variable</summary>
Public Structure ScanCodeVariable
    Public Sub New(t As ScanCodeVariableT, x As ScanCodeVariableX, y As ScanCodeVariableY)
        Me.T = t
        Me.X = x
        Me.Y = y
    End Sub

    Public Property T As ScanCodeVariableT
    Public Property X As ScanCodeVariableX
    Public Property Y As ScanCodeVariableY

    Public Overrides Function ToString() As String
        Return $"{T} {X} {Y}"
    End Function
    Public Shared Function TryParse(string$, <Out> ByRef variable As ScanCodeVariable) As Boolean
        If [string] Is Nothing Then Return False
        Dim T As ScanCodeVariableT
        Dim X As ScanCodeVariableX
        Dim Y As ScanCodeVariableY
        Dim en = [string].GetEnumerator
        If Not en.MoveNext Then Return False

        While Char.IsWhiteSpace(en.Current) AndAlso en.MoveNext : End While
        Dim b As New StringBuilder
        While Not Char.IsWhiteSpace(en.Current) AndAlso en.MoveNext
            b.Append(en.Current)
        End While
        If Not [Enum].TryParse(b.ToString, T) Then Return False

        While Char.IsWhiteSpace(en.Current) AndAlso en.MoveNext : End While
        b.Clear()
        While Not Char.IsWhiteSpace(en.Current) AndAlso en.MoveNext
            b.Append(en.Current)
        End While
        If Not [Enum].TryParse(b.ToString, X) Then Return False

        While Char.IsWhiteSpace(en.Current) AndAlso en.MoveNext : End While
        b.Clear()
        While Not Char.IsWhiteSpace(en.Current) AndAlso en.MoveNext
            b.Append(en.Current)
        End While
        If Not [Enum].TryParse(b.ToString, Y) Then Return False

        variable = New ScanCodeVariable(T, X, Y)
        Return True
    End Function
End Structure

Public Enum VirtualKeyCode

End Enum

<Flags>
Public Enum ScanFlags

End Enum


''' <summary>Defines ScanCode-to-VirtualKey mapping</summary>
Public Class ScanToVK
    Inherits XmlKeyboardObject
    ''' <summary>CTor - creates a new empty instance of the <see cref="Keyboard"/> class</summary>
    Public Sub New()
    End Sub

    ''' <summary>CTor - creates a new instance of the <see cref="ScanToVK"/> class and initializes it forkm <see cref="XElement"/></summary>
    ''' <param name="element">XML element to populate the object from</param>
    ''' <exception cref="ArgumentNullException "><paramref name="element"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="element"/> is not <see cref="Name"/> -or- Cannot load any of child items because of this issue</exception>
    Public Sub New(element As XElement)
        LoadXml(element)
    End Sub

#Region "Keyboard properties"
    ''' <summary>Gets or sets name of variable indicating ScanCode being mapped</summary>
    Public Property Scan As ScanCodeVariable
    ''' <summary>Gets or sets virtual code the scan code is mapped to</summary>
    Public Property VirtualKay As VirtualKeyCode
    ''' <summary>Gets or sets additional flags to OR <see cref="Scan"/> code with</summary>
    Public Property Flags As ScanFlags
    ''' <summary>Gets or sets name of the key</summary>
    Public Property Name As String

#End Region

#Region "XmlKeyboardObject"
    ''' <summary>Gets name of XML element thatrepresents this object</summary>
    ''' <returns>Name of XML element thatrepresents this object</returns>
    Public NotOverridable Overrides ReadOnly Property Name As XName = <map/>.Name

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
