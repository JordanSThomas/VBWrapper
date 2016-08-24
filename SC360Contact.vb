Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SC360Contact

        Dim id As Integer 'A unique handle representing an instance of a contact passing through the 360 workflow. This is used to reference the contact instance in subsequent API calls (for example, deleting the contact entry from the outbound connection)..
        Dim type As String '	The type of entry that this contact record is (either "row" or "list").
        Dim processed As Boolean 'A flag indicating whether the record has been processed yet or not (processed in this case means the record has been loaded via API at least once).
        Dim created As Boolean '	A unix timestamp indicating when the record arrived at the outbound connection.
        Dim row As Integer  'If the record is a contact, this is the Contact Manager contact ID.
        Dim list As Integer '	The list associated with the record.
        Dim communication As Boolean  '	A unique system identifier of the communication.

        Public Property pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value
            End Set
        End Property

        Public Property pub_type As Integer
            Get
                Return type
            End Get
            Set(value As Integer)
                type = value
            End Set
        End Property

        Public Property pub_processed As Boolean
            Get
                Return processed
            End Get
            Set(value As Boolean)
                processed = value
            End Set
        End Property

        Public Property pub_created As Boolean
            Get
                Return created
            End Get
            Set(value As Boolean)
                created = value
            End Set
        End Property
        Public Property pub_row As Integer
            Get
                Return row
            End Get
            Set(value As Integer)
                row = value
            End Set
        End Property
        Public Property pub_list As Integer
            Get
                Return list
            End Get
            Set(value As Integer)
                list = value
            End Set
        End Property
        Public Property pub_communication As Boolean
            Get
                Return communication
            End Get
            Set(value As Boolean)
                communication = value
            End Set
        End Property
    End Class
End Namespace
