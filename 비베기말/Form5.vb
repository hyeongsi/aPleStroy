Imports System.Threading
Public Class Form5
    Private Declare Function GetTickCount64 Lib "kernel32" () As Long
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Public Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" _
    (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As _
     Integer, ByVal hwndCallback As Integer) As Integer
    Dim brush1 As SolidBrush
    Dim brush2 As SolidBrush
    Dim timeGauge As Integer

    Dim isStart As Boolean = False
    Dim isEnd As Boolean = False
    Dim isGameOver As Boolean = False
    Dim lock As Boolean = False
    Dim pt As Boolean = False

    Dim startTime As Long
    Dim currentTime As Long
    Dim lastTime As Long
    Dim lastPlayerAnimTime As Long

    Dim lastMonsterSpawnTime As Long
    Dim lastMonsterAnimTime As Long

    Dim lastDieAnimTime As Long

    Dim backImage As Image
    Dim backBitmap As Bitmap

    Dim ldImage As Image
    Dim ldBitmap As Bitmap

    Dim playerImage As Image
    Dim playerBitmap(6) As Bitmap

    Dim monsterImage As Image
    Dim monsterBitmap As Bitmap

    Dim thread_main As Thread
    Structure Rect
        Dim x As Integer
        Dim y As Integer
        Dim height As Integer
        Dim width As Integer
    End Structure

    Dim backMap As Rect
    Dim backMap2 As Rect

    Dim ldRect As Rect
    Dim ldRect2 As Rect
    Dim ldRect3 As Rect
    Structure CharInfo
        Dim hp As Integer
        Dim pos As Rect
        Dim anim As Integer
        Dim state As Integer '0 : idle, 1 : move, 2 : attack, 3 : hit, 4 : die
    End Structure

    Dim plrInfo As CharInfo
    Dim isJump As Boolean
    Dim isJumpEffectSound As Boolean

    Dim monsterInfo(10) As Rect
    Dim monsterSpeed(10) As Integer
    Dim monsterSurv(10) As Boolean

    Dim velocity As Integer = 28

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        thread_main = New Thread(AddressOf Main) With {.IsBackground = True}

        My.Computer.Audio.Play("sound\hanggu.wav", AudioPlayMode.BackgroundLoop)
        mciSendString("open sound\jump.wav alias jumpsound1", 0, 0, 0)
    End Sub
    Private Sub Form5_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        thread_main.Start()
        LoadBitmap()
        SpawnPlayer()

        backMap.x = 0
        backMap.width = 1498
        backMap.height = 662
        backMap2.x = 1498
        backMap2.width = 1498
        backMap2.height = 662

        ldRect.x = 0
        ldRect.y = 450 - 75
        ldRect.width = 665
        ldRect.height = 59
        ldRect2.x = 665
        ldRect2.y = 450 - 75
        ldRect2.width = 665
        ldRect2.height = 59
        ldRect3.x = 1330
        ldRect3.y = 450 - 75
        ldRect3.width = 665
        ldRect3.height = 59

        brush1 = New SolidBrush(Color.Black)
        brush2 = New SolidBrush(Color.Red)

        timeGauge = 0
        isStart = True

    End Sub
    Sub SpawnPlayer()
        plrInfo.hp = 3
        plrInfo.pos.x = 100
        plrInfo.pos.y = 287
        plrInfo.anim = 0
        plrInfo.state = 0

        isJump = False
        isJumpEffectSound = False
    End Sub
    Sub LoadBitmap()
        backImage = My.Resources.ResourceManager.GetObject("backImage")
        backBitmap = New Bitmap(backImage)
        backBitmap.MakeTransparent()

        ldImage = My.Resources.ResourceManager.GetObject("loadimage")
        ldBitmap = New Bitmap(ldImage)
        ldBitmap.MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("junior_lace")
        monsterBitmap = New Bitmap(monsterImage)
        monsterBitmap.MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_9_move")
        playerBitmap(0) = New Bitmap(playerImage)
        playerBitmap(0).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_10_move")
        playerBitmap(1) = New Bitmap(playerImage)
        playerBitmap(1).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_11_move")
        playerBitmap(2) = New Bitmap(playerImage)
        playerBitmap(2).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_12_move")
        playerBitmap(3) = New Bitmap(playerImage)
        playerBitmap(3).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_28_jump_move")
        playerBitmap(4) = New Bitmap(playerImage)
        playerBitmap(4).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_30_hit")
        playerBitmap(5) = New Bitmap(playerImage)
        playerBitmap(5).MakeTransparent()
    End Sub

    Private Sub Main()
        MsgBox("학업을 위해 옆 동네 도서관에 간 사이에" & vbCrLf & vbCrLf & "마을이 몬스터에 의해 공격받고 있다는 소식을 들었습니다." & vbCrLf & vbCrLf &
            "이대로 가다간 마을 주민들이 몬스터들에게 당해" & vbCrLf & vbCrLf & "코딩 노예가 될 것 입니다." & vbCrLf & vbCrLf & "가는 길에 마주치는 몬스터를 상대할 시간도 없습니다." & vbCrLf & vbCrLf &
            "몬스터들을 피해 빨리 마을로 달려가야겠습니다." & vbCrLf & vbCrLf & vbCrLf & "- Alt : 점프",, "스토리")

        startTime = GetTickCount64()
        currentTime = GetTickCount64()
        lastTime = GetTickCount64()
        lastPlayerAnimTime = GetTickCount64()
        lastMonsterSpawnTime = GetTickCount64()
        lastMonsterAnimTime = GetTickCount64()

        Do
            currentTime = GetTickCount64()

            If currentTime > lastTime + 33 Then
                lastTime = currentTime

                If isGameOver = False Then
                    ScrollMap()
                    SetState()
                    SwichPlayerAnim()

                    MonsterSpawn()
                    MonsterMove()

                    Jump()
                    Hit()
                End If
                Invoke(Sub() Me.Invalidate())

            End If

        Loop

    End Sub

    Sub ScrollMap()
        backMap.x -= 1
        backMap2.x -= 1

        If backMap.x <= -backMap.width Then
            backMap.x = backMap.width
        End If
        If backMap2.x <= -backMap2.width Then
            backMap2.x = backMap2.width
        End If

        ldRect.x -= 8
        ldRect2.x -= 8
        ldRect3.x -= 8

        If ldRect.x <= -ldRect.width Then
            ldRect.x = ldRect3.x + ldRect.width
        End If
        If ldRect2.x <= -ldRect2.width Then
            ldRect2.x = ldRect.x + ldRect2.width
        End If
        If ldRect3.x <= -ldRect3.width Then
            ldRect3.x = ldRect2.x + ldRect3.width
        End If
    End Sub
    Sub SetState()
        If GetAsyncKeyState(Keys.Menu) And isJump = False Then    'alt key input
            isJumpEffectSound = True
            plrInfo.state = 1
            isJump = True
        End If
    End Sub
    Sub SwichPlayerAnim()
        If plrInfo.state = 0 Then
            If plrInfo.anim >= 0 And plrInfo.anim <= 3 Then     'idle anim ++
                If currentTime > lastPlayerAnimTime + 100 Then     'idle anim swiching cooltime
                    lastPlayerAnimTime = currentTime

                    plrInfo.anim += 1
                    If plrInfo.anim > 3 Then    'left idle init
                        plrInfo.anim = 0
                    End If
                End If
            Else                                'left idle init
                plrInfo.anim = 0
            End If
        ElseIf plrInfo.state = 1 Then       'jump
            plrInfo.anim = 4
        End If
    End Sub
    Sub Hit()
        For i As Integer = 0 To monsterInfo.Count() - 1
            If monsterSurv(i) = True Then
                If (plrInfo.pos.x + playerBitmap(plrInfo.anim).Width - 50) >= monsterInfo(i).x And (plrInfo.pos.x + 50) <= (monsterInfo(i).x + monsterInfo(i).width) Then
                    If (plrInfo.pos.y + playerBitmap(plrInfo.anim).Height - 10) >= monsterInfo(i).y + 2 And (plrInfo.pos.y + 10) <= (monsterInfo(i).y + monsterInfo(i).height - 15) Then
                        isGameOver = True
                        lastDieAnimTime = GetTickCount64()
                    End If
                End If
            End If
        Next

    End Sub
    Sub Jump()
        If isJump = True Then
            plrInfo.pos.y -= velocity
            velocity -= 4

            If plrInfo.pos.y >= 287 Then
                velocity = 28
                isJump = False
                plrInfo.state = 0
            End If
        End If

    End Sub

    Sub MonsterSpawn()
        If CInt((currentTime - startTime) / 1000) >= 44 Then
            Return
        End If

        Randomize()
        If GetTickCount64() > lastMonsterSpawnTime + CInt(CInt(3000 * Rnd()) + 2000) Then
            lastMonsterSpawnTime = GetTickCount64()
            For i As Integer = 0 To monsterInfo.Count() - 1
                If monsterSurv(i) = False Then
                    monsterSurv(i) = True
                    monsterInfo(i).width = 41
                    monsterInfo(i).height = 36
                    monsterInfo(i).x = 800
                    Dim rNum = CInt(CInt(2 * Rnd()))

                    If rNum = 0 Then
                        monsterInfo(i).y = 338
                    ElseIf rNum = 1 Then
                        monsterInfo(i).y = 302
                    ElseIf rNum = 2 Then
                        monsterInfo(i).y = 260
                    End If

                    Dim rNum2 = CInt(CInt(2 * Rnd()))

                    If rNum2 = 0 Then
                        monsterSpeed(i) = 12
                    ElseIf rNum2 = 1 Then
                        monsterSpeed(i) = 14
                    ElseIf rNum2 = 2 Then
                        monsterSpeed(i) = 16
                    End If

                    Exit For
                End If
            Next
        End If
    End Sub
    Sub MonsterMove()
        For i As Integer = 0 To monsterInfo.Count() - 1
            If monsterSurv(i) = True Then
                monsterInfo(i).x -= monsterSpeed(i)
            End If

            If monsterInfo(i).x <= -40 Then
                monsterSurv(i) = False
            End If
        Next
    End Sub

    Private Sub Form4_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        If isGameOver = False Then
            Label1.Text = CInt((currentTime - startTime) / 1000) & "초"
        End If

        e.Graphics.DrawImage(backBitmap, backMap.x, 0, backBitmap.Width, backBitmap.Height)
        e.Graphics.DrawImage(backBitmap, backMap2.x, 0, backBitmap.Width, backBitmap.Height)

        e.Graphics.DrawImage(ldBitmap, ldRect.x, ldRect.y, ldBitmap.Width, ldBitmap.Height)
        e.Graphics.DrawImage(ldBitmap, ldRect2.x, ldRect2.y, ldBitmap.Width, ldBitmap.Height)
        e.Graphics.DrawImage(ldBitmap, ldRect3.x, ldRect3.y, ldBitmap.Width, ldBitmap.Height)

        For i As Integer = 0 To monsterInfo.Count() - 1
            If monsterSurv(i) = True Then
                e.Graphics.DrawImage(monsterBitmap, monsterInfo(i).x, monsterInfo(i).y, monsterInfo(i).width, monsterInfo(i).height)
            End If
        Next


        If timeGauge >= Me.Width - 70 Then
                plrInfo.pos.x += 3
            End If

        If plrInfo.pos.x < Me.Width - 10 Then
            If isGameOver = False Then
                e.Graphics.DrawImage(playerBitmap(plrInfo.anim), plrInfo.pos.x, plrInfo.pos.y, playerBitmap(plrInfo.anim).Width, playerBitmap(plrInfo.anim).Height)
            End If
        ElseIf isEnd = False Then
            isEnd = True
            MsgBox("열심히 달려 제시간에 마을에 도착했습니다." & vbCrLf & vbCrLf & "확인 버튼을 누르면 다음 스테이지로 이동합니다",, "NEXT STAGE")
            thread_main.Abort()
            Form1.Show()
            Me.Close()
        End If


        If isStart = True Then
            e.Graphics.FillRectangle(brush1, 20, 20, 10, 20)
            e.Graphics.FillRectangle(brush2, 30, 20, timeGauge, 20)

            e.Graphics.FillRectangle(brush1, Me.Width - 40, 20, 10, 20)

            If isGameOver = False Then
                If timeGauge < Me.Width - 70 Then
                    timeGauge = CInt((Me.Width - 70) / 290) * CInt((currentTime - startTime) / 200)
                End If
            End If

            If timeGauge >= Me.Width - 70 Then
                timeGauge = Me.Width - 70
            End If
        End If

        If isJumpEffectSound = True And isJump = True Then
            isJumpEffectSound = False
            mciSendString("play jumpsound1 from 0", 0, 0, 0)
        End If

        If isGameOver = True And lock = False Then
            e.Graphics.DrawImage(playerBitmap(5), plrInfo.pos.x, plrInfo.pos.y, playerBitmap(plrInfo.anim).Width, playerBitmap(plrInfo.anim).Height)

            If GetTickCount64() > lastDieAnimTime + 200 Then
                pt = True
            End If

            If pt = True Then
                lock = True
                MsgBox("마을에 달려가던 도중 몬스터에게 습격을 당해 다리가 부러졌습니다." & vbCrLf & vbCrLf & "제시간에 마을에 도착하지 못해, 마을이 파괴되고" & vbCrLf & "마을주민들이 모두 코딩노예가 되었습니다." & vbCrLf & vbCrLf & "아픈 몸을 이끌고 도망칩니다." & vbCrLf & vbCrLf & "확인 버튼을 누르면 로비로 이동합니다.",, "TIME OVER")

                Form2.Show()
                Me.Close()
            End If
        End If
    End Sub
    Private Sub Form4_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        thread_main.Abort()
    End Sub

    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("button21")
    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As MouseEventArgs) Handles Button1.MouseUp
        thread_main.Abort()
        Form2.Show()
        Me.Close()
    End Sub
End Class