<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MEXRF_main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListBox3 As System.Windows.Forms.ListBox
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MEXRF_main))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.SysParToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VMSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AttenuationCoefficientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XrayDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.WhoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SummaryREsultsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurePassiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MiscFfunctionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertCNFToSPEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateSummedSpectrumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubtractPassiveSpectrumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetUpPassiveCorrectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.filenamebox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.RunPlease = New System.Windows.Forms.Button()
        Me.sampleinfoBox4 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox3 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox2 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SampleIDBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.select_files_Button = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Cycle_number_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox2 = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox3 = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox4 = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox5 = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox6 = New System.Windows.Forms.MaskedTextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.U_conc_xrf_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Pu_conc_xrf_box = New System.Windows.Forms.MaskedTextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Get_KED_Results = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.sampleinfoBox5 = New System.Windows.Forms.MaskedTextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.clear_ked_input = New System.Windows.Forms.Button()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.use_MEXRF_box = New System.Windows.Forms.CheckBox()
        Me.ref_el_Pu_button = New System.Windows.Forms.RadioButton()
        Me.ref_el_U_button = New System.Windows.Forms.RadioButton()
        Me.KED_check_box_8 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_8 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_7 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_7 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_6 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_1 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_5 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_2 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_4 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_3 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_3 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_4 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_2 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_5 = New System.Windows.Forms.MaskedTextBox()
        Me.KED_check_box_1 = New System.Windows.Forms.CheckBox()
        Me.KED_Box_6 = New System.Windows.Forms.MaskedTextBox()
        Me.U_KA1_box = New System.Windows.Forms.MaskedTextBox()
        Me.PU_KA1_box = New System.Windows.Forms.MaskedTextBox()
        Me.vms_U_Pu_count_ratio_box = New System.Windows.Forms.MaskedTextBox()
        Me.VMS_U_Pu_ratio_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.U_KA2_box = New System.Windows.Forms.MaskedTextBox()
        Me.U_Ka2_seed_counts_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Pu_seed_counts_box = New System.Windows.Forms.MaskedTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cd_prelim_area_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.U_Ka1_seed_counts_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Pu_Ka1_ROI2_box = New System.Windows.Forms.MaskedTextBox()
        Me.Pu_Ka1_ROI1_box = New System.Windows.Forms.MaskedTextBox()
        Me.U_Ka2_ROI2_box = New System.Windows.Forms.MaskedTextBox()
        Me.U_Ka1_ROI2_box = New System.Windows.Forms.MaskedTextBox()
        Me.U_Ka2_ROI1_box = New System.Windows.Forms.MaskedTextBox()
        Me.U_Ka1_ROI1_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.nchan_checkbox = New System.Windows.Forms.CheckBox()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Display_E_button = New System.Windows.Forms.RadioButton()
        Me.Display_chan_button = New System.Windows.Forms.RadioButton()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GammarayDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        ListBox3 = New System.Windows.Forms.ListBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox3
        '
        ListBox3.BackColor = System.Drawing.SystemColors.Window
        ListBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        ListBox3.CausesValidation = False
        ListBox3.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ListBox3.ItemHeight = 23
        ListBox3.Items.AddRange(New Object() {"Cycle # :", "Vial Diameter :  ", "Sample Temperature :  ", "U235 enrichment :  ", "Pu atomic weight :  ", "Live Time :"})
        ListBox3.Location = New System.Drawing.Point(2, 5)
        ListBox3.MinimumSize = New System.Drawing.Size(0, 160)
        ListBox3.Name = "ListBox3"
        ListBox3.Size = New System.Drawing.Size(247, 138)
        ListBox3.TabIndex = 34
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(140, 391)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(106, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Show File Name"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.SummaryREsultsToolStripMenuItem, Me.ConfigurePassiveToolStripMenuItem, Me.MiscFfunctionsToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1370, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "First Menu"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox1, Me.SysParToolStripMenuItem, Me.VMSToolStripMenuItem, Me.AttenuationCoefficientsToolStripMenuItem, Me.XrayDataToolStripMenuItem, Me.GammarayDataToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(78, 20)
        Me.ToolStripMenuItem1.Text = "Parameters"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 23)
        Me.ToolStripTextBox1.Text = "Free"
        '
        'SysParToolStripMenuItem
        '
        Me.SysParToolStripMenuItem.Name = "SysParToolStripMenuItem"
        Me.SysParToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.SysParToolStripMenuItem.Text = "System"
        '
        'VMSToolStripMenuItem
        '
        Me.VMSToolStripMenuItem.Name = "VMSToolStripMenuItem"
        Me.VMSToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.VMSToolStripMenuItem.Text = "VMS"
        '
        'AttenuationCoefficientsToolStripMenuItem
        '
        Me.AttenuationCoefficientsToolStripMenuItem.Name = "AttenuationCoefficientsToolStripMenuItem"
        Me.AttenuationCoefficientsToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.AttenuationCoefficientsToolStripMenuItem.Text = "Attenuation Coefficients"
        '
        'XrayDataToolStripMenuItem
        '
        Me.XrayDataToolStripMenuItem.Name = "XrayDataToolStripMenuItem"
        Me.XrayDataToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.XrayDataToolStripMenuItem.Text = "X-ray Data"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WhoToolStripMenuItem})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(93, 20)
        Me.ToolStripMenuItem2.Text = "Configuration"
        '
        'WhoToolStripMenuItem
        '
        Me.WhoToolStripMenuItem.Name = "WhoToolStripMenuItem"
        Me.WhoToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.WhoToolStripMenuItem.Text = "set file paths"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(56, 20)
        Me.ToolStripMenuItem3.Text = "Results"
        '
        'SummaryREsultsToolStripMenuItem
        '
        Me.SummaryREsultsToolStripMenuItem.Name = "SummaryREsultsToolStripMenuItem"
        Me.SummaryREsultsToolStripMenuItem.Size = New System.Drawing.Size(110, 20)
        Me.SummaryREsultsToolStripMenuItem.Text = "Summary Results"
        '
        'ConfigurePassiveToolStripMenuItem
        '
        Me.ConfigurePassiveToolStripMenuItem.Name = "ConfigurePassiveToolStripMenuItem"
        Me.ConfigurePassiveToolStripMenuItem.Size = New System.Drawing.Size(113, 20)
        Me.ConfigurePassiveToolStripMenuItem.Text = "Configure Passive"
        '
        'MiscFfunctionsToolStripMenuItem
        '
        Me.MiscFfunctionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConvertCNFToSPEToolStripMenuItem, Me.CreateSummedSpectrumToolStripMenuItem, Me.SubtractPassiveSpectrumToolStripMenuItem, Me.SetUpPassiveCorrectionToolStripMenuItem})
        Me.MiscFfunctionsToolStripMenuItem.Name = "MiscFfunctionsToolStripMenuItem"
        Me.MiscFfunctionsToolStripMenuItem.Size = New System.Drawing.Size(102, 20)
        Me.MiscFfunctionsToolStripMenuItem.Text = "Misc. Functions"
        '
        'ConvertCNFToSPEToolStripMenuItem
        '
        Me.ConvertCNFToSPEToolStripMenuItem.Name = "ConvertCNFToSPEToolStripMenuItem"
        Me.ConvertCNFToSPEToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.ConvertCNFToSPEToolStripMenuItem.Text = "Convert CNF to SPE"
        '
        'CreateSummedSpectrumToolStripMenuItem
        '
        Me.CreateSummedSpectrumToolStripMenuItem.Name = "CreateSummedSpectrumToolStripMenuItem"
        Me.CreateSummedSpectrumToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.CreateSummedSpectrumToolStripMenuItem.Text = "Create Summed Spectrum"
        '
        'SubtractPassiveSpectrumToolStripMenuItem
        '
        Me.SubtractPassiveSpectrumToolStripMenuItem.Name = "SubtractPassiveSpectrumToolStripMenuItem"
        Me.SubtractPassiveSpectrumToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.SubtractPassiveSpectrumToolStripMenuItem.Text = "Subtract Passive Spectrum"
        '
        'SetUpPassiveCorrectionToolStripMenuItem
        '
        Me.SetUpPassiveCorrectionToolStripMenuItem.Name = "SetUpPassiveCorrectionToolStripMenuItem"
        Me.SetUpPassiveCorrectionToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.SetUpPassiveCorrectionToolStripMenuItem.Text = "Configure Passive Correction"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        Me.AboutToolStripMenuItem.ToolTipText = resources.GetString("AboutToolStripMenuItem.ToolTipText")
        '
        'BackgroundWorker1
        '
        '
        'filenamebox1
        '
        Me.filenamebox1.Enabled = False
        Me.filenamebox1.Location = New System.Drawing.Point(103, 249)
        Me.filenamebox1.Name = "filenamebox1"
        Me.filenamebox1.Size = New System.Drawing.Size(744, 20)
        Me.filenamebox1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button1.Location = New System.Drawing.Point(12, 137)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 33)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Execute Fit"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Alignment = System.Drawing.StringAlignment.Far
        Legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(12, 221)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.CustomProperties = "IsXAxisQuantitative=True"
        Series1.Legend = "Legend1"
        Series1.Name = "Raw_Counts"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(1039, 459)
        Me.Chart1.TabIndex = 7
        Me.Chart1.Text = "Input Spectrum"
        '
        'RunPlease
        '
        Me.RunPlease.Location = New System.Drawing.Point(87, 441)
        Me.RunPlease.Name = "RunPlease"
        Me.RunPlease.Size = New System.Drawing.Size(75, 23)
        Me.RunPlease.TabIndex = 0
        Me.RunPlease.Text = "Run VB"
        Me.RunPlease.UseVisualStyleBackColor = True
        '
        'sampleinfoBox4
        '
        Me.sampleinfoBox4.Location = New System.Drawing.Point(170, 103)
        Me.sampleinfoBox4.Name = "sampleinfoBox4"
        Me.sampleinfoBox4.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox4.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.sampleinfoBox4, "Value in AMU")
        '
        'sampleinfoBox3
        '
        Me.sampleinfoBox3.Location = New System.Drawing.Point(170, 77)
        Me.sampleinfoBox3.Name = "sampleinfoBox3"
        Me.sampleinfoBox3.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox3.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.sampleinfoBox3, "Value in wt%")
        '
        'sampleinfoBox2
        '
        Me.sampleinfoBox2.Location = New System.Drawing.Point(170, 53)
        Me.sampleinfoBox2.Name = "sampleinfoBox2"
        Me.sampleinfoBox2.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox2.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.sampleinfoBox2, "Sample Vial temperature at time of assay in degrees C.")
        '
        'sampleinfoBox1
        '
        Me.sampleinfoBox1.Location = New System.Drawing.Point(170, 28)
        Me.sampleinfoBox1.Name = "sampleinfoBox1"
        Me.sampleinfoBox1.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox1.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.sampleinfoBox1, "KED sample vial diameter/thickness in cm")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(835, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Sample info from data file"
        '
        'SampleIDBox1
        '
        Me.SampleIDBox1.Enabled = False
        Me.SampleIDBox1.Location = New System.Drawing.Point(103, 223)
        Me.SampleIDBox1.Name = "SampleIDBox1"
        Me.SampleIDBox1.Size = New System.Drawing.Size(171, 20)
        Me.SampleIDBox1.TabIndex = 40
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 225)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Sample ID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 250)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 16)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "File Name:"
        '
        'select_files_Button
        '
        Me.select_files_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.select_files_Button.Location = New System.Drawing.Point(12, 46)
        Me.select_files_Button.Name = "select_files_Button"
        Me.select_files_Button.Size = New System.Drawing.Size(129, 23)
        Me.select_files_Button.TabIndex = 43
        Me.select_files_Button.Text = "Select XRF Files"
        Me.ToolTip1.SetToolTip(Me.select_files_Button, "Reads in 1 to 16 files" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "File formats accepted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     CNF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     SPE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     RES  (MEK" &
        "ED results file)")
        Me.select_files_Button.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(169, 46)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(623, 134)
        Me.ListBox1.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.ListBox1, "Select the file to diplay parameters and plot the spectrum")
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(12, 88)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(129, 23)
        Me.Button6.TabIndex = 46
        Me.Button6.Text = "Clear Selected Files"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Cycle_number_Box
        '
        Me.Cycle_number_Box.Location = New System.Drawing.Point(170, 5)
        Me.Cycle_number_Box.Name = "Cycle_number_Box"
        Me.Cycle_number_Box.Size = New System.Drawing.Size(79, 20)
        Me.Cycle_number_Box.TabIndex = 47
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(222, 20)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Override Data File Parameters"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Location = New System.Drawing.Point(130, 41)
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(79, 20)
        Me.MaskedTextBox1.TabIndex = 50
        '
        'MaskedTextBox2
        '
        Me.MaskedTextBox2.Location = New System.Drawing.Point(130, 67)
        Me.MaskedTextBox2.Name = "MaskedTextBox2"
        Me.MaskedTextBox2.Size = New System.Drawing.Size(79, 20)
        Me.MaskedTextBox2.TabIndex = 51
        '
        'MaskedTextBox3
        '
        Me.MaskedTextBox3.Location = New System.Drawing.Point(130, 93)
        Me.MaskedTextBox3.Name = "MaskedTextBox3"
        Me.MaskedTextBox3.Size = New System.Drawing.Size(79, 20)
        Me.MaskedTextBox3.TabIndex = 52
        '
        'MaskedTextBox4
        '
        Me.MaskedTextBox4.Location = New System.Drawing.Point(130, 119)
        Me.MaskedTextBox4.Name = "MaskedTextBox4"
        Me.MaskedTextBox4.Size = New System.Drawing.Size(79, 20)
        Me.MaskedTextBox4.TabIndex = 53
        '
        'MaskedTextBox5
        '
        Me.MaskedTextBox5.Location = New System.Drawing.Point(130, 145)
        Me.MaskedTextBox5.Name = "MaskedTextBox5"
        Me.MaskedTextBox5.Size = New System.Drawing.Size(79, 20)
        Me.MaskedTextBox5.TabIndex = 54
        '
        'MaskedTextBox6
        '
        Me.MaskedTextBox6.Location = New System.Drawing.Point(130, 168)
        Me.MaskedTextBox6.Name = "MaskedTextBox6"
        Me.MaskedTextBox6.Size = New System.Drawing.Size(79, 20)
        Me.MaskedTextBox6.TabIndex = 55
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(7, 43)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(113, 17)
        Me.CheckBox1.TabIndex = 56
        Me.CheckBox1.Text = "Energy Offet (keV)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(7, 69)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(117, 17)
        Me.CheckBox2.TabIndex = 57
        Me.CheckBox2.Text = "Energy Slope (keV)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(7, 95)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(111, 17)
        Me.CheckBox3.TabIndex = 58
        Me.CheckBox3.Text = "Vial Diameter (cm)"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(7, 121)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox4.TabIndex = 59
        Me.CheckBox4.Text = "Sample Tempeature"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(7, 147)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(107, 17)
        Me.CheckBox5.TabIndex = 60
        Me.CheckBox5.Text = "U235 enrichment"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(7, 170)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(111, 17)
        Me.CheckBox6.TabIndex = 61
        Me.CheckBox6.Text = "Pu Atomic Weight"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.CheckBox6)
        Me.Panel1.Controls.Add(Me.MaskedTextBox1)
        Me.Panel1.Controls.Add(Me.CheckBox5)
        Me.Panel1.Controls.Add(Me.MaskedTextBox2)
        Me.Panel1.Controls.Add(Me.CheckBox4)
        Me.Panel1.Controls.Add(Me.MaskedTextBox3)
        Me.Panel1.Controls.Add(Me.CheckBox3)
        Me.Panel1.Controls.Add(Me.MaskedTextBox4)
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Controls.Add(Me.MaskedTextBox5)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.MaskedTextBox6)
        Me.Panel1.Location = New System.Drawing.Point(1080, 223)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(226, 217)
        Me.Panel1.TabIndex = 62
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1199, 446)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(107, 24)
        Me.Button2.TabIndex = 63
        Me.Button2.Text = "prelim e_cal"
        Me.ToolTip1.SetToolTip(Me.Button2, resources.GetString("Button2.ToolTip"))
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(371, 5)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(107, 25)
        Me.Button5.TabIndex = 64
        Me.Button5.Text = "prelim peak areas"
        Me.ToolTip1.SetToolTip(Me.Button5, resources.GetString("Button5.ToolTip"))
        Me.Button5.UseVisualStyleBackColor = True
        '
        'U_conc_xrf_box
        '
        Me.U_conc_xrf_box.Location = New System.Drawing.Point(559, 149)
        Me.U_conc_xrf_box.Name = "U_conc_xrf_box"
        Me.U_conc_xrf_box.Size = New System.Drawing.Size(79, 20)
        Me.U_conc_xrf_box.TabIndex = 65
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(497, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "[U] (g/L)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(497, 176)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 16)
        Me.Label6.TabIndex = 68
        Me.Label6.Text = "[Pu] (g/L)"
        '
        'Pu_conc_xrf_box
        '
        Me.Pu_conc_xrf_box.Location = New System.Drawing.Point(559, 175)
        Me.Pu_conc_xrf_box.Name = "Pu_conc_xrf_box"
        Me.Pu_conc_xrf_box.Size = New System.Drawing.Size(79, 20)
        Me.Pu_conc_xrf_box.TabIndex = 67
        '
        'Get_KED_Results
        '
        Me.Get_KED_Results.Location = New System.Drawing.Point(1228, 567)
        Me.Get_KED_Results.Name = "Get_KED_Results"
        Me.Get_KED_Results.Size = New System.Drawing.Size(107, 25)
        Me.Get_KED_Results.TabIndex = 71
        Me.Get_KED_Results.Text = "Get KED Results"
        Me.ToolTip1.SetToolTip(Me.Get_KED_Results, resources.GetString("Get_KED_Results.ToolTip"))
        Me.Get_KED_Results.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 20)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Use KED Results"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'sampleinfoBox5
        '
        Me.sampleinfoBox5.Location = New System.Drawing.Point(170, 127)
        Me.sampleinfoBox5.Name = "sampleinfoBox5"
        Me.sampleinfoBox5.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox5.TabIndex = 88
        Me.ToolTip1.SetToolTip(Me.sampleinfoBox5, "Value in AMU")
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 294)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(123, 16)
        Me.Label22.TabIndex = 69
        Me.Label22.Text = "Reference Element"
        Me.ToolTip1.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip"))
        '
        'clear_ked_input
        '
        Me.clear_ked_input.AutoSize = True
        Me.clear_ked_input.Location = New System.Drawing.Point(183, 255)
        Me.clear_ked_input.Name = "clear_ked_input"
        Me.clear_ked_input.Size = New System.Drawing.Size(42, 23)
        Me.clear_ked_input.TabIndex = 94
        Me.clear_ked_input.Text = "reset"
        Me.ToolTip1.SetToolTip(Me.clear_ked_input, resources.GetString("clear_ked_input.ToolTip"))
        Me.clear_ked_input.UseVisualStyleBackColor = True
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(1220, 102)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(86, 28)
        Me.Button7.TabIndex = 70
        Me.Button7.Text = "Fit Progress"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.clear_ked_input)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.use_MEXRF_box)
        Me.Panel2.Controls.Add(Me.ref_el_Pu_button)
        Me.Panel2.Controls.Add(Me.ref_el_U_button)
        Me.Panel2.Controls.Add(Me.KED_check_box_8)
        Me.Panel2.Controls.Add(Me.KED_Box_8)
        Me.Panel2.Controls.Add(Me.KED_check_box_7)
        Me.Panel2.Controls.Add(Me.KED_Box_7)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.KED_check_box_6)
        Me.Panel2.Controls.Add(Me.KED_Box_1)
        Me.Panel2.Controls.Add(Me.KED_check_box_5)
        Me.Panel2.Controls.Add(Me.KED_Box_2)
        Me.Panel2.Controls.Add(Me.KED_check_box_4)
        Me.Panel2.Controls.Add(Me.KED_Box_3)
        Me.Panel2.Controls.Add(Me.KED_check_box_3)
        Me.Panel2.Controls.Add(Me.KED_Box_4)
        Me.Panel2.Controls.Add(Me.KED_check_box_2)
        Me.Panel2.Controls.Add(Me.KED_Box_5)
        Me.Panel2.Controls.Add(Me.KED_check_box_1)
        Me.Panel2.Controls.Add(Me.KED_Box_6)
        Me.Panel2.Location = New System.Drawing.Point(1080, 598)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(255, 323)
        Me.Panel2.TabIndex = 72
        '
        'use_MEXRF_box
        '
        Me.use_MEXRF_box.AutoSize = True
        Me.use_MEXRF_box.Location = New System.Drawing.Point(7, 261)
        Me.use_MEXRF_box.Name = "use_MEXRF_box"
        Me.use_MEXRF_box.Size = New System.Drawing.Size(137, 17)
        Me.use_MEXRF_box.TabIndex = 66
        Me.use_MEXRF_box.Text = "Use MEXRF Fit Results"
        Me.use_MEXRF_box.UseVisualStyleBackColor = True
        '
        'ref_el_Pu_button
        '
        Me.ref_el_Pu_button.AutoSize = True
        Me.ref_el_Pu_button.Location = New System.Drawing.Point(193, 293)
        Me.ref_el_Pu_button.Name = "ref_el_Pu_button"
        Me.ref_el_Pu_button.Size = New System.Drawing.Size(44, 17)
        Me.ref_el_Pu_button.TabIndex = 68
        Me.ref_el_Pu_button.TabStop = True
        Me.ref_el_Pu_button.Text = "[Pu]"
        Me.ref_el_Pu_button.UseVisualStyleBackColor = True
        '
        'ref_el_U_button
        '
        Me.ref_el_U_button.AutoSize = True
        Me.ref_el_U_button.Location = New System.Drawing.Point(141, 293)
        Me.ref_el_U_button.Name = "ref_el_U_button"
        Me.ref_el_U_button.Size = New System.Drawing.Size(39, 17)
        Me.ref_el_U_button.TabIndex = 67
        Me.ref_el_U_button.TabStop = True
        Me.ref_el_U_button.Text = "[U]"
        Me.ref_el_U_button.UseVisualStyleBackColor = True
        '
        'KED_check_box_8
        '
        Me.KED_check_box_8.AutoSize = True
        Me.KED_check_box_8.Location = New System.Drawing.Point(7, 216)
        Me.KED_check_box_8.Name = "KED_check_box_8"
        Me.KED_check_box_8.Size = New System.Drawing.Size(95, 17)
        Me.KED_check_box_8.TabIndex = 65
        Me.KED_check_box_8.Text = "[Matrix] (g /cc)"
        Me.KED_check_box_8.UseVisualStyleBackColor = True
        '
        'KED_Box_8
        '
        Me.KED_Box_8.Location = New System.Drawing.Point(147, 214)
        Me.KED_Box_8.Name = "KED_Box_8"
        Me.KED_Box_8.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_8.TabIndex = 64
        '
        'KED_check_box_7
        '
        Me.KED_check_box_7.AutoSize = True
        Me.KED_check_box_7.Location = New System.Drawing.Point(7, 193)
        Me.KED_check_box_7.Name = "KED_check_box_7"
        Me.KED_check_box_7.Size = New System.Drawing.Size(73, 17)
        Me.KED_check_box_7.TabIndex = 63
        Me.KED_check_box_7.Text = "[Am] g / L"
        Me.KED_check_box_7.UseVisualStyleBackColor = True
        '
        'KED_Box_7
        '
        Me.KED_Box_7.Location = New System.Drawing.Point(147, 191)
        Me.KED_Box_7.Name = "KED_Box_7"
        Me.KED_Box_7.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_7.TabIndex = 62
        '
        'KED_check_box_6
        '
        Me.KED_check_box_6.AutoSize = True
        Me.KED_check_box_6.Location = New System.Drawing.Point(7, 170)
        Me.KED_check_box_6.Name = "KED_check_box_6"
        Me.KED_check_box_6.Size = New System.Drawing.Size(73, 17)
        Me.KED_check_box_6.TabIndex = 61
        Me.KED_check_box_6.Text = "[Cm] g / L"
        Me.KED_check_box_6.UseVisualStyleBackColor = True
        '
        'KED_Box_1
        '
        Me.KED_Box_1.Location = New System.Drawing.Point(147, 41)
        Me.KED_Box_1.Name = "KED_Box_1"
        Me.KED_Box_1.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_1.TabIndex = 50
        '
        'KED_check_box_5
        '
        Me.KED_check_box_5.AutoSize = True
        Me.KED_check_box_5.Location = New System.Drawing.Point(7, 147)
        Me.KED_check_box_5.Name = "KED_check_box_5"
        Me.KED_check_box_5.Size = New System.Drawing.Size(72, 17)
        Me.KED_check_box_5.TabIndex = 60
        Me.KED_check_box_5.Text = "[Np] g / L"
        Me.KED_check_box_5.UseVisualStyleBackColor = True
        '
        'KED_Box_2
        '
        Me.KED_Box_2.Location = New System.Drawing.Point(147, 67)
        Me.KED_Box_2.Name = "KED_Box_2"
        Me.KED_Box_2.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_2.TabIndex = 51
        '
        'KED_check_box_4
        '
        Me.KED_check_box_4.AutoSize = True
        Me.KED_check_box_4.Location = New System.Drawing.Point(7, 121)
        Me.KED_check_box_4.Name = "KED_check_box_4"
        Me.KED_check_box_4.Size = New System.Drawing.Size(71, 17)
        Me.KED_check_box_4.TabIndex = 59
        Me.KED_check_box_4.Text = "[Pu] g / L"
        Me.KED_check_box_4.UseVisualStyleBackColor = True
        '
        'KED_Box_3
        '
        Me.KED_Box_3.Location = New System.Drawing.Point(147, 93)
        Me.KED_Box_3.Name = "KED_Box_3"
        Me.KED_Box_3.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_3.TabIndex = 52
        '
        'KED_check_box_3
        '
        Me.KED_check_box_3.AutoSize = True
        Me.KED_check_box_3.Location = New System.Drawing.Point(7, 95)
        Me.KED_check_box_3.Name = "KED_check_box_3"
        Me.KED_check_box_3.Size = New System.Drawing.Size(66, 17)
        Me.KED_check_box_3.TabIndex = 58
        Me.KED_check_box_3.Text = "[U] g / L"
        Me.KED_check_box_3.UseVisualStyleBackColor = True
        '
        'KED_Box_4
        '
        Me.KED_Box_4.Location = New System.Drawing.Point(147, 119)
        Me.KED_Box_4.Name = "KED_Box_4"
        Me.KED_Box_4.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_4.TabIndex = 53
        '
        'KED_check_box_2
        '
        Me.KED_check_box_2.AutoSize = True
        Me.KED_check_box_2.Location = New System.Drawing.Point(7, 69)
        Me.KED_check_box_2.Name = "KED_check_box_2"
        Me.KED_check_box_2.Size = New System.Drawing.Size(140, 17)
        Me.KED_check_box_2.TabIndex = 57
        Me.KED_check_box_2.Text = "Bremsstrahlung Shaping"
        Me.KED_check_box_2.UseVisualStyleBackColor = True
        '
        'KED_Box_5
        '
        Me.KED_Box_5.Location = New System.Drawing.Point(147, 145)
        Me.KED_Box_5.Name = "KED_Box_5"
        Me.KED_Box_5.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_5.TabIndex = 54
        '
        'KED_check_box_1
        '
        Me.KED_check_box_1.AutoSize = True
        Me.KED_check_box_1.Location = New System.Drawing.Point(7, 43)
        Me.KED_check_box_1.Name = "KED_check_box_1"
        Me.KED_check_box_1.Size = New System.Drawing.Size(112, 17)
        Me.KED_check_box_1.TabIndex = 56
        Me.KED_check_box_1.Text = "HV End Point (kV)"
        Me.KED_check_box_1.UseVisualStyleBackColor = True
        '
        'KED_Box_6
        '
        Me.KED_Box_6.Location = New System.Drawing.Point(147, 168)
        Me.KED_Box_6.Name = "KED_Box_6"
        Me.KED_Box_6.Size = New System.Drawing.Size(79, 20)
        Me.KED_Box_6.TabIndex = 55
        '
        'U_KA1_box
        '
        Me.U_KA1_box.Location = New System.Drawing.Point(274, 82)
        Me.U_KA1_box.Name = "U_KA1_box"
        Me.U_KA1_box.Size = New System.Drawing.Size(79, 20)
        Me.U_KA1_box.TabIndex = 73
        '
        'PU_KA1_box
        '
        Me.PU_KA1_box.Location = New System.Drawing.Point(274, 107)
        Me.PU_KA1_box.Name = "PU_KA1_box"
        Me.PU_KA1_box.Size = New System.Drawing.Size(79, 20)
        Me.PU_KA1_box.TabIndex = 74
        '
        'vms_U_Pu_count_ratio_box
        '
        Me.vms_U_Pu_count_ratio_box.Location = New System.Drawing.Point(133, 133)
        Me.vms_U_Pu_count_ratio_box.Name = "vms_U_Pu_count_ratio_box"
        Me.vms_U_Pu_count_ratio_box.Size = New System.Drawing.Size(79, 20)
        Me.vms_U_Pu_count_ratio_box.TabIndex = 75
        '
        'VMS_U_Pu_ratio_box
        '
        Me.VMS_U_Pu_ratio_box.Location = New System.Drawing.Point(133, 159)
        Me.VMS_U_Pu_ratio_box.Name = "VMS_U_Pu_ratio_box"
        Me.VMS_U_Pu_ratio_box.Size = New System.Drawing.Size(79, 20)
        Me.VMS_U_Pu_ratio_box.TabIndex = 76
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 82)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 16)
        Me.Label8.TabIndex = 77
        Me.Label8.Text = "U Ka1"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 107)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 16)
        Me.Label9.TabIndex = 78
        Me.Label9.Text = "Pu Ka1"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 134)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(110, 16)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "U/Pu Count Ratio"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(13, 160)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 16)
        Me.Label11.TabIndex = 80
        Me.Label11.Text = "[U]/Pu]"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(13, 55)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 16)
        Me.Label12.TabIndex = 82
        Me.Label12.Text = "U Ka2"
        '
        'U_KA2_box
        '
        Me.U_KA2_box.Location = New System.Drawing.Point(274, 55)
        Me.U_KA2_box.Name = "U_KA2_box"
        Me.U_KA2_box.Size = New System.Drawing.Size(79, 20)
        Me.U_KA2_box.TabIndex = 81
        '
        'U_Ka2_seed_counts_box
        '
        Me.U_Ka2_seed_counts_box.Location = New System.Drawing.Point(559, 54)
        Me.U_Ka2_seed_counts_box.Name = "U_Ka2_seed_counts_box"
        Me.U_Ka2_seed_counts_box.Size = New System.Drawing.Size(117, 20)
        Me.U_Ka2_seed_counts_box.TabIndex = 83
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(491, 83)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(62, 16)
        Me.Label13.TabIndex = 84
        Me.Label13.Text = "U Counts"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(491, 116)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 16)
        Me.Label14.TabIndex = 85
        Me.Label14.Text = "Pu Counts"
        '
        'Pu_seed_counts_box
        '
        Me.Pu_seed_counts_box.Location = New System.Drawing.Point(559, 116)
        Me.Pu_seed_counts_box.Name = "Pu_seed_counts_box"
        Me.Pu_seed_counts_box.Size = New System.Drawing.Size(117, 20)
        Me.Pu_seed_counts_box.TabIndex = 86
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.cd_prelim_area_box)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.U_Ka1_seed_counts_box)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Button5)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.Pu_conc_xrf_box)
        Me.Panel3.Controls.Add(Me.Pu_Ka1_ROI2_box)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.U_conc_xrf_box)
        Me.Panel3.Controls.Add(Me.Pu_Ka1_ROI1_box)
        Me.Panel3.Controls.Add(Me.U_Ka2_ROI2_box)
        Me.Panel3.Controls.Add(Me.U_Ka1_ROI2_box)
        Me.Panel3.Controls.Add(Me.U_Ka2_ROI1_box)
        Me.Panel3.Controls.Add(Me.U_Ka1_ROI1_box)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Pu_seed_counts_box)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.U_Ka2_seed_counts_box)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.U_KA2_box)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.VMS_U_Pu_ratio_box)
        Me.Panel3.Controls.Add(Me.vms_U_Pu_count_ratio_box)
        Me.Panel3.Controls.Add(Me.PU_KA1_box)
        Me.Panel3.Controls.Add(Me.U_KA1_box)
        Me.Panel3.Location = New System.Drawing.Point(332, 700)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(699, 221)
        Me.Panel3.TabIndex = 87
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(13, 194)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 16)
        Me.Label21.TabIndex = 101
        Me.Label21.Text = "Ref Peak Area"
        '
        'cd_prelim_area_box
        '
        Me.cd_prelim_area_box.Location = New System.Drawing.Point(133, 193)
        Me.cd_prelim_area_box.Name = "cd_prelim_area_box"
        Me.cd_prelim_area_box.Size = New System.Drawing.Size(79, 20)
        Me.cd_prelim_area_box.TabIndex = 100
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(491, 55)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(62, 16)
        Me.Label20.TabIndex = 99
        Me.Label20.Text = "U Counts"
        '
        'U_Ka1_seed_counts_box
        '
        Me.U_Ka1_seed_counts_box.Location = New System.Drawing.Point(559, 81)
        Me.U_Ka1_seed_counts_box.Name = "U_Ka1_seed_counts_box"
        Me.U_Ka1_seed_counts_box.Size = New System.Drawing.Size(117, 20)
        Me.U_Ka1_seed_counts_box.TabIndex = 98
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(556, 32)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(79, 16)
        Me.Label19.TabIndex = 97
        Me.Label19.Text = "Seed Value"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(286, 33)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 16)
        Me.Label18.TabIndex = 96
        Me.Label18.Text = "ROI net"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(187, 32)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 16)
        Me.Label17.TabIndex = 95
        Me.Label17.Text = "BKG"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(91, 32)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 16)
        Me.Label16.TabIndex = 94
        Me.Label16.Text = "Integral"
        '
        'Pu_Ka1_ROI2_box
        '
        Me.Pu_Ka1_ROI2_box.Location = New System.Drawing.Point(180, 107)
        Me.Pu_Ka1_ROI2_box.Name = "Pu_Ka1_ROI2_box"
        Me.Pu_Ka1_ROI2_box.Size = New System.Drawing.Size(79, 20)
        Me.Pu_Ka1_ROI2_box.TabIndex = 93
        '
        'Pu_Ka1_ROI1_box
        '
        Me.Pu_Ka1_ROI1_box.Location = New System.Drawing.Point(85, 107)
        Me.Pu_Ka1_ROI1_box.Name = "Pu_Ka1_ROI1_box"
        Me.Pu_Ka1_ROI1_box.Size = New System.Drawing.Size(79, 20)
        Me.Pu_Ka1_ROI1_box.TabIndex = 92
        '
        'U_Ka2_ROI2_box
        '
        Me.U_Ka2_ROI2_box.Location = New System.Drawing.Point(180, 54)
        Me.U_Ka2_ROI2_box.Name = "U_Ka2_ROI2_box"
        Me.U_Ka2_ROI2_box.Size = New System.Drawing.Size(79, 20)
        Me.U_Ka2_ROI2_box.TabIndex = 91
        '
        'U_Ka1_ROI2_box
        '
        Me.U_Ka1_ROI2_box.Location = New System.Drawing.Point(180, 81)
        Me.U_Ka1_ROI2_box.Name = "U_Ka1_ROI2_box"
        Me.U_Ka1_ROI2_box.Size = New System.Drawing.Size(79, 20)
        Me.U_Ka1_ROI2_box.TabIndex = 90
        '
        'U_Ka2_ROI1_box
        '
        Me.U_Ka2_ROI1_box.Location = New System.Drawing.Point(85, 54)
        Me.U_Ka2_ROI1_box.Name = "U_Ka2_ROI1_box"
        Me.U_Ka2_ROI1_box.Size = New System.Drawing.Size(79, 20)
        Me.U_Ka2_ROI1_box.TabIndex = 89
        '
        'U_Ka1_ROI1_box
        '
        Me.U_Ka1_ROI1_box.Location = New System.Drawing.Point(85, 81)
        Me.U_Ka1_ROI1_box.Name = "U_Ka1_ROI1_box"
        Me.U_Ka1_ROI1_box.Size = New System.Drawing.Size(79, 20)
        Me.U_Ka1_ROI1_box.TabIndex = 88
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(13, 7)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(217, 16)
        Me.Label15.TabIndex = 87
        Me.Label15.Text = "Initial Estimates (VMS like analysis)"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.Window
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.sampleinfoBox5)
        Me.Panel4.Controls.Add(Me.Cycle_number_Box)
        Me.Panel4.Controls.Add(Me.sampleinfoBox4)
        Me.Panel4.Controls.Add(Me.sampleinfoBox3)
        Me.Panel4.Controls.Add(Me.sampleinfoBox2)
        Me.Panel4.Controls.Add(Me.sampleinfoBox1)
        Me.Panel4.Controls.Add(ListBox3)
        Me.Panel4.Location = New System.Drawing.Point(836, 46)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(260, 155)
        Me.Panel4.TabIndex = 89
        '
        'nchan_checkbox
        '
        Me.nchan_checkbox.AutoSize = True
        Me.nchan_checkbox.Location = New System.Drawing.Point(1223, 151)
        Me.nchan_checkbox.Name = "nchan_checkbox"
        Me.nchan_checkbox.Size = New System.Drawing.Size(84, 17)
        Me.nchan_checkbox.TabIndex = 66
        Me.nchan_checkbox.Text = "4k channels"
        Me.nchan_checkbox.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(1084, 567)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(133, 25)
        Me.Button8.TabIndex = 90
        Me.Button8.Text = "Read KED Summary File"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Display_E_button
        '
        Me.Display_E_button.AutoSize = True
        Me.Display_E_button.Checked = True
        Me.Display_E_button.Location = New System.Drawing.Point(56, 700)
        Me.Display_E_button.Name = "Display_E_button"
        Me.Display_E_button.Size = New System.Drawing.Size(58, 17)
        Me.Display_E_button.TabIndex = 91
        Me.Display_E_button.TabStop = True
        Me.Display_E_button.Text = "Energy"
        Me.Display_E_button.UseVisualStyleBackColor = True
        '
        'Display_chan_button
        '
        Me.Display_chan_button.AutoSize = True
        Me.Display_chan_button.Location = New System.Drawing.Point(120, 700)
        Me.Display_chan_button.Name = "Display_chan_button"
        Me.Display_chan_button.Size = New System.Drawing.Size(69, 17)
        Me.Display_chan_button.TabIndex = 92
        Me.Display_chan_button.Text = "Channels"
        Me.Display_chan_button.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Location = New System.Drawing.Point(56, 834)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(42, 181)
        Me.Panel5.TabIndex = 93
        '
        'GammarayDataToolStripMenuItem
        '
        Me.GammarayDataToolStripMenuItem.Name = "GammarayDataToolStripMenuItem"
        Me.GammarayDataToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.GammarayDataToolStripMenuItem.Text = "Gamma-ray Data"
        '
        'MEXRF_main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1387, 951)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Display_chan_button)
        Me.Controls.Add(Me.Display_E_button)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.nchan_checkbox)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Get_KED_Results)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.select_files_Button)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SampleIDBox1)
        Me.Controls.Add(Me.filenamebox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.RunPlease)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MEXRF_main"
        Me.Text = "MEXRF"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents SysParToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WhoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents filenamebox1 As MaskedTextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents RunPlease As Button
    Friend WithEvents sampleinfoBox4 As MaskedTextBox
    Friend WithEvents sampleinfoBox3 As MaskedTextBox
    Friend WithEvents sampleinfoBox2 As MaskedTextBox
    Friend WithEvents sampleinfoBox1 As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents SampleIDBox1 As MaskedTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents select_files_Button As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Button6 As Button
    Friend WithEvents Cycle_number_Box As MaskedTextBox
    Friend WithEvents SummaryREsultsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents MaskedTextBox1 As MaskedTextBox
    Friend WithEvents MaskedTextBox2 As MaskedTextBox
    Friend WithEvents MaskedTextBox3 As MaskedTextBox
    Friend WithEvents MaskedTextBox4 As MaskedTextBox
    Friend WithEvents MaskedTextBox5 As MaskedTextBox
    Friend WithEvents MaskedTextBox6 As MaskedTextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckBox6 As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents U_conc_xrf_box As MaskedTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents VMSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label6 As Label
    Friend WithEvents Pu_conc_xrf_box As MaskedTextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents Button7 As Button
    Friend WithEvents AttenuationCoefficientsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel2 As Panel
    Friend WithEvents KED_check_box_7 As CheckBox
    Friend WithEvents KED_Box_7 As MaskedTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents KED_check_box_6 As CheckBox
    Friend WithEvents KED_Box_1 As MaskedTextBox
    Friend WithEvents KED_check_box_5 As CheckBox
    Friend WithEvents KED_Box_2 As MaskedTextBox
    Friend WithEvents KED_check_box_4 As CheckBox
    Friend WithEvents KED_Box_3 As MaskedTextBox
    Friend WithEvents KED_check_box_3 As CheckBox
    Friend WithEvents KED_Box_4 As MaskedTextBox
    Friend WithEvents KED_check_box_2 As CheckBox
    Friend WithEvents KED_Box_5 As MaskedTextBox
    Friend WithEvents KED_check_box_1 As CheckBox
    Friend WithEvents KED_Box_6 As MaskedTextBox
    Friend WithEvents Get_KED_Results As Button
    Friend WithEvents KED_check_box_8 As CheckBox
    Friend WithEvents KED_Box_8 As MaskedTextBox
    Friend WithEvents XrayDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents VMS_U_Pu_ratio_box As MaskedTextBox
    Friend WithEvents vms_U_Pu_count_ratio_box As MaskedTextBox
    Friend WithEvents PU_KA1_box As MaskedTextBox
    Friend WithEvents U_KA1_box As MaskedTextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Pu_seed_counts_box As MaskedTextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents U_Ka2_seed_counts_box As MaskedTextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents U_KA2_box As MaskedTextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents sampleinfoBox5 As MaskedTextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Pu_Ka1_ROI2_box As MaskedTextBox
    Friend WithEvents Pu_Ka1_ROI1_box As MaskedTextBox
    Friend WithEvents U_Ka2_ROI2_box As MaskedTextBox
    Friend WithEvents U_Ka1_ROI2_box As MaskedTextBox
    Friend WithEvents U_Ka2_ROI1_box As MaskedTextBox
    Friend WithEvents U_Ka1_ROI1_box As MaskedTextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents U_Ka1_seed_counts_box As MaskedTextBox
    Friend WithEvents nchan_checkbox As CheckBox
    Friend WithEvents Button8 As Button
    Friend WithEvents Label21 As Label
    Friend WithEvents cd_prelim_area_box As MaskedTextBox
    Friend WithEvents Display_chan_button As RadioButton
    Friend WithEvents Display_E_button As RadioButton
    Friend WithEvents MiscFfunctionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateSummedSpectrumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetUpPassiveCorrectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConvertCNFToSPEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SubtractPassiveSpectrumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfigurePassiveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label22 As Label
    Friend WithEvents use_MEXRF_box As CheckBox
    Friend WithEvents ref_el_Pu_button As RadioButton
    Friend WithEvents ref_el_U_button As RadioButton
    Friend WithEvents clear_ked_input As Button
    Friend WithEvents GammarayDataToolStripMenuItem As ToolStripMenuItem
End Class
