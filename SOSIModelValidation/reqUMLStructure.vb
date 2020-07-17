Partial Public Class ModelValidation

    Sub reqUMLStructure(theClass As EA.Element)

        If UCase(theClass.Stereotype) = "INTERFACE" Or theClass.Type = "Interface" Then
            Select Case ruleSet
                Case "SOSI"
                    Output("Error:  Class [«" & theClass.Stereotype & "» " & theClass.Name & "].  Interface stereotype for classes is not allowed in ApplicationSchema packages. [/req/uml/structure]")
                Case "19109"
                    Output("Error:  Class [«" & theClass.Stereotype & "» " & theClass.Name & "].  Interface stereotype for classes is not allowed in ApplicationSchema packages. [ISO19109:2015 /req/uml/structure]")
            End Select
            errorCounter += 1
        End If

        If theClass.Abstract = "1" Then
            Dim connector As EA.Connector
            Dim hasSpecializations = False
            Dim specInSameApplicationSchema = False
            For Each connector In theClass.Connectors
                If connector.Type = "Generalization" Then
                    If theClass.ElementID = connector.SupplierID Then
                        hasSpecializations = True
                        Dim subClass As EA.Element
                        Dim packID
                        subClass = theRepository.GetElementByID(connector.ClientID)
                        For Each packID In packageIDList
                            If subClass.PackageID = packID Then specInSameApplicationSchema = True
                        Next
                    End If
                End If
            Next
            If Not (hasSpecializations And specInSameApplicationSchema) Then
                Select Case ruleSet
                    Case "SOSI"
                        Output("Error: Class [«" & theClass.Stereotype & "» " & theClass.Name & "]. Abstract class does not have any instantiable specializations in the ApplicationSchema. [/req/uml/structure]")
                    Case "19109"
                        Output("Error: Class [«" & theClass.Stereotype & "» " & theClass.Name & "]. Abstract class does not have any instantiable specializations in the ApplicationSchema. [ISO19109:2015 /req/uml/structure]")
                End Select
                errorCounter += 1
            End If
        End If

    End Sub


End Class