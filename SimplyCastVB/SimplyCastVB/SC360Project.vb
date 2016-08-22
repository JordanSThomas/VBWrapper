Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SC360Project

        Dim id As Integer '	The unique identifier of the project.
        Dim name As String 'The name of the project.
        Dim active As Boolean '	A flag indicating whether the project is active or not.
        Dim connections As List(Of SC360Connection) 'The API connection endpoints that exist for the project. These endpoints are configured from the 360 user interface.

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



        Public Property pub_connections As List(Of SC360Connection)
            Get
                Return connections
            End Get
            Set(value As List(Of SC360Connection))
                connections = value
            End Set
        End Property
    End Class
End Namespace
