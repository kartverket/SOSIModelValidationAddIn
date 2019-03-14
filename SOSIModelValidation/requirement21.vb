Partial Public Class ModelValidation

    Sub requirement21(thePackageElement As EA.Element)

        Dim connector As EA.Connector
        Dim dependee As EA.Element
        Dim externalElementID
        Dim externalElement As EA.Element
        Dim externalElementPackageID
        Dim externalElementPackage As EA.Package

        'First check - dependencies that exist in model should be shown in diagrams

        For Each connector In thePackageElement.Connectors
            If connector.Type = "Usage" Or connector.Type = "Package" Or connector.Type = "Dependency" Then
                If thePackageElement.ElementID = connector.ClientID Then
                    If Not packageDependenciesShownElementIDList.Contains(connector.SupplierID) Then
                        dependee = theRepository.GetElementByID(connector.SupplierID)
                        Select Case ruleSet
                            Case "SOSI"
                                Output("Error: Dependency to package [«" & dependee.Stereotype & "» " & dependee.Name & "] exist in model, but is not shown in any package diagram [/krav/21]")
                            Case "19109", "19103"
                                Output("Error: Dependency to package [«" & dependee.Stereotype & "» " & dependee.Name & "] exist in model, but is not shown in any package diagram [ISO19103:2015/requirement21]")
                        End Select
                        errorCounter += 1
                    End If
                End If
            End If
        Next

        ' Second check - packages containing external referenced elements should be shown in diagrams

        For Each externalElementID In externalReferencedElementIDList
            externalElement = theRepository.GetElementByID(externalElementID)
            externalElementPackageID = externalElement.PackageID
            externalElementPackage = theRepository.GetPackageByID(externalElementPackageID)
            If Not PackageIsShown(externalElementPackage) Then
                Select Case ruleSet
                    Case "SOSI"
                        Output("Error:  Dependency to package [«" & externalElementPackage.Element.Stereotype & "» " & externalElementPackage.Name & "] (or any of its superpackages) containing element [«" & externalElement.Stereotype & "» " & externalElement.Name & "] is not shown in any package diagram [/krav/21]")
                    Case "19109", "19103"
                        Output("Error:  Dependency to package [«" & externalElementPackage.Element.Stereotype & "» " & externalElementPackage.Name & "] (or any of its superpackages) containing element [«" & externalElement.Stereotype & "» " & externalElement.Name & "] is not shown in any package diagram [ISO19103:2015/requirement21]")
                End Select
                errorCounter += 1
            End If
        Next


    End Sub
End Class