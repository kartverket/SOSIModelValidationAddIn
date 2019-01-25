Partial Public Class ModelValidation
    'Sub name:      requirement19
    'Author: 		Magnus Karge
    'Date: 			20190123 
    'Ruleset:       19103
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [krav/19]:
    '				Find classes without definition (notes/rolenotes) 
    '               Here only the redirection to subs in the checkDefinition file happens where the logic is implemented.

    'Calls: 		checkDefinition* in file checkDefinition.vb

    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Element 


    Sub requirement19(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement, "ISO 19103:2015/requirement/19")
    End Sub




End Class

