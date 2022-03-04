Imports System.IO
Imports System.Diagnostics

Public Class Form12
    Public watchfolder As FileSystemWatcher
    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_startwatch.Click
        watchfolder = New System.IO.FileSystemWatcher()

        'this is the path we want to monitor
        watchfolder.Path = txt_watchpath.Text

        'Add a list of Filter we want to specify
        'make sure you use OR for each Filter as we need to
        'all of those 

        watchfolder.NotifyFilter = IO.NotifyFilters.DirectoryName
        watchfolder.NotifyFilter = watchfolder.NotifyFilter Or
                           IO.NotifyFilters.FileName
        watchfolder.NotifyFilter = watchfolder.NotifyFilter Or
                           IO.NotifyFilters.Attributes

        ' add the handler to each event
        AddHandler watchfolder.Changed, AddressOf logchange
        AddHandler watchfolder.Created, AddressOf logchange
        AddHandler watchfolder.Deleted, AddressOf logchange

        ' add the rename handler as the signature is different
        AddHandler watchfolder.Renamed, AddressOf logrename

        'Set this property to true to start watching
        watchfolder.EnableRaisingEvents = True

        btn_startwatch.Enabled = False
        btn_stop.Enabled = True

        'End of code for btn_start_click
    End Sub

    Private Sub logchange(ByVal source As Object, ByVal e As _
                        System.IO.FileSystemEventArgs)
        If e.ChangeType = IO.WatcherChangeTypes.Changed Then
            txt_folderactivity.Text &= "File " & e.FullPath &
                                    " has been modified" & vbCrLf
        End If
        If e.ChangeType = IO.WatcherChangeTypes.Created Then
            txt_folderactivity.Text &= "File " & e.FullPath &
                                     " has been created" & vbCrLf
        End If
        If e.ChangeType = IO.WatcherChangeTypes.Deleted Then
            txt_folderactivity.Text &= "File " & e.FullPath &
                                    " has been deleted" & vbCrLf
        End If
    End Sub

    Public Sub logrename(ByVal source As Object, ByVal e As _
                            System.IO.RenamedEventArgs)
        txt_folderactivity.Text &= "File" & e.OldName &
                      " has been renamed to " & e.Name & vbCrLf
    End Sub

    Private Sub btn_stop_Click(sender As Object, e As EventArgs) Handles btn_stop.Click
        ' Stop watching the folder
        watchfolder.EnableRaisingEvents = False
        btn_startwatch.Enabled = True
        btn_stop.Enabled = False
    End Sub
End Class