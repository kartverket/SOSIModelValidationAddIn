Public Class ModelValidation
    ' Version and year
    Dim versionNumber As String
    Dim versionYear As String
    ' Counters
    Dim errorCounter As Integer
    Dim warningCounter As Integer
    Dim omittedCounter As Integer
    ' Log level, rule set and options from radiobuttons and checkboxes
    Dim logLevel = "Warning"
    Dim ruleSet = "SOSI"

    ' Option to avoid naming checks on certain codelists.
    Dim checkAllCodeNames = True
    Dim avoidableCodeLists() = New String() {"Kommunenummer", "Fylkesnummer"}


    ' Time variables for report statistics
    Dim startTime, endTime, elapsedTime

    ' Repository object
    Dim theRepository As EA.Repository
    ' Validation window object
    Dim validationWindow As SOSIModelValidationWindow
    ' Package object
    Dim thePackage As EA.Package
    Dim startPackageID As Long

    'For kravHoveddiagramNavning
    Dim numberOfHoveddiagram
    Dim numberOfHoveddiagramWithAdditionalInformationInTheName
    Dim foundHoveddiagram As Boolean


    'For reqUMLProfile:
    Dim NationalTypes As New System.Collections.ArrayList
    Dim ProfileTypes As New System.Collections.ArrayList
    Dim ExtensionTypes As New System.Collections.ArrayList
    Dim CoreTypes As New System.Collections.ArrayList
    'reqUmlProfileLoad()

    ' Lists of model structures used by several subs and functions
    Dim packageIDList As New System.Collections.ArrayList
    Dim classifierIDList As New System.Collections.ArrayList
    Dim packageIDToBeReferencedList As New System.Collections.ArrayList
    Dim packageDependenciesElementIDList As New System.Collections.ArrayList
    Dim featureTypeElementIDsList As New System.Collections.ArrayList
    Dim featureTypeNamesList As New System.Collections.ArrayList
    Dim externalReferencedElementIDList As New System.Collections.ArrayList
    Dim packageDependenciesShownElementIDList As New System.Collections.ArrayList

    ' Sub ModelValidation
    ' Check that the selected object is a package
    ' Start the model validation window

    Public Sub SetVersion(Version As String, Year As String)
        versionNumber = Version
        versionYear = Year
    End Sub

    Public Sub ModelValidationStartWindow(startRepository As EA.Repository)
        theRepository = startRepository
        validationWindow = New SOSIModelValidationWindow
        validationWindow.SetOwnerObject(Me)
        Dim treeSelectedType
        treeSelectedType = theRepository.GetTreeSelectedItemType()
        Select Case treeSelectedType
            Case EA.ObjectType.otPackage
                thePackage = theRepository.GetTreeSelectedObject()
                startPackageID = thePackage.PackageID

                If Not thePackage.IsModel Then
                    validationWindow.Label1.Text() = "SOSI Model Validation add-in" + vbCrLf + "version " + versionNumber + vbCrLf + "Kartverket " + versionYear
                    validationWindow.Label5.Text() = "Selected package: «" + thePackage.Element.Stereotype + "» " + thePackage.Element.Name
                    TestFeedbackClear()
                    'generate text list for tool tip on avoidable code lists 
                    Dim avoidableCodeListsText As String = ""
                    Dim avoidableCodeList As String
                    For Each avoidableCodeList In avoidableCodeLists
                        If Not avoidableCodeListsText = "" Then avoidableCodeListsText = avoidableCodeListsText + ", "
                        avoidableCodeListsText = avoidableCodeListsText + avoidableCodeList
                    Next
                    validationWindow.setAvoidableCodeListsText(avoidableCodeListsText)
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
        Output("Start time: " + DateTime.Now.ToString)
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

    Private Sub TestFeedback(testElementType As String, testElementStereotype As String, testElementName As String, testMoreInfo As String)
        validationWindow.Label2.Text() = testElementType
        If testElementStereotype = "" Then
            validationWindow.Label3.Text() = testElementName
        Else
            validationWindow.Label3.Text() = "«" + testElementStereotype + "» " + testElementName
        End If
        validationWindow.Label4.Text() = testMoreInfo
        validationWindow.Refresh()
    End Sub

    Private Sub TestFeedbackClear()
        validationWindow.Label2.Text() = ""
        validationWindow.Label3.Text() = ""
        validationWindow.Label4.Text() = ""
        validationWindow.Refresh()
    End Sub

    Public Sub RunValidation()
        'Initialization of variables common to several tests should be done from this sub.
        'Tests that are run only on the start package should be called from this sub.
        'Tests that are run on all start packages should be called from sub findInvalidElementsInPackage

        'reload start package
        thePackage = theRepository.GetPackageByID(startPackageID)
        validationWindow.Label5.Text() = "Selected package: «" + thePackage.Element.Stereotype + "» " + thePackage.Element.Name
        validationWindow.Refresh()

        'initialize variables, set counters to 0 and clear lists
        errorCounter = 0
        warningCounter = 0
        omittedCounter = 0
        numberOfHoveddiagram = 0
        numberOfHoveddiagramWithAdditionalInformationInTheName = 0
        foundHoveddiagram = False
        startTime = Timer
        packageIDList.Clear()
        classifierIDList.Clear()
        packageDependenciesElementIDList.Clear()
        featureTypeElementIDsList.Clear()
        featureTypeNamesList.Clear()
        externalReferencedElementIDList.Clear()
        packageDependenciesShownElementIDList.Clear()

        'set log level
        If validationWindow.RadioButtonW.Checked Then
            logLevel = "Warning"
        ElseIf validationWindow.RadioButtonE.Checked Then
            logLevel = "Error"
        Else
            ' Default value in case no radiobutton is checked
            logLevel = "Warning"
        End If

        'set rule set
        If validationWindow.RadioButtonSOSI.Checked Then
            ruleSet = "SOSI"
        ElseIf validationWindow.RadioButtonISO19103.Checked Then
            ruleSet = "19103"
        ElseIf validationWindow.RadioButtonISO19109.Checked Then
            ruleSet = "19109"
        Else
            ' Default value in case no radiobutton is checked
            ruleSet = "SOSI"
        End If

        If validationWindow.CheckAllCodeNames.Checked Then
            checkAllCodeNames = True
        Else
            checkAllCodeNames = False
        End If

        'start of report: Show header
        ReportHeader()

        TestFeedback("Model", thePackage.Element.Stereotype, thePackage.Name, "")

        'Check model for script breaking structures
        If scriptBreakingStructuresInModel(thePackage) Then
            Output("Critical Errors: The errors listed above must be corrected before the program can validate the model.")
            Output("Aborting Test.")
        Else

            ' populate lists that will be used in the validation checks
            Call PopulatePackageIDList(thePackage)
            Call PopulateClassifierLists(thePackage)
            Call PopulatePackageDependenciesElementIDList(thePackage.Element)
            Call PopulateExternalReferencedElementIDList(thePackage)
            Call PopulatePackageDependenciesShownElementIDList(thePackage)

            Select Case ruleSet
                Case "SOSI", "19109"
                    If UCase(thePackage.Element.Stereotype) <> "APPLICATIONSCHEMA" Then
                        Output("Error: Selected package does not have stereotype ApplicationSchema. The selected rule set is for Application Schema packages. Some tests will not be run.")
                        errorCounter += 1
                    End If
            End Select

            Call reqUMLProfileLoad()
            Call reqUMLIntegration(thePackage)
            Call requirement17(thePackage.Element)
            Call requirement21(thePackage.Element)

            ' Tests that should be done recursivly on subpackages should called in FindInvalidElementsInPackage
            Call FindInvalidElementsInPackage(thePackage)


            'global tests in the end of the validation process
            If ruleSet = "SOSI" Or ruleSet = "19109" Then
                Call reqUMLFeature(featureTypeNamesList.Clone, featureTypeElementIDsList.Clone)
                Call reqGeneralFeature(featureTypeNamesList.Clone, featureTypeElementIDsList.Clone)
                Call kravHoveddiagramNavning(thePackage)
                Call kravHoveddiagramDetaljeringNavning(thePackage)
            End If

        End If
        ' end of report: Show footer with results
        endTime = Timer
        elapsedTime = endTime - startTime
        TestFeedbackClear()
        ReportFooter()
    End Sub

    Sub FindInvalidElementsInPackage(thePackage As EA.Package)
        'test functions that should be done recursivly in all subpackages

        Dim packages As EA.Collection
        Dim constraintPCollection As EA.Collection
        Dim currentPackage As EA.Package
        Dim currentPConstraint As EA.Constraint

        packages = thePackage.Packages


        TestFeedback("Package", thePackage.Element.Stereotype, thePackage.Name, "")

        anbefalingStyleGuide(thePackage)
        If ruleSet = "SOSI" Then
            If Not foundHoveddiagram Then
                Call checkPackageForHoveddiagram(thePackage)
            End If

            Call findHoveddiagramInPackage(thePackage)
            Call kravDefinisjoner(thePackage)
            Call kravNavning(thePackage)
        End If


        kravOversiktsdiagram(thePackage)
        kravSOSIModellregisterApplikasjonskjemaStandardPakkenavnUtkast(thePackage)
        requirement15(thePackage)
        reqUmlPackaging(thePackage)
        kravSOSIModellregisterApplikasjonskjemaVersjonsnummer(thePackage)
        kravSOSIModellregisterApplikasjonsskjemaStatus(thePackage)


        constraintPCollection = thePackage.Element.Constraints
        For Each currentPConstraint In constraintPCollection
            'call package constraint checks
            TestFeedback("Package Constraint", thePackage.Stereotype, thePackage.Name, currentPConstraint.Name)
            reqUmlConstraint(currentPConstraint, thePackage)
            If ruleSet = "SOSI" Then
                Call kravDefinisjoner(currentPConstraint, thePackage)
            End If

        Next

        'do checks for all elements in package
        findinvalidElementsInClassifiers(thePackage)



        'recursive call to subpackages

        For Each currentPackage In packages
            Call requirement16(currentPackage)

            FindInvalidElementsInPackage(currentPackage)

        Next
    End Sub

    Sub findinvalidElementsInClassifiers(thePackage As EA.Package)
        Dim elements As EA.Collection
        Dim attributes As EA.Collection
        Dim connectors As EA.Collection
        Dim operations As EA.Collection
        Dim constraints As EA.Collection
        Dim currentElement As EA.Element
        Dim currentAttribute As EA.Attribute
        Dim currentConnector As EA.Connector
        Dim currentOperation As EA.Method
        Dim currentConstraint As EA.Constraint
        Dim currentConConstraint As EA.ConnectorConstraint
        Dim currentAttConstraint As EA.AttributeConstraint

        elements = thePackage.Elements

        'ClassNames.Clear()

        For Each currentElement In elements
            TestFeedback("Element", currentElement.Stereotype, currentElement.Name, "")

            ' All classifiers

            anbefalingStyleGuide(currentElement)
            reqUMLStructure(currentElement)


            If currentElement.Type = "Class" Or currentElement.Type = "Enumeration" Or currentElement.Type = "DataType" Then


                'SOSI ruleset
                If ruleSet = "SOSI" Then
                    Call kravDefinisjoner(currentElement)
                    Call krav3(currentElement)
                    Call krav19(currentElement)
                    Call kravNavning(currentElement)
                End If

                '19103 ruleset (also implicitly included when 19109 is selected)
                If ruleSet = "19103" Or ruleSet = "19109" Then
                    Call requirement3(currentElement)
                    Call requirement19(currentElement)
                End If

                '19109 ruleset
                If ruleSet = "19109" Then
                    Call reqUMLDocumentation(currentElement)
                End If


                Call requirement14(currentElement)
                Call requirement15(currentElement)
                Call requirement16(currentElement)
                kravEnkelArv(currentElement)

                If UCase(currentElement.Stereotype) = "CODELIST" Or UCase(currentElement.Stereotype) = "ENUMERATION" Or currentElement.Type = "Enumeration" Then
                    ' Call element subs for codelists and enumerations


                    Call kravEksternKodeliste(currentElement)

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

                    TestFeedback("Attribute", currentElement.Stereotype, currentElement.Name, currentAttribute.Name)
                    Call kravFlerspråklighetElement(currentAttribute)
                    ' Call attribute checks

                    'If UCase(currentElement.Stereotype) = "FEATURETYPE" Or UCase(currentElement.Stereotype) = "DATATYPE" Or UCase(currentElement.Stereotype) = "UNION" Then
                    'Call reqUMLProfile(currentElement, currentAttribute)
                    'Else
                    '' bør også teste om koder i kodelister har type i det hele tatt, og eventuelt anbefale disse slettet
                    'End If

                    Call requirement15(currentElement, currentAttribute)
                    'flyttet vekk fra kodelister reqUMLProfile(currentElement, currentAttribute)



                    'SOSI ruleset
                    If ruleSet = "SOSI" Then
                        Call kravDefinisjoner(currentAttribute)
                        Call krav3(currentAttribute)
                        Call kravNavning(currentAttribute)
                    End If

                    '19103 ruleset
                    If ruleSet = "19103" Then
                        Call requirement3(currentAttribute)
                    End If

                    '19109 ruleset
                    If ruleSet = "19109" Then
                        Call reqUMLDocumentation(currentAttribute)
                    End If

                    constraints = currentAttribute.Constraints
                    For Each currentAttConstraint In constraints
                        'call attribute constraint checks
                        reqUmlConstraint(currentAttConstraint, currentAttribute)
                        If ruleSet = "SOSI" Then
                            Call kravDefinisjoner(currentAttConstraint, currentAttribute)
                        End If
                    Next
                Next

                connectors = currentElement.Connectors
                For Each currentConnector In connectors

                    TestFeedback("Connector", currentConnector.Stereotype, currentConnector.Name, "")
  
                   
                    'if the current element is on the connectors client side conduct some tests 
                    '(this condition is needed to make sure only associations where the source end is connected to 
                    'elements within "this" package will be checked. Associations with source end connected to elements
                    'outside of "this" package are possibly locked and not editable)

                    If currentElement.ElementID = currentConnector.ClientID And (currentConnector.Type = "Aggregation" Or currentConnector.Type = "Association") Then
                        ' call connector checks
                        'SOSI ruleset
                        If ruleSet = "SOSI" Then
                            Call kravDefinisjoner(currentConnector)
                            Call krav3(currentConnector)
                            Call kravNavning(currentConnector)
                            Call kravNavning(currentConnector, currentElement)

                        End If

                        '19103 ruleset
                        If ruleSet = "19103" Then
                            Call requirement3(currentConnector)
                        End If

                        '19109 ruleset
                        If ruleSet = "19109" Then
                            Call reqUMLDocumentation(currentConnector)
                        End If

                        Call requirement15(currentElement, currentConnector)
                        Call requirement16(currentConnector)


                        kravFlerspråklighetElement(currentConnector.SupplierEnd)
                        kravFlerspråklighetElement(currentConnector.ClientEnd)
                        Call requirement10(currentElement, currentConnector, currentConnector.SupplierEnd)
                        Call requirement10(currentElement, currentConnector, currentConnector.ClientEnd)
                        Call requirement11(currentElement, currentConnector, currentConnector.SupplierEnd)
                        Call requirement11(currentElement, currentConnector, currentConnector.ClientEnd)
                        Call requirement12(currentElement, currentConnector, currentConnector.SupplierEnd)
                        Call requirement12(currentElement, currentConnector, currentConnector.ClientEnd)
                        Call requirement15(currentElement, currentConnector.SupplierEnd)
                        Call requirement15(currentElement, currentConnector.ClientEnd)
                        Call requirement16(currentElement, currentConnector, currentConnector.SupplierEnd)
                        Call requirement16(currentElement, currentConnector, currentConnector.ClientEnd)

                        constraints = currentConnector.Constraints
                        For Each currentConConstraint In constraints
                            TestFeedback("Connector Constraint", currentConnector.Stereotype, currentConnector.Name, "Constraint" + currentConConstraint.Name)

                            'call connector constraint checks
                            reqUmlConstraint(currentConConstraint, currentConnector)
                            If ruleSet = "SOSI" Then
                                Call kravDefinisjoner(currentConConstraint, currentConnector)
                            End If

                        Next
                    End If
                Next



                operations = currentElement.Methods
                For Each currentOperation In operations

                    TestFeedback("Operation", currentElement.Stereotype, currentElement.Name, currentOperation.Name)
                    'call operation checks
                    If ruleSet = "SOSI" Then
                        kravDefinisjoner(currentOperation)
                        krav3(currentOperation)
                        kravNavning(currentOperation)

                    End If

                    If ruleSet = "19103" Then
                        requirement3(currentOperation)
                    End If

                    kravFlerspråklighetElement(currentOperation)

                Next

                constraints = currentElement.Constraints
                For Each currentConstraint In constraints
                    TestFeedback("Constraint", currentElement.Stereotype, currentElement.Name, currentConstraint.Name)
                    'call element constraint checks
                    reqUmlConstraint(currentConstraint, currentElement)

                    If ruleSet = "SOSI" Then
                        Call kravDefinisjoner(currentConstraint, currentElement)
                    End If

                Next

            End If
        Next

    End Sub


End Class
