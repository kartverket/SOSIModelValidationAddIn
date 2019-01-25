Partial Public Class ModelValidation
    'Sub name:      checkUniqueFeatureTypeNames
    'Called by:     reqGeneralFeature in file reqGeneralFeature.vb
    '               reqUMLFeature in file reqUMLFeature.vb
    '              
    'Author: 		Magnus Karge 
    'Date: 			20190124
    'Ruleset:       SOSI, 19109
    'Purpose: 		to check if FeatureType names are unique within the applicationSchema 
    '               see [ISO 19109:2015/req/general/feature], [SOSI/req/general/feature], [ISO 19109:2015/req/uml/feature], [SOSI/req/uml/feature]

    ' @param[in]: 	FeatureTypeNames - the list containing the names of all featureTypes in the starting package and its subpackages 
    '               FeatureTypeElementIDs - the list containing the element ids of all featureTypes in the starting package and its subpackages 
    '               reference - the reference to the related requirement in the standard
    Sub checkUniqueFeatureTypeNames(FeatureTypeNames As System.Collections.ArrayList, FeatureTypeElementIDs As System.Collections.ArrayList, reference As String)
        'iterate over elements in the  name and id arrays until the arrays are empty
        Do Until FeatureTypeNames.Count = 0 And FeatureTypeElementIDs.Count = 0
            Dim temporaryFeatureTypeArray As New System.Collections.ArrayList
            Dim ftNameToCompare
            ftNameToCompare = FeatureTypeNames.Item(0)
            Dim ftElementID
            ftElementID = FeatureTypeElementIDs.Item(0)
            Dim initialElementToAdd As EA.Element
            initialElementToAdd = theRepository.GetElementByID(ftElementID)
            temporaryFeatureTypeArray.Add(initialElementToAdd)
            FeatureTypeNames.RemoveAt(0)
            FeatureTypeElementIDs.RemoveAt(0)
            Dim elementNumber
            For elementNumber = FeatureTypeNames.Count - 1 To 0 Step -1
                Dim currentName
                currentName = FeatureTypeNames.Item(elementNumber)
                If currentName = ftNameToCompare Then
                    Dim currentElementID
                    currentElementID = FeatureTypeElementIDs.Item(elementNumber)
                    Dim additionalElementToAdd As EA.Element
                    additionalElementToAdd = theRepository.GetElementByID(currentElementID)
                    'add element with matching name & id to the temporary array and remove its name and ID from the name and id array
                    temporaryFeatureTypeArray.Add(additionalElementToAdd)
                    FeatureTypeNames.RemoveAt(elementNumber)
                    FeatureTypeElementIDs.RemoveAt(elementNumber)
                End If
            Next

            'generate error messages according to content of the temporary array
            Dim tempStoredFeatureType As EA.Element

            If temporaryFeatureTypeArray.Count > 1 Then
                Output("Error: Found nonunique names for the following classes. [" + reference + "]")
                'counting one error per name conflict (not one error per class with nonunique name)
                errorCounter = errorCounter + 1
                For Each tempStoredFeatureType In temporaryFeatureTypeArray
                    Dim theFeatureTypePackage As EA.Package
                    theFeatureTypePackage = theRepository.GetPackageByID(tempStoredFeatureType.PackageID)
                    Dim theFeatureTypePackageName As String
                    theFeatureTypePackageName = theFeatureTypePackage.Name
                    Output("   Class [«" & tempStoredFeatureType.Stereotype & "» " & tempStoredFeatureType.Name & "] in package [" & theFeatureTypePackageName & "]")
                Next
            End If
        Loop

    End Sub

End Class

