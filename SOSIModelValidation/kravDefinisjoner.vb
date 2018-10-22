Partial Public Class ModelValidation
    'Sub name:      checkDefinition
    'Author: 		Magnus Karge (minor contribution by Tore Johnsen)
    'Date: 			20181018 
    'Purpose: 		Check if the provided argument for input parameter theElement fulfills the requirements in [krav/definisjoner]:
    '				Find elements (packages, classes, attributes, association roles, operations, constraints) without definition (notes/rolenotes) 
    '				
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


    Sub checkDefinitionOfElement(theElement As EA.Package)
        Call checkDefinitionOfPackage(theElement)
    End Sub

    Sub checkDefinitionOfElement(theElement As EA.Element)
        Call checkDefinitionOfClass(theElement)
    End Sub

    Sub checkDefinitionOfElement(theElement As EA.Attribute)
        Call checkDefinitionOfAttribute(theElement)
    End Sub

    Sub checkDefinitionOfElement(theElement As EA.Connector)
        Call checkDefinitionOfAssociationRole(theElement)
    End Sub

    Sub checkDefinitionOfElement(theElement As EA.Method)
        Call checkDefinitionOfOperation(theElement)
    End Sub

    'Sub checkDefinitionOfElement(theElement As EA.Constraint)
    '    Call checkDefinitionOfConstraint(theElement)
    'End Sub

    ' Code for when the function's parameter is a package 
    Sub checkDefinitionOfPackage(thePackage As EA.Package)
        'check package definition 
        If thePackage.Notes = "" Then
            Output("Error: Package [" & thePackage.Name & "] lacks a definition. [/krav/definisjoner]")
            errorCounter += 1
        End If
    End Sub

    Sub checkDefinitionOfClass(theClass As EA.Element)
        If theClass.Notes = "" Then
            Output("Error: Class [«" & getStereotypeOfClass(theClass) & "» " & theClass.Name & "] has no definition. [/krav/definisjoner]")
            errorCounter += 1
        End If
    End Sub

    Sub checkDefinitionOfAttribute(theAttribute As EA.Attribute)
        'get the attribute's parent element = the class that owns the attribute
        Dim attributeParentElement As EA.Element
        attributeParentElement = theRepository.GetElementByID(theAttribute.ParentID)

        If UCase(attributeParentElement.Stereotype) <> "CODELIST" Then
            If UCase(attributeParentElement.Stereotype) <> "ENUMERATION" Then
                If attributeParentElement.Type <> "Enumeration" Then
                    If theAttribute.Notes = "" Then
                        Output("Error: Class [«" & getStereotypeOfClass(attributeParentElement) & "» " & attributeParentElement.Name & "] \ attribute [" & theAttribute.Name & "] has no definition. [/krav/definisjoner]")
                        errorCounter += 1
                    End If
                End If
            End If
        End If

    End Sub

    Sub checkDefinitionOfAssociationRole(theConnector As EA.Connector)
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

            Output("Error: Class [«" & getStereotypeOfClass(sourceEndElement) & "» " & sourceEndElement.Name & "] \ association role [" & sourceEndName & "] has no definition. [/krav/3] & [/krav/definisjoner]")
            errorCounter = errorCounter + 1
        End If

        If Not targetEndName = "" And targetEndDefinition = "" And theConnector.Type <> "Dependency" Then
            'get the element on the source end of the connector (also source end element here because error message is related to the element on the source end of the connector) 
            sourceEndElement = theRepository.GetElementByID(sourceEndElementID)

            Output("Error: Class [«" & getStereotypeOfClass(sourceEndElement) & "» " & sourceEndElement.Name & "] \ association role [" & targetEndName & "] has no definition. [/krav/definisjoner]")
            errorCounter = errorCounter + 1
        End If
    End Sub

    Sub checkDefinitionOfOperation(theOperation As EA.Method)
        'get the method's parent element, which is the class the method is part of 
        Dim methodParentElement As EA.Element
        methodParentElement = theRepository.GetElementByID(theOperation.ParentID)

        If theOperation.Notes = "" Then
            Output("Error: Class [«" & getStereotypeOfClass(methodParentElement) & "» " & methodParentElement.Name & "] \ operation [" & theOperation.Name & "] has no definition. [/krav/definisjoner]")
            errorCounter = errorCounter + 1
        End If
    End Sub




    'Purpose: 		help function to set stereotype that is shown 
    '				in diagrams but not accessible as such via EAObjectAPI
    'Used in sub: 	checkElementName
    '@param[in]: theClass (EA.Element)
    'returns: theClass's visible stereotype as character string, empty string if nothing found
    Function getStereotypeOfClass(theClass)
        Dim visibleStereotype
        visibleStereotype = ""
        If (UCase(theClass.Stereotype) = UCase("featuretype")) Or (UCase(theClass.Stereotype) = UCase("codelist")) Or (UCase(theClass.Stereotype) = UCase("datatype")) Or (UCase(theClass.Stereotype) = UCase("enumeration")) Then
            'param theClass is Classifier subtype Class with different stereotypes
            visibleStereotype = theClass.Stereotype
        ElseIf (UCase(theClass.Type) = UCase("enumeration")) Or (UCase(theClass.Type) = UCase("datatype")) Then
            'param theClass is Classifier subtype DataType or Enumeration
            visibleStereotype = theClass.Type
        End If
        getStereotypeOfClass = visibleStereotype
    End Function


    ' Sub checkDefinition(theObject As EA.ObjectType)
    '     'Declare local variables 
    '     Dim currentAttribute As EA.Attribute
    '     Dim currentMethod As EA.Method
    '     Dim currentConnector As EA.Connector
    '     Dim currentElement As EA.Element
    '     Dim currentPackage As EA.Package

    '     Select Case theObject.ObjectType
    '         Case otElement
    ' Code for when the function's parameter is an element 
    'Set currentElement = theObject 

    '	If currentElement.Notes = "" Then
    '                 Session.Output("Error: Class [«" & getStereotypeOfClass(currentElement) & "» " & currentElement.Name & "] has no definition. [/krav/3], [/krav/definisjoner] & [/krav/19]")
    '                 globalErrorCounter = globalErrorCounter + 1
    '             End If
    '         Case otAttribute
    '	' Code for when the function's parameter is an attribute 

    '	set currentAttribute = theObject 

    '	'get the attribute's parent element 
    '	Dim attributeParentElement As EA.Element
    '	set attributeParentElement = Repository.GetElementByID(currentAttribute.ParentID) 

    'If UCase(attributeParentElement.Stereotype) <> "CODELIST" Then
    '                 If UCase(attributeParentElement.Stereotype) <> "ENUMERATION" Then
    '                     If attributeParentElement.Type <> "Enumeration" Then
    '                         If currentAttribute.Notes = "" Then
    '                             Session.Output("Error: Class [«" & getStereotypeOfClass(attributeParentElement) & "» " & attributeParentElement.Name & "] \ attribute [" & currentAttribute.Name & "] has no definition. [/krav/3] & [/krav/definisjoner]")
    '                             globalErrorCounter = globalErrorCounter + 1
    '                         End If
    '                     End If
    '                 End If
    '             End If

    '         Case otMethod
    '	' Code for when the function's parameter is a method 

    '	set currentMethod = theObject 

    '	'get the method's parent element, which is the class the method is part of 
    '	Dim methodParentElement As EA.Element
    '	set methodParentElement = Repository.GetElementByID(currentMethod.ParentID) 

    '	If currentMethod.Notes = "" Then
    '                 Session.Output("Error: Class [«" & getStereotypeOfClass(methodParentElement) & "» " & methodParentElement.Name & "] \ operation [" & currentMethod.Name & "] has no definition. [/krav/3] & [/krav/definisjoner]")
    '                 globalErrorCounter = globalErrorCounter + 1
    '             End If
    '         Case otConnector
    '	' Code for when the function's parameter is a connector 

    '	set currentConnector = theObject 

    '	'get the necessary connector attributes 
    '	Dim sourceEndElementID
    '             sourceEndElementID = currentConnector.ClientID 'id of the element on the source end of the connector 
    '             Dim sourceEndNavigable
    '             sourceEndNavigable = currentConnector.ClientEnd.Navigable 'navigability on the source end of the connector 
    '             Dim sourceEndName
    '             sourceEndName = currentConnector.ClientEnd.Role 'role name on the source end of the connector 
    '             Dim sourceEndDefinition
    '             sourceEndDefinition = currentConnector.ClientEnd.RoleNote 'role definition on the source end of the connector 

    '             Dim targetEndNavigable
    '             targetEndNavigable = currentConnector.SupplierEnd.Navigable 'navigability on the target end of the connector 
    '             Dim targetEndName
    '             targetEndName = currentConnector.SupplierEnd.Role 'role name on the target end of the connector 
    '             Dim targetEndDefinition
    '             targetEndDefinition = currentConnector.SupplierEnd.RoleNote 'role definition on the target end of the connector 


    '             Dim sourceEndElement As EA.Element

    '             If sourceEndNavigable = "Navigable" And sourceEndDefinition = "" And currentConnector.Type <> "Dependency" Then
    '		'get the element on the source end of the connector 
    '		set sourceEndElement = Repository.GetElementByID(sourceEndElementID) 

    '	Session.Output("Error: Class [«" & getStereotypeOfClass(sourceEndElement) & "» " & sourceEndElement.Name & "] \ association role [" & sourceEndName & "] has no definition. [/krav/3] & [/krav/definisjoner]")
    '                 globalErrorCounter = globalErrorCounter + 1
    '             End If

    '             If targetEndNavigable = "Navigable" And targetEndDefinition = "" And currentConnector.Type <> "Dependency" Then
    '		'get the element on the source end of the connector (also source end element here because error message is related to the element on the source end of the connector) 
    '		set sourceEndElement = Repository.GetElementByID(sourceEndElementID) 

    '	Session.Output("Error: Class [«" & getStereotypeOfClass(sourceEndElement) & "» " & sourceEndElement.Name & "] \ association role [" & targetEndName & "] has no definition. [/krav/3] & [/krav/definisjoner]")
    '                 globalErrorCounter = globalErrorCounter + 1
    '             End If
    '         Case otPackage
    '	' Code for when the function's parameter is a package 

    '	set currentPackage = theObject 

    '	'check package definition 
    'If currentPackage.Notes = "" Then
    '                 Session.Output("Error: Package [" & currentPackage.Name & "] lacks a definition. [/krav/definisjoner]")
    '                 globalErrorCounter = globalErrorCounter + 1
    '             End If
    '         Case Else
    '             'TODO: need some type of exception handling here
    '             Session.Output("Debug: Function [CheckDefinition] started with invalid parameter.")
    '     End Select

    ' End Sub
End Class

