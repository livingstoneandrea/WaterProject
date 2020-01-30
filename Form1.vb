
Imports MySql.Data.MySqlClient

Public Class login_form
    Private database_conn As New MySqlConnection("server=localhost; uid=root; database=WaterClients;Integrated Security=True")

    Private database_cmd As MySqlCommand

    Public database_adapter As MySqlDataAdapter
    Public database_table As DataTable
    Public ds As New DataSet
    

    Private Sub btnlogin_pressed_Click(sender As Object, e As EventArgs) Handles btnlogin_pressed.Click
        Dim client_name = txtlogin_name.Text
        Dim row As DataRow

        Try
            database_conn.Open()
            MessageBox.Show("connected to the database")
            Dim strsql_select As String = "SELECT*FROM clients WHERE  client_name='" & client_name & "'AND password ='" & txtlogin_pass.Text & "'"
            database_cmd = New MySqlCommand(strsql_select, database_conn)


            database_table = New DataTable()
            database_adapter = New MySqlDataAdapter(database_cmd)
            database_table.Clear()
            database_adapter.Fill(ds)

            database_adapter.Fill(database_table)
            If database_table.Rows.Count = Nothing Then
                MsgBox("Invalid usr name or password or not yet registered as a member")
            Else
                Me.Hide()
                Bill_calcfrm.Show()

                row = database_table.Select()(0)

                ' Bill_calcfrm.cmbclient_type.SelectedItem = row("client_type").ToString

                Bill_calcfrm.lblclientType.Text = row("client_type").ToString
                Bill_calcfrm.lblclient_name.Text = row("client_name").ToString
                Bill_calcfrm.lblConnection_type.Text = "==>" & row("client_type").ToString
                Bill_calcfrm.lblClient.Text = row("client_type").ToString


                ' MsgBox("client type  retrieved  from database is:" & row("client_type"))


            End If
        Catch ex As Exception
            MsgBox("Execution error occurred " & vbNewLine & ex.Message())

        Finally
            If database_conn.State = ConnectionState.Open Then
                database_conn.Close()
            End If
        End Try

    End Sub

    Private Sub pic_closebtn_Click(sender As Object, e As EventArgs) Handles pic_closebtn.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnreg_user_Click(sender As Object, e As EventArgs) Handles btnreg_user.Click
        Try
            database_conn.Open()

            Dim strsql_insert As String = "INSERT INTO clients(client_name,email_address,password,client_type) VALUES('" & CType(txtuser_name.Text, String) & "','" & CType(txtemail_field.Text, String) & "','" & CType(txtpass_field.Text, String) & "','" & CType(cmpClient_type.Text, String) & "')"
            ' Dim params As MySqlParameter = New MySqlParameter()

            database_cmd = New MySqlCommand(strsql_insert, database_conn)
            database_adapter = New MySqlDataAdapter(database_cmd)
            database_adapter.Fill(ds, "clients")


            MessageBox.Show("insert was succesful")



        Catch ex As Exception
            MsgBox("Exception occured " & ex.Message)
        End Try
    End Sub

    Private Sub btnsignUp_Click(sender As Object, e As EventArgs) Handles btnsignUp.Click
        pnlsign_upinterface.Show()
        pnl_loginInterface.Hide()
    End Sub

    Private Sub pnlsign_upinterface_Paint(sender As Object, e As PaintEventArgs) Handles pnlsign_upinterface.Paint

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        pnl_loginInterface.Visible = True
        pnlsign_upinterface.Visible = False
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.WindowState = WindowState.Minimized
    End Sub
End Class
