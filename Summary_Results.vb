
Imports System.Math
Imports System.IO
Imports System.Text
Public Class Summary_Results
    Public init_parm_file_name As String
    Public live_time1

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim summary_table As New DataTable
        Dim summary_file_name, temp_num As String

        Dim raw_count(5, 99), raw_count_sig(5, 99), corr_count(5, 99), corr_count_sig(5, 99)
        Dim raw_rate(5, 99), raw_rate_sig(5, 99), corr_rate(5, 99), corr_rate_sig(5, 99)
        Dim raw_wgt_avg(5), raw_wgt_avg_sig(5), raw_wgt(5), raw_stdev(5), simple_avg(60), simple_avg_err(60)
        Dim corr_count_wgt(5), corr_count_rate_stdev(5), wt_avg_corr_rate(5), wt_avg_corr_rate_sig(5)
        Dim realtime_array(99), livetime_array(99), chisqr_array(99)
        Dim vms_u_rate1(99), vms_u_rate2(99), vms_pu_rate(99)

        Dim Z1_Z2_rate_ratio(5, 99), wt_avg_rate_ratio(5), Z1_Z2_rate_ratio_sig(5, 99), wt_avg_rate_ratio_sig(5), wt_avg_rate_ratio_wgt(5)
        Dim temp_array1(99), temp_array2(99), temp_array3(99), temp_array4(99), temp_array5(99), temp_array6(99)
        Dim temp_array1_sig(99), temp_array2_sig(99), temp_array3_sig(99), temp_array4_sig(99), temp_array5_sig(99), temp_array6_sig(99)
        Dim raw_count_covar_array(5, 5, 99), rates_covar_array(5, 5, 99)

        Dim U_conc(99), Pu_conc(99), Np_conc(99), Am_conc(99), Cm_conc(99), Ma_conc(99)
        Dim U_sig(99), Pu_sig(99), Np_sig(99), Am_sig(99), Cm_sig(99), Ma_sig(99)

        Dim corr_U_conc(99), corr_Pu_conc(99), corr_Np_conc(99), corr_Am_conc(99), corr_Cm_conc(99), corr_Ma_conc(99)
        Dim corr_U_sig(99), corr_Pu_sig(99), corr_Np_sig(99), corr_Am_sig(99), corr_Cm_sig(99), corr_Ma_sig(99)
        Dim prelim_U_array(99), prelimin_Pu_array(99)
        Dim i, i_corr, ref_element As Integer

        Dim sample_temp(99), vial_diam(99), u_enrich(99), pu_iso_wgt(99)
        Dim Temp_correction(99), reference_temp(99), temp_linear(99), sample_correction(99), Pu_wgt_correction(99)
        Dim enrich_correction(99), enrichment, U234, U238
        Dim Pu_wgt_ref, Pu_wgt
        Dim temp_matrix(42, 99), temp_matrix_err(42, 99), par_avg(42), par_sigma(42), par_stdev(42), par_wt(42) As Double
        Dim par_wt_err(42), temp_array(42), temp_array_err(42) As Double
        Dim corr_array(5)
        Dim E_HV_err, par_x1_err, concent_in_err(6)

        Dim Z1_Z2_conv_fact(5, 99), Z1_Z2_conv_fact_sig(5, 99), Z1_Z2_avg_conv_fact(5), Z1_Z2_avg_conv_fact_sig(5)
        Dim conc_ratio_array(6, 99), conc_ratio_array_err(6, 99), ref_element_norm
        Dim num_iterations(99) As Integer

        Dim corr_temp
        Dim lo_tail_area, lo_tail_decay, high_tail_area, high_tail_decay, e_chan, step_bkg

        '    Dim element_index(10)

        Dim none_checked, no_indep_flag As Boolean
        '
        ' read in MEXRF system parameters (should move this out of primary loop)
        '
        ' const_file_name = "\MEXRF\xrf_files\system_constants\xrf_system_constants.csv"  
        '
        Call get_system_constants(const_file_name)
        Call get_VMS_constants()
        '
        no_indep_flag = True

        ' set up element names for screen display
        '
        For i = 1 To 10
            element_index(i) = 6
            For j = 1 To 10
                If element_priority(j) = i Then element_index(i) = j
            Next j
        Next i

        For i = 1 To num_files
            ref_peak_1(i) = 0
            ref_peak_2(i) = 0
            xrf_total_rate(i) = 0
        Next i

        ' set labels for corrected peak rate list box

        ListBox1.Items(0) = "" & element_symbol(element_index(1)) & ""
        ListBox1.Items(1) = "" & element_symbol(element_index(2)) & ""
        ListBox1.Items(2) = "" & element_symbol(element_index(3)) & ""
        ListBox1.Items(3) = "" & element_symbol(element_index(4)) & ""
        ListBox1.Items(4) = "" & element_symbol(element_index(5)) & ""

        ' set labels for corrected hybrid anlaysis list box
        Dim element_checked(5) As String

        If MEXRF_main.KED_check_box_3.Checked And MEXRF_main.ref_el_Pu_button.Checked = False Then element_checked(1) = "  *" Else element_checked(1) = ""
        If MEXRF_main.KED_check_box_4.Checked And MEXRF_main.ref_el_Pu_button.Checked = True Then element_checked(2) = " *" Else element_checked(2) = ""
        If MEXRF_main.KED_check_box_5.Checked And MEXRF_main.KED_check_box_3.Checked = False Then element_checked(3) = " *" Else element_checked(3) = ""
        If MEXRF_main.KED_check_box_6.Checked And MEXRF_main.KED_check_box_3.Checked = False Then element_checked(4) = " *" Else element_checked(4) = ""
        If MEXRF_main.KED_check_box_7.Checked And MEXRF_main.KED_check_box_3.Checked = False Then element_checked(5) = " *" Else element_checked(5) = ""

        none_checked = True
        If MEXRF_main.KED_check_box_3.Checked Then none_checked = False
        If MEXRF_main.KED_check_box_4.Checked Then none_checked = False
        If MEXRF_main.KED_check_box_5.Checked Then none_checked = False
        If MEXRF_main.KED_check_box_6.Checked Then none_checked = False
        If MEXRF_main.KED_check_box_7.Checked Then none_checked = False

        HybridPanel.Visible = True
        If none_checked Then HybridPanel.Visible = False

        Hybrid_analysis_listbox.Items(0) = "" & element_symbol(element_index(1)) & element_checked(1) & ""
        Hybrid_analysis_listbox.Items(1) = "" & element_symbol(element_index(2)) & element_checked(2) & ""
        Hybrid_analysis_listbox.Items(2) = "" & element_symbol(element_index(3)) & element_checked(3) & ""
        Hybrid_analysis_listbox.Items(3) = "" & element_symbol(element_index(4)) & element_checked(4) & ""
        Hybrid_analysis_listbox.Items(4) = "" & element_symbol(element_index(5)) & element_checked(5) & ""

        ' set labels for MEKED results list box
        For i = 1 To 5
            If Strings.Right(KED_elements(i), 1) <> "]" Then KED_elements(i) = KED_elements(i) & "]"
            KED_ListBox.Items(i - 1) = KED_elements(i)
        Next
        KED_ListBox.Items(5) = "[Matrix]"
        '
        ' list results from MEKED analysis file (read in from MEXRF_main screen)
        MEKED_Element_1_box.Text = (Format(1000 * KED_PAR(1), "###0.000 "))
        MEKED_Element_2_box.Text = (Format(1000 * KED_PAR(2), "###0.000 "))
        MEKED_Element_3_box.Text = (Format(1000 * KED_PAR(3), "###0.000 "))
        MEKED_Element_4_box.Text = (Format(1000 * KED_PAR(4), "###0.000 "))
        MEKED_Element_5_box.Text = (Format(1000 * KED_PAR(5), "###0.000 "))
        MEKED_matrix_box.Text = (Format(KED_PAR(6), "###0.000 "))
        '
        MEKED_Element_err_1_box.Text = (Format(1000 * KED_PAR_err(1), "###0.000 "))
        MEKED_Element_err_2_box.Text = (Format(1000 * KED_PAR_err(2), "###0.000 "))
        MEKED_Element_err_3_box.Text = (Format(1000 * KED_PAR_err(3), "###0.000 "))
        MEKED_Element_err_4_box.Text = (Format(1000 * KED_PAR_err(4), "###0.000 "))
        MEKED_Element_err_5_box.Text = (Format(1000 * KED_PAR_err(5), "###0.000 "))
        MEKED_matrix_err_box.Text = (Format(KED_PAR_err(6), "###0.000 "))

        If KED_PAR(2) <> 0 Then MEKED_UPu_box.Text = Int(1000 * KED_PAR(1) / KED_PAR(2)) / 1000 Else MEKED_UPu_box.Text = 0
        MEKED_UPu_err_box.Text = 0
        If KED_PAR(2) <> 0 Then MEKED_UPu_err_box.Text = Int(1000 * ((KED_PAR_err(1) / KED_PAR(2)) ^ 2 + (KED_PAR(1) * KED_PAR_err(2) / KED_PAR(2) ^ 2)) ^ 0.5) / 1000
        '
        ' Reads Fit Results from text file (.rpt) and displays results
        '       
        Dim n_parms, idex, jdex As Integer
        Dim res_flag As Boolean
        Dim mdex, st_len, st_pos As Integer
        res_flag = False

        '  ----------------------------------------------------------------------------------------
        '   Read in each of the results files and place fit results into temp_matrix
        '  ----------------------------------------------------------------------------------------
        '
        For Result_num = 1 To num_files

            Dim spectra_name, new_result_name As String

            spectra_name = input_file_names(Result_num)

            st_pos = Strings.InStrRev(spectra_name, "\")
            st_len = Strings.Len(spectra_name)

            new_result_name = resultfiledir & Strings.Mid(spectra_name, st_pos + 1, st_len - 4 - st_pos) & ".res"      ' extract file name from path and change input file to .res file

            n_parms = 42        ' number of free parameters
            idex = -2
            jdex = 0

            If Strings.Right(spectra_name, 3) = "res" Then res_flag = True Else res_flag = False

            ' read in result file for most recent analysis 
            Call split_file(new_result_name)                    ' extract spectrum from results file
            Call get_split_pars(new_result_name)                ' read fit results from results file
            Call get_split_constants(new_result_name)           ' read constants stored in fit results file

            '  ---   determine reference peak channel locations for QA tracking purposes  ---
            ref_peak_1(Result_num) = peak_centroid(raw_data, e_cal_par(2), results_arr(15), results_arr(16), results_arr(17))
            ref_peak_2(Result_num) = peak_centroid(raw_data, e_cal_par(4), results_arr(15), results_arr(16), results_arr(17))
            '
            sample_temp(Result_num) = sample_temperature
            vial_diam(Result_num) = vial_diameter
            pu_iso_wgt(Result_num) = Pu_weight
            livetime_array(Result_num) = live_time
            realtime_array(Result_num) = real_time
            chisqr_array(Result_num) = chisqr
            acq_date(Result_num) = acq_date_temp
            acq_time(Result_num) = acq_time_temp

            For j = 1 To max_channels
                xrf_total_rate(Result_num) = xrf_total_rate(Result_num) + raw_data(j) / livetime_array(Result_num)      ' sum spectrum count rate
            Next j

            If err_arr(9) <> 0 Then no_indep_flag = False

            'create matrices of fit results and uncertainties for each run and parameter 
            For mdex = 1 To 42
                temp_matrix(mdex, Result_num) = results_arr(mdex)
                temp_matrix_err(mdex, Result_num) = err_arr(mdex)
            Next mdex
            '
            '   correct raw counts for tailing (add in counts for the step bkg and the tail intensity) for each file
            '   calculate uncertainties and then calculate rates
            '
            For i = 1 To 5
                raw_count(i, Result_num) = results_arr(i)
                raw_count_sig(i, Result_num) = err_arr(i)
            Next i

            ' collect covariance terms for rate ratio error calculuation
            For i = 1 To 5
                For j = 1 To 5
                    raw_count_covar_array(i, j, Result_num) = covar_array(i, j)
                Next j
            Next i


            Ma_conc(Result_num) = results_arr(6)
            Ma_sig(Result_num) = err_arr(6)


            ref_element = 1
            If MEXRF_main.KED_check_box_4.Checked Then ref_element = 2
            If MEXRF_main.KED_check_box_3.Checked Then ref_element = 1

            If MEXRF_main.ref_el_U_button.Checked = True Then ref_element = 1
            If MEXRF_main.ref_el_Pu_button.Checked = True Then ref_element = 2

            ListBox3.Items(0) = "U:Pu rate ratio"
                If ref_element = 2 Then ListBox3.Items(0) = "Pu:U rate ratio"

                ListBox4.Items(0) = "[U]/[Pu]"
                If ref_element = 2 Then ListBox4.Items(0) = "[Pu]/[U]"

            ' Correct count rates for tailing



            lo_tail_area = results_arr(18)
            lo_tail_decay = results_arr(19)
            high_tail_area = results_arr(37)
            high_tail_decay = results_arr(38)
            e_chan = results_arr(16)
            step_bkg = results_arr(20)

            For i = 1 To 5
                corr_temp = 1 + Abs(2 * lo_tail_area / lo_tail_decay / e_chan) + Abs(2 * high_tail_area / high_tail_decay / e_chan) + step_bkg / e_chan    ' relative contribution to peak intensity from tails
                '
                corr_count(i, Result_num) = results_arr(i) * corr_temp      ' corrected count rate
                '
                corr_count_sig(i, Result_num) = (err_arr(i) * corr_temp) ^ 2
                corr_count_sig(i, Result_num) = corr_count_sig(i, Result_num) + ((results_arr(i) * 2 * lo_tail_area / lo_tail_decay + results_arr(i) * 2 * high_tail_area / high_tail_decay) * err_arr(16) / results_arr(16) ^ 2) ^ 2

                corr_count_sig(i, Result_num) = corr_count_sig(i, Result_num) + (results_arr(i) * 2 * lo_tail_area / lo_tail_decay ^ 2 * err_arr(19) / results_arr(16)) ^ 2
                corr_count_sig(i, Result_num) = corr_count_sig(i, Result_num) + (results_arr(i) * 2 * high_tail_area / high_tail_decay ^ 2 * err_arr(38) / results_arr(16)) ^ 2

                corr_count_sig(i, Result_num) = corr_count_sig(i, Result_num) + (results_arr(i) * 2 * err_arr(18) / lo_tail_decay / results_arr(16)) ^ 2
                corr_count_sig(i, Result_num) = corr_count_sig(i, Result_num) + (results_arr(i) * 2 * err_arr(37) / high_tail_decay / results_arr(16)) ^ 2

                corr_count_sig(i, Result_num) = (corr_count_sig(i, Result_num) + (results_arr(i) * err_arr(20)) ^ 2) ^ 0.5

                corr_rate(i, Result_num) = corr_count(i, Result_num) / live_time                                ' count rate for each element for each assay
                corr_rate_sig(i, Result_num) = corr_count_sig(i, Result_num) / live_time                        ' count rate uncertainty w/o covariance

                '
                ' calculate count ratio for each element relative to the first element for each result file
                '
                '   tailing corrections are independent of peak.call calc_vms
                '   cacluate ratios ratios directly from the reported counts to avoid overestimate of errors
                '                '



                ' correct for passive rates if needed by subtracting actinide rates from a passive xrf measurment file
                Dim temp_net_rates(5), temp_net_sig(5)
                For j = 1 To 5
                    If fiss_prod_corr_type = 3 Then temp_net_rates(j) = results_arr(j) - pass_sum_rates(j) * livetime_array(Result_num) Else temp_net_rates(j) = results_arr(j)
                    If fiss_prod_corr_type = 3 Then temp_net_sig(j) = (err_arr(j) ^ 2 + (livetime_array(Result_num) * pass_sum_rates_err(j)) ^ 2) ^ 0.5 Else temp_net_sig(j) = err_arr(j)

                    '  MsgBox("r_a = " & results_arr(j) & " p_r = " & pass_sum_rates(j) & " t_r = " & temp_rates(j))
                Next j



                Z1_Z2_rate_ratio_sig(i, Result_num) = 0
                If results_arr(i) <> 0 Then Z1_Z2_rate_ratio(i, Result_num) = temp_net_rates(ref_element) / temp_net_rates(i) Else Z1_Z2_rate_ratio(i, Result_num) = 0
                    If i = ref_element Then Z1_Z2_rate_ratio(i, Result_num) = 1
                If results_arr(i) <> 0 Then Z1_Z2_rate_ratio_sig(i, Result_num) = (temp_net_sig(ref_element) / temp_net_rates(i)) ^ 2 + (temp_net_rates(ref_element) / temp_net_rates(i) ^ 2 * temp_net_sig(i)) ^ 2
                '  If results_arr(i) <> 0 Then Z1_Z2_rate_ratio_sig(i, Result_num) = Z1_Z2_rate_ratio_sig(i, Result_num) - 2 * (temp_rates(ref_element) / temp_rates(i) ^ 3) * raw_count_covar_array(ref_element, i, Result_num)
                If results_arr(i) <> 0 Then Z1_Z2_rate_ratio_sig(i, Result_num) = Z1_Z2_rate_ratio_sig(i, Result_num) ^ 0.5
                    If ref_element = i Then Z1_Z2_rate_ratio_sig(i, Result_num) = 0
                Next i
            '
            'Call calc_vms_conc2(fitted_spec)

            Call calc_vms_rates()
            vms_u_rate1(Result_num) = u1_out
            vms_u_rate2(Result_num) = u2_out
            vms_pu_rate(Result_num) = pu_out

            prelim_U_array(Result_num) = prelim_U_conc
                prelimin_Pu_array(Result_num) = prelim_Pu_conc
                reference_temp(Result_num) = Const_val(37)
                temp_linear(Result_num) = Const_val(38)
                Temp_correction(Result_num) = 1

                If sample_temp(Result_num) <> 0 Then Temp_correction(Result_num) = 1 + (sample_temp(Result_num) - reference_temp(Result_num)) * temp_linear(Result_num)

                enrichment = u_enrich(Result_num)
                U234 = enrichment * (0.0055 / 0.71)
                U238 = 100 - U234 - enrichment

                enrich_correction(Result_num) = 1
                If enrichment <> 0 Then enrich_correction(Result_num) = 238 / ((U234 * 234 + enrichment * 235 + U238 * 238) / 100)

                Pu_wgt = pu_iso_wgt(Result_num)
                Pu_wgt_ref = 244
                Pu_wgt_correction(Result_num) = 1
                If Pu_wgt <> 0 Then Pu_wgt_correction(Result_num) = Pu_wgt_ref / Pu_wgt

                sample_correction(Result_num) = enrich_correction(Result_num) * Temp_correction(Result_num)
            '
            For i_corr = 1 To 5
                corr_array(i_corr) = 1
                If element_symbol(element_index(i_corr)) = "Pu" Then corr_array(i_corr) = Pu_wgt_correction(Result_num)

            Next i_corr

            num_iterations(Result_num) = fit_iterations

        Next Result_num
            '   ----------------------------------------------------------------------------------------
            '                  End read results
            '   ----------------------------------------------------------------------------------------
            '
            Dim Ma_avg

        Dim Ma_sigma, Ma_stdev, corr_Ma_stdev
        Dim corr_U_avg, corr_Pu_avg, corr_Np_avg, corr_Am_avg, corr_Cm_avg, corr_Ma_avg
        Dim corr_U_sigma, corr_Np_sigma, corr_Pu_sigma, corr_Am_sigma, corr_Cm_sigma, corr_Ma_sigma
        '
        '  initialize arrays to calculate average results
        For mdex = 6 To 42
            par_avg(mdex) = 0
            par_stdev(mdex) = 0
            par_sigma(mdex) = 0
            temp_array(mdex) = 0
            temp_array_err(mdex) = 0
            par_wt(mdex) = 1
        Next mdex

        For i = 1 To 5
            raw_wgt_avg(i) = 0
            raw_wgt(i) = 0
            raw_stdev(i) = 0
            wt_avg_corr_rate(i) = 0
            corr_count_rate_stdev(i) = 0
            wt_avg_corr_rate(i) = 0
            wt_avg_rate_ratio(i) = 0
            wt_avg_rate_ratio_sig(i) = 0
            wt_avg_rate_ratio_wgt(i) = 0
        Next i
        '
        '   calculate average counts and count ratios for the 5 actinides

        For j = 1 To 5
            For i = 1 To num_files
                temp_array1(i) = raw_count(j, i)
                temp_array1_sig(i) = raw_count_sig(j, i)

                temp_array2(i) = corr_rate(j, i)
                temp_array2_sig(i) = corr_rate_sig(j, i)

                temp_array3(i) = Z1_Z2_rate_ratio(j, i)
                temp_array3_sig(i) = Z1_Z2_rate_ratio_sig(j, i)

            Next i

            raw_wgt_avg(j) = wt_average(temp_array1, temp_array1_sig, num_files, 0)
            raw_stdev(j) = stdev(temp_array1, num_files)
            raw_wgt_avg_sig(j) = wt_average(temp_array1, temp_array1_sig, num_files, 1)

            wt_avg_corr_rate(j) = wt_average(temp_array2, temp_array2_sig, num_files, 0)
            corr_count_rate_stdev(j) = stdev(temp_array2, num_files)
            wt_avg_corr_rate_sig(j) = wt_average(temp_array2, temp_array2_sig, num_files, 1)

            wt_avg_rate_ratio(j) = wt_average(temp_array3, temp_array3_sig, num_files, 0)
            If j = ref_element Then wt_avg_rate_ratio(j) = 1
            wt_avg_rate_ratio_sig(j) = wt_average(temp_array3, temp_array3_sig, num_files, 1)
        Next j
        '
        ' calculate weighted averages for remaining parameters
        '
        For mdex = 6 To 42
            simple_avg(mdex) = 0
            simple_avg_err(mdex) = 0
        Next mdex

        For i = 1 To num_files

            For mdex = 6 To 42
                simple_avg(mdex) = simple_avg(mdex) + temp_matrix(mdex, i)              ' simple average
                simple_avg_err(mdex) = simple_avg_err(mdex) + err_arr(mdex) ^ 2         '  parameter weights
            Next mdex

        Next i

        For mdex = 6 To 42
            simple_avg(mdex) = simple_avg(mdex) / num_files
            simple_avg_err(mdex) = simple_avg_err(mdex) ^ 0.5 / num_files
        Next mdex

        For mdex = 6 To 42
            For i = 1 To num_files
                temp_array(i) = temp_matrix(mdex, i)
                temp_array_err(i) = temp_matrix_err(mdex, i)
            Next i
            par_avg(mdex) = wt_avg(temp_array, temp_array_err, num_files, 1)         ' weighted average parameter 
            par_sigma(mdex) = wt_avg(temp_array, temp_array_err, num_files, 2)       ' weighted average parameter uncertainty
            par_stdev(mdex) = st_dev(temp_array, num_files)                          ' parameter standard deviation
        Next mdex

        ' define column headings
        Dim col_labels(100) As String

        col_labels(1) = "Cycle"
        col_labels(2) = actinide_names(element_index(1))
        col_labels(3) = "Sigma_" & element_symbol(element_index(1))
        col_labels(4) = actinide_names(element_index(2))
        col_labels(5) = "Sigma_" & element_symbol(element_index(2))
        col_labels(6) = actinide_names(element_index(3))
        col_labels(7) = "Sigma_" & element_symbol(element_index(3))
        col_labels(8) = actinide_names(element_index(4))
        col_labels(9) = "Sigma_" & element_symbol(element_index(4))
        col_labels(10) = actinide_names(element_index(5))
        col_labels(11) = "Sigma_" & element_symbol(element_index(5))
        col_labels(12) = fiss_prod_name(1) & " (Counts)"
        col_labels(13) = "Sigma_" & fiss_prod_name(1)
        col_labels(14) = fiss_prod_name(2) & " (Counts)"
        col_labels(15) = "Sigma_" & fiss_prod_name(2)
        col_labels(16) = fiss_prod_name(3) & " (Counts)"
        col_labels(17) = "Sigma_" & fiss_prod_name(3)
        col_labels(18) = "[" & element_symbol(element_index(1)) & "]"
        col_labels(19) = "Sigma_[" & element_symbol(element_index(1)) & "]"
        col_labels(20) = "[" & element_symbol(element_index(2)) & "]"
        col_labels(21) = "Sigma_[" & element_symbol(element_index(2)) & "]"
        col_labels(22) = "[" & element_symbol(element_index(3)) & "]"
        col_labels(23) = "Sigma_[" & element_symbol(element_index(3)) & "]"
        col_labels(24) = "[" & element_symbol(element_index(4)) & "]"
        col_labels(25) = "Sigma_[" & element_symbol(element_index(4)) & "]"
        col_labels(26) = "[" & element_symbol(element_index(5)) & "]"
        col_labels(27) = "Sigma_[" & element_symbol(element_index(5)) & "]"
        col_labels(28) = "[Matrix]"
        col_labels(29) = "Sigma_[Matrix]"
        col_labels(30) = "E offset (keV)"
        col_labels(31) = "Sigma_E offset"
        col_labels(32) = "E linear (keV/ch)"
        col_labels(33) = "Sigma_E linear"
        col_labels(34) = "Gaussian Width"
        col_labels(35) = "Sigma Gaussian Width"

        col_labels(36) = "Tail Int. (relative)"
        col_labels(37) = "Sigma_Tail Intensity"
        col_labels(38) = "Tail decay constant"
        col_labels(39) = "Sigma_decay const"
        col_labels(40) = "Step Background"
        col_labels(41) = "Sigma_step bkg"
        col_labels(42) = "Ge escape Int. (relative)"
        col_labels(43) = "Sigma_Ge escape"
        col_labels(44) = "Ref Peak Width (keV)"
        col_labels(45) = "Sigma_Peak Width"
        col_labels(46) = "Ref Peak Energy (keV)"
        col_labels(47) = "Sigma_Peak Energy"
        col_labels(48) = "Ref Peak Counts"
        col_labels(49) = "Sigma_peak counts"
        col_labels(50) = "Random Background"
        col_labels(51) = "Sigma_Rand Bkg"
        col_labels(52) = "Linear Bkg constant"
        col_labels(53) = "Sigma_Bkg constant"
        col_labels(54) = "Linear Bkg slope"
        col_labels(55) = "Sigma_Bkg slope"
        col_labels(56) = "HV End Point (keV)"
        col_labels(57) = "Sigma_HV End Point (keV)"
        col_labels(58) = "Shaping Parameter"
        col_labels(59) = "Sigma_Shaping Parameter"
        col_labels(60) = "Brem. Back Scatter Intensity"
        col_labels(61) = "Sigma_Brem. Back Scatter"
        col_labels(62) = "Elastic Scatter Fraction (rel.)"
        col_labels(63) = "Sigma_Elastic Scatter"

        col_labels(64) = "Spline E Ref. 1"
        col_labels(65) = "Sigma_ E Ref. 1"
        col_labels(66) = "Spline E Ref. 2"
        col_labels(67) = "Sigma_ E Ref. 2"
        col_labels(68) = "Spline par A1"
        col_labels(69) = "Sigma_par A1"
        col_labels(70) = "Spline par A2"
        col_labels(71) = "Sigma_par A2"
        col_labels(72) = "Spline Tau"
        col_labels(73) = "Sigma_Spline Tau"

        col_labels(74) = "high tail int"
        col_labels(75) = "high tail int err"
        col_labels(76) = "high tail decay"
        col_labels(77) = "high tail decay err"

        col_labels(78) = "ref peak step int"
        col_labels(79) = "ref peak step int err"
        col_labels(80) = "W X-ray Int"
        col_labels(81) = "W X-ray Int err"
        col_labels(82) = "SS Wal Thickness"
        col_labels(83) = "SS Wal Thickness err"
        col_labels(84) = "Pb X-ray Int"
        col_labels(85) = "Pb X-ray Int err"

        col_labels(86) = "Sample_Temperature"
        col_labels(87) = "File_Name"
        col_labels(88) = "count time"
        col_labels(89) = "Total Rate"
        col_labels(90) = "VMS U Rate 1"
        col_labels(91) = "VMS U Rate 2"
        col_labels(92) = "VMS Pu Rate"

        col_labels(93) = "Iterations"
        col_labels(94) = "chi_square"


        With summary_table
            .Columns.Add("Cycle", System.Type.GetType("System.String"))
            .Columns.Add(col_labels(2), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(3), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(4), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(5), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(6), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(7), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(8), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(9), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(10), System.Type.GetType("System.Double"))
            .Columns.Add(col_labels(11), System.Type.GetType("System.Double"))

            For i = 12 To 94
                .Columns.Add(col_labels(i), System.Type.GetType("System.String"))
            Next i
        End With

        Dim trunc_num
        '  Place raw counts for each element and file into summary table
        For i = 1 To num_files
            trunc_num = 10
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = i
            newrow(col_labels(2)) = (Format(raw_count(1, i), "###0.0 "))
            newrow(col_labels(3)) = (Format(raw_count_sig(1, i), "###0.0 "))
            newrow(col_labels(4)) = (Format(raw_count(2, i), "###0.0 "))
            newrow(col_labels(5)) = (Format(raw_count_sig(2, i), "###0.0 "))
            newrow(col_labels(6)) = (Format(raw_count(3, i), "###0.0 "))
            newrow(col_labels(7)) = (Format(raw_count_sig(3, i), "###0.0 "))
            newrow(col_labels(8)) = (Format(raw_count(4, i), "###0.0 "))
            newrow(col_labels(9)) = (Format(raw_count_sig(4, i), "###0.0 "))
            newrow(col_labels(10)) = (Format(raw_count(5, i), "###0.0 "))
            newrow(col_labels(11)) = (Format(raw_count_sig(5, i), "###0.0 "))

            Dim temp_format1 As String



            For mdex = 6 To 42
                temp_format1 = "###0.0000000"
                If temp_matrix(mdex, i) = 0 Then temp_format1 = "###0.0"
                If temp_matrix(mdex, i) > 1 Then temp_format1 = "###0.00000"
                If temp_matrix(mdex, i) > 100 Then temp_format1 = "###0.000"
                If temp_matrix(mdex, i) > 1000000 Then temp_format1 = "###0"
                newrow(col_labels(14 + 2 * (mdex - 7))) = Format(temp_matrix(mdex, i), temp_format1)
                newrow(col_labels(15 + 2 * (mdex - 7))) = Format(temp_matrix_err(mdex, i), temp_format1)
            Next mdex

            newrow(col_labels(86)) = (Format(sample_temp(i), "##0.00"))
            newrow(col_labels(87)) = input_file_names(i)

            newrow(col_labels(88)) = livetime_array(i)
            newrow(col_labels(89)) = Format(xrf_total_rate(i), "#####0.0")

            newrow(col_labels(90)) = Format(vms_u_rate1(i), "#####0.0")
            newrow(col_labels(91)) = Format(vms_u_rate2(i), "#####0.0")
            newrow(col_labels(92)) = Format(vms_pu_rate(i), "#####0.0")

            newrow(col_labels(93)) = Format(num_iterations(i), "####0")
            newrow(col_labels(94)) = Format(chisqr_array(i), "##0.000")


            summary_table.Rows.Add(newrow)

        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i


        Ma_avg = 0
        Ma_stdev = 0
        Ma_sigma = 0


        trunc_num = 1000

        ' place weighted average counts and uncertainties into table
        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Weighted Average"
            newrow(col_labels(2)) = Int(trunc_num * raw_wgt_avg(1)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * raw_wgt_avg_sig(1)) / trunc_num

            newrow(col_labels(4)) = Int(trunc_num * raw_wgt_avg(2)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * raw_wgt_avg_sig(2)) / trunc_num

            newrow(col_labels(6)) = Int(trunc_num * raw_wgt_avg(3)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * raw_wgt_avg_sig(3)) / trunc_num

            newrow(col_labels(8)) = Int(trunc_num * raw_wgt_avg(4)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * raw_wgt_avg_sig(4)) / trunc_num

            newrow(col_labels(10)) = Int(trunc_num * raw_wgt_avg(5)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * raw_wgt_avg_sig(5)) / trunc_num

            For mdex = 6 To 38
                newrow(col_labels(14 + 2 * (mdex - 7))) = Int(trunc_num * par_avg(mdex)) / trunc_num
                newrow(col_labels(15 + 2 * (mdex - 7))) = Int(trunc_num * par_sigma(mdex)) / trunc_num
            Next mdex
            summary_table.Rows.Add(newrow)
        Next i

        '   place standard deviation counts for each element into the tble 

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Standard Deviation"
            newrow(col_labels(2)) = Int(trunc_num * raw_stdev(1)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * raw_stdev(2)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * raw_stdev(3)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * raw_stdev(4)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * raw_stdev(5)) / trunc_num

            For mdex = 7 To 42
                newrow(col_labels(14 + 2 * (mdex - 7))) = Int(trunc_num * par_stdev(mdex)) / trunc_num
            Next mdex
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        '  ==========================================================
        '          Place corrected count rates into the table
        '  ==========================================================
        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Corrected Count Rates"
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To num_files
            trunc_num = 1000
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = i
            newrow(col_labels(2)) = Int(trunc_num * corr_rate(1, i)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * corr_rate_sig(1, i)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * corr_rate(2, i)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * corr_rate_sig(2, i)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * corr_rate(3, i)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * corr_rate_sig(3, i)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * corr_rate(4, i)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * corr_rate_sig(4, i)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * corr_rate(5, i)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * corr_rate_sig(4, i)) / trunc_num

            summary_table.Rows.Add(newrow)

        Next i


        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = " "
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Weighted Average Corrected Rates"
            trunc_num = 1000
            newrow(col_labels(2)) = Int(trunc_num * wt_avg_corr_rate(1)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * wt_avg_corr_rate_sig(1)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * wt_avg_corr_rate(2)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * wt_avg_corr_rate_sig(2)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * wt_avg_corr_rate(3)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * wt_avg_corr_rate_sig(3)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * wt_avg_corr_rate(4)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * wt_avg_corr_rate_sig(4)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * wt_avg_corr_rate(5)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * wt_avg_corr_rate_sig(5)) / trunc_num

            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Standard Deviation "
            trunc_num = 1000
            newrow(col_labels(2)) = Int(trunc_num * corr_count_rate_stdev(1)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * corr_count_rate_stdev(2)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * corr_count_rate_stdev(3)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * corr_count_rate_stdev(4)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * corr_count_rate_stdev(5)) / trunc_num

            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Corr.St.Dev./n^0.5"
            trunc_num = 1000
            newrow(col_labels(2)) = Int(trunc_num * corr_count_rate_stdev(1) / Max(1, num_files) ^ 0.5) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * corr_count_rate_stdev(2) / Max(1, num_files) ^ 0.5) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * corr_count_rate_stdev(3) / Max(1, num_files) ^ 0.5) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * corr_count_rate_stdev(4) / Max(1, num_files) ^ 0.5) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * corr_count_rate_stdev(5) / Max(1, num_files) ^ 0.5) / trunc_num

            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Passive Rates from MEXRF Summary file"
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Rates"
            trunc_num = 1000
            newrow(col_labels(2)) = Int(trunc_num * pass_sum_rates(1)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * pass_sum_rates_err(1)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * pass_sum_rates(2)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * pass_sum_rates_err(2)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * pass_sum_rates(3)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * pass_sum_rates_err(3)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * pass_sum_rates(4)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * pass_sum_rates_err(4)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * pass_sum_rates(5)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * pass_sum_rates_err(5)) / trunc_num

            summary_table.Rows.Add(newrow)
        Next i
        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Peak Ratios Relative to Reference Element"
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To num_files
            trunc_num = 10000
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = i
            newrow(col_labels(2)) = Int(trunc_num * Z1_Z2_rate_ratio(1, i)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * Z1_Z2_rate_ratio_sig(1, i)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * Z1_Z2_rate_ratio(2, i)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * Z1_Z2_rate_ratio_sig(2, i)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * Z1_Z2_rate_ratio(3, i)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * Z1_Z2_rate_ratio_sig(3, i)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * Z1_Z2_rate_ratio(4, i)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * Z1_Z2_rate_ratio_sig(4, i)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * Z1_Z2_rate_ratio(5, i)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * Z1_Z2_rate_ratio_sig(5, i)) / trunc_num


            summary_table.Rows.Add(newrow)

        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Weighted Average Raw Ratios"
            trunc_num = 10000
            newrow(col_labels(2)) = Int(trunc_num * wt_avg_rate_ratio(1)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * wt_avg_rate_ratio_sig(1)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * wt_avg_rate_ratio(2)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * wt_avg_rate_ratio_sig(2)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * wt_avg_rate_ratio(3)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * wt_avg_rate_ratio_sig(3)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * wt_avg_rate_ratio(4)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * wt_avg_rate_ratio_sig(4)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * wt_avg_rate_ratio(5)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * wt_avg_rate_ratio_sig(5)) / trunc_num


            summary_table.Rows.Add(newrow)
        Next i




        Dim new_u, new_pu, new_u_err, new_pu_err, U_Pu_ratio, U_Pu_ratio_err, u_dex, pu_dex, corr_act_conc(5), corr_act_conc_err(5)
        u_dex = 0
        pu_dex = 0
        corr_act_conc(1) = corr_U_avg
        corr_act_conc(2) = corr_Np_avg
        corr_act_conc(3) = corr_Pu_avg
        corr_act_conc(4) = corr_Am_avg
        corr_act_conc(5) = corr_Cm_avg

        corr_act_conc_err(1) = corr_U_sigma
        corr_act_conc_err(2) = corr_Np_sigma
        corr_act_conc_err(3) = corr_Pu_sigma
        corr_act_conc_err(4) = corr_Am_sigma
        corr_act_conc_err(5) = corr_Cm_sigma

        For i_corr = 1 To 5

            If element_symbol(element_index(i_corr)) = "Pu" Then pu_dex = i_corr
            If element_symbol(element_index(i_corr)) = "U" Then u_dex = i_corr

        Next i_corr

        new_u = corr_act_conc(u_dex)
        new_pu = corr_act_conc(pu_dex)
        new_u_err = corr_act_conc_err(u_dex)
        new_pu_err = corr_act_conc_err(pu_dex)

        If raw_wgt_avg(2) = 0 Then U_Pu_ratio = 0 Else U_Pu_ratio = raw_wgt_avg(1) / raw_wgt_avg(2)
        If raw_wgt_avg(2) = 0 Then U_Pu_ratio_err = 0 Else U_Pu_ratio_err = (Abs((raw_wgt_avg_sig(1) / raw_wgt_avg(2)) ^ 2 + (raw_wgt_avg(1) * raw_wgt_avg_sig(2) / raw_wgt_avg(2) ^ 2) ^ 2)) ^ 0.5

        Dim temp_ele As Integer
        temp_ele = 2
        If ref_element = 2 Then temp_ele = 1

        UPu_Box.Text = Int(wt_avg_rate_ratio(temp_ele) * 10000) / 10000
        UPu_err_Box.Text = Int(wt_avg_rate_ratio_sig(temp_ele) * 10000) / 10000
        '    UPu_Box.Text = Int(U_Pu_ratio * 10000) / 10000
        '    UPu_err_Box.Text = Int(U_Pu_ratio_err * 10000) / 10000

        ' ----------------------------------------------------------------------------------------------
        '    Start Hybrid analysis
        ' ----------------------------------------------------------------------------------------------
        '

        Dim E_HV, par_x, R_UPU_INITIAL, R_UAm_initial, au_apu_initial, u_wgt
        Dim sum_lines(5), norm_branch(5), U_PU_init, U_PU_init_sig

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

        u_wgt = (U_enrichment / 100 * 235 + (1 - U_enrichment / 100) * 238)

        par_x = results_arr(29)
        E_HV = results_arr(28)
        '
        '   ****************************************************************************************
        '
        If MEXRF_main.KED_check_box_1.Checked Then E_HV = MEXRF_main.KED_Box_1.Text
        par_x = 1.109 - 0.00435 * 74 + 0.00175 * E_HV
        If MEXRF_main.KED_check_box_2.Checked Then par_x = MEXRF_main.KED_Box_2.Text

        '        If both Then U And Pu boxes have been checked treat U as the primary element
        ref_element = 1
        If MEXRF_main.KED_check_box_4.Checked Then ref_element = 2
        If MEXRF_main.KED_check_box_3.Checked Then ref_element = 1
        If MEXRF_main.ref_el_U_button.Checked = True Then ref_element = 1
        If MEXRF_main.ref_el_Pu_button.Checked = True Then ref_element = 2

        '
        '   ****************************************************************************************
        '
        Dim concentrations(6)
        ' use independent results for calculating R_UPu
        For i = 1 To 6
            concentrations(i) = 0
            concentrations(i) = temp_matrix(8 + i, 1)
        Next i

        ' overwrite independent results with MEKED values or manual entry from main screen.
        ' these concentrations will be used to calculate the values for R_UPu, R_UNp ...

        If MEXRF_main.KED_check_box_3.Checked Then concentrations(1) = MEXRF_main.KED_Box_3.Text
        If MEXRF_main.KED_check_box_4.Checked Then concentrations(2) = MEXRF_main.KED_Box_4.Text
        If MEXRF_main.KED_check_box_5.Checked Then concentrations(3) = MEXRF_main.KED_Box_5.Text
        If MEXRF_main.KED_check_box_6.Checked Then concentrations(4) = MEXRF_main.KED_Box_6.Text
        If MEXRF_main.KED_check_box_7.Checked Then concentrations(5) = MEXRF_main.KED_Box_7.Text
        If MEXRF_main.KED_check_box_8.Checked Then concentrations(6) = MEXRF_main.KED_Box_8.Text

        ' set each concentration to a slightly positive value to avoid divide by zero in ratio calculations
        For i = 1 To 6
            If concentrations(i) <= 0 Then concentrations(i) = 0.000000001
        Next i

        Dim conc_array(6), hv_temp, par_x_temp, de_temp

        ' The order of the elements is important in calculating the R_Upu etc ratios

        For i = 1 To num_files
            de_temp = temp_matrix(16, i)
            hv_temp = temp_matrix(28, i)
            E_HV_err = temp_matrix_err(28, i)

            par_x_temp = temp_matrix(29, i)
            par_x1_err = temp_matrix_err(29, i)

            For j = 1 To 6
                conc_array(j) = 0
                conc_array(j) = temp_matrix(8 + j, i)
                If conc_array(j) <= 0 Then conc_array(j) = 0.000000001
                concent_in_err(j) = temp_matrix_err(j + 8, i)
            Next j

            If MEXRF_main.KED_check_box_1.Checked Then hv_temp = MEXRF_main.KED_Box_1.Text
            If MEXRF_main.KED_check_box_2.Checked Then par_x_temp = MEXRF_main.KED_Box_2.Text

            If MEXRF_main.KED_check_box_3.Checked Then conc_array(1) = MEXRF_main.KED_Box_3.Text
            If MEXRF_main.KED_check_box_4.Checked Then conc_array(2) = MEXRF_main.KED_Box_4.Text
            If MEXRF_main.KED_check_box_5.Checked Then conc_array(3) = MEXRF_main.KED_Box_5.Text
            If MEXRF_main.KED_check_box_6.Checked Then conc_array(4) = MEXRF_main.KED_Box_6.Text
            If MEXRF_main.KED_check_box_7.Checked Then conc_array(5) = MEXRF_main.KED_Box_7.Text
            If MEXRF_main.KED_check_box_8.Checked Then conc_array(6) = MEXRF_main.KED_Box_8.Text

            If MEXRF_main.KED_check_box_1.Checked Then E_HV_err = KED_PAR_err(8)
            If MEXRF_main.KED_check_box_2.Checked Then par_x1_err = KED_PAR_err(10)


            If MEXRF_main.KED_check_box_3.Checked Then concent_in_err(1) = KED_PAR_err(1)
            If MEXRF_main.KED_check_box_4.Checked Then concent_in_err(2) = KED_PAR_err(2)
            If MEXRF_main.KED_check_box_5.Checked Then concent_in_err(3) = KED_PAR_err(3)
            If MEXRF_main.KED_check_box_6.Checked Then concent_in_err(4) = KED_PAR_err(4)
            If MEXRF_main.KED_check_box_7.Checked Then concent_in_err(5) = KED_PAR_err(5)
            If MEXRF_main.KED_check_box_8.Checked Then concent_in_err(6) = KED_PAR_err(6)



            If MEXRF_main.use_MEXRF_box.Checked = True Then hv_temp = temp_matrix(28, i)                                     ' if use_mexrf_box is checked use the MEXRF HV
            If MEXRF_main.use_MEXRF_box.Checked = True Then par_x_temp = 1.109 - 0.00435 * 74 + 0.00175 * hv_temp            ' if use_mexrf_box is checked use the MEXRF shaping parameter
            If MEXRF_main.use_MEXRF_box.Checked = True Then E_HV_err = temp_matrix_err(28, i)                                ' if use_mexrf_box is checked use the MEXRF HV
            If MEXRF_main.use_MEXRF_box.Checked = True Then par_x1_err = 0.00175 * E_HV_err                                  ' if use_mexrf_box is checked use the MEXRF shaping parameter
            If MEXRF_main.use_MEXRF_box.Checked = True Then conc_array(1) = temp_matrix(9, i)                                ' if use_mexrf_box is checked use the MEXRF shaping parameter
            If MEXRF_main.use_MEXRF_box.Checked = True Then concent_in_err(1) = temp_matrix_err(9, i)                        ' if use_mexrf_box is checked use the MEXRF HV
            '
            '   Calculate conversion factors
            '
            '   Dim Z1_Z2_conv_fact(5, 99), Z1_Z2_conv_fact_sig(5, 99)


            For j = 1 To 5
                Z1_Z2_conv_fact(j, i) = calc_R_UZ2(j, hv_temp, par_x_temp, conc_array) * norm_branch(j)
                '  Z1_Z2_conv_fact_sig(j, i) = calc_R_UZ2_ERR(j, hv_temp, par_x_temp, conc_array, E_HV_err, par_x1_err, concent_in_err) * norm_branch(j)
                Z1_Z2_conv_fact_sig(j, i) = 0           ' estimated uncertainty is insignifcant - turned off calculation due to time required for estimate
            Next j


        Next i
        For i = 1 To 6
            concent_in_err(i) = 0
        Next i

        ' determine average conversion factor for each element
        For j = 2 To 5
            For i = 1 To num_files
                temp_array(i) = Z1_Z2_conv_fact(j, i)
            Next i

            Z1_Z2_avg_conv_fact(j) = average(temp_array, num_files)
            '   Z1_Z2_avg_conv_fact_sig(j) = stdev(temp_array, num_files)
            Z1_Z2_avg_conv_fact_sig(j) = Z1_Z2_conv_fact_sig(j, 1)


        Next j

        Z1_Z2_avg_conv_fact(1) = 1
        Z1_Z2_avg_conv_fact(2) = Z1_Z2_avg_conv_fact(2)               '  average r_z1z2_ratio
        Z1_Z2_avg_conv_fact(3) = Z1_Z2_avg_conv_fact(3)
        Z1_Z2_avg_conv_fact(4) = Z1_Z2_avg_conv_fact(4)
        Z1_Z2_avg_conv_fact(5) = Z1_Z2_avg_conv_fact(5)
        ' z1_z2_avg_conv_fact(5) = z1_z2_avg_conv_fact(2) ^ 1.5

        R_UPU_INITIAL = Z1_Z2_avg_conv_fact(2)

        z1_z2_avg_conv_fact_err(1) = 0
        z1_z2_avg_conv_fact_err(2) = Z1_Z2_avg_conv_fact_sig(2)
        z1_z2_avg_conv_fact_err(3) = Z1_Z2_avg_conv_fact_sig(3)
        z1_z2_avg_conv_fact_err(4) = Z1_Z2_avg_conv_fact_sig(4)
        z1_z2_avg_conv_fact_err(5) = Z1_Z2_avg_conv_fact_sig(5)


        R_UPU_box.Text = Int(10000 * Z1_Z2_avg_conv_fact(2)) / 10000
        R_UPU_err_Box.Text = Int(10000 * z1_z2_avg_conv_fact_err(2)) / 10000

        au_apu_initial = u_wgt / Pu_weight                                      '  atomic weight correction

        Dim ref_wgt, np_wgt, am_wgt, cm_wgt
        ref_wgt = u_wgt
        np_wgt = 237
        am_wgt = 241
        cm_wgt = 244
        If ref_element = 2 Then ref_wgt = Pu_weight

        atomic_mass_ratio(1) = ref_wgt / u_wgt
        atomic_mass_ratio(2) = ref_wgt / Pu_weight
        atomic_mass_ratio(3) = ref_wgt / np_wgt
        atomic_mass_ratio(4) = ref_wgt / cm_wgt
        atomic_mass_ratio(5) = ref_wgt / am_wgt



        UPU_wt_ratio_box.Text = Int(10000 * au_apu_initial) / 10000

        U_PU_init = wt_avg_rate_ratio(2) / R_UPU_INITIAL * au_apu_initial                 '  U_Pu concentration ratio
        U_PU_init_sig = wt_avg_rate_ratio_sig(2) / R_UPU_INITIAL * au_apu_initial


        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        ' display calculated RUPu ratio

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "R_Z(1):Z(n)"
            trunc_num = 100000
            newrow(col_labels(2)) = Int(trunc_num * Z1_Z2_avg_conv_fact(1)) / trunc_num        '  all ratios relative to U concentration
            newrow(col_labels(3)) = Int(trunc_num * z1_z2_avg_conv_fact_err(1)) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * Z1_Z2_avg_conv_fact(2)) / trunc_num        '  R_UPu ratio
            newrow(col_labels(5)) = Int(trunc_num * z1_z2_avg_conv_fact_err(2)) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * Z1_Z2_avg_conv_fact(3)) / trunc_num        '  R UNp ratio
            newrow(col_labels(7)) = Int(trunc_num * z1_z2_avg_conv_fact_err(3)) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * Z1_Z2_avg_conv_fact(4)) / trunc_num        '  R_UCm ratio
            newrow(col_labels(9)) = Int(trunc_num * z1_z2_avg_conv_fact_err(4)) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * Z1_Z2_avg_conv_fact(5)) / trunc_num       '   R_UAm ratio
            newrow(col_labels(11)) = Int(trunc_num * z1_z2_avg_conv_fact_err(5)) / trunc_num

            summary_table.Rows.Add(newrow)
        Next i

        'calculate fission product correction factor using emprical method

        Dim fiss_corr_factor, fiss_corr_factor_err, compton_rate, compton_rate_err
        fiss_corr_factor = 1
        fiss_corr_factor_err = 0
        ' If fiss_prod_corr_type <> 4 Then GoTo skipfisscorr

        For i = 1 To num_files
            temp_array1(i) = 0
            temp_array1(i) = temp_matrix(26, i) * (Const_val(78) - Const_val(77)) + temp_matrix(27, i) * (Const_val(78) ^ 2 - Const_val(77) ^ 2) / 2
            temp_array1(i) = temp_array1(i) / livetime_array(i) / temp_matrix(16, i)
        Next i

        For i = 1 To num_files
            temp_array2(i) = 0
            temp_array2(i) = ((temp_matrix_err(26, i) * (Const_val(78) - Const_val(77))) ^ 2 + (temp_matrix_err(27, i) * (Const_val(78) ^ 2 - Const_val(77) ^ 2) / 2) ^ 2) ^ 0.5
            temp_array2(i) = temp_array2(i) / livetime_array(i) / temp_matrix(16, i)
        Next i

        compton_rate = wt_average(temp_array1, temp_array2, num_files, 0)                    '  average(temp_array1, num_files)
        compton_rate_err = wt_average(temp_array1, temp_array2, num_files, 1)                '  stdev(temp_array1, num_files)


        If fiss_prod_corr_type <> 4 Then GoTo skipfisscorr

        fiss_corr_factor = Const_val(75) * compton_rate
        fiss_corr_factor_err = ((Const_val(76) * compton_rate) ^ 2 + (Const_val(75) * compton_rate_err) ^ 2) ^ 0.5
        fiss_corr_factor = 1 + fiss_corr_factor / Z1_Z2_avg_conv_fact(2)
        fiss_corr_factor_err = fiss_corr_factor_err / Z1_Z2_avg_conv_fact(2)

skipfisscorr:
        ' apply fission product correction factor using constant bias
        If fiss_prod_corr_type = 5 Then fiss_corr_factor = Const_val(71)
        If fiss_prod_corr_type = 5 Then fiss_corr_factor_err = Const_val(72)

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Self Interrogation corr factor"
            trunc_num = 100000
            newrow(col_labels(2)) = Int(trunc_num * 1) / trunc_num                          '  all ratios relative to U concentration
            newrow(col_labels(3)) = Int(trunc_num * 0) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * fiss_corr_factor) / trunc_num              '  R_UPu ratio
            newrow(col_labels(5)) = Int(trunc_num * fiss_corr_factor_err) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * fiss_corr_factor ^ 0.5) / trunc_num        '  R UNp ratio
            newrow(col_labels(7)) = Int(trunc_num * 0) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * fiss_corr_factor ^ 2) / trunc_num        '  R_UCm ratio
            newrow(col_labels(9)) = Int(trunc_num * 0) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * fiss_corr_factor ^ 1.5) / trunc_num       '   R_UAm ratio
            newrow(col_labels(11)) = Int(trunc_num * 0) / trunc_num

            summary_table.Rows.Add(newrow)
        Next i



        ' display calculated atomic weight ratios
        Dim blankit As String
        blankit = " "
        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "R_A(1):AZ(n)"
            trunc_num = 100000
            newrow(col_labels(2)) = Int(trunc_num * atomic_mass_ratio(1)) / trunc_num        '  all ratios relative to U concentration

            newrow(col_labels(4)) = Int(trunc_num * atomic_mass_ratio(2)) / trunc_num        '  R_UPu ratio

            newrow(col_labels(6)) = Int(trunc_num * atomic_mass_ratio(3)) / trunc_num        '  R UNp ratio

            newrow(col_labels(8)) = Int(trunc_num * atomic_mass_ratio(4)) / trunc_num        '  R_UCm ratio

            newrow(col_labels(10)) = Int(trunc_num * atomic_mass_ratio(5)) / trunc_num       '   R_UAm ratio

            summary_table.Rows.Add(newrow)
        Next i



        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Hybrid Analysis Results"
            summary_table.Rows.Add(newrow)
        Next i

        ' calcuate cycle by cycle concentration ratios
        For i = 1 To num_files
            trunc_num = 10000
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = i
            ref_element_norm = Z1_Z2_avg_conv_fact(2) ^ (1 - ref_element)       ' if ref_element=1 (is U) then re_element_norm = 1 
            temp_array1(i) = Z1_Z2_rate_ratio(1, i) * atomic_mass_ratio(1) / ref_element_norm
            If ref_element = 1 Then temp_array1_sig(i) = 0 Else temp_array1_sig(i) = Z1_Z2_rate_ratio_sig(1, i) * atomic_mass_ratio(2) / ref_element_norm

            '  **********
            temp_array2(i) = Z1_Z2_rate_ratio(2, i) * (atomic_mass_ratio(2) / (Z1_Z2_avg_conv_fact(2) * fiss_corr_factor)) / ref_element_norm
            If ref_element = 2 Then temp_array2_sig(i) = 0 Else temp_array2_sig(i) = Z1_Z2_rate_ratio_sig(2, i) / Z1_Z2_avg_conv_fact(2) * (atomic_mass_ratio(2) / ref_element_norm)
            '  **********

            temp_array3(i) = Z1_Z2_rate_ratio(3, i) * atomic_mass_ratio(3) / Z1_Z2_avg_conv_fact(3) / ref_element_norm
            temp_array3_sig(i) = Z1_Z2_rate_ratio_sig(3, i) / (Z1_Z2_avg_conv_fact(3) * ref_element_norm) * atomic_mass_ratio(3)

            temp_array4(i) = Z1_Z2_rate_ratio(4, i) / (Z1_Z2_avg_conv_fact(4) * ref_element_norm) * atomic_mass_ratio(4)
            temp_array4_sig(i) = Z1_Z2_rate_ratio_sig(4, i) / (Z1_Z2_avg_conv_fact(4) * ref_element_norm) * atomic_mass_ratio(4)

            temp_array5(i) = Z1_Z2_rate_ratio(5, i) / (Z1_Z2_avg_conv_fact(5) * ref_element_norm) * atomic_mass_ratio(5)
            temp_array5_sig(i) = Z1_Z2_rate_ratio_sig(5, i) / (Z1_Z2_avg_conv_fact(5) * ref_element_norm) * atomic_mass_ratio(5)

            'add concentration ratios to table

            newrow(col_labels(2)) = Int(trunc_num * temp_array1(i)) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * temp_array1_sig(i)) / trunc_num

            newrow(col_labels(4)) = Int(trunc_num * temp_array2(i)) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * temp_array2_sig(i)) / trunc_num

            newrow(col_labels(6)) = Int(trunc_num * temp_array3(i)) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * temp_array3_sig(i)) / trunc_num

            newrow(col_labels(8)) = Int(trunc_num * temp_array4(i)) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * temp_array4_sig(i)) / trunc_num

            newrow(col_labels(10)) = Int(trunc_num * temp_array5(i)) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * temp_array5_sig(i)) / trunc_num

            summary_table.Rows.Add(newrow)

            conc_ratio_array(1, i) = temp_array1(i)
            conc_ratio_array_err(1, i) = temp_array1_sig(i)
            conc_ratio_array(2, i) = temp_array2(i)
            conc_ratio_array_err(2, i) = temp_array2_sig(i)
            conc_ratio_array(3, i) = temp_array3(i)
            conc_ratio_array_err(3, i) = temp_array3_sig(i)
            conc_ratio_array(4, i) = temp_array4(i)
            conc_ratio_array_err(4, i) = temp_array4_sig(i)
            conc_ratio_array(5, i) = temp_array5(i)
            conc_ratio_array_err(5, i) = temp_array5_sig(i)


        Next i

            Dim R_UPu_avg, R_upu_avg_err, R_UAm_avg, R_UAm_avg_err, R_UCm_avg, R_UCm_avg_err, R_UNp_avg, R_UNp_avg_err
        Dim R_UU_avg, R_UU_avg_err

        R_UU_avg = 0
        R_UU_avg_err = 0
        R_UPu_avg = 0
        R_upu_avg_err = 0
        R_UAm_avg = 0
        R_UAm_avg_err = 0
        R_UCm_avg = 0
        R_UCm_avg_err = 0
        R_UNp_avg = 0
        R_UNp_avg_err = 0

        R_UU_avg = average(temp_array1, num_files)
        R_UU_avg_err = stdev(temp_array1, num_files) / num_files ^ 0.5
        R_UPu_avg = average(temp_array2, num_files)
        R_upu_avg_err = stdev(temp_array2, num_files) / num_files ^ 0.5
        R_UNp_avg = average(temp_array3, num_files)
        R_UNp_avg_err = stdev(temp_array3, num_files) / num_files ^ 0.5
        R_UCm_avg = average(temp_array4, num_files)
        R_UCm_avg_err = stdev(temp_array4, num_files) / num_files ^ 0.5
        R_UAm_avg = average(temp_array5, num_files)
        R_UAm_avg_err = stdev(temp_array5, num_files) / num_files ^ 0.5


        For i = 1 To 1
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = ""
            summary_table.Rows.Add(newrow)
        Next i

        For i = 1 To 1
            trunc_num = 100000
            Dim newrow As DataRow = summary_table.NewRow
            newrow("Cycle") = "Average Concentration Ratios Relative to Reference Element"
            newrow(col_labels(2)) = Int(trunc_num * R_UU_avg) / trunc_num
            newrow(col_labels(3)) = Int(trunc_num * R_UU_avg_err) / trunc_num
            newrow(col_labels(4)) = Int(trunc_num * R_UPu_avg) / trunc_num
            newrow(col_labels(5)) = Int(trunc_num * R_upu_avg_err) / trunc_num
            newrow(col_labels(6)) = Int(trunc_num * R_UNp_avg) / trunc_num
            newrow(col_labels(7)) = Int(trunc_num * R_UNp_avg_err) / trunc_num
            newrow(col_labels(8)) = Int(trunc_num * R_UCm_avg) / trunc_num
            newrow(col_labels(9)) = Int(trunc_num * R_UCm_avg_err) / trunc_num
            newrow(col_labels(10)) = Int(trunc_num * R_UAm_avg) / trunc_num
            newrow(col_labels(11)) = Int(trunc_num * R_UAm_avg_err) / trunc_num

            summary_table.Rows.Add(newrow)

        Next i


100:    grid1.DataSource = summary_table


        Dim corrected_rates_avg(99), corrected_rates_avg_err(99), rates_temp(99), error_temp(99)


        For i = 1 To 5

            corrected_rates_avg(i) = 0
            corrected_rates_avg_err(i) = 0
        Next i

        '       CALCULATE WEIGHTED AVERAGE RATES

        For i = 1 To 5
            For j = 1 To num_files
                rates_temp(j) = corr_rate(i, j)
                error_temp(j) = corr_rate_sig(i, j)
            Next j
            corrected_rates_avg(i) = wt_average(rates_temp, error_temp, num_files, 0)
            corrected_rates_avg_err(i) = wt_average(rates_temp, error_temp, num_files, 1)

        Next i

        Me.actresbox1.Text = Int(corrected_rates_avg(1) * 1000 + 0.5) / 1000
        Me.actresbox2.Text = Int(corrected_rates_avg(2) * 1000 + 0.5) / 1000
        Me.actresbox3.Text = Int(corrected_rates_avg(3) * 1000 + 0.5) / 1000
        Me.actresbox4.Text = Int(corrected_rates_avg(4) * 1000 + 0.5) / 1000
        Me.actresbox5.Text = Int(corrected_rates_avg(5) * 1000 + 0.5) / 1000
        Me.actresbox6.Text = live_time



        Me.acterrBox1.Text = Int(Max(corrected_rates_avg_err(1), 0) * 1000 + 0.5) / 1000
        Me.acterrBox2.Text = Int(Max(corrected_rates_avg_err(2), 0) * 1000 + 0.5) / 1000
        Me.acterrBox3.Text = Int(Max(corrected_rates_avg_err(3), 0) * 1000 + 0.5) / 1000
        Me.acterrBox4.Text = Int(Max(corrected_rates_avg_err(4), 0) * 1000 + 0.5) / 1000
        Me.acterrBox5.Text = Int(Max(corrected_rates_avg_err(5), 0) * 1000 + 0.5) / 1000


        Dim avg_temp
        avg_temp = 0

        For i = 1 To num_files
            avg_temp = avg_temp + sample_temp(i)
        Next

        avg_temp = Int(avg_temp / num_files * 1000) / 1000

        sampleinfoBox1.Text = vial_diameter
        sampleinfoBox2.Text = avg_temp
        sampleinfoBox3.Text = U_enrichment
        sampleinfoBox4.Text = Pu_weight
        '  sampleinfoBox5.Text = Form2.SampleIDBox1.Text
        sampleinfoBox6.Text = Const_val(37)
        sampleinfoBox7.Text = Const_val(38)

        Dim u_concentration, Pu_concentration, am_concentration, cm_concentration, np_concentration
        Dim u_concent_err, pu_concent_err, am_concent_err, cm_concent_err, np_concent_err, r_upu_temp

        np_concentration = 0

        '    If R_UNp_avg <> 0 Then np_concentration = u_concentration / R_UNp_avg

        pu_concent_err = 0

        u_concentration = temp_matrix(9, 1) * 1000
        u_concent_err = temp_matrix_err(9, 1) * 1000

        r_upu_temp = R_UPu_avg
        If ref_element = 2 Then r_upu_temp = 1 / R_UU_avg

        If ref_element = 1 Then
            u_concentration = Format(1000 * MEXRF_main.KED_Box_3.Text, "###0.000")
            u_concent_err = Format(1000 * KED_PAR_err(1), "###0.000")

            If R_UPu_avg <> 0 Then Pu_concentration = u_concentration / R_UPu_avg Else Pu_concentration = 0
            If R_UPu_avg <> 0 Then pu_concent_err = (Abs((u_concent_err / R_UPu_avg) ^ 2 + (u_concentration * R_upu_avg_err / R_UPu_avg ^ 2) ^ 2)) ^ 0.5

            ' If MEXRF_main.use_MEXRF_box.Checked = True Then

            If R_UNp_avg <> 0 Then np_concentration = u_concentration / R_UNp_avg * R_UU_avg
            If R_UCm_avg <> 0 Then cm_concentration = u_concentration / R_UCm_avg * R_UU_avg
            If R_UAm_avg <> 0 Then am_concentration = u_concentration / R_UAm_avg * R_UU_avg

            If R_UCm_avg <> 0 Then cm_concent_err = (Abs((u_concent_err / R_UCm_avg) ^ 2 + (u_concentration * R_UCm_avg_err / R_UCm_avg ^ 2) ^ 2)) ^ 0.5
            If R_UAm_avg <> 0 Then am_concent_err = (Abs((u_concent_err / R_UAm_avg) ^ 2 + (u_concentration * R_UAm_avg_err / R_UAm_avg ^ 2) ^ 2)) ^ 0.5
            If R_UNp_avg <> 0 Then np_concent_err = (Abs((u_concent_err / R_UNp_avg) ^ 2 + (u_concentration * R_UNp_avg_err / R_UNp_avg ^ 2) ^ 2)) ^ 0.5

        End If

            If ref_element = 2 Then

            u_concentration = Format(1000 * MEXRF_main.KED_Box_4.Text / R_UU_avg, "###0.000")
            u_concent_err = Format(1000 * ((KED_PAR_err(2) / R_UU_avg) ^ 2 + (R_UU_avg_err * KED_PAR(2) / R_UU_avg ^ 2) ^ 2) ^ 0.5, "###0.000")

            If MEXRF_main.KED_check_box_4.Checked Then Pu_concentration = Int(1000 * 1000 * MEXRF_main.KED_Box_4.Text) / 1000
            If MEXRF_main.KED_check_box_4.Checked Then pu_concent_err = Int(1000 * 1000 * KED_PAR_err(2)) / 1000

            If R_UNp_avg <> 0 Then np_concentration = Pu_concentration / R_UNp_avg
            If R_UCm_avg <> 0 Then cm_concentration = Pu_concentration / R_UCm_avg
            If R_UAm_avg <> 0 Then am_concentration = Pu_concentration / R_UAm_avg

            If R_UCm_avg <> 0 Then cm_concent_err = (Abs((pu_concent_err / R_UCm_avg) ^ 2 + (Pu_concentration * R_UCm_avg_err / R_UCm_avg ^ 2) ^ 2)) ^ 0.5
            If R_UAm_avg <> 0 Then am_concent_err = (Abs((pu_concent_err / R_UAm_avg) ^ 2 + (Pu_concentration * R_UAm_avg_err / R_UAm_avg ^ 2) ^ 2)) ^ 0.5
            If R_UNp_avg <> 0 Then np_concent_err = (Abs((pu_concent_err / R_UNp_avg) ^ 2 + (Pu_concentration * R_UNp_avg_err / R_UNp_avg ^ 2) ^ 2)) ^ 0.5

        End If


        Hybrid_Element_1_box.Text = Int(1000 * u_concentration) / 1000
        Hybrid_Element_err_1_box.Text = Int(1000 * u_concent_err) / 1000

        Hybrid_Element_2_box.Text = Int(1000 * Pu_concentration) / 1000
        Hybrid_Element_err_2_box.Text = Int(1000 * pu_concent_err) / 1000

        Hybrid_Element_3_box.Text = Int(1000 * np_concentration) / 1000
        Hybrid_Element_err_3_box.Text = Int(1000 * np_concent_err) / 1000

        Hybrid_Element_4_box.Text = Int(1000 * cm_concentration) / 1000
        Hybrid_Element_err_4_box.Text = Int(1000 * cm_concent_err) / 1000

        Hybrid_Element_5_box.Text = Int(1000 * am_concentration) / 1000
        Hybrid_Element_err_5_box.Text = Int(1000 * am_concent_err) / 1000


        If ref_element = 1 Then
            UPu_conc_ratio_box.Text = Format(R_UPu_avg, "###0.0000")
            UPu_conc_ratio_err_box.Text = Format(R_upu_avg_err, "###0.0000")
        End If

        If ref_element = 2 Then
            UPu_conc_ratio_box.Text = Format(R_UU_avg, "###0.0000")
            UPu_conc_ratio_err_box.Text = Format(R_UU_avg_err, "###0.0000")
        End If


        '  ---------------------------------------------------------------------
        '
        '                          stand-alone analysis
        '
        '  --------------------------------------------------------------------- 

        U_est_par(1) = Const_val(43)
        U_est_par(2) = Const_val(44)
        U_est_par(3) = Const_val(45)
        pu_est_par(1) = Const_val(46)
        pu_est_par(2) = Const_val(47)
        pu_est_par(3) = Const_val(48)

        Dim peak_area1, u_conc_xrf_est, pu_conc_xrf_est, peak_area2


        peak_area1 = corrected_rates_avg(1)
        '  peak_area1 =  wt_avg_corr_rate(1)
        '
        u_conc_xrf_est = Inv_Lambert(U_est_par(2) / U_est_par(1) * (peak_area1 - U_est_par(3))) / U_est_par(2)
        pu_conc_xrf_est = u_conc_xrf_est / vms_R_UPu

        peak_area2 = corrected_rates_avg(2)
        ' peak_area2 =  wt_avg_corr_rate(2)

        pu_conc_xrf_est = Inv_Lambert(pu_est_par(2) / pu_est_par(1) * (peak_area2 - pu_est_par(3))) / pu_est_par(2)

        Dim u_sa_conc_err, pu_sa_conc_err
        Dim el_index As Integer

        u_sa_conc_err = SA_conc_uncertainty(peak_area1, corrected_rates_avg_err(1), U_est_par)
        pu_sa_conc_err = SA_conc_uncertainty(peak_area2, corrected_rates_avg_err(2), pu_est_par)

        Dim SA_PARS(3), sa_el_conc(5), sa_el_conc_err(5), peak_area_temp, conc_temp(6), peak_area_err_temp
        Dim attn_factor

        For i = 1 To 6
            conc_temp(i) = 0
        Next

        ' Element 1
        i = 43
        el_index = 1
        SA_PARS(1) = Const_val(i)
        SA_PARS(2) = Const_val(i + 1)
        SA_PARS(3) = Const_val(i + 2)

        conc_temp(2) = pu_conc_xrf_est / 1000
        attn_factor = 1
        If conc_temp(2) > 0 Then attn_factor = Exp(-attn_fact_7_co(115.6, conc_temp) * vial_diameter / 2)

        peak_area_temp = corrected_rates_avg(el_index)
        peak_area_err_temp = corrected_rates_avg_err(el_index)
        sa_el_conc(el_index) = Inv_Lambert(SA_PARS(2) / SA_PARS(1) * (peak_area_temp - SA_PARS(3))) / SA_PARS(2) / attn_factor
        sa_el_conc_err(el_index) = SA_conc_uncertainty(peak_area_temp, peak_area_err_temp, SA_PARS)

        SA_Element_1_box.Text = Int(1000 * sa_el_conc(el_index)) / 1000
        SA_Element_err_1_box.Text = Int(1000 * sa_el_conc_err(el_index)) / 1000

        '    MsgBox("A1 " & SA_PARS(1) & "  A2 " & SA_PARS(2) & "  A3 " & SA_PARS(3))
        '   MsgBox("PEAK_TEMP  " & peak_area_temp)

        For i = 1 To 6
            conc_temp(i) = 0
        Next

        ' Element 2
        i = 46
        el_index = 2
        SA_PARS(1) = Const_val(i)
        SA_PARS(2) = Const_val(i + 1)
        SA_PARS(3) = Const_val(i + 2)

        conc_temp(1) = sa_el_conc(1) / 1000
        attn_factor = 1
        If conc_temp(1) > 0 Then attn_factor = Exp(-attn_fact_7_co(121.8, conc_temp) * vial_diameter / 2)
        peak_area_temp = corrected_rates_avg(el_index)
        peak_area_err_temp = corrected_rates_avg_err(el_index)
        sa_el_conc(el_index) = Inv_Lambert(SA_PARS(2) / SA_PARS(1) * (peak_area_temp - SA_PARS(3))) / SA_PARS(2) / attn_factor
        sa_el_conc_err(el_index) = SA_conc_uncertainty(peak_area_temp, peak_area_err_temp, SA_PARS) / attn_factor

        SA_Element_2_box.Text = Int(1000 * sa_el_conc(el_index)) / 1000
        SA_Element_err_2_box.Text = Int(1000 * sa_el_conc_err(el_index)) / 1000

        ' Element 3

        i = 49
        el_index = 3
        SA_PARS(1) = Const_val(i)
        SA_PARS(2) = Const_val(i + 1)
        SA_PARS(3) = Const_val(i + 2)
        peak_area_temp = corrected_rates_avg(el_index)
        sa_el_conc(el_index) = Inv_Lambert(SA_PARS(2) / SA_PARS(1) * (peak_area_temp - SA_PARS(3))) / SA_PARS(2)
        sa_el_conc_err(el_index) = SA_conc_uncertainty(corrected_rates_avg(el_index), corrected_rates_avg_err(el_index), SA_PARS)

        SA_Element_3_box.Text = Int(1000 * sa_el_conc(el_index)) / 1000
        SA_Element_err_3_box.Text = Int(1000 * sa_el_conc_err(el_index)) / 1000

        ' Element 4
        i = 52
        el_index = 4
        SA_PARS(1) = Const_val(i)
        SA_PARS(2) = Const_val(i + 1)
        SA_PARS(3) = Const_val(i + 2)
        conc_temp(1) = Max(sa_el_conc(1) / 1000, 0)
        conc_temp(2) = Max(sa_el_conc(2) / 1000, 0)
        attn_factor = Exp(-attn_fact_7_co(123.8, conc_temp) * vial_diameter / 2)
        peak_area_temp = corrected_rates_avg(el_index)
        peak_area_err_temp = corrected_rates_avg_err(el_index)
        sa_el_conc(el_index) = Inv_Lambert(SA_PARS(2) / SA_PARS(1) * (peak_area_temp - SA_PARS(3))) / SA_PARS(2) / attn_factor
        sa_el_conc_err(el_index) = SA_conc_uncertainty(peak_area_temp, peak_area_err_temp, SA_PARS) / attn_factor

        SA_Element_4_box.Text = Int(1000 * sa_el_conc(el_index)) / 1000
        SA_Element_err_4_box.Text = Int(1000 * sa_el_conc_err(el_index)) / 1000

        '  Element 5
        i = 55
        el_index = 5
        SA_PARS(1) = Const_val(i)
        SA_PARS(2) = Const_val(i + 1)
        SA_PARS(3) = Const_val(i + 2)

        conc_temp(1) = Max(sa_el_conc(1) / 1000, 0)
        conc_temp(2) = Max(sa_el_conc(2) / 1000, 0)
        attn_factor = Exp(-attn_fact_7_co(123.8, conc_temp) * vial_diameter / 2)
        peak_area_temp = corrected_rates_avg(el_index)
        peak_area_err_temp = corrected_rates_avg_err(el_index)
        sa_el_conc(el_index) = Inv_Lambert(SA_PARS(2) / SA_PARS(1) * (peak_area_temp - SA_PARS(3))) / SA_PARS(2) / attn_factor
        sa_el_conc_err(el_index) = SA_conc_uncertainty(peak_area_temp, peak_area_err_temp, SA_PARS) / attn_factor

        SA_Element_5_box.Text = Int(1000 * sa_el_conc(el_index)) / 1000
        SA_Element_err_5_box.Text = Int(1000 * sa_el_conc_err(el_index)) / 1000


        '  ---------------------------------------------------------------------
        '
        '       ---------------  display Independent Hybrid Results ---------------
        '
        '  ---------------------------------------------------------------------
        '
        ' the independent results use the uranium concentratioin deterimined from the backscatter spectrum fit parameter 9.
        ' the remaining concentrations are determined using U backscatter concentration and the hybrid ratios.
        '
        Dim indep_results(6), indep_u_corr, indep_u_corr_err
        Dim indep_avg_ratio(5), indep_results_err(5)
        IndependentPanel.Visible = True
        If no_indep_flag Then IndependentPanel.Visible = False
        indep_u_corr = 1            '   1.2587 - 0.00148 * (1000 * par_avg(9))
        indep_u_corr_err = 0.03

        For i = 2 To 5

            indep_avg_ratio(i) = wt_avg_rate_ratio(i)
        Next i

        indep_results(1) = 1000 * par_avg(9) / indep_u_corr
        If indep_avg_ratio(2) <> 0 Then indep_results(2) = indep_results(1) / R_UPu_avg * R_UU_avg Else indep_results(2) = 0
        If indep_avg_ratio(3) <> 0 Then indep_results(3) = indep_results(1) / R_UNp_avg * R_UU_avg Else indep_results(3) = 0
        If indep_avg_ratio(4) <> 0 Then indep_results(4) = indep_results(1) / R_UCm_avg * R_UU_avg Else indep_results(4) = 0
        If indep_avg_ratio(5) <> 0 Then indep_results(5) = indep_results(1) / R_UAm_avg * R_UU_avg Else indep_results(5) = 0

        indep_results_err(1) = Sqrt(Abs(1000 * par_sigma(9) ^ 2 + (indep_results(1) * indep_u_corr_err) ^ 2))

        If indep_avg_ratio(2) <> 0 Then indep_results_err(2) = Sqrt((indep_results_err(1) / R_UPu_avg * R_UU_avg) ^ 2) Else indep_results(2) = 0
        If indep_avg_ratio(3) <> 0 Then indep_results_err(3) = Sqrt((indep_results_err(1) / R_UNp_avg * R_UU_avg) ^ 2) Else indep_results(3) = 0
        If indep_avg_ratio(4) <> 0 Then indep_results_err(4) = Sqrt((indep_results_err(1) / R_UCm_avg * R_UU_avg) ^ 2) Else indep_results(4) = 0
        If indep_avg_ratio(5) <> 0 Then indep_results_err(5) = Sqrt((indep_results_err(1) / R_UAm_avg * R_UU_avg) ^ 2) Else indep_results(5) = 0


        Indep_Element_1_box.Text = Int(1000 * indep_results(1)) / 1000
        Indep_Element_2_box.Text = Int(1000 * indep_results(2)) / 1000
        Indep_Element_3_box.Text = Int(1000 * indep_results(3)) / 1000
        Indep_Element_4_box.Text = Int(1000 * indep_results(4)) / 1000
        Indep_Element_5_box.Text = Int(1000 * indep_results(5)) / 1000
        Indep_matrix_box.Text = Int(1000 * par_avg(14)) / 1000
        '
        Indep_Element_err_1_box.Text = Int(1000 * indep_results_err(1)) / 1000
        Indep_Element_err_2_box.Text = Int(1000 * indep_results_err(2)) / 1000
        Indep_Element_err_3_box.Text = Int(1000 * indep_results_err(3)) / 1000
        Indep_Element_err_4_box.Text = Int(1000 * indep_results_err(4)) / 1000
        Indep_Element_err_5_box.Text = Int(1000 * indep_results_err(5)) / 1000
        Indep_matrix_err_box.Text = Int(1000 * par_sigma(14)) / 1000






        '**********************************************************************************************
        '
        '                                 Create Summary Report
        '
        '**********************************************************************************************

        Dim klineout, star_line, star_line2, star_line3, dash_line, equal_line, todays_date, str_i As String
        Dim str_out, tempstr1, tempstr2, tempstr3, tempstr4, tempstr5, tempstr6, plus_minus, sum_fil_name As String

        Dim temp_str1, temp_str2, temp_str3, temp_str4, t_file_name As String
        Dim max_file_name, f_pad As Integer
        Dim Pu_ref As Boolean

        max_file_name = num_files

        dash_line = "-----------------------------------------------------------------------------------------------" & vbCrLf
        equal_line = "==============================================================================================" & vbCrLf
        star_line = " ****************************************************************************************** " & vbCrLf
        star_line2 = " *                                                                                        * " & vbCrLf
        star_line3 = " *                                MEXRF ANALYSIS SUMMARY                                  * " & vbCrLf
        plus_minus = " " & ChrW(177) & " "       ' plus or minus symbol
        klineout = star_line

        st_pos = Strings.InStrRev(input_file_names(1), "\")
        st_len = Strings.Len(input_file_names(1))
        sum_fil_name = Strings.Mid(input_file_names(1), st_pos + 1, st_len - 4 - st_pos)

        max_file_name = 0
        For i = 1 To num_files
            If Strings.Len(input_file_names(i)) > max_file_name Then max_file_name = Strings.Len(input_file_names(i))     '  find length of longest file name
        Next i
        f_pad = max_file_name + 4

        summary_file_name = summaryfiledir & sum_fil_name & "_summary.txt"

        Dim fs As FileStream = File.Create(summary_file_name)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(klineout)
        fs.Write(info, 0, info.Length)
        fs.Close()

        My.Computer.FileSystem.WriteAllText(summary_file_name, star_line2, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, star_line3, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, star_line2, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, star_line & vbCrLf, True)

        My.Computer.FileSystem.WriteAllText(summary_file_name, " Summary File:" & summary_file_name & vbCrLf & vbCrLf,
                                            True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " Input File Names:" & vbCrLf, True)

        For i = 1 To num_files
            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            acq_date_temp = Strings.Replace(acq_date(i), "-", "/")
            If Strings.Len(acq_date_temp) <= 0 Then
                acq_date_temp = ""
                acq_time(i) = ""
            End If

            t_file_name = input_file_names(i)
            klineout = temp_num & t_file_name.PadRight(f_pad, " "c) & " Acq: " & acq_date_temp.PadLeft(10, " "c) &
            acq_time(i).PadLeft(10, " "c) & vbCrLf

            My.Computer.FileSystem.WriteAllText(summary_file_name, klineout, True)

        Next i

        '   My.Computer.FileSystem.WriteAllText(summary_file_name, "" & vbCrLf & " Acquistion Start: " & " ------- " & vbCrLf, True)

        klineout = vbCrLf & " MEKED summary file name:  " & meked_summary_name & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(summary_file_name, klineout, True)

        todays_date = Now()
        My.Computer.FileSystem.WriteAllText(summary_file_name,
                                            "" & vbCrLf & " Analysis Date: " & todays_date & vbCrLf & vbCrLf, True)

        '   ************************************************************************************
        '     PRINTOUT SUMMARY CONCENTRATION VALUES - All METHODS
        '   ************************************************************************************
        Dim temp_element, temp_str5, temp_units, vial_desc, temp_string(10), temp_format(5) As String
        Dim interrogation_method As String
        Dim other_rand, temp_par, temp_err, systematic_err, counting_err, max_temp_par
        Dim temp_digits As Integer
        Dim v_dex As Integer
        Dim hybrid_result(6), hybrid_result_err(6), temp_MEKED(6), temp_MEKED_err(6)
        '

        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name,
                                            " SUMMARY CONCENTRATION VALUES - All METHODS", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)


        temp_str1 = " Element       MEKED                Hybrid            Stand-alone     Independent Hybrid "
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)

        v_dex = 1                                     ' sample vial type
        If Const_val(16) = 1 Then v_dex = 2
        vial_desc = "poly vial"
        If v_dex = 2 Then vial_desc = "glass cuvette"

        For i = 1 To 6
            temp_MEKED(i) = meked_summary(i)
            temp_MEKED_err(i) = meked_summary_err(i)
        Next i
        temp_MEKED(4) = meked_summary(5)
        temp_MEKED(5) = meked_summary(4)
        temp_MEKED_err(4) = meked_summary_err(5)
        temp_MEKED_err(5) = meked_summary_err(4)

        For i = 1 To 6
            If MEKED_sumMary_flag = False And MEKED_results_flag = False Then temp_MEKED(i) = 0
            If MEKED_sumMary_flag = False And MEKED_results_flag = False Then temp_MEKED_err(i) = 0
        Next i

        hybrid_result(1) = Int(100000 * u_concentration) / 100000
        hybrid_result(2) = Pu_concentration
        hybrid_result(3) = np_concentration
        hybrid_result(4) = cm_concentration
        hybrid_result(5) = am_concentration

        hybrid_result_err(1) = Int(100000 * u_concent_err) / 100000
        hybrid_result_err(2) = pu_concent_err
        hybrid_result_err(3) = np_concent_err
        hybrid_result_err(4) = cm_concent_err
        hybrid_result_err(5) = am_concent_err

        Dim temp_indep_res, temp_indep_err, temp_hybrid_res, temp_hybrid_err

        For j = 1 To 5

            temp_indep_res = indep_results(j)
            temp_indep_err = indep_results_err(j)
            temp_hybrid_res = hybrid_result(j)
            temp_hybrid_err = hybrid_result_err(j)

            If MEXRF_main.ref_el_U_button.Checked = False Then temp_indep_res = 0
            If MEXRF_main.ref_el_U_button.Checked = False Then temp_indep_err = 0

            If MEXRF_main.KED_check_box_3.Checked = False And MEXRF_main.KED_check_box_4.Checked = False Then temp_hybrid_res = 0
            If MEXRF_main.KED_check_box_3.Checked = False And MEXRF_main.KED_check_box_4.Checked = False Then temp_hybrid_err = 0

            temp_units = " (g/L)"

            temp_element = element_symbol(element_index(j))
            If j = 6 Then temp_element = "Matrix"
            If j = 6 Then temp_units = " (g/mL)"
            tempstr1 = (Format(temp_MEKED(j), "##0.000")).PadLeft(8, " "c) & " " & ChrW(177) & "" & (Format(temp_MEKED_err(j), "##0.000")).PadLeft(8, " "c)
            If MEKED_results_flag = False And MEKED_sumMary_flag = False Then tempstr1 = "         ±      "

            temp_str3 = (Format(temp_hybrid_res, "##0.000")).PadLeft(8, " "c) & " " & ChrW(177) & "" & (Format(temp_hybrid_err, "##0.000")).PadLeft(8, " "c)
            temp_str4 = (Format(sa_el_conc(j), "##0.000")).PadLeft(8, " "c) & " " & ChrW(177) & "" & (Format(sa_el_conc_err(j), "##0.000")).PadLeft(8, " "c)
            temp_str5 = (Format(temp_indep_res, "##0.000")).PadLeft(8, " "c) & " " & ChrW(177) & "" & (Format(temp_indep_err, "##0.000")).PadLeft(8, " "c)
            If MEXRF_main.ref_el_U_button.Checked = False Then temp_str5 = "       ±      "
            temp_str2 = (" [" & temp_element & "]").PadRight(6, " "c)
            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_str2 & "  " & tempstr1 & "    " & temp_str3 & "  " & temp_str4 & "  " & temp_str5 & "     " & temp_units & vbCrLf, True)
        Next j

        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)


        interrogation_method = "N/A"
        If fiss_prod_corr_type = 1 Then interrogation_method = "none"
        If fiss_prod_corr_type = 2 Then interrogation_method = "Passive Spectrum Subtracted during fitting."
        If fiss_prod_corr_type = 3 Then interrogation_method = "Passive Rates Subtracted during post processing"
        If fiss_prod_corr_type = 4 Then interrogation_method = "Emprical correction applied during post processing."
        If fiss_prod_corr_type = 5 Then interrogation_method = "Fixed bias correction factor applied during post processing."

        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & " Self Interrogation Method:  " & interrogation_method & vbCrLf & vbCrLf, True)



        '   ************************************************************************************
        '     PRINTOUT CORRECTED ACTINIDE X-RAY COUNT RATES
        '   ************************************************************************************
        '
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " MEXRF ACTINIDE CHARACTERISTIC X-RAY COUNT RATES", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)


        temp_str1 = "Cycle     Uranium         Plutonium             Neptunium         Curium           Americium"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)


        For i = 1 To num_files


            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            tempstr1 = ""
            For j = 1 To 5

                temp_string(j) = ""
                temp_par = corr_count(j, i) / livetime_array(i)
                temp_err = corr_count_sig(j, i) / livetime_array(i)

                temp_string(j) = (Format(temp_par, "##0.00")).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, "##0.00")).PadLeft(7, " "c)

                tempstr1 = tempstr1 & temp_string(j)
            Next j

            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr1 & vbCrLf, True)

        Next i

        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        '
        '  *********  print simple average and reduced standard deviation
        '
        tempstr1 = ""
        For j = 1 To 5

            For i = 1 To num_files
                temp_array1(i) = corr_count(j, i) / livetime_array(i)
            Next i
            temp_string(j) = ""
            temp_par = average(temp_array1, num_files)
            temp_err = stdev(temp_array1, num_files) / num_files ^ 0.5

            temp_string(j) = (Format(temp_par, "##0.00")).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, "##0.00")).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "Avg."
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr1 & vbCrLf, True)
        '
        '  *********   print standard deviation beneath simple average
        '
        tempstr1 = ""
        temp_err = ""
        For j = 1 To 5
            For i = 1 To num_files
                temp_array1(i) = corr_count(j, i) / livetime_array(i)
            Next i

            temp_string(j) = ""
            temp_par = stdev(temp_array1, num_files)
            temp_string(j) = (Format(temp_par, "##0.00")).PadLeft(9, " "c) & "         "
            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "StDev."
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_num & "" & tempstr1 & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        '
        '  *********   print weighted average  ****************
        '
        tempstr1 = ""
        For j = 1 To 5


            temp_string(j) = ""
            temp_par = corrected_rates_avg(j)
            temp_err = corrected_rates_avg_err(j)

            temp_string(j) = (Format(temp_par, "##0.00")).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, "##0.00")).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "WtAvg."
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "" & tempstr1 & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line & vbCrLf, True)


        '   ************************************************************************************
        '     PRINTOUT PASSIVE X-RAY COUNT RATE RATIOS - if correction type 3 is selected
        '   ************************************************************************************
        '
        If fiss_prod_corr_type <> 3 Then GoTo skip_FP
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " MEXRF PASSIVE COUNT RATES FROM " & passive_summary_file_name, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)

        tempstr1 = ""
        For j = 1 To 5

            temp_string(j) = ""
            temp_par = pass_sum_rates(j)
            temp_err = pass_sum_rates_err(j)

            temp_string(j) = (Format(temp_par, "##0.00")).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, "##0.00")).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "Rates:"
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "" & tempstr1 & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line & vbCrLf, True)


skip_fp:
        '   ************************************************************************************
        '     PRINTOUT ACTINIDE X-RAY COUNT RATE RATIOS
        '   ************************************************************************************
        '
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " MEXRF ACTINIDE X-RAY COUNT RATE RATIOS", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)

        temp_str1 = "Cycle     Uranium         Plutonium             Neptunium         Curium           Americium"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)


        '  determine number of decimal places for printout
        For j = 1 To 5
            max_temp_par = 0
            For i = 1 To num_files
                If Abs(Z1_Z2_rate_ratio(j, i)) > max_temp_par Then max_temp_par = Abs(Z1_Z2_rate_ratio(j, i))
                If Abs(Z1_Z2_rate_ratio_sig(j, i)) > max_temp_par Then max_temp_par = Abs(Z1_Z2_rate_ratio_sig(j, i))
            Next i

            temp_format(j) = "##0.0000"
            If Abs(max_temp_par) >= 1 Then temp_format(j) = "##0.000"
            If Abs(max_temp_par) >= 100 Then temp_format(j) = "##0.00"
            If Abs(max_temp_par) >= 10000 Then temp_format(j) = "##0"
        Next j

        '  PRINT cycle by cycle ratios

        For i = 1 To num_files

            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            tempstr1 = ""
            For j = 1 To 5

                temp_string(j) = ""
                temp_par = Z1_Z2_rate_ratio(j, i)
                temp_err = Z1_Z2_rate_ratio_sig(j, i)

                temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

                tempstr1 = tempstr1 & temp_string(j)
            Next j

            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr1 & vbCrLf, True)

        Next i

        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)

        '
        '  *********  print simple average and reduced standard deviation
        '
        tempstr1 = ""
        For j = 1 To 5

            For i = 1 To num_files
                temp_array1(i) = Z1_Z2_rate_ratio(j, i)
            Next i
            temp_string(j) = ""
            temp_par = average(temp_array1, num_files)
            temp_err = stdev(temp_array1, num_files) / num_files ^ 0.5



            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "Avg."
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr1 & vbCrLf, True)
        '
        '  *********   print standard deviation beneath simple average
        '
        tempstr1 = ""
        temp_err = ""
        For j = 1 To 5
            For i = 1 To num_files
                temp_array1(i) = Z1_Z2_rate_ratio(j, i)
            Next i

            temp_string(j) = ""
            temp_par = stdev(temp_array1, num_files)
            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & "         "

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "StDev."
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_num & "" & tempstr1 & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        '
        '  *********   print weighted average  ****************
        '
        tempstr1 = ""
        For j = 1 To 5

            For i = 1 To num_files
                temp_array1(i) = Z1_Z2_rate_ratio(j, i)
                temp_array2(i) = Z1_Z2_rate_ratio_sig(j, i)
            Next i

            temp_string(j) = ""
            temp_par = wt_average(temp_array1, temp_array2, num_files, 0)
            temp_err = wt_average(temp_array1, temp_array2, num_files, 1)

            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)


            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "WtAvg."
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "" & tempstr1 & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line & vbCrLf, True)


        '   ************************************************************************************
        '     PRINTOUT SUMMARY HYBRID RU_Z VALUES
        '   ************************************************************************************
        '      R_UPu ,  R_UNp ....
        '
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " MEXRF ACTINIDE R_UZ CONVERSION FACTORS", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)


        temp_str1 = "          Uranium           Plutonium         Neptunium         Curium            Americium"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)


        temp_num = "    "
        tempstr1 = "R_UZ   "
        For j = 1 To 5

            temp_string(j) = ""
            temp_par = Z1_Z2_avg_conv_fact(j)
            temp_err = z1_z2_avg_conv_fact_err(j)
            temp_format(j) = "##0.0000"
            '      If Abs(temp_par) >= 1 Then temp_format(j) = "##0.000"
            '      If Abs(temp_par) >= 100 Then temp_format(j) = "##0.00"
            '      If Abs(temp_par) >= 10000 Then temp_format(j) = "##0"
            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

                tempstr1 = tempstr1 & temp_string(j)
            Next j

        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & tempstr1 & vbCrLf, True)

        '  atomic_mass_ratio

        tempstr1 = "Atom wt"
        For j = 1 To 5

            temp_string(j) = ""
            temp_par = atomic_mass_ratio(j)
            temp_err = 0
            temp_format(j) = "##0.0000"
            '   If Abs(temp_par) >= 1 Then temp_format(j) = "##0.000"
            '   If Abs(temp_par) >= 100 Then temp_format(j) = "##0.00"
            '   If Abs(temp_par) >= 10000 Then temp_format(j) = "##0"
            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j

        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & tempstr1 & vbCrLf, True)


        '  R_UZ correction factor for corr_type 4 or 5
        If fiss_prod_corr_type < 4 Then GoTo skip_self_irr

        temp_array(1) = 1
        temp_array(2) = fiss_corr_factor
        temp_array(3) = fiss_corr_factor ^ 0.5
        temp_array(4) = fiss_corr_factor ^ 2
        temp_array(5) = fiss_corr_factor ^ 1.5

        temp_array1(1) = 0
        If fiss_corr_factor <> 0 Then temp_array1(2) = fiss_corr_factor_err Else temp_array1(2) = 0
        If fiss_corr_factor <> 0 Then temp_array1(3) = fiss_corr_factor_err / fiss_corr_factor ^ 0.5 / 2 Else temp_array1(3) = 0
        If fiss_corr_factor <> 0 Then temp_array1(4) = 2 * fiss_corr_factor * fiss_corr_factor_err Else temp_array1(4) = 0
        If fiss_corr_factor <> 0 Then temp_array1(5) = 1.5 * fiss_corr_factor ^ 0.5 * fiss_corr_factor_err Else temp_array1(5) = 0

        tempstr1 = "Self Irr"
        For j = 1 To 5

            temp_string(j) = ""
            temp_par = temp_array(j)
            temp_err = temp_array1(j)
            temp_format(j) = "##0.0000"
            '   If Abs(temp_par) >= 1 Then temp_format(j) = "##0.000"
            '   If Abs(temp_par) >= 100 Then temp_format(j) = "##0.00"
            '   If Abs(temp_par) >= 10000 Then temp_format(j) = "##0"
            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j

        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & tempstr1 & vbCrLf, True)
skip_self_irr:

        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)



        '   ************************************************************************************
        '     PRINTOUT ACTINIDE [U]/[Z] Concentration RATIOS
        '   ************************************************************************************
        '
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " MEXRF ACTINIDE CONCENTRATION RATIOS", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)

        temp_str1 = "Cycle     Uranium         Plutonium             Neptunium         Curium           Americium"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)


        ' conc_ratio_array(j, i) = temp_array1(i)
        '  conc_ratio_array_err(j, i) = temp_array1_sig(i)


        '  determine number of decimal places for printout
        For j = 1 To 5
            max_temp_par = 0
            For i = 1 To num_files
                If Abs(conc_ratio_array(j, i)) > max_temp_par Then max_temp_par = Abs(conc_ratio_array(j, i))
                If Abs(conc_ratio_array_err(j, i)) > max_temp_par Then max_temp_par = Abs(conc_ratio_array_err(j, i))
            Next i

            temp_format(j) = "##0.0000"
            If Abs(max_temp_par) >= 1 Then temp_format(j) = "##0.000"
            If Abs(max_temp_par) >= 100 Then temp_format(j) = "##0.00"
            If Abs(max_temp_par) >= 10000 Then temp_format(j) = "##0"
        Next j

        '  PRINT cycle by cycle ratios

        For i = 1 To num_files

            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            tempstr1 = ""
            For j = 1 To 5

                temp_string(j) = ""
                temp_par = conc_ratio_array(j, i)
                temp_err = conc_ratio_array_err(j, i)

                temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

                tempstr1 = tempstr1 & temp_string(j)
            Next j

            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr1 & vbCrLf, True)

        Next i

        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)

        '
        '  *********  print simple average and reduced standard deviation
        '
        tempstr1 = ""
        For j = 1 To 5

            For i = 1 To num_files
                temp_array1(i) = conc_ratio_array(j, i)
            Next i
            temp_string(j) = ""
            temp_par = average(temp_array1, num_files)
            temp_err = stdev(temp_array1, num_files) / num_files ^ 0.5



            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "Avg."
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr1 & vbCrLf, True)
        '
        '  *********   print standard deviation beneath simple average
        '
        tempstr1 = ""
        temp_err = ""
        For j = 1 To 5
            For i = 1 To num_files
                temp_array1(i) = conc_ratio_array(j, i)
            Next i

            temp_string(j) = ""
            temp_par = stdev(temp_array1, num_files)
            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & "         "

            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "StDev."
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_num & "" & tempstr1 & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        '
        '  *********   print weighted average  ****************
        '
        tempstr1 = ""
        For j = 1 To 5

            For i = 1 To num_files
                temp_array1(i) = conc_ratio_array(j, i)
                temp_array2(i) = conc_ratio_array_err(j, i)
            Next i

            temp_string(j) = ""
            temp_par = wt_average(temp_array1, temp_array2, num_files, 0)
            temp_err = wt_average(temp_array1, temp_array2, num_files, 1)

            temp_string(j) = (Format(temp_par, temp_format(j))).PadLeft(9, " "c) & " " & ChrW(177) & (Format(temp_err, temp_format(j))).PadLeft(7, " "c)


            tempstr1 = tempstr1 & temp_string(j)
        Next j
        temp_num = "WtAvg."
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "" & tempstr1 & vbCrLf, True)
        '   My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)



        '   ************************************************************************************
        '     PRINTOUT XRF Status data (peak channels, rates, etc)
        '   ************************************************************************************
        '
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " XRF Status Data", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)

        temp_str1 = "           Live       Real     Centroid   Centroid    Offset       Slope    Ref Peak"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)
        temp_str1 = "Cycle     Time(s)    Time(s)    Peak 1     Peak 2      (keV)       (keV)    Rate /1(s)    Chi2/n"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)

        Dim temp_brem, temp_hv, temp_shaping, temp_cd_int, temp_Cd_fwhm, temp_e0, temp_de, temp_sys_fwhm
        Dim temp_lt, temp_rt, temp_ref_peak1, temp_ref_peak2, temp_totals



        For i = 1 To num_files

            lo_tail_area = temp_matrix(18, i)
            lo_tail_decay = temp_matrix(19, i)
            high_tail_area = temp_matrix(37, i)
            high_tail_decay = temp_matrix(38, i)
            e_chan = temp_matrix(16, i)
            step_bkg = temp_matrix(20, i)

            corr_temp = 1 + Abs(2 * lo_tail_area / lo_tail_decay / e_chan) + Abs(2 * high_tail_area / high_tail_decay / e_chan) + step_bkg / e_chan

            temp_cd_int = temp_matrix(24, i) / livetime_array(i) * corr_temp
            temp_array1(i) = temp_cd_int
            '   temp_Cd_fwhm = temp_matrix(22, i) * 2.35 * 1000
            '    temp_array1(i) = temp_Cd_fwhm

            lo_tail_area = temp_matrix(18, i)
            lo_tail_decay = temp_matrix(19, i)
            high_tail_area = temp_matrix(37, i)
            high_tail_decay = temp_matrix(38, i)
            e_chan = temp_matrix(16, i)
            step_bkg = temp_matrix(20, i)

            corr_temp = 1 + Abs(2 * lo_tail_area / lo_tail_decay / e_chan) + Abs(2 * high_tail_area / high_tail_decay / e_chan) + step_bkg / e_chan

            temp_e0 = temp_matrix(15, i)
            temp_array2(i) = temp_e0
            temp_de = temp_matrix(16, i)
            temp_array3(i) = temp_de
            temp_lt = livetime_array(i)
            temp_rt = realtime_array(i)
            temp_ref_peak1 = ref_peak_1(i)
            temp_ref_peak2 = ref_peak_2(i)

            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            tempstr2 = (Format(temp_lt, "##0.0")).PadLeft(10, " "c) & " " & (Format(temp_rt, "##0.0")).PadLeft(10, " "c)
            tempstr3 = (Format(temp_ref_peak1, "##0.00")).PadLeft(10, " "c) & " " & (Format(temp_ref_peak2, "##0.00")).PadLeft(10, " "c)
            tempstr4 = (Format(temp_e0, "#0.00000")).PadLeft(11, " "c) & " " & (Format(temp_de, "#0.00000")).PadLeft(11, " "c)
            tempstr5 = (Format(temp_cd_int, "##0.00")).PadLeft(12, " "c)
            tempstr6 = (Format(chisqr_array(i), "##0.00")).PadLeft(12, " "c)
            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "   " & tempstr2 & tempstr3 & tempstr4 & tempstr5 & tempstr6 & vbCrLf, True)

        Next i

        ' print average and standard deviations
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        temp_Cd_fwhm = average(temp_array1, num_files)
        temp_e0 = average(temp_array2, num_files)
        temp_de = average(temp_array3, num_files)
        temp_lt = average(livetime_array, num_files)
        temp_rt = average(realtime_array, num_files)
        temp_ref_peak1 = average(ref_peak_1, num_files)
        temp_ref_peak2 = average(ref_peak_2, num_files)

        temp_num = "Average"
        tempstr2 = (Format(temp_lt, "##0.0")).PadLeft(10, " "c) & " " & (Format(temp_rt, "##0.0")).PadLeft(10, " "c)
        tempstr3 = (Format(temp_ref_peak1, "##0.00")).PadLeft(10, " "c) & " " & (Format(temp_ref_peak2, "##0.00")).PadLeft(10, " "c)
        tempstr4 = (Format(temp_e0, "#0.00000")).PadLeft(11, " "c) & " " & (Format(temp_de, "#0.00000")).PadLeft(11, " "c)
        tempstr5 = (Format(temp_Cd_fwhm, "##0.00")).PadLeft(12, " "c)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "" & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)

        temp_Cd_fwhm = stdev(temp_array1, num_files)
        temp_e0 = stdev(temp_array2, num_files)
        temp_de = stdev(temp_array3, num_files)
        temp_lt = stdev(livetime_array, num_files)
        temp_rt = stdev(realtime_array, num_files)
        temp_ref_peak1 = stdev(ref_peak_1, num_files)
        temp_ref_peak2 = stdev(ref_peak_2, num_files)

        temp_num = "St.Dev."
        tempstr2 = (Format(temp_lt, "##0.0")).PadLeft(10, " "c) & " " & (Format(temp_rt, "##0.0")).PadLeft(10, " "c)
        tempstr3 = (Format(temp_ref_peak1, "##0.00")).PadLeft(10, " "c) & " " & (Format(temp_ref_peak2, "##0.00")).PadLeft(10, " "c)
        tempstr4 = (Format(temp_e0, "#0.00000")).PadLeft(11, " "c) & " " & (Format(temp_de, "#0.00000")).PadLeft(11, " "c)
        tempstr5 = (Format(temp_Cd_fwhm, "##0.00")).PadLeft(12, " "c)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "" & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)

        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & vbCrLf & "Energy Ref Peak 1 = " & e_cal_par(2) & " keV", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & vbCrLf & "Energy Ref Peak 2 = " & e_cal_par(4) & " keV" & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)

        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & vbCrLf & vbCrLf, True)

        ' --------------------------------------------------------------------------------------------------------

        temp_str1 = "           Ref Peak     Spectrum       Total      Bremsstrahlung   X-ray HV     Shaping "
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)
        temp_str1 = "Cycle      FWHM (eV)    FWHM (eV)    Rate (1/s)    Rate (1/s)        (kV)      Constant"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)

        For i = 1 To num_files

            temp_Cd_fwhm = temp_matrix(22, i) * 2.35 * 1000
            temp_array1(i) = temp_Cd_fwhm
            '  temp_cd_int = temp_matrix(24, i) / livetime_array(i)
            temp_brem = temp_matrix(30, i) / livetime_array(i)
            temp_hv = temp_matrix(28, i)
            temp_shaping = temp_matrix(29, i)
            temp_sys_fwhm = temp_matrix(17, i) * 2.35 * 1000
            temp_totals = xrf_total_rate(i)

            '   temp_array1(i) = temp_cd_int
            temp_array1(i) = temp_Cd_fwhm
            temp_array2(i) = temp_brem
            temp_array3(i) = temp_hv
            temp_array4(i) = temp_shaping
            temp_array5(i) = temp_sys_fwhm


            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            tempstr2 = (Format(temp_Cd_fwhm, "##0.00")).PadLeft(12, " "c)
            tempstr3 = (Format(temp_sys_fwhm, "##0.00")).PadLeft(13, " "c)
            tempstr4 = (Format(temp_totals, "##0.0")).PadLeft(15, " "c) & "  " & (Format(temp_brem, "##0")).PadLeft(12, " "c)
            tempstr5 = (Format(temp_hv, "##0.00")).PadLeft(14, " "c) & "  " & (Format(temp_shaping, "#0.00000")).PadLeft(11, " "c)
            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)

        Next i

        ' print average and standard deviations
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        temp_cd_int = average(temp_array1, num_files)
        temp_brem = average(temp_array2, num_files)
        temp_hv = average(temp_array3, num_files)
        temp_shaping = average(temp_array4, num_files)
        temp_sys_fwhm = average(temp_array5, num_files)
        temp_totals = average(xrf_total_rate, num_files)

        temp_num = "Average"
        tempstr2 = (Format(temp_cd_int, "##0.00")).PadLeft(11, " "c)
        tempstr3 = (Format(temp_sys_fwhm, "##0.00")).PadLeft(13, " "c)
        tempstr4 = (Format(temp_totals, "##0.0")).PadLeft(15, " "c) & "  " & (Format(temp_brem, "##0")).PadLeft(12, " "c)
        tempstr5 = (Format(temp_hv, "##0.00")).PadLeft(14, " "c) & "  " & (Format(temp_shaping, "#0.00000")).PadLeft(11, " "c)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)

        temp_cd_int = stdev(temp_array1, num_files)
        temp_brem = stdev(temp_array2, num_files)
        temp_hv = stdev(temp_array3, num_files)
        temp_shaping = stdev(temp_array4, num_files)
        temp_sys_fwhm = stdev(temp_array5, num_files)
        temp_totals = stdev(xrf_total_rate, num_files)

        temp_num = "St.Dev."
        tempstr2 = (Format(temp_cd_int, "##0.00")).PadLeft(11, " "c)
        tempstr3 = (Format(temp_sys_fwhm, "##0.00")).PadLeft(13, " "c)
        tempstr4 = (Format(temp_totals, "##0.0")).PadLeft(15, " "c) & "  " & (Format(temp_brem, "##0")).PadLeft(12, " "c)
        tempstr5 = (Format(temp_hv, "##0.00")).PadLeft(14, " "c) & "  " & (Format(temp_shaping, "#0.00000")).PadLeft(11, " "c)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)


        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)


        ' --------------------------------------------------------------------------------------------------------

        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, " Fission Product and Compton Information ", True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)

        temp_str1 = "             " & fiss_prod_name(1) & "       " & fiss_prod_name(2) & "        " & fiss_prod_name(3) & "      Lin.Bkg.par1  Lin.Bkg.par2   Random "
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)
        temp_str1 = "Cycle      Rate (1/s)   Rate (1/s)   Rate (1/s)         (1/s)       (1/s)       Factor"
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)
        Dim fiss_prod_1, fiss_prod_2, fiss_prod_3, temp_lin_bkg, temp_lin_bkg2, temp_rand
        For i = 1 To num_files

            lo_tail_area = temp_matrix(18, i)
            lo_tail_decay = temp_matrix(19, i)
            high_tail_area = temp_matrix(37, i)
            high_tail_decay = temp_matrix(38, i)
            e_chan = temp_matrix(16, i)
            step_bkg = temp_matrix(20, i)

            corr_temp = 1 + Abs(2 * lo_tail_area / lo_tail_decay / e_chan) + Abs(2 * high_tail_area / high_tail_decay / e_chan) + step_bkg / e_chan

            fiss_prod_1 = temp_matrix(6, i) / livetime_array(i) * corr_temp
            fiss_prod_2 = temp_matrix(7, i) / livetime_array(i) * corr_temp
            fiss_prod_3 = temp_matrix(8, i) / livetime_array(i) * corr_temp
            temp_lin_bkg = temp_matrix(26, i) / livetime_array(i)
            temp_lin_bkg2 = temp_matrix(27, i) / livetime_array(i)
            temp_rand = temp_matrix(25, i)

            temp_array1(i) = temp_Cd_fwhm
            '  temp_cd_int = temp_matrix(24, i) / livetime_array(i)
            temp_brem = temp_matrix(30, i) / livetime_array(i)
            temp_hv = temp_matrix(28, i)
            temp_shaping = temp_matrix(29, i)
            temp_sys_fwhm = temp_matrix(17, i) * 2.35 * 1000
            temp_totals = xrf_total_rate(i)

            '   temp_array1(i) = temp_cd_int
            temp_array1(i) = fiss_prod_1
            temp_array2(i) = fiss_prod_2
            temp_array3(i) = fiss_prod_3
            temp_array4(i) = temp_lin_bkg
            temp_array5(i) = temp_lin_bkg2
            temp_array6(i) = temp_rand


            temp_num = i & "  "
            If i < 10 Then temp_num = " " & i & "  "
            tempstr2 = (Format(fiss_prod_1, "##0.00")).PadLeft(12, " "c)
            tempstr3 = (Format(fiss_prod_2, "##0.00")).PadLeft(13, " "c)
            tempstr4 = (Format(fiss_prod_3, "##0.00")).PadLeft(15, " "c) & "  " & (Format(temp_lin_bkg, "##0.000")).PadLeft(12, " "c)
            tempstr5 = (Format(temp_lin_bkg2, "##0.000")).PadLeft(14, " "c) & "  " & (Format(temp_rand, "#0.00000")).PadLeft(11, " "c)
            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & "  " & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)

        Next i

        ' print average and standard deviations
        My.Computer.FileSystem.WriteAllText(summary_file_name, dash_line, True)
        fiss_prod_1 = average(temp_array1, num_files)
        fiss_prod_2 = average(temp_array2, num_files)
        fiss_prod_3 = average(temp_array3, num_files)
        temp_lin_bkg = average(temp_array4, num_files)
        temp_lin_bkg2 = average(temp_array5, num_files)
        temp_rand = average(temp_array6, num_files)

        temp_num = "Average"
        tempstr2 = (Format(fiss_prod_1, "##0.00")).PadLeft(11, " "c)
        tempstr3 = (Format(fiss_prod_2, "##0.00")).PadLeft(13, " "c)
        tempstr4 = (Format(fiss_prod_3, "##0.00")).PadLeft(15, " "c) & "  " & (Format(temp_lin_bkg, "##0.000")).PadLeft(12, " "c)
        tempstr5 = (Format(temp_lin_bkg2, "##0.000")).PadLeft(14, " "c) & "  " & (Format(temp_rand, "#0.00000")).PadLeft(11, " "c)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)

        temp_cd_int = stdev(temp_array1, num_files)
        temp_brem = stdev(temp_array2, num_files)
        temp_hv = stdev(temp_array3, num_files)
        temp_shaping = stdev(temp_array4, num_files)
        temp_sys_fwhm = stdev(temp_array5, num_files)
        temp_totals = stdev(xrf_total_rate, num_files)

        fiss_prod_1 = stdev(temp_array1, num_files)
        fiss_prod_2 = stdev(temp_array2, num_files)
        fiss_prod_3 = stdev(temp_array3, num_files)
        temp_lin_bkg = stdev(temp_array4, num_files)
        temp_lin_bkg2 = stdev(temp_array5, num_files)
        temp_rand = stdev(temp_array6, num_files)


        temp_num = "St.Dev."
        tempstr2 = (Format(fiss_prod_1, "##0.00")).PadLeft(11, " "c)
        tempstr3 = (Format(fiss_prod_2, "##0.00")).PadLeft(13, " "c)
        tempstr4 = (Format(fiss_prod_3, "##0.00")).PadLeft(15, " "c) & "  " & (Format(temp_lin_bkg, "##0.000")).PadLeft(12, " "c)
        tempstr5 = (Format(temp_lin_bkg2, "##0.000")).PadLeft(14, " "c) & "  " & (Format(temp_rand, "#0.00000")).PadLeft(11, " "c)
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & tempstr2 & tempstr3 & tempstr4 & tempstr5 & vbCrLf, True)


        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        temp_num = "						Compton Rate = "
        tempstr2 = Int(1000 * compton_rate) / 1000 & " " & ChrW(177) & "  " & Int(1000 * compton_rate_err) / 1000
        My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_num & tempstr2 & vbCrLf, True)

        '   ************************************************************************************
        '     PRINTOUT MEKED CONCENTRATION VALUES
        '   ************************************************************************************


        If (MEKED_sumMary_flag = False And MEKED_results_flag = False) Then meked_summary_name = " NO FILE SELECTED"
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line, True)
        klineout = " MEKED summary file name:  " & meked_summary_name
        My.Computer.FileSystem.WriteAllText(summary_file_name, klineout, True)
        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf, True)


        My.Computer.FileSystem.WriteAllText(summary_file_name,
                                            " MEKED Corrected Concentrations (Weighted Averages)" & vbCrLf & vbCrLf, True)


        temp_str1 = " Element     Concentration   Total Uncertainty   Systematic   Counting     Other Random "
        My.Computer.FileSystem.WriteAllText(summary_file_name, temp_str1 & vbCrLf, True)

        v_dex = 1                                     ' sample vial type
        If Const_val(16) = 1 Then v_dex = 2
        vial_desc = "poly vial"
        If v_dex = 2 Then vial_desc = "glass cuvette"
        If MEKED_sumMary_flag = False And MEKED_results_flag = False Then GoTo SKIP_LIST
        For j = 1 To 6
            temp_par = meked_summary(j)
            temp_err = meked_summary_err(j)
            systematic_err = meked_systematic_err(j)
            other_rand = meked_other_err(j)
            counting_err = meked_counting_err(j)
            temp_units = " (g/L)"


            temp_element = KED_elements(j)          '   element_symbol(element_index(j))
            If j = 6 Then temp_element = "Matrix"
            If j = 6 Then temp_units = " (g/mL)"
            tempstr1 = (Format(temp_par, "##0.000")).PadLeft(10, " "c) & " " & ChrW(177) & "       " & (Format(temp_err, "##0.000")).PadLeft(10, " "c)
            temp_str3 = (Format(systematic_err, "##0.000")).PadLeft(10, " "c) & " " & (Format(counting_err, "##0.000")).PadLeft(10, " "c)
            temp_str5 = (Format(other_rand, "##0.000")).PadLeft(12, " "c)
            temp_str2 = ("  " & temp_element & " ").PadRight(10, " "c)
            My.Computer.FileSystem.WriteAllText(summary_file_name, "" & temp_str2 & "  " & tempstr1 & "    " & temp_str3 & "  " & temp_str5 & "     " & temp_units & vbCrLf, True)
        Next j
SKIP_LIST:

        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & " " & vbCrLf, True)

        klineout = " MEKED X-ray HV bias (kV):        " & Int(1000 * KED_PAR(8)) / 1000 & " " & ChrW(177) & "  " & Int(1000 * KED_PAR_err(8)) / 100 & vbCrLf
        My.Computer.FileSystem.WriteAllText(summary_file_name, klineout, True)
        klineout = " MEKED Bremms Shaping parameter:    " & Int(10000 * KED_PAR(10)) / 10000 & " " & ChrW(177) & "  " & Int(10000 * KED_PAR_err(10)) / 10000 & vbCrLf
        My.Computer.FileSystem.WriteAllText(summary_file_name, klineout, True)

        My.Computer.FileSystem.WriteAllText(summary_file_name, vbCrLf & equal_line & vbCrLf & vbCrLf, True)


    End Sub

    Private Sub grid1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grid1.CellContentClick

    End Sub

    Function wt_avg(conc, sig, n_vals, i_out)
        Dim i As Integer
        Dim avg, wt1, wt_tot, avg_tot, sig_out, temp_out, simple_avg, sum_sig
        wt_tot = 0
        avg_tot = 0
        simple_avg = 0
        sum_sig = 0
        For i = 1 To n_vals
            wt1 = 1 / sig(i) ^ 2
            wt_tot = wt_tot + wt1
            avg_tot = avg_tot + conc(i) * wt1
        Next

        For i = 1 To n_vals
            simple_avg = simple_avg + conc(i)
            sum_sig = sum_sig + Abs(sig(i))
        Next

        simple_avg = simple_avg / n_vals

        avg = avg_tot / wt_tot
        sig_out = 1 / wt_tot ^ 0.5
        If sum_sig = 0 Then avg = simple_avg
        temp_out = avg
        If i_out = 2 Then temp_out = sig_out
        wt_avg = temp_out

    End Function

    Function st_dev(conc, n_vals)
        Dim i As Integer
        Dim avg
        avg = 0
        st_dev = 0

        If n_vals <= 1 Then GoTo 10

        For i = 1 To n_vals
            avg = avg + conc(i)
        Next i
        avg = avg / num_files
        For i = 1 To n_vals
            st_dev = st_dev + (conc(i) - avg) ^ 2
        Next i

        If n_vals > 1 Then st_dev = (st_dev / (n_vals - 1)) ^ 0.5
10:      ' end function
    End Function


    Private Sub get_fit_results(results_file_name As String)
        Dim idex, jdex, kdex, i_temp, j_temp As Integer
        Dim str11 As String
        idex = -2
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
                        If idex = -1 And jdex = 4 Then acq_date_temp = str11
                        If idex = -1 And jdex = 5 Then acq_time_temp = str11
                        If idex = -1 Then GoTo 50


                        If idex = 0 And jdex < 10 Then chisqr = Val(str11)
                        If idex = 0 And kdex = 2 Then vial_diameter = Val(str11)
                        If idex = 0 And kdex = 3 Then sample_temperature = Val(str11)
                        If idex = 0 And kdex = 4 Then U_enrichment = Val(str11)
                        If idex = 0 And kdex = 5 Then Pu_weight = Val(str11)
                        If idex = 0 And kdex = 6 Then live_time = Val(str11)
                        If idex = 0 And jdex > 10 Then GoTo 50


                        If idex > 0 And idex < 43 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 2 Then results_arr(idex) = Val(str11)
                        If idex > 0 And idex < 43 And kdex = 3 Then err_arr(idex) = Val(str11)

                        If idex > 43 And kdex = 1 Then i_temp = Val(str11)
                        If idex > 43 And kdex = 2 Then j_temp = Val(str11)
                        If idex > 43 And kdex = 3 Then covar_array(i_temp, j_temp) = Val(str11)
                        ' If idex > 43 Then MsgBox(idex & " " & jdex & " " & kdex & " " & str11 & " " & covar_array(i_temp, j_temp))
50:                 Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

100:                ' gather covariance terms


            End While
        End Using

    End Sub

    Function SA_conc_uncertainty(peak_area, peak_area_err, el_est_par)
        Dim el_sa_conc_err, d_el_sa_conc_err_a, d_el_sa_conc_err_b, d_el_sa_conc_err_c, peak_area_1
        ' calculates the concentration uncertainty for the Lambert response function for the Standalone anlaysis


        peak_area_1 = peak_area - el_est_par(3)
        el_sa_conc_err = Inv_Lambert(el_est_par(2) / el_est_par(1) * (peak_area_1 + peak_area_err / 2)) / el_est_par(2)
        el_sa_conc_err = Abs(el_sa_conc_err - Inv_Lambert(el_est_par(2) / el_est_par(1) * (peak_area_1 - peak_area_err / 2)) / el_est_par(2))

        d_el_sa_conc_err_a = Inv_Lambert(el_est_par(2) / (el_est_par(1) * 1.003) * peak_area_1) / el_est_par(2)
        d_el_sa_conc_err_a = (d_el_sa_conc_err_a - Inv_Lambert(el_est_par(2) / (el_est_par(1) * 0.997) * peak_area_1) / el_est_par(2)) / (el_est_par(1) * 0.006)

        d_el_sa_conc_err_b = Inv_Lambert(el_est_par(2) * 1.003 / el_est_par(1) * peak_area_1) / (el_est_par(2) * 1.003)
        d_el_sa_conc_err_b = (d_el_sa_conc_err_b - Inv_Lambert(el_est_par(2) * 0.997 / el_est_par(1) * peak_area_1) / (el_est_par(2) * 0.997)) / (el_est_par(2) * 0.006)


        el_sa_conc_err = el_sa_conc_err ^ 2 + (d_el_sa_conc_err_a * standalone_err(2, 1)) ^ 2 + (d_el_sa_conc_err_b * standalone_err(2, 2)) ^ 2
        el_sa_conc_err = (el_sa_conc_err + 2 * d_el_sa_conc_err_a * d_el_sa_conc_err_b * standalone_covar(2, 1, 2)) ^ 0.5

        SA_conc_uncertainty = el_sa_conc_err

    End Function

    Private Sub MaskedTextBox22_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MEKED_Element_1_box.MaskInputRejected

    End Sub

    Private Sub MEKED_UPu_box_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MEKED_UPu_box.MaskInputRejected

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class