<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class summed_spectra
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(summed_spectra))
        Me.nchan_checkbox = New System.Windows.Forms.CheckBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.select_files_Button = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.sampleinfoBox5 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox4 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox3 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox2 = New System.Windows.Forms.MaskedTextBox()
        Me.sampleinfoBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Cycle_number_Box = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SampleIDBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.filenamebox1 = New System.Windows.Forms.MaskedTextBox()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        ListBox3 = New System.Windows.Forms.ListBox()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'nchan_checkbox
        '
        Me.nchan_checkbox.AutoSize = True
        Me.nchan_checkbox.Location = New System.Drawing.Point(263, 16)
        Me.nchan_checkbox.Name = "nchan_checkbox"
        Me.nchan_checkbox.Size = New System.Drawing.Size(84, 17)
        Me.nchan_checkbox.TabIndex = 69
        Me.nchan_checkbox.Text = "4k channels"
        Me.nchan_checkbox.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(30, 51)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(711, 134)
        Me.ListBox1.TabIndex = 68
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'select_files_Button
        '
        Me.select_files_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.select_files_Button.Location = New System.Drawing.Point(30, 12)
        Me.select_files_Button.Name = "select_files_Button"
        Me.select_files_Button.Size = New System.Drawing.Size(217, 23)
        Me.select_files_Button.TabIndex = 70
        Me.select_files_Button.Text = "Select XRF Files"
        Me.ToolTip1.SetToolTip(Me.select_files_Button, "Reads in 1 to 16 files" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "File formats accepted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     CNF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     SPE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     RES  (MEK" &
        "ED results file)")
        Me.select_files_Button.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(34, 323)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(258, 23)
        Me.Button1.TabIndex = 71
        Me.Button1.Text = "Save Summed Spectrum as .SPE file"
        Me.ToolTip1.SetToolTip(Me.Button1, "Reads in 1 to 16 files" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "File formats accepted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     CNF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     SPE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     RES  (MEK" &
        "ED results file)")
        Me.Button1.UseVisualStyleBackColor = True
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
        Me.Panel4.Location = New System.Drawing.Point(738, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(348, 155)
        Me.Panel4.TabIndex = 91
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
        Me.Label1.Location = New System.Drawing.Point(737, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Sample info from data file"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(31, 229)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 16)
        Me.Label3.TabIndex = 95
        Me.Label3.Text = "File Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 204)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 94
        Me.Label2.Text = "Sample ID:"
        '
        'SampleIDBox1
        '
        Me.SampleIDBox1.Enabled = False
        Me.SampleIDBox1.Location = New System.Drawing.Point(112, 202)
        Me.SampleIDBox1.Name = "SampleIDBox1"
        Me.SampleIDBox1.Size = New System.Drawing.Size(259, 20)
        Me.SampleIDBox1.TabIndex = 93
        '
        'filenamebox1
        '
        Me.filenamebox1.Enabled = False
        Me.filenamebox1.Location = New System.Drawing.Point(112, 228)
        Me.filenamebox1.Name = "filenamebox1"
        Me.filenamebox1.Size = New System.Drawing.Size(832, 20)
        Me.filenamebox1.TabIndex = 92
        '
        'summed_spectra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1085, 450)
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
        Me.Name = "summed_spectra"
        Me.Text = "Sum Spectra"
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents nchan_checkbox As CheckBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents select_files_Button As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents sampleinfoBox5 As MaskedTextBox
    Friend WithEvents Cycle_number_Box As MaskedTextBox
    Friend WithEvents sampleinfoBox4 As MaskedTextBox
    Friend WithEvents sampleinfoBox3 As MaskedTextBox
    Friend WithEvents sampleinfoBox2 As MaskedTextBox
    Friend WithEvents sampleinfoBox1 As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SampleIDBox1 As MaskedTextBox
    Friend WithEvents filenamebox1 As MaskedTextBox
    Friend WithEvents SaveFileDialog2 As SaveFileDialog
End Class
