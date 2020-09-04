﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.Options = New System.Windows.Forms.GroupBox()
        Me.RadioButtonCLN = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCLI = New System.Windows.Forms.RadioButton()
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
        Me.Output = New System.Windows.Forms.RichTextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TreeView = New System.Windows.Forms.TreeView()
        Me.ErrorSummary = New System.Windows.Forms.RichTextBox()
        Me.LogLevel.SuspendLayout()
        Me.Options.SuspendLayout()
        Me.RuleSet.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
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
        Me.LogLevel.Location = New System.Drawing.Point(15, 171)
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
        Me.RadioButtonE.Text = "Error"
        Me.RadioButtonE.UseVisualStyleBackColor = True
        '
        'RadioButtonW
        '
        Me.RadioButtonW.AutoSize = True
        Me.RadioButtonW.Checked = True
        Me.RadioButtonW.Location = New System.Drawing.Point(7, 20)
        Me.RadioButtonW.Name = "RadioButtonW"
        Me.RadioButtonW.Size = New System.Drawing.Size(65, 17)
        Me.RadioButtonW.TabIndex = 0
        Me.RadioButtonW.TabStop = True
        Me.RadioButtonW.Text = "Warning"
        Me.RadioButtonW.UseVisualStyleBackColor = True
        '
        'Options
        '
        Me.Options.Controls.Add(Me.RadioButtonCLN)
        Me.Options.Controls.Add(Me.RadioButtonCLI)
        Me.Options.Location = New System.Drawing.Point(308, 171)
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(246, 100)
        Me.Options.TabIndex = 3
        Me.Options.TabStop = False
        Me.Options.Text = "Conformance classes"
        '
        'RadioButtonCLN
        '
        Me.RadioButtonCLN.AutoSize = True
        Me.RadioButtonCLN.Location = New System.Drawing.Point(6, 42)
        Me.RadioButtonCLN.Name = "RadioButtonCLN"
        Me.RadioButtonCLN.Size = New System.Drawing.Size(172, 17)
        Me.RadioButtonCLN.TabIndex = 1
        Me.RadioButtonCLN.Text = "National adaptions for codelists"
        Me.RadioButtonCLN.UseVisualStyleBackColor = True
        '
        'RadioButtonCLI
        '
        Me.RadioButtonCLI.AutoSize = True
        Me.RadioButtonCLI.Checked = True
        Me.RadioButtonCLI.Location = New System.Drawing.Point(6, 19)
        Me.RadioButtonCLI.Name = "RadioButtonCLI"
        Me.RadioButtonCLI.Size = New System.Drawing.Size(191, 17)
        Me.RadioButtonCLI.TabIndex = 0
        Me.RadioButtonCLI.TabStop = True
        Me.RadioButtonCLI.Text = "International standards for codelists"
        Me.RadioButtonCLI.UseVisualStyleBackColor = True
        '
        'ButtonRun
        '
        Me.ButtonRun.Location = New System.Drawing.Point(15, 277)
        Me.ButtonRun.Name = "ButtonRun"
        Me.ButtonRun.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRun.TabIndex = 4
        Me.ButtonRun.Text = "Run"
        Me.ButtonRun.UseVisualStyleBackColor = True
        '
        'ButtonCopy
        '
        Me.ButtonCopy.Location = New System.Drawing.Point(96, 277)
        Me.ButtonCopy.Name = "ButtonCopy"
        Me.ButtonCopy.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCopy.TabIndex = 5
        Me.ButtonCopy.Text = "Copy Log"
        Me.ButtonCopy.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(257, 277)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(75, 23)
        Me.ButtonClose.TabIndex = 6
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(176, 277)
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
        Me.RuleSet.Location = New System.Drawing.Point(158, 171)
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
        Me.RadioButtonISO19109.Location = New System.Drawing.Point(6, 42)
        Me.RadioButtonISO19109.Name = "RadioButtonISO19109"
        Me.RadioButtonISO19109.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonISO19109.TabIndex = 2
        Me.RadioButtonISO19109.Text = "ISO 19109"
        Me.RadioButtonISO19109.UseVisualStyleBackColor = True
        '
        'RadioButtonISO19103
        '
        Me.RadioButtonISO19103.AutoSize = True
        Me.RadioButtonISO19103.Enabled = False
        Me.RadioButtonISO19103.Location = New System.Drawing.Point(6, 65)
        Me.RadioButtonISO19103.Name = "RadioButtonISO19103"
        Me.RadioButtonISO19103.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonISO19103.TabIndex = 1
        Me.RadioButtonISO19103.Text = "ISO 19103"
        Me.RadioButtonISO19103.UseVisualStyleBackColor = True
        '
        'RadioButtonSOSI
        '
        Me.RadioButtonSOSI.AutoSize = True
        Me.RadioButtonSOSI.Location = New System.Drawing.Point(6, 19)
        Me.RadioButtonSOSI.Name = "RadioButtonSOSI"
        Me.RadioButtonSOSI.Size = New System.Drawing.Size(68, 17)
        Me.RadioButtonSOSI.TabIndex = 0
        Me.RadioButtonSOSI.Text = "SOSI 5.0"
        Me.RadioButtonSOSI.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(560, 171)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(194, 99)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Currently Testing"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Label2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(376, 52)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = resources.GetString("Label6.Text")
        '
        'Output
        '
        Me.Output.BackColor = System.Drawing.SystemColors.Window
        Me.Output.Location = New System.Drawing.Point(-1, 0)
        Me.Output.Name = "Output"
        Me.Output.ReadOnly = True
        Me.Output.Size = New System.Drawing.Size(781, 327)
        Me.Output.TabIndex = 12
        Me.Output.Text = ""
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 306)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(784, 349)
        Me.TabControl1.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Output)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(776, 323)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Validation Output"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ErrorSummary)
        Me.TabPage2.Controls.Add(Me.TreeView)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(776, 323)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tree View"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TreeView
        '
        Me.TreeView.Location = New System.Drawing.Point(0, 0)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.Size = New System.Drawing.Size(372, 327)
        Me.TreeView.TabIndex = 0
        '
        'ErrorSummary
        '
        Me.ErrorSummary.Location = New System.Drawing.Point(378, 0)
        Me.ErrorSummary.Name = "ErrorSummary"
        Me.ErrorSummary.ReadOnly = True
        Me.ErrorSummary.Size = New System.Drawing.Size(402, 323)
        Me.ErrorSummary.TabIndex = 1
        Me.ErrorSummary.Text = ""
        '
        'SOSIModelValidationWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 667)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RuleSet)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.ButtonCopy)
        Me.Controls.Add(Me.ButtonRun)
        Me.Controls.Add(Me.Options)
        Me.Controls.Add(Me.LogLevel)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LogLevel As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonE As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonW As System.Windows.Forms.RadioButton
    Friend WithEvents Options As System.Windows.Forms.GroupBox
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
    Friend WithEvents RadioButtonCLN As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCLI As System.Windows.Forms.RadioButton
    Friend WithEvents Output As System.Windows.Forms.RichTextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    Friend WithEvents ErrorSummary As System.Windows.Forms.RichTextBox
End Class
