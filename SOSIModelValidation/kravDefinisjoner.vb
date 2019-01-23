Partial Public Class ModelValidation
    'Sub name:      kravDefinisjoner
    'Author: 		Magnus Karge (minor contribution by Tore Johnsen)
    'Date: 			20190109
    'Ruleset:       SOSI
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [krav/definisjoner]:
    '				Find elements (packages, classes, attributes, association roles, operations, constraints) without definition (notes/rolenotes) 
    '               Here only the redirection to subs in the checkDefinition file happens where the logic is implemented.

    'Calls: 		checkDefinition in file checkDefinition.vb

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Attribute, EA.Method, EA.Connector, EA.Element 


    Sub kravDefinisjoner(theElement As EA.Package)
        Call checkDefinitionOfPackage(theElement, "krav/definisjoner")
    End Sub

    Sub kravDefinisjoner(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement, "krav/definisjoner")
    End Sub

    Sub kravDefinisjoner(theElement As EA.Attribute)
        Call checkDefinitionOfAttribute(theElement, "krav/definisjoner")
    End Sub

    Sub kravDefinisjoner(theElement As EA.Connector)
        Call checkDefinitionOfAssociationRole(theElement, "krav/definisjoner")
    End Sub

    Sub kravDefinisjoner(theElement As EA.Method)
        Call checkDefinitionOfOperation(theElement, "krav/definisjoner")
    End Sub

    Sub kravDefinisjoner(theElement As Object, constraintHoldingElement As Object)
        Call checkDefinitionOfConstraint(theElement, constraintHoldingElement, "krav/definisjoner")
    End Sub


End Class

