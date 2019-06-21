Partial Public Class ModelValidation

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Script Name: checkNumericinitialValues
    ' Author: Sara Henriksen, Tore Johnsen
    ' Date: 27.07.16
    '       19.10.2018 - Moved from script to AddIn
    '       2019-06-20 - tester mot avoidableCodeLists (Kent)
    ' Purpose: checks every initial values in  codeLists and enumerations for a package. Returns a warning for each attribute with intitial value that is numeric 
    ' /anbefaling/1
    ' sub procedure to check if the initial values of the attributes in a CodeList/enumeration are numeric or not. 
    ' @param[in]: theElement (EA.element) The element containing  attributes with potentially numeric inital values 
    ' Conformity class: Basisregler for UML

    Sub recommendation1(theElement)

        Dim attr As EA.Attribute
        If Not checkAllCodeNames And avoidableCodeLists.Contains(theElement.Name) Then
            If logLevel = "Warning" Then
                Output("Info: Class [" & theElement.Name & "] content was not tested for compliance with /anbefaling/1")
            End If
        Else
            'navigate through all attributes in the codeLists/enumeration 
            For Each attr In theElement.Attributes
                'check if the initial values are numeric 
                If IsNumeric(attr.Default) Then
                    If logLevel = "Warning" Then
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ attribute [" & attr.Name & "] has numeric initial value [" & attr.Default & "] that is probably meaningless. Recommended to use script <flyttInitialverdiPåKodelistekoderTilSOSITag>. [/anbefaling/1]")
                        warningCounter += 1
                    End If
                End If
            Next
        End If
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------


End Class
