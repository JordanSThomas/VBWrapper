
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace SimplyCastWrapper
    <XmlRoot("error")>
    Public Class Error_Message
        Private ex_error As String
        '/// <summary>
        ' /// The text summary of the error encountered.
        ' /// </summary>
        '''<summary>
        '''Helper class for deserializing XML errors.
        '''</summary>

        <XmlText()>
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
