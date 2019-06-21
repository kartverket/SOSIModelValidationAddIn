Partial Public Class ModelValidation

    'Sub name:      requirement7
    'Author: 		Kent Jonsrud
    'Date: 			2018-10-18, 2019-06-20
    'Purpose: 		'/krav/7 Iso 19103 Requirement 7 - definitions on code list and code list codes
    'Parameter: 	the element that shall have definitions
    'Requirement class:     requirement7
    'Conformance class:     from iso 19103 part nnn

    Sub requirement7(theThing As EA.Element)
        Call requirement7onElement(theThing)
    End Sub
    Sub krav7(theThing As EA.Element)
        Call krav7onElement(theThing)
    End Sub
    Sub requirement7onElement(theElement)
        If theElement.Notes = "" Then
            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has no definition. [/krav/7]")
            errorCounter += 1
        End If
        For Each attribute In theElement.Attributes
            If attribute.Notes = "" Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has code without definition [" & attribute.Name & "]. [/krav/7]")
                errorCounter += 1
            End If
        Next
    End Sub
    Sub krav7onElement(theElement)
        If Not checkAllCodeNames And avoidableCodeLists.Contains(theElement.Name) Then
            Output("Info: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] content was not tested for compliance with /krav/7")
        Else
            If theElement.Notes = "" Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has no definition. [/krav/7]")
                errorCounter += 1
            End If
            For Each attribute In theElement.Attributes
                If attribute.Notes = "" Then
                    If logLevel = "Warning" Then
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] only codes that are proper names can be without definition [" & attribute.Name & "]. [/krav/7]")
                        warningCounter += 1
                    End If
                End If
            Next
        End If
    End Sub
End Class