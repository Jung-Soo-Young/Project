Public Class inforbook
    Dim ds As DataSet

    Private Sub inforbook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        infor_book(Label2.Text)
    End Sub

    Public Sub infor_book(ByVal book_code As String)
        ds = New DataSet
        SQL = "Select * From book Where b_code = '" & book_code & "'"
        DCom.CommandText = SQL
        DA = New OleDb.OleDbDataAdapter(SQL, Con)
        DA.Fill(ds, "book")

        Label4.Text = ds.Tables("book").Rows(0).Item(1)
        Label6.Text = ds.Tables("book").Rows(0).Item(2)
        Label8.Text = ds.Tables("book").Rows(0).Item(3)
        Label10.Text = ds.Tables("book").Rows(0).Item(4)
        Label12.Text = ds.Tables("book").Rows(0).Item(5)
        If ds.Tables("book").Rows(0).Item(6).ToString Like "0" Then
            Label14.Text = "대여 가능"
        Else
            Label14.Text = "대여 불가능(현재 대여 ID : " & ds.Tables("book").Rows(0).Item(6).ToString & ")"
            TextBox1.Text = "대여 불가능"
            btnLent.Enabled = False
            TextBox1.Enabled = False
        End If
        If ds.Tables("book").Rows(0).Item(8).ToString <> "" Then
            Label16.Text = Strings.Left(ds.Tables("book").Rows(0).Item(8).ToString, 10)
            Label18.Text = Strings.Left(DateAdd("d", 10, ds.Tables("book").Rows(0).Item(8)), 10)
        Else
            Label16.Text = ""
            Label18.Text = ""
        End If
        Label20.Text = ds.Tables("book").Rows(0).Item(7)
    End Sub

    Private Sub btnLent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLent.Click
        ds = New DataSet
        Ret = MsgBox("해당 도서를 대여하시겠습니까?", vbYesNo, "확인")
        If Ret = vbYes Then
            SQL = "Select * From member Where m_id = '" & TextBox1.Text & "'"
            DCom.CommandText = SQL
            DA = New OleDb.OleDbDataAdapter(SQL, Con)
            DA.Fill(ds, "member")

            If ds.Tables("member").Rows.Count = 0 Then
                MsgBox("등록된 회원이 아닙니다", vbOKOnly, "확인")
                TextBox1.Text = ""
                TextBox1.Focus()
            Else
                Call memid.P_BookLent(TextBox1.Text, Label2.Text)
                Call main.list_search("", "")
                Call main.lent_id()
                Call information.information()
                MsgBox("대여되었습니다." & vbCrLf & "반납일을 꼭 지켜주세요", , "확인")
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLent.PerformClick()
        End If
    End Sub
End Class