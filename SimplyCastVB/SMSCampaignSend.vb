Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SMSCampaignSend

        Dim id As Integer 'The unique identifier of the SMS campaign send.
        Dim queued As Boolean '	If this send has been queued to be sent.
        Dim sent As Boolean '	If this send has been sent.
        Dim numberto As String 'The phone number the SMS was sent to. "Numberto" is used instead of "To" because VB has the latter as a reserved word.
        Dim numberfrom As String 'The phone number the SMS was sent from. This may not be included if it is not available. "Numberfrom" is used instead of "From" for consistency w/ "numberto".
        Dim queuedTime As DateTime 'The time the SMS was queued to be sent.
        Dim sentTime As DateTime
        '	The time the SMS was sent.
        Dim message As String '	The compiled message that was sent. This includes merged in data.
        Dim contact As Object '	An object containing an id (contact ID). This is not present if there is no contact.
        Dim list As Object '	An object containing an id (list ID). This is not present if there is no list.

        'The pub_ prefix is used because Visual Basic is not case sensitive, and as such the same word capitalized wouldn't work as a public property of a private member.
        Public Property pub_id As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property

        Public Property pub_queued As Boolean
            Get
                Return Me.queued
            End Get
            Set(value As Boolean)
                queued = value

            End Set
        End Property

        Public Property pub_sent As Boolean
            Get
                Return Me.sent
            End Get
            Set(value As Boolean)
                sent = value

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

        Public Property pub_queuedTime As DateTime

            Get
                Return queuedTime
            End Get
            Set(value As DateTime)
                queuedTime = value
            End Set
        End Property

        Public Property pub_sentTime As DateTime

            Get
                Return sentTime
            End Get
            Set(value As DateTime)
                sentTime = value
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

