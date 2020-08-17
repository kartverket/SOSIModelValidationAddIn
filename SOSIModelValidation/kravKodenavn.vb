Partial Public Class ModelValidation

    'Sub name:      kravKodenavn
    'Author: 		Kent Jonsrud
    'Date: 			2020-08-17
    'Purpose: 		'/krav/Kodenavn is a national adaption of Iso 19103 Requirement 6 - legal NonWhitespaceNames on code list code names. If a initial value (Default) exists then check for valid and unique NWNAmes in all initial values for this codelist.
    'Parameter: 	an element that has stereotype CodeList or Enumeration, or keyword enumeration (EA-type Enumeration)
    'Requirement class:     n/a
    'Conformance class:     Regler for UML-modellering versjon 5.1:2020 - Basisregler for UML med nasjonale tilpassinger (innstramminger/utelatelser)


    Sub kravKodenavn(theThing As EA.Element)
        Call kravKodenavnonElement(theThing)
    End Sub
    Sub kravKodenavnonElement(theElement)
        Dim CodeNames As New System.Collections.ArrayList
        Dim CodeDefaults As New System.Collections.ArrayList

        For Each attribute In theElement.Attributes
            If attribute.Default <> "" Then
                ' test whether the default value is a unique NWName, ignore code name
                If Not isNWName(attribute.Default) Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has illegal character in code initial value [" & attribute.Name & "] [" & attribute.Default & "]. [/krav/kodenavn]")
                    errorCounter += 1
                End If
                If CodeDefaults.IndexOf(UCase(attribute.Default), 0) <> -1 Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique code initial value [" & attribute.Name & "] [" & attribute.Default & "]. [/krav/16]")
                    errorCounter += 1
                Else
                    CodeNames.Add(UCase(attribute.Name))
                    CodeDefaults.Add(UCase(attribute.Default))
                End If
            Else
                ' test whether the code name is a unique NWName (and no default is present)
                If Not isNWName(attribute.Name) Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has illegal character in code name [" & attribute.Name & "]. [/krav/6]")
                    errorCounter += 1
                End If
                If Not isLCNameX(attribute.Name) Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has code name starting with uppercase character or letter [" & attribute.Name & "]. [/krav/6]")
                    warningCounter += 1
                End If
                If CodeNames.IndexOf(UCase(attribute.Name), 0) <> -1 Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique code names [" & attribute.Name & "]. [/krav/16]")
                    errorCounter += 1
                Else
                    CodeNames.Add(UCase(attribute.Name))
                End If
            End If
        Next

        If CodeDefaults.Count <> 0 And CodeNames.Count <> CodeDefaults.Count Then
            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has a mix of codes with, and without initial values [" & CodeNames.Count - CodeDefaults.Count & "]. [/krav/kodebruk]")
            errorCounter += 1
        End If

    End Sub

    Function isLCNameX(streng)
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
        isLCNameX = u
    End Function
End Class