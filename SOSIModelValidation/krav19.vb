Partial Public Class ModelValidation
    'Sub name:      krav19
    'Author: 		Magnus Karge
    'Date: 			20190123 
    'Ruleset:       SOSI
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [krav/19]:
    '				Find classes without definition (notes/rolenotes) 
    '               Here only the redirection to subs in the checkDefinition file happens where the logic is implemented.

    'Calls: 		checkDefinition* in file checkDefinition.vb

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Element 


    Sub krav19(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement, "krav/19")
    End Sub




End Class

