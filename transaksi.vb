Imports System.Data.Odbc
Public Class transaksi

    Private Property TglMySQL As String
        Sub KondisiAwal()
            LBLNamaPlg.Text = ""
            LBLAlamat.Text = ""
            LBLTelepon.Text = ""
        LBLTanggal.Text = Today
        LBLJam.Text = TimeOfDay
        LBLAdmin.Text = user.nama.Text
            LBLKembali.Text = ""
            TextBox2.Text = ""
            LBLNamaBarang.Text = ""
            LBLHargaBarang.Text = ""
            TextBox3.Text = ""
        TextBox3.Enabled = False
            LBLItem.Text = ""
            Call MunculKodePelanggan()
            Call NomorOtomatis()
            Call BuatKolom()
        Label12.Text = "0"
            TextBox1.Text = ""

        End Sub
        Sub MunculKodePelanggan()
            Call koneksidb()
            ComboBox1.Items.Clear()
            Cmd = New OdbcCommand("Select * From tbl_pelanggan", Conn)
            Rd = Cmd.ExecuteReader
            Do While Rd.Read
                ComboBox1.Items.Add(Rd.Item(0))
            Loop
        End Sub
        Sub NomorOtomatis()
            Call koneksidb()
            Cmd = New OdbcCommand("Select* From tbl_jual where nojual in(Select max(nojual)from tbl_jual)", Conn)
            Dim UrutanKode As String
            Dim Hitung As Long
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                UrutanKode = "J" + Format(Now, "yyMMdd") + "001"
            Else
                Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 9) + 1
                UrutanKode = "J" + Format(Now, " yyMMdd") +
               Microsoft.VisualBasic.Right("000" & Hitung, 3)
            End If
            LBLNoJual.Text = UrutanKode
        End Sub
        Sub BuatKolom()
            DataGridView1.Columns.Clear()
            DataGridView1.Columns.Add("Kode", "Kode")
            DataGridView1.Columns.Add("Nama", "Nama Barang")
            DataGridView1.Columns.Add("Harga", "Harga")
            DataGridView1.Columns.Add("Jumlah", "Jumlah")
            DataGridView1.Columns.Add("Subtotal", "Subtotal")
        End Sub
        Private Sub transaksi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Call KondisiAwal()
            Call MunculKodePelanggan()

        End Sub
        Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
            Call koneksidb()
            Cmd = New OdbcCommand("Select * From tbl_pelanggan where kodepelanggan=" &
                                  ComboBox1.Text & "", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                LBLNamaPlg.Text = Rd!namapelanggan
                LBLAlamat.Text = Rd!alamatpelanggan
                LBLTelepon.Text = Rd!telponpelanggan
            End If
        End Sub

        Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
            LBLJam.Text = TimeOfDay
        End Sub

        Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
            If e.KeyChar = Chr(13) Then
                Call koneksidb()
                Cmd = New OdbcCommand("Select *From tbl_barang where kodebarang ='" &
                TextBox2.Text & "'", Conn)
                Rd = Cmd.ExecuteReader
                Rd.Read()
                If Not Rd.HasRows Then
                    MsgBox("Kode barang Tidak Ada")
                Else
                    TextBox2.Text = Rd.Item("kodebarang")
                    LBLNamaBarang.Text = Rd.Item("namabarang")
                    LBLHargaBarang.Text = Rd.Item("hargabarang")
                    TextBox3.Enabled = True
                End If
            End If
        End Sub

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
            If LBLNamaBarang.Text = "" Or TextBox3.Text = "" Then
                MsgBox(" Silahkan Masukan Kode Barang Dan Tekan ENTER !!!")
            Else
                DataGridView1.Rows.Add(New String() {TextBox2.Text, LBLNamaBarang.Text,
                LBLHargaBarang.Text, TextBox3.Text, Val(LBLHargaBarang.Text) *
                Val(TextBox3.Text)})
                Call RumusSubtotal()
                TextBox2.Text = ""
                LBLNamaBarang.Text = ""
                LBLHargaBarang.Text = ""
                TextBox3.Text = ""
                TextBox3.Enabled = False
            End If
        End Sub
        Sub RumusSubtotal()
            Dim hitung As Integer = 0
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                hitung = hitung + DataGridView1.Rows(i).Cells(4).Value
            Label12.Text = hitung
            Next
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If LBLKembali.Text = "" Or LBLNamaPlg.Text = "" Or Label12.Text = "" Then
            MsgBox("Transaksi Tidak Ada, silahkan lakukan transaksi terlebih dahulu")
        Else
            TglMySQL = Format(Today, "yyyy-MM-dd")
            Dim SimpanJual As String = "Insert into tbl_jual values ('" & LBLNoJual.Text &
            "','" & TglMySQL & "','" & LBLJam.Text & "','" & LBLItem.Text &
            "','" & Label12.Text & "', '" & TextBox1.Text & "','" & LBLKembali.Text &
            "','" & ComboBox1.Text & "','" & user.nama.Text & "')"
            Cmd = New OdbcCommand(SimpanJual, Conn)
            Cmd.ExecuteNonQuery()
            For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = " Insert into tbl_detailjual values ('" & LBLNoJual.Text &
                "', '" & DataGridView1.Rows(Baris).Cells(0).Value &
                "','" & DataGridView1.Rows(Baris).Cells(1).Value &
                "','" & DataGridView1.Rows(Baris).Cells(2).Value &
                "','" & DataGridView1.Rows(Baris).Cells(3).Value &
                "','" & DataGridView1.Rows(Baris).Cells(4).Value & "')"
                Cmd = New OdbcCommand(SimpanDetail, Conn)
                Cmd.ExecuteNonQuery()
                Cmd = New OdbcCommand("select * from tbl_barang where kodebarang ='" &
                DataGridView1.Rows(Baris).Cells(0).Value & "'", Conn)
                Rd = Cmd.ExecuteReader
                Rd.Read()
                Dim KurangiStock As String = "Update tbl_barang set JumlahBarang = '" &
                Rd.Item("JumlahBarang") - DataGridView1.Rows(Baris).Cells(3).Value & "' where KodeBarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'"
                Cmd = New OdbcCommand(KurangiStock, Conn)
                Cmd.ExecuteNonQuery()
            Next
            Call KondisiAwal()
            MsgBox("Transaksi Berhasil Disimpan")
        End If
        End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "TUTUP" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub
End Class