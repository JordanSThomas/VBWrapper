Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace SimplyCastWrapper
    Public Class ResourceAPI
        ' /// <summary>
        '/// The API connection used to make requests with.
        '/// </summary>
        Protected Shared connection As SimplyCastAPIConnector


        '/// <summary>
        '/// Constructor.
        '/// </summary>
        ' /// <param name="connection">The API connection, provided by 
        '/// the SimplyCastAPI class.</param>
        Public Sub New(new_connection As SimplyCastAPIConnector)
            connection = new_connection
        End Sub


    End Class

End Namespace
