<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Set_directories
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Set_directories))
        Me.results_box = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.atten_param_box = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.root_box = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.exe_root_box = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.genie2k_box = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.system_constants_box = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.seed_parms_box = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.executable_box = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.drive_letter_Box = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.vms_consts_box = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.summary_box = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.xray_data_box = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.net_spec_box = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.convert_files_box = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.summed_files_box = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'results_box
        '
        Me.results_box.Location = New System.Drawing.Point(220, 352)
        Me.results_box.Name = "results_box"
        Me.results_box.Size = New System.Drawing.Size(423, 20)
        Me.results_box.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(108, 355)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Result Files Directory"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(82, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(132, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Attnuation Coefficients File"
        '
        'atten_param_box
        '
        Me.atten_param_box.Location = New System.Drawing.Point(220, 172)
        Me.atten_param_box.Name = "atten_param_box"
        Me.atten_param_box.Size = New System.Drawing.Size(423, 20)
        Me.atten_param_box.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(99, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "XRF_FIT root directory"
        '
        'root_box
        '
        Me.root_box.Location = New System.Drawing.Point(220, 67)
        Me.root_box.Name = "root_box"
        Me.root_box.Size = New System.Drawing.Size(423, 20)
        Me.root_box.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(37, 445)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "MEXRF executable directory prefix"
        '
        'exe_root_box
        '
        Me.exe_root_box.Location = New System.Drawing.Point(220, 442)
        Me.exe_root_box.Name = "exe_root_box"
        Me.exe_root_box.Size = New System.Drawing.Size(423, 20)
        Me.exe_root_box.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(86, 471)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Genie 2k exefiles location"
        '
        'genie2k_box
        '
        Me.genie2k_box.Location = New System.Drawing.Point(220, 468)
        Me.genie2k_box.Name = "genie2k_box"
        Me.genie2k_box.Size = New System.Drawing.Size(423, 20)
        Me.genie2k_box.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(79, 121)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(135, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "System Constants Location"
        '
        'system_constants_box
        '
        Me.system_constants_box.Location = New System.Drawing.Point(220, 118)
        Me.system_constants_box.Name = "system_constants_box"
        Me.system_constants_box.Size = New System.Drawing.Size(423, 20)
        Me.system_constants_box.TabIndex = 10
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Title = "Open Data File"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(79, 149)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Seed Parameters Location"
        '
        'seed_parms_box
        '
        Me.seed_parms_box.Location = New System.Drawing.Point(220, 146)
        Me.seed_parms_box.Name = "seed_parms_box"
        Me.seed_parms_box.Size = New System.Drawing.Size(423, 20)
        Me.seed_parms_box.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(86, 419)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(128, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "MEXRF executable name"
        '
        'executable_box
        '
        Me.executable_box.Location = New System.Drawing.Point(220, 416)
        Me.executable_box.Name = "executable_box"
        Me.executable_box.Size = New System.Drawing.Size(423, 20)
        Me.executable_box.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(129, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Root drive letter"
        '
        'drive_letter_Box
        '
        Me.drive_letter_Box.Location = New System.Drawing.Point(220, 41)
        Me.drive_letter_Box.Name = "drive_letter_Box"
        Me.drive_letter_Box.Size = New System.Drawing.Size(423, 20)
        Me.drive_letter_Box.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(104, 228)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "VMS Coefficients File"
        '
        'vms_consts_box
        '
        Me.vms_consts_box.Location = New System.Drawing.Point(220, 225)
        Me.vms_consts_box.Name = "vms_consts_box"
        Me.vms_consts_box.Size = New System.Drawing.Size(423, 20)
        Me.vms_consts_box.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(92, 381)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(119, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Summary Files Directory"
        '
        'summary_box
        '
        Me.summary_box.Location = New System.Drawing.Point(220, 378)
        Me.summary_box.Name = "summary_box"
        Me.summary_box.Size = New System.Drawing.Size(423, 20)
        Me.summary_box.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(135, 202)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "X-ray Data File"
        '
        'xray_data_box
        '
        Me.xray_data_box.Location = New System.Drawing.Point(220, 199)
        Me.xray_data_box.Name = "xray_data_box"
        Me.xray_data_box.Size = New System.Drawing.Size(423, 20)
        Me.xray_data_box.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(77, 267)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(136, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Net Spectrum File Directory"
        '
        'net_spec_box
        '
        Me.net_spec_box.Location = New System.Drawing.Point(220, 264)
        Me.net_spec_box.Name = "net_spec_box"
        Me.net_spec_box.Size = New System.Drawing.Size(423, 20)
        Me.net_spec_box.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(86, 294)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(125, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Converted Files Directory"
        '
        'convert_files_box
        '
        Me.convert_files_box.Location = New System.Drawing.Point(220, 291)
        Me.convert_files_box.Name = "convert_files_box"
        Me.convert_files_box.Size = New System.Drawing.Size(423, 20)
        Me.convert_files_box.TabIndex = 26
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(86, 320)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Summed Files Directory"
        '
        'summed_files_box
        '
        Me.summed_files_box.Location = New System.Drawing.Point(220, 317)
        Me.summed_files_box.Name = "summed_files_box"
        Me.summed_files_box.Size = New System.Drawing.Size(423, 20)
        Me.summed_files_box.TabIndex = 28
        '
        'Set_directories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 574)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.summed_files_box)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.convert_files_box)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.net_spec_box)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.xray_data_box)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.summary_box)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.vms_consts_box)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.drive_letter_Box)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.executable_box)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.seed_parms_box)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.system_constants_box)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.genie2k_box)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.exe_root_box)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.root_box)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.atten_param_box)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.results_box)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Set_directories"
        Me.Text = "Configuration Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents results_box As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents atten_param_box As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents root_box As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents exe_root_box As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents genie2k_box As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents system_constants_box As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Label7 As Label
    Friend WithEvents seed_parms_box As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents executable_box As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents drive_letter_Box As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents vms_consts_box As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents summary_box As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents xray_data_box As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents net_spec_box As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents convert_files_box As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents summed_files_box As TextBox
End Class
