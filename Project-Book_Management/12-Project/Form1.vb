Public Class Form1
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '[도서 관리(메인)], [도서 관리], [회원 관리], [정보] 폼을 자식 폼으로 설정한다.
        main.MdiParent = Me
        book.MdiParent = Me
        member.MdiParent = Me
        information.MdiParent = Me

        DB_Access() '모듈에서 설정한 데이터베이스 연결 프로시저를 실행한다.
        main.Show() '도서 관리(메인) 폼을 연다.
    End Sub

    '[도서 관리] 메뉴를 클릭하면 도서 관리 폼을 연다.
    Private Sub 도서관리ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 도서관리ToolStripMenuItem.Click
        book.Show()
    End Sub
    '[회원 관리] 메뉴를 클릭하면 회원 관리 폼을 연다.
    Private Sub 회원관리ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 회원관리ToolStripMenuItem.Click
        member.Show()
    End Sub

    '[정보] 메뉴를 클릭하면 정보 폼을 연다.
    Private Sub 정보ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 정보ToolStripMenuItem.Click
        information.Show()
    End Sub
    '[종료] 메뉴를 클릭하면 프로그램을 종료한다.
    Private Sub 종료ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 종료ToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
End Class
