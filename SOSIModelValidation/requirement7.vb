Partial Public Class ModelValidation

    'Sub name:      requirement7
    'Author: 		Kent Jonsrud
    'Date: 			2018-10-18
    'Purpose: 		'/krav/15 Iso 19103 Requirement 7 - definitions on code list codes
    'Parameter: 	the element that has a stereotype CodeList or type Enumeration
    'Requirement class:     requirement6
    'Conformance class:     from iso 19103 part nnn

    Sub requirement7(theThing As EA.Element)
        Call requirement7onElement(theThing)
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
End Class