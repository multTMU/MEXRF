Imports System.IO
Imports System.Text
Imports System.Math
Public Class summed_spectra
    Public sum_file_names(99), datafilenames_for_sum As String
    Public input_to_spe(4096), sum_spec_array(4096, 99), live_time_array(99), real_time_array(99), sample_temp_array(99)
    Public temp_de, temp_e_0, sum_live, sum_real, sample_temp_avg
    Public num_sum As Integer
    Public s_n_cycle As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        If nchan_checkbox.Checked Then max_channels = 4096

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
        If Strings.Right(datafilenames_for_sum, 3) = "cnf" Or Strings.Right(datafilenames_for_sum, 3) = "CNF" Then Call read_cnf_data_file(datafilenames_for_sum)
        If Strings.Right(datafilenames_for_sum, 3) = "spe" Or Strings.Right(datafilenames_for_sum, 3) = "SPE" Then Call read_spe_data_file()
        If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then Call split_file(datafilenames_for_sum)



        If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then e_0 = results_arr(15)
        If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then d_e = results_arr(16)




        Me.SampleIDBox1.Text = Sample_ID
        Me.sampleinfoBox1.Text = vial_diameter
        Me.sampleinfoBox2.Text = sample_temperature
        Me.sampleinfoBox3.Text = U_enrichment
        Me.sampleinfoBox4.Text = Pu_weight
        Me.sampleinfoBox5.Text = live_time
        Me.filenamebox1.Text = datafilenames_for_sum
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        ' saves summed spectrum as in SPE format
        '
        Dim idex As Integer
        Dim spe_files(99) As String


        ' read in each of the selected files

        For i_file = 1 To num_sum
            datafilenames_for_sum = sum_file_names(i_file)

            If Strings.Right(datafilenames_for_sum, 3) = "cnf" Or Strings.Right(datafilenames_for_sum, 3) = "CNF" Then Call read_cnf_data_file(datafilenames_for_sum)
            If Strings.Right(datafilenames_for_sum, 3) = "spe" Or Strings.Right(datafilenames_for_sum, 3) = "SPE" Then Call read_spe_data_file()
            If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then Call split_file(datafilenames_for_sum)

            temp_e_0 = e_0
            temp_de = d_e

            If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then temp_e_0 = results_arr(15)
            If Strings.Right(datafilenames_for_sum, 3) = "res" Or Strings.Right(datafilenames_for_sum, 3) = "RES" Then temp_de = results_arr(16)

            live_time_array(i_file) = live_time
            real_time_array(i_file) = real_time
            sample_temp_array(i_file) = sample_temperature
            For j = 1 To max_channels
                sum_spec_array(j, i_file) = raw_data(j)
            Next j
        Next i_file


        Dim sum_data(4096)

        sum_live = 0
        sum_real = 0
        sample_temp_avg = 0
        For i = 1 To num_sum
            sum_live = sum_live + live_time_array(i)
            sum_real = sum_real + real_time_array(i)
            sample_temp_avg = sample_temp_avg + sample_temp_array(i) / num_sum
        Next i


        For i = 1 To max_channels
            sum_data(i) = 0
            For j = 1 To num_sum
                sum_data(i) = sum_data(i) + sum_spec_array(i, j)
            Next j
        Next


        ' save as new spe fi.le
        Dim summed_file_name As String

        SaveFileDialog1.Filter = "TXT Files (*.spe*)|*.spe"
        SaveFileDialog1.InitialDirectory = root_dir_name

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
            Then
            My.Computer.FileSystem.WriteAllText _
                (SaveFileDialog1.FileName, "", True)

            summed_file_name = SaveFileDialog1.FileName
        End If

        If summed_file_name = "" Then Return

        Call save_spe_file(summed_file_name, sum_data, num_sum)

    End Sub

    Public Sub read_spe_data_file()
        ' reads file in text format from datafilenames_for_sum
        ' first find energy cal
        ' then locate $DATA:

        Dim n_chan, idex, jdex, chan_start, i_trip, i_tot, tim_flag, i_lr, ener_flag As Integer
        Dim vial_flag, temper_flag, enrich_flag, Puwgt_flag As Integer


        Dim str11, str_time, str_vial, str_ener, str_temper, str_enrich, str_Puwgt, Sample_ID_str As String
        Dim dat1
        Dim live_time2, real_time2

        vial_diameter = 1
        sample_temperature = 22
        U_enrichment = 0.7
        Pu_weight = 0
        live_time = 1
        real_time = 1
        idex = 0
        '     MsgBox("reader " & datafilenames_for_sum)
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(datafilenames_for_sum)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters("#")       ' set delimiter to a value it wont encounter - turn off dlimiting
            Dim currentRow As String()
            i_trip = 0
            i_tot = 0
            tim_flag = 3
            vial_flag = 3
            temper_flag = 3
            enrich_flag = 3
            Puwgt_flag = 3
            ener_flag = 3


            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    For Each currentField In currentRow
                        str11 = currentField
                        If Strings.Left(str11, 6) = "Sample" Then Sample_ID_str = str11
                        dat1 = Val(str11)
                        i_tot = i_tot + 1

                        '      find live/real time line label in file
                        tim_flag = tim_flag + 1
                        If Strings.Left(str11, 5) = "$MEAS" Then tim_flag = 1
                        If tim_flag = 2 Then str_time = str11     ' put live and real time into temp string 
                        If tim_flag = 2 Then tim_flag = 3


                        '      find live/real time line label in file
                        ener_flag = ener_flag + 1
                        If Strings.Left(str11, 5) = "$ENER" Then ener_flag = 1
                        If ener_flag = 2 Then str_ener = str11     ' put live and real time into temp string 
                        If ener_flag = 2 Then ener_flag = 3

                        '      find vial diameter line label in file
                        vial_flag = vial_flag + 1
                        If Strings.Left(str11, 9) = "$KED PATH" Then vial_flag = 1
                        If vial_flag = 2 Then str_vial = str11     ' put vial diameter into temp string 


                        '      find vial temperature line label in file
                        temper_flag = temper_flag + 1
                        If Strings.Left(str11, 11) = "$KED SAMPLE" Then temper_flag = 1
                        If temper_flag = 2 Then str_temper = str11     ' put vial temperature into temp string 

                        '      find uranium enerichment line label in file
                        enrich_flag = enrich_flag + 1
                        If Strings.Left(str11, 18) = "$KED DECLARED U235" Then enrich_flag = 1
                        If enrich_flag = 2 Then str_enrich = str11     ' put vial 235U enichment into temp string 

                        '      find Pu atomic weight line label in file
                        Puwgt_flag = Puwgt_flag + 1
                        If Strings.Left(str11, 16) = "$KED DECLARED PU" Then Puwgt_flag = 1
                        If Puwgt_flag = 2 Then str_Puwgt = str11     ' put vial Pu Atomic weight into temp string 

                        '  find start of spectral data in file
                        If str11 = "$DATA:" Then chan_start = 1
                        If i_trip <> 0 Then chan_start = 1
                        If chan_start = 1 Then i_trip = i_trip + 1
                        If i_trip > 2 Then raw_data(i_trip - 2) = dat1
                        If i_trip > max_channels + 2 Then GoTo 100
                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While

100:        'MsgBox("array too long " & i_tot & " " & raw_data(max_channels))

            live_time = get_assay_time(str_time, 1)
            real_time = get_assay_time(str_time, 2)

            temp_e_0 = get_e_cal(str_ener, 1)
            temp_de = get_e_cal(str_ener, 2)

            vial_diameter = Val(str_vial)
            sample_temperature = Val(str_temper)
            U_enrichment = Val(str_enrich)
            Pu_weight = Val(str_Puwgt)

        End Using

        Dim i_len As Integer

        i_len = Strings.Len(Sample_ID_str)
        If i_len > 11 Then Sample_ID = Strings.Right(Sample_ID_str, i_len - 11)

        Me.SampleIDBox1.Text = Sample_ID
        Me.sampleinfoBox1.Text = vial_diameter
        Me.sampleinfoBox2.Text = sample_temperature
        Me.sampleinfoBox3.Text = U_enrichment
        Me.sampleinfoBox4.Text = Pu_weight
        Me.Cycle_number_Box.Text = s_n_cycle

    End Sub

    Sub save_spe_file(summed_file_name, summed_dat, num_sum)
        Dim templine As String
        Dim sum_data(4096)



        n_channels = max_channels
        templine = templine & "$SPEC_ID:" & vbCrLf
        templine = templine & summed_file_name & vbCrLf
        templine = templine & "$SPEC_REM:" & vbCrLf
        templine = templine & "Sample ID: " & Sample_ID & vbCrLf
        templine = templine & "$DATE_MEA: " & vbCrLf
        templine = templine & acq_date_temp & " " & acq_time_temp & vbCrLf
        templine = templine & "$MEAS_TIM: " & vbCrLf
        templine = templine & sum_live & " " & sum_real & vbCrLf
        templine = templine & "$ENER_FIT: " & vbCrLf
        templine = templine & Int(100000 * temp_e_0) / 100000 & " " & Int(100000 * temp_de) / 100000 & vbCrLf
        templine = templine & "$KED DECLARED U235 ENRICHMENT: " & vbCrLf
        templine = templine & U_enrichment & vbCrLf
        templine = templine & "$KED DECLARED PU ATOMIC WEIGHT: " & vbCrLf
        templine = templine & Pu_weight & vbCrLf
        templine = templine & "$KED PATH LENGTH: " & vbCrLf
        templine = templine & vial_diameter & vbCrLf
        templine = templine & "$KED SAMPLE TEMPERATURE: " & vbCrLf
        templine = templine & sample_temp_avg & vbCrLf
        templine = templine & "$DATA: " & vbCrLf
        templine = templine & "0 " & max_channels - 1 & vbCrLf


        Dim fs As FileStream = File.Create(summed_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(templine)
        fs.Write(info, 0, info.Length)

        fs.Close()
        For i = 1 To max_channels
            'create temp spec file for input to XRF_FIT
            templine = summed_dat(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(summed_file_name, templine, True)
        Next i

    End Sub


End Class