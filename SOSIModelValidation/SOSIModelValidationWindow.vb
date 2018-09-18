﻿Public Class SOSIModelValidationWindow
    ' Instance of ModelValidation that owns this instance of the Window
    Dim OwnerObject As ModelValidation

    Public Sub SetOwnerObject(OwnerO As ModelValidation)
        OwnerObject = OwnerO
    End Sub

    Private Sub SOSIModelVlaidationWindow_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Dim toolTips As New System.Windows.Forms.ToolTip()

        toolTips.AutoPopDelay = 5000
        toolTips.InitialDelay = 1000
        toolTips.ReshowDelay = 500

        toolTips.ShowAlways = True

        toolTips.SetToolTip(Me.ButtonClear, "Clear report window")
        toolTips.SetToolTip(Me.ButtonClose, "Close SOSI Model Validation")
        toolTips.SetToolTip(Me.ButtonCopy, "Copy report window to clipboard")
        toolTips.SetToolTip(Me.ButtonRun, "Run SOSI Model Validation on selected package")
        toolTips.SetToolTip(Me.RadioButtonE, "Show Errors only")
        toolTips.SetToolTip(Me.RadioButtonW, "Show Errors and Warnings")

    End Sub

    Private Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click
        OwnerObject.RunValidation()
    End Sub

    Private Sub ButtonCopy_Click(sender As Object, e As EventArgs) Handles ButtonCopy.Click
        Output.SelectAll()
        Output.Copy()
        Output.DeselectAll()
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        Output.Clear()
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Close()
    End Sub

End Class