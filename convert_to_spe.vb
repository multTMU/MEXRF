Imports System.IO
Imports System.Text
Imports System.Math
Public Class Convert_CNF_Files

    Public sum_file_names(99), convert_file_name, datafilenames_for_sum As String
    Public input_to_spe(4096), sum_spec_array(4096, 99), live_time_array(99), real_time_array(99), sample_temp_array(99)
    Public temp_de, temp_e_0, sum_live, sum_real, sample_temp_avg

    Private Sub IN_16K_BOX_CheckedChanged(sender As Object, e As EventArgs) Handles IN_16K_BOX.CheckedChanged
        If IN_16K_BOX.Checked = True Then
            IN_2K_BOX.Checked = False
            IN_4K_BOX.Checked = False
            IN_8K_BOX.Checked = False

        End If
    End Sub

    Private Sub Out_2K_Box_CheckedChanged(sender As Object, e As EventArgs) Handles Out_2K_Box.CheckedChanged
        If Out_2K_Box.Checked = True Then
            Out_4K_Box.Checked = False
        End If
    End Sub

    Private Sub Out_4K_Box_CheckedChanged(sender As Object, e As EventArgs) Handles Out_4K_Box.CheckedChanged
        If Out_4K_Box.Checked = True Then
            Out_2K_Box.Checked = False
        End If
    End Sub

    Private Sub IN_8K_BOX_CheckedChanged(sender As Object, e As EventArgs) Handles IN_8K_BOX.CheckedChanged
        If IN_8K_BOX.Checked = True Then
            IN_2K_BOX.Checked = False
            IN_4K_BOX.Checked = False
            IN_16K_BOX.Checked = False

        End If
    End Sub

    Private Sub IN_4K_BOX_CheckedChanged(sender As Object, e As EventArgs) Handles IN_4K_BOX.CheckedChanged
        If IN_4K_BOX.Checked = True Then
            IN_2K_BOX.Checked = False
            IN_8K_BOX.Checked = False
            IN_16K_BOX.Checked = False

        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles IN_2K_BOX.CheckedChanged
        If IN_2K_BOX.Checked = True Then
            IN_4K_BOX.Checked = False
            IN_8K_BOX.Checked = False
            IN_16K_BOX.Checked = False
            Out_2K_Box.Checked = True
            Out_4K_Box.Checked = False
        End If
    End Sub

    Public num_sum As Integer
    Public s_n_cycle As Integer
    Private Sub Convert_CNF_Files_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Read_status_label.Text = ""

    End Sub

    Private Sub select_files_Button_Click(sender As Object, e As EventArgs) Handles select_files_Button.Click

        'Select and Read Data Files
        '
        Dim LastDir As String = ""

        Dim idex As Integer

        Dim Response1 As String
        Dim Newdata1 As Integer
        Dim outfilename(99) As String

        max_channels = 2048
        If IN_4K_BOX.Checked Then max_channels = 4096
        If IN_8K_BOX.Checked Then max_channels = 8192
        If IN_16K_BOX.Checked Then max_channels = 16382

        ' file names for input to xrf_fit_###.exe
        For i = 1 To 99
            outfilename(i) = exe_dir_name & "\spec_fit_" & i - 1 & ".txt"
        Next i

        ListBox1.Items.Clear()

        Dim fbd As New OpenFileDialog
        If LastDir = "" Then Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)

        With fbd
            .Title = "Select multiple XRF files"
            ' .InitialDirectory = LastDir
            .InitialDirectory = resultfiledir
            .Multiselect = True 'allow user to select multiple items (files)
        End With

        idex = 0
        If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then ' check user click ok or cancel

            LastDir = Path.GetDirectoryName(fbd.FileName) 'Update your last Directory.

            For Each mFile As String In fbd.FileNames
                idex = idex + 1
                sum_file_names(idex) = mFile
                ListBox1.Items.Add(mFile)
            Next
        End If

        num_sum = idex
        If num_sum = 0 Then Return

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim temp_file As String
        Dim real_time1, live_time1, vial_diameter1, sample_temperature1, U_enrichment1, Pu_weight1
        Dim n_files As Integer
        Dim chan_scale As Boolean

        If ListBox1.SelectedItem = "" Then Return
        n_files = Val(ListBox1.Items.Count.ToString())
        s_n_cycle = ListBox1.SelectedIndex + 1


        Call get_xray_data_file(xray_data_file_name, fil_label_X)

        temp_file = ListBox1.SelectedItem.ToString()

        datafilenames_for_sum = temp_file



        '   Call read_spe_data_file()
        Read_status_label.Text = "Reading Files"
        Application.DoEvents()
10:
        Call CheckIfReadingCNF()  ' check to see if any other genie2k read is in process
        If number_reads_going > 0 Then GoTo 10


        If Strings.Right(datafilenames_for_sum, 3) = "cnf" Or Strings.Right(datafilenames_for_sum, 3) = "CNF" Then Call read_cnf_data_file(datafilenames_for_sum)
        '   If Strings.Right(datafilenames_for_sum, 3) = "spe" Or Strings.Right(datafilenames_for_sum, 3) = "SPE" Then Call read_spe_data_file()
        If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then Call split_file(datafilenames_for_sum)



        If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then e_0 = results_arr(15)
        If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then d_e = results_arr(16)

        Read_status_label.Text = ""
        Application.DoEvents()

        Me.SampleIDBox1.Text = Sample_ID
        Me.sampleinfoBox1.Text = vial_diameter
        Me.sampleinfoBox2.Text = sample_temperature
        Me.sampleinfoBox3.Text = U_enrichment
        Me.sampleinfoBox4.Text = Pu_weight
        Me.sampleinfoBox5.Text = live_time
        Me.in_chan_box.Text = num_chans_in
        Me.filenamebox1.Text = datafilenames_for_sum
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        ' saves summed spectrum as in SPE format
        '
        Dim idex, st_pos, st_len As Integer
        Dim spe_files(99) As String


        ' read in each of the selected files

        For i_file = 1 To num_sum
            datafilenames_for_sum = sum_file_names(i_file)

            Read_status_label.Text = "Reading Files"
            Application.DoEvents()
10:
            Call CheckIfReadingCNF()  ' check to see if any other genie2k read is in process
            MsgBox(" reads " & number_reads_going)
            If number_reads_going > 0 Then GoTo 10

            If Strings.Right(datafilenames_for_sum, 3) = "cnf" Or Strings.Right(datafilenames_for_sum, 3) = "CNF" Then Call read_cnf_data_file(datafilenames_for_sum)
            '    If Strings.Right(datafilenames_for_sum, 3) = "spe" Or Strings.Right(datafilenames_for_sum, 3) = "SPE" Then Call read_spe_data_file()
            If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then Call split_file(datafilenames_for_sum)


            temp_e_0 = e_0
            temp_de = d_e

            If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then temp_e_0 = results_arr(15)
            If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then temp_de = results_arr(16)

            Read_status_label.Text = ""
            Application.DoEvents()

            '  strip off path to obtain root file name
            Dim spectra_name, new_result_name As String

            spectra_name = datafilenames_for_sum

            st_pos = Strings.InStrRev(spectra_name, "\")
            st_len = Strings.Len(spectra_name)

            convert_file_name = convertfiledir & Strings.Mid(spectra_name, st_pos + 1, st_len - 4 - st_pos) & ".spe"      ' extract file name from path and change input file to .spe file


            sum_live = live_time
            sum_real = real_time
            sample_temp_avg = sample_temperature


            Call save_spe_file(convert_file_name, raw_data)

        Next i_file




    End Sub


    Sub save_spe_file(summed_file_name, summed_dat)
        Dim templine As String
        Dim out_data(4096), grouped_data
        Dim n_group, n_out_channels As Integer

        n_group = Int(num_chans_in / 2048 + 0.1)
        If Out_4K_Box.Checked = True Then n_group = Int(num_chans_in / 4096)

        n_out_channels = 2048
        If Out_4K_Box.Checked = True Then n_out_channels = 4096

        templine = ""
        n_channels = max_channels
        templine = templine & "$SPEC_ID:" & vbCrLf
        templine = templine & summed_file_name & vbCrLf
        templine = templine & "$SPEC_REM:" & vbCrLf
        templine = templine & "Sample ID: " & Sample_ID & vbCrLf
        templine = templine & "$DATE_MEA: " & str_date & vbCrLf
        templine = templine & acq_date_temp & " " & acq_time_temp & vbCrLf
        templine = templine & "$MEAS_TIM: " & vbCrLf
        templine = templine & sum_live & " " & sum_real & vbCrLf
        templine = templine & "$ENER_FIT: " & vbCrLf
        templine = templine & Int(100000 * temp_e_0 * n_group) / 100000 & " " & Int(100000 * temp_de * n_group) / 100000 & vbCrLf
        templine = templine & "$KED DECLARED U235 ENRICHMENT: " & vbCrLf
        templine = templine & U_enrichment & vbCrLf
        templine = templine & "$KED DECLARED PU ATOMIC WEIGHT: " & vbCrLf
        templine = templine & Pu_weight & vbCrLf
        templine = templine & "$KED PATH LENGTH: " & vbCrLf
        templine = templine & vial_diameter & vbCrLf
        templine = templine & "$KED SAMPLE TEMPERATURE: " & vbCrLf
        templine = templine & sample_temp_avg & vbCrLf
        templine = templine & "$DATA: " & vbCrLf
        templine = templine & "0 " & n_out_channels - 1 & vbCrLf

        Dim fs As FileStream = File.Create(summed_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(templine)
        fs.Write(info, 0, info.Length)

        fs.Close()
        '  MsgBox("chan in =  " & num_chans_in & " out = " & n_out_channels & " n_group = " & n_group)

        '    For i = 1 To num_chans_in Step n_group
        '    grouped_data = 0
        '    For j = 1 To n_group
        '    grouped_data = grouped_data + summed_dat(i + j - 1)
        '    Next j
        '   out_data(Int((i + 1) / 2 + 0.01)) = grouped_data
        '    Next i

        Dim k_index As Integer
        For i = 1 To n_out_channels
            grouped_data = 0
            For j = 1 To n_group
                k_index = (i - 1) * n_group + j
                grouped_data = grouped_data + summed_dat(k_index)
            Next j
            out_data(i) = grouped_data
        Next i




        For i = 1 To n_out_channels
            templine = out_data(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(summed_file_name, templine, True)

        Next i

        '   MsgBox("summed_dat(3750) = " & summed_dat(3750))

        '  For i = 1 To max_channels
        '  'create temp spec file for input to XRF_FIT
        '   templine = summed_dat(i) & vbCrLf
        '  My.Computer.FileSystem.WriteAllText(summed_file_name, templine, True)
        '   Next i

    End Sub

End Class