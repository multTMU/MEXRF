<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fission_Products
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fission_Products))
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.empirical_corr_par_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.passive_file_name_box = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.passive_summary_name_box = New System.Windows.Forms.TextBox()
        Me.empirical_corr_par_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.empirical_corr_E2_box = New System.Windows.Forms.MaskedTextBox()
        Me.empirical_corr_E1_box = New System.Windows.Forms.MaskedTextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.fp_bias_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.fp_bias_box = New System.Windows.Forms.MaskedTextBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.U_passive_rate_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.U_passive_rate_box = New System.Windows.Forms.MaskedTextBox()
        Me.ListBox5 = New System.Windows.Forms.ListBox()
        Me.Pu_passive_rate_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.Pu_passive_rate_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ListBox6 = New System.Windows.Forms.ListBox()
        Me.Np_passive_rate_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.Np_passive_rate_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ListBox7 = New System.Windows.Forms.ListBox()
        Me.Cm_passive_rate_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.Cm_passive_rate_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ListBox8 = New System.Windows.Forms.ListBox()
        Me.Am_passive_rate_err_box = New System.Windows.Forms.MaskedTextBox()
        Me.Am_passive_rate_box = New System.Windows.Forms.MaskedTextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(14, 32)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(153, 17)
        Me.RadioButton2.TabIndex = 62
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Subtract Passive Spectrum"
        Me.ToolTip1.SetToolTip(Me.RadioButton2, resources.GetString("RadioButton2.ToolTip"))
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(14, 55)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(136, 17)
        Me.RadioButton3.TabIndex = 63
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Subtract Passive Rates"
        Me.ToolTip1.SetToolTip(Me.RadioButton3, resources.GetString("RadioButton3.ToolTip"))
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(14, 78)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(118, 17)
        Me.RadioButton4.TabIndex = 64
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Empirical Correction"
        Me.ToolTip1.SetToolTip(Me.RadioButton4, "Refer to " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """A Self Irradiation Correction for the Hybrid K-Edge Densitometer""" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "R." &
        "D. McElroy, Jr. and Stephen Croft" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Proceedings of the INMM  60th Annual Meeting " &
        "(2019).")
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Select Method"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'empirical_corr_par_box
        '
        Me.empirical_corr_par_box.Enabled = False
        Me.empirical_corr_par_box.Location = New System.Drawing.Point(144, 36)
        Me.empirical_corr_par_box.Name = "empirical_corr_par_box"
        Me.empirical_corr_par_box.Size = New System.Drawing.Size(110, 20)
        Me.empirical_corr_par_box.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.empirical_corr_par_box, resources.GetString("empirical_corr_par_box.ToolTip"))
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 118)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(182, 16)
        Me.Label7.TabIndex = 60
        Me.Label7.Text = "Constant Bias Correction:"
        Me.ToolTip1.SetToolTip(Me.Label7, "Correction applied to R_UPu factor")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(455, 315)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(181, 16)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "Rates from Summary File"
        Me.ToolTip1.SetToolTip(Me.Label8, "These self interrogation rates will be subtracted from the corresponding" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "count r" &
        "ates in the active spectra." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(697, 199)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 27)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "Select New"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(81, 204)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 16)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "XRF Passive Spectrum:"
        '
        'passive_file_name_box
        '
        Me.passive_file_name_box.Location = New System.Drawing.Point(262, 203)
        Me.passive_file_name_box.Name = "passive_file_name_box"
        Me.passive_file_name_box.Size = New System.Drawing.Size(412, 20)
        Me.passive_file_name_box.TabIndex = 33
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(697, 246)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(71, 27)
        Me.Button2.TabIndex = 38
        Me.Button2.Text = "Select New"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 251)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(229, 16)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Passive Analysis Summary File:"
        '
        'passive_summary_name_box
        '
        Me.passive_summary_name_box.Location = New System.Drawing.Point(262, 250)
        Me.passive_summary_name_box.Name = "passive_summary_name_box"
        Me.passive_summary_name_box.Size = New System.Drawing.Size(412, 20)
        Me.passive_summary_name_box.TabIndex = 36
        '
        'empirical_corr_par_err_box
        '
        Me.empirical_corr_par_err_box.Enabled = False
        Me.empirical_corr_par_err_box.Location = New System.Drawing.Point(282, 36)
        Me.empirical_corr_par_err_box.Name = "empirical_corr_par_err_box"
        Me.empirical_corr_par_err_box.Size = New System.Drawing.Size(110, 20)
        Me.empirical_corr_par_err_box.TabIndex = 40
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(78, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "a_RUPu: "
        '
        'ListBox2
        '
        Me.ListBox2.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox2.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 23
        Me.ListBox2.Items.AddRange(New Object() {"±"})
        Me.ListBox2.Location = New System.Drawing.Point(260, 32)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(16, 23)
        Me.ListBox2.TabIndex = 50
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(232, 16)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Empirical Correction Parameters"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Energy Range (keV): "
        '
        'empirical_corr_E2_box
        '
        Me.empirical_corr_E2_box.Enabled = False
        Me.empirical_corr_E2_box.Location = New System.Drawing.Point(282, 67)
        Me.empirical_corr_E2_box.Name = "empirical_corr_E2_box"
        Me.empirical_corr_E2_box.Size = New System.Drawing.Size(110, 20)
        Me.empirical_corr_E2_box.TabIndex = 54
        '
        'empirical_corr_E1_box
        '
        Me.empirical_corr_E1_box.Enabled = False
        Me.empirical_corr_E1_box.Location = New System.Drawing.Point(144, 67)
        Me.empirical_corr_E1_box.Name = "empirical_corr_E1_box"
        Me.empirical_corr_E1_box.Size = New System.Drawing.Size(110, 20)
        Me.empirical_corr_E1_box.TabIndex = 53
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Items.AddRange(New Object() {"to"})
        Me.ListBox1.Location = New System.Drawing.Point(260, 72)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(16, 13)
        Me.ListBox1.TabIndex = 55
        '
        'ListBox3
        '
        Me.ListBox3.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox3.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 23
        Me.ListBox3.Items.AddRange(New Object() {"±"})
        Me.ListBox3.Location = New System.Drawing.Point(260, 139)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(16, 23)
        Me.ListBox3.TabIndex = 59
        '
        'fp_bias_err_box
        '
        Me.fp_bias_err_box.Enabled = False
        Me.fp_bias_err_box.Location = New System.Drawing.Point(282, 143)
        Me.fp_bias_err_box.Name = "fp_bias_err_box"
        Me.fp_bias_err_box.Size = New System.Drawing.Size(110, 20)
        Me.fp_bias_err_box.TabIndex = 57
        '
        'fp_bias_box
        '
        Me.fp_bias_box.Enabled = False
        Me.fp_bias_box.Location = New System.Drawing.Point(144, 143)
        Me.fp_bias_box.Name = "fp_bias_box"
        Me.fp_bias_box.Size = New System.Drawing.Size(110, 20)
        Me.fp_bias_box.TabIndex = 56
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(14, 9)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(51, 17)
        Me.RadioButton1.TabIndex = 61
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "None"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(14, 101)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton5.TabIndex = 65
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "Constant Bias"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.RadioButton5)
        Me.Panel2.Controls.Add(Me.RadioButton4)
        Me.Panel2.Controls.Add(Me.RadioButton3)
        Me.Panel2.Controls.Add(Me.RadioButton2)
        Me.Panel2.Controls.Add(Me.RadioButton1)
        Me.Panel2.Location = New System.Drawing.Point(25, 39)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(172, 128)
        Me.Panel2.TabIndex = 66
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(20, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 16)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "U:"
        '
        'ListBox4
        '
        Me.ListBox4.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox4.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.ItemHeight = 23
        Me.ListBox4.Items.AddRange(New Object() {"±"})
        Me.ListBox4.Location = New System.Drawing.Point(171, 18)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(16, 23)
        Me.ListBox4.TabIndex = 71
        '
        'U_passive_rate_err_box
        '
        Me.U_passive_rate_err_box.Enabled = False
        Me.U_passive_rate_err_box.Location = New System.Drawing.Point(193, 22)
        Me.U_passive_rate_err_box.Name = "U_passive_rate_err_box"
        Me.U_passive_rate_err_box.Size = New System.Drawing.Size(110, 20)
        Me.U_passive_rate_err_box.TabIndex = 70
        '
        'U_passive_rate_box
        '
        Me.U_passive_rate_box.Enabled = False
        Me.U_passive_rate_box.Location = New System.Drawing.Point(55, 22)
        Me.U_passive_rate_box.Name = "U_passive_rate_box"
        Me.U_passive_rate_box.Size = New System.Drawing.Size(110, 20)
        Me.U_passive_rate_box.TabIndex = 69
        '
        'ListBox5
        '
        Me.ListBox5.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox5.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox5.FormattingEnabled = True
        Me.ListBox5.ItemHeight = 23
        Me.ListBox5.Items.AddRange(New Object() {"±"})
        Me.ListBox5.Location = New System.Drawing.Point(171, 50)
        Me.ListBox5.Name = "ListBox5"
        Me.ListBox5.Size = New System.Drawing.Size(16, 23)
        Me.ListBox5.TabIndex = 75
        '
        'Pu_passive_rate_err_box
        '
        Me.Pu_passive_rate_err_box.Enabled = False
        Me.Pu_passive_rate_err_box.Location = New System.Drawing.Point(193, 54)
        Me.Pu_passive_rate_err_box.Name = "Pu_passive_rate_err_box"
        Me.Pu_passive_rate_err_box.Size = New System.Drawing.Size(110, 20)
        Me.Pu_passive_rate_err_box.TabIndex = 74
        '
        'Pu_passive_rate_box
        '
        Me.Pu_passive_rate_box.Enabled = False
        Me.Pu_passive_rate_box.Location = New System.Drawing.Point(55, 54)
        Me.Pu_passive_rate_box.Name = "Pu_passive_rate_box"
        Me.Pu_passive_rate_box.Size = New System.Drawing.Size(110, 20)
        Me.Pu_passive_rate_box.TabIndex = 73
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 55)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 16)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Pu:"
        '
        'ListBox6
        '
        Me.ListBox6.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox6.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox6.FormattingEnabled = True
        Me.ListBox6.ItemHeight = 23
        Me.ListBox6.Items.AddRange(New Object() {"±"})
        Me.ListBox6.Location = New System.Drawing.Point(171, 82)
        Me.ListBox6.Name = "ListBox6"
        Me.ListBox6.Size = New System.Drawing.Size(16, 23)
        Me.ListBox6.TabIndex = 79
        '
        'Np_passive_rate_err_box
        '
        Me.Np_passive_rate_err_box.Enabled = False
        Me.Np_passive_rate_err_box.Location = New System.Drawing.Point(193, 86)
        Me.Np_passive_rate_err_box.Name = "Np_passive_rate_err_box"
        Me.Np_passive_rate_err_box.Size = New System.Drawing.Size(110, 20)
        Me.Np_passive_rate_err_box.TabIndex = 78
        '
        'Np_passive_rate_box
        '
        Me.Np_passive_rate_box.Enabled = False
        Me.Np_passive_rate_box.Location = New System.Drawing.Point(55, 86)
        Me.Np_passive_rate_box.Name = "Np_passive_rate_box"
        Me.Np_passive_rate_box.Size = New System.Drawing.Size(110, 20)
        Me.Np_passive_rate_box.TabIndex = 77
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 87)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 16)
        Me.Label11.TabIndex = 76
        Me.Label11.Text = "Np:"
        '
        'ListBox7
        '
        Me.ListBox7.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox7.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox7.FormattingEnabled = True
        Me.ListBox7.ItemHeight = 23
        Me.ListBox7.Items.AddRange(New Object() {"±"})
        Me.ListBox7.Location = New System.Drawing.Point(171, 110)
        Me.ListBox7.Name = "ListBox7"
        Me.ListBox7.Size = New System.Drawing.Size(16, 23)
        Me.ListBox7.TabIndex = 83
        '
        'Cm_passive_rate_err_box
        '
        Me.Cm_passive_rate_err_box.Enabled = False
        Me.Cm_passive_rate_err_box.Location = New System.Drawing.Point(193, 114)
        Me.Cm_passive_rate_err_box.Name = "Cm_passive_rate_err_box"
        Me.Cm_passive_rate_err_box.Size = New System.Drawing.Size(110, 20)
        Me.Cm_passive_rate_err_box.TabIndex = 82
        '
        'Cm_passive_rate_box
        '
        Me.Cm_passive_rate_box.Enabled = False
        Me.Cm_passive_rate_box.Location = New System.Drawing.Point(55, 114)
        Me.Cm_passive_rate_box.Name = "Cm_passive_rate_box"
        Me.Cm_passive_rate_box.Size = New System.Drawing.Size(110, 20)
        Me.Cm_passive_rate_box.TabIndex = 81
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 115)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 16)
        Me.Label12.TabIndex = 80
        Me.Label12.Text = "Cm:"
        '
        'ListBox8
        '
        Me.ListBox8.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox8.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox8.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox8.FormattingEnabled = True
        Me.ListBox8.ItemHeight = 23
        Me.ListBox8.Items.AddRange(New Object() {"±"})
        Me.ListBox8.Location = New System.Drawing.Point(171, 141)
        Me.ListBox8.Name = "ListBox8"
        Me.ListBox8.Size = New System.Drawing.Size(16, 23)
        Me.ListBox8.TabIndex = 87
        '
        'Am_passive_rate_err_box
        '
        Me.Am_passive_rate_err_box.Enabled = False
        Me.Am_passive_rate_err_box.Location = New System.Drawing.Point(193, 145)
        Me.Am_passive_rate_err_box.Name = "Am_passive_rate_err_box"
        Me.Am_passive_rate_err_box.Size = New System.Drawing.Size(110, 20)
        Me.Am_passive_rate_err_box.TabIndex = 86
        '
        'Am_passive_rate_box
        '
        Me.Am_passive_rate_box.Enabled = False
        Me.Am_passive_rate_box.Location = New System.Drawing.Point(55, 145)
        Me.Am_passive_rate_box.Name = "Am_passive_rate_box"
        Me.Am_passive_rate_box.Size = New System.Drawing.Size(110, 20)
        Me.Am_passive_rate_box.TabIndex = 85
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(20, 146)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 16)
        Me.Label13.TabIndex = 84
        Me.Label13.Text = "Am:"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ListBox8)
        Me.Panel1.Controls.Add(Me.Am_passive_rate_err_box)
        Me.Panel1.Controls.Add(Me.Am_passive_rate_box)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.ListBox7)
        Me.Panel1.Controls.Add(Me.Cm_passive_rate_err_box)
        Me.Panel1.Controls.Add(Me.Cm_passive_rate_box)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.ListBox6)
        Me.Panel1.Controls.Add(Me.Np_passive_rate_err_box)
        Me.Panel1.Controls.Add(Me.Np_passive_rate_box)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.ListBox5)
        Me.Panel1.Controls.Add(Me.Pu_passive_rate_err_box)
        Me.Panel1.Controls.Add(Me.Pu_passive_rate_box)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.ListBox4)
        Me.Panel1.Controls.Add(Me.U_passive_rate_err_box)
        Me.Panel1.Controls.Add(Me.U_passive_rate_box)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(448, 334)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(331, 179)
        Me.Panel1.TabIndex = 88
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.ListBox1)
        Me.Panel3.Controls.Add(Me.empirical_corr_E2_box)
        Me.Panel3.Controls.Add(Me.empirical_corr_E1_box)
        Me.Panel3.Controls.Add(Me.ListBox3)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.fp_bias_err_box)
        Me.Panel3.Controls.Add(Me.fp_bias_box)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.ListBox2)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.empirical_corr_par_err_box)
        Me.Panel3.Controls.Add(Me.empirical_corr_par_box)
        Me.Panel3.Location = New System.Drawing.Point(19, 334)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(403, 179)
        Me.Panel3.TabIndex = 89
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(81, 148)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 13)
        Me.Label14.TabIndex = 61
        Me.Label14.Text = "a_bias: "
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(29, 315)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(200, 16)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "From System Constants File"
        '
        'Fission_Products
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 525)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.passive_summary_name_box)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.passive_file_name_box)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Fission_Products"
        Me.Text = "Fission Product Setup"
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents empirical_corr_par_err_box As MaskedTextBox
    Friend WithEvents empirical_corr_par_box As MaskedTextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents passive_summary_name_box As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents passive_file_name_box As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents empirical_corr_E2_box As MaskedTextBox
    Friend WithEvents empirical_corr_E1_box As MaskedTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label7 As Label
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents fp_bias_err_box As MaskedTextBox
    Friend WithEvents fp_bias_box As MaskedTextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ListBox8 As ListBox
    Friend WithEvents Am_passive_rate_err_box As MaskedTextBox
    Friend WithEvents Am_passive_rate_box As MaskedTextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents ListBox7 As ListBox
    Friend WithEvents Cm_passive_rate_err_box As MaskedTextBox
    Friend WithEvents Cm_passive_rate_box As MaskedTextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents ListBox6 As ListBox
    Friend WithEvents Np_passive_rate_err_box As MaskedTextBox
    Friend WithEvents Np_passive_rate_box As MaskedTextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents ListBox5 As ListBox
    Friend WithEvents Pu_passive_rate_err_box As MaskedTextBox
    Friend WithEvents Pu_passive_rate_box As MaskedTextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents ListBox4 As ListBox
    Friend WithEvents U_passive_rate_err_box As MaskedTextBox
    Friend WithEvents U_passive_rate_box As MaskedTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
End Class
