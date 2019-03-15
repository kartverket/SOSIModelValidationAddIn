Partial Public Class ModelValidation


    'File name: checkName
    'Purpose:   To gather all 


    ' Sub name: checkNameOfPackage
    ' Author: Magnus Karge
    ' Date: 20190125 
    ' Ruleset: SOSI, 19103
    ' Purpose:  sub procedure to check if a package name is written correctly
    ' 			Implementation of /krav/navning
    ' 			
    ' @param[in]: theElement - The element to check. A package in this case.

    Sub checkNameOfPackage(theElement As EA.Package, reference As String)
        If Not Left(theElement.Name, 1) = UCase(Left(theElement.Name, 1)) Then
            Output("Error: Package name [" & theElement.Name & "] shall start with capital letter. [" + reference + "]")
            errorCounter += 1
        End If
    End Sub

    ' Sub name: checkNameOfClass
    ' Author: Magnus Karge
    ' Date: 20190125 
    ' Ruleset: SOSI, 19103
    ' Purpose:  sub procedure to check if a package name is written correctly
    ' 			Implementation of /krav/navning
    ' 			
    ' @param[in]: theElement - The element to check. A class in this case.

    Sub checkNameOfClass(theElement As EA.Element, reference As String)
        If Not Left(theElement.Name, 1) = UCase(Left(theElement.Name, 1)) Then
            Output("Error: Class name [«" & getStereotypeOfClass(theElement) & "» " & theElement.Name & "] shall start with capital letter. [" + reference + "]")
            errorCounter += 1
        End If
    End Sub

    ' Sub name: checkNameOfAttribute
    ' Author: Magnus Karge
    ' Date: 20190125 
    ' Ruleset: SOSI, 19103
    ' Purpose:  sub procedure to check if a attribute name is written correctly
    ' 			Implementation of /krav/navning
    ' 			
    ' @param[in]: theElement - The element to check. A attribute in this case.

    Sub checkNameOfAttribute(theElement As EA.Attribute, reference As String)
        If Not Left(theElement.Name, 1) = LCase(Left(theElement.Name, 1)) Then
            Dim attributeParentElement As EA.Element
            attributeParentElement = theRepository.GetElementByID(theElement.ParentID)
            Output("Error: Attribute name [" & theElement.Name & "] in class [«" & getStereotypeOfClass(attributeParentElement) & "» " & attributeParentElement.Name & "] shall start with lowercase letter. [" + reference + "]")
            errorCounter += 1
        End If
    End Sub

    ' Sub name: checkNameOfConnector
    ' Author: Magnus Karge
    ' Date: 20190125 
    ' Ruleset: SOSI, 19103
    ' Purpose:  sub procedure to check if a connector name is written correctly
    ' 			Implementation of /krav/navning
    ' 			
    ' @param[in]: theElement - The element to check. A connector in this case.

    Sub checkNameOfConnector(theElement As EA.Connector, reference As String)
        If Not (theElement.Name = "" Or Len(theElement.Name) = 0) And Not Left(theElement.Name, 1) = UCase(Left(theElement.Name, 1)) Then
            Dim associationSourceElement As EA.Element
            Dim associationTargetElement As EA.Element
            associationSourceElement = theRepository.GetElementByID(theElement.ClientID)
            associationTargetElement = theRepository.GetElementByID(theElement.SupplierID)
            Output("Error: Association name [" & theElement.Name & "] between class [«" & getStereotypeOfClass(associationSourceElement) & "» " & associationSourceElement.Name & "] and class [«" & getStereotypeOfClass(associationTargetElement) & "» " & associationTargetElement.Name & "] shall start with capital letter. [" + reference + "]")
            errorCounter += 1
        End If
    End Sub

    ' Sub name: checkNameOfRole
    ' Author: Magnus Karge
    ' Date: 20190312 
    ' Ruleset: SOSI, 19103
    ' Purpose:  sub procedure to check if a association role name is written correctly
    ' 			Implementation of /krav/navning
    ' 			
    ' @param[in] 	theElement (EA.Element). The element that "ownes" the association to check
    '				sourceEndName (CharacterString). role name on association's source end
    '				targetEndName (CharacterString). role name on association's target end
    '				elementOnOppositeSide (EA.Element). The element on the opposite side of the association to check
    Sub checkNameOfRole(theElement As EA.Element, sourceEndName As String, targetEndName As String, elementOnOppositeSide As EA.Element, reference As String)
        If Not sourceEndName = "" And Not Left(sourceEndName, 1) = LCase(Left(sourceEndName, 1)) Then
            Output("Error: Role name [" & sourceEndName & "] on association end connected to class [" & theElement.Name & "] shall start with lowercase letter. [" + reference + "]")
            errorCounter += 1
        End If

        If Not (targetEndName = "") And Not (Left(targetEndName, 1) = LCase(Left(targetEndName, 1))) Then
            Output("Error: Role name [" & targetEndName & "] on association end connected to class [" & elementOnOppositeSide.Name & "] shall start with lowercase letter. [" + reference + "]")
            errorCounter += 1
        End If
    End Sub

    ' Sub name: checkNameOfOperation
    ' Author: Magnus Karge
    ' Date: 20190125 
    ' Ruleset: SOSI, 19103
    ' Purpose:  sub procedure to check if the operations's name starts with lower case
    ' 			Implementation of /krav/navning
    ' 			
    ' @param[in]: theElement - The element to check. A connector in this case.
    'TODO: this rule should not apply for constructor operations 
    Sub checkNameOfOperation(theOperation As EA.Method, reference As String)
        If Not Left(theOperation.Name, 1) = LCase(Left(theOperation.Name, 1)) Then
            Dim currentElement As EA.Element
            currentElement = theRepository.GetElementByID(theOperation.ParentID)
            Output("Error: Operation name [" & theOperation.Name & "] in class [" & currentElement.Name & "] shall start with lowercase letter. [" + reference + "]")
            errorCounter += 1
        End If
    End Sub

End Class

