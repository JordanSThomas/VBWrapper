Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class FormCampaignSubmission

        Dim id As Integer 'The unique identifier of the form campaign submission.
        Dim submittedTime As DateTime 'The time of the submission.
        Dim contact As Object '	An object containing an id (contact ID). This is not present if there is no contact.
        Dim results As List(Of Object) 'The results of the submission. The results collection is made up of individual objects which each possess: a key (the field name), a label at time of submission (if available), a datatype ('text', 'date', 'datetime', 'checkbox' or 'unknown'), and a value (as a collection or as a scalar value). The value will generally only possess one piece of data in the scalar format, but may possess multiple in a collection on the case of certain dataTypes, such as with the 'checkbox' type. Dates and datetimes will be presented in the DateTime format. Dates will appear in the UTC timezone. DateTime will be presented in the account timezone. An error field may appear on an object if an error occurs during retrieval of the result data.
        Public Property pub_id As Integer
            Get
                Return id
            End Get
            Set(value As Integer)
                id = value
            End Set
        End Property
        Public Property pub_submittedTime As DateTime
            Get
                Return submittedTime

            End Get
            Set(value As DateTime)
                submittedTime = value
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
        Public Property pub_results As List(Of Object)
            Get
                Return results
            End Get
            Set(value As List(Of Object))
                results = value

            End Set
        End Property
    End Class
End Namespace
