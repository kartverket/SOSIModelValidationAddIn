''' <summary>
''' 
''' </summary>
Partial Public Class ModelValidation
    'Sub name:      reqUMLDocumentation
    'Author: 		Magnus Karge (minor contribution by Tore Johnsen)
    'Date: 			20190109 
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [krav/definisjoner]:
    '				Find elements (classes, attributes, association roles, operations, constraints) without definition (notes/rolenotes) 
    '               Here only the redirection to subs in the checkDefinition file happens where the logic is implemented.

    'Calls: 		checkDefinition* in file checkDefinition.vb

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Attribute, EA.Method, EA.Connector, EA.Element 


    Sub reqUMLDocumentation(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement, "ISO 19109:2015/req/uml/documentation")
    End Sub

    Sub reqUMLDocumentation(theElement As EA.Attribute)
        Call checkDefinitionOfAttribute(theElement, "ISO 19109:2015/req/uml/documentation")
    End Sub

    Sub reqUMLDocumentation(theElement As EA.Connector)
        Call checkDefinitionOfAssociationRole(theElement, "ISO 19109:2015/req/uml/documentation")
    End Sub

    Sub reqUMLDocumentation(theElement As EA.Method)
        Call checkDefinitionOfOperation(theElement, "ISO 19109:2015/req/uml/documentation")
    End Sub

    Sub reqUMLDocumentation(theElement As EA.Constraint)
        'Call checkDefinitionOfConstraint(theElement, "ISO 19109:2015/req/uml/documentation")
    End Sub


End Class

