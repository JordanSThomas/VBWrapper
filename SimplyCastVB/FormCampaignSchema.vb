Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
'Comments cribbed verbatim from Simplycast's API references. https:'app.simplycast.com/?q=api/reference
Namespace SimplyCastWrapper

    Public Class FormCampaignSchema

        Dim fields As Object 'A collection of the most recent fields on the form campaign. These may or may not correspond to the FormCampaignSubmission results. A field object is made up of a key (the field name), a dataType ('text', 'date', 'datetime', 'checkbox' or 'unknown'), and a label (if available).

        Public Property pub_fields As Object
            Get
                Return fields
            End Get
            Set(value As Object)
                fields = value

            End Set
        End Property
    End Class
End Namespace
