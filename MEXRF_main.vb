Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting


Public Class MEXRF_main
    Dim FORM4 As New Form()
    Dim MyForm9 As Summary_Results
    Dim MyForm10 As VMS_Setup
    Dim MyForm11 As Set_directories
    Dim MyForm13 As Fit_Status
    Dim MyForm14 As AttenuationCoefficients
    Dim MyForm15 As Xray_Data_entry
    Dim MyForm16 As Fission_Products
    Dim myform17 As summed_spectra
    Dim myform18 As Convert_CNF_Files
    Dim myform19 As Subtract_passive
    Dim myform20 As Gamma_ray_data_entry

    Public initval2(42)
    Public Par(42)
    Public i_file, n_cycle As Integer

    Public sample_temp1

    Public input_file_name As String
    Public file_proc_number(99) As Integer

    Public Declare Sub Sleep Lib "kernel32" Alias "Sleep" (ByVal dwMilliseconds As Long)

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles RunPlease.Click
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        Dim datafilename1 As String

        openFileDialog1.InitialDirectory = root_dir_base_name
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    datafilename1 = openFileDialog1.FileName

                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        End If

        '   Call Main(datafilename1, initval2)
    End Sub



    Public Concentrate1()
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        n_cycle = 1
        max_channels = 2048
        If nchan_checkbox.Checked Then max_channels = 4096

        'root_dir_base_name = Strings.Left(My.Application.Info.DirectoryPath, 1) & ":\"
        root_dir_base_name = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

        root_dir_name = Path.Combine(root_dir_base_name, "MEXRF\")
        exe_dir_name = Path.Combine(root_dir_base_name, root_dir_name & "XRF_T0")

        genie2k_dir = ""
        If My.Computer.FileSystem.DirectoryExists("C:\genie2k\exefiles") Then genie2k_dir = "C:"
        If My.Computer.FileSystem.DirectoryExists("D:\genie2k\exefiles") Then genie2k_dir = "D:"
        If My.Computer.FileSystem.DirectoryExists("E:\genie2k\exefiles") Then genie2k_dir = "E:"
        If My.Computer.FileSystem.DirectoryExists("F:\genie2k\exefiles") Then genie2k_dir = "F:"

        sys_consts_dir_name = Path.Combine(root_dir_base_name, root_dir_name & "xrf_files\System_constants\")
        xrf_vms_consts_dir = Path.Combine(root_dir_base_name, root_dir_name & "xrf_files\VMS_constants\")
        seed_parm_dir_name = Path.Combine(root_dir_base_name, root_dir_name & "\xrf_files\Seed_parameters\")
        attn_param_dir = Path.Combine(root_dir_base_name, root_dir_name & "xrf_files\attenuation_coefficients\")
        xray_data_file_dir = Path.Combine(root_dir_base_name, root_dir_name & "xrf_files\attenuation_coefficients\")
        resultfiledir = Path.Combine(root_dir_base_name, root_dir_name & "results\")
        convertfiledir = Path.Combine(root_dir_base_name, root_dir_name & "convert\")
        netspectradir = Path.Combine(root_dir_base_name, root_dir_name & "net_spectra\")
        gamma_ray_lib_path = Path.Combine(root_dir_base_name, root_dir_name & "xrf_files\fission_lib\")
        summaryfiledir = Path.Combine(root_dir_base_name, root_dir_name & "summary_files\")


        gamma_ray_lib_name = "default_gamma_lib.csv"
        Call get_gamma_data_file(gamma_ray_lib_path & gamma_ray_lib_name)

        attn_parm_def_name = attn_param_dir & "attn_coeffs_default.csv"
        xray_data_file_name = xray_data_file_dir & "XRF_branching_ratios_default.csv"

        const_file_name = sys_consts_dir_name & "xrf_system_constants.csv"
        genie_report_dir_name = genie2k_dir & "\genie2k\exefiles\report.exe"

        attn_par_file_name = attn_parm_def_name
        str_prg_root = "xrf_fit_v1p1"
        exe_name = str_prg_root & ".exe"

        meked_summary_name = ""
        passive_summary_file_name = ""

        MEKED_sumMary_flag = False           ' indicates if an MEKED summary file has been read in
        MEKED_results_flag = False          ' indicates if an MEKED result file has been read in

        For i = 1 To 5
            pass_sum_rates(i) = 0
            pass_sum_rates_err(i) = 0
        Next

        ' --------------------------------------------------------------------------------------------------------------


        Display_E_button.Checked = True         ' default horizontal scale = Energy
        Display_chan_button.Checked = False

        Call get_attn_par_file(attn_par_file_name, fil_label)
        '      Call get_xray_data_file(attn_par_file_name, fil_label_X)
        Call get_xray_data_file(xray_data_file_name, fil_label_X)
        Call get_system_constants(const_file_name)


        Me.MaskedTextBox1.Text = 0
        Me.MaskedTextBox2.Text = 0.075
        Me.MaskedTextBox3.Text = 1.414
        Me.MaskedTextBox4.Text = 22.0
        Me.MaskedTextBox5.Text = 0.7
        Me.MaskedTextBox6.Text = 239.63

        For i = 1 To 36
            KED_PAR(i) = 0
            KED_PAR_err(i) = 0
        Next i

        KED_PAR(1) = 0.2
        KED_PAR(2) = 0.002
        KED_PAR(6) = 1.15
        KED_PAR(8) = 149.5
        KED_PAR(10) = 0.9

        Dim meked_seed As String

        meked_seed = seed_parm_dir_name & "generic_ked_parameters.res" & " "

        Call get_ked_pars(meked_seed)

        MEKED_sumMary_flag = False
        MEKED_results_flag = False


        KED_Box_1.Text = Int(10000 * KED_PAR(8)) / 10000
        KED_Box_2.Text = Int(10000 * KED_PAR(10)) / 10000
        KED_Box_3.Text = Int(10000 * KED_PAR(1)) / 10000
        KED_Box_4.Text = Int(10000 * KED_PAR(2)) / 10000
        KED_Box_5.Text = Int(10000 * KED_PAR(3)) / 10000
        KED_Box_6.Text = Int(10000 * KED_PAR(5)) / 10000
        KED_Box_7.Text = Int(10000 * KED_PAR(4)) / 10000
        KED_Box_8.Text = Int(10000 * KED_PAR(6)) / 10000


    End Sub



    Public Property FileName As String
    Public datafilename1 As String

    Public Sub Button2_Click(sender As System.Object, e As System.EventArgs)



    End Sub

    Public Property datafilenamebox1() As String
        Get
            Return Me.filenamebox1.Text
        End Get
        Set(ByVal Value As String)
            Me.filenamebox1.Text = Value
        End Set
    End Property


    Public Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        '  MsgBox(" Name2 =" & datafilename1)

    End Sub

    Dim myForm4 As Fit_Constants_Entry
    Public Sub SysParToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SysParToolStripMenuItem.Click

        If myForm Is Nothing Then
            myForm4 = New Fit_Constants_Entry
        End If

        myForm4.Show()
        myForm4 = Nothing
    End Sub

    Dim myForm As Free_Param_entry
    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click
        Dim attn_par_file_name As String
        '
        'read in attenuation parameter file to retrieve element labels
        '
        attn_par_file_name = attn_parm_def_name
        Call get_attn_par_file(attn_par_file_name, fil_label)
        xray_data_file_name = xray_data_file_name
        Call get_xray_data_file(xray_data_file_name, fil_label_X)
        '
        '   Open seed parameter form (FORM14)
        '
        If myForm Is Nothing Then
            myForm = New Free_Param_entry
        End If
        myForm.Show()
        myForm = Nothing
    End Sub


    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles filenamebox1.MaskInputRejected

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Sub StartHard()
        Dim strPrgName As String
        Dim strArg, strArg2 As String
        Dim dir_blank, start_dir As String

        ' spawns the xrf_fit executables

        dir_blank = exe_dir_name

        max_channels = 2048
        If nchan_checkbox.Checked Then max_channels = 4096

        For idex = 1 To num_files
            start_dir = dir_blank
            FileSystem.ChDir(start_dir)
            strPrgName_exe = exe_dir_name & "\" & str_prg_root & ".exe"

            strArg = "2048 "
            If max_channels = 4096 Then strArg = "4096 "

            strArg2 = "xfit_parms_" & idex - 1

            file_proc_number(idex) = Shell("""" & strPrgName_exe & """ """ & strArg & """ """ & strArg2 & """", vbNormalFocus)

        Next idex

        Dim textout As String
        textout = ""
        For i = 1 To num_files

            textout = textout & " " & i & " ; " & file_proc_number(i) & vbCrLf

        Next i

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Call StartHard()
        If MyForm13 Is Nothing Then
            MyForm13 = New Fit_Status
        End If

        MyForm13.Show()
        MyForm13 = Nothing
    End Sub

    Public Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        '      With Chart1.Series(0)
        '      .Points.DataBind(dtTest.DefaultView, "Energy", "Counts", Nothing)
        '      .ChartType = DataVisualization.Charting.SeriesChartType.Line
        '      .BorderWidth = 4
        '     End With

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub


    Dim dtTest As New DataTable

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Public Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunPlease.Click
        Dim ener, ymin, ymax
        ' e_0 = 0.0001
        ' del_e = 0.09
        ymin = 0
        ymax = 0
        For i = 1 To 2040 * Int(max_channels / 2048)
            ener = e_0 + d_e * i
            If ener > 20 And raw_data(i) > ymax Then ymax = raw_data(i)
        Next
        ymin = ymax
        For i = 1 To max_channels - 1
            ener = e_0 + d_e * i
            If ener > 20 And raw_data(i) < ymin Then ymin = raw_data(i)
        Next

        If ymin < 1 Then ymin = 1
        If ymax < 10 Then ymax = 10
        ymax = 10 ^ Int(Log10(ymax) + 1)
        ymin = 10 ^ Int(Log10(ymin))
        If ymin < 1 Then ymin = 1

        With Chart1.ChartAreas(0)

            .AxisX.Minimum = 0
            .AxisX.Interval = 20
            .AxisX.Maximum = 175

            .AxisY.IsLogarithmic = True
            .AxisY.Minimum = ymin
            .AxisY.Maximum = ymax

            .AxisX.Title = "Energy (keV)"
            .AxisY.Title = "Counts"
        End With

        Chart1.Series.Clear()

        Chart1.Series.Add("Raw_Counts")

        With Chart1.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 2000 * Int(max_channels / 2048)
                ener = e_0 + d_e * i
                If raw_data(i) > 0 Then Me.Chart1.Series("Raw_Counts").Points.AddXY(ener, raw_data(i)) Else Me.Chart1.Series("Raw_Counts").Points.AddXY(ener, 1)
            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart1.Annotations.Add(PC)
            End With

        End With
    End Sub

    Private Sub sampleinfoBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles sampleinfoBox1.MaskInputRejected

    End Sub

    Private Sub sampleinfoBox2_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles sampleinfoBox2.MaskInputRejected

    End Sub

    Private Sub sampleinfoBox3_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles sampleinfoBox3.MaskInputRejected

    End Sub

    Private Sub sampleinfoBox4_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles sampleinfoBox4.MaskInputRejected

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RefTempBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub CalTempBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

    End Sub





    Private Sub SampleIDBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles SampleIDBox1.MaskInputRejected

    End Sub

    Private Sub Button5_Click_1(sender As Object, e As System.EventArgs) Handles select_files_Button.Click
        '
        'Select and Read Data Files
        '
        Dim LastDir As String = ""
        Dim spe_files(99) As String
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
                spe_files(idex) = mFile
                ListBox1.Items.Add(mFile)
            Next
        End If

        num_files = idex
        If num_files = 0 Then Return



        '   *************************************************************************
        Dim i_file, i1, i2 As Integer

        For i_file = 1 To num_files
            datafilename1 = spe_files(i_file)

            '  strip off path
            i1 = Strings.InStrRev(datafilename1, "\")
            i2 = Strings.Len(datafilename1)
            input_file_name = Strings.Right(datafilename1, i2 - i1)

            ' ***** Create the input file for XRF_FIT *****
            datafilenamebox1 = datafilename1
            input_file_names(i_file) = datafilename1


            '   If Strings.Right(datafilename1, 3) = "cnf" Or Strings.Right(datafilename1, 3) = "CNF" Then Call read_cnf_data_file(datafilename1) Else Call read_spe_data_file()

            If Strings.Right(datafilename1, 3) = "cnf" Or Strings.Right(datafilename1, 3) = "CNF" Then Call read_cnf_data_file(datafilename1)
            If Strings.Right(datafilename1, 3) = "spe" Or Strings.Right(datafilename1, 3) = "SPE" Then Call read_spe_data_file()
            If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then Call split_file(datafilename1)

            If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then e_0 = results_arr(15)
            If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then d_e = results_arr(16)

            ' read in the raw data from the selected SPE file

            Call Button4_Click(sender, e)

            '    Write the input files for XRF_FIT vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

            Dim templine, short_file_name As String
            resultfiledir = resultfiledir
            short_file_name = resultfiledir & Strings.Left(input_file_name, Strings.Len(input_file_name) - 4)

            If CheckBox1.Checked Then e_0 = MaskedTextBox1.Text
            If CheckBox2.Checked Then d_e = MaskedTextBox2.Text
            If CheckBox3.Checked Then vial_diameter = MaskedTextBox3.Text
            If CheckBox4.Checked Then sample_temperature = MaskedTextBox4.Text
            If CheckBox5.Checked Then U_enrichment = MaskedTextBox5.Text
            If CheckBox6.Checked Then Pu_weight = MaskedTextBox6.Text

            If KED_check_box_1.Checked Then KED_PAR(8) = KED_Box_1.Text
            If KED_check_box_2.Checked Then KED_PAR(10) = KED_Box_2.Text
            If KED_check_box_3.Checked Then KED_PAR(1) = KED_Box_3.Text
            If KED_check_box_4.Checked Then KED_PAR(2) = KED_Box_4.Text
            If KED_check_box_5.Checked Then KED_PAR(3) = KED_Box_5.Text
            If KED_check_box_6.Checked Then KED_PAR(5) = KED_Box_6.Text
            If KED_check_box_7.Checked Then KED_PAR(4) = KED_Box_7.Text
            If KED_check_box_8.Checked Then KED_PAR(6) = KED_Box_8.Text

            Dim str_date_2 As String
            str_date_2 = Strings.Replace(str_date, "/", "-")        ' Fortran code doesnt like to read in the "/" so replace it with "-"
            If (str_date = "") Then str_date_2 = "01-01-65 12:00"

            n_channels = max_channels - 1
            templine = input_file_name & " " & short_file_name & " " & Sample_ID & " " & str_date_2 & vbCrLf
            templine = templine & n_channels & " " & live_time & " " & real_time & " " & vial_diameter & " " & sample_temperature & " " & U_enrichment & " " & Pu_weight & " 0 0 0 " & vbCrLf

            Dim fs As FileStream = File.Create(outfilename(i_file))
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(templine)
            fs.Write(info, 0, info.Length)

            fs.Close()
            For i = 1 To max_channels
                'create temp spec file for input to XRF_FIT
                templine = " " & i - 1 & " " & raw_data(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outfilename(i_file), templine, True)
            Next i

        Next i_file

        '    >>>>>>>>>>>>>>>  read and distribute passive spectrum for subtraction <<<<<<<<<<<<<<<<<<<<

        Call get_system_constants(const_file_name)
        Call get_VMS_constants()
        If Const_val(35) <> 1 Then GoTo 100

        datafilename1 = vms_ref_file_name

        MsgBox("Passive file read in for subtration: " & vms_ref_file_name)

        '  strip off path
        i1 = Strings.InStrRev(datafilename1, "\")
        i2 = Strings.Len(datafilename1)
        input_file_name = Strings.Right(datafilename1, i2 - i1)

        ' ***** Create the pssive file for XRF_FIT *****
        '   If Strings.Right(datafilename1, 3) = "cnf" Or Strings.Right(datafilename1, 3) = "CNF" Then Call read_cnf_data_file(datafilename1) Else Call read_spe_data_file()

        If Strings.Right(datafilename1, 3) = "cnf" Or Strings.Right(datafilename1, 3) = "CNF" Then Call read_cnf_passive_file(datafilename1)
        If Strings.Right(datafilename1, 3) = "spe" Or Strings.Right(datafilename1, 3) = "SPE" Then Call read_spe_passive_file()


        For i_file = 1 To num_files

            '  >>>>>>>>>>>>>>>>>>  Write the passive spectrum file for XRF_FIT <<<<<<<<<<<<<<<<<<<<<<

            outfilename(i_file) = exe_dir_name & "\spec_passive_" & i_file - 1 & ".txt"

            Dim templine, short_file_name As String
            resultfiledir = resultfiledir
            short_file_name = resultfiledir & Strings.Left(input_file_name, Strings.Len(input_file_name) - 4)

            n_channels = max_channels - 1

            templine = input_file_name & " " & short_file_name & " " & Sample_ID & " " & acq_date_time & vbCrLf
            templine = templine & n_channels & " " & live_time & " " & real_time & " " & vial_diameter & " " & sample_temperature & " " & U_enrichment & " " & Pu_weight & " 0 0 0 " & vbCrLf

            Dim fs As FileStream = File.Create(outfilename(i_file))
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(templine)
            fs.Write(info, 0, info.Length)

            fs.Close()
            For i = 1 To max_channels
                'create temp spec file for input to XRF_FIT
                templine = " " & i & " " & passive_data(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outfilename(i_file), templine, True)
            Next i

        Next i_file


100:   '   end it
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub SummaryREsultsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryREsultsToolStripMenuItem.Click
        If myForm Is Nothing Then
            MyForm9 = New Summary_Results
        End If
        MyForm9.Show()
        MyForm9 = Nothing
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim temp_file As String
        Dim real_time1, live_time1, vial_diameter1, sample_temperature1, U_enrichment1, Pu_weight1
        Dim n_files As Integer
        Dim chan_scale As Boolean

        If ListBox1.SelectedItem = "" Then Return
        n_files = Val(ListBox1.Items.Count.ToString())
        n_cycle = ListBox1.SelectedIndex + 1


        Call get_xray_data_file(xray_data_file_name, fil_label_X)

        temp_file = ListBox1.SelectedItem.ToString()

        datafilename1 = temp_file


        '   Call read_spe_data_file()
        If Strings.Right(datafilename1, 3) = "cnf" Or Strings.Right(datafilename1, 3) = "CNF" Then Call read_cnf_data_file(datafilename1)
        If Strings.Right(datafilename1, 3) = "spe" Or Strings.Right(datafilename1, 3) = "SPE" Then Call read_spe_data_file()
        If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then Call split_file(datafilename1)



        If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then e_0 = results_arr(15)
        If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then d_e = results_arr(16)

        If CheckBox1.Checked Then e_0 = MaskedTextBox1.Text
        If CheckBox2.Checked Then d_e = MaskedTextBox2.Text
        If CheckBox3.Checked Then vial_diameter = MaskedTextBox3.Text
        If CheckBox4.Checked Then sample_temperature = MaskedTextBox4.Text
        If CheckBox5.Checked Then U_enrichment = MaskedTextBox5.Text
        If CheckBox6.Checked Then Pu_weight = MaskedTextBox6.Text


        Me.SampleIDBox1.Text = Sample_ID
        Me.sampleinfoBox1.Text = vial_diameter
        Me.sampleinfoBox2.Text = sample_temperature
        Me.sampleinfoBox3.Text = U_enrichment
        Me.sampleinfoBox4.Text = Pu_weight
        Me.sampleinfoBox5.Text = live_time
        Me.filenamebox1.Text = datafilename1
        Dim ener, ymin, ymax
        ' e_0 = 0.0001
        ' del_e = 0.09
        ymin = 0
        ymax = 0

        If Display_chan_button.Checked = True Then chan_scale = True

        For i = 1 To 2000 * Int(max_channels / 2048)
            ener = e_0 + d_e * i
            If chan_scale = True Then ener = i
            If ener > 20 And raw_data(i) > ymax Then ymax = raw_data(i)
        Next
        ymin = ymax
        For i = 1 To max_channels - 1
            ener = e_0 + d_e * i
            If chan_scale = True Then ener = i
            If ener > 20 And raw_data(i) < ymin Then ymin = raw_data(i)
        Next

        If ymin < 1 Then ymin = 1
        If ymax < 10 Then ymax = 10
        ymax = 10 ^ Int(Log10(ymax) + 1)
        ymin = 10 ^ Int(Log10(ymin))
        If ymin < 1 Then ymin = 1


        With Chart1.ChartAreas(0)

            .AxisX.Minimum = 0
            .AxisX.Interval = 20
            .AxisX.Maximum = 175

            If chan_scale = True Then .AxisX.Interval = Int(max_channels / 10)
            If chan_scale = True Then .AxisX.Maximum = max_channels

            .AxisY.IsLogarithmic = True
            .AxisY.Minimum = ymin
            .AxisY.Maximum = ymax

            .AxisX.Title = "Energy (keV)"
            If chan_scale = True Then .AxisX.Title = "Channels"
            .AxisY.Title = "Counts"
        End With

        Chart1.Series.Clear()


        Chart1.Series.Add("Raw_Counts")

        With Chart1.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To 2000 * Int(max_channels / 2048)
                ener = e_0 + d_e * i
                If chan_scale = True Then ener = i
                If raw_data(i) > 0 Then Me.Chart1.Series("Raw_Counts").Points.AddXY(ener, raw_data(i)) Else Me.Chart1.Series("Raw_Counts").Points.AddXY(ener, 1)
            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart1.Annotations.Add(PC)
            End With

        End With

        Call calc_vms_conc_a()
    End Sub

    Public Sub read_spe_data_file()
        ' reads file in text format from datafilename1
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
        '     MsgBox("reader " & datafilename1)
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(datafilename1)
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

            e_0 = get_e_cal(str_ener, 1)
            d_e = get_e_cal(str_ener, 2)

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
        Me.Cycle_number_Box.Text = n_cycle

    End Sub

    Dim MyForm6 As Individual_results



    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        If myForm Is Nothing Then
            MyForm6 = New Individual_results
        End If
        MyForm6.Show()
        MyForm6 = Nothing
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click

        Dim l_chan, h_chan, e_low, e_high

        Call get_VMS_constants()

        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        h_chan = e_cal_par(3)
        e_high = e_cal_par(4)

        prelim_energy_cal(l_chan, h_chan, e_low, e_high, raw_data)

        Me.MaskedTextBox1.Text = Int(1000000 * new_e0) / 1000000
        If new_de <> 0 Then Me.MaskedTextBox2.Text = Int(1000000 * new_de) / 1000000

    End Sub

    Private Sub Button5_Click_2(sender As Object, e As EventArgs) Handles Button5.Click
        Call calc_vms_conc_a()
    End Sub

    Sub calc_vms_conc()
        Dim fit_rois(6), alpha, rel_spec(4100)
        Dim bkg_corr_ref(4100), bkg_corr_spec(4100), ref_bkg(4100), spec_bkg(4100), b_spec_temp, b_ref_temp, b_spec_sum, b_ref_sum
        Dim B_L_spec, B_L_ref, B_U_spec, B_U_ref
        Dim n_bkg_L_L, n_bkg_L_R, n_bkg_U_L, n_bkg_U_R
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, delta_mu_u

        Dim l_chan, h_chan, e_low, e_high
        Dim u_edge
        alpha = 1.1

        Call get_VMS_constants()

        'prelim energy calibration

        u_edge = fit_rois(1)
        U_e_l1 = fit_rois(2)
        U_e_l2 = fit_rois(3)
        U_e_r1 = fit_rois(4)
        U_e_r2 = fit_rois(5)

        '    delta_mu_u = fit_rois(6)

        h_chan = e_cal_par(3)
        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        e_high = e_cal_par(4)

        Call prelim_energy_cal(l_chan, h_chan, e_low, e_high, raw_data)


        Dim ROI_BKG(5, 3), Peak_ROI(5, 3) As Integer
        Dim bkg_area(5), peak_area(5), net_peak_area(5)
        Dim FWHM_VMS, u_conc_xrf_est
        Dim test_peak As String

        FWHM_VMS = 2.35 * results_arr(17)
        ' calculated background region of interst channel bounds
        For i = 1 To 5
            ROI_BKG(i, 1) = Int((X_fit_rois(i, 1) - new_e0) / new_de)    ' start channel
            ROI_BKG(i, 2) = Int((X_fit_rois(i, 2) - new_e0) / new_de)    ' end channel 
            ROI_BKG(i, 3) = ROI_BKG(i, 2) - ROI_BKG(i, 1) + 1            ' num channels
        Next i

        For i = 1 To 3
            Peak_ROI(i, 1) = Int(((X_fit_rois(5 + i, 1) - FWHM_VMS * X_fit_rois(5 + i, 2) * (X_fit_rois(5 + i, 1) / 88.0) ^ 0.5) - new_e0) / new_de)    ' start channel
            Peak_ROI(i, 2) = Int(((X_fit_rois(5 + i, 1) + FWHM_VMS * X_fit_rois(5 + i, 2) * (X_fit_rois(5 + i, 1) / 88.0) ^ 0.5) - new_e0) / new_de)    ' end channel 
            Peak_ROI(i, 3) = Peak_ROI(i, 2) - Peak_ROI(i, 1)                                                                                            ' num channels
        Next i

        Peak_ROI(4, 1) = Int((X_fit_rois(9, 1) - new_e0) / new_de)    ' start channel
        Peak_ROI(4, 2) = Int((X_fit_rois(9, 2) - new_e0) / new_de)    ' end channel 
        Peak_ROI(4, 3) = ROI_BKG(4, 2) - ROI_BKG(4, 1) + 1            ' num channels

        Peak_ROI(5, 1) = Int(((X_fit_rois(10, 1) - FWHM_VMS * X_fit_rois(10, 2) * (X_fit_rois(10, 1) / 88.0) ^ 0.5) - new_e0) / new_de)     ' start channel
        Peak_ROI(5, 2) = Int(((X_fit_rois(10, 3) + FWHM_VMS * X_fit_rois(10, 4) * (X_fit_rois(10, 1) / 88.0) ^ 0.5) - new_e0) / new_de)     ' end channel 
        Peak_ROI(5, 3) = Peak_ROI(5, 2) - Peak_ROI(5, 1)                                                                                             ' num channels

        test_peak = ""
        For i = 1 To 5
            bkg_area(i) = 0
            For j = ROI_BKG(i, 1) To ROI_BKG(i, 2)
                bkg_area(i) = bkg_area(i) + raw_data(j)
            Next j
        Next i

        For i = 1 To 5
            peak_area(i) = 0

            For j = Peak_ROI(i, 1) To Peak_ROI(i, 2)
                peak_area(i) = peak_area(i) + raw_data(j)
            Next j
            test_peak = test_peak & i & " " & bkg_area(i) & " : " & peak_area(i) & vbCrLf

        Next i

        ' MsgBox(test_peak)


        ' subtract backrounds

        Dim bkg_temp, sum_YJ, sum_YK, sum_Y1, sum_y2, Peak_BKG(5), Pu_bkg_fact

        '
        Pu_bkg_fact = 1.3959
        vms_R_UPu_cal = 1.4842
        '
        bkg_temp = 0

        sum_Y1 = 0
        sum_y2 = 0
        sum_YJ = 0
        sum_YK = 0
        For i = 1 To 5
            Peak_BKG(i) = 0
        Next i

        For i = ROI_BKG(1, 1) To ROI_BKG(1, 2)
            sum_Y1 = sum_Y1 + raw_data(i)                           ' sum counts in bkg roi 1
        Next i

        sum_Y1 = sum_Y1 / (ROI_BKG(1, 2) - ROI_BKG(1, 1) + 1)       ' counts per channel bkg roi 1

        For i = ROI_BKG(2, 1) To ROI_BKG(2, 2)
            sum_y2 = sum_y2 + raw_data(i)                           ' sum counts in bkg roi 2
        Next i
        sum_y2 = sum_y2 / (ROI_BKG(2, 2) - ROI_BKG(2, 1) + 1)       ' counts per channel bkg roi 2


        For i = ROI_BKG(1, 2) To ROI_BKG(2, 1)
            sum_YK = sum_YK + raw_data(i)                           ' sum counts between bkg ROIs
        Next i

        For j = Peak_ROI(1, 1) To Peak_ROI(1, 2)
            Peak_BKG(1) = Peak_BKG(1) + back_i(j, ROI_BKG(1, 2), sum_Y1, sum_y2, sum_YK, raw_data)                          ' sum counts in bkg roi 1
        Next j

        For j = Peak_ROI(2, 1) To Peak_ROI(2, 2)
            Peak_BKG(2) = Peak_BKG(2) + back_i(j, ROI_BKG(1, 2), sum_Y1, sum_y2, sum_YK, raw_data)                          ' sum counts in bkg roi 1
        Next j


        net_peak_area(1) = peak_area(1) - Peak_BKG(1)               ' U K-alpha 2 peak
        net_peak_area(2) = peak_area(2) - Peak_BKG(2)               ' U K-alpha 1 peak

        '  -------------------------------
        '  VMS   Calculated Pu K alpha 1 net peak counts
        '  -----------------------------------
        bkg_temp = 0
        bkg_temp = (bkg_area(2) + bkg_area(3)) * Peak_ROI(3, 3) / (ROI_BKG(2, 3) + ROI_BKG(3, 3))

        Peak_BKG(3) = bkg_temp / Pu_bkg_fact
        net_peak_area(3) = peak_area(3) - Peak_BKG(3)


        vms_R_UPu = (HPGE_eff(103.734) / HPGE_eff(98.436)) * (net_peak_area(2) / net_peak_area(3)) / (vms_R_UPu_cal * Exp(-0.110224 * results_arr(9)))

        U_Ka1_ROI1_box.Text = peak_area(1)
        U_Ka1_ROI2_box.Text = Peak_BKG(1)
        U_Ka2_ROI1_box.Text = peak_area(2)
        U_Ka2_ROI2_box.Text = Peak_BKG(2)
        Pu_Ka1_ROI1_box.Text = peak_area(3)
        Pu_Ka1_ROI2_box.Text = Peak_BKG(3)

        U_KA2_box.Text = Int(net_peak_area(1) * 100) / 100
        U_KA1_box.Text = Int(net_peak_area(2) * 100) / 100
        PU_KA1_box.Text = Int(net_peak_area(3) * 100) / 100

        U_Ka1_seed_counts_box.Text = Int(net_peak_area(2) / (xray_Lib(1, 1, 2) / 100))
        U_Ka2_seed_counts_box.Text = Int(net_peak_area(1) / (xray_Lib(1, 2, 2) / 100))
        Pu_seed_counts_box.Text = Int(net_peak_area(3) / (xray_Lib(2, 1, 2) / 100))

        U_est_par(1) = 1.0064
        U_est_par(2) = 0.007468
        U_est_par(3) = 0.000003414

        Dim peak_area1
        peak_area1 = net_peak_area(2) / xray_Lib(1, 1, 2) / live_time * 100
        ' u_conc_xrf_est = U_est_par(1) + U_est_par(2) * peak_area1 + U_est_par(3) * peak_area1 ^ 2
        u_conc_xrf_est = Inv_Lambert(peak_area1 * U_est_par(2) / U_est_par(1)) / U_est_par(2)


        ' 1
        U_conc_xrf_box.Text = " 1: " & Int(1000 * u_conc_xrf_est) / 1000

        vms_U_Pu_count_ratio_box.Text = Int(1000 * net_peak_area(2) / net_peak_area(3)) / 1000
        VMS_U_Pu_ratio_box.Text = Int(vms_R_UPu * 1000) / 1000

        Return


        '   remove background

        n_bkg_L_L = Int((bkg_roi(1) - new_e0) / new_de)
        n_bkg_L_R = Int((bkg_roi(2) - new_e0) / new_de)
        n_bkg_U_L = Int((bkg_roi(3) - new_e0) / new_de)
        n_bkg_U_R = Int((bkg_roi(4) - new_e0) / new_de)
        '

        B_L_spec = 0
        B_L_ref = 0
        B_U_spec = 0
        B_U_ref = 0

        For i = n_bkg_L_L To n_bkg_L_R
            B_L_spec = B_L_spec + raw_data(i)
            B_L_ref = B_L_ref + vms_ref_raw_data(i)
        Next i

        For i = n_bkg_U_L To n_bkg_U_R
            B_U_spec = B_L_spec + raw_data(i)
            B_U_ref = B_L_ref + vms_ref_raw_data(i)
        Next i

        B_L_spec = B_L_spec / (n_bkg_L_R - n_bkg_L_L + 1)
        B_L_ref = B_L_ref / (n_bkg_L_R - n_bkg_L_L + 1)
        B_U_spec = B_U_spec / (n_bkg_U_R - n_bkg_U_L + 1)
        B_U_ref = B_U_ref / (n_bkg_U_R - n_bkg_U_L + 1)

        b_spec_sum = 0
        b_ref_sum = 0
        b_spec_temp = 0
        b_ref_temp = 0


        For i = n_bkg_L_R To n_bkg_U_L
            b_spec_sum = b_spec_sum + raw_data(i)
            b_ref_sum = b_ref_sum + vms_ref_raw_data(i)
        Next i

        For i = n_bkg_L_R To n_bkg_U_L

            b_spec_temp = b_spec_temp + raw_data(i)
            b_ref_temp = b_ref_temp + vms_ref_raw_data(i)

            spec_bkg(i) = B_L_spec + (B_U_spec - B_L_spec) * b_spec_temp / b_spec_sum
            ref_bkg(i) = B_L_ref + (B_U_ref - B_L_ref) * b_ref_temp / b_ref_sum
        Next i

        For i = 1000 * Int(max_channels / 2048) To 2000 * Int(max_channels / 2048)
            rel_spec(i) = (raw_data(i) - spec_bkg(i)) / (vms_ref_raw_data(i) - ref_bkg(i)) * vms_ref_live_time / live_time
        Next i


        For i = 1 To 6
            fit_rois(i) = K_fit_rois(1, i)
        Next
        prelim_U_conc = prelim_conc(fit_rois, rel_spec)
        '       2
        U_conc_xrf_box.Text = " 2:" & Int((prelim_U_conc + K_fit_rois(1, 7)) * 1000000) / 1000

        For i = 1 To 6
            fit_rois(i) = K_fit_rois(3, i)
        Next
        prelim_Pu_conc = prelim_conc(fit_rois, rel_spec)

        Pu_conc_xrf_box.Text = Int((prelim_Pu_conc + K_fit_rois(3, 7)) * 1000000) / 1000


    End Sub



    Sub calc_vms_conc_a()
        Dim fit_rois(6), alpha, rel_spec(4100)
        Dim bkg_corr_ref(4100), bkg_corr_spec(4100), ref_bkg(4100), spec_bkg(4100)
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, delta_mu_u

        Dim l_chan, h_chan, e_low, e_high
        Dim u_edge
        alpha = 1.1

        Call get_VMS_constants()

        'prelim energy calibration

        u_edge = fit_rois(1)
        U_e_l1 = fit_rois(2)
        U_e_l2 = fit_rois(3)
        U_e_r1 = fit_rois(4)
        U_e_r2 = fit_rois(5)

        delta_mu_u = fit_rois(6)

        h_chan = e_cal_par(3)
        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        e_high = e_cal_par(4)


        Call prelim_energy_cal(l_chan, h_chan, e_low, e_high, raw_data)

        '   remove background
        Dim ROI_BKG(5, 3), Peak_ROI(5, 3) As Integer
        Dim bkg_area(5), peak_area(5)
        Dim FWHM_VMS, uk1_fwhm, uk2_fwhm
        Dim test_peak As String


        Dim uk1_ROIS(6), uk2_ROIS(6), uk1_int, uk2_int, uk1_bkg_l, uk1_bkg_h, uk2_bkg_l, uk2_bkg_h, uk1_s, uk2_s
        Dim uk1_net_peak_area, uk2_net_peak_area, uk1_background, uk2_background

        FWHM_VMS = 2.35 * results_arr(17)

        uk1_fwhm = (X_fit_rois(7, 1) / 88) ^ 0.5 * FWHM_VMS


        '  --------------------------------------------------------------------------------------------------------------------

        ' calculated background region of interst channel bounds
        For i = 1 To 5
            ROI_BKG(i, 1) = Int((X_fit_rois(i, 1) - new_e0) / new_de)    ' start channel
            ROI_BKG(i, 2) = Int((X_fit_rois(i, 2) - new_e0) / new_de)    ' end channel 
            ROI_BKG(i, 3) = ROI_BKG(i, 2) - ROI_BKG(i, 1) + 1            ' num channels
        Next i

        For i = 1 To 3
            Peak_ROI(i, 1) = Int(((X_fit_rois(5 + i, 1) - FWHM_VMS * X_fit_rois(5 + i, 2) * (X_fit_rois(5 + i, 1) / 88.0) ^ 0.5) - new_e0) / new_de)    ' start channel
            Peak_ROI(i, 2) = Int(((X_fit_rois(5 + i, 1) + FWHM_VMS * X_fit_rois(5 + i, 2) * (X_fit_rois(5 + i, 1) / 88.0) ^ 0.5) - new_e0) / new_de)    ' end channel 
            Peak_ROI(i, 3) = Peak_ROI(i, 2) - Peak_ROI(i, 1)                                                                                            ' num channels
        Next i

        Peak_ROI(4, 1) = Int((X_fit_rois(9, 1) - new_e0) / new_de)    ' start channel
        Peak_ROI(4, 2) = Int((X_fit_rois(9, 2) - new_e0) / new_de)    ' end channel 
        Peak_ROI(4, 3) = ROI_BKG(4, 2) - ROI_BKG(4, 1) + 1            ' num channels

        Peak_ROI(5, 1) = Int(((X_fit_rois(10, 1) - FWHM_VMS * X_fit_rois(10, 2) * (X_fit_rois(10, 1) / 88.0) ^ 0.5) - new_e0) / new_de)     ' start channel
        Peak_ROI(5, 2) = Int(((X_fit_rois(10, 3) + FWHM_VMS * X_fit_rois(10, 4) * (X_fit_rois(10, 1) / 88.0) ^ 0.5) - new_e0) / new_de)     ' end channel 
        Peak_ROI(5, 3) = Peak_ROI(5, 2) - Peak_ROI(5, 1)                                                                                             ' num channels

        test_peak = ""
        For i = 1 To 5
            bkg_area(i) = 0
            For j = ROI_BKG(i, 1) To ROI_BKG(i, 2)
                bkg_area(i) = bkg_area(i) + raw_data(j)
            Next j
        Next i

        For i = 1 To 5
            peak_area(i) = 0

            For j = Peak_ROI(i, 1) To Peak_ROI(i, 2)
                peak_area(i) = peak_area(i) + raw_data(j)
            Next j
            test_peak = test_peak & i & " " & bkg_area(i) & " : " & peak_area(i) & vbCrLf

        Next i

        ' MsgBox(test_peak)


        ' subtract backrounds

        Dim bkg_temp, sum_YJ, sum_YK, sum_Y1, sum_y2, Peak_BKG(5), Pu_bkg_fact

        '        '
        bkg_temp = 0

        sum_Y1 = 0
        sum_y2 = 0
        sum_YJ = 0
        sum_YK = 0
        For i = 1 To 5
            Peak_BKG(i) = 0
        Next i

        For i = ROI_BKG(1, 1) To ROI_BKG(1, 2)
            sum_Y1 = sum_Y1 + raw_data(i)                           ' sum counts in bkg roi 1
        Next i

        sum_Y1 = sum_Y1 / (ROI_BKG(1, 2) - ROI_BKG(1, 1) + 1)       ' counts per channel bkg roi 1

        For i = ROI_BKG(2, 1) To ROI_BKG(2, 2)
            sum_y2 = sum_y2 + raw_data(i)                           ' sum counts in bkg roi 2
        Next i
        sum_y2 = sum_y2 / (ROI_BKG(2, 2) - ROI_BKG(2, 1) + 1)       ' counts per channel bkg roi 2


        For i = ROI_BKG(1, 2) To ROI_BKG(2, 1)
            sum_YK = sum_YK + raw_data(i)                           ' sum counts between bkg ROIs
        Next i

        For j = Peak_ROI(1, 1) To Peak_ROI(1, 2)
            Peak_BKG(1) = Peak_BKG(1) + back_i(j, ROI_BKG(1, 2), sum_Y1, sum_y2, sum_YK, raw_data)                          ' sum counts in bkg roi 1
        Next j

        For j = Peak_ROI(2, 1) To Peak_ROI(2, 2)
            Peak_BKG(2) = Peak_BKG(2) + back_i(j, ROI_BKG(1, 2), sum_Y1, sum_y2, sum_YK, raw_data)                          ' sum counts in bkg roi 1
        Next j

        Dim net_peak_area(5)
        net_peak_area(1) = peak_area(1) - Peak_BKG(1)               ' U K-alpha 2 peak
        net_peak_area(2) = peak_area(2) - Peak_BKG(2)               ' U K-alpha 1 peak

        '  -------------------------------
        '  VMS   Calculated Pu K alpha 1 net peak counts
        '  -----------------------------------
        bkg_temp = 0
        bkg_temp = (bkg_area(2) + bkg_area(3)) * Peak_ROI(3, 3) / (ROI_BKG(2, 3) + ROI_BKG(3, 3))

        Peak_BKG(3) = bkg_temp / vms_Pu_bkg_wt
        net_peak_area(3) = peak_area(3) - Peak_BKG(3)

        Dim rel_a
        If U_enrichment = 0 Or Pu_weight = 0 Then rel_a = 1 Else rel_a = Pu_weight / (238 * (1 - U_enrichment) + 235 * U_enrichment)


        vms_R_UPu = rel_a * (HPGE_eff(103.734) / HPGE_eff(98.436)) * (net_peak_area(2) / net_peak_area(3)) / (vms_R_UPu_cal * Exp(-0.110224 * results_arr(9)))

        '  ---------------------------------------------------------------------------------------------------------------------

        uk1_ROIS(1) = Int((X_fit_rois(7, 1) - uk1_fwhm - new_e0) / new_de)                       ' start Peak ROI
        uk1_ROIS(2) = Int(((X_fit_rois(7, 1) + uk1_fwhm - new_e0) / new_de) + 0.5)               ' num channels
        uk1_ROIS(3) = Int(((X_fit_rois(7, 3) - new_e0) / new_de) - X_fit_rois(7, 4) / 2)                              ' start Lower BKG ROI
        uk1_ROIS(4) = X_fit_rois(7, 4)                                                                               ' num channels
        uk1_ROIS(5) = Int(((X_fit_rois(7, 5) - new_e0) / new_de) - X_fit_rois(7, 6) / 2)                              ' start Lower BKG ROI
        uk1_ROIS(6) = X_fit_rois(7, 6)                                                                               ' num channels

        uk2_fwhm = (X_fit_rois(6, 1) / 88) ^ 0.5 * FWHM_VMS

        uk2_ROIS(1) = Int((X_fit_rois(6, 1) - uk1_fwhm - new_e0) / new_de)                       ' start Peak ROI
        uk2_ROIS(2) = Int(((X_fit_rois(6, 1) + uk1_fwhm - new_e0) / new_de) + 0.5)               ' num channels
        uk2_ROIS(3) = Int(((X_fit_rois(6, 3) - new_e0) / new_de) - X_fit_rois(6, 4) / 2)                              ' start Lower BKG ROI
        uk2_ROIS(4) = X_fit_rois(6, 4)                                                                               ' num channels
        uk2_ROIS(5) = Int(((X_fit_rois(6, 5) - new_e0) / new_de) - X_fit_rois(6, 6) / 2)                              ' start Lower BKG ROI
        uk2_ROIS(6) = X_fit_rois(6, 6)                                                                               ' num channels

        uk1_int = 0
        uk2_int = 0
        uk1_bkg_l = 0
        uk1_bkg_h = 0
        uk2_bkg_l = 0
        uk2_bkg_h = 0

        For i = uk1_ROIS(1) To uk1_ROIS(2)
            uk1_int = uk1_int + raw_data(i)
        Next i

        For i = uk1_ROIS(3) To uk1_ROIS(3) + uk1_ROIS(4) - 1
            uk1_bkg_l = uk1_bkg_l + raw_data(i)
        Next i

        For i = uk1_ROIS(5) To uk1_ROIS(5) + uk1_ROIS(6) - 1
            uk1_bkg_h = uk1_bkg_h + raw_data(i)
        Next i

        For i = uk2_ROIS(1) To uk2_ROIS(2)
            uk2_int = uk2_int + raw_data(i)
        Next i

        For i = uk2_ROIS(3) To uk2_ROIS(3) + uk2_ROIS(4) - 1
            uk2_bkg_l = uk2_bkg_l + raw_data(i)
        Next i

        For i = uk2_ROIS(5) To uk2_ROIS(5) + uk2_ROIS(6) - 1
            uk2_bkg_h = uk2_bkg_h + raw_data(i)
        Next

        uk1_background = ((uk1_bkg_l / uk1_ROIS(4)) + (uk1_bkg_h / uk1_ROIS(6))) * (1 + uk1_ROIS(2) - uk1_ROIS(1)) / 2
        uk1_net_peak_area = uk1_int - uk1_background

        uk2_background = ((uk2_bkg_l / uk2_ROIS(4)) + (uk2_bkg_h / uk2_ROIS(6))) * (1 + uk2_ROIS(2) - uk2_ROIS(1)) / 2
        uk2_net_peak_area = uk2_int - uk2_background

        U_est_par(1) = Const_val(43)
        U_est_par(2) = Const_val(44)
        U_est_par(3) = Const_val(45)
        pu_est_par(1) = Const_val(46)
        pu_est_par(2) = Const_val(47)
        pu_est_par(3) = Const_val(48)

        Dim peak_area1, u_conc_xrf_est, pu_conc_xrf_est, peak_area2
        peak_area1 = uk1_net_peak_area / xray_Lib(1, 1, 2) / live_time * 100
        '    u_conc_xrf_est = U_est_par(1) + U_est_par(2) * peak_area1 + U_est_par(3) * peak_area1 ^ 2

        u_conc_xrf_est = Inv_Lambert(U_est_par(2) / U_est_par(1) * peak_area1) / U_est_par(2)
        pu_conc_xrf_est = u_conc_xrf_est / vms_R_UPu

        peak_area2 = net_peak_area(3) / xray_Lib(2, 1, 2) / live_time * 100
        '       If u_conc_xrf_est < 10 Then pu_conc_xrf_est = pu_est_par(1) + pu_est_par(2) * peak_area2 + pu_est_par(3) * peak_area2 ^ 2   ' quadratic
        '       If u_conc_xrf_est < 10 Then pu_conc_xrf_est = -Log(-(peak_area2 - pu_est_par(1)) / pu_est_par(1)) / pu_est_par(2)           '  

        If u_conc_xrf_est < 10 Then pu_conc_xrf_est = Inv_Lambert(pu_est_par(2) / pu_est_par(1) * peak_area2) / pu_est_par(2)
        '       3
        U_conc_xrf_box.Text = Int(1000 * u_conc_xrf_est) / 1000
        vms_R_UPu = (HPGE_eff(103.734) / HPGE_eff(98.436)) * (uk1_net_peak_area / net_peak_area(3)) / (vms_R_UPu_cal * Exp(-0.110224 * results_arr(9)))

        Pu_conc_xrf_box.Text = Int(1000 * pu_conc_xrf_est) / 1000

        U_Ka1_ROI1_box.Text = uk1_int
        U_Ka1_ROI2_box.Text = Int(10 * uk1_background) / 10
        U_Ka2_ROI1_box.Text = uk2_int
        U_Ka2_ROI2_box.Text = Int(10 * uk2_background) / 10
        Pu_Ka1_ROI1_box.Text = peak_area(3)
        Pu_Ka1_ROI2_box.Text = Int(10 * Peak_BKG(3)) / 10

        U_KA1_box.Text = Int(uk1_net_peak_area * 10) / 10
        U_KA2_box.Text = Int(uk2_net_peak_area * 10) / 10
        PU_KA1_box.Text = Int(net_peak_area(3) * 10) / 10



        U_Ka1_seed_counts_box.Text = Int(uk1_net_peak_area / (xray_Lib(1, 1, 2) / 100))
        U_Ka2_seed_counts_box.Text = Int(uk2_net_peak_area / (xray_Lib(1, 2, 2) / 100))
        Pu_seed_counts_box.Text = Int(net_peak_area(3) / (xray_Lib(2, 1, 2) / 100))

        vms_U_Pu_count_ratio_box.Text = Int(1000 * uk1_net_peak_area / net_peak_area(3)) / 1000
        VMS_U_Pu_ratio_box.Text = Int(vms_R_UPu * 1000) / 1000

        cd_area = prelim_cd(fit_rois, raw_data)
        cd_prelim_area_box.Text = Int(cd_area * 10) / 10

        Return
    End Sub

    '
    '   ----------------------------------------------------------------------------------------
    '

    Sub calc_vms_conc_ked()
        Dim fit_rois(6), alpha, rel_spec(4100)
        Dim bkg_corr_ref(4100), bkg_corr_spec(4100), ref_bkg(4100), spec_bkg(4100), b_spec_temp, b_ref_temp, b_spec_sum, b_ref_sum
        Dim B_L_spec, B_L_ref, B_U_spec, B_U_ref
        Dim n_bkg_L_L, n_bkg_L_R, n_bkg_U_L, n_bkg_U_R
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, delta_mu_u

        Dim l_chan, h_chan, e_low, e_high
        Dim u_edge
        alpha = 1.1

        Call get_VMS_constants()

        Return

        If Strings.Right(vms_ref_file_name, 3) = "cnf" Or Strings.Right(vms_ref_file_name, 3) = "CNF" Then Call read_cnf_data_file(vms_ref_file_name)
        If Strings.Right(vms_ref_file_name, 3) = "spe" Or Strings.Right(vms_ref_file_name, 3) = "SPE" Then Call read_vms_ref_data_file(vms_ref_file_name)
        If Strings.Right(vms_ref_file_name, 3) = "res" Or Strings.Right(vms_ref_file_name, 3) = "RES" Then Call split_file(vms_ref_file_name)

        If Strings.Right(vms_ref_file_name, 3) = "res" Or Strings.Right(vms_ref_file_name, 3) = "RES" Then e_0 = results_arr(15)
        If Strings.Right(vms_ref_file_name, 3) = "res" Or Strings.Right(vms_ref_file_name, 3) = "RES" Then d_e = results_arr(16)

        'prelim energy calibration

        u_edge = fit_rois(1)
        U_e_l1 = fit_rois(2)
        U_e_l2 = fit_rois(3)
        U_e_r1 = fit_rois(4)
        U_e_r2 = fit_rois(5)

        delta_mu_u = fit_rois(6)

        h_chan = e_cal_par(3)
        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        e_high = e_cal_par(4)

        prelim_energy_cal(l_chan, h_chan, e_low, e_high, raw_data)

        '   remove background

        n_bkg_L_L = Int((bkg_roi(1) - new_e0) / new_de)
        n_bkg_L_R = Int((bkg_roi(2) - new_e0) / new_de)
        n_bkg_U_L = Int((bkg_roi(3) - new_e0) / new_de)
        n_bkg_U_R = Int((bkg_roi(4) - new_e0) / new_de)
        '

        B_L_spec = 0
        B_L_ref = 0
        B_U_spec = 0
        B_U_ref = 0

        For i = n_bkg_L_L To n_bkg_L_R
            B_L_spec = B_L_spec + raw_data(i)
            B_L_ref = B_L_ref + vms_ref_raw_data(i)
        Next i

        For i = n_bkg_U_L To n_bkg_U_R
            B_U_spec = B_L_spec + raw_data(i)
            B_U_ref = B_L_ref + vms_ref_raw_data(i)
        Next i

        B_L_spec = B_L_spec / (n_bkg_L_R - n_bkg_L_L + 1)
        B_L_ref = B_L_ref / (n_bkg_L_R - n_bkg_L_L + 1)
        B_U_spec = B_U_spec / (n_bkg_U_R - n_bkg_U_L + 1)
        B_U_ref = B_U_ref / (n_bkg_U_R - n_bkg_U_L + 1)

        b_spec_sum = 0
        b_ref_sum = 0
        b_spec_temp = 0
        b_ref_temp = 0


        For i = n_bkg_L_R To n_bkg_U_L
            b_spec_sum = b_spec_sum + raw_data(i)
            b_ref_sum = b_ref_sum + vms_ref_raw_data(i)
        Next i

        For i = n_bkg_L_R To n_bkg_U_L

            b_spec_temp = b_spec_temp + raw_data(i)
            b_ref_temp = b_ref_temp + vms_ref_raw_data(i)

            spec_bkg(i) = B_L_spec + (B_U_spec - B_L_spec) * b_spec_temp / b_spec_sum
            ref_bkg(i) = B_L_ref + (B_U_ref - B_L_ref) * b_ref_temp / b_ref_sum
        Next i

        For i = 1000 * Int(max_channels / 2048) To 2000 * Int(max_channels / 2048)
            rel_spec(i) = (raw_data(i) - spec_bkg(i)) / (vms_ref_raw_data(i) - ref_bkg(i)) * vms_ref_live_time / live_time
        Next i


        For i = 1 To 6
            fit_rois(i) = K_fit_rois(1, i)
        Next
        prelim_U_conc = prelim_conc(fit_rois, rel_spec)
        '   4
        U_conc_xrf_box.Text = "4 " & Int((prelim_U_conc + K_fit_rois(1, 7)) * 1000000) / 1000

        For i = 1 To 6
            fit_rois(i) = K_fit_rois(3, i)
        Next
        prelim_Pu_conc = prelim_conc(fit_rois, rel_spec)

        Pu_conc_xrf_box.Text = Int((prelim_Pu_conc + K_fit_rois(3, 7)) * 1000000) / 1000

        cd_area = prelim_cd(fit_rois, raw_data)

        cd_prelim_area_box.Text = Int(cd_area * 10) / 10


    End Sub
    Function prelim_conc(fit_rois, norm_spec)

        Dim l_chan, h_chan, e_low, e_high
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, alpha
        Dim U_L1, U_L2, U_R1, U_R2 As Integer
        Dim temp_x(100), temp_y(100)
        Dim u_lower_e0, u_lower_de, u_upper_e0, u_upper_de

        Dim interp_L, interp_H
        Dim U_edge_ratio, prelim_Z_conc, delta_mu_u, energy, u_edge

        alpha = 1.1

        Dim n_pts As Integer
        h_chan = e_cal_par(3)
        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        e_high = e_cal_par(4)

        u_edge = fit_rois(1)
        U_e_l1 = fit_rois(2)
        U_e_l2 = fit_rois(3)
        U_e_r1 = fit_rois(4)
        U_e_r2 = fit_rois(5)

        delta_mu_u = fit_rois(6)

        h_chan = e_cal_par(3)
        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        e_high = e_cal_par(4)

        prelim_energy_cal(l_chan, h_chan, e_low, e_high, raw_data)
        cd_area = peak_area(88.034, new_e0, new_de, 0.22, raw_data)

        U_L1 = Int((U_e_l1 - new_e0) / new_de)
        U_L2 = Int((U_e_l2 - new_e0) / new_de)
        U_R1 = Int((U_e_r1 - new_e0) / new_de)
        U_R2 = Int((U_e_r2 - new_e0) / new_de)


        For i = U_L1 To U_L2
            energy = new_e0 + i * new_de
            temp_x(i - U_L1 + 1) = Log(new_e0 + i * new_de) '- Log(U_e_l1)
            temp_y(i - U_L1 + 1) = Log(Log(1 / norm_spec(i)))
        Next i

        n_pts = U_L2 - U_L1 + 1

        u_lower_e0 = fit_lin(temp_x, temp_y, n_pts, 1)     '  coefficients for lower roi
        u_lower_de = fit_lin(temp_x, temp_y, n_pts, 2)

        For i = U_R1 To U_R2

            temp_x(i - U_R1 + 1) = Log(new_e0 + i * new_de) ' - Log(U_e_r1)
            temp_y(i - U_R1 + 1) = Log(Log(1 / norm_spec(i)))
        Next i

        n_pts = U_R2 - U_R1 + 1

        u_upper_e0 = fit_lin(temp_x, temp_y, n_pts, 1)      '  coefficients for upper roi
        u_upper_de = fit_lin(temp_x, temp_y, n_pts, 2)

        interp_L = u_lower_e0 + u_lower_de * Log(u_edge)
        interp_H = u_upper_e0 + u_upper_de * Log(u_edge)

        U_edge_ratio = Exp(interp_H) - Exp(interp_L)

        prelim_Z_conc = (U_edge_ratio) / delta_mu_u / vial_diameter


        prelim_conc = prelim_Z_conc
    End Function

    Function prelim_cd(fit_rois, norm_spec)

        Dim l_chan, h_chan, e_low, e_high
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, alpha
        Dim temp_x(100), temp_y(100)
        Dim delta_mu_u, u_edge

        alpha = 1.1

        Dim n_pts As Integer

        u_edge = fit_rois(1)
        U_e_l1 = fit_rois(2)
        U_e_l2 = fit_rois(3)
        U_e_r1 = fit_rois(4)
        U_e_r2 = fit_rois(5)

        delta_mu_u = fit_rois(6)

        h_chan = e_cal_par(3)
        l_chan = e_cal_par(1)
        e_low = e_cal_par(2)
        e_high = e_cal_par(4)

        prelim_energy_cal(l_chan, h_chan, e_low, e_high, raw_data)
        cd_area = peak_area(88.034, new_e0, new_de, 0.22, raw_data)

        prelim_cd = cd_area
    End Function


    Private Sub WhoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WhoToolStripMenuItem.Click
        If myForm Is Nothing Then
            MyForm11 = New Set_directories
        End If

        MyForm11.Show()
        MyForm11 = Nothing
    End Sub

    Private Sub VMSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VMSToolStripMenuItem.Click

        If myForm Is Nothing Then
            MyForm10 = New VMS_Setup
        End If

        MyForm10.Show()
        MyForm10 = Nothing

    End Sub



    Function get_chan_data(str00, i_lr)
        Dim str11, str12 As String
        Dim i, i11, i12 As Integer
        Dim chan_data(8)
        str11 = Strings.LTrim(str00)

        For i = 1 To 7

            i11 = Strings.InStr(str11, " ")
            chan_data(i) = Val(Strings.Left(str11, i11))
            i12 = Strings.Len(str11)
            str11 = Strings.Right(str11, i12 - i11)
            str11 = Strings.LTrim(str11)
        Next i
        chan_data(8) = Val(str11)
        get_chan_data = chan_data(i_lr)
    End Function

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim a111 As String

        a111 = a111 & "MEXRF Fit, Ver. 1.1" & vbCrLf
        a111 = a111 & "March 2, 2022" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "Questions?, contact" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "Robert D. McElroy, Jr." & vbCrLf
        a111 = a111 & "Oak Ridge National Laboratory" & vbCrLf
        a111 = a111 & "One Bethel Valley Road" & vbCrLf
        a111 = a111 & "P.O. Box 2008, MS 6166" & vbCrLf
        a111 = a111 & "Oak Ridge, TN 37831-6166" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "mcelroyrd@ornl.gov" & "" & vbCrLf
        a111 = a111 & "" & vbCrLf
        a111 = a111 & "Note: This software is intended only as a tool to aide in the  " & vbCrLf
        a111 = a111 & "           evaluation of the spectral fitting approach MEXRF analysis  " & vbCrLf

        MsgBox(a111)
    End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        '   BackgroundWorker1.RunWorkerAsync()
        If MyForm13 Is Nothing Then
            MyForm13 = New Fit_Status
        End If

        MyForm13.Show()
        MyForm13 = Nothing

    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
start_loop: number_running = 0
        If MyForm13 Is Nothing Then
            MyForm13 = New Fit_Status
        End If

        MyForm13.Show()
        MyForm13 = Nothing
        GoTo start_loop
    End Sub

    Private Sub AttenuationCoefficientsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AttenuationCoefficientsToolStripMenuItem.Click
        If MyForm14 Is Nothing Then
            MyForm14 = New AttenuationCoefficients
        End If

        MyForm14.Show()
        MyForm14 = Nothing
    End Sub

    Private Sub Chart1_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart1.MouseMove

        Dim result As HitTestResult = Chart1.HitTest(e.X, e.Y)

        If result.ChartElementType = ChartElementType.DataPoint Then
            Chart1.Series(0).Points(result.PointIndex).XValue.ToString()

            Dim thisPt As New PointF(CSng(Chart1.Series(0).Points(result.PointIndex).XValue),
                                    CSng(Chart1.Series(0).Points(result.PointIndex).YValues(0)))
            Dim ta As New CalloutAnnotation
            With ta
                .AnchorDataPoint = Chart1.Series(0).Points(result.PointIndex)
                .X = thisPt.X + 1
                .Y = thisPt.Y + 1
                .Text = thisPt.ToString
                .CalloutStyle = CalloutStyle.RoundedRectangle
                .ForeColor = Color.Red
                .Font = New Font("Arial", 8, FontStyle.Bold)
            End With
            Chart1.Annotations(0) = ta
            Chart1.Invalidate()
        End If
    End Sub


    Public Sub read_cnf_data_file(HKEDfilename)
        '
        '  ******  Extract Spectral Data from CNF file and store in temporary file \MEXRF\xrfjunk.rpt
        '  ******  using genie report template datadmp.tpl which as been modifed for the KED files
        '
        Dim n_chan, idex, jdex, chan_start, i_trip, i_tot, tim_flag, i_lr, ener_flag As Integer
        Dim vial_flag, temper_flag, enrich_flag, Puwgt_flag As Integer
        Dim str11, str_time, str_vial, str_ener, str_temper, str_enrich, str_Puwgt, Sample_ID_str As String
        Dim dat1
        Dim live_time2, real_time2

        Call get_system_constants(const_file_name)
        vial_diameter = MaskedTextBox3.Text                     ' if no vial diameter in file set to constants value
        sample_temperature = MaskedTextBox4.Text                 ' if no temp available in file set to reference temp
        U_enrichment = MaskedTextBox5.Text                      ' if no enrichment in file set to natural
        Pu_weight = MaskedTextBox6.Text                         ' if no pu weight in file set to reference weight
        live_time = 1
        real_time = 1
        idex = 0
        e_0 = 0
        d_e = 0.09

        Dim PauseTime, Start, Finish, TotalTime
        Dim strPrgName As String
        Dim strArg As String
        Dim temp$(9)
        Dim filname, tarname, trPrgName, outf, dirname As String

        outf = "xrfjunk.rpt"

        filname = Chr(34) & HKEDfilename & Chr(34)          '  ADD double quotes to accommodate spaces in file path

        tarname = " /template=" & root_dir_name & "\datadmp.tpl /newfile /section=data /outfile=" & root_dir_name & outf$
        strPrgName = genie_report_dir_name & " " & filname & tarname

        strArg = ""

        Dim retval
        retval = Shell(strPrgName, vbHide)

        delay(1500)        '  pause while cnf file is read using canberra executable
        If max_channels > 2048 Then delay(1000)

        '   ********************************************************************************************
        '
        '  ******** Extract data from temporary file
        '
        Dim temp_file_name As String

        temp_file_name = root_dir_name & "xrfjunk.rpt"
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(temp_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(":")       ' 
            Dim currentRow, str10 As String()
            Dim str1, str2, path_str, temper_str, ecal_0_str, ecal_slope_str As String
            Dim spec_flag As Boolean
            Dim i_chan As Integer

            i_trip = 0
            i_tot = 0
            tim_flag = 3
            vial_flag = 3
            temper_flag = 3
            enrich_flag = 3
            Puwgt_flag = 3
            ener_flag = 3
            Sample_ID_str = "no_sample_id"
            str1 = ""
            str2 = ""
            spec_flag = False

            idex = 0
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1
                    Dim currentField As String
                    jdex = 0
                    For Each currentField In currentRow
                        '        str10 = currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If jdex = 1 Then str1 = str11
                        If jdex = 2 Then str2 = str11


                        If Strings.Left(str11, 7) = "Channel" Then spec_flag = True
                        If Strings.Left(str11, 7) = "Channel" Then GoTo 50

                        If str1 = "Elapsed Live time" And jdex = 2 Then live_time = Val(str2)
                        If str1 = "Elapsed Real Time" And jdex = 2 Then real_time = Val(str2)
                        If str1 = "Acq. Start time" And jdex = 2 Then str_date = str2
                        If str1 = "Acq. Start time" And jdex = 3 Then str_date = str_date & ":" & str11
                        If str1 = "Acq. Start time" And jdex = 4 Then str_date = str_date & ":" & str11
                        If str1 = "KED Path Length" And jdex = 2 Then path_str = Val(str2)
                        If str1 = "Pu Atomic weight" And jdex = 2 Then str_Puwgt = Val(str2)
                        If str1 = "U enrichment" And jdex = 2 Then str_enrich = Val(str2)
                        If str1 = "Sample Vial Temp" And jdex = 2 Then temper_str = Val(str2)
                        If str1 = "Energy cal zero" And jdex = 2 Then ecal_0_str = Val(str2)
                        If str1 = "Energy cal slope" And jdex = 2 Then ecal_slope_str = Val(str2)
                        If str1 = "Sample Title" And jdex = 2 Then Sample_ID_str = str2
                        If str1 = "Channels" And jdex = 2 Then num_chans_in = Val(str2)
                        '  If str1 = "Sample Title" And jdex = 2 Then MsgBox(Sample_ID_str)
                        '         MsgBox(idex & "; " & jdex & " : " & str11 & " str1= " & str1 & " str2 = " & str2)
                        If spec_flag = False Then GoTo 50

                        i_chan = Val(str1)

                        Dim text_out As String
                        text_out = str2 & vbCrLf
                        For j_chan = 1 To 8
                            raw_data(i_chan + j_chan - 1) = get_chan_data(str2, j_chan)
                            text_out = text_out & i_chan & " : " & j_chan & " :: " & raw_data(i_chan + j_chan + 1) & vbCrLf
                            '                         num_chans_in = i_chan + j_chan + 1
                        Next j_chan
                        '   MsgBox(text_out)
50:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While

            Dim vial_temp, temp_temp

100:        'MsgBox("array too long " & i_tot & " " & raw_data(max_channels))
            If Strings.Left(path_str, 1) <> "?" Then vial_temp = Val(path_str)
            If Strings.Left(temper_str, 1) <> "?" Then temp_temp = Val(temper_str)
            If vial_temp > 0 Then vial_diameter = vial_temp
            If temp_temp > 0 Then sample_temperature = temp_temp

            e_0 = Val(ecal_0_str)
            d_e = Val(ecal_slope_str)

            '      vial_diameter = Val(str_vial)
            '      sample_temperature = Val(str_temper)
            U_enrichment = Val(str_enrich)
            Pu_weight = Val(str_Puwgt)

        End Using
200:   ' skip junk


        Dim i_len As Integer
        Sample_ID = "No_ID_found"
        i_len = Strings.Len(Sample_ID_str)
        '       If i_len > 11 Then Sample_ID = Strings.Right(Sample_ID_str, i_len - 11)
        If i_len > 3 Then Sample_ID = Strings.LTrim(Strings.RTrim(Sample_ID_str))
        acq_date_time = str_date

        Me.SampleIDBox1.Text = Sample_ID
        Me.sampleinfoBox1.Text = vial_diameter
        Me.sampleinfoBox2.Text = sample_temperature
        Me.sampleinfoBox3.Text = U_enrichment
        Me.sampleinfoBox4.Text = Pu_weight
        Me.Cycle_number_Box.Text = Strings.Left(Strings.Right(datafilename1, 6), 2)


    End Sub
    '  ******************************************************************************************



    Public Sub read_cnf_passive_file(HKEDfilename)
        '
        '  ******  Extract Spectral Data from CNF file and store in temporary file \MEXRF\xrfjunk.rpt
        '  ******  using genie report template datadmp.tpl which as been modifed for the KED files
        '
        Dim n_chan, idex, jdex, chan_start, i_trip, i_tot, tim_flag, i_lr, ener_flag As Integer
        Dim vial_flag, temper_flag, enrich_flag, Puwgt_flag As Integer
        Dim str11, str_time, str_vial, str_ener, str_temper, str_enrich, str_Puwgt, Sample_ID_str As String
        Dim dat1
        Dim live_time2, real_time2

        Call get_system_constants(const_file_name)
        vial_diameter = MaskedTextBox3.Text                     ' if no vial diameter in file set to constants value
        sample_temperature = MaskedTextBox4.Text                 ' if no temp available in file set to reference temp
        U_enrichment = MaskedTextBox5.Text                      ' if no enrichment in file set to natural
        Pu_weight = MaskedTextBox6.Text                         ' if no pu weight in file set to reference weight
        live_time = 1
        real_time = 1
        idex = 0
        e_0 = 0
        d_e = 0.09

        Dim PauseTime, Start, Finish, TotalTime
        Dim strPrgName As String
        Dim strArg As String
        Dim temp$(9)
        Dim filname, tarname, trPrgName, outf, dirname As String

        outf = "xrfjunk.rpt"

        filname = HKEDfilename

        tarname = " /template=" & root_dir_name & "\datadmp.tpl /newfile /section=data /outfile=" & root_dir_name & outf$
        strPrgName = genie_report_dir_name & " " & filname & tarname

        strArg = ""

        Dim retval
        retval = Shell(strPrgName, vbHide)

        If max_channels < 2050 Then delay(1500) Else delay(2500)                '  pause while cnf file is read using canberra executable

        '   ********************************************************************************************
        '
        '  ******** Extract data from temporary file
        '
        Dim temp_file_name As String

        temp_file_name = root_dir_name & "XRFjunk.rpt"
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(temp_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(":")       ' 
            Dim currentRow, str10 As String()
            Dim str1, str2, path_str, temper_str As String
            Dim spec_flag As Boolean
            Dim i_chan As Integer

            i_trip = 0
            i_tot = 0
            tim_flag = 3
            vial_flag = 3
            temper_flag = 3
            enrich_flag = 3
            Puwgt_flag = 3
            ener_flag = 3
            Sample_ID_str = "no_sample_id"
            str1 = ""
            str2 = ""
            spec_flag = False

            idex = 0
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1
                    Dim currentField As String
                    jdex = 0
                    For Each currentField In currentRow
                        '        str10 = currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If jdex = 1 Then str1 = str11
                        If jdex = 2 Then str2 = str11


                        If Strings.Left(str11, 7) = "Channel" Then spec_flag = True
                        If Strings.Left(str11, 7) = "Channel" Then GoTo 50

                        If str1 = "Elapsed Live time" And jdex = 2 Then live_time = Val(str2)
                        If str1 = "Elapsed Real Time" And jdex = 2 Then real_time = Val(str2)
                        If str1 = "KED Path Length" And jdex = 2 Then path_str = Val(str2)
                        If str1 = "Sample Vial Temp" And jdex = 2 Then temper_str = Val(str2)
                        If str1 = "Sample Title" And jdex = 2 Then Sample_ID_str = str2
                        '  If str1 = "Sample Title" And jdex = 2 Then MsgBox(Sample_ID_str)
                        '         MsgBox(idex & "; " & jdex & " : " & str11 & " str1= " & str1 & " str2 = " & str2)
                        If spec_flag = False Then GoTo 50

                        i_chan = Val(str1)

                        Dim text_out As String
                        text_out = str2 & vbCrLf
                        For j_chan = 1 To 8
                            passive_data(i_chan + j_chan - 1) = get_chan_data(str2, j_chan)
                            text_out = text_out & i_chan & " : " & j_chan & " :: " & passive_data(i_chan + j_chan + 1) & vbCrLf
                        Next j_chan
                        '   MsgBox(text_out)
50:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While


            Dim vial_temp, temp_temp

100:        'MsgBox("array too long " & i_tot & " " & raw_data(max_channels))
            If Strings.Left(path_str, 1) <> "?" Then vial_temp = Val(path_str)
            If Strings.Left(temper_str, 1) <> "?" Then temp_temp = Val(temper_str)
            If vial_temp > 0 Then vial_diameter = vial_temp
            If temp_temp > 0 Then sample_temperature = temp_temp

            U_enrichment = Val(str_enrich)
            Pu_weight = Val(str_Puwgt)

        End Using
200:   ' skip junk


        Dim i_len As Integer
        Sample_ID = "No_ID_found"
        i_len = Strings.Len(Sample_ID_str)
        '       If i_len > 11 Then Sample_ID = Strings.Right(Sample_ID_str, i_len - 11)
        If i_len > 3 Then Sample_ID = Strings.LTrim(Strings.RTrim(Sample_ID_str))



    End Sub




    '  ******************************************************************************************

    Public Sub read_spe_passive_file()
        ' reads file in text format from datafilename1
        ' first find energy cal
        ' then locate $DATA:

        Dim n_chan, idex, jdex, chan_start, i_trip, i_tot, tim_flag, i_lr, ener_flag As Integer
        Dim vial_flag, temper_flag, enrich_flag, Puwgt_flag As Integer


        Dim str11, str_time, str_vial, str_ener, str_temper, str_enrich, str_Puwgt, Sample_ID_str As String
        Dim dat1
        Dim live_time2, real_time2
        ' Dim vial_diameter, sample_temperature, U_enrichment, Pu_weight
        vial_diameter = 1
        sample_temperature = 22
        U_enrichment = 0.7
        Pu_weight = 0
        live_time = 1
        real_time = 1
        idex = 0
        '     MsgBox("reader " & datafilename1)
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(datafilename1)
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
                        If i_trip > 2 Then passive_data(i_trip - 2) = dat1
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

            e_0 = get_e_cal(str_ener, 1)
            d_e = get_e_cal(str_ener, 2)

            vial_diameter = Val(str_vial)
            sample_temperature = Val(str_temper)
            U_enrichment = Val(str_enrich)
            Pu_weight = Val(str_Puwgt)

        End Using

        Dim i_len As Integer

        i_len = Strings.Len(Sample_ID_str)
        If i_len > 11 Then Sample_ID = Strings.Right(Sample_ID_str, i_len - 11)


    End Sub

    Private Sub XrayDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XrayDataToolStripMenuItem.Click
        If MyForm15 Is Nothing Then
            MyForm15 = New Xray_Data_entry
        End If

        MyForm15.Show()
        MyForm15 = Nothing
    End Sub



    Private Sub Get_KED_Results_Click(sender As Object, e As EventArgs) Handles Get_KED_Results.Click

        '   get parameters from KED results files
        Dim num_ked_files


        'Select and Read Data Files
        '
        Dim LastDir As String = ""
        Dim ked_files(99) As String
        Dim idex As Integer

        Dim Response1 As String
        Dim Newdata1 As Integer

        MEKED_results_flag = True
        '
        ' file names for input to xrf_fit_###.exe

        For i = 1 To 99
            ked_files(i) = exe_dir_name & "\spec_fit_" & i - 1 & ".txt"
        Next i

        Dim fbd As New OpenFileDialog
        If LastDir = "" Then Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)

        With fbd
            .Title = "Select KED Result file"
            .InitialDirectory = LastDir
            .Multiselect = True 'allow user to select KED result files
        End With

        idex = 0
        If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then ' check user click ok or cancel

            LastDir = Path.GetDirectoryName(fbd.FileName) 'Update your last Directory.

            For Each mFile As String In fbd.FileNames
                idex = idex + 1
                ked_files(idex) = mFile
                '     ListBox1.Items.Add(mFile)
            Next
        End If

        num_ked_files = idex
        If num_ked_files = 0 Then GoTo 100

        For i = 1 To 36
            KED_PAR(i) = 0
            KED_PAR_err(i) = 0
        Next i


        '   *************************************************************************
        Dim i_file, i1, i2 As Integer

        For i_file = 1 To 1   ' num_ked_files
            datafilename1 = ked_files(i_file)

            '  strip off path
            i1 = Strings.InStrRev(datafilename1, "\")
            i2 = Strings.Len(datafilename1)
            input_file_name = Strings.Right(datafilename1, i2 - i1)

            ' ***** Create the input file for XRF_FIT *****
            datafilenamebox1 = datafilename1
            input_ked_filenames(i_file) = datafilename1


            If Strings.Right(datafilename1, 3) = "res" Or Strings.Right(datafilename1, 3) = "RES" Then Call get_ked_pars(datafilename1)
            If Strings.Right(datafilename1, 3) <> "res" Then Return


            ' read in the raw data from the selected SPE file


            '     Call Button4_Click(sender, e)

            KED_Box_1.Text = KED_PAR(8)
            KED_Box_2.Text = KED_PAR(10)
            KED_Box_3.Text = KED_PAR(1)
            KED_Box_4.Text = KED_PAR(2)
            KED_Box_5.Text = KED_PAR(3)
            KED_Box_6.Text = KED_PAR(5)
            KED_Box_7.Text = KED_PAR(4)
            KED_Box_8.Text = KED_PAR(6)

            KED_check_box_1.Checked = True
            KED_check_box_2.Checked = True
            KED_check_box_3.Checked = True
            KED_check_box_4.Checked = True

            If CheckBox1.Checked Then e_0 = MaskedTextBox1.Text
            If CheckBox2.Checked Then d_e = MaskedTextBox2.Text
            If CheckBox3.Checked Then vial_diameter = MaskedTextBox3.Text
            If CheckBox4.Checked Then sample_temperature = MaskedTextBox4.Text
            If CheckBox5.Checked Then U_enrichment = MaskedTextBox5.Text
            If CheckBox6.Checked Then Pu_weight = MaskedTextBox6.Text

            If KED_check_box_1.Checked Then KED_PAR(8) = KED_Box_1.Text
            If KED_check_box_2.Checked Then KED_PAR(10) = KED_Box_2.Text
            If KED_check_box_3.Checked Then KED_PAR(1) = KED_Box_3.Text
            If KED_check_box_4.Checked Then KED_PAR(2) = KED_Box_4.Text
            If KED_check_box_5.Checked Then KED_PAR(3) = KED_Box_5.Text
            If KED_check_box_6.Checked Then KED_PAR(5) = KED_Box_6.Text
            If KED_check_box_7.Checked Then KED_PAR(4) = KED_Box_7.Text
            If KED_check_box_8.Checked Then KED_PAR(6) = KED_Box_8.Text


        Next i_file
        MEKED_results_flag = True

100:    ' UPDATE PARAMETERS FROM CHECK BOXES



        If KED_check_box_1.Checked Then KED_PAR(8) = KED_Box_1.Text
        If KED_check_box_2.Checked Then KED_PAR(10) = KED_Box_2.Text
        If KED_check_box_3.Checked Then KED_PAR(1) = KED_Box_3.Text
        If KED_check_box_4.Checked Then KED_PAR(2) = KED_Box_4.Text
        If KED_check_box_5.Checked Then KED_PAR(3) = KED_Box_5.Text
        If KED_check_box_6.Checked Then KED_PAR(5) = KED_Box_6.Text
        If KED_check_box_7.Checked Then KED_PAR(4) = KED_Box_7.Text
        If KED_check_box_8.Checked Then KED_PAR(6) = KED_Box_8.Text


    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call get_MEKED_summary("")

        MEKED_sumMary_flag = True

        For i = 1 To 5
            KED_PAR(i) = meked_summary(i) / 1000
            KED_PAR_err(i) = meked_summary_err(i) / 1000
        Next i

        For i = 6 To 36
            KED_PAR(i) = meked_summary(i)
            KED_PAR_err(i) = meked_summary_err(i)
        Next i


        KED_Box_1.Text = KED_PAR(8)
        KED_Box_2.Text = KED_PAR(10)
        KED_Box_3.Text = KED_PAR(1)
        KED_Box_4.Text = KED_PAR(2)
        KED_Box_5.Text = KED_PAR(3)
        KED_Box_6.Text = KED_PAR(5)
        KED_Box_7.Text = KED_PAR(4)
        KED_Box_8.Text = KED_PAR(6)

        KED_check_box_1.Checked = True
        KED_check_box_2.Checked = True
        KED_check_box_3.Checked = True
        KED_check_box_4.Checked = True

        If KED_PAR(1) > KED_PAR(2) Then ref_el_U_button.Checked = True Else ref_el_Pu_button.Checked = True

    End Sub

    Private Sub Dispaly_E_button_CheckedChanged(sender As Object, e As EventArgs) Handles Display_E_button.CheckedChanged

    End Sub

    Function back_i(i, i_start, B_L, B_r, sum_roi, spectrum)

        Dim sum_yj
        sum_yj = 0
        For j = i_start To i
            sum_yj = sum_yj + spectrum(j)
        Next j

        back_i = B_L + (B_r - B_L) * sum_yj / sum_roi

    End Function

    Private Sub SetUpPassiveCorrectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetUpPassiveCorrectionToolStripMenuItem.Click
        If myForm Is Nothing Then
            MyForm16 = New Fission_Products
        End If
        MyForm16.Show()
        MyForm16 = Nothing
    End Sub

    Private Sub CreateSummedSpectrumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateSummedSpectrumToolStripMenuItem.Click
        If myForm Is Nothing Then
            myform17 = New summed_spectra
        End If
        myform17.Show()
        myform17 = Nothing
    End Sub

    Private Sub ConvertCNFToSPEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertCNFToSPEToolStripMenuItem.Click
        If myForm Is Nothing Then
            myform18 = New Convert_CNF_Files
        End If
        myform18.Show()
        myform18 = Nothing
    End Sub

    Private Sub SubtractPassiveSpectrumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubtractPassiveSpectrumToolStripMenuItem.Click
        If myForm Is Nothing Then
            myform19 = New Subtract_passive
        End If
        myform19.Show()
        myform19 = Nothing
    End Sub

    Private Sub ConfigurePassiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurePassiveToolStripMenuItem.Click
        If myForm Is Nothing Then
            MyForm16 = New Fission_Products
        End If
        MyForm16.Show()
        MyForm16 = Nothing
    End Sub

    Private Sub cd_prelim_area_box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles cd_prelim_area_box.MaskInputRejected

    End Sub

    Private Sub clear_ked_input_Click(sender As Object, e As EventArgs) Handles clear_ked_input.Click

        ' resets KED entries
        Dim meked_seed As String

        meked_seed = seed_parm_dir_name & "generic_ked_parameters.res" & " "

        Call get_ked_pars(meked_seed)

        MEKED_sumMary_flag = False
        MEKED_results_flag = False


        KED_Box_1.Text = Int(10000 * KED_PAR(8)) / 10000
        KED_Box_2.Text = Int(10000 * KED_PAR(10)) / 10000
        KED_Box_3.Text = Int(10000 * KED_PAR(1)) / 10000
        KED_Box_4.Text = Int(10000 * KED_PAR(2)) / 10000
        KED_Box_5.Text = Int(10000 * KED_PAR(3)) / 10000
        KED_Box_6.Text = Int(10000 * KED_PAR(5)) / 10000
        KED_Box_7.Text = Int(10000 * KED_PAR(4)) / 10000
        KED_Box_8.Text = Int(10000 * KED_PAR(6)) / 10000

        KED_check_box_1.Checked = False
        KED_check_box_2.Checked = False
        KED_check_box_3.Checked = False
        KED_check_box_4.Checked = False

        ref_el_U_button.Checked = False
        ref_el_Pu_button.Checked = False


    End Sub



    Public Sub display_E_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles RunPlease.Click
        Dim ener, ymin, ymax
        Dim chan_scale As Boolean
        ' e_0 = 0.0001
        ' del_e = 0.09
        chan_scale = False
        If Display_chan_button.Checked = True Then chan_scale = True

        ymin = 0
        ymax = 0
        For i = 1 To max_channels - 8
            ener = e_0 + d_e * i
            If chan_scale = True Then ener = i
            If ener > 20 And raw_data(i) > ymax Then ymax = raw_data(i)
        Next
        ymin = ymax
        For i = 1 To max_channels - 1
            ener = e_0 + d_e * i
            If ener > 20 And raw_data(i) < ymin Then ymin = raw_data(i)
        Next

        If ymin < 1 Then ymin = 1
        If ymax < 10 Then ymax = 10
        ymax = 10 ^ Int(Log10(ymax) + 1)
        ymin = 10 ^ Int(Log10(ymin))
        If ymin < 1 Then ymin = 1

        With Chart1.ChartAreas(0)

            .AxisX.Minimum = 0
            .AxisX.Interval = 20
            .AxisX.Maximum = 180

            If chan_scale = True Then .AxisX.Interval = Int(max_channels / 10)
            If chan_scale = True Then .AxisX.Maximum = max_channels

            .AxisY.IsLogarithmic = True
            .AxisY.Minimum = ymin
            .AxisY.Maximum = ymax

            .AxisX.Title = "Energy (keV)"
            If chan_scale = True Then .AxisX.Title = "Channels"
            .AxisY.Title = "Counts"
        End With

        Chart1.Series.Clear()

        Chart1.Series.Add("Raw_Counts")

        With Chart1.Series(0)
            .ChartType = DataVisualization.Charting.SeriesChartType.Line
            .BorderWidth = 2
            '    .Color = Color.Blue
            '  .MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            '  .MarkerSize = 8
            '.IsVisibleInLegend = False
            For i = 1 To max_channels - 8
                ener = e_0 + d_e * i
                If raw_data(i) > 0 Then Me.Chart1.Series("Raw_Counts").Points.AddXY(ener, raw_data(i)) Else _
                    Me.Chart1.Series("Raw_Counts").Points.AddXY(ener, 1)
            Next

            Dim PC As New CalloutAnnotation
            With PC
                Chart1.Annotations.Add(PC)
            End With

        End With
    End Sub

    Private Sub GammarayDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GammarayDataToolStripMenuItem.Click
        '
        '  Gamma-ray parameters for fission products
        '
        If myform20 Is Nothing Then
            myform20 = New Gamma_ray_data_entry
        End If

        myform20.Show()
        myform20 = Nothing


    End Sub

    Private Sub read_ked_seed(initval1)

        Dim generic_file_name, temp_file_name As String
        Dim i_len, i_st, ichecked1(42) As Integer


        generic_file_name = seed_parm_dir_name & "generic_ked_parameters.txt"

        Dim fileReader As System.IO.StreamReader
        fileReader =
        My.Computer.FileSystem.OpenTextFileReader(generic_file_name)
        Dim stringReader As String
        inlineval = fileReader.ReadLine()
        temp_file_name = inlineval
        For i = 1 To 36
            ' stringReader = fileReader.ReadLine()
            inlineval = fileReader.ReadLine()
            i_len = Strings.Len(inlineval)
            If i > 9 Then i_st = 4 Else i_st = 3
            initval1(i) = Val(Strings.Mid(inlineval, i_st, i_len - 5))
            ichecked1(i) = Val(Strings.Right(inlineval, 2))

        Next i


    End Sub

End Class
