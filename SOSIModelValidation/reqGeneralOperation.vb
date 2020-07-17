Partial Public Class ModelValidation
    'Sub name:      reqGeneralOperation
    'Author: 		Magnus Karge
    'Date: 			20200714 
    'Purpose: 		one sub checking different parts of an operation. In particular the signature (return type), the name and the definition. 
    '				implements (parts of) req/general/operation in ISO 19109 and SOSI Regler for UML-modellering 5.1

    'Called by:     sub findinvalidElementsInClassifiers

    '@param[in] 	theOperation (EA.Method): the operation to check

    Sub reqGeneralOperation(theOperation As EA.Method)
        Dim operationParentElement As Element
        operationParentElement = theRepository.GetElementByID(theOperation.ParentID)
        Dim reference = "req/general/operation" 'same reference for both SOSI and ISO

        If ruleSet = "19109" Or ruleSet = "SOSI" Then
            'check the name
            If theOperation.Name = "" Then
                Output("Error: Class [«" & getStereotypeOfClass(operationParentElement) & "» " & operationParentElement.Name & "] has at least one operation without a name. [" & reference & "]")
                errorCounter += 1
            End If
            'check the signature's return type

            If theOperation.ReturnType = "" Then
                Output("Error: Class [«" & getStereotypeOfClass(operationParentElement) & "» " & operationParentElement.Name & "] \ operation [" & theOperation.Name & "] has no return type. [" & reference & "]")
                errorCounter += 1
            End If
            'check the definition
            If theOperation.Notes = "" Then
                Output("Error: Class [«" & getStereotypeOfClass(operationParentElement) & "» " & operationParentElement.Name & "] \ operation [" & theOperation.Name & "] has no definition. [" & reference & "]")
                errorCounter += 1
            End If
        End If
    End Sub



End Class
