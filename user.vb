Imports System.Data.Odbc
Public Class user
    Sub Awal()
        type.Items.Add("Admin")
        type.Items.Add("Operator")
        Call koneksidb()
        Da = New OdbcDataAdapter("select * from tbl_admin", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_admin")
        DataUser.DataSource = Ds.Tables("tbl_admin")
        DataUser.ReadOnly = True
        Button2.Text = "tutup"
    End Sub
    Private Sub user_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Awal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If nama.Text = "" Or password.Text = "" Or type.Text = "" Then
            MsgBox("Silakan Isi Semua Field")
        Else
            Call koneksidb()
            Dim inputdata As String = "insert into tbl_admin values('" & id.Text & "','" & nama.Text & "','" & password.Text & "','" & type.Text & "')"
            Cmd = New OdbcCommand(inputdata, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("Input Data Berhasil")
            Call Awal()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "tutup" Then
            Me.Close()
        Else
            Call Awal()
        End If
    End Sub
End Class