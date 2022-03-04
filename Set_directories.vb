Public Class Set_directories
    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '  root_dir_base_name = Strings.Left(My.Application.Info.DirectoryPath, 1) & ":\"
        '       root_dir_name = Path.Combine(root_dir_base_name, "MEXRF\")
        '        exe_dir_name = Path.Combine(root_dir_base_name, root_dir_name & "XRF_T0")
        '       sys_consts_dir_name = Path.Combine(root_dir_base_name, root_dir_name & "hked_files\System_constants\")
        '  ked_vms_consts_dir = Path.Combine(root_dir_base_name, root_dir_name & "hked_files\VMS_constants\")
        '       seed_parm_dir_name = Path.Combine(root_dir_base_name, root_dir_name & "\hked_files\Seed_parameters\")
        '       resultfiledir = Path.Combine(root_dir_base_name, root_dir_name & "results\")
        '  summaryfiledir = Path.Combine(root_dir_base_name, root_dir_name & "summary_files\")
        '       attn_parm_def_name = Path.Combine(root_dir_base_name, root_dir_name & "hked_files\attn_coeffs_default.csv")
        '       genie_report_dir_name = genie2k_dir & "\genie2k\exefiles\REPORT"
        '  str_prg_root = "xrf_fit_4k_v1p1"



        summary_box.Text = summaryfiledir
        atten_param_box.Text = attn_parm_def_name     '  attn_par_file_name
        xray_data_box.Text = xray_data_file_dir
        genie2k_box.Text = genie_report_dir_name
        executable_box.Text = str_prg_root
        drive_letter_Box.Text = root_dir_base_name
        root_box.Text = root_dir_name
        exe_root_box.Text = exe_dir_name
        vms_consts_box.Text = xrf_vms_consts_dir
        system_constants_box.Text = sys_consts_dir_name
        seed_parms_box.Text = seed_parm_dir_name
        results_box.Text = resultfiledir
        convert_files_box.Text = convertfiledir
        net_spec_box.Text = netspectradir
        summed_files_box.Text = summaryfiledir

        '  system_constants_box.Text = "c:\MEXRF\xrf_files\System_constants\"
        ' seed_parms_box.Text = "c:\MEXRF\xrf_files\Seed_parameters\"
        ' resultfiledir = "c:\MEXRF\results\"
        ' atten_param_box.Text = "c:\MEXRF\xrf_files\attn_coeffs_default.csv"     '  attn_par_file_name
        '  genie2k_box.Text = "c:\genie2k\exefiles\REPORT"
        '  executable_box.Text = "xrf_fit_FP_V1P4b_vb"

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub executable_box_TextChanged(sender As Object, e As EventArgs) Handles executable_box.TextChanged

    End Sub
End Class