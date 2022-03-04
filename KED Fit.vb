Public Class Form1
    Public free_params(28)
    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        '   MessageBox.Show("You are in the CheckedListBox.Click event.")

    End Sub

    Private Sub CheckedListBox3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CheckedListBox3.SelectedIndexChanged

    End Sub

    Private Sub CheckedListBox2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CheckedListBox2.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub checkbox_1()
        Dim i As Integer
        Dim s As String
        For i = 1 To 28
            free_params(i) = 0
        Next i
        s = "Checked Items:" & ControlChars.CrLf
        For i = 0 To (CheckedListBox1.Items.Count - 1)
            If CheckedListBox1.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & CheckedListBox1.Items(i).ToString & ControlChars.CrLf
                free_params(i + 1) = 1
            End If
        Next
        MessageBox.Show(s)
        MessageBox.Show(free_params(3))


    End Sub
    Private Sub checkbox_1a()
        ' Determine if there are any items checked.
        If CheckedListBox1.CheckedItems.Count <> 0 Then
            ' If so, loop through all checked items and print results.
            Dim x As Integer
            Dim s As String = ""
            For x = 0 To CheckedListBox1.CheckedItems.Count - 1
                s = s & "Checked Item " & (x + 1).ToString & " = " & CheckedListBox1.CheckedItems(x).ToString & ControlChars.CrLf
            Next x
            MessageBox.Show(s)
        End If



    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Call checkbox_1()
    End Sub
End Class

