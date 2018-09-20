Public Class ModelValidation
    Dim versionNumber = "1.0.0"
    Dim versionYear = "2018"
    Dim errorCounter As Integer
    Dim warningCounter As Integer
    Dim logLevel = "Warning"
    Dim ruleSet = "SOSI"
    Dim startTime, endTime, elapsedTime

    Dim theRepository As EA.Repository

    Dim validationWindow As SOSIModelValidationWindow

    Dim thePackage As EA.Package

    'For reqUMLProfile:
    Dim ProfileTypes As New System.Collections.ArrayList
    Dim ExtensionTypes As New System.Collections.ArrayList
    Dim CoreTypes As New System.Collections.ArrayList
    'reqUmlProfileLoad()


    Dim packageIDList As New System.Collections.ArrayList
    Dim classifierIDList As New System.Collections.ArrayList

    'KODEOPPRYDDING:  Må følgende variable være på klassenivå?
    Dim ClassNames As New System.Collections.ArrayList
    Dim PackageNames As New System.Collections.ArrayList

    ' Sub ModelValidation
    ' Check that the selected object is a package
    ' Check that the selected package has stereotype applicationSchema
    ' Start the model validation window

    ' this is a test
    ' and another test
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
        packageIDList.Clear()
        classifierIDList.Clear()



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

            ' populate lists that will be used in the validation checks
            Call PopulatePackageIDList(thePackage)
            Call PopulateClassifierIDList(thePackage)

            ' THESE SUBS MUST BE DECLARED AND NAME CHANGED TO NEW NAMING SCHEMES
            ' Subs below are for tests that are not recursively performed in sub packages
            'Call findPackageDependencies(thePackage.Element)
            'Call getElementIDsOfExternalReferencedElements(thePackage)
            'Call findPackagesToBeReferenced()
            'Call checkPackageDependency(thePackage)
            'Call dependencyLoop(thePackage.Element)
            'check if there are packages with stereotype "applicationSchema"in package hierarchy upwards from start package
            'CheckParentPackageStereotype(thePackage)
            Call reqUMLProfileLoad()

            ' Tests that should be done recursivly on subpackages should called in FindInvalidElementsInPackage
            Call FindInvalidElementsInPackage(thePackage)




        End If
        ' end of report: Show footer with results
        endTime = Timer
        elapsedTime = endTime - startTime
        ReportFooter()
    End Sub

    Sub FindInvalidElementsInPackage(thePackage As EA.Package)
        'test functions that should be done recursivly in all subpackages

        Dim elements As EA.Collection
        Dim packages As EA.Collection
        Dim attributes As EA.Collection
        Dim connectors As EA.Collection
        Dim operations As EA.Collection
        Dim currentPackage As EA.Package
        Dim currentPConstraint As EA.Constraint
        Dim currentElement As EA.Element
        Dim currentAttribute As EA.Attribute
        Dim currentConnector As EA.Connector
        Dim currentOperation As EA.Method

        elements = thePackage.Elements
        packages = thePackage.Packages


        Output("Debug Package " + thePackage.Name)
        ' Call to tests
        ' Call to tests
        ' Call to tests
        'CheckDefinition(thePackage)
        'Requirement15(thePackage)
        '	call checkEndingOfPackageName(package)
        '   Call checkUtkast(Package)
        '   Call checkSubPackageStereotype(Package)
        '

        'recursive call to subpackages

        For Each currentPackage In packages

            FindInvalidElementsInPackage(currentPackage)
            ' Skal denne kalles her?
            Dim constraintPCollection As EA.Collection
            constraintPCollection = currentPackage.Element.Constraints
            For Each currentPConstraint In currentPackage.Element.Constraints
                'call checConstriant
            Next

        Next

        ClassNames.Clear()

        For Each currentElement In elements

            Output("Debug Element " + currentElement.Name)

            ' Call element subs for all classifiers

            If currentElement.Type = "Class" Or currentElement.Type = "Enumeration" Or currentElement.Type = "DataType" Then
                ' Call element subs for all class types

                If UCase(currentElement.Stereotype) = "CODELIST" Or UCase(currentElement.Stereotype) = "ENUMERATION" Or currentElement.Type = "Enumeration" Then
                    ' Call element subs for codelists and enumerations

                End If

                If UCase(currentElement.Stereotype) = "FEATURETYPE" Then
                    ' Call element subs for feature types

                End If



                attributes = currentElement.Attributes
                For Each currentAttribute In attributes

                    Output("Debug Attribute " + currentAttribute.Name)
                    ' Call attribute checks
                    reqUMLProfile(currentElement, currentAttribute)
                Next



                connectors = currentElement.Connectors
                For Each currentConnector In connectors

                    Output("Debug Connector " + currentConnector.Name)
                    ' call connector checks

                Next




                operations = currentElement.Methods
                For Each currentOperation In operations

                    Output("Debug Operation" + currentOperation.Name)
                    'call operation checks

                Next


            End If

        Next





    End Sub


End Class
