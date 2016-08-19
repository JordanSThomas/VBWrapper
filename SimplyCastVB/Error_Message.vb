
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace SimplyCastWrapper
    Public Class Error_Message
        Private ex_error As String
        '/// <summary>
        ' /// The text summary of the error encountered.
        ' /// </summary>
        '[XmlTextAttribute()]
        Public Property Message As String
            Get
                Return ex_error
            End Get
            Set(value As String)
                ex_error = value
            End Set
        End Property
    End Class

End Namespace
