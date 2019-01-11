Partial Public Class ModelValidation
    'Sub name:      krav3
    'Author: 		Magnus Karge (minor contribution by Tore Johnsen)
    'Date: 			20190109 
    'Ruleset:       SOSI
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [krav/3]:
    '				Find elements (classes, attributes, navigable association roles, operations) without definition (notes/rolenotes) 
    '               Here only the redirection to subs in the checkDefinition file happens where the logic is implemented.

    'Calls: 		checkDefinition* in file checkDefinition.vb

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Attribute, EA.Method, EA.Connector, EA.Element 


    Sub krav3(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement, "krav/3")
    End Sub

    Sub krav3(theElement As EA.Attribute)
        Call checkDefinitionOfAttribute(theElement, "krav/3")
    End Sub

    Sub krav3(theElement As EA.Connector)
        Call checkDefinitionOfAssociationRole(theElement, "krav/3")
    End Sub

    Sub krav3(theElement As EA.Method)
        Call checkDefinitionOfOperation(theElement, "krav/3")
    End Sub


End Class

