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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SOSIModelValidationWindow))
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LogLevel.SuspendLayout()
        Me.Options.SuspendLayout()
        Me.RuleSet.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'LogLevel
        '
        Me.LogLevel.Controls.Add(Me.RadioButtonE)
        Me.LogLevel.Controls.Add(Me.RadioButtonW)
        Me.LogLevel.Location = New System.Drawing.Point(20, 210)
        Me.LogLevel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LogLevel.Name = "LogLevel"
        Me.LogLevel.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LogLevel.Size = New System.Drawing.Size(183, 123)
        Me.LogLevel.TabIndex = 1
        Me.LogLevel.TabStop = False
        Me.LogLevel.Text = "Log Level"
        '
        'RadioButtonE
        '
        Me.RadioButtonE.AutoSize = True
        Me.RadioButtonE.Location = New System.Drawing.Point(9, 54)
        Me.RadioButtonE.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonE.Name = "RadioButtonE"
        Me.RadioButtonE.Size = New System.Drawing.Size(61, 21)
        Me.RadioButtonE.TabIndex = 1
        Me.RadioButtonE.TabStop = True
        Me.RadioButtonE.Text = "Error"
        Me.RadioButtonE.UseVisualStyleBackColor = True
        '
        'RadioButtonW
        '
        Me.RadioButtonW.AutoSize = True
        Me.RadioButtonW.Location = New System.Drawing.Point(9, 25)
        Me.RadioButtonW.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonW.Name = "RadioButtonW"
        Me.RadioButtonW.Size = New System.Drawing.Size(82, 21)
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
        Me.Output.Location = New System.Drawing.Point(16, 377)
        Me.Output.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Output.Multiline = True
        Me.Output.Name = "Output"
        Me.Output.ReadOnly = True
        Me.Output.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.Output.Size = New System.Drawing.Size(1044, 429)
        Me.Output.TabIndex = 2
        '
        'Options
        '
        Me.Options.Controls.Add(Me.CheckAllCodeNames)
        Me.Options.Location = New System.Drawing.Point(411, 210)
        Me.Options.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Options.Name = "Options"
        Me.Options.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Options.Size = New System.Drawing.Size(328, 123)
        Me.Options.TabIndex = 3
        Me.Options.TabStop = False
        Me.Options.Text = "Options"
        '
        'CheckAllCodeNames
        '
        Me.CheckAllCodeNames.AutoSize = True
        Me.CheckAllCodeNames.Checked = True
        Me.CheckAllCodeNames.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckAllCodeNames.Location = New System.Drawing.Point(9, 25)
        Me.CheckAllCodeNames.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckAllCodeNames.Name = "CheckAllCodeNames"
        Me.CheckAllCodeNames.Size = New System.Drawing.Size(296, 21)
        Me.CheckAllCodeNames.TabIndex = 0
        Me.CheckAllCodeNames.Text = "Test naming requirements on all code lists"
        Me.CheckAllCodeNames.UseVisualStyleBackColor = True
        '
        'ButtonRun
        '
        Me.ButtonRun.Location = New System.Drawing.Point(20, 341)
        Me.ButtonRun.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonRun.Name = "ButtonRun"
        Me.ButtonRun.Size = New System.Drawing.Size(100, 28)
        Me.ButtonRun.TabIndex = 4
        Me.ButtonRun.Text = "Run"
        Me.ButtonRun.UseVisualStyleBackColor = True
        '
        'ButtonCopy
        '
        Me.ButtonCopy.Location = New System.Drawing.Point(128, 341)
        Me.ButtonCopy.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonCopy.Name = "ButtonCopy"
        Me.ButtonCopy.Size = New System.Drawing.Size(100, 28)
        Me.ButtonCopy.TabIndex = 5
        Me.ButtonCopy.Text = "Copy Log"
        Me.ButtonCopy.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(343, 341)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(100, 28)
        Me.ButtonClose.TabIndex = 6
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(235, 341)
        Me.ButtonClear.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(100, 28)
        Me.ButtonClear.TabIndex = 7
        Me.ButtonClear.Text = "Clear Log"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'RuleSet
        '
        Me.RuleSet.Controls.Add(Me.RadioButtonISO19109)
        Me.RuleSet.Controls.Add(Me.RadioButtonISO19103)
        Me.RuleSet.Controls.Add(Me.RadioButtonSOSI)
        Me.RuleSet.Location = New System.Drawing.Point(211, 210)
        Me.RuleSet.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RuleSet.Name = "RuleSet"
        Me.RuleSet.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RuleSet.Size = New System.Drawing.Size(192, 123)
        Me.RuleSet.TabIndex = 8
        Me.RuleSet.TabStop = False
        Me.RuleSet.Text = "Rule Set"
        '
        'RadioButtonISO19109
        '
        Me.RadioButtonISO19109.AutoSize = True
        Me.RadioButtonISO19109.Enabled = False
        Me.RadioButtonISO19109.Location = New System.Drawing.Point(8, 52)
        Me.RadioButtonISO19109.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonISO19109.Name = "RadioButtonISO19109"
        Me.RadioButtonISO19109.Size = New System.Drawing.Size(96, 21)
        Me.RadioButtonISO19109.TabIndex = 2
        Me.RadioButtonISO19109.Text = "ISO 19109"
        Me.RadioButtonISO19109.UseVisualStyleBackColor = True
        '
        'RadioButtonISO19103
        '
        Me.RadioButtonISO19103.AutoSize = True
        Me.RadioButtonISO19103.Enabled = False
        Me.RadioButtonISO19103.Location = New System.Drawing.Point(8, 80)
        Me.RadioButtonISO19103.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonISO19103.Name = "RadioButtonISO19103"
        Me.RadioButtonISO19103.Size = New System.Drawing.Size(96, 21)
        Me.RadioButtonISO19103.TabIndex = 1
        Me.RadioButtonISO19103.Text = "ISO 19103"
        Me.RadioButtonISO19103.UseVisualStyleBackColor = True
        '
        'RadioButtonSOSI
        '
        Me.RadioButtonSOSI.AutoSize = True
        Me.RadioButtonSOSI.Checked = True
        Me.RadioButtonSOSI.Location = New System.Drawing.Point(8, 23)
        Me.RadioButtonSOSI.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonSOSI.Name = "RadioButtonSOSI"
        Me.RadioButtonSOSI.Size = New System.Drawing.Size(85, 21)
        Me.RadioButtonSOSI.TabIndex = 0
        Me.RadioButtonSOSI.TabStop = True
        Me.RadioButtonSOSI.Text = "SOSI 5.0"
        Me.RadioButtonSOSI.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(747, 210)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(259, 122)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Currently Testing"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 89)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 59)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 17)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 30)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Label2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 162)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 69)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(500, 68)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = resources.GetString("Label6.Text")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(895, 69)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Check for update"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SOSIModelValidationWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1077, 821)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RuleSet)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.ButtonCopy)
        Me.Controls.Add(Me.ButtonRun)
        Me.Controls.Add(Me.Options)
        Me.Controls.Add(Me.Output)
        Me.Controls.Add(Me.LogLevel)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "SOSIModelValidationWindow"
        Me.Text = "SOSI Model Validation"
        Me.LogLevel.ResumeLayout(False)
        Me.LogLevel.PerformLayout()
        Me.Options.ResumeLayout(False)
        Me.Options.PerformLayout()
        Me.RuleSet.ResumeLayout(False)
        Me.RuleSet.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
