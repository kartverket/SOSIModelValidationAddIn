Partial Public Class ModelValidation
    'Sub name:      reqUMLFeature
    'Author:        Magnus Karge
    'Date:          20190124
    'Ruleset:       SOSI, 19109
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [ISO 19109:2015/req/uml/feature] or [SOSI/req/uml/feature]
    '               Here only the redirection to sub in the checkUniqueFeatureTypeNames file happens where the logic is implemented.
    'Calls:	        checkUniqueFeatureTypeNames in checkUniqueFeatureTypeNames.vb			
    ' @param[in]: 	FeatureTypeNames - the list containing the names of all featureTypes in the starting package and its subpackages 
    '               FeatureTypeElementIDs - the list containing the element ids of all featureTypes in the starting package and its subpackages 

    Sub reqUMLFeature(FeatureTypeNames As System.Collections.ArrayList, FeatureTypeElementIDs As System.Collections.ArrayList)
        Dim reference As String = ""

        If ruleSet = "SOSI" Then
            reference = "req/uml/feature"
        End If
        If ruleSet = "19109" Then
            reference = "ISO 19109:2015/req/uml/feature"
        End If
        Call checkUniqueFeatureTypeNames(FeatureTypeNames, FeatureTypeElementIDs, reference)

    End Sub
End Class

