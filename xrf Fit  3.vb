Imports System.Math


Public Module Module1
    Public datafilename1, Result_num
    Public resultfiledir, data_file_name, exe_name, root_dir_name, root_dir_base_name, exe_dir_name, convertfiledir, netspectradir As String
    Public xray_data_file_dir, genie2k_dir, attn_parm_def_name, strPrgName_exe As String
    Public sys_consts_dir_name, xrf_vms_consts_dir, seed_parm_dir_name, summaryfiledir, genie_report_dir_name, str_prg_root, attn_param_dir As String
    Public strPrgName2 As String
    Public live_time, real_time, vial_diameter, sample_temperature, U_enrichment, Pu_weight, sample_temp1
    Public results_arr(42), err_arr(42), covar_array(42, 42), chisqr
    Public covar_label(42) As String
    Public vms_ref_file_name, passive_summary_file_name As String
    Public vms_ref_raw_data(4100), vms_ref_live_time, vms_ref_real_time, vms_ref_e_0, vms_ref_d_e, vms_ref_current, vms_sample_current
    Public vms_R_UPu, vms_R_UPu_err, vms_Pu_bkg_wt, vms_Pu_bkg_wt_err, vms_R_UPu_cal, vms_R_UPu_cal_err, vms_am_bkg_wt, vms_am_bkg_err
    Public U_est_par(3), pu_est_par(3), U1_out, U2_out, pu_out
    Public fiss_prod_corr_type As Integer

    Public MEKED_sumMary_flag, MEKED_results_flag As Boolean

    Public R_z1_z2(36, 5), R_z1_z2_err(36, 5), z1_z2_avg_conv_fact(5), z1_z2_avg_conv_fact_err(5), atomic_mass_ratio(5)

    Public passive_data(4100)
    Public bkg_roi(4)

    Public fit_range(2)

    '  KED results arrays
    '
    Public KED_PAR(36), KED_PAR_err(36), ked_covar_array(36, 36), meked_summary(36), meked_summary_err(36)
    Public meked_systematic_err(36), meked_counting_err(36), meked_other_err(36)
    Public ked_par_array(99, 36), ked_err_arra(99, 36)
    Public input_ked_filenames(36), KED_elements(6), meked_analyis_date As String

    Public corrected_rates(21, 6), corrected_rates_err(21, 6)

    Public Const_val(90), file_const_val(90)
    Public standalone(5, 3), standalone_err(5, 3), standalone_covar(5, 3, 3)
    Public const_name(90), const_file_name, outconsts_def_csv As String
    Public fit_iterations, target_iterations As Integer
    '
    Public is_free_par(42) As Integer
    Public e_cal_par(10), K_fit_rois(10, 7), X_fit_rois(10, 7)
    Public num_files As Integer
    Public initval2(42)
    Public e_0, d_e
    Public original_spc(4100), fitted_spec(4100), err_array(4100), rel_resid(4100), rand_temp(4100)
    Public Sample_ID As String
    Public data(4100), rand_spec(4100), continue1, inlineval, n_channels, coinc_gate, free_params(42)

    Public bkg_spec(4100), corrected_spec(4100), fitted_plus_bkg_spec(4100)
    Public raw_data(16500) As Integer
    Public kfit_parms(36), kfit_consts(32), i_conc(7), i_attn(5), e_fit_min, e_fit_max, chisqr_common
    Public outfilename As String, outparameter As String, fit_lo_energy As String, fit_hi_energy As String

    Public new_de, new_e0, prelim_U_conc, prelim_Pu_conc, cd_area
    Public temp_file_dir As String
    Public status_prc, status_prc_read As String
    Public number_running, number_reads_going
    Public p() As Process
    Public root_filenames(99) As String

    Public max_directories As Integer
    Public num_chans_in As Integer

    Public amu_lib(10, 8), cmu_lib(2, 10), sol_lib(2, 10), edges(10), cont_edges(2), sol_edges(2), lor_width(10)
    Public xray_Lib(10, 15, 3)   '   element, transition, (E, BR, width) 
    Public misc_lib(6, 10), misc_edges(6), misc_attn_names(6)
    Public element_priority(10), act_norm_factor(10), act_norm_err(10), element_symbol(10), element_index(10)
    Public actinide_names(10), container_names(2), solution_names(2), fil_label(250), fil_label_X(200), pars_label(200) As String
    Public attn_par_file_name, out_attn_csv, xray_data_file_name, out_xray_data_csv As String
    Public max_channels, max_chan_num As Integer

    Public str_date, acq_date_time, acq_date(99), acq_time(99), input_file_names(99) As String
    Public xrf_total_rate(99), xrf_brem_rate(99), xrf_brem_rate_err(99), ref_peak_1(99), ref_peak_2(99)
    Public acq_date_temp, acq_time_temp As String
    Public meked_summary_name As String
    Public xrf_e(10, 15), xrf_rel(10, 15), lor_w(10, 15), xrf_abs(10, 15), xrf_bkg(14), concentrate(6), pass_sum_rates(5), pass_sum_rates_err(5)

    Public gamma_ray_lib_name, fiss_prod_name(5), fil_label_g(100), gamma_ray_lib_path As String
    Public gamma_energy(5, 10), gamma_int(5, 10), gamma_pts(5)


    Public Sub free_parm_form()
        ' Create a new instance of the form.
        Dim form1 As New Form()
        ' Create two buttons to use as the accept and cancel buttons.
        Dim button1 As New Button()
        Dim button2 As New Button()

        ' Set the text of button1 to "OK".
        button1.Text = "OK"
        ' Set the position of the button on the form.
        button1.Location = New Point(10, 10)
        ' Set the text of button2 to "Cancel".
        button2.Text = "Cancel"
        ' Set the position of the button based on the location of button1.
        button2.Location =
           New Point(button1.Left, button1.Height + button1.Top + 10)
        ' Set the caption bar text of the form.   
        form1.Text = "My Dialog Box"
        ' Display a help button on the form.
        form1.HelpButton = True

        ' Define the border style of the form to a dialog box.
        form1.FormBorderStyle = FormBorderStyle.FixedDialog
        ' Set the MaximizeBox to false to remove the maximize box.
        form1.MaximizeBox = False
        ' Set the MinimizeBox to false to remove the minimize box.
        form1.MinimizeBox = False
        ' Set the accept button of the form to button1.
        form1.AcceptButton = button1
        ' Set the cancel button of the form to button2.
        form1.CancelButton = button2
        ' Set the start position of the form to the center of the screen.
        form1.StartPosition = FormStartPosition.CenterScreen

        ' Add button1 to the form.
        form1.Controls.Add(button1)
        ' Add button2 to the form.
        form1.Controls.Add(button2)

        ' Display the form as a modal dialog box.
        form1.ShowDialog()
    End Sub







End Module
