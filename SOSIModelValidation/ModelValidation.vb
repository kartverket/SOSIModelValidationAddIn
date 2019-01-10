Public Class ModelValidation
    Dim versionNumber = "1.0.0"
    Dim versionYear = "2018"
    Dim errorCounter As Integer
    Dim warningCounter As Integer
    Dim omittedCounter As Integer
    Dim logLevel = "Warning"
    Dim ruleSet = "SOSI"
    Dim startTime, endTime, elapsedTime

    Dim theRepository As EA.Repository

    Dim validationWindow As SOSIModelValidationWindow

    Dim thePackage As EA.Package

    'For reqUMLProfile:
    Dim NationalTypes As New System.Collections.ArrayList
    Dim ProfileTypes As New System.Collections.ArrayList
    Dim ExtensionTypes As New System.Collections.ArrayList
    Dim CoreTypes As New System.Collections.ArrayList
    'reqUmlProfileLoad()


    Dim packageIDList As New System.Collections.ArrayList
    Dim classifierIDList As New System.Collections.ArrayList



    ' Sub ModelValidation
    ' Check that the selected object is a package
    ' Check that the selected package has stereotype applicationSchema
    ' Start the model validation window

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
                    Dim messageText = "SOSI Model Validation add-in" + vbCrLf + "version " + versionNumber + vbCrLf + "Kartverket " + versionYear + vbCrLf + vbCrLf
                    messageText = messageText + "Model validation based on requirements and recommendations in SOSI standard 'Regler for UML-modellering 5.0'" + vbCrLf + vbCrLf
                    messageText = messageText + "Selected package: «" + thePackage.Element.Stereotype + "» " + thePackage.Element.Name
                    validationWindow.Label1.Text() = messageText
                    validationWindow.Show()
                Else
                    System.Windows.Forms.MessageBox.Show("Please select a package in the project browser.")
                End If
            Case Else
                System.Windows.Forms.MessageBox.Show("Please select a package in the project browser.")
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
        Select Case ruleSet
            Case "SOSI"
                Output("Selected rule set: SOSI Generell del - Regler for UML modellering - versjon 5.0")
            Case "19103"
                Output("Selected rule set: ISO 19103:2015 - Geographic information - Conceptual schema language")
            Case "19109"
                Output("Selected rule set: ISO 19109:2015 - Geographic information - Rules for application schema")
        End Select
        Output("-----------------------------------")
    End Sub

    Private Sub ReportFooter()
        ' write report footer
        Output("-----------------------------------")
        Output("Number of errors found: " & errorCounter)
        If logLevel = "Warning" Then
            Output("Number of warnings found: " & warningCounter)
        End If
        If omittedCounter > 0 Then
            Output("Number of omitted tests:" & omittedCounter)
        End If
        Output("Time used: " & FormatNumber(elapsedTime, 2))
        Output("-----------------------------------")
    End Sub

    Public Sub RunValidation()
        'Initialization of variables common to several tests should be done from this sub.
        'Tests that are run only on the start package should be called from this sub.
        'Tests that are run on all start packages should be called from sub findInvalidElementsInPackage

        'initialize variables
        errorCounter = 0
        warningCounter = 0
        omittedCounter = 0
        startTime = Timer
        packageIDList.Clear()
        classifierIDList.Clear()

        'set log level
        If validationWindow.RadioButtonW.Checked Then
            logLevel = "Warning"
        ElseIf validationWindow.RadioButtonE.Checked Then
            logLevel = "Error"
        Else
            'this should not happen if radio buttons are checked...
            logLevel = "Unknown"
        End If

        'set rule set
        If validationWindow.RadioButtonSOSI.Checked Then
            ruleSet = "SOSI"
        ElseIf validationWindow.RadioButtonISO19103.Checked Then
            ruleSet = "19103"
        ElseIf validationWindow.RadioButtonISO19109.Checked Then
            ruleSet = "19109"
        Else
            ruleSet = "SOSI"
        End If

        'start of report: Show header
        ReportHeader()

        'Check model for script breaking structures
        If scriptBreakingStructuresInModel(thePackage) Then
            Output("Critical Errors: The errors listed above must be corrected before the program can validate the model.")
            Output("Aborting Test.")
        Else

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

            Select Case ruleSet
                Case "SOSI", "19109"
                    If UCase(thePackage.Element.Stereotype) <> "APPLICATIONSCHEMA" Then
                        Output("Error: Selected package does not have stereotype ApplicationSchema.  The selected rule set is intended for Application Schema packages.")
                        errorCounter += 1
                    End If
            End Select

            Call reqUMLProfileLoad()
            Call reqUMLIntegration(thePackage)

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

        Dim packages As EA.Collection
        Dim currentPackage As EA.Package
        Dim currentPConstraint As EA.Constraint

        packages = thePackage.Packages


        Output("Debug Package " + thePackage.Name)

        anbefalingStyleGuide(thePackage)
        kravOversiktsdiagram(thePackage)
        kravSOSIModellregisterApplikasjonskjemaStandardPakkenavnUtkast(thePackage)
        requirement15(thePackage)
        reqUmlPackaging(thePackage)
        kravSOSIModellregisterApplikasjonskjemaVersjonsnummer(thePackage)
        kravSOSIModellregisterApplikasjonsskjemaStatus(thePackage)

        'do checks for all elements in package
        findinvalidElementsInClassifiers(thePackage)

        For Each currentPackage In packages
            Call requirement16(currentPackage)
            ' Skal denne kalles her?
            Dim constraintPCollection As EA.Collection
            constraintPCollection = currentPackage.Element.Constraints
            For Each currentPConstraint In currentPackage.Element.Constraints
                'call checConstriant
            Next
            'recursively call FindInvalidElementsInPackage for subpackages
            FindInvalidElementsInPackage(currentPackage)
        Next
    End Sub

    Sub findinvalidElementsInClassifiers(thePackage As EA.Package)
        Dim elements As EA.Collection
        Dim attributes As EA.Collection
        Dim connectors As EA.Collection
        Dim operations As EA.Collection
        Dim currentElement As EA.Element
        Dim currentAttribute As EA.Attribute
        Dim currentConnector As EA.Connector
        Dim currentOperation As EA.Method

        elements = thePackage.Elements

        'ClassNames.Clear()

        For Each currentElement In elements
            Output("Debug --- Element " + currentElement.Name + " Type " + currentElement.Type)

            ' All classifiers

            anbefalingStyleGuide(currentElement)
            reqUMLStructure(currentElement)


            If currentElement.Type = "Class" Or currentElement.Type = "Enumeration" Or currentElement.Type = "DataType" Then

                ' Call element subs for all class types

                Call requirement14(currentElement)
                Call requirement15(currentElement)
                Call requirement16(currentElement)
                kravEnkelArv(currentElement)

                If UCase(currentElement.Stereotype) = "CODELIST" Or UCase(currentElement.Stereotype) = "ENUMERATION" Or currentElement.Type = "Enumeration" Then

                    ' Call element subs for codelists and enumerations

                    recommendation1(currentElement)
                    Call requirement6(currentElement)
                    Call requirement7(currentElement)
                Else

                    ' Call element subs for classes that are not codelists or enumerations

                    attributes = currentElement.Attributes
                    For Each currentAttribute In attributes
                        Select Case ruleSet
                            Case "SOSI"
                                reqUMLProfileNorsk(currentElement, currentAttribute)
                            Case "19109"
                                reqUMLProfile(currentElement, currentAttribute)
                            Case "19103"
                                requirement25(currentElement, currentAttribute)
                        End Select
                        Call requirement16(currentAttribute)
                    Next

                End If

                If UCase(currentElement.Stereotype) = "FEATURETYPE" Then

                    ' Call element subs for feature types

                    Call reqGeneralFeature(currentElement, currentElement)
                    Call kravFlerspråklighetElement(currentElement)
                End If


                attributes = currentElement.Attributes
                For Each currentAttribute In attributes

                    Output("Debug Attribute " + currentAttribute.Name)
                    Call kravFlerspråklighetElement(currentAttribute)
                    ' Call attribute checks
                    Call requirement15(currentElement, currentAttribute)
                    'flyttet vekk fra kodelister reqUMLProfile(currentElement, currentAttribute)
                Next



                connectors = currentElement.Connectors
                For Each currentConnector In connectors

                    Output("Debug Connector " + currentConnector.Name + " " + currentConnector.Stereotype)
                    ' call connector checks
                    Call requirement15(currentElement, currentConnector)
                    Call requirement16(currentConnector)

                    If currentConnector.Type = "Aggregation" Or currentConnector.Type = "Assosiation" Then
                        kravFlerspråklighetElement(currentConnector.SupplierEnd)
                        kravFlerspråklighetElement(currentConnector.ClientEnd)
                        Call requirement15(currentElement, currentConnector.SupplierEnd)
                        Call requirement15(currentElement, currentConnector.ClientEnd)
                        Call requirement16(currentConnector.SupplierEnd)
                        Call requirement16(currentConnector.ClientEnd)
                    End If

                Next

                operations = currentElement.Methods
                For Each currentOperation In operations

                    Output("Debug Operation" + currentOperation.Name)
                    'call operation checks
                    kravFlerspråklighetElement(currentOperation)

                Next
            End If
        Next

    End Sub


End Class
