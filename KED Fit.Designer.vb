<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckedListBox3 = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Title = "Open Data File"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"U", "Np", "Pu", "Am", "Cm", "Nitric"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(12, 30)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(120, 94)
        Me.CheckedListBox1.TabIndex = 0
        Me.CheckedListBox1.Tag = "Components"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Sample Components"
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.FormattingEnabled = True
        Me.CheckedListBox2.Items.AddRange(New Object() {"HV Endpoint", "X-ray Intensity", "Kramer Power", "Ge Scatter Intensity", "Ge Scatter Angle", "Colimnator Scatter Ratio", "Colimnator Scatter Angle", "Stainless Filter Length"})
        Me.CheckedListBox2.Location = New System.Drawing.Point(12, 157)
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.Size = New System.Drawing.Size(144, 139)
        Me.CheckedListBox2.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "X-ray Tube Parameters"
        '
        'CheckedListBox3
        '
        Me.CheckedListBox3.FormattingEnabled = True
        Me.CheckedListBox3.Items.AddRange(New Object() {"E_zero", "Delta E", "Gaussian Width", "Lorentzian Width U", "Lorentzian Width Pu", "Ge Escape Peak Constant", "Fast Tail Area", "Fast Tail Decay (1/keV)", "Slow Tail Area", "Slow Tail Decay (1/keV)", "Cd-109 Intensity", "Cd-109 Energy"})
        Me.CheckedListBox3.Location = New System.Drawing.Point(221, 27)
        Me.CheckedListBox3.Name = "CheckedListBox3"
        Me.CheckedListBox3.Size = New System.Drawing.Size(157, 199)
        Me.CheckedListBox3.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(233, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Spectrum Parameters"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(76, 345)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(246, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Checked Parameters will be allowed vary during fit!"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(236, 289)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Done"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 367)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckedListBox3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckedListBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Name = "Form1"
        Me.Tag = ""
        Me.Text = "Free Parameters"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox3 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
