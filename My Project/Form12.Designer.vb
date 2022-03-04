<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form12
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form12))
        Me.txt_watchpath = New System.Windows.Forms.TextBox()
        Me.btn_startwatch = New System.Windows.Forms.Button()
        Me.btn_stop = New System.Windows.Forms.Button()
        Me.txt_folderactivity = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txt_watchpath
        '
        Me.txt_watchpath.Location = New System.Drawing.Point(70, 132)
        Me.txt_watchpath.Name = "txt_watchpath"
        Me.txt_watchpath.Size = New System.Drawing.Size(577, 20)
        Me.txt_watchpath.TabIndex = 0
        '
        'btn_startwatch
        '
        Me.btn_startwatch.Location = New System.Drawing.Point(571, 36)
        Me.btn_startwatch.Name = "btn_startwatch"
        Me.btn_startwatch.Size = New System.Drawing.Size(132, 27)
        Me.btn_startwatch.TabIndex = 1
        Me.btn_startwatch.Text = "btn_startwatch"
        Me.btn_startwatch.UseVisualStyleBackColor = True
        '
        'btn_stop
        '
        Me.btn_stop.Location = New System.Drawing.Point(572, 81)
        Me.btn_stop.Name = "btn_stop"
        Me.btn_stop.Size = New System.Drawing.Size(130, 32)
        Me.btn_stop.TabIndex = 2
        Me.btn_stop.Text = "stop"
        Me.btn_stop.UseVisualStyleBackColor = True
        '
        'txt_folderactivity
        '
        Me.txt_folderactivity.Location = New System.Drawing.Point(70, 205)
        Me.txt_folderactivity.Name = "txt_folderactivity"
        Me.txt_folderactivity.Size = New System.Drawing.Size(577, 20)
        Me.txt_folderactivity.TabIndex = 3
        '
        'Form12
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txt_folderactivity)
        Me.Controls.Add(Me.btn_stop)
        Me.Controls.Add(Me.btn_startwatch)
        Me.Controls.Add(Me.txt_watchpath)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form12"
        Me.Text = "Fit Complete"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_watchpath As TextBox
    Friend WithEvents btn_startwatch As Button
    Friend WithEvents btn_stop As Button
    Friend WithEvents txt_folderactivity As TextBox
End Class
