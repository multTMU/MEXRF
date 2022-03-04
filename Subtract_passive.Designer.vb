<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Subtract_passive
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
        Dim ListBox4 As System.Windows.Forms.ListBox
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Subtract_passive))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SampleIDBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.filenamebox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.sampleinfoBox5 = New System.Windows.Forms.MaskedTextBox()
        Me.Cycle_number_Box = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox4 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox3 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox2 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.select_files_Button = New System.Windows.Forms.Button()
        Me.nchan_checkbox = New System.Windows.Forms.CheckBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.select_passive_files = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.sampleinfoBox25 = New System.Windows.Forms.MaskedTextBox()
        Me.Cycle_number_Box2 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox24 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox23 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox22 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox21 = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SampleIDBox21 = New System.Windows.Forms.MaskedTextBox()
        Me.filenamebox2 = New System.Windows.Forms.MaskedTextBox()
        ListBox3 = New System.Windows.Forms.ListBox()
        ListBox4 = New System.Windows.Forms.ListBox()
        Me.Panel4.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        'ListBox4
        '
        ListBox4.BackColor = System.Drawing.SystemColors.Window
        ListBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        ListBox4.CausesValidation = False
        ListBox4.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ListBox4.ItemHeight = 23
        ListBox4.Items.AddRange(New Object() {"Cycle # :", "Vial Diameter :  ", "Sample Temperature :  ", "U235 enrichment :  ", "Pu atomic weight :  ", "Live Time :"})
        ListBox4.Location = New System.Drawing.Point(2, 5)
        ListBox4.MinimumSize = New System.Drawing.Size(0, 160)
        ListBox4.Name = "ListBox4"
        ListBox4.Size = New System.Drawing.Size(247, 138)
        ListBox4.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-127, 275)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 16)
        Me.Label3.TabIndex = 105
        Me.Label3.Text = "File Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-127, 250)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Sample ID:"
        '
        'SampleIDBox1
        '
        Me.SampleIDBox1.Enabled = False
        Me.SampleIDBox1.Location = New System.Drawing.Point(12, 215)
        Me.SampleIDBox1.Name = "SampleIDBox1"
        Me.SampleIDBox1.Size = New System.Drawing.Size(259, 20)
        Me.SampleIDBox1.TabIndex = 103
        '
        'filenamebox1
        '
        Me.filenamebox1.Enabled = False
        Me.filenamebox1.Location = New System.Drawing.Point(12, 241)
        Me.filenamebox1.Name = "filenamebox1"
        Me.filenamebox1.Size = New System.Drawing.Size(832, 20)
        Me.filenamebox1.TabIndex = 102
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
        Me.Panel4.Location = New System.Drawing.Point(770, 39)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(270, 155)
        Me.Panel4.TabIndex = 101
        '
        'sampleinfoBox5
        '
        Me.sampleinfoBox5.Location = New System.Drawing.Point(170, 127)
        Me.sampleinfoBox5.Name = "sampleinfoBox5"
        Me.sampleinfoBox5.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox5.TabIndex = 88
        '
        'Cycle_number_Box
        '
        Me.Cycle_number_Box.Location = New System.Drawing.Point(170, 5)
        Me.Cycle_number_Box.Name = "Cycle_number_Box"
        Me.Cycle_number_Box.Size = New System.Drawing.Size(79, 20)
        Me.Cycle_number_Box.TabIndex = 47
        '
        'sampleinfoBox4
        '
        Me.sampleinfoBox4.Location = New System.Drawing.Point(170, 103)
        Me.sampleinfoBox4.Name = "sampleinfoBox4"
        Me.sampleinfoBox4.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox4.TabIndex = 38
        '
        'sampleinfoBox3
        '
        Me.sampleinfoBox3.Location = New System.Drawing.Point(170, 77)
        Me.sampleinfoBox3.Name = "sampleinfoBox3"
        Me.sampleinfoBox3.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox3.TabIndex = 37
        '
        'sampleinfoBox2
        '
        Me.sampleinfoBox2.Location = New System.Drawing.Point(170, 53)
        Me.sampleinfoBox2.Name = "sampleinfoBox2"
        Me.sampleinfoBox2.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox2.TabIndex = 36
        '
        'sampleinfoBox1
        '
        Me.sampleinfoBox1.Location = New System.Drawing.Point(170, 28)
        Me.sampleinfoBox1.Name = "sampleinfoBox1"
        Me.sampleinfoBox1.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox1.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(769, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 100
        Me.Label1.Text = "Sample info from data file"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(12, 541)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(216, 23)
        Me.Button1.TabIndex = 99
        Me.Button1.Text = "Save Net Spectra as .SPE files"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'select_files_Button
        '
        Me.select_files_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.select_files_Button.Location = New System.Drawing.Point(12, 22)
        Me.select_files_Button.Name = "select_files_Button"
        Me.select_files_Button.Size = New System.Drawing.Size(217, 23)
        Me.select_files_Button.TabIndex = 98
        Me.select_files_Button.Text = "Select XRF Files"
        Me.select_files_Button.UseVisualStyleBackColor = True
        '
        'nchan_checkbox
        '
        Me.nchan_checkbox.AutoSize = True
        Me.nchan_checkbox.Location = New System.Drawing.Point(245, 26)
        Me.nchan_checkbox.Name = "nchan_checkbox"
        Me.nchan_checkbox.Size = New System.Drawing.Size(84, 17)
        Me.nchan_checkbox.TabIndex = 97
        Me.nchan_checkbox.Text = "4k channels"
        Me.nchan_checkbox.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 61)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(711, 134)
        Me.ListBox1.TabIndex = 96
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'select_passive_files
        '
        Me.select_passive_files.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.select_passive_files.Location = New System.Drawing.Point(12, 284)
        Me.select_passive_files.Name = "select_passive_files"
        Me.select_passive_files.Size = New System.Drawing.Size(217, 23)
        Me.select_passive_files.TabIndex = 108
        Me.select_passive_files.Text = "Select XRF Passive Files"
        Me.select_passive_files.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(12, 323)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(711, 134)
        Me.ListBox2.TabIndex = 106
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.sampleinfoBox25)
        Me.Panel1.Controls.Add(Me.Cycle_number_Box2)
        Me.Panel1.Controls.Add(Me.sampleinfoBox24)
        Me.Panel1.Controls.Add(Me.sampleinfoBox23)
        Me.Panel1.Controls.Add(Me.sampleinfoBox22)
        Me.Panel1.Controls.Add(Me.sampleinfoBox21)
        Me.Panel1.Controls.Add(ListBox4)
        Me.Panel1.Location = New System.Drawing.Point(768, 306)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(270, 155)
        Me.Panel1.TabIndex = 110
        '
        'sampleinfoBox25
        '
        Me.sampleinfoBox25.Location = New System.Drawing.Point(170, 127)
        Me.sampleinfoBox25.Name = "sampleinfoBox25"
        Me.sampleinfoBox25.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox25.TabIndex = 88
        '
        'Cycle_number_Box2
        '
        Me.Cycle_number_Box2.Location = New System.Drawing.Point(170, 5)
        Me.Cycle_number_Box2.Name = "Cycle_number_Box2"
        Me.Cycle_number_Box2.Size = New System.Drawing.Size(79, 20)
        Me.Cycle_number_Box2.TabIndex = 47
        '
        'sampleinfoBox24
        '
        Me.sampleinfoBox24.Location = New System.Drawing.Point(170, 103)
        Me.sampleinfoBox24.Name = "sampleinfoBox24"
        Me.sampleinfoBox24.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox24.TabIndex = 38
        '
        'sampleinfoBox23
        '
        Me.sampleinfoBox23.Location = New System.Drawing.Point(170, 77)
        Me.sampleinfoBox23.Name = "sampleinfoBox23"
        Me.sampleinfoBox23.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox23.TabIndex = 37
        '
        'sampleinfoBox22
        '
        Me.sampleinfoBox22.Location = New System.Drawing.Point(170, 53)
        Me.sampleinfoBox22.Name = "sampleinfoBox22"
        Me.sampleinfoBox22.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox22.TabIndex = 36
        '
        'sampleinfoBox21
        '
        Me.sampleinfoBox21.Location = New System.Drawing.Point(170, 28)
        Me.sampleinfoBox21.Name = "sampleinfoBox21"
        Me.sampleinfoBox21.Size = New System.Drawing.Size(79, 20)
        Me.sampleinfoBox21.TabIndex = 35
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(767, 294)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(125, 13)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "Sample info from data file"
        '
        'SampleIDBox21
        '
        Me.SampleIDBox21.Enabled = False
        Me.SampleIDBox21.Location = New System.Drawing.Point(12, 473)
        Me.SampleIDBox21.Name = "SampleIDBox21"
        Me.SampleIDBox21.Size = New System.Drawing.Size(259, 20)
        Me.SampleIDBox21.TabIndex = 112
        '
        'filenamebox2
        '
        Me.filenamebox2.Enabled = False
        Me.filenamebox2.Location = New System.Drawing.Point(12, 499)
        Me.filenamebox2.Name = "filenamebox2"
        Me.filenamebox2.Size = New System.Drawing.Size(832, 20)
        Me.filenamebox2.TabIndex = 111
        '
        'Subtract_passive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1117, 623)
        Me.Controls.Add(Me.SampleIDBox21)
        Me.Controls.Add(Me.filenamebox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.select_passive_files)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SampleIDBox1)
        Me.Controls.Add(Me.filenamebox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.select_files_Button)
        Me.Controls.Add(Me.nchan_checkbox)
        Me.Controls.Add(Me.ListBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Subtract_passive"
        Me.Text = "Subtract Passive Spectra"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

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
    Friend WithEvents nchan_checkbox As CheckBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog2 As SaveFileDialog
    Friend WithEvents SampleIDBox21 As MaskedTextBox
    Friend WithEvents filenamebox2 As MaskedTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents sampleinfoBox25 As MaskedTextBox
    Friend WithEvents Cycle_number_Box2 As MaskedTextBox
    Friend WithEvents sampleinfoBox24 As MaskedTextBox
    Friend WithEvents sampleinfoBox23 As MaskedTextBox
    Friend WithEvents sampleinfoBox22 As MaskedTextBox
    Friend WithEvents sampleinfoBox21 As MaskedTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents select_passive_files As Button
    Friend WithEvents ListBox2 As ListBox
End Class
