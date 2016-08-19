Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SMSCampaignResponse

        Dim id As Integer '	The unique identifier of the SMS campaign response.
        Dim message As String 'The message that was received.
        Dim numberfrom As String 'The phone number the SMS was sent from.
        Dim numberto As String '	The phone number the SMS was sent to. This may not be included if it is not available.
        Dim time As DateTime '	The time the response was received
        Dim contact As Object '	An object containing an id (contact ID). This is not present if there is no contact.
        Dim list As Object 'An object containing an id (list ID). This is not present if there is no list.

        'The pub_ prefix is used because Visual Basic is not case sensitive, and as such the same word capitalized wouldn't work as a public property of a private member.
        Public Property pub_id As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property





        Public Property pub_numberto As String
            Get
                Return Me.numberto
            End Get
            Set(value As String)
                numberto = value
            End Set
        End Property

        Public Property pub_numberfrom As String
            Get
                Return Me.numberfrom
            End Get
            Set(value As String)
                numberfrom = value
            End Set
        End Property


        Public Property pub_Time As DateTime

            Get
                Return time
            End Get
            Set(value As DateTime)
                time = value
            End Set
        End Property

        Public Property pub_message As String
            Get
                Return Me.message
            End Get
            Set(value As String)
                message = value
            End Set
        End Property

        Public Property pub_contact As Object
            Get
                Return Me.contact
            End Get
            Set(value As Object)
                contact = value
            End Set
        End Property

        Public Property pub_list As Object
            Get
                Return Me.list
            End Get
            Set(value As Object)
                list = value
            End Set
        End Property
    End Class

End Namespace

