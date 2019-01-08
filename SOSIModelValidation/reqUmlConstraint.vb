Partial Public Class ModelValidation
    ' Script Name: checkConstraint
    ' Author: Sara Henriksen
    ' Date: 26.08.16
    ' Purpose: to check if a constraint lacks name or definition. 
    ' req/uml/constraint & krav/definisjoner
    ' sub procedure to check the current element/attribute/connector/package for constraints without name or definition
    ' not sure if it is possible in EA that constraints without names can exist, checking it anyways
    ' @param[in]: currentConstraint (EA.Constraint) theElement (EA.ObjectType) The object to check against req/uml/constraint,  
    ' supposed to be one of the following types: EA.Element, EA.Attribute, EA.Connector, EA.package

    Sub reqUmlConstraint(currentConstraint, theElement)

        Dim currentConnector As EA.Connector
        Dim currentElement As EA.Element
        Dim currentAttribute As EA.Attribute
        Dim currentPackage As EA.Package

        Select Case theElement.ObjectType

        'if the object is an element
            Case EA.ObjectType.otElement
                currentElement = theElement

                'if the current constraint lacks definition, then return an error
                If currentConstraint.Notes = "" Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ constraint [" & currentConstraint.Name & "] lacks definition. [/req/uml/constraint] & [krav/definisjoner]")
                    errorCounter = errorCounter + 1
                End If

                'if the current constraint lacks a name, then return an error 
                If currentConstraint.Name = "" Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & currentElement.Name & "] has a constraint without a name. [/req/uml/constraint]")
                    errorCounter = errorCounter + 1
                End If

        'if the object is an attribute 
            Case EA.ObjectType.otAttribute
                currentAttribute = theElement

                'if the current constraint lacks definition, then return an error
                Dim parentElementID
                parentElementID = currentAttribute.ParentID
                Dim parentElementOfAttribute As EA.Element
                parentElementOfAttribute = theRepository.GetElementByID(parentElementID)
                If currentConstraint.Notes = "" Then
                    Output("Error: Class [" & parentElementOfAttribute.Name & "] \ attribute [" & theElement.Name & "] \ constraint [" & currentConstraint.Name & "] lacks definition. [/req/uml/constraint] & [krav/definisjoner]")
                    errorCounter = errorCounter + 1
                End If

                'if the current constraint lacks a name, then return an error 	
                If currentConstraint.Name = "" Then
                    Output("Error: Attribute [" & theElement.Name & "] has a constraint without a name. [/req/uml/constraint]")
                    errorCounter = errorCounter + 1
                End If

            Case EA.ObjectType.otPackage
                currentPackage = theElement

                'if the current constraint lacks definition, then return an error message
                If currentConstraint.Notes = "" Then
                    Output("Error: Package [«" & theElement.Element.Stereotype & "» " & theElement.Name & "] \ constraint [" & currentConstraint.Name & "] lacks definition. [/req/uml/constraint] & [krav/definisjoner]")
                    errorCounter = errorCounter + 1
                End If

                'if the current constraint lacks a name, then return an error meessage		
                If currentConstraint.Name = "" Then
                    Output("Error: Package [«" & theElement.Element.Stereotype & "» " & currentElement.Name & "] has a constraint without a name. [/req/uml/constraint]")
                    errorCounter = errorCounter + 1
                End If

            Case EA.ObjectType.otConnector
                currentConnector = theElement

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

                    Output("Error: Constraint [" & currentConstraint.Name & "] owned by connector [ " & theElement.Name & "] between class [" & sourceElementOfConnector.Name & "] and class [" & targetElementOfConnector.Name & "] lacks definition. [/req/uml/constraint] & [krav/definisjoner]")
                    errorCounter = errorCounter + 1
                End If

                'if the current constraint lacks a name, then return an error message		
                If currentConstraint.Name = "" Then
                    Output("Error: Connector [" & theElement.Name & "] has a constraint without a name. [/req/uml/constraint]")
                    errorCounter = errorCounter + 1

                End If
        End Select
    End Sub


End Class
