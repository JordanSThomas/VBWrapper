Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization
Imports System.IO
Imports System.Net

Namespace SimplyCastWrapper
    Public Class SimplyCastAPIConnector
        ' SimplyCast REST API connector.
        'See https://app.simplycast.com/?q=api/reference

        'Public Static static_BytesToHex As String 'Methods can't be declared static in VB, so a distinct variable with static_ as a prefix is used where a static value with the same value as the relevant method would be needed.
        Private PublicKey As String 'Public Key, identifies the user.
        Private SecretKey As String 'Secret Key, used to generate a signature
        Private apiURL As String = "https://api.simplycast.com/" 'API URL without the resource endpoint

        Public Property Pub_apiURL As String 'Pub_nn is used because VB is not case sensitive and will not recognize capitalized versions of type names as distinct types.
            Get
                Return Me.apiURL
            End Get
            Set(value As String)
                Me.apiURL = value
            End Set
        End Property

        Public Sub New(Param_PublicKey As String, Param_SecretKey As String)
            Me.PublicKey = Param_PublicKey
            Me.SecretKey = Param_SecretKey
        End Sub

        Private Function _Serialize(serializableobject As Object) As String
            Dim serialized As String = ""
            Dim ns As XmlSerializerNamespaces = New XmlSerializerNamespaces()
            ns.Add("", "")
            Dim serializer As XmlSerializer = New XmlSerializer(serializableobject.GetType())
            Using memStream As MemoryStream = New MemoryStream()


                Using writer As StreamWriter = New StreamWriter(memStream, Encoding.UTF8)

                    serializer.Serialize(writer, serializableobject, ns)
                    serialized = Encoding.UTF8.GetString(memStream.ToArray())
                End Using
            End Using
            Return serialized

        End Function

        Private Function _Deserialize(Of T)(xml As String) As T
            Dim obj As T = Activator.CreateInstance(GetType(T)) 'VB's default equivalent?
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(T)) 'What would VB use for typeof?
            Using reader As TextReader = New StringReader(xml)

                obj = CType(serializer.Deserialize(reader), T)
            End Using
            Return obj

        End Function

        'Note: Date is nota valid identifier name in VB so param_ is used as a prefix.
        Private Function _GenerateAuthHeader(method As String, resource As String, param_date As String, requestBodyHash As String)
            Dim signature As String = (method + Environment.NewLine + param_date + Environment.NewLine + resource + Environment.NewLine + requestBodyHash)
            Dim hmac As System.Security.Cryptography.HMACSHA1 = New System.Security.Cryptography.HMACSHA1()
            hmac.Key = Encoding.UTF8.GetBytes(Me.SecretKey)
            Dim authStr As String = Me.PublicKey + ":" + SimplyCastAPIConnector.BytesToHex(hmac.ComputeHash(Encoding.UTF8.GetBytes(signature)))
            Return "Authorization: HMAC " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes(authStr))
        End Function


        Public Shared Function BytesToHex(bytes As Byte()) As String
            Dim str As String = ""
            For i As Integer = 0 To bytes.Length - 1 Step 1
                str = str + bytes(i).ToString("X2")
            Next i
            Return str

        End Function

        'Note: Call is not a valid identifier name in VB, so API_ is used as a prefix.
        Public Function API_Call(Of T)(method As String, resource As String, QueryParameters As Dictionary(Of String, String), requestBody As Object) As T
            Dim requestBodyString As String = ""
            If requestBody Is Nothing Then
                requestBodyString = ""
            Else
                requestBodyString = Me._Serialize(requestBody)


            End If
            Dim Call_Date As String = DateTime.UtcNow.ToString("r")
            Dim requestBodyHash As String = ""
            If requestBodyString.Length > 0 Then
                requestBodyHash = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(SimplyCastAPIConnector.BytesToHex(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(requestBodyString.Trim())))))
            End If
            Dim authHeader As String = Me._GenerateAuthHeader(method, resource, Call_Date, requestBodyHash)
            Dim url As String = Me.apiURL.Trim("/") + "/" + resource.Trim("/")
            If (QueryParameters IsNot Nothing) Then
                If QueryParameters.Count > 0 Then

                    url = url + "?"
                    For Each q As KeyValuePair(Of String, String) In QueryParameters
                        url = url + System.Uri.EscapeDataString(q.Key) + "=" + System.Uri.EscapeDataString(q.Value) + "&"
                    Next
                    url = url.TrimEnd("&")
                End If
            End If
            Dim webHandle As HttpWebRequest = WebRequest.Create(url)
            webHandle.Method = method
            webHandle.Headers.Add("X-Date", Call_Date)
            webHandle.Accept = "application/xml"
            webHandle.Headers.Add(authHeader)
            If (requestBodyString.Length > 0) Then
                Dim contentlength As Integer = Encoding.UTF8.GetBytes(requestBodyString).Length
                webHandle.ContentType = "application/xml"
                webHandle.ContentLength = contentlength
                webHandle.Headers.Add("Content-MD5", requestBodyHash)
                Dim requestStream As Stream = webHandle.GetRequestStream()
                requestStream.Write(Encoding.UTF8.GetBytes(requestBodyString), 0, contentlength)
                requestStream.Close()
            End If

            Dim webResponse As HttpWebResponse
            Try
                webResponse = CType(webHandle.GetResponse(), HttpWebResponse)
            Catch ex As WebException
                webResponse = ex.Response
            End Try

            Dim receiveStream As Stream = webResponse.GetResponseStream()
            Dim readStream As StreamReader = New StreamReader(receiveStream, Encoding.UTF8)
            Dim responseData As String = readStream.ReadToEnd()
            readStream.Close()
            receiveStream.Close()

            If Convert.ToInt32(webResponse.StatusCode >= 400) Then 'Errors related to the client
                Dim myException As APIException
                Try
                    Dim ex_error As Error_Message = _Deserialize(Of Error_Message)(responseData) 'parse the error message
                    myException = New APIException(ex_error.Message) 'ready an exception with the error message

                    myException.StatusCode = webResponse.StatusCode
                    myException.StatusDescription = webResponse.StatusDescription

                Catch ex As Exception
                    'if even the error handler throws an error.
                    myException = New APIException("An error condition occurred from the API, but could not be deserialized.", ex)
                    myException.StatusCode = webResponse.StatusCode
                    myException.StatusDescription = webResponse.StatusDescription




                End Try
                If Not Main.pub_exampletest Then
                    Throw myException 'Provide the message to the user.
                Else
                    Console.WriteLine(myException.ToString())

                End If

            ElseIf webResponse.StatusCode = HttpStatusCode.NoContent Then
                Return Activator.CreateInstance(GetType(T))


            End If
            If Not Main.pub_exampletest Then
                If GetType(T) Is GetType(String) Then
                    Return CType(Convert.ChangeType(responseData, GetType(T)), T)

                Else
                    Return CType(Me._Deserialize(Of T)(responseData), T)
                End If
            Else
                Return Activator.CreateInstance(GetType(T)) 'if this is the example test, return No Content
            End If


            'Bypass the serializer if the caller wants to handle it itself.

        End Function

    End Class

End Namespace
