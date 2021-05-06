Public Class information
    Dim ds1, ds2 As DataSet

    Private Sub information_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call information()
    End Sub

    Public Sub information()
        ds1 = New DataSet
        ds2 = New DataSet
        SQL = "Select Count(*) From book"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds1, "book")
        Label2.Text = ds1.Tables("book").Rows(0).Item(0) & "권"

        SQL = "Select Count(*) From member"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds1, "member")
        Label4.Text = ds1.Tables("member").Rows(0).Item(0) & "명"

        SQL = "Select Count(*) From book Where b_lent <> '0'"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds2, "book")
        Label6.Text = ds2.Tables("book").Rows(0).Item(0) & "권"

        SQL = "Select Count(*) From member Where m_lent <> '0'"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds2, "member")
        Label8.Text = ds2.Tables("member").Rows(0).Item(0) & "명"
    End Sub
End Class