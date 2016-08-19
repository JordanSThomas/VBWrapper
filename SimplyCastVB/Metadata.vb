Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SimplyCastWrapper

    Public Class Metadata

        Dim id As String '	The unique identifier of the metadata field. System fields will be named user defined fields will be numeric and autoassigned.
        Dim type As String 'The type of metadata field. 'multi string' and 'multi number' allow multiple values of the given type stored as an array. 'single number' and 'single string' can store only one value of the given type. 'sum number' will sum new values applied to it, rather than overwrite them.
        Dim userDefined As Boolean '	If the metadata field is user defined, this parameter will equal 1. It will equal 0 if the metadata field is a system field.
        Dim visible As Boolean 'If the metadata field is visible in the user interface, this value will equal 1. It will equal 0 if it is not visible.
        Dim editable As Boolean 'If the metadata field is editable externally (by user or API call), this parameter will equal 1. It will equal 0 if the field value is read-only.
        Dim links As List(Of ContactList)  '	A link to the metadata field resource.
        Public name As String '	The name of the metadata field.
        Public value As Object 'Depending on the data type, could either be a single string or integer, or a collection of strings or a collection of integers. This property will only exist on metadata fields (that is, an actual value that exists on a contact).
        Public Property pub_id As Integer
            Get
                Return Me.id
            End Get
            Set(value As Integer)
                Me.id = value
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

        Public Property pub_type As String
            Get
                Return Me.type
            End Get
            Set(value As String)
                Me.type = value
            End Set
        End Property

        Public Property pub_links As List(Of ContactList)
            Get
                Return Me.links
            End Get
            Set(value As List(Of ContactList))
                links = value
            End Set
        End Property

        Enum metadata_types As Integer
            multinumber = 0
            multistring = 1
            singlenumber = 2
            singlestring = 3
            sumnumber = 4
        End Enum


    End Class
End Namespace
