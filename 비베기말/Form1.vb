Imports System.Threading
Public Class Form1
    Private Declare Function GetTickCount64 Lib "kernel32" () As Long
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Dim thread_main As Thread
    Dim currentTime As Long
    Dim lastTime As Long
    Dim lastPlayerAnimTime As Long
    Dim lastZombieSpawnTime As Long
    Dim lastZombieAnimTime As Long

    Dim zombieImage As Image
    Dim zombieBitmap(10) As Bitmap

    '==============================
    Structure PlayerInfo
        Dim hp As Integer
        Dim speed As Integer
        Dim damage As Integer
        Dim pos As Rect
        Dim anim As Integer '0~1 (leftIdle), 2~3 (rightIdle), 4~7 (leftMove), 891011 (rightMove) 
        Dim dir As Boolean  'left : false,  right : true
        Dim state As Integer '0 : idle, 1 : move, 2 : attack
    End Structure

    Dim playerImage As Image
    Dim playerBitmap(24) As Bitmap

    Dim isSpawnPlayer As Boolean
    Dim plrInfo As PlayerInfo
    '===============================
    Structure Rect
        Dim x As Integer
        Dim y As Integer
        Dim height As Integer
        Dim width As Integer
    End Structure

    '===========================================
    Structure CharInfo
        Dim hp As Integer
        Dim speed As Integer
        Dim damage As Integer
        Dim pos As Rect
        Dim anim As Integer '0~1 / attack(idle), 2~5 / move, 6 / hit, 7~9 / die
    End Structure
    Dim zomInfoSpawn As CharInfo
    Dim zomInfo As CharInfo
    Dim spawnZombieList As New ArrayList()
    '===========================================   / zombie 관련 변수

    Sub LoadBitmap()
        zombieImage = My.Resources.ResourceManager.GetObject("1_idle")
        zombieBitmap(0) = New Bitmap(zombieImage)
        zombieBitmap(0).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("2_idle")
        zombieBitmap(1) = New Bitmap(zombieImage)
        zombieBitmap(1).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("3_move")
        zombieBitmap(2) = New Bitmap(zombieImage)
        zombieBitmap(2).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("4_move")
        zombieBitmap(3) = New Bitmap(zombieImage)
        zombieBitmap(3).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("5_move")
        zombieBitmap(4) = New Bitmap(zombieImage)
        zombieBitmap(4).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("6_move")
        zombieBitmap(5) = New Bitmap(zombieImage)
        zombieBitmap(5).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("7_hit")
        zombieBitmap(6) = New Bitmap(zombieImage)
        zombieBitmap(6).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("8_die")
        zombieBitmap(7) = New Bitmap(zombieImage)
        zombieBitmap(7).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("9_die")
        zombieBitmap(8) = New Bitmap(zombieImage)
        zombieBitmap(8).MakeTransparent()

        zombieImage = My.Resources.ResourceManager.GetObject("10_die")
        zombieBitmap(9) = New Bitmap(zombieImage)
        zombieBitmap(9).MakeTransparent()
        '========================================================================idle
        playerImage = My.Resources.ResourceManager.GetObject("Char_1_idle")
        playerBitmap(0) = New Bitmap(playerImage)
        playerBitmap(0).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_2_idle")
        playerBitmap(1) = New Bitmap(playerImage)
        playerBitmap(1).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_3_idle")
        playerBitmap(2) = New Bitmap(playerImage)
        playerBitmap(2).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_4_idle")
        playerBitmap(3) = New Bitmap(playerImage)
        playerBitmap(3).MakeTransparent()
        '========================================================================move
        playerImage = My.Resources.ResourceManager.GetObject("Char_5_move")
        playerBitmap(4) = New Bitmap(playerImage)
        playerBitmap(4).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_6_move")
        playerBitmap(5) = New Bitmap(playerImage)
        playerBitmap(5).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_7_move")
        playerBitmap(6) = New Bitmap(playerImage)
        playerBitmap(6).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_8_move")
        playerBitmap(7) = New Bitmap(playerImage)
        playerBitmap(7).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_9_move")
        playerBitmap(8) = New Bitmap(playerImage)
        playerBitmap(8).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_10_move")
        playerBitmap(9) = New Bitmap(playerImage)
        playerBitmap(9).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_11_move")
        playerBitmap(10) = New Bitmap(playerImage)
        playerBitmap(10).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_12_move")
        playerBitmap(11) = New Bitmap(playerImage)
        playerBitmap(11).MakeTransparent()
        '========================================================================attack
        playerImage = My.Resources.ResourceManager.GetObject("Char_13_attack")
        playerBitmap(12) = New Bitmap(playerImage)
        playerBitmap(12).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_14_attack")
        playerBitmap(13) = New Bitmap(playerImage)
        playerBitmap(13).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_15_attack")
        playerBitmap(14) = New Bitmap(playerImage)
        playerBitmap(14).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_16_attack")
        playerBitmap(15) = New Bitmap(playerImage)
        playerBitmap(15).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_17_attack")
        playerBitmap(16) = New Bitmap(playerImage)
        playerBitmap(16).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_18_attack")
        playerBitmap(17) = New Bitmap(playerImage)
        playerBitmap(17).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_19_attack")
        playerBitmap(18) = New Bitmap(playerImage)
        playerBitmap(18).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_20_attack")
        playerBitmap(19) = New Bitmap(playerImage)
        playerBitmap(19).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_21_attack")
        playerBitmap(20) = New Bitmap(playerImage)
        playerBitmap(20).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_22_attack")
        playerBitmap(21) = New Bitmap(playerImage)
        playerBitmap(21).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_23_attack")
        playerBitmap(22) = New Bitmap(playerImage)
        playerBitmap(22).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_24_attack")
        playerBitmap(23) = New Bitmap(playerImage)
        playerBitmap(23).MakeTransparent()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBitmap()
        isSpawnPlayer = False
        thread_main = New Thread(AddressOf Main) With {.IsBackground = True}
    End Sub
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        thread_main.Start()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        If spawnZombieList.Count() >= 1 Then
            For i As Integer = 0 To spawnZombieList.Count() - 1
                e.Graphics.DrawImage(zombieBitmap(CType(spawnZombieList(i), CharInfo).anim), CType(spawnZombieList(i), CharInfo).pos.x, CType(spawnZombieList(i), CharInfo).pos.y, CType(spawnZombieList(i), CharInfo).pos.width, CType(spawnZombieList(i), CharInfo).pos.height)
            Next
        End If

        If isSpawnPlayer = True Then
            e.Graphics.DrawImage(playerBitmap(plrInfo.anim), plrInfo.pos.x, plrInfo.pos.y, plrInfo.pos.width, plrInfo.pos.height)
        End If
    End Sub

    Private Sub Main()
        lastTime = GetTickCount64()
        lastZombieAnimTime = GetTickCount64()
        lastZombieSpawnTime = GetTickCount64()
        lastPlayerAnimTime = GetTickCount64()
        SpawnPlayer()
        Do
            currentTime = GetTickCount64()

            If currentTime > lastTime + 33 Then
                lastTime = currentTime

                MoveZombie()
                CheckOutOfRangeZombie()

                InputKeyPlayer()
                SwitchPlayerAnim()
                Invoke(Sub() Me.Invalidate())
            End If

            '좀비 애니메이션 수정 / 0.25초마다 이미지 전환
            If currentTime > lastZombieAnimTime + 250 Then
                lastZombieAnimTime = currentTime
                SwitchZombieAnim()
            End If

            '좀비스폰 5초로 고정시켜놓음, 이거 나중에 랜덤하게 수정
            If currentTime > lastZombieSpawnTime + 5000 Then
                lastZombieSpawnTime = currentTime
                SpawnZombie()
            End If
        Loop
    End Sub

    Sub SpawnPlayer()
        plrInfo.hp = 10
        plrInfo.speed = 5
        plrInfo.damage = 1
        plrInfo.pos.x = 200
        plrInfo.pos.y = 477
        plrInfo.pos.width = 124
        plrInfo.pos.height = 95
        plrInfo.anim = 2
        plrInfo.dir = True

        isSpawnPlayer = True
    End Sub
    Sub InputKeyPlayer()
        If GetAsyncKeyState(Keys.Left) Then         'left key input
            plrInfo.state = 1
            plrInfo.dir = False
            plrInfo.pos.x -= plrInfo.speed
        ElseIf GetAsyncKeyState(Keys.Right) Then    'right key input
            plrInfo.state = 1
            plrInfo.dir = True
            plrInfo.pos.x += plrInfo.speed
        Else                                        'idle
            plrInfo.state = 0
        End If
    End Sub
    Sub SwitchPlayerAnim()
        If plrInfo.state = 0 Then           'idle
            If plrInfo.dir = False Then         'left idle
                If plrInfo.anim >= 0 And plrInfo.anim <= 1 Then     'idle anim ++
                    If currentTime > lastPlayerAnimTime + 1000 Then     'idle anim swiching cooltime
                        lastPlayerAnimTime = currentTime

                        plrInfo.anim += 1
                        If plrInfo.anim > 1 Then    'left idle init
                            plrInfo.anim = 0
                        End If
                    End If
                Else                                'left idle init
                    plrInfo.anim = 0
                End If
            Else                                'right idle
                If plrInfo.anim >= 2 And plrInfo.anim <= 3 Then     'idle anim ++
                    If currentTime > lastPlayerAnimTime + 1000 Then     'idle anim swiching cooltime
                        lastPlayerAnimTime = currentTime

                        plrInfo.anim += 1
                        If plrInfo.anim > 3 Then    'right idle init
                            plrInfo.anim = 2
                        End If
                    End If
                Else                                'right idle init
                    plrInfo.anim = 2
                End If
            End If
        ElseIf plrInfo.state = 1 Then       'move
            If plrInfo.dir = False Then         'left move
                If plrInfo.anim >= 4 And plrInfo.anim <= 7 Then     'move anim ++
                    If currentTime > lastPlayerAnimTime + 225 Then      'move anim swiching cooltime
                        lastPlayerAnimTime = currentTime

                        plrInfo.anim += 1
                        If plrInfo.anim > 7 Then    'left move init
                            plrInfo.anim = 4
                        End If
                    End If
                Else                                'left move init
                    plrInfo.anim = 4
                End If
            Else                                'right move
                If plrInfo.anim >= 8 And plrInfo.anim <= 11 Then    'move anim ++
                    If currentTime > lastPlayerAnimTime + 225 Then      'move anim swiching cooltime
                        lastPlayerAnimTime = currentTime

                        plrInfo.anim += 1
                        If plrInfo.anim > 11 Then   'right move init
                            plrInfo.anim = 8
                        End If
                    End If
                Else                                'right move init
                    plrInfo.anim = 8
                End If

            End If
        End If

    End Sub

    Sub SpawnZombie()
        zomInfoSpawn.hp = 3
        zomInfoSpawn.speed = 1
        zomInfoSpawn.damage = 1
        zomInfoSpawn.pos.x = 900
        zomInfoSpawn.pos.y = 490
        zomInfoSpawn.pos.height = 76
        zomInfoSpawn.pos.width = 65
        zomInfoSpawn.anim = 2

        spawnZombieList.Add(zomInfoSpawn)
    End Sub
    Sub MoveZombie()
        If spawnZombieList.Count() = 0 Then
            Return
        End If

        'speed 만큼 x좌표 수정
        For i As Integer = 0 To spawnZombieList.Count() - 1
            If CType(spawnZombieList(i), CharInfo).anim >= 2 And CType(spawnZombieList(i), CharInfo).anim <= 5 Then
                zomInfo.pos.x = CType(spawnZombieList(i), CharInfo).pos.x - CType(spawnZombieList(i), CharInfo).speed       '이동일때만 speed만큼 좌표 --
            Else
                Continue For                 '이동 상태 아니면 이동 X
            End If

            zomInfo.hp = CType(spawnZombieList(i), CharInfo).hp
            zomInfo.speed = CType(spawnZombieList(i), CharInfo).speed
            zomInfo.damage = CType(spawnZombieList(i), CharInfo).damage
            zomInfo.pos.y = CType(spawnZombieList(i), CharInfo).pos.y
            zomInfo.pos.height = CType(spawnZombieList(i), CharInfo).pos.height
            zomInfo.pos.width = CType(spawnZombieList(i), CharInfo).pos.width

            spawnZombieList(i) = zomInfo
        Next

    End Sub
    Sub CheckOutOfRangeZombie()
        If spawnZombieList.Count() = 0 Then
            Return
        End If

        '식물 뚫고 집에 도착 시 좀비 삭제, /  게임오버 처리 (게임오버 처리 추가 해야함)
        For i As Integer = 0 To spawnZombieList.Count() - 1
            If CType(spawnZombieList(i), CharInfo).pos.x <= (0 - CType(spawnZombieList(i), CharInfo).pos.width) Then
                spawnZombieList.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    Sub SwitchZombieAnim()
        If spawnZombieList.Count() = 0 Then
            Return
        End If

        For i As Integer = 0 To spawnZombieList.Count() - 1
            If CType(spawnZombieList(i), CharInfo).anim >= 2 And CType(spawnZombieList(i), CharInfo).anim <= 5 Then
                If CType(spawnZombieList(i), CharInfo).anim = 5 Then
                    zomInfo.anim = CType(spawnZombieList(i), CharInfo).anim - 3         '이동 마지막 애니메이션 -> 이동 첫 애니메이션
                Else
                    zomInfo.anim = CType(spawnZombieList(i), CharInfo).anim + 1         '이동 애니메이션 ++
                End If
                zomInfo.pos.x = CType(spawnZombieList(i), CharInfo).pos.x - CType(spawnZombieList(i), CharInfo).speed       '이동일때만 speed만큼 좌표 - 하자
            Else
                zomInfo.pos.x = CType(spawnZombieList(i), CharInfo).pos.x
                zomInfo.anim = CType(spawnZombieList(i), CharInfo).anim                 '이동 상태 아니면 애니메이션 전환 X   <-상태 추가 시 수정해야함
            End If

            zomInfo.hp = CType(spawnZombieList(i), CharInfo).hp
            zomInfo.speed = CType(spawnZombieList(i), CharInfo).speed
            zomInfo.damage = CType(spawnZombieList(i), CharInfo).damage
            zomInfo.pos.y = CType(spawnZombieList(i), CharInfo).pos.y
            zomInfo.pos.height = CType(spawnZombieList(i), CharInfo).pos.height
            zomInfo.pos.width = CType(spawnZombieList(i), CharInfo).pos.width

            spawnZombieList(i) = zomInfo
        Next
    End Sub
    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        thread_main.Abort()
    End Sub

End Class
