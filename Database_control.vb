Imports MySql.Data.MySqlClient


Public Class Database_control

    Private database_conn As New MySqlConnection("server=localhost; uid=root; database=WaterClients;Integrated Security=True")

    Private database_cmd As MySqlCommand

    Public database_adapter As MySqlDataAdapter
    Public database_table As DataTable
    Public ds As New DataSet



    Private Sub database_query()
        ' Dim client_name = txtlogin_name.Text
        Dim row As DataRow

        Try
            database_conn.Open()
            MessageBox.Show("connected to the database")
            ' Dim strsql_select As String = "SELECT*FROM clients WHERE  client_name='" & client_name & "'AND password ='" & txtlogin_pass.Text & "'"
            'database_cmd = New MySqlCommand(strsql_select, database_conn)


            database_table = New DataTable()
            database_adapter = New MySqlDataAdapter(database_cmd)
            database_table.Clear()
            database_adapter.Fill(ds)

            database_adapter.Fill(database_table)
            If database_table.Rows.Count = Nothing Then
                MsgBox("Invalid usr name or password or not yet registered as a member")
            Else

                row = database_table.Select()(0)


                'cmpClient_type.SelectedText = row("client_type")
                'lblclientType.Text = row("client_type")
                MsgBox("client type is" & row("client_type"))



            End If
        Catch ex As Exception
            MsgBox("Execution error occurred " & vbNewLine & ex.Message())

        Finally
            If database_conn.State = ConnectionState.Open Then
                database_conn.Close()
            End If
        End Try




    End Sub





End Class
