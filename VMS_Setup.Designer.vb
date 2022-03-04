<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VMS_Setup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VMS_Setup))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.P2_E_box = New System.Windows.Forms.TextBox()
        Me.P1_E_box = New System.Windows.Forms.TextBox()
        Me.B1_E_box = New System.Windows.Forms.TextBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.P2_W_box = New System.Windows.Forms.TextBox()
        Me.P1_W_box = New System.Windows.Forms.TextBox()
        Me.B1_W_box = New System.Windows.Forms.TextBox()
        Me.P5L_W_box = New System.Windows.Forms.TextBox()
        Me.B3_W_box = New System.Windows.Forms.TextBox()
        Me.P4_W_box = New System.Windows.Forms.TextBox()
        Me.P5L_E_box = New System.Windows.Forms.TextBox()
        Me.B3_E_box = New System.Windows.Forms.TextBox()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.TextBox20 = New System.Windows.Forms.TextBox()
        Me.TextBox21 = New System.Windows.Forms.TextBox()
        Me.TextBox22 = New System.Windows.Forms.TextBox()
        Me.ListBox5 = New System.Windows.Forms.ListBox()
        Me.TextBox23 = New System.Windows.Forms.TextBox()
        Me.TextBox24 = New System.Windows.Forms.TextBox()
        Me.TextBox25 = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.vms_ref_file_box = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ref_current_box = New System.Windows.Forms.TextBox()
        Me.sample_current_box = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox26 = New System.Windows.Forms.TextBox()
        Me.TextBox27 = New System.Windows.Forms.TextBox()
        Me.TextBox28 = New System.Windows.Forms.TextBox()
        Me.TextBox29 = New System.Windows.Forms.TextBox()
        Me.ListBox6 = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.P5R_W_box = New System.Windows.Forms.TextBox()
        Me.P5R_E_box = New System.Windows.Forms.TextBox()
        Me.B5_W_box = New System.Windows.Forms.TextBox()
        Me.B4_W_box = New System.Windows.Forms.TextBox()
        Me.B5_E_box = New System.Windows.Forms.TextBox()
        Me.B4_E_box = New System.Windows.Forms.TextBox()
        Me.P3_W_box = New System.Windows.Forms.TextBox()
        Me.P3_E_box = New System.Windows.Forms.TextBox()
        Me.P4_E_box = New System.Windows.Forms.TextBox()
        Me.B2_W_box = New System.Windows.Forms.TextBox()
        Me.B2_E_box = New System.Windows.Forms.TextBox()
        Me.ListBox7 = New System.Windows.Forms.ListBox()
        Me.Pu_Bkg_wt_err_Box = New System.Windows.Forms.TextBox()
        Me.Cal_R_UPu_err_Box = New System.Windows.Forms.TextBox()
        Me.Pu_Bkg_wt_Box = New System.Windows.Forms.TextBox()
        Me.Cal_R_UPu_Box = New System.Windows.Forms.TextBox()
        Me.uk2_L_BK_channels_box = New System.Windows.Forms.TextBox()
        Me.uk2_L_BK_E_box = New System.Windows.Forms.TextBox()
        Me.uk2_U_BK_E_box = New System.Windows.Forms.TextBox()
        Me.uk2_U_BK_channels_box = New System.Windows.Forms.TextBox()
        Me.uk1_U_BK_channels_box = New System.Windows.Forms.TextBox()
        Me.uk1_U_BK_E_box = New System.Windows.Forms.TextBox()
        Me.uk1_L_BK_channels_box = New System.Windows.Forms.TextBox()
        Me.uk1_L_BK_E_box = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Am_Bkg_wt_Box = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SelectVMSParameterFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveNewVMSParameterFIleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 20
        Me.ListBox1.Items.AddRange(New Object() {"Energy Calibration", "  Lower Peak Channel Number", "  Lower Peak Energy (keV)", "", "  Upper Peak Channel Number", "  Upper Peak Energy (keV)", " "})
        Me.ListBox1.Location = New System.Drawing.Point(12, 87)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(337, 144)
        Me.ListBox1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(242, 111)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(86, 20)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(242, 134)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(86, 20)
        Me.TextBox2.TabIndex = 2
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(242, 169)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(86, 20)
        Me.TextBox3.TabIndex = 3
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(242, 195)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(86, 20)
        Me.TextBox4.TabIndex = 4
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(197, 318)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(86, 20)
        Me.TextBox6.TabIndex = 8
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(197, 292)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(86, 20)
        Me.TextBox7.TabIndex = 7
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(198, 268)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(86, 20)
        Me.TextBox8.TabIndex = 6
        '
        'ListBox2
        '
        Me.ListBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 20
        Me.ListBox2.Items.AddRange(New Object() {"K-Edge Energies (keV)", "  Uranium", "  Plutonium", "  Americium"})
        Me.ListBox2.Location = New System.Drawing.Point(12, 248)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(294, 104)
        Me.ListBox2.TabIndex = 5
        '
        'P2_E_box
        '
        Me.P2_E_box.Location = New System.Drawing.Point(155, 470)
        Me.P2_E_box.Name = "P2_E_box"
        Me.P2_E_box.Size = New System.Drawing.Size(86, 20)
        Me.P2_E_box.TabIndex = 12
        '
        'P1_E_box
        '
        Me.P1_E_box.Location = New System.Drawing.Point(155, 445)
        Me.P1_E_box.Name = "P1_E_box"
        Me.P1_E_box.Size = New System.Drawing.Size(86, 20)
        Me.P1_E_box.TabIndex = 11
        '
        'B1_E_box
        '
        Me.B1_E_box.Location = New System.Drawing.Point(155, 422)
        Me.B1_E_box.Name = "B1_E_box"
        Me.B1_E_box.Size = New System.Drawing.Size(86, 20)
        Me.B1_E_box.TabIndex = 10
        '
        'ListBox3
        '
        Me.ListBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 24
        Me.ListBox3.Items.AddRange(New Object() {"  B1     Bkg 1", "  P1     UK2", "  P2     UK1", "  B2     SUML", "  P3     SUMPu", "  P4     SUMAM", "  B3     SUMR", "  P5     UKB", "  B4     Bkg", "  B5     SUMFP"})
        Me.ListBox3.Location = New System.Drawing.Point(12, 422)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(855, 244)
        Me.ListBox3.TabIndex = 9
        '
        'P2_W_box
        '
        Me.P2_W_box.Location = New System.Drawing.Point(257, 470)
        Me.P2_W_box.Name = "P2_W_box"
        Me.P2_W_box.Size = New System.Drawing.Size(86, 20)
        Me.P2_W_box.TabIndex = 15
        '
        'P1_W_box
        '
        Me.P1_W_box.Location = New System.Drawing.Point(257, 445)
        Me.P1_W_box.Name = "P1_W_box"
        Me.P1_W_box.Size = New System.Drawing.Size(86, 20)
        Me.P1_W_box.TabIndex = 14
        '
        'B1_W_box
        '
        Me.B1_W_box.Location = New System.Drawing.Point(257, 422)
        Me.B1_W_box.Name = "B1_W_box"
        Me.B1_W_box.Size = New System.Drawing.Size(86, 20)
        Me.B1_W_box.TabIndex = 13
        '
        'P5L_W_box
        '
        Me.P5L_W_box.Location = New System.Drawing.Point(257, 593)
        Me.P5L_W_box.Name = "P5L_W_box"
        Me.P5L_W_box.Size = New System.Drawing.Size(86, 20)
        Me.P5L_W_box.TabIndex = 21
        '
        'B3_W_box
        '
        Me.B3_W_box.Location = New System.Drawing.Point(257, 570)
        Me.B3_W_box.Name = "B3_W_box"
        Me.B3_W_box.Size = New System.Drawing.Size(86, 20)
        Me.B3_W_box.TabIndex = 20
        '
        'P4_W_box
        '
        Me.P4_W_box.Location = New System.Drawing.Point(257, 546)
        Me.P4_W_box.Name = "P4_W_box"
        Me.P4_W_box.Size = New System.Drawing.Size(86, 20)
        Me.P4_W_box.TabIndex = 19
        '
        'P5L_E_box
        '
        Me.P5L_E_box.Location = New System.Drawing.Point(155, 593)
        Me.P5L_E_box.Name = "P5L_E_box"
        Me.P5L_E_box.Size = New System.Drawing.Size(86, 20)
        Me.P5L_E_box.TabIndex = 18
        '
        'B3_E_box
        '
        Me.B3_E_box.Location = New System.Drawing.Point(155, 570)
        Me.B3_E_box.Name = "B3_E_box"
        Me.B3_E_box.Size = New System.Drawing.Size(86, 20)
        Me.B3_E_box.TabIndex = 17
        '
        'ListBox4
        '
        Me.ListBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.ItemHeight = 20
        Me.ListBox4.Items.AddRange(New Object() {"VMS Analysis XRF Analysis Windows ", "                                            Range (keV)                          " &
                "     "})
        Me.ListBox4.Location = New System.Drawing.Point(12, 369)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(855, 104)
        Me.ListBox4.TabIndex = 22
        '
        'TextBox20
        '
        Me.TextBox20.Location = New System.Drawing.Point(446, 316)
        Me.TextBox20.Name = "TextBox20"
        Me.TextBox20.Size = New System.Drawing.Size(86, 20)
        Me.TextBox20.TabIndex = 26
        '
        'TextBox21
        '
        Me.TextBox21.Location = New System.Drawing.Point(446, 292)
        Me.TextBox21.Name = "TextBox21"
        Me.TextBox21.Size = New System.Drawing.Size(86, 20)
        Me.TextBox21.TabIndex = 25
        '
        'TextBox22
        '
        Me.TextBox22.Location = New System.Drawing.Point(447, 268)
        Me.TextBox22.Name = "TextBox22"
        Me.TextBox22.Size = New System.Drawing.Size(86, 20)
        Me.TextBox22.TabIndex = 24
        '
        'ListBox5
        '
        Me.ListBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox5.FormattingEnabled = True
        Me.ListBox5.ItemHeight = 20
        Me.ListBox5.Items.AddRange(New Object() {"                           Delta mu           Offset", "  Uranium", "  Plutonium", "  Americium"})
        Me.ListBox5.Location = New System.Drawing.Point(342, 248)
        Me.ListBox5.Name = "ListBox5"
        Me.ListBox5.Size = New System.Drawing.Size(294, 104)
        Me.ListBox5.TabIndex = 23
        '
        'TextBox23
        '
        Me.TextBox23.Location = New System.Drawing.Point(538, 316)
        Me.TextBox23.Name = "TextBox23"
        Me.TextBox23.Size = New System.Drawing.Size(86, 20)
        Me.TextBox23.TabIndex = 29
        '
        'TextBox24
        '
        Me.TextBox24.Location = New System.Drawing.Point(538, 292)
        Me.TextBox24.Name = "TextBox24"
        Me.TextBox24.Size = New System.Drawing.Size(86, 20)
        Me.TextBox24.TabIndex = 28
        '
        'TextBox25
        '
        Me.TextBox25.Location = New System.Drawing.Point(539, 268)
        Me.TextBox25.Name = "TextBox25"
        Me.TextBox25.Size = New System.Drawing.Size(86, 20)
        Me.TextBox25.TabIndex = 27
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'vms_ref_file_box
        '
        Me.vms_ref_file_box.Location = New System.Drawing.Point(238, 684)
        Me.vms_ref_file_box.Name = "vms_ref_file_box"
        Me.vms_ref_file_box.Size = New System.Drawing.Size(412, 20)
        Me.vms_ref_file_box.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 685)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(166, 16)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "XRF Passive Spectrum"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(673, 680)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 27)
        Me.Button1.TabIndex = 32
        Me.Button1.Text = "Select New"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ref_current_box
        '
        Me.ref_current_box.Location = New System.Drawing.Point(196, 726)
        Me.ref_current_box.Name = "ref_current_box"
        Me.ref_current_box.Size = New System.Drawing.Size(86, 20)
        Me.ref_current_box.TabIndex = 33
        '
        'sample_current_box
        '
        Me.sample_current_box.Location = New System.Drawing.Point(425, 726)
        Me.sample_current_box.Name = "sample_current_box"
        Me.sample_current_box.Size = New System.Drawing.Size(86, 20)
        Me.sample_current_box.TabIndex = 34
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 727)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 16)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Reference Current"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(303, 726)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 16)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Sample  Current"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(732, 87)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(94, 47)
        Me.Button2.TabIndex = 37
        Me.Button2.Text = "Make Current"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox26
        '
        Me.TextBox26.Location = New System.Drawing.Point(512, 111)
        Me.TextBox26.Name = "TextBox26"
        Me.TextBox26.Size = New System.Drawing.Size(86, 20)
        Me.TextBox26.TabIndex = 41
        '
        'TextBox27
        '
        Me.TextBox27.Location = New System.Drawing.Point(512, 134)
        Me.TextBox27.Name = "TextBox27"
        Me.TextBox27.Size = New System.Drawing.Size(86, 20)
        Me.TextBox27.TabIndex = 40
        '
        'TextBox28
        '
        Me.TextBox28.Location = New System.Drawing.Point(512, 169)
        Me.TextBox28.Name = "TextBox28"
        Me.TextBox28.Size = New System.Drawing.Size(86, 20)
        Me.TextBox28.TabIndex = 39
        '
        'TextBox29
        '
        Me.TextBox29.Location = New System.Drawing.Point(512, 195)
        Me.TextBox29.Name = "TextBox29"
        Me.TextBox29.Size = New System.Drawing.Size(86, 20)
        Me.TextBox29.TabIndex = 38
        '
        'ListBox6
        '
        Me.ListBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox6.FormattingEnabled = True
        Me.ListBox6.ItemHeight = 20
        Me.ListBox6.Items.AddRange(New Object() {"Background Regions of Interest (keV)", "  Lower Left", "  Lower Right", "", "  Upper Left", "  Upper Right", " "})
        Me.ListBox6.Location = New System.Drawing.Point(385, 87)
        Me.ListBox6.Name = "ListBox6"
        Me.ListBox6.Size = New System.Drawing.Size(284, 144)
        Me.ListBox6.TabIndex = 42
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(285, 16)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Parameters for Traditional XRF Analysis"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(222, 16)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Used for automatic parameter seed."
        '
        'P5R_W_box
        '
        Me.P5R_W_box.Location = New System.Drawing.Point(471, 593)
        Me.P5R_W_box.Name = "P5R_W_box"
        Me.P5R_W_box.Size = New System.Drawing.Size(86, 20)
        Me.P5R_W_box.TabIndex = 46
        '
        'P5R_E_box
        '
        Me.P5R_E_box.Location = New System.Drawing.Point(369, 593)
        Me.P5R_E_box.Name = "P5R_E_box"
        Me.P5R_E_box.Size = New System.Drawing.Size(86, 20)
        Me.P5R_E_box.TabIndex = 45
        '
        'B5_W_box
        '
        Me.B5_W_box.Location = New System.Drawing.Point(257, 641)
        Me.B5_W_box.Name = "B5_W_box"
        Me.B5_W_box.Size = New System.Drawing.Size(86, 20)
        Me.B5_W_box.TabIndex = 50
        '
        'B4_W_box
        '
        Me.B4_W_box.Location = New System.Drawing.Point(257, 617)
        Me.B4_W_box.Name = "B4_W_box"
        Me.B4_W_box.Size = New System.Drawing.Size(86, 20)
        Me.B4_W_box.TabIndex = 49
        '
        'B5_E_box
        '
        Me.B5_E_box.Location = New System.Drawing.Point(155, 641)
        Me.B5_E_box.Name = "B5_E_box"
        Me.B5_E_box.Size = New System.Drawing.Size(86, 20)
        Me.B5_E_box.TabIndex = 48
        '
        'B4_E_box
        '
        Me.B4_E_box.Location = New System.Drawing.Point(155, 617)
        Me.B4_E_box.Name = "B4_E_box"
        Me.B4_E_box.Size = New System.Drawing.Size(86, 20)
        Me.B4_E_box.TabIndex = 47
        '
        'P3_W_box
        '
        Me.P3_W_box.Location = New System.Drawing.Point(257, 520)
        Me.P3_W_box.Name = "P3_W_box"
        Me.P3_W_box.Size = New System.Drawing.Size(86, 20)
        Me.P3_W_box.TabIndex = 52
        '
        'P3_E_box
        '
        Me.P3_E_box.Location = New System.Drawing.Point(155, 520)
        Me.P3_E_box.Name = "P3_E_box"
        Me.P3_E_box.Size = New System.Drawing.Size(86, 20)
        Me.P3_E_box.TabIndex = 51
        '
        'P4_E_box
        '
        Me.P4_E_box.Location = New System.Drawing.Point(155, 546)
        Me.P4_E_box.Name = "P4_E_box"
        Me.P4_E_box.Size = New System.Drawing.Size(86, 20)
        Me.P4_E_box.TabIndex = 53
        '
        'B2_W_box
        '
        Me.B2_W_box.Location = New System.Drawing.Point(257, 496)
        Me.B2_W_box.Name = "B2_W_box"
        Me.B2_W_box.Size = New System.Drawing.Size(86, 20)
        Me.B2_W_box.TabIndex = 56
        '
        'B2_E_box
        '
        Me.B2_E_box.Location = New System.Drawing.Point(155, 496)
        Me.B2_E_box.Name = "B2_E_box"
        Me.B2_E_box.Size = New System.Drawing.Size(86, 20)
        Me.B2_E_box.TabIndex = 55
        '
        'ListBox7
        '
        Me.ListBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox7.FormattingEnabled = True
        Me.ListBox7.ItemHeight = 20
        Me.ListBox7.Items.AddRange(New Object() {"                                      Value           Error", "  R_UPu", "  Pu Bk Weight", "  Am Bk Weight"})
        Me.ListBox7.Location = New System.Drawing.Point(659, 248)
        Me.ListBox7.Name = "ListBox7"
        Me.ListBox7.Size = New System.Drawing.Size(307, 104)
        Me.ListBox7.TabIndex = 57
        '
        'Pu_Bkg_wt_err_Box
        '
        Me.Pu_Bkg_wt_err_Box.Location = New System.Drawing.Point(873, 293)
        Me.Pu_Bkg_wt_err_Box.Name = "Pu_Bkg_wt_err_Box"
        Me.Pu_Bkg_wt_err_Box.Size = New System.Drawing.Size(86, 20)
        Me.Pu_Bkg_wt_err_Box.TabIndex = 61
        '
        'Cal_R_UPu_err_Box
        '
        Me.Cal_R_UPu_err_Box.Location = New System.Drawing.Point(873, 271)
        Me.Cal_R_UPu_err_Box.Name = "Cal_R_UPu_err_Box"
        Me.Cal_R_UPu_err_Box.Size = New System.Drawing.Size(86, 20)
        Me.Cal_R_UPu_err_Box.TabIndex = 60
        '
        'Pu_Bkg_wt_Box
        '
        Me.Pu_Bkg_wt_Box.Location = New System.Drawing.Point(781, 293)
        Me.Pu_Bkg_wt_Box.Name = "Pu_Bkg_wt_Box"
        Me.Pu_Bkg_wt_Box.Size = New System.Drawing.Size(86, 20)
        Me.Pu_Bkg_wt_Box.TabIndex = 59
        '
        'Cal_R_UPu_Box
        '
        Me.Cal_R_UPu_Box.Location = New System.Drawing.Point(781, 271)
        Me.Cal_R_UPu_Box.Name = "Cal_R_UPu_Box"
        Me.Cal_R_UPu_Box.Size = New System.Drawing.Size(86, 20)
        Me.Cal_R_UPu_Box.TabIndex = 58
        '
        'uk2_L_BK_channels_box
        '
        Me.uk2_L_BK_channels_box.Location = New System.Drawing.Point(462, 445)
        Me.uk2_L_BK_channels_box.Name = "uk2_L_BK_channels_box"
        Me.uk2_L_BK_channels_box.Size = New System.Drawing.Size(86, 20)
        Me.uk2_L_BK_channels_box.TabIndex = 63
        '
        'uk2_L_BK_E_box
        '
        Me.uk2_L_BK_E_box.Location = New System.Drawing.Point(360, 445)
        Me.uk2_L_BK_E_box.Name = "uk2_L_BK_E_box"
        Me.uk2_L_BK_E_box.Size = New System.Drawing.Size(86, 20)
        Me.uk2_L_BK_E_box.TabIndex = 62
        '
        'uk2_U_BK_E_box
        '
        Me.uk2_U_BK_E_box.Location = New System.Drawing.Point(571, 445)
        Me.uk2_U_BK_E_box.Name = "uk2_U_BK_E_box"
        Me.uk2_U_BK_E_box.Size = New System.Drawing.Size(86, 20)
        Me.uk2_U_BK_E_box.TabIndex = 64
        '
        'uk2_U_BK_channels_box
        '
        Me.uk2_U_BK_channels_box.Location = New System.Drawing.Point(673, 445)
        Me.uk2_U_BK_channels_box.Name = "uk2_U_BK_channels_box"
        Me.uk2_U_BK_channels_box.Size = New System.Drawing.Size(86, 20)
        Me.uk2_U_BK_channels_box.TabIndex = 65
        '
        'uk1_U_BK_channels_box
        '
        Me.uk1_U_BK_channels_box.Location = New System.Drawing.Point(673, 470)
        Me.uk1_U_BK_channels_box.Name = "uk1_U_BK_channels_box"
        Me.uk1_U_BK_channels_box.Size = New System.Drawing.Size(86, 20)
        Me.uk1_U_BK_channels_box.TabIndex = 69
        '
        'uk1_U_BK_E_box
        '
        Me.uk1_U_BK_E_box.Location = New System.Drawing.Point(571, 470)
        Me.uk1_U_BK_E_box.Name = "uk1_U_BK_E_box"
        Me.uk1_U_BK_E_box.Size = New System.Drawing.Size(86, 20)
        Me.uk1_U_BK_E_box.TabIndex = 68
        '
        'uk1_L_BK_channels_box
        '
        Me.uk1_L_BK_channels_box.Location = New System.Drawing.Point(462, 470)
        Me.uk1_L_BK_channels_box.Name = "uk1_L_BK_channels_box"
        Me.uk1_L_BK_channels_box.Size = New System.Drawing.Size(86, 20)
        Me.uk1_L_BK_channels_box.TabIndex = 67
        '
        'uk1_L_BK_E_box
        '
        Me.uk1_L_BK_E_box.Location = New System.Drawing.Point(360, 470)
        Me.uk1_L_BK_E_box.Name = "uk1_L_BK_E_box"
        Me.uk1_L_BK_E_box.Size = New System.Drawing.Size(86, 20)
        Me.uk1_L_BK_E_box.TabIndex = 66
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(873, 316)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(86, 20)
        Me.TextBox5.TabIndex = 71
        '
        'Am_Bkg_wt_Box
        '
        Me.Am_Bkg_wt_Box.Location = New System.Drawing.Point(781, 316)
        Me.Am_Bkg_wt_Box.Name = "Am_Bkg_wt_Box"
        Me.Am_Bkg_wt_Box.Size = New System.Drawing.Size(86, 20)
        Me.Am_Bkg_wt_Box.TabIndex = 70
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectVMSParameterFileToolStripMenuItem, Me.SaveNewVMSParameterFIleToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1007, 24)
        Me.MenuStrip1.TabIndex = 72
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SelectVMSParameterFileToolStripMenuItem
        '
        Me.SelectVMSParameterFileToolStripMenuItem.Name = "SelectVMSParameterFileToolStripMenuItem"
        Me.SelectVMSParameterFileToolStripMenuItem.Size = New System.Drawing.Size(155, 20)
        Me.SelectVMSParameterFileToolStripMenuItem.Text = "Select VMS Parameter File"
        '
        'SaveNewVMSParameterFIleToolStripMenuItem
        '
        Me.SaveNewVMSParameterFIleToolStripMenuItem.Name = "SaveNewVMSParameterFIleToolStripMenuItem"
        Me.SaveNewVMSParameterFIleToolStripMenuItem.Size = New System.Drawing.Size(175, 20)
        Me.SaveNewVMSParameterFIleToolStripMenuItem.Text = "Save New VMS Parameter FIle"
        '
        'VMS_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 757)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Am_Bkg_wt_Box)
        Me.Controls.Add(Me.uk1_U_BK_channels_box)
        Me.Controls.Add(Me.uk1_U_BK_E_box)
        Me.Controls.Add(Me.uk1_L_BK_channels_box)
        Me.Controls.Add(Me.uk1_L_BK_E_box)
        Me.Controls.Add(Me.uk2_U_BK_channels_box)
        Me.Controls.Add(Me.uk2_U_BK_E_box)
        Me.Controls.Add(Me.uk2_L_BK_channels_box)
        Me.Controls.Add(Me.uk2_L_BK_E_box)
        Me.Controls.Add(Me.Pu_Bkg_wt_err_Box)
        Me.Controls.Add(Me.Cal_R_UPu_err_Box)
        Me.Controls.Add(Me.Pu_Bkg_wt_Box)
        Me.Controls.Add(Me.Cal_R_UPu_Box)
        Me.Controls.Add(Me.ListBox7)
        Me.Controls.Add(Me.B2_W_box)
        Me.Controls.Add(Me.B2_E_box)
        Me.Controls.Add(Me.P4_E_box)
        Me.Controls.Add(Me.P3_W_box)
        Me.Controls.Add(Me.P3_E_box)
        Me.Controls.Add(Me.B5_W_box)
        Me.Controls.Add(Me.B4_W_box)
        Me.Controls.Add(Me.B5_E_box)
        Me.Controls.Add(Me.B4_E_box)
        Me.Controls.Add(Me.P5R_W_box)
        Me.Controls.Add(Me.P5R_E_box)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox26)
        Me.Controls.Add(Me.TextBox27)
        Me.Controls.Add(Me.TextBox28)
        Me.Controls.Add(Me.TextBox29)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.sample_current_box)
        Me.Controls.Add(Me.ref_current_box)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.vms_ref_file_box)
        Me.Controls.Add(Me.TextBox23)
        Me.Controls.Add(Me.TextBox24)
        Me.Controls.Add(Me.TextBox25)
        Me.Controls.Add(Me.TextBox20)
        Me.Controls.Add(Me.TextBox21)
        Me.Controls.Add(Me.TextBox22)
        Me.Controls.Add(Me.ListBox5)
        Me.Controls.Add(Me.P5L_W_box)
        Me.Controls.Add(Me.B3_W_box)
        Me.Controls.Add(Me.P4_W_box)
        Me.Controls.Add(Me.P5L_E_box)
        Me.Controls.Add(Me.B3_E_box)
        Me.Controls.Add(Me.P2_W_box)
        Me.Controls.Add(Me.P1_W_box)
        Me.Controls.Add(Me.B1_W_box)
        Me.Controls.Add(Me.P2_E_box)
        Me.Controls.Add(Me.P1_E_box)
        Me.Controls.Add(Me.B1_E_box)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox7)
        Me.Controls.Add(Me.TextBox8)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.ListBox4)
        Me.Controls.Add(Me.ListBox6)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "VMS_Setup"
        Me.Text = "VMS Analysis Parameters"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents P2_E_box As TextBox
    Friend WithEvents P1_E_box As TextBox
    Friend WithEvents B1_E_box As TextBox
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents P2_W_box As TextBox
    Friend WithEvents P1_W_box As TextBox
    Friend WithEvents B1_W_box As TextBox
    Friend WithEvents P5L_W_box As TextBox
    Friend WithEvents B3_W_box As TextBox
    Friend WithEvents P4_W_box As TextBox
    Friend WithEvents P5L_E_box As TextBox
    Friend WithEvents B3_E_box As TextBox
    Friend WithEvents ListBox4 As ListBox
    Friend WithEvents TextBox20 As TextBox
    Friend WithEvents TextBox21 As TextBox
    Friend WithEvents TextBox22 As TextBox
    Friend WithEvents ListBox5 As ListBox
    Friend WithEvents TextBox23 As TextBox
    Friend WithEvents TextBox24 As TextBox
    Friend WithEvents TextBox25 As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents vms_ref_file_box As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ref_current_box As TextBox
    Friend WithEvents sample_current_box As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox26 As TextBox
    Friend WithEvents TextBox27 As TextBox
    Friend WithEvents TextBox28 As TextBox
    Friend WithEvents TextBox29 As TextBox
    Friend WithEvents ListBox6 As ListBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents P5R_W_box As TextBox
    Friend WithEvents P5R_E_box As TextBox
    Friend WithEvents B5_W_box As TextBox
    Friend WithEvents B4_W_box As TextBox
    Friend WithEvents B5_E_box As TextBox
    Friend WithEvents B4_E_box As TextBox
    Friend WithEvents P3_W_box As TextBox
    Friend WithEvents P3_E_box As TextBox
    Friend WithEvents P4_E_box As TextBox
    Friend WithEvents B2_W_box As TextBox
    Friend WithEvents B2_E_box As TextBox
    Friend WithEvents ListBox7 As ListBox
    Friend WithEvents Pu_Bkg_wt_err_Box As TextBox
    Friend WithEvents Cal_R_UPu_err_Box As TextBox
    Friend WithEvents Pu_Bkg_wt_Box As TextBox
    Friend WithEvents Cal_R_UPu_Box As TextBox
    Friend WithEvents uk2_L_BK_channels_box As TextBox
    Friend WithEvents uk2_L_BK_E_box As TextBox
    Friend WithEvents uk2_U_BK_E_box As TextBox
    Friend WithEvents uk2_U_BK_channels_box As TextBox
    Friend WithEvents uk1_U_BK_channels_box As TextBox
    Friend WithEvents uk1_U_BK_E_box As TextBox
    Friend WithEvents uk1_L_BK_channels_box As TextBox
    Friend WithEvents uk1_L_BK_E_box As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Am_Bkg_wt_Box As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SelectVMSParameterFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveNewVMSParameterFIleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
