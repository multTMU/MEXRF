
Imports System.IO
Imports System.Text

Public Class AttenuationCoefficients
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '  
        '    Save updated attenuation parameters in a CSV file format default file name
        '
        Dim klineout As String
        Dim j, k As Integer

        'update paraemters with screen values
        Call update_attn_parms()

        out_attn_csv = attn_param_dir & "attn_coeffs_default.csv"

        klineout = ""

        Dim fs As FileStream = File.Create(out_attn_csv)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 1 To 10
            klineout = fil_label((i - 1)*10 + 1) & ", " & actinide_names(i) & vbCrLf

            For j = 1 To 8
                klineout = klineout & fil_label((i - 1)*10 + j + 1) & ", " & amu_lib(i, j) & vbCrLf
            Next j
            klineout = klineout & fil_label(10*i) & ", " & edges(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

        Next i

        For i = 1 To 2
            klineout = fil_label((i - 1)*12 + 101) & ", " & container_names(i) & vbCrLf

            For j = 1 To 10
                klineout = klineout & fil_label((i - 1)*12 + j + 101) & ", " & cmu_lib(i, j) & vbCrLf
            Next j
            klineout = klineout & fil_label(12*i + 100) & ", " & cont_edges(i) & vbCrLf

            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

        Next i


        For i = 1 To 2
            klineout = fil_label((i - 1)*12 + 125) & ", " & solution_names(i) & vbCrLf

            For j = 1 To 10
                klineout = klineout & fil_label((i - 1)*12 + j + 125) & ", " & sol_lib(i, j) & vbCrLf
            Next j
            klineout = klineout & fil_label(12*i + 124) & ", " & sol_edges(i) & vbCrLf

            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

        Next i

        For j = 1 To 10
            klineout = fil_label(148 + j) & ", " & lor_width(j) & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
        Next
        For j = 1 To 10
            klineout = fil_label(158 + j) & ", " & element_priority(j) & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
        Next
        For j = 1 To 10
            klineout = fil_label(168 + j) & ", " & element_symbol(j) & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
        Next
        For j = 1 To 10
            klineout = fil_label(178 + j) & ", " & act_norm_factor(j) & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
        Next
        For j = 1 To 10
            klineout = fil_label(188 + j) & ", " & act_norm_err(j) & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
        Next

        For i = 1 To 3
            klineout = fil_label((i - 1)*12 + 199) & ", " & misc_attn_names(i) & vbCrLf

            For j = 1 To 10
                klineout = klineout & fil_label((i - 1)*12 + j + 199) & ", " & misc_lib(i, j) & vbCrLf
            Next j
            klineout = klineout & fil_label(12*i + 198) & ", " & misc_edges(i) & vbCrLf

            My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

        Next i
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        ' Open a csv file containing customized attenuation coefficients
        '
        Dim new_attn_par_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "\MEXRF\XRF_files\attenuation_coefficients\"
        openFileDialog1.Filter = "txt files (*.csv)|*.csv"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    new_attn_par_file_name = openFileDialog1.FileName

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

        If new_attn_par_file_name = "" Then MsgBox("No data file selected")
        If new_attn_par_file_name = "" Then Return

        Call get_attn_par_file(new_attn_par_file_name, fil_label)

        Call form14update()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '  
        '    save attenuation parameters in a CSV file format with new file name
        '
        Dim klineout As String
        Dim j, k As Integer

        'update paraemters with screen values
        Call update_attn_parms()

        SaveFileDialog1.Filter = "TXT Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
            Then
            My.Computer.FileSystem.WriteAllText _
                (SaveFileDialog1.FileName, "", True)

            out_attn_csv = SaveFileDialog1.FileName

            klineout = ""

            Dim fs As FileStream = File.Create(out_attn_csv)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()

            For i = 1 To 10
                klineout = fil_label((i - 1)*10 + 1) & ", " & actinide_names(i) & vbCrLf

                For j = 1 To 8
                    klineout = klineout & fil_label((i - 1)*10 + j + 1) & ", " & amu_lib(i, j) & vbCrLf
                Next j
                klineout = klineout & fil_label(10*i) & ", " & edges(i) & vbCrLf

                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

            Next i

            For i = 1 To 2
                klineout = fil_label((i - 1)*12 + 101) & ", " & container_names(i) & vbCrLf

                For j = 1 To 10
                    klineout = klineout & fil_label((i - 1)*12 + j + 101) & ", " & cmu_lib(i, j) & vbCrLf
                Next j
                klineout = klineout & fil_label(12*i + 100) & ", " & cont_edges(i) & vbCrLf

                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

            Next i


            For i = 1 To 2
                klineout = fil_label((i - 1)*12 + 125) & ", " & solution_names(i) & vbCrLf

                For j = 1 To 10
                    klineout = klineout & fil_label((i - 1)*12 + j + 125) & ", " & sol_lib(i, j) & vbCrLf
                Next j
                klineout = klineout & fil_label(12*i + 124) & ", " & sol_edges(i) & vbCrLf

                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

            Next i

            For j = 1 To 10
                klineout = fil_label(148 + j) & ", " & lor_width(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
            Next
            For j = 1 To 10
                klineout = fil_label(158 + j) & ", " & element_priority(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
            Next
            For j = 1 To 10
                klineout = fil_label(168 + j) & ", " & element_symbol(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
            Next
            For j = 1 To 10
                klineout = fil_label(178 + j) & ", " & act_norm_factor(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
            Next
            For j = 1 To 10
                klineout = fil_label(188 + j) & ", " & act_norm_err(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)
            Next

            For i = 1 To 3
                klineout = fil_label((i - 1)*12 + 199) & ", " & misc_attn_names(i) & vbCrLf

                For j = 1 To 10
                    klineout = klineout & fil_label((i - 1)*12 + j + 199) & ", " & misc_lib(i, j) & vbCrLf
                Next j
                klineout = klineout & fil_label(12*i + 198) & ", " & misc_edges(i) & vbCrLf

                My.Computer.FileSystem.WriteAllText(out_attn_csv, klineout, True)

            Next i

        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '
        '  convert csv file to format readable by FORTRAN executble
        '
        Dim klineout, outconstants As String
        Dim i, j, k, element_index(10) As Integer

        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i

        For i = 0 To 0
            klineout = ""
            outconstants = exe_dir_name & "\MEKED_attn_coeffs.txt"
            klineout = outconstants & vbCrLf
            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()
            For j = 1 To 10
                For k = 1 To 8
                    klineout = " " & j & " " & k & " " & amu_lib(element_index(j), k) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                Next k
            Next j

            For j = 1 To 10
                klineout = " " & j & " " & edges(element_index(j)) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            Next j

            For j = 1 To 10
                klineout = " " & j & " " & lor_width(element_index(j)) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            Next j

            For j = 1 To 2
                For k = 1 To 10
                    klineout = " " & j & " " & k & " " & cmu_lib(j, k) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                Next k
                klineout = " " & j & " " & 11 & " " & cont_edges(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next j

            For j = 1 To 2
                For k = 1 To 10
                    klineout = " " & j & " " & k & " " & sol_lib(j, k) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                Next k
                klineout = " " & j & " " & 11 & " " & sol_edges(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next j


            For j = 1 To 10
                klineout = " " & j & " " & act_norm_factor(element_index(j)) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            Next j

            For j = 1 To 10
                klineout = " " & j & " " & act_norm_err(element_index(j)) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)

            Next j


            For j = 1 To 3
                For k = 1 To 10
                    klineout = " " & j & " " & k & " " & misc_lib(j, k) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                Next k
                klineout = " " & j & " " & 11 & " " & misc_edges(j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next j

        Next i
    End Sub


    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file


        attn_par_file_name = attn_param_dir & "attn_coeffs_default.csv"

        Call get_attn_par_file(attn_par_file_name, fil_label)

        Call form14update()
    End Sub


    Sub form14update()
        TextBox1.Text = actinide_names(1)
        TextBox2.Text = actinide_names(2)
        TextBox3.Text = actinide_names(3)
        TextBox4.Text = actinide_names(4)
        TextBox5.Text = actinide_names(5)
        TextBox6.Text = actinide_names(6)
        TextBox7.Text = actinide_names(7)
        TextBox8.Text = actinide_names(8)
        TextBox9.Text = actinide_names(9)
        TextBox10.Text = actinide_names(10)

        TextBox11.Text = amu_lib(1, 1)
        TextBox21.Text = amu_lib(1, 2)
        TextBox31.Text = amu_lib(1, 3)
        TextBox41.Text = amu_lib(1, 4)
        TextBox51.Text = amu_lib(1, 5)
        TextBox61.Text = amu_lib(1, 6)
        TextBox71.Text = amu_lib(1, 7)
        TextBox81.Text = amu_lib(1, 8)

        TextBox12.Text = amu_lib(2, 1)
        TextBox22.Text = amu_lib(2, 2)
        TextBox32.Text = amu_lib(2, 3)
        TextBox42.Text = amu_lib(2, 4)
        TextBox52.Text = amu_lib(2, 5)
        TextBox62.Text = amu_lib(2, 6)
        TextBox72.Text = amu_lib(2, 7)
        TextBox82.Text = amu_lib(2, 8)

        TextBox13.Text = amu_lib(3, 1)
        TextBox23.Text = amu_lib(3, 2)
        TextBox33.Text = amu_lib(3, 3)
        TextBox43.Text = amu_lib(3, 4)
        TextBox53.Text = amu_lib(3, 5)
        TextBox63.Text = amu_lib(3, 6)
        TextBox73.Text = amu_lib(3, 7)
        TextBox83.Text = amu_lib(3, 8)

        TextBox14.Text = amu_lib(4, 1)
        TextBox24.Text = amu_lib(4, 2)
        TextBox34.Text = amu_lib(4, 3)
        TextBox44.Text = amu_lib(4, 4)
        TextBox54.Text = amu_lib(4, 5)
        TextBox64.Text = amu_lib(4, 6)
        TextBox74.Text = amu_lib(4, 7)
        TextBox84.Text = amu_lib(4, 8)

        TextBox15.Text = amu_lib(5, 1)
        TextBox25.Text = amu_lib(5, 2)
        TextBox35.Text = amu_lib(5, 3)
        TextBox45.Text = amu_lib(5, 4)
        TextBox55.Text = amu_lib(5, 5)
        TextBox65.Text = amu_lib(5, 6)
        TextBox75.Text = amu_lib(5, 7)
        TextBox85.Text = amu_lib(5, 8)

        TextBox16.Text = amu_lib(6, 1)
        TextBox26.Text = amu_lib(6, 2)
        TextBox36.Text = amu_lib(6, 3)
        TextBox46.Text = amu_lib(6, 4)
        TextBox56.Text = amu_lib(6, 5)
        TextBox66.Text = amu_lib(6, 6)
        TextBox76.Text = amu_lib(6, 7)
        TextBox86.Text = amu_lib(6, 8)

        TextBox17.Text = amu_lib(7, 1)
        TextBox27.Text = amu_lib(7, 2)
        TextBox37.Text = amu_lib(7, 3)
        TextBox47.Text = amu_lib(7, 4)
        TextBox57.Text = amu_lib(7, 5)
        TextBox67.Text = amu_lib(7, 6)
        TextBox77.Text = amu_lib(7, 7)
        TextBox87.Text = amu_lib(7, 8)

        TextBox18.Text = amu_lib(8, 1)
        TextBox28.Text = amu_lib(8, 2)
        TextBox38.Text = amu_lib(8, 3)
        TextBox48.Text = amu_lib(8, 4)
        TextBox58.Text = amu_lib(8, 5)
        TextBox68.Text = amu_lib(8, 6)
        TextBox78.Text = amu_lib(8, 7)
        TextBox88.Text = amu_lib(8, 8)

        TextBox19.Text = amu_lib(9, 1)
        TextBox29.Text = amu_lib(9, 2)
        TextBox39.Text = amu_lib(9, 3)
        TextBox49.Text = amu_lib(9, 4)
        TextBox59.Text = amu_lib(9, 5)
        TextBox69.Text = amu_lib(9, 6)
        TextBox79.Text = amu_lib(9, 7)
        TextBox89.Text = amu_lib(9, 8)

        TextBox20.Text = amu_lib(10, 1)
        TextBox30.Text = amu_lib(10, 2)
        TextBox40.Text = amu_lib(10, 3)
        TextBox50.Text = amu_lib(10, 4)
        TextBox60.Text = amu_lib(10, 5)
        TextBox70.Text = amu_lib(10, 6)
        TextBox80.Text = amu_lib(10, 7)
        TextBox90.Text = amu_lib(10, 8)

        TextBox91.Text = act_norm_factor(1)
        TextBox92.Text = act_norm_factor(2)
        TextBox93.Text = act_norm_factor(3)
        TextBox94.Text = act_norm_factor(4)
        TextBox95.Text = act_norm_factor(5)
        TextBox96.Text = act_norm_factor(6)
        TextBox97.Text = act_norm_factor(7)
        TextBox98.Text = act_norm_factor(8)
        TextBox99.Text = act_norm_factor(9)
        TextBox100.Text = act_norm_factor(10)

        TextBox391.Text = act_norm_err(1)
        TextBox392.Text = act_norm_err(2)
        TextBox393.Text = act_norm_err(3)
        TextBox394.Text = act_norm_err(4)
        TextBox395.Text = act_norm_err(5)
        TextBox396.Text = act_norm_err(6)
        TextBox397.Text = act_norm_err(7)
        TextBox398.Text = act_norm_err(8)
        TextBox399.Text = act_norm_err(9)
        TextBox400.Text = act_norm_err(10)


        TextBox101.Text = container_names(1)
        TextBox102.Text = cmu_lib(1, 1)
        TextBox103.Text = cmu_lib(1, 2)
        TextBox104.Text = cmu_lib(1, 3)
        TextBox105.Text = cmu_lib(1, 4)
        TextBox106.Text = cmu_lib(1, 5)
        TextBox107.Text = cmu_lib(1, 6)
        TextBox108.Text = cmu_lib(1, 7)
        TextBox109.Text = cmu_lib(1, 8)
        TextBox110a.Text = cmu_lib(1, 9)
        TextBox111a.Text = cmu_lib(1, 10)
        cr_over_box1.Text = cont_edges(1)

        TextBox110.Text = container_names(2)
        TextBox111.Text = cmu_lib(2, 1)
        TextBox112.Text = cmu_lib(2, 2)
        TextBox113.Text = cmu_lib(2, 3)
        TextBox114.Text = cmu_lib(2, 4)
        TextBox115.Text = cmu_lib(2, 5)
        TextBox116.Text = cmu_lib(2, 6)
        TextBox117.Text = cmu_lib(2, 7)
        TextBox118.Text = cmu_lib(2, 8)
        TextBox120a.Text = cmu_lib(2, 9)
        TextBox121a.Text = cmu_lib(2, 10)
        cr_over_box2.Text = cont_edges(2)

        TextBox119.Text = solution_names(1)
        TextBox120.Text = sol_lib(1, 1)
        TextBox121.Text = sol_lib(1, 2)
        TextBox122.Text = sol_lib(1, 3)
        TextBox123.Text = sol_lib(1, 4)
        TextBox124.Text = sol_lib(1, 5)
        TextBox125.Text = sol_lib(1, 6)
        TextBox126.Text = sol_lib(1, 7)
        TextBox127.Text = sol_lib(1, 8)
        TextBox130a.Text = sol_lib(1, 9)
        TextBox131a.Text = sol_lib(1, 10)
        cr_over_box3.Text = sol_edges(1)

        TextBox201.Text = edges(1)
        TextBox202.Text = edges(2)
        TextBox203.Text = edges(3)
        TextBox204.Text = edges(4)
        TextBox205.Text = edges(5)
        TextBox206.Text = edges(6)
        TextBox207.Text = edges(7)
        TextBox208.Text = edges(8)
        TextBox209.Text = edges(9)
        TextBox210.Text = edges(10)

        TextBox211.Text = lor_width(1)
        TextBox212.Text = lor_width(2)
        TextBox213.Text = lor_width(3)
        TextBox214.Text = lor_width(4)
        TextBox215.Text = lor_width(5)
        TextBox216.Text = lor_width(6)
        TextBox217.Text = lor_width(7)
        TextBox218.Text = lor_width(8)
        TextBox219.Text = lor_width(9)
        TextBox220.Text = lor_width(10)

        TextBox221.Text = element_priority(1)
        TextBox222.Text = element_priority(2)
        TextBox223.Text = element_priority(3)
        TextBox224.Text = element_priority(4)
        TextBox225.Text = element_priority(5)
        TextBox226.Text = element_priority(6)
        TextBox227.Text = element_priority(7)
        TextBox228.Text = element_priority(8)
        TextBox229.Text = element_priority(9)
        TextBox230.Text = element_priority(10)

        TextBox231.Text = element_symbol(1)
        TextBox232.Text = element_symbol(2)
        TextBox233.Text = element_symbol(3)
        TextBox234.Text = element_symbol(4)
        TextBox235.Text = element_symbol(5)
        TextBox236.Text = element_symbol(6)
        TextBox237.Text = element_symbol(7)
        TextBox238.Text = element_symbol(8)
        TextBox239.Text = element_symbol(9)
        TextBox240.Text = element_symbol(10)


        misctext_1_1.Text = misc_attn_names(1)
        misctext_1_2.Text = misc_lib(1, 1)
        misctext_1_3.Text = misc_lib(1, 2)
        misctext_1_4.Text = misc_lib(1, 3)
        misctext_1_5.Text = misc_lib(1, 4)
        misctext_1_6.Text = misc_lib(1, 5)
        misctext_1_7.Text = misc_lib(1, 6)
        misctext_1_8.Text = misc_lib(1, 7)
        misctext_1_9.Text = misc_lib(1, 8)
        misctext_1_10.Text = misc_lib(1, 9)
        misctext_1_11.Text = misc_lib(1, 10)
        cr_over_box4.Text = misc_edges(1)

        misctext_2_1.Text = misc_attn_names(2)
        misctext_2_2.Text = misc_lib(2, 1)
        misctext_2_3.Text = misc_lib(2, 2)
        misctext_2_4.Text = misc_lib(2, 3)
        misctext_2_5.Text = misc_lib(2, 4)
        misctext_2_6.Text = misc_lib(2, 5)
        misctext_2_7.Text = misc_lib(2, 6)
        misctext_2_8.Text = misc_lib(2, 7)
        misctext_2_9.Text = misc_lib(2, 8)
        misctext_2_10.Text = misc_lib(2, 9)
        misctext_2_11.Text = misc_lib(2, 10)
        cr_over_box5.Text = misc_edges(2)

        misctext_3_1.Text = misc_attn_names(3)
        misctext_3_2.Text = misc_lib(3, 1)
        misctext_3_3.Text = misc_lib(3, 2)
        misctext_3_4.Text = misc_lib(3, 3)
        misctext_3_5.Text = misc_lib(3, 4)
        misctext_3_6.Text = misc_lib(3, 5)
        misctext_3_7.Text = misc_lib(3, 6)
        misctext_3_8.Text = misc_lib(3, 7)
        misctext_3_9.Text = misc_lib(3, 8)
        misctext_3_10.Text = misc_lib(3, 9)
        misctext_3_11.Text = misc_lib(3, 10)
        cr_over_box6.Text = misc_edges(3)
    End Sub

    Sub update_attn_parms()
        actinide_names(1) = TextBox1.Text
        actinide_names(2) = TextBox2.Text
        actinide_names(3) = TextBox3.Text
        actinide_names(4) = TextBox4.Text
        actinide_names(5) = TextBox5.Text
        actinide_names(6) = TextBox6.Text
        actinide_names(7) = TextBox7.Text
        actinide_names(8) = TextBox8.Text
        actinide_names(9) = TextBox9.Text
        actinide_names(10) = TextBox10.Text

        amu_lib(1, 1) = TextBox11.Text
        amu_lib(1, 2) = TextBox21.Text
        amu_lib(1, 3) = TextBox31.Text
        amu_lib(1, 4) = TextBox41.Text
        amu_lib(1, 5) = TextBox51.Text
        amu_lib(1, 6) = TextBox61.Text
        amu_lib(1, 7) = TextBox71.Text
        amu_lib(1, 8) = TextBox81.Text

        amu_lib(2, 1) = TextBox12.Text
        amu_lib(2, 2) = TextBox22.Text
        amu_lib(2, 3) = TextBox32.Text
        amu_lib(2, 4) = TextBox42.Text
        amu_lib(2, 5) = TextBox52.Text
        amu_lib(2, 6) = TextBox62.Text
        amu_lib(2, 7) = TextBox72.Text
        amu_lib(2, 8) = TextBox82.Text

        amu_lib(3, 1) = TextBox13.Text
        amu_lib(3, 2) = TextBox23.Text
        amu_lib(3, 3) = TextBox33.Text
        amu_lib(3, 4) = TextBox43.Text
        amu_lib(3, 5) = TextBox53.Text
        amu_lib(3, 6) = TextBox63.Text
        amu_lib(3, 7) = TextBox73.Text
        amu_lib(3, 8) = TextBox83.Text

        amu_lib(4, 1) = TextBox14.Text
        amu_lib(4, 2) = TextBox24.Text
        amu_lib(4, 3) = TextBox34.Text
        amu_lib(4, 4) = TextBox44.Text
        amu_lib(4, 5) = TextBox54.Text
        amu_lib(4, 6) = TextBox64.Text
        amu_lib(4, 7) = TextBox74.Text
        amu_lib(4, 8) = TextBox84.Text

        amu_lib(5, 1) = TextBox15.Text
        amu_lib(5, 2) = TextBox25.Text
        amu_lib(5, 3) = TextBox35.Text
        amu_lib(5, 4) = TextBox45.Text
        amu_lib(5, 5) = TextBox55.Text
        amu_lib(5, 6) = TextBox65.Text
        amu_lib(5, 7) = TextBox75.Text
        amu_lib(5, 8) = TextBox85.Text

        amu_lib(6, 1) = TextBox16.Text
        amu_lib(6, 2) = TextBox26.Text
        amu_lib(6, 3) = TextBox36.Text
        amu_lib(6, 4) = TextBox46.Text
        amu_lib(6, 5) = TextBox56.Text
        amu_lib(6, 6) = TextBox66.Text
        amu_lib(6, 7) = TextBox76.Text
        amu_lib(6, 8) = TextBox86.Text

        amu_lib(7, 1) = TextBox17.Text
        amu_lib(7, 2) = TextBox27.Text
        amu_lib(7, 3) = TextBox37.Text
        amu_lib(7, 4) = TextBox47.Text
        amu_lib(7, 5) = TextBox57.Text
        amu_lib(7, 6) = TextBox67.Text
        amu_lib(7, 7) = TextBox77.Text
        amu_lib(7, 8) = TextBox87.Text

        amu_lib(8, 1) = TextBox18.Text
        amu_lib(8, 2) = TextBox28.Text
        amu_lib(8, 3) = TextBox38.Text
        amu_lib(8, 4) = TextBox48.Text
        amu_lib(8, 5) = TextBox58.Text
        amu_lib(8, 6) = TextBox68.Text
        amu_lib(8, 7) = TextBox78.Text
        amu_lib(8, 8) = TextBox88.Text

        amu_lib(9, 1) = TextBox19.Text
        amu_lib(9, 2) = TextBox29.Text
        amu_lib(9, 3) = TextBox39.Text
        amu_lib(9, 4) = TextBox49.Text
        amu_lib(9, 5) = TextBox59.Text
        amu_lib(9, 6) = TextBox69.Text
        amu_lib(9, 7) = TextBox79.Text
        amu_lib(9, 8) = TextBox89.Text

        amu_lib(10, 1) = TextBox20.Text
        amu_lib(10, 2) = TextBox30.Text
        amu_lib(10, 3) = TextBox40.Text
        amu_lib(10, 4) = TextBox50.Text
        amu_lib(10, 5) = TextBox60.Text
        amu_lib(10, 6) = TextBox70.Text
        amu_lib(10, 7) = TextBox80.Text
        amu_lib(10, 8) = TextBox90.Text


        act_norm_factor(1) = TextBox91.Text
        act_norm_factor(2) = TextBox92.Text
        act_norm_factor(3) = TextBox93.Text
        act_norm_factor(4) = TextBox94.Text
        act_norm_factor(5) = TextBox95.Text
        act_norm_factor(6) = TextBox96.Text
        act_norm_factor(7) = TextBox97.Text
        act_norm_factor(8) = TextBox98.Text
        act_norm_factor(9) = TextBox99.Text
        act_norm_factor(10) = TextBox100.Text

        act_norm_err(1) = TextBox391.Text
        act_norm_err(2) = TextBox392.Text
        act_norm_err(3) = TextBox393.Text
        act_norm_err(4) = TextBox394.Text
        act_norm_err(5) = TextBox395.Text
        act_norm_err(6) = TextBox396.Text
        act_norm_err(7) = TextBox397.Text
        act_norm_err(8) = TextBox398.Text
        act_norm_err(9) = TextBox399.Text
        act_norm_err(10) = TextBox400.Text

        edges(1) = TextBox201.Text
        edges(2) = TextBox202.Text
        edges(3) = TextBox203.Text
        edges(4) = TextBox204.Text
        edges(5) = TextBox205.Text
        edges(6) = TextBox206.Text
        edges(7) = TextBox207.Text
        edges(8) = TextBox208.Text
        edges(9) = TextBox209.Text
        edges(10) = TextBox210.Text

        container_names(1) = TextBox101.Text
        cmu_lib(1, 1) = TextBox102.Text
        cmu_lib(1, 2) = TextBox103.Text
        cmu_lib(1, 3) = TextBox104.Text
        cmu_lib(1, 4) = TextBox105.Text
        cmu_lib(1, 5) = TextBox106.Text
        cmu_lib(1, 6) = TextBox107.Text
        cmu_lib(1, 7) = TextBox108.Text
        cmu_lib(1, 8) = TextBox109.Text
        cmu_lib(1, 9) = TextBox110a.Text
        cmu_lib(1, 10) = TextBox111a.Text
        cont_edges(1) = cr_over_box1.Text


        container_names(2) = TextBox110.Text
        cmu_lib(2, 1) = TextBox111.Text
        cmu_lib(2, 2) = TextBox112.Text
        cmu_lib(2, 3) = TextBox113.Text
        cmu_lib(2, 4) = TextBox114.Text
        cmu_lib(2, 5) = TextBox115.Text
        cmu_lib(2, 6) = TextBox116.Text
        cmu_lib(2, 7) = TextBox117.Text
        cmu_lib(2, 8) = TextBox118.Text
        cmu_lib(2, 9) = TextBox120a.Text
        cmu_lib(2, 10) = TextBox121a.Text
        cont_edges(2) = cr_over_box2.Text

        solution_names(1) = TextBox119.Text
        sol_lib(1, 1) = TextBox120.Text
        sol_lib(1, 2) = TextBox121.Text
        sol_lib(1, 3) = TextBox122.Text
        sol_lib(1, 4) = TextBox123.Text
        sol_lib(1, 5) = TextBox124.Text
        sol_lib(1, 6) = TextBox125.Text
        sol_lib(1, 7) = TextBox126.Text
        sol_lib(1, 8) = TextBox127.Text
        sol_lib(1, 9) = TextBox130a.Text
        sol_lib(1, 10) = TextBox131a.Text
        sol_edges(1) = cr_over_box3.Text

        lor_width(1) = TextBox211.Text
        lor_width(2) = TextBox212.Text
        lor_width(3) = TextBox213.Text
        lor_width(4) = TextBox214.Text
        lor_width(5) = TextBox215.Text
        lor_width(6) = TextBox216.Text
        lor_width(7) = TextBox217.Text
        lor_width(8) = TextBox218.Text
        lor_width(9) = TextBox219.Text
        lor_width(10) = TextBox220.Text

        element_priority(1) = TextBox221.Text   ' select which ones are available to the fitting program
        element_priority(2) = TextBox222.Text
        element_priority(3) = TextBox223.Text
        element_priority(4) = TextBox224.Text
        element_priority(5) = TextBox225.Text
        element_priority(6) = TextBox226.Text
        element_priority(7) = TextBox227.Text
        element_priority(8) = TextBox228.Text
        element_priority(9) = TextBox229.Text
        element_priority(10) = TextBox230.Text

        element_symbol(1) = TextBox231.Text
        element_symbol(2) = TextBox232.Text
        element_symbol(3) = TextBox233.Text
        element_symbol(4) = TextBox234.Text
        element_symbol(5) = TextBox235.Text
        element_symbol(6) = TextBox236.Text
        element_symbol(7) = TextBox237.Text
        element_symbol(8) = TextBox238.Text
        element_symbol(9) = TextBox239.Text
        element_symbol(10) = TextBox240.Text

        misc_attn_names(1) = misctext_1_1.Text
        misc_lib(1, 1) = misctext_1_2.Text
        misc_lib(1, 2) = misctext_1_3.Text
        misc_lib(1, 3) = misctext_1_4.Text
        misc_lib(1, 4) = misctext_1_5.Text
        misc_lib(1, 5) = misctext_1_6.Text
        misc_lib(1, 6) = misctext_1_7.Text
        misc_lib(1, 7) = misctext_1_8.Text
        misc_lib(1, 8) = misctext_1_9.Text
        misc_lib(1, 9) = misctext_1_10.Text
        misc_lib(1, 10) = misctext_1_11.Text
        misc_edges(1) = cr_over_box4.Text

        misc_attn_names(2) = misctext_2_1.Text
        misc_lib(2, 1) = misctext_2_2.Text
        misc_lib(2, 2) = misctext_2_3.Text
        misc_lib(2, 3) = misctext_2_4.Text
        misc_lib(2, 4) = misctext_2_5.Text
        misc_lib(2, 5) = misctext_2_6.Text
        misc_lib(2, 6) = misctext_2_7.Text
        misc_lib(2, 7) = misctext_2_8.Text
        misc_lib(2, 8) = misctext_2_9.Text
        misc_lib(2, 9) = misctext_2_10.Text
        misc_lib(2, 10) = misctext_2_11.Text
        misc_edges(2) = cr_over_box5.Text

        misc_attn_names(3) = misctext_3_1.Text
        misc_lib(3, 1) = misctext_3_2.Text
        misc_lib(3, 2) = misctext_3_3.Text
        misc_lib(3, 3) = misctext_3_4.Text
        misc_lib(3, 4) = misctext_3_5.Text
        misc_lib(3, 5) = misctext_3_6.Text
        misc_lib(3, 6) = misctext_3_7.Text
        misc_lib(3, 7) = misctext_3_8.Text
        misc_lib(3, 8) = misctext_3_9.Text
        misc_lib(3, 9) = misctext_3_10.Text
        misc_lib(3, 10) = misctext_3_11.Text
        misc_edges(3) = cr_over_box6.Text
    End Sub
End Class