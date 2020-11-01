﻿Public Class Form2
    Dim isClickedSpawnBtn As Integer    '-1: 선택x,  0 :삽   1:첫번째스폰,  2:두번쨰스폰 ....

    Private moneyRainBtn(4) As Button
    Private sunflowerMoneyBtn(45) As Button
    Private mapBoardBtn(45) As Button

    Structure ActionTimerStruct                       '소환 시 시간에 따른 행동에 쓰일 구조체
        Dim index As Integer
        Dim countTime As Date
    End Structure
    Dim actionTimer As New ActionTimerStruct
    Dim sunflowerSpawnTimerList As New ArrayList()    '해바라기 돈 스폰 시간에 쓰일 리스트

    Dim plantPrice(4) As Integer

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        '버튼 동적생성, 기본 설정

        For index As Integer = 0 To 3
            moneyRainBtn(index) = New Button()
            With Me.moneyRainBtn(index)
                .Name = "moneyRainBtn" & CStr(index)
                .Location = New System.Drawing.Point(CInt(Int((900 - 433 + 1) * Rnd() + 433)), 100)
                .Size() = New System.Drawing.Size(68, 57)
                .BackgroundImageLayout = ImageLayout.Stretch
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.MouseDownBackColor = Color.Transparent
                .FlatAppearance.MouseOverBackColor = Color.Transparent
                .BackColor = Color.Transparent
                .BackgroundImage = sunImage.Image
                .TabStop = False
                .Visible = False
            End With
            AddHandler Me.moneyRainBtn(index).Click, AddressOf moneyRainBtn_Click
            Me.Controls.AddRange(New Button() {moneyRainBtn(index)})
            Controls.SetChildIndex(moneyRainBtn(index), 90)
        Next
        For index As Integer = 0 To 3
            With Me.moneyRainBtn(index)
                .Parent = backgroundImage
            End With
        Next
        Dim count As Integer
        count = 0
        For yIndex As Integer = 0 To 4
            For xIndex As Integer = 0 To 8
                sunflowerMoneyBtn(count) = New Button()
                With Me.sunflowerMoneyBtn(count)
                    .Name = "sunflowerMoneyBtn" & CStr(count)
                    .Location = New System.Drawing.Point(256 + (xIndex * 81), 86 + (yIndex * 100))
                    .Size() = New System.Drawing.Size(37, 33)
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 0
                    .FlatAppearance.MouseDownBackColor = Color.Transparent
                    .FlatAppearance.MouseOverBackColor = Color.Transparent
                    .BackColor = Color.Transparent
                    .BackgroundImage = sunImage.Image
                    .TabStop = False
                    .Visible = False
                End With
                AddHandler Me.sunflowerMoneyBtn(count).Click, AddressOf sunflowerMoneyBtn_Click
                Me.Controls.AddRange(New Button() {sunflowerMoneyBtn(count)})
                Controls.SetChildIndex(sunflowerMoneyBtn(count), 91)
                count += 1
            Next
        Next
        For index As Integer = 0 To 44 '위의 for문에서 parent 설정 시 오류.. 따로 parent 설정
            With Me.sunflowerMoneyBtn(index)
                .Parent = backgroundImage
            End With
        Next
        count = 0
        For yIndex As Integer = 0 To 4
            For xIndex As Integer = 0 To 8
                mapBoardBtn(count) = New Button()
                With Me.mapBoardBtn(count)
                    .Name = "mapBoardBtn" & CStr(count)
                    .Location = New System.Drawing.Point(256 + (xIndex * 81), 86 + (yIndex * 100))
                    .Size() = New System.Drawing.Size(75, 67)
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 0
                    .FlatAppearance.MouseDownBackColor = Color.Transparent
                    .FlatAppearance.MouseOverBackColor = Color.Transparent
                    .BackColor = Color.Transparent
                    .TabStop = False
                End With
                AddHandler Me.mapBoardBtn(count).Click, AddressOf mapBoardBtn_Click
                Me.Controls.AddRange(New Button() {mapBoardBtn(count)})
                Controls.SetChildIndex(mapBoardBtn(count), 92)
                count += 1
            Next
        Next
        For index As Integer = 0 To 44 '위의 for문에서 parent 설정 시 오류.. 따로 parent 설정
            With Me.mapBoardBtn(index)
                .Parent = backgroundImage
            End With
        Next


        Controls.SetChildIndex(backgroundImage, 100)    '배경 z 좌표 100으로 설정 (제일 뒤)

        isClickedSpawnBtn = -1                                  '스폰버튼 초기화

        leftTopUI.Parent = backgroundImage                      '좌측 상단 UI 흰부분 바탕 동기화
        sunImage.Parent = leftTopUI                             '좌측 상단 돈 UI 흰부분 바탕 동기화

        currentMoneyLabelUi.Text = 1000

        currentMoneyLabelUi.Parent = leftTopUI                  '돈 UI 흰부분 바탕 동기화
        plantPrice(0) = plantMoneyLabelUi1.Text                 '가격 설정
        plantPrice(1) = plantMoneyLabelUi2.Text
        plantPrice(2) = plantMoneyLabelUi3.Text
        plantPrice(3) = plantMoneyLabelUi4.Text

        moneySpawnTimer.Interval = CInt(Int((5000 - 3000 + 1) * Rnd() + 3000))
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

            '해바라기 삭제에 대한 코드 (리스트 삭제)
            For i As Integer = 0 To sunflowerSpawnTimerList.Count() - 1
                If sunflowerSpawnTimerList(i).index = findIndex Then
                    sunflowerSpawnTimerList.RemoveAt(i)
                    Exit For '삭제 후 다음 값 참조하면 sunflowerSpawnTimerList.Count()값 변경되어 outofindex 오류 발생 따라서 exit 해줌
                End If
            Next
        ElseIf (isClickedSpawnBtn = 1 And (sunflowerSpawnBtn.Enabled = True) And (mapBoardBtn(findIndex).BackgroundImage Is Nothing) And (CInt(currentMoneyLabelUi.Text) >= plantPrice(0))) Then    '해바라기 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(0)))
            mapBoardBtn(findIndex).BackgroundImage = sunflowerSpawnBtn.BackgroundImage
            sunflowerSpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant1spawTimer.Enabled = True

            actionTimer.index = findIndex   '구조체에 생성 시간 대입
            actionTimer.countTime = Now()
            sunflowerSpawnTimerList.Add(actionTimer)
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
    Private Sub sunflowerMoneyBtn_Click(sender As Object, e As EventArgs)
        Dim findIndex As Integer
        findIndex = 0
        For index As Integer = 0 To 44
            If (sender.Name = ("sunflowerMoneyBtn" & CStr(index))) Then
                Exit For
            Else
                findIndex += 1
            End If
        Next

        currentMoneyLabelUi.Text += 25
        sunflowerMoneyBtn(findIndex).Visible = False
    End Sub
    Private Sub moneyRainBtn_Click(sender As Object, e As EventArgs)
        Dim findIndex As Integer
        findIndex = 0
        For index As Integer = 0 To 3
            If (sender.Name = ("moneyRainBtn" & CStr(index))) Then
                Exit For
            Else
                findIndex += 1
            End If
        Next

        currentMoneyLabelUi.Text += 25
        moneyRainBtn(findIndex).Visible = False
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

    Private Sub moneyTimer_Tick(sender As Object, e As EventArgs) Handles moneyTimer.Tick
        For index As Integer = 0 To 3
            If moneyRainBtn(index).Visible = True Then
                moneyRainBtn(index).Location = New Point(moneyRainBtn(index).Location.X, moneyRainBtn(index).Location.Y + 1)
            End If
            If (moneyRainBtn(index).Visible = True) And (moneyRainBtn(index).Location.Y >= 577) Then
                moneyRainBtn(index).Visible = False
            End If
        Next
    End Sub

    Private Sub moneySpawnTimer_Tick(sender As Object, e As EventArgs) Handles moneySpawnTimer.Tick
        moneySpawnTimer.Interval = CInt(Int((9000 - 4000 + 1) * Rnd() + 4000))
        For index As Integer = 0 To 3
            If moneyRainBtn(index).Visible = False Then
                moneyRainBtn(index).Visible = True
                moneyRainBtn(index).Location = New Point(CInt(Int((900 - 433 + 1) * Rnd() + 433)), -54)
                Exit For
            End If
        Next
    End Sub

    Private Sub spawnSunflowerTimer_Tick(sender As Object, e As EventArgs) Handles spawnSunflowerTimer.Tick
        spawnSunflowerTimer.Interval = CInt(Int((100 - 50 + 1) * Rnd() + 50))
        For index As Integer = 0 To 44
            If mapBoardBtn(index).BackgroundImage Is sunflowerSpawnBtn.BackgroundImage Then
                For i As Integer = 0 To sunflowerSpawnTimerList.Count() - 1
                    If (sunflowerSpawnTimerList(i).index = index) And (DateDiff("s", sunflowerSpawnTimerList(i).countTime, Now()) > (CInt(Int((10 - 5 + 1) * Rnd() + 5)))) Then
                        Dim copyData As ActionTimerStruct
                        copyData.index = sunflowerSpawnTimerList(i).index   '삭제 안하고 list에 바로 접근하려 했으나 오류로 삭제 후 삽입 진행
                        copyData.countTime = Now()
                        sunflowerSpawnTimerList.RemoveAt(i)
                        sunflowerSpawnTimerList.Add(copyData)
                        sunflowerMoneyBtn(index).Visible = True
                    End If
                Next
            End If
        Next
    End Sub
End Class