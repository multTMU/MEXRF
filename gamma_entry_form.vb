Imports System.IO
Imports System.Text
Public Class Gamma_ray_data_entry
    Public tbx1, tbx2, tbx3, tbx4, tbx5, tbx6

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '  
        '    save fission product library in a CSV file format with new file name
        '
        Dim klineout, temp_name As String
        Dim j, k As Integer

        'update paraemters with screen values
        Call update_gamma_lib_parms()


        SaveFileDialog1.Filter = "TXT Files (*.csv*)|*.csv"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
       Then
            My.Computer.FileSystem.WriteAllText _
         (SaveFileDialog1.FileName, "", True)

            temp_name = SaveFileDialog1.FileName

            klineout = ""

            Dim fs As FileStream = File.Create(temp_name)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
            fs.Write(info, 0, info.Length)

            fs.Close()


            klineout = temp_name & vbCrLf
            My.Computer.FileSystem.WriteAllText(temp_name, klineout, True)
            '  out_attn_csv file format; each line contains   identifier, energy, intensity, width

            For i = 1 To 5
                klineout = fil_label_g(i) & ", " & fiss_prod_name(i) & ", " & gamma_pts(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(temp_name, klineout, True)

            Next i

            For i = 1 To 3
                For j = 1 To 10
                    k = (i - 1) * 10 + j
                    klineout = fil_label_g(k + 5) & ", " & gamma_energy(i, j) & ", " & gamma_int(i, j) & vbCrLf
                    My.Computer.FileSystem.WriteAllText(temp_name, klineout, True)
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
        Dim gamma_lib(3, 10), tempsum

        Call update_gamma_lib_parms()


        klineout = ""
        outconstants = exe_dir_name & "\XRF_gamma_lib_default.txt"
        klineout = outconstants & vbCrLf
        Dim fs As FileStream = File.Create(outconstants)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        For i = 1 To 5
            klineout = "a1" & " " & gamma_pts(i) & " " & 0.0 & vbCrLf
            My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
        Next i

        For i = 1 To 3

            For j = 1 To 10

                klineout = "a1" & " " & gamma_energy(i, j) & " " & gamma_int(i, j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outconstants, klineout, True)
            Next j

        Next i

        '  Next l1


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        '  
        '    Save updated fission produce library in a CSV file format default file name
        '
        Dim klineout, temp_name As String
        Dim j, k As Integer

        'update paraemters with screen values
        Call update_gamma_lib_parms()

        gamma_ray_lib_name = "default_gamma_lib.csv"
        temp_name = gamma_ray_lib_path & gamma_ray_lib_name


        klineout = ""

        Dim fs As FileStream = File.Create(temp_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)

        fs.Close()

        klineout = temp_name & vbCrLf
        My.Computer.FileSystem.WriteAllText(temp_name, klineout, True)
        '  out_attn_csv file format; each line contains   identifier, energy, intensity, width

        For i = 1 To 5
            klineout = fil_label_g(i) & ", " & fiss_prod_name(i) & ", " & gamma_pts(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(temp_name, klineout, True)

        Next i

        For i = 1 To 3
            For j = 1 To 10
                k = (i - 1) * 10 + j
                klineout = fil_label_g(k + 5) & ", " & gamma_energy(i, j) & ", " & gamma_int(i, j) & vbCrLf
                My.Computer.FileSystem.WriteAllText(temp_name, klineout, True)
            Next j

        Next i



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        ' Open and read a csv file containing customized gamma-ray data for fission produces
        '
        Dim gamma_ray_lib As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = gamma_ray_lib_path
        openFileDialog1.Filter = "txt files (*.csv)|*.csv"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    gamma_ray_lib = openFileDialog1.FileName

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

        Call get_gamma_data_file(gamma_ray_lib)

        Call update_gamma_screen()


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbx1 = {TextBox101, TextBox201, TextBox301, TextBox401, TextBox501, TextBox601, TextBox701, TextBox801, TextBox901, TextBox1001}
        tbx2 = {TextBox102, TextBox202, TextBox302, TextBox402, TextBox502, TextBox602, TextBox702, TextBox802, TextBox902, TextBox1002}
        tbx3 = {TextBox103, TextBox203, TextBox303, TextBox403, TextBox503, TextBox603, TextBox703, TextBox803, TextBox903, TextBox1003}
        tbx4 = {TextBox104, TextBox204, TextBox304, TextBox404, TextBox504, TextBox604, TextBox704, TextBox804, TextBox904, TextBox1004}
        tbx5 = {TextBox105, TextBox205, TextBox305, TextBox405, TextBox505, TextBox605, TextBox705, TextBox805, TextBox905, TextBox1005}
        tbx6 = {TextBox106, TextBox206, TextBox306, TextBox406, TextBox506, TextBox606, TextBox706, TextBox806, TextBox906, TextBox1006}
        Dim temp_name As String

        gamma_ray_lib_name = "default_gamma_lib.csv"
        temp_name = gamma_ray_lib_path & gamma_ray_lib_name
        Call get_gamma_data_file(temp_name)

        Call update_gamma_screen()

    End Sub

    Sub update_gamma_lib_parms()
        fiss_prod_name(1) = isotope_1_box.Text
        fiss_prod_name(2) = isotope_2_box.Text
        fiss_prod_name(3) = isotope_3_box.Text

        gamma_pts(1) = gamma_lines_box1.Text
        gamma_pts(2) = gamma_lines_box2.Text
        gamma_pts(3) = gamma_lines_box3.Text

        For i = 1 To 10
            gamma_energy(1, i) = tbx1(i - 1).Text
            gamma_int(1, i) = tbx2(i - 1).Text
            gamma_energy(2, i) = tbx3(i - 1).Text
            gamma_int(2, i) = tbx4(i - 1).Text
            gamma_energy(3, i) = tbx5(i - 1).Text
            gamma_int(3, i) = tbx6(i - 1).Text
        Next i

    End Sub

    Sub update_gamma_screen()

        isotope_1_box.Text = fiss_prod_name(1)
        isotope_2_box.Text = fiss_prod_name(2)
        isotope_3_box.Text = fiss_prod_name(3)

        gamma_lines_box1.Text = gamma_pts(1)
        gamma_lines_box2.Text = gamma_pts(2)
        gamma_lines_box3.Text = gamma_pts(3)

        For i = 1 To 10
            tbx1(i - 1).Text = gamma_energy(1, i)
            tbx2(i - 1).Text = gamma_int(1, i)
            tbx3(i - 1).Text = gamma_energy(2, i)
            tbx4(i - 1).Text = gamma_int(2, i)
            tbx5(i - 1).Text = gamma_energy(3, i)
            tbx6(i - 1).Text = gamma_int(3, i)
        Next i

    End Sub
End Class