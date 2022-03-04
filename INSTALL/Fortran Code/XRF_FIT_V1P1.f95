! XRF Fitting program January 07, 2022
! 
!	Commandline Usage:  xrf_fit_v1p0  number_channels parameter_file_name  
!
!		number_channels = integer number of channels in spectrum
!		parameter_file_name => xfit_parms_##.txt where ## is the spectrum number
!
!       Added passive background subtraction 							12/19/2020
!	Added offset_r (# channels for random correction offset) as xfit_consts(15)   -  	12/29/2020
!	Increased XRF Peak array to include upto 10 elements with 15 lines per element (10, 15)   -   1/17/2021
!             fit still only uses 1st 5 elements for now
!	Added comments- 2/19/2021
!	Corrected double definition of vial diamater (now only used xfit_consts(6) - 		2/19/2021
!	Extended XFIT_consts array to 48 from 36 - 2/27/2021
!	Added amu_read subroutine to read in attenuation parameters from a file - 3/19/2021
!	Added parameter n_it (number of sub intevals per channel to the contstants file (previously fixed at n_it=10)  - 3/19/2021
!	Modified to accommodate 4k spectra - 							3/28/2021
!	Replaced attnuation functions for the sample, vial, ss, be and cd for consistency with MEKED attenuation functions - 5/1/2021
!	Corrected calculation for weig(i) in curfit						5/20/2021
!	Extended to 42 parameters								6/19/2021
!	Added high side tailing to peaks							6/19/2021
!	Added W X-ray peaks as fit parameter							7/03/2021
!	Extended XFIT_consts array to 60 from 48 to accommodate standalone parameters		7/10/2021
!       Appended XFIT_consts to output report file						7/12/2021
!       Read in acquistion date and time from spec_fit file and store in results file		8/12/2021
!       Changed output files to add passive spectrum back in after completion of fit		8/12/2021
!	Move peak ratio correction subroutine into FUNCTN2					8/12/2021
!	deleted obsolete cal_x_ratio and amu_value subroutines					9/04/2021
!	Temporarily allow fit of ss thickness using par(41)					9/30/2021
!	Added Pb X-ray contribution (coming from detector shield)				10/1/2021
!	Updated fission product energies							10/12/2021
!	Extended XFIT_consts array to 70 from 60 to accommodate HPGe efficiency parameters	10/25/2021
!	Added number of iterations performed to results file					12/21/2021
!	corrected fission product peaks to use spectrum FWHM 					12/29/2021
!	modified Cd step tail function								12/30/2021
!	Brem_bkg_new modified to ensure result is greater than zero				12/30/2021
!	Increased constants array from xfit_consts 70 to 90 values				01/05/2022
!	Initial Release - renamed file as xrf_fit_v1p0						01/07/2022
!	Read fission product data from file							02/01/2022
!
program MEXRF_FIT
!
	DOUBLE PRECISION zdat(4100), WEIGHT(4100), RAND_SPEC(4100)
	DOUBLE PRECISION smooth_spec(4100), r_short(4100), gauss_width
	DOUBLE PRECISION YFIT(4100), YDAT(4100), ERR2(4100), BKG(4100)
	DOUBLE PRECISION passive_data(4100), Passive_error1(4100), passive_LT, passive_norm
	DOUBLE PRECISION array(42, 42), BDUM(42), aa_temp(42)
	DOUBLE PRECISION deltaa(42), a1(42), AA(42), xfit_parms(42)
	DOUBLE PRECISION SIGMAA(42), xfit_errors(42), COVAR_ARRAY(42,42)
	DOUBLE PRECISION COVAR_TEMP1(42),covar_out1(42,42)
	DOUBLE PRECISION COVAR_TEMP2(42),covar_out2(42,42)
	DOUBLE PRECISION x_adjust(50,3), xrf_bkgnd(14)

	DOUBLE PRECISION vial_dia, sample_temp, temperature, u_enrich,pu_weight
!
	DOUBLE PRECISION TAIL_AREA, TAIL_DECAY, FLAMDA, GATE
	DOUBLE PRECISION sum1, sum_width, spec_fold2, x_chan, z, e_sm
	DOUBLE PRECISION cd_init, raw_start, cd_bkg, d_vial, d_xrf, d_col
 	DOUBLE PRECISION ENER_SCAT, ENER_ESC, ENERGY, b_scat, chi1, chi2
	DOUBLE PRECISION chisq_init, chisqr, D_E, deltae, dltchi, e0, e1
	DOUBLE PRECISION e_0, e_l1, e_l2, e_zero, i_first
	DOUBLE PRECISION t_real, rand_count, rand_scale, raw_count
	DOUBLE PRECISION STEP_SIZE, ang_min
	DOUBLE PRECISION temp_array(101), tails(4), e_escape(2), i_escape(2)
	DOUBLE PRECISION Y, temp, rand_norm, spec_norm, random_norm, rand_mult
!
	DOUBLE PRECISION back_scat, functn2, FCHISQ1, tail_x, g_smooth2, spec_tail
	DOUBLE PRECISION p_t_real, p_vial_dia, p_sample_temp, p_u_enrich,p_pu_weight
	DOUBLE PRECISION spec_tail_decay, spec_tail_int
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!
	INTEGER i_xfit(42), idex_a(42), free_params(42), NTERMS
	INTEGER ichn, i_scat_hi, i_scat_lo, ichn_start, ichn_stop
	INTEGER idone, ichannels, ij, ispit, itmax, j, k, l1, ijj
	INTEGER mtrms, mxp, i_cd_max, n_cd_chan
	INTEGER ifil, lcmd, namflag, chan_num, chan_num_in, seq_num
	INTEGER nchannels, nchan, nchn, niter, nmax, nmax2, nmin2, nt_max
	INTEGER num_free, offset_r, L_norm_limit,  H_norm_limit
	INTEGER NPTS, num_NFREE, Nmin, MODE
	INTEGER NLOW, NHIGH, I, ILIVE, p_ilive
	INTEGER re_i
!
	REAL E_LO1,E_LO2,E_HI1,E_HI2
!
	character string1*8
	character FILNAM1*70,SPECTEMP*70, newparname*60, Sample_ID*60,PASSIVE_SPC*60
	character FILNAM2*60
    	character inputfilename*60,  outfilname*100, specout2*100, results_rpt*100
	CHARACTER FILNAME*70, SPECOUT*70, PARMSOUT*70
	character xray_lib_name*100
	character fil_temp*60
	CHARACTER arg*60 ,  seq_char*60
	CHARACTER(8) acq_date_mm, acq_date_dd, acq_date_yy
	CHARACTER(10) acq_time

!
	character passfilename*70, dummyname*60, passive_ID*60
!
	DOUBLE PRECISION xfit_consts(90),XDAT(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100), ERROR1(4100)
	DOUBLE PRECISION concentrate(6), xrf_bkg(14), eff_pars(7)
	DOUBLE PRECISION xrf_e(10,15), xrf_rel(10,15), lor_w(10,15) 
	Double Precision norm_x(10), xrf_abs(10,15)
	INTEGER i_conc(7), i_attn(7), element_peaks(7)
!
	DOUBLE PRECISION lor_widths(10), act_norm_factor(10)
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2)
	
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges

	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/effdat/eff_pars
 	COMMON/XRFDAT2/RAW_DATA, RAND_TEMP, ERROR1
	COMMON/XRFDAT3/xrf_bkg, concentrate
	COMMON/XRFDAT4/xrf_e, xrf_rel, lor_w, element_peaks	
	COMMON/XRFDAT5/xrf_abs
	COMMON/XRFDAT6/YDAT, WEIGHT
!
!	----------------------------------------------------
!	Free Parameter array
!	----------------------------------------------------
!
!        Xfit_parms(1) = U intensity 
!        Xfit_parms(2) = Np intensity 
!        Xfit_parms(3) = Pu intensity 
!        Xfit_parms(4) = Am intensity 
!        Xfit_parms(5) = Cm intensity 
!        Xfit_parms(6) = Eu154 intensity 
!        Xfit_parms(7) = Eu155 intensity 
!        Xfit_parms(8) = Ce144 intensity 
!        Xfit_parms(9) = [Uranium ]  (g/cc) 
!        Xfit_parms(10) = [Nepturium ]  (g/cc) 
!        Xfit_parms(11) = [Plutonium]  (g/cc) 
!        Xfit_parms(12) = [Americium ]  (g/cc) 
!        Xfit_parms(13) = [Curium ]  (g/cc) 
!        Xfit_parms(14) = [Matrix ]  (g/cc) 
!        Xfit_parms(15) = Energy offset (keV)
!        Xfit_parms(16) = Energy slope (keV)
!        Xfit_parms(17) = Gaussian Width (keV)
!        Xfit_parms(18) = Tail Intensity
!        Xfit_parms(19) = Tail Decay Constant
!        Xfit_parms(20) = Step Background
!        Xfit_parms(21) = Ge Escape Intensity
!        Xfit_parms(22) = Cd Peak Width (keV)
!        Xfit_parms(23) = Cd Peak Energy (keV)
!        Xfit_parms(24) = Cd Peak Intensity
!        Xfit_parms(25) = Random Background factor
!        Xfit_parms(26) = Linear Background Offset
!        Xfit_parms(27) = Linear Background Slope
!        Xfit_parms(28) = HV Endpoint Energy (kV
!        Xfit_parms(29) = Shaping Parameter
!        Xfit_parms(30) = Backscatter Intensity
!        Xfit_parms(31) = Elastic Scatter Faction 
!        Xfit_parms(32) = E_ref_1 (keV)
!        Xfit_parms(33) = E_ref 2 (keV )
!        Xfit_parms(34) = Spline A1
!        Xfit_parms(35) = Spline A2
!        Xfit_parms(36) = Spline Tau
!        Xfit_parms(37) = intensity high side peak tail
!        Xfit_parms(38) = decay Spline Tau
!        Xfit_parms(39) = reference peak step background intensity
!        Xfit_parms(40) = W peak intensity
!        Xfit_parms(41) = Stainless steel wall thickness (cm) - temp over ride of Xfit_consts(32)
!        Xfit_parms(42) = Pb Peak intensity
!
!	----------------------------------------------------
!	System constants array
!	----------------------------------------------------
!
!	Xfit_consts(1) =  number of U characteristic X-ray peaks
!	Xfit_consts(2) =  number of Pu characteristic X-ray peaks
!	Xfit_consts(3) =  number of Np characteristic X-ray peaks
!	Xfit_consts(4) =  number of Cm characteristic X-ray peaks
!	Xfit_consts(5) =  number of Am characteristic X-ray peaks
!	Xfit_consts(6) =  Inner diameter XRF sample vial (cm) --- assumed to be PE
!	Xfit_consts(7) =  Depth of interaction region (cm)
!	Xfit_consts(8) =  not used
!	Xfit_consts(9) =  Lower energy of fit region (keV)
!	Xfit_consts(10) = Upper energy of fit region (keV)
!	Xfit_consts(11) = Maximum number of iterations to be performed
!	Xfit_consts(12) = Lower channel number for random correction normalizatin region
!	Xfit_consts(13) = Upper channel number for random correction normalizatin region
!	Xfit_consts(14) = Random correction multiplier (possibly redundant with xfit_parms(25)
!	Xfit_consts(15) = Random correction number of channels to offest
!	Xfit_consts(16) = Kappa : transmission factor correction --> see PANDA
!	Xfit_consts(17) = Lower energy of region to exclude from fit (kev)
!	Xfit_consts(18) = Upper energy of region to exclude from fit (kev)
!	Xfit_consts(19) = lower energy cut off for reference peak tail (keV)
!	Xfit_consts(20) = g_width2 elastic scatter bremstrahlung smoothing parameter (keV)
!	Xfit_consts(21) = not used
!	Xfit_consts(22) = not used
!	Xfit_consts(23) = second order bremmstrahlung shaping parameter, y --> normally set to 0
!	Xfit_consts(24) = X-ray tube beam incident angle (degrees)
!	Xfit_consts(25) = X-ray tube x-ray take-off angle (degrees)
!	Xfit_consts(26) = g_width inelastic backscatter bremstrahlung smoothing parameter
!	Xfit_consts(27) = not used
!	Xfit_consts(28) = XRF detector backwards angle (degrees)
!	Xfit_consts(29) = Cd foil thickess (cm)  -- between X-ray tube and transfer tube
!	Xfit_consts(30) = Sample vial single wall thickness (cm)
!	Xfit_consts(31) = convergence criteria
!	Xfit_consts(32) = total stainless steel wall thickness between x-ray generator and sample
!	Xfit_consts(33) = XRF collimator inner diameter (cm)
!	Xfit_consts(34) = Ask for fit continuation (1 =  yes)
!	Xfit_consts(35) = Subtract passive spectrum (1 = yes)
!	Xfit_consts(36) = Fit increment (multiplier within curfit for calculation of derivative)
!	Xfit_consts(37) = Reference Temperature (degrees C) [pass through parameter - not used in this routine]
!	Xfit_consts(38) = Temperature correction coefficient (1/degrees C) [pass through parameter - not used in this routine]
!	Xfit_consts(39) = Temperature correction coefficient uncertainty (1/degrees C) [pass through parameter - not used in this routine]
!	Xfit_consts(40) = Sub intervals per channel - integrate each count per channel in n sub increments to correct for large delta_e
!	Xfit_consts(41) = not used
!	Xfit_consts(42) = not used
!	Xfit_consts(43 - > 60) = standalone concentration calibration parameters - not used in this code
!	Xfit_consts(61 - > 67) = HPGe detector efficiency, includes attenuation of end cap
!	Xfit_consts(68 - > 90) = not used in this executable
!
!	----------------------------------------------------
!
	write(*,*) "MEXRF Fit (Build v1.0 January 7, 2022)"
!
!
!	*********************************
!	Extract information from command line
!	*********************************
!
!     get max number of channels from the command line
!
	mxp=2048					  ! default 2k spectrum
!
  	CALL get_command_argument(1, arg)
!    	print *,' arg= ', arg
	READ(arg,*)chan_num_in               		  ! converts from character to a number
	if(chan_num_in.EQ.4096) mxp=4096

	CALL get_command_argument(2, arg)		  ! file name for input data parameters
	filnam2=trim(arg)
!
!	*********************************
!
!	-------------------------------------------------------------------------------
!    		read in attenuation coefficients from file "MEKED_attn_coeffs.txt"
!	-------------------------------------------------------------------------------
!
	CALL read_amu_values()
!
!	-----------------------------------------------------
!			read in x-ray peak data
!	-----------------------------------------------------
	xray_lib_name="XRF_branching_ratios_default.txt"
!
	open (unit=10,file=xray_lib_name)
!
	read (10, *) fil_temp
	do 5997 l1 = 1, 5
	do 5997 l2 = 1, 15
	
	read (10,*) fil_temp, xrf_e(l1,l2), xrf_abs(l1,l2), lor_w(l1,l2)
!	print *,l1, l2, xrf_e(l1,l2), xrf_abs(l1,l2), lor_w(l1,l2)
5997	continue
	print *,' end read xray peak data'
	close (unit=10)
! 	normalize peak areas
!
!	do 5998 l1 = 1, 5
!	norm_x(l1)=0
!	do 5998 l2 = 1, 15
!	norm_x(l1)=norm_x(l1)+xrf_abs(l1,l2)
!	5998	continue
!	
!	do 5999 l1 = 1, 5
!	do 5999 l2 = 1, 15
!	xrf_rel(l1,l2)=xrf_abs(l1,l2)/norm_x(l1)
!	5999	continue
!
!       ********** end read **********
!
!	-----------------------------------------------------
!			read in gamma-ray peak data
!	-----------------------------------------------------
	xray_lib_name="XRF_gamma_lib_default.txt"
!
	open (unit=10,file=xray_lib_name)
!
	read (10, *) fil_temp
	do 6001 l1 = 1, 5
	read (10,*) fil_temp, gamma_pts(l1)
6001	continue

	do 6002 l1 = 1, 3
	do 6002 l2 = 1, 10
	
	read (10,*) fil_temp, fiss_gamma_e(l1,l2), fiss_gamma_i(l1,l2)
!	print *,l1, l2, xrf_e(l1,l2), xrf_abs(l1,l2), lor_w(l1,l2)
6002	continue
	print *,' end read gamma peak data'
	close (unit=10)

!
!       ********** end read **********
!

!	-----------------------------------------------------
!		Read in Trial XRF parameters
!	-----------------------------------------------------
!
	FILNAME=TRIM(TRIM(filnam2)//".txt")

!
	open (unit=10,file=FILNAME)
!
	read (10,*)FILNAM1
	do 1 l1 = 1, 42
	read (10,*)i,xfit_parms(l1), free_params(l1)
1	continue
	print *,' end read '
	close (unit=10)
!
!       ********** end read **********
!
!	create file names for use during fit
!		All files except for the results file (*.res) are required to be within the same directory as the executable
!		so no prefixes are used
!
	SPECOUT=TRIM(TRIM(FILNAM1)//".SPC")		!  final output text file containing raw spec and fitted spectrum
	SPECTEMP=TRIM(TRIM(FILNAM1)//".TMP")		!  temp output text file containing raw spec and fitted spectrum
	PARMSOUT=TRIM(TRIM(FILNAM1)//".PAR") 		!  output file including fit parameters uncertainties and covariances after iteratation
	FILNAME=TRIM(TRIM(FILNAM1)// ".txt")		!  input spectrum text file
	newparname=TRIM(TRIM(FILNAM1)// ".rpt")		!  output text file containing final fit parameters uncertainties and covariance matrix

!
	PASSIVE_SPC= TRIM("spec_passive_"//TRIM(FILNAM1(10:11))//".txt")	  ! passive spectrum file name
!
!
	PRINT*, FILNAME, SPECTEMP, PASSIVE_SPC
!
!	READ IN FITTING CONSTANTS FROM XRF_FIT_CONSTANTS.DAT
!
	open (unit=10,file="XRF_FIT_CONSTANTS.txt")
!
	do 999 l1 = 1, 90
	read (10,*) i, xfit_consts(l1)
	print *,'consts ', i, xfit_consts(l1)
999	continue
	print *,' end read constants'
	close (unit=10)
!
!
	do 998 l1=1,14
	xrf_bkg(l1)=xfit_consts(l1+19)
	xrf_bkg(12)=xfit_consts(6)
	print *,'bkg ', l1, xrf_bkg(l1)
998	continue
!
	do 997 l1=1,5
	element_peaks(l1)=INT(xfit_consts(l1))
997	continue
	do 996 l1= 1, 7
	eff_pars(l1)=xfit_consts(60+l1)
996	continue

!
!	FIT REGION
	E_L1=xfit_consts(9)
	E_L2=xfit_consts(10)
	ichn_start=int((e_l1-xfit_parms(15))/xfit_parms(16))
	ichn_stop=int((e_l2-xfit_parms(15))/xfit_parms(16))
	NMIN=ichn_start
	NMAX=ICHN_STOP
!
! 	mxp=2048
	MTRMS=42
	nterms = 0
!	nimn=ichn_start
	nmax=ichn_stop
	nchannels = ichn_stop - ichn_start + 1
	nt_max = 42
	DO 2 i = 1 , 42
	i_xfit(i) = 0
	idex_a(i) = 0
	a1(i) = 0
	xfit_errors(i) = 0
2	CONTINUE
!
	j = 0
	DO 3 i = 1 , nt_max
	If (free_params(i).EQ.1) j = j + 1
	If (free_params(i).EQ.1) nterms = nterms + 1
	If (free_params(i).EQ.1) i_xfit(i) = j
!	position of xfit in a 
	If (free_params(i).EQ.1) idex_a(j) = i
!	position of a in xfit 
3	CONTINUE
	DO 4 I = 1 , NTERMS
	PRINT *, i, IDEX_A(I)
4	CONTINUE
!
!	SETUP DATA ARRAYS
!
	DO 5 I = 1,7
	I_CONC(I)=1
	I_ATTN(I)=1
5	CONTINUE
!
	DO 11 I = 1 , MTRMS
	A1(I)=0.
	AA(I)=0.
	DELTAA(I)=0.
	SIGMAA(I)=0.
	BDUM(I)=0.
	DO 10 J= 1, MTRMS
	ARRAY(I,J)=0.0
10	CONTINUE
11	CONTINUE
!
!	********************************************
!
	DO 15 I=1,NT_MAX
	PRINT *,I, XFIT_PARMS(I)
15	CONTINUE
!	************ Initialize arrays  ************
	DO 20 I = 1,mxp
	SCAT_DAT(I)=0.
	RAW_DATA(I)=0.
	RAND_TEMP=0.
	XDAT(I)=0.
	weight(i)=0.
	ERROR1(I)=1.
20	CONTINUE
!
	E_0=xfit_parms(15)
	D_E=xfit_parms(16)
!
!	-----------------------------------------------------
!			Read in data file 
!	-----------------------------------------------------
!
	open (unit=10,file=FILNAME)
    	read (10,*)inputfilename, outfilname, Sample_ID, acq_date_mm, acq_time
	read (10,*)ichn, ilive, t_real, vial_dia, sample_temp, u_enrich,pu_weight
	print *, ichn, ilive,t_real, vial_dia, sample_temp, u_enrich, pu_weight
!       may want to use e0 and e1 as starting values for e_cal
!
!	read (10,*)i,x_chan
	do 25 l1 = 1, mxp
	read (10,*)i,x_chan
!	IF (L1.GT.ICHN_STOP) GOTO 25
!	IF (L1.LT.ICHN_START) GOTO 25
	raw_data(i+1)=x_chan
	ERROR1(i+1)=0.
	weight(i+1)=0
	IF (RAW_DATA(i+1).GT.0) ERROR1(i+1)=SQRT(RAW_DATA(i+1))
	If (ERROR1(i+1).NE.0) weight(i+1) = 1.0 /error1(i+1)**2
25	continue
	print *,' end read '
	close (unit=10)
	NCHN=ICHN_STOP-ICHN_START+1
	NMAX=NCHN
!
    print *, ' ***************************************** '
    print *, ' number channels: ', ichn
    print *, ' data file name: ', inputfilename
    print *, ' Sample Id: ', Sample_ID
    print *, ' acq date: ' , acq_date_mm 
    print *, ' acq time: ' , acq_time
    print *, ' live time = ', ilive, '  real time = ', int(t_real*1000)/1000
    print *, ' vial diameter: ', vial_dia, ' sample temperature: ', sample_temp
    print *, ' 235U enrichment: ', U_enrich, ' Pu weight percent: ', pu_wgt

    print *, ' ***************************************** '
!
!       ********** end read **********
!
!	----------------------------------------------------------------------------
!		Read in passive data file for self irradiation correction
!	----------------------------------------------------------------------------
!
	do 26 l1 = 1, mxp
	passive_data(l1)=0.0
26	Passive_error1(l1)=0.0
!
	IF (xfit_consts(35).NE.1) GOTO 28 		!  skip passive spectrum read unless flag is set in constants file
!
	open (unit=10,file=PASSIVE_SPC)
    	read (10,*)passfilename, dummyname, passive_ID
	read (10,*)ichn, p_ilive, p_t_real, p_vial_dia, p_sample_temp, p_u_enrich,p_pu_weight
	print *, ichn, p_ilive, p_t_real, p_vial_dia, p_sample_temp, p_u_enrich,p_pu_weight
!

!
!	read (10,*)i,x_chan
	do 27 l1 = 1, mxp
	read (10,*)i,x_chan
!	IF (L1.GT.ICHN_STOP) GOTO 27
!	IF (L1.LT.ICHN_START) GOTO 27
	passive_data(i+1)=x_chan
	IF (passive_DATA(i+1).GT.0) Passive_error1(i+1)=SQRT(passive_DATA(i+1))
	If (Passive_ERROR1(i+1).NE.0) weight(i+1) = 1.0 /Passive_error1(i+1)**2
27	continue
	print *,' end read '
	close (unit=10)
28	continue
!
!       ********** end read **********
!		
!
!	*******   Subtract passive spectrum   *******
	passive_norm=1
	IF (p_ilive.NE.0) passive_norm= FLOAT(ilive)/FLOAT(p_ilive)
!
	PRINT *, "passive norm = ", ilive, p_ilive,  passive_norm
	do 30 l1 =1, mxp-1
	raw_data(l1)=raw_data(l1)-passive_data(l1)*passive_norm
30	error1(l1)=(error1(l1)**2 + (Passive_error1(l1)*passive_norm)**2)**0.5
!
!
!	*******   CALCULATE SPECTRUM FOR RANDOM SUMMING CORRECTION   *******
!
!	THESE PARAMETERS TO GO INTO A SETUP FILE		
	NLOW=INT(50*mxp/2048)				! Adjusted for file size
	NHIGH=INT(2000*mxp/2048)			! Adjusted for file size
!
	L_norm_limit = xfit_consts(12)			! normalization window lower bound
	H_norm_limit = xfit_consts(13) 			! normalization window upper bound
	Rand_mult = xfit_consts(14)			! holdover from eariler version, xfit_consts(14) should =1
	tail_area=0.0
	tail_decay=1.0
	sum_width=0.5
	GATE= 0.00000078813904
	offset_r=xfit_consts(15)
	deltae=xfit_parms(16)
	gauss_width=xfit_parms(17)
!
	DO 45 I = 1, mxp
	r_short(i)=0.0
	RAND_TEMP(I)=0.0
	smooth_spec=0.0
45	CONTINUE
!
	DO 50 I = 2, int(2040*mxp/2048)
	RAND_TEMP(I)=spec_fold2(NLOW, NHIGH, I, GATE, ilive, Raw_data)
50	CONTINUE
!
	DO 55 I=75, int(2020*mxp/2048)
	e_sm=deltae*dfloat(i)
	smooth_spec(i + offset_r) = g_smooth2(e_sm,deltae, 2.0*gauss_width, i, RAND_TEMP)
55	continue

	rand_norm = 0.0
	spec_norm = 0.0

	DO 57 ij = 1 , int(2040*mxp/2048)
	DO 56 i= 1, 50
	j = ij + i - 1
	r_short(i) = 0
	IF(j .LE. mxp) r_short(i) = smooth_spec(j)  
56	CONTINUE

        spec_tail_int=1.0D0
	spec_tail_decay=0.2D0
	energy = 0.0 + deltae * ij
	temp = smooth_spec(ij)  !  +  +spec_tail(energy, spec_tail_int, spec_tail_decay, delta_e, r_short)  ! <-----
	rand_spec(ij) = temp
57	continue    
	
	DO 58 i= L_norm_limit,  H_norm_limit
	rand_norm = rand_norm + rand_spec(i)    
	spec_norm=spec_norm + raw_data(i)
58	continue
!
	random_norm = spec_norm / rand_norm * Rand_mult
!
	DO 59 i = 1 , mxp				!   
	RAND_TEMP(i) = rand_spec(i) * random_norm      	!   The code has been modified to subtract the background each iteration
59	CONTINUE					!   The random coincidence spectrum will be normalized by FREE paramter a(25) 
!							!   earlier versions would subtract random spectrum at this point
!
!	---------------------------------------------------------------------------------
!
	DO 60 I = 2 , int(2040*mxp/2048)
	XDAT(I)=RAW_DATA(I)
	ERROR1(I)=(RAW_DATA(I)**2)**0.25
	energy=xfit_parms(15)+xfit_parms(16)*I
	weight(i)=0.
	If (error1(i).NE.0) weight(i) = 1.0 /ERROR1(i)**2
	if (energy.GE.xfit_consts(17) .AND. energy.LE.xfit_consts(18)) weight(i)=0.0
!	PRINT *, I, XDAT(I), ERROR1(I)
60	CONTINUE
!	READ *, ione
!
!	---------------------------------------------------------------------------------
!	Convert absolute X-ray branching ratios to relative branching ratios
!	---------------------------------------------------------------------------------
	deltae=xfit_parms(16)
	d_vial = xfit_consts(6)
	d_xrf = xfit_consts(7)
	d_col = xfit_consts(8)
!
	DO 82 l1=1, 6
82	concentrate(l1)=0.0     ! dont correct relative peak intensities at this stage 
!				  Only calculates the relative branching ratios
!
!	CALL call_x_ratio_corr2(deltae, d_vial, d_xrf, d_col, xrf_abs)
!
!	---------------------------------------------------------------------------------
!
	DO 83 l1=1, 6
83	concentrate(l1)=xfit_parms(8+l1)

!
!	**************************************
!	********** CONDENSE MATRIX  **********
!	**************************************
!
	PRINT *, 'REACHED CONDENSE MATRIX'
!
!	to speed analysis, the parameter array is re-arranged to place the free parameters at the beginning of the array
!	This shrinks the size of the array, a1(n), to be varied during the fitting process 
!	The parameter order is contained in the array "idex_a"
!
	DO 100 i = 1 , nterms
	a1(i) = xfit_parms(idex_a(i))
100	CONTINUE
!

 	tails(1) = dabs(xfit_parms(18))		! Tail should always have a postive value
	tails(2) = (xfit_parms(19))
	tails(3)=0.0				! high side tail is set to zero
	tails(4)=1.0
!	tails(3) = dabs(xfit_parms(25))    ! paramters have been realloated
!	tails(4) = dabs(xfit_parms(8))     ! paramters have been realloated
110	CONTINUE
	e_zero = xfit_parms(15)
	deltae = xfit_parms(16)
!
	DO 120 i = 1 , nterms
	deltaa(i) = a1(i) * xfit_consts(36)
	aa_temp(i)= a1(i)
	If (deltaa(i).EQ.0) deltaa(i) = 0.0001
120	CONTINUE
!
	nmin2 = ICHN_START
	nmax2 = ICHN_STOP
!
	npts = 1 + nmax2 - nmin2
	Sum1 = 0.
	DO 130 i = nmin2 , nmax2
	Sum1 = Sum1 + xdat(i)
!
130	CONTINUE
	DO 140 i = 1 , mxp
	ERR2(i) = error1(i)
	ydat(i) = xdat(i)
140	CONTINUE
!
	itmax = INT(XFIT_CONSTS(11))
	i_first=0
190	continue
	chi1 = 0.
	niter = 0
	MODE = 1
!
!	************************************************
!	***************  START FITTING  ****************
!	************************************************
!
	FLAMDA = 0.001
	PRINT *, 'REACHED FITTING'
!
!
	do 200 i = 1 , mxp
	ZDAT(i) = e_zero + float(i) * deltae
200	continue
!
	do 201 i=ichn_start, ichn_stop
	yfit(i)=functn2(i,xfit_parms)
!	PRINT *, I, YFIT(I)
201	continue
	num_free = npts - nterms
!	ydat is the stripped data array, yfit is the calculated response
	chisq_init = FCHISQ1(ydat, weight, npts, num_free, NMIN, yfit)
	print *, 'chisq_init = ', chisq_init
!
	open (unit=20,file=SPECTEMP,action="write",status="replace")
	write (20,*) "file:", FILNAME
	write (20,*) "CHAN  RAW_DATA    RAW_ERROR   Corrected   fit result"
	do 202 I = 1, mxp
	ENERGY=E_0+D_E*I
	z=functn2(I,xfit_parms)
!	TEMP=functn_xfit(energy, XFIT_PARMS)
	write (20,*) I,RAW_datA(i),ERROR1(I),XDAT(I), Z
202	continue
	close (unit=20)
!
210	continue
	do 5555 i = 1 , itmax
	do 220 ij = 1 , nterms
	deltaa(ij) = a1(ij) * xfit_consts(36)
	If (deltaa(ij).EQ.0) deltaa(ij) = aa_temp(ij)* xfit_consts(36)
220	continue
!
	niter = 1 + niter
!
	print *, 'iteration = ', niter,' of ', itmax, ' file: ', filname
	print *, 'nterms = ', nterms
!
!
	FLAMDA = 0.001
	MODE=1
	PRINT *,'REACHED CURFIT'
!
!	-----------------------------------------------------------------------------------
!
	Call curfit(ydat, ERR2, npts, nterms, MODE, a1, xfit_parms,deltaa, SIGMAA, &
	FLAMDA, yfit, CHISQR, nmin2, free_params,Idex_a, COVAR_ARRAY)
!
!	-----------------------------------------------------------------------------------
!
	Call a_to_kfit(SIGMAA, xfit_errors, idex_a, nterms)
	PRINT *, 'Nterms after= ', nterms
!
	xfit_parms(18)=dabs(xfit_parms(18))
!	xfit_parms(25)=dabs(xfit_parms(25))    ! paramters have been realloated
!	xfit_parms(8)=dabs(xfit_parms(8))      ! paramters have been realloated
245	continue
 	CHI2 = CHISQR
	PRINT *, 'chi2 = ', CHI2, NMIN2
	DO 250 ispit = 1 , nt_max
	PRINT *, 'a1(',ispit ,')=',xfit_parms(ispit),xfit_errors(ispit)
250	CONTINUE
	DLTCHI = Abs(CHI2 - chi1) / CHI2
	chi1 = CHI2
	PRINT *, 'chi_square = ', CHI1
	If (DLTCHI.LT.Xfit_consts(31)) GoTo 5600
5555	continue
!	
!	If flag is set - ask to continue fitting
!
    print*,' CHI-SQUARE DID NOT CONVERGE AFTER ',ITMAX,' ITERATIONS'
    if (xfit_consts(34).EQ.0) GOTO 5600
    print *,' CONTINUE (Y,N)?'
    read *, STRING1
    IF((STRING1.NE. 'Y').AND.(STRING1.NE. 'y'))GOTO 5600
    IF(I_FIRST.NE.0) GOTO 5600
    print *,' MAX NUMBER OF ITERATIONS?'
    read *,ITMAX
    GoTo 210
!
!	****************************************
!		Iterations complete
!	****************************************
!
5600    continue
	DO 5650 i = 1 , nterms
	AA(i) = a1(i)
5650	CONTINUE
	DO 5700 i = 1 , nterms
	SIGMAA(i + 1) = SIGMAA(i + 1)
5700	CONTINUE
!
	PRINT *, 'chi_square = ', CHI2
!
1001	continue
!
!	****************************************
!		EXPAND COVARIANCE ARRAY
!	****************************************
!
	DO 6010 I = 1 , 42
	DO 6000 J = 1 , 42
	COVAR_TEMP1(J)=COVAR_ARRAY(I,J)
6000	CONTINUE
!
	Call a_to_kfit(COVAR_TEMP1, COVAR_TEMP2, idex_a, nterms)
	DO 6010 K=1, 42
	COVAR_OUT1(I,K)=COVAR_TEMP2(K)
6010	CONTINUE
!
	DO 6030 I = 1 , 42
	DO 6020 J = 1 , 42
	COVAR_TEMP1(J)=COVAR_OUT1(J,I)
6020	CONTINUE
	Call a_to_kfit(COVAR_TEMP1, COVAR_TEMP2, idex_a, nterms)
!
	DO 6030 K=1, 42
	COVAR_OUT1(K,I)=COVAR_TEMP2(K)
6030    CONTINUE
!
	open (unit=20,file=PARMSOUT,action="write",status="replace")
	write (20,*) filname
	write (20,*) CHI2
	do 6100 I = 1, 42
	write (20,*) i,XFIT_PARMS(I),xfit_errors(I)
6100    continue
	DO 6110 I = 1, 42
 	DO 6110 J = 1, 42
	write (20,*) i,J, COVAR_OUT1(I,J)
6110	continue
	close (unit=20)
!
!	*****************************************************************************
!		write raw data and fit spectrum to out file spec_out_##.spc
!	*****************************************************************************

	open (unit=20,file=SPECOUT,action="write",status="replace")
!
	write (20,*) "file:", FILNAME, FILNAME
	write (20,*) "CHAN RAW_DATA RAW_ERROR Corrected fit"
	do 325 I = 1, mxp
!
	ENERGY=E_0+D_E*I
	B_SCAT=SCAT_DAT(I)
!	z=functn2(i,xfit_parms)+B_SCAT
	z=functn2(i,xfit_parms)
!	write (20,*) i,RAW_DATA(I),ERROR1(I), XDAT(I), Z
	write (20,*) i,RAW_DATA(I)+passive_DATA(i),ERROR1(I), XDAT(I)+passive_DATA(i), Z+passive_DATA(i)    !  add passive spec back in
325	continue
	close (unit=20)
	i_first = i_first+1
	print *,'looping back'
	itmax=1
!	if (I_first .eq. 1) goto 190
! 
!	***************************************************************************
!       ****************** ADD ERROR CORRECTION ROUTINE HERE  *********************
!	***************************************************************************
!
	call Est_error(xfit_parms, xfit_errors, j_dex)
!
!	***********************************
!	Write Results to *.RES file
!		spectrum info
!		Fit results
!		Covariance matrix
!		raw data and fit spectrum
!	***********************************
!
	SPECOUT2=TRIM(TRIM(outfilname)//".res")

    open (unit=20,file=specout2,action="write",status="replace")
!

    write (20,*) TRIM(FILNAME), " ", TRIM(inputfilename) , " ", TRIM(Sample_ID), " ", TRIM(acq_date_mm) , " ", acq_time
    write (20,*) CHI2, vial_dia, sample_temp, u_enrich, pu_weight, ilive, t_real
    do 455 i = 1, 42
    write (20,*) i,xfit_parms(i), xfit_errors(i), free_params(i)
455 continue
    DO 460 I = 1, 42
    DO 460 J = 1, 42
    COVAR_OUT1(I,J)= COVAR_OUT1(I,J)*(float(J_dex))**2
    write (20,*) i,J, COVAR_OUT1(I,J)
460    continue
   write (20,*) "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ"
!
    write (20,*) "file:", FILNAME, FILNAME, inputfilename
    write (20,*) "CHAN RAW_DATA RAW_ERROR Corrected fit"
    do 326 I = 1, mxp
!
	ENERGY=E_0+D_E*I
	B_SCAT=SCAT_DAT(I)
!	z=functn2(i,xfit_parms)+B_SCAT
	z=functn2(i,xfit_parms)
	IF(z.GT. 1.0D15) z=1.0D15
    write (20,*) i,RAW_DATA(I)+passive_DATA(i),ERROR1(I), XDAT(I)+passive_DATA(i), Z+passive_DATA(i)   !  add passive spec back in
326 continue
    write (20,*) "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY"
!    
    do 327 i=1 , 90
    write (20,*) i, xfit_consts(i)
327 continue
    write (20,*) "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
    write (20,*) niter, XFIT_CONSTS(11)
    close (unit=20)

!	
!
!	***********************************
!
!
    open (unit=20,file=newparNAME,action="write",status="replace")
!
    write (20,*) FILNAME, inputfilename
    write (20,*) CHI2, vial_dia, sample_temp, u_enrich, pu_weight, j_dex
    do 425 i = 1, 42
    write (20,*) i,xfit_parms(i), xfit_errors(i)
425 continue
    DO 430 I = 1, 42
    DO 430 J = 1, 42
    COVAR_OUT1(I,J)= COVAR_OUT1(I,J)*(float(J_dex))**2
    write (20,*) i,J, COVAR_OUT1(I,J)
430    continue
    close (unit=20)
!
!	***********************************
!
        goto 441
    SPECOUT2=TRIM(TRIM(outfilname)//".rpt")
!
	    open (unit=20,file=specout2,action="write",status="replace")
!
	    write (20,*) FILNAME, inputfilename
	    write (20,*) CHI2, vial_dia, sample_temp, u_enrich, pu_weight, j_dex
	    do 435 i = 1, 42
	    write (20,*) i,xfit_parms(i), xfit_errors(i)
435 continue
	    DO 440 I = 1, 42
	    DO 440 J = 1, 42
!	    COVAR_OUT1(I,J)= COVAR_OUT1(I,J)*(float(J_dex))**2
    write (20,*) i,J, COVAR_OUT1(I,J)
440    continue
    close (unit=20)
441      continue
!
!
	END
!
!	******************************************************************
!	****************       END XRF_FIT     ***************************
!	******************************************************************
!
!
	SUBROUTINE a_to_kfit(a1, kfit_parms, idex_a, nterms)
	DOUBLE PRECISION CHISQR, AFREE
	DOUBLE PRECISION kfit_parms(42), a1(42)
	INTEGER idex_a(42),  NTERMS
!	Stuffs variable parameters from a1(i) back into kfit_parms(i)
	DO 10 i = 1 , nterms
	kfit_parms(idex_a(i)) = a1(i)
10	CONTINUE
	RETURN
	END
!
!	*********************************
!	Calculate Reduced Chi-Square
!	*********************************
!
	Function FCHISQ1(y, weight, npts, nfree, NMN1, yfit)
	DOUBLE PRECISION YFIT(4100), Y(4100), WEIGHT(4100), fchisq1
	DOUBLE PRECISION AFREE, CHISQR
	INTEGER NPTS, NFREE, NMN1
!	COMMON/CALCDAT/YFIT
	AFREE = DFLOAT(nfree)
	NMN2 = NMN1 + npts - 1
	CHISQR = 0.
!	If (nfree.LE.0) Return
	DO 30 i = NMN1 , NMN2
!	PRINT *, I, Y(I), YFIT(I), WEIGHT(I)
	CHISQR = CHISQR + weight(i) * (y(i) - yfit(i))**2
30	CONTINUE
	FCHISQ1 = CHISQR / AFREE
	PRINT *, 'fchis_1', FCHISQ1
	RETURN
	END
!
!	**********************************************************
!	FUNCTIONS THAT DECRIBE THE XRF RESPONSE FUNCTION
!	**********************************************************
!
    Function functn2(chan_num, a)
!       * * * XRFFUNC * * *
!
!	XRF Response function
!
!	Ported from XRF VBA routine
!	August 21, 2018
!
	DOUBLE PRECISION functn2, tail_x, HPGE_eff, voigt, gaussian, attn_fact_7_co        ! functions called
	DOUBLE PRECISION brem_bkg_new, brem_bkg_narrow, e_ebel, ebel_par_x				! functions called
	DOUBLE PRECISION energy_calc, escape_area, brem_bkg3
	DOUBLE PRECISION xfit_parms(42), a(42)
	DOUBLE PRECISION pk_area(15), el_counts(15), lor_width(15)
	DOUBLE PRECISION concentration(6), i_scat(6)
	DOUBLE PRECISION bkg_par(4),TAILS(4), temp_tail(4)
	DOUBLE PRECISION e_escape(2),i_escape(2)
	DOUBLE PRECISION brem_I, ebel_I, b_tail_area, b_tau, tail_2, tau_2
 !
	DOUBLE PRECISION e_start, deltae, gauss_width, step_bkg_inc, xrf_bkg_norm, ge_area
	DOUBLE PRECISION bkg, trans_factor, attn_factor, attn_length, kappa, sum_el_area
	DOUBLE PRECISION ge_wt, erf_bkg, cd_step_inc, cd_area, cd_tail_low_E
	DOUBLE PRECISION e2, ENERGY, energy1, e3
	DOUBLE PRECISION E_cd, I0_CD, peak_width, cd_step_bkg, bkg_step, ref_step_bkg
	DOUBLE PRECISION Cd109, bkg1, bkg2, bkg3, corr_attn
	DOUBLE PRECISION TEMP_VAL, temp_num, xrf_temp, e_tail
	DOUBLE PRECISION d_vial, d_xrf, d_col, eff_0, delta_attn, xrf_abs(10,15)
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER max_pts, gamma_pts(5)	
	DOUBLE PRECISION g_energy1, g_energy2
!
	INTEGER chan_num, n_it, i_int, ii, i, j
!
	DOUBLE PRECISION xfit_consts(90),XDAT(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100), ERROR1(4100)
	DOUBLE PRECISION concentrate(6), xrf_bkg(14), xrf_bkg_spline(5)
	DOUBLE PRECISION fp_e(100), fp_i(100), fp_w, attn_factor_1, W_AREA, PB_AREA, ener2
	DOUBLE PRECISION xrf_e(10,15), xrf_rel(10,15), lor_w(10,15) , rand_bkg
	INTEGER i_conc(7), i_attn(7), element_peaks(7), i_fp
!
	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/XRFDAT2/RAW_DATA, RAND_TEMP, ERROR1
	COMMON/XRFDAT3/xrf_bkg, concentrate
	COMMON/XRFDAT4/xrf_e, xrf_rel, lor_w, element_peaks
	COMMON/XRFDAT5/xrf_abs
!
!
!	Primary response components
!
!	temp_val = contribution from actinide X-ray peaks and gamma-ray peaks - includes tails 
!	erf_bkg=   step background for X-ray peaks and gamma-ray peaks
!	bkg =      linear background - Compton background from the fission products
!	bkg1 =     primary x-ray beam backscatter contribution 
!	bkg2 =     1st Ge escape peak from x-ray beam backscatter contribution	
!	bkg3 =     2nd Ge escape peak from x-ray beam backscatter contribution	
!       bkg_step = step background from x-ray beam backscatter contribution
!	rand_bkg = random background contribution determined by convoluting measured spectrum with itself
!                  this scaled using a(25) to allow introduction of a linear Compton background from the fission products
!	W_AREA =   Tungsten X-ray peak contribution
!	Pb_AREA =  Lead X-ray peak contribution
!
!
!	Calls to 
!		sub call_x_ratio_corr2
!
!		function HPGE_eff
!		function attn_fact_7_co
!		function energy_calc
!		function voigt
!		function W_VOIGT
!		function PB_VOIGT
!		function gaussian
!		function tail_x
!		function brem_bkg_new
!
!
	bkg = 0.0D0
	erf_bkg = 0.0D0
	Cd_step_bkg = dabs(a(39))
	cd_tail_low_E=xfit_consts(19)
	Cd109 =0
	deltae = a(16)
	n_it=xfit_consts(40)			! subdivide channels increment
	if (n_it .LT.1 ) n_it=1
!
	Pi = 3.14159263
!
	DO 10  i = 1, 6
	concentration(i) = a(i+8)
	i_scat(i) = 0
10	continue
!
	i_scat(6) = 0

	d_vial = xfit_consts(6)
	d_xrf = xfit_consts(7)
	d_col = xfit_consts(8)

	eff_0 = HPGE_eff(xrf_e(1, 1))	! hpge detection effieincy at first x-ray energy
!
	e_start = a(15)
	deltae = a(16)
	gauss_width = a(17)

	CALL call_x_ratio_corr2(deltae, d_vial, d_xrf, d_col, xrf_abs)
!
	TAILS(1)=dabs(a(18))		!  contribution must always be positive
	TAILS(2)=dabs(a(19))		!  first tail must always fall away from the peak on the low energy side
	TAILS(3)=dabs(a(37))		!  contribution must always be positive
	TAILS(4)=a(38)			!  second tail may be be either low or high sided
!
	temp_tail(1)=dabs(a(18))	!  contribution must always be positive
	temp_tail(2)=dabs(a(19))	!  first tail must always fall away from the peak on the low energy side
	temp_tail(3)=dabs(a(37))	!  contribution must always be positive
	temp_tail(4)=a(38)		!  second tail may be be either low or high sided
!
	step_bkg_inc = abs(a(20))
	ge_area = a(21)

	bkg_par(1) = a(26)		!  compton bkg contribution offset
	bkg_par(2) = a(27)		!  compton bkg contribution linear term
	bkg_par(3) = 0.0		!  polynomial background limited to linear
	bkg_par(4) = 0.0		!  polynomial background limited to linear
!
	e_escape(1) = 9.88      
	e_escape(2) = 10.98
!
! 	fission product peaks
!
	k=0

	do 20 i=1, 3
	  do 20 j=1, gamma_pts(i)
	   k=k+1
	   fp_e(k)=fiss_gamma_e(i,j)
20	continue

	fp_i(1)=a(6)
	fp_i(1+gamma_pts(1))=a(7)
	fp_i(1+gamma_pts(1)+gamma_pts(2))=a(8)

	k=0

 	do 22 i=1, 3
	  max_pts=gamma_pts(i)
	  do 22 j=1, max_pts
	    k=k+1
	    g_energy1=FISS_GAMMA_E(i,1)
	    g_energy2=FISS_GAMMA_E(i,j)

            fp_i(k)=a(i+5)*FISS_GAMMA_I(i,j)/FISS_GAMMA_I(i,1)*(HPGE_eff(g_energy2)/HPGE_eff(g_energy1))
	    delta_attn=attn_fact_7_co(g_energy2, deltae, concentration)-attn_fact_7_co(g_energy1, deltae, concentration)
            fp_i(k)=fp_i(k)*exp(-delta_attn*d_vial)

22	continue

!
	energy1 = energy_calc(chan_num, e_start, deltae)
! ---------------------------------------------------
100	temp_val = 0
	bkg = 0
	pk_area(1) = a(1)	! U
	pk_area(2) = a(2)	! Pu
	pk_area(3) = a(3)	! Np
	pk_area(4) = a(4)	! Cm
	pk_area(5) = a(5)	! Am

! 	subdivide channel to integrate over channel - n_it defined in system constants

	DO 40 i_int = 1 , n_it
	energy = energy1 + (i_int - 5.5) * deltae / n_it
	If (n_it.EQ.1) energy = energy1

	DO 35 ii = 1 , 5      !  cycle through 5 actinides
	n_pks = element_peaks(ii)
	sum_el_area = 0
    
!	add relative branching ratios for the current element
	DO 25 j = 1 , n_pks
	sum_el_area = sum_el_area + xrf_rel(ii, j)
25	continue
    
!	Distribute peak area accross the XRF peaks
	Do 27 j = 1 , n_pks
	el_counts(j) = 0
        If (sum_el_area .NE. 0) el_counts(j) = pk_area(ii) * (xrf_rel(ii, j) / sum_el_area) 
27	continue
!
!
!	Calculated characteristic actinides x-ray contribution to the spectrum
!
	DO 30 jj = 1 , n_pks
	temp_val = temp_val + el_counts(jj) * &
	VOIGT(energy, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj))                             			!   x-ray peaks
!	
	e2 = energy + e_escape(1)
	ge_wt = escape_area(xrf_e(ii, jj), 1) * ge_area
	If(ii.LT.3) temp_val = temp_val + el_counts(jj) * ge_wt *  &
	VOIGT(e2, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj)) * 2 * lor_w(ii, jj) / Pi				!   escape peak 1

	e2 = energy + e_escape(2)
	ge_wt = escape_area(xrf_e(ii, jj), 2) * ge_area
	If(ii.LT.3) temp_val = temp_val + el_counts(jj) * ge_wt *  &
	VOIGT(e2, gauss_width, xrf_e(ii, jj), TAILS, deltae, lor_w(ii, jj)) * 2 * lor_w(ii, jj) / Pi				!   escape peak 2

	erf_bkg = erf_bkg + step_bkg_inc * el_counts(jj) * (1 + DErf((xrf_e(ii, jj) - energy) / 2 / gauss_width)) / 2           !   step background

30	continue
35	continue
40	continue
!
	temp_val = temp_val / n_it
!
!	Cd-109 peak
	peak_width = abs(a(22))

	E_cd = a(23)
	I0_CD = 1.0
!
	energy = energy_calc(chan_num, e_start, deltae)
	If (energy.GT.93)GoTo 50
	cd_area = a(24) 
	Cd109 = cd_area * gaussian(energy, E_cd, I0_CD, peak_width, deltae)						!    Cd peak area
!	if (energy.GT.cd_tail_low_E) Cd109 = Cd109 + Cd_step_bkg * cd_area * (1 + Erf((E_cd - energy) / 2 / peak_width)) / 2	 !    Cd step background
	Cd109 = Cd109 + Cd_step_bkg * cd_area * (1 + Erf((E_cd - energy)/2/peak_width)) * (1 + Erf((energy-cd_tail_low_E)/6/peak_width))/4	 !    Cd step background

	Cd109 = Cd109 * (1 + tail_x(energy, E_cd, temp_tail, peak_width))						!    Cd tail
!	
        e2 = energy + e_escape(1)
        ge_wt = escape_area(e_Cd, 1) * ge_area
	CD109 = CD109 + ge_wt * cd_area * gaussian(e2, E_cd, I0_CD, peak_width, deltae)					!   Cd escape peak 1
        e2 = energy + e_escape(2)
        ge_wt = escape_area(e_Cd, 2) * ge_area
	CD109 = CD109 + ge_wt * cd_area * gaussian(e2, E_cd, I0_CD, peak_width, deltae)					!   Cd escape peak 2
!
50	continue
!
!	add in fission product peaks
!
	if ((gamma_pts(1)+gamma_pts(2)+gamma_pts(3)).LE.0) GOTO 46
	DO 45 i_fp=1, (gamma_pts(1)+gamma_pts(2)+gamma_pts(3))
	   energy = energy_calc(chan_num, e_start, deltae)
	   fp_w=abs(a(17))*(fp_e(i_fp)/88.0)**0.5
	   Cd109 = cd109 + fp_i(i_fp) * gaussian(energy, fp_e(i_fp), I0_CD, fp_w, deltae)					!    FP peak area
45	continue
46	continue
!
!	calulcate attenuation correction for emitted x-rays
!
	energy = energy_calc(chan_num, e_start, deltae)
	kappa = xfit_consts(16)
	attn_factor = attn_fact_7_co(energy, deltae, concentration)		! attenuation factor at current energy
	attn_factor_1 = attn_fact_7_co(xrf_e(1,1), deltae, concentration)       ! reference correction to U Ka1 X-ray
	attn_length = (d_vial / 2 - d_xrf)
!
	If (d_xrf .GT. d_vial) d_xrf = d_vial
	If (attn_length .LT. 0)attn_length = 0.0
!
! 	correct x-ray peaks  for relative attenuation and efficiency relative to 98.4 keV
	temp_val = temp_val*Exp(-attn_length*(attn_factor-attn_factor_1))*(HPGE_eff(energy)/HPGE_eff(xrf_e(1,1)))      
!
	energy1 = energy_calc(chan_num, e_start, deltae)
!
	bkg = bkg_par(1) + bkg_par(2) * energy1 + bkg_par(3) * energy1 ** 2 + bkg_par(4) * energy1 ** 3	 !polynomial bkg for fission products

	erf_bkg = erf_bkg / float(n_it)           !  + bkg
!	
!
	e_ebel = a(28)                      ! x-ray end point energy
	ebel_par_x = a(29)	            ! Generator shaping parameter
	brem_I = dabs(a(30))                ! brem intensity
	ebel_I = dabs(a(31))                ! elastic scatter fraction
	xrf_bkg_spline(1) = dabs(a(32))    	! spline ref energy 1
	xrf_bkg_spline(2) = dabs(a(33))    	! spline ref energy 2
	xrf_bkg_spline(4) = a(34)		! spline par A1
	xrf_bkg_spline(5) = a(35)		! spline par A2
	xrf_bkg_spline(3) = dabs(a(36))    	! spline decay constant

	xrf_bkg(13)=a(41)     !   allow fitting of stainless steel thickness

	energy = energy_calc(chan_num, e_start, deltae)

	bkg1 = brem_bkg_new(energy,e_ebel,ebel_par_x,xrf_bkg,brem_I,ebel_I, xrf_bkg_spline, concentration,deltae)	!  brem continuum (already attenuated)

	e2 = energy + e_escape(1)
	ge_wt = escape_area(e2, 1) * ge_area
	bkg2=0
!	bkg2 = ge_wt* brem_bkg_new(e2,e_ebel,ebel_par_x,xrf_bkg,brem_I,ebel_I, xrf_bkg_spline, concentration,deltae) ! brem escape peak contribution

	e2 = energy + e_escape(2)
	ge_wt = escape_area(e2, 1) * ge_area
	bkg3=0
!	bkg3 = ge_wt* brem_bkg_new(e2,e_ebel,ebel_par_x,xrf_bkg,brem_I,ebel_I, xrf_bkg_spline, concentration,deltae)  ! brem escape peak contribution

	bkg_step=0

	e3=energy
101	e3=e3+deltae
!	bkg_step=bkg_step+step_bkg_inc*brem_bkg_narrow(e3,e_ebel,ebel_par_x,xrf_bkg,brem_I,ebel_I, xrf_bkg_spline, concentration,deltae)
	if(e3.LT.e_ebel) goto 101
!
!
	rand_bkg = a(25)*rand_temp(chan_num)
!
!	add in W and Pb X-rays
!
	ENERGY= energy_calc(chan_num, e_start, deltae)
	ENER2= energy_calc(chan_num, e_start, deltae)
	W_AREA=a(40)*HPGE_eff(ENER2)/HPGE_eff(59.2D0)*deltae
	W_AREA=W_AREA*W_VOIGT(energy,Gauss_width,tails,deltae)

	PB_AREA=a(42)*HPGE_eff(ENER2)/HPGE_eff(74.969D0)*deltae
	PB_AREA=PB_AREA*PB_VOIGT(energy,Gauss_width,tails,deltae)
!
	functn2 = temp_val + erf_bkg + bkg + bkg1 + 0*bkg2 + 0*bkg3 + Cd109 + rand_bkg + bkg_step + W_AREA + PB_AREA
!
	RETURN
	End
!
!	*********************************
!	*********************************
!
	Function attn_fact_7_co(energy, deltae, CONCENTRATION)
!
!	calculates macroscopic attenuation factor for sample  
!
	DOUBLE PRECISION  concent(7),CONCENTRATION(6), LOG_E, energy
	DOUBLE PRECISION  MU_BELOW, MU_ABOVE,HNO3_attn_fact
	DOUBLE PRECISION attn_fact_7_co, deltae, xfit_consts(90)
!
	DOUBLE PRECISION kfit_parms(42), kfit_consts(42)
	DOUBLE PRECISION xdat(4100), ERROR1(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100)
	INTEGER i_conc(7), i_attn(7), chan_num

	DOUBLE PRECISION LOR_U, LOR_PU, LOR_AM, LOR_NP, LOR_CM
	DOUBLE PRECISION th_edge, U_EDGE, PU_EDGE, AM_EDGE, NP_EDGE
	DOUBLE PRECISION CM_EDGE, HNO3_edge, PI, ALG_E,Y
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10)
	DOUBLE PRECISION lorentz_w(5), act_attn_fact(6)
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!
!
	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges
!
	Pi = 3.1415926535898
!
	do 1 i = 1 , 6
	concent(i)=CONCENTRATION(i)
1	continue

	lorentz_w(1) = lor_widths(1)			
	lorentz_w(2) = lor_widths(2)
	lorentz_w(3) = lor_widths(3)
	lorentz_w(4) = lor_widths(4)
	lorentz_w(5) = lor_widths(5)
!
	attn_fact_7_co = 0.0
!
	attn1 = 0
!
	alg_e = Log(energy)
!
	i_dex=1
	amu_below = exp(amu_lib(i_dex,4)*alg_e**3+amu_lib(i_dex,3)*alg_e**2+amu_lib(i_dex,2)*alg_e + amu_lib(i_dex,1))
	amu_above = exp(amu_lib(i_dex,8)*alg_e**3+amu_lib(i_dex,7)*alg_e**2+amu_lib(i_dex,6)*alg_e + amu_lib(i_dex,5))
	y = 2.0*(energy-edges(i_dex))/lorentz_w(i_dex)
	atany = atan(y)
	act_attn_fact(i_dex)=(amu_below+(amu_above-amu_below)*(0.5+atany/Pi))
	act_attn_fact(i_dex)=act_attn_fact(i_dex)*concent(i_dex)
!	comment out the next line to broaden above the K-edge
	If (energy.GT.edges(i_dex)) act_attn_fact(i_dex) = amu_above*concent(i_dex)
!
!	Element 2
	i_dex=2
	amu_below = exp(amu_lib(i_dex,4)*alg_e**3+amu_lib(i_dex,3)*alg_e**2+amu_lib(i_dex,2)*alg_e + amu_lib(i_dex,1))
	amu_above = exp(amu_lib(i_dex,8)*alg_e**3+amu_lib(i_dex,7)*alg_e**2+amu_lib(i_dex,6)*alg_e + amu_lib(i_dex,5))
	y = 2.0*(energy-edges(i_dex))/lorentz_w(i_dex)
	atany = atan(y)
	act_attn_fact(i_dex)=(amu_below+(amu_above-amu_below)*(0.5+atany/Pi))
	act_attn_fact(i_dex)=act_attn_fact(i_dex)*concent(i_dex)
!	comment out the next line to broaden above the K-edge
	If (energy.GT.edges(i_dex)) act_attn_fact(i_dex) = amu_above*concent(i_dex)
!
!	Elements 3 to 5 are broadened above the K-edge (yes I could have used a loop)
!
!	Element 3
	i_dex=3
	amu_below = exp(amu_lib(i_dex,4)*alg_e**3+amu_lib(i_dex,3)*alg_e**2+amu_lib(i_dex,2)*alg_e + amu_lib(i_dex,1))
	amu_above = exp(amu_lib(i_dex,8)*alg_e**3+amu_lib(i_dex,7)*alg_e**2+amu_lib(i_dex,6)*alg_e + amu_lib(i_dex,5))
	y = 2.0*(energy-edges(i_dex))/lorentz_w(i_dex)
	atany = atan(y)
	act_attn_fact(i_dex)=(amu_below+(amu_above-amu_below)*(0.5+atany/Pi))
	act_attn_fact(i_dex)=act_attn_fact(i_dex)*concent(i_dex)
!
!	Element  4
	i_dex=4
	amu_below = exp(amu_lib(i_dex,4)*alg_e**3+amu_lib(i_dex,3)*alg_e**2+amu_lib(i_dex,2)*alg_e + amu_lib(i_dex,1))
	amu_above = exp(amu_lib(i_dex,8)*alg_e**3+amu_lib(i_dex,7)*alg_e**2+amu_lib(i_dex,6)*alg_e + amu_lib(i_dex,5))
	y = 2.0*(energy-edges(i_dex))/lorentz_w(i_dex)
	atany = atan(y)
	act_attn_fact(i_dex)=(amu_below+(amu_above-amu_below)*(0.5+atany/Pi))
	act_attn_fact(i_dex)=act_attn_fact(i_dex)*concent(i_dex)
!
!	Element  5
	i_dex=5
	amu_below = exp(amu_lib(i_dex,4)*alg_e**3+amu_lib(i_dex,3)*alg_e**2+amu_lib(i_dex,2)*alg_e + amu_lib(i_dex,1))
	amu_above = exp(amu_lib(i_dex,8)*alg_e**3+amu_lib(i_dex,7)*alg_e**2+amu_lib(i_dex,6)*alg_e + amu_lib(i_dex,5))
	y = 2.0*(energy-edges(i_dex))/lorentz_w(i_dex)
	atany = atan(y)
	act_attn_fact(i_dex)=(amu_below+(amu_above-amu_below)*(0.5+atany/Pi))
	act_attn_fact(i_dex)=act_attn_fact(i_dex)*concent(i_dex)
!
!	Solution Matrix
	i_dex=1
	amu_below = exp(sol_lib(i_dex,4)*alg_e**3+sol_lib(i_dex,3)*alg_e**2+sol_lib(i_dex,2)*alg_e + sol_lib(i_dex,1))
	amu_above = exp(sol_lib(i_dex,9)*alg_e**3+sol_lib(i_dex,8)*alg_e**2+sol_lib(i_dex,7)*alg_e + sol_lib(i_dex,6))
	HNO3_attn_fact=amu_below*concent(6)
!	If (energy.GT.HNO3_edge) HNO3_attn_fact=amu_above*concent(6)
!
	attn_fact_7_co=0
	do 20 i_dex = 1, 5
	attn_fact_7_co =attn_fact_7_co + act_attn_fact(i_dex)*act_norm_factor(i_dex) 
!
20	continue
	attn_fact_7_co=attn_fact_7_co+HNO3_attn_fact
	RETURN
	END
!
!	*********************************
!	*********************************
!
	Function attn_fact_W_PE2(energy, par_1, par_2)
!	attn coeff Tungsten Photoelectric
	DOUBLE PRECISION energy, attn_fact_W_PE2, par_1, par_2
	DOUBLE PRECISION aL1, aL2, aH1, aH2, edg
!
!	change over point
	edge = 69.53
!
	aL1 = 232603
	aL2 = -2.734
	aH1 = 938475
	aH2 = -2.679
!
	attn_fact_W_PE2 = 1
	attn_fact_W_PE2 = aL1*energy**aL2
	If (energy.GT.edge) attn_fact_W_PE2 = aH1*energy**aH2
!
	attn_fact_W_PE2 = (par_1 + par_2 * energy) * attn_fact_W_PE2
!
	RETURN
	END
!
!	*********************************
!
	Function scat_fact_7(energy, concentration)
!	 returns the macroscopic coherent scattering coefficient for the solution
	DOUBLE PRECISION energy, concentration(6)
	DOUBLE PRECISION cont_U, cont_PU, cont_AM, cont_matrix, scat_fact_7
	DOUBLE PRECISION cont_NP, cont_CM
	Pi = 3.1415926535898

	cont_U = 460.6980 * energy ** (-1.6369)
	cont_PU = 476.85935 * energy ** (-1.63267)
	cont_NP = 437.23855 * energy ** (-1.61976)
	cont_AM = 480.57713 * energy ** (-1.63180)
	cont_CM = 431.90360 * energy ** (-1.60687)
	cont_matrix = 32.229 * energy ** (-1.892)

	scat_fact_7 = cont_U * concentration(1) + cont_PU * concentration(2) + cont_matrix * concentration(6)
	scat_fact_7 = scat_fact_7+cont_AM * concentration(5)+cont_NP * concentration(3)+cont_CM * concentration(4)
	End Function
!
!	*********************************
!
	Function attn_fact_W(energy)
!	attn coeff Tungsten P	
	DOUBLE PRECISION energy, attn_fact_W
	DOUBLE PRECISION aL1, aL2, aH1, aH2, edge
	edge = 69.53
!
	aL1 = 160974
	aL2 = -2.608
	aH1 = 493198
	aH2 = -2.523
!
	attn_fact_W = 1
	attn_fact_W = aL1 * energy**aL2
	If (energy.GT.edge) attn_fact_W = aH1 * energy**aH2
!
	RETURN
	End
!
!	*********************************
!
	Function attn_fact_ss_7(energy, i_co)
!
	DOUBLE PRECISION energy, attn_fact_ss_7
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10)
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges
!
!	attn coeff for SS
!	parameters read in from attenuation factors data file
!
	edge = misc_edges(1)
!	attn parameters with coherent scattering

	Amu_ss_L1 = misc_lib(1,1)
	Amu_ss_L2 = misc_lib(1,2)
	Amu_ss_L3 = misc_lib(1,3)
	Amu_ss_L4 = misc_lib(1,4)
	Amu_ss_L5 = misc_lib(1,5)

	Amu_ss_H1 = misc_lib(1,6)
	Amu_ss_H2 = misc_lib(1,7)
	Amu_ss_H3 = misc_lib(1,8)
	Amu_ss_H4 = misc_lib(1,9)
	Amu_ss_H5 = misc_lib(1,10)
!
100	continue

	al_e= log(energy)

	Amu_below = exp(Amu_ss_L5*al_e**4+Amu_ss_L4*al_e**3+Amu_ss_L3*al_e**2+Amu_ss_L2*al_e + Amu_ss_L1)
	Amu_above = exp(Amu_ss_H5*al_e**4+Amu_ss_H4*al_e**3+Amu_ss_H3*al_e**2+Amu_ss_H2*al_e + Amu_ss_H1)
!
	attn_fact_ss_7 = Amu_below
	If (energy.GT.edge) attn_fact_ss_7 = Amu_above
!
	RETURN
	End
!
!	*********************************
!
	Function attn_fact_Ge_7(energy, i_co)
	DOUBLE PRECISION energy, attn_fact_Ge_7
	DOUBLE PRECISION Amu_ss_L1, Amu_ss_L2, Amu_ss_L3
	DOUBLE PRECISION Amu_ss_H1, Amu_ss_H2, Amu_ss_H3
	DOUBLE PRECISION al_e, Amu_below, Amu_above, alog_e
!
!	attn coeff for Ge
!
	edge = 11.1
!	attn parameters with coherent scattering
	amu_ge_L1 = -0.3273
	amu_ge_L2 = -2.2106
	amu_ge_L3 = 4.1075
	amu_ge_H1 = 0.4545
	amu_ge_H2 = -4.105
	amu_ge_H3 = 6.1549
!
	If (i_co.EQ.1) GoTo 100
!
!	attn parameters without coherent scattering
	amu_ge_L1 = -0.3674
	amu_ge_L2 = -2.1865
	amu_ge_L3 = 4.1042
	amu_ge_H1 = 0.4528
	amu_ge_H2 = -4.1419
	amu_ge_H3 = 6.1966
!
100	continue
!
	alog_e = DLog10(energy)
	amu_below = 10**(amu_ge_L1*alog_e**2+amu_ge_L2*alog_e+amu_ge_L3)
	amu_above = 10**(amu_ge_H1*alog_e**2+amu_ge_H2*alog_e+amu_ge_H3)
!
	attn_fact_Ge_7 = amu_below
!
	If (energy.GT.edge) attn_fact_Ge_7 = amu_above
	RETURN
	END
!
!	*********************************
!
	Function attn_fact_cd_7(energy, i_co)
	DOUBLE PRECISION energy, attn_fact_cd_7
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10)
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges
!
!	attn coeff for cadmium
!	
!	attn parameters with coherent scattering
!
	al_e2=log(energy)
	edge=misc_edges(3)

	amu_cd_L1 = misc_lib(3,1)
	amu_cd_L2 = misc_lib(3,2)
	amu_cd_L3 = misc_lib(3,3)
	amu_cd_L4 = misc_lib(3,4)
	amu_cd_L5 = misc_lib(3,5)

	amu_cd_H1 = misc_lib(3,6)
	amu_cd_H2 = misc_lib(3,7)
	amu_cd_H3 = misc_lib(3,8)
	amu_cd_H4 = misc_lib(3,9)
	amu_cd_H5 = misc_lib(3,10)

	Amu_below = exp(amu_cd_L5*al_e2**4+amu_cd_L4*al_e2**3+amu_cd_L3*al_e2**2+amu_cd_L2*al_e2 + amu_cd_L1)
	Amu_above = exp(amu_cd_H5*al_e2**4+amu_cd_H4*al_e2**3+amu_cd_H3*al_e2**2+amu_cd_H2*al_e2 + amu_cd_H1)
!
	attn_fact_cd_7 = amu_below
	If (energy.GT.edge) attn_fact_cd_7 = amu_above

	If (i_co.EQ.1) GoTo 100
!
!	attn parameters without coherent scattering
!
	edge = 102
	amu_cd_L1 = 0.03
	amu_cd_L2 = -2.8373
	amu_cd_L3 = 5.7089
	amu_cd_H1 = 1.2242
	amu_cd_H2 = -7.6469
	amu_cd_H3 = 10.555
!
	alog_e = Log10(energy)
	amu_below = 10**(amu_cd_L1*alog_e**2+amu_cd_L2*alog_e+amu_cd_L3)
	amu_above = 10**(amu_cd_H1*alog_e**2+amu_cd_H2*alog_e+amu_cd_H3)
!
	attn_fact_cd_7 = amu_below
	If (energy.GT.edge) attn_fact_cd_7 = amu_above

100	continue

	RETURN
	END
!
!	*********************************
!
	Function attn_fact_poly_co(energy)
	DOUBLE PRECISION attn_fact_poly_co, energy
!
!	attn coeff for polyethylene
!
	edge = 30
!
	aL1 = 372.99
	aL2 = -2.337
	aH1 = 0.7068
	aH2 = -0.306
!
!	attn_fact_poly_co = 1.0
!	attn_fact_poly_co = aL1 * energy**aL2
!	If (energy.GT.edge) attn_fact_poly_co = aH1*energy**aH2


	attn_fact_poly_co=attn_fact_vial(energy, 1)

	RETURN
	END
!
!	*********************************
!
	Function attn_fact_vial(energy, vial_type)
	DOUBLE PRECISION energy, alg_e
!	DOUBLE PRECISION amu_POLY_L0, amu_POLY_L1, amu_POLY_L2, amu_POLY_L3
!	DOUBLE PRECISION amu_SiO2_L0, amu_SiO2_L1, amu_SiO2_L2, amu_SiO2_L3
	DOUBLE PRECISION amu_L0, amu_L1, amu_L2, amu_L3
	INTEGER vial_type
!
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10)
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges
!
!	attn coeff for polyethylene
!
	amu_L0=cmu_lib(1,1)
	amu_L1=cmu_lib(1,2)
	amu_L2=cmu_lib(1,3)
	amu_L3=cmu_lib(1,4)

	if(vial_type.NE.1) GOTO 10

	amu_L0=cmu_lib(2,1)
	amu_L1=cmu_lib(2,2)
	amu_L2=cmu_lib(2,3)
	amu_L3=cmu_lib(2,4)
!
10	continue
	alg_e=LOG(energy)
	attn_fact_vial = exp(amu_L3*alg_e**3+amu_L2*alg_e**2+amu_L1*alg_e+amu_L0)

	RETURN
	END
!
!	*********************************
!
	Function attn_fact_be_co(energy)
!
	DOUBLE PRECISION energy
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10)
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges
!
!	attn coeff for Beryllium
!
	aH1 = misc_lib(2, 1)
	aH2 = misc_lib(2, 2)
!
	attn_fact_be_co = aH1 * energy**aH2
!
	RETURN
	END
!
!	*********************************

	Function energy_calc(i, E0, deltae)
	INTEGER i
	DOUBLE PRECISION E0, deltae, energy_calc
	energy_calc = E0 + i * deltae
	End Function
!
!	*********************************
!
	Function HPGE_eff(energy)
!
!	ISOCS EFFICIENT CALCULATION FOR GL0210 HPGE DETECTOR
!
	DOUBLE PRECISION energy, x, eff, HPGE_eff,  eff_p(7)
	COMMON/effdat/eff_p
!	eff_p(1) = -7.26
!	eff_p(2) = 0.8296
!	eff_p(3) = -0.5733
!	eff_p(4) = -0.3383
!	eff_p(5) = 0.4638
!	eff_p(6) = -0.1317
!	eff_p(7) = 160.0
!
	x = Log(eff_p(7) / energy)
	eff = 0.0
!
	DO 10 i = 1 , 6
	eff = eff + eff_p(i) * x**(i - 1)
10	CONTINUE
!
	eff = DExp(eff)
	HPGE_eff = eff
	RETURN
	END
!
!	*********************************
!
	Function gaussian(energy, e0, P0, SIGMA, deltae)
!
	DOUBLE PRECISION ENERGY, E0, P0, SIGMA, DELTAE, gaussian, y1, y
	Pi = 3.14159265358979
	y1 = ((energy - e0)**2)/2.0/ SIGMA**2
	y =deltae*P0*Exp(-y1)/(2.0 * Pi)**0.5/SIGMA
	gaussian = y
	RETURN
	END
!
!	*********************************
!
	Function VOIGT(energy, gauss_width, E0, tails, deltae, lor_w)
!
	DOUBLE PRECISION voigt, tail_x, czosnyka
	DOUBLE PRECISION energy, e_temp, tails(4), lor_w, deltae, gauss_width
	DOUBLE PRECISION del_peak, e_off, y_temp1, y_temp2, y_temp
	DOUBLE PRECISION gauss_width_0, E0, lor_width, e_1, temp_tail(4), tail, tail_w1, tail_w
	VOIGT = 0
	if (abs(energy-E0).GT.50.) Return
	y_temp = 0
	tail = 0
	tail_w1=0
	del_peak=tails(3)		!  allows for splitting of peak due to electronics problem (non guassian broadening) 
	e_off=tails(4)			!  allows for splitting of peak due to electronics problem 

	del_peak=0		 
	e_off=0			 

	temp_tail(1)=Tails(1)     
	temp_tail(2)=Tails(2)
	temp_tail(3)=Tails(3)
	temp_tail(4)=Tails(4)

	gauss_width_0 = (E0 / 88.0) ** 0.5 * gauss_width
	e_temp = E0
	lor_width = lor_w
	y_temp = (1.0-del_peak)* czosnyka(energy, e_temp, lor_width, gauss_width_0)
	y_temp = Y_temp + del_peak* czosnyka(energy, e_temp, lor_width*(1.0+e_off) , gauss_width_0)

	i_trn = 5

!	tail orign distributed with peak intensity as a function of energy across the lorentzian peak
!	DO 10 i = 1 , i_trn
!	  e_1 = E0 + (i - Int(i_trn / 2.0 + 0.5)) * deltae
!	  tail_w = czosnyka(e_1, e_temp, lor_width, gauss_width_0)
!	  tail_w1=tail_w1+tail_w
!	  tail = tail + tail_w * tail_x(energy + deltae, e_1, tails, gauss_width_0)
!10	continue

	tail =tail_x(energy , E0, temp_tail, gauss_width_0)
	VOIGT = deltae * y_temp + tail 

	RETURN
	End Function
!
!	*********************************
!
	Function escape_area(energy, np)
!
!	np is escape peak 1 or 2 (9.88 or 10.99 keV)
	DOUBLE PRECISION escape_area, energy, edge, attn_fact_ge
	DOUBLE PRECISION frac, A1, A2, esc_e(2), esc_mu(2)

	INTEGER np

	esc_e(1) = 9.88
	esc_e(2) = 10.99
	esc_mu(1) = 37.42
	esc_mu(2) = 37.42
!
	edge = 11.1
!
	frac = 0.467
	If (np.EQ.2) frac = 0.0603
!
	A1 = 77700
	A2 = -2.523
!
	attn_fact_ge = 2.81
!
	If (energy.GT.edge)attn_fact_ge = A1*energy**A2
!
	escape_area = frac*attn_fact_ge/esc_mu(np)/2.5
	RETURN
	END
!
!	*********************************
!
	Function spec_fold2(nlow, nhigh, npt, gate, live_time, XRAW)
!
!	computes the random coincidence summing function
!
!	RAW_DATA contains the raw spectrum in counts
!	nlow, nhigh are the start and stop channels over which the convolution is performed
!	npt is the channel number of the resultant spectrum
!	gate is the coincidence gate time in seconds (~0.5 E-6)
!	Note: This is a first order correction. However, for summed energies of less than
!	150 kev the second order terms should be negligible.
!
!
	DOUBLE PRECISION XRAW(4100), GATE, spec_fold2, temp
	INTEGER nlow, nhigh, npt, live_time
!
	alive_time=float(live_time)
	temp = 0.0
	do 10, i = nlow , nhigh+1
	If (i.GT.npt) GoTo 20
	If ((npt - i).LT. 1) GoTo 10
	temp=temp+XRAW(i)*XRAW(npt-i)*gate/alive_time**2
10 	continue
20 	continue
!
	spec_fold2 = temp
	RETURN
	END
!
!	*********************************
!
    Function spec_tail(energy,lo_tail_area,lo_tail_decay,delta_e, X)
	DOUBLE PRECISION LO_TAIL_AREA, lo_tail_decay, X(50)
	DOUBLE PRECISION xfit_parms(42), xfit_consts(90)
	DOUBLE PRECISION xdat(4100), ERROR1(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100)
	DOUBLE PRECISION temp, spec_tail
	INTEGER i_conc(7), i_attn(7), chan_num
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!

	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/XRFDAT2/RAW_DATA, RAND_TEMP, ERROR1

	DOUBLE PRECISION ENERGY
!
!	TAILING USES A REACH AHEAD APPROACH, NREACH CHANNELS
!
	fast_tail_area = 0.0
	fast_tail_decay = 1.0
	NREACH=50
	temp = 0.0
	do 10 i = 1 , NREACH
	e_i = i * delta_e
	temp=temp+X(i)*lo_tail_area*Exp(-e_i*lo_tail_decay)
!
10	continue
	spec_tail = temp
	RETURN
	END
!
!	*********************************
!
	Function back_scat(energy_scat1,i_10, KFIT_PARMS)
!
	DOUBLE PRECISION  back_scat
	INTEGER I, i_10, i_1, I_START
	DOUBLE PRECISION kfit_parms(42), kfit_consts(90)
	DOUBLE PRECISION xdat(4100), ERROR1(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100)
	INTEGER i_conc(7), i_attn(7), chan_num
	COMMON/KEDDAT/kfit_consts,i_conc,i_attn,xdat,scat_dat
	COMMON/KEDDAT2/RAW_DATA, RAND_TEMP, ERROR1
!
	DOUBLE PRECISION F_S, M_E, R_C, E_0
	DOUBLE PRECISION X_INTENSITY,KRAM_POWER,SCAT_I,SCAT_E,SPEC_0
	DOUBLE PRECISION DELTA_E, TEMP_SUM,angle_min, E1, E_1
	DOUBLE PRECISION COS_SCAT_DAT, ang_scat, P_E
	DOUBLE PRECISION ATTN_TEMP, REL_COR, TEMP1, TEMP2
	DOUBLE PRECISION energy_scat, energy_scat1, cos_ang_scat, attn_fact_Ge_7
!
	energy_scat = energy_scat1 + 1.8
	i_0 = i_10 + 20
	f_s = 1.0 / 137.04
	M_E = 511.0
	r_c = 0.38616
	e_0 = kfit_parms(8)
	x_intensity = kfit_parms(9)
	Kram_power = kfit_parms(10)
	scat_i = dAbs(kfit_parms(11))
	spec_0 = kfit_parms(13)
	delta_e = kfit_parms(14)
	temp_sum = 0.0
	angle_min = (KFIT_CONSTS(11)*3.1415926/180)
!
!	i_1 = int((energy - spec_0) / delta_e)
!	I_start = Max(i_0, i_1)
!
	DO 50 I = I_10 , I_10+1000
	e_1 = spec_0 + delta_e * (i)
!
	If (e_1.GT.e_0) GoTo 100
	cos_ang_scat = 0.0
!
	TEMP2=dAbs(1.0-(e_1/energy_scat-1.0)*m_e/e_1)
	If (TEMP2.GT.1) GoTo 50
	cos_ang_scat=(1.0-(e_1/energy_scat-1.0)*m_e/e_1)
	ang_scat = Acos(cos_ang_scat)
!
	If (ang_scat.LT.angle_min) GoTo 50
	P_E=1./(1+e_1/m_e*(1.-cos_ang_scat))
	ATTN_TEMP=dExp(-attn_fact_Ge_7(e_1, 1)*5.323* 1.0)
	rel_cor=ATTN_TEMP/(1-ATTN_TEMP)
	TEMP1=xdat(i)*(f_s*r_c*P_E)**2 * (P_E+1/P_E-1+cos_ang_scat**2)/2*rel_cor
!
	If (cos_ang_scat.NE.0) temp_sum = temp_sum + TEMP1
50	continue
100	continue
!
	back_scat = temp_sum
	RETURN
	END

!
!	**********************************************************************************
!	*******************************  CURFIT Routine  *********************************
!	**********************************************************************************
!
  SUBROUTINE curfit(y,ERROR_m,npts, nterms, MODE,A1, xfit_parms, &
	deltaa, SIGMAA, FLAMDA, yfit, CHISQR, NMIN, &
	free_params, idex_a, COVAR_ARRAY)
!
!		Based in very large part onthe CURFIT Routine in
!		Bevington's Data Reduction adn Error Analysis for the Physical Sciences (1978)
!		
!
	DOUBLE PRECISION functn2, CHISQ1, FCHISQ1
!
	DOUBLE PRECISION YFIT(4100), Y(4100), ERROR_M(4100)
	DOUBLE PRECISION xfit_parms(42), COVAR_ARRAY(42,42)
	DOUBLE PRECISION SIGMAA(42), DELTAA(42), A1(42), b(42)
	DOUBLE PRECISION array_m(42, 42), BDUM(42), xfit_temp(42)
	DOUBLE PRECISION WEIG(4100), ALPHA(42, 42), BETA(42), deriv(42)
	DOUBLE PRECISION CHISQR, FLAMDA, ALPHA_TEMP, TEMPOUT, F1
!
	INTEGER chan_num, JX, IX, j, k
	INTEGER NPTS, NTERMS, MODE, NMIN
	INTEGER IDEX_A(42), FREE_PARAMS(42)
!
	DOUBLE PRECISION xfit_consts(90),XDAT(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100), ERROR1(4100)
	DOUBLE PRECISION concentrate(6), xrf_bkg(14)
	DOUBLE PRECISION xrf_e(10,15), xrf_rel(10,15), lor_w(10,15) 
	INTEGER i_conc(7), i_attn(7), element_peaks(7)
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!

	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/XRFDAT2/RAW_DATA, RAND_TEMP, ERROR1
!	COMMON/XRFDAT3/xrf_bkg, concentrate
!	COMMON/XRFDAT4/xrf_e, xrf_rel, lor_w, element_peaks
!
	nfree = npts - nterms
	NMAX = NMIN + npts - 1
	matloops = 0
11	nfree = npts - nterms
!	----------------------  EVALUATE WEIGHTS ----------------
20	continue
        DO 29  i = NMIN , NMAX
	WEIG(i)=0.
        If (ERROR_m(i).LE.0.) ERROR_m(i) = dAbs(y(i)) ** 0.5
        If (ERROR_m(i).NE.0.) WEIG(i) = 1 / ERROR_m(i) ** 2
	energy=xfit_parms(15)+xfit_parms(16)*I
29	if (energy.GT.xfit_consts(17) .AND. energy.LT.xfit_consts(18)) WEIG(i)=0.0
!
!	--------------- EVALUATE ALPHA AND BETA MATRICES -----------
!
31	DO 34 J = 1, 42
        BETA(j) = 0
        DO 34 k = 1 , j
34      ALPHA(j, k) = 0
!
	Call a_to_kfit(A1, xfit_parms, idex_a, nterms)
!
41	DO 50 ij = NMIN , NMAX
	Call fderiv2(ij,A1,nterms,deltaa,deriv,free_params,idex_a,xfit_parms)
!
	DO 47 j = 1 , nterms
	BETA(j)=BETA(j)+WEIG(ij)*(y(ij)-functn2(ij,xfit_parms))*deriv(j)
	DO 46  k = 1 , j
	ALPHA(j,k)=ALPHA(j,k)+WEIG(ij)*deriv(j)*deriv(k)
!	print *, j, k, alpha(j,k), weig(ij), deriv(j), deriv(k)
!	print *, weig(ij)
46	CONTINUE
47	CONTINUE
50	CONTINUE
!
51      DO 53 j = 1 , nterms
        DO 53 k = 1 , j
53      ALPHA(k, j) = ALPHA(j, k)
!
!	----------   EVALUATE CHI SQUARE AT STARTING POINT    ---------
!
61	DO 62 i = NMIN , NMAX
62      yfit(i) = functn2(i,xfit_parms)
63	CHISQ1 = FCHISQ1(y, WEIG, npts, nfree, NMIN, yfit)
	PRINT *, ' fchsi = ', CHISQ1
!
!	 --------  INVERT MODIFIED CURVATURE MATRIX TO FIND NEW PARAMETERS
!
71	DO 74 j = 1 , nterms
	DO 73 k = 1 , nterms
        F1 = (ALPHA(j, j) * ALPHA(k, k)) ** 0.5
73        If (F1 .NE. 0) array_m(j, k) = (ALPHA(j, k) / F1)
74	Array_m(j, j) = 1.0 + (FLAMDA)
!
	matloops = matloops + 1
80	Call MATInv(nterms, det, array_m)
81	DO 84 j = 1 , nterms
	b(j) = A1(j)
	DO 84 k = 1 , nterms
    	F1 = (ALPHA(j, j) * ALPHA(k, k)) ** 0.5
84  	If (F1.NE.0.0) b(j)=b(j)+BETA(k)*array_m(j,k)/F1
!
!	 ------------- IF CHI SQUARE INCREASED, INCREASE FLAMDA AND TRY AGAIN
!
        DO 87 i_101 = 1 , 42
        xfit_temp(i_101) = xfit_parms(i_101)
87	CONTINUE
!
        Call a_to_kfit(b, xfit_temp, idex_a, nterms)
!
91	DO 92 i = NMIN , NMAX
92      yfit(i) = functn2(i,xfit_temp)
93	CHISQR = FCHISQ1(y, WEIG, npts, nfree, NMIN, yfit)
	If (CHISQ1.GE.CHISQR) GOTO 101
	if (matloops.GE.50) GOTO 101
	PRINT *, 'loops ', matloops , CHISQR
95	FLAMDA = 10.0 * FLAMDA
	GoTo 71
!
!	-----------  EVALUATE NEW PARAMETERS
!
101	CONTINUE
	DO 103 j = 1 , nterms
	A1(J) = b(J)
	alpha_temp = ALPHA(j, j)
	If (alpha_temp .EQ. 0.0) SIGMAA(j) = 0.0
    	If (alpha_temp .NE.0.0)SIGMAA(j)=((1.+FLAMDA)*array_m(j,j)/alpha_temp)**0.5
103	CONTINUE
        FLAMDA = FLAMDA / 10.0
	Call a_to_kfit(A1, xfit_parms, idex_a, nterms)
110	continue
!
	PRINT *, I, XFIT_PARMS(I), SIGMAA(I)
!
!	CALCULATE COVARIANCE MATRIX
!
	DO 150 JX = 1 , NTERMS
	DO 150 IX = 1 , NTERMS
	alpha_temp = ALPHA(IX, JX)
	If (alpha_temp .EQ. 0) tempout = 0.0
	If (alpha_temp.NE.0)tempout=((1.0+FLAMDA)*array_m(IX, JX)/alpha_temp)
	COVAR_ARRAY(IX,JX) = tempout
150	CONTINUE
  PRINT *, 'IN CURFIT CHISQR =', CHISQR, CHISQ1
	RETURN
	END
!
!	*********************************
!
	SUBROUTINE fderiv2(ichan_num, A1, nterms, deltaa, deriv, free_params, idex_a, xfit_parms)
!
!
	DOUBLE PRECISION A1(42), b(42),deriv(42), deltaa(42), xfit_temp(42), xfit_parms(42)
	DOUBLE PRECISION ahigh_val, alow_val
	DOUBLE PRECISION functn2
!
	INTEGER chan_num, idex_a(42), ichan_num
	INTEGER NPTS, num_NFREE, Nmin, MODE, NTERMS
	INTEGER FREE_PARAMS(42)
!
	DOUBLE PRECISION xfit_consts(90),XDAT(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100), ERROR1(4100)
	DOUBLE PRECISION concentrate(6), xrf_bkg(14)
	DOUBLE PRECISION xrf_e(10,15), xrf_rel(10,15), lor_w(10,15) 
	INTEGER i_conc(7), i_attn(7), element_peaks(7)
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!

	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/XRFDAT2/RAW_DATA, RAND_TEMP, ERROR1
	COMMON/XRFDAT3/xrf_bkg, concentrate
	COMMON/XRFDAT4/xrf_e, xrf_rel, lor_w, element_peaks
!
	chan_num=ichan_num
!	orig_val = functn2(chan_num, xfit_parms)
!	
	DO 10 i = 1 , 42
	xfit_temp(i) = xfit_parms(i)
10	CONTINUE
!
	DO 20 i = 1 , nterms
	b(i) = A1(i)
20	CONTINUE
!
	DO 30 i = 1 , nterms
	b(i) = A1(i) - deltaa(i)
	Call a_to_kfit(b, xfit_temp, idex_a, nterms)
!
	alow_val = functn2(chan_num, xfit_temp)
!
	b(i) = A1(i) + deltaa(i)
	Call a_to_kfit(b, xfit_temp, idex_a, nterms)
!
	ahigh_val = functn2(chan_num, xfit_temp)
!
	DERIV(I)=0.
	If (deltaa(i).NE.0)deriv(i) = (ahigh_val - alow_val) / 2.0 / deltaa(i)
	b(i) = A1(i)
!	
30	CONTINUE
!
	RETURN
	END
!
!	*********************************
!
!       M A T I N V
!
!       PURPOSE
!     INVERT A SYMETRIC MATRIX AND CALCULATE ITS DETERMINANT
!
!       USAGE
!     CALL MATINV (NORDER,DET)
!
!       DESCRIPTION OF PARAMETERS
!     ARRAY  -  INPUT MATRIX WHICH IS REPLACED BY ITS INVERSE
!     NORDER -  DEGREE OF MATRIX (ORDER OF DETERMINANT)
!     DET    -  DETERMINANT OF INPUT MATRIX
!
!       SUBROUTINES USED
!     NONE
!
!       COMMENTS
!
!       BAsedin large part on the Matinv routine in BEVINGTON PP.302-303.
!           FEB.  1978,   J.R.BOYCE
!
        SUBROUTINE MATINV(NORDR,DET, ARRAY)
        DOUBLE PRECISION ARRAY(42,42),AMAX,SAVE,A,B,BDUM(42)
        DIMENSION IK(42),JK(42)
!    COMMON/POLQ/BDUM,ARRAY
!       TYPE *,' $'
        DET=1.
        DO 100 K=1,NORDR
!-------FIND LARGEST ELEMENT ARRAY(I,J) IN REST OF MATRIX
        AMAX=0.D0
21      DO 30 I=K,NORDR
        DO 30 J=K,NORDR
        A=DABS(AMAX)
        B=DABS(ARRAY(I,J))
        IF(A-B) 24,24,30
24      AMAX=ARRAY(I,J)
        IK(K)=I
        JK(K)=J
30      CONTINUE
!-------INTERCHANGE ROWS AND COLLUMNS TO PUT AMAX IN ARRAY(K,K)
        IF(AMAX) 41,32,41
32      DET=0.
        RETURN
41      I=IK(K)
        IF (I-K) 21,51,43
43      DO 50 J=1,NORDR
        SAVE=ARRAY(K,J)
        ARRAY(K,J)=ARRAY(I,J)
50      ARRAY(I,J)=-SAVE
51      J=JK(K)
        IF(J-K) 21,61,53
53      DO 60 I=1,NORDR
        SAVE = ARRAY(I,K)
        ARRAY(I,K)=ARRAY(I,J)
60      ARRAY(I,J)=-SAVE
!-------ACCUMULATE ELEMENTS OF INVERSE MATRIX
61      DO 70 I=1,NORDR
        IF(I.EQ.K) GO TO 70
        ARRAY(I,K)=-ARRAY(I,K)/AMAX
70      CONTINUE
        DO 80 I=1,NORDR
        DO 80 J=1,NORDR
        IF(I.EQ.K) GO TO 80
        IF(J.EQ.K) GO TO 80
        ARRAY(I,J)=ARRAY(I,J) + ARRAY(I,K)*ARRAY(K,J)
80      CONTINUE
        DO 90 J=1,NORDR
        IF(J.EQ.K) GO TO 90
        ARRAY(K,J)= ARRAY(K,J)/AMAX
90      CONTINUE
        ARRAY(K,K)=1.D0/AMAX
100     DET=DET*SNGL(AMAX)
!     TYPE *,' $$'
!-------RESTORE ORDERING OF MATRIX
        DO 130 L=1,NORDR
        K=NORDR-L+1
        J=IK(K)
        IF(J.LE.K) GO TO 111
        DO 110 I=1,NORDR
        SAVE=ARRAY(I,K)
        ARRAY(I,K)=-ARRAY(I,J)
110     ARRAY(I,J)=SAVE
111     I=JK(K)
        IF(I.LE.K) GO TO 130
        DO 120 J=1,NORDR
        SAVE=ARRAY(K,J)
        ARRAY(K,J)=-ARRAY(I,J)
120     ARRAY(I,J)=SAVE
130     CONTINUE
        RETURN
        END
!
!	*********************************
!
!	*********************************
!
	Function czosnyka(energy, e0, lor_width, gauss_sig)
!
	DOUBLE PRECISION  czosnyka
	DOUBLE PRECISION ENERGY, E0, lor_width, gauss_sig
	DOUBLE PRECISION ka(3), la(3), ma(3), na(3)
	DOUBLE PRECISION A, V, V_SUM, X, Y, Z, TOT
!
	ka(1) = 0.0
	la(1) = 0.0
	ma(1) = 1.32272
	na(1) = 0.081905
!
	ka(2) = 1.09148
	la(2) = 0.090227
	ma(2) = 1.29081
	na(2) = 0.0093116
!
	ka(3) = 2.30556
	la(3) = 0.0035776
	ma(3) = 1.17417
	na(3) = -0.0116099
!
	a = lor_width / 2 / gauss_sig
	v = (energy - E0) / gauss_sig
	v_sum = 0.0
!
	DO 10 i = 1 ,3
	x = (ka(i) * la(i) + na(i) * (a + ma(i)))
	y = (ka(i)**2 + (a + ma(i))**2 + v**2)
	Z = (ka(i)**2 + (a + ma(i))**2 + v**2)**2-4*ka(i)**2*v**2
	tot = (x * y - 2 * ka(i) * la(i) * v**2) / Z
	v_sum = v_sum + tot
10	CONTINUE
!
	czosnyka = 2 * lor_width * v_sum / a / gauss_sig**2
	return
	end
!
!	*********************************
!
	Function tail_x(energy,e0,TAILS,Gauss_width)
!
!	Based on Ray Gunnink's representation of the tailing function
!
	DOUBLE PRECISION lo_tail_a,lo_tail_decay,fast_tail_a
	DOUBLE PRECISION TAIL_X, energy, e0, T_X, lo_lambda, high_lambda, t_x2
	DOUBLE PRECISION Gauss_width, TAILS(4), deltae, ALPHA, TAU1, beta, TAU2
	DOUBLE PRECISION g_width
!
	tail_x = 0.0D0
	t_x = 0.0
	t_x2 = 0.0
	g_width=Gauss_width
!
	lo_tail_a = dABS(TAILS(1))
	TAU1 = dABS(TAILS(2))
	fast_tail_a= dABS(TAILS(3))
	TAU2 = dabs(TAILS(4))
!
	beta=TAU1*(energy-e0)+0.5*(g_width*TAU1)**2		! primnary low side tail
!
	t_x = lo_tail_a * Exp(beta)
	beta= (energy-e0)/g_width+g_width*TAU1
	t_x = t_x * ERFC(beta/2**0.5)
!
5	beta=TAU2*(e0-energy)+0.5*(g_width*TAU2)**2		! changed to default to high side tail

	t_x2 = fast_tail_a * Exp(beta)
	beta= (e0-energy)/g_width+g_width*TAU2
	t_x2 = t_x2 * ERFC(beta/2**0.5)

10	continue
	tail_x = t_x + t_x2
101	continue
	return
	end
!
!	*********************************
!
	function char_x1(E_0, k)
 	double precision R, S_inv, f_abs, rho_z_bar, rho_zm, nu, U0, JAE, m_z
	double precision par_z, E_0, EX0(9), I0(9), par_A, z_k, b_k, NUM
	double precision omega, I_A, t, omega_k, p_k, const1, char_x1
	double precision xfit_consts(90), attn_fact_W
	DOUBLE PRECISION xdat(4100), scat_dat(4100)
	double precision Pi, phi, eps, xtemp
   	INTEGER i_conc(7), i_attn(7)
  	INTEGER k
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!

    COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts

    DATA EX0/59.318,57.981,57.425,67.244,69.067,66.950,62.273,67.685,69.484/
	DATA I0/0.47,0.274,0.000206,0.103,0.0358,0.0535,0.0006,0.00241,0.0051/
    par_z=74.0
    par_A=183.0
    omega_k=1.0
    omega=1.0
    const1=1.0
    I_A=15.0
    t=1.0
!
    z_k=2.0
    b_k=0.35
    JAE=0.135*par_z
    m_z=0.132-0.9211/par_z**0.5
    U0=E_0/EX0(k)
    nu=E_0**m_z*(0.1904-0.2236*log(par_z)+0.1292*(log(par_z))**2-0.0149*(log(par_z))**3)
    rho_zm=par_a/par_z*(0.787E-5*JAE**0.5*E_0**1.5 + 0.735E-6*E_0**2)
    rho_z_bar=rho_zm*log(U0)*(0.49269-0.10987*nu+0.78857*nu**2)/(0.70256-1.09865*nu+1.0046*nu**2+log(U0))
    S_inv=z_k*b_k/par_z*(U0*log(U0)+1-U0)*(1+16.05*sqrt(JAE/EX0(k))*(sqrt(U0)*log(U0)+2.0*(1-sqrt(U0)))/(U0*log(U0)+1.0-U0))
    R=1-0.0081517*par_z+3.613E-5*par_z**2+0.009583*par_z*exp(-U0)+0.001141*E_0
!
	Pi = 3.14159265358979
	phi = (90.0 - XFIT_CONSTS(24)) / 180.0 * Pi
	eps = XFIT_CONSTS(25) / 180.0 * Pi
    xtemp=1.0-EXP(-attn_fact_W(ex0(k))*2*RHO_Z_BAR*DSIN(PHI)/DSIN(EPS))
    f_abs=xtemp/(attn_fact_W(ex0(k))*2.0*rho_z_bar*DSIN(PHI)/DSIN(EPS))
!
    Num=const1*omega*I_A*t*R*S_inv*omega_k*I0(k)*f_abs
    char_x1=num
    return

    end
!
!	*********************************
!
Function brem_bkg3(energy, bkg_pars)
!
	DOUBLE PRECISION energy, bkg_pars(5), scal1, scal2, bkg, brem_bkg3


!	bkg_pars(1) = dabs(a(32))        ! spline ref energy 1
!	bkg_pars(2) = dabs(a(33))        ! spline ref energy 2
!	bkg_pars(4) = a(34)		 ! spline par A1
!	bkg_pars(5) = a(35)		 ! spline par A2
!	bkg_pars(3) = dabs(a(36))        ! spline decay constant


	scal1 = energy - bkg_pars(1)
	scal2 = energy - bkg_pars(2)

	bkg = 0
	If ((scal1 / bkg_pars(3)) .LT. 200) bkg = bkg_pars(4) / DCosh(scal1 / bkg_pars(3))
	If ((scal2 / bkg_pars(3)) .LT. 200) bkg = bkg + bkg_pars(5) / DCosh(scal2 / bkg_pars(3))

!	If ((scal1 ** 2 / bkg_pars(6)) .LT. 200) bkg = bkg + bkg_pars(7) / DCosh(scal1 ** 2 / bkg_pars(6))
!	If ((scal2 ** 2 / bkg_pars(6)) .LT. 200) bkg = bkg + bkg_pars(8) / DCosh(scal2 ** 2 / bkg_pars(6))

!	If ((scal1 ** 3 / bkg_pars(9)) .LT. 200) bkg = bkg + bkg_pars(10) / DCosh(scal1 ** 3 / bkg_pars(9))
!	If ((scal2 ** 3 / bkg_pars(9)) .LT. 200) bkg = bkg + bkg_pars(11) / DCosh(scal2 ** 3 / bkg_pars(9))

!	If ((scal1 ** 4 / bkg_pars(12)) .LT. 200) bkg = bkg + bkg_pars(13) / DCosh(scal1 ** 4 / bkg_pars(12))
!	If ((scal2 ** 4 / bkg_pars(12)) .LT. 200) bkg = bkg + bkg_pars(14) / DCosh(scal2 ** 4 / bkg_pars(12))
!
	brem_bkg3 = bkg
!
End	Function
!
!
!   	-----------------------------------------------------------------------------------------------------------
!   	-----------------------------------------------------------------------------------------------------------
!
Subroutine call_x_ratio_corr2(deltae, v_diameter, d_xrf, v_outer,xrf_abs)
!
! for solution concentration, U_conc and Pu_conc, and vial diameter v_d, adjust branching ratios for attenuation effects
! normalize to U K-alpha peak
!
!	Double Precision mu_u(10), mu_pu(10), mu_np(10), mu_cm(10), mu_am(10)
	Double Precision conc1(7), xrf_abs(10, 15)
	Double Precision test_trans(10), mu_matrix(5, 15), corr_matrix(5, 15), eff_matrix(5, 15), f_e(5, 15)
	DOUBLE PRECISION concentration(6), deltae
	DOUBLE PRECISION HPGE_eff, attn_fact_7_co
	DOUBLE PRECISION xfit_parms(42)
	DOUBLE PRECISION v_diameter, d_xrf, v_outer
	DOUBLE PRECISION utot, putot, nptot, cmtot, amtot
!
	DOUBLE PRECISION xfit_consts(90),XDAT(4100), scat_dat(4100)
	DOUBLE PRECISION raw_data(4100),RAND_TEMP(4100), ERROR1(4100)
	DOUBLE PRECISION concentrate(6), xrf_bkg(14)
	DOUBLE PRECISION xrf_e(10,15), xrf_rel(10,15), lor_w(10,15) 
	INTEGER i_conc(7), i_attn(7), element_peaks(7)
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!
!
	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts
	COMMON/XRFDAT2/RAW_DATA, RAND_TEMP, ERROR1
	COMMON/XRFDAT3/xrf_bkg, concentrate
	COMMON/XRFDAT4/xrf_e, xrf_rel, lor_w, element_peaks
!     
!    
!  Normalize branching ratios
!
	utot = 0
	putot = 0
	nptot = 0
	cmtot = 0
	amtot = 0
!
	Do 20 i = 1 , 15
	utot = utot + xrf_abs(1, i)
	putot = putot + xrf_abs(2, i)
	nptot = nptot + xrf_abs(3, i)
	cmtot = cmtot + xrf_abs(4, i)
	amtot = amtot + xrf_abs(5, i)
20	CONTINUE

	DO 30 i = 1 , 15
	xrf_rel(1, i) = xrf_abs(1, i) / utot
	xrf_rel(2, i) = xrf_abs(2, i) / putot
	xrf_rel(3, i) = xrf_abs(3, i) / nptot
	xrf_rel(4, i) = xrf_abs(4, i) / cmtot
	xrf_rel(5, i) = xrf_abs(5, i) / amtot
30	CONTINUE
!
!	adjust branching ratios for detector efficiency and attenuation
!
	kappa = xfit_consts(16)
!
!	v_outer = v_diameter / 2 - d_xrf
!	If(d_xrf .GT. v_diameter) d_xrf = v_diameter
!	If(v_outer .LT. 0) v_outer = 0

	GOTO 80   !    branching ratio corrections now performed during fit

	DO 40 i = 1 , 6
	i_conc(i) = 1
	concentration(i)=concentrate(i)
!	conc1(i) = 1
40	CONTINUE
!
!	Create matrix of macroscopic attenuation factors
!
	DO 50 L = 1 , 5					! for each of 5 elements
	DO 45 i = 1 , 15				! for each of 15 x-ray lines
!
        mu_matrix(L, i) =   attn_fact_7_co(xrf_e(L, i), deltae, concentration)    
        corr_matrix(L, i) = 1
        eff_matrix(L, i) = HPGE_eff(xrf_e(L, i))
        f_e(L, i) = Exp(-mu_matrix(L, i) * d_xrf)
!
45	CONTINUE
50	CONTINUE

	DO 60 L = 1 , 5
	DO 55 i = 1 , 15
        If (f_e(L, i) .NE. 0) corr_matrix(L, i) = -LOG(f_e(L, i) ** kappa)/(1 - f_e(L, i)**kappa) * Exp(-v_outer*mu_matrix(L, i))
55	CONTINUE
60	CONTINUE
!
	DO 70 L = 1 , 5
	DO 65 i = 1 , 15
!           xrf_rel(L, i) = xrf_rel(L, i) * corr_matrix(L, i) / corr_matrix(1, 1) * eff_matrix(L, i) / eff_matrix(1, 1)
           xrf_rel(L, i) = xrf_rel(L, i)  
65	CONTINUE
70	CONTINUE
80	continue
	return
!
End 
!
!   	---------------------------------------------------
!
Function g_smooth(energy, deltae, gauss_width, X)
!
!	Simple guassian smoothing of a spectral segment, X
!
	DOUBLE PRECISION energy, deltae, gauss_width, X(101), y, E0
	DOUBLE PRECISION g_smooth,  gaussian
!
	y = 0.0
	DO 10 i = 1 , 101
	E0 = energy + dfloat(51 - i) * deltae
	y = y + X(i) * gaussian(energy, E0, 1.0D0, gauss_width, deltae)
10	continue
!
	g_smooth = y  
!
End Function
!
!   	---------------------------------------------------
!
Function g_smooth2(energy, deltae, gauss_width, j, X)
!
	DOUBLE PRECISION energy, deltae, gauss_width, X(4100)
	DOUBLE PRECISION y, g_smooth2, E0, gaussian, tail
	INTEGER j, i

	y = 0.0
	DO 10, i= 1 , 71
	E0 = energy + (35 - i) * deltae

	If( i + j - 35 .LE. 4100) y = y + X(i + j - 35) * gaussian(energy, E0, 1.D0, gauss_width, deltae)

10	continue

	tail = 0.0

	g_smooth2 = y * deltae + tail
	RETURN
End
!
!   	---------------------------------------------------
!
Function E_KN(energy, angle)
!	Returns Klein-Nishina backscatter energy
	DOUBLE PRECISION E_KNR, energy, angle, e_m, cos_th
	e_m = 510.99906
	Pi = 3.1415926535898
	cos_th = DCOS(angle / 180.0 * Pi)
	E_KN = energy / (1.0D0 + (energy / e_m) * (1 - cos_th))

End Function
!
!   	---------------------------------------------------
!
Function E_KNR(energy, angle)
!	Returns inverse Klein-Nishina backscatter energy
	DOUBLE PRECISION E_KNR, energy, angle, e_m, Pi
	e_m = 510.99906
	Pi = 3.1415926535898
	E_KNR = energy / (1.0D0 - (energy / e_m) * (1.0D0 - DCOS(angle / 180.0 * Pi)))
End Function
!
!   	---------------------------------------------------
!
Function theta_KNR(E_s, E_0)
!	Returns inverse Klein-Nishina backscatter angle for incident energy E_0 and scatter energy E_s 
	DOUBLE PRECISION E_KNR, energy, angle, E_m, Pi, E_0, E_s, cos_theta, E_min, theta_KNR

	E_m = 510.99906
	Pi = 3.1415926535898
	theta_KNR = 0.0

	E_min = E_0 / (1.0 + 2.0 * (E_0 / E_m))
	if(E_s .LT. E_min) GOTO 10
	if(E_s .GT. E_0) GOTO 10
	cos_theta = (1.0 - (1.0 - E_s / E_0) * E_m / E_s)
	theta_KNR = DAcos(cos_theta) 

10	continue

End Function
!
!   	---------------------------------------------------
!
Function brem_bkg_new(energy, e_0, par_x, ebel_par, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)
!        
!	Calculates backscatter spectrum from the theorectical source term
!	ebel1 is the simple backscattered Bremmstrahlung spectrum
!	ebel2 is the elastically scattered Bremmstrahlung spectrum
!	The continuum representing multiple scatter events is approximated using a smoothed expontential spline
!

	DOUBLE PRECISION temp_array(101), ebel_par(14), concentration(6), temp_array2(101)
	DOUBLE PRECISION brem_I, ebel_I, tail_area, tau, deltae, dbin
	DOUBLE PRECISION e_0, em2, par_x, par_y, ang_1, ang_2, g_width, sol_thick, w_1, w_2
	DOUBLE PRECISION cd_thick, Vial_thick, Vial_Diameter, SS_thick, d_coll, Pi, xrf_ang
	DOUBLE PRECISION energy, e_temp, e_p,  attn, attn1, attn2, attn3, pol_thick
	DOUBLE PRECISION ebel1, ebel2, e_tail, spline, xrf_bkg_spline(5)
	DOUBLE PRECISION E_KNR, brem_bkg_new, g_smooth, ebel, HPGE_eff, tail_x2, brem_bkg3 	 ! Functions called by this routine
	DOUBLE PRECISION attn_fact_poly_co, attn_fact_7_co, attn_fact_ss_7, attn_fact_cd_7
	DOUBLE PRECISION scat_fact, scat_fact_7, g_width2, e_3, spline2, tail_2, tau_2, e_off
!
!	ebel_par(1) = g_width2
!	ebel_par(2) = not used
!	ebel_par(3) = brem shaping par a
!	ebel_par(4) = brem shaping par b (normally = 0)
!	ebel_par(5) = generator target angle 1
!	ebel_par(6) = generator target angle 2
!	ebel_par(7) = g_width  -  guassian width of smoothing function applied to scatter distribution
!	ebel_par(8) = not used
!	ebel_par(9) = X-ray backscatter angle (HPGE backward angle)
!	ebel_par(10) = Cd foil thickness
!	ebel_par(11) = Vial Wall thickness 
!	ebel_par(12) = Vial Diameter (inner diameter of the sample vial)
!	ebel_par(13) = stainless steel thickness (total between vial and detector)
!	ebel_par(14) = d_coll
!
	g_width2 = ebel_par(1)	      ! guassian width of smoothing function applied to elastic scatter distribution
	pol_thick = ebel_par(2)       ! not used 
	em_2 = 511.0D0		      ! electron rest mass
!	Par_x = ebel_par(3)	      ! bremsstrahlung distribution shapping parameter (free parameter)
	par_y = ebel_par(4)	      ! bremsstrahlung distribution shapping parameter (2nd order)
	ang_1 = ebel_par(5)           !  Generator internal angle
	ang_2 = ebel_par(6)           !  Generator internal angle
	g_width = ebel_par(7)         ! guassian width of smoothing function applied to inelastic scatter distribution
	e_off = ebel_par(8)	      ! energy offset for tailing function ( set = 0)
	w_1 = 1			      ! tungsten target atten coefficient weighting factor
	w_2 = 0			      ! tungsten target atten coefficient weighting factor
	xrf_ang = ebel_par(9)         ! HPGE backward angle
	cd_thick = ebel_par(10)       ! Cd foil thickness between generator and vial
	Vial_thick = ebel_par(11)     ! vial wall thickness
	Vial_diameter = ebel_par(12)  ! inner diameter of the sample vial
	SS_thick = ebel_par(13)	      ! stainless steel thickness (total between vial and detector)
	d_coll = ebel_par(14)	      ! XRF collimator diameter (cm)
!
	Pi = 3.1415926535898
!
	attn2 = Exp(-attn_fact_ss_7(energy, 1) * SS_thick / 0.866025 * 8.0) 			! adjust ss thickness for 30 degree angle
	attn2 = attn2 * Exp(-Vial_thick * 0.95 * attn_fact_poly_co(energy))
	attn2 =attn2*Exp(-(Vial_diameter/2.0)*attn_fact_7_co(energy, deltae, concentration))    ! attn for out going X-rays
!
	DO 10  i = 1 , 101									! loop to build temp array for gausssian smoothing
	  e_temp = energy + dfloat(i - 51) * deltae
	  e_p = E_KNR(e_temp, xrf_ang)
!
	attn1 = Exp(-attn_fact_ss_7(e_p, 1) * SS_thick * 8.0) * Exp(-attn_fact_cd_7(e_p,1) * cd_thick * 8.67) 
	attn1 = attn1*Exp(-Vial_thick * 0.95 * attn_fact_poly_co(e_p))
	attn1 = attn1*Exp(-(Vial_diameter/2.0)*attn_fact_7_co(e_p, deltae, concentration))	!  attn for incoming X-rays
!
	dbin=(E_KNR(e_temp+deltae/2, xrf_ang)-E_KNR(e_temp-deltae/2.0, xrf_ang))
	ebel1 = ebel(e_p, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)*attn2*attn1/dbin		!  inelastic backsatter X-rays
	temp_array(i)=ebel1
!
!	scat_fact = 1.0 -  DEXP(-d_coll*scat_fact_7(e_temp, concentration))			! elastic scatter macroscopic x-section
	scat_fact = d_coll*scat_fact_7(e_temp, concentration)	
!
	temp_array2(i) = scat_fact*ebel(e_temp, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)*attn2*attn2			! brem dist elastic scatter
!
10	continue
!
	ebel1=g_smooth(energy, deltae, g_width, temp_array) 				! brem dist inelastic scatter	
	ebel2=g_smooth(energy, deltae, g_width2, temp_array2)				! brem dist elastic scatter
!
	e_tail=E_KN(e_0, xrf_ang)-e_off						! spline based on hyperbolic cosine approach
	spline = brem_bkg3(energy, xrf_bkg_spline)					! bridge between inelastic and elastic 
!
	brem_bkg_new = HPGE_eff(energy) * (brem_I * (ebel1 + ebel_I * ebel2 + spline))
	brem_bkg_new=max(brem_bkg_new,0.0D0)
!
End Function
!
!   	---------------------------------------------------
!
Function brem_bkg_narrow(energy, e_0, par_x, ebel_par, brem_I, ebel_I, xrf_bkg_spline, concentration, deltae)
!        
!	Calculates backscatter spectrum from the theorectical source term
!	ebel1 is the simple backscattered Bremmstrahlung spectrum
!	ebel2 is the elastically scattered Bremmstrahlung spectrum
!	The continuum representing multiple scatter events is approximated using a smoothed expontential spline
!
!	This function does not have guassian broadening is used to estimate the step background contribution from
!	the Bremmstrahlung spectrum
!
	DOUBLE PRECISION temp_array(101), ebel_par(14), concentration(6), temp_array2(101)
	DOUBLE PRECISION brem_I, ebel_I, tail_area, tau, deltae, dbin
	DOUBLE PRECISION e_0, em2, par_x, par_y, ang_1, ang_2, g_width, sol_thick, w_1, w_2
	DOUBLE PRECISION cd_thick, Vial_thick, Vial_Diameter, SS_thick, d_coll, Pi, xrf_ang
	DOUBLE PRECISION energy, e_temp, e_p,  attn, attn1, attn2, attn3, pol_thick
	DOUBLE PRECISION ebel1, ebel2, e_tail, spline, xrf_bkg_spline(5)
	DOUBLE PRECISION E_KNR, brem_bkg_narrow, g_smooth, ebel, HPGE_eff, tail_x2, brem_bkg3 	 ! Functions called by this routine
	DOUBLE PRECISION attn_fact_poly_co, attn_fact_7_co, attn_fact_ss_7, attn_fact_cd_7
	DOUBLE PRECISION scat_fact, scat_fact_7, g_width2, e_3, spline2, tail_2, tau_2, e_off
!
!	ebel_par(1) = g_width2
!	ebel_par(2) = not used
!	ebel_par(3) = brem shaping par a
!	ebel_par(4) = brem shaping par b (normally = 0)
!	ebel_par(5) = generator target angle 1
!	ebel_par(6) = generator target angle 2
!	ebel_par(7) = g_width  -  guassian width of smoothing function applied to scatter distribution
!	ebel_par(8) = not used
!	ebel_par(9) = X-ray backscatter angle (HPGE backward angle)
!	ebel_par(10) = Cd foil thickness
!	ebel_par(11) = Vial Wall thickness 
!	ebel_par(12) = Vial Diameter (inner diameter of the sample vial)
!	ebel_par(13) = stainless steel thickness (total between vial and detector)
!	ebel_par(14) = d_coll
!
	g_width2 = ebel_par(1)	      ! guassian width of smoothing function applied to elastic scatter distribution
	em_2 = 511.0D0		      ! electron rest mass
!	Par_x = ebel_par(3)	      ! bremsstrahlung distribution shapping parameter (free parameter)
	par_y = ebel_par(4)	      ! bremsstrahlung distribution shapping parameter (2nd order)
	ang_1 = ebel_par(5)           !  Generator internal angle
	ang_2 = ebel_par(6)           !  Generator internal angle
	g_width = ebel_par(7)         ! guassian width of smoothing function applied to inelastic scatter distribution
	e_off = ebel_par(8)	      ! energy offset for tailing function ( set = 0)
	w_1 = 1			      ! tungsten target atten coefficient weighting factor
	w_2 = 0			      ! tungsten target atten coefficient weighting factor
	xrf_ang = ebel_par(9)         ! HPGE backward angle
	cd_thick = ebel_par(10)       ! Cd foil thickness between generator and vial
	Vial_thick = ebel_par(11)     ! vial wall thickness
	Vial_diameter = ebel_par(12)  ! inner diameter of the sample vial
	SS_thick = ebel_par(13)	      ! stainless steel thickness (total between vial and detector)
	d_coll = ebel_par(14)	      ! XRF collimator diameter (cm)
!
	Pi = 3.1415926535898
!
	attn2 = Exp(-attn_fact_ss_7(energy, 1) * SS_thick / 0.866025 * 8.0) 			! adjust ss thickness for 30 degree angle
	attn2 = attn2 * Exp(-Vial_thick * 0.95 * attn_fact_poly_co(energy))
	attn2 =attn2*Exp(-(Vial_diameter/2.0)*attn_fact_7_co(energy, deltae, concentration))    ! attn for out going X-rays
!
									! loop to build temp array for gausssian smoothing
	  e_temp = energy 
	  e_p = E_KNR(e_temp, xrf_ang)
!
	attn1 = Exp(-attn_fact_ss_7(e_p, 1) * SS_thick * 8.0) * Exp(-attn_fact_cd_7(e_p,1) * cd_thick * 8.67) 
	attn1 = attn1*Exp(-Vial_thick * 0.95 * attn_fact_poly_co(e_p))
	attn1 = attn1*Exp(-(Vial_diameter/2.0)*attn_fact_7_co(e_p, deltae, concentration))	!  attn for incoming X-rays
!
	dbin=(E_KNR(e_temp+deltae/2, xrf_ang)-E_KNR(e_temp-deltae/2.0, xrf_ang))
	ebel1 = ebel(e_p, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)*attn2*attn1/dbin		!  inelastic backsatter X-rays
!
!	scat_fact = 1.0 -  DEXP(-d_coll*scat_fact_7(e_temp, concentration))			! elastic scatter macroscopic x-section
	scat_fact = d_coll*scat_fact_7(e_temp, concentration)	
!
	ebel2 = scat_fact*ebel(e_temp, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)*attn2*attn2				! brem dist elastic scatter
!
10	continue
!
!
	e_tail=E_KN(e_0, xrf_ang)-e_off						! spline based on hyperbolic cosine approach
	spline = brem_bkg3(energy, xrf_bkg_spline)					! bridge between inelastic and elastic 
!
	brem_bkg_narrow = HPGE_eff(energy) * (brem_I * (ebel1 + ebel_I * ebel2 + spline))
!
End Function
!
!   	---------------------------------------------------
!
	Function ebel(energy, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2)
!
	DOUBLE PRECISION energy, e_0, Par_x, par_y, ang_1, ang_2, w_1, w_2
	DOUBLE PRECISION mu_z, F_ABS, I_A, par_nu, dn_de
	DOUBLE PRECISION d_e, cal_c, tim, omega, eps, phi
	DOUBLE PRECISION U_0, par_z, par_m, par_j, alnz
	DOUBLE PRECISION rho_zm, rho_zbar, attn_fact_W
	DOUBLE PRECISION ebel, attn_fact_W_PE2
	ebel = 0.0
	If (energy.GT.e_0) GoTo 100
	d_e = 0.09
	cal_c = 1.0
	tim = 1.0
	I_A = 15.0
	omega = (0.15 / 12)**2		!  aproximate solid angle of detector/colimator	

!	tungsten target
	par_z = 74.0
	par_a = 183.84
!
	Pi = 3.14159265358979
	phi = (90.0 - ang_1) / 180.0 * Pi
	eps = ang_2 / 180.0 * Pi
!
	par_m = 0.1382 - 0.9211 / par_z**0.5
	par_j = 0.0135 * par_z
	U_0 = e_0 / energy
!
	alnz=dlog(par_z)		! dlog = natural log
!
	par_nu=(e_0**par_m)*(0.1904-0.2236*alnz+0.1292*alnz**2-0.0149*alnz**3)
!
	rho_zm = par_a / par_z*(0.00000787 * par_j**0.5*e_0**1.5 + 0.000000735 * e_0**2)
!
	rho_zbar=rho_zm*(0.49269-1.0987*par_nu+0.78557*par_nu**2)*dlog(U_0)
	rho_zbar=rho_zbar/(0.70256-1.09865*par_nu+1.0046*par_nu**2+dlog(U_0))
!
	mu_z = attn_fact_W_PE2(energy, w_1, w_2) 
	f_abs=(1-Exp(-mu_z*2*rho_zbar*DSin(phi)/DSin(eps)))/(mu_z*2*rho_zbar*DSin(phi)/DSin(eps))
!
	dn_de=cal_c*omega*I_A*tim*par_z*(e_0/energy-1.0)**Par_x*f_abs*d_e
!
	ebel = dn_de * (e_0 / energy)**par_y
100	continue
!
End FUNCTION 
!
!   	---------------------------------------------------
!
Function tail_x2(energy, E0, tail_area, tail_decay, gauss_width, deltae)
	DOUBLE PRECISION energy, E0, tail_area, tail_decay, gauss_width, deltae
	DOUBLE PRECISION tx, tx_2, g_width, tau1, tau2, BETA, del_e
	DOUBLE PRECISION tail_x2
!
	tail_x2 = 0.0D0
	t_x = 0.0D0
	t_x2 = 0.0D0
!
	g_width = gauss_width
	tau2 = Abs(tail_decay)
!
	del_e = E0 - energy
	BETA = del_e * tau2 + 0.5 * (g_width * tau2) ** 2
	t_x2 = tail_area * DExp(BETA)
	BETA = del_e / g_width + g_width * tau2
	t_x2 = t_x2 * DErFC(BETA / 3.0)
!
	tail_x2 =  t_x2
!
End Function
!
!	*********************************
!
Subroutine read_amu_values()
!
!	Reads in attenuation coefficient parameters from file MEKED_attn_coeffs.txt
!
	DOUBLE PRECISION amu_lib(10,8), cmu_lib(2,10), sol_lib(2,10), edges(10), misc_lib(6,10), misc_edges(6)
	DOUBLE PRECISION sol_edges(2), cont_edges(2), lor_widths(10), act_norm_factor(10), energy
	CHARACTER FILNAME*60, FILNAM1*60 
	COMMON/attn_parms/amu_lib, cmu_lib, sol_lib, edges, sol_edges, cont_edges, lor_widths, act_norm_factor, misc_lib, misc_edges
	integer i, j, l1,l2
	REAL a1, b1, c1

    FILNAME='MEKED_attn_coeffs.txt'
!
    open (unit=10,file=FILNAME)
!
    read (10,*)FILNAM1

!	Get actinide attenuation coefficient parameters, amu_lib
	do 1111 l1 = 1, 10
	do 1111 l2 = 1, 8
	read (10,*) a1, b1, c1
	amu_lib(l1, l2)=c1
1111	continue

!	Get actinide K-Edge energies, edges  
	do 1112 l1 = 1, 10
	read (10,*) a1, edges(l1)
1112	continue

!	Get lorentzian widths to broaden the K-edge transistion, lor_widths
	do 1113 l1 = 1, 10
	read (10,*) a1, lor_widths(l1)
1113	continue

!	Get container attenuation coefficient parameters, cmu_lib
	do 1114 l1 = 1, 2
	do 1114 l2 = 1, 11
	read (10,*) a1, b1, c1
	if(l2.EQ.11) cont_edges(l1)=c1
	if(l2.LT.11) cmu_lib(l1,l2)=c1
1114	continue

!	Get solution attenuation coefficient parameters, sol_lib
	do 1115 l1 = 1, 2
	do 1115 l2 = 1, 11
	read (10,*) a1, b1, c1
	if(l2.EQ.11) sol_edges(l1)=c1
	if(l2.LT.11) sol_lib(l1,l2)=c1

1115	continue

!	Get attenuation factor correction factors, act_norm_factor
	do 1116 l1=1, 10
	read (10,*) a1, c1
	act_norm_factor(l1)=c1
1116	continue

!	Read attenuation factor correction factors errors, act_norm_factor
	do 1117 l1=1, 10
	read (10,*) a1, c1
1117	continue
!
!	Get misc attenuation coefficient parameters, misc_lib
	do 1118 l1 = 1, 3
	do 1118 l2 = 1, 11
	read (10,*) a1, b1, c1
	if(l2.EQ.11) misc_edges(l1)=c1
	if(l2.LT.11) misc_lib(l1,l2)=c1
	
1118	continue

    close (unit=10)

	RETURN
	END
!
!	*********************************
!
!	*********************************
!
	Function W_VOIGT(energy,Gauss_width,tails,deltae)
!
!   Creates tungsten X-ray peaks
!	The relative peak intensities will not be very accurate because the source distribution/attenuation in not defined.
!	Estimation of the true coincidence summing peaks will be further complicated by the source distribution
!
	DOUBLE PRECISION ENERGY, GAUSS_WIDTH, E0(12), I0(12),lor_w(12)
	DOUBLE PRECISION lo_tail_a,lo_tail_decay,fast_tail_a, czosnyka
	DOUBLE PRECISION deltae,gauss_width_0, Y_TEMP, LOR_WIDTH, tails(4)
	DOUBLE PRECISION TAIL, E_TEMP,fast_tail_decay, E_1, char_x1, tail_x
!
 	DATA E0/59.318, 57.981, 57.425, 67.244, 69.067, 66.950, 69.273, 67.685, 69.484, 68.82, 80.0, 80.1/
! 	DATA E0/59.318, 57.981, 57.425, 67.235, 69.088, 75.543, 77.042, 78.846,  0, 0, 0, 0/
	DATA lor_w/0.0432,0.0374,0.043,0.0486,0.05,0.05,0.05,0.05,0.05,0.05,0.05,0.05/
!   
!
! 	 DATA I0/0.3769, 0.2197, 0.0001, 0.0826, 0.0287, 0.0429, 0.0004, 0.0019, 0.004, 0.0265, 0.0634, 0.0305/
!	 DATA I0/0.47,0.274,0.000206,0.103,0.0358,0.0535,0.0006,0.00241,0.0051,0.0, 0.0, 0.0/
! 	 DATA I0/0.37699, 0.21978, 0.00017, 0.23239, 0.07365, 0.0, 0.0, 0.0,  0.0, 0.0, 0.0, 0.0/
	 DATA I0/0.57, 0.25818, 0.00018, 0.1785625, 0.0665375, 0.0916875, 0.0011125, 0.0042375, 0.009625, 0.0773 , 0.0, 0.0/

	W_VOIGT = 0.0
	y_temp = 0.0
	tail = 0.0
	npks=10

!
	do 10 j = 1 , npks
	gauss_width_0 = (e0(j) / 88.0)**0.5 * Gauss_width
	e_temp = e0(j)
!	
	lor_width = lor_w(j)
	y_temp = i0(j) * czosnyka(energy,e_temp,lor_width,gauss_width_0)
!
	tail = 0
	DO 5 i = 16 , 16
	e_1 = e0(j) + FLOAT(16 - i) * deltae
	tail=tail+ tail_x(e_1,energy,TAILS,gauss_width_0)

5	CONTINUE
	tail = i0(j) * tail
!
	W_VOIGT = W_VOIGT + y_temp    !  + tail
!
10	continue
!
	return
	end
!
!	*********************************
!
	Function PB_VOIGT(energy,Gauss_width,tails,deltae)
!
!       Creates tungsten X-ray peaks
!	The relative peak intensities will not be very accurate because the source distribution/attenuation in not defined.
!	Estimation of the true coincidence summing peaks will be further complicated by the source distribution
!
	DOUBLE PRECISION ENERGY, GAUSS_WIDTH, E0(12), I0(12),lor_w(12)
	DOUBLE PRECISION lo_tail_a,lo_tail_decay,fast_tail_a, czosnyka
	DOUBLE PRECISION deltae,gauss_width_0, Y_TEMP, LOR_WIDTH, tails(4)
	DOUBLE PRECISION TAIL, E_TEMP,fast_tail_decay, E_1, char_x1, tail_x
!
 	DATA E0/74.969, 72.805, 72.144, 84.938, 87.300, 84.450, 87.580, 85.470, 87.911, 88.003, 0.0, 0.0/
	DATA lor_w/0.066,0.068,0.068,0.068,0.068,0.068,0.068,0.068,0.068,0.068,0.068,0.068/
	DATA I0/0.462, 0.277, 0.000428, 0.1070, 0.0391, 0.0558, 0.0009, 0.00312, 0.0070, 0.000165, 0.0, 0.0/

	PB_VOIGT = 0.0
	y_temp = 0.0
	tail = 0.0
	npks=10
!
	do 10 j = 1 , npks
	gauss_width_0 = (e0(j) / 88.0)**0.5 * Gauss_width
	e_temp = e0(j)
!	
	lor_width = lor_w(j)
	y_temp = i0(j) * czosnyka(energy,e_temp,lor_width,gauss_width_0)
!
	tail = 0
	DO 5 i = 16 , 16
	e_1 = e0(j) + FLOAT(16 - i) * deltae
	tail=tail+ tail_x(e_1,energy,TAILS,gauss_width_0)

5	CONTINUE
	tail = i0(j) * tail
!
	PB_VOIGT = PB_VOIGT + y_temp    !  + tail
!
10	continue
!
	return
	end
!
!	*********************************
!
	Function Bi_VOIGT(energy,Gauss_width,tails,deltae)
!
!       Creates tungsten X-ray peaks
!	The relative peak intensities will not be very accurate because the source distribution/attenuation in not defined.
!	Estimation of the true coincidence summing peaks will be further complicated by the source distribution
!
	DOUBLE PRECISION ENERGY, GAUSS_WIDTH
	DOUBLE PRECISION lo_tail_a,lo_tail_decay,fast_tail_a, czosnyka
	DOUBLE PRECISION deltae,gauss_width_0, Y_TEMP, LOR_WIDTH, tails(4)
	DOUBLE PRECISION TAIL, E_TEMP,fast_tail_decay, E_1,  tail_x
	DOUBLE PRECISION xrf_e_bi(12), xrf_abs_bi(12), lor_w_bi(12)
!
!        Bi xrf energies
        xrf_e_bi(1) = 77.107    !  Ka1
        xrf_e_bi(2) = 74.815    !  Ka2
        xrf_e_bi(3) = 74.138    !  Ka3
        xrf_e_bi(4) = 87.349    !  kb1
        xrf_e_bi(5) = 89.784    !  kb2
        xrf_e_bi(6) = 86.830    !  Kb3
        xrf_e_bi(7) = 90.074    !  kb4
        xrf_e_bi(8) = 87.892    !  kb5
        xrf_e_bi(9) = 90.421    !  KO23
        xrf_e_bi(10) = 90.522   !  KP23

!         Bi xrf yields
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

!        Bi lorenztian widths
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
!
	Bi_VOIGT = 0.0
	y_temp = 0.0
	tail = 0.0
	npks=10
!
	do 10 j = 1 , npks
	gauss_width_0 = (xrf_e_bi(j) / 88.0)**0.5 * Gauss_width
	e_temp = xrf_e_bi(j)
!	
	lor_width = lor_w_bi(j)
	y_temp = xrf_abs_bi(j) * czosnyka(energy,e_temp,lor_width,gauss_width_0)
!
	tail = 0
	DO 5 i = 16 , 16
	e_1 = xrf_e_bi(j) + FLOAT(16 - i) * deltae
	tail=tail+ tail_x(e_1,energy,TAILS,gauss_width_0)

5	CONTINUE
	tail = xrf_abs_bi(j) * tail
!
	Bi_VOIGT = Bi_VOIGT + y_temp    !  + tail
!
10	continue
!
	return
	end
!
!	*********************************


!	*********************************
!
        SUBROUTINE Est_error(xfit_parms, xfit_errors, m_dex)
!
!	Adjust uncertainties to compensate for collapse in curfit
!
    DOUBLE PRECISION functn2, FCHISQ1, chi_init
    DOUBLE PRECISION xfit_parms(42), xfit_errors(42), TEMP_ARRAY(201)
    DOUBLE PRECISION chi_new, chi_target, YDAT(4100), WEIGHT(4100),YFIT(4100), YFIT1(4100)
    DOUBLE PRECISION xfit_consts(90),XDAT(4100), scat_dat(4100)
    INTEGER chan_num, CHAN_1, i_conc(7), I_attn(7)
    INTEGER NMIN, NMAX, i_dex, j, num_free, j_dex, iiii, m_dex
	DOUBLE PRECISION FISS_GAMMA_E(3,10), FISS_GAMMA_I(3,10)
	INTEGER gamma_pts(5)
!

	COMMON/XRFDAT6/YDAT, WEIGHT
	COMMON/XRFDAT/xfit_consts,i_conc,i_attn,xdat,scat_dat, FISS_GAMMA_E, FISS_GAMMA_I,gamma_pts

!
	NMIN=INT((xfit_consts(9)-xfit_parms(15))/xfit_parms(16))
	NMAX=INT((xfit_consts(10)-xfit_parms(15))/xfit_parms(16))
	npts=1+NMAX-NMIN
	num_free=npts-20

        DO 5 i=1,42
	TEMP_ARRAY(i)=XFIT_PARMS(i)
5        continue
	
  	DO 10 i = NMIN , NMAX
        yfit(i) = functn2(i, xfit_parms)
	jjjj=1
10	continue

	chi_init = FCHISQ1(ydat, weight, npts, num_free, NMIN, yfit)
	chi_target=(1.0+chi_init**2)**0.5

	j=1
	j_dex=1
	DO 100 j=1,30			! max tries = 30

	do 30 i_dex=1, 42
	xfit_parms(i_dex)=TEMP_ARRAY(i_dex)+xfit_errors(i_dex)*float(j)
30	continue

	j_dex=j
  	DO 50 k = NMIN , NMAX
50      yfit1(k) = functn2(k,xfit_parms)

        chi_new=FCHISQ1(ydat, weight, npts, num_free, NMIN, yfit1)

	if(chi_new .GE. chi_target) GOTO 110
100	continue

110	continue

        DO 1000 i=1,36
	m_dex=j_dex-1
	if (j_dex .LE. 1) m_dex=1
	xfit_errors(i)=xfit_errors(i)*float(m_dex)
1000	xFIT_PARMS(i)=TEMP_ARRAY(i)


	return
	end
!
