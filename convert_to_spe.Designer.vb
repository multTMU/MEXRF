<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Convert_CNF_Files
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
        Dim ListBox3 As System.Windows.Forms.ListBox
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Convert_CNF_Files))
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.sampleinfoBox5 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox4 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox3 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox2 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.select_files_Button = New System.Windows.Forms.Button()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SampleIDBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.filenamebox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Cycle_number_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IN_4K_BOX = New System.Windows.Forms.CheckBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.IN_2K_BOX = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.IN_8K_BOX = New System.Windows.Forms.CheckBox()
        Me.IN_16K_BOX = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Out_2K_Box = New System.Windows.Forms.CheckBox()
        Me.Out_4K_Box = New System.Windows.Forms.CheckBox()
        Me.in_chan_box = New System.Windows.Forms.MaskedTextBox()
        Me.Read_status_label = New System.Windows.Forms.Label()
        ListBox3 = New System.Windows.Forms.ListBox()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox3
        '
        ListBox3.BackColor = System.Drawing.SystemColors.Window
        ListBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        ListBox3.CausesValidation = False
        ListBox3.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ListBox3.ItemHeight = 23
        ListBox3.Items.AddRange(New Object() {"Cycle # :", "Vial Diameter :  ", "Sample Temperature :  ", "U235 enrichment :  ", "Pu atomic weight :  ", "Live Time :", "Channels:"})
        ListBox3.Location = New System.Drawing.Point(7, 5)
        ListBox3.MinimumSize = New System.Drawing.Size(0, 160)
        ListBox3.Name = "ListBox3"
        ListBox3.Size = New System.Drawing.Size(251, 184)
        ListBox3.TabIndex = 34
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'sampleinfoBox5
        '
        Me.sampleinfoBox5.Location = New System.Drawing.Point(170, 127)
        Me.sampleinfoBox5.Name = "sampleinfoBox5"
        Me.sampleinfoBox5.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox5.TabIndex = 88
        Me.ToolTip1.SetToolTip(Me.sampleinfoBox5, "Value in AMU")
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
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(19, 338)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(210, 32)
        Me.Button1.TabIndex = 99
        Me.Button1.Text = "Save CNF Spectra as .SPE file"
        Me.ToolTip1.SetToolTip(Me.Button1, "Reads in 1 to 16 files" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "File formats accepted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     CNF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     SPE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     RES  (MEK" &
        "ED results file)")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'select_files_Button
        '
        Me.select_files_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.select_files_Button.Location = New System.Drawing.Point(15, 27)
        Me.select_files_Button.Name = "select_files_Button"
        Me.select_files_Button.Size = New System.Drawing.Size(129, 23)
        Me.select_files_Button.TabIndex = 98
        Me.select_files_Button.Text = "Select XRF Files"
        Me.ToolTip1.SetToolTip(Me.select_files_Button, "Reads in 1 to 16 files" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "File formats accepted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     CNF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     SPE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     RES  (MEK" &
        "ED results file)")
        Me.select_files_Button.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 244)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 16)
        Me.Label3.TabIndex = 105
        Me.Label3.Text = "File Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 219)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Sample ID:"
        '
        'SampleIDBox1
        '
        Me.SampleIDBox1.Enabled = False
        Me.SampleIDBox1.Location = New System.Drawing.Point(97, 217)
        Me.SampleIDBox1.Name = "SampleIDBox1"
        Me.SampleIDBox1.Size = New System.Drawing.Size(171, 20)
        Me.SampleIDBox1.TabIndex = 103
        '
        'filenamebox1
        '
        Me.filenamebox1.Enabled = False
        Me.filenamebox1.Location = New System.Drawing.Point(97, 243)
        Me.filenamebox1.Name = "filenamebox1"
        Me.filenamebox1.Size = New System.Drawing.Size(744, 20)
        Me.filenamebox1.TabIndex = 102
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.Window
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.in_chan_box)
        Me.Panel4.Controls.Add(Me.sampleinfoBox5)
        Me.Panel4.Controls.Add(Me.Cycle_number_Box)
        Me.Panel4.Controls.Add(Me.sampleinfoBox4)
        Me.Panel4.Controls.Add(Me.sampleinfoBox3)
        Me.Panel4.Controls.Add(Me.sampleinfoBox2)
        Me.Panel4.Controls.Add(Me.sampleinfoBox1)
        Me.Panel4.Controls.Add(ListBox3)
        Me.Panel4.Location = New System.Drawing.Point(723, 33)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(260, 188)
        Me.Panel4.TabIndex = 101
        '
        'Cycle_number_Box
        '
        Me.Cycle_number_Box.Location = New System.Drawing.Point(170, 5)
        Me.Cycle_number_Box.Name = "Cycle_number_Box"
        Me.Cycle_number_Box.Size = New System.Drawing.Size(79, 20)
        Me.Cycle_number_Box.TabIndex = 47
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(722, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 100
        Me.Label1.Text = "Sample info from data file"
        '
        'IN_4K_BOX
        '
        Me.IN_4K_BOX.AutoSize = True
        Me.IN_4K_BOX.Location = New System.Drawing.Point(100, 6)
        Me.IN_4K_BOX.Name = "IN_4K_BOX"
        Me.IN_4K_BOX.Size = New System.Drawing.Size(84, 17)
        Me.IN_4K_BOX.TabIndex = 97
        Me.IN_4K_BOX.Text = "4k channels"
        Me.IN_4K_BOX.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(15, 66)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(623, 134)
        Me.ListBox1.TabIndex = 96
        '
        'IN_2K_BOX
        '
        Me.IN_2K_BOX.AutoSize = True
        Me.IN_2K_BOX.Checked = True
        Me.IN_2K_BOX.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IN_2K_BOX.Location = New System.Drawing.Point(10, 6)
        Me.IN_2K_BOX.Name = "IN_2K_BOX"
        Me.IN_2K_BOX.Size = New System.Drawing.Size(84, 17)
        Me.IN_2K_BOX.TabIndex = 106
        Me.IN_2K_BOX.Text = "2k channels"
        Me.IN_2K_BOX.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.IN_16K_BOX)
        Me.Panel1.Controls.Add(Me.IN_8K_BOX)
        Me.Panel1.Controls.Add(Me.IN_2K_BOX)
        Me.Panel1.Controls.Add(Me.IN_4K_BOX)
        Me.Panel1.Location = New System.Drawing.Point(174, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(398, 32)
        Me.Panel1.TabIndex = 107
        '
        'IN_8K_BOX
        '
        Me.IN_8K_BOX.AutoSize = True
        Me.IN_8K_BOX.Location = New System.Drawing.Point(194, 6)
        Me.IN_8K_BOX.Name = "IN_8K_BOX"
        Me.IN_8K_BOX.Size = New System.Drawing.Size(84, 17)
        Me.IN_8K_BOX.TabIndex = 107
        Me.IN_8K_BOX.Text = "8k channels"
        Me.IN_8K_BOX.UseVisualStyleBackColor = True
        '
        'IN_16K_BOX
        '
        Me.IN_16K_BOX.AutoSize = True
        Me.IN_16K_BOX.Location = New System.Drawing.Point(294, 6)
        Me.IN_16K_BOX.Name = "IN_16K_BOX"
        Me.IN_16K_BOX.Size = New System.Drawing.Size(90, 17)
        Me.IN_16K_BOX.TabIndex = 108
        Me.IN_16K_BOX.Text = "16k channels"
        Me.IN_16K_BOX.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Out_2K_Box)
        Me.Panel2.Controls.Add(Me.Out_4K_Box)
        Me.Panel2.Location = New System.Drawing.Point(268, 338)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(106, 57)
        Me.Panel2.TabIndex = 109
        '
        'Out_2K_Box
        '
        Me.Out_2K_Box.AutoSize = True
        Me.Out_2K_Box.Location = New System.Drawing.Point(10, 6)
        Me.Out_2K_Box.Name = "Out_2K_Box"
        Me.Out_2K_Box.Size = New System.Drawing.Size(84, 17)
        Me.Out_2K_Box.TabIndex = 106
        Me.Out_2K_Box.Text = "2k channels"
        Me.Out_2K_Box.UseVisualStyleBackColor = True
        '
        'Out_4K_Box
        '
        Me.Out_4K_Box.AutoSize = True
        Me.Out_4K_Box.Location = New System.Drawing.Point(10, 29)
        Me.Out_4K_Box.Name = "Out_4K_Box"
        Me.Out_4K_Box.Size = New System.Drawing.Size(84, 17)
        Me.Out_4K_Box.TabIndex = 97
        Me.Out_4K_Box.Text = "4k channels"
        Me.Out_4K_Box.UseVisualStyleBackColor = True
        '
        'in_chan_box
        '
        Me.in_chan_box.Location = New System.Drawing.Point(170, 151)
        Me.in_chan_box.Name = "in_chan_box"
        Me.in_chan_box.Size = New System.Drawing.Size(79, 20)
        Me.in_chan_box.TabIndex = 89
        Me.ToolTip1.SetToolTip(Me.in_chan_box, "Value in AMU")
        '
        'Read_status_label
        '
        Me.Read_status_label.AutoSize = True
        Me.Read_status_label.Location = New System.Drawing.Point(171, 294)
        Me.Read_status_label.Name = "Read_status_label"
        Me.Read_status_label.Size = New System.Drawing.Size(64, 13)
        Me.Read_status_label.TabIndex = 110
        Me.Read_status_label.Text = "Read status"
        '
        'Convert_CNF_Files
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 450)
        Me.Controls.Add(Me.Read_status_label)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SampleIDBox1)
        Me.Controls.Add(Me.filenamebox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.select_files_Button)
        Me.Controls.Add(Me.ListBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Convert_CNF_Files"
        Me.Text = "Convert CNF to SPE"
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog2 As SaveFileDialog
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SampleIDBox1 As MaskedTextBox
    Friend WithEvents filenamebox1 As MaskedTextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents sampleinfoBox5 As MaskedTextBox
    Friend WithEvents Cycle_number_Box As MaskedTextBox
    Friend WithEvents sampleinfoBox4 As MaskedTextBox
    Friend WithEvents sampleinfoBox3 As MaskedTextBox
    Friend WithEvents sampleinfoBox2 As MaskedTextBox
    Friend WithEvents sampleinfoBox1 As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents select_files_Button As Button
    Friend WithEvents IN_4K_BOX As CheckBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Out_2K_Box As CheckBox
    Friend WithEvents Out_4K_Box As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents IN_16K_BOX As CheckBox
    Friend WithEvents IN_8K_BOX As CheckBox
    Friend WithEvents IN_2K_BOX As CheckBox
    Friend WithEvents in_chan_box As MaskedTextBox
    Friend WithEvents Read_status_label As Label
End Class
