Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class EmailCampaignSend

        Dim id As Integer '	The unique identifier of the email campaign send.
        Dim sentTime As DateTime 'The time the email was sent.
        Dim delivered As Boolean 'If the email was delivered.
        Dim deliveredTime As DateTime 'The time the email was delivered. This is not present if the email was not delivered.
        Dim list As Object 'An object containing an id (list ID). This is not present if there is no list.
        Dim contact As Object 'An object containing an id (contact ID). This is not present if there is no contact.
        Dim hardBounced As Boolean '	If the email hard bounced.
        Dim hardBouncedTime As DateTime '	The time the email hard bounced. This is not present if the email did not hard bounce.
        Dim softBounced As Boolean 'If the email soft bounced.
        Dim softBouncedTime As DateTime 'The time the email soft bounced. This is not present if the email did not soft bounce.
        Dim failed As Boolean '	If the email failed to deliver.
        Dim unsubscribed As Boolean 'If the contact unsubscribed Imports this email.
        Dim unsubscribeTime As DateTime 'The time the contact unsubscribed. This is not present if the contact did not unsubscribe

        Public Property pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value
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
        Public Property pub_deliveredTime As DateTime
            Get
                Return deliveredTime
            End Get
            Set(value As DateTime)
                deliveredTime = value
            End Set
        End Property
        Public Property pub_hardbouncedTime As DateTime
            Get
                Return hardBouncedTime
            End Get
            Set(value As DateTime)
                hardBouncedTime = value
            End Set
        End Property
        Public Property pub_softbouncedTime As DateTime
            Get
                Return softBouncedTime
            End Get
            Set(value As DateTime)
                softBouncedTime = value
            End Set
        End Property
        Public Property pub_unsubscribeTime As DateTime
            Get
                Return unsubscribeTime
            End Get
            Set(value As DateTime)
                unsubscribeTime = value
            End Set
        End Property
        Public Property pub_delivered As Boolean
            Get
                Return failed
            End Get
            Set(value As Boolean)
                failed = value
            End Set
        End Property

        Public Property pub_Failed As Boolean
            Get
                Return failed
            End Get
            Set(value As Boolean)
                failed = value
            End Set
        End Property
        Public Property pub_HardBounced As Boolean
            Get
                Return hardBounced

            End Get
            Set(value As Boolean)
                hardBounced = value
            End Set
        End Property
        Public Property pub_SoftBounced As Boolean
            Get
                Return softBounced

            End Get
            Set(value As Boolean)
                softBounced = value
            End Set
        End Property
        Public Property pub_Unsubscribed As Boolean
            Get
                Return unsubscribed

            End Get
            Set(value As Boolean)
                unsubscribed = value
            End Set
        End Property
        Public Property pub_list As Object
            Get
                Return list
            End Get
            Set(value As Object)
                list = value
            End Set
        End Property
        Public Property pub_contact As Object
            Get
                Return contact
            End Get
            Set(value As Object)
                contact = value
            End Set
        End Property
    End Class
End Namespace
