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
        Me.CheckAllCodeNames = New System.Windows.Forms.CheckBox()
        Me.ButtonRun = New System.Windows.Forms.Button()
        Me.ButtonCopy = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.RuleSet = New System.Windows.Forms.GroupBox()
        Me.RadioButtonISO19109 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonISO19103 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSOSI = New System.Windows.Forms.RadioButton()
        Me.LogLevel.SuspendLayout()
        Me.Options.SuspendLayout()
        Me.RuleSet.SuspendLayout()
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
        Me.LogLevel.Location = New System.Drawing.Point(15, 118)
        Me.LogLevel.Name = "LogLevel"
        Me.LogLevel.Size = New System.Drawing.Size(137, 100)
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
        Me.Output.Location = New System.Drawing.Point(12, 257)
        Me.Output.Multiline = True
        Me.Output.Name = "Output"
        Me.Output.ReadOnly = True
        Me.Output.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.Output.Size = New System.Drawing.Size(784, 352)
        Me.Output.TabIndex = 2
        '
        'Options
        '
        Me.Options.Controls.Add(Me.CheckAllCodeNames)
        Me.Options.Location = New System.Drawing.Point(308, 118)
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(246, 100)
        Me.Options.TabIndex = 3
        Me.Options.TabStop = False
        Me.Options.Text = "Options"
        '
        'CheckAllCodeNames
        '
        Me.CheckAllCodeNames.AutoSize = True
        Me.CheckAllCodeNames.Checked = True
        Me.CheckAllCodeNames.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckAllCodeNames.Location = New System.Drawing.Point(7, 20)
        Me.CheckAllCodeNames.Name = "CheckAllCodeNames"
        Me.CheckAllCodeNames.Size = New System.Drawing.Size(222, 17)
        Me.CheckAllCodeNames.TabIndex = 0
        Me.CheckAllCodeNames.Text = "Test naming requirements on all code lists"
        Me.CheckAllCodeNames.UseVisualStyleBackColor = True
        '
        'ButtonRun
        '
        Me.ButtonRun.Location = New System.Drawing.Point(15, 225)
        Me.ButtonRun.Name = "ButtonRun"
        Me.ButtonRun.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRun.TabIndex = 4
        Me.ButtonRun.Text = "Run"
        Me.ButtonRun.UseVisualStyleBackColor = True
        '
        'ButtonCopy
        '
        Me.ButtonCopy.Location = New System.Drawing.Point(97, 225)
        Me.ButtonCopy.Name = "ButtonCopy"
        Me.ButtonCopy.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCopy.TabIndex = 5
        Me.ButtonCopy.Text = "Copy Log"
        Me.ButtonCopy.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(259, 225)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(75, 23)
        Me.ButtonClose.TabIndex = 6
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(178, 225)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(75, 23)
        Me.ButtonClear.TabIndex = 7
        Me.ButtonClear.Text = "Clear Log"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'RuleSet
        '
        Me.RuleSet.Controls.Add(Me.RadioButtonISO19109)
        Me.RuleSet.Controls.Add(Me.RadioButtonISO19103)
        Me.RuleSet.Controls.Add(Me.RadioButtonSOSI)
        Me.RuleSet.Location = New System.Drawing.Point(158, 118)
        Me.RuleSet.Name = "RuleSet"
        Me.RuleSet.Size = New System.Drawing.Size(144, 100)
        Me.RuleSet.TabIndex = 8
        Me.RuleSet.TabStop = False
        Me.RuleSet.Text = "Rule Set"
        '
        'RadioButtonISO19109
        '
        Me.RadioButtonISO19109.AutoSize = True
        Me.RadioButtonISO19109.Enabled = False
        Me.RadioButtonISO19109.Location = New System.Drawing.Point(7, 68)
        Me.RadioButtonISO19109.Name = "RadioButtonISO19109"
        Me.RadioButtonISO19109.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonISO19109.TabIndex = 2
        Me.RadioButtonISO19109.TabStop = True
        Me.RadioButtonISO19109.Text = "ISO 19109"
        Me.RadioButtonISO19109.UseVisualStyleBackColor = True
        '
        'RadioButtonISO19103
        '
        Me.RadioButtonISO19103.AutoSize = True
        Me.RadioButtonISO19103.Enabled = False
        Me.RadioButtonISO19103.Location = New System.Drawing.Point(7, 44)
        Me.RadioButtonISO19103.Name = "RadioButtonISO19103"
        Me.RadioButtonISO19103.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonISO19103.TabIndex = 1
        Me.RadioButtonISO19103.TabStop = True
        Me.RadioButtonISO19103.Text = "ISO 19103"
        Me.RadioButtonISO19103.UseVisualStyleBackColor = True
        '
        'RadioButtonSOSI
        '
        Me.RadioButtonSOSI.AutoSize = True
        Me.RadioButtonSOSI.Location = New System.Drawing.Point(7, 20)
        Me.RadioButtonSOSI.Name = "RadioButtonSOSI"
        Me.RadioButtonSOSI.Size = New System.Drawing.Size(68, 17)
        Me.RadioButtonSOSI.TabIndex = 0
        Me.RadioButtonSOSI.TabStop = True
        Me.RadioButtonSOSI.Text = "SOSI 5.0"
        Me.RadioButtonSOSI.UseVisualStyleBackColor = True
        '
        'SOSIModelValidationWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 621)
        Me.Controls.Add(Me.RuleSet)
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
        Me.RuleSet.ResumeLayout(False)
        Me.RuleSet.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LogLevel As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonE As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonW As System.Windows.Forms.RadioButton
    Friend WithEvents Output As System.Windows.Forms.TextBox
    Friend WithEvents Options As System.Windows.Forms.GroupBox
    Friend WithEvents CheckAllCodeNames As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonRun As System.Windows.Forms.Button
    Friend WithEvents ButtonCopy As System.Windows.Forms.Button
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
    Friend WithEvents RuleSet As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonISO19109 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonISO19103 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSOSI As System.Windows.Forms.RadioButton
End Class
