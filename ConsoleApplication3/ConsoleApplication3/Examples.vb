Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace SimplyCastWrapper
    Public Class Examples
        Private api As SimplyCastAPI
        Dim testing As Boolean = False
        Public Sub executetest(ByRef is_example As Boolean) 'This is a test of the various features of simplycast. ByRef means it affects the main variable passed as a parameter as well.
            Dim contactID As Integer = 1
            Dim projectID As Integer = 1
            Dim inboundconnectionID As Integer = 1
            Dim outboundconnectionID As Integer = 1
            testing = is_example 'This is the only way 'testing' can be set to true.

            Me.examples_listmanagement()
            Me.ContactManagementExample()
            Me.ColumnManagementExample()
            Me.SimplyCast360Example(contactID, projectID, inboundconnectionID, outboundconnectionID)
            Me.MetadataColumnManagementExample()
        End Sub
        Public Sub New(exampleAPI As SimplyCastAPI, exampletest As Boolean)
            api = exampleAPI
            testing = exampletest
        End Sub

        Public Sub examples_listmanagement()
            'first, create the list
            Dim myList As ContactList = Main.ContactList_Create_Request("My New List")
            Console.WriteLine("Created new list")

            'second, retrieve the list by ID

            myList = Main.ContactList_Retrieve_Request(myList.Pub_ID) 'myList is used as the local variable name instead of List, because List is reserved in VB and VB is case insensitive. 
            Console.WriteLine("Retrieved list " + myList.Pub_ID.ToString() + " by ID")
            'third, retrieve a number of lists, by name, to match the name of the new list.

            Dim ListCollection As List(Of ContactList) = Main.ContactList_RetrieveMultiple_Request(100, "My New List", 0)

            'Retrieve the same list again, this time by name. Since lists can 
            'possibly share a name, error conditions are avoided by returning 
            'a collection of lists instead of a single list.

            If ListCollection.Count > 0 Then 'are there any hits in this list search?
                'make myList the first of these
                myList = ListCollection.Item(0)
            End If


            Console.WriteLine("Retrieved list " + myList.Pub_ID.ToString() + " by name")

            'Now work with a list of columns.

            ' ''' //Create a new contact and add it to our list. You can also add 
            ' ''' //lists upon creation of a contact as the second parameter 
            ' ''' //(example in the contact management example).

            Dim columns As List(Of Column) = Main.Columns_Get_Request()
            ' ''' //Fields are added by column ID, not by the name of the column. You
            ' ''' //can get the ID of a column by calling GetByName() on a column 
            ' ''' //collection.
            Dim fields As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            '   ''' //Fields are added by column ID, not by the name of the column. You
            ' ''' //can get the ID of a column by calling GetByName() on a column 
            ' ''' //collection.
            Dim tempcolumn As Column 'This needs to be Nothing initially, as a sanity check.
            For Each e As Column In columns

                If e.name = "email" Then

                    tempcolumn = e
                End If

            Next
            If tempcolumn IsNot Nothing Then
                fields.Add(tempcolumn.name, "test@example.com") 'Will deal with this later, Aug 18
            Else
                Console.WriteLine("No column with email as a field!")
            End If

            Dim myContact As Contact = Main.Contacts_Create_Request(fields)

            Main.ContactList_AddToList_Request(myList.Pub_ID, New List(Of Integer)({myContact.pub_ID}), False)

            Console.WriteLine("Added contact " + myContact.pub_ID.ToString() + " to list " + myList.Pub_ID.ToString())

            'now, rename the list

            myList = Main.ContactList_Modify_Request(myList.Pub_ID, "New list name")
            Console.WriteLine("Renamed list to '" + myList.name + "'")

            'Finally, delete the list. Contacts in the list won't be deleted, just the list that refers to them.

            Main.ContactList_Delete_Request(myList.Pub_ID)


            Console.WriteLine("Deleted list.")

            Console.Write("Press enter to continue...")
            Console.Read()
        End Sub
        'Managing contacts.
        Public Sub ContactManagementExample()

            Dim myList As ContactList = New ContactList()

            '' //Create a testing list.
            Try

                myList = Main.ContactList_Create_Request("test list")
            Catch ex As APIException

                myList = Main.ContactList_GetByName_Request("test list").Item(0)


            End Try


            Console.WriteLine("Created test list " + myList.Pub_ID.ToString())

            '' //Retrieve our column mapping. We can use this to find out the
            '' //column IDs we'll need when setting contact fields. 
            Dim columns As List(Of Column) = Main.Columns_Get_Request()

            '' //Build up our contact data. Notice that we're using the column ID
            '' //as the field key, *Not* the column name.
            Dim fields As Dictionary(Of String, String) = New Dictionary(Of String, String)()

            If Main.Columns_ContactManager_RetrieveByName_Request("email", columns) IsNot Nothing Then
                fields.Add(Main.Columns_ContactManager_RetrieveByName_Request("email", columns).pub_id, "test@example.com")
            Else

                Console.WriteLine("There is no email column available!")
            End If
            If Main.Columns_ContactManager_RetrieveByName_Request("name", columns) IsNot Nothing Then
                fields.Add(Main.Columns_ContactManager_RetrieveByName_Request("name", columns).pub_id, "Test Contact")
            Else

                Console.WriteLine("There is no name column available!")
            End If

            '' //Create a new contact, and assign them to the test list at the
            '' //same time.
            Dim myContact As Contact = Main.Contacts_Create_Request(fields, New List(Of Integer)())

            Console.WriteLine("Created contact " + myContact.pub_ID.ToString())

            '' //Attempt an 'upsert operation' on the created contact. The name
            '' //should be updated to the given value.
            Console.WriteLine("Attempting upsert (should update contact " + myContact.pub_ID.ToString() + ").")
            If Main.Columns_ContactManager_RetrieveByName_Request("name", columns) IsNot Nothing Then
                fields.Item(Main.Columns_ContactManager_RetrieveByName_Request("name", columns).pub_id) = "Updated Test Contact"
            Else
                Console.WriteLine("There is no name column available!")
            End If

            Dim contacts As List(Of Contact)
            If (Main.Columns_ContactManager_RetrieveByName_Request("email", columns)) IsNot Nothing Then

                contacts = Main.Contacts_UpdateMultiple_Request(Main.Columns_ContactManager_RetrieveByName_Request("email", columns).pub_id, fields)

                For Each c As Contact In contacts

                    Console.WriteLine("Updated contact " + c.pub_ID + " (email address: " + c.pub_Fields.Find(Function(x) x.pub_name.Contains("email")).value + ") to have name '" + c.pub_Fields.Find(Function(x) x.pub_name.Contains("email")).value + "'.")


                Next

                '' //Run a query on the contact database to retrieve our contact. 
                '' //A contrived example, but an example nonetheless. The syntax 
                '' //is detailed in the online documentation, but basically looks
                '' //something like: '`colID` = "value" AND `col2ID` = "other value"'
            Else
                Console.WriteLine("There is no email column available!")
            End If

            Dim query As String
            If Main.Columns_ContactManager_RetrieveByName_Request("email", columns) IsNot Nothing Then
                query = "`" + (Main.Columns_ContactManager_RetrieveByName_Request("email", columns).pub_id.ToString() + "` = ""test@example.com"" AND `" + Main.Columns_ContactManager_RetrieveByName_Request("name", columns).pub_id.ToString() + "` = ""Updated Test Contact""")
            Else
                Console.WriteLine("There is no email column available! Running blank query.")
                query = ""
            End If
            Console.WriteLine("Executing query: " + query)
            Dim queryResult As List(Of Contact) = Main.Contacts_GetGeneral_Request(query)

            If (queryResult.Count > 0) Then

                Console.WriteLine("Ran search query and found " + queryResult.Count + " contacts")

            Else

                Console.WriteLine("Ran search query and found no contacts.")
            End If

            '' //Retrieve a field by its name, and get the value from it.
            'Dim email As String = myContact.GetFieldsByName("email")[0].Value
            Console.WriteLine("Retrieving the email field and its value.")
            Dim email As String
            If (myContact.pub_Fields IsNot Nothing) Then
                email = myContact.pub_Fields.Find(Function(x) x.pub_name.Contains("email")).value
                Console.WriteLine("\tContact email: " + email)
            Else
                Console.WriteLine("There are no fields to run an email check on!")
            End If


            '' //Output each list that the contact belongs to (should only be our
            '' //test list).
            If myContact.pub_lists IsNot Nothing Then
                For Each l As ContactList In myContact.pub_lists

                    Console.WriteLine("\tContact belongs to list " + l.Pub_ID)
                Next
            Else
                Console.WriteLine("Contact does not belong to any lists!")
            End If



            '' //Change the contact's phone number.
            Dim updateFields As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            If Main.Columns_ContactManager_RetrieveByName_Request("email", columns) IsNot Nothing Then

                updateFields.Add(Main.Columns_ContactManager_RetrieveByName_Request("phone", columns).pub_id, "15555551234")
                myContact = Main.Contacts_Update_Request(myContact.pub_ID, updateFields)

                Console.WriteLine("Updated phone number: " + myContact.pub_Fields.Find(Function(x) x.pub_name.Equals("phone")).value)
            Else
                Console.WriteLine("There is no phone column available!")
            End If



            '' //Add tags to a contact.
            Dim metadataColumns As List(Of Metadata) = Main.Metadata_Column_RetrieveAll_Request()
            Dim metadataFieldID As String
            If metadataColumns.Find(Function(x) x.name.Equals("Tags")) IsNot Nothing Then
                metadataFieldID = metadataColumns.Find(Function(x) x.name.Equals("Tags")).pub_id

                Main.Metadata_Column_Modify_Request(myContact.pub_ID, metadataFieldID, New List(Of String)({"tag 1", "tag 2"}))

                Console.WriteLine("Updated metadata tags")

                '' //Get metadata tags and display them.
                Dim metadataField As Metadata = Main.Contacts_RetrieveMetadataField_Request(myContact.pub_ID, "Tags").Item(0)
                Console.WriteLine("Retrieved metadata tags.")

                For Each value As String In metadataField.value
                    Console.WriteLine("\tMetadata tag: " + value)
                Next
            Else
                Console.WriteLine("There is no 'tags' metadata field available!")
            End If

            '' //Delete our test list
            Main.ContactList_Delete_Request(myList.Pub_ID)

            Console.WriteLine("Deleted test list")

            '' //Refresh the contact and see if it still belongs to the list.
            myContact = Main.Contacts_GetByID_Request(myContact.pub_ID)
            If myContact.pub_lists IsNot Nothing Then
                Console.WriteLine("Contact belongs to " + myContact.pub_lists.Count.ToString() + " lists")
            Else
                Console.WriteLine("Contact does not belong to any lists.")
            End If


            Main.Contacts_DeleteGeneral_Request(myContact.pub_ID)

            Console.WriteLine("Deleted contact.")

            Console.Write("Press enter to continue...")
            Console.Read()


        End Sub
        'column management
        Public Sub ColumnManagementExample()

            Dim myColumns As List(Of Column) = Main.Columns_Get_Request()
            Dim myColumn As Column

            ' //Create the test column if it doesn't exist yet, or load it if it
            ' //does.
            If (Main.Columns_ContactManager_RetrieveByName_Request("test column", myColumns) Is Nothing) Then

                myColumn = Main.Columns_CreateNew(New List(Of String), "test column", "String")
                Console.WriteLine("Created column " + myColumn.pub_id.ToString())

            Else

                myColumn = Main.Columns_ContactManager_RetrieveByName_Request("test column", myColumns)
                Console.WriteLine("Retrieved column " + myColumn.pub_id)
            End If

            ' //Replace all merge tags with %%MERGE TAG%%. Merge tags replace 
            ' //into content with field data.
            Main.Columns_Mergetags_Request(myColumn.pub_id, New List(Of String)({"%%MERGE TAG%%"}), False)

            Console.WriteLine("Updated merge tags.")

            ' //Reload the column.
            myColumn = Main.Columns_GetSingle_Request(myColumn.pub_id)

            Console.WriteLine("Reloaded column.")

            ' //Display each merge tag.
            If myColumn.mergeTags IsNot Nothing Then
                For Each mergeTag As String In myColumn.mergeTags

                    Console.WriteLine("\tMerge tag: " + mergeTag)
                Next
            Else
                Console.WriteLine("No merge tags available.")
            End If

            Console.Write("Press enter to continue...")
            Console.Read()
        End Sub

        ''' <summary>
        ''' Metadata column management examples.
        ''' </summary>
        Public Sub MetadataColumnManagementExample()

            ' //Create a New contact to test with.
            Dim columns As List(Of Column) = Main.Columns_Get_Request()

            Dim fields As Dictionary(Of String, String) = New Dictionary(Of String, String)()

            If Main.Columns_ContactManager_RetrieveByName_Request("email", columns) IsNot Nothing Then
                fields.Add(Main.Columns_ContactManager_RetrieveByName_Request("email", columns).pub_id, "test@example.com")
            Else

                Console.WriteLine("There is no email column available!")
            End If
            If Main.Columns_ContactManager_RetrieveByName_Request("name", columns) IsNot Nothing Then
                fields.Add(Main.Columns_ContactManager_RetrieveByName_Request("name", columns).pub_id, "Test Contact")
            Else

                Console.WriteLine("There is no name column available!")
            End If


            Dim mycontact As Contact = Main.Contacts_Create_Request(fields)

            Console.WriteLine("Created test contact " + mycontact.pub_ID.ToString())

            Dim metadataColumns As List(Of Metadata) = Main.Metadata_Column_RetrieveAll_Request()
            Console.WriteLine("Retrieved all metadata columns.")

            ' //Add new metadata columns.
            If (metadataColumns.Find(Function(x) x.name.Equals("Test Sum Column")) Is Nothing) Then

                Main.Metadata_Column_Create_Request("Test Single Column", Metadata.metadata_types.singlenumber)
            End If
            If (metadataColumns.Find(Function(x) x.name.Equals("Test Multi Column")) Is Nothing) Then

                Main.Metadata_Column_Create_Request("Test Multi Column", Metadata.metadata_types.multistring)
            End If
            If (metadataColumns.Find(Function(x) x.name.Equals("Test Sum Column")) Is Nothing) Then

                Main.Metadata_Column_Create_Request("Test Sum Column", Metadata.metadata_types.sumnumber)
            End If

            Console.WriteLine("Creating and processing metadata test columns.")

            ' //Update our single number field to have the value 2.
            Try
                Dim singleColumn As Metadata

                singleColumn = Main.Metadata_Column_Modify_Request(mycontact.pub_ID, metadataColumns.Find(Function(x) x.name.Equals("Test Single Column")).pub_id, New List(Of String)({"2"}))

                Console.WriteLine("Updated single column field with one integer.")
                Console.WriteLine("Value: " + singleColumn.value)

                ' //Update our multi-string field to have two values.
                Dim multiColumn As Metadata =
                    Main.Metadata_Column_Modify_Request(mycontact.pub_ID, metadataColumns.Find(Function(x) x.name.Equals("Test Multi Column")).pub_id, New List(Of String)({"test multi value 1", "test multi value 2"}))

                Console.WriteLine("Updated multi-valued field with two strings.")
                For Each value As String In multiColumn.value

                    Console.WriteLine("Value: " + value)
                Next

                ' //"Sum number" fields are incrementers. Updating the field with a
                ' //number will actually add to the existing value, rather than 
                ' //replacing it.
                Dim sumColumn As Metadata = New Metadata()
                For i As Integer = 0 To 3 Step 1

                    sumColumn = Main.Metadata_Column_Modify_Request(mycontact.pub_ID, metadataColumns.Find(Function(x) x.name.Equals("Test Sum Column")).pub_id, New List(Of String)({"1"}))
                    Console.WriteLine("Incremented sum number field by 1")
                    Console.WriteLine("Value: " + sumColumn.value)
                Next

                Main.Metadata_Column_Delete_Request(singleColumn.pub_id)
                Main.Metadata_Column_Delete_Request(multiColumn.pub_id)
                Main.Metadata_Column_Delete_Request(sumColumn.pub_id)

                Console.WriteLine("Removed test metadata columns.")

                Main.Contacts_DeleteGeneral_Request(mycontact.pub_ID)

                Console.WriteLine("Removed test contact " + mycontact.pub_ID)
                Console.Write("Metadata column creation and processing successful!")
            Catch ex As Exception
                Console.Write("Metadata column creation and processing unsuccessful!")
            End Try


            Console.Write("Press enter to continue...")
            Console.Read()
        End Sub

        ''' <summary>
        ''' 360 Examples. This example requires you to create a test 360 
        ''' project that contains an inbound And outbound 360 API connection
        ''' node, And the ID of a contact to test with.
        ''' 
        ''' NOTE that this will remove things from outbound nodes. Make sure
        ''' your project you test with Is indeed a testing project.
        ''' </summary>
        Public Sub SimplyCast360Example(contactID As Integer, projectID As Integer, inboundConnectionID As Integer, outboundConnectionID As Integer)

            Dim project As SC360Project = Main.SC360_Get360Project_Request(projectID)

            Console.WriteLine("Loaded 360 project '" + project.pub_name + "'")

            Dim mycontact As Contact = Main.Contacts_GetByID_Request(contactID)

            Console.WriteLine("Loaded contact " + mycontact.pub_ID.ToString())

            Dim listID As Integer = 0
            If mycontact.pub_lists IsNot Nothing Then
                If mycontact.pub_lists.Count = 0 Then

                    Throw New Exception("The test contact must belong to at least one list.")
                End If
                listID = mycontact.pub_lists.Item(0).Pub_ID
            Else


            End If



            Main.SC360_PushContactTo360InboundAPIConnElem_Request(projectID, inboundConnectionID, listID, contactID)

            Console.WriteLine("Pushed contact to inbound node.")

            Console.WriteLine("Reading from outbound node to see if any contacts have arrived")

            Dim contacts As List(Of Contact) = Main.SC360_GetStandardOutboundAPIContacts_Request(projectID, outboundConnectionID, True)

            ' //Iterate through each contact And remove them from the outbound
            ' //node. 
            If (contacts IsNot Nothing And contacts.Count > 0) Then

                For Each c As Contact In contacts

                    Console.WriteLine("Found contact " + c.pub_ID)

                    Main.SC360_Delete360OutboundContact_Request(projectID, outboundConnectionID, c.pub_ID)
                    Console.WriteLine("Removed contact " + c.pub_ID + " (360 ID " + c.pub_ID + ") from outbound node.")
                Next

            Else

                Console.WriteLine("No contacts in outbound node.")

            End If
            Console.Write("Press enter to continue...")
            Console.Read()
        End Sub
    End Class
End Namespace

