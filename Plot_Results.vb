Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Plot_Results
    Public result_num1, result_num_last As Integer
    Public vert_scale, min_energy, width_energy
    Public plot_calc(4100), plot_calc1(4100), plot_calc2(4100), plot_calc3(4100), plot_calc4(4100), gammas(4100)
    Public plot_spec(4100), plot_bkg(4100), plot_rand_bkg(4100)
    Public results_file, results_file1 As String
    Public show_box_changed, same_spec_details_flag As Boolean

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  MaskedTextBox1.Text = Form6.MaskedTextBox1.Text
        '  Result_num1 = MaskedTextBox1.Text
        Dim i, i1 As Integer, result_num1
        result_num1 = 1
        For i1 = 1 To 4100
            plot_calc(i1) = 0.000000001
            plot_calc2(i1) = 0.000000001
            plot_calc3(i1) = 0.000000001
            plot_calc4(i1) = 0.000000001
            plot_calc1(i1) = 0.000000001
            plot_bkg(i1) = 0.000000001
            gammas(i1) = 0.000000001
        Next i1

        same_spec_details_flag = False
        results_file1 = MEXRF_main.ListBox1.Items(result_num1 - 1)
        e_0 = 0
        d_e = 0.09
        For i = 1 To num_files
            ListBox7.Items.Add("Cycle " & i)
        Next i
        RadioButton2.Checked = True         ' linear vertical scale
        RadioButton1.Checked = False        ' log vertical scale

        RadioButton3.Checked = True         ' energy scale
        RadioButton4.Checked = False        ' channels

        show_box_changed = False
        show_components_box.Checked = False    ' default display option  components  = off

        min_energy = 75                     ' inital ener
        width_energy = 75
        ' min_energy = Val(E0_UpDown.Text)
        ' width_energy = Val(Width_UpDown.Text)
        result_num_last = 0
        result_num1 = 1


        Call get_plot_spec(result_num1)
    End Sub


    Dim dtfitted As New DataTable

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart2.Click

        '     With Chart2.Series(0)
        '     .Points.DataBind(dtfitted.DefaultView, "Energy", "Counts", Nothing)
        '     .ChartType = DataVisualization.Charting.SeriesChartType.Line
        '     .BorderWidth = 4
        '    End With

    End Sub
    Public Sub get_plot_spec(Result_num1)

        ' displays plot of each spectra

        ' Call get_fit_spec()
        If Strings.Right(results_file1, 3) <> "res" Then Call get_fit_spec(Result_num1) Else Call split_file(results_file1)

        ' calc error array and relative residual



        For i = 1 To max_channels
            bkg_spec(i) = original_spc(i) - corrected_spec(i)
            fitted_plus_bkg_spec(i) = bkg_spec(i) + fitted_spec(i)
            If (RadioButton1.Checked And original_spc(i) < 1) Then plot_spec(i) = 0.1 Else plot_spec(i) = original_spc(i)
            If (RadioButton1.Checked And fitted_plus_bkg_spec(i) < 1) Then fitted_plus_bkg_spec(i) = 0.1
            If (RadioButton1.Checked And bkg_spec(i) < 1) Then plot_bkg(4100) = 0.1 Else plot_bkg(4100) = bkg_spec(i)
        Next i

        For i = 1 To max_channels

            err_array(i) = 1
            If original_spc(i) <> 0 Then err_array(i) = (original_spc(i) ^ 0.5)
            '      If original_spc(i) <> 0 Then rel_resid(i) = (original_spc(i) - fitted_spec(i)) / err_array(i)
            If original_spc(i) <> 0 Then rel_resid(i) = (original_spc(i) - (bkg_spec(i) + fitted_spec(i))) / err_array(i)

        Next i

        ' calculate fit components

        If Result_num1 <> result_num_last Then same_spec_details_flag = False
        If Result_num1 = result_num_last And same_spec_details_flag = True Then GoTo skipcalc
        Cursor = Cursors.WaitCursor

        Dim i_start, i_stop1, i_stop2 As Integer


        If show_components_box.Checked = True Then GoTo add_details
        For i = 1 To max_channels
            plot_rand_bkg(i) = 0.000000001
            plot_calc(i) = 0.000000001
            plot_calc1(i) = 0.000000001
            plot_calc2(i) = 0.000000001
            plot_calc3(i) = 0.000000001
            plot_calc4(i) = 0.000000001
            plot_bkg(i) = 0.000000001
            gammas(i) = 0.000000001
        Next i
add_details:

        If show_components_box.Checked = False Then GoTo skip_details
        Cursor = Cursors.WaitCursor

        Call random_spec(original_spc, max_channels)
        i_start = Int((65 - results_arr(15)) / results_arr(16))
        i_stop1 = Min(max_channels - 10, Int((150 - results_arr(15)) / results_arr(16)))
        i_stop2 = max_channels - 10

        For i = i_start To i_stop2
            plot_rand_bkg(i) = Max(rand_temp(i) * results_arr(25), 0.000000001)
            gammas(i) = Max(functn_fiss(i, results_arr), 0.000000001)
        Next i

        For i = i_start To i_stop1
            plot_calc(i) = Max(functn3(i, results_arr, 1), 0.000000001)
            plot_calc1(i) = Max(functn3(i, results_arr, 2), 0.000000001)
            plot_calc2(i) = Max(functn3(i, results_arr, 3), 0.000000001)
            plot_calc3(i) = Max(functn3(i, results_arr, 4), 0.000000001)
            plot_calc4(i) = Max(functn3(i, results_arr, 5), 0.000000001)
            plot_bkg(i) = Max(functn_bkg(i, results_arr), 0.000000001)

        Next i

        same_spec_details_flag = True
        Cursor = Cursors.Default
skip_details:
        result_num_last = Result_num1
        Cursor = Cursors.Default
skipcalc:
        '    start plotting

        Dim e_0, d_e, ener, e_min, e_max, ymax2
        Dim chan1, chan2 As Integer

        e_0 = results_arr(15)
        d_e = results_arr(16)


        e_min = Max(0, min_energy)
        e_max = Min(min_energy + width_energy, 180)
        chan1 = Int((e_min - e_0) / d_e + 0.5)
        chan2 = Int((e_max - e_0) / d_e + 0.5)
        chan2 = Min(chan2, max_channels)
        Dim ymin, ymax
        ymin = 0
        ymax = 0
        For i = chan1 To chan2
            ener = e_0 + d_e * (i + 1)
            If i > 0 And ener > 10 And raw_data(Max(1, i)) > ymax Then ymax = raw_data(Max(1, i))
        Next
        ymin = ymax
        ymax2 = ymax
        For i = chan1 To chan2
            ener = e_0 + d_e * (i + 1)
            If i > 0 And ener > 10 And raw_data(Max(1, i)) < ymin Then ymin = raw_data(Max(1, i))
        Next

        If ymin < 1 Then ymin = 1
        If ymax < 10 Then ymax = 10
        ymax = 10 ^ Int(Log10(ymax) + 1)
        ymin = 10 ^ Int(Log10(ymin))
        If ymin < 1 Then ymin = 1

        Dim y_temp
        y_temp = Int(ymax2 / (ymax / 10) + 1)
        '  ymax2 = Max(1, Int(ymax2 / 1000 + 0.5) * 1000)
        ymax2 = y_temp * (ymax / 10)

        Dim max_digits As Integer
        Dim chart3_digits As String

        With Chart2.ChartAreas(0)
            .AxisX.Minimum = e_min
            .AxisX.Interval = 10
            .AxisX.Maximum = e_max
            .AxisY.Minimum = 0
            .AxisY.Maximum = ymax2 / (2 ^ vert_scale)

            .AxisY.IsLogarithmic = False
            If RadioButton1.Checked Then
                .AxisY.IsLogarithmic = True
                .AxisY.Minimum = ymin
                .AxisY.Maximum = ymax
            End If

            '         .AxisY.Maximum = i_max
            '       .AxisY.Interval = i_interval
            .AxisX.Title = "Energy (keV)"
            If RadioButton4.Checked Then
                .AxisX.Title = "Channel"
                .AxisX.Minimum = Int(e_min / d_e)
                .AxisX.Interval = 100
                .AxisX.Maximum = Int(e_max / d_e)
            End If

            max_digits = Max(Int(Log10(Int(ymax / d_e))), 2)
            .AxisY.Title = "Counts"
        End With
        Me.Chart2.Series("fitted_Results").Points.Clear()
        Me.Chart2.Series("raw_counts").Points.Clear()
        Me.Chart2.Series("bkg").Points.Clear()
        Me.Chart2.Series("U_response").Points.Clear()
        Me.Chart2.Series("Pu_response").Points.Clear()
        Me.Chart2.Series("Np_response").Points.Clear()
        Me.Chart2.Series("Cm_response").Points.Clear()
        Me.Chart2.Series("Am_response").Points.Clear()
        Me.Chart2.Series("Gamma_rays").Points.Clear()
        For i = 1 To max_channels - 1
            '  ener = e_0 + d_e * (i )
            If RadioButton3.Checked Then ener = e_0 + d_e * (i) Else ener = i

            Me.Chart2.Series("fitted_Results").Points.AddXY(ener, fitted_plus_bkg_spec(i))
            Me.Chart2.Series("raw_counts").Points.AddXY(ener, plot_spec(i))
            If show_components_box.Checked = False Then GoTo dont_display
            Me.Chart2.Series("bkg").Points.AddXY(ener, plot_bkg(i))
            Me.Chart2.Series("U_response").Points.AddXY(ener, plot_calc(i))
            Me.Chart2.Series("Pu_response").Points.AddXY(ener, plot_calc1(i))
            Me.Chart2.Series("Np_response").Points.AddXY(ener, plot_calc2(i))
            Me.Chart2.Series("Cm_response").Points.AddXY(ener, plot_calc3(i))
            Me.Chart2.Series("Am_response").Points.AddXY(ener, plot_calc4(i))
            Me.Chart2.Series("Gamma_rays").Points.AddXY(ener, gammas(i))
dont_display:
        Next i

        chart3_digits = "00"
        For i = 3 To max_digits - 2
            chart3_digits = chart3_digits & "0"
        Next i


        With Chart3.ChartAreas(0)
            .AxisX.Minimum = e_min
            .AxisX.Interval = 10
            .AxisX.Maximum = e_max
            .AxisY.Minimum = -15
            .AxisY.Maximum = 15
            .AxisY.Interval = 5

            .AxisY.LabelStyle.Format = chart3_digits
            .AxisX.Title = "Energy (keV)"

            If RadioButton4.Checked Then
                .AxisX.Title = "Channel"
                .AxisX.Minimum = Int(e_min / d_e)
                .AxisX.Interval = 100
                .AxisX.Maximum = Int(e_max / d_e)
            End If

            .AxisY.Title = "Relative Residuals"
        End With
        Me.Chart3.Series("Relative Residuals").Points.Clear()

        For i = 1 To max_channels - 1
            ' ener = e_0 + d_e * (i+1)
            If RadioButton3.Checked Then ener = e_0 + d_e * (i + 1) Else ener = i - 1
            Me.Chart3.Series("Relative Residuals").Points.AddXY(ener, rel_resid(i))

        Next i

        Dim PC2 As New CalloutAnnotation
        With PC2
            Chart2.Annotations.Add(PC2)
        End With

        Dim PC As New CalloutAnnotation
        With PC
            Chart3.Annotations.Add(PC)
        End With

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        get_plot_spec(Result_num1)
    End Sub
    Dim dtresidual As New DataTable
    Private Sub Chart3_Click(sender As Object, e As EventArgs) Handles Chart3.Click

        '      With Chart2.Series(0)
        '      .Points.DataBind(dtresidual.DefaultView, "Energy", "Counts", Nothing)
        '      .ChartType = DataVisualization.Charting.SeriesChartType.Line
        '          .BorderWidth = 4
        '     End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

        Text = "Print Form Fit to Page Example"
        Size = New Size(1000, 700)


        Dim preview As New PrintPreviewDialog
        Dim pd As New System.Drawing.Printing.PrintDocument
        pd.DefaultPageSettings.Landscape = True
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

        result_num1 = Val(Strings.Right(ListBox7.SelectedItem.ToString(), 2))
        ' results_file1 = ListBox7.SelectedItem.ToString()

        results_file1 = MEXRF_main.ListBox1.Items(result_num1 - 1)
        Call get_plot_spec(result_num1)

        '  If Strings.Right(results_file1, 3) <> "res" Then Call get_fit_spec(result_num1) Else split_file(results_file1)
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then RadioButton1.Checked = False


        Call get_plot_spec(result_num1)

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then RadioButton2.Checked = False

        Call get_plot_spec(result_num1)

    End Sub

    Private Sub show_components_box_CheckedChanged(sender As Object, e As EventArgs) Handles show_components_box.CheckedChanged
        show_box_changed = True
        Call get_plot_spec(result_num1)
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
                        If idex > 0 And idex < max_channels And kdex = 4 Then corrected_spec(idex) = Val(str11)

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

    Private Sub Chart2_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart2.MouseMove

        Dim result As HitTestResult = Chart2.HitTest(e.X, e.Y)

        If result.ChartElementType = ChartElementType.DataPoint Then
            Chart2.Series(0).Points(result.PointIndex).XValue.ToString()

            Dim thisPt As New PointF(CSng(Chart2.Series(0).Points(result.PointIndex).XValue),
                                CSng(Chart2.Series(0).Points(result.PointIndex).YValues(0)))
            Dim ta As New CalloutAnnotation
            With ta
                .AnchorDataPoint = Chart2.Series(0).Points(result.PointIndex)
                .X = thisPt.X + 1
                .Y = thisPt.Y + 1
                .Text = thisPt.ToString
                .CalloutStyle = CalloutStyle.RoundedRectangle
                .ForeColor = Color.Red
                .Font = New Font("Arial", 8, FontStyle.Bold)
            End With
            Chart2.Annotations(0) = ta
            Chart2.Invalidate()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll

        vert_scale = TrackBar1.Value

        Call get_plot_spec(result_num1)

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton4.Checked Then RadioButton3.Checked = False


        Call get_plot_spec(result_num1)
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton3.Checked Then RadioButton4.Checked = False


        Call get_plot_spec(result_num1)
    End Sub

    Private Sub E0_UpDown_ValueChanged(sender As Object, e As EventArgs) Handles E0_UpDown.ValueChanged
        min_energy = Val(E0_UpDown.Text)

        result_num1 = Max(1, result_num1)
        Call get_plot_spec(result_num1)
    End Sub

    Private Sub Width_UpDown_ValueChanged(sender As Object, e As EventArgs) Handles Width_UpDown.ValueChanged
        width_energy = Val(Width_UpDown.Text)

        result_num1 = Max(1, result_num1)
        Call get_plot_spec(result_num1)
    End Sub

    Private Sub Chart3_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart3.MouseMove

        Dim result As HitTestResult = Chart3.HitTest(e.X, e.Y)

        If result.ChartElementType = ChartElementType.DataPoint Then
            Chart3.Series(0).Points(result.PointIndex).XValue.ToString()

            Dim thisPt As New PointF(CSng(Chart3.Series(0).Points(result.PointIndex).XValue),
                                CSng(Chart3.Series(0).Points(result.PointIndex).YValues(0)))
            Dim tb As New CalloutAnnotation
            With tb
                .AnchorDataPoint = Chart3.Series(0).Points(result.PointIndex)
                .X = thisPt.X + 1
                .Y = thisPt.Y + 1
                .Text = thisPt.ToString
                .CalloutStyle = CalloutStyle.RoundedRectangle
                .ForeColor = Color.Red
                .Font = New Font("Arial", 8, FontStyle.Bold)
            End With
            Chart3.Annotations(0) = tb
            Chart3.Invalidate()
        End If
    End Sub

End Class