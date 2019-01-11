Partial Public Class ModelValidation
    ' sub reqUMLIntegration
    ' 2019-01-09
    ' Author: Åsmund Tjora, based on original script code by Åsmund Tjora and Magnus Karge

    ' Contains several tests:
    ' Check that start package has stereotype ApplicationSchema (omit tests if not)
    ' Check that no sub packages has stereotype ApplicationSchema
    ' Check that no parent packages has stereotype ApplicationSchema
    ' Check that external referenced packages has stereotype ApplicationSchema (warning only)

    Sub reqUMLIntegration(thePackage As EA.Package)
        ' The tests on req/UML/Profile will only run if the start package is an application schema.
        ' If the start package is not an application schema, give a message that test is omitted
        If UCase(thePackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
            reqUMLIntegrationSubPackages(thePackage)
            reqUMLIntegrationParentPackages(thePackage)
            reqUMLIntegrationPackageDependencies(thePackage)
            reqUMLIntegrationDependencyLoop(thePackage.Element)
        Else
            Select Case ruleSet
                Case "SOSI"
                    Output("Test omitted: The start package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] does not have stereotype ApplicationSchema.  Tests on [req/UML/integration] will not be run.")
                Case "19109"
                    Output("Test omitted: The start package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] does not have stereotype ApplicationSchema.  Tests on [ISO19109:2015/req/UML/integration] will not be run.")
            End Select
            omittedCounter += 1
        End If
    End Sub

    Sub reqUMLIntegrationSubPackages(thePackage As EA.Package)
        Dim subPackage As EA.Package
        For Each subPackage In thePackage.Packages
            If UCase(subPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                Select Case ruleSet
                    Case "SOSI"
                        Output("Error: Package [«" & subPackage.Element.Stereotype & "» " & subPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [/req/uml/integration]")
                    Case "19109"
                        Output("Error: Package [«" & subPackage.Element.Stereotype & "» " & subPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [ISO 19109:2015/req/uml/integration]")
                End Select
                errorCounter += 1
            End If
            reqUMLIntegrationSubPackages(subPackage)
        Next
    End Sub

    Sub reqUMLIntegrationParentPackages(thePackage As EA.Package)
        Dim parentPackageID = thePackage.ParentID
        Dim parentPackage As EA.Package
        parentPackage = theRepository.GetPackageByID(parentPackageID)
        Do While Not (parentPackageID = 0) And Not (parentPackage.IsModel)
            If UCase(parentPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                Select Case ruleSet
                    Case "SOSI"
                        Output("Error: Package [«" & parentPackage.Element.Stereotype & "» " & parentPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [/req/uml/integration]")
                    Case "19109"
                        Output("Error: Package [«" & parentPackage.Element.Stereotype & "» " & parentPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [ISO 19109:2015/req/uml/integration]")
                End Select
                errorCounter += 1
            End If
            parentPackageID = parentPackage.ParentID
            parentPackage = theRepository.GetPackageByID(parentPackageID)
        Loop
    End Sub

    Sub reqUMLIntegrationPackageDependencies(thePackage As EA.Package)
        Dim packageElementID
        Dim investigatedPackage
        For Each packageElementID In packageDependenciesElementIDList
            investigatedPackage = theRepository.GetElementByID(packageElementID)
            If Not UCase(investigatedPackage.Stereotype) = "APPLICATIONSCHEMA" Then
                If ruleSet = "SOSI" Then
                    Output("Warning:  Dependency to package [«" & investigatedPackage.Stereotype & "» " & investigatedPackage.Name & "] found.  Dependencies shall only be to ApplicationSchema packages or Standard schemas. Ignore this warning if [«" & investigatedPackage.Stereotype & "» " & investigatedPackage.Name & "] is a standard schema [req/uml/integration]")
                ElseIf ruleSet = "19109" Then
                    Output("Warning:  Dependency to package [«" & investigatedPackage.Stereotype & "» " & investigatedPackage.Name & "] found.  Dependencies shall only be to ApplicationSchema packages or Standard schemas. Ignore this warning if [«" & investigatedPackage.Stereotype & "» " & investigatedPackage.Name & "] is a standard schema [ISO 19109:2015/req/uml/integration]")
                End If
                warningCounter += 1
            End If
        Next
    End Sub

    'Function name: dependencyLoop
    'Author: 		Åsmund Tjora
    'Date: 			20170511 
    'Purpose: 		Check that dependency structure does not form loops.  Return true if no loops are found, return false if loops are found
    'Parameter: 	Package element where check originates
    'Return value:	false if no loops are found, true if loops are found.
    Sub reqUMLIntegrationDependencyLoop(thePackageElement)
        Dim retVal
        Dim checkedPackagesList As New System.Collections.ArrayList
        retVal = dependencyLoopCheck(thePackageElement, checkedPackagesList)
        If retVal Then
            Output("Error:  The dependency structure originating in [«" & thePackageElement.StereoType & "» " & thePackageElement.name & "] contains dependency loops [/req/uml/integration]")
            Output("          See the list above for the packages that are part of a loop.")
            Output("          Ignore this error for dependencies between packages outside the control of the current project.")
            errorCounter = errorCounter + 1
        End If
    End Sub

    Function dependencyLoopCheck(thePackageElement, dependantCheckedPackagesList)
        Dim retVal
        Dim localRetVal
        Dim dependee As EA.Element
        Dim connector As EA.Connector

        ' Generate a copy of the input list.  
        ' The operations done on the list should not be visible by the dependant in order to avoid false positive when there are common dependees.
        Dim checkedPackagesList As New System.Collections.ArrayList
        Dim ElementID
        For Each ElementID In dependantCheckedPackagesList
            checkedPackagesList.Add(ElementID)
        Next

        retVal = False
        checkedPackagesList.Add(thePackageElement.ElementID)
        For Each connector In thePackageElement.Connectors
            localRetVal = False
            If connector.Type = "Usage" Or connector.Type = "Package" Or connector.Type = "Dependency" Then
                If thePackageElement.ElementID = connector.ClientID Then
                    dependee = theRepository.GetElementByID(connector.SupplierID)
                    Dim checkedPackageID
                    For Each checkedPackageID In checkedPackagesList
                        If checkedPackageID = dependee.ElementID Then localRetVal = True
                    Next
                    If localRetVal Then
                        Output("         Package [«" & dependee.Stereotype & "» " & dependee.Name & "] is part of a dependency loop")
                    Else
                        localRetVal = dependencyLoopCheck(dependee, checkedPackagesList)
                    End If
                    retVal = retVal Or localRetVal
                End If
            End If
        Next

        dependencyLoopCheck = retVal
    End Function



End Class