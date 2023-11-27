Imports System.Data.Odbc
Public Class login
    Sub Terbuka()
        Form1.DataMasterToolStripMenuItem.Enabled = True
        Form1.TransaksiToolStripMenuItem.Enabled = True
        Form1.LaporanToolStripMenuItem.Enabled = True
        Form1.LogoutToolStripMenuItem.Enabled = True
    End Sub
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Kode Admin dan Password Tidak Boleh Kosong")
        Else
            Call koneksidb()
            Cmd = New OdbcCommand("Select * From tbl_admin where namaadmin='" & TextBox1.Text & "' and passwordadmin='" & TextBox2.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                Me.Close()
                Call Terbuka()
            Else
                MsgBox("Kode Admin Dan Password Salah")
            End If
        End If
    End Sub
End Class