Imports System.Math
Imports System.IO
Imports System.Text


Imports System.Runtime.InteropServices
Module Module2


    Sub get_MEKED_summary(seed_name As String)
        '
        '  Read in summary MEKED data file (text format) 
        '   Attempt to assign values to individual XRF input files
        '   if Apply_Avg_MEKED flag is true apply average MEKED result to all XRF analyses
        '
        '
        Dim openFileDialog1 As New OpenFileDialog()
        Dim myStream As Stream = Nothing
        Dim meked_sum_par(36, 99)
        Dim str11, meked_root_files(99), in_str(120) As String
        Dim temp_str, temp_str2, temp_str3, temp_str4, temp_str5, str12 As String
        Dim idex, jdex, r_num, file_num, ele_index, meked_element_index As Integer
        Dim u_flag, totals_flag, meked_flag, ele_flag(6) As Boolean
        Dim str_len1, str_len2

        u_flag = False
        totals_flag = False
        meked_flag = False
        meked_element_index = -1
        For i = 1 To 36
            meked_summary(i) = 0
            meked_summary_err(i) = 0
            meked_systematic_err(i) = 0
            meked_counting_err(i) = 0
            meked_other_err(i) = 0
            For j = 1 To 99
                meked_sum_par(i, j) = 0
            Next j
        Next i

        For i = 1 To 6
            ele_flag(i) = False
            KED_elements(i) = ""
        Next i


        meked_analyis_date = ""
        meked_summary_name = ""
        If seed_name <> "" Then meked_summary_name = seed_name

        '  meked_summary_name = "\\MEKED\summary_files\sum_1.txt"

        If seed_name <> "" Then GoTo start_read

        openFileDialog1.InitialDirectory = root_dir_base_name & "MEKED\summary_files\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True



        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    meked_summary_name = openFileDialog1.FileName
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
        If meked_summary_name = "" Then MsgBox("No data file selected")
        If meked_summary_name = "" Then Return

start_read:

        r_num = 0
        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(meked_summary_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(" ")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String

                    If u_flag Then GoTo 50

                    jdex = 0
                    idex = idex + 1
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        '  Strings.Left(root_dir_base_name, 2) = "C:' or"D:" or ...
                        If Strings.Left(str11, 2) = Strings.Left(root_dir_base_name, 2) Then r_num = r_num + 1
                        If Strings.Left(str11, 2) = Strings.Left(root_dir_base_name, 2) Then meked_root_files(r_num) = str11

                        If str11 = "Uncorrected" Then u_flag = True
                        str12 = str12 & str11 & " "
                    Next


50:         ' start read concentrations
                    jdex = 0
                    idex = idex + 1

                    For Each currentField In currentRow
                        str11 = currentField
                        jdex = jdex + 1
                        in_str(jdex) = str11
                    Next

                    temp_str = ""
                    For k = 4 To 16
                        temp_str = temp_str & in_str(k)                           ' grab element concentration value
                    Next


                    temp_str3 = ""
                    For k = 1 To 8
                        temp_str3 = temp_str3 & in_str(k)
                    Next
                    If Strings.Left(Strings.Trim(temp_str3), 5) = "Total" Then totals_flag = True

                    temp_str4 = ""
                    temp_str5 = ""
                    For k = 15 To 18
                        temp_str4 = temp_str4 & in_str(k)
                        temp_str5 = temp_str5 & in_str(k + 4)
                    Next
                    temp_str4 = Strings.Trim(temp_str4)
                    temp_str5 = Strings.Trim(temp_str5)

                    temp_str = Strings.Trim(temp_str)
                    If Strings.Left(in_str(2), 3) = "Ele" Then meked_flag = True
                    If meked_flag Then meked_element_index = meked_element_index + 1
                    If meked_element_index > 0 And meked_element_index < 7 Then KED_elements(meked_element_index) = Strings.Left(in_str(2), 3)


                    ele_index = 1
                    If KED_elements(ele_index) = "[U]" And ele_flag(ele_index) = False Then

                        str_len1 = Strings.Len(str12)
                        str12 = Strings.Right(str12, str_len1 - 4)
                        str12 = Strings.Trim(str12)
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_systematic_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_counting_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_other_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        ele_flag(ele_index) = True

                    End If

                    '--------------------------
                    ele_index = 2
                    If KED_elements(ele_index) = "[Pu" And ele_flag(ele_index) = False Then

                        str_len1 = Strings.Len(str12)
                        str12 = Strings.Right(str12, str_len1 - 5)
                        str12 = Strings.Trim(str12)
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_systematic_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_counting_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_other_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        ele_flag(ele_index) = True

                    End If

                    '--------------------------
                    ele_index = 3
                    If KED_elements(ele_index) = "[Np" And ele_flag(ele_index) = False Then

                        str_len1 = Strings.Len(str12)
                        str12 = Strings.Right(str12, str_len1 - 5)
                        str12 = Strings.Trim(str12)
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_systematic_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_counting_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_other_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        ele_flag(ele_index) = True

                    End If
                    '--------------------------
                    ele_index = 4
                    If KED_elements(ele_index) = "[Am" And ele_flag(ele_index) = False Then

                        str_len1 = Strings.Len(str12)
                        str12 = Strings.Right(str12, str_len1 - 5)
                        str12 = Strings.Trim(str12)
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_systematic_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_counting_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_other_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        ele_flag(ele_index) = True

                    End If
                    '--------------------------
                    ele_index = 5
                    If KED_elements(ele_index) = "[Cm" And ele_flag(ele_index) = False Then

                        str_len1 = Strings.Len(str12)
                        str12 = Strings.Right(str12, str_len1 - 5)
                        str12 = Strings.Trim(str12)
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_systematic_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_counting_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_other_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        ele_flag(ele_index) = True

                    End If

                    '--------------------------
                    ele_index = 6
                    If KED_elements(ele_index) = "[Ma" And ele_flag(ele_index) = False Then

                        str_len1 = Strings.Len(str12)
                        str12 = Strings.Right(str12, str_len1 - 9)
                        str12 = Strings.Trim(str12)
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_summary_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_systematic_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_counting_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        str12 = Strings.Trim(Strings.Right(str12, str_len1 - str_len2 - 2))
                        str_len1 = Strings.Len(str12)
                        str_len2 = Strings.InStr(str12, " ")
                        meked_other_err(ele_index) = Val(Strings.Trim(Strings.Left(str12, str_len2 - 1)))

                        ele_flag(ele_index) = True

                    End If

                    '--------------------------

                    If Strings.Left(in_str(1), 6) = "HV_avg" And meked_summary(8) = 0 Then meked_summary(8) = Val(in_str(2))
                    If Strings.Left(in_str(1), 6) = "HV_avg" And meked_summary_err(8) = 0 Then meked_summary_err(8) = Val(in_str(4))

                    If Strings.Left(in_str(1), 6) = "HV_avg" And meked_summary(10) = 0 Then meked_summary(10) = Val(in_str(7))
                    If Strings.Left(in_str(1), 6) = "HV_avg" And meked_summary_err(10) = 0 Then meked_summary_err(10) = Val(in_str(9))

                    If Strings.Left(in_str(1), 3) = "avg" And meked_summary(9) = 0 And totals_flag Then meked_summary(9) = Val(temp_str4)
                    If Strings.Left(in_str(1), 3) = "avg" And meked_summary_err(9) = 0 And totals_flag Then meked_summary_err(9) = Val(temp_str5)

                    str12 = ""

60:
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using



    End Sub

    Sub get_root_filenames()
        ' creates a list of file names w/o prefixes or suffixes read in for fitting

        Dim temp_filename, st_pos As String

        Dim r_num, st_len As Integer

        If num_files = 0 Then Return

        For r_num = 1 To num_files
            temp_filename = MEXRF_main.ListBox1.Items(r_num - 1)

            st_pos = Strings.InStrRev(temp_filename, "\")
            st_len = Strings.Len(temp_filename)

            root_filenames(r_num) = Strings.Mid(temp_filename, st_pos + 1, st_len - 4 - st_pos)

        Next r_num


    End Sub


    Sub prelim_energy_cal(l_chan, h_chan, e_low, e_high, xdat)

        ' l_chan  :  stabilizer setting for lower peak (nominmal location)
        ' h_chan  :  stabilizer setting for upper peak (nominmal location)
        ' e_low   :  energy of lower peak
        ' e_high  :  energy of upper peak
        Dim ll_1, ll_2, lr_1, lr_2, l_max_chan, l_max_count, h_max_chan, h_max_count
        Dim de_init
        de_init = (e_high - e_low) / (h_chan - l_chan)

        ' lower peak location
        ll_1 = l_chan - 10
        ll_2 = l_chan + 10
        l_max_chan = ll_1
        l_max_count = xdat(11_1)

        For i = ll_1 To ll_2
            If xdat(i) > l_max_count Then l_max_chan = i
            If xdat(i) > l_max_count Then l_max_count = xdat(i)
        Next i

        ' upper peak location
        lr_1 = h_chan - 10
        lr_2 = h_chan + 10
        h_max_chan = lr_1
        h_max_count = xdat(11_1)

        For i = lr_1 To lr_2
            If xdat(i) > h_max_count Then h_max_chan = i
            If xdat(i) > h_max_count Then h_max_count = xdat(i)
        Next i

        '     Dim new_de, new_e0
        new_de = (e_high - e_low) / (h_max_chan - l_max_chan)
        new_e0 = e_high - new_de * h_max_chan

    End Sub

    Function peak_area(e_0, e_offset, del_e, e_res, xdat)
        Dim ll_1, ll_2, i, i_center As Integer
        Dim bkg, gross_area
        i_center = Int(e_0 / del_e - e_offset)
        ll_1 = i_center - Int((2.35 * 1.6 * e_res) / del_e)
        ll_2 = i_center + Int((2.35 * 1.6 * e_res) / del_e)
        peak_area = 0
        gross_area = 0

        For i = ll_1 To ll_2
            gross_area = gross_area + xdat(i)
        Next

        bkg = 0
        For i = 1 To 4
            bkg = bkg + xdat(ll_1 - i) + xdat(ll_2 + i)
        Next i
        bkg = bkg * (ll_2 - ll_1 + 1) / 8

        peak_area = gross_area - bkg

    End Function

    Function fit_lin(x, y, n_pts, i_dex)

        Dim i As Integer
        Dim weight(100), wx(100), wy(100), wx2(100), wxy(100) As Double
        Dim s_weight, s_wx, s_wy, s_wx2, s_wxy, delta, par_1, par_2, par_1_err, par_2_err As Double

        s_weight = 0
        s_wx = 0
        s_wy = 0
        s_wx2 = 0
        s_wxy = 0

        For i = 1 To n_pts
            weight(i) = 1 / y(i) ^ 0.5

        Next i

        For i = 1 To n_pts
            wx(i) = weight(i) * x(i)
            wy(i) = weight(i) * y(i)
            wx2(i) = weight(i) * x(i) ^ 2
            wxy(i) = weight(i) * x(i) * y(i)

            s_weight = s_weight + weight(i)
            s_wx = s_wx + wx(i)
            s_wy = s_wy + wy(i)
            s_wx2 = s_wx2 + wx2(i)
            s_wxy = s_wxy + wxy(i)

        Next i

        delta = s_wx2 * s_weight - s_wx ^ 2
        par_1 = (s_wy * s_wx2 - s_wx * s_wxy) / delta
        par_2 = (s_weight * s_wxy - s_wx * s_wy) / delta

        par_1_err = (s_wxy / s_wy) ^ 0.5
        par_2_err = (s_weight / delta)

        fit_lin = par_1
        If i_dex = 2 Then fit_lin = par_2
        If i_dex = 3 Then fit_lin = par_1_err
        If i_dex = 4 Then fit_lin = par_2_err

    End Function

    Function fit_log(x, y, n_pts, i_dex)
        ' y=ln(rawcounts)  this function applies appropriate weighting factors
        Dim i As Integer
        Dim weight(100), wx(100), wy(100), wx2(100), wxy(100) As Double
        Dim s_weight, s_wx, s_wy, s_wx2, s_wxy, delta, par_1, par_2, par_1_err, par_2_err As Double

        s_weight = 0
        s_wx = 0
        s_wy = 0
        s_wx2 = 0
        s_wxy = 0

        For i = 1 To n_pts
            weight(i) = y(i) ^ 0.5

        Next i

        For i = 1 To n_pts
            wx(i) = weight(i) * x(i)
            wy(i) = weight(i) * y(i)
            wx2(i) = weight(i) * x(i) ^ 2
            wxy(i) = weight(i) * x(i) * y(i)

            s_weight = s_weight + weight(i)
            s_wx = s_wx + wx(i)
            s_wy = s_wy + wy(i)
            s_wx2 = s_wx2 + wx2(i)
            s_wxy = s_wxy + wxy(i)


        Next i

        delta = s_wx2 * s_weight - s_wx ^ 2
        par_1 = (s_wy * s_wx2 - s_wx * s_wxy) / delta
        par_2 = (s_weight * s_wxy - s_wx * s_wy) / delta

        par_1_err = (s_wxy / s_wy) ^ 0.5
        par_2_err = (s_weight / delta)

        fit_log = par_1
        If i_dex = 2 Then fit_log = par_2
        If i_dex = 3 Then fit_log = par_1_err
        If i_dex = 4 Then fit_log = par_2_err

    End Function



    Sub get_split_pars(input_file_name As String)

        Dim i As Integer


        ' Reads Fit Results from text file (.res) 

        Dim init_parm_file_name As String
        Dim n_parms, idex, jdex, kdex, i_temp, j_temp, fit_n As Integer
        Dim const_flag As Boolean
        Dim str11 As String
        const_flag = False

        n_parms = 42
        idex = -2
        jdex = 0
        fit_n = 0

        Using MyReader As New Microsoft.VisualBasic.
                            FileIO.TextFieldParser(input_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(" ")
            Dim currentRow As String()

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    idex = idex + 1
                    ' If idex > 42 Then GoTo 100
                    jdex = 0
                    kdex = 0
                    '                 If idex > n_parms Then GoTo 100
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If Strings.Left(str11, 1) = "" Then GoTo 50

                        If idex > 1332 Then GoTo 100
                        '  If Strings.Left(str11, 5) = "ZZZZZ" Then GoTo 100

                        kdex = kdex + 1
                        If idex = -1 Then init_parm_file_name = str11
                        If idex = -1 And jdex = 3 Then data_file_name = str11
                        If idex = -1 And jdex = 4 Then Sample_ID = str11
                        If idex = -1 And jdex = 5 Then acq_date_temp = str11
                        If idex = -1 And jdex = 6 Then acq_time_temp = str11
                        '     
                        If idex = -1 Then GoTo 50
                        If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        If idex = 0 And kdex = 2 Then vial_diameter = Val(str11)
                        If idex = 0 And kdex = 3 Then sample_temperature = Val(str11)
                        If idex = 0 And kdex = 4 Then U_enrichment = Val(str11)
                        If idex = 0 And kdex = 5 Then Pu_weight = Val(str11)
                        If idex = 0 And kdex = 6 Then live_time = Val(str11)
                        If idex = 0 And kdex = 7 Then real_time = Val(str11)

                        If idex = 0 And jdex > 10 Then GoTo 50

                        If idex > 0 And idex < 43 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 2 Then results_arr(idex) = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 3 Then err_arr(idex) = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 4 Then is_free_par(idex) = Val(str11)
                        '  ((42 ^ 2 + 42 + 1)=1807
                        If idex > 42 And idex < 1807 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 42 And idex < 1807 And kdex = 2 Then j_temp = Val(str11)
                        If idex > 42 And idex < 1807 And kdex = 3 Then covar_array(i_temp, j_temp) = Val(str11)
                        ' If idex > 42 Then MsgBox(idex & " " & jdex & " " & kdex & " " & str11 & " " & covar_array(i_temp, j_temp))
50:                 Next


                Catch ex As Microsoft.VisualBasic.
                      FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms


            End While
        End Using


        e_0 = results_arr(13)
        d_e = results_arr(14)


    End Sub

    Sub get_split_constants(input_file_name As String)

        Dim i As Integer


        ' Reads constants used during fit from Fit Results text file (.res) 

        Dim n_parms, idex, jdex, kdex, fit_n As Integer
        Dim c_dex, iter_dex As Integer
        Dim const_flag, iter_flag As Boolean
        Dim str11 As String
        const_flag = False
        iter_flag = False

        n_parms = 42
        idex = -2
        jdex = 0
        fit_n = 0
        c_dex = -1
        iter_dex = -1

        Using MyReader As New Microsoft.VisualBasic.
                            FileIO.TextFieldParser(input_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(" ")
            Dim currentRow As String()

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    idex = idex + 1
                    If const_flag Then c_dex = c_dex + 1
                    ' 
                    jdex = 0
                    kdex = 0
                    '                
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If Strings.Left(str11, 1) = "" Then GoTo skip_2nd

                        If Strings.Left(str11, 5) = "YYYYY" Then const_flag = True
                        If Strings.Left(str11, 5) = "XXXXX" Then const_flag = False
                        If Strings.Left(str11, 5) = "XXXXX" Then iter_flag = True
                        kdex = kdex + 1
                        '   
                        If c_dex = 0 Then GoTo skip_first
                        If iter_flag = True Then GoTo skip_first
                        If const_flag And kdex = 2 Then file_const_val(c_dex + 1) = Val(str11)
skip_first:

                        If iter_flag And c_dex = 90 And kdex = 1 Then fit_iterations = Val(str11)
                        If iter_flag And c_dex = 90 And kdex = 2 Then target_iterations = Val(str11)

skip_2nd:
                    Next

                Catch ex As Microsoft.VisualBasic.
                      FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:
            End While
        End Using

    End Sub


    Sub split_file(input_file_name As String)
        '
        ' extracts original spectrum from results file
        '
        Dim init_spec_file_name As String
        Dim n_chans, idex, jdex, kdex, i_temp, fit_n As Integer
        Dim whocares
        Dim const_flag As Boolean
        Dim str11, Sample_ID_str As String
        Dim skip_flag As Integer
        skip_flag = 0
        fit_range(1) = 0
        fit_range(2) = 0

        Call get_split_pars(input_file_name)
        const_flag = False
        n_chans = max_channels
        idex = -2
        jdex = 0


        Using MyReader As New Microsoft.VisualBasic.
                            FileIO.TextFieldParser(input_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(" ")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    idex = idex + skip_flag
                    jdex = 0
                    kdex = 0
                    '                 If idex > n_parms Then GoTo 100
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If Strings.Left(Strings.Trim(str11), 5) = "ZZZZZ" Then skip_flag = 1
                        If skip_flag = 0 Then GoTo 50
                        If Strings.Left(str11, 1) = "" Then GoTo 50
                        If str11 = "NaN" Then str11 = "0"
                        kdex = kdex + 1
                        If idex = -1 Then init_spec_file_name = str11
                        If idex = -1 Then GoTo 50

                        If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        If idex = 0 And jdex > 10 Then GoTo 50

                        If idex > 0 And idex < max_channels And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 2 Then original_spc(i_temp) = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 2 Then raw_data(i_temp) = original_spc(i_temp)
                        If idex > 0 And idex < max_channels And kdex = 3 Then whocares = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 4 Then corrected_spec(i_temp) = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 5 Then fitted_spec(i_temp) = Val(str11)


                        If Strings.Mid(str11, 4, 4) = "YYYY" Then const_flag = True
                        If const_flag Then fit_n = fit_n + 1
                        If fit_n = 19 And kdex = 2 Then fit_range(1) = Val(str11)
                        If fit_n = 21 And kdex = 2 Then fit_range(2) = Val(str11)

50:                 Next
                Catch ex As Microsoft.VisualBasic.
                      FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms

            End While
        End Using


        Dim i_len As Integer

        i_len = Strings.Len(Sample_ID_str)
        If i_len > 11 Then Sample_ID = Strings.Right(Sample_ID_str, i_len - 11)

    End Sub

    Function get_assay_time(str00, i_lr)
        Dim str11, str12 As String
        Dim i11, i12 As Integer
        '      
        i11 = Strings.InStr(str00, " ")
        str11 = Strings.Left(str00, i11)
        i12 = Strings.Len(str00)
        str12 = Strings.Right(str00, i12 - i11)
        live_time = Val(str11)
        real_time = Val(str12)

        get_assay_time = live_time
        If i_lr = 2 Then get_assay_time = real_time
    End Function

    Function get_e_cal(str00, i_lr)
        Dim str11, str12 As String
        Dim i11, i12 As Integer
        ' 
        i11 = Strings.InStr(str00, " ")
        str11 = Strings.Left(str00, i11)
        i12 = Strings.Len(str00)
        str12 = Strings.Right(str00, i12 - i11)
        e_0 = Val(str11)
        d_e = Val(str12)

        get_e_cal = live_time
        If i_lr = 2 Then get_e_cal = d_e
    End Function


    Sub calc_vms_conc2(spectrum)
        Dim fit_rois(6), alpha, rel_spec(4100)
        Dim bkg_corr_ref(4100), bkg_corr_spec(4100), ref_bkg(4100), spec_bkg(4100), b_spec_temp, b_ref_temp, b_spec_sum, b_ref_sum
        Dim B_L_spec, B_L_ref, B_U_spec, B_U_ref
        Dim n_bkg_L_L, n_bkg_L_R, n_bkg_U_L, n_bkg_U_R
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, delta_mu_u

        Dim l_chan, h_chan, e_low, e_high
        Dim u_edge
        alpha = 1.1

        Call get_VMS_constants()



        If Strings.Right(vms_ref_file_name, 3) = "cnf" Or Strings.Right(vms_ref_file_name, 3) = "CNF" Then Call read_cnf_data_file(vms_ref_file_name)
        If Strings.Right(vms_ref_file_name, 3) = "spe" Or Strings.Right(vms_ref_file_name, 3) = "SPE" Then Call read_vms_ref_data_file(vms_ref_file_name)
        If Strings.Right(vms_ref_file_name, 3) = "res" Or Strings.Right(vms_ref_file_name, 3) = "RES" Then Call split_file(vms_ref_file_name)

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

        prelim_energy_cal(l_chan, h_chan, e_low, e_high, spectrum)

        '   remove background
        Dim ROI_BKG(5, 3), Peak_ROI(5, 3) As Integer
        Dim bkg_area(5), peak_area(5)
        Dim FWHM_VMS
        Dim test_peak As String

        For i = 1 To 5
            bkg_area(i) = 0
            peak_area(i) = 0
        Next i

        FWHM_VMS = 2.35 * results_arr(17)

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
        Peak_ROI(4, 2) = ROI_BKG(4, 2) - ROI_BKG(4, 1) + 1            ' num channels

        Peak_ROI(5, 1) = Int(((X_fit_rois(10, 1) - FWHM_VMS * X_fit_rois(10, 2) * (X_fit_rois(10, 1) / 88.0) ^ 0.5) - new_e0) / new_de)     ' start channel
        Peak_ROI(5, 2) = Int(((X_fit_rois(10, 3) + FWHM_VMS * X_fit_rois(10, 4) * (X_fit_rois(10, 1) / 88.0) ^ 0.5) - new_e0) / new_de)     ' end channel 
        Peak_ROI(5, 3) = Peak_ROI(5, 2) - Peak_ROI(5, 1)                                                                                             ' num channels

        test_peak = ""
        For i = 1 To 5

            For j = Peak_ROI(i, 1) To Peak_ROI(i, 2)
                bkg_area(i) = bkg_area(bkg_area(i) + raw_data(j))
                peak_area(i) = peak_area(bkg_area(i) + raw_data(j))
            Next j
            test_peak = test_peak & i & " " & bkg_area(i) & " : " & peak_area(i) & vbCrLf

        Next i


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
            B_L_spec = B_L_spec + spectrum(i)
            B_L_ref = B_L_ref + vms_ref_raw_data(i)
        Next i

        For i = n_bkg_U_L To n_bkg_U_R
            B_U_spec = B_L_spec + spectrum(i)
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
            b_spec_sum = b_spec_sum + spectrum(i)
            b_ref_sum = b_ref_sum + vms_ref_raw_data(i)
        Next i

        For i = n_bkg_L_R To n_bkg_U_L

            b_spec_temp = b_spec_temp + spectrum(i)
            b_ref_temp = b_ref_temp + vms_ref_raw_data(i)

            spec_bkg(i) = B_L_spec + (B_U_spec - B_L_spec) * b_spec_temp / b_spec_sum
            ref_bkg(i) = B_L_ref + (B_U_ref - B_L_ref) * b_ref_temp / b_ref_sum
        Next i

        For i = 1000 * Int(max_channels / 2048) To 2000 * Int(max_channels / 2048)
            rel_spec(i) = (spectrum(i) - spec_bkg(i)) / (vms_ref_raw_data(i) - ref_bkg(i)) * vms_ref_live_time / live_time
        Next i


        For i = 1 To 6
            fit_rois(i) = K_fit_rois(1, i)
        Next
        prelim_U_conc = prelim_conc(fit_rois, rel_spec)

        For i = 1 To 6
            fit_rois(i) = K_fit_rois(3, i)
        Next
        prelim_Pu_conc = prelim_conc(fit_rois, rel_spec)


    End Sub

    Sub calc_vms_conc3(spectrum)
        Dim fit_rois(6), alpha, rel_spec(4100)
        Dim bkg_corr_ref(4100), bkg_corr_spec(4100), ref_bkg(4100), spec_bkg(4100)
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, delta_mu_u

        Dim l_chan, h_chan, e_low, e_high
        Dim u_edge
        alpha = 1.1

        Call get_VMS_constants()


        If Strings.Right(vms_ref_file_name, 3) = "cnf" Or Strings.Right(vms_ref_file_name, 3) = "CNF" Then Call read_cnf_data_file(vms_ref_file_name)
        If Strings.Right(vms_ref_file_name, 3) = "spe" Or Strings.Right(vms_ref_file_name, 3) = "SPE" Then Call read_vms_ref_data_file(vms_ref_file_name)
        If Strings.Right(vms_ref_file_name, 3) = "res" Or Strings.Right(vms_ref_file_name, 3) = "RES" Then Call split_file(vms_ref_file_name)

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

        prelim_energy_cal(l_chan, h_chan, e_low, e_high, spectrum)

        '   remove background
        Dim ROI_BKG(5, 3), Peak_ROI(5, 3) As Integer
        Dim bkg_area(5), peak_area(5)
        Dim FWHM_VMS, uk1_fwhm, uk2_fwhm

        Dim uk1_ROIS(6), uk2_ROIS(6), uk1_int, uk2_int, uk1_bkg_l, uk1_bkg_h, uk2_bkg_l, uk2_bkg_h, uk1_s, uk2_s

        FWHM_VMS = 2.35 * results_arr(17)

        uk1_fwhm = (X_fit_rois(7, 1) / 88) ^ 0.5 * FWHM_VMS

        uk1_ROIS(1) = Int((X_fit_rois(7, 1) - uk1_fwhm * X_fit_rois(7, 1) - new_e0) / new_de)                       ' start Peak ROI
        uk1_ROIS(2) = Int(((X_fit_rois(7, 1) + uk1_fwhm * X_fit_rois(7, 1) - new_e0) / new_de) + 0.5)               ' num channels
        uk1_ROIS(3) = Int(((X_fit_rois(7, 3) - new_e0) / new_de) - X_fit_rois(7, 4) / 2)                              ' start Lower BKG ROI
        uk1_ROIS(4) = X_fit_rois(7, 4)                                                                               ' num channels
        uk1_ROIS(5) = Int(((X_fit_rois(7, 5) - new_e0) / new_de) - X_fit_rois(7, 6) / 2)                              ' start Lower BKG ROI
        uk1_ROIS(6) = X_fit_rois(7, 6)                                                                               ' num channels

        uk2_fwhm = (X_fit_rois(6, 1) / 88) ^ 0.5 * FWHM_VMS

        uk2_ROIS(1) = Int((X_fit_rois(6, 1) - uk1_fwhm * X_fit_rois(6, 1) - new_e0) / new_de)                       ' start Peak ROI
        uk2_ROIS(2) = Int(((X_fit_rois(6, 1) + uk1_fwhm * X_fit_rois(6, 1) - new_e0) / new_de) + 0.5)               ' num channels
        uk2_ROIS(3) = Int(((X_fit_rois(6, 3) - new_e0) / new_de) - X_fit_rois(6, 4) / 2)                              ' start Lower BKG ROI
        uk2_ROIS(4) = X_fit_rois(6, 4)                                                                               ' num channels
        uk2_ROIS(5) = Int(((X_fit_rois(7, 5) - new_e0) / new_de) - X_fit_rois(6, 6) / 2)                              ' start Lower BKG ROI
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

        For i = uk1_ROIS(3) To uk1_ROIS(4)
            uk1_bkg_l = uk1_bkg_l + raw_data(i)
        Next i

        For i = uk1_ROIS(5) To uk1_ROIS(6)
            uk1_bkg_h = uk1_bkg_h + raw_data(i)
        Next i

        For i = uk2_ROIS(1) To uk2_ROIS(2)
            uk2_int = uk2_int + raw_data(i)
        Next i

        For i = uk2_ROIS(3) To uk2_ROIS(4)
            uk2_bkg_l = uk2_bkg_l + raw_data(i)
        Next i

        For i = uk2_ROIS(5) To uk2_ROIS(6)
            uk2_bkg_h = uk2_bkg_h + raw_data(i)
        Next

        '
        '   ----------------------------------------------------------------------------------------
        '
    End Sub
    Function prelim_conc(fit_rois, norm_spec)

        Dim l_chan, h_chan, e_low, e_high
        Dim U_e_l1, U_e_l2, U_e_r1, U_e_r2, alpha
        Dim U_L1, U_L2, U_R1, U_R2 As Integer
        Dim temp_x(100), temp_y(100)
        Dim u_lower_e0, u_lower_de, u_upper_e0, u_upper_de
        '     Dim pu_lower_e0, pu_lower_de, pu_upper_e0, pu_upper_de

        Dim interp_L, interp_H
        Dim U_edge_ratio, prelim_Z_conc, delta_mu_u, energy, u_edge
        '   Dim Pu_edge_ratio, prelim_Pu_conc, delta_mu_pu, pu_edge

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
            '         temp_x(i - U_L1 + 1) = new_e0 + i * new_de
            '      temp_y(i - U_L1 + 1) = (raw_data(i))
            temp_x(i - U_L1 + 1) = Log(new_e0 + i * new_de) '- Log(U_e_l1)
            temp_y(i - U_L1 + 1) = Log(Log(1 / norm_spec(i)))
        Next i

        n_pts = U_L2 - U_L1 + 1

        u_lower_e0 = fit_lin(temp_x, temp_y, n_pts, 1)     '  coefficients for lower roi
        u_lower_de = fit_lin(temp_x, temp_y, n_pts, 2)

        For i = U_R1 To U_R2

            temp_x(i - U_R1 + 1) = Log(new_e0 + i * new_de)
            temp_y(i - U_R1 + 1) = Log(Log(1 / norm_spec(i)))
        Next i

        n_pts = U_R2 - U_R1 + 1

        u_upper_e0 = fit_lin(temp_x, temp_y, n_pts, 1)      '  coefficients for upper roi
        u_upper_de = fit_lin(temp_x, temp_y, n_pts, 2)

        interp_L = u_lower_e0 + u_lower_de * Log(u_edge)
        interp_H = u_upper_e0 + u_upper_de * Log(u_edge)

        U_edge_ratio = Exp(interp_H) - Exp(interp_L)

        '         prelim_Z_conc = -Log(U_edge_ratio) / delta_mu_u / vial_diameter
        prelim_Z_conc = (U_edge_ratio) / delta_mu_u / vial_diameter

        prelim_conc = prelim_Z_conc
    End Function

    Public Sub get_VMS_constants()
        Dim vms_const_file_name, str11 As String
        Dim vms_char_names(36) As String
        Dim j, idex, jdex As Integer


        vms_const_file_name = xrf_vms_consts_dir & "XRF_VMS_CONSTANTS.csv"


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
                        FileIO.TextFieldParser(vms_const_file_name)
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

    Public Sub read_vms_ref_data_file(ref_file_name As String)
        ' reads file in text format from ref_file_name
        ' first find energy cal
        ' then locate $DATA:

        Dim idex, chan_start, i_trip, i_tot, tim_flag, ener_flag As Integer
        Dim vial_flag, temper_flag, enrich_flag, Puwgt_flag As Integer


        Dim str11, str_time, str_vial, str_ener, str_temper, str_enrich, str_Puwgt, Sample_ID_str As String
        Dim dat1

        vms_ref_live_time = 1
        vms_ref_real_time = 1
        idex = 0
        '     MsgBox("reader " & ref_file_name)
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(ref_file_name)
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
                        If i_trip <> 0 Then vms_ref_raw_data(i_trip) = dat1
                        If i_trip > max_channels - 1 Then GoTo 100
                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While

100:        'MsgBox("array too long " & i_tot & " " & raw_data(max_channels))

            vms_ref_live_time = get_assay_time(str_time, 1)
            vms_ref_real_time = get_assay_time(str_time, 2)

            vms_ref_e_0 = get_e_cal(str_ener, 1)
            vms_ref_d_e = get_e_cal(str_ener, 2)

        End Using

    End Sub

    Public Sub read_cnf_data_file(HKEDfilename)
        '
        '  ******  Extract Spectral Data from CNF file and store in temporary file c:\MEXRF\xrfjunk.rpt
        '  ******  using genie report template datadmp.tpl which as been modifed for the KED files
        '
        Dim idex, jdex, i_trip, i_tot, tim_flag, i_lr, ener_flag As Integer
        Dim vial_flag, temper_flag, enrich_flag, Puwgt_flag As Integer
        Dim str11, str_enrich, str_Puwgt, Sample_ID_str As String
        Dim ecal_0_str, ecal_slope_str As String

        Call get_system_constants(const_file_name)

        live_time = 1
        real_time = 1
        idex = 0
        e_0 = 0
        d_e = 0.09

        Dim PauseTime, Start, Finish, TotalTime

        Dim strArg As String
        Dim temp$(9)
        Dim filname, tarname, trPrgName, outf, dirname As String

        outf = "xrfjunk.rpt"

        filname = HKEDfilename

        tarname = " /template=" & root_dir_name & "\datadmp.tpl /newfile /section=data /outfile=" & root_dir_name & outf$
        strPrgName2 = genie_report_dir_name & " " & filname & tarname

        strArg = ""

        Dim retval
        retval = Shell(strPrgName2, vbHide)

        If max_channels < 2050 Then delay(1500) Else delay(2500)                '  pause while cnf file is read using canberra executable
        If max_channels > 4200 Then delay(1000)

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
            Dim str1, str2, path_str, temper_str As String
            Dim spec_flag As Boolean
            Dim i_chan, chan_test As Integer

            num_chans_in = 0
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
                        '   
                        jdex = jdex + 1
                        str11 = currentField
                        If jdex = 1 Then str1 = str11
                        If jdex = 2 Then str2 = str11

                        If Strings.Left(str11, 7) = "Channel" Then spec_flag = True
                        If Strings.Left(str11, 7) = "Channel" Then GoTo 50

                        If str1 = "Elapsed Live time" And jdex = 2 Then live_time = Val(str2)
                        If str1 = "Elapsed Real Time" And jdex = 2 Then real_time = Val(str2)
                        If str1 = "KED Path Length" And jdex = 2 Then path_str = Val(str2)
                        If str1 = "Pu Atomic weight" And jdex = 2 Then str_Puwgt = Val(str2)
                        If str1 = "U enrichment" And jdex = 2 Then str_enrich = Val(str2)
                        If str1 = "Sample Vial Temp" And jdex = 2 Then temper_str = Val(str2)
                        If str1 = "Energy cal zero" And jdex = 2 Then ecal_0_str = Val(str2)
                        If str1 = "Energy cal slope" And jdex = 2 Then ecal_slope_str = Val(str2)
                        If str1 = "Sample Title" And jdex = 2 Then Sample_ID_str = str2
                        If str1 = "Channels" And jdex = 2 Then num_chans_in = Val(str2)
                        '         MsgBox(idex & "; " & jdex & " : " & str11 & " str1= " & str1 & " str2 = " & str2)
                        If spec_flag = False Then GoTo 50

                        i_chan = Val(str1)
                        Dim text_out As String
                        text_out = str2 & vbCrLf
                        For j_chan = 1 To 8
                            raw_data(i_chan + j_chan - 1) = get_chan_data(str2, j_chan)
                            text_out = text_out & i_chan & " : " & j_chan & " :: " & raw_data(i_chan + j_chan + 1) & vbCrLf

                        Next j_chan

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

    Private Declare Function timeGetTime Lib "winmm.dll" () As Long
    Public lngStartTime As Long
    Public Sub delay(msdelay As Long)
        lngStartTime = timeGetTime()
        Do Until (timeGetTime() - lngStartTime) > msdelay
        Loop
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
        '       MsgBox("in " & chan_data(1) & " , " & chan_data(2) & " , " & chan_data(3) & " , " & chan_data(4) & " , " & chan_data(5) & " , " & chan_data(6) & " , " & chan_data(7) & " , " & chan_data(8))

        get_chan_data = chan_data(i_lr)
    End Function

    Public Sub get_system_constants(const_file_name1 As String)
        Dim str11 As String
        Dim idex, jdex As Integer

        const_name(1) = "Peaks_1"
        const_name(2) = "Peaks_2"
        const_name(3) = "Peaks_3"
        const_name(4) = "Peaks_4"
        const_name(5) = "Peaks_5"
        const_name(6) = "Vial_default"
        const_name(7) = "Vial_depth"
        const_name(8) = "V_outer"
        const_name(9) = "Fit_lower_E"
        const_name(10) = "Fit_Upper_e"
        const_name(11) = "Max_iterations"
        const_name(12) = "Rand_cor_lower"
        const_name(13) = "Rand_cor_upper"
        const_name(14) = "Rand_cor_mult"
        const_name(15) = "Rand_cor_offset"
        const_name(16) = "kappa"
        const_name(17) = "exclude_lower"
        const_name(18) = "exclude_upper"
        const_name(19) = "na_1"
        const_name(20) = "g_width2"
        const_name(21) = "na_2"
        const_name(22) = "na_3"
        const_name(23) = "par_y"
        const_name(24) = "gen_angle1"
        const_name(25) = "gen_angle2"
        const_name(26) = "g_width"
        const_name(27) = "tailing_offset"
        const_name(28) = "XRF_back_angle"
        const_name(29) = "Cd_thickness"
        const_name(30) = "vial_thick"
        const_name(31) = "na_4"
        const_name(32) = "SS_filter"
        const_name(33) = "d_coll"
        const_name(34) = "allow_continue"
        const_name(35) = "use_passive"
        const_name(36) = "fit_increment"
        const_name(37) = "ref_temp"
        const_name(38) = "temp_coeff"
        const_name(39) = "temp_coeff_sigma"
        const_name(40) = "sub_intervals"
        const_name(41) = "spare_1"
        const_name(42) = "spare_2"
        const_name(43) = "prime_SA_1"
        const_name(44) = "prime_SA_2"
        const_name(45) = "prime_SA_3"
        const_name(46) = "second_SA_1"
        const_name(47) = "second_SA_2"
        const_name(48) = "second_SA_3"

        const_name(49) = "tertiary_SA_1"
        const_name(50) = "tertiary_SA_2"
        const_name(51) = "tertiary_SA_3"
        const_name(52) = "quaternary_SA_1"
        const_name(53) = "quaternary_SA_2"
        const_name(54) = "quaternary_SA_3"
        const_name(55) = "quinary_SA_1"
        const_name(56) = "quinary_SA_2"
        const_name(57) = "quinary_SA_3"

        const_name(58) = "blank1"
        const_name(59) = "blank2"
        const_name(60) = "blank3"

        const_name(61) = "Eff_par_1"
        const_name(62) = "Eff_par_2"
        const_name(63) = "Eff_par_3"
        const_name(64) = "Eff_par_4"
        const_name(65) = "Eff_par_5"
        const_name(66) = "Eff_par_6"
        const_name(67) = "Eff_par_7"
        const_name(68) = "blank4"
        const_name(69) = "blank5"
        const_name(70) = "blank6"

        const_name(71) = "fp_bias"
        const_name(72) = "fp_bias_err"
        const_name(73) = "blank8"
        const_name(74) = "blank9"
        const_name(75) = "fp_corr_par"
        const_name(76) = "fp_corr_par_err"
        const_name(77) = "fp_corr_E1"
        const_name(78) = "fp_corr_E2"
        const_name(79) = "blank10"
        const_name(80) = "blank11"

        const_name(81) = "blank12"
        const_name(82) = "blank13"
        const_name(83) = "blank14"
        const_name(84) = "blank15"
        const_name(85) = "blank16"
        const_name(86) = "blank17"
        const_name(87) = "blank18"
        const_name(88) = "blank19"
        const_name(89) = "blank20"
        const_name(90) = "blank21"


        idex = 0
        Dim in_str(10), filname As String
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(const_file_name1)
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

                    For i = 1 To 90
                        If const_name(i) = in_str(1) Then Const_val(i) = Val(in_str(2))
                    Next i


                    If idex > 42 And idex < 46 Then
                        standalone(1, idex - 42) = Val(in_str(2))
                        standalone_err(1, idex - 42) = Val(in_str(3))
                        For j = 1 To 3
                            standalone_covar(1, idex - 42, j) = Val(in_str(j + 3))
                        Next j
                    End If

                    If idex > 45 And idex < 49 Then
                        standalone(2, idex - 45) = Val(in_str(2))
                        standalone_err(2, idex - 45) = Val(in_str(3))
                        For j = 1 To 3
                            standalone_covar(2, idex - 45, j) = Val(in_str(j + 3))
                        Next j
                    End If

                    If idex > 48 And idex < 52 Then
                        standalone(3, idex - 48) = Val(in_str(2))
                        standalone_err(3, idex - 48) = Val(in_str(3))
                        For j = 1 To 3
                            standalone_covar(3, idex - 48, j) = Val(in_str(j + 3))
                        Next j
                    End If

                    If idex > 51 And idex < 55 Then
                        standalone(4, idex - 51) = Val(in_str(2))
                        standalone_err(4, idex - 51) = Val(in_str(3))
                        For j = 1 To 3
                            standalone_covar(4, idex - 51, j) = Val(in_str(j + 3))
                        Next j
                    End If

                    If idex > 54 And idex < 58 Then
                        standalone(5, idex - 54) = Val(in_str(2))
                        standalone_err(5, idex - 54) = Val(in_str(3))
                        For j = 1 To 3
                            standalone_covar(5, idex - 54, j) = Val(in_str(j + 3))
                        Next j
                    End If
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Sub CheckIfRunning()
        Dim xrf_proc_name As String
        xrf_proc_name = exe_name
        '  p = Process.GetProcessesByName(xrf_proc_name)
        p = Process.GetProcessesByName(str_prg_root)
        If p.Count > 0 Then
            ' Process is running
            status_prc = " fits running"
            number_running = p.Count
        Else
            ' Process is not running
            status_prc = " fits running"
        End If
    End Sub

    Sub CheckIfReadingCNF()
        Dim xrf_proc_name As String
        xrf_proc_name = exe_name
        '  p = Process.GetProcessesByName(xrf_proc_name)
        p = Process.GetProcessesByName(strPrgName2)
        If p.Count > 0 Then
            ' Process is running
            status_prc_read = " reading file"
            number_reads_going = p.Count
        Else
            ' Process is not running
            status_prc_read = " reading file"
        End If
    End Sub

    Sub get_attn_par_file(attn_par_file_name, fil_label)

        Dim attn_file_name, vms_char_names(20), str11 As String
        Dim j, idex, jdex As Integer


        attn_file_name = attn_par_file_name


        fil_label(1) = "actinide_names(1)"
        fil_label(2) = "amu_lib(1; 1)"
        fil_label(3) = "amu_lib(1; 2)"
        fil_label(4) = "amu_lib(1; 3)"
        fil_label(5) = "amu_lib(1; 4)"
        fil_label(6) = "amu_lib(1; 5)"
        fil_label(7) = "amu_lib(1; 6)"
        fil_label(8) = "amu_lib(1; 7)"
        fil_label(9) = "amu_lib(1; 8)"
        fil_label(10) = "edges(1)"
        fil_label(11) = "actinide_names(2)"
        fil_label(12) = "amu_lib(2; 1)"
        fil_label(13) = "amu_lib(2; 2)"
        fil_label(14) = "amu_lib(2; 3)"
        fil_label(15) = "amu_lib(2; 4)"
        fil_label(16) = "amu_lib(2; 5)"
        fil_label(17) = "amu_lib(2; 6)"
        fil_label(18) = "amu_lib(2; 7)"
        fil_label(19) = "amu_lib(2; 8)"
        fil_label(20) = "edges(2)"
        fil_label(21) = "actinide_names(3)"
        fil_label(22) = "amu_lib(3; 1)"
        fil_label(23) = "amu_lib(3; 2)"
        fil_label(24) = "amu_lib(3; 3)"
        fil_label(25) = "amu_lib(3; 4)"
        fil_label(26) = "amu_lib(3; 5)"
        fil_label(27) = "amu_lib(3; 6)"
        fil_label(28) = "amu_lib(3; 7)"
        fil_label(29) = "amu_lib(3; 8)"
        fil_label(30) = "edges(3)"
        fil_label(31) = "actinide_names(4)"
        fil_label(32) = "amu_lib(4; 1)"
        fil_label(33) = "amu_lib(4; 2)"
        fil_label(34) = "amu_lib(4; 3)"
        fil_label(35) = "amu_lib(4; 4)"
        fil_label(36) = "amu_lib(4; 5)"
        fil_label(37) = "amu_lib(4; 6)"
        fil_label(38) = "amu_lib(4; 7)"
        fil_label(39) = "amu_lib(4; 8)"
        fil_label(40) = "edges(4)"
        fil_label(41) = "actinide_names(5)"
        fil_label(42) = "amu_lib(5; 1)"
        fil_label(43) = "amu_lib(5; 2)"
        fil_label(44) = "amu_lib(5; 3)"
        fil_label(45) = "amu_lib(5; 4)"
        fil_label(46) = "amu_lib(5; 5)"
        fil_label(47) = "amu_lib(5; 6)"
        fil_label(48) = "amu_lib(5; 7)"
        fil_label(49) = "amu_lib(5; 8)"
        fil_label(50) = "edges(5)"
        fil_label(51) = "actinide_names(6)"
        fil_label(52) = "amu_lib(6; 1)"
        fil_label(53) = "amu_lib(6; 2)"
        fil_label(54) = "amu_lib(6; 3)"
        fil_label(55) = "amu_lib(6; 4)"
        fil_label(56) = "amu_lib(6; 5)"
        fil_label(57) = "amu_lib(6; 6)"
        fil_label(58) = "amu_lib(6; 7)"
        fil_label(59) = "amu_lib(6; 8)"
        fil_label(60) = "edges(6)"
        fil_label(61) = "actinide_names(7)"
        fil_label(62) = "amu_lib(7; 1)"
        fil_label(63) = "amu_lib(7; 2)"
        fil_label(64) = "amu_lib(7; 3)"
        fil_label(65) = "amu_lib(7; 4)"
        fil_label(66) = "amu_lib(7; 5)"
        fil_label(67) = "amu_lib(7; 6)"
        fil_label(68) = "amu_lib(7; 7)"
        fil_label(69) = "amu_lib(7; 8)"
        fil_label(70) = "edges(7)"
        fil_label(71) = "actinide_names(8)"
        fil_label(72) = "amu_lib(8; 1)"
        fil_label(73) = "amu_lib(8; 2)"
        fil_label(74) = "amu_lib(8; 3)"
        fil_label(75) = "amu_lib(8; 4)"
        fil_label(76) = "amu_lib(8; 5)"
        fil_label(77) = "amu_lib(8; 6)"
        fil_label(78) = "amu_lib(8; 7)"
        fil_label(79) = "amu_lib(8; 8)"
        fil_label(80) = "edges(8)"
        fil_label(81) = "actinide_names(9)"
        fil_label(82) = "amu_lib(9; 1)"
        fil_label(83) = "amu_lib(9; 2)"
        fil_label(84) = "amu_lib(9; 3)"
        fil_label(85) = "amu_lib(9; 4)"
        fil_label(86) = "amu_lib(9; 5)"
        fil_label(87) = "amu_lib(9; 6)"
        fil_label(88) = "amu_lib(9; 7)"
        fil_label(89) = "amu_lib(9; 8)"
        fil_label(90) = "edges(9)"
        fil_label(91) = "actinide_names(10)"
        fil_label(92) = "amu_lib(10; 1)"
        fil_label(93) = "amu_lib(10; 2)"
        fil_label(94) = "amu_lib(10; 3)"
        fil_label(95) = "amu_lib(10; 4)"
        fil_label(96) = "amu_lib(10; 5)"
        fil_label(97) = "amu_lib(10; 6)"
        fil_label(98) = "amu_lib(10; 7)"
        fil_label(99) = "amu_lib(10; 8)"
        fil_label(100) = "edges(10)"
        fil_label(101) = "container_names(1)"
        fil_label(102) = "cmu_lib(1; 1)"
        fil_label(103) = "cmu_lib(1; 2)"
        fil_label(104) = "cmu_lib(1; 3)"
        fil_label(105) = "cmu_lib(1; 4)"
        fil_label(106) = "cmu_lib(1; 5)"
        fil_label(107) = "cmu_lib(1; 6)"
        fil_label(108) = "cmu_lib(1; 7)"
        fil_label(109) = "cmu_lib(1; 8)"
        fil_label(110) = "cmu_lib(1; 9)"
        fil_label(111) = "cmu_lib(1; 10)"
        fil_label(112) = "cont_edges(1)"
        fil_label(113) = "container_names(2)"
        fil_label(114) = "cmu_lib(2; 1)"
        fil_label(115) = "cmu_lib(2; 2)"
        fil_label(116) = "cmu_lib(2; 3)"
        fil_label(117) = "cmu_lib(2; 4)"
        fil_label(118) = "cmu_lib(2; 5)"
        fil_label(119) = "cmu_lib(2; 6)"
        fil_label(120) = "cmu_lib(2; 7)"
        fil_label(121) = "cmu_lib(2; 8)"
        fil_label(122) = "cmu_lib(2; 9)"
        fil_label(123) = "cmu_lib(2; 10)"
        fil_label(124) = "cont_edges(2)"
        fil_label(125) = "solution_names(1)"
        fil_label(126) = "sol_lib(1; 1)"
        fil_label(127) = "sol_lib(1; 2)"
        fil_label(128) = "sol_lib(1; 3)"
        fil_label(129) = "sol_lib(1; 4)"
        fil_label(130) = "sol_lib(1; 5)"
        fil_label(131) = "sol_lib(1; 6)"
        fil_label(132) = "sol_lib(1; 7)"
        fil_label(133) = "sol_lib(1; 8)"
        fil_label(134) = "sol_lib(1; 9)"
        fil_label(135) = "sol_lib(1; 10)"
        fil_label(136) = "sol_edges(1)"
        fil_label(137) = "solution_names(2)"
        fil_label(138) = "sol_lib(2; 1)"
        fil_label(139) = "sol_lib(2; 2)"
        fil_label(140) = "sol_lib(2; 3)"
        fil_label(141) = "sol_lib(2; 4)"
        fil_label(142) = "sol_lib(2; 5)"
        fil_label(143) = "sol_lib(2; 6)"
        fil_label(144) = "sol_lib(2; 7)"
        fil_label(145) = "sol_lib(2; 8)"
        fil_label(146) = "sol_lib(2; 9)"
        fil_label(147) = "sol_lib(2; 10)"
        fil_label(148) = "sol_edges(2)"
        fil_label(149) = "lor_width(1)"
        fil_label(150) = "lor_width(2)"
        fil_label(151) = "lor_width(3)"
        fil_label(152) = "lor_width(4)"
        fil_label(153) = "lor_width(5)"
        fil_label(154) = "lor_width(6)"
        fil_label(155) = "lor_width(7)"
        fil_label(156) = "lor_width(8)"
        fil_label(157) = "lor_width(9)"
        fil_label(158) = "lor_width(10)"
        fil_label(159) = "element_priority(1)"
        fil_label(160) = "element_priority(2)"
        fil_label(161) = "element_priority(3)"
        fil_label(162) = "element_priority(4)"
        fil_label(163) = "element_priority(5)"
        fil_label(164) = "element_priority(6)"
        fil_label(165) = "element_priority(7)"
        fil_label(166) = "element_priority(8)"
        fil_label(167) = "element_priority(9)"
        fil_label(168) = "element_priority(10)"
        fil_label(169) = "element_symbol(1)"
        fil_label(170) = "element_symbol(2)"
        fil_label(171) = "element_symbol(3)"
        fil_label(172) = "element_symbol(4)"
        fil_label(173) = "element_symbol(5)"
        fil_label(174) = "element_symbol(6)"
        fil_label(175) = "element_symbol(7)"
        fil_label(176) = "element_symbol(8)"
        fil_label(177) = "element_symbol(9)"
        fil_label(178) = "element_symbol(10)"
        fil_label(179) = "act_norm_factor(1)"
        fil_label(180) = "act_norm_factor(2)"
        fil_label(181) = "act_norm_factor(3)"
        fil_label(182) = "act_norm_factor(4)"
        fil_label(183) = "act_norm_factor(5)"
        fil_label(184) = "act_norm_factor(6)"
        fil_label(185) = "act_norm_factor(7)"
        fil_label(186) = "act_norm_factor(8)"
        fil_label(187) = "act_norm_factor(9)"
        fil_label(188) = "act_norm_factor(10)"
        fil_label(189) = "act_norm_err(1)"
        fil_label(190) = "act_norm_err(2)"
        fil_label(191) = "act_norm_err(3)"
        fil_label(192) = "act_norm_err(4)"
        fil_label(193) = "act_norm_err(5)"
        fil_label(194) = "act_norm_err(6)"
        fil_label(195) = "act_norm_err(7)"
        fil_label(196) = "act_norm_err(8)"
        fil_label(197) = "act_norm_err(9)"
        fil_label(198) = "act_norm_err(10)"
        fil_label(199) = "misc_attn_names(1)"
        fil_label(200) = "misc_lib(1; 1)"
        fil_label(201) = "misc_lib(1; 2)"
        fil_label(202) = "misc_lib(1; 3)"
        fil_label(203) = "misc_lib(1; 4)"
        fil_label(204) = "misc_lib(1; 5)"
        fil_label(205) = "misc_lib(1; 6)"
        fil_label(206) = "misc_lib(1; 7)"
        fil_label(207) = "misc_lib(1; 8)"
        fil_label(208) = "misc_lib(1; 9)"
        fil_label(209) = "misc_lib(1; 10)"
        fil_label(210) = "misc_edges(1)"
        fil_label(211) = "misc_attn_names(2)"
        fil_label(212) = "misc_lib(2; 1)"
        fil_label(213) = "misc_lib(2; 2)"
        fil_label(214) = "misc_lib(2; 3)"
        fil_label(215) = "misc_lib(2; 4)"
        fil_label(216) = "misc_lib(2; 5)"
        fil_label(217) = "misc_lib(2; 6)"
        fil_label(218) = "misc_lib(2; 7)"
        fil_label(219) = "misc_lib(2; 8)"
        fil_label(220) = "misc_lib(2; 9)"
        fil_label(221) = "misc_lib(2; 10)"
        fil_label(222) = "misc_edges(2)"
        fil_label(223) = "misc_attn_names(3)"
        fil_label(224) = "misc_lib(3; 1)"
        fil_label(225) = "misc_lib(3; 2)"
        fil_label(226) = "misc_lib(3; 3)"
        fil_label(227) = "misc_lib(3; 4)"
        fil_label(228) = "misc_lib(3; 5)"
        fil_label(229) = "misc_lib(3; 6)"
        fil_label(230) = "misc_lib(3; 7)"
        fil_label(231) = "misc_lib(3; 8)"
        fil_label(232) = "misc_lib(3; 9)"
        fil_label(233) = "misc_lib(3; 10)"
        fil_label(234) = "misc_edges(3)"


        '   ----------------------------------------------

        idex = 0
        Dim in_str(10), filname As String
        Using MyReader As New Microsoft.VisualBasic.
            FileIO.TextFieldParser(attn_file_name)
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

                    Dim lab_dex As Integer

                    If idex > 100 Then GoTo skip1
                    '
                    ' read actinide attn coefficients from file
                    '
                    For j = 1 To 10

                        For k = 1 To 10
                            lab_dex = (j - 1) * 10 + k
                            If k = 1 And fil_label(lab_dex) = in_str(1) Then actinide_names(j) = in_str(2)
                            If k > 1 And k < 10 And fil_label(lab_dex) = in_str(1) Then _
                                amu_lib(j, k - 1) = Val(in_str(2))
                            If k = 10 And fil_label(lab_dex) = in_str(1) Then edges(j) = Val(in_str(2))
                        Next k

                    Next j

                    GoTo skip3
skip1:
                    ' read container attn coefficients from file
                    '
                    If idex > 124 Then GoTo skip2
                    For j = 1 To 2

                        For k = 1 To 12
                            lab_dex = (j - 1) * 12 + k + 100
                            If k = 1 And fil_label(lab_dex) = in_str(1) Then container_names(j) = in_str(2)
                            If k > 1 And k < 12 And fil_label(lab_dex) = in_str(1) Then _
                                cmu_lib(j, k - 1) = Val(in_str(2))
                            If k = 12 And fil_label(lab_dex) = in_str(1) Then cont_edges(j) = Val(in_str(2))
                        Next k

                    Next j

                    GoTo skip3
skip2:
                    ' read solution attn coefficients from file
                    '
                    For j = 1 To 2

                        For k = 1 To 12
                            lab_dex = (j - 1) * 12 + k + 124
                            If k = 1 And fil_label(lab_dex) = in_str(1) Then solution_names(j) = in_str(2)
                            If k > 1 And k < 12 And fil_label(lab_dex) = in_str(1) Then _
                                sol_lib(j, k - 1) = Val(in_str(2))
                            If k = 12 And fil_label(lab_dex) = in_str(1) Then sol_edges(j) = Val(in_str(2))
                        Next k

                    Next j
                    '
                    '  read lorentzian width from file
                    '  read element use from file
                    '  read element symbols from file
                    '
                    For k = 1 To 10
                        lab_dex = k + 148
                        If fil_label(lab_dex) = in_str(1) Then lor_width(k) = Val(in_str(2))
                        lab_dex = k + 158
                        If fil_label(lab_dex) = in_str(1) Then element_priority(k) = in_str(2)
                        lab_dex = k + 168
                        If fil_label(lab_dex) = in_str(1) Then element_symbol(k) = in_str(2)
                        lab_dex = k + 178
                        If fil_label(lab_dex) = in_str(1) Then act_norm_factor(k) = in_str(2)
                        lab_dex = k + 188
                        If fil_label(lab_dex) = in_str(1) Then act_norm_err(k) = in_str(2)
                    Next k

                    ' read misc attn coefficients from file
                    '
                    For j = 1 To 3

                        For k = 1 To 12
                            lab_dex = (j - 1) * 12 + k + 198
                            If k = 1 And fil_label(lab_dex) = in_str(1) Then misc_attn_names(j) = in_str(2)
                            If k > 1 And k < 12 And fil_label(lab_dex) = in_str(1) Then _
                                misc_lib(j, k - 1) = Val(in_str(2))
                            If k = 12 And fil_label(lab_dex) = in_str(1) Then misc_edges(j) = Val(in_str(2))
                        Next k

                    Next j

skip3:

                Catch ex As Microsoft.VisualBasic.
                    FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using
    End Sub


    Function R_UZ2(i_z, e_0, par_x, par_y, ang_1, ang_2, w_1, w_2, Vial_diameter, SS_thick, cd_thick, d_coll, concentration)
        Dim u_conc(6), pu_conc(6), np_conc(6), am_conc(6), cm_conc(6), xrf_e(5, 10), xrf_i(5, 10), upu_array(15), wt_array(15), uZ_array(15)
        Dim z_conc(6, 6), eff_corr_z(6), z_temp(6), conc_2nd(6), conc_prime(6), edges2(6), i_conc(6)
        Dim path_1, col_1, theta, d_I, d_f, attn_1, attn_2, attn, wt, wt_tot, r_temp
        Dim wt_x, e_Z2_kb, uz_temp, r_Uz_temp, wt_sum
        Dim energy, eff_corr
        Dim tempconc_1, tempconc_2
        Dim xsect, conc_temp

        ' calculates the conversion factor for U and the element i_z

        R_UZ2 = 0

        If i_z = 1 Then R_UZ2 = 1
        If i_z = 1 Then GoTo 500

        Call xrf_dat(xrf_e, xrf_i)

        edges2(1) = edges(element_index(1))      '   115.602
        edges2(2) = edges(element_index(2))     ' 121.791
        edges2(3) = edges(element_index(3))     ' 118.699
        edges2(4) = edges(element_index(4))     ' 128.241
        edges2(5) = edges(element_index(5))     ' 124.982

        tempconc_1 = concentration(1)
        tempconc_2 = concentration(2)

        For i = 1 To 15
            uZ_array(i) = 0
            wt_array(i) = 0
        Next i
        R_UZ2 = 0

        For i = 1 To 6
            conc_prime(i) = 0
            conc_2nd(i) = 0
            eff_corr_z(i) = 0
            z_temp(i) = 0
            i_conc(i) = 1
        Next i

        For i = 1 To 6
            For j = 1 To 6
                z_conc(i, j) = 0
            Next j
        Next i

        For i = 1 To 6
            z_conc(i, i) = concentration(i)
        Next i

        path_1 = Path_len(concentration, Vial_diameter)
        col_1 = path_1

        'Next i
        theta = 30

        For j = 3 To 10
            d_I = j / 10
            d_f = out_len(d_I, theta, Vial_diameter)

            R_UZ2 = 0

            For iz = 1 To 6
                z_temp(iz) = 0
            Next iz
            '
            For iz = 1 To 5
                eff_corr_z(iz) = HPGE_eff(xrf_e(iz, 1))
            Next iz

            GoTo 50
            'detection efficiency and transmission through sample vial of out going X-ray
            For i = 1 To 10
                attn_1 = Exp(-attn_fact_ss_7(xrf_e(1, i)) * 7.87 * SS_thick) * Exp(-attn_fact_poly_co(xrf_e(1, i)) * 0.95 * 0.2)
                attn_2 = Exp(-attn_fact_ss_7(xrf_e(i_z, i)) * 7.87 * SS_thick) * Exp(-attn_fact_poly_co(xrf_e(i_z, i)) * 0.95 * 0.2)

                attn_1 = attn_1 * Exp(-attn_fact_7_co(xrf_e(1, i), concentration) * d_f)
                attn_2 = attn_2 * Exp(-attn_fact_7_co(xrf_e(i_z, i), concentration) * d_f)

                eff_corr_z(1) = eff_corr_z(1) + HPGE_eff(xrf_e(1, i)) * xrf_i(1, i) * attn_1
                eff_corr_z(i_z) = eff_corr_z(i_z) + HPGE_eff(xrf_e(i_z, i)) * xrf_i(i_z, i) * attn_2

            Next i
50:
            '
            '----------------------------------------------------
            '
            eff_corr = eff_corr_z(1) / eff_corr_z(i_z)
            '
            ' calculate relative production rate
            wt = 0
            For i = 1 To 6
                conc_prime(i) = z_conc(1, i)
                conc_2nd(i) = z_conc(i_z, i)
            Next i

            For i = 1 To 200

                energy = 100 + i * 0.25
                If energy >= e_0 Then GoTo 100
                attn = Exp(-attn_fact_cd_co(energy) * 8.67 * cd_thick) * Exp(-attn_fact_ss_7(energy) * 7.87 * SS_thick) * Exp(-attn_fact_poly_co(energy) * 0.95 * 0.2)
                r_temp = ebel(energy, e_0, par_x, par_y, ang_1, ang_2, w_1, w_2) * Exp(-attn_fact_7_co(energy, concentration) * d_I) * attn
                wt = wt + r_temp
                '
                z_temp(1) = z_temp(1) + r_temp * attn_fact_7_PE(energy, conc_prime)
                z_temp(i_z) = z_temp(i_z) + r_temp * attn_fact_7_PE(energy, conc_2nd)

100:

            Next i

            xsect = attn_fact_7_PE(120, conc_prime) '**************************************
            conc_temp = conc_prime(1)

            wt_x = 0
            wt_tot = 0
            ' Calculate Z on U enhancement effect from each x-ray line above the k-shell binding energy
            For k = 1 To 10
                wt_tot = wt_tot + xrf_i(2, k) + xrf_i(3, k) + xrf_i(4, k) + xrf_i(5, k)

                If xrf_e(2, k) < edges2(1) Then GoTo 130
                e_Z2_kb = xrf_e(2, k)
                wt_x = wt_x + xrf_i(2, k) * (1 - Exp(-d_coll / 2 * attn_fact_7_PE(e_Z2_kb, conc_prime)))

130:            If xrf_e(3, k) < edges2(1) Then GoTo 140
                e_Z2_kb = xrf_e(3, k)
                wt_x = wt_x + xrf_i(3, k) * (1 - Exp(-d_coll / 2 * attn_fact_7_PE(e_Z2_kb, conc_prime)))

140:            If xrf_e(4, k) < edges2(1) Then GoTo 150
                e_Z2_kb = xrf_e(4, k)
                wt_x = wt_x + xrf_i(4, k) * (1 - Exp(-d_coll / 2 * attn_fact_7_PE(e_Z2_kb, conc_prime)))

150:            If xrf_e(5, k) < edges2(1) Then GoTo 200
                e_Z2_kb = xrf_e(5, k)
                wt_x = wt_x + xrf_i(5, k) * (1 - Exp(-d_coll / 2 * attn_fact_7_PE(e_Z2_kb, conc_prime)))
200:
            Next k
            '
            uz_temp = z_temp(i_z) * wt_x / wt_tot

            r_Uz_temp = (z_temp(1) + uz_temp) / z_temp(i_z)

            uZ_array(j) = r_Uz_temp
            wt_array(j) = wt
        Next j

        '
        wt_sum = 0
        R_UZ2 = 0

        For i = 3 To 10
            wt_sum = wt_sum + wt_array(i)
            R_UZ2 = uZ_array(i) * wt_array(i) + R_UZ2
        Next i

        R_UZ2 = R_UZ2 / wt_sum * concentration(i_z) / concentration(1)
        R_UZ2 = R_UZ2

500:

    End Function


    Function R_UPU(e_0, par_x1, par_y1, ang_1, ang_2, w_1, w_2, delta_e, V_diam, SS_thick, cd_thick, d_coll, concentration, i_conc, u_lor)
        Dim u_conc(6), pu_conc(6), am_conc(6), xrf_e(5, 10), xrf_i(5, 10), upu_array(15), wt_array(15), vial_wall_thick
        Dim wt_sum, wt

        Dim u_temp, pu_temp, path_1, col_1, eff_corr_u, eff_corr_pu, theta, d_I, d_f, attn_1, attn_2

        Call xrf_dat(xrf_e, xrf_i)

        For i = 1 To 15
            upu_array(i) = 0
            wt_array(i) = 0
        Next i

        R_UPU = 0
        u_temp = 0
        pu_temp = 0
        For i = 1 To 6
            pu_conc(i) = 0
            u_conc(i) = 0
            am_conc(i) = 0
        Next i

        u_conc(1) = concentration(1)
        pu_conc(2) = concentration(2)
        am_conc(5) = concentration(5)

        vial_wall_thick = 0.95 * Const_val(30)    '  vial wall density * thickess 

        path_1 = Path_len(concentration, V_diam)

        col_1 = path_1

        eff_corr_u = 0
        eff_corr_pu = 0


        'Next i
        theta = 30       ' set back scatter angle

        For j = 3 To 10
            d_I = j / 10
            d_f = out_len(d_I, theta, V_diam)
            eff_corr_u = 0
            eff_corr_pu = 0
            R_UPU = 0
            u_temp = 0
            pu_temp = 0
            '
            eff_corr_u = HPGE_eff(98.4)
            eff_corr_pu = HPGE_eff(103.75)
            GoTo 50
            'detection efficiency and transmission through sample vial of out going X-ray
            For i = 1 To 10
                attn_1 = Exp(-attn_fact_ss_7(xrf_e(1, i)) * 7.87 * SS_thick) * Exp(-attn_fact_poly_co(xrf_e(1, i)) * vial_wall_thick)
                attn_2 = Exp(-attn_fact_ss_7(xrf_e(2, i)) * 7.87 * SS_thick) * Exp(-attn_fact_poly_co(xrf_e(2, i)) * vial_wall_thick)
                'attn_fact_7_co1(energy, concentration, i_conc, deltae, u_lor, pu_lor)
                attn_1 = attn_1 * Exp(-attn_fact_7_co(xrf_e(1, i), concentration) * d_f)
                attn_2 = attn_2 * Exp(-attn_fact_7_co(xrf_e(2, i), concentration) * d_f)


                eff_corr_u = eff_corr_u + HPGE_eff(xrf_e(1, i)) * xrf_i(1, i) * attn_1
                eff_corr_pu = eff_corr_pu + HPGE_eff(xrf_e(2, i)) * xrf_i(2, i) * attn_2
            Next i
50:
            '
            '----------------------------------------------------
            '

            Dim eff_corr, energy, attn, r_temp

            eff_corr = eff_corr_u / eff_corr_pu
            '
            ' calculate relative production rate
            wt = 0
            For i = 1 To 200

                energy = 100 + i * 0.25
                If energy >= e_0 Then GoTo 100
                attn = Exp(-attn_fact_cd_co(energy) * 8.67 * cd_thick) * Exp(-attn_fact_ss_7(energy) * 7.87 * SS_thick) * Exp(-attn_fact_poly_co(energy) * vial_wall_thick)
                r_temp = ebel(energy, e_0, par_x1, par_y1, ang_1, ang_2, w_1, w_2) * Exp(-attn_fact_7_co(energy, concentration) * d_I) * attn
                wt = wt + r_temp
                '
                u_temp = u_temp + r_temp * attn_fact_7_PE(energy, u_conc)
                pu_temp = pu_temp + r_temp * attn_fact_7_PE(energy, pu_conc)

100:
            Next i

            Dim wt_x, wt_tot, e_pu_kb, upu_temp, R_UPU_temp

            wt_x = 0
            wt_tot = 0
            ' Calculate Pu on U enhancement effect from each x-ray line above the k-shell binding energy
            For k = 1 To 10
                wt_tot = wt_tot + xrf_i(2, k)
                If xrf_e(2, k) < 115.6 Then GoTo 200
                e_pu_kb = xrf_e(2, k)
                wt_x = wt_x + xrf_i(2, k) * (1 - Exp(-d_coll / 2 * attn_fact_7_PE(e_pu_kb, u_conc)))
200:
            Next k
            '
            upu_temp = pu_temp * wt_x / wt_tot

            R_UPU_temp = (u_temp + upu_temp) / pu_temp
            ' / eff_corr
            upu_array(j) = R_UPU_temp
            wt_array(j) = wt
        Next j
        '
        wt_sum = 0.0
        R_UPU = 0
        For i = 3 To 10
            wt_sum = wt_sum + wt_array(i)
            R_UPU = upu_array(i) * wt_array(i) + R_UPU
        Next i

        R_UPU = R_UPU / wt_sum * concentration(2) / concentration(1)

    End Function


    '   ---------------------------------------------------------------------------------------------


    Sub xrf_dat(xrf_e, xrf_abs)

        For i = 1 To 5
            For j = 1 To 10

                xrf_e(i, j) = xray_Lib(i, j, 1)
                xrf_abs(i, j) = xray_Lib(i, j, 2)
            Next j
        Next i
        '
        '       GoTo 100
        xrf_e(1, 1) = 98.43158   '  Ka1
        xrf_e(1, 2) = 94.65084   '  Ka2
        xrf_e(1, 3) = 93.83986   '  Ka3
        xrf_e(1, 4) = 111.29508   '  kb1
        xrf_e(1, 5) = 114.507   '  kb2
        xrf_e(1, 6) = 110.4167   '  Kb3
        xrf_e(1, 7) = 115.011   '  kb4
        xrf_e(1, 8) = 112.009   '  kb5
        xrf_e(1, 9) = 115.377   '  KO23
        xrf_e(1, 10) = 115.58   '  KP23

        ' Pu xrf energies (i=2)
        xrf_e(2, 1) = 103.734   '  Ka1
        xrf_e(2, 2) = 99.5232   '  Ka2
        xrf_e(2, 3) = 98.6815   '  Ka3
        xrf_e(2, 4) = 117.2437   '  Kb1
        xrf_e(2, 5) = 120.4376   '  kb2
        xrf_e(2, 6) = 116.2518   '  kb3
        xrf_e(2, 7) = 120.9437   '  kb4
        xrf_e(2, 8) = 117.91665   '  kb5
        xrf_e(2, 9) = 121.543   '  KO23
        xrf_e(2, 10) = 121.7668   '  KP23



        ' Np xrf energies (i=3)
        xrf_e(3, 1) = 101.059
        xrf_e(3, 2) = 97.069
        xrf_e(3, 3) = 96.242
        xrf_e(3, 4) = 114.234
        xrf_e(3, 5) = 117.463
        xrf_e(3, 6) = 113.303
        xrf_e(3, 7) = 117.876
        xrf_e(3, 8) = 114.912
        xrf_e(3, 9) = 118.429
        xrf_e(3, 10) = 118.646

        ' cm xrf energies (i=4)
        xrf_e(4, 1) = 109.271
        xrf_e(4, 2) = 104.59
        xrf_e(4, 3) = 103.715
        xrf_e(4, 4) = 123.403
        xrf_e(4, 5) = 126.889
        xrf_e(4, 6) = 122.304
        xrf_e(4, 7) = 127.352
        xrf_e(4, 8) = 124.124
        xrf_e(4, 9) = 127.97
        xrf_e(4, 10) = 128.21
        '
        ' Am xrf energies (i=5)
        xrf_e(5, 1) = 106.472
        xrf_e(5, 2) = 102.03
        xrf_e(5, 3) = 101.174
        xrf_e(5, 4) = 120.284
        xrf_e(5, 5) = 123.68
        xrf_e(5, 6) = 119.243
        xrf_e(5, 7) = 124.127
        xrf_e(5, 8) = 120.989
        xrf_e(5, 9) = 124.723
        xrf_e(5, 10) = 124.955
        '
        ' U xrf yields
        xrf_abs(1, 1) = 45.1
        xrf_abs(1, 2) = 28.2
        xrf_abs(1, 3) = 0.099
        xrf_abs(1, 4) = 10.7
        xrf_abs(1, 5) = 4.15
        xrf_abs(1, 6) = 5.65
        xrf_abs(1, 7) = 0.12
        xrf_abs(1, 8) = 0.397
        xrf_abs(1, 9) = 0.95
        xrf_abs(1, 10) = 0.159

        ' pu xrf yields
        xrf_abs(2, 1) = 45.1
        xrf_abs(2, 2) = 28.4
        xrf_abs(2, 3) = 0.114
        xrf_abs(2, 4) = 10.7
        xrf_abs(2, 5) = 4.18
        xrf_abs(2, 6) = 5.44
        xrf_abs(2, 7) = 0.13
        xrf_abs(2, 8) = 0.413
        xrf_abs(2, 9) = 0.99
        xrf_abs(2, 10) = 0.157


        ' np xrf yields
        xrf_abs(3, 1) = 45.1
        xrf_abs(3, 2) = 28.3
        xrf_abs(3, 3) = 0.105
        xrf_abs(3, 4) = 10.7
        xrf_abs(3, 5) = 4.17
        xrf_abs(3, 6) = 5.65
        xrf_abs(3, 7) = 0.12
        xrf_abs(3, 8) = 0.405
        xrf_abs(3, 9) = 0.97
        xrf_abs(3, 10) = 0.162

        ' cm xrf intensities
        xrf_abs(4, 1) = 44.8
        xrf_abs(4, 2) = 28.5
        xrf_abs(4, 3) = 0.132
        xrf_abs(4, 4) = 10.6
        xrf_abs(4, 5) = 4.19
        xrf_abs(4, 6) = 5.63
        xrf_abs(4, 7) = 0.13
        xrf_abs(4, 8) = 0.429
        xrf_abs(4, 9) = 1.02
        xrf_abs(4, 10) = 0.169
        '
        ' am xrf intensities
        xrf_abs(5, 1) = 44.9
        xrf_abs(5, 2) = 28.5
        xrf_abs(5, 3) = 0.123
        xrf_abs(5, 4) = 10.7
        xrf_abs(5, 5) = 4.19
        xrf_abs(5, 6) = 5.64
        xrf_abs(5, 7) = 0.13
        xrf_abs(5, 8) = 0.421
        xrf_abs(5, 9) = 1
        xrf_abs(5, 10) = 0.158
100:

    End Sub

    Function Path_len(conc, v_d)
        Dim in_temp(15), out_temp(15), c_temp(15), t_temp(15), w_temp(15)
        Dim t_1, t_2, d_t, t_wt, w_wt

        t_1 = 0.27
        t_2 = v_d - t_1
        d_t = (t_2 - t_1) / 14
        t_wt = 0
        w_wt = 0

        For i = 1 To 15
            t_temp(i) = t_1 + (i - 1) * d_t
            in_temp(i) = Exp(-attn_fact_7_co(116, conc) * t_temp(i))
            out_temp(i) = Exp(-attn_fact_7_co(98.6, conc) * t_temp(i))
            c_temp(i) = in_temp(i) * out_temp(i)
        Next i

        For i = 2 To 15
            w_temp(i) = (c_temp(i) - c_temp(i - 1)) ^ 2
            t_wt = t_wt + t_temp(i) * w_temp(i)
            w_wt = w_wt + w_temp(i)
        Next i

        Path_len = t_wt / w_wt


    End Function

    Function out_len(d_I, theta, d_vial)
        Dim tan_th, d_ip, a, b, c, x_f, y_f, d_trans
        tan_th = Tan(theta * 3.14159263 / 180)
        d_ip = d_vial / 2 - d_I
        a = 1 + tan_th ^ 2
        b = -d_ip * tan_th ^ 2
        c = tan_th ^ 2 * d_ip ^ 2 - (d_vial / 2) ^ 2
        x_f = (-b + (b ^ 2 - a * c) ^ 0.5) / (1 + tan_th ^ 2)
        'temp1 = (d_vial / 2 - d_I) * tan_th ^ 2
        'temp2 = -(temp1 ^ 2) - (1 + tan_th ^ 2) * (temp1 - d_vial ^ 2 / 4)
        'x_f = (temp1 + (temp2) ^ 0.5) / (1 + tan_th ^ 2)
        y_f = ((d_vial / 2) ^ 2 - x_f ^ 2) ^ 0.5
        d_trans = ((x_f - d_ip) ^ 2 + y_f ^ 2) ^ 0.5
        out_len = d_trans
    End Function

    Function HPGE_eff(energy)
        Dim eff_p(7), x, eff
        eff_p(1) = -7.26
        eff_p(2) = 0.8296
        eff_p(3) = -0.5733
        eff_p(4) = -0.3383
        eff_p(5) = 0.4638
        eff_p(6) = -0.1317
        eff_p(7) = 160

        X = Log(eff_p(7) / energy)
        eff = 0
        For i = 1 To 6
            eff = eff + eff_p(i) * X ^ (i - 1)
        Next i
        eff = Exp(eff)

        HPGE_eff = eff

    End Function

    Function ebel(energy, e_0, par_x, par_y, ang_1, ang_2, w_1, w_2)
        Dim cal_c, tim, I_A, omega, par_z, par_a, PI, phi, eps, par_m, par_j, U_0, alnz, par_nu, d_e1, rho_zm, rho_zbar, mu_z, f_abs, dn_de

        ebel = 0
        If energy > e_0 Then GoTo 100
        d_e1 = 0.075
        cal_c = 1
        tim = 1
        I_A = 15
        omega = (0.15 / 12) ^ 2  ' solid angle of detector

        'tungsten target
        par_z = 74
        par_a = 183.84

        PI = 3.14159265358979
        phi = (90 - ang_1) / 180 * PI
        eps = ang_2 / 180 * PI

        par_m = 0.1382 - 0.9211 / par_z ^ 0.5
        par_j = 0.0135 * par_z
        U_0 = e_0 / energy

        alnz = Log(par_z)
        par_nu = e_0 ^ par_m * (0.1904 - 0.2236 * alnz + 0.1292 * alnz ^ 2 - 0.0149 * alnz ^ 3)
        rho_zm = par_a / par_z * (0.00000787 * par_j ^ 0.5 * e_0 ^ 1.5 + 0.000000735 * e_0 ^ 2)
        rho_zbar = rho_zm * (0.49269 - 1.0987 * par_nu + 0.78557 * par_nu ^ 2) * Log(U_0) / (0.70256 - 1.09865 * par_nu + 1.0046 * par_nu ^ 2 + Log(U_0))
        mu_z = attn_fact_W_PE2(energy, w_1, w_2)

        f_abs = (1 - Exp(-mu_z * 2 * rho_zbar * Sin(phi) / Sin(eps))) / (mu_z * 2 * rho_zbar * Sin(phi) / Sin(eps))

        dn_de = cal_c * omega * I_A * tim * par_z * (e_0 / energy - 1) ^ par_x * f_abs * d_e1

        ebel = dn_de * (e_0 / energy) ^ par_y


100:

    End Function
    Function attn_fact_7_co(energy, concentration)
        '
        '	calculates macroscopic attenuation factor for sample 
        '

        Dim ALG_E, atany, Y
        Dim lorentz_w(5), act_attn_fact(6)
        Dim Pi, LOG_E, concent(7), attn1
        Dim AMU_BELOW, AMU_ABOVE, HNO3_attn_fact
        Dim i_conc(7), i_attn(7), i_dex, k_dex As Integer

        Pi = 3.1415926535898
        '
        For i = 1 To 6
            concent(i) = concentration(i)
        Next i

        For i = 1 To 5
            lorentz_w(i) = lor_width(element_index(5))
        Next i
        '
        attn_fact_7_co = 0.0
        attn1 = 0
        ALG_E = Log(energy)
        '
        k_dex = 1
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib(i_dex, 4) * ALG_E ^ 3 + amu_lib(i_dex, 3) * ALG_E ^ 2 + amu_lib(i_dex, 2) * ALG_E + amu_lib(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib(i_dex, 8) * ALG_E ^ 3 + amu_lib(i_dex, 7) * ALG_E ^ 2 + amu_lib(i_dex, 6) * ALG_E + amu_lib(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = (AMU_BELOW + (AMU_ABOVE - AMU_BELOW) * (0.5 + atany / Pi))
        act_attn_fact(k_dex) = act_attn_fact(k_dex) * concent(k_dex)
        '	comment out the next line to broaden above the K-edge
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = AMU_ABOVE * concent(k_dex)
        '
        '	Element 2
        k_dex = 2
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib(i_dex, 4) * ALG_E ^ 3 + amu_lib(i_dex, 3) * ALG_E ^ 2 + amu_lib(i_dex, 2) * ALG_E + amu_lib(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib(i_dex, 8) * ALG_E ^ 3 + amu_lib(i_dex, 7) * ALG_E ^ 2 + amu_lib(i_dex, 6) * ALG_E + amu_lib(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = (AMU_BELOW + (AMU_ABOVE - AMU_BELOW) * (0.5 + atany / Pi))
        act_attn_fact(k_dex) = act_attn_fact(k_dex) * concent(k_dex)
        '	comment out the next line to broaden above the K-edge
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = AMU_ABOVE * concent(k_dex)
        '
        '	Elements 3 to 5 are broadened above the K-edge (yes I could have used a loop)
        '
        '	Element 3
        k_dex = 3
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib(i_dex, 4) * ALG_E ^ 3 + amu_lib(i_dex, 3) * ALG_E ^ 2 + amu_lib(i_dex, 2) * ALG_E + amu_lib(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib(i_dex, 8) * ALG_E ^ 3 + amu_lib(i_dex, 7) * ALG_E ^ 2 + amu_lib(i_dex, 6) * ALG_E + amu_lib(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(i_dex) = (AMU_BELOW + (AMU_ABOVE - AMU_BELOW) * (0.5 + atany / Pi))
        act_attn_fact(i_dex) = act_attn_fact(i_dex) * concent(i_dex)
        '
        '	Element  4
        k_dex = 4
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib(i_dex, 4) * ALG_E ^ 3 + amu_lib(i_dex, 3) * ALG_E ^ 2 + amu_lib(i_dex, 2) * ALG_E + amu_lib(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib(i_dex, 8) * ALG_E ^ 3 + amu_lib(i_dex, 7) * ALG_E ^ 2 + amu_lib(i_dex, 6) * ALG_E + amu_lib(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = (AMU_BELOW + (AMU_ABOVE - AMU_BELOW) * (0.5 + atany / Pi))
        act_attn_fact(k_dex) = act_attn_fact(k_dex) * concent(k_dex)
        '
        '	Element  5
        k_dex = 5
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib(i_dex, 4) * ALG_E ^ 3 + amu_lib(i_dex, 3) * ALG_E ^ 2 + amu_lib(i_dex, 2) * ALG_E + amu_lib(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib(i_dex, 8) * ALG_E ^ 3 + amu_lib(i_dex, 7) * ALG_E ^ 2 + amu_lib(i_dex, 6) * ALG_E + amu_lib(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = (AMU_BELOW + (AMU_ABOVE - AMU_BELOW) * (0.5 + atany / Pi))
        act_attn_fact(k_dex) = act_attn_fact(k_dex) * concent(k_dex)
        '
        '	Solution Matrix
        i_dex = 1
        AMU_BELOW = Exp(sol_lib(i_dex, 4) * ALG_E ^ 3 + sol_lib(i_dex, 3) * ALG_E ^ 2 + sol_lib(i_dex, 2) * ALG_E + sol_lib(i_dex, 1))
        AMU_ABOVE = Exp(sol_lib(i_dex, 9) * ALG_E ^ 3 + sol_lib(i_dex, 8) * ALG_E ^ 2 + sol_lib(i_dex, 7) * ALG_E + sol_lib(i_dex, 6))
        HNO3_attn_fact = AMU_BELOW * concent(6)
        '	If (energy.GT.HNO3_edge) HNO3_attn_fact=amu_above*concent(6)
        '
        attn_fact_7_co = 0
        For i_dex = 1 To 5
            attn_fact_7_co = attn_fact_7_co + act_attn_fact(i_dex) * act_norm_factor(i_dex)
        Next i_dex

        ' MsgBox("attn1 = " & attn_fact_7_co)

        attn_fact_7_co = attn_fact_7_co + HNO3_attn_fact
        ' MsgBox("attn2 = " & attn_fact_7_co)

    End Function

    Function attn_fact_7_PE(energy, concentration)
        '
        '	calculates macroscopic photoelectric effect attenuation factor for sample 
        '

        Dim ALG_E, atany, Y
        Dim lorentz_w(5), act_attn_fact(6)
        Dim Pi, LOG_E, concent(7), attn1
        Dim AMU_BELOW, AMU_ABOVE, HNO3_attn_fact
        Dim i_conc(7), i_attn(7), i_dex, k_dex As Integer
        Dim amu_lib_PE(10, 8)

        amu_lib_PE(1, 1) = 10.0119329058011
        amu_lib_PE(1, 2) = -0.798847823209396
        amu_lib_PE(1, 3) = -0.425145312039676
        amu_lib_PE(1, 4) = 0.0315647947634744
        amu_lib_PE(1, 5) = 10.5120150462138
        amu_lib_PE(1, 6) = -0.810695671963562
        amu_lib_PE(1, 7) = -0.321809695147853
        amu_lib_PE(1, 8) = 0.0194237531047691
        '       Uranium
        amu_lib_PE(2, 1) = 10.5941954154486
        amu_lib_PE(2, 2) = -1.23331101724208
        amu_lib_PE(2, 3) = -0.308737521058118
        amu_lib_PE(2, 4) = 0.0215805921102263
        amu_lib_PE(2, 5) = 5.99632768317113
        amu_lib_PE(2, 6) = 1.83110847280586
        amu_lib_PE(2, 7) = -0.83428998853748
        amu_lib_PE(2, 8) = 0.0527746796385797
        '       Neptunium
        amu_lib_PE(3, 1) = 9.77516670702122
        amu_lib_PE(3, 2) = -0.59312589465438
        amu_lib_PE(3, 3) = -0.46579206323546
        amu_lib_PE(3, 4) = 0.0344265749850448
        amu_lib_PE(3, 5) = 14.8784321731759
        amu_lib_PE(3, 6) = -3.39484442015879
        amu_lib_PE(3, 7) = 0.192973995140209
        amu_lib_PE(3, 8) = -0.0143725867229355
        '        Plutonium
        amu_lib_PE(4, 1) = 11.0042178746686
        amu_lib_PE(4, 2) = -1.50632020659343
        amu_lib_PE(4, 3) = -0.237088123546455
        amu_lib_PE(4, 4) = 0.0156650391049262
        amu_lib_PE(4, 5) = 6.83357820271047
        amu_lib_PE(4, 6) = 1.36461535884654
        amu_lib_PE(4, 7) = -0.74344370611065
        amu_lib_PE(4, 8) = 0.0471145809104296
        '      Americium
        amu_lib_PE(5, 1) = 11.392451880772
        amu_lib_PE(5, 2) = -1.7972486400665
        amu_lib_PE(5, 3) = -0.16239124699228
        amu_lib_PE(5, 4) = 0.00952369196106988
        amu_lib_PE(5, 5) = 8.75797859870001
        amu_lib_PE(5, 6) = 0.132637936229021
        amu_lib_PE(5, 7) = -0.481672214669475
        amu_lib_PE(5, 8) = 0.0287974097446665
        '      Curium
        amu_lib_PE(6, 1) = 11.2140750131224
        amu_lib_PE(6, 2) = -1.64179044065502
        amu_lib_PE(6, 3) = -0.201876469518327
        amu_lib_PE(6, 4) = 0.0129141338923042
        amu_lib_PE(6, 5) = -3.34146339201519
        amu_lib_PE(6, 6) = 7.12810292350788
        amu_lib_PE(6, 7) = -1.82824642263536
        amu_lib_PE(6, 8) = 0.115243669399937


        Pi = 3.1415926535898
        '
        For i = 1 To 6
            concent(i) = concentration(i)
        Next i

        For i = 1 To 5
            lorentz_w(i) = lor_width(element_index(i))
        Next i
        '
        attn_fact_7_PE = 0.0
        attn1 = 0
        ALG_E = Log(energy)
        '
        k_dex = 1
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib_PE(i_dex, 4) * ALG_E ^ 3 + amu_lib_PE(i_dex, 3) * ALG_E ^ 2 + amu_lib_PE(i_dex, 2) * ALG_E + amu_lib_PE(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib_PE(i_dex, 8) * ALG_E ^ 3 + amu_lib_PE(i_dex, 7) * ALG_E ^ 2 + amu_lib_PE(i_dex, 6) * ALG_E + amu_lib_PE(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = 0
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = (AMU_ABOVE - AMU_BELOW) * concent(k_dex)
        '
        '	Element 2
        k_dex = 2
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib_PE(i_dex, 4) * ALG_E ^ 3 + amu_lib_PE(i_dex, 3) * ALG_E ^ 2 + amu_lib_PE(i_dex, 2) * ALG_E + amu_lib_PE(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib_PE(i_dex, 8) * ALG_E ^ 3 + amu_lib_PE(i_dex, 7) * ALG_E ^ 2 + amu_lib_PE(i_dex, 6) * ALG_E + amu_lib_PE(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = 0
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = (AMU_ABOVE - AMU_BELOW) * concent(k_dex)
        '
        '	Elements 3 to 5 are broadened above the K-edge (yes I could have used a loop)
        '
        '	Element 3
        k_dex = 3
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib_PE(i_dex, 4) * ALG_E ^ 3 + amu_lib_PE(i_dex, 3) * ALG_E ^ 2 + amu_lib_PE(i_dex, 2) * ALG_E + amu_lib_PE(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib_PE(i_dex, 8) * ALG_E ^ 3 + amu_lib_PE(i_dex, 7) * ALG_E ^ 2 + amu_lib_PE(i_dex, 6) * ALG_E + amu_lib_PE(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = 0
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = (AMU_ABOVE - AMU_BELOW) * concent(k_dex)
        '
        '	Element  4
        k_dex = 4
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib_PE(i_dex, 4) * ALG_E ^ 3 + amu_lib_PE(i_dex, 3) * ALG_E ^ 2 + amu_lib_PE(i_dex, 2) * ALG_E + amu_lib_PE(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib_PE(i_dex, 8) * ALG_E ^ 3 + amu_lib_PE(i_dex, 7) * ALG_E ^ 2 + amu_lib_PE(i_dex, 6) * ALG_E + amu_lib_PE(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = 0
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = (AMU_ABOVE - AMU_BELOW) * concent(k_dex)
        '
        '	Element  5
        k_dex = 5
        i_dex = element_index(k_dex)
        AMU_BELOW = Exp(amu_lib_PE(i_dex, 4) * ALG_E ^ 3 + amu_lib_PE(i_dex, 3) * ALG_E ^ 2 + amu_lib_PE(i_dex, 2) * ALG_E + amu_lib_PE(i_dex, 1))
        AMU_ABOVE = Exp(amu_lib_PE(i_dex, 8) * ALG_E ^ 3 + amu_lib_PE(i_dex, 7) * ALG_E ^ 2 + amu_lib_PE(i_dex, 6) * ALG_E + amu_lib_PE(i_dex, 5))
        Y = 2.0 * (energy - edges(i_dex)) / lorentz_w(k_dex)
        atany = Atan(Y)
        act_attn_fact(k_dex) = 0
        If (energy > edges(i_dex)) Then act_attn_fact(k_dex) = (AMU_ABOVE - AMU_BELOW) * concent(k_dex)
        '
        '	Solution Matrix
        i_dex = 1
        AMU_BELOW = Exp(sol_lib(i_dex, 4) * ALG_E ^ 3 + sol_lib(i_dex, 3) * ALG_E ^ 2 + sol_lib(i_dex, 2) * ALG_E + sol_lib(i_dex, 1))
        AMU_ABOVE = Exp(sol_lib(i_dex, 9) * ALG_E ^ 3 + sol_lib(i_dex, 8) * ALG_E ^ 2 + sol_lib(i_dex, 7) * ALG_E + sol_lib(i_dex, 6))
        HNO3_attn_fact = AMU_BELOW * concent(6)
        '	If (energy.GT.HNO3_edge) HNO3_attn_fact=amu_above*concent(6)
        '
        attn_fact_7_PE = 0
        For i_dex = 1 To 5
            attn_fact_7_PE = attn_fact_7_PE + act_attn_fact(i_dex) * act_norm_factor(i_dex)
        Next i_dex



        attn_fact_7_PE = attn_fact_7_PE


    End Function



    Function attn_fact_7_co_old(energy, concentration)
        ' returns the macroscopic attenuation coefficient for the solution

        Dim pi, lor_u, lor_pu, lor_am, lor_np, lor_cm
        Dim mu_u_l1, mu_u_l2, mu_u_l3, mu_u_h1, mu_u_h2, mu_u_h3
        Dim mu_pu_L0, mu_pu_l1, mu_pu_l2, mu_pu_l3, mu_pu_h1, mu_pu_h2, mu_pu_h3
        Dim mu_am_l1, mu_am_l2, mu_am_l3, mu_am_h1, mu_am_h2, mu_am_h3
        Dim mu_np_l1, mu_np_l2, mu_np_l3, mu_np_h1, mu_np_h2, mu_np_h3
        Dim mu_cm_l1, mu_cm_l2, mu_cm_l3, mu_cm_h1, mu_cm_h2, mu_cm_h3
        Dim mu_nitric_l1, mu_nitric_l2, mu_nitric_l3, mu_nitric_h1, mu_nitric_h2, mu_nitric_h3
        Dim u_edge, pu_edge, am_edge, np_edge, cm_edge, nitric_edge
        Dim attn1, nsteps, nstart, mu_below, mu_above, log_e, y, atany, u_attn_fact, pu_attn_fact, am_attn_fact, np_attn_fact, cm_attn_fact, nitric_attn_fact
        pi = 3.14159263



        lor_u = 0.1045
        lor_pu = 0.105
        lor_am = 0.1082
        lor_np = 0.1005
        lor_cm = 0.112

        mu_u_l1 = 0.0862
        mu_u_l2 = -2.8288
        mu_u_l3 = 5.6047
        mu_u_h1 = 0.1857
        mu_u_h2 = -3.2325
        mu_u_h3 = 6.5683

100:    u_edge = 115.602

        mu_pu_L0 = -0.00555953170342569
        mu_pu_l1 = 0.271378989178255
        mu_pu_l2 = -3.46339268223892
        mu_pu_l3 = 6.21454330042099
        mu_pu_h1 = 0.185737127
        mu_pu_h2 = -3.223039805
        mu_pu_h3 = 6.576141055


200:    pu_edge = 121.791

        mu_am_l1 = 0.088
        mu_am_l2 = -2.8207
        mu_am_l3 = 5.6318
        mu_am_h1 = 0.1645
        mu_am_h2 = -3.1239
        mu_am_h3 = 6.4687

250:    am_edge = 124.982

        mu_np_l1 = 0.0583
        mu_np_l2 = -2.722
        mu_np_l3 = 5.5244
        mu_np_h1 = 0.1941
        mu_np_h2 = -3.2653
        mu_np_h3 = 6.6167


275:    np_edge = 118.699

        ' Cm with coherent scattering
        mu_cm_l1 = 0.0795
        mu_cm_l2 = -2.786
        mu_cm_l3 = 5.6087
        mu_cm_h1 = 0.088
        mu_cm_h2 = -2.7804
        mu_cm_h3 = 6.0914

        cm_edge = 128.241

        'nitric with coherent scattering
        mu_nitric_l1 = 1.9134
        mu_nitric_l2 = -7.1617
        mu_nitric_l3 = 5.9849
        mu_nitric_h1 = 0.118
        mu_nitric_h2 = -0.8204
        mu_nitric_h3 = 0.3958

300:    nitric_edge = 45
        nitric_edge = 45

        attn_fact_7_co_old = 0

        attn1 = 0

        nsteps = Int(10 / 0.075) * 2 + 1
        nstart = Int(nsteps / 2)

        log_e = Log(energy, 10)

        mu_below = 10 ^ (mu_u_l1 * log_e ^ 2 + mu_u_l2 * log_e + mu_u_l3)
        mu_above = 10 ^ (mu_u_h1 * log_e ^ 2 + mu_u_h2 * log_e + mu_u_h3)
        y = 2 * (energy - u_edge) / lor_u
        atany = Atan(y)
        u_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(1)
        'u_attn_fact = mu_below * concentration(1)
        If energy > u_edge Then u_attn_fact = mu_above * concentration(1)

        mu_below = 10 ^ (mu_pu_L0 * log_e ^ 3 + mu_pu_l1 * log_e ^ 2 + mu_pu_l2 * log_e + mu_pu_l3)
        mu_above = 10 ^ (mu_pu_h1 * log_e ^ 2 + mu_pu_h2 * log_e + mu_pu_h3)
        y = 2 * (energy - pu_edge) / lor_pu
        atany = Atan(y)
        pu_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(2)
        'pu_attn_fact = mu_below * concentration(2)
        If energy > pu_edge Then pu_attn_fact = mu_above * concentration(2)

        mu_below = 10 ^ (mu_am_l1 * log_e ^ 2 + mu_am_l2 * log_e + mu_am_l3)
        mu_above = 10 ^ (mu_am_h1 * log_e ^ 2 + mu_am_h2 * log_e + mu_am_h3)
        y = 2 * (energy - am_edge) / lor_am
        atany = Atan(y)
        am_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(4)

        mu_below = 10 ^ (mu_np_l1 * log_e ^ 2 + mu_np_l2 * log_e + mu_np_l3)
        mu_above = 10 ^ (mu_np_h1 * log_e ^ 2 + mu_np_h2 * log_e + mu_np_h3)
        y = 2 * (energy - np_edge) / lor_np
        atany = Atan(y)
        np_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(3)

        mu_below = 10 ^ (mu_cm_l1 * log_e ^ 2 + mu_cm_l2 * log_e + mu_cm_l3)
        mu_above = 10 ^ (mu_cm_h1 * log_e ^ 2 + mu_cm_h2 * log_e + mu_cm_h3)
        y = 2 * (energy - cm_edge) / lor_cm
        atany = Atan(y)
        cm_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(5)

        'If i_conc(6) = 2 Then GoTo 950
        'GoTo 950
        mu_below = 10 ^ (mu_nitric_l1 * log_e ^ 2 + mu_nitric_l2 * log_e + mu_nitric_l3)
        mu_above = 10 ^ (mu_nitric_h1 * log_e ^ 2 + mu_nitric_h2 * log_e + mu_nitric_h3)

        nitric_attn_fact = mu_below * concentration(6)
        If energy > nitric_edge Then nitric_attn_fact = mu_above * concentration(6)
        attn_fact_7_co_old = u_attn_fact + pu_attn_fact + am_attn_fact + np_attn_fact + cm_attn_fact + nitric_attn_fact

        GoTo 999
        Dim glass_attn_fact
950:    glass_attn_fact = attn_fact_SiO2_7(energy, 1) * concentration(6)
        attn_fact_7_co_old = u_attn_fact + pu_attn_fact + am_attn_fact + np_attn_fact + cm_attn_fact + glass_attn_fact

999:

    End Function

    Function attn_fact_ss_7(energy)
        ' attn coeff for SS
        Dim edge, mu_ss_l1, mu_ss_l2, mu_ss_l3, mu_ss_h1, mu_ss_h2, mu_ss_h3, log_e, mu_below, mu_above
        edge = 60
        'attn parameters with coherent scattering
        mu_ss_l1 = -0.1964
        mu_ss_l2 = -2.2074
        mu_ss_l3 = 4.6049
        mu_ss_h1 = 1.8729
        mu_ss_h2 = -9.3876
        mu_ss_h3 = 10.8492

        GoTo 100
        'attn parameters without coherent scattering
        mu_ss_l1 = -0.2392
        mu_ss_l2 = -2.1287
        mu_ss_l3 = 4.5662
        mu_ss_h1 = 2.0803
        mu_ss_h2 = -10.227
        mu_ss_h3 = 11.6519

100:    continue1 = 1
        log_e = Log(energy, 10)
        mu_below = 10 ^ (mu_ss_l1 * log_e ^ 2 + mu_ss_l2 * log_e + mu_ss_l3)
        mu_above = 10 ^ (mu_ss_h1 * log_e ^ 2 + mu_ss_h2 * log_e + mu_ss_h3)

        attn_fact_ss_7 = mu_below
        If energy > edge Then attn_fact_ss_7 = mu_above

    End Function

    Function attn_fact_poly_co(energy)
        ' attn coeff for polyethylene

        Dim edge, l1, l2, H1, H2
        edge = 30   ' change over point



        l1 = 372.99
        l2 = -2.337
        H1 = 0.7068
        H2 = -0.306


        attn_fact_poly_co = 1
        attn_fact_poly_co = l1 * energy ^ l2
        If energy > edge Then attn_fact_poly_co = H1 * energy ^ H2


    End Function

    Function attn_fact_cd_co(energy)
        ' attn coeff for polyethylene
        Dim edge, l1, l2, H1, H2
        edge = 26.71   ' change over point


        l1 = 56259
        l2 = -2.664
        H1 = 224482
        H2 = -2.572

        attn_fact_cd_co = 1
        attn_fact_cd_co = l1 * energy ^ l2
        If energy > edge Then attn_fact_cd_co = H1 * energy ^ H2

    End Function

    Function attn_fact_UPU_PE_6old(energy, concentration, i_conc, deltae, u_lor)
        ' PHOTO ELECTRIC ATTN COEFS
        Dim lor_u, lor_pu, lor_am, lor_np
        Dim mu_u_L1, mu_u_l2, mu_u_H1, mu_u_H2, u_edge
        Dim mu_pu_L1, mu_pu_l2, mu_pu_H1, mu_pu_H2, pu_edge
        Dim mu_am_L1, mu_am_l2, mu_am_H1, mu_am_H2, am_edge
        Dim mu_np_L1, mu_np_l2, mu_np_H1, mu_np_H2, np_edge
        Dim mu_cm_L1, mu_cm_l2, mu_cm_H1, mu_cm_H2, cm_edge
        Dim mu_nitric_L1, mu_nitric_L2, mu_nitric_H1, mu_nitric_H2, nitric_edge

        lor_u = u_lor
        lor_pu = lor_u
        lor_am = lor_u
        lor_np = lor_u

        ' U with out coherent scattering
        mu_u_L1 = 282707
        mu_u_l2 = -2.61
        mu_u_H1 = 534446
        mu_u_H2 = -2.452

        If i_conc(1) <> 1 Then GoTo 100
        ' U with coherent scattering
        mu_u_L1 = 219209
        mu_u_l2 = -2.525
        mu_u_H1 = 472547
        mu_u_H2 = -2.417

100:    u_edge = 115.6

        ' Pu with out coherent scattering
        mu_pu_L1 = 292163
        mu_pu_l2 = -2.597
        mu_pu_H1 = 531436
        mu_pu_H2 = -2.437

        ' Pu with coherent scattering
        If i_conc(2) <> 1 Then GoTo 200
        mu_pu_L1 = 227031
        mu_pu_l2 = -2.513
        mu_pu_H1 = 471727
        mu_pu_H2 = -2.404

200:    pu_edge = 121.8

        ' Am with out coherent scattering
        mu_am_L1 = 297852
        mu_am_l2 = -2.595
        mu_am_H1 = 531564
        mu_am_H2 = -2.434

        ' Am with coherent scattering
        If i_conc(5) <> 1 Then GoTo 250
        mu_am_L1 = 228486
        mu_am_l2 = -2.604
        mu_am_H1 = 566029
        mu_am_H2 = -2.455

250:    am_edge = 125

        ' Np with out coherent scattering
        mu_np_L1 = 290518
        mu_np_l2 = -2.604
        mu_np_H1 = 566029
        mu_np_H2 = -2.455

        ' Np with coherent scattering
        If i_conc(5) <> 1 Then GoTo 275
        mu_np_L1 = 226198
        mu_np_l2 = -2.521
        mu_np_H1 = 502902
        mu_np_H2 = -2.422

275:    np_edge = 118.7


        'nitric without coherent scattering
        mu_nitric_L1 = 2.5393
        mu_nitric_L2 = -0.639
        mu_nitric_H1 = 0.5946
        mu_nitric_H2 = -0.281

        If i_conc(6) <> 1 Then GoTo 300
        'nitric with coherent scattering

        mu_nitric_L1 = 4.2049
        mu_nitric_L2 = -0.745
        mu_nitric_H1 = 0.7476
        mu_nitric_H2 = -0.323

300:    nitric_edge = 60
        nitric_edge = 60

        attn_fact_UPU_PE_6old = 0



        Dim attn1, nsteps, nstart, mu_below, mu_above, atany, y
        Dim u_attn_fact, pu_attn_fact, am_attn_fact, np_attn_fact

        attn1 = 0

        nsteps = Int(10 / deltae) * 2 + 1
        nstart = Int(nsteps / 2)

        mu_below = mu_u_L1 * energy ^ mu_u_l2
        mu_above = mu_u_H1 * energy ^ mu_u_H2
        y = 2 * (energy - u_edge) / lor_u
        atany = Atan(y)
        u_attn_fact = (0 * mu_below + (mu_above - mu_below) * (0.5 + 1 / PI * atany)) * concentration(1)
        If energy < u_edge Then u_attn_fact = 0

        mu_below = mu_pu_L1 * energy ^ mu_pu_l2
        mu_above = mu_pu_H1 * energy ^ mu_pu_H2
        y = 2 * (energy - pu_edge) / lor_pu
        atany = Atan(y)
        pu_attn_fact = (0 * mu_below + (mu_above - mu_below) * (0.5 + 1 / PI * atany)) * concentration(2)
        If energy < pu_edge Then pu_attn_fact = 0

        mu_below = mu_am_L1 * energy ^ mu_am_l2
        mu_above = mu_am_H1 * energy ^ mu_am_H2
        y = 2 * (energy - am_edge) / lor_am
        atany = Atan(y)
        am_attn_fact = (0 * mu_below + (mu_above - mu_below) * (0.5 + 1 / PI * atany)) * concentration(5)

        mu_below = mu_np_L1 * energy ^ mu_np_l2
        mu_above = mu_np_H1 * energy ^ mu_np_H2
        y = 2 * (energy - np_edge) / lor_np
        atany = Atan(y)
        np_attn_fact = (0 * mu_below + (mu_above - mu_below) * (0.5 + 1 / PI * atany)) * concentration(3)


        attn_fact_UPU_PE_6old = u_attn_fact + pu_attn_fact + am_attn_fact + np_attn_fact


    End Function

    Function attn_fact_W_PE2(energy, par_1, par_2)
        ' attn coeff Tungsten Photoelectric
        Dim edge, l1, l2, H1, H2

        edge = 69.53   ' change over point


        l1 = 232603
        l2 = -2.734
        H1 = 938475
        H2 = -2.679

        attn_fact_W_PE2 = 1
        attn_fact_W_PE2 = l1 * energy ^ l2
        If energy > edge Then attn_fact_W_PE2 = H1 * energy ^ H2

        attn_fact_W_PE2 = (par_1 + par_2 * energy) * attn_fact_W_PE2

    End Function

    Function attn_fact_SiO2_7(energy, i_co)
        ' attn coeff for cd
        Dim edge, mu_SiO2_L1, mu_SiO2_L2, mu_SiO2_L3, mu_SiO2_L4
        Dim mu_SiO2_H1, mu_SiO2_H2, mu_SiO2_H3, mu_SiO2_H4
        Dim log_e, mu_below, mu_above

        edge = 80
        'attn parameters with coherent scattering
        mu_SiO2_L1 = 3.14702279545457
        mu_SiO2_L2 = -0.646593633764226
        mu_SiO2_L3 = 2397628.60098791
        mu_SiO2_L4 = -4.45016507346956
        mu_SiO2_H1 = 0.906710888471664
        mu_SiO2_H2 = -0.373792442480407
        mu_SiO2_H3 = 24195120.0167647
        mu_SiO2_H4 = -4.79670165547389

        GoTo 100
        'attn parameters without coherent scattering
        mu_SiO2_L1 = 0.00126245238095238
        mu_SiO2_L2 = -0.2089425
        mu_SiO2_L3 = 8.85923154761905
        mu_SiO2_H1 = 0.00000258318065492562
        mu_SiO2_H2 = -0.00112718821034234
        mu_SiO2_H3 = 0.245734713213053

100:
        log_e = Log(energy, 10)
        mu_below = mu_SiO2_L1 * energy ^ mu_SiO2_L2 + mu_SiO2_L3 * energy ^ mu_SiO2_L4
        mu_above = mu_SiO2_H1 * energy ^ mu_SiO2_H2 + mu_SiO2_H3 * energy ^ mu_SiO2_H4

        attn_fact_SiO2_7 = mu_below
        If energy > edge Then attn_fact_SiO2_7 = mu_above

    End Function


    Sub get_ked_pars(input_file_name As String)

        ' Reads Fit Results from text file (.res) and displays results

        Dim n_parms, idex, jdex, kdex, i_temp, j_temp As Integer

        Dim str11 As String

        n_parms = 36
        idex = -2
        jdex = 0

        Using MyReader As New Microsoft.VisualBasic.
                            FileIO.TextFieldParser(input_file_name)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(" ")
            Dim currentRow As String()

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    idex = idex + 1
                    ' If idex > 36 Then GoTo 100
                    jdex = 0
                    kdex = 0
                    '                 If idex > n_parms Then GoTo 100
                    For Each currentField In currentRow
                        jdex = jdex + 1
                        str11 = currentField
                        If Strings.Left(str11, 1) = "" Then GoTo 50
                        If idex > 1332 Then GoTo 100
                        '  If Strings.Left(str11, 5) = "ZZZZZ" Then GoTo 100

                        kdex = kdex + 1
                        '    If idex = -1 Then init_parm_file_name = str11
                        '    If idex = -1 And jdex = 3 Then data_file_name = str11
                        '    If idex = -1 And jdex = 4 Then Sample_ID = str11
                        '     If idex = -1 Then GoTo 50
                        '     If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        '     If idex = 0 And kdex = 2 Then vial_diameter = Val(str11)
                        '     If idex = 0 And kdex = 3 Then sample_temperature = Val(str11)
                        '    If idex = 0 And kdex = 4 Then U_enrichment = Val(str11)
                        '    If idex = 0 And kdex = 5 Then Pu_weight = Val(str11)
                        '    If idex = 0 And kdex = 6 Then live_time = Val(str11)
                        '    If idex = 0 And kdex = 7 Then real_time = Val(str11)
                        If idex = 0 And jdex > 10 Then GoTo 50


                        If idex > 0 And idex < 37 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < 37 And kdex = 2 Then KED_PAR(idex) = Val(str11)
                        If idex > 0 And idex < 37 And kdex = 3 Then KED_PAR_err(idex) = Val(str11)
                        '   If idex > 0 And idex < 37 And kdex = 4 Then is_free_par(idex) = Val(str11)
                        '  ((36 ^ 2 + 36 + 1)=1333
                        If idex > 36 And idex < 1333 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 36 And idex < 1333 And kdex = 2 Then j_temp = Val(str11)
                        If idex > 36 And idex < 1333 And kdex = 3 Then ked_covar_array(i_temp, j_temp) = Val(str11)
                        ' If idex > 36 Then MsgBox(idex & " " & jdex & " " & kdex & " " & str11 & " " & covar_array(i_temp, j_temp))
50:                 Next
                Catch ex As Microsoft.VisualBasic.
                      FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms


            End While
        End Using



    End Sub

    Sub get_xray_data_file(xray_data_file_name, fil_label)

        Dim xdat_file_name, vms_char_names(36), str11 As String
        Dim j, idex, jdex As Integer


        xdat_file_name = xray_data_file_name
        '  XRF_branching_ratios_default.csv


        fil_label_X(1) = "U1"
        fil_label_X(2) = "U2"
        fil_label_X(3) = "U3"
        fil_label_X(4) = "U4"
        fil_label_X(5) = "U5"
        fil_label_X(6) = "U6"
        fil_label_X(7) = "U7"
        fil_label_X(8) = "U8"
        fil_label_X(9) = "U9"
        fil_label_X(10) = "U10"
        fil_label_X(11) = "U11"
        fil_label_X(12) = "U12"
        fil_label_X(13) = "U13"
        fil_label_X(14) = "U14"
        fil_label_X(15) = "U15"
        fil_label_X(16) = "Pu1"
        fil_label_X(17) = "Pu2"
        fil_label_X(18) = "Pu3"
        fil_label_X(19) = "Pu4"
        fil_label_X(20) = "Pu5"
        fil_label_X(21) = "Pu6"
        fil_label_X(22) = "Pu7"
        fil_label_X(23) = "Pu8"
        fil_label_X(24) = "Pu9"
        fil_label_X(25) = "Pu10"
        fil_label_X(26) = "Pu11"
        fil_label_X(27) = "Pu12"
        fil_label_X(28) = "Pu13"
        fil_label_X(29) = "Pu14"
        fil_label_X(30) = "Pu15"
        fil_label_X(31) = "Np1"
        fil_label_X(32) = "Np2"
        fil_label_X(33) = "Np3"
        fil_label_X(34) = "Np4"
        fil_label_X(35) = "Np5"
        fil_label_X(36) = "Np6"
        fil_label_X(37) = "Np7"
        fil_label_X(38) = "Np8"
        fil_label_X(39) = "Np9"
        fil_label_X(40) = "Np10"
        fil_label_X(41) = "Np11"
        fil_label_X(42) = "Np12"
        fil_label_X(43) = "Np13"
        fil_label_X(44) = "Np14"
        fil_label_X(45) = "Np15"
        fil_label_X(46) = "Cm1"
        fil_label_X(47) = "Cm2"
        fil_label_X(48) = "Cm3"
        fil_label_X(49) = "Cm4"
        fil_label_X(50) = "Cm5"
        fil_label_X(51) = "Cm6"
        fil_label_X(52) = "Cm7"
        fil_label_X(53) = "Cm8"
        fil_label_X(54) = "Cm9"
        fil_label_X(55) = "Cm10"
        fil_label_X(56) = "Cm11"
        fil_label_X(57) = "Cm12"
        fil_label_X(58) = "Cm13"
        fil_label_X(59) = "Cm14"
        fil_label_X(60) = "Cm15"
        fil_label_X(61) = "Am1"
        fil_label_X(62) = "Am2"
        fil_label_X(63) = "Am3"
        fil_label_X(64) = "Am4"
        fil_label_X(65) = "Am5"
        fil_label_X(66) = "Am6"
        fil_label_X(67) = "Am7"
        fil_label_X(68) = "Am8"
        fil_label_X(69) = "Am9"
        fil_label_X(70) = "Am10"
        fil_label_X(71) = "Am11"
        fil_label_X(72) = "Am12"
        fil_label_X(73) = "Am13"
        fil_label_X(74) = "Am14"
        fil_label_X(75) = "Am15"
        fil_label_X(76) = "Bi1"
        fil_label_X(77) = "Bi2"
        fil_label_X(78) = "Bi3"
        fil_label_X(79) = "Bi4"
        fil_label_X(80) = "Bi5"
        fil_label_X(81) = "Bi6"
        fil_label_X(82) = "Bi7"
        fil_label_X(83) = "Bi8"
        fil_label_X(84) = "Bi9"
        fil_label_X(85) = "Bi10"
        fil_label_X(86) = "Bi11"
        fil_label_X(87) = "Bi12"
        fil_label_X(88) = "Bi13"
        fil_label_X(89) = "Bi14"
        fil_label_X(90) = "Bi15"
        fil_label_X(91) = "Ea1"
        fil_label_X(92) = "Ea2"
        fil_label_X(93) = "Ea3"
        fil_label_X(94) = "Ea4"
        fil_label_X(95) = "Ea5"
        fil_label_X(96) = "Ea6"
        fil_label_X(97) = "Ea7"
        fil_label_X(98) = "Ea8"
        fil_label_X(99) = "Ea9"
        fil_label_X(100) = "Ea10"
        fil_label_X(101) = "Ea11"
        fil_label_X(102) = "Ea12"
        fil_label_X(103) = "Ea13"
        fil_label_X(104) = "Ea14"
        fil_label_X(105) = "Ea15"
        fil_label_X(106) = "Eb1"
        fil_label_X(107) = "Eb2"
        fil_label_X(108) = "Eb3"
        fil_label_X(109) = "Eb4"
        fil_label_X(110) = "Eb5"
        fil_label_X(111) = "Eb6"
        fil_label_X(112) = "Eb7"
        fil_label_X(113) = "Eb8"
        fil_label_X(114) = "Eb9"
        fil_label_X(115) = "Eb10"
        fil_label_X(116) = "Eb11"
        fil_label_X(117) = "Eb12"
        fil_label_X(118) = "Eb13"
        fil_label_X(119) = "Eb14"
        fil_label_X(120) = "Eb15"
        fil_label_X(121) = "Ec1"
        fil_label_X(122) = "Ec2"
        fil_label_X(123) = "Ec3"
        fil_label_X(124) = "Ec4"
        fil_label_X(125) = "Ec5"
        fil_label_X(126) = "Ec6"
        fil_label_X(127) = "Ec7"
        fil_label_X(128) = "Ec8"
        fil_label_X(129) = "Ec9"
        fil_label_X(130) = "Ec10"
        fil_label_X(131) = "Ec11"
        fil_label_X(132) = "Ec12"
        fil_label_X(133) = "Ec13"
        fil_label_X(134) = "Ec14"
        fil_label_X(135) = "Ec15"
        fil_label_X(136) = "Ed1"
        fil_label_X(137) = "Ed2"
        fil_label_X(138) = "Ed3"
        fil_label_X(139) = "Ed4"
        fil_label_X(140) = "Ed5"
        fil_label_X(141) = "Ed6"
        fil_label_X(142) = "Ed7"
        fil_label_X(143) = "Ed8"
        fil_label_X(144) = "Ed9"
        fil_label_X(145) = "Ed10"
        fil_label_X(146) = "Ed11"
        fil_label_X(147) = "Ed12"
        fil_label_X(148) = "Ed13"
        fil_label_X(149) = "Ed14"
        fil_label_X(150) = "Ed15"

        '   ----------------------------------------------
        Dim outtest As String
        outtest = ""
        idex = -1
        Dim in_str(10), filname As String
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(xdat_file_name)
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
                        If jdex > 10 Then GoTo skip1

                        in_str(jdex) = str11

                    Next

                    Dim el_dex, tr_dex As Integer

                    If idex = 0 Then GoTo skip1
                    '
                    ' read xray data from file  
                    '
                    el_dex = Int((idex - 1) / 15) + 1                       ' 15 entries per line in csv file
                    tr_dex = idex - 15 * Int((idex - 1) / 15)
                    If el_dex > 10 Then GoTo skip1                          ' max 10 elements in file
                    For j = 2 To 4                                          ' each line   descriptor, energy, rel int, lor width
                        xray_Lib(el_dex, tr_dex, j - 1) = Val(in_str(j))

                    Next j
                    '       outtest = idex & " : " & xray_Lib(el_dex, tr_dex, 1) & " : " & xray_Lib(el_dex, tr_dex, 2) & " : " & xray_Lib(el_dex, tr_dex, 3)
                    '        MsgBox(outtest)

skip1:

                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        For i = 1 To 5
            For j = 1 To 15

                xrf_e(i, j) = xray_Lib(i, j, 1)
                xrf_abs(i, j) = xray_Lib(i, j, 2)
                lor_w(i, j) = xray_Lib(i, j, 3)

            Next
        Next


    End Sub


    Function calc_R_UPU(E_HV, par_x1)
        ' called from individual results sheet
        Dim a, par_y, ang_1, ang_2, w_1, w_2, delta_e, ss_thick, cd_thick, d_coll, u_lor
        Dim concentration(6)
        Dim i_conc(6) As Integer

        '  e_0 = E_HV
        '  par_x1 = results_arr(29)
        ' par_x1 = 1.109 - 0.00435 * 74 + 0.00175 * e_0
        par_y = Const_val(23)
        ang_1 = Const_val(24)
        ang_2 = Const_val(25)
        w_1 = 1
        w_2 = 0
        delta_e = results_arr(16)
        ss_thick = Const_val(32)
        cd_thick = Const_val(29)
        d_coll = Const_val(33)


        Dim concentrations(6)
        ' use independent results for calculating R_UPu
        For i = 1 To 6
            concentrations(i) = results_arr(8 + i)
        Next i

        ' overwrite independent resutls with MEKED values or manual entry from main screen.

        If MEXRF_main.KED_check_box_3.Checked Then concentrations(1) = MEXRF_main.KED_Box_3.Text
        If MEXRF_main.KED_check_box_4.Checked Then concentrations(2) = MEXRF_main.KED_Box_4.Text
        If MEXRF_main.KED_check_box_5.Checked Then concentrations(3) = MEXRF_main.KED_Box_5.Text
        If MEXRF_main.KED_check_box_6.Checked Then concentrations(4) = MEXRF_main.KED_Box_6.Text
        If MEXRF_main.KED_check_box_7.Checked Then concentrations(5) = MEXRF_main.KED_Box_7.Text
        If MEXRF_main.KED_check_box_8.Checked Then concentrations(6) = MEXRF_main.KED_Box_8.Text

        For i = 1 To 6
            If concentrations(i) <= 0 Then concentrations(i) = 0.0000001
        Next i

        '    R_UPU_INITIAL = calc_R_UPU2(E_HV, par_x, concentrations, 0.075) * norm_branch(2)



        i_conc(1) = 1
        i_conc(2) = 1
        i_conc(3) = 1
        i_conc(4) = 1
        i_conc(5) = 1
        i_conc(6) = 1
        u_lor = 0.105

        '    MsgBox("conc = " & concentrations(1) & " : " & concentrations(2) & " : " & concentrations(4) & " : " & concentrations(5))
        '    MsgBox("hv = " & E_HV & " : par_x1 = " & par_x1)
        a = R_UPU(E_HV, par_x1, par_y, ang_1, ang_2, w_1, w_2, delta_e, vial_diameter, ss_thick, cd_thick, d_coll, concentrations, i_conc, u_lor)
        Dim b
        b = R_UZ2(2, E_HV, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, concentrations)

        calc_R_UPU = b
    End Function


    Function calc_R_UZ(i_z, E_HV, par_x1)
        ' called from individual results sheet
        Dim a, par_y, ang_1, ang_2, w_1, w_2, delta_e, ss_thick, cd_thick, d_coll, u_lor
        Dim concentration(6)
        Dim i_conc(6) As Integer

        par_y = Const_val(23)
        ang_1 = Const_val(24)
        ang_2 = Const_val(25)
        w_1 = 1
        w_2 = 0
        delta_e = results_arr(16)
        ss_thick = Const_val(32)
        cd_thick = Const_val(29)
        d_coll = Const_val(33)


        Dim concentrations(6)
        ' use independent results for calculating R_UPu
        For i = 1 To 6
            concentrations(i) = results_arr(8 + i)
        Next i

        ' overwrite independent resutls with MEKED values or manual entry from main screen.

        If MEXRF_main.KED_check_box_3.Checked Then concentrations(1) = MEXRF_main.KED_Box_3.Text
        If MEXRF_main.KED_check_box_4.Checked Then concentrations(2) = MEXRF_main.KED_Box_4.Text
        If MEXRF_main.KED_check_box_5.Checked Then concentrations(3) = MEXRF_main.KED_Box_5.Text
        If MEXRF_main.KED_check_box_6.Checked Then concentrations(4) = MEXRF_main.KED_Box_6.Text
        If MEXRF_main.KED_check_box_7.Checked Then concentrations(5) = MEXRF_main.KED_Box_7.Text
        If MEXRF_main.KED_check_box_8.Checked Then concentrations(6) = MEXRF_main.KED_Box_8.Text

        For i = 1 To 6
            If concentrations(i) <= 0 Then concentrations(i) = 0.0000001
        Next i

        '    R_UPU_INITIAL = calc_R_UPU2(E_HV, par_x, concentrations, 0.075) * norm_branch(2)




        a = R_UZ2(i_z, E_HV, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, concentrations)

        calc_R_UZ = a
    End Function


    Function calc_R_UZ2(i_z, E_HV, par_x1, concent_in)
        Dim a, par_y, ang_1, ang_2, w_1, w_2, ss_thick, cd_thick, d_coll
        Dim conc2(6)

        Dim i_conc(6) As Integer
        ' call from summary results sheet

        par_y = Const_val(23)           ' optional adjustment parameters for shaping constant - should be set to zero 
        ang_1 = Const_val(24)           ' x-ay generator incident angle (degrees)
        ang_2 = Const_val(25)           ' x-ay generator take-off angle (degrees)
        w_1 = 1                         ' optional adjustment parameters for tungsten attenuation coefficient for small angles - leave at 1
        w_2 = 0                         ' optional adjustment parameters for tungsten attenuation coefficient for small angles - leave at 0
        cd_thick = Const_val(29)        ' thickness of all cd layer between x-ray gnerator and sample vial
        ss_thick = Const_val(32)        ' thicness of SS wall between x-ray gnerator and sample vial
        d_coll = Const_val(33)          ' xrf collimator diameter in cm typically 0.25 or 0.30 

        For i = 1 To 6                  ' to prevent divide by zero in r-upu calculation set any zero or negative concentrations to a very small value
            conc2(i) = concent_in(i)
            If conc2(1) < 0 Then conc2(i) = 0.000000001
        Next

        '
        a = R_UZ2(i_z, E_HV, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2)

        calc_R_UZ2 = a

    End Function

    Function calc_R_UZ2_ERR(i_z, E_HV, par_x1, concent_in, E_HV_err, par_x1_err, concent_in_err)
        Dim a, par_y, ang_1, ang_2, w_1, w_2, ss_thick, cd_thick, d_coll
        Dim conc2(6)
        Dim a_err_HV, a_err_par_x1, conc_err, temp_err, conc_temp

        ' call from summary results sheet

        par_y = Const_val(23)           ' optional adjustment parameters for shaping constant - should be set to zero 
        ang_1 = Const_val(24)           ' x-ay generator incident angle (degrees)
        ang_2 = Const_val(25)           ' x-ay generator take-off angle (degrees)
        w_1 = 1                         ' optional adjustment parameters for tungsten attenuation coefficient for small angles - leave at 1
        w_2 = 0                         ' optional adjustment parameters for tungsten attenuation coefficient for small angles - leave at 0
        cd_thick = Const_val(29)        ' thickness of all cd layer between x-ray gnerator and sample vial
        ss_thick = Const_val(32)        ' thicness of SS wall between x-ray gnerator and sample vial
        d_coll = Const_val(33)          ' xrf collimator diameter in cm typically 0.25 or 0.30 

        For i = 1 To 6                  ' to prevent divide by zero in r-upu calculation set any zero or negative concentrations to a very small value
            conc2(i) = concent_in(i)
            If conc2(1) < 0 Then conc2(i) = 0.000000001
        Next

        '
        '   a = R_UZ2(i_z, E_HV, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2)
        a_err_HV = R_UZ2(i_z, E_HV + E_HV_err / 2, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2)
        a_err_HV = (a_err_HV - R_UZ2(i_z, E_HV - E_HV_err / 2, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2))

        a_err_par_x1 = R_UZ2(i_z, E_HV, par_x1 + par_x1_err / 2, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2)
        a_err_par_x1 = (a_err_par_x1 - R_UZ2(i_z, E_HV, par_x1 - par_x1_err / 2, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2))
        conc_err = 0
        For i = 1 To 6
            conc_temp = conc2(i)
            conc2(i) = conc2(i) + concent_in_err(i) / 2
            temp_err = R_UZ2(i_z, E_HV + E_HV_err / 2, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2)
            conc2(i) = conc2(i) - concent_in_err(i)
            temp_err = temp_err - R_UZ2(i_z, E_HV + E_HV_err / 2, par_x1, par_y, ang_1, ang_2, w_1, w_2, vial_diameter, ss_thick, cd_thick, d_coll, conc2)
            conc2(i) = conc_temp
            conc_err = conc_err + temp_err ^ 2
        Next i

        calc_R_UZ2_ERR = (a_err_HV ^ 2 + a_err_par_x1 ^ 2 + conc_err) ^ 0.5

    End Function

    Public Function Inv_Lambert(x As Double) As Double
        '
        '      Function provided on the web  seems to have starting problems and uses complex Nos where I only need real
        '      This version solves for the upper branch of the solution only
        '
        '      It uses standard Newton iteration
        '
        '      The starting value is log(x+1) which is within 40% of target from -0.33 < x < 10^100
        '
        '      Results are accurate to about 9dp from -0.3678 < x < 10^100
        '
        '      Values for second Branch are not computed ie -0.36788... < x < 0, W(x) < -1
        '
        '      There is some potential to reduce iterations for smaller +ve x values
        '      but may need to increase as x approaches -1
        '
        '                           RB Jordan 3/2/2015
        '

        Dim xTry As Double
        Dim Iter As Integer
        Inv_Lambert = 0

        '      If x > 0 Then
        '     Inv_Lambert = 0             ' on negative count rate return
        '     Exit Function
        '     End If

        If x < -Exp(-1) Then
            Inv_Lambert = 300           ' for the 1.414 cm vial values less than -1/e are infinitely thick  - set to 300 g/L
            Exit Function
        End If

        xTry = Log(1 + x)
        For Iter = 1 To 9
            xTry = xTry - (xTry - x / Exp(xTry)) / (1 + xTry)
        Next Iter

        Inv_Lambert = xTry

    End Function

    Public Function deriv_Inv_Lambert(x As Double, i As Integer)
        '
        '   Caculates derivatives of the Lambert function as a function of each parameter and of x
        '   where x = f(I, a, b)
        Dim deriv_x, deriv_a, deriv_b
        '
        ' i = 0 : derivative wrt x
        '
        deriv_x = Inv_Lambert(x)


    End Function

    Function peak_centroid(xdat, e_peak, e_offset, delta_e, e_sigma)

        Dim centroid, area_1, area_2, bkg1, bkg2
        Dim l_e, r_e As Integer

        peak_centroid = 0
        area_1 = 0
        area_2 = 0
        centroid = (e_peak - e_offset) / delta_e

        l_e = Int(centroid - 2.5 * 2.35 * e_sigma / delta_e)
        r_e = Int(centroid + 2.5 * 2.35 * e_sigma / delta_e)

        bkg1 = xdat(l_e - 1)
        bkg2 = (xdat(r_e + 1) - xdat(r_e - 1)) / (r_e - l_e + 3)

        For i = l_e To r_e
            area_1 = area_1 + (xdat(i) - (bkg1 + bkg2 * (i - l_e))) * i
            area_2 = area_2 + xdat(i) - (bkg1 + bkg2 * (i - l_e))
        Next i

        peak_centroid = area_1 / area_2 - 2
    End Function

    Function average(in_array, n_in)
        average = 0
        If n_in < 1 Then GoTo 10

        For i = 1 To n_in
            average = average + in_array(i) / n_in
        Next i
10:
    End Function

    Function stdev(in_array, n_in)
        Dim avg
        avg = 0
        stdev = 0
        If n_in < 2 Then GoTo 10

        For i = 1 To n_in
            avg = avg + in_array(i) / n_in
        Next i

        For i = 1 To n_in
            stdev = stdev + (in_array(i) - avg) ^ 2
        Next i

        stdev = ((stdev) / (n_in - 1)) ^ 0.5
10:
    End Function

    Function wt_average(array1, err_array1, n_in, i_out)
        Dim wt_avg, wt_err, avg_temp, wt_temp
        wt_avg = 0
        wt_err = 0
        avg_temp = 0
        wt_temp = 0

        For i = 1 To n_in
            If err_array1(i) <> 0 Then
                wt_temp = wt_temp + 1 / err_array1(i) ^ 2
                avg_temp = avg_temp + array1(i) / err_array1(i) ^ 2
            End If
        Next i

        If wt_temp > 0 Then
            wt_avg = avg_temp / wt_temp
            wt_err = 1 / wt_temp ^ 0.5
        End If
        wt_average = wt_avg
        If i_out = 1 Then wt_average = wt_err
    End Function

    Sub calc_vms_rates()
        Dim fit_rois(6), alpha, rel_spec(4100)
        Dim bkg_corr_ref(4100), bkg_corr_spec(4100), ref_bkg(4100), spec_bkg(4100), b_spec_temp, b_ref_temp, b_spec_sum, b_ref_sum

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
        Dim uk1_chan, uk2_chan
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

        Dim peak_area1, peak_area2
        peak_area1 = uk1_net_peak_area / xray_Lib(1, 1, 2) / live_time * 100


        peak_area2 = net_peak_area(3) / xray_Lib(2, 1, 2) / live_time * 100
        '  


        U1_out = uk1_net_peak_area / (xray_Lib(1, 1, 2) / 100) / live_time
        U2_out = uk2_net_peak_area / (xray_Lib(1, 2, 2) / 100) / live_time
        pu_out = net_peak_area(3) / (xray_Lib(2, 1, 2) / 100) / live_time



        Return
    End Sub

    Function back_i(i, i_start, B_L, B_r, sum_roi, spectrum)

        Dim sum_yj
        sum_yj = 0
        For j = i_start To i
            sum_yj = sum_yj + spectrum(j)
        Next j

        back_i = B_L + (B_r - B_L) * sum_yj / sum_roi

    End Function



    Sub get_gamma_data_file(gamma_ray_lib_name)

        Dim gdat_file_name As String
        Dim j, idex, jdex As Integer
        Dim iso_dex, line_dex As Integer

        Dim myStream As Stream = Nothing

        Dim str20 As String

        gdat_file_name = gamma_ray_lib_name
        If gdat_file_name = "" Then Return

        '  gamma_branching_ratios_default.csv

        fil_label_g(1) = "fp1_name"
        fil_label_g(2) = "fp2_name"
        fil_label_g(3) = "fp3_name"
        fil_label_g(4) = "fp4_name"
        fil_label_g(5) = "fp5_name"

        fil_label_g(6) = "fp1_1"
        fil_label_g(7) = "fp1_2"
        fil_label_g(8) = "fp1_3"
        fil_label_g(9) = "fp1_4"
        fil_label_g(10) = "fp1_5"
        fil_label_g(11) = "fp1_6"
        fil_label_g(12) = "fp1_7"
        fil_label_g(13) = "fp1_8"
        fil_label_g(14) = "fp1_9"
        fil_label_g(15) = "fp1_10"

        fil_label_g(16) = "fp2_1"
        fil_label_g(17) = "fp2_2"
        fil_label_g(18) = "fp2_3"
        fil_label_g(19) = "fp2_4"
        fil_label_g(20) = "fp2_5"
        fil_label_g(21) = "fp2_6"
        fil_label_g(22) = "fp2_7"
        fil_label_g(23) = "fp2_8"
        fil_label_g(24) = "fp2_9"
        fil_label_g(25) = "fp2_10"

        fil_label_g(26) = "fp3_1"
        fil_label_g(27) = "fp3_2"
        fil_label_g(28) = "fp3_3"
        fil_label_g(29) = "fp3_4"
        fil_label_g(30) = "fp3_5"
        fil_label_g(31) = "fp3_6"
        fil_label_g(32) = "fp3_7"
        fil_label_g(33) = "fp3_8"
        fil_label_g(34) = "fp3_9"
        fil_label_g(35) = "fp3_10"

        '   ----------------------------------------------
        Dim temp_str(10) As String

        idex = 0
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(gdat_file_name)
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

                        jdex = jdex + 1
                        str20 = currentField

                        temp_str(jdex) = str20
                        If idex = 1 Then GoTo 55

55:                 Next

                    For i = 1 To 5
                        If temp_str(1) = fil_label_g(i) Then fiss_prod_name(i) = temp_str(2)
                        If temp_str(1) = fil_label_g(i) Then gamma_pts(i) = Val(temp_str(3))
                    Next i


                    If idex > 6 Then
                        For i = 6 To 35
                            If Trim(temp_str(1)) = fil_label_g(i) Then
                                iso_dex = Int((i - 6) / 10) + 1
                                line_dex = i - 5 - (iso_dex - 1) * 10
                                gamma_energy(iso_dex, line_dex) = Val(temp_str(2))
                                gamma_int(iso_dex, line_dex) = Val(temp_str(3))
                            End If
                        Next i
                    End If

                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using


    End Sub
End Module
