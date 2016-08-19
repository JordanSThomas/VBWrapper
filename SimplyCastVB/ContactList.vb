Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class ContactList 'known as listentity in C#. If Chris Allard requires I conform my type names, I can and will change them to match the C# ones.

        Dim id As Integer 'The internal ID of the contact list. This ID is unique to the list, and will not be reassigned if the list is deleted.
        Dim size As Integer 'The total number of contacts in the list.
        Dim created As DateTime 'The date and time that the list was created.
        Dim lastAdded As DateTime '	The last time that a contact was added to the list.
        Dim lastDeleted As DateTime '	The last time that a contact was deleted from a list.
        Public name As String 'The user-defined name of the list. Is not unique more than one list can share the same name.
        Dim links As List(Of Link)  'A relational link to the contact list resource.

        'VB equivalent of XMLAttribute goes here
        'The identifying number of the contact list
        Public Property Pub_ID As Integer 'Pub_nn is a prefix to define gettable/settable public properties for private members, since VB is case-insensitive and would not recognize capitals as a difference in the type name.
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property

        'The amount of contacts in the list
        Public Property Pub_Size As Integer
            Get
                Return Me.size
            End Get
            Set(value As Integer)
                Me.size = value
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
        'The time of the last addition to the list
        Public Property Pub_LastAdded As DateTime
            Get
                Return Me.lastAdded

            End Get
            Set(value As DateTime)
                Me.lastAdded = value

            End Set
        End Property
        'The time of the last deletion from the list
        Public Property Pub_LastDeleted As DateTime
            Get
                Return Me.lastDeleted
            End Get
            Set(value As DateTime)
                Me.lastDeleted = value

            End Set
        End Property
        'The list name
        Public Property Pub_Name As String
            Get
                Return Me.name
            End Get
            Set(value As String)
                Me.name = value
            End Set
        End Property
        'A collection of relation links. Will have a link to the contact list.
        Public Property Pub_Links As List(Of Link)
            Get
                Return Me.links
            End Get
            Set(value As List(Of Link))
                Me.links = value
            End Set
        End Property
    End Class
End Namespace
