Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class EmailCampaignInteraction

        Dim id As Integer 'The unique identifier of the email interaction.
        Dim time As DateTime '	The time the interaction occurred.
        Dim type As String '	The type of interaction. Valid types are 'linkClick', 'emailOpen', and 'webView'.
        Dim webLink As Object '	The webLink object with an 'id' and 'url' field. If the interaction is a link click this will be present.
        Dim platform As String 'The platform that was detected during the interaction. This is not present if no platform detected.
        Dim browser As String 'The web browser that was detected during the interaction. This is not present if no web browser detected.

        Public Property Pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value

            End Set
        End Property

        Public Property Pub_time As DateTime
            Get
                Return time
            End Get
            Set(value As DateTime)
                time = value
            End Set
        End Property

        Public Property Pub_type As String
            Get
                Return type
            End Get
            Set(value As String)
                type = value
            End Set
        End Property

        Public Property pub_weblink As Object
            Get

                Return webLink
            End Get
            Set(value As Object)
                webLink = value
            End Set
        End Property

        Public Property pub_platform As String
            Get
                Return platform
            End Get
            Set(value As String)
                platform = value
            End Set
        End Property

        Public Property pub_browser As String
            Get
                Return browser
            End Get
            Set(value As String)
                browser = value
            End Set
        End Property
    End Class
End Namespace