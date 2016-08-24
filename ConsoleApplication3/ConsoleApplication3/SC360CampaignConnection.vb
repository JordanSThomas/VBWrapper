Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SC360CampaignConnection

        Dim id As Integer 'The unique identifier of the 360 or autoresponder connection.
        Dim active As Boolean '	The flag indicating whether or not this connection is active.
        Dim name As String 'The name of the connection.
        Dim type As String '	The type of the connection.
        Dim created As DateTime '	The time when the 360 or autoresponder connection was created.
        Dim modified As DateTime 'The time when the 360 or autoresponder connection was last modified.

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

        'The date and time of the list's creation
        Public Property Pub_Created As DateTime
            Get
                Return Me.created

            End Get
            Set(value As DateTime)
                Me.created = value
            End Set
        End Property
        'The time of the last modification to the form
        Public Property Pub_Modified As DateTime
            Get
                Return Me.modified

            End Get
            Set(value As DateTime)
                Me.modified = value

            End Set
        End Property
    End Class
End Namespace
