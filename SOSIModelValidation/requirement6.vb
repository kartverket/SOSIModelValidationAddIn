Partial Public Class ModelValidation

    'Sub name:      requirement6
    'Author: 		Kent Jonsrud
    'Date: 			2018-10-18
    'Purpose: 		'/krav/15 Iso 19103 Requirement 6 - legal NCNames on code list codes
    'Parameter: 	the element that has a stereotype CodeList or type Enumeration
    'Requirement class:     requirement6
    'Conformance class:     from iso 19103 part nnn

    Sub requirement6(theThing As EA.Element)
        Call requirement6onElement(theThing)
    End Sub
    Sub requirement6onElement(theElement)
        Dim CodeNames As New System.Collections.ArrayList
        If Not isNCName(theElement.Name) Then
            Output("Error: Class [" & theElement.Name & "] has illegal name. [/krav/6]")
            errorCounter += 1
        End If
        'PropertyNames.Clear()
        If "optionX" = "optionX" And theElement.Name <> "Kommunenummer" Then
            For Each attribute In theElement.Attributes
                If Not isNCName(attribute.Name) Then
                    If theElement.Name <> "Kommunenummer" Then
                        Output("Error: Class [" & theElement.Name & "] has illegal code name [" & attribute.Name & "]. [/krav/6]")
                    End If
                End If
                If CodeNames.IndexOf(UCase(attribute.Name), 0) <> -1 Then
                    Output("Error: Class [" & theElement.Name & "] has non-unique code names [" & attribute.Name & "]. [/krav/16]")
                    errorCounter += 1
                Else
                    CodeNames.Add(UCase(attribute.Name))
                End If
            Next
        Else
            Output("Info: Class [" & theElement.Name & "] was not tested against /krav/6")
        End If
    End Sub
End Class