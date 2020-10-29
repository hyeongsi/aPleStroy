Public Class Form2
    Dim isClickedSpawnBtn As Integer    '-1: 선택x,  0 :삽   1:첫번째스폰,  2:두번쨰스폰 ....

    Dim moneyList As ArrayList      '돈비를 저장할 리스트
    Private moneyRainBtn As New Button()     'money rain~~!!

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        'CInt(Int((900 - 433 + 1) * Rnd() + 433))
        With Me.moneyRainBtn
            .Text = "버튼"
            .Location = New System.Drawing.Point(500, 500) 'y-54로 설정
            .Size() = New System.Drawing.Size(68, 57)
        End With
        AddHandler Me.moneyRainBtn.Click, AddressOf moneyRainBtn_Click

        currentMoneyLabelUi.Text = 1000
        isClickedSpawnBtn = -1                                  '스폰버튼 초기화

        leftTopUI.BackColor = Color.Transparent                 '좌측 상단 UI 흰부분 바탕 동기화
        leftTopUI.Parent = backgroundImage

        sunImage.BackColor = Color.Transparent                 '좌측 상단 돈 UI 흰부분 바탕 동기화
        sunImage.Parent = leftTopUI

        currentMoneyLabelUi.BackColor = Color.Transparent       '돈 UI 흰부분 바탕 동기화
        currentMoneyLabelUi.Parent = leftTopUI

        mapBoardBtn00.BackColor = Color.Transparent             '맵보드 흰부분 바탕 동기화
        mapBoardBtn00.Parent = backgroundImage

        moneyTimer.Interval = CInt(Int((5000 - 1000 + 1) * Rnd() + 1000))
        moneyTimer.Enabled = True                                      '돈 스폰시작!!

    End Sub
    Private Sub moneyTimer_Tick(sender As Object, e As EventArgs) Handles moneyTimer.Tick
        moneyTimer.Interval = CInt(Int((5000 - 1000 + 1) * Rnd() + 1000))

    End Sub
    Private Sub sunflowerSpawnBtn_Click(sender As Object, e As EventArgs) Handles sunflowerSpawnBtn.Click
        If CInt(currentMoneyLabelUi.Text) >= 50 Then
            isClickedSpawnBtn = 1
        Else
            isClickedSpawnBtn = -1
        End If
    End Sub

    Private Sub mapBoardBtn00_Click(sender As Object, e As EventArgs) Handles mapBoardBtn00.Click

        If isClickedSpawnBtn = 0 And (mapBoardBtn00.BackgroundImage IsNot Nothing) Then       '삽 선택
            mapBoardBtn00.BackgroundImage = Nothing
        ElseIf isClickedSpawnBtn = 1 And (sunflowerSpawnBtn.Enabled = True) And (mapBoardBtn00.BackgroundImage Is Nothing) Then    '해바라기 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - 50))
            mapBoardBtn00.BackgroundImage = sunflowerSpawnBtn.BackgroundImage
        ElseIf isClickedSpawnBtn = 2 And (plant1SpawnBtn.Enabled = True) And (mapBoardBtn00.BackgroundImage Is Nothing) Then       '식물1 선택
            mapBoardBtn00.BackgroundImage = plant1SpawnBtn.BackgroundImage
        ElseIf isClickedSpawnBtn = 3 And (plant2SpawnBtn.Enabled = True) And (mapBoardBtn00.BackgroundImage Is Nothing) Then       '식물2 선택
            mapBoardBtn00.BackgroundImage = plant2SpawnBtn.BackgroundImage
        ElseIf isClickedSpawnBtn = 4 And (plant3SpawnBtn.Enabled = True) And (mapBoardBtn00.BackgroundImage Is Nothing) Then       '식물3 선택
            mapBoardBtn00.BackgroundImage = plant3SpawnBtn.BackgroundImage
        End If

    End Sub

    Private Sub shovelBtn_Click(sender As Object, e As EventArgs) Handles shovelBtn.Click
        isClickedSpawnBtn = 0
        Me.Controls.Add(moneyRainBtn)
    End Sub

    Private Sub moneyRainBtn_Click(sender As Object, e As EventArgs)
        currentMoneyLabelUi.Text = CStr(CInt(currentMoneyLabelUi.Text) + 25)
    End Sub
End Class