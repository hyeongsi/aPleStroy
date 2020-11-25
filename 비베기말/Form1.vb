Imports System.Threading
Public Class Form1
    Private Declare Function GetTickCount64 Lib "kernel32" () As Long

    Dim thread_main As Thread
    Dim currentTime As Long
    Dim lastTime As Long
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
        Dim state As Integer '0~1 (leftIdle), 2~3 (rightIdle), 4~7 (leftMove), 891011 (rightMove) 
        Dim dir As Boolean  'left : false,  right : true
    End Structure

    Dim playerImage As Image
    Dim playerBitmap(12) As Bitmap

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
        Dim state As Integer '0~1 / attack(idle), 2~5 / move, 6 / hit, 7~9 / die
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
                e.Graphics.DrawImage(zombieBitmap(CType(spawnZombieList(i), CharInfo).state), CType(spawnZombieList(i), CharInfo).pos.x, CType(spawnZombieList(i), CharInfo).pos.y, CType(spawnZombieList(i), CharInfo).pos.width, CType(spawnZombieList(i), CharInfo).pos.height)
            Next
        End If

        If isSpawnPlayer = True Then
            e.Graphics.DrawImage(playerBitmap(plrInfo.state), plrInfo.pos.x, plrInfo.pos.y, plrInfo.pos.width, plrInfo.pos.height)
        End If
    End Sub

    Private Sub Main()
        lastTime = GetTickCount64()
        lastZombieAnimTime = GetTickCount64()
        lastZombieSpawnTime = GetTickCount64()

        SpawnPlayer()
        Do
            currentTime = GetTickCount64()

            If currentTime > lastTime + 33 Then
                lastTime = currentTime

                MoveZombie()
                CheckOutOfRangeZombie()
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
        plrInfo.speed = 3
        plrInfo.damage = 1
        plrInfo.pos.x = 200
        plrInfo.pos.y = 490
        plrInfo.pos.height = 76
        plrInfo.pos.width = 65
        plrInfo.state = 2
        plrInfo.dir = True

        isSpawnPlayer = True
    End Sub

    Sub SpawnZombie()
        zomInfoSpawn.hp = 3
        zomInfoSpawn.speed = 1
        zomInfoSpawn.damage = 1
        zomInfoSpawn.pos.x = 900
        zomInfoSpawn.pos.y = 490
        zomInfoSpawn.pos.height = 76
        zomInfoSpawn.pos.width = 65
        zomInfoSpawn.state = 2

        spawnZombieList.Add(zomInfoSpawn)
    End Sub
    Sub MoveZombie()
        If spawnZombieList.Count() = 0 Then
            Return
        End If

        'speed 만큼 x좌표 수정
        For i As Integer = 0 To spawnZombieList.Count() - 1
            If CType(spawnZombieList(i), CharInfo).state >= 2 And CType(spawnZombieList(i), CharInfo).state <= 5 Then
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
            If CType(spawnZombieList(i), CharInfo).state >= 2 And CType(spawnZombieList(i), CharInfo).state <= 5 Then
                If CType(spawnZombieList(i), CharInfo).state = 5 Then
                    zomInfo.state = CType(spawnZombieList(i), CharInfo).state - 3         '이동 마지막 애니메이션 -> 이동 첫 애니메이션
                Else
                    zomInfo.state = CType(spawnZombieList(i), CharInfo).state + 1         '이동 애니메이션 ++
                End If
                zomInfo.pos.x = CType(spawnZombieList(i), CharInfo).pos.x - CType(spawnZombieList(i), CharInfo).speed       '이동일때만 speed만큼 좌표 - 하자
            Else
                zomInfo.pos.x = CType(spawnZombieList(i), CharInfo).pos.x
                zomInfo.state = CType(spawnZombieList(i), CharInfo).state                 '이동 상태 아니면 애니메이션 전환 X   <-상태 추가 시 수정해야함
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
