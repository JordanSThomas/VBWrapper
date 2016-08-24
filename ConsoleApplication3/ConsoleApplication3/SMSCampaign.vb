Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SMSCampaign

        Dim id As Integer 'The unique identifier of the SMS campaign.
        Dim status As String 'The status of the SMS campaign. Valid statues are: transactional_sending, transactional_idle, transactional_queued, building, built, error, sending, complete, aborted, and declined.
        Dim totalPrice As Single '	The total amount of money charged for the SMS campaign.
        Dim totalQueued As Integer 'The total number of contacts that were queued to be sent on the SMS campaign.
        Dim totalSent As Integer 'The total number of contacts that were sent on the SMS campaign.
        Dim totalFailed As Integer '	The total number of contacts that failed to send on the SMS campaign. This only appears in comprehensive mode.
        Dim sentTime As DateTime 'The time the SMS campaign was sent. This field is not present on a transactional campaign.
        Dim transactional As Boolean 'Whether or not a campaign is transactional. A transactional campaign is a campaign that is ongoing with no clear start or end, such as an SMS campaign within a 360 campaign.
        Dim name As String 'The name of the campaign.
        Dim message As String '	The text message, which is sent to the recipients of the campaign.
        Dim lists As List(Of Object) '	A collection of objects, each containing an id (list ID) by default. These objects represent lists connected to the SMS campaign. When in comprehensive mode this object will also have a count of contacts that were placed into the campaign a count of the total contacts queued to send a count of the total contacts that were sent and the name of the contact list. This field may not be present if there are no lists to display.
        Dim totalResponses As Integer 'The total number of text-message responses to the SMS campaign.
        Public Property pub_id As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property
        Public Property pub_status As String
            Get
                Return Me.status
            End Get
            Set(value As String)
                status = value
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
        Public Property pub_totalQueued As Integer
            Get
                Return totalQueued
            End Get
            Set(value As Integer)
                totalQueued = value
            End Set
        End Property

        Public Property pub_totalSent As Integer
            Get
                Return totalSent
            End Get
            Set(value As Integer)
                totalSent = value
            End Set
        End Property
        Public Property pub_totalFailed As Integer
            Get
                Return totalFailed
            End Get
            Set(value As Integer)
                totalFailed = value
            End Set
        End Property
        Public Property pub_totalResponses As Integer
            Get
                Return totalResponses
            End Get
            Set(value As Integer)
                totalResponses = value
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
        Public Property pub_transactional As Boolean
            Get
                Return Me.transactional
            End Get
            Set(value As Boolean)
                transactional = value

            End Set
        End Property
        Public Property pub_name As String
            Get
                Return name
            End Get
            Set(value As String)
                name = value
            End Set
        End Property

        Public Property pub_lists As List(Of Object)
            Get
                Return Me.lists
            End Get
            Set(value As List(Of Object))
                lists = value
            End Set
        End Property
    End Class
End Namespace
