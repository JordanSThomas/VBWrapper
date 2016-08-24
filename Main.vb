Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
'Comments cribbed verbatim from Simplycast's API references. https:'app.simplycast.com/?q=api/reference
Namespace SimplyCastWrapper



    Public Class Main

        Inherits ResourceAPI
        Shared exampletest As Boolean = False
        Public Shared Property pub_exampletest As Boolean
            Get
                Return exampletest
            End Get
            Set(value As Boolean)
                exampletest = value
            End Set
        End Property
        Public Sub New(connector As SimplyCastAPIConnector)
            MyBase.New(connector)
            Dim sampleAPI As SimplyCastAPI = New SimplyCastAPI()

            Dim myexample As Examples = New Examples(sampleAPI, exampletest)
        End Sub


        'HTTP status codes
        '200 OK	This Is the generic “Everything processed successfully” status.
        '201 Created	This status indicates a New resource has been created. A representation of the created resource element should be returned in the response message body this should include a link to the created resource.
        '202 Accepted	The request has been accepted for processing, but may Not have completed when the response Is returned. This status Is usually associated with a batch request, And a status resource that can be polled will be returned in the response message body.
        '204 No Content	The request has been completed, And there Is no message body in the response. This Is usually the response to a successful DELETE request.
        '4xx Client Error
        '400 Bad Request	There was an issue with the request. This could be due to a bad request message body Or invalid query parameters. The error response message should have a detailed message of what was wrong with the request.
        '401 Unauthorized	This status will be returned either when no authentication Is provided for a non-public resource, Or when the public key Or the request signature could Not be validated. The particular issue will be provided in the error response message.
        '403 Forbidden	This error will occur when attempting to access a resource that does Not belong to the account holding the authenticated key
        '404 Not Found	Resource Is Not found. This may be returned when attempting to access a previously deleted resource.
        '405 Method Not Allowed	The HTTP method used (GET, POST, etc.) Is Not allowed on this resource.
        '415 Unsupported Media Type	The media type (application/xml, application/json, etc.) requested Is Not supported. All resources as of writing support XML And JSON for both request And response message bodies.
        '5xx Server Error
        '500 Internal Server Error	This error Is thrown when an internal API error occurs. In the event that this does happen, the API team Is notified of the error so that it can be resolved quickly.

        Enum httpstatuscodes


            OK = 200
            CREATED = 201
            ACCEPTED = 202
            NO_CONTENT = 204
            BAD_REQUEST = 400
            UNAUTHORIZED = 401
            FORBIDDEN = 403
            NOT_FOUND = 404
            METHOD_NOT_ALLOWED = 405
            UNSUPPORTED_MEDIA_TYPE = 415
            INTERNAL_SERVER_ERROR = 500


        End Enum

        Public Enum SuppressionType

            '/// <summary>
            '/// An email suppression list.
            ' /// </summary>
            Email

            ' /// <summary>
            '  /// A phone number suppression list.
            '/// </summary>
            Phone

            '/// <summary>
            ' /// A fax number suppression list.
            '/// </summary>
            Fax

            ' /// <summary>
            ' /// A mobile number suppression list.
            ' /// </summary>
            Mobile
        End Enum

        Public Shared Function BoolToInt(value As Boolean) As Integer
            'returns 1 if true, 0 if false, for cases where a true wouldn't implicitly return '1'. Forces an explicit interpretation of 1 for true.

            If (True = value) Then
                Return 1

            Else : Return 0
            End If
        End Function
        'ContactList
        Public Shared Function ContactList_RetrieveMultiple_Request(limit As Integer, listname As String, offset As Integer) As List(Of ContactList)
            Dim QueryParams As Dictionary(Of String, String) = New Dictionary(Of String, String)(3)
            QueryParams.Add("offset", offset.ToString())
            QueryParams.Add("limit", offset.ToString())
            If listname.Length > 0 Then
                QueryParams.Add("listname", listname)
            End If
            'This resource allows for the retrieval of a collection of contact lists. Imports the 'query' parameter, it is possible to use this resource to retrieve contact lists by their name if you do not already have the list identifier.
            Return connection.API_Call(Of List(Of ContactList))(SimplyCastAPI.API_GET, "contactmanager/lists", QueryParams, Nothing)
        End Function
        'Will deal with responses later, as of Aug 16, 2016
        Public Shared Function ContactList_RetrieveMultiple_Response(filtercount As Integer, lists As List(Of ContactList), responseCount As Integer, totalCount As Integer) As Integer

            'The response will contain a collection of ContactList entities.If there are no lists on the account, or no lists match a given query, a response of 204 No Content will be returned.
            If (lists Is Nothing) Then
                Return httpstatuscodes.NO_CONTENT

            Else : Return httpstatuscodes.OK
            End If
        End Function
        Public Shared Function ContactList_Create_Request(listname As String) As ContactList
            Dim List As ContactList = New ContactList()
            List.name = listname
            Return connection.API_Call(Of ContactList)(SimplyCastAPI.API_POST, "contactmanager/lists", Nothing, List)
            'Create a new contact list. The created list will be empty adding new contacts can be performed by POSTing to the contact subresource of the list.
            'Return httpstatuscodes.OK
        End Function
        Public Shared Function ContactList_Create_Response(List As ContactList) As Integer

            'The response will relay the created list representation as a ContactList object.
            Return httpstatuscodes.OK
        End Function
        Public Shared Function ContactList_Retrieve_Request(ListID As Integer) As ContactList
            '/// <summary>
            '        /// Retrieve a contact list entity by its ID.
            '       /// </summary>
            '      /// <param name="listID">The ID of the list to retrieve.</param>
            '     /// <returns>A ListEntity of the retrieved contact list.</returns>
            Return connection.API_Call(Of ContactList)(SimplyCastAPI.API_GET, "contactmanager/lists/" + ListID.ToString(), Nothing, Nothing)
            'Return httpstatuscodes.OK
        End Function
        'Retrieve by name
        Public Shared Function ContactList_Retrieve_Request(ListName As String) As ContactList
            '/// <summary>
            '        /// Retrieve a contact list entity by its name.
            '       /// </summary>
            '      /// <param name="listname">The name of the list to retrieve.</param>
            '     /// <returns>A ListEntity of the retrieved contact list.</returns>
            Return connection.API_Call(Of ContactList)(SimplyCastAPI.API_GET, "contactmanager/lists/" + ListName, Nothing, Nothing)
            'Return httpstatuscodes.OK
        End Function


        Public Shared Function ContactList_Retrieve_Response(List As ContactList) As Integer

            Return httpstatuscodes.OK
        End Function
        'Modify the specified contact list. Currently, renaming the list is the only modification that is supported.
        Public Shared Function ContactList_Modify_Request(listID As Integer, newname As String) As ContactList
            Dim List As ContactList = New ContactList
            List.name = newname
            Return connection.API_Call(Of ContactList)(SimplyCastAPI.API_POST, "contactmanager/lists/" + listID.ToString(), Nothing, List)
            ' Return httpstatuscodes.OK
        End Function
        Public Shared Function ContactList_Modify_Response(List As ContactList) As Integer

            Return httpstatuscodes.OK
        End Function
        'Permanently delete a contact list. As a contact list is only collection of references to contacts, deleting the list will not delete the assoctiated contacts. This action is irreversible.
        Public Shared Sub ContactList_Delete_Request(listID As Integer)
            connection.API_Call(Of Object)(SimplyCastAPI.API_DELETE, "contactmanager/lists/" + listID.ToString(), Nothing, Nothing)

        End Sub
        Public Shared Function ContactList_Delete_Response() As Integer

            'On successful deletion of a contact list, a '204 No Content' response code will be returned.
            Return httpstatuscodes.NO_CONTENT
        End Function
        'Get a group of contacts from a list.
        Public Shared Function ContactList_GetFromList_Request(ListID As Integer, Limit As Integer, Offset As Integer, query As String) As List(Of Contact)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)(3)
            queryParameters.Add("offset", Offset.ToString())
            queryParameters.Add("limit", Limit.ToString())
            If (query.Length > 0) Then

                queryParameters.Add("query", query)
            End If

            Return connection.API_Call(Of List(Of Contact))(SimplyCastAPI.API_GET, "contactmanager/lists/" + ListID + "/contacts", queryParameters, Nothing)

            'Return httpstatuscodes.OK
        End Function
        'Get a group of contacts from a list.
        Public Shared Function ContactList_GetByName_Request(listName As String, Optional offset As Integer = 0, Optional limit As Integer = 0) As List(Of ContactList)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)(3)
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())


            If listName.Length > 0 Then
                queryParameters.Add("listName", listName)
            End If


            Return connection.API_Call(Of List(Of ContactList))(SimplyCastAPI.API_GET, "contactmanager/lists", queryParameters, Nothing)
            'Return httpstatuscodes.OK
        End Function

        Public Shared Function ContactList_GetFromList_Response(contacts As List(Of Contact), filterCount As Integer, responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function ContactList_AddToList_Request(listID As Integer, contacts As List(Of Integer), strict As Boolean) As List(Of Contact)
            'Dim contactIDs As List(Of Integer)
            'Aug 16, 2016, 3:12pm- will have to deliberate on how to deal with this.
            Dim queryParams As Dictionary(Of String, String) = New Dictionary(Of String, String)
            queryParams.Add("strict", BoolToInt(strict))
            'For each id, add a blank contact with that ID to the list
            Return connection.API_Call(Of List(Of Contact))("POST", "contactmanager/lists/" + listID.ToString() + "/contacts", queryParams, contacts) 'ints concatenated onto strings need an explicit .ToString to avoid an invalid cast
        End Function
        Public Shared Function ContactList_AddToList_Response(contacts As List(Of Contact)) As Integer

            Return httpstatuscodes.OK
        End Function
        'Remove a contact from a list, specifically.
        Public Shared Sub ContactList_DeleteFromList_Request(listID As Integer, contactID As Integer)
            connection.API_Call(Of Object)("DELETE", "contactmanager/lists/" + listID.ToString() + "/contacts/" + contactID.ToString(), Nothing, Nothing)

        End Sub
        Public Shared Function ContactList_DeleteFromList_Response() As Integer

            'On successful deletion of a contact from a list, a '204 No Content' response will be returned
            Return httpstatuscodes.NO_CONTENT
        End Function
        'Contacts
        Public Shared Function Contacts_GetGeneral_Request(query As String, Optional extended As Boolean = False, Optional ignoreEmptyFields As Boolean = False, Optional offset As Integer = 0, Optional limit As Integer = 100) As List(Of Contact)
            Dim queryparams As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryparams.Add("offset", offset.ToString())
            queryparams.Add("limit", limit.ToString())
            If (query.Length > 0) Then

                queryparams.Add("query", query)
            End If
            queryparams.Add("ignoreEmptyFields", Main.BoolToInt(ignoreEmptyFields))
            queryparams.Add("extended", Main.BoolToInt(extended))


            Return connection.API_Call(Of List(Of Contact))("GET", "contactmanager/contacts", queryparams, Nothing)
            ' Return httpstatuscodes.OK
        End Function
        Public Shared Function Contacts_GetGeneral_Response(contacts As List(Of Contact), filterCount As Integer, links As List(Of ContactList), responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        'Create a contact, from a dictionary of fields. ListIDs is nothing by default, if left that way it loads a new integer list, otherwise it uses the parameter provided.

        Public Shared Function Contacts_Create_Request(fields As Dictionary(Of String, String), Optional listIDs As List(Of Integer) = Nothing) As Contact
            If (listIDs Is Nothing) Then
                Return Contacts_Create_Request(fields, New List(Of Integer))
            Else
                Dim myContact As Contact = New Contact()
                Dim fieldlist As List(Of Field) = New List(Of Field)(fields.Count)
                Dim fieldcounter As Integer = 0
                For Each f As KeyValuePair(Of String, String) In fields

                    Dim fieldEntity As Field = New Field(f.Key, f.Value)
                    'fieldEntity.pub_id = f.Key
                    'fieldEntity.value = f.Value
                    fieldlist.Item(fieldcounter) = fieldEntity
                    fieldcounter = fieldcounter + 1
                Next
                myContact.fields = fieldlist
                If (listIDs.Count > 0) Then
                    Dim listcounter As Integer = 0
                    Dim listlist As List(Of ContactList) = New List(Of ContactList)(listIDs.Count)
                    For Each index As Integer In listIDs
                        Dim listEntity As ContactList = New ContactList()
                        listEntity.Pub_ID = index
                        listlist.Item(listcounter) = listEntity
                        listcounter = listcounter + 1

                    Next
                    myContact.pub_lists = listlist


                End If
                myContact.pub_ID = Nothing
                Return connection.API_Call(Of Contact)("POST", "contactmanager/contacts", Nothing, myContact)
            End If


            'Will return to this later- Aug 17
            'Return httpstatuscodes.OK
        End Function
        'Public Shared Function Contacts_Create_Request(contact As Contact, mergecolumn As String) As Integer
        'Will return to this later- Aug 17
        '  Return httpstatuscodes.OK
        ' End Function
        Public Shared Function Contacts_Create_Response(contact As Contact) As Integer

            Return httpstatuscodes.OK
        End Function

        '/// <summary>
        '/// Get a contact from the contact database by its ID.
        '/// </summary>
        '/// <param name="contactID">The ID of the contact to retrieve.</param>
        '/// <returns>A representation of the retrieved contact.</returns>
        Public Shared Function Contacts_GetByID_Request(contactID As Integer) As Contact
            Return connection.API_Call(Of Contact)("GET", "contactmanager/contacts/" + contactID.ToString(), Nothing, Nothing)
            ' Return httpstatuscodes.OK
        End Function
        Public Shared Function Contacts_GetByID_Response(contact As Contact) As Integer

            Return httpstatuscodes.OK
        End Function
        '/// <summary>
        '/// Update the field values of the given contact.
        '/// </summary>
        '/// <param name="contactID">The ID of the contact  to update.</param>
        '/// <param name="fields">A dictionary of field IDs (as the key) and 
        '/// the new field value (as the value).</param>
        '/// <returns>A representation of the updated contact.</returns>
        Public Shared Function Contacts_Update_Request(contactid As Integer, fields As Dictionary(Of String, String)) As Contact
            Dim myContact As Contact = New Contact()
            Dim fieldlist As List(Of Field) = New List(Of Field)(fields.Count)
            Dim fieldcounter As Integer = 0
            For Each f As KeyValuePair(Of String, String) In fields

                Dim fieldEntity As Field = New Field(f.Key, f.Value)
                ' fieldEntity.pub_id = f.Key
                ' fieldEntity.value = f.Value
                fieldlist.Item(fieldcounter) = fieldEntity
                fieldcounter = fieldcounter + 1
            Next
            myContact.fields = fieldlist
            Return connection.API_Call(Of Contact)("POST", "contactmanager/contacts/" + contactid, Nothing, myContact)
        End Function
        'This does one with multiple  contacts in an unofficial list
        Public Shared Function Contacts_UpdateMultiple_Request(contactid As Integer, fields As Dictionary(Of String, String)) As List(Of Contact)
            Dim myContact As Contact = New Contact()
            Dim fieldlist As List(Of Field) = New List(Of Field)(fields.Count)
            Dim fieldcounter As Integer = 0
            For Each f As KeyValuePair(Of String, String) In fields

                Dim fieldEntity As Field = New Field(f.Key, f.Value)
                'fieldEntity.pub_id = f.Key
                'fieldEntity.value = f.Value
                fieldlist.Item(fieldcounter) = fieldEntity
                fieldcounter = fieldcounter + 1
            Next
            myContact.fields = fieldlist
            Return connection.API_Call(Of List(Of Contact))("POST", "contactmanager/contacts/" + contactid, Nothing, myContact)
        End Function
        Public Shared Function Contacts_Update_Response(contact As Contact) As Integer

            Return httpstatuscodes.OK
        End Function
        'Delete a contact from the database, and any lists it may be in
        Public Shared Sub Contacts_DeleteGeneral_Request(contactID As Integer)
            connection.API_Call(Of Object)("DELETE", "contactmanager/contacts/" + contactID.ToString(), Nothing, Nothing)

        End Sub
        Public Shared Function Contacts_DeleteGeneral_Response() As Integer

            Return httpstatuscodes.NO_CONTENT
        End Function

        Public Shared Function Contacts_RetrieveAllContactMetadataFields_Request(contactID As Integer, limit As Integer, offset As Integer) As List(Of Metadata)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            Return connection.API_Call(Of List(Of Metadata))("GET", "contactmanager/contacts/" + contactID + "/metadata", queryParameters, Nothing)


        End Function
        Public Shared Function Contacts_RetrieveAllContactMetadataFields_Response(filterCount As Integer, links As List(Of ContactList), metadata As List(Of Metadata), responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        '  /// <summary>
        '        /// Get a metadata field for a contact by ID or name (overloaded).
        '       /// </summary>
        '      /// <param name="contactID">The ID/name of the contact to get the metadata 
        '     /// field from.</param>
        '    /// <param name="metadataFieldID">The ID of the metadata field to get. 
        '   /// System metadata columns are typically strings. User-defined 
        '  /// metadata columns, however, are autoincrementing unique 
        ' /// integers.</param>
        '/// <returns>The requested metadata field.</returns>
        '
        '
        '
        Public Shared Function Contacts_RetrieveMetadataField_Request(contactID As Integer, metadataID As Integer) As Metadata
            Return connection.API_Call(Of Metadata)("GET", "contactmanager/contacts/" + contactID + "/metadata/" + metadataID, Nothing, Nothing)


        End Function
        'retrieve metadata field as a string
        Public Shared Function Contacts_RetrieveMetadataField_Request(contactid As Integer, metadataName As String) As List(Of Metadata)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("query", "`name` = '" + metadataName + "'")

            Return connection.API_Call(Of List(Of Metadata))("GET", "contactmanager/contacts/" + contactid + "/metadata", queryParameters, Nothing)
        End Function

        Public Shared Function Contacts_RetrieveMetadataField_Response(metadataField As Metadata) As Integer

            Return httpstatuscodes.OK
        End Function
        ' /// <summary>
        ' /// Update a metadata field. This function accepts an array of string 
        ' /// values. If the metadata field type accepts a singular value, you
        ' /// are expected to hand only a single value in this parameter. If 
        ' /// the field type accepts numeric values, hand the string 
        ' /// representation of the numeric value. 
        '  /// </summary>
        ''  /// <param name="contactID">The ID of the contact to modify.</param>
        '  /// <param name="metadataFieldID">The ID of the field to modify.
        ' /// </param>
        ' /// <param name="values">The value(s) to set on the field.</param>
        ' /// <returns>The metadata field with the updated value.</returns>
        Public Shared Function Contacts_ModifyMetadataFieldValue_Request(contactID As Integer, metadataFieldID As String, values As String, Optional overwrite As Boolean = False) As Metadata
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()

            queryParameters.Add("overwrite", Main.BoolToInt(overwrite))

            Dim body As Metadata = New Metadata()
            body.value = values

            Return connection.API_Call(Of Metadata)("POST", "contactmanager/contacts/" + contactID + "/metadata/" + metadataFieldID, queryParameters, body)


        End Function
        Public Shared Function Contacts_ModifyMetadataFieldValue_Response(metadataField As Metadata) As Integer

            Return httpstatuscodes.OK
        End Function

        'Create contacts with batch, optionally accepting an id as a string
        'For the VB type wrapper, unless otherwise advised, batches will be lists of objects. Batch collections will be lists of these object lists.
        Public Shared Function Contacts_CreateWithBatch_Request(requests As List(Of Contact)) As List(Of Contact)
            Dim batch As List(Of Contact) = New List(Of Contact)()
            batch = requests
            Return connection.API_Call(Of List(Of Contact))("POST", "contactmanager/contacts/batch", Nothing, batch)

        End Function

        Public Shared Function Contacts_CreateWithBatch_Request(mergeColumn As String, requests As List(Of Contact)) As List(Of Object)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("mergecolumn", mergeColumn)
            Dim batch As List(Of Contact) = New List(Of Contact)()
            batch = requests
            Return connection.API_Call(Of List(Of Object))("POST", "contactmanager/contacts/batch", Nothing, batch)

        End Function
        Public Shared Function Contacts_CreateWithBatch_Response(Batch As List(Of Object)) As Integer

            Return httpstatuscodes.OK
        End Function

        'Retrieve batch information with ID as integer or string.
        Public Shared Function Contacts_RetrieveBatchInfo_Request(batchID As Integer) As List(Of Object)

            Return connection.API_Call(Of List(Of Object))("GET", "contactmanager/contacts/batch/" + batchID.ToString(), Nothing, Nothing)
        End Function

        Public Shared Function Contacts_RetrieveBatchInfo_Request(batchID As String) As List(Of Object)

            Return connection.API_Call(Of List(Of Object))("GET", "contactmanager/contacts/batch/" + batchID, Nothing, Nothing)
        End Function
        Public Shared Function Contacts_RetrieveBatchInfo_Response(Batch As List(Of Object)) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function Contacts_RetrieveBatchResults_Request(batchID As Integer) As List(Of List(Of Object))
            Return connection.API_Call(Of List(Of List(Of Object)))("GET", "contactmanager/contacts/batch/" + batchID.ToString() + "/result", Nothing, Nothing)

        End Function
        Public Shared Function Contacts_RetrieveBatchResults_Response(Count As Integer, ErrorMessage As String, results As List(Of Object)) As Integer

            Return httpstatuscodes.OK
        End Function
        'Metadata
        Public Shared Function Metadata_Column_RetrieveAll_Request(Optional offset As Integer = 0, Optional limit As Integer = 100) As List(Of Metadata)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            Return connection.API_Call(Of List(Of Metadata))("GET", "contactmanager/metadata", queryParameters, Nothing)

        End Function
        Public Shared Function Metadata_Column_RetrieveAll_Response() As Integer

            Return httpstatuscodes.OK
        End Function
        'create a metadata column
        Public Shared Function Metadata_Column_Create_Request(name As String, type As String) As Metadata

            Dim requestBody As Metadata = New Metadata()
            requestBody.name = name
            requestBody.pub_type = type
            type = type.ToLower 'to ensure case insensitivity

            'to ensure that strings without spaces get read properly
            If type = "multinumber" Then type = "multi number"
            If type = "multistring" Then type = "multi string"
            If type = "singlenumber" Then type = "single number"
            If type = "singlestring" Then type = "single string"
            If type = "sumnumber" Then type = "sum number"

            'Sanity check for type
            requestBody.pub_type = type
            If (type <> "multi number" Or type <> "multi string" Or type <> "single number" Or type <> "single string" Or type <> "sum number") Then
                Console.WriteLine("Improper metadata type specified. Column not created.")
                Return Nothing 'Abort function
            End If



            Return connection.API_Call(Of Metadata)("POST", "contactmanager/metadata", Nothing, requestBody)
        End Function
        'create a metadata column
        Public Shared Function Metadata_Column_Create_Request(name As String, type As Integer) As Metadata

            Dim requestBody As Metadata = New Metadata()
            requestBody.name = name
            Select Case type
                Case Metadata.metadata_types.multinumber
                    requestBody.pub_type = "multi number"
                Case Metadata.metadata_types.multistring
                    requestBody.pub_type = "multi string"
                Case Metadata.metadata_types.singlenumber
                    requestBody.pub_type = "single number"
                Case Metadata.metadata_types.singlestring
                    requestBody.pub_type = "single string"
                Case Metadata.metadata_types.sumnumber
                    requestBody.pub_type = "sum number"
                    'Sanity check for type
                Case Else
                    Console.WriteLine("Improper metadata type specified. Column not created.")
                    Return Nothing 'Abort function
            End Select
            requestBody.pub_type = type



            Return connection.API_Call(Of Metadata)("POST", "contactmanager/metadata", Nothing, requestBody)
        End Function
        Public Shared Function Metadata_Column_Create_Response(metadataColumn As Metadata) As Integer

            Return httpstatuscodes.OK
        End Function
        'Get request by ID , single column
        Public Shared Function Metadata_Column_Get_Request(column As String) As Metadata

            Return connection.API_Call(Of Metadata)("GET", "contactmanager/metadata/" + column, Nothing, Nothing)



        End Function
        'get request by name, multiple columns
        Public Shared Function Metadata_Column_Multiple_Get_Request(column As String) As List(Of Metadata)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("query", "`name` = '" + column + "'")
            Return connection.API_Call(Of List(Of Metadata))("GET", "contactmanager/metadata", queryParameters, Nothing)



        End Function

        Public Shared Function Metadata_Column_Get_Response(metadataColumn As Metadata) As Integer

            Return httpstatuscodes.OK
        End Function
        'Rename a metadata column
        Public Shared Function Metadata_Column_Modify_Request(columnid As Integer, name As String, Optional values As List(Of String) = Nothing, Optional overwrite As Boolean = False) As Metadata
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            Dim requestBody As Metadata = New Metadata
            queryParameters.Add("overwrite", Main.BoolToInt(overwrite))
            requestBody.name = name
            requestBody.value = values
            Return connection.API_Call(Of Metadata)("POST", "contactmanager/metadata/" + columnid, Nothing, requestBody)
            'Return httpstatuscodes.OK
        End Function
        Public Shared Function Metadata_Column_Modify_Response(metadataColumn As Metadata) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Sub Metadata_Column_Delete_Request(columnID As String)
            connection.API_Call(Of Object)("DELETE", "contactmanager/metadata/" + columnID, Nothing, Nothing)
            'Return httpstatuscodes.NO_CONTENT
        End Sub
        Public Shared Function Metadata_Column_Delete_Response() As Integer

            Return httpstatuscodes.NO_CONTENT
        End Function
        'Columns
        'retrieve columns  by name
        Public Shared Function Columns_ContactManager_RetrieveAll_Request(name As String) As List(Of Column)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("query", "`name` = '" + name + "'")
            Return connection.API_Call(Of List(Of Column))("GET", "contactmanager/columns", queryParameters, Nothing)

        End Function
        Public Shared Function Columns_ContactManager_RetrieveByName_Request(name As String, columns As List(Of Column)) As Column
            For Each e As Column In columns

                If e.name = name Then

                    Return e
                End If
            Next

            Return Nothing

        End Function
        Public Shared Function Columns_ContactManager_RetrieveAll_Response(columns As List(Of Column), filterCount As Integer, links As List(Of ContactList), responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function Columns_CreateNew(mergeTags As List(Of String), name As String, Type As String) As Column

            Dim mycolumn As Column = New Column()
            mycolumn.name = name

            mycolumn.pub_type = Type
            mycolumn.mergeTags = mergeTags

            Return connection.API_Call(Of Column)("POST", "contactmanager/columns", Nothing, mycolumn)
        End Function
        Public Shared Function Columns_CreateNew(column As Column) As Integer

            Return httpstatuscodes.OK
        End Function
        'get single column
        Public Shared Function Columns_GetSingle_Request(columnID As Integer) As Column


            Return connection.API_Call(Of Column)("GET", "contactmanager/columns" + columnID.ToString(), Nothing, Nothing)

        End Function

        'Get columns as a list
        Public Shared Function Columns_Get_Request(Optional offset As Integer = 0, Optional limit As Integer = 100) As List(Of Column)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())

            Return connection.API_Call(Of List(Of Column))("GET", "contactmanager/columns", queryParameters, Nothing)

        End Function
        Public Shared Function Columns_Get_Response(column As Column) As Integer

            Return httpstatuscodes.OK
        End Function
        'modify by appending merge tags
        Public Shared Function Columns_Mergetags_Request(columnID As String, mergetags As List(Of String), append As Boolean) As Column
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("append", Main.BoolToInt(append))

            Dim mycolumn As Column = New Column()
            mycolumn.mergeTags = mergetags

            Return connection.API_Call(Of Column)("POST", "contactmanager/columns/" + columnID, queryParameters, mycolumn)

        End Function
        'rename columns

        'Suppression
        'Get suppression list entries

        Public Shared Function Suppression_GetSuppressionListEntries_Request(listtype As SuppressionType, Optional offset As Integer = 0, Optional limit As Integer = 100, Optional query As String = "") As List(Of String)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If

            Dim list As String

            Select Case listtype
                Case SuppressionType.Email
                    list = "Email"
                Case SuppressionType.Fax
                    list = "Fax"
                Case SuppressionType.Mobile
                    list = "Mobile"
                Case SuppressionType.Phone
                    list = "phone"
                Case Else
                    Console.WriteLine("Suppression entry type not recognized.  Suppression entry request denied.")
                    Return Nothing
            End Select


            Return connection.API_Call(Of List(Of String))("GET", "suppression/" + list, queryParameters, Nothing)

        End Function
        Public Shared Function Suppression_GetSuppressionListEntries_Response(filterCount As Integer, responseCount As Integer, suppressionEntries As List(Of String)) As Integer

            Return httpstatuscodes.OK
        End Function
        'Add suppression list entries
        Public Shared Function Suppression_AddSuppressionListEntries_Request(listtype As SuppressionType, entries As List(Of String)) As List(Of String)
            Dim entry As List(Of String) = New List(Of String)()
            entry = entries
            Dim mylist As String
            Select Case listtype
                Case SuppressionType.Email
                    mylist = "Email"
                Case SuppressionType.Fax
                    mylist = "Fax"
                Case SuppressionType.Mobile
                    mylist = "Mobile"
                Case SuppressionType.Phone
                    mylist = "phone"
                Case Else
                    Console.WriteLine("Suppression entry type not recognized.  Suppression entry request denied.")
                    Return Nothing
            End Select

            Return connection.API_Call(Of List(Of String))("POST", "suppression/" + mylist, Nothing, entry)

        End Function
        Public Shared Function Suppression_AddSuppressionListEntries_Response(suppressionEntries As List(Of String)) As Integer

            Return httpstatuscodes.OK
        End Function
        'Simplycast 360
        'Get SC 360 project
        Public Shared Function SC360_Get360Project_Request(projectID As Integer) As SC360Project
            Return connection.API_Call(Of SC360Project)("GET", "crossmarketer/" + projectID.ToString(), Nothing, Nothing)

        End Function
        Public Shared Function SC360_Get360Project_Response() As Integer

            Return httpstatuscodes.OK
        End Function
        'Get a connection element
        Public Shared Function SC360_Get360APIConnElem_Request(projectID As Integer, is_inbound As Boolean, connectionID As Integer) As SC360Connection
            Dim path As String
            If (is_inbound) Then
                path = "inbound"
            Else
                path = "outbound"

            End If
            Return connection.API_Call(Of SC360Connection)("GET", "crossmarketer/" + projectID + "/" + path + "/" + connectionID, Nothing, Nothing)


        End Function
        Public Shared Function SC360_Get360APIConnElem_Response(connection As SC360Connection) As Integer

            Return httpstatuscodes.OK
        End Function
        'push a contact
        Public Shared Sub SC360_PushContactTo360InboundAPIConnElem_Request(projectID As Integer, connectionID As Integer, listID As Integer, contactID As Integer)
            Dim mycontact As SC360Contact = New SC360Contact
            mycontact.pub_list = listID
            mycontact.pub_row = contactID
            connection.API_Call(Of Object)("POST", "crossmarketer/" + projectID.ToString() + "/inbound/" + connectionID.ToString(), Nothing, mycontact)
        End Sub
        Public Shared Function SC360_PushContactTo360InboundAPIConnElem_Response(list As Object, row As Object) As Integer

            Return httpstatuscodes.NO_CONTENT
        End Function
        'push a list
        Public Shared Sub SC360_PushListTo360InboundAPIConnElem_Request(projectID As Integer, connectionID As Integer, listID As Integer)
            Dim mylist As List(Of SC360Contact) = New List(Of SC360Contact)()
            For Each c As SC360Contact In mylist
                c.pub_list = listID
            Next

            connection.API_Call(Of Object)("POST", "crossmarketer/" + projectID + "/inbound/" + connectionID, Nothing, mylist)

        End Sub
        Public Shared Function SC360_PushListTo360InboundAPIConnElem_Response(list As Object, row As Object) As Integer

            Return httpstatuscodes.NO_CONTENT
        End Function

        'get multiple outbound contacts 
        Public Shared Function SC360_Get360OutboundAPIContacts_Request(projectID As Integer, connectionID As Integer, showProcessed As Boolean) As List(Of SC360Contact)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("showprocessed", Main.BoolToInt(showProcessed))
            Return connection.API_Call(Of List(Of SC360Contact))("GET", "crossmarketer/" + projectID + "/outbound/" + connectionID, queryParameters, Nothing)


        End Function
        'processing standard contacts rather than ones made by SC360
        Public Shared Function SC360_GetStandardOutboundAPIContacts_Request(projectID As Integer, connectionID As Integer, showProcessed As Boolean) As List(Of Contact)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("showprocessed", Main.BoolToInt(showProcessed))
            Return connection.API_Call(Of List(Of Contact))("GET", "crossmarketer/" + projectID.ToString() + "/outbound/" + connectionID.ToString(), queryParameters, Nothing)


        End Function
        Public Shared Function SC360_Get360OutboundAPIContacts_Response(connection As SC360Connection) As Integer

            Return httpstatuscodes.OK
        End Function
        'retrieve a simplycast 360 outbound contact
        Public Shared Function SC360_Retrieve360OutboundContact_Request(projectID As Integer, connectionID As Integer, contactID As Integer) As SC360Contact
            Return connection.API_Call(Of SC360Contact)("GET", "crossmarketer/" + projectID + "/outbound/" + connectionID + "/" + contactID, Nothing, Nothing)

        End Function
        Public Shared Function SC360_Retrieve360OutboundContact_Response(contact As SC360Contact) As Integer

            Return httpstatuscodes.OK
        End Function
        'Delete a simplycast 360 outbound contact
        Public Shared Sub SC360_Delete360OutboundContact_Request(projectID As Integer, connectionID As Integer, contactID As Integer)
            connection.API_Call(Of Object)("DELETE", "crossmarketer/" + projectID + "/outbound/" + connectionID + "/" + contactID, Nothing, Nothing)

        End Sub
        Public Shared Function SC360_Delete360OutboundContact_Response() As Integer

            Return httpstatuscodes.NO_CONTENT
        End Function
        'The following are a series of retrieve functions for Simplycast360, email, sms and forms. As such they will be almost identical, with the key exception being the type they affect. They are not represented in C#.
        Public Shared Function SC360_RetrieveMultiple360OrAutoresponderCampaigns_Request(offset As Integer, limit As Integer, query As String, sortfield As String, sortorder As String) As List(Of SC360Campaign)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of SC360Campaign))("GET", "autoresponder/", queryParameters, Nothing)

        End Function
        Public Shared Function SC360_RetrieveMultiple360OrAutoresponderCampaigns_Response(campaigns As List(Of SC360Campaign), filterCount As Integer, responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SC360_RetrieveSingle360OrAutoresponderCampaign_Request() As SC360Campaign


            Return connection.API_Call(Of SC360Campaign)("GET", "autoresponder/", Nothing, Nothing)
        End Function
        Public Shared Function SC360_RetrieveSingle360OrAutoresponderCampaign_Response(campaign As SC360Campaign) As Integer

            If (campaign Is Nothing) Then

                Return httpstatuscodes.NO_CONTENT

            Else

                Return httpstatuscodes.OK
            End If
        End Function
        Public Shared Function SC360_RetrieveMultipleElementsFrom360OrAutoresponderCampaign_Request(offset As Integer, limit As Integer, query As String, sortfield As String, sortorder As String) As List(Of SC360CampaignElement)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of SC360CampaignElement))("GET", "autoresponder/", queryParameters, Nothing)
        End Function
        Public Shared Function SC360_RetrieveMultipleElementsFrom360OrAutoresponderCampaign_Response(elements As List(Of SC360CampaignElement), filterCount As Integer, responseCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SC360_RetrieveSingleElementFrom360OrAutoresponderCampaign_Request() As SC360CampaignElement

            Return connection.API_Call(Of SC360CampaignElement)("GET", "autoresponder/", Nothing, Nothing)
        End Function
        Public Shared Function SC360_RetrieveSingleElementFrom360OrAutoresponderCampaign_Response(element As SC360CampaignElement) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SC360_RetrieveMultipleConnectionsFrom360OrAutoresponderCampaign_Request(offset As Integer, limit As Integer, query As String, sortfield As String, sortorder As String) As List(Of SC360CampaignConnection)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of SC360CampaignConnection))("GET", "autoresponder/", queryParameters, Nothing)
        End Function
        Public Shared Function SC360_RetrieveMultipleConnectionsFrom360OrAutoresponderCampaign_Response(connections As List(Of SC360CampaignConnection), filterCount As Integer, responseCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SC360_RetrieveSingleConnectionFrom360OrAutoresponderCampaign_Request() As SC360CampaignConnection
            Return connection.API_Call(Of SC360CampaignConnection)("GET", "autoresponder/", Nothing, Nothing)

        End Function
        Public Shared Function SC360_RetrieveSingleConnectionFrom360OrAutoresponderCampaign_Response(connection As SC360CampaignConnection) As Integer

            Return httpstatuscodes.OK
        End Function

        'SMS
        Public Shared Function SMS_RetrieveMultipleCampaigns_Request(offset As Integer, limit As Integer, query As String, sortfield As String, sortorder As String) As List(Of SMSCampaign)


            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of SMSCampaign))("GET", "sms/", queryParameters, Nothing)
        End Function
        Public Shared Function SMS_RetrieveMultipleCampaigns_Response(offset As Integer, limit As Integer, query As String, sortfield As String, sortorder As String) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SMS_RetrieveSingleCampaign_Request(comprehensive As Boolean, query As String) As SMSCampaign


            Return connection.API_Call(Of SMSCampaign)("GET", "sms/", Nothing, Nothing)
        End Function
        Public Shared Function SMS_RetrieveSingleCampaign_Response(campaign As SMSCampaign) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SMS_RetrieveCampaignSends_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As List(Of SMSCampaignSend)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of SMSCampaignSend))("GET", "sms/", queryParameters, Nothing)

        End Function
        Public Shared Function SMS_RetrieveCampaignSends_Response(filterCount As Integer, responseCount As Integer, sends As List(Of SMSCampaignSend), totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function SMS_RetrieveCampaignResponses_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As List(Of SMSCampaignResponse)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of SMSCampaignResponse))("GET", "sms/", queryParameters, Nothing)
        End Function
        Public Shared Function SMS_RetrieveCampaignResponses_Response(filterCount As Integer, responseCount As Integer, responses As List(Of SMSCampaignResponse), totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        'EMAIL
        Public Shared Function Email_RetrieveMultipleEmailCampaigns_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As List(Of EmailCampaign)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of EmailCampaign))("GET", "email/", queryParameters, Nothing)
        End Function
        Public Shared Function Email_RetrieveMultipleEmailCampaigns_Response(campaigns As List(Of EmailCampaign), filterCount As Integer, responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function Email_RetrieveSingleEmailCampaign_Request(comprehensive As Boolean) As EmailCampaign
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("comprehensive", Main.BoolToInt(comprehensive))
            Return connection.API_Call(Of EmailCampaign)("GET", "email/", queryParameters, Nothing)
        End Function
        Public Shared Function Email_RetrieveSingleEmailCampaign_Response(campaign As EmailCampaign) As Integer

            Return httpstatuscodes.OK
        End Function

        '   Public Shared Function Email_RetrieveEmailCampaignMessage_Request() As EmailCampaignInteraction

        '    Return connection.API_Call(Of EmailCampaignInteraction)("GET", "email/", Nothing, Nothing)
        '  End Function
        'Public Shared Function Email_RetrieveEmailCampaignMessage_Response(html As String, text As String) As Integer

        '  If (Nothing = html Or Nothing = text) Then
        '    Return httpstatuscodes.NO_CONTENT

        '   Else : Return httpstatuscodes.OK
        '   End If
        ' End Function
        Public Shared Function Email_RetrieveEmailCampaignSends_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As List(Of EmailCampaignSend)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of EmailCampaignSend))("GET", "email/", queryParameters, Nothing)
        End Function
        Public Shared Function Email_RetrieveEmailCampaignSends_Response(filterCount As Integer, responseCount As Integer, sends As List(Of EmailCampaignSend), totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function Email_RetrieveEmailCampaignInteractions_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String, unique As Boolean) As List(Of EmailCampaignInteraction)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of EmailCampaignInteraction))("GET", "email/", queryParameters, Nothing)
        End Function
        Public Shared Function Email_RetrieveEmailCampaignInteractions_Response(filterCount As Integer, interactions As List(Of EmailCampaignInteraction), responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function Email_RetrieveEmailCampaignTrackingLinks_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As EmailCampaignTrackingLink

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of EmailCampaignTrackingLink)("GET", "email/", queryParameters, Nothing)
        End Function
        Public Shared Function Email_RetrieveEmailCampaignTrackingLinks_Response(filterCount As Integer, responseCount As Integer, totalCount As Integer, trackingLinks As List(Of EmailCampaignTrackingLink)) As Integer

            Return httpstatuscodes.OK
        End Function

        '---Form & Survey---
        Public Shared Function FormAndSurvey_RetrieveMultipleFormCampaigns_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As List(Of FormCampaign)

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of FormCampaign))("GET", "email/", queryParameters, Nothing)
        End Function
        Public Shared Function FormAndSurvey_RetrieveMultipleFormCampaigns_Response(campaigns As List(Of FormCampaign), filterCount As Integer, responseCount As Integer, totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function FormAndSurvey_RetrieveSingleFormCampaign_Request(comprehensive As Boolean) As FormCampaign

            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("comprehensive", Main.BoolToInt(comprehensive))
            Return connection.API_Call(Of FormCampaign)("GET", "formandsurvey/", queryParameters, Nothing)
        End Function
        Public Shared Function FormAndSurvey_RetrieveSingleFormCampaign_Response(campaign As FormCampaign) As Integer

            Return httpstatuscodes.OK
        End Function
        ' Public Shared Function FormAndSurvey_RetrieveFormCampaignContent_Request() As Integer

        'Return httpstatuscodes.OK
        'End Function
        'Public Shared Function FormAndSurvey_RetrieveFormCampaignContent_Response(html As String) As Integer

        '   If (Nothing = html) Then
        '      Return httpstatuscodes.NO_CONTENT
        ' Else : Return httpstatuscodes.OK
        'End If

        'End Function
        Public Shared Function FormAndSurvey_RetrieveMultipleFormCampaignSubmissions_Request(limit As Integer, offset As Integer, query As String, sortfield As String, sortorder As String) As List(Of FormCampaignSubmission)
            Dim queryParameters As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            queryParameters.Add("offset", offset.ToString())
            queryParameters.Add("limit", limit.ToString())
            queryParameters.Add("sortfield", sortfield)
            queryParameters.Add("sortorder", sortorder)
            If (query.Length > 0) Then

                queryParameters.Add("query", query)

            End If
            Return connection.API_Call(Of List(Of FormCampaignSubmission))("GET", "formandsurvey/", queryParameters, Nothing)

        End Function
        Public Shared Function FormAndSurvey_RetrieveMultipleFormCampaignSubmissions_Response(filterCount As Integer, responseCount As Integer, submissions As List(Of FormCampaignSubmission), totalCount As Integer) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function FormAndSurvey_RetrieveSingleFormCampaignSubmission_Request() As FormCampaignSubmission

            Return connection.API_Call(Of FormCampaignSubmission)("GET", "formandsurvey/", Nothing, Nothing)
        End Function
        Public Shared Function FormAndSurvey_RetrieveSingleFormCampaignSubmission_Response(submission As FormCampaignSubmission) As Integer

            Return httpstatuscodes.OK
        End Function
        Public Shared Function FormAndSurvey_RetrieveCurrentSchemaForFormCampaign_Request() As FormCampaignSchema

            Return connection.API_Call(Of FormCampaignSchema)("GET", "formandsurvey/", Nothing, Nothing)
        End Function
        Public Shared Function FormAndSurvey_RetrieveCurrentSchemaForFormCampaign_Response(schema As FormCampaignSchema) As Integer

            If (schema Is Nothing) Then
                Return httpstatuscodes.NO_CONTENT
            Else : Return httpstatuscodes.OK
            End If
        End Function



















    End Class
End Namespace
