Imports System.Threading
Public Class Form4
    Private Declare Function GetTickCount64 Lib "kernel32" () As Long
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Dim currentTime As Long
    Dim lastTime As Long
    Dim lastPlayerAnimTime As Long

    Dim backImage As Image
    Dim backBitmap As Bitmap

    Dim ldImage As Image
    Dim ldBitmap As Bitmap

    Dim playerImage As Image
    Dim playerBitmap(4) As Bitmap

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

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        thread_main = New Thread(AddressOf Main) With {.IsBackground = True}

        currentTime = GetTickCount64()
        lastTime = GetTickCount64()
        lastPlayerAnimTime = GetTickCount64()
    End Sub
    Private Sub Form4_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
    End Sub
    Sub SpawnPlayer()
        plrInfo.hp = 3
        plrInfo.pos.x = 100
        plrInfo.pos.y = 287
        plrInfo.anim = 0
        plrInfo.state = 0
    End Sub
    Sub LoadBitmap()
        backImage = My.Resources.ResourceManager.GetObject("backImage")
        backBitmap = New Bitmap(backImage)
        backBitmap.MakeTransparent()

        ldImage = My.Resources.ResourceManager.GetObject("loadimage")
        ldBitmap = New Bitmap(ldImage)
        ldBitmap.MakeTransparent()

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
    End Sub

    Private Sub Main()

        Do
            currentTime = GetTickCount64()

            If currentTime > lastTime + 33 Then
                lastTime = currentTime
                ScrollMap()
                SetState()
                SwichPlayerAnim()

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

        ldRect.x -= 6
        ldRect2.x -= 6
        ldRect3.x -= 6

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
        End If
    End Sub
    Private Sub Form4_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        e.Graphics.DrawImage(backBitmap, backMap.x, 0, backBitmap.Width, backBitmap.Height)
        e.Graphics.DrawImage(backBitmap, backMap2.x, 0, backBitmap.Width, backBitmap.Height)

        e.Graphics.DrawImage(ldBitmap, ldRect.x, ldRect.y, ldBitmap.Width, ldBitmap.Height)
        e.Graphics.DrawImage(ldBitmap, ldRect2.x, ldRect2.y, ldBitmap.Width, ldBitmap.Height)
        e.Graphics.DrawImage(ldBitmap, ldRect3.x, ldRect3.y, ldBitmap.Width, ldBitmap.Height)

        e.Graphics.DrawImage(playerBitmap(plrInfo.anim), plrInfo.pos.x, plrInfo.pos.y, playerBitmap(plrInfo.anim).Width, playerBitmap(plrInfo.anim).Height)
    End Sub
    Private Sub Form4_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        thread_main.Abort()
    End Sub
End Class