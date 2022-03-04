Imports System.IO
Imports System.Text
Public Class Xray_Data_entry


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '  
        '    Save updated attenuation parameters in a CSV file format default file name
        '
        Dim klineout As String
        Dim j, k As Integer

        'update paraemters with screen values
        Call update_xray_lib_parms()

        out_xray_data_csv = xrf_vms_consts_dir & "XRF_branching_ratios_default.csv"

        klineout = ""

        Dim fs As FileStream = File.Create(out_xray_data_csv)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        klineout = out_xray_data_csv & vbCrLf
        My.Computer.FileSystem.WriteAllText(out_xray_data_csv, klineout, True)
        '  out_attn_csv file format; each line contains   identifier, energy, intensity, width

        For i = 1 To 10

            For j = 1 To 15
                k = (i - 1) * 15 + j
                klineout = fil_label_X(k) & ", " & xray_Lib(i, j, 1) & ", " & xray_Lib(i, j, 2) & ", " & xray_Lib(i, j, 3) & vbCrLf
                My.Computer.FileSystem.WriteAllText(out_xray_data_csv, klineout, True)
            Next j

        Next i

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        ' Open and read a csv file containing customized attenuation coefficients
        '
        Dim new_xray_data_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = root_dir_name & "xrf_files\"
        openFileDialog1.Filter = "txt files (*.csv)|*.csv"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    new_xray_data_file_name = openFileDialog1.FileName

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

        Call get_xray_data_file(new_xray_data_file_name, fil_label_X)

        Call form15update()
             End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '  
        '    save attenuation parameters in a CSV file format with new file name
        '
        Dim klineout As String
        Dim j, k As Integer

        'update paraemters with screen values
        Call update_xray_lib_parms()


        SaveFileDialog1.Filter = "TXT Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)

            out_xray_data_csv = SaveFileDialog1.FileName

            klineout = ""

            Dim fs As FileStream = File.Create(out_xray_data_csv)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()


            klineout = out_xray_data_csv & vbCrLf
            My.Computer.FileSystem.WriteAllText(out_xray_data_csv, klineout, True)
            '  out_attn_csv file format; each line contains   identifier, energy, intensity, width

            For i = 1 To 10

                For j = 1 To 15
                    k = (i - 1) * 15 + j
                    klineout = fil_label_X(k) & ", " & xray_Lib(i, j, 1) & ", " & xray_Lib(i, j, 2) & ", " & xray_Lib(i, j, 3) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(out_xray_data_csv, klineout, True)
                Next j

            Next i

        End If
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '
        '  convert csv file to format readable by FORTRAN executble
        '
        Dim klineout, outconstants As String
        Dim i, j, k, element_index(10) As Integer
        Dim norm_lib(10, 15), tempsum
        Call update_xray_lib_parms()
        For i = 1 To 10
            tempsum = xray_Lib(i, 1, 2)
            For j = 1 To 15
                If xray_Lib(i, j, 2) > tempsum Then tempsum = xray_Lib(i, j, 2)
            Next j
            For j = 1 To 15
                norm_lib(i, j) = xray_Lib(i, j, 2) / tempsum * 100
            Next j
        Next i


        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i

        ' For l1 = 0 To 0
        klineout = ""
            outconstants = exe_dir_name & "\XRF_branching_ratios_default.txt"
            klineout = outconstants & vbCrLf
            Dim fs As FileStream = File.Create(outconstants)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()
            '      klineout = outconstants & vbCrLf
            '    My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            '  out_attn_csv file format; each line contains   identifier, energy, intensity, width



            For i = 1 To 10

                For j = 1 To 15
                    k = (i - 1) * 15 + j
                    klineout = "a1" & " " & xray_Lib(i, j, 1) & " " & xray_Lib(i, j, 2) & " " & xray_Lib(i, j, 3) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
                Next j

            Next i

        '  Next l1


    End Sub


    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        ' Read in seed parameters from generic file


        '   xray_data_file_name = "c:\MEXRF\xrf_files\XRF_branching_ratios_default.csv"

        Call get_xray_data_file(xray_data_file_name, fil_label_X)

        Call form15update()




    End Sub



    Sub form15update()
        Dim br_sum(10)

        Element_1_box.Text = "Uranium"
        Element_2_box.Text = "Plutonium"
        Element_3_box.Text = "Neptunium"
        Element_4_box.Text = "Curium"
        Element_5_Box.Text = "Americium"
        Element_6_box.Text = "spare"


        Dim testString As String
        For jt = 1 To 3                                                                             ' for each type - Energy, Branching ratio, lorentz width
            For je = 1 To 3                                                                         ' for each element

                For i = 1 To 13                                                                     ' for each line
                    testString = "TextBox" & 800 + 10 * (i - 1) + 1 + (je - 1) * 3 + (jt - 1)

                    Dim TB As TextBox = Nothing
                    Dim Ctr As Control = Controls(testString)

                    If TypeOf Ctr Is TextBox Then
                        TB = DirectCast(Ctr, TextBox)
                        TB.Text = xray_Lib(je, i, jt)
                    End If

                    ' If TB IsNot Nothing Then MessageBox.Show("Found")
                Next i

            Next je
        Next jt


        For jt = 1 To 3                                                                             ' for each type - Energy, Branching ratio, lorentz width
            For je = 1 To 2                                                                         ' for each element

                For i = 1 To 10                                                                     ' for each line
                    testString = "TextBox" & 600 + 10 * (i - 1) + 1 + (je - 1) * 3 + (jt - 1)

                    Dim TB As TextBox = Nothing
                    Dim Ctr As Control = Controls(testString)

                    If TypeOf Ctr Is TextBox Then
                        TB = DirectCast(Ctr, TextBox)
                        TB.Text = xray_Lib(je + 3, i, jt)
                    End If

                    ' If TB IsNot Nothing Then MessageBox.Show("Found")
                Next i

            Next je
        Next jt


        For i = 1 To 5
            br_sum(i) = 0
        Next i

        For i = 1 To 5
            For j = 1 To 10
                br_sum(i) = br_sum(i) + xray_Lib(i, j, 2)
            Next j
        Next i



        TextBox1.Text = br_sum(1)
        TextBox2.Text = br_sum(2)
        TextBox3.Text = br_sum(3)
        TextBox4.Text = br_sum(4)
        TextBox5.Text = br_sum(5)



    End Sub

    Private Property TextBox(index_box As String) As Object
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Object)
            Throw New NotImplementedException()
        End Set
    End Property

    Sub update_xray_lib_parms()



        '     element_symbol(1) = TextBox231.Text


        Dim testString As String
        For jt = 1 To 3                                                                             ' for each type - Energy, Branching ratio, lorentz width
            For je = 1 To 3                                                                         ' for each element

                For i = 1 To 13                                                                     ' for each line
                    testString = "TextBox" & 800 + 10 * (i - 1) + 1 + (je - 1) * 3 + (jt - 1)

                    Dim TB As TextBox = Nothing
                    Dim Ctr As Control = Controls(testString)

                    If TypeOf Ctr Is TextBox Then
                        TB = DirectCast(Ctr, TextBox)
                        xray_Lib(je, i, jt) = TB.Text
                    End If

                    ' If TB IsNot Nothing Then MessageBox.Show("Found")
                Next i

            Next je
        Next jt


        For jt = 1 To 3                                                                             ' for each type - Energy, Branching ratio, lorentz width
            For je = 1 To 3                                                                         ' for each element

                For i = 1 To 10                                                                     ' for each line
                    testString = "TextBox" & 200 + 10 * (i - 1) + 1 + (je - 1) * 3 + (jt - 1)

                    Dim TB As TextBox = Nothing
                    Dim Ctr As Control = Controls(testString)

                    If TypeOf Ctr Is TextBox Then
                        TB = DirectCast(Ctr, TextBox)
                        xray_Lib(je, i, jt) = TB.Text
                    End If

                    ' If TB IsNot Nothing Then MessageBox.Show("Found")
                Next i

            Next je
        Next jt

    End Sub


End Class