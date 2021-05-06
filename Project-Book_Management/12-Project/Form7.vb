Public Class inforLentUser
    Dim ds As DataSet

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub inforLentUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet
        SQL = "Select m_name From member Where m_id = '" & Label1.Text & "'"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds, "member")
        GroupBox1.Text = ds.Tables("member").Rows(0).Item(0) & "(ID : " & Label1.Text & ")"

        infor_user()
    End Sub

    Public Sub infor_header()
        DataGridView1.Columns(0).HeaderText = "코드"
        DataGridView1.Columns(1).HeaderText = "제목"
        DataGridView1.Columns(2).HeaderText = "저자"
        DataGridView1.Columns(3).Visible = False
    End Sub

    Public Sub infor_user()
        ds = New DataSet
        SQL = "Select b_code, b_title, b_name, b_lent_date From book Where b_lent = '" & Label1.Text & "'"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds, "book")
        Dim i As Integer
        Dim lentDate As String
        Dim returnDate As String
        i = 0
        ds.Tables("book").Columns.Add("대여일")
        ds.Tables("book").Columns.Add("반납일")
        Do While i < ds.Tables("book").Rows.Count
            lentDate = Strings.Left(ds.Tables("book").Rows(i).Item(3), 10)
            returnDate = Strings.Left(DateAdd("d", 10, ds.Tables("book").Rows(0).Item(3)), 10)
            ds.Tables("book").Rows(i).Item(4) = lentDate
            ds.Tables("book").Rows(i).Item(5) = returnDate
            i = i + 1
        Loop
        BindingSource1.DataSource = ds.Tables("book")
        DataGridView1.DataSource = BindingSource1
        infor_header()
        DataGridView1.Refresh()
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        If TextBox1.Text = "" Then
            MsgBox("목록을 선택해주시기 바랍니다.", , "확인")
        Else
            main.txtFind2.Text = TextBox1.Text
            Me.Close()
            main.RadioButton4.Checked = True
            main.btnLentOrReturn.PerformClick()
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        TextBox1.Text = DataGridView1.Item(0, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
    End Sub
End Class