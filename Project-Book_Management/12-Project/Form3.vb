Public Class book
    Dim ds As DataSet

    Public addBYesNo As Boolean
    Public updateBYesNo As Boolean

    Private b_title As String
    Private b_name As String
    Private b_publishing As String
    Private b_date As String

    Public Sub booklist()
        ds = New DataSet
        SQL = "Select * From book ORDER BY b_code ASC"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds, "book")
        BindingSource1.DataSource = ds.Tables("book")
        DataGridView1.DataSource = BindingSource1
        book_header()
        book_counter()
        DataGridView1.Refresh()
    End Sub

    Public Sub book_counter()
        Dim i As Integer
        i = DataGridView1.RowCount
        Label1.Text = "총 " & i & "권"
    End Sub

    Public Sub book_header()
        DataGridView1.Columns(0).HeaderText = "코드"
        DataGridView1.Columns(1).HeaderText = "제목"
        DataGridView1.Columns(2).HeaderText = "저자"
        DataGridView1.Columns(3).HeaderText = "출판사"
        DataGridView1.Columns(4).HeaderText = "발행일"
        DataGridView1.Columns(5).HeaderText = "가격"
        DataGridView1.Columns(6).Visible = False
        DataGridView1.Columns(7).Visible = False
        DataGridView1.Columns(8).Visible = False

        DataGridView1.Columns(0).Width = 60
        DataGridView1.Columns(1).Width = 110
        DataGridView1.Columns(2).Width = 80
        DataGridView1.Columns(3).Width = 80
        DataGridView1.Columns(4).Width = 80
        DataGridView1.Columns(5).Width = 60
    End Sub

    Private Sub book_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call booklist()
        Left = 0

        txtCode.Enabled = False
        txtTitle.Enabled = False
        txtName.Enabled = False
        txtPublishing.Enabled = False
        txtDate0.Enabled = False
        txtDate1.Enabled = False
        txtDate2.Enabled = False
        txtPrice.Enabled = False

        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = True
        btnDel.Enabled = True
        btnCancel.Enabled = False
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        txtCode.Enabled = True
        txtTitle.Enabled = True
        txtName.Enabled = True
        txtPublishing.Enabled = True
        txtDate0.Enabled = True
        txtDate1.Enabled = True
        txtDate2.Enabled = True
        txtPrice.Enabled = True

        txtCode.Text = ""
        txtTitle.Text = ""
        txtName.Text = ""
        txtPublishing.Text = ""
        txtDate0.Text = ""
        txtDate1.Text = ""
        txtDate2.Text = ""
        txtPrice.Text = ""
        txtCode.Focus()

        btnAdd.Enabled = False
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDel.Enabled = False
        btnCancel.Enabled = True

        addBYesNo = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ds = New DataSet
        If addBYesNo = True Then
            If Trim(txtCode.Text) = "" Then
                MsgBox("코드를 입력하시오.", vbOKOnly, "확인")
                txtCode.Focus()
            ElseIf Trim(txtTitle.Text) = "" Then
                MsgBox("책 제목을 입력하시오.", vbOKOnly, "확인")
                txtTitle.Focus()
            ElseIf Trim(txtName.Text) = "" Then
                MsgBox("저자를 입력하시오.", vbOKOnly, "확인")
                txtName.Focus()
            ElseIf Trim(txtPublishing.Text) = "" Then
                MsgBox("출판사를 입력하시오.", vbOKOnly, "확인")
                txtPublishing.Focus()
            ElseIf Trim(txtDate0.Text) = "" Then
                MsgBox("연도를 입력하시오.", vbOKOnly, "확인")
                txtDate0.Focus()
            ElseIf Trim(txtDate1.Text) = "" Then
                MsgBox("월을 입력하시오.", vbOKOnly, "확인")
                txtDate1.Focus()
            ElseIf Trim(txtDate2.Text) = "" Then
                MsgBox("일을 입력하시오.", vbOKOnly, "확인")
                txtDate2.Focus()
            ElseIf Trim(txtPrice.Text) = "" Then
                MsgBox("가격을 입력하시오.", vbOKOnly, "확인")
                txtPrice.Focus()
            Else
                SQL = "Select * From book Where b_code = '" & txtCode.Text & "'"
                DCom.CommandText = SQL
                DA = New OleDb.OleDbDataAdapter(SQL, Con)
                DA.Fill(ds, "book")

                If ds.Tables("book").Rows.Count = 0 Then
                    b_title = Replace(txtTitle.Text, "'", "'")
                    b_name = Replace(txtName.Text, "'", "'")
                    b_publishing = Replace(txtPublishing.Text, "'", "'")
                    b_date = txtDate0.Text & "-" & txtDate1.Text & "-" & txtDate2.Text

                    SQL = "Insert Into book(b_code, b_title, b_name, b_publishing, b_date, b_price, b_lent, b_lent_num) Values"
                    SQL = SQL & "('" & txtCode.Text & "'"
                    SQL = SQL & ",'" & b_title & "'"
                    SQL = SQL & ",'" & b_name & "'"
                    SQL = SQL & ",'" & b_publishing & "'"
                    SQL = SQL & ",'" & b_date & "'"
                    SQL = SQL & ",'" & txtPrice.Text & "'"
                    SQL = SQL & ",'" & "0" & "', 0)"
                    DCom.CommandText = SQL
                    DCom.ExecuteNonQuery()
                    DA.Fill(ds, "book")

                    txtCode.Text = ""
                    txtTitle.Text = ""
                    txtName.Text = ""
                    txtPublishing.Text = ""
                    txtDate0.Text = ""
                    txtDate1.Text = ""
                    txtDate2.Text = ""
                    txtPrice.Text = ""

                    txtCode.Enabled = False
                    txtTitle.Enabled = False
                    txtName.Enabled = False
                    txtPublishing.Enabled = False
                    txtDate0.Enabled = False
                    txtDate1.Enabled = False
                    txtDate2.Enabled = False
                    txtPrice.Enabled = False

                    btnAdd.Enabled = True
                    btnSave.Enabled = False
                    btnEdit.Enabled = True
                    btnDel.Enabled = True
                    btnCancel.Enabled = False

                    addBYesNo = False
                    MsgBox("도서 등록 완료!", vbOKOnly, "확인")
                Else
                    MsgBox("도서코드가 중복됩니다.", , "확인")
                    txtCode.Text = ""
                    txtCode.Focus()
                End If

                Call booklist()
                Call main.list_search("", "")
            End If

        ElseIf updateBYesNo = True Then
            If txtCode.Text = "" Then
                MsgBox("수정할 목록을 선택하시오.", vbOKOnly, "확인")
            Else
                Ret = MsgBox("정말 수정하시겠습니까?", vbYesNo, "수정")
                If Ret = vbYes Then
                    b_title = Replace(txtTitle.Text, "'", "'")
                    b_name = Replace(txtName.Text, "'", "'")
                    b_publishing = Replace(txtPublishing.Text, "'", "'")
                    b_date = txtDate0.Text & "-" & txtDate1.Text & "-" & txtDate2.Text

                    SQL = "Update book Set "
                    SQL = SQL & "b_title = '" & b_title & "'"
                    SQL = SQL & ", b_name = '" & b_name & "'"
                    SQL = SQL & ", b_publishing = '" & b_publishing & "'"
                    SQL = SQL & ", b_date = '" & b_date & "'"
                    SQL = SQL & ", b_price = '" & txtPrice.Text & "'"
                    SQL = SQL & " Where b_code = '" & txtCode.Text & "'"
                    DCom.CommandText = SQL
                    DCom.ExecuteNonQuery()
                    DA.Fill(ds, "book")

                    Call booklist()
                    Call main.list_search("", "")
                    btnCancel.PerformClick()
                End If
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtCode.Text = "" Then
            MsgBox("수정할 목록을 선택하시오.", , "확인")
        Else
            txtCode.Enabled = True
            txtTitle.Enabled = True
            txtName.Enabled = True
            txtPublishing.Enabled = True
            txtDate0.Enabled = True
            txtDate1.Enabled = True
            txtDate2.Enabled = True
            txtPrice.Enabled = True

            btnAdd.Enabled = False
            btnSave.Enabled = True
            btnEdit.Enabled = False
            btnDel.Enabled = False
            btnCancel.Enabled = True

            updateBYesNo = True
        End If
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        ds = New DataSet
        If txtCode.Text = "" Then
            MsgBox("삭제할 목록을 선택하시오.", vbOKOnly, "확인")
        Else
            SQL = "Select b_lent From book Where b_code = '" & txtCode.Text & "'"
            DCom.CommandText = SQL
            DA = New OleDb.OleDbDataAdapter(SQL, Con)
            DA.Fill(ds, "book")

            Ret = MsgBox("선택한 도서를 삭제하시겠습니까?(복구 불가능)", vbYesNo, "삭제")
            If Ret = vbYes Then
                If ds.Tables("book").Rows(0).Item(0).ToString <> "0" Then
                    MsgBox("현재 대여 중입니다.(반납 후 삭제 바람)", , "확인")
                Else
                    SQL = "Delete From book Where b_code = '" & txtCode.Text & "'"
                    DCom.CommandText = SQL
                    DCom.ExecuteNonQuery()
                    DA.Fill(ds, "book")

                    txtCode.Text = ""
                    txtTitle.Text = ""
                    txtName.Text = ""
                    txtPublishing.Text = ""
                    txtDate0.Text = ""
                    txtDate1.Text = ""
                    txtDate2.Text = ""
                    txtPrice.Text = ""

                    Call booklist()
                    Call main.list_search("", "")
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtCode.Enabled = False
        txtTitle.Enabled = False
        txtName.Enabled = False
        txtPublishing.Enabled = False
        txtDate0.Enabled = False
        txtDate1.Enabled = False
        txtDate2.Enabled = False
        txtPrice.Enabled = False

        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = True
        btnDel.Enabled = True
        btnCancel.Enabled = False

        addBYesNo = False
        updateBYesNo = False
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If addBYesNo = True Or updateBYesNo = True Then
            txtCode.Text = ""
            txtTitle.Text = ""
            txtName.Text = ""
            txtPublishing.Text = ""
            txtDate0.Text = ""
            txtDate1.Text = ""
            txtDate2.Text = ""
            txtPrice.Text = ""
        Else
            Dim date_split() As String
            b_date = DataGridView1.Item(4, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            date_split = Split(b_date, "-")

            txtCode.Text = DataGridView1.Item(0, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            txtTitle.Text = DataGridView1.Item(1, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            txtName.Text = DataGridView1.Item(2, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            txtPublishing.Text = DataGridView1.Item(3, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            txtDate0.Text = date_split(0)
            txtDate1.Text = date_split(1)
            txtDate2.Text = date_split(2)
            txtPrice.Text = DataGridView1.Item(5, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
        End If
    End Sub
End Class