Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class FormCampaign

        Dim id As Integer '	The unique identifier of the form campaign.
        Dim name As String 'The name of the form campaign.
        Dim published As Boolean 'The flag indicating whether or not the form campaign is published.
        Dim title As String 'The title of the form campaign.
        Dim description As String '	The description of the form campaign.
        Dim optinRequired As Boolean '	The flag indicating if an optin process is required for the contacts collected by the form campaign.
        Dim status As String '	The status of the form campaign. Valid statuses are 'pending', 'approved', 'declined' and 'reapproval'.
        Dim type As String 'The type of form campaign. Valid types are 'survey' and 'form'.
        Dim created As DateTime '	The time when the form campaign was created.
        Dim modified As DateTime 'The time when the form campaign was last modified.
        Dim closingtime As DateTime 'The time when the form campaign is scheduled to close submissions. This only appears if a time is set.

        Dim messages As List(Of Object) 'Normally treated as object<object<object))), which is not recognized by C#. A collection of objects containing messages used by the form campaign. The first level of object represents the type of message, such as 'optin' or 'thankyou'. The second level of object represents the communication channel of the messages being sent, such as email. The final level of object represents a linkage to another API resource. The message contains a name, an id, a type and a set of links. The XML version differs in that it has one collection that contains objects named after the channel of communication. This will not be present if there are no messages.
        Dim totalSubmissions As Integer '	The count of submissions recorded on the form campaign. This only appears when comprehensive mode is enabled.
        Dim lists As List(Of Object) '	A collection of lists associated with the form campaign. These are the lists that contacts are placed onto upon form submission. A list object contains an id (list ID). This is not present if there are no lists.

        Public Property pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value

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

        Public Property pub_published As Boolean
            Get
                Return published
            End Get
            Set(value As Boolean)
                published = value
            End Set
        End Property

        Public Property pub_title As String
            Get
                Return title
            End Get
            Set(value As String)
                title = value

            End Set
        End Property

        Public Property pub_description As String
            Get
                Return description
            End Get
            Set(value As String)
                description = value

            End Set
        End Property

        Public Property pub_optinrequired As Boolean
            Get
                Return optinRequired
            End Get
            Set(value As Boolean)
                optinRequired = value
            End Set
        End Property

        Public Property pub_status As String
            Get
                Return status
            End Get
            Set(value As String)
                status = value
            End Set
        End Property

        Public Property pub_type As String
            Get
                Return type
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
        'The time of the form's closure
        Public Property Pub_ClosingTime As DateTime
            Get
                Return Me.closingtime

            End Get
            Set(value As DateTime)
                Me.closingtime = value

            End Set
        End Property

        Public Property pub_Messages As List(Of Object)
            Get
                Return Me.messages
            End Get
            Set(value As List(Of Object))
                messages = value
            End Set
        End Property

        Public Property pub_totalsubmissions As Integer
            Get
                Return Me.totalSubmissions
            End Get
            Set(value As Integer)
                Me.totalSubmissions = value
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
