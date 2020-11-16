Public Class Form2
    Dim isClickedSpawnBtn As Integer    '-1: 선택x,  0 :삽   1:첫번째스폰,  2:두번쨰스폰 ....

    Private moneyRain(4) As PictureBox
    Private sunflowerMoney(45) As PictureBox
    Private mapBoardPictureBox(45) As PictureBox
    Structure ActionTimerStruct                       '소환 시 시간에 따른 행동에 쓰일 구조체
        Dim index As Integer
        Dim countTime As Date
    End Structure
    Dim actionTimer As ActionTimerStruct
    Dim copyData_ActionTimerStruct As ActionTimerStruct
    Structure SpawnPlantStruct                     '소환 시 시간에 따른 행동에 쓰일 구조체
        Dim index As Integer
        Dim picturebox As PictureBox
    End Structure
    Dim spawnPlant As SpawnPlantStruct
    Dim spawnFlowerIndexList As New ArrayList()         '스폰된 식물의 인덱스 저장할 리스트
    Dim sunflowerSpawnTimerList As New ArrayList()    '해바라기 돈 스폰 시간에 쓰일 리스트
    Dim plant1SpawnTimerList As New ArrayList()    '식물1 스폰 시간에 쓰일 리스트
    Dim plant1BulletList As New ArrayList()        '식물1 총알 리스트

    Structure ZombieInfo                       '좀비 관리 구조체
        Dim hp As Integer
        Dim picturebox As PictureBox
    End Structure
    Dim zombiesInfo As ZombieInfo
    Dim copyData_ZombieInfo As ZombieInfo
    Dim zombiesSpawnList As New ArrayList()     '좀비 객체 관리할 리스트
    Dim zombieLine(5) As Integer                '라인별 좀비 유무 체크
    Dim speed As Integer = 0

    Dim plantPrice(4) As Integer
    Dim point As Point
    Dim pSize As Size
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Randomize()

        Dim count As Integer
        count = 0

        count = 0
        pSize.Width = 75
        pSize.Height = 67
        For yIndex As Integer = 0 To 4
            For xIndex As Integer = 0 To 8
                mapBoardPictureBox(count) = New PictureBox()
                point.X = 256 + (xIndex * 81)
                point.Y = 86 + (yIndex * 100)
                With Me.mapBoardPictureBox(count)
                    .Name = "mapBoardPictureBox" & CStr(count)
                    .Location = point
                    .Size() = pSize
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BackColor = Color.Transparent
                    .TabStop = False
                End With
                AddHandler Me.mapBoardPictureBox(count).Click, AddressOf mapBoardPictureBox_Click
                Me.Controls.Add(mapBoardPictureBox(count))
                count += 1
            Next
        Next

        Init()
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

    Private Sub mapBoardPictureBox_Click(sender As Object, e As EventArgs) 'byval 이라서 sender 바로 사용 못함.
        Dim findIndex As Integer
        findIndex = 0
        For yIndex As Integer = 0 To 44
            If (sender.Name = ("mapBoardPictureBox" & CStr(findIndex))) Then
                Exit For
            Else
                findIndex += 1
            End If
        Next

        If (isClickedSpawnBtn = 0 And (mapBoardPictureBox(findIndex).BackgroundImage IsNot Nothing)) Then       '삽 선택
            mapBoardPictureBox(findIndex).BackgroundImage = Nothing
            shovelBtn.BackColor = Color.Transparent
            isClickedSpawnBtn = -1

            '해바라기 삭제에 대한 코드 (리스트 삭제)
            DeleteSunflower(findIndex)
            '식물1 삭제에 대한 코드 (리스트 삭제)
            DeletePlant1(findIndex)

        ElseIf (isClickedSpawnBtn = 1 And (sunflowerSpawnBtn.Enabled = True) And (mapBoardPictureBox(findIndex).BackgroundImage Is Nothing) And (CInt(currentMoneyLabelUi.Text) >= plantPrice(0))) Then    '해바라기 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(0)))
            mapBoardPictureBox(findIndex).BackgroundImage = sunflowerSpawnBtn.BackgroundImage
            sunflowerSpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            sunflowerspawTimer.Enabled = True

            actionTimer.index = findIndex   '구조체에 생성 시간 대입
            actionTimer.countTime = Now()
            sunflowerSpawnTimerList.Add(actionTimer)

            spawnPlant.index = findIndex
            spawnPlant.picturebox = mapBoardPictureBox(findIndex)
            spawnFlowerIndexList.Add(spawnPlant)
        ElseIf (isClickedSpawnBtn = 2 And (plant1SpawnBtn.Enabled = True) And (mapBoardPictureBox(findIndex).BackgroundImage Is Nothing And (CInt(currentMoneyLabelUi.Text) >= plantPrice(1)))) Then       '식물1 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(1)))
            mapBoardPictureBox(findIndex).BackgroundImage = plant1SpawnBtn.BackgroundImage
            plant1SpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant1spawTimer.Enabled = True

            actionTimer.index = findIndex   '구조체에 생성 시간 대입
            actionTimer.countTime = Now()
            plant1SpawnTimerList.Add(actionTimer)

            spawnPlant.index = findIndex
            spawnPlant.picturebox = mapBoardPictureBox(findIndex)
            spawnFlowerIndexList.Add(spawnPlant)
        ElseIf (isClickedSpawnBtn = 3 And (plant2SpawnBtn.Enabled = True) And (mapBoardPictureBox(findIndex).BackgroundImage Is Nothing And (CInt(currentMoneyLabelUi.Text) >= plantPrice(2)))) Then       '식물2 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(2)))
            mapBoardPictureBox(findIndex).BackgroundImage = plant2SpawnBtn.BackgroundImage
            plant2SpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant2spawTimer.Enabled = True
        ElseIf (isClickedSpawnBtn = 4 And (plant3SpawnBtn.Enabled = True) And (mapBoardPictureBox(findIndex).BackgroundImage Is Nothing And (CInt(currentMoneyLabelUi.Text) >= plantPrice(3)))) Then       '식물3 선택
            currentMoneyLabelUi.Text = CStr((CInt(currentMoneyLabelUi.Text) - plantPrice(3)))
            mapBoardPictureBox(findIndex).BackgroundImage = plant3SpawnBtn.BackgroundImage
            plant3SpawnBtn.BackColor = Color.Red
            isClickedSpawnBtn = -1
            plant3spawTimer.Enabled = True
        End If
    End Sub

    '삽 버튼 누르면 삽 활성화
    Private Sub shovelBtn_Click(sender As Object, e As EventArgs) Handles shovelBtn.Click
        isClickedSpawnBtn = 0
        shovelBtn.BackColor = Color.GreenYellow
    End Sub
    '해바라기 돈 클릭 시 돈+25 And 돈 삭제
    Private Sub sunflowerMoneyBtn_Click(sender As Object, e As EventArgs)
        Dim findIndex As Integer = 0

        For index As Integer = 0 To 44
            If (sender.Name = ("sunflowerMoney" & CStr(index))) Then
                Exit For
            Else
                findIndex += 1
            End If
        Next

        currentMoneyLabelUi.Text += 25
        sunflowerMoney(findIndex).Visible = False
    End Sub

    '돈비 클릭 시 돈 +25 And 클릭한 돈비 삭제
    Private Sub moneyRain_Click(sender As Object, e As EventArgs) Handles moneyRain0.Click, moneyRain1.Click, moneyRain2.Click, moneyRain3.Click
        Dim findIndex As Integer = 0

        For index As Integer = 0 To 3
            If (sender.Name = ("moneyRain" & CStr(index))) Then
                Exit For
            Else
                findIndex += 1
            End If
        Next

        currentMoneyLabelUi.Text += 25
        moneyRain(findIndex).Visible = False
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

    '식물1 소환 쿨타임 타이머
    Private Sub plant1spawTimer_Tick(sender As Object, e As EventArgs) Handles sunflowerspawTimer.Tick
        If sunflowerSpawnBtn.BackColor = Color.Red Then
            sunflowerSpawnBtn.BackColor = Color.Transparent
            sunflowerspawTimer.Enabled = False
        End If
    End Sub
    '식물2 소환 쿨타임 타이머
    Private Sub plant2spawTimer_Tick(sender As Object, e As EventArgs) Handles plant1spawTimer.Tick
        If plant1SpawnBtn.BackColor = Color.Red Then
            plant1SpawnBtn.BackColor = Color.Transparent
            plant1spawTimer.Enabled = False
        End If
    End Sub
    '식물3 소환 쿨타임 타이머
    Private Sub plant3spawTimer_Tick(sender As Object, e As EventArgs) Handles plant2spawTimer.Tick
        If plant2SpawnBtn.BackColor = Color.Red Then
            plant2SpawnBtn.BackColor = Color.Transparent
            plant2spawTimer.Enabled = False
        End If
    End Sub
    '식물4 소환 쿨타임 타이머
    Private Sub plant4spawTimer_Tick(sender As Object, e As EventArgs) Handles plant3spawTimer.Tick
        If plant3SpawnBtn.BackColor = Color.Red Then
            plant3SpawnBtn.BackColor = Color.Transparent
            plant3spawTimer.Enabled = False
        End If
    End Sub

    '돈비 스폰하는 타이머
    Private Sub moneyRainSpawnTimer_Tick(sender As Object, e As EventArgs) Handles moneyRainSpawnTimer.Tick
        moneyRainSpawnTimer.Interval = CInt(Int((10000 - 5000 + 1) * Rnd() + 5000))
        For index As Integer = 0 To 3
            If moneyRain(index).Visible = False Then
                moneyRain(index).Visible = True
                point.X = CInt(Int((900 - 433 + 1) * Rnd() + 433))
                point.Y = -54
                moneyRain(index).Location = point
                Exit For
            End If
        Next
    End Sub

    Private Sub spawnSunflowerMoneyTimer_Tick(sender As Object, e As EventArgs) Handles spawnSunflowerMoneyTimer.Tick
        For i As Integer = 0 To sunflowerSpawnTimerList.Count() - 1
            If (DateDiff("s", sunflowerSpawnTimerList(i).countTime, Now()) > (CInt(Int((15 - 10 + 1) * Rnd() + 10))) And sunflowerMoney(sunflowerSpawnTimerList(i).index).Visible = False) Then
                copyData_ActionTimerStruct.index = sunflowerSpawnTimerList(i).index   '삭제 안하고 list에 바로 접근하려 했으나 오류로 삭제 후 삽입 진행
                copyData_ActionTimerStruct.countTime = Now()
                sunflowerMoney(copyData_ActionTimerStruct.index).Visible = True
                sunflowerSpawnTimerList.RemoveAt(i)
                sunflowerSpawnTimerList.Add(copyData_ActionTimerStruct)
            End If
        Next
    End Sub
    Private Sub spawnBulletTimer_Tick(sender As Object, e As EventArgs) Handles spawnBulletTimer.Tick
        '조건 추가하기, 좀비가 해당 줄에 있을 때 생성하도록 만들기!!
        If plant1SpawnTimerList.Count() = 0 Then
            Return
        End If

        For i As Integer = 0 To plant1SpawnTimerList.Count() - 1
            If (DateDiff("s", plant1SpawnTimerList(i).countTime, Now()) > 3) And (zombieLine(plant1SpawnTimerList(i).index / 9) > 0) Then
                Dim bulletPictureBox As New PictureBox()
                pSize.Width = 20
                pSize.Height = 20
                point.X = mapBoardPictureBox(plant1SpawnTimerList(i).index).Location.X + 60
                point.Y = mapBoardPictureBox(plant1SpawnTimerList(i).index).Location.Y + 20
                With bulletPictureBox
                    .Location = point
                    .Size() = pSize
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BackColor = Color.Transparent
                    .BackgroundImage = My.Resources.bullet
                    .TabStop = False
                End With
                Me.Controls.Add(bulletPictureBox)

                With bulletPictureBox
                    .BringToFront()
                End With

                plant1BulletList.Add(bulletPictureBox)

                copyData_ActionTimerStruct.index = plant1SpawnTimerList(i).index   '삭제 안하고 list에 바로 접근하려 했으나 오류로 삭제 후 삽입 진행
                copyData_ActionTimerStruct.countTime = Now()
                plant1SpawnTimerList.RemoveAt(i)
                plant1SpawnTimerList.Add(copyData_ActionTimerStruct)
            End If
        Next
    End Sub
    Private Sub moveBulletTimer_Tick(sender As Object, e As EventArgs) Handles moveBulletTimer.Tick
        If plant1BulletList.Count() <= 0 Then
            Return
        End If

        For index As Integer = 0 To plant1BulletList.Count() - 1
            CType(plant1BulletList(index), PictureBox).Left += 10 + speed
        Next

        Dim escape As Boolean = False
        For index As Integer = 0 To plant1BulletList.Count() - 1
            For indexB As Integer = 0 To zombiesSpawnList.Count() - 1
                If plant1BulletList(index).Bounds.IntersectsWith(zombiesSpawnList(indexB).picturebox.Bounds) Then
                    copyData_ZombieInfo.picturebox = zombiesSpawnList(indexB).picturebox   '삭제 안하고 list에 바로 접근하려 했으나 오류로 삭제 후 삽입 진행
                    copyData_ZombieInfo.hp = zombiesSpawnList(indexB).hp - 1
                    zombiesSpawnList.RemoveAt(indexB)
                    zombiesSpawnList.Add(copyData_ZombieInfo)

                    Me.Controls.Remove(plant1BulletList(index))
                    plant1BulletList.RemoveAt(index)
                    escape = True
                    Exit For
                End If
            Next
            If escape = True Then
                escape = False
                Exit For
            End If
        Next

        For index As Integer = 0 To plant1BulletList.Count() - 1
            If CType(plant1BulletList(index), PictureBox).Location.X >= 1008 Then
                plant1BulletList.RemoveAt(index)
                Exit For
            End If
        Next

    End Sub
    Private Sub moneyMoveTimer_Tick(sender As Object, e As EventArgs) Handles moneyMoveTimer.Tick
        For index As Integer = 0 To 3
            If moneyRain(index).Visible = True Then
                moneyRain(index).Top += 4 + speed
            End If
            If (moneyRain(index).Visible = True) And (moneyRain(index).Location.Y >= 577) Then
                moneyRain(index).Visible = False
            End If
        Next
    End Sub
    Private Sub zombiesMoveTimer_Tick(sender As Object, e As EventArgs) Handles zombiesMoveTimer.Tick
        If zombiesSpawnList.Count() <= 0 Then
            Return
        End If

        DeleteZombieLine()
        DieZombie()

        If spawnFlowerIndexList.Count() >= 1 Then
            For index As Integer = 0 To zombiesSpawnList.Count() - 1
                For index2 As Integer = 0 To spawnFlowerIndexList.Count() - 1
                    If CType(zombiesSpawnList(index), ZombieInfo).picturebox.Bounds.IntersectsWith(CType(spawnFlowerIndexList(index2), SpawnPlantStruct).picturebox.Bounds) Then
                        CType(spawnFlowerIndexList(index2), SpawnPlantStruct).picturebox.BackgroundImage = Nothing
                        If (CType(spawnFlowerIndexList(index2), SpawnPlantStruct).picturebox.BackgroundImage Is sunflowerSpawnBtn.BackgroundImage) Then
                            DeleteSunflower(index2)
                            spawnFlowerIndexList.RemoveAt(index2)
                        Else
                            DeletePlant1(index2)
                            spawnFlowerIndexList.RemoveAt(index2)
                        End If
                        Exit For
                    End If
                Next
            Next
        End If

        MoveZombie()

    End Sub

    Private Sub zombiesSpawnTimer_Tick(sender As Object, e As EventArgs) Handles zombiesSpawnTimer.Tick
        zombiesSpawnTimer.Interval = CInt(Int((10000 - 5000 + 1) * Rnd() + 5000))

        Dim zombiePictureBox As New PictureBox()

        point.X = 1007
        point.Y = 86 + (CInt(Int((5) * Rnd())) * 100)

        If CInt(point.Y / 100) = 1 Then
            zombieLine(0) += 1
        ElseIf CInt(point.Y / 100) = 2 Then
            zombieLine(1) += 1
        ElseIf CInt(point.Y / 100) = 3 Then
            zombieLine(2) += 1
        ElseIf CInt(point.Y / 100) = 4 Then
            zombieLine(3) += 1
        ElseIf CInt(point.Y / 100) = 5 Then
            zombieLine(4) += 1
        End If

        pSize.Width = 75
        pSize.Height = 78
        With zombiePictureBox
            .Location = point
            .Size() = pSize
            .BackgroundImageLayout = ImageLayout.Stretch
            .BackColor = Color.Transparent
            .BackgroundImage = My.Resources.zombie2
            .TabStop = False
        End With
        Me.Controls.Add(zombiePictureBox)

        With zombiePictureBox
            .BringToFront()
        End With

        zombiesInfo.hp = 3
        zombiesInfo.picturebox = zombiePictureBox
        zombiesSpawnList.Add(zombiesInfo)

        If zombiesSpawnList.Count() Mod 5 = 0 Then
            speed += 1
        End If
    End Sub

    Private Sub Init()
        isClickedSpawnBtn = -1                                  '스폰버튼 초기화

        sunImage.Parent = leftTopUI                             '좌측 상단 돈 UI 흰부분 바탕 동기화

        currentMoneyLabelUi.Text = 1000

        For i As Integer = 0 To 4
            zombieLine(i) = 0
        Next

        currentMoneyLabelUi.Parent = leftTopUI                  '돈 UI 흰부분 바탕 동기화
        plantPrice(0) = plantMoneyLabelUi1.Text                 '가격 설정
        plantPrice(1) = plantMoneyLabelUi2.Text
        plantPrice(2) = plantMoneyLabelUi3.Text
        plantPrice(3) = plantMoneyLabelUi4.Text

        moneyRain(0) = moneyRain0
        moneyRain(1) = moneyRain1
        moneyRain(2) = moneyRain2
        moneyRain(3) = moneyRain3

        sunflowerMoney(0) = sunflowerMoney0
        sunflowerMoney(1) = sunflowerMoney1
        sunflowerMoney(2) = sunflowerMoney2
        sunflowerMoney(3) = sunflowerMoney3
        sunflowerMoney(4) = sunflowerMoney4
        sunflowerMoney(5) = sunflowerMoney5
        sunflowerMoney(6) = sunflowerMoney6
        sunflowerMoney(7) = sunflowerMoney7
        sunflowerMoney(8) = sunflowerMoney8
        sunflowerMoney(9) = sunflowerMoney9
        sunflowerMoney(10) = sunflowerMoney10
        sunflowerMoney(11) = sunflowerMoney11
        sunflowerMoney(12) = sunflowerMoney12
        sunflowerMoney(13) = sunflowerMoney13
        sunflowerMoney(14) = sunflowerMoney14
        sunflowerMoney(15) = sunflowerMoney15
        sunflowerMoney(16) = sunflowerMoney16
        sunflowerMoney(17) = sunflowerMoney17
        sunflowerMoney(18) = sunflowerMoney18

        sunflowerMoney(19) = sunflowerMoney19
        sunflowerMoney(20) = sunflowerMoney20
        sunflowerMoney(21) = sunflowerMoney21
        sunflowerMoney(22) = sunflowerMoney22
        sunflowerMoney(23) = sunflowerMoney23
        sunflowerMoney(24) = sunflowerMoney24
        sunflowerMoney(25) = sunflowerMoney25
        sunflowerMoney(26) = sunflowerMoney26
        sunflowerMoney(27) = sunflowerMoney27
        sunflowerMoney(28) = sunflowerMoney28
        sunflowerMoney(29) = sunflowerMoney29

        sunflowerMoney(30) = sunflowerMoney30
        sunflowerMoney(31) = sunflowerMoney31
        sunflowerMoney(32) = sunflowerMoney32
        sunflowerMoney(33) = sunflowerMoney33
        sunflowerMoney(34) = sunflowerMoney34
        sunflowerMoney(35) = sunflowerMoney35
        sunflowerMoney(36) = sunflowerMoney36
        sunflowerMoney(37) = sunflowerMoney37
        sunflowerMoney(38) = sunflowerMoney38
        sunflowerMoney(39) = sunflowerMoney39

        sunflowerMoney(40) = sunflowerMoney40
        sunflowerMoney(41) = sunflowerMoney41
        sunflowerMoney(42) = sunflowerMoney42
        sunflowerMoney(43) = sunflowerMoney43
        sunflowerMoney(44) = sunflowerMoney44

        For i As Integer = 0 To 44
            AddHandler Me.sunflowerMoney(i).Click, AddressOf sunflowerMoneyBtn_Click
        Next

        moneyRainSpawnTimer.Interval = CInt(Int((5000 - 3000 + 1) * Rnd() + 3000))
    End Sub
    Sub DeleteSunflower(findIndex As Integer)
        For i As Integer = 0 To sunflowerSpawnTimerList.Count() - 1
            If sunflowerSpawnTimerList(i).index = findIndex Then
                For i2 As Integer = 0 To spawnFlowerIndexList.Count() - 1
                    spawnFlowerIndexList(i2).index = findIndex
                    spawnFlowerIndexList.RemoveAt(i2)
                    Exit For
                Next
                sunflowerSpawnTimerList.RemoveAt(i)
                Exit For '삭제 후 다음 값 참조하면 sunflowerSpawnTimerList.Count()값 변경되어 outofindex 오류 발생 따라서 exit 해줌
            End If
        Next
    End Sub
    Sub DeletePlant1(findIndex)
        For i As Integer = 0 To plant1SpawnTimerList.Count() - 1
            If plant1SpawnTimerList(i).index = findIndex Then
                For i2 As Integer = 0 To spawnFlowerIndexList.Count() - 1
                    spawnFlowerIndexList(i2).index = findIndex
                    spawnFlowerIndexList.RemoveAt(i2)
                    Exit For
                Next
                plant1SpawnTimerList.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub
    Sub DeleteZombieLine()
        For index As Integer = 0 To zombiesSpawnList.Count() - 1
            If CType(zombiesSpawnList(index), ZombieInfo).hp <= 0 Then

                If CInt(CType(zombiesSpawnList(index), ZombieInfo).picturebox.Location.Y / 100) = 1 Then
                    zombieLine(0) -= 1
                ElseIf CInt(CType(zombiesSpawnList(index), ZombieInfo).picturebox.Location.Y / 100) = 2 Then
                    zombieLine(1) -= 1
                ElseIf CInt(CType(zombiesSpawnList(index), ZombieInfo).picturebox.Location.Y / 100) = 3 Then
                    zombieLine(2) -= 1
                ElseIf CInt(CType(zombiesSpawnList(index), ZombieInfo).picturebox.Location.Y / 100) = 4 Then
                    zombieLine(3) -= 1
                ElseIf CInt(CType(zombiesSpawnList(index), ZombieInfo).picturebox.Location.Y / 100) = 5 Then
                    zombieLine(4) -= 1
                End If

                Exit For
            End If
        Next
    End Sub
    Sub MoveZombie()
        For index As Integer = 0 To zombiesSpawnList.Count() - 1
            CType(zombiesSpawnList(index), ZombieInfo).picturebox.Left -= 2 + speed

            If CType(zombiesSpawnList(index), ZombieInfo).picturebox.Location.X <= 218 Then
                Process.GetCurrentProcess.Kill()
                zombiesSpawnList.RemoveAt(index)
                Exit For
            End If
        Next
    End Sub
    Sub DieZombie()
        For index As Integer = 0 To zombiesSpawnList.Count() - 1
            If CType(zombiesSpawnList(index), ZombieInfo).hp <= 0 Then
                zombiesSpawnList(index).pictureBox.visible = False
                Me.Controls.Remove(zombiesSpawnList(index).pictureBox)
                zombiesSpawnList.RemoveAt(index)
                Exit For
            End If
        Next
    End Sub
End Class