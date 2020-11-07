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
        Me.plantMoneyLabelUi1 = New System.Windows.Forms.Label()
        Me.moneyMoveTimer = New System.Windows.Forms.Timer(Me.components)
        Me.sunImage = New System.Windows.Forms.PictureBox()
        Me.plantMoneyLabelUi2 = New System.Windows.Forms.Label()
        Me.plantMoneyLabelUi3 = New System.Windows.Forms.Label()
        Me.plantMoneyLabelUi4 = New System.Windows.Forms.Label()
        Me.plant1spawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.plant2spawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.plant3spawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.plant4spawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.moneyRainSpawnTimer = New System.Windows.Forms.Timer(Me.components)
        Me.spawnSunflowerMoneyTimer = New System.Windows.Forms.Timer(Me.components)
        Me.zombiesMoveTimer = New System.Windows.Forms.Timer(Me.components)
        Me.zombiesSpawnTimer = New System.Windows.Forms.Timer(Me.components)
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
        Me.shovelBtn.BackColor = System.Drawing.Color.Transparent
        Me.shovelBtn.BackgroundImage = CType(resources.GetObject("shovelBtn.BackgroundImage"), System.Drawing.Image)
        Me.shovelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.shovelBtn.Location = New System.Drawing.Point(370, 0)
        Me.shovelBtn.Name = "shovelBtn"
        Me.shovelBtn.Size = New System.Drawing.Size(67, 69)
        Me.shovelBtn.TabIndex = 1
        Me.shovelBtn.TabStop = False
        Me.shovelBtn.UseVisualStyleBackColor = False
        '
        'leftTopUI
        '
        Me.leftTopUI.BackColor = System.Drawing.Color.Transparent
        Me.leftTopUI.BackgroundImage = CType(resources.GetObject("leftTopUI.BackgroundImage"), System.Drawing.Image)
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
        Me.sunflowerSpawnBtn.TabStop = False
        Me.sunflowerSpawnBtn.UseVisualStyleBackColor = True
        '
        'plant1SpawnBtn
        '
        Me.plant1SpawnBtn.BackColor = System.Drawing.SystemColors.Control
        Me.plant1SpawnBtn.BackgroundImage = CType(resources.GetObject("plant1SpawnBtn.BackgroundImage"), System.Drawing.Image)
        Me.plant1SpawnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plant1SpawnBtn.Location = New System.Drawing.Point(137, 6)
        Me.plant1SpawnBtn.Name = "plant1SpawnBtn"
        Me.plant1SpawnBtn.Size = New System.Drawing.Size(68, 57)
        Me.plant1SpawnBtn.TabIndex = 6
        Me.plant1SpawnBtn.TabStop = False
        Me.plant1SpawnBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.plant1SpawnBtn.UseVisualStyleBackColor = False
        '
        'plant2SpawnBtn
        '
        Me.plant2SpawnBtn.BackgroundImage = CType(resources.GetObject("plant2SpawnBtn.BackgroundImage"), System.Drawing.Image)
        Me.plant2SpawnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plant2SpawnBtn.Location = New System.Drawing.Point(207, 6)
        Me.plant2SpawnBtn.Name = "plant2SpawnBtn"
        Me.plant2SpawnBtn.Size = New System.Drawing.Size(68, 57)
        Me.plant2SpawnBtn.TabIndex = 7
        Me.plant2SpawnBtn.TabStop = False
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
        Me.plant3SpawnBtn.TabStop = False
        Me.plant3SpawnBtn.UseVisualStyleBackColor = True
        '
        'plantMoneyLabelUi1
        '
        Me.plantMoneyLabelUi1.AutoSize = True
        Me.plantMoneyLabelUi1.BackColor = System.Drawing.SystemColors.Control
        Me.plantMoneyLabelUi1.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.plantMoneyLabelUi1.Location = New System.Drawing.Point(106, 44)
        Me.plantMoneyLabelUi1.Name = "plantMoneyLabelUi1"
        Me.plantMoneyLabelUi1.Size = New System.Drawing.Size(29, 19)
        Me.plantMoneyLabelUi1.TabIndex = 9
        Me.plantMoneyLabelUi1.Text = "50"
        '
        'moneyMoveTimer
        '
        Me.moneyMoveTimer.Enabled = True
        Me.moneyMoveTimer.Interval = 14
        '
        'sunImage
        '
        Me.sunImage.BackColor = System.Drawing.Color.Transparent
        Me.sunImage.Image = CType(resources.GetObject("sunImage.Image"), System.Drawing.Image)
        Me.sunImage.Location = New System.Drawing.Point(8, 3)
        Me.sunImage.Name = "sunImage"
        Me.sunImage.Size = New System.Drawing.Size(60, 60)
        Me.sunImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.sunImage.TabIndex = 11
        Me.sunImage.TabStop = False
        '
        'plantMoneyLabelUi2
        '
        Me.plantMoneyLabelUi2.AutoSize = True
        Me.plantMoneyLabelUi2.BackColor = System.Drawing.SystemColors.Control
        Me.plantMoneyLabelUi2.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.plantMoneyLabelUi2.Location = New System.Drawing.Point(166, 44)
        Me.plantMoneyLabelUi2.Name = "plantMoneyLabelUi2"
        Me.plantMoneyLabelUi2.Size = New System.Drawing.Size(39, 19)
        Me.plantMoneyLabelUi2.TabIndex = 12
        Me.plantMoneyLabelUi2.Text = "100"
        '
        'plantMoneyLabelUi3
        '
        Me.plantMoneyLabelUi3.AutoSize = True
        Me.plantMoneyLabelUi3.BackColor = System.Drawing.SystemColors.Control
        Me.plantMoneyLabelUi3.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.plantMoneyLabelUi3.Location = New System.Drawing.Point(236, 44)
        Me.plantMoneyLabelUi3.Name = "plantMoneyLabelUi3"
        Me.plantMoneyLabelUi3.Size = New System.Drawing.Size(39, 19)
        Me.plantMoneyLabelUi3.TabIndex = 13
        Me.plantMoneyLabelUi3.Text = "150"
        '
        'plantMoneyLabelUi4
        '
        Me.plantMoneyLabelUi4.AutoSize = True
        Me.plantMoneyLabelUi4.BackColor = System.Drawing.SystemColors.Control
        Me.plantMoneyLabelUi4.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.plantMoneyLabelUi4.Location = New System.Drawing.Point(306, 44)
        Me.plantMoneyLabelUi4.Name = "plantMoneyLabelUi4"
        Me.plantMoneyLabelUi4.Size = New System.Drawing.Size(39, 19)
        Me.plantMoneyLabelUi4.TabIndex = 14
        Me.plantMoneyLabelUi4.Text = "200"
        '
        'plant1spawTimer
        '
        Me.plant1spawTimer.Interval = 1000
        '
        'plant2spawTimer
        '
        Me.plant2spawTimer.Interval = 2000
        '
        'plant3spawTimer
        '
        Me.plant3spawTimer.Interval = 3000
        '
        'plant4spawTimer
        '
        Me.plant4spawTimer.Interval = 4000
        '
        'moneyRainSpawnTimer
        '
        Me.moneyRainSpawnTimer.Enabled = True
        Me.moneyRainSpawnTimer.Interval = 50
        '
        'spawnSunflowerMoneyTimer
        '
        Me.spawnSunflowerMoneyTimer.Enabled = True
        Me.spawnSunflowerMoneyTimer.Interval = 1010
        '
        'zombiesMoveTimer
        '
        Me.zombiesMoveTimer.Enabled = True
        Me.zombiesMoveTimer.Interval = 56
        '
        'zombiesSpawnTimer
        '
        Me.zombiesSpawnTimer.Enabled = True
        Me.zombiesSpawnTimer.Interval = 10000
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 587)
        Me.Controls.Add(Me.plantMoneyLabelUi4)
        Me.Controls.Add(Me.plantMoneyLabelUi3)
        Me.Controls.Add(Me.plantMoneyLabelUi2)
        Me.Controls.Add(Me.sunImage)
        Me.Controls.Add(Me.plantMoneyLabelUi1)
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
    Friend WithEvents plantMoneyLabelUi1 As Label
    Friend WithEvents moneyMoveTimer As Timer
    Friend WithEvents sunImage As PictureBox
    Friend WithEvents plantMoneyLabelUi2 As Label
    Friend WithEvents plantMoneyLabelUi3 As Label
    Friend WithEvents plantMoneyLabelUi4 As Label
    Friend WithEvents plant1spawTimer As Timer
    Friend WithEvents plant2spawTimer As Timer
    Friend WithEvents plant3spawTimer As Timer
    Friend WithEvents plant4spawTimer As Timer
    Friend WithEvents moneyRainSpawnTimer As Timer
    Friend WithEvents spawnSunflowerMoneyTimer As Timer
    Friend WithEvents zombiesMoveTimer As Timer
    Friend WithEvents zombiesSpawnTimer As Timer
End Class
