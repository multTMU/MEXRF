Imports System.IO
Imports System.Text
Imports System.Math
Public Class Fission_Products
    Public vms_char_names(36) As String
    Private Sub Fission_Products_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButton1.Checked = True
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False
        RadioButton5.Checked = False
        fiss_prod_corr_type = 1

        Call get_system_constants(const_file_name)
        Call get_VMS_constants()

        fp_bias_box.Text = Const_val(71)
        fp_bias_err_box.Text = Const_val(72)

        empirical_corr_par_box.Text = Const_val(75)
        empirical_corr_par_err_box.Text = Const_val(76)
        empirical_corr_E1_box.Text = Const_val(77)
        empirical_corr_E2_box.Text = Const_val(78)

        passive_file_name_box.Text = vms_ref_file_name
        passive_summary_name_box.Text = passive_summary_file_name

        If passive_summary_file_name <> "" Then
            U_passive_rate_box.Text = pass_sum_rates(1)
            U_passive_rate_err_box.Text = pass_sum_rates_err(1)
            Pu_passive_rate_box.Text = pass_sum_rates(2)
            Pu_passive_rate_err_box.Text = pass_sum_rates_err(2)
            Np_passive_rate_box.Text = pass_sum_rates(3)
            Np_passive_rate_err_box.Text = pass_sum_rates_err(3)
            Cm_passive_rate_box.Text = pass_sum_rates(4)
            Cm_passive_rate_err_box.Text = pass_sum_rates_err(4)
            Am_passive_rate_box.Text = pass_sum_rates(5)
            Am_passive_rate_err_box.Text = pass_sum_rates_err(5)
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

        fiss_prod_corr_type = 1
        If RadioButton2.Checked = False Then Const_val(35) = 0
        Call write_system_constants_file()
    End Sub



    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        ' changes single value in the constants text file used by the fortran executable.
        Call get_system_constants(const_file_name)
        Call get_VMS_constants()
        fiss_prod_corr_type = 2
        If RadioButton2.Checked = True Then Const_val(35) = 1
        If RadioButton2.Checked = False Then Const_val(35) = 0

        vms_ref_file_name = passive_file_name_box.Text

        Call write_system_constants_file()
        If vms_ref_file_name <> "" Then Call update_vms_constants()
        Return
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        fiss_prod_corr_type = 3
        If RadioButton2.Checked = False Then Const_val(35) = 0
        Call write_system_constants_file()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        fiss_prod_corr_type = 4
        If RadioButton2.Checked = False Then Const_val(35) = 0
        Call write_system_constants_file()
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        fiss_prod_corr_type = 5

        If RadioButton2.Checked = False Then Const_val(35) = 0
        Call write_system_constants_file()
    End Sub


    Sub write_system_constants_file()
        '
        ' write new xrf fitting constants file for use with fortran executable
        Dim outconstants As String
        Dim klineout As String
        Dim i, j, k As Integer


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


        '   ----------------------------------------------------------------------------------

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()


        openFileDialog1.InitialDirectory = xrf_vms_consts_dir       ' "c:\MEXRF\XRF_files\"
        openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    vms_ref_file_name = openFileDialog1.FileName

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
        passive_file_name_box.Text = vms_ref_file_name
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'select xrf passive summary file
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()


        openFileDialog1.InitialDirectory = summaryfiledir       ' "c:\MEXRF\summary_files\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    passive_summary_file_name = openFileDialog1.FileName

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
        passive_summary_name_box.Text = passive_summary_file_name

        Call get_MEXRF_summary(passive_summary_file_name)

        U_passive_rate_box.Text = pass_sum_rates(1)
        U_passive_rate_err_box.Text = pass_sum_rates_err(1)
        Pu_passive_rate_box.Text = pass_sum_rates(2)
        Pu_passive_rate_err_box.Text = pass_sum_rates_err(2)
        Np_passive_rate_box.Text = pass_sum_rates(3)
        Np_passive_rate_err_box.Text = pass_sum_rates_err(3)
        Cm_passive_rate_box.Text = pass_sum_rates(4)
        Cm_passive_rate_err_box.Text = pass_sum_rates_err(4)
        Am_passive_rate_box.Text = pass_sum_rates(5)
        Am_passive_rate_err_box.Text = pass_sum_rates_err(5)


    End Sub

    Sub get_MEXRF_summary(passive_summary_file_name As String)

        Dim jdex, idex, r_num As Integer
        Dim wt_avg_flag As Boolean
        Dim str11, str12, str13(80) As String

        wt_avg_flag = False
        Using MyReader As New Microsoft.VisualBasic.
                   FileIO.TextFieldParser(passive_summary_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(" ")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String

                    If wt_avg_flag Then GoTo 50

                    jdex = 0
                    idex = -1
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If Strings.Left(str11, 5) = "WtAvg" Then wt_avg_flag = True
                        If wt_avg_flag <> True Then GoTo 45
                        If Strings.Left(str11, 1) = "" Then GoTo 45
                        If Strings.Left(str11, 1) = " " Then GoTo 45
                        If Strings.Left(str11, 1) = "±" Then GoTo 45
                        idex = idex + 1

                        If idex = 1 Then pass_sum_rates(1) = Val(str11)
                        If idex = 2 Then pass_sum_rates_err(1) = Val(str11)
                        If idex = 3 Then pass_sum_rates(2) = Val(str11)
                        If idex = 4 Then pass_sum_rates_err(2) = Val(str11)
                        If idex = 5 Then pass_sum_rates(3) = Val(str11)
                        If idex = 6 Then pass_sum_rates_err(3) = Val(str11)
                        If idex = 7 Then pass_sum_rates(4) = Val(str11)
                        If idex = 8 Then pass_sum_rates_err(4) = Val(str11)
                        If idex = 9 Then pass_sum_rates(5) = Val(str11)
                        If idex = 10 Then pass_sum_rates_err(5) = Val(str11)

                        str12 = str12 & str11 & " "
45:                 Next


50:         ' start read concentrations


                    str12 = ""

60:
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using





    End Sub

    Sub update_vms_constants()
        '
        ' write new XRF VMS constants file
        '
        Dim const_file_name As String
        const_file_name = xrf_vms_consts_dir & "XRF_VMS_CONSTANTS.csv"


        Dim klineout As String
        vms_char_names(1) = "file name"
        vms_char_names(2) = "E_cal_L_chan"
        vms_char_names(3) = "E_cal_L_energy"
        vms_char_names(4) = "E_cal_H_chan"
        vms_char_names(5) = "E_cal_H_energy"
        vms_char_names(6) = "U"
        vms_char_names(7) = "Np"
        vms_char_names(8) = "Pu"
        vms_char_names(9) = "Am"
        vms_char_names(10) = "Cm"

        vms_char_names(11) = "B1"
        vms_char_names(12) = "B2"
        vms_char_names(13) = "B3"
        vms_char_names(14) = "B4"
        vms_char_names(15) = "B5"
        vms_char_names(16) = "P1"
        vms_char_names(17) = "P2"
        vms_char_names(18) = "P3"
        vms_char_names(19) = "P4"
        vms_char_names(20) = "P5"

        vms_char_names(21) = "Ref_file"
        vms_char_names(22) = "ref_current"
        vms_char_names(23) = "Sample_current"
        vms_char_names(24) = "Bkg_ROI"
        vms_char_names(25) = "XX"
        vms_char_names(26) = "delta_mu_u"
        vms_char_names(27) = "delta_mu_np"
        vms_char_names(28) = "delta_mu_pu"
        vms_char_names(29) = "delta_mu_am"
        vms_char_names(30) = "delta_mu_cm"
        vms_char_names(31) = "R_UPu_0"
        vms_char_names(32) = "Pu_bkg_wgt"
        vms_char_names(33) = "Am_bkg_wgt"

        'overwrite contstants file for fitting routine

        klineout = vms_char_names(1) & ", " & const_file_name & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf

        Dim fs As FileStream = File.Create(const_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 2 To 5
            klineout = vms_char_names(i) & ", " & e_cal_par(i - 1) & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
            My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)

        Next i
        For i = 6 To 10
            klineout = vms_char_names(i)
            For j = 1 To 7
                klineout = klineout & ", " & K_fit_rois(i - 5, j)
            Next j
            klineout = klineout & vbCrLf
            My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        Next i

        For i = 1 To 10
            klineout = vms_char_names(i + 10)
            For j = 1 To 7
                klineout = klineout & ", " & X_fit_rois(i, j)
            Next j
            klineout = klineout & vbCrLf
            My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        Next i

        klineout = vms_char_names(21) & ", " & vms_ref_file_name & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        klineout = vms_char_names(22) & ", " & vms_ref_current & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        klineout = vms_char_names(23) & ", " & vms_sample_current & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)

        klineout = vms_char_names(24) & ", " & bkg_roi(1) & ", " & bkg_roi(2) & ", " & bkg_roi(3) & ", " & bkg_roi(4) & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        klineout = vms_char_names(25) & ", " & 0 & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)

        klineout = vms_char_names(31) & ", " & vms_R_UPu_cal & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        klineout = vms_char_names(32) & ", " & vms_Pu_bkg_wt & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)
        klineout = vms_char_names(33) & ", " & vms_am_bkg_wt & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & ", 0 " & vbCrLf
        My.Computer.FileSystem.WriteAllText(const_file_name, klineout, True)

    End Sub

    Private Sub passive_file_name_box_TextChanged(sender As Object, e As EventArgs) Handles passive_file_name_box.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class