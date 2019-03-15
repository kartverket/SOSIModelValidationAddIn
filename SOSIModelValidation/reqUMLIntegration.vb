Partial Public Class ModelValidation
    ' sub reqUMLIntegration
    ' 2019-01-21
    ' Author: Åsmund Tjora, based on original script code by Åsmund Tjora and Magnus Karge

    ' Contains several tests:
    ' Check that start package has stereotype ApplicationSchema (omit tests if not)
    ' Check that no sub packages has stereotype ApplicationSchema
    ' Check that no parent packages has stereotype ApplicationSchema
    ' Check that externally referenced packages has stereotype ApplicationSchema (warning only)
    ' TODO Check that externally referenced elements are in externally referenced packages

    Sub reqUMLIntegration(thePackage As EA.Package)
        ' The tests on req/UML/Profile will only run if the start package is an application schema.
        ' If the start package is not an application schema, give a message that test is omitted
        Dim ruleString = ""
        Select Case ruleSet
            Case "SOSI"
                ruleString = "/req/UML/integration"
            Case "19109"
                ruleString = "ISO19109:2015/req/UML/integration"
        End Select

        If UCase(thePackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
            reqUMLIntegrationSubPackages(thePackage, ruleString)
            reqUMLIntegrationParentPackages(thePackage, ruleString)
            reqUMLIntegrationPackageDependencies(thePackage, ruleString)
            reqUMLIntegrationDependencyLoop(thePackage.Element, ruleString)
            reqUMLIntegrationPackagesToBeReferenced(ruleString)
        Else
            Output("Test omitted: The start package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] does not have stereotype ApplicationSchema.  Tests on [" & ruleString & "] will not be run.")
            omittedCounter += 1
        End If
    End Sub

    Sub reqUMLIntegrationSubPackages(thePackage As EA.Package, ruleString As String)
        Dim subPackage As EA.Package
        For Each subPackage In thePackage.Packages
            If UCase(subPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                Output("Error: Package [«" & subPackage.Element.Stereotype & "» " & subPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [" & ruleString & "]")
                errorCounter += 1
            End If
            reqUMLIntegrationSubPackages(subPackage, ruleString)
        Next
    End Sub

    Sub reqUMLIntegrationParentPackages(thePackage As EA.Package, ruleString As String)
        Dim parentPackageID = thePackage.ParentID
        Dim parentPackage As EA.Package
        parentPackage = theRepository.GetPackageByID(parentPackageID)
        Do While Not (parentPackageID = 0) And Not (parentPackage.IsModel)
            If UCase(parentPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                Output("Error: Package [«" & parentPackage.Element.Stereotype & "» " & parentPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [" & ruleString & "]")
                errorCounter += 1
            End If
            parentPackageID = parentPackage.ParentID
            parentPackage = theRepository.GetPackageByID(parentPackageID)
        Loop
    End Sub

    Sub reqUMLIntegrationPackageDependencies(thePackage As EA.Package, ruleString As String)
        Dim packageElementID
        Dim investigatedPackage
        For Each packageElementID In packageDependenciesElementIDList
            investigatedPackage = theRepository.GetElementByID(packageElementID)
            If Not UCase(investigatedPackage.Stereotype) = "APPLICATIONSCHEMA" Then
                Output("Warning:  Dependency to package [«" & investigatedPackage.Stereotype & "» " & investigatedPackage.Name & "] found.  Dependencies shall only be to ApplicationSchema packages or Standard schemas. Ignore this warning if [«" & investigatedPackage.Stereotype & "» " & investigatedPackage.Name & "] is a standard schema [" & ruleString & "]")
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
    Sub reqUMLIntegrationDependencyLoop(thePackageElement, ruleString)
        Dim retVal
        Dim checkedPackagesList As New System.Collections.ArrayList
        retVal = dependencyLoopCheck(thePackageElement, checkedPackagesList)
        If retVal Then
            Output("Error:  The dependency structure originating in [«" & thePackageElement.StereoType & "» " & thePackageElement.name & "] contains dependency loops [" & ruleString & "]")
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

    ' sub reqUMLIntegrationPackagesToBeReferenced
    ' Original code from script:  20170303, Magnus Karge
    ' Current version: 20190122, Åsmund Tjora

    Sub reqUMLIntegrationPackagesToBeReferenced(ruleString As String)
        Dim externalReferencedElementID
        Dim currentExternalElement As EA.Element
        Dim packageToReference As EA.Package

        For Each externalReferencedElementID In externalReferencedElementIDList
            currentExternalElement = theRepository.GetElementByID(externalReferencedElementID)
            Dim parentPackageID
            parentPackageID = currentExternalElement.PackageID
            Dim tempPackageIDsOfApplicationSchemaPackageInHierarchy As New System.Collections.ArrayList
            Dim tempPackageIDsOfReferencedPackageInHierarchy As New System.Collections.ArrayList
            Dim tempPackageIDOfPotentialReferencedPackage = parentPackageID
            Dim parentPackageIsApplicationSchema = False
            Dim parentPackage As EA.Package

            ' If parentPackage exists and is not the model root:
            ' Find if the package is an application schema
            ' Find if the package is referenced
            ' Find if the package is in a subpackage of an application schema
            ' Find if the package is in a subpackage of a referenced package
            ' If there is an application schema package in the hierarchy, the dependency should be to this package

            If Not (parentPackageID = 0) Then
                parentPackage = theRepository.GetPackageByID(parentPackageID)
                If Not parentPackage.IsModel Then
                    If UCase(parentPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                        parentPackageIsApplicationSchema = True
                        tempPackageIDsOfApplicationSchemaPackageInHierarchy.Add(parentPackageID)
                    End If

                    If packageDependenciesElementIDList.Contains(parentPackage.Element.ElementID) Then
                        tempPackageIDsOfReferencedPackageInHierarchy.Add(parentPackageID)
                    End If
                End If

                Do While ((Not parentPackageID = 0) And (Not parentPackage.IsModel))
                    parentPackageID = parentPackage.ParentID
                    parentPackage = theRepository.GetPackageByID(parentPackageID)
                    If Not parentPackage.IsModel Then
                        If UCase(parentPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                            parentPackageIsApplicationSchema = True
                            tempPackageIDsOfApplicationSchemaPackageInHierarchy.Add(parentPackageID)
                            tempPackageIDOfPotentialReferencedPackage = parentPackageID
                        End If
                        If packageDependenciesElementIDList.Contains(parentPackage.Element.ElementID) Then
                            tempPackageIDsOfReferencedPackageInHierarchy.Add(parentPackageID)
                        End If
                    End If
                Loop

            End If

            If tempPackageIDsOfApplicationSchemaPackageInHierarchy.Count = 0 And tempPackageIDsOfReferencedPackageInHierarchy.Count = 0 Then
                packageToReference = theRepository.GetPackageByID(tempPackageIDOfPotentialReferencedPackage)
                Output("Error: Missing dependency for package [«" & packageToReference.Element.Stereotype & "» " & packageToReference.Name & "] (or any of its subpackages) containing external referenced class [«" & currentExternalElement.Stereotype & "» " & currentExternalElement.Name & "] [" & ruleString & "]")
                errorCounter += 1
            ElseIf tempPackageIDsOfApplicationSchemaPackageInHierarchy.Count > 0 Then
                packageToReference = theRepository.GetPackageByID(tempPackageIDsOfApplicationSchemaPackageInHierarchy(0))
                If tempPackageIDsOfReferencedPackageInHierarchy.Count = 0 Then
                    Output("Error: Missing dependency for package [«" & packageToReference.Element.Stereotype & "» " & packageToReference.Name & "] containing external referenced class [«" & currentExternalElement.Stereotype & "» " & currentExternalElement.Name & "] [" & ruleString & "]")
                    errorCounter += 1
                Else
                    If Not tempPackageIDsOfReferencedPackageInHierarchy.Contains(packageToReference.PackageID) Then
                        Output("Error: Missing dependency for package [«" & packageToReference.Element.Stereotype & "» " & packageToReference.Name & "] containing external referenced class [«" & currentExternalElement.Stereotype & "» " & currentExternalElement.Name & "] [" & ruleString & "]")
                        errorCounter += 1
                        Output("       Please exchange dependency to the following package(s) with dependency to the «" & packageToReference.Element.Stereotype & "» " & packageToReference.Name & " package:")
                        For Each packageIDOfReferencedPackage In tempPackageIDsOfReferencedPackageInHierarchy
                            Dim referencedPackage As EA.Package
                            referencedPackage = theRepository.GetPackageByID(packageIDOfReferencedPackage)
                            Output("       «" & referencedPackage.Element.Stereotype & "» " & referencedPackage.Name)
                        Next
                    ElseIf tempPackageIDsOfReferencedPackageInHierarchy.Count > 1 Then
                        Output("Error: Redundant dependencies for package [«" & packageToReference.Element.Stereotype & "» " & packageToReference.Name & "] containing external referenced class [«" & currentExternalElement.Stereotype & "» " & currentExternalElement.Name & "] [" & ruleString & "]")
                        errorCounter += 1
                        Output("       Please remove dependencies to the following package(s):")
                        For Each packageIDOfReferencedPackage In tempPackageIDsOfReferencedPackageInHierarchy
                            If Not packageToReference.PackageID = packageIDOfReferencedPackage Then
                                Dim referencedPackage As EA.Package
                                referencedPackage = theRepository.GetPackageByID(packageIDOfReferencedPackage)
                                Output("       «" & referencedPackage.Element.Stereotype & "» " & referencedPackage.Name)
                            End If
                        Next

                    End If
                End If
            End If



        Next

    End Sub

End Class