Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class EmailCampaign

        Dim id As Integer 'The unique identifier of the email campaign.
        Dim status As String 'The status of the email campaign. Valid statues are: transactional_sending, transactional_idle, transactional_queued, building, built, error, sending, complete, aborted, and declined.
        Dim sentTime As DateTime    'The time the email campaign was sent. This field is not present on a transactional campaign.
        Dim totalQueued As Integer 'The total number of contacts that were queued to be sent on the email campaign.
        Dim totalSent As Integer 'The total number of contacts that were sent on the email campaign.
        Dim totalFailed As Integer '	The total number of contacts that failed to send on the email campaign. This only appears in comprehensive mode.
        Dim totalHardBounces As Integer 'The total number of contacts that were hard bounced on the email campaign. A hard bounce may occur if a valid email receiver (server) cannot be found. This only appears in comprehensive mode.
        Dim totalSoftBounces As Integer '	The total number of contacts that were soft bounced on the email campaign. A soft bounce may occur if an email receiver (server) did not want to accept the email. This can occur if a receiver is too busy, an inbox is full, or any number of other internal issues with the receiver occur. This only appears in comprehensive mode.
        Dim totalUnsubscribes As Integer 'The total number of contacts that unsubscribed on the email campaign. This only appears in comprehensive mode.
        Dim subject As String 'The subject line of the email campaign.
        Dim lists As List(Of Object) 'A collection of objects, each containing an id (list ID) by default. These objects represent lists connected to the email campaign. When in comprehensive mode this object will also have a count of contacts and the name of the contact list. This field may not be present if there are no lists to display.


        Public Property pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value
            End Set
        End Property

        Public Property pub_status As Integer
            Get
                Return status
            End Get
            Set(value As Integer)
                status = value
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
        Public Property pub_totalHardBounces As Integer
            Get
                Return totalHardBounces

            End Get
            Set(value As Integer)
                totalHardBounces = value
            End Set
        End Property
        Public Property pub_totalSoftBounces As Integer
            Get
                Return totalSoftBounces

            End Get
            Set(value As Integer)
                totalSoftBounces = value
            End Set
        End Property
        Public Property pub_totalUnsubscribes As Integer
            Get
                Return totalUnsubscribes

            End Get
            Set(value As Integer)
                totalUnsubscribes = value
            End Set
        End Property
        Public Property pub_subject As String
            Get
                Return subject
            End Get
            Set(value As String)
                subject = value

            End Set
        End Property
        Public Property pub_lists As List(Of Object)
            Get
                Return Me.lists
            End Get
            Set(value As List(Of Object))
                Me.lists = value
            End Set
        End Property
    End Class
End Namespace
