Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class Link

        Dim rel As String '	The type of relational link. One possible value is self, which means that the link directs to the resource for the representation that the link is in. 'Next' and 'prev' are used for providing paging functionality relative to the current page.
        Dim href As String '	The destination URL of the relational link.

        Public Property pub_rel As String
            Get
                Return rel
            End Get
            Set(value As String)
                rel = value

            End Set
        End Property

        Public Property pub_href As String
            Get
                Return href
            End Get
            Set(value As String)
                href = value

            End Set
        End Property
    End Class
End Namespace
