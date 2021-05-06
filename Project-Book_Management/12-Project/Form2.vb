Public Class main
    Dim ds As DataSet '임시 데이터를 저장할 DataSet을 생성한다.

    Public book_code As String '대여에 사용할 변수를 선언한다.
    Public id_code As String   '반납에 사용할 변수를 선언한다.

    'SQL 문을 실행한다. 그 결과를 데이터베이스에 반영하고
    'DataGridView1에 출력한다.
    Public Sub sql_execute(ByVal SQL As String, ByVal Dataset As DataSet)

        'SQL 문을 저장하여 실행하고 DataSet에 결과를 저장한다.
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(Dataset, "book")

        Dim i As Integer
        i = 0
        Do While i < Dataset.Tables("book").Rows.Count
            'book 테이블의 대여값이 "0"이면 대여 '가능'으로,
            '아니면 '불가능'으로 변경한다.
            If Dataset.Tables("book").Rows(i).Item(6).ToString Like "0" Then
                Dataset.Tables("book").Rows(i).Item(6) = "가능"
            Else
                Dataset.Tables("book").Rows(i).Item(6) = "불가능"
            End If
            i = i + 1
        Loop

        'DataSet을 DataGridView1과 연결한다.
        BindingSource1.DataSource = Dataset.Tables("book")
        DataGridView1.DataSource = BindingSource1

        'DataGridView에 출력된 책의 개수를 세는 프로시저를 실행한다.
        book_counter()

        'DataGridView에 열 머리와 열 너비를 설정하는 프로시저를 실행한다.
        book_header()

        'DataGridView를 강제로 다시 그린다.
        DataGridView1.Refresh()
    End Sub

    '변수 i에 DataGridView1의 행 개수를 세어 저장하고 Label3에 출력한다.
    Public Sub book_counter()
        Dim i As Integer
        i = DataGridView1.RowCount
        Label3.Text = "총 " & i & "권"
    End Sub

    'DataGridView1의 열 머리글을 수정한다.
    '사용하지 않을 열은 숨기고, 숨기지 않은 열의 너비를 조정한다.
    Public Sub book_header()
        DataGridView1.Columns(0).HeaderText = "코드"
        DataGridView1.Columns(1).HeaderText = "제목"
        DataGridView1.Columns(2).HeaderText = "저자"
        DataGridView1.Columns(3).HeaderText = "출판사"
        DataGridView1.Columns(4).Visible = False
        DataGridView1.Columns(5).Visible = False
        DataGridView1.Columns(6).HeaderText = "대여"
        DataGridView1.Columns(7).Visible = False
        DataGridView1.Columns(8).Visible = False

        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 90
        DataGridView1.Columns(3).Width = 110
        DataGridView1.Columns(6).Width = 80
    End Sub

    Public Sub list_search(ByVal Find As String, ByVal Sort As String)
        ds = New DataSet

        '검색어와 정렬 방식을 입력하지 않으면
        '제목을 기준으로 오름차순 정렬하고 전체 책 목록을 출력한다.
        If Find = "" And Sort = "" Then
            SQL = "Select * From book ORDER BY b_title ASC"
            sql_execute(SQL, ds)

            '선택한 항목(정렬 방식)을 기준으로 오름차순 정렬하고
            '전체 책 목록을 출력한다.
        ElseIf Find = "" And Sort <> "" Then
            If Sort = "title" Then
                SQL = "Select * From book ORDER BY b_title ASC"
            ElseIf Sort = "name" Then
                SQL = "Select * From book ORDER BY b_name ASC"
            ElseIf Sort = "publishing" Then
                SQL = "Select * From book ORDER BY b_publishing ASC"
            ElseIf Sort = "lent" Then
                SQL = "Select * From book ORDER BY b_lent ASC"
            End If
            sql_execute(SQL, ds)

            '제목, 코드 중 라디오버튼으로 선택한 범위에서 검색어를 검색 및 출력한다.
            '조건에 맞는 항목이 없다면 메시지를 출력하고 전체 책 목록을 출력한다
        ElseIf Find <> "" Then
            If RadioButton1.Checked Then
                SQL = "Select * From book Where b_title Like '%" & Find & "%'"
            ElseIf RadioButton2.Checked Then
                SQL = "Select * From book Where b_code = '" & Find & "'"
            End If
            sql_execute(SQL, ds)
            If ds.Tables("book").Rows.Count = 0 Then
                MsgBox("해당 도서가 없습니다", , "확인")
                SQL = "Select * From book ORDER BY b_title ASC"
                sql_execute(SQL, ds)
            End If
        End If
    End Sub

    '전체 책 목록에서 더블클릭된 행의 도서 코드를
    'inforbook 폼의 Label2에 출력하고 inforbook 폼을 연다(도서 정보 창 표시).
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        inforbook.Label2.Text = DataGridView1.Item(0, Int(DataGridView1.CurrentRow.Index.ToString)).Value.ToString
        inforbook.Show()
    End Sub

    'DataGridView2와 데이터베이스를 연결하여 대여 회원 목록을 출력한다.
    Public Sub sql_execute2(ByVal SQL As String, ByVal Dataset As DataSet)
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(Dataset, "member")
        BindingSource2.DataSource = Dataset.Tables("member")
        DataGridView2.DataSource = BindingSource2
        lentm_counter()
        lentm_header()
        DataGridView2.Refresh()
    End Sub

    '전체 대여 회원 수를 출력한다.
    Public Sub lentm_counter()
        Dim i As Integer
        i = DataGridView2.RowCount
        Label2.Text = "총 " & i & "명"
    End Sub

    '대여 회원 목록을 출력하는 DataGridView2의 열 머리와 열 너비를 설정한다.
    Public Sub lentm_header()
        DataGridView2.Columns(0).HeaderText = "ID"
        DataGridView2.Columns(1).HeaderText = "이름"
        DataGridView2.Columns(2).Visible = False
        DataGridView2.Columns(3).Visible = False
        DataGridView2.Columns(4).Visible = False
        DataGridView2.Columns(5).Visible = False
        DataGridView2.Columns(6).HeaderText = "권수"
        DataGridView2.Columns(7).Visible = False

        DataGridView2.Columns(0).Width = 80
        DataGridView2.Columns(1).Width = 80
        DataGridView2.Columns(6).Width = 80
    End Sub

    '책을 대여한 회원들의 ID를 오름차순으로 정렬하여 출력한다.
    Public Sub lent_id()
        ds = New DataSet
        SQL = "Select * From member Where m_lent <> 0 ORDER BY m_id ASC"
        sql_execute2(SQL, ds)
    End Sub

    '대여 회원 목록에서 더블클릭한 행의 ID를 inforLentUser 폼의 Label1에 출력하고
    'inforLentUser 폼을 연다(대여 정보 폼에 사용자별 대여 현황 표시).
    Private Sub DataGridView2_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDoubleClick
        inforLentUser.Label1.Text = DataGridView2.Item(0, Int(DataGridView2.CurrentRow.Index.ToString)).Value.ToString
        inforLentUser.Show()
    End Sub

    '연체자를 데이터베이스에서 검색하여 DataGridView3에 출력한다.
    Public Sub sql_execute3(ByVal SQL As String, ByVal Dataset As DataSet)
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(Dataset, "book")
        Dim i As Integer
        i = 0
        Dataset.Tables("book").Columns.Add("연체일")
        Do While i < Dataset.Tables("book").Rows.Count
            Dataset.Tables("book").Rows(i).Item(3) = Strings.Left(Dataset.Tables("book").Rows(i).Item(3), 10)
            Dim lentDiff As String

            '오늘 날짜에서 대여일을 뺀 기간이1 0일을 넘는 경우
            '며칠을 초과했는지 저장하여 출력한다.
            Dim ti As Date
            ti = Dataset.Tables("book").Rows(0).Item(3)
            lentDiff = DateDiff("d", ti, Now()) - 10
            Dataset.Tables("book").Rows(i).Item(4) = lentDiff.ToString & "일"
            i = i + 1
        Loop
        BindingSource3.DataSource = Dataset.Tables("book")
        DataGridView3.DataSource = BindingSource3
        blackm_counter()
        blackm_header()
        DataGridView3.Refresh()
    End Sub

    '몇 명이 연체했는지 출력한다.
    Public Sub blackm_counter()
        Dim i As Integer
        i = DataGridView3.RowCount
        Label4.Text = "총 " & i & "명"
    End Sub

    '연체자를 출력하는 DataGridView3의 열 머리와 열 너비를 설정한다.
    Public Sub blackm_header()
        DataGridView3.Columns(0).HeaderText = "이름"
        DataGridView3.Columns(1).HeaderText = "코드"
        DataGridView3.Columns(2).HeaderText = "제목"
        DataGridView3.Columns(3).HeaderText = "대출일"

        DataGridView3.Columns(0).Width = 80
        DataGridView3.Columns(1).Width = 60
        DataGridView3.Columns(2).Width = 100
        DataGridView3.Columns(3).Width = 80
        DataGridView3.Columns(4).Width = 70
    End Sub

    '대여일이 10일을 넘는 회원 수를 연체자 목록에 출력한다.
    Public Sub blacklist()
        ds = New DataSet
        SQL = "Select B.m_name, A.b_code, A.b_title, A.b_lent_date From book A Inner Join member B on A.b_lent = B.m_id Where A.b_lent_date + 10 < now() ORDER BY A.b_lent_date ASC"
        sql_execute3(SQL, ds)
    End Sub

    '전체 책 목록, 대여 회원, 연체자를 출력하는 프로시저를 실행한다.
    Private Sub main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call list_search("", "")
        Call lent_id()
        Call blacklist()
        Left = 0
        Top = 0
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If Trim(txtFind1.Text) = "" Then
            MsgBox("검색 미입력", , "확인")
        Else
            Call list_search(Trim(txtFind1.Text), "")
        End If
    End Sub

    Private Sub btnLentOrReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLentOrReturn.Click
        ds = New DataSet
        If Trim(txtFind2.Text) = "" Then
            MsgBox("도서코드 미입력", , "확인")
            txtFind2.Focus()
        Else
            If RadioButton3.Checked Then
                SQL = "Select * From book Where b_code = '" & txtFind2.Text & "'"
                DCom.CommandText = SQL
                DA = New OleDb.OleDbDataAdapter(SQL, Con)
                DA.Fill(ds, "book")

                If ds.Tables("book").Rows.Count = 0 Then
                    MsgBox("해당되는 도서가 없습니다.", , "확인")
                    txtFind2.Text = ""
                    txtFind2.Focus()
                Else
                    If ds.Tables("book").Rows(0).Item(6).ToString Like "0" Then
                        memid.Show()
                    Else
                        MsgBox("이미 대여된 도서입니다.", , "확인")
                        txtFind2.Text = ""
                        txtFind2.Focus()
                    End If
                End If
            ElseIf RadioButton4.Checked Then
                Ret = MsgBox("해당 도서를 반납하시겠습니까?", vbYesNo, "확인")
                If Ret = vbYes Then
                    SQL = "Select * From book Where b_code='" & txtFind2.Text & "'"
                    DCom.CommandText = SQL
                    DA = New OleDb.OleDbDataAdapter(SQL, Con)
                    DA.Fill(ds, "book")
                    If ds.Tables("book").Rows.Count = 0 Then
                        MsgBox("해당되는 도서가 없습니다.", , "확인")
                        txtFind2.Text = ""
                        txtFind2.Focus()
                    ElseIf ds.Tables("book").Rows(0).Item(6).ToString Like "0" Then
                        MsgBox("대여되지 않은 도서입니다.", , "확인")
                        txtFind2.Text = ""
                        txtFind2.Focus()
                    Else
                        id_code = ds.Tables("book").Rows(0).Item(6)
                        SQL = "Update book Set b_lent = '0', b_lent_date = Null Where b_code = '" & txtFind2.Text & "'"
                        DCom.CommandText = SQL
                        DCom.ExecuteNonQuery()
                        DA.Fill(ds, "book")

                        SQL = "Update member Set m_lent = m_lent-1 Where m_id = '" & id_code & "'"
                        DCom.CommandText = SQL
                        DCom.ExecuteNonQuery()
                        DA.Fill(ds, "member")

                        txtFind2.Text = ""
                        txtFind2.Focus()

                        Call list_search("", "")
                        lent_id()
                        blacklist()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Call list_search("", "")
    End Sub

    Private Sub btnSortByTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSortByTitle.Click
        Call list_search("", "title")
    End Sub

    Private Sub btnSortByAuthor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSotrByAuthor.Click
        Call list_search("", "name")
    End Sub

    Private Sub btnSortByPublisher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSortByPublisher.Click
        Call list_search("", "publishing")
    End Sub

    Private Sub btnSortByLending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSortByLending.Click
        Call list_search("", "lent")
    End Sub

    Private Sub RadioButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
        txtFind1.Focus()
    End Sub

    Private Sub RadioButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        txtFind1.Focus()
    End Sub

    Private Sub RadioButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.Click
        txtFind2.Focus()
    End Sub

    Private Sub RadioButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.Click
        txtFind2.Focus()
    End Sub

    Private Sub txtFind1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFind1.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnFind.PerformClick()
        End If
    End Sub

    Private Sub txtFind2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFind2.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLentOrReturn.PerformClick()
        End If
    End Sub
End Class