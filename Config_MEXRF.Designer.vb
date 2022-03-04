<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_MEXRF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config_MEXRF))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SetUpChooseHKEDSystemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetDefaultDirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetUpChooseHKEDSystemToolStripMenuItem, Me.SetDefaultDirectoryToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SetUpChooseHKEDSystemToolStripMenuItem
        '
        Me.SetUpChooseHKEDSystemToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2})
        Me.SetUpChooseHKEDSystemToolStripMenuItem.Name = "SetUpChooseHKEDSystemToolStripMenuItem"
        Me.SetUpChooseHKEDSystemToolStripMenuItem.Size = New System.Drawing.Size(178, 20)
        Me.SetUpChooseHKEDSystemToolStripMenuItem.Text = "Set Up / Choose HKED System"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 22)
        Me.ToolStripMenuItem1.Text = "Create System"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 22)
        Me.ToolStripMenuItem2.Text = "Edit System"
        '
        'SetDefaultDirectoryToolStripMenuItem
        '
        Me.SetDefaultDirectoryToolStripMenuItem.Name = "SetDefaultDirectoryToolStripMenuItem"
        Me.SetDefaultDirectoryToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.SetDefaultDirectoryToolStripMenuItem.Text = "Pathways"
        '
        'Form8
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form8"
        Me.Text = "Configuration"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SetUpChooseHKEDSystemToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents SetDefaultDirectoryToolStripMenuItem As ToolStripMenuItem
End Class
