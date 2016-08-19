Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace SimplyCastWrapper
    Public Class Examples
        Private api As SimplyCastAPI

        Public Sub New(exampleAPI As SimplyCastAPI)
            api = exampleAPI
        End Sub

        Public Sub examples_listmanagement()
            'first, create the list
            Dim myList As ContactList = Main.ContactList_Create_Request("My New List")
            Console.WriteLine("Created new list")

            'second, retrieve the list by ID

            myList = Main.ContactList_Retrieve_Request(myList.Pub_ID) 'myList is used as the local variable name instead of List, because List is reserved in VB and VB is case insensitive. 
            Console.WriteLine("Retrieved list " + myList.Pub_ID + " by ID")
            'third, retrieve a number of lists, by name, to match the name of the new list.

            Dim ListCollection As List(Of ContactList) = Main.ContactList_RetrieveMultiple_Request(100, "My New List", 0)

            'Retrieve the same list again, this time by name. Since lists can 
            'possibly share a name, error conditions are avoided by returning 
            'a collection of lists instead of a single list.

            If ListCollection.Count > 0 Then 'are there any hits in this list search?
                'make myList the first of these
                myList = ListCollection.Item(0)
            End If


            Console.WriteLine("Retrieved list " + myList.Pub_ID + " by name")

            'Now work with a list of columns.

            ' '//Create a new contact and add it to our list. You can also add 
            ' '//lists upon creation of a contact as the second parameter 
            ' '//(example in the contact management example).

            Dim columns As List(Of Column) = Main.Columns_Get_Request()
            ''//Fields are added by column ID, not by the name of the column. You
            ''//can get the ID of a column by calling GetByName() on a column 
            ''//collection.
            Dim fields As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            '   '//Fields are added by column ID, not by the name of the column. You
            ''//can get the ID of a column by calling GetByName() on a column 
            ''//collection.
            Dim tempcolumn As Column = New Column()
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

            Main.ContactList_AddToList_Request(myList.Pub_ID, New List(Of Integer)(myContact.pub_ID), False)

            Console.WriteLine("Added contact " + myContact.pub_ID + " to list " + myList.Pub_ID)

            'now, rename the list

            myList = Main.ContactList_Modify_Request(myList.Pub_ID, "New list name")
            Console.WriteLine("Renamed list to '" + myList.name + "'")

            'Finally, delete the list. Contacts in the list won't be deleted, just the list that refers to them.

            Main.ContactList_Delete_Request(myList.Pub_ID)


            Console.WriteLine("Deleted list.")

            Console.Write("Press enter to continue...")
            Console.Read()
        End Sub


    End Class
End Namespace

