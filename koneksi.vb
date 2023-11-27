Imports System.Data.Odbc
Module koneksi
    Public Conn As New OdbcConnection
    Public Da As OdbcDataAdapter
    Public Ds As DataSet
    Public Rd As OdbcDataReader
    Public Cmd As OdbcCommand
    Public MyDB As String

    Public Sub koneksidb()
        MyDB = "Driver={Mysql ODBC 3.51 Driver}; Database=db_penjualan;server=localhost;uid=root;pwd="
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub
End Module
