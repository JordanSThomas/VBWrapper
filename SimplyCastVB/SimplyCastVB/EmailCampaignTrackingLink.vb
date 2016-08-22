Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class EmailCampaignTrackingLink

        Dim id As Integer '	The unique identifier of the email campaign tracking link.
        Dim trackingLink As String 'The link that appears in the email and is tracked.

        Public Property pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value

            End Set
        End Property

        Public Property pub_trackingLink As String
            Get
                Return trackingLink
            End Get
            Set(value As String)
                trackingLink = value
            End Set
        End Property
    End Class
End Namespace
