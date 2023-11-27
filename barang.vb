Imports System.Data.Odbc
Public Class barang
    Sub KondisiAwal()
        kode.Text = ""
        nama.Text = ""
        harga.Text = ""
        jumlah.Text = ""
        satuan.Text = ""

        kode.Enabled = False
        nama.Enabled = False
        harga.Enabled = False
        jumlah.Enabled = False
        satuan.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True

        Button1.Text = "input"
        Button2.Text = "edit"
        Button3.Text = "hapus"
        Button4.Text = "tutup"

        Call koneksidb()
        Da = New OdbcDataAdapter("select * from tbl_barang", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_barang")
        DataGridView1.DataSource = Ds.Tables("tbl_barang")
        DataGridView1.ReadOnly = True
    End Sub
    Sub MunculSatuan()
        Call koneksidb()
        Cmd = New OdbcCommand("Select distinct satuanbarang from tbl_barang", Conn)
        Rd = Cmd.ExecuteReader
        satuan.Items.Clear()
        Do While Rd.Read
            satuan.Items.Add(Rd.Item("satuanbarang"))

        Loop
    End Sub
    Sub SiapIsi()
        kode.Enabled = True
        nama.Enabled = True
        harga.Enabled = True
        jumlah.Enabled = True
        satuan.Enabled = True

        Call MunculSatuan()
    End Sub
    Private Sub barang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "input" Then
            Button1.Text = "simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "batal"
            Call SiapIsi()
        Else
            If kode.Text = "" Or nama.Text = "" Or harga.Text = "" Or jumlah.Text = "" Then
                MsgBox("silahkan isi semua field")
            Else
                Call koneksidb()
                Dim inputdata As String = " insert into tbl_barang values ('" & kode.Text &
                    "','" & nama.Text &
                    "','" & harga.Text &
                    "','" & jumlah.Text & "','" & satuan.Text & "')"
                Cmd = New OdbcCommand(inputdata, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("input data berhasil")
                Call KondisiAwal()

            End If

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "edit" Then
            Button2.Text = "simpan"
            Button1.Text = False
            Button3.Enabled = False
            Button4.Text = "batal"
            Call SiapIsi()
        Else
            If kode.Text = "" Or nama.Text = "" Or harga.Text = "" Or jumlah.Text = "" Or satuan.Text = "" Then
                MsgBox("silahkan isi semua field")
            Else
                Call koneksidb()
                Dim updatedata As String = " update tbl_barang set namabarang='" & nama.Text &
                    "',hargabarang='" & harga.Text &
                    "',jumlahbarang='" & jumlah.Text &
                    "',satuanbarang='" & satuan.Text &
                    "' where kodebarang='" & kode.Text & "'"
                Cmd = New OdbcCommand(updatedata, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("update data berhasil")
                Call KondisiAwal()

            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "hapus" Then
            Button3.Text = "delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "batal"
            Call SiapIsi()
        Else
            If kode.Text = "" Or nama.Text = "" Or harga.Text = "" Or jumlah.Text = "" Then
                MsgBox("silahkan isi semua field")
            Else
                Call koneksidb()
                Dim hapusdata As String = " delete from tbl_barang where kodebarang='" & kode.Text & "'"
                Cmd = New OdbcCommand(hapusdata, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("hapus data berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles kode.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksidb()
            Cmd = New OdbcCommand("Select *from tbl_barang where kodebarang ='" & kode.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("kode barang tidak ada")
            Else
                kode.Text = Rd.Item("kodebarang")
                nama.Text = Rd.Item("namabarang")
                harga.Text = Rd.Item("hargabarang")
                jumlah.Text = Rd.Item("jumlahbarang")
                satuan.Text = Rd.Item("satuanbarang")
            End If
        End If
    End Sub

    Private Sub TextBox1_RightToLeftChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles kode.RightToLeftChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kode.TextChanged

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "tutup" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles harga.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles harga.TextChanged

    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles jumlah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles jumlah.TextChanged

    End Sub
End Class