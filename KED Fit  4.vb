Imports System.Math


Public Module Module1
    Public datafilename1

    Public data(2049), rand_spec(2049), continue1, inlineval, live_time, n_channels, coinc_gate, free_params(36)
    Public sum_width, delta_e, spec_e_init, folded_spec(2048), rand_corr_spec(2048), corr_spectrum(2048)
    Public kfit_parms(36), kfit_consts(32), i_conc(7), i_attn(5), e_fit_min, e_fit_max, chisqr_common
    Public infilename As String, outfilename As String, outparameter As String, fit_lo_energy As String, fit_hi_energy As String
    Dim msg = "Do you want to continue?"
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


    Sub Main(datafilename1)

        '        Dim data(2049), rand_spec(2049), continue1, inlineval, live_time, n_channels, coinc_gate
        Dim energy, low_chan, high_chan, spec_sum, fold_sum, rand_ratio, b$, n_channels, t_fit, tail_area, tail_decay, live_time
        Dim e_l1, e_l2, e_h1, e_h2, c_e_l1, c_e_l2, c_e_h1, c_e_h2, bkg_center, lo_region, hi_region, sum_c, temp1, ichn_start, ichn_stop, a$
        Dim datafilename As String
        a$ = "test 1" & vbCrLf


        MsgBox("3 file name= " & datafilename1)


        Console.Write(a$)

        Console.WriteLine("Enter file name:")
        Console.WriteLine()
        '       Do
        Console.Write("   ")
        infilename = Console.ReadLine()
        If infilename IsNot Nothing Then Console.WriteLine("      " + infilename)
        '      Loop While infilename IsNot Nothing

        Console.WriteLine("Enter lower fit energy:")
        Console.WriteLine()
        '       Do
        Console.Write("   ")
        fit_lo_energy = Console.ReadLine()
        ichn_start = fit_lo_energy / 0.09
        If infilename IsNot Nothing Then Console.WriteLine("      " + fit_lo_energy)

        Console.WriteLine("Enter maximum fit energy:")
        Console.WriteLine()
        '       Do
        Console.Write("   ")
        fit_hi_energy = Console.ReadLine()
        ichn_stop = fit_hi_energy / 0.09
        If infilename IsNot Nothing Then Console.WriteLine("      " + fit_hi_energy)
        '     
        c_e_l1 = Len(datafilename1)
        infilename = Left(datafilename1, c_e_l1 - 4)

        datafilename = infilename & ".txt"
        outfilename = infilename & "_fit.txt"
        outparameter = infilename & "_parm.txt"
        If infilename = " " Then datafilename = "C:\\travel\testfile.txt"
        If infilename = " " Then outfilename = "C:\\travel\testfile_fit.txt"
        If infilename = " " Then outparameter = "C:\\travel\testfile_param.txt"
        Console.WriteLine("      " + datafilename)

        Dim fileReader As System.IO.StreamReader
        fileReader =
        My.Computer.FileSystem.OpenTextFileReader(datafilename)
        Dim stringReader As String
        n_channels = 2048
        For i = 1 To n_channels
            ' stringReader = fileReader.ReadLine()
            inlineval = fileReader.ReadLine()
            If inlineval = "end" Then GoTo 100
            data(i) = inlineval
            '         MsgBox("The first line of the file is " & stringReader)
        Next i
100:    continue1 = 1



        MsgBox("live time= " & data(1) & " Data(100) " & data(100))

        ' rand correction parameters
        coinc_gate = 0.000000788
        tail_area = 0.05
        tail_decay = 1
        sum_width = 0.5
        spec_e_init = 0
        delta_e = 0.09

        'background correction parameters
        e_l1 = 61.478
        e_l2 = 64.435
        e_h2 = 151.247
        e_h1 = 157.698
        'background correction channels
        c_e_l1 = Int((e_l1 - spec_e_init) / delta_e)
        c_e_l2 = Int((e_l2 - spec_e_init) / delta_e)
        c_e_h1 = Int((e_h1 - spec_e_init) / delta_e)
        c_e_h2 = Int((e_h2 - spec_e_init) / delta_e)

        For i = 1 To n_channels
            rand_spec(i) = spec_fold2(50, 2000, i, coinc_gate, data)
        Next i
        '        MsgBox("Random spec(100) = " & rand_spec(100))
        For i = 1 To n_channels
            energy = spec_e_init + i * delta_e
            folded_spec(i) = spec_tail(energy, tail_area, tail_decay, sum_width, delta_e, rand_spec) + rand_spec(i)
        Next
        '        MsgBox("folded_spec(100)= " & folded_spec(100))

        low_chan = 155.0 / delta_e
        high_chan = 165.0 / delta_e
        spec_sum = 0
        fold_sum = 0
        rand_ratio = 1
        For i = low_chan To high_chan
            spec_sum = spec_sum + data(i)
            fold_sum = fold_sum + folded_spec(i)
        Next
        rand_ratio = spec_sum / fold_sum
        For i = 1 To n_channels
            rand_corr_spec(i) = data(i) - (rand_ratio * folded_spec(i))
        Next i
        '       MsgBox("rand_ratio = " & rand_ratio & " rand_corr_spec (100)= " & rand_corr_spec(100))

        'IF(D33<64,lo_region,(1-SUM(E33:E$704)/bkg_center)*(lo_region-hi_region)+hi_region)
        ' background correction
        bkg_center = 0
        lo_region = 0
        hi_region = 0
        For i = c_e_l1 To c_e_l2
            lo_region = lo_region + rand_corr_spec(i)
        Next i
        For i = c_e_h1 To c_e_h2
            hi_region = hi_region + rand_corr_spec(i)
        Next i
        For i = c_e_l2 To c_e_h2
            bkg_center = bkg_center + rand_corr_spec(i)
        Next i
        lo_region = lo_region / (c_e_l2 - c_e_l1 + 1)
        hi_region = hi_region / (c_e_h2 - c_e_h1 + 1)
        For i = 1 To n_channels
            If i <= c_e_l1 Then corr_spectrum(i) = rand_corr_spec(i) - lo_region
            If i <= c_e_l1 Then GoTo 101
            If i >= c_e_h2 Then corr_spectrum(i) = rand_corr_spec(i) - hi_region
            If i >= c_e_h2 Then GoTo 101
            sum_c = 0
            For j = c_e_l1 To i
                sum_c = sum_c + rand_corr_spec(j)
            Next
            corr_spectrum(i) = rand_corr_spec(i) - ((1 - sum_c / bkg_center) * (lo_region - hi_region) + hi_region)
101:        continue1 = 1

        Next i

        '       MsgBox("lo_region = " & lo_region & " corr_spec (100)= " & corr_spectrum(100))
        '       MsgBox("hi_region = " & hi_region & " corr_spec (1700)= " & corr_spectrum(1700))
        '
        temp1 = initialize_parms()
        '


        For i = 1 To n_channels
            b$ = corr_spectrum(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText("C:\travel\test11.txt", b$, True)
        Next i
        ichn_start = 1000
        ichn_stop = 1651
        Call aufit(ichn_start, ichn_stop)

    End Sub

    Function spec_fold2(nlow, nhigh, npt, gate, x)
        Dim temp
        '  x contains the count rate spectrum in counts per second
        '  nlow, nhigh are the start and stop channels over which the convolution is performed
        '  npt is the channel number of the resultant spectrum
        '  gate is the coincidence gate time in seconds (~0.5 E-6)
        '  Note: This is a first order correction. However, for summed energies of less than 150 kev the second order terms should be negligible.
        temp = 0
        live_time = x(1)
        For i = nlow To nhigh
            If i > npt Then GoTo 20
            If npt - i < 1 Then GoTo 10

            temp = temp + x(i) * x(npt - i) * gate / live_time ^ 2
10:         continue1 = 1
        Next i
20:     continue1 = 1

        spec_fold2 = temp

    End Function
    Function spec_tail(energy, lo_tail_area, lo_tail_decay, Gauss_width, delta_e, x)
        Dim fast_tail_area, fast_tail_decay, temp, e_i
        '
        fast_tail_area = 0
        fast_tail_decay = 1
        temp = 0
        For i = 1 To 50
            e_i = i * delta_e
            temp = temp + x(i) * lo_tail_area * Exp(-e_i * lo_tail_decay)

        Next i
        spec_tail = temp

    End Function

    Function functn2(chan_num, kfit_parms, kfit_consts, i_conc, i_attn)
        ' --------------  Peak Response function  -----
        'X-ray data
        Dim chan_1, e_start, deltae, gauss_width, p_i, p_e, ref_e, escape_a, p_w, ener_00, ener_0, esc_1, esc_2, x, y, ener_10, ener_11, temp
        Dim w_1, w_2, scat_angle, scat_ang2, scat_p1, scat_p2, scat_i, angles, scat_e, ss_thick, scatter, temp_val
        Dim temp_array(51), tails(4), e_escape(2), i_escape(2)
        chan_1 = chan_num - 1
        e_start = kfit_parms(13)
        deltae = kfit_parms(14)
        gauss_width = kfit_parms(15)
        p_i = kfit_parms(19)
        p_e = kfit_parms(20)
        'p_w = kfit_parms(21)
        ref_e = kfit_consts(5)
        escape_a = kfit_parms(7)
        p_w = gauss_width * (p_e / ref_e) ^ 0.5
        e_escape(1) = 9.88
        e_escape(2) = 10.99
        i_escape(1) = 1
        i_escape(2) = 1

        tails(1) = kfit_parms(22)
        tails(2) = kfit_parms(23)
        tails(3) = kfit_parms(24)
        tails(4) = kfit_parms(25)

        ener_00 = e_start + chan_1 * deltae
        'ener_0 = ener_00 - 26 * deltae

        y = 0
        For j = 1 To 51
            '
            ener_0 = ener_00 + (j - 26) * deltae
            '
            'escape peaks from continuum and 109Cd peak
            esc_1 = escape_area(ener_0 + e_escape(1), 1) * escape_a * i_escape(1) * functn_kfit(ener_0 + e_escape(1), kfit_parms, kfit_consts, i_conc, i_attn)
            esc_1 = esc_1 + escape_area(ener_0 + e_escape(1), 1) * escape_a * i_escape(1) * gaussian(ener_0 + e_escape(1), p_e, p_i, p_w, deltae)
            esc_2 = escape_area(ener_0 + e_escape(2), 2) * escape_a * i_escape(2) * functn_kfit(ener_0 + e_escape(2), kfit_parms, kfit_consts, i_conc, i_attn)
            esc_2 = esc_2 + escape_area(ener_0 + e_escape(2), 2) * escape_a * i_escape(2) * gaussian(ener_0 + e_escape(2), p_e, p_i, p_w, deltae)
            '
            'create bremsstrahlung array for smoothing
            '
            x = bent_kramers3(1, ener_0, 1, kfit_parms, kfit_consts, i_conc, i_attn)
            temp_array(j) = functn_kfit(ener_0, kfit_parms, kfit_consts, i_conc, i_attn) + esc_1 + esc_2 + x
            '
        Next j
        'add tailing
        '
        For j = 1 To 100
            ener_10 = deltae * j + ener_00
            ener_11 = deltae * j
            temp = functn_kfit(ener_10, kfit_parms, kfit_consts, i_conc, i_attn) + gaussian(ener_10, p_e, p_i, p_w, deltae)
            '
            y = y + temp * (tails(1) * Exp(-tails(2) * ener_11) + tails(3) * Exp(-tails(4) * ener_11))
            '
        Next j

        temp_val = g_smooth(ener_00, deltae, gauss_width, tails, temp_array)

        ref_e = kfit_consts(5)
        w_1 = kfit_consts(9)
        w_2 = kfit_consts(10)
        scat_angle = kfit_consts(11)
        scat_ang2 = kfit_consts(12)
        scat_p1 = kfit_consts(13)
        scat_p2 = kfit_consts(14)
        scat_i = kfit_parms(11)
        angles = kfit_parms(12)
        scat_e = kfit_parms(8)
        ss_thick = kfit_parms(18)

        scatter = bent_kramers2(1, ener_00, 5, scat_e, angles, scat_i)
        scatter = scatter * Exp(-7.9 * ss_thick * attn_fact_ss_7(ener_00, 1)) * HPGE_eff(ener_00) / HPGE_eff(ref_e)
        '
        functn2 = temp_val + scatter + y + gaussian(ener_00, p_e, p_i, p_w, deltae)
        '
    End Function
    Function escape_area(energy, np)
        '
        '  np is escape peak 1 or 2 (9.88 or 10.99 keV)
        Dim esc_e(2), esc_mu(2), edge, frac, L1, L2, attn_fact_ge
        esc_e(1) = 9.88
        esc_e(2) = 10.99
        esc_mu(1) = 37.42
        esc_mu(2) = 37.42

        edge = 11.1   '

        frac = 0.467
        If np = 2 Then frac = 0.0603

        L1 = 77700
        L2 = -2.523

        attn_fact_ge = 2.81

        If energy > edge Then attn_fact_ge = L1 * energy ^ L2

        escape_area = frac * attn_fact_ge / esc_mu(np) / 2.5

    End Function
    Function functn_kfit(ener, kfit_parms, kfit_consts, i_conc, i_attn)
        '
        Dim e_array(51), concentration(7)
        Dim e_0, x_intensity, par_x, par_y, ang_1, ang_2, w_1, w_2, deltae, gaus_width, lor_width, lor_width_pu, ss_thick, vial_d, vial_t, be_thick, cd_thick, xray_ref_energy
        Dim i_ss, i_w, i_cd, i_poly, i_be, cd_rho, be_rho, rho_vial, ss_rho, energy, x, attn
        '
        For i = 1 To 7
            concentration(i) = kfit_parms(i)
        Next i
        '
        e_0 = kfit_parms(8)
        x_intensity = kfit_parms(9)
        par_x = kfit_parms(10)
        par_y = kfit_consts(6)
        ang_1 = kfit_consts(7)
        ang_2 = kfit_consts(8)
        w_1 = kfit_consts(9)
        w_2 = kfit_consts(10)

        '
        deltae = kfit_parms(14)
        gaus_width = kfit_parms(15)

        lor_width = kfit_parms(16)
        lor_width_pu = kfit_parms(17)
        ss_thick = kfit_parms(18)
        vial_d = kfit_consts(1)
        vial_t = kfit_consts(2)
        be_thick = kfit_consts(3)
        cd_thick = kfit_consts(4)
        xray_ref_energy = kfit_consts(5)
        '
        i_ss = i_attn(1)
        i_w = i_attn(2)
        i_cd = i_attn(3)
        i_poly = i_attn(4)
        i_be = i_attn(5)

        '
        cd_rho = 8.65
        be_rho = 1.85
        rho_vial = 0.95
        ss_rho = 7.9
        '
        'e1 = ener - 26 * delta_e
        '
        'For i = 1 To 51
        'energy = e1 + delta_e * i
        energy = ener
        x = x_intensity * ebel(energy, e_0, par_x, par_y, ang_1, ang_2, w_1, w_2)
        '
        attn = Exp(-ss_thick * ss_rho * attn_fact_ss_7(energy, i_ss))
        attn = attn * Exp(-vial_d * attn_fact_7_co(energy, concentration, i_conc, deltae, lor_width, lor_width_pu))
        attn = attn * Exp(-vial_t * rho_vial * interp_poly(energy))
        attn = attn * Exp(-be_thick * be_rho * interp_be(energy))
        attn = attn * Exp(-cd_thick * cd_rho * attn_fact_cd_7(energy, i_cd))

        'e_array(i) = x * attn * HPGE_eff(energy) / HPGE_eff(xray_ref_energy)
        'Next i
        'functn_k = g_smooth(ener, delta_e, gaus_width, tails, e_array)

        functn_kfit = x * attn * HPGE_eff(energy) / HPGE_eff(xray_ref_energy)

    End Function

    Function ebel(energy, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)
        Dim d_e, cal_c, tim, i_a, omega, par_z, par_a, pi, phi, eps, par_m, par_j, U_0, par_nu, rho_nu, rho_zm, rho_zbar, mu_z, f_abs, dn_de
        ebel = 0
        If energy > e_0 Then GoTo 100
        d_e = 0.09
        cal_c = 1
        tim = 1
        i_a = 15
        omega = (0.4 / 10) ^ 2

        'tungsten target
        par_z = 74
        par_a = 183.84

        pi = 3.14159265358979
        phi = (90 - ang_1) / 180 * pi
        eps = ang_2 / 180 * pi

        par_m = 0.1382 - 0.9211 / par_z ^ 0.5
        par_j = 0.0135 * par_z
        U_0 = e_0 / energy

        par_nu = e_0 ^ par_m * (0.1904 - 0.2236 * Log(par_z) + 0.1292 * Log(par_z) ^ 2 - 0.0149 * Log(par_z) ^ 3)
        rho_zm = par_a / par_z * (0.00000787 * par_j ^ 0.5 * e_0 ^ 1.5 + 0.000000735 * e_0 ^ 2)
        rho_zbar = rho_zm * (0.49269 - 1.0987 * par_nu + 0.78557 * par_nu ^ 2) * Log(U_0) / (0.70256 - 1.09865 * par_nu + 1.0046 * par_nu ^ 2 + Log(U_0))

        par_nu = e_0 ^ par_m * (0.1904 - 0.2236 * Log(par_z) + 0.1292 * Log(par_z) ^ 2 - 0.0149 * Log(par_z) ^ 3)
        rho_zm = par_a / par_z * (0.00000787 * par_j ^ 0.5 * e_0 ^ 1.5 + 0.000000735 * e_0 ^ 2)
        rho_zbar = rho_zm * (0.49269 - 1.0987 * par_nu + 0.78557 * par_nu ^ 2) * Log(U_0) / (0.70256 - 1.09865 * par_nu + 1.0046 * par_nu ^ 2 + Log(U_0))

        'mu_z = attn_fact_W_PE2(energy, w_1, w_2)

        mu_z = interp_W(energy) * (w_1 + w_2 * energy)
        f_abs = (1 - Exp(-mu_z * 2 * rho_zbar * Sin(phi) / Sin(eps))) / (mu_z * 2 * rho_zbar * Sin(phi) / Sin(eps))

        dn_de = cal_c * omega * i_a * tim * par_z * (e_0 / energy - 1) ^ Par_x * f_abs * d_e

        ebel = dn_de * (e_0 / energy) ^ par_y
100:    continue1 = 1

    End Function
    Function interp_W(energy)
        Dim enereg(101), attn_coeff(101)
        Dim ener(5), attn_c(5), idex, sum_e, sum_e2, sum_a, sum_ea, a, b, y


        enereg(1) = 1
        enereg(2) = 1.5
        enereg(3) = 1.809
        enereg(4) = 1.809
        enereg(5) = 1.84
        enereg(6) = 1.872
        enereg(7) = 1.872
        enereg(8) = 2
        enereg(9) = 2.281
        enereg(10) = 2.281
        enereg(11) = 2.423
        enereg(12) = 2.575
        enereg(13) = 2.575
        enereg(14) = 2.694
        enereg(15) = 2.82
        enereg(16) = 2.82
        enereg(17) = 3
        enereg(18) = 4
        enereg(19) = 5
        enereg(20) = 6
        enereg(21) = 8
        enereg(22) = 10
        enereg(23) = 10.21
        enereg(24) = 10.21
        enereg(25) = 10.85
        enereg(26) = 11.54
        enereg(27) = 11.54
        enereg(28) = 11.82
        enereg(29) = 12.1
        enereg(30) = 12.1
        enereg(31) = 15
        enereg(32) = 20
        enereg(33) = 30
        enereg(34) = 40
        enereg(35) = 50
        enereg(36) = 60
        enereg(37) = 62
        enereg(38) = 64
        enereg(39) = 66
        enereg(40) = 68
        enereg(41) = 69.53
        enereg(42) = 69.53
        enereg(43) = 70
        enereg(44) = 72
        enereg(45) = 74
        enereg(46) = 76
        enereg(47) = 80
        enereg(48) = 82
        enereg(49) = 84
        enereg(50) = 86
        enereg(51) = 88
        enereg(52) = 90
        enereg(53) = 92
        enereg(54) = 94
        enereg(55) = 96
        enereg(56) = 98
        enereg(57) = 100
        enereg(58) = 102
        enereg(59) = 104
        enereg(60) = 106
        enereg(61) = 108
        enereg(62) = 110
        enereg(63) = 112
        enereg(64) = 114
        enereg(65) = 116
        enereg(66) = 118
        enereg(67) = 120
        enereg(68) = 122
        enereg(69) = 124
        enereg(70) = 126
        enereg(71) = 128
        enereg(72) = 130
        enereg(73) = 132
        enereg(74) = 134
        enereg(75) = 136
        enereg(76) = 138
        enereg(77) = 140
        enereg(78) = 142
        enereg(79) = 144
        enereg(80) = 146
        enereg(81) = 148
        enereg(82) = 150
        enereg(83) = 152
        enereg(84) = 154
        enereg(85) = 156
        enereg(86) = 158
        enereg(87) = 160
        enereg(88) = 200
        enereg(89) = 300
        enereg(90) = 400
        enereg(91) = 500
        enereg(92) = 600
        enereg(93) = 800
        enereg(94) = 1000
        enereg(95) = 1022
        enereg(96) = 1250
        enereg(97) = 1500
        enereg(98) = 2000
        enereg(99) = 2044
        enereg(100) = 3000
        enereg(101) = 4000
        '
        attn_coeff(1) = 3683
        attn_coeff(2) = 1644
        attn_coeff(3) = 1108
        attn_coeff(4) = 1315
        attn_coeff(5) = 1942
        attn_coeff(6) = 2865
        attn_coeff(7) = 3123
        attn_coeff(8) = 3921
        attn_coeff(9) = 2828
        attn_coeff(10) = 3279
        attn_coeff(11) = 2832
        attn_coeff(12) = 2446
        attn_coeff(13) = 2599
        attn_coeff(14) = 2338
        attn_coeff(15) = 2104
        attn_coeff(16) = 2194
        attn_coeff(17) = 1902
        attn_coeff(18) = 956.3
        attn_coeff(19) = 553.2
        attn_coeff(20) = 351.3
        attn_coeff(21) = 170.5
        attn_coeff(22) = 96.9
        attn_coeff(23) = 92
        attn_coeff(24) = 233.4
        attn_coeff(25) = 198.5
        attn_coeff(26) = 168.9
        attn_coeff(27) = 231.2
        attn_coeff(28) = 218.5
        attn_coeff(29) = 206.5
        attn_coeff(30) = 238.2
        attn_coeff(31) = 138.9
        attn_coeff(32) = 65.72
        attn_coeff(33) = 22.73
        attn_coeff(34) = 10.67
        attn_coeff(35) = 5.95
        attn_coeff(36) = 3.713
        attn_coeff(37) = 3.413
        attn_coeff(38) = 3.147
        attn_coeff(39) = 2.91
        attn_coeff(40) = 2.698
        attn_coeff(41) = 2.552
        attn_coeff(42) = 11.23
        attn_coeff(43) = 11.04
        attn_coeff(44) = 10.25
        attn_coeff(45) = 9.55
        attn_coeff(46) = 8.912
        attn_coeff(47) = 7.809
        attn_coeff(48) = 7.331
        attn_coeff(49) = 6.895
        attn_coeff(50) = 6.495
        attn_coeff(51) = 6.128
        attn_coeff(52) = 5.79
        attn_coeff(53) = 5.478
        attn_coeff(54) = 5.188
        attn_coeff(55) = 4.92
        attn_coeff(56) = 4.67
        attn_coeff(57) = 4.437
        attn_coeff(58) = 4.22
        attn_coeff(59) = 4.016
        attn_coeff(60) = 3.826
        attn_coeff(61) = 3.648
        attn_coeff(62) = 3.481
        attn_coeff(63) = 3.325
        attn_coeff(64) = 3.178
        attn_coeff(65) = 3.039
        attn_coeff(66) = 2.909
        attn_coeff(67) = 2.787
        attn_coeff(68) = 2.672
        attn_coeff(69) = 2.563
        attn_coeff(70) = 2.46
        attn_coeff(71) = 2.363
        attn_coeff(72) = 2.272
        attn_coeff(73) = 2.185
        attn_coeff(74) = 2.103
        attn_coeff(75) = 2.025
        attn_coeff(76) = 1.952
        attn_coeff(77) = 1.882
        attn_coeff(78) = 1.815
        attn_coeff(79) = 1.752
        attn_coeff(80) = 1.692
        attn_coeff(81) = 1.635
        attn_coeff(82) = 1.581
        attn_coeff(83) = 1.53
        attn_coeff(84) = 1.48
        attn_coeff(85) = 1.434
        attn_coeff(86) = 1.389
        attn_coeff(87) = 1.346
        attn_coeff(88) = 0.7845
        attn_coeff(89) = 0.3238
        attn_coeff(90) = 0.1925
        attn_coeff(91) = 0.1378
        attn_coeff(92) = 0.1093
        attn_coeff(93) = 0.08066
        attn_coeff(94) = 0.06618
        attn_coeff(95) = 0.06497
        attn_coeff(96) = 0.05576
        attn_coeff(97) = 0.05
        attn_coeff(98) = 0.04433
        attn_coeff(99) = 0.04404
        attn_coeff(100) = 0.04075
        attn_coeff(101) = 0.04038
        '
        For i = 2 To 99
            idex = i
            If energy < enereg(i) Then GoTo 100
        Next i

100:    continue1 = 1

        ener(1) = Log(enereg(idex - 2), 10)
        ener(2) = Log(enereg(idex - 1), 10)
        ener(3) = Log(enereg(idex), 10)
        ener(4) = Log(enereg(idex + 1), 10)
        ener(5) = Log(enereg(idex + 2), 10)
        '
        attn_c(1) = Log(attn_coeff(idex - 2), 10)
        attn_c(2) = Log(attn_coeff(idex - 1), 10)
        attn_c(3) = Log(attn_coeff(idex), 10)
        attn_c(4) = Log(attn_coeff(idex + 1), 10)
        attn_c(5) = Log(attn_coeff(idex + 2), 10)
        '
        sum_e = 0
        sum_e2 = 0
        sum_a = 0
        sum_ea = 0
        '
        For i = 1 To 4
            sum_e = sum_e + ener(i)
            sum_e2 = sum_e2 + ener(i) ^ 2
            sum_a = sum_a + attn_c(i)
            sum_ea = sum_ea + ener(i) * attn_c(i)
        Next i
        '
        b = (4 * sum_ea - sum_a * sum_e) / (4 * sum_e2 - sum_e ^ 2)
        a = (sum_a - b * sum_e) / 4
        '
        y = a + b * Log(energy, 10)
        '
        interp_W = 10 ^ y
        '
    End Function
    Function attn_fact_7_co(energy, concentration, i_conc, deltae, u_lor, pu_lor)
        Dim pi, lor_u, lor_pu, lor_am, lor_np, lor_cm
        Dim mu_u_l1, mu_u_l2, mu_u_l3, mu_u_h1, mu_u_h2, mu_u_h3
        Dim mu_pu_l1, mu_pu_l2, mu_pu_l3, mu_pu_h1, mu_pu_h2, mu_pu_h3
        Dim mu_am_l1, mu_am_l2, mu_am_l3, mu_am_h1, mu_am_h2, mu_am_h3
        Dim mu_np_l1, mu_np_l2, mu_np_l3, mu_np_h1, mu_np_h2, mu_np_h3
        Dim mu_cm_l1, mu_cm_l2, mu_cm_l3, mu_cm_h1, mu_cm_h2, mu_cm_h3
        Dim mu_nitric_l1, mu_nitric_l2, mu_nitric_l3, mu_nitric_h1, mu_nitric_h2, mu_nitric_h3
        Dim u_edge, pu_edge, am_edge, np_edge, cm_edge, nitric_edge
        Dim attn1, nsteps, nstart, mu_below, mu_above, log_e, y, atany, u_attn_fact, pu_attn_fact, am_attn_fact, np_attn_fact, cm_attn_fact, nitric_attn_fact
        pi = 3.14159263

        lor_u = u_lor
        lor_pu = pu_lor
        lor_am = u_lor
        lor_np = u_lor
        lor_cm = u_lor

        ' U with out coherent scattering
        mu_u_l1 = 0.0208
        mu_u_l2 = -2.683
        mu_u_l3 = 5.515
        mu_u_h1 = 0.1778
        mu_u_h2 = -3.2324
        mu_u_h3 = 6.5838

        If i_conc(1) <> 1 Then GoTo 100
        ' U with coherent scattering
        mu_u_l1 = 0.0862
        mu_u_l2 = -2.8288
        mu_u_l3 = 5.6047
        mu_u_h1 = 0.1857
        mu_u_h2 = -3.2325
        mu_u_h3 = 6.5683

100:    u_edge = 115.606

        ' Pu with out coherent scattering
        mu_pu_l1 = 292163
        mu_pu_l2 = -2.597
        mu_pu_h1 = 531436
        mu_pu_h2 = -2.437

        ' Pu with coherent scattering
        If i_conc(2) <> 1 Then GoTo 200
        mu_pu_l1 = 227031
        mu_pu_l2 = -2.513
        mu_pu_h1 = 471727
        mu_pu_h2 = -2.404

200:    pu_edge = 121.8

        ' Am with out coherent scattering
        mu_am_l1 = 297852
        mu_am_l2 = -2.595
        mu_am_h1 = 531564
        mu_am_h2 = -2.434

        ' Am with coherent scattering
        If i_conc(5) <> 1 Then GoTo 250
        mu_am_l1 = 228486
        mu_am_l2 = -2.509
        mu_am_h1 = 472019
        mu_am_h2 = -2.4

250:    am_edge = 125

        ' Np with out coherent scattering
        mu_np_l1 = 290518
        mu_np_l2 = -2.521
        mu_np_h1 = 502902
        mu_np_h2 = -2.422

        ' Np with coherent scattering
        If i_conc(3) <> 1 Then GoTo 275
        mu_np_l1 = 226198
        mu_np_l2 = -2.521
        mu_np_h1 = 502902
        mu_np_h2 = -2.422

275:    np_edge = 118.7


        'nitric without coherent scattering
        mu_nitric_l1 = 2.1117
        mu_nitric_l2 = -7.7352
        mu_nitric_l3 = 6.344
        mu_nitric_h1 = 0.0347
        mu_nitric_h2 = -0.4303
        mu_nitric_h3 = -0.0652


        If i_conc(6) <> 1 Then GoTo 300
        'nitric with coherent scattering

        mu_nitric_l1 = 1.9134
        mu_nitric_l2 = -7.1617
        mu_nitric_l3 = 5.9849
        mu_nitric_h1 = 0.118
        mu_nitric_h2 = -0.8204
        mu_nitric_h3 = 0.3958

300:    nitric_edge = 45
        nitric_edge = 45

        attn_fact_7_co = 0

        attn1 = 0

        nsteps = Int(10 / deltae) * 2 + 1
        nstart = Int(nsteps / 2)

        log_e = Log(energy, 10)


        mu_below = 10 ^ (mu_u_l1 * log_e ^ 2 + mu_u_l2 * log_e + mu_u_l3)
        mu_above = 10 ^ (mu_u_h1 * log_e ^ 2 + mu_u_h2 * log_e + mu_u_h3)
        y = 2 * (energy - u_edge) / lor_u
        atany = Atan(y)
        u_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(1)
        If energy > u_edge Then u_attn_fact = mu_above * concentration(1)

        mu_below = mu_pu_l1 * energy ^ mu_pu_l2
        mu_above = mu_pu_h1 * energy ^ mu_pu_h2
        y = 2 * (energy - pu_edge) / lor_pu
        atany = Atan(y)
        pu_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(2)
        If energy > pu_edge Then pu_attn_fact = mu_above * concentration(2)

        mu_below = mu_am_l1 * energy ^ mu_am_l2
        mu_above = mu_am_h1 * energy ^ mu_am_h2
        y = 2 * (energy - am_edge) / lor_am
        atany = Atan(y)
        am_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(5)

        mu_below = mu_np_l1 * energy ^ mu_np_l2
        mu_above = mu_np_h1 * energy ^ mu_np_h2
        y = 2 * (energy - np_edge) / lor_np
        atany = Atan(y)
        np_attn_fact = (mu_below + (mu_above - mu_below) * (0.5 + 1 / pi * atany)) * concentration(3)


        mu_below = 10 ^ (mu_nitric_l1 * log_e ^ 2 + mu_nitric_l2 * log_e + mu_nitric_l3)
        mu_above = 10 ^ (mu_nitric_h1 * log_e ^ 2 + mu_nitric_h2 * log_e + mu_nitric_h3)

        nitric_attn_fact = mu_below * concentration(6)
        If energy > nitric_edge Then nitric_attn_fact = mu_above * concentration(6)


        attn_fact_7_co = u_attn_fact + pu_attn_fact + nitric_attn_fact + am_attn_fact + np_attn_fact

    End Function
    Function attn_fact_ss_7(energy, i_co)
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

        If i_co = 1 Then GoTo 100
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

    Function attn_fact_cd_7(energy, i_co)
        Dim edge, mu_cd_l1, mu_cd_l2, mu_cd_l3, mu_cd_h1, mu_cd_h2, mu_cd_h3, log_e, mu_below, mu_above

        ' attn coeff for cd
        edge = 102
        'attn parameters with coherent scattering
        mu_cd_l1 = 0.0676
        mu_cd_l2 = -2.9308
        mu_cd_l3 = 5.773
        mu_cd_h1 = 1.1386
        mu_cd_h2 = -7.2351
        mu_cd_h3 = 10.1005


        If i_co = 1 Then GoTo 100
        'attn parameters without coherent scattering
        mu_cd_l1 = 0.03
        mu_cd_l2 = -2.8373
        mu_cd_l3 = 5.7089
        mu_cd_h1 = 1.2242
        mu_cd_h2 = -7.6469
        mu_cd_h3 = 10.555

100:    continue1 = 1
        log_e = Log(energy, 10)
        mu_below = 10 ^ (mu_cd_l1 * log_e ^ 2 + mu_cd_l2 * log_e + mu_cd_l3)
        mu_above = 10 ^ (mu_cd_h1 * log_e ^ 2 + mu_cd_h2 * log_e + mu_cd_h3)

        attn_fact_cd_7 = mu_below
        If energy > edge Then attn_fact_cd_7 = mu_above

    End Function

    Function interp_poly(energy)
        Dim enereg(101), attn_coeff(101)
        Dim ener(5), attn_c(5), idex, sum_e, sum_e2, sum_a, sum_ea, a, b, y
        '
        enereg(1) = 1
        enereg(2) = 1.5
        enereg(3) = 2
        enereg(4) = 3
        enereg(5) = 4
        enereg(6) = 5
        enereg(7) = 6
        enereg(8) = 8
        enereg(9) = 10
        enereg(10) = 15
        enereg(11) = 20
        enereg(12) = 30
        enereg(13) = 40
        enereg(14) = 50
        enereg(15) = 60
        enereg(16) = 62
        enereg(17) = 64
        enereg(18) = 66
        enereg(19) = 68
        enereg(20) = 70
        enereg(21) = 72
        enereg(22) = 74
        enereg(23) = 76
        enereg(24) = 80
        enereg(25) = 82
        enereg(26) = 84
        enereg(27) = 86
        enereg(28) = 88
        enereg(29) = 90
        enereg(30) = 92
        enereg(31) = 94
        enereg(32) = 96
        enereg(33) = 98
        enereg(34) = 100
        enereg(35) = 102
        enereg(36) = 104
        enereg(37) = 106
        enereg(38) = 108
        enereg(39) = 110
        enereg(40) = 112
        enereg(41) = 114
        enereg(42) = 116
        enereg(43) = 118
        enereg(44) = 120
        enereg(45) = 122
        enereg(46) = 124
        enereg(47) = 126
        enereg(48) = 128
        enereg(49) = 130
        enereg(50) = 132
        enereg(51) = 134
        enereg(52) = 136
        enereg(53) = 138
        enereg(54) = 140
        enereg(55) = 142
        enereg(56) = 144
        enereg(57) = 146
        enereg(58) = 148
        enereg(59) = 150
        enereg(60) = 152
        enereg(61) = 154
        enereg(62) = 156
        enereg(63) = 158
        enereg(64) = 160
        enereg(65) = 200
        enereg(66) = 300
        enereg(67) = 400
        enereg(68) = 500
        enereg(69) = 600
        enereg(70) = 800
        enereg(71) = 1000
        enereg(72) = 1022
        enereg(73) = 1250
        enereg(74) = 1500
        enereg(75) = 2000
        enereg(76) = 2044
        enereg(77) = 3000
        enereg(78) = 4000
        enereg(79) = 5000
        enereg(80) = 6000
        enereg(81) = 7000
        enereg(82) = 8000
        enereg(83) = 9000
        enereg(84) = 10000
        enereg(85) = 11000
        enereg(86) = 12000
        enereg(87) = 13000
        enereg(88) = 14000
        enereg(89) = 15000
        enereg(90) = 16000
        enereg(91) = 18000
        enereg(92) = 20000
        enereg(93) = 22000
        enereg(94) = 24000
        enereg(95) = 26000
        enereg(96) = 28000
        enereg(97) = 30000
        enereg(98) = 40000
        enereg(99) = 50000
        enereg(100) = 60000
        enereg(101) = 80000
        '
        attn_coeff(1) = 1894
        attn_coeff(2) = 600.1
        attn_coeff(3) = 259.2
        attn_coeff(4) = 77.42
        attn_coeff(5) = 32.42
        attn_coeff(6) = 16.43
        attn_coeff(7) = 9.431
        attn_coeff(8) = 3.975
        attn_coeff(9) = 2.087
        attn_coeff(10) = 0.7455
        attn_coeff(11) = 0.4316
        attn_coeff(12) = 0.2707
        attn_coeff(13) = 0.2275
        attn_coeff(14) = 0.2084
        attn_coeff(15) = 0.197
        attn_coeff(16) = 0.1952
        attn_coeff(17) = 0.1934
        attn_coeff(18) = 0.1918
        attn_coeff(19) = 0.1903
        attn_coeff(20) = 0.1888
        attn_coeff(21) = 0.1874
        attn_coeff(22) = 0.186
        attn_coeff(23) = 0.1847
        attn_coeff(24) = 0.1823
        attn_coeff(25) = 0.1811
        attn_coeff(26) = 0.18
        attn_coeff(27) = 0.1789
        attn_coeff(28) = 0.1778
        attn_coeff(29) = 0.1768
        attn_coeff(30) = 0.1757
        attn_coeff(31) = 0.1748
        attn_coeff(32) = 0.1738
        attn_coeff(33) = 0.1728
        attn_coeff(34) = 0.1719
        attn_coeff(35) = 0.171
        attn_coeff(36) = 0.1701
        attn_coeff(37) = 0.1693
        attn_coeff(38) = 0.1684
        attn_coeff(39) = 0.1676
        attn_coeff(40) = 0.1667
        attn_coeff(41) = 0.1659
        attn_coeff(42) = 0.1651
        attn_coeff(43) = 0.1644
        attn_coeff(44) = 0.1636
        attn_coeff(45) = 0.1629
        attn_coeff(46) = 0.1621
        attn_coeff(47) = 0.1614
        attn_coeff(48) = 0.1607
        attn_coeff(49) = 0.16
        attn_coeff(50) = 0.1593
        attn_coeff(51) = 0.1586
        attn_coeff(52) = 0.1579
        attn_coeff(53) = 0.1572
        attn_coeff(54) = 0.1566
        attn_coeff(55) = 0.1559
        attn_coeff(56) = 0.1553
        attn_coeff(57) = 0.1547
        attn_coeff(58) = 0.154
        attn_coeff(59) = 0.1534
        attn_coeff(60) = 0.1528
        attn_coeff(61) = 0.1522
        attn_coeff(62) = 0.1516
        attn_coeff(63) = 0.151
        attn_coeff(64) = 0.1505
        attn_coeff(65) = 0.1402
        attn_coeff(66) = 0.1217
        attn_coeff(67) = 0.109
        attn_coeff(68) = 0.09947
        attn_coeff(69) = 0.09198
        attn_coeff(70) = 0.08078
        attn_coeff(71) = 0.07263
        attn_coeff(72) = 0.07188
        attn_coeff(73) = 0.06495
        attn_coeff(74) = 0.0591
        attn_coeff(75) = 0.05064
        attn_coeff(76) = 0.05005
        attn_coeff(77) = 0.04045
        attn_coeff(78) = 0.03444
        attn_coeff(79) = 0.03045
        attn_coeff(80) = 0.02761
        attn_coeff(81) = 0.02548
        attn_coeff(82) = 0.02383
        attn_coeff(83) = 0.02252
        attn_coeff(84) = 0.02145
        attn_coeff(85) = 0.02057
        attn_coeff(86) = 0.01983
        attn_coeff(87) = 0.0192
        attn_coeff(88) = 0.01866
        attn_coeff(89) = 0.01819
        attn_coeff(90) = 0.01778
        attn_coeff(91) = 0.01711
        attn_coeff(92) = 0.01658
        attn_coeff(93) = 0.01617
        attn_coeff(94) = 0.01583
        attn_coeff(95) = 0.01554
        attn_coeff(96) = 0.01531
        attn_coeff(97) = 0.01511
        attn_coeff(98) = 0.01452
        attn_coeff(99) = 0.01425
        attn_coeff(100) = 0.01414
        attn_coeff(101) = 0.01411
        '
        For i = 2 To 99
            idex = i
            If energy < enereg(i) Then GoTo 100
        Next i

100:    continue1 = 1

        ener(1) = Log(enereg(idex - 2), 10)
        ener(2) = Log(enereg(idex - 1), 10)
        ener(3) = Log(enereg(idex), 10)
        ener(4) = Log(enereg(idex + 1), 10)
        ener(5) = Log(enereg(idex + 2), 10)
        '
        attn_c(1) = Log(attn_coeff(idex - 2), 10)
        attn_c(2) = Log(attn_coeff(idex - 1), 10)
        attn_c(3) = Log(attn_coeff(idex), 10)
        attn_c(4) = Log(attn_coeff(idex + 1), 10)
        attn_c(5) = Log(attn_coeff(idex + 2), 10)
        '
        sum_e = 0
        sum_e2 = 0
        sum_a = 0
        sum_ea = 0
        '
        For i = 1 To 4
            sum_e = sum_e + ener(i)
            sum_e2 = sum_e2 + ener(i) ^ 2
            sum_a = sum_a + attn_c(i)
            sum_ea = sum_ea + ener(i) * attn_c(i)
        Next i
        '
        b = (4 * sum_ea - sum_a * sum_e) / (4 * sum_e2 - sum_e ^ 2)
        a = (sum_a - b * sum_e) / 4
        '
        y = a + b * Log(energy, 10)
        '
        interp_poly = 10 ^ y
        '
    End Function
    Function interp_be(energy)
        Dim enereg(101), attn_coeff(101)
        Dim ener(5), attn_c(5), idex, sum_e, sum_e2, sum_a, sum_ea, a, b, y
        '
        enereg(1) = 1
        enereg(2) = 1.5
        enereg(3) = 2
        enereg(4) = 3
        enereg(5) = 4
        enereg(6) = 5
        enereg(7) = 6
        enereg(8) = 8
        enereg(9) = 10
        enereg(10) = 15
        enereg(11) = 20
        enereg(12) = 30
        enereg(13) = 40
        enereg(14) = 50
        enereg(15) = 60
        enereg(16) = 62
        enereg(17) = 64
        enereg(18) = 66
        enereg(19) = 68
        enereg(20) = 70
        enereg(21) = 72
        enereg(22) = 74
        enereg(23) = 76
        enereg(24) = 80
        enereg(25) = 82
        enereg(26) = 84
        enereg(27) = 86
        enereg(28) = 88
        enereg(29) = 90
        enereg(30) = 92
        enereg(31) = 94
        enereg(32) = 96
        enereg(33) = 98
        enereg(34) = 100
        enereg(35) = 102
        enereg(36) = 104
        enereg(37) = 106
        enereg(38) = 108
        enereg(39) = 110
        enereg(40) = 112
        enereg(41) = 114
        enereg(42) = 116
        enereg(43) = 118
        enereg(44) = 120
        enereg(45) = 122
        enereg(46) = 124
        enereg(47) = 126
        enereg(48) = 128
        enereg(49) = 130
        enereg(50) = 132
        enereg(51) = 134
        enereg(52) = 136
        enereg(53) = 138
        enereg(54) = 140
        enereg(55) = 142
        enereg(56) = 144
        enereg(57) = 146
        enereg(58) = 148
        enereg(59) = 150
        enereg(60) = 152
        enereg(61) = 154
        enereg(62) = 156
        enereg(63) = 158
        enereg(64) = 160
        enereg(65) = 200
        enereg(66) = 300
        enereg(67) = 400
        enereg(68) = 500
        enereg(69) = 600
        enereg(70) = 800
        enereg(71) = 1000
        enereg(72) = 1022
        enereg(73) = 1250
        enereg(74) = 1500
        enereg(75) = 2000
        enereg(76) = 2044
        enereg(77) = 3000
        enereg(78) = 4000
        enereg(79) = 5000
        enereg(80) = 6000
        enereg(81) = 7000
        enereg(82) = 8000
        enereg(83) = 9000
        enereg(84) = 10000
        enereg(85) = 11000
        enereg(86) = 12000
        enereg(87) = 13000
        enereg(88) = 14000
        enereg(89) = 15000
        enereg(90) = 16000
        enereg(91) = 18000
        enereg(92) = 20000
        enereg(93) = 22000
        enereg(94) = 24000
        enereg(95) = 26000
        enereg(96) = 28000
        enereg(97) = 30000
        enereg(98) = 40000
        enereg(99) = 50000
        enereg(100) = 60000
        enereg(101) = 80000

        '
        attn_coeff(1) = 604.2
        attn_coeff(2) = 179.7
        attn_coeff(3) = 74.7
        attn_coeff(4) = 21.27
        attn_coeff(5) = 8.685
        attn_coeff(6) = 4.369
        attn_coeff(7) = 2.528
        attn_coeff(8) = 1.124
        attn_coeff(9) = 0.6466
        attn_coeff(10) = 0.307
        attn_coeff(11) = 0.2251
        attn_coeff(12) = 0.1792
        attn_coeff(13) = 0.164
        attn_coeff(14) = 0.1554
        attn_coeff(15) = 0.1493
        attn_coeff(16) = 0.1482
        attn_coeff(17) = 0.1472
        attn_coeff(18) = 0.1462
        attn_coeff(19) = 0.1453
        attn_coeff(20) = 0.1443
        attn_coeff(21) = 0.1434
        attn_coeff(22) = 0.1426
        attn_coeff(23) = 0.1417
        attn_coeff(24) = 0.1401
        attn_coeff(25) = 0.1393
        attn_coeff(26) = 0.1385
        attn_coeff(27) = 0.1377
        attn_coeff(28) = 0.137
        attn_coeff(29) = 0.1362
        attn_coeff(30) = 0.1355
        attn_coeff(31) = 0.1348
        attn_coeff(32) = 0.1341
        attn_coeff(33) = 0.1334
        attn_coeff(34) = 0.1328
        attn_coeff(35) = 0.1321
        attn_coeff(36) = 0.1315
        attn_coeff(37) = 0.1308
        attn_coeff(38) = 0.1302
        attn_coeff(39) = 0.1296
        attn_coeff(40) = 0.129
        attn_coeff(41) = 0.1284
        attn_coeff(42) = 0.1278
        attn_coeff(43) = 0.1272
        attn_coeff(44) = 0.1267
        attn_coeff(45) = 0.1261
        attn_coeff(46) = 0.1256
        attn_coeff(47) = 0.125
        attn_coeff(48) = 0.1245
        attn_coeff(49) = 0.1239
        attn_coeff(50) = 0.1234
        attn_coeff(51) = 0.1229
        attn_coeff(52) = 0.1224
        attn_coeff(53) = 0.1219
        attn_coeff(54) = 0.1214
        attn_coeff(55) = 0.1209
        attn_coeff(56) = 0.1204
        attn_coeff(57) = 0.1199
        attn_coeff(58) = 0.1195
        attn_coeff(59) = 0.119
        attn_coeff(60) = 0.1185
        attn_coeff(61) = 0.1181
        attn_coeff(62) = 0.1176
        attn_coeff(63) = 0.1172
        attn_coeff(64) = 0.1168
        attn_coeff(65) = 0.1089
        attn_coeff(66) = 0.09463
        attn_coeff(67) = 0.08472
        attn_coeff(68) = 0.07739
        attn_coeff(69) = 0.07155
        attn_coeff(70) = 0.06286
        attn_coeff(71) = 0.05652
        attn_coeff(72) = 0.05591
        attn_coeff(73) = 0.05054
        attn_coeff(74) = 0.04598
        attn_coeff(75) = 0.03938
        attn_coeff(76) = 0.03891
        attn_coeff(77) = 0.03138
        attn_coeff(78) = 0.02664
        attn_coeff(79) = 0.02347
        attn_coeff(80) = 0.02121
        attn_coeff(81) = 0.01951
        attn_coeff(82) = 0.01819
        attn_coeff(83) = 0.01714
        attn_coeff(84) = 0.01627
        attn_coeff(85) = 0.01556
        attn_coeff(86) = 0.01495
        attn_coeff(87) = 0.01443
        attn_coeff(88) = 0.01399
        attn_coeff(89) = 0.01361
        attn_coeff(90) = 0.01327
        attn_coeff(91) = 0.01271
        attn_coeff(92) = 0.01227
        attn_coeff(93) = 0.01191
        attn_coeff(94) = 0.01161
        attn_coeff(95) = 0.01137
        attn_coeff(96) = 0.01117
        attn_coeff(97) = 0.011
        attn_coeff(98) = 0.01045
        attn_coeff(99) = 0.01018
        attn_coeff(100) = 0.01004
        attn_coeff(101) = 0.009942
        '
        For i = 2 To 99
            idex = i
            If energy < enereg(i) Then GoTo 100
        Next i

100:    continue1 = 1

        ener(1) = Log(enereg(idex - 2), 10)
        ener(2) = Log(enereg(idex - 1), 10)
        ener(3) = Log(enereg(idex), 10)
        ener(4) = Log(enereg(idex + 1), 10)
        ener(5) = Log(enereg(idex + 2), 10)
        '
        attn_c(1) = Log(attn_coeff(idex - 2), 10)
        attn_c(2) = Log(attn_coeff(idex - 1), 10)
        attn_c(3) = Log(attn_coeff(idex), 10)
        attn_c(4) = Log(attn_coeff(idex + 1), 10)
        attn_c(5) = Log(attn_coeff(idex + 2), 10)
        '
        sum_e = 0
        sum_e2 = 0
        sum_a = 0
        sum_ea = 0
        '
        For i = 1 To 4
            sum_e = sum_e + ener(i)
            sum_e2 = sum_e2 + ener(i) ^ 2
            sum_a = sum_a + attn_c(i)
            sum_ea = sum_ea + ener(i) * attn_c(i)
        Next i
        '
        b = (4 * sum_ea - sum_a * sum_e) / (4 * sum_e2 - sum_e ^ 2)
        a = (sum_a - b * sum_e) / 4
        '
        y = a + b * Log(energy, 10)
        '
        interp_be = 10 ^ y
        '
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

        x = Log(eff_p(7) / energy)
        eff = 0
        For i = 1 To 6
            eff = eff + eff_p(i) * x ^ (i - 1)
        Next i
        eff = Exp(eff)

        HPGE_eff = eff

    End Function
    Function gaussian(energy, e0, i0, SIGMA, deltae)
        Dim pi, y1, y
        pi = 3.14159265358979
        y1 = ((energy - e0) ^ 2) / 2 / SIGMA ^ 2

        y = deltae * i0 * Exp(-y1) / (2 * pi) ^ 0.5 / SIGMA
        gaussian = y

    End Function

    Function bent_kramers3(num, energy, delta_angle, kfit_parms, kfit_consts, i_conc, i_attn)
        Dim pi, m_e, num_div, d_a, d_e, e_0, i_0, b_kramers3, angles, angle, alpha, alpha2, rad_th, cos_th, ff_1, ff_2, d_scat_de, energy_unscat, y
        pi = 3.14159265358979
        m_e = 510.99906
        b_kramers3 = 0
        num_div = 1
        d_a = delta_angle / num_div
        '
        d_e = 0.05
        e_0 = kfit_parms(8)
        i_0 = kfit_parms(27)
        angles = kfit_parms(28)
        '
        For j = 1 To num_div
            If angles < d_a Then d_a = 0
            angle = angles + (10 - j) * d_a
            '
            alpha = energy / m_e
            alpha2 = (energy + 0.05) / m_e
            rad_th = angle * pi / 180
            cos_th = Cos(angle * pi / 180)

            ff_1 = 1 / (1 - alpha * (1 - cos_th))
            ff_2 = 1 / (1 - alpha2 * (1 - cos_th))
            'd_scat_de = (fF_2 - fF_1) / 0.05
            d_scat_de = 1
            energy_unscat = ff_1 * energy
            '
            y = functn_kfit(energy_unscat, kfit_parms, kfit_consts, i_conc, i_attn)
            'If energy_unscat < e_0 Then b_kramers3 = b_kramers3 + i_0 * (e_0 - energy_unscat) / energy_unscat / d_scat_de / num_div
            If energy_unscat < e_0 Then b_kramers3 = b_kramers3 + i_0 * y / d_scat_de / num_div

        Next j

        bent_kramers3 = b_kramers3
    End Function

    Function initialize_parms()
        Dim k$
        kfit_parms(1) = 0.323946263776864
        kfit_parms(2) = 0
        kfit_parms(3) = 0
        kfit_parms(4) = 0
        kfit_parms(5) = 0
        kfit_parms(6) = 1.205
        kfit_parms(7) = 2.59195120833899
        kfit_parms(8) = 148.739715026994
        kfit_parms(9) = 921865877.585985
        kfit_parms(10) = 0.938301393470713
        kfit_parms(11) = 18340182.349663
        kfit_parms(12) = 139.701676982461
        kfit_parms(13) = -0.062843145221328
        kfit_parms(14) = 0.0899578153383957
        kfit_parms(15) = 0.23416759598276
        kfit_parms(16) = 0.090227746574702
        kfit_parms(17) = 0.0675522505675622
        kfit_parms(18) = 2.44002052057459
        kfit_parms(19) = 147425.41348146
        kfit_parms(20) = 88.034
        kfit_parms(21) = 1
        kfit_parms(22) = 0.246919254451744
        kfit_parms(23) = 32.3582596580056
        kfit_parms(24) = 0
        kfit_parms(25) = 0.070229347622753
        kfit_parms(26) = 149.002430414552
        kfit_parms(27) = 0.864048358808887
        kfit_parms(28) = 0.229
        '
        free_params(1) = 1
        free_params(2) = 0
        free_params(3) = 0
        free_params(4) = 0
        free_params(5) = 0
        free_params(6) = 1
        free_params(7) = 1
        free_params(8) = 1
        free_params(9) = 1
        free_params(10) = 1
        free_params(11) = 1
        free_params(12) = 0
        free_params(13) = 1
        free_params(14) = 1
        free_params(15) = 1
        free_params(16) = 1
        free_params(17) = 0
        free_params(18) = 1
        free_params(19) = 0
        free_params(20) = 0
        free_params(21) = 0
        free_params(22) = 1
        free_params(23) = 1
        free_params(24) = 0
        free_params(25) = 0
        free_params(26) = 0
        free_params(27) = 1
        free_params(28) = 0
        free_params(29) = 0
        free_params(30) = 0
        free_params(31) = 0
        free_params(32) = 0
        free_params(33) = 0
        free_params(34) = 0
        free_params(35) = 0
        free_params(36) = 0

        '
        kfit_consts(1) = 1.418
        kfit_consts(2) = 0.4
        kfit_consts(3) = 0.08
        kfit_consts(4) = 0.1
        kfit_consts(5) = 115.6
        kfit_consts(6) = 0
        kfit_consts(7) = 19.48669850347
        kfit_consts(8) = 62.9305116365779
        kfit_consts(9) = 1
        kfit_consts(10) = 0
        kfit_consts(11) = 30
        kfit_consts(12) = 90
        kfit_consts(13) = 2.5
        kfit_consts(14) = 0
        kfit_consts(14) = 0
        For i = 1 To 7
            i_conc(i) = 1
        Next i
        For i = 1 To 5
            i_attn(i) = 1

        Next i
        e_fit_min = 90
        e_fit_max = 145
        initialize_parms = 1

        Dim fileReader As System.IO.StreamReader
        fileReader =
        My.Computer.FileSystem.OpenTextFileReader("C:\\travel\param_in.txt")
        Dim stringReader As String

        For i = 1 To 28
            ' stringReader = fileReader.ReadLine()
            inlineval = fileReader.ReadLine()
            kfit_parms(i) = inlineval
            '         MsgBox("The first line of the file is " & stringReader)
        Next i
        For i = 1 To 28
            ' stringReader = fileReader.ReadLine()
            inlineval = fileReader.ReadLine()
            free_params(i) = inlineval
            '         MsgBox("The first line of the file is " & stringReader)
        Next i

        For i = 1 To 28
            k$ = "par (" & i & ") = " & kfit_parms(i) & "    " & free_params(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outparameter, k$, True)
            Console.Write(k$)
        Next i

    End Function
    Function bent_kramers2(num, energy, delta_angle, end_points, angles, intensities)
        Dim pi, m_e, d_a, d_e, e_0, i_0, b_kramers2, angle, alpha, alpha2, rad_th, cos_th, ff_1, ff_2, d_scat_de, energy_unscat, y

        pi = 3.14159265358979
        m_e = 510.99906
        b_kramers2 = 0
        d_a = delta_angle / 21

        d_e = 0.05

        For j = 1 To 21
            If angles < d_a Then d_a = 0
            angle = angles + (10 - j) * d_a

            e_0 = end_points
            i_0 = intensities

            alpha = energy / m_e
            alpha2 = (energy + 0.05) / m_e
            rad_th = angle * pi / 180
            cos_th = Cos(angle * pi / 180)

            ff_1 = 1 / (1 - alpha * (1 - cos_th))
            ff_2 = 1 / (1 - alpha2 * (1 - cos_th))
            'd_scat_de = (fF_2 - fF_1) / 0.05
            d_scat_de = 1
            energy_unscat = ff_1 * energy

            If energy_unscat < e_0 Then b_kramers2 = b_kramers2 + i_0 * (e_0 - energy_unscat) / energy_unscat / d_scat_de / 21
        Next j


        bent_kramers2 = b_kramers2
    End Function

    Function g_smooth(energy, deltae, Gauss_width, tails, X)
        Dim npks, lo_tail_area, lo_tail_decay, fast_tail_area, fast_tail_decay, step_bkg_inc, Gauss_width2, y, e0, tail

        npks = 1

        'Gauss_width = 0.219
        lo_tail_area = tails(1)
        lo_tail_decay = tails(2)
        fast_tail_area = tails(3)
        fast_tail_decay = tails(4)
        step_bkg_inc = 0.00043

        Gauss_width2 = Gauss_width * (energy / 115.6) ^ 0.5

        'gaussian(energy, e0, i0, peak_width)
        'VOIGT(energy, npks, Gauss_width, E0, I0, lo_tail_area, lo_tail_decay, fast_tail_area, fast_tail_decay, deltae, lor_w)
        y = 0
        For i = 1 To 51

            e0 = energy + (25 - i) * deltae

            y = y + X(i) * gaussian(energy, e0, 1, Gauss_width2, deltae)

        Next i

        tail = 0
        '  tail_x(energy, e0, lo_tail_area, lo_tail_decay, fast_tail_area, fast_tail_decay, Gauss_width)
        'For i = 1 To 16
        'e_1 = energy + (16 - i) * deltae
        'tail = tail + x(25 + i) * tail_x(energy, e_1, lo_tail_area, lo_tail_decay, fast_tail_area, fast_tail_decay, Gauss_width)
        'next i

        g_smooth = y + tail

    End Function

    Sub aufit(ichn_start, ichn_stop)
        'Sub aufit(kfit_parms, kfit_consts, gen_parms, fit_sheet$, free_params)
        '                           JUNE 25, 1986
        '
        '                           A U F I T
        '
        '       Program to fit N peaks in an Auger spectrum assuming the peak
        '   shape can be approximated as the convolution of a gaussian and lorentzian.
        '   Initial values for peak energies, widths and intensities are
        '   required.
        '
        '
        Dim tempdata(2048), yfit(2048), weight(2048), ydat(2048)
        Dim ZDAT(2048), ERR2(2048), t_fit, b$
        Dim xdata(2048), error1(2048)
        Dim EPK(10), G0K(10), RIK(10), concentrate(7)
        Dim IBETA(10), IALPH(10) As Integer
        Dim npts, nterms, MODE, NMIN As Integer
        Dim tail_parms(4), tails(4)
        '
        Dim array_m(36, 36), BDUM(36)    '   as double precision
        Dim deltaa(36), a(36), AA(36), SIGMAA(36), i_kfit(36), idex_a(36), kfit_errors(36)
        Dim IFLE(19), NL(4) As Integer
        '   BYTE NAME(10),IDENT(60)
        Dim ITYP, RUNNO, NCHN As Integer
        Dim nt_max, ngo, itst, mxp, chi1, nff2, nff, nmax, mtrms, nchannels, temp1, j, e_zero, deltae, nmin2, nmax2, sum, niter, itmax, flamda
        Dim num_free, chis_init, chi2, chisqr, DLTCHI, chisq_init, c$, chisqr_fit
        '   COMMON/RDTCM/RUNNO,TMEL,ITYP,NAME,IDENT,NCHN
        '   COMMON/ADDON/IBETA,F4
        '
        '
        temp1 = initialize_parms()
        nterms = 0
        nt_max = 28
        '
        For i = 1 To 36
            i_kfit(i) = 0
            idex_a(i) = 0
            a(i) = 0
            kfit_errors(i) = 0
        Next i
        '
        j = 0
        For i = 1 To nt_max
            If free_params(i) = 1 Then j = j + 1
            If free_params(i) = 1 Then nterms = nterms + 1
            If free_params(i) = 1 Then i_kfit(i) = j ' position of kfit in a e.g. U only  nitric conc kfit_param (6)  -> a(2) so i_kfit(6)=2
            If free_params(i) = 1 Then idex_a(j) = i ' position of a in kfit e.g. U only  nitric conc a(2)-> kfit_param(6) so idex_a(2)=6
        Next i
        '
        '
        For i = 1 To 7
            concentrate(i) = kfit_parms(i)
        Next i
        For i = 1 To 4
            tails(i) = kfit_parms(21 + i)
        Next i

        ngo = 1
        itst = 0
        mxp = 2048
        chi1 = 0.0#
        nff2 = 0
        nff = 0
        NMIN = ichn_start
        nmax = ichn_stop
        mtrms = 36
        For i = 1 To mxp
            yfit(i) = 0
            ydat(i) = 0
            weight(i) = 0
10:     Next i
        '
        For i = 1 To mtrms
            a(i) = 0.0#
            AA(i) = 0.0#
            deltaa(i) = 0.0#
            SIGMAA(i) = 0.0#
            BDUM(i) = 0.0#
            For j = 1 To mtrms
                array_m(i, j) = 0.0#
            Next j
20:     Next i
        '
        '       READ IN SPECTRA FROM DISC
        '
        '   ----------------------------------------------
        nchannels = ichn_stop - ichn_start + 1
        NCHN = 0

        For i = 1 To mxp
            xdata(i) = 0
            error1(i) = 0
            yfit(i) = 0
            weight(i) = 0
        Next i
        '
        For i = ichn_start To ichn_stop
            tempdata(i) = corr_spectrum(i)
            yfit(i) = functn2(i, kfit_parms, kfit_consts, i_conc, i_attn)
            '           If tempdata(i) = "end" Then GoTo 47
            xdata(i) = tempdata(i)
            error1(i) = tempdata(i) ^ 0.5
            If (error1(i) <> 0) Then weight(i) = 1 / error1(i) ^ 2
            NCHN = i
47:         continue1 = 1
        Next i
        '
        nmax = NCHN

100:    continue1 = 1
        ' condense matrix
        For i = 1 To nterms
            a(i) = kfit_parms(idex_a(i))
        Next i

        For i = 1 To 4
            tails(i) = kfit_parms(21 + i)
        Next i
        e_zero = kfit_parms(13)
        deltae = kfit_parms(14)

        For i = 1 To nterms
            deltaa(i) = a(i) * 0.01
        Next i
        '    if free_params(13) = 1
        '    deltaa(13) = a(14) / 4

        For i = 1 To nterms
            If deltaa(i) = 0 Then deltaa(i) = 0.0001
            'If free_params(i) = 0 Then deltaa(i) = 0
        Next i

        nmin2 = Int(0.5 + (e_fit_min - e_zero) / deltae)
        nmax2 = Int(0.5 + (e_fit_max - e_zero) / deltae)

        npts = 1 + nmax2 - nmin2
        sum = 0.0#
        '        NMN1 = 1 + nmin2 - n1
        For i = nmin2 To nmax2
            sum = sum + xdata(i)
209:    Next i
        For i = 1 To mxp
            ERR2(i) = error1(i)
            ydat(i) = xdata(i)
            '                 Sheets(actsheetname$).Cells(16 + i, 15) = ydat(i)
210:    Next i
        '
        '
        chi1 = 0.0#
        chisqr = 0
        chisqr_fit = 0
        niter = 0
        MODE = 1
        '
        '       START FITTING
        '
        itmax = 3
        flamda = 0.001
        '
        For i = 1 To mxp
            ZDAT(i) = e_zero + i * deltae  '  energy
            '    Sheets(actsheetname$).Cells(30 + i, 16) = ZDAT(i)
214:    Next i
        '
        num_free = npts - nterms
        chisq_init = FCHISQ1(ydat, weight, npts, num_free, NMIN, yfit)
        '       Sheets(actsheetname$).Cells(40, 4) = "chisq_init = "
        '      Sheets(actsheetname$).Cells(40, 5) = chisq_init
        MsgBox("starting chisqr " & chisq_init)

215:    continue1 = 1
        For i = 1 To itmax
            For ij = 1 To nterms
                deltaa(i) = a(i) * 0.03
                '  If (deltaa(3 * IJ) < 0.01) Then deltaa(3 * IJ) = 0.01
216:        Next ij
            '    deltaa(13) = a(14)
            niter = 1 + niter
            '
            '           Sheets(actsheetname$).Cells(42, 4) = "interation = " & niter
            '          Sheets(actsheetname$).Cells(47, 4) = "nterms = " & nterms
            '  
            c$ = "reached curfit " & chisqr & vbCrLf
            Console.Write(c$)
            Call curfit(ZDAT, ydat, ERR2, npts, nterms, MODE, a, kfit_parms, kfit_consts, i_conc, i_attn, deltaa, SIGMAA, flamda, yfit, chisqr_fit, nmin2, 0, idex_a)
            chisqr = chisqr_common
            '
            c$ = "out of curfit niter = " & niter & " chi= " & chisqr & vbCrLf
            Console.Write(c$)
            Call a_to_kfit(SIGMAA, kfit_errors, idex_a, nterms)
            '           Sheets(actsheetname$).Cells(48, 4) = "nterms after= " & nterms
            chi2 = chisqr
            '         Sheets(actsheetname$).Cells(43, 4) = "CHI2 = "
            '          Sheets(actsheetname$).Cells(43, 5) = CHI2
            For ispit = 1 To nt_max
                '      Sheets(actsheetname$).Cells(4 + ispit, 3) = "a(" & ispit & ") ="
                '     Sheets(actsheetname$).Cells(4 + ispit, 4) = kfit_parms(ispit)
                '    Sheets(actsheetname$).Cells(4 + ispit, 5) = kfit_errors(ispit)
            Next ispit
            DLTCHI = Abs(chi2 - chi1) / chi2
            chi1 = chi2
            b$ = "chi = " & chi2 & vbCrLf
            My.Computer.FileSystem.WriteAllText(outparameter, b$, True)
            '           Sheets(actsheetname$).Cells(42, 8) = "chi_square = "
            '           Sheets(actsheetname$).Cells(42, 9) = chi1

            If (DLTCHI < 0.0001) Then GoTo 5600

            MsgBox("iteration " & niter & " chisqr " & chi2)

5555:   Next i
        '    TYPE *,' CHI-SQUARE DID NOT CONVERGE AFTER ',ITMAX,' ITERATIONS'
        '    TYPE *,' CONTINUE?'
        '    ACCEPT 5561, IYES
        ' 5561    FORMAT( 1A2)
        '    IF(IYES .NE. 'YE')GO TO 5600
        '    TYPE *,' MAX NUMBER OF ITERATIONS?'
        '    ACCEPT *,ITMAX
        '    GoTo 215
        '        asdf = 1 / 0
        '
5600:   continue1 = 1
        For i = 1 To nterms
            AA(i) = a(i)
5650:   Next i
        For i = 1 To nterms
            SIGMAA(i + 1) = SIGMAA(i + 1)
5700:   Next i

        For idone = 1 To nt_max
            '           Sheets(actsheetname$).Cells(idone + 100, 2) = a(idone)
            '          Sheets(actsheetname$).Cells(idone + 100, 3) = SIGMAA(idone)
            '         Sheets(actsheetname$).Cells(idone + 100, 4) = deltaa(idone)
        Next idone
        '        Sheets(actsheetname$).Cells(25, 8) = "chi-square = " & CHI2
        For idone = 1 To nchannels
            '            Sheets(actsheetname$).Cells(29 + idone, 40) = yfit(idone)
        Next idone
        '
        '

        For idone = ichn_start To ichn_stop

            '           Sheets(actsheetname$).Cells(29 + idone, 40) = ydat(idone)
            '           Sheets(actsheetname$).Cells(29 + idone, 41) = functn2(idone, kfit_parms, kfit_consts, i_conc, i_attn)
        Next idone
        For i = 1 To 2048
            If i > ichn_start Then t_fit = functn2(i, kfit_parms, kfit_consts, i_conc, i_attn) Else t_fit = 0
            b$ = t_fit & vbCrLf
            My.Computer.FileSystem.WriteAllText(outfilename, b$, True)
        Next i

        For i = 1 To 36
            b$ = kfit_parms(i) & " +/- " & kfit_errors(i) & vbCrLf
            My.Computer.FileSystem.WriteAllText(outparameter, b$, True)
        Next i
        b$ = "chi init = " & chisq_init & vbCrLf
        My.Computer.FileSystem.WriteAllText(outparameter, b$, True)
        b$ = "chi = " & chi2 & vbCrLf
        My.Computer.FileSystem.WriteAllText(outparameter, b$, True)
        b$ = "niter = " & niter & vbCrLf
        My.Computer.FileSystem.WriteAllText(outparameter, b$, True)
1001:   continue1 = 1
    End Sub
    Function FCHISQ1(y, weight, npts, nfree, NMN1, yfit)
        Dim free, nmn2, chisqr
        free = (nfree)
        nmn2 = NMN1 + npts - 1
        chisqr = 0.0#
        '        If (nfree <= 0) Then Return
        For i = NMN1 To nmn2
            chisqr = chisqr + weight(i) * (y(i) - yfit(i)) ^ 2
30:     Next i

        FCHISQ1 = chisqr / free

    End Function
    Sub a_to_kfit(a, kfit_parms, idex_a, nterms)
        'Stuffs variable parameters from a(i) back into kfit_parms(i)
        For i = 1 To nterms
            kfit_parms(idex_a(i)) = a(i)
        Next i

    End Sub
    Sub curfit(X, y, ERROR_m, npts, nterms, MODE, a, kfit_parms, kfit_consts, i_conc, i_attn, deltaa, SIGMAA, FLAMDA, yfit, CHISQR, NMIN, IPOL, idex_a)
        'curfit(ZDAT, ydat, ERR2, npts, nterms, MODE, a, kfit_parms, kfit_consts, i_conc, i_attn, deltaa, SIGMAA, flamda, yfit, chisqr, nmin2, 0, idex_a)
        '
        Dim array_m(36, 36), BDUM(36), kfit_temp(36), d$
        '    Dim ERROR_m(1), X(1)
        '    Dim Y(1), a(1), deltaa(1), SIGMAA(1), yfit(1)
        Dim WEIG(2048), ALPHA(36, 36), BETA(36), deriv(36)
        Dim b(36)
        Dim NFREE, nmax, matloops, chisq1, f1, det, alpha_temp
        '
        NFREE = npts - nterms
        nmax = NMIN + npts - 1
        matloops = 0
11:     NFREE = npts - nterms
        '----------------------  EVALUATE WEIGHTS ----------------

20:     continue1 = 1
        For i = NMIN To nmax
            '        If (ERROR_m(i) = 0#) Then ERROR_m(i) = (Abs(y(i)) ^ 0.5)
            ERROR_m(i) = (Abs(y(i)) ^ 0.5)
29:         WEIG(i) = 1 / ERROR_m(i) ^ 2

        Next i

        '--------------- EVALUATE ALPHA AND BETA MATRICES -----------
        '
31:     For j = 1 To nterms
            BETA(j) = 0
            For k = 1 To j
34:             ALPHA(j, k) = 0
                '        array_m(j, K) = 0
            Next k
        Next j

        Call a_to_kfit(a, kfit_parms, idex_a, nterms)
        d$ = "in curfit 34 " & NMIN & " " & nmax & " " & nterms & vbCrLf
        Console.Write(d$)
41:     For i As Integer = NMIN To nmax
            d$ = "in loop i= " & i & vbCrLf
            Console.Write(d$)
            Call fderiv2(i, a, kfit_parms, kfit_consts, nterms, deltaa, deriv, i_conc, i_attn, free_params, idex_a)
            For j = 1 To nterms
                BETA(j) = BETA(j) + WEIG(i) * (y(i) - functn2(i, kfit_parms, kfit_consts, i_conc, i_attn)) * deriv(j)
                For k = 1 To j
                    ALPHA(j, k) = ALPHA(j, k) + WEIG(i) * deriv(j) * deriv(k)
                Next k
            Next j
        Next i
        d$ = "in curfit 41"
        Console.Write(d$)
51:     For j = 1 To nterms
            For k = 1 To j
53:             ALPHA(k, j) = ALPHA(j, k)
            Next k
        Next j
        'For ix = 1 To nterms
        'For jx = 1 To nterms
        '       Sheets(actsheetname$).Cells(20 + ix, 38 + jx) = ALPHA(ix, jx)
        'Next jx
        'Next ix

        '----------   EVALUATE CHI SQUARE AT STARTING POINT    ---------

61:     For i = NMIN To nmax
62:         yfit(i) = functn2(i, kfit_parms, kfit_consts, i_conc, i_attn)
        Next i
63:     chisq1 = FCHISQ1(y, WEIG, npts, NFREE, NMIN, yfit)

        '  --------  INVERT MODIFIED CURVATURE MATRIX TO FIND NEW PARAMETERS

71:     For j = 1 To nterms
            For k = 1 To nterms
                f1 = (ALPHA(j, j) * ALPHA(k, k)) ^ 0.5
73:             If (f1 <> 0) Then array_m(j, k) = (ALPHA(j, k) / f1)
            Next k
74:         array_m(j, j) = 1 + (FLAMDA)
        Next j

        matloops = matloops + 1
        If matloops > 50 Then GoTo 101
        '       Sheets(actsheetname$).Cells(42, 6) = "loops " & matloops
        '    For ix = 1 To nterms
        '    For jx = 1 To nterms
        '        Sheets(actsheetname$).Cells(56 + ix, 59 + jx) = array_m(ix, jx)
        '    Next jx
        '    Next ix
        d$ = "reached matinv"
        Console.Write(d$)
80:     Call MATInv(nterms, det, array_m)
        d$ = "out of matinv"
        Console.Write(d$)
        For ix = 1 To nterms
            For jx = 1 To nterms
                '               Sheets(actsheetname$).Cells(40 + ix, 58 + jx) = array_m(ix, jx)
            Next jx
        Next ix

81:     For j = 1 To nterms
            b(j) = a(j)
            For k = 1 To nterms
                f1 = (ALPHA(j, j) * ALPHA(k, k)) ^ 0.5
84:             If (f1 <> 0) Then b(j) = b(j) + BETA(k) * array_m(j, k) / f1
            Next k
        Next j

        ' ------------- IF CHI SQUARE INCREASED, INCREASE FLAMDA AND TRY AGAIN

        '    For jx = 1 To nterms
        '        Sheets(actsheetname$).Cells(2 + jx, 44) = b(jx)
        '        Sheets(actsheetname$).Cells(2 + jx, 45) = SIGMAA(jx)
        '    Next jx
        '
        For i_101 = 1 To 36
            kfit_temp(i_101) = kfit_parms(i_101)
        Next i_101
        '
        Call a_to_kfit(b, kfit_temp, idex_a, nterms)
        '
91:     For i = NMIN To nmax
92:         yfit(i) = functn2(i, kfit_temp, kfit_consts, i_conc, i_attn)
        Next i

93:     CHISQR = FCHISQ1(y, WEIG, npts, NFREE, NMIN, yfit)
        chisqr_common = CHISQR
        d$ = "in curfit chi= " & CHISQR & vbCrLf
        Console.Write(d$)
        If (chisq1 - CHISQR >= 0) Then GoTo 101
95:     FLAMDA = 10.0# * FLAMDA
        '    For iwait = 1 To 10
        '    Sheets(actsheetname$).Cells(20, 37) = b(iwait)
        '    Next iwait
        GoTo 71

        '-----------  EVALUATE NEW PARAMETERS

101:    For j = 1 To nterms
            a(j) = b(j)
            alpha_temp = ALPHA(j, j)
            If (alpha_temp = 0) Then SIGMAA(j) = 0
            If (alpha_temp <> 0) Then SIGMAA(j) = ((1.0# + FLAMDA) * array_m(j, j) / alpha_temp) ^ 0.5
        Next j
        FLAMDA = FLAMDA / 10.0#
        Call a_to_kfit(a, kfit_parms, idex_a, nterms)

110:    continue1 = 1

        '    For ix = 1 To nterms
        '    For jx = 1 To nterms
        '        alpha_temp = ALPHA(ix, jx)
        '        If (alpha_temp = 0) Then temp = 0
        '        If (alpha_temp <> 0) Then temp = ((1# + FLAMDA) * array_m(ix, jx) / alpha_temp)
        '        Sheets(actsheetname$).Cells(56 + ix, 58 + jx) = temp
        '    Next jx
        '    Next ix

    End Sub
    Sub fderiv2(chan_num, a, kfit_parms, kfit_consts, nterms, deltaa, deriv, i_conc, i_attn, free_params, idex_a)
        '
        Dim b(36), kfit_temp(36), low_val, High_val, orig_val
        '
        'e_zero = a(13)
        'deltae = a(14)

        'energy = e_zero + chan_num * deltae
        orig_val = functn2(chan_num, kfit_parms, kfit_consts, i_conc, i_attn)
        For i = 1 To 36
            kfit_temp(i) = kfit_parms(i)
        Next i

        For i = 1 To nterms
            b(i) = a(i)
        Next i

        For i = 1 To nterms
            b(i) = a(i) - deltaa(i)
            Call a_to_kfit(b, kfit_temp, idex_a, nterms)
            low_val = functn2(chan_num, kfit_temp, kfit_consts, i_conc, i_attn)
            '
            b(i) = a(i) + deltaa(i)
            Call a_to_kfit(b, kfit_temp, idex_a, nterms)
            High_val = functn2(chan_num, kfit_temp, kfit_consts, i_conc, i_attn)
            If deltaa(i) <> 0 Then deriv(i) = (High_val - low_val) / 2 / deltaa(i) Else deriv(i) = 0
            b(i) = a(i)
        Next i

        'For i = 1 To nterms
        '  If free_params(i) = 0 Then deriv(i) = 0
        'Next i


    End Sub
    Sub MATInv(nordr, det, array_m)
        Dim BDUM(36)
        Dim ik(36), jk(36), amax, a, b, asdf, i, j, k, save

        det = 1.0#

        For k = 1 To nordr
            '-------FIND LARGEST ELEMENT ARRAY(I,J) IN REST OF MATRIX
            amax = 0
21:         For i = k To nordr
                For j = k To nordr
                    a = Abs(amax)
                    b = Abs(array_m(i, j))
                    If (a - b) > 0 Then GoTo 30
24:                 amax = array_m(i, j)
                    ik(k) = i
                    jk(k) = j
                Next j
            Next i
30:         continue1 = 1
            '-------INTERCHANGE ROWS AND COLLUMNS TO PUT AMAX IN ARRAY(K,K)
            If (amax <> 0) Then GoTo 41
32:         det = 0.0#
            asdf = 1 / 0 '  Return
41:         i = ik(k)
            If (i - k < 0) Then GoTo 21
            If (i - k = 0) Then GoTo 51
43:         For j = 1 To nordr
                save = array_m(k, j)
                array_m(k, j) = array_m(i, j)
50:             array_m(i, j) = -save
            Next j
51:         j = jk(k)
            If (j - k < 0) Then GoTo 21
            If (j - k = 0) Then GoTo 61

53:         For i = 1 To nordr
                save = array_m(i, k)
                array_m(i, k) = array_m(i, j)
60:             array_m(i, j) = -save
            Next i
            '-------ACCUMULATE ELEMENTS OF INVERSE MATRIX
61:         For i = 1 To nordr
                If (i = k) Then GoTo 70
                array_m(i, k) = -array_m(i, k) / amax
70:             continue1 = 1
            Next i

            For i = 1 To nordr
                For j = 1 To nordr
                    If (i = k) Then GoTo 80
                    If (j = k) Then GoTo 80
                    array_m(i, j) = array_m(i, j) + array_m(i, k) * array_m(k, j)
80:                 continue1 = 1
                Next j
            Next i
            For j = 1 To nordr
                If (j = k) Then GoTo 90
                array_m(k, j) = array_m(k, j) / amax
90:             continue1 = 1
            Next j
            array_m(k, k) = 1.0# / amax
            det = det * (amax)
100:    Next k
        '
        'C-------RESTORE ORDERING OF MATRIX
        '
        For L = 1 To nordr
            k = nordr - L + 1
            j = ik(k)
            If (j = k) Then GoTo 111
            For i = 1 To nordr
                save = array_m(i, k)
                array_m(i, k) = -array_m(i, j)
110:            array_m(i, j) = save
            Next i
111:        i = jk(k)
            If (i = k) Then GoTo 130
            For j = 1 To nordr
                save = array_m(k, j)
                array_m(k, j) = -array_m(i, j)
120:            array_m(i, j) = save
            Next j
130:        continue1 = 1
        Next L

    End Sub



End Module

Public Class Form4
    Public free_params(28)

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        '   MessageBox.Show("You are in the CheckedListBox.Click event.")

    End Sub

    Private Sub CheckedListBox3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CheckedListBox3.SelectedIndexChanged

    End Sub

    Private Sub CheckedListBox2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CheckedListBox2.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub


    Public Sub checkbox_1()
        Dim i As Integer
        Dim s As String
        Dim infile_name As String
        For i = 1 To 28
            free_params(i) = 0
        Next i
        s = "Checked Items:" & ControlChars.CrLf
        For i = 0 To (CheckedListBox1.Items.Count - 1)
            If CheckedListBox1.GetItemChecked(i) = True Then
                s = s & "Item " & (i + 1).ToString & " = " & CheckedListBox1.Items(i).ToString & ControlChars.CrLf
                free_params(i + 1) = 1
            End If
        Next
        MessageBox.Show(s)
        MessageBox.Show(free_params(3))
        infile_name = "testfile"
        '   Call Main()

    End Sub
    Private Sub checkbox_1a()
        ' Determine if there are any items checked.
        If CheckedListBox1.CheckedItems.Count <> 0 Then
            ' If so, loop through all checked items and print results.
            Dim x As Integer
            Dim s As String = ""
            For x = 0 To CheckedListBox1.CheckedItems.Count - 1
                s = s & "Checked Item " & (x + 1).ToString & " = " & CheckedListBox1.CheckedItems(x).ToString & ControlChars.CrLf
            Next x
            MessageBox.Show(s)
        End If



    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Call checkbox_1()
    End Sub

    Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click

    End Sub
End Class