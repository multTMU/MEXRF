Imports System.IO
Imports System.Math
Imports System.Text


Public Class Individual_results
    'Public results_arr(42), covar_array(42, 42), err_arr(42)
    '  Public live_time, real_time, vial_diameter, sample_temperature, U_enrichment, Pu_weight, sample_temp1
    Dim results_table As New DataTable
    Dim covar_table As New DataTable

    '   Public results_arr(42)
    '   Public err_arr(42)

    Public table_flag, table_flag_2, table_flag_3, table_flag_4 As Boolean
    Public init_parm_file_name As String
    Public Result_num As Integer
    Public Sub Button1_Click(sender As Object, e As EventArgs)


    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        grid1.DataSource = results_table
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grid1.CellContentClick

    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim i As Integer

        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i

        ListBox1.Items(0) = "" & element_symbol(element_index(1)) & ""
        ListBox1.Items(1) = "" & element_symbol(element_index(2)) & ""
        ListBox1.Items(2) = "" & element_symbol(element_index(3)) & ""
        ListBox1.Items(3) = "" & element_symbol(element_index(4)) & ""
        ListBox1.Items(4) = "" & element_symbol(element_index(5)) & ""

        For i = 1 To num_files
            ListBox7.Items.Add("Cycle " & i)
        Next i
        ListBox7.Items.Add("Summary")
        table_flag = False
        table_flag_2 = False
        table_flag_3 = False
        table_flag_4 = False
    End Sub

    Private Sub MaskedTextBox9_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles acterrBox4.MaskInputRejected

    End Sub
    Public Property Actresval_1() As String
        Get
            Return Me.actresbox1.Text
        End Get
        Set(ByVal Value As String)
            Me.actresbox1.Text = Value
        End Set
    End Property




    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        grid1.Hide()
        grid2.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        grid2.Hide()
        grid1.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

        ' Reads Fitted Spectrum from text file (.spc) and displays results

        Dim fitspec_file_name, init_spec_file_name As String
        Dim n_chans, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim icontinue As Integer
        Dim whocares, rubbish
        Dim original_spc(4100), fitted_spec(4100)

        Dim str11 As String

        n_chans = max_channels
        idex = -2
        jdex = 0
        fitspec_file_name = exe_dir_name & "\spec_fit_0.spc"

        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(fitspec_file_name)
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
                        If str11 = "NaN" Then str11 = "0"
                        kdex = kdex + 1
                        If idex = -1 Then init_spec_file_name = str11
                        If idex = -1 Then GoTo 50

                        If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        If idex = 0 And jdex > 10 Then GoTo 50

                        If idex > 0 And idex < max_channels And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 2 Then original_spc(idex) = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 3 Then whocares = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 4 Then rubbish = Val(str11)

                        If idex > 0 And idex < max_channels And kdex = 5 Then fitted_spec(idex) = Val(str11)

50:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms


            End While
        End Using




    End Sub


    Public Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim myForm As Plot_Results

        If myForm Is Nothing Then
            myForm = New Plot_Results
        End If
        myForm.Show()
        myForm = Nothing


    End Sub



    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click


        Text = "Print Form Fit to Page Example"
        Size = New Size(750, 3000)


        Dim preview As New PrintPreviewDialog
        Dim pd As New System.Drawing.Printing.PrintDocument
        pd.DefaultPageSettings.Landscape = False
        AddHandler pd.PrintPage, AddressOf OnPrintPage
        preview.Document = pd
        preview.ShowDialog()

    End Sub

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)


        'create a memory bitmap and size to the form
        Using bmp As Bitmap = New Bitmap(Me.Width, Me.Height)


            'draw the form on the memory bitmap
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))


            'draw the form image on the printer graphics sized and centered to margins
            Dim ratio As Single = CSng(bmp.Width / bmp.Height)


            If ratio > e.MarginBounds.Width / e.MarginBounds.Height Then


                e.Graphics.DrawImage(bmp,
                e.MarginBounds.Left,
                CInt(e.MarginBounds.Top + (e.MarginBounds.Height / 2) - ((e.MarginBounds.Width / ratio) / 2)),
                e.MarginBounds.Width,
                CInt(e.MarginBounds.Width / ratio))


            Else


                e.Graphics.DrawImage(bmp,
                CInt(e.MarginBounds.Left + (e.MarginBounds.Width / 2) - (e.MarginBounds.Height * ratio / 2)),
                e.MarginBounds.Top,
                CInt(e.MarginBounds.Height * ratio),
                e.MarginBounds.Height)


            End If


        End Using


    End Sub

    Private Sub ListBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox7.SelectedIndexChanged
        Dim results_file1 As String

        Dim res_flag As Boolean

        Dim results_file_name As String
        Dim n_parms, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim i, icontinue As Integer


        n_parms = 42

        For i = 1 To n_parms
            For j = 1 To n_parms
                covar_array(i, j) = 0
            Next j
        Next i

        If ListBox7.SelectedItem.ToString() = "Summary" Then GoTo 200
        Result_num = Val(Strings.Right(ListBox7.SelectedItem.ToString(), 2))
        MaskedTextBox1.Text = Result_num


        results_file1 = MEXRF_main.ListBox1.Items(Result_num - 1)

        If Strings.Right(results_file1, 3) <> "res" Then Call get_fit_spec(Result_num) Else Call split_file(results_file1)


        ' Reads Fit Results from text file (.rpt) and displays results

        '    Dim chisqr

        Dim str11 As String



        idex = -2
        jdex = 0
        results_file_name = exe_dir_name & "\spec_fit_" & Result_num - 1 & ".rpt"

        If Strings.Right(results_file1, 3) = "res" Then res_flag = True Else res_flag = False
        If res_flag Then Call get_split_pars(results_file1) Else Call Get_fit_results_file(results_file_name)

        ' Extract run time system constants from results file
        If res_flag Then Call get_split_constants(results_file1)


        fit_iterations_box.Text = fit_iterations
        target_iterations_box.Text = target_iterations

        '    Call get_fit_results_file(results_file_name)

        MaskedTextBox2.Text = data_file_name
        sample_temp1 = sample_temperature

        If table_flag Then GoTo 10
        table_flag = True
        With results_table
            .Columns.Add("Index", System.Type.GetType("System.Double"))
            .Columns.Add("Parameter", System.Type.GetType("System.String"))
            .Columns.Add("Value", System.Type.GetType("System.Double"))
            .Columns.Add("Uncertainty", System.Type.GetType("System.Double"))
        End With

10:     Dim par_names(42) As String
        par_names(1) = "" & element_symbol(element_index(1)) & " Raw Counts"
        par_names(2) = "" & element_symbol(element_index(2)) & " Raw Counts"
        par_names(3) = "" & element_symbol(element_index(3)) & " Raw Counts"
        par_names(4) = "" & element_symbol(element_index(4)) & " Raw Counts"
        par_names(5) = "" & element_symbol(element_index(5)) & " Raw Counts"
        par_names(6) = fiss_prod_name(1) & " Int. (counts)"
        par_names(7) = fiss_prod_name(2) & " Int. (counts)"
        par_names(8) = fiss_prod_name(3) & " Int. (counts)"
        par_names(9) = "[" & element_symbol(element_index(1)) & "] g/cc "
        par_names(10) = "[" & element_symbol(element_index(2)) & "] g/cc "

        par_names(11) = "[" & element_symbol(element_index(3)) & "] g/cc "
        par_names(12) = "[" & element_symbol(element_index(4)) & "] g/cc "
        par_names(13) = "[" & element_symbol(element_index(5)) & "] g/cc "
        par_names(14) = "[matrix] g/cc "
        par_names(15) = "E cal offset (keV)"
        par_names(16) = "E cal slope (keV/ch)"
        par_names(17) = "Guassian Width (keV)"
        par_names(18) = "Tail Int. (relative)"
        par_names(19) = "Tail decay const (1/keV)"
        par_names(20) = "step background (relative)"

        par_names(21) = "Ge escape peak Int. (rel.)"
        par_names(22) = "Ref Peak Wdith (keV)"
        par_names(23) = "Ref Peak Energy (keV)"
        par_names(24) = "Ref Peak Int. (counts)"
        par_names(25) = "Random Background Factor."
        par_names(26) = "Linear Background offset (counts)"
        par_names(27) = "Linear Background slope (coounts/ch)"
        par_names(28) = "HV Endpoint (kV)"
        par_names(29) = "Bremms. Shaping Parameter"
        par_names(30) = "Backscatter intensity"

        par_names(31) = "Elastic Scatter Fraction"
        par_names(32) = "E ref 1 (keV)"
        par_names(33) = "E ref 2 (keV)"
        par_names(34) = "Spline A1"
        par_names(35) = "Spline A2"
        par_names(36) = "Spline Tau"

        par_names(37) = "high side tail intensity"
        par_names(38) = "high side tail decay"
        par_names(39) = "Ref. Peak step tail intensity"
        par_names(40) = "W X-ray Peak Intensity"
        par_names(41) = "SS wall thickness"
        par_names(42) = "Pb X-ray Peak Intensity"

        If table_flag_2 Then results_table.Rows.Clear()
        table_flag_2 = True
        For i = 1 To 42
            Dim newrow As DataRow = results_table.NewRow
            newrow("Index") = i
            newrow("Parameter") = par_names(i)
            newrow("Value") = results_arr(i)
            newrow("Uncertainty") = err_arr(i)
            results_table.Rows.Add(newrow)
        Next i



20:     Call get_system_constants(const_file_name)



        sampleinfoBox1.Text = vial_diameter
        sampleinfoBox2.Text = sample_temp1
        sampleinfoBox3.Text = U_enrichment
        sampleinfoBox4.Text = Pu_weight
        sampleinfoBox5.Text = Sample_ID    '   Form2.SampleIDBox1.Text
        sampleinfoBox6.Text = Const_val(37)
        sampleinfoBox7.Text = Const_val(38)

        'tempercal1
        Dim Temp_correction, reference_temp, temp_linear
        '  sample_temperature = Val(sampleinfoBox2.Text)

        reference_temp = Const_val(37)
        temp_linear = Const_val(38)
        Temp_correction = 1
        If sample_temp1 <> 0 Then Temp_correction = 1 + (sample_temp1 - reference_temp) * temp_linear

        Dim enrich_correction, enrichment, U234, U238
        enrichment = Val(sampleinfoBox3.Text)
        U234 = enrichment * (0.0055 / 0.71)
        U238 = 100 - U234 - enrichment

        enrich_correction = 1
        If enrichment <> 0 Then enrich_correction = 238 / ((U234 * 234 + enrichment * 235 + U238 * 238) / 100)
        '   MsgBox("enrichment corr = " & enrich_correction)

        Dim Pu_wgt_correction, Pu_wgt_ref, Pu_wgt
        Pu_wgt = Val(sampleinfoBox4.Text)
        Pu_wgt_ref = 244
        Pu_wgt_correction = 1
        If Pu_wgt <> 0 Then Pu_wgt_correction = Pu_wgt_ref / Pu_wgt

        Dim Sample_correction
        Sample_correction = enrich_correction * Temp_correction

        Dim results_arr_temp(5), err_arr_temp(5)
        '  **** Correct peak areas for tailing ******
        Dim cor_counts(5), cor_counts_err(5), rate_ratios(5), rate_ratios_err(5)

        Dim corr_temp

        For i = 1 To 5
            corr_temp = 1 + Abs(2 * results_arr(18) / results_arr(19) / results_arr(16)) + Abs(2 * results_arr(37) / results_arr(38) / results_arr(16)) + results_arr(20)
            cor_counts(i) = results_arr(i) * corr_temp
            cor_counts_err(i) = (err_arr(i) * corr_temp) ^ 2

            cor_counts_err(i) = cor_counts_err(i) + ((results_arr(i) * 2 * results_arr(18) / results_arr(19) + results_arr(i) * 2 * results_arr(37) / results_arr(38)) * err_arr(16) / results_arr(16) ^ 2) ^ 2
            cor_counts_err(i) = cor_counts_err(i) + +(results_arr(i) * 2 * results_arr(18) / results_arr(19) ^ 2 * err_arr(19) / results_arr(16)) ^ 2
            cor_counts_err(i) = cor_counts_err(i) + +(results_arr(i) * 2 * results_arr(37) / results_arr(38) ^ 2 * err_arr(38) / results_arr(16)) ^ 2

            cor_counts_err(i) = cor_counts_err(i) + +(results_arr(i) * 2 * err_arr(18) / results_arr(19) / results_arr(16)) ^ 2
            cor_counts_err(i) = cor_counts_err(i) + +(results_arr(i) * 2 * err_arr(37) / results_arr(38) / results_arr(16)) ^ 2
            cor_counts_err(i) = (cor_counts_err(i) + (results_arr(i) * err_arr(20)) ^ 2) ^ 0.5
            cor_counts(i) = cor_counts(i) / live_time
            cor_counts_err(i) = cor_counts_err(i) / live_time
        Next i



        rate_ratios(1) = 1
        rate_ratios_err(1) = 0
        For i = 2 To 5
            rate_ratios(i) = 0
            If results_arr(i) <> 0 Then rate_ratios(i) = results_arr(1) / results_arr(i)
            rate_ratios_err(i) = 0
            If results_arr(i) <> 0 Then rate_ratios_err(i) = ((err_arr(1) / results_arr(i)) ^ 2 + (results_arr(1) / results_arr(i) ^ 2 * err_arr(i)) ^ 2) ^ 0.5

        Next


        For i = 1 To 5
            results_arr_temp(i) = cor_counts(i)
            err_arr_temp(i) = cor_counts_err(i)

        Next i

        Me.actresbox1.Text = Int(results_arr_temp(1) * 100 + 0.5) / 100
        Me.actresbox2.Text = Int(results_arr_temp(2) * 100 + 0.5) / 100
        Me.actresbox3.Text = Int(results_arr_temp(3) * 100 + 0.5) / 100
        Me.actresbox4.Text = Int(results_arr_temp(4) * 100 + 0.5) / 100
        Me.actresbox5.Text = Int(results_arr_temp(5) * 100 + 0.5) / 100


        Me.acterrBox1.Text = Int(err_arr_temp(1) * 100 + 0.5) / 100
        Me.acterrBox2.Text = Int(err_arr_temp(2) * 100 + 0.5) / 100
        Me.acterrBox3.Text = Int(err_arr_temp(3) * 100 + 0.5) / 100
        Me.acterrBox4.Text = Int(err_arr_temp(4) * 100 + 0.5) / 100
        Me.acterrBox5.Text = Int(err_arr_temp(5) * 100 + 0.5) / 100


        Me.chisqrBox1.Text = Int(chisqr * 1000 + 0.5) / 1000

        Me.Rate_ratio_box_1.Text = Int(rate_ratios(2) * 1000 + 0.5) / 1000
        Me.Rate_ratio_box_2.Text = Int(rate_ratios(3) * 1000 + 0.5) / 1000
        Me.Rate_ratio_box_3.Text = Int(rate_ratios(4) * 1000 + 0.5) / 1000
        Me.Rate_ratio_box_4.Text = Int(rate_ratios(5) * 1000 + 0.5) / 1000

        Me.Rate_ratio_err_box_1.Text = Int(rate_ratios_err(2) * 1000 + 0.5) / 1000
        Me.Rate_ratio_err_box_2.Text = Int(rate_ratios_err(3) * 1000 + 0.5) / 1000
        Me.Rate_ratio_err_box_3.Text = Int(rate_ratios_err(4) * 1000 + 0.5) / 1000
        Me.Rate_ratio_err_box_4.Text = Int(rate_ratios_err(5) * 1000 + 0.5) / 1000


        Dim r_upu_initial, au_aPu_initial, u_wgt, U_PU_init

        Dim sum_lines(5), norm_branch(5)
        For i = 1 To 5
            sum_lines(i) = 0
            norm_branch(i) = 1
        Next i

        For j = 1 To 5
            For i = 1 To 10
                sum_lines(j) = sum_lines(j) + xray_Lib(j, i, 2)
            Next i
        Next j

        For i = 2 To 5
            If sum_lines(1) <> 0 Then norm_branch(i) = sum_lines(i) / sum_lines(1)
        Next i

        If MEXRF_main.CheckBox3.Checked Then vial_diameter = MEXRF_main.MaskedTextBox3.Text
        If MEXRF_main.CheckBox4.Checked Then sample_temperature = MEXRF_main.MaskedTextBox4.Text
        If MEXRF_main.CheckBox5.Checked Then U_enrichment = MEXRF_main.MaskedTextBox5.Text
        If MEXRF_main.CheckBox6.Checked Then Pu_weight = MEXRF_main.MaskedTextBox6.Text

        sampleinfoBox1.Text = vial_diameter
        sampleinfoBox2.Text = sample_temp1
        sampleinfoBox3.Text = U_enrichment
        sampleinfoBox4.Text = Pu_weight

        u_wgt = (U_enrichment / 100 * 235 + (1 - U_enrichment / 100) * 238)

        '  par_x = results_arr(29)
        Dim E_HV, par_x
        E_HV = results_arr(28)
        If MEXRF_main.KED_check_box_1.Checked Then E_HV = MEXRF_main.KED_Box_1.Text
        par_x = 1.109 - 0.00435 * 74 + 0.00175 * E_HV
        If MEXRF_main.KED_check_box_2.Checked Then par_x = MEXRF_main.KED_Box_2.Text

        r_upu_initial = calc_R_UZ(2, E_HV, par_x) * norm_branch(2)

        '  MsgBox("Hv = " & E_HV & " par_x = " & par_x & " r_upu = " & r_upu_initial)

        au_aPu_initial = u_wgt / Pu_weight
        U_PU_init = rate_ratios(2) / r_upu_initial * au_aPu_initial

        HV_bias_box.Text = E_HV
        Shape_par_box.Text = par_x

        R_UPU_box.Text = Int(r_upu_initial * 1000 + 0.5) / 1000
        AU_APu_box.Text = Int(au_aPu_initial * 1000 + 0.5) / 1000
        UPU_box.Text = Int(U_PU_init * 1000 + 0.5) / 1000

        low_E_box.Text = fit_range(1)
        high_E_box.Text = fit_range(2)

        grid1.DataSource = results_table

        '  Create table for covariance array

        covar_label(0) = "index"
        For i = 1 To 42
            covar_label(i) = "(" & i & ")"
        Next

        If table_flag_3 Then GoTo 15
        table_flag_3 = True
        With covar_table
            For i = 0 To 42
                .Columns.Add(covar_label(i), System.Type.GetType("System.Double"))
            Next i

        End With

15:     If table_flag_4 Then covar_table.Rows.Clear()
        table_flag_4 = True
        For k = 1 To 42
            Dim newrow As DataRow = covar_table.NewRow
            For j = 0 To 42
                If j = 0 Then newrow("index") = k
                If j <> 0 Then newrow(covar_label(j)) = covar_array(j, k)
            Next j
            covar_table.Rows.Add(newrow)
        Next k

        grid2.DataSource = covar_table

        Return

200:   ' Show Summary Results

    End Sub



    Sub Get_fit_results_file(results_fil_nam As String)

        Dim idex, jdex, kdex, i_temp, j_temp, n_parms As Integer
        '      Dim chisqr
        Dim str11 As String

        n_parms = 42
        idex = -2
        jdex = 0
        For i = 1 To 42
            For j = 1 To 42
                covar_array(i, j) = 0
            Next j
        Next i


        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(results_fil_nam)
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
                        If idex = -1 And jdex > 3 Then data_file_name = str11


                        If idex = -1 Then GoTo 50

                        If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        If idex = 0 And kdex = 2 Then vial_diameter = Val(str11)
                        If idex = 0 And kdex = 3 Then sample_temperature = Val(str11)
                        If idex = 0 And kdex = 4 Then U_enrichment = Val(str11)
                        If idex = 0 And kdex = 5 Then Pu_weight = Val(str11)
                        If idex = 0 And jdex > 10 Then GoTo 50

                        If idex > 0 And idex < 43 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 2 Then results_arr(idex) = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 3 Then err_arr(idex) = Val(str11)

                        If idex > 42 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 42 And kdex = 2 Then j_temp = Val(str11)
                        If idex > 42 And kdex = 3 Then covar_array(i_temp, j_temp) = Val(str11)

50:                 Next
                Catch ex As Microsoft.VisualBasic.
                        FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms


            End While
        End Using



    End Sub
    Public Sub get_fit_spec(Result_num1)
        ' Reads Fitted Spectrum from text file (.spc) and displays results

        Dim fitspec_file_name, init_spec_file_name As String
        Dim n_chans, idex, jdex, kdex, i_temp, j_temp As Integer
        Dim icontinue As Integer
        Dim whocares, rubbish

        ' MaskedTextBox1.Text = Form6.MaskedTextBox1.Text
        Dim str11 As String

        '   MsgBox(" file = " & Result_num1)

        n_chans = max_channels
        idex = -2
        jdex = 0
        fitspec_file_name = exe_dir_name & "\spec_fit_" & Result_num1 - 1 & ".spc"

        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(fitspec_file_name)
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
                        If idex = -1 And jdex = 5 Then Sample_ID = str11
                        '  If idex = -1 Then MsgBox(idex & " ; " & jdex & " ; " & str11)

                        If Strings.Left(str11, 1) = "" Then GoTo 50
                        If str11 = "NaN" Then str11 = "0"
                        kdex = kdex + 1
                        If idex = -1 Then init_spec_file_name = str11
                        If idex = -1 Then GoTo 50

                        If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        If idex = 0 And jdex > 10 Then GoTo 50

                        If idex > 0 And idex < max_channels And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 2 Then original_spc(idex) = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 3 Then whocares = Val(str11)
                        If idex > 0 And idex < max_channels And kdex = 4 Then rubbish = Val(str11)

                        If idex > 0 And idex < max_channels And kdex = 5 Then fitted_spec(idex) = Val(str11)



50:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms


            End While
        End Using
    End Sub

    Private Sub ListBox7_QueryAccessibilityHelp(sender As Object, e As QueryAccessibilityHelpEventArgs) Handles ListBox7.QueryAccessibilityHelp

    End Sub
End Class