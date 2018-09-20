Partial Public Class ModelValidation

    ' -----------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Sub Name: reqGeneralFeature
    ' Author: Tore Johnsen
    ' Date: 2017-02-22
    ' Purpose: Checks that no classes with stereotype «FeatureType» inherits from a class named GM_Object or TM_Object.
    ' @param[in]: currentElement, startClass

    Sub reqGeneralFeature(currentElement, reqGeneralFeatureStartClass)

        Dim reqGeneralFeatureSuperClass As EA.Element
        Dim connector As EA.Connector

        For Each connector In currentElement.Connectors
            If connector.Type = "Generalization" Then
                If UCase(currentElement.Stereotype) = "FEATURETYPE" Then
                    If currentElement.ElementID = connector.ClientID Then
                        reqGeneralFeatureSuperClass = theRepository.GetElementByID(connector.SupplierID)

                        If UCase(reqGeneralFeatureSuperClass.Name) = "GM_OBJECT" Or UCase(reqGeneralFeatureSuperClass.Name) = "TM_OBJECT" And UCase(currentElement.Stereotype) = "FEATURETYPE" And UCase(reqGeneralFeatureSuperClass.Stereotype) = "FEATURETYPE" Then
                            Output("Error: Class [" & reqGeneralFeatureStartClass.Name & "] inherits from a class named [" & reqGeneralFeatureSuperClass.Name & "]. [req/general/feature]")
                            errorCounter = errorCounter + 1
                        Else Call reqGeneralFeature(reqGeneralFeatureSuperClass, reqGeneralFeatureStartClass)
                        End If
                    End If
                End If
            End If
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------

End Class
