Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Public Class Fit_Status

    Private WithEvents tm As New Timer
    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load

loop_back:
        '  Me.BackgroundWorker1.RunWorkerAsync()

        ' delay(1000)
        '  GoTo loop_back
        tm.Interval = 1000
        tm.Start()
        '   GoTo loop_back
    End Sub
    Private Sub tm_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tm.Tick
        number_running = 0
        Call CheckIfRunning()
        TextBox1.Text = number_running & " " & status_prc
        If number_running = 0 Then Close()
    End Sub


End Class