Namespace SimplyCastWrapper
    Public Class SimplyCastAPI
        'This class represents the API that handles all requests.
        'Get, Post, Delete actions are represented as public, constant strings.
        Public Const API_GET As String = "GET"
        Public Const API_POST As String = "POST"
        Public Const API_DELETE As String = "DELETE"

        'connection item
        Private connection As SimplyCastAPIConnector
        'Contact manager API handle.

        'Private contactManager As ContactManagerAPI
        Public Property pub_connection As SimplyCastAPIConnector
            Get
                Return connection
            End Get
            Set(value As SimplyCastAPIConnector)
                Me.connection = value
            End Set
        End Property


        '360 API handle.

        'Private simplycast360 As SC360API


    End Class
End Namespace

