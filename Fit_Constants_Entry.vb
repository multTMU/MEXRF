Imports System.IO
Imports System.Text
Public Class Fit_Constants_Entry



    Public Result_num As Integer
    Public Property vial_pass As String
    'Public const_val(48)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' write new xrf fitting constants file for use with fortran executable
        Dim outconstants As String
        Dim klineout As String
        Dim i, j, k As Integer

        Const_val(1) = ConBox1.Text
        Const_val(2) = ConBox2.Text
        Const_val(3) = ConBox3.Text
        Const_val(4) = ConBox4.Text
        Const_val(5) = ConBox5.Text
        Const_val(6) = ConBox6.Text
        Const_val(7) = ConBox7.Text
        Const_val(8) = ConBox8.Text
        Const_val(9) = ConBox9.Text
        Const_val(10) = ConBox10.Text

        Const_val(11) = ConBox11.Text
        Const_val(12) = ConBox12.Text
        Const_val(13) = ConBox13.Text
        Const_val(14) = ConBox14.Text
        Const_val(15) = ConBox15.Text
        Const_val(16) = ConBox16.Text
        Const_val(17) = ConBox17.Text
        Const_val(18) = ConBox18.Text
        Const_val(19) = ConBox19.Text
        Const_val(20) = ConBox20.Text

        Const_val(21) = ConBox21.Text
        Const_val(22) = ConBox22.Text
        Const_val(23) = ConBox23.Text
        Const_val(24) = ConBox24.Text
        Const_val(25) = ConBox25.Text
        Const_val(26) = ConBox26.Text
        Const_val(27) = ConBox27.Text
        Const_val(28) = ConBox28.Text
        Const_val(29) = ConBox29.Text
        Const_val(30) = ConBox30.Text

        Const_val(31) = ConBox31.Text
        Const_val(32) = ConBox32.Text
        Const_val(33) = ConBox33.Text
        Const_val(34) = ConBox34.Text
        Const_val(35) = ConBox35.Text
        Const_val(36) = ConBox36.Text
        Const_val(37) = ConBox37.Text
        Const_val(38) = ConBox38.Text
        Const_val(39) = ConBox39.Text
        Const_val(40) = ConBox40.Text

        Const_val(41) = 0
        Const_val(42) = 0

        Const_val(43) = Standalone_1_1.Text
        Const_val(44) = Standalone_1_2.Text
        Const_val(45) = Standalone_1_3.Text
        Const_val(46) = Standalone_2_1.Text
        Const_val(47) = Standalone_2_2.Text
        Const_val(48) = Standalone_2_3.Text

        Const_val(49) = Standalone_3_1.Text
        Const_val(50) = Standalone_3_2.Text
        Const_val(51) = Standalone_3_3.Text
        Const_val(52) = Standalone_4_1.Text
        Const_val(53) = Standalone_4_2.Text
        Const_val(54) = Standalone_4_3.Text

        Const_val(55) = Standalone_5_1.Text
        Const_val(56) = Standalone_5_2.Text
        Const_val(57) = Standalone_5_3.Text

        Const_val(58) = 0
        Const_val(59) = 0
        Const_val(60) = 0

        Const_val(61) = EFF_par_Box1.Text
        Const_val(62) = EFF_par_Box2.Text
        Const_val(63) = EFF_par_Box3.Text
        Const_val(64) = EFF_par_Box4.Text
        Const_val(65) = EFF_par_Box5.Text
        Const_val(66) = EFF_par_Box6.Text
        Const_val(67) = EFF_par_Box7.Text

        Const_val(68) = 0
        Const_val(69) = 0
        Const_val(70) = 0

        For i = 71 To 90
            Const_val(i) = 0
        Next i

        Const_val(71) = fp_bias_box.Text
        Const_val(72) = fp_bias_err_box.Text

        Const_val(75) = empirical_corr_par_box.Text
        Const_val(76) = empirical_corr_par_err_box.Text
        Const_val(77) = empirical_corr_E1_box.Text
        Const_val(78) = empirical_corr_E2_box.Text

        For i = 1 To 2
            For j = 1 To 3
                standalone(i, j) = Const_val(42 + (i - 1) * 3 + j)
            Next j
        Next i

        standalone_err(1, 1) = Stand_err_1_1.Text
        standalone_err(1, 2) = Stand_err_1_2.Text
        standalone_err(1, 3) = Stand_err_1_3.Text
        standalone_err(2, 1) = Stand_err_2_1.Text
        standalone_err(2, 2) = Stand_err_2_2.Text
        standalone_err(2, 3) = Stand_err_2_3.Text

        standalone_err(3, 1) = Stand_err_3_1.Text
        standalone_err(3, 2) = Stand_err_3_2.Text
        standalone_err(3, 3) = Stand_err_3_3.Text
        standalone_err(4, 1) = Stand_err_4_1.Text
        standalone_err(4, 2) = Stand_err_4_2.Text
        standalone_err(4, 3) = Stand_err_4_3.Text

        standalone_err(5, 1) = Stand_err_5_1.Text
        standalone_err(5, 2) = Stand_err_5_2.Text
        standalone_err(5, 3) = Stand_err_5_3.Text

        standalone_covar(1, 1, 1) = Stand_covar_1_1_1.Text
        standalone_covar(1, 1, 2) = Stand_covar_1_1_2.Text
        standalone_covar(1, 1, 3) = Stand_covar_1_1_3.Text
        standalone_covar(1, 2, 1) = Stand_covar_1_2_1.Text
        standalone_covar(1, 2, 2) = Stand_covar_1_2_2.Text
        standalone_covar(1, 2, 3) = Stand_covar_1_2_3.Text
        standalone_covar(1, 3, 1) = Stand_covar_1_3_1.Text
        standalone_covar(1, 3, 2) = Stand_covar_1_3_2.Text
        standalone_covar(1, 3, 3) = Stand_covar_1_3_3.Text
        '
        standalone_covar(2, 1, 1) = Stand_covar_2_1_1.Text
        standalone_covar(2, 1, 2) = Stand_covar_2_1_2.Text
        standalone_covar(2, 1, 3) = Stand_covar_2_1_3.Text
        standalone_covar(2, 2, 1) = Stand_covar_2_2_1.Text
        standalone_covar(2, 2, 2) = Stand_covar_2_2_2.Text
        standalone_covar(2, 2, 3) = Stand_covar_2_2_3.Text
        standalone_covar(2, 3, 1) = Stand_covar_2_3_1.Text
        standalone_covar(2, 3, 2) = Stand_covar_2_3_2.Text
        standalone_covar(2, 3, 3) = Stand_covar_2_3_3.Text

        standalone_covar(3, 1, 1) = Stand_covar_3_1_1.Text
        standalone_covar(3, 1, 2) = Stand_covar_3_1_2.Text
        standalone_covar(3, 1, 3) = Stand_covar_3_1_3.Text
        standalone_covar(3, 2, 1) = Stand_covar_3_2_1.Text
        standalone_covar(3, 2, 2) = Stand_covar_3_2_2.Text
        standalone_covar(3, 2, 3) = Stand_covar_3_2_3.Text
        standalone_covar(3, 3, 1) = Stand_covar_3_3_1.Text
        standalone_covar(3, 3, 2) = Stand_covar_3_3_2.Text
        standalone_covar(3, 3, 3) = Stand_covar_3_3_3.Text

        standalone_covar(4, 1, 1) = Stand_covar_4_1_1.Text
        standalone_covar(4, 1, 2) = Stand_covar_4_1_2.Text
        standalone_covar(4, 1, 3) = Stand_covar_4_1_3.Text
        standalone_covar(4, 2, 1) = Stand_covar_4_2_1.Text
        standalone_covar(4, 2, 2) = Stand_covar_4_2_2.Text
        standalone_covar(4, 2, 3) = Stand_covar_4_2_3.Text
        standalone_covar(4, 3, 1) = Stand_covar_4_3_1.Text
        standalone_covar(4, 3, 2) = Stand_covar_4_3_2.Text
        standalone_covar(4, 3, 3) = Stand_covar_4_3_3.Text

        standalone_covar(5, 1, 1) = Stand_covar_5_1_1.Text
        standalone_covar(5, 1, 2) = Stand_covar_5_1_2.Text
        standalone_covar(5, 1, 3) = Stand_covar_5_1_3.Text
        standalone_covar(5, 2, 1) = Stand_covar_5_2_1.Text
        standalone_covar(5, 2, 2) = Stand_covar_5_2_2.Text
        standalone_covar(5, 2, 3) = Stand_covar_5_2_3.Text
        standalone_covar(5, 3, 1) = Stand_covar_5_3_1.Text
        standalone_covar(5, 3, 2) = Stand_covar_5_3_2.Text
        standalone_covar(5, 3, 3) = Stand_covar_5_3_3.Text
        '
        'overwrite contstants file for fitting routine  - text file located in xrf_t0
        ' unpdates xrf_system_constants file - csv form located in system_constants directory
        ' 
        For j = 0 To 0
            klineout = " " & 1 & " " & Const_val(1) & vbCrLf
            outconstants = exe_dir_name & "\XRF_FIT_CONSTANTS.txt"
            outconsts_def_csv = const_file_name

            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            For i = 2 To 42
                klineout = " " & i & " " & Const_val(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            '  ------------------


            For i = 43 To 45
                k = i - 42

                klineout = " " & i & " " & Const_val(i) & " " & standalone_err(1, k) & " " & standalone_covar(1, k, 1)
                klineout = klineout & " " & standalone_covar(1, k, 2) & " " & standalone_covar(1, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 46 To 48
                k = i - 45

                klineout = " " & i & " " & Const_val(i) & " " & standalone_err(2, k) & " " & standalone_covar(2, k, 1)
                klineout = klineout & " " & standalone_covar(2, k, 2) & " " & standalone_covar(2, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 49 To 51
                k = i - 48

                klineout = " " & i & " " & Const_val(i) & " " & standalone_err(3, k) & " " & standalone_covar(3, k, 1)
                klineout = klineout & " " & standalone_covar(3, k, 2) & " " & standalone_covar(3, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 52 To 54
                k = i - 51

                klineout = " " & i & " " & Const_val(i) & " " & standalone_err(4, k) & " " & standalone_covar(4, k, 1)
                klineout = klineout & " " & standalone_covar(4, k, 2) & " " & standalone_covar(4, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 55 To 57
                k = i - 54

                klineout = " " & i & " " & Const_val(i) & " " & standalone_err(5, k) & " " & standalone_covar(5, k, 1)
                klineout = klineout & " " & standalone_covar(5, k, 2) & " " & standalone_covar(5, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

            For i = 58 To 90
                klineout = " " & i & " " & Const_val(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                '    Console.Write(klineout)
            Next i

        Next j

        Close()
        '   ----------------------------------------------------------------------------------
        '  Call write_system_constants_file(outconsts_def_csv)

        outconstants = const_file_name  '  outconsts_def_csv
        '
        klineout = const_name(1) & ", " & Const_val(1) & vbCrLf


        Dim fs1 As FileStream = File.Create(outconstants)
        Dim info1 As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs1.Write(info1, 0, info1.Length)

        fs1.Close()

        Dim i_dx, j_dx, el_dx As Integer
        i_dx = 43
        j_dx = 45

        For i = 2 To 42
            klineout = const_name(i) & ", " & Const_val(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        el_dx = 1
        For i = i_dx To j_dx
            k = i - (i_dx - 1)
            klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
            klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        el_dx = 2
        i_dx = i_dx + 3
        j_dx = j_dx + 3

        For i = i_dx To j_dx
            k = i - (i_dx - 1)
            klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
            klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        el_dx = 3
        i_dx = i_dx + 3
        j_dx = j_dx + 3

        For i = i_dx To j_dx
            k = i - (i_dx - 1)
            klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
            klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        el_dx = 4
        i_dx = i_dx + 3
        j_dx = j_dx + 3

        For i = i_dx To j_dx
            k = i - (i_dx - 1)
            klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
            klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        el_dx = 5
        i_dx = i_dx + 3
        j_dx = j_dx + 3

        For i = i_dx To j_dx
            k = i - (i_dx - 1)
            klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
            klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        For i = 58 To 90
            klineout = const_name(i) & ", " & Const_val(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i



    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Read in seed parameters from generic file
        Dim seed_file_name As String
        Dim const_file_name1 As String
        Dim n_consts, idex, jdex As Integer
        Dim flag_vial_d_overwrite As Boolean

        Dim str11 As String
        max_directories = 3

        ' const_file_name = "C:\\MEXRF\XRF_T0\XRF_FIT_CONSTANTS.txt"
        const_file_name1 = const_file_name


        Call get_system_constants(const_file_name1)

        Call display_constants(const_file_name1)



        Vial_diam_Box1.Text = MEXRF_main.sampleinfoBox1.Text
        vial_diameter = Val(Vial_diam_Box1.Text)

        flag_vial_d_overwrite = CheckBox1.Checked
        If flag_vial_d_overwrite Then ConBox1.Text = vial_diameter


    End Sub





    Public Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Dim flag_vial_d_overwrite As Boolean
        Vial_diam_Box1.Text = MEXRF_main.sampleinfoBox1.Text
        vial_diameter = Val(Vial_diam_Box1.Text)
        flag_vial_d_overwrite = CheckBox1.Checked
        If flag_vial_d_overwrite Then ConBox1.Text = vial_diameter
        If flag_vial_d_overwrite And vial_diameter >= 2 Then ConBox16.Text = 1
        If flag_vial_d_overwrite And vial_diameter < 2 Then ConBox16.Text = 0


    End Sub

    Public Sub Vial_diam_Box1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles Vial_diam_Box1.MaskInputRejected

    End Sub
    Public Property FileName As String

    Private Sub SaveAsNewConstantsFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsNewConstantsFileToolStripMenuItem.Click
        ' write new ked fitting constants file

        Call write_system_constants_file()

    End Sub
    Sub write_system_constants_file()
        '
        ' create system parameters CSV file
        '
        Dim outconstants As String
        Dim klineout As String
        Dim i, j, k As Integer


        Const_val(1) = ConBox1.Text
        Const_val(2) = ConBox2.Text
        Const_val(3) = ConBox3.Text
        Const_val(4) = ConBox4.Text
        Const_val(5) = ConBox5.Text
        Const_val(6) = ConBox6.Text
        Const_val(7) = ConBox7.Text
        Const_val(8) = ConBox8.Text
        Const_val(9) = ConBox9.Text
        Const_val(10) = ConBox10.Text

        Const_val(11) = ConBox11.Text
        Const_val(12) = ConBox12.Text
        Const_val(13) = ConBox13.Text
        Const_val(14) = ConBox14.Text
        Const_val(15) = ConBox15.Text
        Const_val(16) = ConBox16.Text
        Const_val(17) = ConBox17.Text
        Const_val(18) = ConBox18.Text
        Const_val(19) = ConBox19.Text
        Const_val(20) = ConBox20.Text

        Const_val(21) = ConBox21.Text
        Const_val(22) = ConBox22.Text
        Const_val(23) = ConBox23.Text
        Const_val(24) = ConBox24.Text
        Const_val(25) = ConBox25.Text
        Const_val(26) = ConBox26.Text
        Const_val(27) = ConBox27.Text
        Const_val(28) = ConBox28.Text
        Const_val(29) = ConBox29.Text
        Const_val(30) = ConBox30.Text

        Const_val(31) = ConBox31.Text
        Const_val(32) = ConBox32.Text
        Const_val(33) = ConBox33.Text
        Const_val(34) = ConBox34.Text
        Const_val(35) = ConBox35.Text
        Const_val(36) = ConBox36.Text

        Const_val(37) = ConBox37.Text
        Const_val(38) = ConBox38.Text
        Const_val(39) = ConBox39.Text
        Const_val(40) = ConBox40.Text

        Const_val(43) = Standalone_1_1.Text
        Const_val(44) = Standalone_1_2.Text
        Const_val(45) = Standalone_1_3.Text
        Const_val(46) = Standalone_2_1.Text
        Const_val(47) = Standalone_2_2.Text
        Const_val(48) = Standalone_2_3.Text
        Const_val(49) = Standalone_3_1.Text
        Const_val(50) = Standalone_3_2.Text
        Const_val(51) = Standalone_3_3.Text
        Const_val(52) = Standalone_4_1.Text
        Const_val(53) = Standalone_4_2.Text
        Const_val(54) = Standalone_4_3.Text
        Const_val(55) = Standalone_5_1.Text
        Const_val(56) = Standalone_5_2.Text
        Const_val(57) = Standalone_5_3.Text
        Const_val(58) = 0
        Const_val(59) = 0
        Const_val(60) = 0

        Const_val(61) = EFF_par_Box1.Text
        Const_val(62) = EFF_par_Box2.Text
        Const_val(63) = EFF_par_Box3.Text
        Const_val(64) = EFF_par_Box4.Text
        Const_val(65) = EFF_par_Box5.Text
        Const_val(66) = EFF_par_Box6.Text
        Const_val(67) = EFF_par_Box7.Text
        Const_val(68) = 0
        Const_val(69) = 0
        Const_val(70) = 0

        For i = 71 To 90
            Const_val(i) = 0
        Next i

        Const_val(71) = fp_bias_box.Text
        Const_val(72) = fp_bias_err_box.Text

        Const_val(75) = empirical_corr_par_box.Text
        Const_val(76) = empirical_corr_par_err_box.Text
        Const_val(77) = empirical_corr_E1_box.Text
        Const_val(78) = empirical_corr_E2_box.Text


        For i = 1 To 2
            For j = 1 To 3
                standalone(i, j) = Const_val(42 + (i - 1) * 3 + j)
            Next j
        Next i

        standalone_err(1, 1) = Stand_err_1_1.Text
        standalone_err(1, 2) = Stand_err_1_2.Text
        standalone_err(1, 3) = Stand_err_1_3.Text
        standalone_err(2, 1) = Stand_err_2_1.Text
        standalone_err(2, 2) = Stand_err_2_2.Text
        standalone_err(2, 3) = Stand_err_2_3.Text
        standalone_err(3, 1) = Stand_err_3_1.Text
        standalone_err(3, 2) = Stand_err_3_2.Text
        standalone_err(3, 3) = Stand_err_3_3.Text
        standalone_err(4, 1) = Stand_err_4_1.Text
        standalone_err(4, 2) = Stand_err_4_2.Text
        standalone_err(4, 3) = Stand_err_4_3.Text
        standalone_err(5, 1) = Stand_err_5_1.Text
        standalone_err(5, 2) = Stand_err_5_2.Text
        standalone_err(5, 3) = Stand_err_5_3.Text

        standalone_covar(1, 1, 1) = Stand_covar_1_1_1.Text
        standalone_covar(1, 1, 2) = Stand_covar_1_1_2.Text
        standalone_covar(1, 1, 3) = Stand_covar_1_1_3.Text
        standalone_covar(1, 2, 1) = Stand_covar_1_2_1.Text
        standalone_covar(1, 2, 2) = Stand_covar_1_2_2.Text
        standalone_covar(1, 2, 3) = Stand_covar_1_2_3.Text
        standalone_covar(1, 3, 1) = Stand_covar_1_3_1.Text
        standalone_covar(1, 3, 2) = Stand_covar_1_3_2.Text
        standalone_covar(1, 3, 3) = Stand_covar_1_3_3.Text
        '
        standalone_covar(2, 1, 1) = Stand_covar_2_1_1.Text
        standalone_covar(2, 1, 2) = Stand_covar_2_1_2.Text
        standalone_covar(2, 1, 3) = Stand_covar_2_1_3.Text
        standalone_covar(2, 2, 1) = Stand_covar_2_2_1.Text
        standalone_covar(2, 2, 2) = Stand_covar_2_2_2.Text
        standalone_covar(2, 2, 3) = Stand_covar_2_2_3.Text
        standalone_covar(2, 3, 1) = Stand_covar_2_3_1.Text
        standalone_covar(2, 3, 2) = Stand_covar_2_3_2.Text
        standalone_covar(2, 3, 3) = Stand_covar_2_3_3.Text

        standalone_covar(3, 1, 1) = Stand_covar_3_1_1.Text
        standalone_covar(3, 1, 2) = Stand_covar_3_1_2.Text
        standalone_covar(3, 1, 3) = Stand_covar_3_1_3.Text
        standalone_covar(3, 2, 1) = Stand_covar_3_2_1.Text
        standalone_covar(3, 2, 2) = Stand_covar_3_2_2.Text
        standalone_covar(3, 2, 3) = Stand_covar_3_2_3.Text
        standalone_covar(3, 3, 1) = Stand_covar_3_3_1.Text
        standalone_covar(3, 3, 2) = Stand_covar_3_3_2.Text
        standalone_covar(3, 3, 3) = Stand_covar_3_3_3.Text

        standalone_covar(4, 1, 1) = Stand_covar_4_1_1.Text
        standalone_covar(4, 1, 2) = Stand_covar_4_1_2.Text
        standalone_covar(4, 1, 3) = Stand_covar_4_1_3.Text
        standalone_covar(4, 2, 1) = Stand_covar_4_2_1.Text
        standalone_covar(4, 2, 2) = Stand_covar_4_2_2.Text
        standalone_covar(4, 2, 3) = Stand_covar_4_2_3.Text
        standalone_covar(4, 3, 1) = Stand_covar_4_3_1.Text
        standalone_covar(4, 3, 2) = Stand_covar_4_3_2.Text
        standalone_covar(4, 3, 3) = Stand_covar_4_3_3.Text

        standalone_covar(5, 1, 1) = Stand_covar_5_1_1.Text
        standalone_covar(5, 1, 2) = Stand_covar_5_1_2.Text
        standalone_covar(5, 1, 3) = Stand_covar_5_1_3.Text
        standalone_covar(5, 2, 1) = Stand_covar_5_2_1.Text
        standalone_covar(5, 2, 2) = Stand_covar_5_2_2.Text
        standalone_covar(5, 2, 3) = Stand_covar_5_2_3.Text
        standalone_covar(5, 3, 1) = Stand_covar_5_3_1.Text
        standalone_covar(5, 3, 2) = Stand_covar_5_3_2.Text
        standalone_covar(5, 3, 3) = Stand_covar_5_3_3.Text

        SaveFileDialog1.Filter = "CSV Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)

            outconstants = SaveFileDialog1.FileName
            '
            klineout = const_name(1) & ", " & Const_val(1) & vbCrLf

            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            Dim i_dx, j_dx, el_dx As Integer
            i_dx = 43
            j_dx = 45

            For i = 2 To 42
                klineout = const_name(i) & ", " & Const_val(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            el_dx = 1
            For i = i_dx To j_dx
                k = i - (i_dx - 1)
                klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
                klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            el_dx = 2
            i_dx = i_dx + 3
            j_dx = j_dx + 3

            For i = i_dx To j_dx
                k = i - (i_dx - 1)
                klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
                klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            el_dx = 3
            i_dx = i_dx + 3
            j_dx = j_dx + 3

            For i = i_dx To j_dx
                k = i - (i_dx - 1)
                klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
                klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            el_dx = 4
            i_dx = i_dx + 3
            j_dx = j_dx + 3

            For i = i_dx To j_dx
                k = i - (i_dx - 1)
                klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
                klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            el_dx = 5
            i_dx = i_dx + 3
            j_dx = j_dx + 3

            For i = i_dx To j_dx
                k = i - (i_dx - 1)
                klineout = const_name(i) & ", " & Const_val(i) & ", " & standalone_err(el_dx, k) & ", " & standalone_covar(el_dx, k, 1)
                klineout = klineout & ", " & standalone_covar(el_dx, k, 2) & ", " & standalone_covar(el_dx, k, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i

            For i = 58 To 90
                klineout = const_name(i) & ", " & Const_val(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next i


        End If
    End Sub




    Public Sub SelectAnalysisConstantsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAnalysisConstantsToolStripMenuItem.Click


        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        '  Dim datafilename1 As String
        Dim str11, str12, str13, str14 As String
        Dim idex, jdex As Integer
        Dim outfilename, const_file_name1 As String


        outfilename = exe_dir_name & "\spec_fit_0.txt"

        openFileDialog1.InitialDirectory = sys_consts_dir_name
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    const_file_name1 = openFileDialog1.FileName
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
        If const_file_name1 = "" Then MsgBox("No data file selected")
        If const_file_name1 = "" Then Return

        'const_file_name = constantsfilenamebox1.Text

        Call get_system_constants(const_file_name1)

        Call display_constants(const_file_name1)

    End Sub

    Sub display_constants(const_file_name1)

        ' ***** Create the input file for XRF_FIT *****
        constantsfilenamebox1.Text = const_file_name1

        ConBox1.Text = Const_val(1)
        ConBox2.Text = Const_val(2)
        ConBox3.Text = Const_val(3)
        ConBox4.Text = Const_val(4)
        ConBox5.Text = Const_val(5)
        ConBox6.Text = Const_val(6)
        ConBox7.Text = Const_val(7)
        ConBox8.Text = Const_val(8)
        ConBox9.Text = Const_val(9)
        ConBox10.Text = Const_val(10)
        ConBox11.Text = Const_val(11)
        ConBox12.Text = Const_val(12)
        ConBox13.Text = Const_val(13)
        ConBox14.Text = Const_val(14)
        ConBox15.Text = Const_val(15)
        ConBox16.Text = Const_val(16)
        ConBox17.Text = Const_val(17)
        ConBox18.Text = Const_val(18)
        ConBox19.Text = Const_val(19)
        ConBox20.Text = Const_val(20)
        ConBox21.Text = Const_val(21)
        ConBox22.Text = Const_val(22)
        ConBox23.Text = Const_val(23)
        ConBox24.Text = Const_val(24)
        ConBox25.Text = Const_val(25)
        ConBox26.Text = Const_val(26)
        ConBox27.Text = Const_val(27)
        ConBox28.Text = Const_val(28)
        ConBox29.Text = Const_val(29)
        ConBox30.Text = Const_val(30)
        ConBox31.Text = Const_val(31)
        ConBox32.Text = Const_val(32)
        ConBox33.Text = Const_val(33)
        ConBox34.Text = Const_val(34)
        ConBox35.Text = Const_val(35)
        ConBox36.Text = Const_val(36)
        ConBox37.Text = Const_val(37)
        ConBox38.Text = Const_val(38)
        ConBox39.Text = Const_val(39)
        ConBox40.Text = Const_val(40)


        Standalone_1_1.Text = Const_val(43)
        Standalone_1_2.Text = Const_val(44)
        Standalone_1_3.Text = Const_val(45)
        Standalone_2_1.Text = Const_val(46)
        Standalone_2_2.Text = Const_val(47)
        Standalone_2_3.Text = Const_val(48)
        Standalone_2_1.Text = Const_val(46)
        Standalone_2_2.Text = Const_val(47)
        Standalone_2_3.Text = Const_val(48)
        Standalone_3_1.Text = Const_val(49)
        Standalone_3_2.Text = Const_val(50)
        Standalone_3_3.Text = Const_val(51)
        Standalone_4_1.Text = Const_val(52)
        Standalone_4_2.Text = Const_val(53)
        Standalone_4_3.Text = Const_val(54)
        Standalone_5_1.Text = Const_val(55)
        Standalone_5_2.Text = Const_val(56)
        Standalone_5_3.Text = Const_val(57)



        Stand_err_1_1.Text = standalone_err(1, 1)
        Stand_err_1_2.Text = standalone_err(1, 2)
        Stand_err_1_3.Text = standalone_err(1, 3)
        Stand_err_2_1.Text = standalone_err(2, 1)
        Stand_err_2_2.Text = standalone_err(2, 2)
        Stand_err_2_3.Text = standalone_err(2, 3)
        Stand_err_3_1.Text = standalone_err(3, 1)
        Stand_err_3_2.Text = standalone_err(3, 2)
        Stand_err_3_3.Text = standalone_err(3, 3)
        Stand_err_4_1.Text = standalone_err(4, 1)
        Stand_err_4_2.Text = standalone_err(4, 2)
        Stand_err_4_3.Text = standalone_err(4, 3)
        Stand_err_5_1.Text = standalone_err(5, 1)
        Stand_err_5_2.Text = standalone_err(5, 2)
        Stand_err_5_3.Text = standalone_err(5, 3)


        Stand_covar_1_1_1.Text = standalone_covar(1, 1, 1)
        Stand_covar_1_1_2.Text = standalone_covar(1, 1, 2)
        Stand_covar_1_1_3.Text = standalone_covar(1, 1, 3)
        Stand_covar_1_2_1.Text = standalone_covar(1, 2, 1)
        Stand_covar_1_2_2.Text = standalone_covar(1, 2, 2)
        Stand_covar_1_2_3.Text = standalone_covar(1, 2, 3)
        Stand_covar_1_3_1.Text = standalone_covar(1, 3, 1)
        Stand_covar_1_3_2.Text = standalone_covar(1, 3, 2)
        Stand_covar_1_3_3.Text = standalone_covar(1, 3, 3)
        '
        Stand_covar_2_1_1.Text = standalone_covar(2, 1, 1)
        Stand_covar_2_1_2.Text = standalone_covar(2, 1, 2)
        Stand_covar_2_1_3.Text = standalone_covar(2, 1, 3)
        Stand_covar_2_2_1.Text = standalone_covar(2, 2, 1)
        Stand_covar_2_2_2.Text = standalone_covar(2, 2, 2)
        Stand_covar_2_2_3.Text = standalone_covar(2, 2, 3)
        Stand_covar_2_3_1.Text = standalone_covar(2, 3, 1)
        Stand_covar_2_3_2.Text = standalone_covar(2, 3, 2)
        Stand_covar_2_3_3.Text = standalone_covar(2, 3, 3)

        Stand_covar_3_1_1.Text = standalone_covar(3, 1, 1)
        Stand_covar_3_1_2.Text = standalone_covar(3, 1, 2)
        Stand_covar_3_1_3.Text = standalone_covar(3, 1, 3)
        Stand_covar_3_2_1.Text = standalone_covar(3, 2, 1)
        Stand_covar_3_2_2.Text = standalone_covar(3, 2, 2)
        Stand_covar_3_2_3.Text = standalone_covar(3, 2, 3)
        Stand_covar_3_3_1.Text = standalone_covar(3, 3, 1)
        Stand_covar_3_3_2.Text = standalone_covar(3, 3, 2)
        Stand_covar_3_3_3.Text = standalone_covar(3, 3, 3)

        Stand_covar_4_1_1.Text = standalone_covar(4, 1, 1)
        Stand_covar_4_1_2.Text = standalone_covar(4, 1, 2)
        Stand_covar_4_1_3.Text = standalone_covar(4, 1, 3)
        Stand_covar_4_2_1.Text = standalone_covar(4, 2, 1)
        Stand_covar_4_2_2.Text = standalone_covar(4, 2, 2)
        Stand_covar_4_2_3.Text = standalone_covar(4, 2, 3)
        Stand_covar_4_3_1.Text = standalone_covar(4, 3, 1)
        Stand_covar_4_3_2.Text = standalone_covar(4, 3, 2)
        Stand_covar_4_3_3.Text = standalone_covar(4, 3, 3)

        Stand_covar_5_1_1.Text = standalone_covar(5, 1, 1)
        Stand_covar_5_1_2.Text = standalone_covar(5, 1, 2)
        Stand_covar_5_1_3.Text = standalone_covar(5, 1, 3)
        Stand_covar_5_2_1.Text = standalone_covar(5, 2, 1)
        Stand_covar_5_2_2.Text = standalone_covar(5, 2, 2)
        Stand_covar_5_2_3.Text = standalone_covar(5, 2, 3)
        Stand_covar_5_3_1.Text = standalone_covar(5, 3, 1)
        Stand_covar_5_3_2.Text = standalone_covar(5, 3, 2)
        Stand_covar_5_3_3.Text = standalone_covar(5, 3, 3)

        EFF_par_Box1.Text = Const_val(61)
        EFF_par_Box2.Text = Const_val(62)
        EFF_par_Box3.Text = Const_val(63)
        EFF_par_Box4.Text = Const_val(64)
        EFF_par_Box5.Text = Const_val(65)
        EFF_par_Box6.Text = Const_val(66)
        EFF_par_Box7.Text = Const_val(67)

        fp_bias_box.Text = Const_val(71)
        fp_bias_err_box.Text = Const_val(72)

        empirical_corr_par_box.Text = Const_val(75)
        empirical_corr_par_err_box.Text = Const_val(76)
        empirical_corr_E1_box.Text = Const_val(77)
        empirical_corr_E2_box.Text = Const_val(78)

    End Sub

    Private Sub datafilenamebox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub constantsfilenamebox1_TextChanged(sender As Object, e As EventArgs) Handles constantsfilenamebox1.TextChanged

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub



    Private Sub ConBox36_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ConBox36.MaskInputRejected

    End Sub

    Private Sub ConBox24_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ConBox24.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

    End Sub

    Private Sub ConBox2_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ConBox2.MaskInputRejected



    End Sub

    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged

    End Sub
End Class