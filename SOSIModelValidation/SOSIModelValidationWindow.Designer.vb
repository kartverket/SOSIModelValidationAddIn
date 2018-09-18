<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SOSIModelValidationWindow
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LogLevel = New System.Windows.Forms.GroupBox()
        Me.RadioButtonE = New System.Windows.Forms.RadioButton()
        Me.RadioButtonW = New System.Windows.Forms.RadioButton()
        Me.Output = New System.Windows.Forms.TextBox()
        Me.Options = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ButtonRun = New System.Windows.Forms.Button()
        Me.ButtonCopy = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.LogLevel.SuspendLayout()
        Me.Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'LogLevel
        '
        Me.LogLevel.Controls.Add(Me.RadioButtonE)
        Me.LogLevel.Controls.Add(Me.RadioButtonW)
        Me.LogLevel.Location = New System.Drawing.Point(15, 121)
        Me.LogLevel.Name = "LogLevel"
        Me.LogLevel.Size = New System.Drawing.Size(137, 75)
        Me.LogLevel.TabIndex = 1
        Me.LogLevel.TabStop = False
        Me.LogLevel.Text = "Log Level"
        '
        'RadioButtonE
        '
        Me.RadioButtonE.AutoSize = True
        Me.RadioButtonE.Location = New System.Drawing.Point(7, 44)
        Me.RadioButtonE.Name = "RadioButtonE"
        Me.RadioButtonE.Size = New System.Drawing.Size(47, 17)
        Me.RadioButtonE.TabIndex = 1
        Me.RadioButtonE.TabStop = True
        Me.RadioButtonE.Text = "Error"
        Me.RadioButtonE.UseVisualStyleBackColor = True
        '
        'RadioButtonW
        '
        Me.RadioButtonW.AutoSize = True
        Me.RadioButtonW.Location = New System.Drawing.Point(7, 20)
        Me.RadioButtonW.Name = "RadioButtonW"
        Me.RadioButtonW.Size = New System.Drawing.Size(65, 17)
        Me.RadioButtonW.TabIndex = 0
        Me.RadioButtonW.TabStop = True
        Me.RadioButtonW.Text = "Warning"
        Me.RadioButtonW.UseVisualStyleBackColor = True
        '
        'Output
        '
        Me.Output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Output.BackColor = System.Drawing.SystemColors.Window
        Me.Output.Location = New System.Drawing.Point(12, 232)
        Me.Output.Multiline = True
        Me.Output.Name = "Output"
        Me.Output.ReadOnly = True
        Me.Output.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.Output.Size = New System.Drawing.Size(784, 358)
        Me.Output.TabIndex = 2
        '
        'Options
        '
        Me.Options.Controls.Add(Me.CheckBox1)
        Me.Options.Location = New System.Drawing.Point(203, 121)
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(173, 75)
        Me.Options.TabIndex = 3
        Me.Options.TabStop = False
        Me.Options.Text = "Options"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(7, 20)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ButtonRun
        '
        Me.ButtonRun.Location = New System.Drawing.Point(15, 203)
        Me.ButtonRun.Name = "ButtonRun"
        Me.ButtonRun.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRun.TabIndex = 4
        Me.ButtonRun.Text = "Run"
        Me.ButtonRun.UseVisualStyleBackColor = True
        '
        'ButtonCopy
        '
        Me.ButtonCopy.Location = New System.Drawing.Point(97, 203)
        Me.ButtonCopy.Name = "ButtonCopy"
        Me.ButtonCopy.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCopy.TabIndex = 5
        Me.ButtonCopy.Text = "Copy Log"
        Me.ButtonCopy.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(259, 203)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(75, 23)
        Me.ButtonClose.TabIndex = 6
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(178, 203)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(75, 23)
        Me.ButtonClear.TabIndex = 7
        Me.ButtonClear.Text = "Clear Log"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'SOSIModelValidationWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 602)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.ButtonCopy)
        Me.Controls.Add(Me.ButtonRun)
        Me.Controls.Add(Me.Options)
        Me.Controls.Add(Me.Output)
        Me.Controls.Add(Me.LogLevel)
        Me.Controls.Add(Me.Label1)
        Me.Name = "SOSIModelValidationWindow"
        Me.Text = "SOSI Model Validation"
        Me.LogLevel.ResumeLayout(False)
        Me.LogLevel.PerformLayout()
        Me.Options.ResumeLayout(False)
        Me.Options.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LogLevel As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonE As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonW As System.Windows.Forms.RadioButton
    Friend WithEvents Output As System.Windows.Forms.TextBox
    Friend WithEvents Options As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonRun As System.Windows.Forms.Button
    Friend WithEvents ButtonCopy As System.Windows.Forms.Button
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
End Class
