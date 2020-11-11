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
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.startBtn = New System.Windows.Forms.Button()
        Me.explainBtn = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'startBtn
        '
        Me.startBtn.Location = New System.Drawing.Point(204, 351)
        Me.startBtn.Name = "startBtn"
        Me.startBtn.Size = New System.Drawing.Size(75, 23)
        Me.startBtn.TabIndex = 1
        Me.startBtn.Text = "시작"
        Me.startBtn.UseVisualStyleBackColor = True
        '
        'explainBtn
        '
        Me.explainBtn.Location = New System.Drawing.Point(285, 351)
        Me.explainBtn.Name = "explainBtn"
        Me.explainBtn.Size = New System.Drawing.Size(75, 23)
        Me.explainBtn.TabIndex = 1
        Me.explainBtn.Text = "설명"
        Me.explainBtn.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 33
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.visual_basic_fianls_project.My.Resources.Resources.메뉴화면
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(579, 411)
        Me.Controls.Add(Me.explainBtn)
        Me.Controls.Add(Me.startBtn)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.Text = "PLANTS vs ZOMBIES"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents startBtn As Button
    Friend WithEvents explainBtn As Button
    Friend WithEvents Timer1 As Timer
End Class
