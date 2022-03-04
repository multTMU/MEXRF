<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Plot_Results
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series10 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Plot_Results))
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox7 = New System.Windows.Forms.ListBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.show_components_box = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Width_UpDown = New System.Windows.Forms.NumericUpDown()
        Me.E0_UpDown = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Width_UpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.E0_UpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Chart2
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea1)
        Me.Chart2.Dock = System.Windows.Forms.DockStyle.Top
        Legend1.Alignment = System.Drawing.StringAlignment.Far
        Legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top
        Legend1.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend1)
        Me.Chart2.Location = New System.Drawing.Point(0, 0)
        Me.Chart2.Name = "Chart2"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.CustomProperties = "IsXAxisQuantitative=True"
        Series1.Legend = "Legend1"
        Series1.Name = "fitted_Results"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series2.Color = System.Drawing.Color.Black
        Series2.Legend = "Legend1"
        Series2.MarkerSize = 3
        Series2.Name = "raw_counts"
        Series2.YValuesPerPoint = 3
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Legend = "Legend1"
        Series3.Name = "bkg"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Legend = "Legend1"
        Series4.Name = "U_response"
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Legend = "Legend1"
        Series5.Name = "Pu_response"
        Series6.ChartArea = "ChartArea1"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Legend = "Legend1"
        Series6.Name = "Np_response"
        Series7.ChartArea = "ChartArea1"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series7.Legend = "Legend1"
        Series7.Name = "Cm_response"
        Series8.ChartArea = "ChartArea1"
        Series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series8.Color = System.Drawing.Color.Green
        Series8.Legend = "Legend1"
        Series8.Name = "Am_response"
        Series9.ChartArea = "ChartArea1"
        Series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series9.Color = System.Drawing.Color.DarkViolet
        Series9.Legend = "Legend1"
        Series9.Name = "Gamma_rays"
        Me.Chart2.Series.Add(Series1)
        Me.Chart2.Series.Add(Series2)
        Me.Chart2.Series.Add(Series3)
        Me.Chart2.Series.Add(Series4)
        Me.Chart2.Series.Add(Series5)
        Me.Chart2.Series.Add(Series6)
        Me.Chart2.Series.Add(Series7)
        Me.Chart2.Series.Add(Series8)
        Me.Chart2.Series.Add(Series9)
        Me.Chart2.Size = New System.Drawing.Size(994, 385)
        Me.Chart2.TabIndex = 0
        Me.Chart2.Text = "fitted_Results"
        '
        'Chart3
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea2)
        Me.Chart3.Dock = System.Windows.Forms.DockStyle.Bottom
        Legend2.Enabled = False
        Legend2.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend2)
        Me.Chart3.Location = New System.Drawing.Point(0, 382)
        Me.Chart3.Name = "Chart3"
        Series10.ChartArea = "ChartArea1"
        Series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series10.Legend = "Legend1"
        Series10.Name = "Relative Residuals"
        Me.Chart3.Series.Add(Series10)
        Me.Chart3.Size = New System.Drawing.Size(994, 239)
        Me.Chart3.TabIndex = 2
        Me.Chart3.Text = "Chart3"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(19, 1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(89, 20)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Print"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(170, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Cycle #: "
        '
        'ListBox7
        '
        Me.ListBox7.FormattingEnabled = True
        Me.ListBox7.Location = New System.Drawing.Point(225, 0)
        Me.ListBox7.Name = "ListBox7"
        Me.ListBox7.Size = New System.Drawing.Size(167, 43)
        Me.ListBox7.TabIndex = 43
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(104, 12)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(73, 17)
        Me.RadioButton1.TabIndex = 44
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Log Scale"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(15, 12)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(69, 17)
        Me.RadioButton2.TabIndex = 45
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Lin Scale"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.show_components_box)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Width_UpDown)
        Me.Panel1.Controls.Add(Me.E0_UpDown)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.ListBox7)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(994, 704)
        Me.Panel1.TabIndex = 46
        '
        'show_components_box
        '
        Me.show_components_box.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.show_components_box.AutoSize = True
        Me.show_components_box.Location = New System.Drawing.Point(601, 24)
        Me.show_components_box.Name = "show_components_box"
        Me.show_components_box.Size = New System.Drawing.Size(125, 17)
        Me.show_components_box.TabIndex = 55
        Me.show_components_box.Text = "Show fit components"
        Me.show_components_box.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(859, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "E width:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(745, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "E min:"
        '
        'Width_UpDown
        '
        Me.Width_UpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Width_UpDown.Location = New System.Drawing.Point(910, 54)
        Me.Width_UpDown.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        Me.Width_UpDown.Name = "Width_UpDown"
        Me.Width_UpDown.Size = New System.Drawing.Size(66, 20)
        Me.Width_UpDown.TabIndex = 52
        Me.Width_UpDown.Value = New Decimal(New Integer() {75, 0, 0, 0})
        '
        'E0_UpDown
        '
        Me.E0_UpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.E0_UpDown.Location = New System.Drawing.Point(787, 53)
        Me.E0_UpDown.Maximum = New Decimal(New Integer() {140, 0, 0, 0})
        Me.E0_UpDown.Name = "E0_UpDown"
        Me.E0_UpDown.Size = New System.Drawing.Size(66, 20)
        Me.E0_UpDown.TabIndex = 51
        Me.E0_UpDown.Value = New Decimal(New Integer() {75, 0, 0, 0})
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Location = New System.Drawing.Point(788, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(175, 32)
        Me.GroupBox2.TabIndex = 50
        Me.GroupBox2.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton4)
        Me.GroupBox1.Location = New System.Drawing.Point(586, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(153, 32)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(15, 7)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(58, 17)
        Me.RadioButton3.TabIndex = 48
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Energy"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(79, 7)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(69, 17)
        Me.RadioButton4.TabIndex = 47
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Channels"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TrackBar1)
        Me.Panel2.Controls.Add(Me.Chart3)
        Me.Panel2.Controls.Add(Me.Chart2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(994, 621)
        Me.Panel2.TabIndex = 46
        '
        'TrackBar1
        '
        Me.TrackBar1.BackColor = System.Drawing.SystemColors.Window
        Me.TrackBar1.LargeChange = 1
        Me.TrackBar1.Location = New System.Drawing.Point(0, 145)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar1.Size = New System.Drawing.Size(45, 203)
        Me.TrackBar1.TabIndex = 3
        '
        'Plot_Results
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 704)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Plot_Results"
        Me.Text = "Fit Results"
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Width_UpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.E0_UpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents Chart3 As DataVisualization.Charting.Chart
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox7 As ListBox
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents Width_UpDown As NumericUpDown
    Friend WithEvents E0_UpDown As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents show_components_box As CheckBox
End Class
