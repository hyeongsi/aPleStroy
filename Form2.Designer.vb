<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.backgroundImage = New System.Windows.Forms.PictureBox()
        Me.shovelBtn = New System.Windows.Forms.Button()
        Me.leftTopUI = New System.Windows.Forms.PictureBox()
        Me.currentMoneyLabelUi = New System.Windows.Forms.Label()
        Me.sunflowerSpawnBtn = New System.Windows.Forms.Button()
        Me.plant1SpawnBtn = New System.Windows.Forms.Button()
        Me.plant2SpawnBtn = New System.Windows.Forms.Button()
        Me.plant3SpawnBtn = New System.Windows.Forms.Button()
        Me.sunflowerMoneyLabelUi = New System.Windows.Forms.Label()
        Me.mapBoardBtn00 = New System.Windows.Forms.Button()
        Me.moneyTimer = New System.Windows.Forms.Timer(Me.components)
        Me.sunImage = New System.Windows.Forms.PictureBox()
        CType(Me.backgroundImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.leftTopUI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sunImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'backgroundImage
        '
        Me.backgroundImage.Image = CType(resources.GetObject("backgroundImage.Image"), System.Drawing.Image)
        Me.backgroundImage.Location = New System.Drawing.Point(0, 0)
        Me.backgroundImage.Name = "backgroundImage"
        Me.backgroundImage.Size = New System.Drawing.Size(1024, 626)
        Me.backgroundImage.TabIndex = 0
        Me.backgroundImage.TabStop = False
        '
        'shovelBtn
        '
        Me.shovelBtn.BackgroundImage = CType(resources.GetObject("shovelBtn.BackgroundImage"), System.Drawing.Image)
        Me.shovelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.shovelBtn.Location = New System.Drawing.Point(370, 0)
        Me.shovelBtn.Name = "shovelBtn"
        Me.shovelBtn.Size = New System.Drawing.Size(67, 69)
        Me.shovelBtn.TabIndex = 1
        Me.shovelBtn.UseVisualStyleBackColor = True
        '
        'leftTopUI
        '
        Me.leftTopUI.Image = CType(resources.GetObject("leftTopUI.Image"), System.Drawing.Image)
        Me.leftTopUI.Location = New System.Drawing.Point(0, 0)
        Me.leftTopUI.Name = "leftTopUI"
        Me.leftTopUI.Size = New System.Drawing.Size(355, 80)
        Me.leftTopUI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.leftTopUI.TabIndex = 2
        Me.leftTopUI.TabStop = False
        '
        'currentMoneyLabelUi
        '
        Me.currentMoneyLabelUi.AutoSize = True
        Me.currentMoneyLabelUi.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.currentMoneyLabelUi.Location = New System.Drawing.Point(29, 62)
        Me.currentMoneyLabelUi.Name = "currentMoneyLabelUi"
        Me.currentMoneyLabelUi.Size = New System.Drawing.Size(16, 16)
        Me.currentMoneyLabelUi.TabIndex = 4
        Me.currentMoneyLabelUi.Text = "0"
        Me.currentMoneyLabelUi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'sunflowerSpawnBtn
        '
        Me.sunflowerSpawnBtn.BackgroundImage = CType(resources.GetObject("sunflowerSpawnBtn.BackgroundImage"), System.Drawing.Image)
        Me.sunflowerSpawnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.sunflowerSpawnBtn.Location = New System.Drawing.Point(67, 6)
        Me.sunflowerSpawnBtn.Name = "sunflowerSpawnBtn"
        Me.sunflowerSpawnBtn.Size = New System.Drawing.Size(68, 57)
        Me.sunflowerSpawnBtn.TabIndex = 5
        Me.sunflowerSpawnBtn.UseVisualStyleBackColor = True
        '
        'plant1SpawnBtn
        '
        Me.plant1SpawnBtn.BackgroundImage = CType(resources.GetObject("plant1SpawnBtn.BackgroundImage"), System.Drawing.Image)
        Me.plant1SpawnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plant1SpawnBtn.Location = New System.Drawing.Point(137, 6)
        Me.plant1SpawnBtn.Name = "plant1SpawnBtn"
        Me.plant1SpawnBtn.Size = New System.Drawing.Size(68, 57)
        Me.plant1SpawnBtn.TabIndex = 6
        Me.plant1SpawnBtn.UseVisualStyleBackColor = True
        '
        'plant2SpawnBtn
        '
        Me.plant2SpawnBtn.BackgroundImage = CType(resources.GetObject("plant2SpawnBtn.BackgroundImage"), System.Drawing.Image)
        Me.plant2SpawnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plant2SpawnBtn.Location = New System.Drawing.Point(207, 6)
        Me.plant2SpawnBtn.Name = "plant2SpawnBtn"
        Me.plant2SpawnBtn.Size = New System.Drawing.Size(68, 57)
        Me.plant2SpawnBtn.TabIndex = 7
        Me.plant2SpawnBtn.UseVisualStyleBackColor = True
        '
        'plant3SpawnBtn
        '
        Me.plant3SpawnBtn.BackgroundImage = CType(resources.GetObject("plant3SpawnBtn.BackgroundImage"), System.Drawing.Image)
        Me.plant3SpawnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plant3SpawnBtn.Location = New System.Drawing.Point(277, 6)
        Me.plant3SpawnBtn.Name = "plant3SpawnBtn"
        Me.plant3SpawnBtn.Size = New System.Drawing.Size(68, 57)
        Me.plant3SpawnBtn.TabIndex = 8
        Me.plant3SpawnBtn.UseVisualStyleBackColor = True
        '
        'sunflowerMoneyLabelUi
        '
        Me.sunflowerMoneyLabelUi.AutoSize = True
        Me.sunflowerMoneyLabelUi.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.sunflowerMoneyLabelUi.Location = New System.Drawing.Point(106, 44)
        Me.sunflowerMoneyLabelUi.Name = "sunflowerMoneyLabelUi"
        Me.sunflowerMoneyLabelUi.Size = New System.Drawing.Size(29, 19)
        Me.sunflowerMoneyLabelUi.TabIndex = 9
        Me.sunflowerMoneyLabelUi.Text = "50"
        '
        'mapBoardBtn00
        '
        Me.mapBoardBtn00.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.mapBoardBtn00.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.mapBoardBtn00.FlatAppearance.BorderSize = 0
        Me.mapBoardBtn00.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.mapBoardBtn00.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.mapBoardBtn00.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.mapBoardBtn00.Location = New System.Drawing.Point(256, 86)
        Me.mapBoardBtn00.Name = "mapBoardBtn00"
        Me.mapBoardBtn00.Size = New System.Drawing.Size(75, 67)
        Me.mapBoardBtn00.TabIndex = 10
        Me.mapBoardBtn00.UseVisualStyleBackColor = True
        '
        'moneyTimer
        '
        '
        'sunImage
        '
        Me.sunImage.Image = CType(resources.GetObject("sunImage.Image"), System.Drawing.Image)
        Me.sunImage.Location = New System.Drawing.Point(8, 3)
        Me.sunImage.Name = "sunImage"
        Me.sunImage.Size = New System.Drawing.Size(60, 60)
        Me.sunImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.sunImage.TabIndex = 11
        Me.sunImage.TabStop = False
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 587)
        Me.Controls.Add(Me.sunImage)
        Me.Controls.Add(Me.mapBoardBtn00)
        Me.Controls.Add(Me.sunflowerMoneyLabelUi)
        Me.Controls.Add(Me.plant3SpawnBtn)
        Me.Controls.Add(Me.plant2SpawnBtn)
        Me.Controls.Add(Me.plant1SpawnBtn)
        Me.Controls.Add(Me.sunflowerSpawnBtn)
        Me.Controls.Add(Me.currentMoneyLabelUi)
        Me.Controls.Add(Me.leftTopUI)
        Me.Controls.Add(Me.shovelBtn)
        Me.Controls.Add(Me.backgroundImage)
        Me.Name = "Form2"
        Me.Text = "PLANTS vs ZOMBIES"
        CType(Me.backgroundImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.leftTopUI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sunImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents backgroundImage As PictureBox
    Friend WithEvents shovelBtn As Button
    Friend WithEvents leftTopUI As PictureBox
    Friend WithEvents currentMoneyLabelUi As Label
    Friend WithEvents sunflowerSpawnBtn As Button
    Friend WithEvents plant1SpawnBtn As Button
    Friend WithEvents plant2SpawnBtn As Button
    Friend WithEvents plant3SpawnBtn As Button
    Friend WithEvents sunflowerMoneyLabelUi As Label
    Friend WithEvents mapBoardBtn00 As Button
    Friend WithEvents moneyTimer As Timer
    Friend WithEvents sunImage As PictureBox
End Class
