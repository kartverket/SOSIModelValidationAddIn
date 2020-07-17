Partial Public Class ModelValidation

    'Sub name:      kravPakkeavhengighetProduktspesifikasjoner
    'Author: 		Magnus Karge 
    'Date: 			20200715 
    'Ruleset:       SOSI
    'Requirement:   /krav/pakkeavhengighet/produktspesifikasjoner
    'Purpose: 		Check if packages with stereotype applicationSchema and tagged value SOSI_spesifikasjonstype = produktspesifikasjon have external 
    '               dependencies. If so, an error message will be shown

    'Note: 		    Uses global variable externalReferencedElementIDList

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Attribute, EA.Method, EA.Connector, EA.Element 

    Sub kravPakkeavhengighetProduktspesifikasjoner(thePackage As EA.Package)
        Dim packageIsProductSpecification As Boolean = False
        Dim taggedValueSOSI_spesifikasjonstypeIsSet As Boolean = False
        Dim taggedValueSOSI_spesifikasjonstype As EA.TaggedValue

        Dim taggedValuesCollectionOfThePackage As EA.Collection
        taggedValuesCollectionOfThePackage = thePackage.Element.TaggedValues
        Output("DEBUG: tagged values on package " & thePackage.Name & ": " & taggedValuesCollectionOfThePackage.Count)
        Output("DEBUG: external referenced elements (realizations ignored): " & externalReferencedElementIDList.Count)

        If taggedValuesCollectionOfThePackage.GetByName("SOSI_spesifikasjonstype") IsNot Nothing Then
            taggedValueSOSI_spesifikasjonstypeIsSet = True
            taggedValueSOSI_spesifikasjonstype = taggedValuesCollectionOfThePackage.GetByName("SOSI_spesifikasjonstype")
            Output("DEBUG: found taggedValue SOSI_spesifikasjonstype:" & taggedValueSOSI_spesifikasjonstype.Value)
        End If

        If taggedValueSOSI_spesifikasjonstypeIsSet Then
            If UCase(taggedValueSOSI_spesifikasjonstype.Value) = UCase("produktspesifikasjon") Then
                packageIsProductSpecification = True
            End If

        End If

        If packageIsProductSpecification And externalReferencedElementIDList.Count > 0 Then
            Output("Error: Found dependencies to elements outside of product specification package [" & thePackage.Name & "]. Please consider changing tagged value SOSI_spesifikasjonstype or removing dependencies to external elements (see list below)." & " [/krav/pakkeavhengighet/produktspesifikasjoner]")
            Output(vbTab & "Class with dependency to one or more external elements: [class name tbd]")
            errorCounter += 1
        End If
    End Sub

End Class