﻿Imports System.Threading
Public Class Form1
    Private Declare Function GetTickCount64 Lib "kernel32" () As Long
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Dim count = 0

    Dim thread_main As Thread
    Dim currentTime As Long
    Dim lastTime As Long
    Dim lastPlayerAnimTime As Long
    Dim lastMonsterAnimTime As Long
    Dim hitMonsterTime As Long
    Dim switchMonsterStateTime As Long

    Dim velocity As Long

    '==============================
    Structure CharInfo
        Dim hp As Integer
        Dim speed As Integer
        Dim damage As Integer
        Dim pos As Rect
        Dim anim As Integer '0~1 (leftIdle), 2~3 (rightIdle), 4~7 (leftMove), 891011 (rightMove)        /monster : 0~1 : (left Idle), 2~3 (right idle), 4~ move
        Dim dir As Boolean  'left : false,  right : true
        Dim state As Integer '0 : idle, 1 : move, 2 : attack, 3 : hit, 4 : die
    End Structure

    Dim isAttack As Boolean
    Dim isJump As Boolean
    Dim isMove As Boolean

    Dim playerImage As Image
    Dim playerBitmap(28) As Bitmap

    Dim isSpawnPlayer As Boolean
    Dim plrInfo As CharInfo
    '===============================
    Structure Rect
        Dim x As Integer
        Dim y As Integer
        Dim height As Integer
        Dim width As Integer
    End Structure

    '===========================================
    Dim isAttack_Monster As Boolean
    Dim isJump_Monster As Boolean
    Dim isMove_Monster As Boolean
    Dim ishitMonster As Boolean
    Dim isSwitchStateMonster As Boolean

    Dim randomNum As Integer

    Dim monsterImage As Image
    Dim monsterBitmap(20) As Bitmap

    Dim isSpawnMonster As Boolean
    Dim monsterInfo As CharInfo
    '===========================================   / monster 관련 변수

    Sub LoadBitmap()
        monsterImage = My.Resources.ResourceManager.GetObject("1_idle")
        monsterBitmap(0) = New Bitmap(monsterImage)
        monsterBitmap(0).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("2_idle")
        monsterBitmap(1) = New Bitmap(monsterImage)
        monsterBitmap(1).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("3_idle")
        monsterBitmap(2) = New Bitmap(monsterImage)
        monsterBitmap(2).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("4_idle")
        monsterBitmap(3) = New Bitmap(monsterImage)
        monsterBitmap(3).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("5_move")
        monsterBitmap(4) = New Bitmap(monsterImage)
        monsterBitmap(4).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("6_move")
        monsterBitmap(5) = New Bitmap(monsterImage)
        monsterBitmap(5).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("7_move")
        monsterBitmap(6) = New Bitmap(monsterImage)
        monsterBitmap(6).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("8_move")
        monsterBitmap(7) = New Bitmap(monsterImage)
        monsterBitmap(7).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("9_move")
        monsterBitmap(8) = New Bitmap(monsterImage)
        monsterBitmap(8).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("10_move")
        monsterBitmap(9) = New Bitmap(monsterImage)
        monsterBitmap(9).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("11_move")
        monsterBitmap(10) = New Bitmap(monsterImage)
        monsterBitmap(10).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("12_move")
        monsterBitmap(11) = New Bitmap(monsterImage)
        monsterBitmap(11).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("13_hit")
        monsterBitmap(12) = New Bitmap(monsterImage)
        monsterBitmap(12).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("14_hit")
        monsterBitmap(13) = New Bitmap(monsterImage)
        monsterBitmap(13).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("15_die")
        monsterBitmap(14) = New Bitmap(monsterImage)
        monsterBitmap(14).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("16_die")
        monsterBitmap(15) = New Bitmap(monsterImage)
        monsterBitmap(15).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("17_die")
        monsterBitmap(16) = New Bitmap(monsterImage)
        monsterBitmap(16).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("18_die")
        monsterBitmap(17) = New Bitmap(monsterImage)
        monsterBitmap(17).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("19_die")
        monsterBitmap(18) = New Bitmap(monsterImage)
        monsterBitmap(18).MakeTransparent()

        monsterImage = My.Resources.ResourceManager.GetObject("20_die")
        monsterBitmap(20) = New Bitmap(monsterImage)
        monsterBitmap(20).MakeTransparent()
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
        '========================================================================jump
        playerImage = My.Resources.ResourceManager.GetObject("Char_25_jump")
        playerBitmap(24) = New Bitmap(playerImage)
        playerBitmap(24).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_26_jump")
        playerBitmap(25) = New Bitmap(playerImage)
        playerBitmap(25).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_27_jump_move")
        playerBitmap(26) = New Bitmap(playerImage)
        playerBitmap(26).MakeTransparent()

        playerImage = My.Resources.ResourceManager.GetObject("Char_28_jump_move")
        playerBitmap(27) = New Bitmap(playerImage)
        playerBitmap(27).MakeTransparent()

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBitmap()
        isSpawnPlayer = False
        isSpawnMonster = False
        isAttack = False
        isJump = False
        isMove = False
        velocity = 20
        thread_main = New Thread(AddressOf Main) With {.IsBackground = True}
    End Sub
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        thread_main.Start()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        If isSpawnMonster = True Then
            e.Graphics.DrawImage(monsterBitmap(monsterInfo.anim), monsterInfo.pos.x, monsterInfo.pos.y, monsterInfo.pos.width, monsterInfo.pos.height)
        End If

        If isSpawnPlayer = True Then
            e.Graphics.DrawImage(playerBitmap(plrInfo.anim), plrInfo.pos.x, plrInfo.pos.y, plrInfo.pos.width, plrInfo.pos.height)
        End If
    End Sub

    Private Sub Main()
        lastTime = GetTickCount64()
        lastMonsterAnimTime = GetTickCount64()
        lastPlayerAnimTime = GetTickCount64()
        hitMonsterTime = GetTickCount64()

        SpawnMonster()
        SpawnPlayer()
        Do
            currentTime = GetTickCount64()

            If currentTime > lastTime + 33 Then
                lastTime = currentTime

                SetStateMonster()
                SwitchMonsterAnim()

                Label2.Text = monsterInfo.state

                InputKeyPlayer()
                SwitchPlayerAnim()
                Invoke(Sub() Me.Invalidate())

                Jump()
                Attack_Player()
                ClearState()
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
        If GetAsyncKeyState(Keys.Menu) And isJump = False Then    'ctrl key input
            plrInfo.state = 3
            isJump = True
        ElseIf GetAsyncKeyState(Keys.Left) And isAttack = False Then         'left key input
            If plrInfo.state <> 3 Then
                plrInfo.state = 1
            End If

            plrInfo.dir = False
            plrInfo.pos.x -= plrInfo.speed
            isMove = True
        ElseIf GetAsyncKeyState(Keys.Right) And isAttack = False Then    'right key input
            If plrInfo.state <> 3 Then
                plrInfo.state = 1
            End If

            plrInfo.dir = True
            plrInfo.pos.x += plrInfo.speed
            isMove = True
        ElseIf GetAsyncKeyState(Keys.LControlKey) And isAttack = False Then    'ctrl key input
            plrInfo.state = 2

            isAttack = True
            isMove = False
        ElseIf isJump = False And isAttack = False Then
            plrInfo.state = 0
            isMove = False
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
        ElseIf plrInfo.state = 2 Then       'attack
            If plrInfo.dir = False Then         'left attack
                If plrInfo.anim >= 12 And plrInfo.anim <= 17 Then     'attack anim ++
                    If currentTime > lastPlayerAnimTime + 80 Then     'attack anim swiching cooltime
                        lastPlayerAnimTime = currentTime

                        plrInfo.anim += 1
                        If plrInfo.anim > 17 Then    'left attack init
                            plrInfo.anim = 12
                        End If
                    End If
                Else                                'left attack init
                    plrInfo.anim = 12
                End If
            Else                                'right attack
                If plrInfo.anim >= 18 And plrInfo.anim <= 23 Then    'attack anim ++
                    If currentTime > lastPlayerAnimTime + 80 Then      'attack anim swiching cooltime
                        lastPlayerAnimTime = currentTime

                        plrInfo.anim += 1
                        If plrInfo.anim > 23 Then   'right attack init
                            plrInfo.anim = 18
                        End If
                    End If
                Else                                'right attack init
                    plrInfo.anim = 18
                End If
            End If
        ElseIf plrInfo.state = 3 Then       'jump
            If plrInfo.dir = False Then         'left jump
                If isMove = False Then
                    plrInfo.anim = 24
                Else
                    plrInfo.anim = 26
                End If
            Else                                'right jump
                If isMove = False Then
                    plrInfo.anim = 25
                Else
                    plrInfo.anim = 27
                End If
            End If
        End If

    End Sub
    Sub Attack_Player()
        If ishitMonster = True Then     '몬스터 무적 아닐때만 공격판정
            Return
        End If

        If plrInfo.anim = 14 Then
            Dim startX = plrInfo.pos.x
            Dim endX = plrInfo.pos.x + 35
            If endX >= monsterInfo.pos.x And startX <= (monsterInfo.pos.x + monsterInfo.pos.width) Then

                If isMove_Monster = False And isAttack_Monster = False Then
                    count += 1
                    Label1.Text = count

                    monsterInfo.hp -= 1
                    ishitMonster = True
                    hitMonsterTime = GetTickCount64()
                    monsterInfo.state = 2
                End If
            End If
        ElseIf plrInfo.anim = 15 Then
            Dim startX = plrInfo.pos.x
            Dim endX = plrInfo.pos.x + 35
            If endX >= monsterInfo.pos.x And startX <= (monsterInfo.pos.x + monsterInfo.pos.width) Then

                If isMove_Monster = False And isAttack_Monster = False Then
                    count += 1
                    Label1.Text = count

                    monsterInfo.hp -= 1
                    ishitMonster = True
                    hitMonsterTime = GetTickCount64()
                    monsterInfo.state = 2
                End If
            End If
        ElseIf plrInfo.anim = 20 Then
            Dim startX = plrInfo.pos.x + plrInfo.pos.width
            Dim endX = plrInfo.pos.x + plrInfo.pos.width - 33
            If endX <= (monsterInfo.pos.x + monsterInfo.pos.width) And startX >= monsterInfo.pos.x Then

                If isMove_Monster = False And isAttack_Monster = False Then
                    count += 1
                    Label1.Text = count

                    monsterInfo.hp -= 1
                    ishitMonster = True
                    hitMonsterTime = GetTickCount64()
                    monsterInfo.state = 2
                End If
            End If
        ElseIf plrInfo.anim = 21 Then
            Dim startX = plrInfo.pos.x + plrInfo.pos.width
            Dim endX = plrInfo.pos.x + plrInfo.pos.width - 33
            If endX <= (monsterInfo.pos.x + monsterInfo.pos.width) And startX >= monsterInfo.pos.x Then

                If isMove_Monster = False And isAttack_Monster = False Then
                    count += 1
                    Label1.Text = count

                    monsterInfo.hp -= 1
                    ishitMonster = True
                    hitMonsterTime = GetTickCount64()
                    monsterInfo.state = 2
                End If
            End If
        End If

    End Sub
    Sub Hit_Player()
        plrInfo.hp -= 1
        '무적시간 + 밀려나는거 추가
    End Sub
    Sub ClearState()
        If plrInfo.anim = 17 Then       'left attack
            isAttack = False
        ElseIf plrInfo.anim = 23 Then   'right attack
            isAttack = False
        End If
    End Sub
    Sub Jump()
        If isJump = True Then
            plrInfo.pos.y -= velocity
            velocity -= 4


            If plrInfo.pos.y >= 477 Then
                velocity = 20
                isJump = False
                isAttack = False
                isMove = False
                plrInfo.state = 0
            End If
        End If

    End Sub
    Sub SpawnMonster()
        monsterInfo.hp = 5
        monsterInfo.speed = 7
        monsterInfo.damage = 1
        monsterInfo.pos.x = 700
        monsterInfo.pos.y = 338
        monsterInfo.pos.height = 228
        monsterInfo.pos.width = 195
        monsterInfo.anim = 0
        monsterInfo.dir = False
        monsterInfo.state = 0

        isAttack_Monster = False
        isJump_Monster = False
        isMove_Monster = False
        ishitMonster = False
        isSwitchStateMonster = True

        isSpawnMonster = True
    End Sub

    Sub SwitchMonsterAnim()
        If monsterInfo.state = 0 Then           'idle
            If monsterInfo.dir = False Then         'left idle
                If monsterInfo.anim >= 0 And monsterInfo.anim <= 1 Then     'idle anim ++
                    If currentTime > lastMonsterAnimTime + 1000 Then     'idle anim swiching cooltime
                        lastMonsterAnimTime = currentTime

                        monsterInfo.anim += 1
                        If monsterInfo.anim > 1 Then    'left idle init
                            monsterInfo.anim = 0
                        End If
                    End If
                Else                                'left idle init
                    monsterInfo.anim = 0
                End If
            Else                                'right idle
                If monsterInfo.anim >= 2 And monsterInfo.anim <= 3 Then     'idle anim ++
                    If currentTime > lastMonsterAnimTime + 1000 Then     'idle anim swiching cooltime
                        lastMonsterAnimTime = currentTime

                        monsterInfo.anim += 1
                        If monsterInfo.anim > 3 Then    'right idle init
                            monsterInfo.anim = 2
                        End If
                    End If
                Else                                'right idle init
                    monsterInfo.anim = 2
                End If
            End If
        ElseIf monsterInfo.state = 1 Then
            If monsterInfo.dir = False Then         'left move
                If monsterInfo.anim >= 4 And monsterInfo.anim <= 7 Then     'move anim ++
                    If currentTime > lastMonsterAnimTime + 225 Then     'move anim swiching cooltime
                        lastMonsterAnimTime = currentTime

                        monsterInfo.anim += 1
                        If monsterInfo.anim > 7 Then    'left move init
                            monsterInfo.anim = 4
                        End If
                    End If
                Else                                'left move init
                    monsterInfo.anim = 4
                End If
            Else                                'right move
                If monsterInfo.anim >= 8 And monsterInfo.anim <= 11 Then     'move anim ++
                    If currentTime > lastMonsterAnimTime + 225 Then     'move anim swiching cooltime
                        lastMonsterAnimTime = currentTime

                        monsterInfo.anim += 1
                        If monsterInfo.anim > 11 Then    'right move init
                            monsterInfo.anim = 8
                        End If
                    End If
                Else                                'right move init
                    monsterInfo.anim = 8
                End If
            End If
        ElseIf monsterInfo.state = 2 Then       'hit
            If monsterInfo.dir = False Then     'left hit
                monsterInfo.anim = 12
            Else
                monsterInfo.anim = 13           'right hit
            End If
        End If
    End Sub
    Sub SetStateMonster()
        If monsterInfo.hp <= 0 Then     '죽는 처리
            isSpawnMonster = False
        End If

        If ishitMonster = True Then       '무적시간 1초
            If currentTime > hitMonsterTime + 1000 Then     '0.7초 후 무적 해제
                ishitMonster = False

                If isAttack_Monster = False And isMove_Monster = False Then
                    monsterInfo.state = 0
                End If
            End If
        End If

        If isSwitchStateMonster = True Then
            isSwitchStateMonster = False
            Randomize()

            Dim randomizeNum = CInt(CInt(2 * Rnd()) + 1)
            randomNum = CInt(Rnd())
            If randomizeNum = 1 Then                           'idle
                switchMonsterStateTime = currentTime
                monsterInfo.state = 0
            ElseIf randomizeNum = 2 Then                       'move
                switchMonsterStateTime = currentTime
                monsterInfo.state = 1
            ElseIf randomizeNum = 3 Then                       'attack
                switchMonsterStateTime = currentTime
                monsterInfo.state = 3
            End If
        End If

        isMove_Monster = False
        isAttack_Monster = False

        If monsterInfo.state = 0 Then
            MonsterIdle()
        ElseIf monsterInfo.state = 1 Then
            MonsterMove(randomNum)
        ElseIf monsterInfo.state = 3 Then
            MonsterAttack()
        End If
    End Sub
    Sub MonsterIdle()
        If currentTime > switchMonsterStateTime + 2000 Then
            switchMonsterStateTime = currentTime
            isSwitchStateMonster = True
        End If
    End Sub
    Sub MonsterMove(i As Integer)
        isMove_Monster = True

        If i = 0 Then                   'left
            monsterInfo.dir = False
            monsterInfo.pos.x -= monsterInfo.speed
        Else                             'right
            monsterInfo.dir = True
            monsterInfo.pos.x += monsterInfo.speed
        End If

        If monsterInfo.pos.x <= -15 Then
            monsterInfo.pos.x = -15
        ElseIf monsterInfo.pos.x >= 870 Then
            monsterInfo.pos.x = 870
        End If

        If currentTime > switchMonsterStateTime + CInt(CInt(4000 * Rnd()) + 1000) Then
            switchMonsterStateTime = currentTime
            isSwitchStateMonster = True
        End If
    End Sub
    Sub MonsterAttack()
        isAttack_Monster = True

        If currentTime > switchMonsterStateTime + 2000 Then
            switchMonsterStateTime = currentTime
            isSwitchStateMonster = True
        End If
    End Sub
    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        thread_main.Abort()
    End Sub

End Class
