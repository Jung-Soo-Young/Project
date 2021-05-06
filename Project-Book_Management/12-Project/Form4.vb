Public Class member
    Dim ds As DataSet

    Public addUYesNo As Boolean
    Public updateUYesNo As Boolean

    Private t_phone As String
    Private t_name As String
    Private t_address As String

    Public Sub member_list()
        ds = New DataSet
        SQL = "Select * From member ORDER BY m_id ASC"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds, "member")
        BindingSource1.DataSource = ds.Tables("member")
        DataGridView1.DataSource = BindingSource1
        member_header()
        member_counter()
        DataGridView1.Refresh()
    End Sub

    Public Sub member_counter()
        Dim i As Integer
        i = DataGridView1.RowCount
        Label1.Text = "총 " & i & "명"
    End Sub

    Public Sub member_header()
        DataGridView1.Columns(0).HeaderText = "ID"
        DataGridView1.Columns(1).HeaderText = "이름"
        DataGridView1.Columns(2).HeaderText = "전화번호"
        DataGridView1.Columns(3).HeaderText = "주소"
        DataGridView1.Columns(4).HeaderText = "성별"
        DataGridView1.Columns(5).HeaderText = "가입 날짜"
        DataGridView1.Columns(6).Visible = False
        DataGridView1.Columns(7).HeaderText = "총 대여 도서"

        DataGridView1.Columns(0).Width = 50
        DataGridView1.Columns(1).Width = 60
        DataGridView1.Columns(2).Width = 90
        DataGridView1.Columns(3).Width = 90
        DataGridView1.Columns(4).Width = 55
        DataGridView1.Columns(5).Width = 80
        DataGridView1.Columns(7).Width = 95
    End Sub

    Private Sub member_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call member_list()
        Left = 0
        Call init(3)
    End Sub

    Private Sub init(ByVal i As Integer)
        If i = 1 Then
            txtID.Text = ""
            txtName.Text = ""
            txtPhone0.Text = ""
            txtPhone1.Text = ""
            txtPhone2.Text = ""
            txtAddress.Text = ""

            txtID.Enabled = True
            txtName.Enabled = True
            txtPhone0.Enabled = True
            txtPhone1.Enabled = True
            txtPhone2.Enabled = True
            txtAddress.Enabled = True
            txtID.Focus()

        ElseIf i = 2 Then
            txtID.Text = ""
            txtName.Text = ""
            txtPhone0.Text = ""
            txtPhone1.Text = ""
            txtPhone2.Text = ""
            txtAddress.Text = ""

            txtID.Enabled = False
            txtName.Enabled = False
            txtPhone0.Enabled = False
            txtPhone1.Enabled = False
            txtPhone2.Enabled = False
            txtAddress.Enabled = False

        ElseIf i = 3 Then
            txtID.Enabled = False
            txtName.Enabled = False
            txtPhone0.Enabled = False
            txtPhone1.Enabled = False
            txtPhone2.Enabled = False
            txtAddress.Enabled = False

            btnAdd.Enabled = True
            btnSave.Enabled = False
            btnEdit.Enabled = True
            btnDel.Enabled = True
            btnCancel.Enabled = False

        Else
            txtID.Enabled = True
            txtName.Enabled = True
            txtPhone0.Enabled = True
            txtPhone1.Enabled = True
            txtPhone2.Enabled = True
            txtAddress.Enabled = True
            txtID.Focus()

            btnAdd.Enabled = False
            btnSave.Enabled = True
            btnEdit.Enabled = False
            btnDel.Enabled = False
            btnCancel.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call init(1)

        btnAdd.Enabled = False
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDel.Enabled = False
        btnCancel.Enabled = True

        addUYesNo = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ds = New DataSet
        If addUYesNo = True Then
            If txtID.Text = "" Then
                MsgBox("아이디를 입력하세요.", vbOKOnly, "공백")
                txtID.Focus()
            ElseIf txtName.Text = "" Then
                MsgBox("이름을 입력해주세요.", vbOKOnly, "공백")
                txtName.Focus()
            ElseIf txtPhone0.Text = "" Then
                MsgBox("전화번호나 휴대폰 번호를 입력하세요.", vbOKOnly, "공백")
                txtPhone0.Focus()
            ElseIf txtPhone1.Text = "" Then
                MsgBox("전화번호나 휴대폰 번호를 입력하세요.", vbOKOnly, "공백")
                txtPhone1.Focus()
            ElseIf txtPhone2.Text = "" Then
                MsgBox("전화번호나 휴대폰 번호를 입력하세요.", vbOKOnly, "공백")
                txtPhone2.Focus()
            ElseIf txtAddress.Text = "" Then
                MsgBox("주소를 입력하세요.", vbOKOnly, "공백")
                txtAddress.Focus()
            Else
                SQL = "Select * From Member Where m_id = '" & txtID.Text & "'"
                DCom.CommandText = SQL
                DA = New OleDb.OleDbDataAdapter(SQL, Con)
                DA.Fill(ds, "member")

                If ds.Tables("member").Rows.Count = 0 Then
                    Dim sex As String
                    If RadioButton1.Checked Then
                        sex = "남자"
                    Else
                        sex = "여자"
                    End If

                    t_name = Replace(txtName.Text, "'", "'")
                    t_address = Replace(txtAddress.Text, "'", "'")
                    t_phone = txtPhone0.Text & "-" & txtPhone1.Text & "-" & txtPhone2.Text

                    SQL = "Insert Into member(m_id, m_name, m_phone, m_address, m_sex, m_date, m_lent, m_total_lent) Values"
                    SQL = SQL & "('" & txtID.Text & "'"
                    SQL = SQL & ",'" & t_name & "'"
                    SQL = SQL & ",'" & t_phone & "'"
                    SQL = SQL & ",'" & t_address & "'"
                    SQL = SQL & ",'" & sex & "'"
                    SQL = SQL & ",'" & Now() & "'"
                    SQL = SQL & ",0, 0)"
                    DCom.CommandText = SQL
                    DCom.ExecuteNonQuery()
                    DA.Fill(ds, "member")

                    Call init(2)
                    addUYesNo = False
                    MsgBox("신규 등록 완료", vbOKOnly, "확인")
                Else
                    MsgBox("동일 ID가 존재합니다.", vbOKOnly, "확인")
                    Call init(1)
                End If
                btnCancel.PerformClick()
                Call member_list()
            End If

        ElseIf updateUYesNo = True Then
            If txtID.Text = "" Then
                MsgBox("수정할 ID를 선택하지 않았습니다.", vbOKOnly, "확인")
            Else
                Ret = MsgBox("수정하시겠습니까?", vbYesNo, "수정")
                If Ret = vbYes Then
                    t_name = Replace(txtName.Text, "'", "'")
                    t_address = Replace(txtAddress.Text, "'", "'")
                    t_phone = txtPhone0.Text & "-" & txtPhone1.Text & "-" & txtPhone2.Text
                    Dim sex As String
                    If RadioButton1.Checked = True Then
                        sex = "남자"
                    Else
                        sex = "여자"
                    End If

                    SQL = "Update member Set "
                    SQL = SQL & "m_name = '" & t_name & "'"
                    SQL = SQL & ", m_phone = '" & t_phone & "'"
                    SQL = SQL & ", m_address = '" & t_address & "'"
                    SQL = SQL & ", m_sex = '" & sex & "'"
                    SQL = SQL & " Where m_id = '" & txtID.Text & "'"
                    DCom.CommandText = SQL
                    DCom.ExecuteNonQuery()
                    DA.Fill(ds, "member")

                    MsgBox("수정 완료", vbOKOnly, "확인")
                    updateUYesNo = False
                    Call member_list()
                    btnCancel.PerformClick()
                End If
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtID.Text = "" Then
            MsgBox("ID를 선택해주십시오.", , "ID 선택")
        Else
            updateUYesNo = True
            Call init(4)
        End If
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        ds = New DataSet
        If txtID.Text = "" Then
            MsgBox("삭제할 ID를 선택하시오.", vbOKOnly, "확인")
        Else
            Ret = MsgBox("삭제하시겠습니까?(복구 불가능)", vbYesNo, "삭제")
            If Ret = vbYes Then
                SQL = "Select m_lent From member Where m_id = '" & txtID.Text & "'"
                DCom.CommandText = SQL
                DA = New OleDb.OleDbDataAdapter(SQL, Con)
                DA.Fill(ds, "member")

                If ds.Tables("member").Rows(0).Item(0).ToString <> "0" Then
                    MsgBox("먼저 대여한 도서를 반납해주십시오.", , "확인")
                Else
                    SQL = "Delete From member Where m_id = '" & txtID.Text & "'"
                    DCom.CommandText = SQL
                    DCom.ExecuteNonQuery()
                    DA.Fill(ds, "member")

                    txtID.Text = ""
                    txtName.Text = ""
                    txtPhone0.Text = ""
                    txtPhone1.Text = ""
                    txtPhone2.Text = ""
                    txtAddress.Text = ""

                    Call member_list()
                    Call main.lent_id()
                    Call main.blacklist()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call init(3)

        addUYesNo = False
        updateUYesNo = False
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If addUYesNo = True Or updateUYesNo = True Then
            txtID.Text = ""
            txtName.Text = ""
            txtPhone0.Text = ""
            txtPhone1.Text = ""
            txtPhone2.Text = ""
            txtAddress.Text = ""
        Else
            Dim phone_split() As String
            t_phone = DataGridView1.Item(2, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            phone_split = Split(t_phone, "-")

            Dim sex As String
            sex = DataGridView1.Item(4, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            If sex = "남자" Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

            txtID.Text = DataGridView1.Item(0, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            txtName.Text = DataGridView1.Item(1, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
            txtPhone0.Text = phone_split(0)
            txtPhone1.Text = phone_split(1)
            txtPhone2.Text = phone_split(2)
            txtAddress.Text = DataGridView1.Item(3, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
        End If
    End Sub
End Class