Imports System.Threading
Public Class Form1
    Dim thread_main As Thread
    Dim startTime As Long
    Dim currentTime As Long
    Dim lastTime As Long
    Dim lastZombieSpawnTime As Long
    Dim lastZombieAnimTime As Long

    Dim zombieImage As Image
    Dim zombieBitmap(10) As Bitmap

    Structure Rect
        Dim x As Integer
        Dim y As Integer
        Dim height As Integer
        Dim width As Integer
    End Structure

    '===========================================
    Structure ZombieInfo
        Dim hp As Integer
        Dim speed As Integer
        Dim damage As Integer
        Dim pos As Rect
        Dim state As Integer '0~1 / attack(idle), 2~5 / move, 6 / hit, 7~9 / die
    End Structure
    Dim zomInfoSpawn As ZombieInfo
    Dim zomInfo As ZombieInfo
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

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBitmap()

        thread_main = New Thread(AddressOf Main) With {.IsBackground = True}
    End Sub
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        thread_main.Start()
        startTime = DateTime.Now.Ticks
        currentTime = DateTime.Now.Ticks
        lastTime = DateTime.Now.Ticks
        lastZombieSpawnTime = DateTime.Now.Ticks
        lastZombieAnimTime = DateTime.Now.Ticks
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        For i As Integer = 0 To spawnZombieList.Count() - 1
            e.Graphics.DrawImage(zombieBitmap(CType(spawnZombieList(i), ZombieInfo).state), CType(spawnZombieList(i), ZombieInfo).pos.x, CType(spawnZombieList(i), ZombieInfo).pos.y, CType(spawnZombieList(i), ZombieInfo).pos.width, CType(spawnZombieList(i), ZombieInfo).pos.height)
        Next
    End Sub

    Private Sub Main()
        Do
            currentTime = DateTime.Now.Ticks

            If currentTime > lastTime + 333333 Then
                lastTime = currentTime

                MoveZombie()
                CheckOutOfRangeZombie()
                Invoke(Sub() Me.Invalidate())
            End If

            '좀비 애니메이션 수정 / 0.5초마다 이미지 전환
            If currentTime > lastZombieAnimTime + 5000000 Then
                lastZombieAnimTime = currentTime
                SwitchZombieAnim()
            End If

            '좀비스폰 5초로 고정시켜놓음, 이거 나중에 랜덤하게 수정
            If currentTime > lastZombieSpawnTime + 50000000 Then
                lastZombieSpawnTime = currentTime
                SpawnZombie()
            End If
        Loop
    End Sub

    Sub SpawnZombie()
        zomInfoSpawn.hp = 3
        zomInfoSpawn.speed = 1
        zomInfoSpawn.damage = 1
        zomInfoSpawn.pos.x = 900
        zomInfoSpawn.pos.y = 86 + (CInt(Int((5) * Rnd())) * 100)
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
            If CType(spawnZombieList(i), ZombieInfo).state >= 2 And CType(spawnZombieList(i), ZombieInfo).state <= 5 Then
                zomInfo.pos.x = CType(spawnZombieList(i), ZombieInfo).pos.x - CType(spawnZombieList(i), ZombieInfo).speed       '이동일때만 speed만큼 좌표 --
            Else
                Continue For                 '이동 상태 아니면 이동 X
            End If

            zomInfo.hp = CType(spawnZombieList(i), ZombieInfo).hp
            zomInfo.speed = CType(spawnZombieList(i), ZombieInfo).speed
            zomInfo.damage = CType(spawnZombieList(i), ZombieInfo).damage
            zomInfo.pos.y = CType(spawnZombieList(i), ZombieInfo).pos.y
            zomInfo.pos.height = CType(spawnZombieList(i), ZombieInfo).pos.height
            zomInfo.pos.width = CType(spawnZombieList(i), ZombieInfo).pos.width

            spawnZombieList(i) = zomInfo
        Next

    End Sub
    Sub CheckOutOfRangeZombie()
        If spawnZombieList.Count() = 0 Then
            Return
        End If

        '식물 뚫고 집에 도착 시 좀비 삭제, /  게임오버 처리 (게임오버 처리 추가 해야함)
        For i As Integer = 0 To spawnZombieList.Count() - 1
            If CType(spawnZombieList(i), ZombieInfo).pos.x <= 218 Then
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
            If CType(spawnZombieList(i), ZombieInfo).state >= 2 And CType(spawnZombieList(i), ZombieInfo).state <= 5 Then
                If CType(spawnZombieList(i), ZombieInfo).state = 5 Then
                    zomInfo.state = CType(spawnZombieList(i), ZombieInfo).state - 3         '이동 마지막 애니메이션 -> 이동 첫 애니메이션
                Else
                    zomInfo.state = CType(spawnZombieList(i), ZombieInfo).state + 1         '이동 애니메이션 ++
                End If
                zomInfo.pos.x = CType(spawnZombieList(i), ZombieInfo).pos.x - CType(spawnZombieList(i), ZombieInfo).speed       '이동일때만 speed만큼 좌표 - 하자
            Else
                zomInfo.state = CType(spawnZombieList(i), ZombieInfo).state                 '이동 상태 아니면 애니메이션 전환 X   <-상태 추가 시 수정해야함
            End If

            zomInfo.hp = CType(spawnZombieList(i), ZombieInfo).hp
            zomInfo.speed = CType(spawnZombieList(i), ZombieInfo).speed
            zomInfo.damage = CType(spawnZombieList(i), ZombieInfo).damage
            zomInfo.pos.y = CType(spawnZombieList(i), ZombieInfo).pos.y
            zomInfo.pos.height = CType(spawnZombieList(i), ZombieInfo).pos.height
            zomInfo.pos.width = CType(spawnZombieList(i), ZombieInfo).pos.width

            spawnZombieList(i) = zomInfo
        Next
    End Sub
    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        thread_main.Abort()
    End Sub

End Class
