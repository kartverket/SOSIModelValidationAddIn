Partial Public Class ModelValidation
    'Sub name:      checkDefinition
    'Author: 		Magnus Karge (minor contribution by Tore Johnsen)
    'Date: 			20181018 
    'Purpose: 		Collection of subs checking different elements' definition
    '				
    'Called by:     kravDefinisjoner in file kravDefinisjoner.vb
    '               krav3 in file krav3.vb
    '               requirement3 in file requirement3.vb
    '               requirement19 in file requirement19.vb


    'old:
    'Purpose: 		Check if the provided argument for input parameter theObject fulfills the requirements in [krav/3]: 
    '				Find elements (classes, attributes, navigable association roles, operations, datatypes)  
    '				without definition (notes/rolenotes) 
    '				[krav/definisjoner]: 
    '				Find packages and constraints without definition
    '				[krav/19]:
    '				All classes shall have a definition
    '@param[in] 	theObject (EA.ObjectType) The object to check,  
    '				supposed to be one of the following types: EA.Attribute, EA.Method, EA.Connector, EA.Element 



    Sub checkDefinitionOfPackage(thePackage As EA.Package, reference As String)
        If ruleSet = "SOSI" Then
            If thePackage.Notes = "" Then
                Output("Error: Package [" & thePackage.Name & "] lacks a definition. [" & reference & "]")
                errorCounter += 1
            End If
        End If
    End Sub

    Sub checkDefinitionOfClass(theClass As EA.Element, reference As String)
        If theClass.Notes = "" Then
            Output("Error: Class [«" & getStereotypeOfClass(theClass) & "» " & theClass.Name & "] has no definition. [" & reference & "]")
            errorCounter += 1
        End If
    End Sub

    Sub checkDefinitionOfAttribute(theAttribute As EA.Attribute, reference As String)
        'get the attribute's parent element = the class that owns the attribute
        Dim attributeParentElement As EA.Element
        attributeParentElement = theRepository.GetElementByID(theAttribute.ParentID)

        If UCase(attributeParentElement.Stereotype) <> "CODELIST" Then
            If UCase(attributeParentElement.Stereotype) <> "ENUMERATION" Then
                If attributeParentElement.Type <> "Enumeration" Then
                    If theAttribute.Notes = "" Then
                        Output("Error: Class [«" & getStereotypeOfClass(attributeParentElement) & "» " & attributeParentElement.Name & "] \ attribute [" & theAttribute.Name & "] has no definition. [" & reference & "]")
                        errorCounter += 1
                    End If
                End If
            End If
        End If

    End Sub

    Sub checkDefinitionOfAssociationRole(theConnector As EA.Connector, reference As String)
        'get the necessary connector attributes 
        Dim sourceEndElementID
        sourceEndElementID = theConnector.ClientID 'id of the element on the source end of the connector 
        Dim sourceEndNavigable
        sourceEndNavigable = theConnector.ClientEnd.Navigable 'navigability on the source end of the connector 
        Dim sourceEndName
        sourceEndName = theConnector.ClientEnd.Role 'role name on the source end of the connector 
        Dim sourceEndDefinition
        sourceEndDefinition = theConnector.ClientEnd.RoleNote 'role definition on the source end of the connector 

        Dim targetEndNavigable
        targetEndNavigable = theConnector.SupplierEnd.Navigable 'navigability on the target end of the connector 
        Dim targetEndName
        targetEndName = theConnector.SupplierEnd.Role 'role name on the target end of the connector 
        Dim targetEndDefinition
        targetEndDefinition = theConnector.SupplierEnd.RoleNote 'role definition on the target end of the connector 


        Dim sourceEndElement As EA.Element

        If Not sourceEndName = "" And sourceEndDefinition = "" And theConnector.Type <> "Dependency" Then
            'get the element on the source end of the connector 
            sourceEndElement = theRepository.GetElementByID(sourceEndElementID)

            Output("Error: Class [«" & getStereotypeOfClass(sourceEndElement) & "» " & sourceEndElement.Name & "] \ association role [" & sourceEndName & "] has no definition. [" & reference & "]")
            errorCounter = errorCounter + 1
        End If

        If Not targetEndName = "" And targetEndDefinition = "" And theConnector.Type <> "Dependency" Then
            'get the element on the source end of the connector (also source end element here because error message is related to the element on the source end of the connector) 
            sourceEndElement = theRepository.GetElementByID(sourceEndElementID)

            Output("Error: Class [«" & getStereotypeOfClass(sourceEndElement) & "» " & sourceEndElement.Name & "] \ association role [" & targetEndName & "] has no definition. [" & reference & "]")
            errorCounter = errorCounter + 1
        End If
    End Sub

    Sub checkDefinitionOfOperation(theOperation As EA.Method, reference As String)
        'get the method's parent element, which is the class the method is part of 
        Dim methodParentElement As EA.Element
        methodParentElement = theRepository.GetElementByID(theOperation.ParentID)

        If theOperation.Notes = "" Then
            Output("Error: Class [«" & getStereotypeOfClass(methodParentElement) & "» " & methodParentElement.Name & "] \ operation [" & theOperation.Name & "] has no definition. [" & reference & "]")
            errorCounter = errorCounter + 1
        End If
    End Sub

    ' subname: checkConstraint
    ' Author: Sara Henriksen, Magnus Karge
    ' Date: 22.01.19
    ' Purpose: to check if a constraint lacks a definition. 
    ' req/uml/constraint & krav/definisjoner
    ' sub procedure to check the current element/attribute/connector/package for constraints without definition
    ' @param[in]: currentConstraint (EA.Constraint) theElement (EA.ObjectType) The object to check against req/uml/constraint,  
    ' supposed to be one of the following types: EA.Element, EA.Attribute, EA.Connector, EA.package

    Sub checkDefinitionOfConstraint(currentConstraint As Object, constraintHoldingObject As Object, reference As String)

        Dim currentConnector As EA.Connector
        Dim currentElement As EA.Element
        Dim currentAttribute As EA.Attribute
        Dim currentPackage As EA.Package

        Select Case constraintHoldingObject.ObjectType
            'if the object is an element
            Case EA.ObjectType.otElement
                currentElement = constraintHoldingObject

                'if the current constraint lacks definition, then return an error
                If currentConstraint.Notes = "" Then
                    Output("Error: Class [«" & currentElement.Stereotype & "» " & currentElement.Name & "] \ constraint [" & currentConstraint.Name & "] has no definition. [" & reference & "]")
                    errorCounter = errorCounter + 1
                End If


            'if the object is an attribute 
            Case EA.ObjectType.otAttribute
                currentAttribute = constraintHoldingObject

                'if the current constraint lacks definition, then return an error
                Dim parentElementID
                parentElementID = currentAttribute.ParentID
                Dim parentElementOfAttribute As EA.Element
                parentElementOfAttribute = theRepository.GetElementByID(parentElementID)
                If currentConstraint.Notes = "" Then
                    Output("Error: Class [" & parentElementOfAttribute.Name & "] \ attribute [" & currentAttribute.Name & "] \ constraint [" & currentConstraint.Name & "] has no definition. [" & reference & "]")
                    errorCounter = errorCounter + 1
                End If


            Case EA.ObjectType.otPackage
                currentPackage = constraintHoldingObject

                'if the current constraint lacks definition, then return an error message
                If currentConstraint.Notes = "" Then
                    Output("Error: Package [«" & currentPackage.Element.Stereotype & "» " & currentPackage.Name & "] \ constraint [" & currentConstraint.Name & "] has no definition. [" & reference & "]")
                    errorCounter = errorCounter + 1
                End If


            Case EA.ObjectType.otConnector
                currentConnector = constraintHoldingObject

                'if the current constraint lacks definition, then return an error message
                If currentConstraint.Notes = "" Then

                    Dim sourceElementID
                    sourceElementID = currentConnector.ClientID
                    Dim sourceElementOfConnector As EA.Element
                    sourceElementOfConnector = theRepository.GetElementByID(sourceElementID)

                    Dim targetElementID
                    targetElementID = currentConnector.SupplierID
                    Dim targetElementOfConnector As EA.Element
                    targetElementOfConnector = theRepository.GetElementByID(targetElementID)

                    Output("Error: Constraint [" & currentConstraint.Name & "] on connector [ " & currentConnector.Name & "] between class [" & sourceElementOfConnector.Name & "] and class [" & targetElementOfConnector.Name & "] has no definition. [" & reference & "]")
                    errorCounter = errorCounter + 1
                End If


        End Select
    End Sub

    'Purpose: 		help function to set stereotype that is shown 
    '				in diagrams but not accessible as such via EAObjectAPI
    'Used in file: 	checkName.vb, checkDefinition.vb
    '@param[in]: theClass (EA.Element)
    'returns: theClass's visible stereotype as character string, empty string if nothing found
    Function getStereotypeOfClass(theClass)
        Dim visibleStereotype As String
        visibleStereotype = ""
        If (UCase(theClass.StereotypeEX) = UCase("featuretype")) Or (UCase(theClass.StereotypeEX) = UCase("codelist")) Or (UCase(theClass.StereotypeEX) = UCase("datatype")) Or (UCase(theClass.StereotypeEX) = UCase("enumeration")) Or (UCase(theClass.StereotypeEX) = UCase("union")) Then
            'param theClass is Classifier subtype Class with different stereotypes
            visibleStereotype = theClass.StereotypeEX
        ElseIf (UCase(theClass.Type) = UCase("enumeration")) Or (UCase(theClass.Type) = UCase("datatype")) Then
            'param theClass is Classifier subtype DataType or Enumeration
            visibleStereotype = theClass.Type
        End If
        getStereotypeOfClass = visibleStereotype
    End Function


End Class

