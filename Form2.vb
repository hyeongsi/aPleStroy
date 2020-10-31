Public Class Form2
    Dim isClickedSpawnBtn As Integer    '-1: 선택x,  0 :삽   1:첫번째스폰,  2:두번쨰스폰 ....

    Dim moneyList As ArrayList      '돈비를 저장할 리스트
    Private moneyRainBtn As New Button()
    Private mapBoardBtn(45) As Button

    Dim plantPrice(4) As Integer

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        Dim count As Integer
        count = 0
        For yIndex As Integer = 0 To 4
            For xIndex As Integer = 0 To 8
                mapBoardBtn(count) = New Button()
                With Me.mapBoardBtn(count)
                    .Name = "mapBoardBtn" & CStr(count)
                    .Location = New System.Drawing.Point(256 + (xIndex * 81), 86 + (yIndex * 100))
                    .Size() = New System.Drawing.Size(75, 67)
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .Parent = backgroundImage
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 0
                    .FlatAppearance.MouseDownBackColor = Color.Transparent
                    .FlatAppearance.MouseOverBackColor = Color.Transparent
                    .BackColor = Color.Transparent

                    .TabStop = False
                End With
                AddHandler Me.mapBoardBtn(count).Click, AddressOf mapBoardBtn_Click
                Me.Controls.Add(mapBoardBtn(count))

                Controls.SetChildIndex(mapBoardBtn(count), 101)
                count += 1
            Next
        Next
        With Me.moneyRainBtn
            .Location = New System.Drawing.Point(CInt(Int((900 - 433 + 1) * Rnd() + 433)), 100) 'y-54로 설정
            .Size() = New System.Drawing.Size(68, 57)
            .BackgroundImage = sunImage.Image
            .BackgroundImageLayout = ImageLayout.Stretch
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.MouseDownBackColor = Color.Transparent
            .FlatAppearance.MouseOverBackColor = Color.Transparent
            .BackColor = Color.Transparent
            .Parent = backgroundImage
            .TabStop = False
        End With
        AddHandler Me.moneyRainBtn.Click, AddressOf moneyRainBtn_Click

        Controls.SetChildIndex(backgroundImage, 100)    '배경 z 좌표 100으로 설정 (제일 뒤)

        currentMoneyLabelUi.Text = 1000
        isClickedSpawnBtn = -1                                  '스폰버튼 초기화

        leftTopUI.Parent = backgroundImage                      '좌측 상단 UI 흰부분 바탕 동기화
        sunImage.Parent = leftTopUI                             '좌측 상단 돈 UI 흰부분 바탕 동기화

        currentMoneyLabelUi.Parent = leftTopUI                  '돈 UI 흰부분 바탕 동기화
        plantPrice(0) = plantMoneyLabelUi1.Text                 '가격 설정
        plantPrice(1) = plantMoneyLabelUi2.Text
        plantPrice(2) = plantMoneyLabelUi3.Text
        plantPrice(3) = plantMoneyLabelUi4.Text
        'moneyTimer.Enabled = True                                      '돈 스폰시작!!
    End Sub
    Private Sub moneyTimer_Tick(sender As Object, e As EventArgs) Handles moneyTimer.Tick
        moneyTimer.Interval = CInt(Int((5000 - 1000 + 1) * Rnd() + 1000))
        Me.Controls.Add(moneyRainBtn)
    End Sub
    Private Sub spawnBtn_Click(sender As Object, e As EventArgs) Handles sunflowerSpawnBtn.Click, plant1SpawnBtn.Click, plant2SpawnBtn.Click, plant3SpawnBtn.Click
        If (sender.Name = sunflowerSpawnBtn.Name) And (CInt(currentMoneyLabelUi.Text) >= plantPrice(0)) And (sunflowerSpawnBtn.BackColor <> Color.Red) Then
            isClickedSpawnBtn = 1
            clearSelectColor()
            sunflowerSpawnBtn.BackColor = Color.GreenYellow
        End If
        If (sender.Name = plant1SpawnBtn.Name) And CInt(currentMoneyLabelUi.Text) >= plantPrice(1) And (plant1SpawnBtn.BackColor <> Color.Red) Then
            isClickedSpawnBtn = 2
            clearSelectColor()
            plant1SpawnBtn.BackColor = Color.GreenYellow
        End If
        If (sender.Name = plant2SpawnBtn.Name) And CInt(currentMoneyLabelUi.Text) >= plantPrice(2) And (plant2SpawnBtn.BackColor <> Color.Red) Then
            isClickedSpawnBtn = 3
            clearSelectColor()
            plant2SpawnBtn.BackColor = Color.GreenYellow
        End If
        If (sender.Name = plant3SpawnBtn.Name) And CInt(currentMoneyLabelUi.Text) >= plantPrice(3) And (plant3SpawnBtn.BackColor <> Color.Red) Then
            isClickedSpawnBtn = 4
            clearSelectColor()
            plant3SpawnBtn.BackColor = Color.GreenYellow
        End If
    End Sub

    Private Sub mapBoardBtn_Click(sender As Object, e As EventArgs) 'byval 이라서 sender 바로 사용 못함.
        Dim findIndex As Integer
        findIndex = 0
        For yIndex As Integer = 0 To 44
            If (sender.Name = ("mapBoardBtn" & CStr(findIndex))) Then
                Exit For
            Else
                findIndex += 1
            End If
        Next

        If (isClickedSpawnBtn = 0 And (mapBoardBtn(findIndex).BackgroundImage IsNot Nothing)) Then       '삽 선택
            mapBoardBtn(findIndex).BackgroundImage = Nothing
            shovelBtn.BackColor = Color.Transparent
            isClickedSpawnBtn = -1
        ElseIf (isClickedSpawnBtn = 1 And (sunflowerSpawnBtn.Enabled = True) And (mapBoardBtn(findIndex).BackgroundImage Is Nothing) And (CInt(currentMoneyLabelUi.Text) >= plantPrice(0))) Then    '해바라기 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(0)))
            mapBoardBtn(findIndex).BackgroundImage = sunflowerSpawnBtn.BackgroundImage
            sunflowerSpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant1spawTimer.Enabled = True
        ElseIf (isClickedSpawnBtn = 2 And (plant1SpawnBtn.Enabled = True) And (mapBoardBtn(findIndex).BackgroundImage Is Nothing And (CInt(currentMoneyLabelUi.Text) >= plantPrice(1)))) Then       '식물1 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(1)))
            mapBoardBtn(findIndex).BackgroundImage = plant1SpawnBtn.BackgroundImage
            plant1SpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant2spawTimer.Enabled = True
        ElseIf (isClickedSpawnBtn = 3 And (plant2SpawnBtn.Enabled = True) And (mapBoardBtn(findIndex).BackgroundImage Is Nothing And (CInt(currentMoneyLabelUi.Text) >= plantPrice(2)))) Then       '식물2 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(2)))
            mapBoardBtn(findIndex).BackgroundImage = plant2SpawnBtn.BackgroundImage
            plant2SpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant3spawTimer.Enabled = True
        ElseIf (isClickedSpawnBtn = 4 And (plant3SpawnBtn.Enabled = True) And (mapBoardBtn(findIndex).BackgroundImage Is Nothing And (CInt(currentMoneyLabelUi.Text) >= plantPrice(3)))) Then       '식물3 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(3)))
            mapBoardBtn(findIndex).BackgroundImage = plant3SpawnBtn.BackgroundImage
            plant3SpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant4spawTimer.Enabled = True
        End If
    End Sub

    Private Sub shovelBtn_Click(sender As Object, e As EventArgs) Handles shovelBtn.Click
        isClickedSpawnBtn = 0
        shovelBtn.BackColor = Color.GreenYellow
    End Sub
    Private Sub moneyRainBtn_Click(sender As Object, e As EventArgs)
        currentMoneyLabelUi.Text = CStr(CInt(currentMoneyLabelUi.Text) + 25)
        Me.Controls.Remove(moneyRainBtn)
    End Sub
    Private Sub clearSelectColor()
        If sunflowerSpawnBtn.BackColor = Color.GreenYellow Then
            sunflowerSpawnBtn.BackColor = Color.Transparent
        End If
        If plant1SpawnBtn.BackColor = Color.GreenYellow Then
            plant1SpawnBtn.BackColor = Color.Transparent
        End If
        If plant2SpawnBtn.BackColor = Color.GreenYellow Then
            plant2SpawnBtn.BackColor = Color.Transparent
        End If
        If plant3SpawnBtn.BackColor = Color.GreenYellow Then
            plant3SpawnBtn.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub plant1spawTimer_Tick(sender As Object, e As EventArgs) Handles plant1spawTimer.Tick
        If sunflowerSpawnBtn.BackColor = Color.Red Then
            sunflowerSpawnBtn.BackColor = Color.Transparent
            plant1spawTimer.Enabled = False
        End If
    End Sub

    Private Sub plant2spawTimer_Tick(sender As Object, e As EventArgs) Handles plant2spawTimer.Tick
        If plant1SpawnBtn.BackColor = Color.Red Then
            plant1SpawnBtn.BackColor = Color.Transparent
            plant2spawTimer.Enabled = False
        End If
    End Sub

    Private Sub plant3spawTimer_Tick(sender As Object, e As EventArgs) Handles plant3spawTimer.Tick
        If plant2SpawnBtn.BackColor = Color.Red Then
            plant2SpawnBtn.BackColor = Color.Transparent
            plant3spawTimer.Enabled = False
        End If
    End Sub

    Private Sub plant4spawTimer_Tick(sender As Object, e As EventArgs) Handles plant4spawTimer.Tick
        If plant3SpawnBtn.BackColor = Color.Red Then
            plant3SpawnBtn.BackColor = Color.Transparent
            plant4spawTimer.Enabled = False
        End If
    End Sub
End Class