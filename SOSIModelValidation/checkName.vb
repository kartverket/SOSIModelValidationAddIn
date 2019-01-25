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



End Class

