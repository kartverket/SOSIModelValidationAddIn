Partial Public Class ModelValidation

    ' -----------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Sub Name: reqGeneralFeature
    ' Author: Tore Johnsen
    ' Date: 2017-02-22
    ' Purpose: Checks that no classes with stereotype «FeatureType» inherits from a class named GM_Object or TM_Object.
    ' @param[in]: currentElement, startClass

    Sub reqGeneralFeature(currentElement As EA.Element, reqGeneralFeatureStartClass As EA.Element)

        Dim reqGeneralFeatureSuperClass As EA.Element
        Dim connector As EA.Connector

        For Each connector In currentElement.Connectors
            If connector.Type = "Generalization" Then
                If UCase(currentElement.Stereotype) = "FEATURETYPE" Then
                    If currentElement.ElementID = connector.ClientID Then
                        reqGeneralFeatureSuperClass = theRepository.GetElementByID(connector.SupplierID)

                        If UCase(reqGeneralFeatureSuperClass.Name) = "GM_OBJECT" Or UCase(reqGeneralFeatureSuperClass.Name) = "TM_OBJECT" And UCase(currentElement.Stereotype) = "FEATURETYPE" And UCase(reqGeneralFeatureSuperClass.Stereotype) = "FEATURETYPE" Then
                            Output("Error: Class [" & reqGeneralFeatureStartClass.Name & "] inherits from a class named [" & reqGeneralFeatureSuperClass.Name & "]. [req/general/feature]")
                            errorCounter += 1
                        Else Call reqGeneralFeature(reqGeneralFeatureSuperClass, reqGeneralFeatureStartClass)
                        End If
                    End If
                End If
            End If
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------
    ' -----------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name:      reqGeneralFeature
    'Author:        Magnus Karge
    'Date:          20190124
    'Ruleset:       SOSI, 19109
    'Purpose: 		Check if elements in the provided list fulfill the requirement on unique feature names in [ISO 19109:2015/req/general/feature] or [SOSI/req/general/feature]
    '               Here only the redirection to sub in the checkUniqueFeatureTypeNames file happens where the logic is implemented.
    'Calls:	        checkUniqueFeatureTypeNames in checkUniqueFeatureTypeNames.vb			
    ' @param[in]: 	FeatureTypeNames - the list containing the names of all featureTypes in the starting package and its subpackages 
    '               FeatureTypeElementIDs - the list containing the element ids of all featureTypes in the starting package and its subpackages 
    Sub reqGeneralFeature(FeatureTypeNames As System.Collections.ArrayList, FeatureTypeElementIDs As System.Collections.ArrayList)
        Dim reference As String = ""

        If ruleSet = "SOSI" Then
            reference = "req/general/feature"
        End If
        If ruleSet = "19109" Then
            reference = "ISO 19109:2015/req/general/feature"
        End If
        Call checkUniqueFeatureTypeNames(FeatureTypeNames, FeatureTypeElementIDs, reference)
    End Sub
End Class
