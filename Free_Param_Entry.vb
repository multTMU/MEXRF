Imports System.IO
Imports System.Text
Public Class Free_Param_entry
    Public ichecked(42) As Integer
    Public pchecked(42) As Integer
    Public seed_select As Integer
    Public initval2(42)


    Public Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim element_index(10)
        ' determine where the various elements are defined
        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i
        Intensities_ListBox.Items(0) = element_symbol(element_index(1))
        Intensities_ListBox.Items(1) = element_symbol(element_index(2))
        Intensities_ListBox.Items(2) = element_symbol(element_index(3))
        Intensities_ListBox.Items(3) = element_symbol(element_index(4))
        Intensities_ListBox.Items(4) = element_symbol(element_index(5))

    End Sub
    Public Sub seed_parms()
        ' seeds parameter form from file

        Dim n_parms As Integer
        Dim icontinue As Integer
        Dim temp_file_name As String
        Dim str00 As String
        Dim str11 As String
        Dim i11 As Integer
        Dim i12 As Integer
        Dim i13 As Integer
        Dim i14 As Integer
        '    Dim seed_select As Integer
        '    Dim ichecked(36) As Integer
        Dim seed_file_name As String
        Dim temp_seed_name As String
        Dim seed_name As String

        '   MsgBox("selected 2: " & seed_select)

        seed_name = exe_dir_name & "\kfit_seed.txt"
        temp_seed_name = exe_dir_name & "\temp_seed.txt"
        seed_file_name = exe_dir_name & "\kfit_seed.txt"

        If seed_select = 0 Then Call gen_seed_from_last_results()
        If seed_select = 1 Then Call gen_seed_from_results_file(1)
        If seed_select = 2 Then Call gen_seed_from_screen()
        If seed_select = 3 Then Call estimate_seed(1)
        If seed_select = 4 Then Call gen_seed_from_file()
        If seed_select = 5 Then Call estimate_seed(0)

        GoTo 200
        Dim fileReader As System.IO.StreamReader
        fileReader =
        My.Computer.FileSystem.OpenTextFileReader(seed_file_name)
        Dim stringReader As String
        inlineval = fileReader.ReadLine()
        '    MsgBox(inlineval)
        n_parms = 42
        '   inlineval = fileReader.ReadLine()
        temp_file_name = inlineval
        For i = 1 To n_parms
            ' stringReader = fileReader.ReadLine()
            inlineval = fileReader.ReadLine()
            If inlineval = "end" Then GoTo 100
            initval(i) = inlineval
            initval2(i) = inlineval
            '   
        Next i
100:    icontinue = 1

        For j = 1 To n_parms
            str00 = initval2(j)
            ichecked(j) = Val(Strings.Right(str00, 2))
            i11 = Strings.InStr(str00, " ")
            i12 = Strings.Len(str00)
            i13 = i12 - i11
            ' i14 = i13 - i12 - i11 + 1
            str11 = Strings.Right(str00, i13)

            i11 = Strings.InStr(str11, " ")
            i12 = Strings.Len(str11)
            i13 = i12 - i11
            str00 = Strings.Right(str11, i13)

            i11 = Strings.InStr(str00, " ")
            str11 = Strings.Left(str00, i11)
            initval2(j) = Val(str11)

            '   MsgBox(i11 & " , " & i12 & "," & i13 & "," & i14)
        Next j

        '   MsgBox(ichecked(1) & " " & ichecked(2))

200:       ' temp_fileBox1 = temp_file_name


        '   MsgBox(initval2(15) & " , " & results_arr(15))
        '  MsgBox(initval2(28) & " , " & results_arr(28))

        Dim element_index(10)
        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i

        Intensities_ListBox.Items(0) = element_symbol(element_index(1))
        Intensities_ListBox.Items(1) = element_symbol(element_index(2))
        Intensities_ListBox.Items(2) = element_symbol(element_index(3))
        Intensities_ListBox.Items(3) = element_symbol(element_index(4))
        Intensities_ListBox.Items(4) = element_symbol(element_index(5))


        ActBox1.Text = initval2(1)
        ActBox2.Text = initval2(2)
        ActBox3.Text = initval2(3)
        ActBox4.Text = initval2(4)
        ActBox5.Text = initval2(5)
        ActBox6.Text = initval2(6)
        ActBox7.Text = initval2(7)
        ActBox8.Text = initval2(8)
        ActBox9.Text = initval2(9)
        ActBox10.Text = initval2(10)
        ActBox11.Text = initval2(11)
        ActBox12.Text = initval2(12)
        ActBox13.Text = initval2(13)
        ActBox14.Text = initval2(14)
        ActBox15.Text = initval2(15)
        ActBox16.Text = initval2(16)
        ActBox17.Text = initval2(17)
        ActBox18.Text = initval2(18)
        ActBox19.Text = initval2(19)
        ActBox20.Text = initval2(20)
        ActBox21.Text = initval2(21)
        ActBox22.Text = initval2(22)
        ActBox23.Text = initval2(23)
        ActBox24.Text = initval2(24)
        ActBox25.Text = initval2(25)
        ActBox26.Text = initval2(26)
        ActBox27.Text = initval2(27)
        ActBox28.Text = initval2(28)
        ActBox29.Text = initval2(29)
        ActBox30.Text = initval2(30)
        '
        ActBox31.Text = initval2(31)
        ActBox32.Text = initval2(32)
        ActBox33.Text = initval2(33)
        ActBox34.Text = initval2(34)
        ActBox35.Text = initval2(35)
        ActBox36.Text = initval2(36)
        '
        ActBox37.Text = initval2(37)
        ActBox38.Text = initval2(38)
        ActBox39.Text = initval2(39)
        ActBox40.Text = initval2(40)
        ActBox41.Text = initval2(41)
        ActBox42.Text = initval2(42)

        pchecked(1) = ichecked(1)
        pchecked(2) = ichecked(2)
        pchecked(3) = ichecked(3)
        pchecked(4) = ichecked(4)
        pchecked(5) = ichecked(5)
        pchecked(6) = ichecked(6)
        pchecked(7) = ichecked(7)
        pchecked(8) = ichecked(8)
        pchecked(9) = ichecked(9)
        pchecked(10) = ichecked(10)

        pchecked(11) = ichecked(11)
        pchecked(12) = ichecked(12)
        pchecked(13) = ichecked(13)
        pchecked(14) = ichecked(14)
        pchecked(15) = ichecked(15)
        pchecked(16) = ichecked(16)
        pchecked(17) = ichecked(17)
        pchecked(18) = ichecked(18)
        pchecked(19) = ichecked(19)
        pchecked(20) = ichecked(20)

        pchecked(21) = ichecked(21)
        pchecked(22) = ichecked(22)
        pchecked(23) = ichecked(23)
        pchecked(24) = ichecked(24)
        pchecked(25) = ichecked(25)
        pchecked(26) = ichecked(26)
        pchecked(27) = ichecked(27)
        pchecked(28) = ichecked(28)
        pchecked(29) = ichecked(29)
        pchecked(30) = ichecked(30)

        pchecked(31) = ichecked(31)
        pchecked(32) = ichecked(32)
        pchecked(33) = ichecked(33)
        pchecked(34) = ichecked(34)
        pchecked(35) = ichecked(35)
        pchecked(36) = ichecked(36)

        pchecked(37) = ichecked(37)
        pchecked(38) = ichecked(38)
        pchecked(39) = ichecked(39)
        pchecked(40) = ichecked(40)
        pchecked(41) = ichecked(41)
        pchecked(42) = ichecked(42)

        For i = 0 To 4
            If pchecked(i + 1) = 1 Then Intensities_ListBox.SetItemChecked(i, True) Else Intensities_ListBox.SetItemChecked(i, False)
        Next i

        For i = 0 To 2
            If pchecked(i + 6) = 1 Then FP_ListBox.SetItemChecked(i, True) Else FP_ListBox.SetItemChecked(i, False)
        Next i

        For i = 0 To 5
            If pchecked(i + 9) = 1 Then conc_ListBox.SetItemChecked(i, True) Else conc_ListBox.SetItemChecked(i, False)
        Next i

        For i = 0 To 9
            If pchecked(i + 15) = 1 Then spec_par_ListBox.SetItemChecked(i, True) Else spec_par_ListBox.SetItemChecked(i, False)
        Next i

        For i = 0 To 11
            If pchecked(i + 25) = 1 Then Bremms_ListBox.SetItemChecked(i, True) Else Bremms_ListBox.SetItemChecked(i, False)
        Next i

        For i = 0 To 5
            If pchecked(i + 37) = 1 Then AdditionalPars_ListBox.SetItemChecked(i, True) Else AdditionalPars_ListBox.SetItemChecked(i, False)
        Next i

    End Sub

    Public Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Intensities_ListBox.SelectedIndexChanged

    End Sub

    Private Sub CheckedListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Bremms_ListBox.SelectedIndexChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub CheckedListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles spec_par_ListBox.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call checkbox_1()


    End Sub


    Public free_params(42) As Integer
    Public check_params(42) As Integer
    Public Par(42)
    Public initval(42)
    Public initval1
    Public temp_file_root As String

    Public Sub checkbox_1()
        ' reads check boxes on parameter input form
        ' reads seed data from masked checkboxes on input form
        ' creates parameter file for input to XRF_FIT

        Dim i As Integer
        Dim s As String
        Dim infile_name As String
        Dim param As Integer
        Dim param0 As Integer
        Dim klineout As String

        GoTo skip_this

        For i = 1 To 42
            free_params(i) = 0
            check_params(i) = 0
            Par(i) = 0.0
        Next i

        param = 0


        s = "Checked Items:" & ControlChars.CrLf
        For i = 0 To (Intensities_ListBox.Items.Count - 1)
            If Intensities_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & Intensities_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1) = 1
                param = param + 1
            End If
        Next
        param0 = param


        For i = 0 To (FP_ListBox.Items.Count - 1)
            If FP_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & FP_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 5) = 1
                param = param + 1
            End If
        Next

        For i = 0 To (conc_ListBox.Items.Count - 1)
            If conc_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & conc_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 8) = 1
                param = param + 1
            End If
        Next

        For i = 0 To (spec_par_ListBox.Items.Count - 1)
            If spec_par_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & spec_par_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 14) = 1
                param = param + 1
            End If
        Next
        param0 = param + param0

        For i = 0 To (Bremms_ListBox.Items.Count - 1)
            If Bremms_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & Bremms_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 24) = 1
                param = param + 1
            End If
        Next

        param0 = param + param0
        For i = 0 To (AdditionalPars_ListBox.Items.Count - 1)
            If AdditionalPars_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & AdditionalPars_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 36) = 1
                param = param + 1
            End If
        Next

        '      MsgBox("Total Free Parameters = " & param)
        MessageBox.Show(s)
        infile_name = "testfile"
        '   Call Main()
        Par(1) = ActBox1.Text
        Par(2) = ActBox2.Text
        Par(3) = ActBox3.Text
        Par(4) = ActBox4.Text
        Par(5) = ActBox5.Text
        Par(6) = ActBox6.Text
        Par(7) = ActBox7.Text
        Par(8) = ActBox8.Text
        Par(9) = ActBox9.Text
        Par(10) = ActBox10.Text
        '
        Par(11) = ActBox11.Text
        Par(12) = ActBox12.Text
        Par(13) = ActBox13.Text
        Par(14) = ActBox14.Text
        Par(15) = ActBox15.Text
        Par(16) = ActBox16.Text
        Par(17) = ActBox17.Text
        Par(18) = ActBox18.Text
        Par(19) = ActBox19.Text
        Par(20) = ActBox20.Text

        Par(21) = ActBox21.Text
        Par(22) = ActBox22.Text
        Par(23) = ActBox23.Text
        Par(24) = ActBox24.Text
        Par(25) = ActBox25.Text
        Par(26) = ActBox26.Text
        Par(27) = ActBox27.Text
        Par(28) = ActBox28.Text
        Par(29) = ActBox29.Text
        Par(30) = ActBox30.Text
        '
        Par(31) = ActBox31.Text
        Par(32) = ActBox32.Text
        Par(33) = ActBox33.Text
        Par(34) = ActBox34.Text
        Par(35) = ActBox35.Text
        Par(36) = ActBox36.Text

        Par(37) = ActBox37.Text
        Par(38) = ActBox38.Text
        Par(39) = ActBox39.Text
        Par(40) = ActBox40.Text
        Par(41) = ActBox41.Text
        Par(42) = ActBox42.Text

        free_params(1) = check_params(1)
        free_params(2) = check_params(2)
        free_params(3) = check_params(3)
        free_params(4) = check_params(4)
        free_params(5) = check_params(5)
        free_params(6) = check_params(6)
        free_params(7) = check_params(7)
        free_params(8) = check_params(8)
        free_params(9) = check_params(9)
        free_params(10) = check_params(10)

        free_params(11) = check_params(11)
        free_params(12) = check_params(12)
        free_params(13) = check_params(13)
        free_params(14) = check_params(14)
        free_params(15) = check_params(15)
        free_params(16) = check_params(16)
        free_params(17) = check_params(17)
        free_params(18) = check_params(18)
        free_params(19) = check_params(19)
        free_params(20) = check_params(20)

        free_params(21) = check_params(21)
        free_params(22) = check_params(22)
        free_params(23) = check_params(23)
        free_params(24) = check_params(24)
        free_params(25) = check_params(25)
        free_params(26) = check_params(26)
        free_params(27) = check_params(27)
        free_params(28) = check_params(28)
        free_params(29) = check_params(29)
        free_params(30) = check_params(30)

        free_params(31) = check_params(31)
        free_params(32) = check_params(32)
        free_params(33) = check_params(33)
        free_params(34) = check_params(34)
        free_params(35) = check_params(35)
        free_params(36) = check_params(36)

        free_params(37) = check_params(37)
        free_params(38) = check_params(38)
        free_params(39) = check_params(39)
        free_params(40) = check_params(40)
        free_params(41) = check_params(41)
        free_params(42) = check_params(42)

skip_this:

        Call update_parameters()

        'create new parameter file for fitting routine
        seed_select = SourceBox1.SelectedIndex
        '     MsgBox("box num " & seed_select)
        For i_seed = 1 To num_files
            If seed_select = 1 Then
                Call gen_seed_from_results_file(i_seed)
                For i = 1 To 42
                    Par(i) = results_arr(i)
                    free_params(i) = is_free_par(i)
                Next i
            End If
            outparameter = exe_dir_name & "\xfit_parms_" & i_seed - 1 & ".txt" & " "
            Dim fs As FileStream = File.Create(outparameter)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes("spec_fit_" & i_seed - 1 & vbCrLf)
            fs.Write(info, 0, info.Length)

            fs.Close()

            '   klineout = "spec_fit_0" & vbCrLf
            '  My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)

            '  Console.Write(klineout)
            For i = 1 To 42

                klineout = " " & i & " " & Par(i) & "    " & free_params(i) & vbCrLf

                My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)
                '    Console.Write(klineout)
            Next i
        Next i_seed
    End Sub

    Private Sub checkbox_1a()
        ' Determine if there are any items checked.
        If Intensities_ListBox.CheckedItems.Count <> 0 Then
            ' If so, loop through all checked items and print results.
            Dim x As Integer
            Dim s As String = ""
            For x = 0 To Intensities_ListBox.CheckedItems.Count - 1
                s = s & "Checked Item " & (x + 1).ToString & " = " & Intensities_ListBox.CheckedItems(x).ToString & ControlChars.CrLf
            Next x
            MessageBox.Show(s)
        End If


    End Sub


    Public Sub MaskedTextBox1_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActBox1.Click
        ActBox1.Text = initval1
    End Sub


    Private Sub MaskedTextBox3_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ActBox2.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox4_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ActBox4.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox5_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ActBox5.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox6_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ActBox6.MaskInputRejected

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call seed_parms()



    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call update_genric_file()
    End Sub

    Private Sub SourceBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SourceBox1.SelectedIndexChanged
        '    SourceBox1.Items.Clear()
        '    SourceBox1.Items.Add("Use Last Assay Result")      ' item 0
        '    SourceBox1.Items.Add("Use Generic Seed Values")    ' item 1
        '    SourceBox1.Items.Add("Saved Result File")          ' item 2
        '    SourceBox1.Items.Add("Estimate From spectrum")     ' item 3


        '       Dim sindex As Integer
        If SourceBox1.SelectedIndex > -1 Then
            seed_select = SourceBox1.SelectedIndex
            '    Dim source_type As Object
            '    source_type = SourceBox1.SelectedItem
            '    SourceBox1.Items.Add(source_type)
        End If
        '   MsgBox("selected " & seed_select)
    End Sub

    Private Sub gen_seed_from_results_file(i_seed)
        ' create new parameter seed from last assay results files (".rpt") in the specified XRF directory
        '     Dim results_arr(42), err_arr42), covar_array(42, 42)
        Dim temp_check(42)
        Dim results_file_name, init_parm_file_name As String
        Dim n_parms, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim klineout As String
        Dim icontinue As Integer
        Dim chisqr

        Dim str11 As String
        '    For i_seed = 1 To num_files
        '     MsgBox(i_seed & " : " & num_files)
        n_parms = 42
        idex = -2
        jdex = 0

        results_file_name = MEXRF_main.ListBox1.Items(i_seed - 1)
        MsgBox("SEED FILE1 " & results_file_name)
        Call get_split_pars(results_file_name)   ' get parameters from results file from RESULTS directory


        For i = 1 To 42
            temp_check(i) = 1
            If is_free_par(i) = 0 Then temp_check(i) = 0
        Next i

        'create temporary seed file

        '  outparameter = "c:\\MEXRF\XRF_T" & i_seed - 1 & "\xfit_parms.txt"
        outparameter = exe_dir_name & "\xfit_parms_" & i_seed - 1 & ".txt" & " "
        Dim fs As FileStream = File.Create(outparameter)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("spec_fit_" & i_seed - 1 & vbCrLf)
        fs.Write(info, 0, info.Length)

        fs.Close()


        For i = 1 To 42
            initval2(i) = results_arr(i)
            ichecked(i) = temp_check(i)
            klineout = " " & i & " " & results_arr(i) & "    " & temp_check(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)

        Next i


    End Sub


    Private Sub gen_seed_from_last_results()
        ' create new parameter seed from last assay results files (".rpt") in each XRF_# directory
        '     Dim results_arr(42), err_arr(42), covar_array(42, 42)
        Dim temp_check(42)
        Dim results_file_name, init_parm_file_name As String
        Dim n_parms, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim klineout As String
        Dim icontinue, i_seed As Integer
        Dim chisqr

        Dim str11 As String
        For i_seed = 1 To num_files
            '     MsgBox(i_seed & " : " & num_files)
            n_parms = 42
            idex = -2
            jdex = 0

            results_file_name = exe_dir_name & "\spec_fit_" & i_seed - 1 & ".rpt"

            MsgBox("SEED FILE2 " & results_file_name)

            Using MyReader As New Microsoft.VisualBasic.
                            FileIO.TextFieldParser(results_file_name)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(" ")
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Dim currentField As String
                        idex = idex + 1
                        jdex = 0
                        kdex = 0
                        '                 If idex > n_parms Then GoTo 100
                        For Each currentField In currentRow
                            jdex = jdex + 1
                            str11 = currentField
                            If Strings.Left(str11, 1) = "" Then GoTo 50
                            kdex = kdex + 1
                            If idex = -1 Then init_parm_file_name = str11
                            If idex = -1 Then GoTo 50

                            If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                            If idex = 0 And jdex > 10 Then GoTo 50

                            If idex > 0 And idex < 43 And kdex = 1 Then i_temp = Val(str11)
                            If idex > 0 And idex < 43 And kdex = 2 Then results_arr(idex) = Val(str11)
                            If idex > 0 And idex < 43 And kdex = 3 Then err_arr(idex) = Val(str11)

                            If idex > 42 Then GoTo 100
                            If idex > 42 And kdex = 1 Then i_temp = Val(str11)
                            If idex > 42 And kdex = 2 Then j_temp = Val(str11)
                            If idex > 42 And kdex = 3 Then covar_array(i_temp, j_temp) = Val(str11)
                            '     If idex > 42 Then     MsgBox(idex & " " & kdex & " " & str11)
50:                     Next
                    Catch ex As Microsoft.VisualBasic.
                      FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                    End Try

100:                ' skip covariance terms

                End While
            End Using

            For i = 1 To 42
                temp_check(i) = 1
                If err_arr(i) = 0 Then temp_check(i) = 0
            Next i

            'create temporary seed file

            '    outparameter = "c:\\MEXRF\XRF_T" & i_seed - 1 & "\xfit_parms.txt"
            outparameter = exe_dir_name & "\xfit_parms_" & i_seed - 1 & ".txt" & " "
            Dim fs As FileStream = File.Create(outparameter)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes("spec_fit_" & i_seed - 1 & vbCrLf)
            fs.Write(info, 0, info.Length)

            fs.Close()

            '   klineout = "spec_fit_0" & vbCrLf
            '  My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)

            '  Console.Write(klineout)
            For i = 1 To 42
                initval2(i) = results_arr(i)
                ichecked(i) = temp_check(i)
                klineout = " " & i & " " & results_arr(i) & "    " & temp_check(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)
                '    Console.Write(klineout)
            Next i

        Next i_seed

    End Sub

    Private Sub gen_seed_from_screen()
        ' create new parameter seed from screen (".res") in each XRF_# directory - all fits seeded with same parameters
        '     Dim results_arr(42), err_arr(42), covar_array(42, 42)
        Dim temp_check(42)
        Dim results_file_name, init_parm_file_name As String
        Dim n_parms, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim klineout As String
        Dim icontinue, i_seed As Integer
        Dim chisqr

        Dim str11 As String
        For i_seed = 1 To num_files
            '     MsgBox(i_seed & " : " & num_files)
            n_parms = 42
            idex = -2
            jdex = 0

            '   outparameter = "c:\\MEXRF\xrf_T" & i_seed - 1 & "\xfit_parms.txt"
            outparameter = exe_dir_name & "\xfit_parms_" & i_seed - 1 & ".txt" & " "

            Dim fs As FileStream = File.Create(outparameter)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes("spec_fit_" & i_seed - 1 & vbCrLf)
            fs.Write(info, 0, info.Length)

            fs.Close()

            '   klineout = "spec_fit_0" & vbCrLf
            '  My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)

            '  Console.Write(klineout)
            For i = 1 To 42
                klineout = " " & i & " " & Par(i) & "    " & free_params(i) & vbCrLf
                My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)
                '    Console.Write(klineout)
            Next i

        Next i_seed

    End Sub

    Private Sub gen_seed_from_file()
        Dim results_file_name As String
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = resultfiledir
        openFileDialog1.Filter = "txt files (*.txt)|*.res|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    results_file_name = openFileDialog1.FileName

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

1:       '    ******** re-enter here for reanalysis     ********
        If results_file_name = "" Then MsgBox("No data file selected")
        If results_file_name = "" Then Return


        Dim temp_check(42)

        Dim n_parms, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim klineout As String
        Dim icontinue As Integer
        Dim chisqr

        Dim str11 As String
        '    For i_seed = 1 To num_files
        '     MsgBox(i_seed & " : " & num_files)
        n_parms = 42
        idex = -2
        jdex = 0

        Call get_split_pars(results_file_name)   ' get parameters from results file from RESULTS directory

        For i = 1 To 42
            ichecked(i) = 1
            initval2(i) = results_arr(i)
            If is_free_par(i) = 0 Then ichecked(i) = 0
        Next i


    End Sub

    Private Sub estimate_seed(i_skip)

        Dim generic_file_name, temp_file_name As String
        Dim i_len, i_st As Integer


        generic_file_name = seed_parm_dir_name & "generic_fit_parameters.txt"

        Dim fileReader As System.IO.StreamReader
        fileReader =
        My.Computer.FileSystem.OpenTextFileReader(generic_file_name)
        Dim stringReader As String
        inlineval = fileReader.ReadLine()
        temp_file_name = inlineval
        For i = 1 To 42
            ' stringReader = fileReader.ReadLine()
            inlineval = fileReader.ReadLine()
            i_len = Strings.Len(inlineval)
            If i > 9 Then i_st = 4 Else i_st = 3
            initval2(i) = Val(Strings.Mid(inlineval, i_st, i_len - 5))
            ichecked(i) = Val(Strings.Right(inlineval, 2))

        Next i

        If i_skip = 1 Then Return
        '   Call calc_vms_conc()

        Dim element_index(10)
        ' determine where the various elements are defined
        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i

        Dim U_dex, pu_dex, am_dex As Integer
        U_dex = 0
        pu_dex = 0

        For i = 1 To 5
            If element_symbol(element_index(i)) = "U" Then U_dex = i
            If element_symbol(element_index(i)) = "Pu" Then pu_dex = i
            If element_symbol(element_index(i)) = "Am" Then am_dex = i

        Next

        If U_dex <> 0 Then initval2(U_dex) = MEXRF_main.U_Ka1_seed_counts_box.Text
        If pu_dex <> 0 Then initval2(pu_dex) = MEXRF_main.Pu_seed_counts_box.Text
        If pu_dex <> 0 And am_dex <> 0 Then initval2(am_dex) = MEXRF_main.Pu_seed_counts_box.Text / 100


        If MEXRF_main.U_conc_xrf_box.Text > 0 Then initval2(9) = MEXRF_main.U_conc_xrf_box.Text / 1000 Else initval2(9) = 0
        If MEXRF_main.Pu_conc_xrf_box.Text > 0 Then initval2(10) = MEXRF_main.Pu_conc_xrf_box.Text / 1000 Else initval2(10) = 0

        initval2(15) = new_e0
        initval2(16) = new_de
        initval2(24) = MEXRF_main.cd_prelim_area_box.Text


    End Sub

    Sub update_parameters()

        ' reads check boxes on parameter input form
        ' reads seed data from masked checkboxes on input form
        ' creates parameter file for input to XRF_FIT

        Dim i As Integer
        Dim s As String
        Dim infile_name As String
        Dim param As Integer
        Dim param0 As Integer
        Dim klineout As String


        For i = 1 To 42
            free_params(i) = 0
            check_params(i) = 0
            Par(i) = 0.0
        Next i

        param = 0

        s = "Checked Items:" & ControlChars.CrLf
        For i = 0 To (Intensities_ListBox.Items.Count - 1)
            If Intensities_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & Intensities_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1) = 1
                param = param + 1
            End If
        Next

        For i = 0 To (FP_ListBox.Items.Count - 1)
            If FP_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1 + 5).ToString & " = " & FP_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 5) = 1
                param = param + 1
            End If
        Next


        For i = 0 To (conc_ListBox.Items.Count - 1)
            If conc_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1 + 8).ToString & " = " & conc_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 8) = 1
                param = param + 1
            End If
        Next
        param0 = param

        For i = 0 To (spec_par_ListBox.Items.Count - 1)
            If spec_par_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1 + 14).ToString & " = " & spec_par_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 14) = 1
                param = param + 1
            End If
        Next
        param0 = param + param0

        For i = 0 To (Bremms_ListBox.Items.Count - 1)
            If Bremms_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1 + 24).ToString & " = " & Bremms_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 24) = 1
                param = param + 1
            End If
        Next

        param0 = param + param0

        For i = 0 To (AdditionalPars_ListBox.Items.Count - 1)
            If AdditionalPars_ListBox.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1 + 36).ToString & " = " & AdditionalPars_ListBox.Items(i).ToString & ControlChars.CrLf
                check_params(i + 1 + 36) = 1
                param = param + 1
            End If
        Next

        '      MsgBox("Total Free Parameters = " & param)
        MessageBox.Show(s)
        infile_name = "testfile"
        '   Call Main()
        Par(1) = ActBox1.Text
        Par(2) = ActBox2.Text
        Par(3) = ActBox3.Text
        Par(4) = ActBox4.Text
        Par(5) = ActBox5.Text
        Par(6) = ActBox6.Text
        Par(7) = ActBox7.Text
        Par(8) = ActBox8.Text
        Par(9) = ActBox9.Text
        Par(10) = ActBox10.Text
        '
        Par(11) = ActBox11.Text
        Par(12) = ActBox12.Text
        Par(13) = ActBox13.Text
        Par(14) = ActBox14.Text
        Par(15) = ActBox15.Text
        Par(16) = ActBox16.Text
        Par(17) = ActBox17.Text
        Par(18) = ActBox18.Text
        Par(19) = ActBox19.Text
        Par(20) = ActBox20.Text

        Par(21) = ActBox21.Text
        Par(22) = ActBox22.Text
        Par(23) = ActBox23.Text
        Par(24) = ActBox24.Text
        Par(25) = ActBox25.Text
        Par(26) = ActBox26.Text
        Par(27) = ActBox27.Text
        Par(28) = ActBox28.Text
        Par(29) = ActBox29.Text
        Par(30) = ActBox30.Text
        '
        Par(31) = ActBox31.Text
        Par(32) = ActBox32.Text
        Par(33) = ActBox33.Text
        Par(34) = ActBox34.Text
        Par(35) = ActBox35.Text
        Par(36) = ActBox36.Text

        Par(37) = ActBox37.Text
        Par(38) = ActBox38.Text
        Par(39) = ActBox39.Text
        Par(40) = ActBox40.Text
        Par(41) = ActBox41.Text
        Par(42) = ActBox42.Text

        free_params(1) = check_params(1)
        free_params(2) = check_params(2)
        free_params(3) = check_params(3)
        free_params(4) = check_params(4)
        free_params(5) = check_params(5)
        free_params(6) = check_params(6)
        free_params(7) = check_params(7)
        free_params(8) = check_params(8)
        free_params(9) = check_params(9)
        free_params(10) = check_params(10)

        free_params(11) = check_params(11)
        free_params(12) = check_params(12)
        free_params(13) = check_params(13)
        free_params(14) = check_params(14)
        free_params(15) = check_params(15)
        free_params(16) = check_params(16)
        free_params(17) = check_params(17)
        free_params(18) = check_params(18)
        free_params(19) = check_params(19)
        free_params(20) = check_params(20)

        free_params(21) = check_params(21)
        free_params(22) = check_params(22)
        free_params(23) = check_params(23)
        free_params(24) = check_params(24)
        free_params(25) = check_params(25)
        free_params(26) = check_params(26)
        free_params(27) = check_params(27)
        free_params(28) = check_params(28)
        free_params(29) = check_params(29)
        free_params(30) = check_params(30)

        free_params(31) = check_params(31)
        free_params(32) = check_params(32)
        free_params(33) = check_params(33)
        free_params(34) = check_params(34)
        free_params(35) = check_params(35)
        free_params(36) = check_params(36)

        free_params(37) = check_params(37)
        free_params(38) = check_params(38)
        free_params(39) = check_params(39)
        free_params(40) = check_params(40)
        free_params(41) = check_params(41)
        free_params(42) = check_params(42)

    End Sub


    Public Sub update_genric_file()
        ' reads check boxes on parameter input form
        ' reads seed data from masked checkboxes on input form
        ' creates parameter file for input to XRF_FIT

        Dim i As Integer
        Dim s As String
        Dim infile_name As String
        Dim param As Integer
        Dim param0 As Integer
        Dim klineout As String

        Call update_parameters()

        outparameter = seed_parm_dir_name & "generic_fit_parameters.txt" & " "
        Dim fs As FileStream = File.Create(outparameter)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("generic seed parameters" & vbCrLf)
        fs.Write(info, 0, info.Length)

        fs.Close()

        '   klineout = "spec_fit_0" & vbCrLf
        '  My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)

        '  Console.Write(klineout)
        For i = 1 To 42

            klineout = " " & i & " " & Par(i) & "    " & free_params(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outparameter, klineout, True)
            '    Console.Write(klineout)
        Next i

    End Sub

    Private Sub ActBox9_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles ActBox9.MaskInputRejected

    End Sub
End Class