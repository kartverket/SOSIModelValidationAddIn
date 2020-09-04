Public Class SOSIModelValidationWindow
    ' Instance of ModelValidation that owns this instance of the Window
    Dim OwnerObject As ModelValidation
    Dim avoidableCodeListsText As String

    Public Sub SetOwnerObject(OwnerO As ModelValidation)
        OwnerObject = OwnerO
    End Sub

    Public Sub setAvoidableCodeListsText(avoidableCodeLists As String)
        avoidableCodeListsText = avoidableCodeLists
    End Sub

    Private Sub SOSIModelValidationWindow_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Dim toolTips As New System.Windows.Forms.ToolTip With {
        .AutoPopDelay = 5000,
        .InitialDelay = 1000,
        .ReshowDelay = 500,
        .ShowAlways = True
        }

        toolTips.SetToolTip(Me.ButtonClear, "Clear report window")
        toolTips.SetToolTip(Me.ButtonClose, "Close SOSI Model Validation")
        toolTips.SetToolTip(Me.ButtonCopy, "Copy report window to clipboard")
        toolTips.SetToolTip(Me.ButtonRun, "Run SOSI Model Validation on selected package")
        toolTips.SetToolTip(Me.RadioButtonE, "Show Errors only")
        toolTips.SetToolTip(Me.RadioButtonW, "Show Errors and Warnings")
        toolTips.SetToolTip(Me.RadioButtonSOSI, "SOSI Generell del - Regler for UML modellering - versjon 5.0")
        toolTips.SetToolTip(Me.RadioButtonISO19103, "ISO 19103:2015 - Geographic information - Conceptual schema language")
        toolTips.SetToolTip(Me.RadioButtonISO19109, "ISO 19109:2015 - Geographic information - Rules for application schema")
        toolTips.SetToolTip(Me.RadioButtonCLI, "Code names shall be mnemonic and shall not have whitespace, separation characters or special characters." + vbCrLf + "If the codename is a proper name or well known abbreviation, the codename can start with captial letter or a number.")
        toolTips.SetToolTip(Me.RadioButtonCLN, "Code names shall be mnemonic and should not have whitespace, separation characters or special characters." + vbCrLf + "If codes have an initial value/exchange alias, it is allowed with documented exceptions to the requirement on whitespace and separation characters" + vbCrLf + "There shall be exchange aliases for all codes in the list, and these shall have noe whitespace, separation characters or special characters.")

    End Sub

    Private Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click
        OwnerObject.TryRunValidation()
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

    Private Sub Output_Link_Click(sender As Object, e As System.Windows.Forms.LinkClickedEventArgs) Handles Output.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub

    Private Sub TreeView_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect
        ' after selecting a tree node
        ' - clear error summary
        ' - list errors and warnings for selected node
        Dim selectedNode As System.Windows.Forms.TreeNode
        Dim errorEntry As ModelValidation.Errorinfo
        selectedNode = TreeView.SelectedNode
        ErrorSummary.Clear()
        ErrorSummary.SelectionColor = System.Drawing.Color.Black
        ErrorSummary.SelectedText = selectedNode.Text + vbCrLf + selectedNode.Name + vbCrLf
        For Each errorEntry In OwnerObject.errorList
            If errorEntry.objectGUID = selectedNode.Name Then
                ErrorSummary.SelectionColor = System.Drawing.Color.DarkRed
                ErrorSummary.SelectedText = errorEntry.text + vbCrLf + vbCrLf
            End If
        Next
        ErrorSummary.SelectionColor = System.Drawing.Color.DarkOrange
        For Each errorEntry In OwnerObject.warningList
            If errorEntry.objectGUID = selectedNode.Name Then
                ErrorSummary.SelectionColor = System.Drawing.Color.DarkOrange
                ErrorSummary.SelectedText = errorEntry.text + vbCrLf + vbCrLf
            End If
        Next
    End Sub

End Class