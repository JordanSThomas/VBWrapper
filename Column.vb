Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
'Comments cribbed verbatim from Simplycast's API references. https:'app.simplycast.com/?q=api/reference
Namespace SimplyCastWrapper

    Public Class Column

        Dim id As Integer 'The unique identifier of the column.
        Dim type As String 'The data type of the column. Can be either 'string' or 'date'.
        Dim userDefined As Boolean 'If the column is user defined, this parameter will equal 1/true. It will equal 0/false if the column is a system column.
        Dim visible As Boolean 'If the column is visible in the user interface, this value will equal 1. It will equal 0 if it is not visible.
        Dim editable As Boolean 'If the column is editable externally (by user or API call), this parameter will equal 1. It will equal 0 if the column is read-only.
        Dim extended As Boolean 'Extended columns are system columns that don't contain direct contact information, such as email hard bounces. If this parameter is equal to 1, the column is considered an extended column. It will equal 0 otherwise.
        Public name As String 'The column name.
        Public mergeTags As List(Of String) 'A collection of merge tags. Merge tags are used to dynamically insert personalized content into a campaign. If any of the given tags appear in supported content, the values of this column will replace the tag.

        Public Property pub_id As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
            End Set
        End Property

        Public Property pub_type As String
            Get
                Return Me.type
            End Get
            Set(value As String)
                Me.type = value
            End Set

        End Property

        Public Property pub_userDefined As Boolean
            Get
                Return Me.userDefined
            End Get
            Set(value As Boolean)
                Me.userDefined = value
            End Set

        End Property

        Public Property pub_visible As Boolean
            Get
                Return Me.visible
            End Get
            Set(value As Boolean)
                Me.visible = value
            End Set
        End Property

        Public Property pub_editable As Boolean
            Get
                Return Me.editable
            End Get
            Set(value As Boolean)
                Me.editable = value
            End Set
        End Property

        Public Property pub_extended As Boolean
            Get
                Return Me.extended
            End Get
            Set(value As Boolean)
                Me.extended = value
            End Set
        End Property


    End Class
End Namespace
