Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Module module1


    Sub Main()
            Dim sampleAPI As SimplyCastWrapper.SimplyCastAPI = New SimplyCastWrapper.SimplyCastAPI()

        Dim myexample As SimplyCastWrapper.Examples = New SimplyCastWrapper.Examples(sampleAPI, True)
        Dim conn As SimplyCastWrapper.SimplyCastAPIConnector = New SimplyCastWrapper.SimplyCastAPIConnector("public", "secret")
        Dim core As SimplyCastWrapper.Main = New SimplyCastWrapper.Main(conn)
        SimplyCastWrapper.Main.pub_exampletest = True 'the only way we can set exampletest to true.
        myexample.executetest(SimplyCastWrapper.Main.pub_exampletest)
    End Sub


End Module
