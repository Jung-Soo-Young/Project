<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.도서관리ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.회원관리ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.정보ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.종료ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.도서관리ToolStripMenuItem, Me.회원관리ToolStripMenuItem, Me.정보ToolStripMenuItem, Me.종료ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1042, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '도서관리ToolStripMenuItem
        '
        Me.도서관리ToolStripMenuItem.Name = "도서관리ToolStripMenuItem"
        Me.도서관리ToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.도서관리ToolStripMenuItem.Text = "도서 관리"
        Me.도서관리ToolStripMenuItem.ToolTipText = "도서를 관리합니다."
        '
        '회원관리ToolStripMenuItem
        '
        Me.회원관리ToolStripMenuItem.Name = "회원관리ToolStripMenuItem"
        Me.회원관리ToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.회원관리ToolStripMenuItem.Text = "회원 관리"
        Me.회원관리ToolStripMenuItem.ToolTipText = "회원을 관리합니다."
        '
        '정보ToolStripMenuItem
        '
        Me.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem"
        Me.정보ToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.정보ToolStripMenuItem.Text = "정보"
        Me.정보ToolStripMenuItem.ToolTipText = "정보를 확인합니다."
        '
        '종료ToolStripMenuItem
        '
        Me.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem"
        Me.종료ToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.종료ToolStripMenuItem.Text = "종료"
        Me.종료ToolStripMenuItem.ToolTipText = "프로그램을 종료합니다."
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 587)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "도서 관리"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 도서관리ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 회원관리ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 정보ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 종료ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
