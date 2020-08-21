Partial Public Class ModelValidation

    'Sub name:      requirement6
    'Author: 		Kent Jonsrud
    'Date: 			2018-10-18, 2019-06-20, 2020-07-17, 2020-08-21
    'Purpose: 		'Iso 19103 Requirement 6 - legal NonWhitespaceNames on code list code names
    'Parameter: 	an element that has stereotype CodeList or Enumeration, or keyword enumeration (EA-type Enumeration)
    'Requirement class:     requirement6
    'Conformance class:     from iso 19103 part nnn

    Sub requirement6(theThing As EA.Element)
        Call requirement6onElement(theThing)
    End Sub
    Sub requirement6onElement(theElement)
        Dim CodeNames As New System.Collections.ArrayList
        If Not checkAllCodeNames And avoidableCodeLists.Contains(theElement.Name) Then
            Output("Info: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] content was not tested for compliance with /krav/6")
        Else
            For Each attribute In theElement.Attributes
                If Not isNWName(attribute.Name) Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has illegal code name [" & attribute.Name & "]. [/krav/6]")
                    errorCounter += 1
                End If
                If CodeNames.IndexOf(UCase(attribute.Name), 0) <> -1 Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique code names [" & attribute.Name & "]. [/krav/16]")
                    errorCounter += 1
                Else
                    CodeNames.Add(UCase(attribute.Name))
                End If
            Next
        End If
    End Sub
End Class