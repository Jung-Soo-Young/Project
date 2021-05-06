Module Module1
    Public Ret As String
    Public SQL As String
    Public Con As New OleDb.OleDbConnection
    Public DCom As New OleDb.OleDbCommand
    Public DA As New OleDb.OleDbDataAdapter

    Public Sub DB_Access()
        Dim My_con As String
        My_con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\library.accdb"

        Con.ConnectionString = My_con
        DCom.Connection = Con
        Con.Open()
    End Sub
End Module
