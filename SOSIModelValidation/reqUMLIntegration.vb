Partial Public Class ModelValidation
    ' sub reqUMLIntegration
    ' Contains several tests:
    ' Check that start package has stereotype ApplicationSchema
    ' Check that no sub packages has stereotype ApplicationSchema
    ' Check that no parent packages has stereotype ApplicationSchema

    Sub reqUMLIntegration(thePackage As EA.Package)
        ' The tests on req/UML/Profile will only run if the start package is an application schema.
        ' If the start package is not an application schema, give a message that test is omitted
        If UCase(thePackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
            reqUMLIntegrationSubPackages(thePackage)
            reqUMLIntegrationParentPackages(thePackage)

            ' this test will be implemented later
            ' reqUMLIntegrationExternalReferences()

        Else
            Select Case ruleSet
                Case "SOSI"
                    Output("Test omitted: The start package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] does not have stereotype ApplicationSchema.  Tests on [req/UML/integration] will not be run.")
                Case "19109"
                    Output("Test omitted: The start package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] does not have stereotype ApplicationSchema.  Tests on [ISO19109:2015 req/UML/integration] will not be run.")
            End Select
            omittedCounter += 1
        End If
    End Sub
    ' ----------------------------------------------------------------------------------------------
    Sub reqUMLIntegrationSubPackages(thePackage As EA.Package)
        Dim subPackage As EA.Package
        For Each subPackage In thePackage.Packages
            If UCase(subPackage.Element.Stereotype) = "APPLICATIONSCHEMA" Then
                Select Case ruleSet
                    Case "SOSI"
                        Output("Error: Package [«" & subPackage.Element.Stereotype & "» " & subPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [/req/uml/integration]")
                    Case "19109"
                        Output("Error: Package [«" & subPackage.Element.Stereotype & "» " & subPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [ISO 19109:2015 /req/uml/integration]")
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
                        Output("Error: Package [«" & parentPackage.Element.Stereotype & "» " & parentPackage.Name & "]. Package with stereotype ApplicationSchema cannot contain subpackages with stereotype ApplicationSchema. [ISO 19109:2015 /req/uml/integration]")
                End Select
                errorCounter += 1
            End If
            parentPackageID = parentPackage.ParentID
            parentPackage = theRepository.GetPackageByID(parentPackageID)
        Loop
    End Sub


End Class