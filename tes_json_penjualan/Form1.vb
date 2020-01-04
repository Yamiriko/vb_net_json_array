Imports System.Web.Script.Serialization
Imports System.Net
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class Form1
    'Telegram : https://t.me/Yamiriko
    'Facebook : https://www.facebook.com/Jean.Riko.K.P
    'WA/Call/SMS/Line : 0823-8694-4596
    'Email : riko.kurniawan18@yahoo.co.id
    'Github : https://github.com/Yamiriko
    Private Sub baca_json()
        Dim s As String
        Try
            Dim rawresp As String = "{""id"":174543706,""first_name"":""Hamed"",""last_name"":""Ap"",""username"":""hamed_ap"",""type"":""private""}"

            Dim jss As New JavaScriptSerializer()
            Dim dict As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(rawresp)

            s = dict("id")
            MsgBox(s)
        Catch ex As Exception
            MsgBox("Error bro = " + ex.Message)
        End Try
    End Sub

    Private Sub baca_jsonnya(rawresp)
        Try
            Dim token As JToken
            Dim Name, City, Country
            Dim readingJson = Newtonsoft.Json.Linq.JObject.Parse(rawresp)
            For Each value As Object In readingJson("records")
                token = JObject.Parse(value.ToString())
                Name = token.SelectToken("Name")
                City = token.SelectToken("City")
                Country = token.SelectToken("Country")
                With ListView1
                    .View = View.Details
                    Dim otherItems As String() = {City, Country}
                    .Items.Add(Name).SubItems.AddRange(otherItems)
                End With
            Next value
        Catch ex As Exception
            MsgBox("Error bro = " + ex.Message)
        End Try
    End Sub

    Private Function SendRequest(uri As String, jsonDataBytes As Byte(), contentType As String, method As String) As String
        Dim response As String
        Dim request As WebRequest

        Try
            request = WebRequest.Create(uri)
            request.ContentLength = jsonDataBytes.Length
            request.ContentType = contentType
            request.Method = method

            Using requestStream = request.GetRequestStream
                requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
                requestStream.Close()

                Using responseStream = request.GetResponse.GetResponseStream
                    Using reader As New StreamReader(responseStream)
                        response = reader.ReadToEnd()
                    End Using
                End Using
            End Using

            Return response
        Catch ex As Exception
            MsgBox("Error bro = " + ex.Message)
        End Try
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim jsonSring = "" 'Isikan jika datanya POST, jika GET kosongkan saja
        Dim urlnya = "https://www.w3schools.com/angular/customers.php"
        Dim datanya = Encoding.UTF8.GetBytes(jsonSring)
        Dim result_post = SendRequest(urlnya, datanya, "application/json", "POST")
        RichTextBox1.Text = result_post
        Label1.Text = "JSON Data dari : " + urlnya
        baca_jsonnya(result_post)
    End Sub
End Class


