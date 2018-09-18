Public Class ModelValidation
    Dim versionNumber = "1.0.0"
    Dim versionYear = "2018"
    Dim errorCounter As Integer
    Dim warningCounter As Integer
    Dim logLevel = "Warning"
    Dim startTime, endTime, elapsedTime

    Dim theRepository As EA.Repository

    Dim validationWindow As SOSIModelValidationWindow
    Dim thePackage As EA.Package

    ' Sub ModelValidation
    ' Check that the selected object is a package
    ' Check that the selected package has stereotype applicationSchema
    ' Start the model validation window
    ' this is a test
    Public Sub ModelValidationStartWindow(startRepository As EA.Repository)
        theRepository = startRepository
        validationWindow = New SOSIModelValidationWindow
        validationWindow.SetOwnerObject(Me)
        Dim treeSelectedType
        treeSelectedType = theRepository.GetTreeSelectedItemType()
        Select Case treeSelectedType
            Case EA.ObjectType.otPackage
                thePackage = theRepository.GetTreeSelectedObject()
                If Not thePackage.IsModel Then
                    If UCase(thePackage.Element.Stereotype) = UCase("applicationSchema") Then
                        Dim messageText = "SOSI Model Validation add-in" + vbCrLf + "version " + versionNumber + vbCrLf + "Kartverket" + versionYear + vbCrLf + vbCrLf
                        messageText = messageText + "Model validation based on requirements and recommendations in SOSI standard 'Regler for UML-modellering 5.0'" + vbCrLf + vbCrLf
                        messageText = messageText + "Selected package: «" + thePackage.Element.Stereotype + "» " + thePackage.Element.Name
                        validationWindow.Label1.Text() = messageText
                        validationWindow.Show()
                    Else
                        System.Windows.Forms.MessageBox.Show("Please select a package with stereotype «applicationSchema».")
                    End If
                Else
                    System.Windows.Forms.MessageBox.Show("Please select a package with stereotype «applicationSchema».")
                End If
            Case Else
                System.Windows.Forms.MessageBox.Show("Please select a package with stereotype «applicationSchema».")
        End Select
    End Sub

    Private Sub Output(outputText As String)
        ' Add a line of text in output window.  End with CR/LF
        validationWindow.Output.AppendText(outputText + vbCrLf)
    End Sub

    Private Sub OutputNoLineShift(outputText As String)
        ' Add a line of text in output window.  No CR/LF
        validationWindow.Output.AppendText(outputText)
    End Sub

    Private Sub ReportHeader()
        ' write report header
        Output("SOSI Model Validation version " + versionNumber)
        Output("-----------------------------------")
        Output("Selected package: «" + thePackage.Element.Stereotype + "» " + thePackage.Element.Name)
        Output("Selected log level: " + logLevel)
        Output("-----------------------------------")
    End Sub

    Private Sub ReportFooter()
        ' write report footer
        Output("-----------------------------------")
        Output("Number of errors found: " & errorCounter)
        If logLevel = "Warning" Then
            Output("Number of warnings found: " & warningCounter)
        End If
        Output("Time used: " & FormatNumber(elapsedTime, 2))
        Output("-----------------------------------")
    End Sub

    Public Sub RunValidation()

        'initialize variables
        errorCounter = 0
        warningCounter = 0
        startTime = Timer


        'set log level
        If validationWindow.RadioButtonW.Checked Then
            logLevel = "Warning"
        ElseIf validationWindow.RadioButtonE.Checked Then
            logLevel = "Error"
        Else
            'this should not happen if all radio buttons are checked...
            logLevel = "Unknown"
        End If

        'start of report: Show header
        ReportHeader()

        ' calls to actual work
        'Check model for script breaking structures
        If scriptBreakingStructuresInModel(thePackage) Then
            Output("Critical Errors: The errors listed above must be corrected before the script can validate the model.")
            Output("Aborting Script.")
        Else
            'rest of the calls





        End If
        ' end of report: Show footer with results
        endTime = Timer
        elapsedTime = endTime - startTime
        ReportFooter()
    End Sub

End Class
