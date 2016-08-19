Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class SC360CampaignElement

        Dim id As Integer 'The unique identifier of the 360 or autoresponder element.
        Dim active As Boolean '	The flag indicating whether or not this element is active.
        Dim name As String  'The name of the element.
        Dim type As String '	The type of the element. Common types include email (an email campaign) fax (a fax campaign) voice (a voice campaign) sms (an SMS campaign) universaldecision (a decision) timer (a send list at fixed time element) repeatingevent (a repeating trigger) list (a contact list) and delay (a delay element).
        Dim created As DateTime 'The time when the 360 or autoresponder element was created.
        Dim modified As DateTime 'The time when the 360 or autoresponder element was last modified.
        Dim associatedObject As Object 'An associated object is another resource that holds data relevant to the current element's activities. An example of an associated object is an email campaign. An associated object includes the type of the associated resource the id representing the identifier in that resource and links to the resource. Valid types include email (an email campaign), sms (an SMS campaign), and list (a contact list).


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

        Public Property pub_associatedobject As Object
            Get
                Return associatedObject
            End Get
            Set(value As Object)
                associatedObject = value

            End Set
        End Property
    End Class
End Namespace

