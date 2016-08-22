Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class Contact

        Dim id As Integer '	The unique identifier for the contact. This identifier is system-wide. If a contact appears in two different lists, it will have the same ID in both lists.
        Dim created As DateTime 'The date and time that the contact was created.
        Dim modified As DateTime 'The last time that the contact was modified.
        Public fields As List(Of Field) 'A collection of the contact fields, such as name and email address.
        Dim lists As List(Of ContactList)   'A collection of the lists that the contact appears on. Adding a contact to a another list is possible, but not by modifying this property. Contacts are added by POSTing to the list resource.
        Dim links As List(Of ContactList)  'A relational link to the contact resource.

        Public Property pub_ID As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property

        Public Property pub_created As DateTime
            Get
                Return Me.created
            End Get
            Set(value As DateTime)
                Me.created = value
            End Set
        End Property

        Public Property pub_modified As DateTime
            Get
                Return Me.modified
            End Get
            Set(value As DateTime)
                Me.modified = value
            End Set
        End Property

        Public Property pub_Fields As List(Of Field)
            Get
                Return Me.fields
            End Get
            Set(value As List(Of Field))
                Me.fields = value
            End Set
        End Property

        Public Property pub_lists As List(Of ContactList)
            Get
                Return Me.lists
            End Get
            Set(value As List(Of ContactList))
                Me.lists = value
            End Set
        End Property

        Public Property pub_links As List(Of ContactList)
            Get

                Return Me.links
            End Get
            Set(value As List(Of ContactList))
                Me.links = value
            End Set
        End Property
    End Class
End Namespace
