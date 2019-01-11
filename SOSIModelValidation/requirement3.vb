Partial Public Class ModelValidation
    'Sub name:      requirement3
    'Author: 		Magnus Karge (minor contribution by Tore Johnsen)
    'Date: 			20190109 
    'Ruleset:       19103
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [ISO 19103/requirement 3]:
    '				Find elements (classes, attributes, association, operations) without definition (notes/rolenotes) 
    '               Here only the redirection to subs in the checkDefinition file happens where the logic is implemented.

    'Calls: 		checkDefinition in file checkDefinition.vb

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Attribute, EA.Method, EA.Connector, EA.Element 


    Sub requirement3(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement, "ISO 19103:2015/requirement 3")
    End Sub

    Sub requirement3(theElement As EA.Attribute)
        Call checkDefinitionOfAttribute(theElement, "ISO 19103:2015/requirement 3")
    End Sub

    Sub requirement3(theElement As EA.Connector)
        Call checkDefinitionOfAssociationRole(theElement, "ISO 19103:2015/requirement 3")
    End Sub

    Sub requirement3(theElement As EA.Method)
        Call checkDefinitionOfOperation(theElement, "ISO 19103:2015/requirement 3")
    End Sub


End Class

