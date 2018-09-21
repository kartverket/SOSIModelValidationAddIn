Partial Public Class ModelValidation

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Script Name: anbefalingStyleGuide
    ' Author: Sara Henriksen, modification from original script: Åsmund Tjora
    ' Date: 2016-08-29 (original), 2018-09-20 (modified version)
    ' Purpose: check that the stereotype for packages and elements got the right use of lower- and uppercase, if not, return a warning. Stereotypes to be checked:
    ' CodeList, dataType, enumeration, interface, Leaf, Union, FeatureType, ApplicationSchema (case sensitive)
    ' /anbefaling/styleGuide 
    ' sub procedure to check if the stereotype for a given package or element
    ' @param[in]: theElement (EA.ObjectType) The object to check against /anbefaling/styleGuide 
    ' supposed to be one of the following types: EA.Element, EA.Package  

    Sub anbefalingStyleGuide(theElement)

        If logLevel = "Warning" Then
            Select Case theElement.ObjectType

                Case EA.ObjectType.otPackage

                    If UCase(theElement.Element.Stereotype) = "APPLICATIONSCHEMA" And Not theElement.Element.Stereotype = "ApplicationSchema" Then
                        Output("Warning: Package [«" + theElement.Element.Stereotype + "» " + theElement.Name + "]  has a stereotype with wrong use of lower-and uppercase. Expected use of case: ApplicationSchema [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                    If UCase(theElement.Element.Stereotype) = "LEAF" And Not theElement.Element.Stereotype = "Leaf" Then
                        Output("Warning: Package [«" + theElement.Element.Stereotype + " »" + theElement.Name + "]  has a stereotype with wrong use of lower-and uppercase. Expected use of case: Leaf [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                Case EA.ObjectType.otElement

                    If UCase(theElement.Stereotype) = "CODELIST" And Not theElement.Stereotype = "CodeList" Then
                        Output("Warning: Element [«" + theElement.Stereotype + "» " + theElement.Name + "] has a stereotype with wrong use of lower-and uppercase. Expected use of case: CodeList [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                    If UCase(theElement.Stereotype) = "DATATYPE" And Not theElement.Stereotype = "dataType" Then
                        Output("Warning: Element [«" + theElement.Stereotype + "» " + theElement.Name + "] has a stereotype with wrong use of lower-and uppercase. Expected use of case: dataType [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                    If UCase(theElement.Stereotype) = "FEATURETYPE" And Not theElement.Stereotype = "FeatureType" Then
                        Output("Warning: Element [«" + theElement.Stereotype + "» " + theElement.Name + "] has a stereotype with wrong use of lower-and uppercase. Expected use of case: FeatureType [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                    If UCase(theElement.Stereotype) = "UNION" And Not theElement.Stereotype = "Union" Then
                        Output("Warning: Element [«" + theElement.Stereotype + "» " + theElement.Name + "] has a stereotype with wrong use of lower-and uppercase. Expected use of case: Union [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                    If UCase(theElement.Stereotype) = "ENUMERATION" And Not theElement.Stereotype = "enumeration" Then
                        Output("Warning: Element [«" + theElement.Stereotype + "» " + theElement.Name + "] has a stereotype with wrong use of lower-and uppercase. Expected use of case: enumeration [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If

                    If UCase(theElement.Stereotype) = "INTERFACE" And Not theElement.Stereotype = "interface" Then
                        Output("Warning: Element [«" + theElement.Stereotype + "» " + theElement.Name + "] has a stereotype with wrong use of lower-and uppercase. Expected use of case: interface [/anbefaling/styleGuide]")
                        warningCounter += 1
                    End If
            End Select
        End If
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------

End Class