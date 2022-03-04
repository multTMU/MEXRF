Imports System.Math
Imports System.IO
Imports System.Text

Module response_functions



    Function functn2(chan_num, a)
        '       * * * XRFFUNC * * *
        '
        '	XRF Response function
        '
        '	Ported from XRF VBA routine
        '	August 21, 2018
        '
        Dim pk_area(15), el_counts(15), lor_width(15)
        Dim concentration(7), i_scat(7)
        Dim bkg_par(4), TAILS(4), temp_tail(4)
        Dim e_escape(2), i_escape(2)
        Dim brem_I, ebel_I, b_tail_area, b_tau, tail_2, tau_2
        '
        Dim e_start, deltae, gauss_width, step_bkg_inc, xrf_bkg_norm, ge_area
        Dim bkg, trans_factor, attn_factor, attn_length, kappa, sum_el_area
        Dim ge_wt, erf_bkg, cd_step_inc, cd_area
        Dim e2, ENERGY, energy1, e3, pi
        Dim E_cd, I0_CD, peak_width, cd_step_bkg, cd_bkg_step_low, bkg_step
        Dim Cd109, bkg1, bkg2, bkg3, corr_attn
        Dim TEMP_VAL, temp_num, xrf_temp, e_tail
        Dim d_vial, d_xrf, d_col, eff_0, delta_attn
        '
        Dim xrf_bkg_spline(9)
        Dim fp_e(5), fp_i(5), fp_w, attn_factor_1, W_AREA, PB_AREA, ener2
        Dim rand_bkg, e_ebel, ebel_par_x

        Dim n_it, n_pks, i_int, ii, i, j As Integer
        Dim element_peaks(7), i_fp As Integer
        '
        '
        '
        '	Primary response components
        '
        '	temp_val = contribution from actinide X-ray peaks and gamma-ray peaks - includes tails 
        '	erf_bkg=   step background for X-ray peaks and gamma-ray peaks
        '	bkg =      linear background - Compton background from the fission products
        '	bkg1 =     primary x-ray beam backscatter contribution 
        '	bkg2 =     1st Ge escape peak from x-ray beam backscatter contribution	
        '	bkg3 =     2nd Ge escape peak from x-ray beam backscatter contribution	
        '   bkg_step = step background from x-ray beam backscatter contribution
        '	rand_bkg = random background contribution determined by convoluting measured spectrum with itself
        '                  this scaled using a(25) to allow introduction of a linear Compton background from the fission products
        '	W_AREA =   Tungsten X-ray peak contribution
        '	Pb_AREA =  Lead X-ray peak contribution
        '
        '
        bkg = 0.0
        erf_bkg = 0.0
        cd_step_bkg = a(39)
        cd_bkg_step_low = Const_val(19)
        Cd109 = 0
        deltae = a(16)
        n_it = Const_val(40)          ' subdivide channels increment
        If (n_it < 1) Then n_it = 1
        '
        pi = 3.14159265359
        '
        For i = 1 To 6
            concentration(i) = a(i + 8)
            i_scat(i) = 0
        Next i

        i_scat(6) = 0

        d_vial = Const_val(6)
        d_xrf = Const_val(7)
        d_col = Const_val(8)

        eff_0 = HPGE_eff(xrf_e(1, 1))   ' hpge detection effieincy at first x-ray energy
        '
        e_start = a(15)
        deltae = a(16)
        gauss_width = a(17)

        For l1 = 1 To 6
            concentrate(l1) = a(8 + l1)
            concentration(l1) = a(8 + l1)
        Next l1

        Call call_x_ratio_corr2(deltae, d_vial, d_xrf, d_col, xrf_abs)

        '
        TAILS(1) = Abs(a(18))       '  contribution must always be positive
        TAILS(2) = Abs(a(19))       '  first tail must always fall away from the peak on the low energy side
        TAILS(3) = Abs(a(37))       '  contribution must always be positive
        TAILS(4) = a(38)            '  second tail may be be either low or high sided
        '
        temp_tail(1) = Abs(a(18))   '  contribution must always be positive
        temp_tail(2) = Abs(a(19))   '  first tail must always fall away from the peak on the low energy side
        temp_tail(3) = Abs(a(37))   '  contribution must always be positive
        temp_tail(4) = a(38)        '  second tail may be be either low or high sided

        For l1 = 1 To 14
            xrf_bkg(l1) = Const_val(l1 + 19)
            xrf_bkg(12) = Const_val(6)
        Next l1
        '
        step_bkg_inc = Abs(a(20))
        '	xrf_bkg_norm = 0
        ge_area = a(21)

        bkg_par(1) = a(26)      '  compton bkg contribution offset
        bkg_par(2) = a(27)      '  compton bkg contribution linear term
        bkg_par(3) = 0.0        '  polynomial background limited to linear
        bkg_par(4) = 0.0        '  polynomial background limited to linear
        '
        e_escape(1) = 9.88
        e_escape(2) = 10.98
        '
        ' 	fission product peaks
        '
        fp_e(1) = 123.068       ' Eu_154
        fp_e(2) = 105.306       ' Eu_155
        fp_e(3) = 86.543        ' Eu_155
        fp_e(4) = 80.12         ' Ce_144
        fp_e(5) = 133.515       ' Ce_144
        fp_i(1) = a(6)
        fp_i(2) = a(7)
        fp_i(3) = a(7) * 34.0 / 20.6 * (HPGE_eff(fp_e(3)) / HPGE_eff(fp_e(2)))
        delta_attn = attn_fact_7_co(fp_e(3), concentration) - attn_fact_7_co(fp_e(2), concentration)
        fp_i(3) = fp_i(3) * Exp(-delta_attn * d_vial)
        fp_i(5) = a(8)

        fp_i(4) = a(8) * (1.13 / 11.1) * (HPGE_eff(fp_e(4)) / HPGE_eff(fp_e(5)))
        delta_attn = attn_fact_7_co(fp_e(4), concentration) - attn_fact_7_co(fp_e(5), concentration)
        fp_i(4) = fp_i(4) * Exp(-delta_attn * d_vial)

        '
        energy1 = 0
        energy1 = energy_calc(chan_num, e_start, deltae)
        ' ---------------------------------------------------
100:    TEMP_VAL = 0
        bkg = 0
        pk_area(1) = a(1)   ' U
        pk_area(2) = a(2)   ' Pu
        pk_area(3) = a(3)   ' Np
        pk_area(4) = a(4)   ' Cm
        pk_area(5) = a(5)   ' Am

        ' 	subdivide channel to integrate over channel - n_it defined in system constants

        For i_int = 1 To n_it
            ENERGY = energy1 + (i_int - 5.5) * deltae / n_it
            If (n_it = 1) Then ENERGY = energy1

            For ii = 1 To 5                  '  cycle through 5 actinides
                n_pks = Const_val(ii)
                sum_el_area = 0

                '	add relative branching ratios for the current element
                For j = 1 To n_pks
                    sum_el_area = sum_el_area + xrf_rel(ii, j)
                Next j

                '	Distribute peak area accross the XRF peaks

                For j = 1 To n_pks
                    el_counts(j) = 0
                    If (sum_el_area <> 0) Then el_counts(j) = pk_area(ii) * (xrf_rel(ii, j) / sum_el_area)

                Next j

                '
                '       np = 1
                '
                '	Calculated characteristic actinides x-ray contribution to the spectrum
                '
                For jj = 1 To n_pks
                    TEMP_VAL = TEMP_VAL + el_counts(jj) * VOIGT(ENERGY, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj))  '   x-ray peaks
                    '	
                    e2 = ENERGY + e_escape(1)
                    ge_wt = escape_area(xrf_e(ii, jj), 1) * ge_area
                    If (ii < 3) Then TEMP_VAL = TEMP_VAL + el_counts(jj) * ge_wt * VOIGT(e2, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj)) * 2 * lor_w(ii, jj) / PI             '   escape peak 1

                    e2 = ENERGY + e_escape(2)
                    ge_wt = escape_area(xrf_e(ii, jj), 2) * ge_area
                    If (ii < 3) Then TEMP_VAL = TEMP_VAL + el_counts(jj) * ge_wt * VOIGT(e2, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj)) * 2 * lor_w(ii, jj) / pi             '   escape peak 2

                    erf_bkg = erf_bkg + step_bkg_inc * el_counts(jj) * (1 + erf((xrf_e(ii, jj) - ENERGY) / 2 / gauss_width)) / 2           '   step background

                Next jj
            Next ii
        Next i_int
        '
        TEMP_VAL = TEMP_VAL / n_it
        '
        '	Cd-109 peak
        peak_width = a(22)
        E_cd = a(23)
        I0_CD = 1.0
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        If (ENERGY > 93) Then GoTo 50
        cd_area = a(24)
        Cd109 = cd_area * gaussian(ENERGY, E_cd, I0_CD, peak_width, deltae)                     '    Cd peak area
        Cd109 = Cd109 + cd_step_bkg * cd_area * (1 + erf((E_cd - ENERGY) / 2 / peak_width)) * (1 + erf((ENERGY - Const_val(19)) / 2 / (3 * peak_width))) / 4          '    Cd step background
        Cd109 = Cd109 * (1 + tail_x(ENERGY, E_cd, temp_tail, peak_width))                       '    Cd tail

        '	
        e2 = ENERGY + e_escape(1)
        ge_wt = escape_area(E_cd, 1) * ge_area
        Cd109 = Cd109 + ge_wt * cd_area * gaussian(e2, E_cd, I0_CD, peak_width, deltae)
        e2 = ENERGY + e_escape(2)
        ge_wt = escape_area(E_cd, 2) * ge_area
        Cd109 = Cd109 + ge_wt * cd_area * gaussian(e2, E_cd, I0_CD, peak_width, deltae)
        '
50:
        '
        '	add in fission product peaks
        '
        For i_fp = 1 To 5
            ENERGY = energy_calc(chan_num, e_start, deltae)
            fp_w = a(22) * (fp_e(i_fp) / 88.0) ^ 0.5
            Cd109 = Cd109 + fp_i(i_fp) * gaussian(ENERGY, fp_e(i_fp), I0_CD, fp_w, deltae)      '    FP peak area
        Next i_fp
        '
        '	calulcate attenuation correction for emitted x-rays
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        kappa = Const_val(16)
        attn_factor = attn_fact_7_co(ENERGY, concentration)     ' attenuation factor at current energy
        attn_factor_1 = attn_fact_7_co(xrf_e(1, 1), concentration)           ' reference correction to U Ka1 X-ray
        attn_length = (d_vial / 2 - d_xrf)
        '
        If (d_xrf > d_vial) Then d_xrf = d_vial
        If (attn_length < 0) Then attn_length = 0.0
        '
        ' 	correct x-ray peaks  for relative attenuation and efficiency relative to 98.4 keV
        TEMP_VAL = TEMP_VAL * Exp(-attn_length * (attn_factor - attn_factor_1)) * (HPGE_eff(ENERGY) / HPGE_eff(xrf_e(1, 1)))
        '
        energy1 = energy_calc(chan_num, e_start, deltae)
        '
        bkg = bkg_par(1) + bkg_par(2) * energy1 + bkg_par(3) * energy1 ^ 2 + bkg_par(4) * energy1 ^ 3    'polynomial bkg for fission products

        erf_bkg = erf_bkg / (n_it)           '  + bkg
        '	
        '
        e_ebel = a(28)                      ' x-ray end point energy
        ebel_par_x = a(29)              ' Generator shaping parameter
        brem_I = Abs(a(30))                ' brem intensity
        ebel_I = Abs(a(31))                ' elastic scatter fraction
        xrf_bkg_spline(1) = Abs(a(32))        ' spline ref energy 1
        xrf_bkg_spline(2) = Abs(a(33))        ' spline ref energy 2
        xrf_bkg_spline(4) = a(34)          ' spline par A1
        xrf_bkg_spline(5) = a(35)          ' spline par A2
        xrf_bkg_spline(3) = Abs(a(36))        ' spline decay constant
        xrf_bkg_spline(6) = 0
        xrf_bkg_spline(7) = 0
        xrf_bkg_spline(8) = 0
        xrf_bkg_spline(9) = 0

        xrf_bkg(13) = a(41)     '   allow fitting of stainless steel thickness

        ENERGY = energy_calc(chan_num, e_start, deltae)

        bkg1 = brem_bkg_new(ENERGY, e_ebel, ebel_par_x, xrf_bkg, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)    '  brem continuum (already attenuated)

        e2 = ENERGY + e_escape(1)
        ge_wt = escape_area(e2, 1) * ge_area
        bkg2 = 0
        '  bkg2 = ge_wt * brem_bkg_new(e2, e_ebel, ebel_par_x, xrf_bkg, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)

        e2 = ENERGY + e_escape(2)
        ge_wt = escape_area(e2, 1) * ge_area

        bkg3 = 0
        'bkg3 = ge_wt * brem_bkg_new(e2, e_ebel, ebel_par_x, xrf_bkg, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)

        bkg_step = 0

        e3 = ENERGY
101:    e3 = e3 + deltae
        '	bkg_step=bkg_step+step_bkg_inc*brem_bkg_new(e3,e_ebel,ebel_par_x,xrf_bkg,brem_I,ebel_I, xrf_bkg_spline, concentration,deltae)
        If (e3 < e_ebel) Then GoTo 101
        '
        '
        rand_bkg = a(25) * rand_temp(chan_num)



        '
        '	add in W and Pb X-rays
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        ener2 = energy_calc(chan_num, e_start, deltae)
        W_AREA = a(40) * HPGE_eff(ener2) / HPGE_eff(59.2) * deltae
        W_AREA = W_AREA * W_VOIGT(ENERGY, gauss_width, TAILS, deltae)

        PB_AREA = a(42) * HPGE_eff(ener2) / HPGE_eff(74.969) * deltae
        PB_AREA = PB_AREA * PB_VOIGT(ENERGY, gauss_width, TAILS, deltae)
        '
        functn2 = TEMP_VAL + erf_bkg + bkg + bkg1 + 0 * bkg2 + 0 * bkg3 + Cd109 + rand_bkg + bkg_step + W_AREA + PB_AREA

        '
    End Function

    Function functn3(chan_num, a, i_element)
        '       * * * XRFFUNC * * *
        '
        '	XRF Response function <- actinide contribution only
        '
        '	Ported from XRF VBA routine
        '	August 21, 2018
        '
        Dim pk_area(15), el_counts(15), lor_width(15)
        Dim concentration(7), i_scat(7)
        Dim bkg_par(4), TAILS(4), temp_tail(4)
        Dim e_escape(2), i_escape(2)
        Dim brem_I, ebel_I, b_tail_area, b_tau, tail_2, tau_2
        '
        Dim e_start, deltae, gauss_width, step_bkg_inc, xrf_bkg_norm, ge_area
        Dim bkg, trans_factor, attn_factor, attn_length, kappa, sum_el_area
        Dim ge_wt, erf_bkg, cd_step_inc, cd_area
        Dim e2, ENERGY, energy1, e3, pi
        Dim E_cd, I0_CD, peak_width, cd_step_bkg, bkg_step
        Dim Cd109, bkg1, bkg2, bkg3, corr_attn
        Dim TEMP_VAL, temp_num, xrf_temp, e_tail
        Dim d_vial, d_xrf, d_col, eff_0, delta_attn
        '
        Dim xrf_bkg_spline(9)
        Dim fp_e(5), fp_i(5), fp_w, attn_factor_1, W_AREA, PB_AREA, ener2
        Dim rand_bkg, e_ebel, ebel_par_x

        Dim n_it, n_pks, i_int, ii, i, j As Integer
        Dim element_peaks(7), i_fp As Integer
        '
        '
        '
        '	Primary response components
        '
        '	temp_val = contribution from actinide X-ray peaks and gamma-ray peaks - includes tails 
        '	erf_bkg=   step background for X-ray peaks and gamma-ray peaks
        '	bkg =      linear background - Compton background from the fission products
        '	bkg1 =     primary x-ray beam backscatter contribution 
        '	bkg2 =     1st Ge escape peak from x-ray beam backscatter contribution	
        '	bkg3 =     2nd Ge escape peak from x-ray beam backscatter contribution	
        '   bkg_step = step background from x-ray beam backscatter contribution
        '	rand_bkg = random background contribution determined by convoluting measured spectrum with itself
        '                  this scaled using a(25) to allow introduction of a linear Compton background from the fission products
        '	W_AREA =   Tungsten X-ray peak contribution
        '	Pb_AREA =  Lead X-ray peak contribution
        '
        '
        bkg = 0.0
        erf_bkg = 0.0
        cd_step_bkg = 0
        Cd109 = 0
        deltae = a(16)
        n_it = Const_val(40)          ' subdivide channels increment
        If (n_it < 1) Then n_it = 1
        '
        pi = 3.14159265359
        '
        For i = 1 To 6
            concentration(i) = a(i + 8)
            i_scat(i) = 0
        Next i

        i_scat(6) = 0

        d_vial = Const_val(6)
        d_xrf = Const_val(7)
        d_col = Const_val(8)

        eff_0 = HPGE_eff(xrf_e(1, 1))   ' hpge detection effieincy at first x-ray energy
        '
        e_start = a(15)
        deltae = a(16)
        gauss_width = a(17)

        For l1 = 1 To 6
            concentrate(l1) = a(8 + l1)
            concentration(l1) = a(8 + l1)
        Next l1

        Call call_x_ratio_corr2(deltae, d_vial, d_xrf, d_col, xrf_abs)

        '
        TAILS(1) = Abs(a(18))       '  contribution must always be positive
        TAILS(2) = Abs(a(19))       '  first tail must always fall away from the peak on the low energy side
        TAILS(3) = Abs(a(37))       '  contribution must always be positive
        TAILS(4) = a(38)            '  second tail may be be either low or high sided
        '
        temp_tail(1) = Abs(a(18))   '  contribution must always be positive
        temp_tail(2) = Abs(a(19))   '  first tail must always fall away from the peak on the low energy side
        temp_tail(3) = Abs(a(37))   '  contribution must always be positive
        temp_tail(4) = a(38)        '  second tail may be be either low or high sided

        For l1 = 1 To 14
            xrf_bkg(l1) = Const_val(l1 + 19)
            xrf_bkg(12) = Const_val(6)
        Next l1
        '
        step_bkg_inc = Abs(a(20))
        '	xrf_bkg_norm = 0
        ge_area = a(21)

        bkg_par(1) = a(26)      '  compton bkg contribution offset
        bkg_par(2) = a(27)      '  compton bkg contribution linear term
        bkg_par(3) = 0.0        '  polynomial background limited to linear
        bkg_par(4) = 0.0        '  polynomial background limited to linear
        '
        e_escape(1) = 9.88
        e_escape(2) = 10.98
        '
        ' 	fission product peaks
        '
        fp_e(1) = 123.068       ' Eu_154
        fp_e(2) = 105.306       ' Eu_155
        fp_e(3) = 86.543        ' Eu_155
        fp_e(4) = 80.12         ' Ce_144
        fp_e(5) = 133.515       ' Ce_144
        fp_i(1) = a(6)
        fp_i(2) = a(7)
        fp_i(3) = a(7) * 34.0 / 20.6 * (HPGE_eff(fp_e(3)) / HPGE_eff(fp_e(2)))
        delta_attn = attn_fact_7_co(fp_e(3), concentration) - attn_fact_7_co(fp_e(2), concentration)
        fp_i(3) = fp_i(3) * Exp(-delta_attn * d_vial)
        fp_i(5) = a(8)

        fp_i(4) = a(8) * (1.13 / 11.1) * (HPGE_eff(fp_e(4)) / HPGE_eff(fp_e(5)))
        delta_attn = attn_fact_7_co(fp_e(4), concentration) - attn_fact_7_co(fp_e(5), concentration)
        fp_i(4) = fp_i(4) * Exp(-delta_attn * d_vial)

        '
        energy1 = 0
        energy1 = energy_calc(chan_num, e_start, deltae)
        ' ---------------------------------------------------
100:    TEMP_VAL = 0
        bkg = 0
        pk_area(1) = a(1)   ' U
        pk_area(2) = a(2)   ' Pu
        pk_area(3) = a(3)   ' Np
        pk_area(4) = a(4)   ' Cm
        pk_area(5) = a(5)   ' Am

        ' 	subdivide channel to integrate over channel - n_it defined in system constants

        For i_int = 1 To n_it
            ENERGY = energy1 + (i_int - 5.5) * deltae / n_it
            If (n_it = 1) Then ENERGY = energy1

            ii = i_element                 '  cycle through 5 actinides
            n_pks = Const_val(ii)
                sum_el_area = 0

                '	add relative branching ratios for the current element
                For j = 1 To n_pks
                    sum_el_area = sum_el_area + xrf_rel(ii, j)
                Next j

                '	Distribute peak area accross the XRF peaks

                For j = 1 To n_pks
                    el_counts(j) = 0
                    If (sum_el_area <> 0) Then el_counts(j) = pk_area(ii) * (xrf_rel(ii, j) / sum_el_area)

                Next j

                '
                '       np = 1
                '
                '	Calculated characteristic actinides x-ray contribution to the spectrum
                '
                For jj = 1 To n_pks
                    TEMP_VAL = TEMP_VAL + el_counts(jj) * VOIGT(ENERGY, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj))  '   x-ray peaks
                    '	
                    e2 = ENERGY + e_escape(1)
                    ge_wt = escape_area(xrf_e(ii, jj), 1) * ge_area
                    If (ii < 3) Then TEMP_VAL = TEMP_VAL + el_counts(jj) * ge_wt * VOIGT(e2, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj)) * 2 * lor_w(ii, jj) / pi             '   escape peak 1

                    e2 = ENERGY + e_escape(2)
                    ge_wt = escape_area(xrf_e(ii, jj), 2) * ge_area
                    If (ii < 3) Then TEMP_VAL = TEMP_VAL + el_counts(jj) * ge_wt * VOIGT(e2, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj)) * 2 * lor_w(ii, jj) / pi             '   escape peak 2

                    erf_bkg = erf_bkg + step_bkg_inc * el_counts(jj) * (1 + erf((xrf_e(ii, jj) - ENERGY) / 2 / gauss_width)) / 2           '   step background

                Next jj

        Next i_int
        '
        TEMP_VAL = TEMP_VAL / n_it
        '

        '	
        '
50:
        '
        '	add in fission product peaks
        '
        '
        '	calulcate attenuation correction for emitted x-rays
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        kappa = Const_val(16)
        attn_factor = attn_fact_7_co(ENERGY, concentration)     ' attenuation factor at current energy
        attn_factor_1 = attn_fact_7_co(xrf_e(1, 1), concentration)           ' reference correction to U Ka1 X-ray
        attn_length = (d_vial / 2 - d_xrf)
        '
        If (d_xrf > d_vial) Then d_xrf = d_vial
        If (attn_length < 0) Then attn_length = 0.0
        '
        ' 	correct x-ray peaks  for relative attenuation and efficiency relative to 98.4 keV
        TEMP_VAL = TEMP_VAL * Exp(-attn_length * (attn_factor - attn_factor_1)) * (HPGE_eff(ENERGY) / HPGE_eff(xrf_e(1, 1)))
        '
        energy1 = energy_calc(chan_num, e_start, deltae)
        '
        bkg = bkg_par(1) + bkg_par(2) * energy1 + bkg_par(3) * energy1 ^ 2 + bkg_par(4) * energy1 ^ 3    'polynomial bkg for fission products

        erf_bkg = erf_bkg / (n_it)           '  + bkg
        '	

        '
        '
        functn3 = TEMP_VAL + erf_bkg

        '
    End Function

    Function functn_fiss(chan_num, a)
        '       * * * XRFFUNC * * *
        '
        '	XRF Response function - gamma ray contribution
        '
        '	Ported from XRF VBA routine
        '	August 21, 2018
        '
        Dim pk_area(15), el_counts(15), lor_width(15)
        Dim concentration(7), i_scat(7)
        Dim bkg_par(4), TAILS(4), temp_tail(4)
        Dim e_escape(2), i_escape(2)
        Dim brem_I, ebel_I, b_tail_area, b_tau, tail_2, tau_2
        '
        Dim e_start, deltae, gauss_width, step_bkg_inc, xrf_bkg_norm, ge_area
        Dim bkg, trans_factor, attn_factor, attn_length, kappa, sum_el_area
        Dim ge_wt, erf_bkg, cd_step_inc, cd_area
        Dim e2, ENERGY, energy1, e3, pi
        Dim E_cd, I0_CD, peak_width, cd_step_bkg, bkg_step, cd_bkg_step_low
        Dim Cd109, bkg1, bkg2, bkg3, corr_attn
        Dim TEMP_VAL, temp_num, xrf_temp, e_tail
        Dim d_vial, d_xrf, d_col, eff_0, delta_attn
        '
        Dim xrf_bkg_spline(5)
        Dim fp_e(100), fp_i(100), fp_w, attn_factor_1, W_AREA, PB_AREA, ener2
        Dim rand_bkg, e_ebel, ebel_par_x

        Dim n_it, n_pks, i_int, ii, i, j As Integer
        Dim element_peaks(7), i_fp As Integer
        '
        '
        '
        bkg = 0.0
        erf_bkg = 0.0
        cd_step_bkg = a(39)
        cd_bkg_step_low = Const_val(19)
        Cd109 = 0
        deltae = a(16)
        '
        pi = 3.14159265359
        '
        For i = 1 To 6
            concentration(i) = a(i + 8)
            i_scat(i) = 0
        Next i

        i_scat(6) = 0

        d_vial = Const_val(6)
        d_xrf = Const_val(7)
        d_col = Const_val(8)

        eff_0 = HPGE_eff(xrf_e(1, 1))   ' hpge detection effieincy at first x-ray energy
        '
        e_start = a(15)
        deltae = a(16)
        gauss_width = a(17)

        For l1 = 1 To 6
            concentrate(l1) = a(8 + l1)
            concentration(l1) = a(8 + l1)
        Next l1

        '
        TAILS(1) = Abs(a(18))       '  contribution must always be positive
        TAILS(2) = Abs(a(19))       '  first tail must always fall away from the peak on the low energy side
        TAILS(3) = Abs(a(37))       '  contribution must always be positive
        TAILS(4) = a(38)            '  second tail may be be either low or high sided
        '
        temp_tail(1) = Abs(a(18))   '  contribution must always be positive
        temp_tail(2) = Abs(a(19))   '  first tail must always fall away from the peak on the low energy side
        temp_tail(3) = Abs(a(37))   '  contribution must always be positive
        temp_tail(4) = a(38)        '  second tail may be be either low or high sided

        For l1 = 1 To 14
            xrf_bkg(l1) = Const_val(l1 + 19)
            xrf_bkg(12) = Const_val(6)
        Next l1
        '
        step_bkg_inc = Abs(a(20))
        '	xrf_bkg_norm = 0
        ge_area = a(21)
        '
        e_escape(1) = 9.88
        e_escape(2) = 10.98
        '
        ' 	fission product peaks
        '
        Dim k As Integer
        Dim g_energy1, g_energy2
        k = 0

        For i = 1 To 3
            For j = 1 To gamma_pts(i)
                k = k + 1
                fp_e(k) = gamma_energy(i, j)
            Next j
        Next i

        fp_i(1) = a(6)
                fp_i(1 + gamma_pts(1)) = a(7)
                fp_i(1 + gamma_pts(1) + gamma_pts(2)) = a(8)

                k = 0

        For i = 1 To 3
            For j = 1 To gamma_pts(i)
                k = k + 1
                g_energy1 = gamma_energy(i, 1)
                g_energy2 = gamma_energy(i, j)

                fp_i(k) = a(i + 5) * gamma_int(i, j) / gamma_int(i, 1) * (HPGE_eff(g_energy2) / HPGE_eff(g_energy1))
                delta_attn = attn_fact_7_co(g_energy2, concentration) - attn_fact_7_co(g_energy1, concentration)
                fp_i(k) = fp_i(k) * Exp(-delta_attn * d_vial)
            Next j
        Next i

        ' ---------------------------------------------------
100:    TEMP_VAL = 0
        bkg = 0
        '
        '	Cd-109 peak
        peak_width = Abs(a(22))
        E_cd = a(23)
        I0_CD = 1.0
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        If (ENERGY > 93) Then GoTo 50
        cd_area = a(24)

        Cd109 = cd_area * gaussian(ENERGY, E_cd, I0_CD, peak_width, deltae)                     '    Cd peak area
        ' If (ENERGY > Const_val(19) And ENERGY < E_cd) Then 
        Cd109 = Cd109 + cd_step_bkg * cd_area * (1 + erf((E_cd - ENERGY) / 2 / peak_width)) * (1 + erf((ENERGY - Const_val(19)) / 2 / (3 * peak_width))) / 4          '    Cd step background
        Cd109 = Cd109 * (1 + tail_x(ENERGY, E_cd, temp_tail, peak_width))                       '    Cd tail
        '	
        e2 = ENERGY + e_escape(1)
        ge_wt = escape_area(E_cd, 1) * ge_area
        Cd109 = Cd109 + ge_wt * cd_area * gaussian(e2, E_cd, I0_CD, peak_width, deltae)
        e2 = ENERGY + e_escape(2)
        ge_wt = escape_area(E_cd, 2) * ge_area
        Cd109 = Cd109 + ge_wt * cd_area * gaussian(e2, E_cd, I0_CD, peak_width, deltae)
        '
50:
        '
        '	add in fission product peaks
        '
        For i_fp = 1 To 5
            ENERGY = energy_calc(chan_num, e_start, deltae)
            fp_w = a(22) * (fp_e(i_fp) / 88.0) ^ 0.5
            Cd109 = Cd109 + fp_i(i_fp) * gaussian(ENERGY, fp_e(i_fp), I0_CD, fp_w, deltae)      '    FP peak area
        Next i_fp
        '
        '
        functn_fiss = Cd109
        '
        '
    End Function



    Function functn_bkg(chan_num, a)
        '       * * * XRFFUNC * * *
        '
        '	XRF Response function
        '
        Dim pk_area(15), el_counts(15), lor_width(15)
        Dim concentration(7), i_scat(7)
        Dim bkg_par(4), TAILS(4), temp_tail(4)
        Dim e_escape(2), i_escape(2)
        Dim brem_I, ebel_I, b_tail_area, b_tau, tail_2, tau_2
        '
        Dim e_start, deltae, gauss_width, step_bkg_inc, xrf_bkg_norm, ge_area
        Dim bkg, trans_factor, attn_factor, attn_length, kappa, sum_el_area
        Dim ge_wt, erf_bkg, cd_step_inc, cd_area
        Dim e2, ENERGY, energy1, e3, pi
        Dim E_cd, I0_CD, peak_width, cd_step_bkg, bkg_step
        Dim Cd109, bkg1, bkg2, bkg3, corr_attn
        Dim TEMP_VAL, temp_num, xrf_temp, e_tail
        Dim d_vial, d_xrf, d_col, eff_0, delta_attn
        '
        Dim xrf_bkg_spline(9)
        Dim fp_e(5), fp_i(5), fp_w, attn_factor_1, W_AREA, PB_AREA, ener2
        Dim rand_bkg, e_ebel, ebel_par_x

        Dim n_it, n_pks, i_int, ii, i, j As Integer
        Dim element_peaks(7), i_fp As Integer
        '
        '
        '
        '	Primary response components
        '
        '	temp_val = contribution from actinide X-ray peaks and gamma-ray peaks - includes tails 
        '	erf_bkg=   step background for X-ray peaks and gamma-ray peaks
        '	bkg =      linear background - Compton background from the fission products
        '	bkg1 =     primary x-ray beam backscatter contribution 
        '	bkg2 =     1st Ge escape peak from x-ray beam backscatter contribution	
        '	bkg3 =     2nd Ge escape peak from x-ray beam backscatter contribution	
        '   bkg_step = step background from x-ray beam backscatter contribution
        '	rand_bkg = random background contribution determined by convoluting measured spectrum with itself
        '                  this scaled using a(25) to allow introduction of a linear Compton background from the fission products
        '	W_AREA =   Tungsten X-ray peak contribution
        '	Pb_AREA =  Lead X-ray peak contribution
        '
        '
        bkg = 0.0
        erf_bkg = 0.0
        cd_step_bkg = 0
        Cd109 = 0
        W_AREA = 0
        deltae = a(16)
        n_it = Const_val(40)          ' subdivide channels increment
        If (n_it < 1) Then n_it = 1
        '
        pi = 3.14159265359
        '
        For i = 1 To 6
            concentration(i) = a(i + 8)
            i_scat(i) = 0
        Next i

        i_scat(6) = 0

        d_vial = Const_val(6)
        d_xrf = Const_val(7)
        d_col = Const_val(8)

        eff_0 = HPGE_eff(xrf_e(1, 1))   ' hpge detection effieincy at first x-ray energy
        '
        e_start = a(15)
        deltae = a(16)
        gauss_width = a(17)

        For l1 = 1 To 6
            concentrate(l1) = a(8 + l1)
            concentration(l1) = a(8 + l1)
        Next l1

        Call call_x_ratio_corr2(deltae, d_vial, d_xrf, d_col, xrf_abs)

        '
        TAILS(1) = Abs(a(18))       '  contribution must always be positive
        TAILS(2) = Abs(a(19))       '  first tail must always fall away from the peak on the low energy side
        TAILS(3) = Abs(a(37))       '  contribution must always be positive
        TAILS(4) = a(38)            '  second tail may be be either low or high sided
        '
        temp_tail(1) = Abs(a(18))   '  contribution must always be positive
        temp_tail(2) = Abs(a(19))   '  first tail must always fall away from the peak on the low energy side
        temp_tail(3) = Abs(a(37))   '  contribution must always be positive
        temp_tail(4) = a(38)        '  second tail may be be either low or high sided

        For l1 = 1 To 14
            xrf_bkg(l1) = Const_val(l1 + 19)
            xrf_bkg(12) = Const_val(6)
        Next l1
        '
        step_bkg_inc = a(20)
        '	xrf_bkg_norm = 0
        ge_area = a(21)

        bkg_par(1) = a(26)      '  compton bkg contribution offset
        bkg_par(2) = a(27)      '  compton bkg contribution linear term
        bkg_par(3) = 0.0        '  polynomial background limited to linear
        bkg_par(4) = 0.0        '  polynomial background limited to linear
        '
        e_escape(1) = 9.88
        e_escape(2) = 10.98
        '
        energy1 = 0
        energy1 = energy_calc(chan_num, e_start, deltae)
        ' ---------------------------------------------------
100:    TEMP_VAL = 0
        bkg = 0

        '
50:
        '	calulcate attenuation correction for emitted x-rays
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        kappa = Const_val(16)
        attn_factor = attn_fact_7_co(ENERGY, concentration)     ' attenuation factor at current energy
        attn_factor_1 = attn_fact_7_co(xrf_e(1, 1), concentration)           ' reference correction to U Ka1 X-ray
        attn_length = (d_vial / 2 - d_xrf)
        '
        If (d_xrf > d_vial) Then d_xrf = d_vial
        If (attn_length < 0) Then attn_length = 0.0
        '
        ' 	correct x-ray peaks  for relative attenuation and efficiency relative to 98.4 keV

        '
        energy1 = energy_calc(chan_num, e_start, deltae)
        '
        bkg = bkg_par(1) + bkg_par(2) * energy1 + bkg_par(3) * energy1 ^ 2 + bkg_par(4) * energy1 ^ 3    'polynomial bkg for fission products

        erf_bkg = erf_bkg / (n_it)           '  + bkg
        '	
        '
        e_ebel = a(28)                      ' x-ray end point energy
        ebel_par_x = a(29)                  ' Generator shaping parameter
        brem_I = Abs(a(30))                 ' brem intensity
        ebel_I = Abs(a(31))                 ' elastic scatter fraction
        xrf_bkg_spline(1) = Abs(a(32))      ' spline ref energy 1
        xrf_bkg_spline(2) = Abs(a(33))      ' spline ref energy 2
        xrf_bkg_spline(4) = a(34)           ' spline par A1
        xrf_bkg_spline(5) = a(35)           ' spline par A2
        xrf_bkg_spline(3) = Abs(a(36))      ' spline decay constant
        xrf_bkg_spline(6) = 0
        xrf_bkg_spline(7) = 0
        xrf_bkg_spline(8) = 0
        xrf_bkg_spline(9) = 0

        xrf_bkg(13) = a(41)     '   allow fitting of stainless steel thickness

        ENERGY = energy_calc(chan_num, e_start, deltae)

        bkg1 = brem_bkg_new(ENERGY, e_ebel, ebel_par_x, xrf_bkg, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)   '  brem continuum (already attenuated)

        e2 = ENERGY + e_escape(1)
        ge_wt = escape_area(e2, 1) * ge_area
        bkg2 = 0
        '  bkg2 = ge_wt * brem_bkg_new(e2, e_ebel, ebel_par_x, xrf_bkg, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)

        e2 = ENERGY + e_escape(2)
        ge_wt = escape_area(e2, 1) * ge_area

        bkg3 = 0
        'bkg3 = ge_wt * brem_bkg_new(e2, e_ebel, ebel_par_x, xrf_bkg, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)

        bkg_step = 0

        e3 = ENERGY
101:    e3 = e3 + deltae
        '	bkg_step=bkg_step+step_bkg_inc*brem_bkg_new(e3,e_ebel,ebel_par_x,xrf_bkg,brem_I,ebel_I, xrf_bkg_spline, concentration,deltae)
        If (e3 < e_ebel) Then GoTo 101
        '
        '
        rand_bkg = a(25) * rand_temp(chan_num)

        '
        '	add in W and Pb X-rays    ---  I considered these peaks as part of the background
        '
        ENERGY = energy_calc(chan_num, e_start, deltae)
        ener2 = energy_calc(chan_num, e_start, deltae)
        W_AREA = a(40) * HPGE_eff(ener2) / HPGE_eff(59.2) * deltae
        W_AREA = W_AREA * W_VOIGT(ENERGY, gauss_width, TAILS, deltae)

        PB_AREA = a(42) * HPGE_eff(ener2) / HPGE_eff(74.969) * deltae
        PB_AREA = PB_AREA * PB_VOIGT(ENERGY, gauss_width, TAILS, deltae)

        ' MsgBox("its here in functn_bkg")
        '
        functn_bkg = erf_bkg + bkg + bkg1 + rand_bkg + bkg_step + W_AREA + PB_AREA
        '
    End Function


    Function brem_bkg_new(energy, e_0, par_x, ebel_par, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)
        '        
        '	Calculates backscatter spectrum from the theorectical source term
        '	ebel1 is the simple backscattered Bremmstrahlung spectrum
        '	ebel2 is the elastically scattered Bremmstrahlung spectrum
        '	The continuum representing multiple scatter events is approximated using a smoothed expontential spline
        '

        Dim temp_array(101), temp_array2(101)
        Dim tail_area, tau, dbin
        Dim em_2, par_y, ang_1, ang_2, g_width, sol_thick, w_1, w_2
        Dim cd_thick, Vial_thick, Vial_Diameter, SS_thick, d_coll, Pi, xrf_ang
        Dim e_temp, e_p, attn, attn1, attn2, attn3, pol_thick
        Dim ebel1, ebel2, e_tail, spline

        Dim scat_fact, g_width2, e_3, spline2, tail_2, tau_2, e_off
        '
        '	ebel_par(1) = g_width2
        '	ebel_par(2) = not used
        '	ebel_par(3) = not used
        '	ebel_par(4) = brem shaping par b (normally = 0)
        '	ebel_par(5) = generator target angle 1
        '	ebel_par(6) = generator target angle 2
        '	ebel_par(7) = g_width  -  guassian width of smoothing function applied to scatter distribution
        '	ebel_par(8) = not used
        '	ebel_par(9) = X-ray backscatter angle (HPGE backward angle)
        '	ebel_par(10) = Cd foil thickness
        '	ebel_par(11) = Vial Wall thickness 
        '	ebel_par(12) = Vial Diameter (inner diameter of the sample vial)
        '	ebel_par(13) = stainless steel thickness (total between vial and detector)
        '	ebel_par(14) = d_coll
        '
        g_width2 = ebel_par(1)
        pol_thick = ebel_par(2)         ' not used 
        em_2 = 511.0                    ' electron rest mass
        '	Par_x = ebel_par(3)	        ' bremsstrahlung distribution shapping parameter (free parameter)
        par_y = ebel_par(4)             ' bremsstrahlung distribution shapping parameter (2nd order)
        ang_1 = ebel_par(5)             '  Generator internal angle
        ang_2 = ebel_par(6)             '  Generator internal angle
        g_width = ebel_par(7)           ' guassian width of smoothing function applied to scatter distribution
        e_off = ebel_par(8)             ' energy offset for tailing function ( set = 0)
        w_1 = 1                         ' tungsten target atten coefficient weighting factor
        w_2 = 0                         ' tungsten target atten coefficient weighting factor
        xrf_ang = ebel_par(9)           ' HPGE backward angle
        cd_thick = ebel_par(10)         ' Cd foil thickness between generator and vial
        Vial_thick = ebel_par(11)       ' vial wall thickness
        Vial_Diameter = ebel_par(12)    ' inner diameter of the sample vial
        SS_thick = ebel_par(13)         ' stainless steel thickness (total between vial and detector)
        d_coll = ebel_par(14)           ' XRF collimator diameter (cm)
        '
        Pi = 3.1415926535898
        '
        attn2 = Exp(-attn_fact_ss_7(energy, 1) * SS_thick / 0.866025 * 8.0)                     ' adjust ss thickness for 30 degree angle
        attn2 = attn2 * Exp(-Vial_thick * 0.95 * attn_fact_poly_co(energy))
        attn2 = attn2 * Exp(-(Vial_Diameter / 2.0) * attn_fact_7_co(energy, concentration))    ' attn for out going X-rays


        '
        For i = 1 To 101                                    ' loop to build temp array for gausssian smoothing
            e_temp = energy + (i - 51) * deltae
            e_p = E_KNR(e_temp, xrf_ang)
            '
            attn1 = Exp(-attn_fact_ss_7(e_p, 1) * SS_thick * 8.0) * Exp(-attn_fact_cd_7(e_p, 1) * cd_thick * 8.67)
            attn1 = attn1 * Exp(-Vial_thick * 0.95 * attn_fact_poly_co(e_p))
            attn1 = attn1 * Exp(-(Vial_Diameter / 2.0) * attn_fact_7_co(e_p, concentration))    '  attn for incoming X-rays
            '
            dbin = (E_KNR(e_temp + deltae / 2, xrf_ang) - E_KNR(e_temp - deltae / 2.0, xrf_ang))
            ebel1 = ebel(e_p, e_0, par_x, par_y, ang_1, ang_2, w_1, w_2) * attn2 * attn1 / dbin     '  inelastic backsatter X-rays
            temp_array(i) = ebel1
            '
            '	scat_fact = 1.0 -  DEXP(-d_coll*scat_fact_7(e_temp, concentration))			' elastic scatter macroscopic x-section
            scat_fact = d_coll * scat_fact_7(e_temp, concentration)
            '
            temp_array2(i) = scat_fact * ebel(e_temp, e_0, par_x, par_y, ang_1, ang_2, w_1, w_2) * attn2 * attn2        ' brem dist elastic scatter
            '
        Next i
        '
        ebel1 = g_smooth(energy, deltae, g_width, temp_array)               ' brem dist inelastic scatter	
        ebel2 = g_smooth(energy, deltae, g_width2, temp_array2)             ' brem dist elastic scatter
        '
        e_tail = E_KN(e_0, xrf_ang) - e_off                                 ' spline based on hyperbolic cosine approach
        spline = brem_bkg3(energy, xrf_bkg_spline)                          ' bridge between inelastic and elastic      
        '    If energy > 20 Then spline = spline + Abs(xrf_bkg_spline(9)) / energy ^ 2 * attn2                   '  <------------- mod to test new bkg
        '
        brem_bkg_new = HPGE_eff(energy) * (brem_I * (ebel1 + ebel_I * ebel2 + spline))
        brem_bkg_new = Max(brem_bkg_new, 0)
        '
    End Function
    '
    '   	---------------------------------------------------
    '
    Function ebel(energy, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)
        '
        Dim mu_z, F_ABS, I_A, par_nu, dn_de
        Dim d_e, cal_c, tim, omega, eps, phi
        Dim U_0, par_z, par_m, par_j, alnz
        Dim rho_zm, rho_zbar, attn_fact_W, par_a, pi

        ebel = 0.0
        If (energy > e_0) Then GoTo 100
        d_e = 0.09
        cal_c = 1.0
        tim = 1.0
        I_A = 15.0
        omega = (0.15 / 12) ^ 2     '  aproximate solid angle of detector/colimator	

        '	tungsten target
        par_z = 74.0
        par_a = 183.84
        '
        PI = 3.14159265358979
        phi = (90.0 - ang_1) / 180.0 * PI
        eps = ang_2 / 180.0 * PI
        '
        par_m = 0.1382 - 0.9211 / par_z ^ 0.5
        par_j = 0.0135 * par_z
        U_0 = e_0 / energy
        '
        alnz = Log(par_z)      ' dlog = natural log
        '
        par_nu = (e_0 ^ par_m) * (0.1904 - 0.2236 * alnz + 0.1292 * alnz ^ 2 - 0.0149 * alnz ^ 3)
        '
        rho_zm = par_a / par_z * (0.00000787 * par_j ^ 0.5 * e_0 ^ 1.5 + 0.000000735 * e_0 ^ 2)
        '
        rho_zbar = rho_zm * (0.49269 - 1.0987 * par_nu + 0.78557 * par_nu ^ 2) * Log(U_0)
        rho_zbar = rho_zbar / (0.70256 - 1.09865 * par_nu + 1.0046 * par_nu ^ 2 + Log(U_0))
        '
        mu_z = attn_fact_W_PE2(energy, w_1, w_2)
        F_ABS = (1 - Exp(-mu_z * 2 * rho_zbar * Sin(phi) / Sin(eps))) / (mu_z * 2 * rho_zbar * Sin(phi) / Sin(eps))
        '
        dn_de = cal_c * omega * I_A * tim * par_z * (e_0 / energy - 1.0) ^ Par_x * F_ABS * d_e
        '
        ebel = dn_de * (e_0 / energy) ^ par_y
100:
        '
    End Function

    Sub call_x_ratio_corr2(deltae, v_diameter, d_xrf, v_outer, xrf_abs)
        '
        ' for solution concentration, U_conc and Pu_conc, and vial diameter v_d, adjust branching ratios for attenuation effects
        ' normalize to U K-alpha peak
        '
        '	dim mu_u(10), mu_pu(10), mu_np(10), mu_cm(10), mu_am(10)
        Dim conc1(7)
        Dim test_trans(10), mu_matrix(5, 15), corr_matrix(5, 15), eff_matrix(5, 15), f_e(5, 15)
        Dim concentration(6)

        Dim utot, putot, nptot, cmtot, amtot, kappa
        '

        Dim element_peaks(7) As Integer
        '   
        '    
        '  Normalize branching ratios
        '
        utot = 0
        putot = 0
        nptot = 0
        cmtot = 0
        amtot = 0
        '
        For i = 1 To 15
            utot = utot + xrf_abs(1, i)
            putot = putot + xrf_abs(2, i)
            nptot = nptot + xrf_abs(3, i)
            cmtot = cmtot + xrf_abs(4, i)
            amtot = amtot + xrf_abs(5, i)
        Next i

        For i = 1 To 15
            xrf_rel(1, i) = xrf_abs(1, i) / utot
            xrf_rel(2, i) = xrf_abs(2, i) / putot
            xrf_rel(3, i) = xrf_abs(3, i) / nptot
            xrf_rel(4, i) = xrf_abs(4, i) / cmtot
            xrf_rel(5, i) = xrf_abs(5, i) / amtot
        Next i
        '
        '	adjust branching ratios for detector efficiency and attenuation
        '
        kappa = Const_val(16)
        '
        '	v_outer = v_diameter / 2 - d_xrf
        '	If(d_xrf .GT. v_diameter) d_xrf = v_diameter
        '	If(v_outer .LT. 0) v_outer = 0

        GoTo 80                 '  branching ratio corrections now performed during fit!

        For i = 1 To 6
            i_conc(i) = 1
            concentration(i) = concentrate(i)
            '	conc1(i) = 1
        Next i
        '
        '	Create matrix of macroscopic attenuation factors
        '
        For L = 1 To 5                  ' for each of 5 elements
            For i = 1 To 15                 ' for each of 15 x-ray lines
                '
                mu_matrix(L, i) = attn_fact_7_co(xrf_e(L, i), concentration)
                corr_matrix(L, i) = 1
                eff_matrix(L, i) = HPGE_eff(xrf_e(L, i))
                f_e(L, i) = Exp(-mu_matrix(L, i) * d_xrf)
                '
            Next i
        Next L

        For L = 1 To 5
            For i = 1 To 15
                If (f_e(L, i) <> 0) Then corr_matrix(L, i) = -Log(f_e(L, i) ^ kappa) / (1 - f_e(L, i) ^ kappa) * Exp(-v_outer * mu_matrix(L, i))
            Next i
        Next L
        '
        For L = 1 To 5
            For i = 1 To 15
                '           xrf_rel(L, i) = xrf_rel(L, i) * corr_matrix(L, i) / corr_matrix(1, 1) * eff_matrix(L, i) / eff_matrix(1, 1)
                xrf_rel(L, i) = xrf_rel(L, i)
            Next i
        Next L
80:
        Return
        '
    End Sub

    Function VOIGT(energy, gauss_width, E0, tails, deltae, lor_w)
        '
        Dim e_temp, del_peak, e_off, y_temp
        Dim gauss_width_0, lor_width, temp_tail(4), tail, tail_w1, tail_w
        VOIGT = 0
        If (Abs(energy - E0) > 50.0) Then GoTo 50
        y_temp = 0
        tail = 0
        tail_w1 = 0
        del_peak = tails(3)         '  allows for splitting of peak due to electronics problem (non guassian broadening) 
        e_off = tails(4)            '  allows for splitting of peak due to electronics problem 

        del_peak = 0        '  allows for splitting of peak due to electronics problem (non guassian broadening) 
        e_off = 0            '  allows for splitting of peak due to electronics problem 

        temp_tail(1) = tails(1)
        temp_tail(2) = tails(2)
        temp_tail(3) = tails(3)
        temp_tail(4) = tails(4)

        gauss_width_0 = (E0 / 88.0) ^ 0.5 * gauss_width
        e_temp = E0
        lor_width = lor_w
        y_temp = (1.0 - del_peak) * czosnyka(energy, e_temp, lor_width, gauss_width_0)
        y_temp = y_temp + del_peak * czosnyka(energy, e_temp, lor_width * (1.0 + e_off), gauss_width_0)

        tail = tail_x(energy, E0, temp_tail, gauss_width_0)
        VOIGT = deltae * y_temp + tail
50:
    End Function

    Function escape_area(energy, np)
        '
        '	np is escape peak 1 or 2 (9.88 or 10.99 keV)
        Dim edge, attn_fact_ge
        Dim frac, A1, A2, esc_e(2), esc_mu(2)



        esc_e(1) = 9.88
        esc_e(2) = 10.99
        esc_mu(1) = 37.42
        esc_mu(2) = 37.42
        '
        edge = 11.1
        '
        frac = 0.467
        If (np = 2) Then frac = 0.0603
        '
        A1 = 77700
        A2 = -2.523
        '
        attn_fact_ge = 2.81
        '
        If (energy > edge) Then attn_fact_ge = A1 * energy ^ A2
        '
        escape_area = frac * attn_fact_ge / esc_mu(np) / 2.5

    End Function

    Function tail_x(energy, e0, TAILS, Gauss_width)
        '
        '	Based on Ray Gunnink's representation of the tailing function
        '
        Dim lo_tail_a, fast_tail_a
        Dim T_X, t_x2
        Dim TAU1, beta, TAU2, G_width

        '
        tail_x = 0
        T_X = 0.0
        t_x2 = 0.0
        G_width = Gauss_width
        '
        lo_tail_a = Abs(TAILS(1))
        TAU1 = Abs(TAILS(2))
        fast_tail_a = Abs(TAILS(3))
        TAU2 = Abs(TAILS(4))
        '
        beta = TAU1 * (energy - e0) + 0.5 * (G_width * TAU1) ^ 2        ' primnary low side tail

        T_X = lo_tail_a * Exp(beta)
        beta = (energy - e0) / G_width + G_width * TAU1
        T_X = T_X * erfc(beta / 2 ^ 0.5)
        '
5:      beta = TAU2 * (e0 - energy) + 0.5 * (G_width * TAU2) ^ 2        ' changed to default to high side tail

        t_x2 = fast_tail_a * Exp(beta)
        beta = (e0 - energy) / G_width + G_width * TAU2
        t_x2 = t_x2 * erfc(beta / 2 ^ 0.5)

10:
        tail_x = T_X + t_x2
101:

    End Function


    Function energy_calc(i, E0, deltae)
        energy_calc = E0 + i * deltae
    End Function


    Function gaussian(energy, e0, P0, SIGMA, deltae)
        '
        Dim y1, y, pi
        pi = 3.14159265358979
        y1 = ((energy - e0) ^ 2) / 2.0 / SIGMA ^ 2
        y = deltae * P0 * Exp(-y1) / (2.0 * pi) ^ 0.5 / SIGMA
        gaussian = y

    End Function
    Function PB_VOIGT(energy, Gauss_width, tails, deltae)
        '
        '       Creates tungsten X-ray peaks
        '	The relative peak intensities will not be very accurate because the source distribution/attenuation in not defined.
        '	Estimation of the true coincidence summing peaks will be further complicated by the source distribution
        '
        Dim E_pb(12), I0(12), lor_w_pb(12)

        Dim Y_TEMP, LOR_WIDTH_pb, gauss_width_0
        Dim TAIL, E_TEMP, E_1
        Dim npks As Integer
        '
        E_pb = {74.969, 72.805, 72.144, 84.938, 87.3, 84.45, 87.58, 85.47, 87.911, 88.003, 0.0, 0.0}
        lor_w_pb = {0.066, 0.068, 0.068, 0.068, 0.068, 0.068, 0.068, 0.068, 0.068, 0.068, 0.068, 0.068}
        I0 = {0.462, 0.277, 0.000428, 0.107, 0.0391, 0.0558, 0.0009, 0.00312, 0.007, 0.000165, 0.0, 0.0}

        PB_VOIGT = 0.0
        Y_TEMP = 0.0
        TAIL = 0.0
        npks = 10


        '
        For j = 0 To npks - 1
            gauss_width_0 = (E_pb(j) / 88.0) ^ 0.5 * Gauss_width
            E_TEMP = E_pb(j)
            '	
            LOR_WIDTH_pb = lor_w_pb(j)


            Y_TEMP = I0(j) * czosnyka(energy, E_TEMP, LOR_WIDTH_pb, gauss_width_0)
            '
            TAIL = 0
            For i = 16 To 16
                E_1 = E_pb(j) + (16 - i) * deltae
                '       TAIL = TAIL + tail_x(E_1, energy, tails, gauss_width_0)
            Next i

            TAIL = I0(j) * TAIL
            '
            PB_VOIGT = PB_VOIGT + Y_TEMP    '  + tail
            '
        Next j
        '
    End Function

    Function Bi_VOIGT(energy, Gauss_width, tails, deltae)
        '
        '       Creates tungsten X-ray peaks
        '	The relative peak intensities will not be very accurate because the source distribution/attenuation in not defined.
        '	Estimation of the true coincidence summing peaks will be further complicated by the source distribution
        '
        Dim gauss_width_0, Y_TEMP, LOR_WIDTH
        Dim TAIL, E_TEMP, E_1, tail_x
        Dim xrf_e_bi(12), xrf_abs_bi(12), lor_w_bi(12)
        Dim npks As Integer
        '
        '        Bi xrf energies
        xrf_e_bi(1) = 77.107    '  Ka1
        xrf_e_bi(2) = 74.815    '  Ka2
        xrf_e_bi(3) = 74.138    '  Ka3
        xrf_e_bi(4) = 87.349    '  kb1
        xrf_e_bi(5) = 89.784    '  kb2
        xrf_e_bi(6) = 86.83    '  Kb3
        xrf_e_bi(7) = 90.074    '  kb4
        xrf_e_bi(8) = 87.892    '  kb5
        xrf_e_bi(9) = 90.421    '  KO23
        xrf_e_bi(10) = 90.522   '  KP23

        '         Bi xrf yields
        xrf_abs_bi(1) = 46.2
        xrf_abs_bi(2) = 27.7
        xrf_abs_bi(3) = 0.0474
        xrf_abs_bi(4) = 10.7
        xrf_abs_bi(5) = 3.93
        xrf_abs_bi(6) = 5.59
        xrf_abs_bi(7) = 0.09
        xrf_abs_bi(8) = 0.321
        xrf_abs_bi(9) = 0.73
        xrf_abs_bi(10) = 0.031

        '        Bi lorenztian widths
        lor_w_bi(1) = 30.0 / 1000
        lor_w_bi(2) = 30.0 / 1000
        lor_w_bi(3) = 30.0 / 1000
        lor_w_bi(4) = 30.0 / 1000
        lor_w_bi(5) = 30.0 / 1000
        lor_w_bi(6) = 30.0 / 1000
        lor_w_bi(7) = 30.0 / 1000
        lor_w_bi(8) = 30.0 / 1000
        lor_w_bi(9) = 30.0 / 1000
        lor_w_bi(10) = 30.0 / 1000
        '
        Bi_VOIGT = 0.0
        Y_TEMP = 0.0
        TAIL = 0.0
        npks = 10
        '
        For j = 1 To npks
            gauss_width_0 = (xrf_e_bi(j) / 88.0) ^ 0.5 * Gauss_width
            E_TEMP = xrf_e_bi(j)
            '	
            LOR_WIDTH = lor_w_bi(j)
            Y_TEMP = xrf_abs_bi(j) * czosnyka(energy, E_TEMP, LOR_WIDTH, gauss_width_0)
            '
            TAIL = 0
            For i = 16 To 16
                E_1 = xrf_e_bi(j) + (16 - i) * deltae
                TAIL = TAIL + tail_x(E_1, energy, tails, gauss_width_0)

            Next i
            TAIL = xrf_abs_bi(j) * TAIL
            '
            Bi_VOIGT = Bi_VOIGT + Y_TEMP    '  + tail
        Next j
    End Function

    Function W_VOIGT(energy, Gauss_width, tails, deltae)
        '
        '   Creates tungsten X-ray peaks
        '	The relative peak intensities will not be very accurate because the source distribution/attenuation in not defined.
        '	Estimation of the true coincidence summing peaks will be further complicated by the source distribution
        '
        Dim E0(12), I0(12), lor_w(12)
        Dim gauss_width_0, Y_TEMP, LOR_WIDTH
        Dim TAIL, E_TEMP, E_1

        Dim npks As Integer
        '
        E0 = {59.318, 57.981, 57.425, 67.244, 69.067, 66.95, 69.273, 67.685, 69.484, 68.82, 80.0, 80.1}
        lor_w = {0.0432, 0.0374, 0.043, 0.0486, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05}
        I0 = {0.57, 0.25818, 0.00018, 0.1785625, 0.0665375, 0.0916875, 0.0011125, 0.0042375, 0.009625, 0.0773, 0.0, 0.0}

        W_VOIGT = 0.0
        Y_TEMP = 0.0
        TAIL = 0.0
        npks = 10
        '
        For j = 0 To npks - 1
            gauss_width_0 = (E0(j) / 88.0) ^ 0.5 * Gauss_width
            E_TEMP = E0(j)
            '	
            LOR_WIDTH = lor_w(j)
            Y_TEMP = I0(j) * czosnyka(energy, E_TEMP, LOR_WIDTH, gauss_width_0)
            '
            TAIL = 0
            For i = 16 To 16
                E_1 = E0(j) + (16 - i) * deltae
                TAIL = TAIL + tail_x(E_1, energy, tails, gauss_width_0)
            Next i

            TAIL = I0(j) * TAIL
            '
            W_VOIGT = W_VOIGT + Y_TEMP    '  + tail
            '
        Next j
        '
    End Function

    Function czosnyka(energy, e0, lor_width, gauss_sig)
        '
        Dim ka(3), la(3), ma(3), na(3)
        Dim A, V, V_SUM, X, Y, Z, TOT
        '
        ka(1) = 0.0
        la(1) = 0.0
        ma(1) = 1.32272
        na(1) = 0.081905
        '
        ka(2) = 1.09148
        la(2) = 0.090227
        ma(2) = 1.29081
        na(2) = 0.0093116
        '
        ka(3) = 2.30556
        la(3) = 0.0035776
        ma(3) = 1.17417
        na(3) = -0.0116099
        '
        A = lor_width / 2 / gauss_sig
        V = (energy - e0) / gauss_sig
        V_SUM = 0.0
        '
        For i = 1 To 3
            X = (ka(i) * la(i) + na(i) * (A + ma(i)))
            Y = (ka(i) ^ 2 + (A + ma(i)) ^ 2 + V ^ 2)
            Z = (ka(i) ^ 2 + (A + ma(i)) ^ 2 + V ^ 2) ^ 2 - 4 * ka(i) ^ 2 * V ^ 2
            TOT = (X * Y - 2 * ka(i) * la(i) * V ^ 2) / Z
            V_SUM = V_SUM + TOT
        Next i
        '
        czosnyka = 2 * lor_width * V_SUM / A / gauss_sig ^ 2

    End Function
    '

    ' This function is a replacement for the Microsoft Excel Worksheet function NORMSINV.
    ' It uses the algorithm of Peter J. Acklam to compute the inverse normal cumulative
    ' distribution. 
    ' Adapted to VB by Christian d'Heureuse
    Public Function NormSInv(ByVal p As Double) As Double
        Const a1 = -39.6968302866538, a2 = 220.946098424521, a3 = -275.928510446969
        Const a4 = 138.357751867269, a5 = -30.6647980661472, a6 = 2.50662827745924
        Const b1 = -54.4760987982241, b2 = 161.585836858041, b3 = -155.698979859887
        Const b4 = 66.8013118877197, b5 = -13.2806815528857, c1 = -0.00778489400243029
        Const c2 = -0.322396458041136, c3 = -2.40075827716184, c4 = -2.54973253934373
        Const c5 = 4.37466414146497, c6 = 2.93816398269878, d1 = 0.00778469570904146
        Const d2 = 0.32246712907004, d3 = 2.445134137143, d4 = 3.75440866190742
        Const p_low = 0.02425, p_high = 1 - p_low
        Dim q As Double, r As Double
        If p < 0 Or p > 1 Then
            'Err.Raise vbObjectError, , "NormSInv: Argument out of range."
        ElseIf p < p_low Then
            q = Sqrt(-2 * Log(p))
            NormSInv = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) /
         ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        ElseIf p <= p_high Then
            q = p - 0.5 : r = q * q
            NormSInv = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q /
         (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1)
        Else
            q = Sqrt(-2 * Log(1 - p))
            NormSInv = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) /
         ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        End If

    End Function


    Function erf(y)

        ' Claude Leibovici (https://math.stackexchange.com/users/82404/claude-leibovici), How to accurately calculate the error function $\operatorname{erf}(x)$ with a computer?, URL (version: 2020-10-31): https://math.stackexchange.com/q/3889135

        Dim pi, par_1, par_2, par_3, par_4, P2, Q2

        pi = 3.14159265359

        par_1 = 0.125865469596
        par_2 = 0.013157551928
        par_3 = 0.155912363898
        par_4 = 0.015177840198

        P2 = par_1 * y ^ 2 + par_2 * y ^ 4
        Q2 = par_3 * y ^ 2 + par_4 * y ^ 4

        erf = (1 - Exp(-4 / pi * (1 + P2) / (1 + Q2) * y ^ 2))

        erf = Sign(y) * erf

    End Function

    Function erfc(y)

        ' Claude Leibovici (https://math.stackexchange.com/users/82404/claude-leibovici), How to accurately calculate the error function $\operatorname{erf}(x)$ with a computer?, URL (version: 2020-10-31): https://math.stackexchange.com/q/3889135

        Dim pi, par_1, par_2, par_3, par_4, P2, Q2

        pi = 3.14159265359

        par_1 = 0.125865469596
        par_2 = 0.013157551928
        par_3 = 0.155912363898
        par_4 = 0.015177840198

        P2 = par_1 * y ^ 2 + par_2 * y ^ 4
        Q2 = par_3 * y ^ 2 + par_4 * y ^ 4

        erfc = (1 - Exp(-4 / pi * (1 + P2) / (1 + Q2) * y ^ 2)) ^ 0.5

        erfc = 1 - Sign(y) * erfc

    End Function

    Function attn_fact_W_PE2(energy, par_1, par_2)
        '	attn coeff Tungsten Photoelectric
        Dim aL1, aL2, aH1, aH2, edge
        '
        '	change over point
        edge = 69.53
        '
        aL1 = 232603
        aL2 = -2.734
        aH1 = 938475
        aH2 = -2.679
        '
        attn_fact_W_PE2 = 1
        attn_fact_W_PE2 = aL1 * energy ^ aL2
        If (energy > edge) Then attn_fact_W_PE2 = aH1 * energy ^ aH2
        '
        attn_fact_W_PE2 = (par_1 + par_2 * energy) * attn_fact_W_PE2
        '
    End Function
    '
    '
    Function scat_fact_7(energy, concentration1)
        '	 returns the macroscopic coherent scattering coefficient for the solution

        Dim cont_U, cont_PU, cont_matrix, pi
        pi = 3.1415926535898

        cont_U = 460.698 / (energy ^ 1.6369)
        cont_PU = 380.4073 / (energy ^ 1.5773)
        cont_matrix = 32.229 / (energy ^ 1.892)

        scat_fact_7 = cont_U * concentration1(1) + cont_PU * concentration1(2) + cont_matrix * concentration1(6)

    End Function
    '
    '
    Function attn_fact_W(energy)
        '	attn coeff Tungsten P	
        Dim aL1, aL2, aH1, aH2, edge
        edge = 69.53
        '
        aL1 = 160974
        aL2 = -2.608
        aH1 = 493198
        aH2 = -2.523
        '
        attn_fact_W = 1
        attn_fact_W = aL1 * energy ^ aL2
        If (energy > edge) Then attn_fact_W = aH1 * energy ^ aH2
        '
    End Function
    '
    '
    Function attn_fact_ss_7(energy, i_co)
        '
        '	dim amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
        '	dim sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10)
        '
        '	attn coeff for SS
        '	parameters read in from attenuation factors data file
        '
        Dim edge, amu_ss_L1, amu_ss_L2, amu_ss_L3, amu_ss_L4, amu_ss_L5
        Dim amu_ss_h1, amu_ss_h2, amu_ss_h3, amu_ss_h4, amu_ss_h5
        Dim al_e, amu_above, amu_below
        edge = misc_edges(1)
        '	attn parameters with coherent scattering

        Amu_ss_L1 = misc_lib(1, 1)
        Amu_ss_L2 = misc_lib(1, 2)
        Amu_ss_L3 = misc_lib(1, 3)
        Amu_ss_L4 = misc_lib(1, 4)
        Amu_ss_L5 = misc_lib(1, 5)

        Amu_ss_H1 = misc_lib(1, 6)
        Amu_ss_H2 = misc_lib(1, 7)
        Amu_ss_H3 = misc_lib(1, 8)
        Amu_ss_H4 = misc_lib(1, 9)
        Amu_ss_H5 = misc_lib(1, 10)
        '
100:

        al_e = Log(energy)

        Amu_below = Exp(Amu_ss_L5 * al_e ^ 4 + Amu_ss_L4 * al_e ^ 3 + Amu_ss_L3 * al_e ^ 2 + Amu_ss_L2 * al_e + Amu_ss_L1)
        Amu_above = Exp(Amu_ss_H5 * al_e ^ 4 + Amu_ss_H4 * al_e ^ 3 + Amu_ss_H3 * al_e ^ 2 + Amu_ss_H2 * al_e + Amu_ss_H1)
        '
        attn_fact_ss_7 = Amu_below
        If (energy > edge) Then attn_fact_ss_7 = Amu_above
        '
    End Function

    Function attn_fact_Ge_7(energy, i_co)

        Dim amu_ge_L1, amu_ge_L2, amu_ge_L3
        Dim amu_ge_H1, amu_ge_H2, amu_ge_H3
        Dim al_e, Amu_below, Amu_above, alog_e, edge
        '
        '	attn coeff for Ge
        '
        edge = 11.1
        '	attn parameters with coherent scattering
        amu_ge_L1 = -0.3273
        amu_ge_L2 = -2.2106
        amu_ge_L3 = 4.1075
        amu_ge_H1 = 0.4545
        amu_ge_H2 = -4.105
        amu_ge_H3 = 6.1549
        '
        If (i_co = 1) Then GoTo 100
        '
        '	attn parameters without coherent scattering
        amu_ge_L1 = -0.3674
        amu_ge_L2 = -2.1865
        amu_ge_L3 = 4.1042
        amu_ge_H1 = 0.4528
        amu_ge_H2 = -4.1419
        amu_ge_H3 = 6.1966
        '
100:
        '
        alog_e = Log10(energy)
        Amu_below = 10 ^ (amu_ge_L1 * alog_e ^ 2 + amu_ge_L2 * alog_e + amu_ge_L3)
        Amu_above = 10 ^ (amu_ge_H1 * alog_e ^ 2 + amu_ge_H2 * alog_e + amu_ge_H3)
        '
        attn_fact_Ge_7 = Amu_below
        '
        If (energy.GT.edge) Then attn_fact_Ge_7 = amu_above
'
    End Function
    '
    '	-----------------------------------------------------
    '
    Function attn_fact_cd_7(energy, i_co)
        '
        '	attn coeff for cadmium
        '	
        '	attn parameters with coherent scattering
        '
        Dim amu_cd_L1, amu_cd_L2, amu_cd_L3, amu_cd_L4, amu_cd_L5
        Dim amu_cd_h1, amu_cd_h2, amu_cd_h3, amu_cd_h4, amu_cd_h5
        Dim amu_below, amu_above, edge, al_e2, alog_e
        al_e2 = Log(energy)
        edge = misc_edges(3)

        amu_cd_L1 = misc_lib(3, 1)
        amu_cd_L2 = misc_lib(3, 2)
        amu_cd_L3 = misc_lib(3, 3)
        amu_cd_L4 = misc_lib(3, 4)
        amu_cd_L5 = misc_lib(3, 5)

        amu_cd_h1 = misc_lib(3, 6)
        amu_cd_h2 = misc_lib(3, 7)
        amu_cd_h3 = misc_lib(3, 8)
        amu_cd_h4 = misc_lib(3, 9)
        amu_cd_h5 = misc_lib(3, 10)

        amu_below = Exp(amu_cd_L5 * al_e2 ^ 4 + amu_cd_L4 * al_e2 ^ 3 + amu_cd_L3 * al_e2 ^ 2 + amu_cd_L2 * al_e2 + amu_cd_L1)
        amu_above = Exp(amu_cd_h5 * al_e2 ^ 4 + amu_cd_h4 * al_e2 ^ 3 + amu_cd_h3 * al_e2 ^ 2 + amu_cd_h2 * al_e2 + amu_cd_h1)
        '
        attn_fact_cd_7 = amu_below
        If (energy > edge) Then attn_fact_cd_7 = amu_above

        If (i_co = 1) Then GoTo 100
        '
        '	attn parameters without coherent scattering
        '
        edge = 102
        amu_cd_L1 = 0.03
        amu_cd_L2 = -2.8373
        amu_cd_L3 = 5.7089
        amu_cd_h1 = 1.2242
        amu_cd_h2 = -7.6469
        amu_cd_h3 = 10.555
        '
        alog_e = Log10(energy)
        amu_below = 10 ^ (amu_cd_L1 * alog_e ^ 2 + amu_cd_L2 * alog_e + amu_cd_L3)
        amu_above = 10 ^ (amu_cd_h1 * alog_e ^ 2 + amu_cd_h2 * alog_e + amu_cd_h3)
        '
        attn_fact_cd_7 = amu_below
        If (energy.GT.edge) Then attn_fact_cd_7 = amu_above

100:
'
    End Function
    '
    '	-----------------------------------------------------
    '
    Function attn_fact_poly_co(energy)
        Dim edge, aL1, aL2, aH1, aH2
        '
        '	attn coeff for polyethylene
        '
        edge = 30
        '
        aL1 = 372.99
        aL2 = -2.337
        aH1 = 0.7068
        aH2 = -0.306
        '
        attn_fact_poly_co = attn_fact_vial(energy, 1)
        '
    End Function
    '
    '	-----------------------------------------------------
    '
    Function attn_fact_vial(energy, vial_type)
        Dim alg_e
        Dim amu_L0, amu_L1, amu_L2, amu_L3
        '
        '	attn coeff for polyethylene
        '
        amu_L0 = cmu_lib(1, 1)
        amu_L1 = cmu_lib(1, 2)
        amu_L2 = cmu_lib(1, 3)
        amu_L3 = cmu_lib(1, 4)

        If (vial_type <> 1) Then GoTo 10

        amu_L0 = cmu_lib(2, 1)
        amu_L1 = cmu_lib(2, 2)
        amu_L2 = cmu_lib(2, 3)
        amu_L3 = cmu_lib(2, 4)
        '
10:
        alg_e = Log(energy)
        attn_fact_vial = Exp(amu_L3 * alg_e ^ 3 + amu_L2 * alg_e ^ 2 + amu_L1 * alg_e + amu_L0)
        '
    End Function
    '
    '	-----------------------------------------------------
    '
    Function attn_fact_be_co(energy)
        '
        Dim aH1, aH2
        '	attn coeff for Beryllium
        '
        aH1 = misc_lib(2, 1)
        aH2 = misc_lib(2, 2)
        '
        attn_fact_be_co = aH1 * energy ^ aH2
        '
    End Function
    '


    Function g_smooth(energy, deltae, gauss_width, X)
        '
        '	Simple guassian smoothing of a spectral segment, X
        '
        Dim y, E0
        '
        y = 0.0
        For i = 1 To 101
            E0 = energy + (51 - i) * deltae
            y = y + X(i) * gaussian(energy, E0, 1.0, gauss_width, deltae)
        Next i
        '
        g_smooth = y
        '
    End Function

    Function E_KNR(energy, angle)
        '	Returns inverse Klein-Nishina backscatter energy
        Dim e_m, Pi
        e_m = 510.99906
        Pi = 3.1415926535898
        E_KNR = energy / (1.0 - (energy / e_m) * (1.0 - Cos(angle / 180.0 * Pi)))
    End Function
    '
    Function E_KN(energy, angle)
        '	Returns Klein-Nishina backscatter energy
        Dim e_m, cos_th, pi
        e_m = 510.99906
        PI = 3.1415926535898
        cos_th = Cos(angle / 180.0 * PI)
        E_KN = energy / (1.0 + (energy / e_m) * (1 - cos_th))
    End Function
    Function brem_bkg3(energy, bkg_pars)

        '   Describes multiple scattering contribution to the Brems. background
        '
        Dim scal1, scal2, bkg
        scal1 = energy - bkg_pars(1)
        scal2 = energy - bkg_pars(2)
        '
        bkg = 0
        If ((scal1 / bkg_pars(3)) < 200) Then bkg = bkg_pars(4) / Cosh(scal1 / bkg_pars(3))
        If ((scal2 / bkg_pars(3)) < 200) Then bkg = bkg + bkg_pars(5) / Cosh(scal2 / bkg_pars(3))
        '
        brem_bkg3 = bkg
        '
    End Function

    Function brem_bkg4(energy, bkg_pars)
        '   Test function to see if a voigt contribution to the brems background is necessary
        '   Describes multiple scattering contribution to the Brems. background
        '
        'bkg_pars(1) = dabs(a(32))'spline ref energy 1
        'bkg_pars(2) = dabs(a(33))'spline ref energy 2
        'bkg_pars(4) = a(34)'spline par A1 -> intensity 
        'bkg_pars(5) = a(35)'spline par A2 -> intensity
        'bkg_pars(3) = dabs(a(36))'spline decay constant

        'bkg_pars(6) = dabs(a(xx))'voigt component energy
        'bkg_pars(7) = dabs(a(xx))'voigt component intensity
        'bkg_pars(8) = xfit_consts(21)'Voigt component width (keV)
        'bkg_pars(9) = a(17)'HPGE FWHM

        Dim scal1, scal2, bkg
        scal1 = energy - bkg_pars(1)
        scal2 = energy - bkg_pars(2)
        '
        bkg = 0
        If ((scal1 / bkg_pars(3)) < 200) Then bkg = bkg_pars(4) / Cosh(scal1 / bkg_pars(3))
        '    If ((scal2 / bkg_pars(3)) < 200) Then bkg = bkg + bkg_pars(5) / Cosh(scal2 / bkg_pars(3))
        ' bkg = bkg + bkg_pars(5) * czosnyka(energy, bkg_pars(2), 0.04, 0.191) * HPGE_eff(energy)
        '
        brem_bkg4 = bkg
        '
    End Function


    Sub random_spec(spectrum, n_chans)

        '   estimates random spectrum contribution from measured spectrum

        Dim nlow, nhigh, L_norm_limit, h_norm_limit, offset_r, ilive, j As Integer
        Dim tail_area, tail_decay, sum_width, gate
        Dim deltae, gauss_width, rand_norm, spec_norm, rand_mult, e_sm
        Dim r_short(4100), smooth_spec(4100)
        Dim energy, spec_tail_int, spec_tail_decay, temp, random_norm

        '	requires function spec_fold2
        '	requires function g_smooth2

        '	THESE PARAMETERS TO GO INTO A SETUP FILE		
        nlow = Int(50 * n_chans / 2048)         ' Adjusted for file size
        nhigh = Int(2000 * n_chans / 2048)          ' Adjusted for file size
        '
        L_norm_limit = Const_val(12)            ' normalization window lower bound
        h_norm_limit = Const_val(13)            ' normalization window upper bound
        rand_mult = Const_val(14)           ' holdover from eariler version, xfit_consts(14) should =1
        tail_area = 0.0
        tail_decay = 1.0
        sum_width = 0.5
        ilive = live_time
        gate = 0.00000078813904
        offset_r = Const_val(15)
        deltae = results_arr(16)
        gauss_width = results_arr(17)
        '
        For I = 1 To n_chans
            r_short(I) = 0.0
            rand_temp(I) = 0.0
            smooth_spec(I) = 0.0
        Next I
        '
        For I = 2 To Int(2040 * n_chans / 2048)
            rand_temp(I) = spec_fold2(nlow, nhigh, I, gate, ilive, spectrum)
        Next I
        '
        For I = 75 To Int(2020 * n_chans / 2048)
            e_sm = deltae * I
            smooth_spec(I + offset_r) = g_smooth2(e_sm, deltae, 2.0 * gauss_width, I, rand_temp)
        Next I

        rand_norm = 0.0
        spec_norm = 0.0

        For ij = 1 To Int(2040 * n_chans / 2048)
            For i = 1 To 50
                j = ij + i - 1
                r_short(i) = 0
                If (j < n_chans) Then r_short(i) = smooth_spec(j)
            Next i


            spec_tail_int = 1.0
            spec_tail_decay = 0.2
            energy = 0.0 + deltae * ij
            temp = smooth_spec(ij)  '  +  +spec_tail(energy, spec_tail_int, spec_tail_decay, delta_e, r_short)  ' <-----
            rand_spec(ij) = temp
        Next ij

        For i = L_norm_limit To h_norm_limit
            rand_norm = rand_norm + rand_spec(i)
            spec_norm = spec_norm + raw_data(i)
        Next i
        '
        random_norm = spec_norm / rand_norm * rand_mult
        '
        For i = 1 To n_chans                '   
            rand_temp(i) = rand_spec(i) * random_norm       '   The code has been modified to subtract the background each iteration
        Next i

        Return
    End Sub

    Function spec_fold2(nlow, nhigh, npt, gate, live_time, XRAW)
        '
        '	computes the random coincidence summing function
        '
        '	RAW_DATA contains the raw spectrum in counts
        '	nlow, nhigh are the start and stop channels over which the convolution is performed
        '	npt is the channel number of the resultant spectrum
        '	gate is the coincidence gate time in seconds (~0.5 E-6)
        '	Note: This is a first order correction. However, for summed energies of less than
        '	150 kev the second order terms should be negligible.
        '
        '
        Dim temp, alive_time
        '
        alive_time = live_time
        temp = 0.0

        For i = nlow To nhigh + 1
            If (i > npt) Then GoTo 20
            If ((npt - i) < 1) Then GoTo 10
            temp = temp + XRAW(i) * XRAW(npt - i) * gate / alive_time ^ 2
10:     Next i
20:
        '
        spec_fold2 = temp
    End Function

    Function g_smooth2(energy, deltae, gauss_width, j, X)
        '
        Dim y, E0, tail
        Dim i As Integer

        y = 0.0
        For i = 1 To 71
            E0 = energy + (35 - i) * deltae

            If (i + j - 35 <= 4100) Then y = y + X(i + j - 35) * gaussian(energy, E0, 1.0, gauss_width, deltae)

        Next i

        tail = 0.0

        g_smooth2 = y * deltae + tail
    End Function

End Module
