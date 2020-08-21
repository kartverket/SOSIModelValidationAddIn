Partial Public Class ModelValidation

    'Sub name:      krav6
    'Author: 		Kent Jonsrud
    'Date: 			2018-10-18, 2019-06-20, 2020-07-17, 2020-08-21
    'Purpose: 		'/krav/6 adapted from Iso 19103 Requirement 6 - legal NonWhitespaceNames on code list code names
    'Parameter: 	an element that has stereotype CodeList or Enumeration, or keyword enumeration (EA-type Enumeration)
    'Requirement class:     requirement6
    'Conformance class:     from iso 19103 part nnn


    Sub krav6(theThing As EA.Element)
        Call krav6onElement(theThing)
    End Sub
    Sub krav6onElement(theElement)
        Dim CodeNames As New System.Collections.ArrayList

        For Each attribute In theElement.Attributes
            If Not isNWName(attribute.Name) Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has illegal character in code name [" & attribute.Name & "]. [/krav/6]")
                errorCounter += 1
            End If
            If CodeNames.IndexOf(UCase(attribute.Name), 0) <> -1 Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique code names [" & attribute.Name & "]. [/krav/16]")
                errorCounter += 1
            Else
                CodeNames.Add(UCase(attribute.Name))
            End If
        Next

    End Sub

    Function isLCName(streng)
        Dim tegn, u
        u = True
        tegn = Mid(streng, 1, 1)
        If tegn = "1" Or tegn = "2" Or tegn = "3" Or tegn = "4" Or tegn = "5" Or tegn = "6" Or tegn = "7" Or tegn = "8" Or tegn = "9" Or tegn = "0" Then
            u = False
        End If
        If tegn = UCase(tegn) Then
            u = False
        End If
        'end if
        isLCName = u
    End Function
End Class