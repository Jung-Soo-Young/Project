Public Class memid
    Dim ds As DataSet

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        ds = New DataSet
        If Trim(txtID.Text) = "" Then
            MsgBox("ID를 입력하세요", , "확인")
            txtID.Text = ""
            txtID.Focus()

        Else
            Ret = MsgBox("해당 도서를 대여하시겠습니까?", vbYesNo, "확인")

            If Ret = vbYes Then
                SQL = "Select * From member Where m_id = '" & txtID.Text & "'"
                DCom.CommandText = SQL
                DA = New OleDb.OleDbDataAdapter(SQL, Con)
                DA.Fill(ds, "member")

                If ds.Tables("member").Rows.Count = 0 Then
                    MsgBox("등록된 회원이 아닙니다.", vbOKOnly, "확인")
                    txtID.Text = ""
                    txtID.Focus()
                Else
                    Call P_BookLent(txtID.Text, main.txtFind2.Text)
                    Call main.list_search("", "")

                    MsgBox("대여되었습니다." & vbCrLf & "반납일을 꼭 지켜주세요", vbOKOnly, "확인")
                    Call main.lent_id()
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub txtID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtID.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOK.PerformClick()
        End If
    End Sub

    Public Sub P_BookLent(ByVal Mid As String, ByVal Bcode As String)
        ds = New DataSet
        SQL = "Update member Set "
        SQL = SQL & "m_lent = m_lent + 1"
        SQL = SQL & ", m_total_lent = m_total_lent + 1"
        SQL = SQL & " Where m_id = '" & Mid & "'"
        DCom.CommandText = SQL
        DCom.ExecuteNonQuery()
        DA.Fill(ds, "member")

        SQL = "Update book Set "
        SQL = SQL & "b_lent = '" & Mid & "'"
        SQL = SQL & ", b_lent_Date = '" & Now() & "'"
        SQL = SQL & ", b_lent_num = b_lent_num + 1"
        SQL = SQL & " Where b_code = '" & Bcode & "'"
        DCom.CommandText = SQL
        DCom.ExecuteNonQuery()
        DA.Fill(ds, "book")
    End Sub

    Private Sub memid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class