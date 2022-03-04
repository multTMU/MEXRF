Imports System.IO
Imports System.Text
Public Class VMS_Setup

    Public vms_char_names(36) As String
    Public const_file_name1 As String
    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim const_file_name As String

        const_file_name = xrf_vms_consts_dir & "XRF_VMS_CONSTANTS.csv"

        Call get_VMS_constants()


        ' vms_R_UPu, vms_R_UPu_err, vms_Pu_bkg_wt, vms_Pu_bkg_wt_err

        TextBox1.Text = e_cal_par(1)
        TextBox2.Text = e_cal_par(2)
        TextBox3.Text = e_cal_par(3)
        TextBox4.Text = e_cal_par(4)

        TextBox8.Text = K_fit_rois(1, 1)
        TextBox7.Text = K_fit_rois(3, 1)
        TextBox6.Text = K_fit_rois(4, 1)

        TextBox22.Text = K_fit_rois(1, 6)
        TextBox21.Text = K_fit_rois(3, 6)
        TextBox20.Text = K_fit_rois(4, 6)

        TextBox25.Text = K_fit_rois(1, 7)
        TextBox24.Text = K_fit_rois(3, 7)
        TextBox23.Text = K_fit_rois(4, 7)

        B1_E_box.Text = X_fit_rois(1, 1)
        B1_W_box.Text = X_fit_rois(1, 2)
        B2_E_box.Text = X_fit_rois(2, 1)
        B2_W_box.Text = X_fit_rois(2, 2)
        B3_E_box.Text = X_fit_rois(3, 1)
        B3_W_box.Text = X_fit_rois(3, 2)
        B4_E_box.Text = X_fit_rois(4, 1)
        B4_W_box.Text = X_fit_rois(4, 2)
        B5_E_box.Text = X_fit_rois(5, 1)
        B5_W_box.Text = X_fit_rois(5, 2)


        P1_E_box.Text = X_fit_rois(6, 1)
        P1_W_box.Text = X_fit_rois(6, 2)
        P2_E_box.Text = X_fit_rois(7, 1)
        P2_W_box.Text = X_fit_rois(7, 2)
        P3_E_box.Text = X_fit_rois(8, 1)
        P3_W_box.Text = X_fit_rois(8, 2)
        P4_E_box.Text = X_fit_rois(9, 1)
        P4_W_box.Text = X_fit_rois(9, 2)
        P5L_E_box.Text = X_fit_rois(10, 1)
        P5L_W_box.Text = X_fit_rois(10, 2)
        P5R_E_box.Text = X_fit_rois(10, 3)
        P5R_W_box.Text = X_fit_rois(10, 4)


        uk2_L_BK_E_box.Text = X_fit_rois(6, 3)
        uk2_L_BK_channels_box.Text = X_fit_rois(6, 4)
        uk2_U_BK_E_box.Text = X_fit_rois(6, 5)
        uk2_U_BK_channels_box.Text = X_fit_rois(6, 6)

        uk1_L_BK_E_box.Text = X_fit_rois(7, 3)
        uk1_L_BK_channels_box.Text = X_fit_rois(7, 4)
        uk1_U_BK_E_box.Text = X_fit_rois(7, 5)
        uk1_U_BK_channels_box.Text = X_fit_rois(7, 6)

        TextBox26.Text = bkg_roi(1)
        TextBox27.Text = bkg_roi(2)
        TextBox28.Text = bkg_roi(3)
        TextBox29.Text = bkg_roi(4)

        ref_current_box.Text = vms_ref_current
        sample_current_box.Text = vms_sample_current
        vms_ref_file_box.Text = vms_ref_file_name

        Cal_R_UPu_Box.Text = vms_R_UPu_cal
        Pu_Bkg_wt_Box.Text = vms_Pu_bkg_wt
        Am_Bkg_wt_Box.text = vms_am_bkg_wt



    End Sub
    Public Sub get_VMS_constants1(NEW_VMS_FILE As String)
        Dim vms_const_file_name1, str11 As String
        Dim vms_char_names(36) As String
        Dim j, idex, jdex As Integer


        vms_const_file_name1 = NEW_VMS_FILE     ' xrf_vms_consts_dir & "XRF_VMS_CONSTANTS.csv"


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

        idex = 0
        Dim in_str(10), filname As String
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(vms_const_file_name1)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    idex = idex + 1
                    jdex = 0
                    Dim currentField As String
                    For Each currentField In currentRow
                        str11 = currentField
                        jdex = jdex + 1
                        in_str(jdex) = str11

                    Next

                    If idex = 1 Then filname = in_str(2)
                    If vms_char_names(2) = in_str(1) Then e_cal_par(1) = Val(in_str(2))
                    If vms_char_names(3) = in_str(1) Then e_cal_par(2) = Val(in_str(2))
                    If vms_char_names(4) = in_str(1) Then e_cal_par(3) = Val(in_str(2))
                    If vms_char_names(5) = in_str(1) Then e_cal_par(4) = Val(in_str(2))
                    If vms_char_names(21) = in_str(1) Then vms_ref_file_name = in_str(2)
                    If vms_char_names(22) = in_str(1) Then vms_ref_current = Val(in_str(2))
                    If vms_char_names(23) = in_str(1) Then vms_sample_current = Val(in_str(2))
                    If vms_char_names(31) = in_str(1) Then vms_R_UPu_cal = Val(in_str(2))
                    If vms_char_names(32) = in_str(1) Then vms_Pu_bkg_wt = Val(in_str(2))
                    If vms_char_names(33) = in_str(1) Then vms_am_bkg_wt = Val(in_str(2))

                    If idex > 5 And idex < 11 Then

                        For j = 1 To 7
                            K_fit_rois(idex - 5, j) = Val(in_str(j + 1))
                        Next j
                    End If


                    If idex > 10 And idex < 21 Then

                        For j = 1 To 7
                            X_fit_rois(idex - 10, j) = Val(in_str(j + 1))
                        Next j
                    End If

                    If idex = 24 Then
                        For j = 1 To 4
                            bkg_roi(j) = Val(in_str(j + 1))
                        Next
                    End If

                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using



    End Sub

    Function split_string(in_str, idex, vms_char_names)
        Dim j As Integer
        split_string = ""
        For j = 1 To 6
            ' split_string = in_str(1) Then K_fit_rois(idex - 5, j) = Val(in_str(j + 1))
        Next j
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '
        ' write new XRF VMS constants file
        '
        Dim const_file_name As String
        const_file_name = xrf_vms_consts_dir & "XRF_VMS_CONSTANTS.csv"


        e_cal_par(1) = TextBox1.Text
        e_cal_par(2) = TextBox2.Text
        e_cal_par(3) = TextBox3.Text
        e_cal_par(4) = TextBox4.Text

        K_fit_rois(1, 1) = TextBox8.Text
        K_fit_rois(3, 1) = TextBox7.Text
        K_fit_rois(4, 1) = TextBox6.Text

        K_fit_rois(1, 6) = TextBox22.Text
        K_fit_rois(3, 6) = TextBox21.Text
        K_fit_rois(4, 6) = TextBox20.Text

        K_fit_rois(1, 7) = TextBox25.Text
        K_fit_rois(3, 7) = TextBox24.Text
        K_fit_rois(4, 7) = TextBox23.Text

        X_fit_rois(1, 1) = B1_E_box.Text
        X_fit_rois(1, 2) = B1_W_box.Text
        X_fit_rois(2, 1) = B2_E_box.Text
        X_fit_rois(2, 2) = B2_W_box.Text
        X_fit_rois(3, 1) = B3_E_box.Text
        X_fit_rois(3, 2) = B3_W_box.Text
        X_fit_rois(4, 1) = B4_E_box.Text
        X_fit_rois(4, 2) = B4_W_box.Text
        X_fit_rois(5, 1) = B5_E_box.Text
        X_fit_rois(5, 2) = B5_W_box.Text

        X_fit_rois(6, 1) = P1_E_box.Text
        X_fit_rois(6, 2) = P1_W_box.Text
        X_fit_rois(7, 1) = P2_E_box.Text
        X_fit_rois(7, 2) = P2_W_box.Text
        X_fit_rois(8, 1) = P3_E_box.Text
        X_fit_rois(8, 2) = P3_W_box.Text
        X_fit_rois(9, 1) = P4_E_box.Text
        X_fit_rois(9, 2) = P4_W_box.Text
        X_fit_rois(10, 1) = P5L_E_box.Text
        X_fit_rois(10, 2) = P5L_W_box.Text
        X_fit_rois(10, 3) = P5R_E_box.Text
        X_fit_rois(10, 4) = P5R_W_box.Text


        X_fit_rois(6, 3) = uk2_L_BK_E_box.Text
        X_fit_rois(6, 4) = uk2_L_BK_channels_box.Text
        X_fit_rois(6, 5) = uk2_U_BK_E_box.Text
        X_fit_rois(6, 6) = uk2_U_BK_channels_box.Text

        X_fit_rois(7, 3) = uk1_L_BK_E_box.Text
        X_fit_rois(7, 4) = uk1_L_BK_channels_box.Text
        X_fit_rois(7, 5) = uk1_U_BK_E_box.Text
        X_fit_rois(7, 6) = uk1_U_BK_channels_box.Text

        vms_ref_current = ref_current_box.Text
        vms_sample_current = sample_current_box.Text
        vms_ref_file_name = vms_ref_file_box.Text

        vms_R_UPu_cal = Cal_R_UPu_Box.Text
        vms_Pu_bkg_wt = Pu_Bkg_wt_Box.Text
        vms_am_bkg_wt = Am_Bkg_wt_Box.Text


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


        Close()
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
        vms_ref_file_box.Text = vms_ref_file_name

    End Sub

    Private Sub vms_ref_file_box_TextChanged(sender As Object, e As EventArgs) Handles vms_ref_file_box.TextChanged

    End Sub

    Private Sub SelectVMSParameterFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectVMSParameterFileToolStripMenuItem.Click

        ' read in specific VMS data file
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        Dim const_file_name As String

        openFileDialog1.InitialDirectory = xrf_vms_consts_dir
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    const_file_name = openFileDialog1.FileName

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
        If const_file_name = "" Then MsgBox("No data file selected")
        If const_file_name = "" Then Return

        Call get_VMS_constants1(const_file_name)

        TextBox1.Text = e_cal_par(1)
        TextBox2.Text = e_cal_par(2)
        TextBox3.Text = e_cal_par(3)
        TextBox4.Text = e_cal_par(4)

        TextBox8.Text = K_fit_rois(1, 1)
        TextBox7.Text = K_fit_rois(3, 1)
        TextBox6.Text = K_fit_rois(4, 1)

        TextBox22.Text = K_fit_rois(1, 6)
        TextBox21.Text = K_fit_rois(3, 6)
        TextBox20.Text = K_fit_rois(4, 6)

        TextBox25.Text = K_fit_rois(1, 7)
        TextBox24.Text = K_fit_rois(3, 7)
        TextBox23.Text = K_fit_rois(4, 7)

        B1_E_box.Text = X_fit_rois(1, 1)
        B1_W_box.Text = X_fit_rois(1, 2)
        B2_E_box.Text = X_fit_rois(2, 1)
        B2_W_box.Text = X_fit_rois(2, 2)
        B3_E_box.Text = X_fit_rois(3, 1)
        B3_W_box.Text = X_fit_rois(3, 2)
        B4_E_box.Text = X_fit_rois(4, 1)
        B4_W_box.Text = X_fit_rois(4, 2)
        B5_E_box.Text = X_fit_rois(5, 1)
        B5_W_box.Text = X_fit_rois(5, 2)


        P1_E_box.Text = X_fit_rois(6, 1)
        P1_W_box.Text = X_fit_rois(6, 2)
        P2_E_box.Text = X_fit_rois(7, 1)
        P2_W_box.Text = X_fit_rois(7, 2)
        P3_E_box.Text = X_fit_rois(8, 1)
        P3_W_box.Text = X_fit_rois(8, 2)
        P4_E_box.Text = X_fit_rois(9, 1)
        P4_W_box.Text = X_fit_rois(9, 2)
        P5L_E_box.Text = X_fit_rois(10, 1)
        P5L_W_box.Text = X_fit_rois(10, 2)
        P5R_E_box.Text = X_fit_rois(10, 3)
        P5R_W_box.Text = X_fit_rois(10, 4)


        uk2_L_BK_E_box.Text = X_fit_rois(6, 3)
        uk2_L_BK_channels_box.Text = X_fit_rois(6, 4)
        uk2_U_BK_E_box.Text = X_fit_rois(6, 5)
        uk2_U_BK_channels_box.Text = X_fit_rois(6, 6)

        uk1_L_BK_E_box.Text = X_fit_rois(7, 3)
        uk1_L_BK_channels_box.Text = X_fit_rois(7, 4)
        uk1_U_BK_E_box.Text = X_fit_rois(7, 5)
        uk1_U_BK_channels_box.Text = X_fit_rois(7, 6)

        TextBox26.Text = bkg_roi(1)
        TextBox27.Text = bkg_roi(2)
        TextBox28.Text = bkg_roi(3)
        TextBox29.Text = bkg_roi(4)

        ref_current_box.Text = vms_ref_current
        sample_current_box.Text = vms_sample_current
        vms_ref_file_box.Text = vms_ref_file_name

        Cal_R_UPu_Box.Text = vms_R_UPu_cal
        Pu_Bkg_wt_Box.Text = vms_Pu_bkg_wt
        Am_Bkg_wt_Box.Text = vms_am_bkg_wt
    End Sub

    Private Sub SaveNewVMSParameterFIleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveNewVMSParameterFIleToolStripMenuItem.Click

        '  Saves custom VMS parameter file

        Dim vms_file_name As String

        SaveFileDialog1.Filter = "TXT Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
            Then
            My.Computer.FileSystem.WriteAllText _
                (SaveFileDialog1.FileName, "", True)

            vms_file_name = SaveFileDialog1.FileName
        End If

        If vms_file_name = "" Then Return

        Call save_vms_file(vms_file_name)
    End Sub


    Public Sub save_vms_file(vms_file_name)
        '
        ' write new XRF VMS constants file
        '
        Dim const_file_name As String
        const_file_name = vms_file_name


        e_cal_par(1) = TextBox1.Text
        e_cal_par(2) = TextBox2.Text
        e_cal_par(3) = TextBox3.Text
        e_cal_par(4) = TextBox4.Text

        K_fit_rois(1, 1) = TextBox8.Text
        K_fit_rois(3, 1) = TextBox7.Text
        K_fit_rois(4, 1) = TextBox6.Text

        K_fit_rois(1, 6) = TextBox22.Text
        K_fit_rois(3, 6) = TextBox21.Text
        K_fit_rois(4, 6) = TextBox20.Text

        K_fit_rois(1, 7) = TextBox25.Text
        K_fit_rois(3, 7) = TextBox24.Text
        K_fit_rois(4, 7) = TextBox23.Text

        X_fit_rois(1, 1) = B1_E_box.Text
        X_fit_rois(1, 2) = B1_W_box.Text
        X_fit_rois(2, 1) = B2_E_box.Text
        X_fit_rois(2, 2) = B2_W_box.Text
        X_fit_rois(3, 1) = B3_E_box.Text
        X_fit_rois(3, 2) = B3_W_box.Text
        X_fit_rois(4, 1) = B4_E_box.Text
        X_fit_rois(4, 2) = B4_W_box.Text
        X_fit_rois(5, 1) = B5_E_box.Text
        X_fit_rois(5, 2) = B5_W_box.Text

        X_fit_rois(6, 1) = P1_E_box.Text
        X_fit_rois(6, 2) = P1_W_box.Text
        X_fit_rois(7, 1) = P2_E_box.Text
        X_fit_rois(7, 2) = P2_W_box.Text
        X_fit_rois(8, 1) = P3_E_box.Text
        X_fit_rois(8, 2) = P3_W_box.Text
        X_fit_rois(9, 1) = P4_E_box.Text
        X_fit_rois(9, 2) = P4_W_box.Text
        X_fit_rois(10, 1) = P5L_E_box.Text
        X_fit_rois(10, 2) = P5L_W_box.Text
        X_fit_rois(10, 3) = P5R_E_box.Text
        X_fit_rois(10, 4) = P5R_W_box.Text


        X_fit_rois(6, 3) = uk2_L_BK_E_box.Text
        X_fit_rois(6, 4) = uk2_L_BK_channels_box.Text
        X_fit_rois(6, 5) = uk2_U_BK_E_box.Text
        X_fit_rois(6, 6) = uk2_U_BK_channels_box.Text

        X_fit_rois(7, 3) = uk1_L_BK_E_box.Text
        X_fit_rois(7, 4) = uk1_L_BK_channels_box.Text
        X_fit_rois(7, 5) = uk1_U_BK_E_box.Text
        X_fit_rois(7, 6) = uk1_U_BK_channels_box.Text

        vms_ref_current = ref_current_box.Text
        vms_sample_current = sample_current_box.Text
        vms_ref_file_name = vms_ref_file_box.Text

        vms_R_UPu_cal = Cal_R_UPu_Box.Text
        vms_Pu_bkg_wt = Pu_Bkg_wt_Box.Text
        vms_am_bkg_wt = Am_Bkg_wt_Box.Text


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


        Close()

    End Sub


End Class