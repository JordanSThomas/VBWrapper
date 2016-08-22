'//----------------------------------------------------------------
'// APIException.cs
'// Copyright SimplyCast 2014
'// This projected is licensed under the terms of the MIT license.
'//  (see the attached LICENSE.txt).
'//----------------------------------------------------------------
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net

Namespace SimplyCastWrapper

    Public Class APIException
        Inherits Exception
        '<summary>
        '        /// APIExceptions are thrown when a 400 or 500 series error is 
        '/// encountered in an API call.
        '/// </summary>
        Private code As HttpStatusCode
        Private status As String

        Public Sub New()

        End Sub

        Public Sub New(Message As String)
            MyBase.New(Message)


        End Sub

        Public Sub New(Message As String, inner As Exception)
            MyBase.New(Message, inner)
        End Sub
        Public Property StatusCode As HttpStatusCode
            Get
                Return Me.code
            End Get
            Set(value As HttpStatusCode)
                Me.code = value
            End Set
        End Property

        Public Property StatusDescription As String
            Get
                Return Me.status
            End Get
            Set(value As String)
                Me.status = value
            End Set
        End Property
    End Class

End Namespace

