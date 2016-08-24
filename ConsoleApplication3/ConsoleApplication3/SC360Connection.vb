Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SC360Connection

        Dim id As Integer 'The unique identifier of the connection.
        Dim type As String '	The connection type ("in" for inbound, "out" for outbound).
        Dim name As String '	The name of the connection in the UI.
        Dim active As Boolean 'A flag indicating whether the connection is active or not.
        Dim contacts As List(Of SC360Contact) 'A collection of contacts that have arrived to the outbound API connection. Only provided with outbound connection types.

        Public Property pub_id As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property

        Public Property pub_active As Boolean
            Get
                Return Me.active
            End Get
            Set(value As Boolean)

            End Set
        End Property

        Public Property pub_name As String
            Get
                Return Me.name
            End Get
            Set(value As String)
                name = value
            End Set
        End Property

        Public Property pub_type As String
            Get
                Return Me.type
            End Get
            Set(value As String)
                type = value
            End Set
        End Property

        Public Property pub_contacts As List(Of SC360Contact)
            Get
                Return contacts
            End Get
            Set(value As List(Of SC360Contact))
                contacts = value
            End Set
        End Property
    End Class
End Namespace
