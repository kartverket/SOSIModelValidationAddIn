Partial Public Class ModelValidation

    'Sub name:      kravKodenavn
    'Author: 		Kent Jonsrud
    'Date: 			2020-08-17/-18
    'Purpose: 		'/krav/Kodenavn is a national adaption of Iso 19103 Requirement 6 - legal NonWhitespaceNames on code list code names. If a initial value (Default) exists then check for valid and unique NWNAmes in all initial values for this codelist.
    'Parameter: 	an element that has stereotype CodeList or Enumeration, or has keyword enumeration (EA-type Enumeration)
    'Requirement class:     n/a
    'Conformance class:     Regler for UML-modellering versjon 5.1:2020 - Basisregler for UML med nasjonale tilpassinger (innstramminger/utelatelser)


    Sub kravKodenavn(theThing As EA.Element)
        Call kravKodenavnOnElement(theThing)
    End Sub
    Sub kravKodenavnOnElement(theElement)
        Dim CodeNames As New System.Collections.ArrayList
        Dim CodeDefaults As New System.Collections.ArrayList
        Dim kw = theElement.Stereotype
        If kw = "" Then
            kw = "enumeration"
        End If

        For Each attribute In theElement.Attributes
            If attribute.Default <> "" Then
                ' test whether the initial value is a case insensitively unique NWName, ignoring the spelling of the code name
                If Not isNWName(attribute.Default) Then
                    Output("Error: Class [«" & kw & "» " & theElement.Name & "] code name [" & attribute.Name & "] has illegal character in code initial value [" & attribute.Default & "]. [/krav/kodenavn]")
                    errorCounter += 1
                End If
                If CodeDefaults.IndexOf(UCase(attribute.Default), 0) <> -1 Then
                    Output("Error: Class [«" & kw & "» " & theElement.Name & "] code name [" & attribute.Name & "] has non-unique code initial value [" & attribute.Default & "]. [/krav/kodebruk]")
                    errorCounter += 1
                Else
                    If CodeNames.IndexOf(UCase(attribute.Name), 0) = -1 Then
                        CodeNames.Add(UCase(attribute.Name))
                    End If
                    CodeDefaults.Add(UCase(attribute.Default))
                End If
            Else
                ' test whether the code name is a case insensitively unique NWName when no initial value is present
                If Not isNWName(attribute.Name) Then
                    Output("Error: Class [«" & kw & "» " & theElement.Name & "] has illegal character in code name [" & attribute.Name & "]. [/krav/6]")
                    errorCounter += 1
                End If
                If Not isLCNameX(attribute.Name) Then
                    Output("Warning: Class [«" & kw & "» " & theElement.Name & "] has code name starting with uppercase character or letter [" & attribute.Name & "]. [/krav/6]")
                    warningCounter += 1
                End If
                If CodeNames.IndexOf(UCase(attribute.Name), 0) <> -1 Then
                    Output("Error: Class [«" & kw & "» " & theElement.Name & "] has non-unique code names [" & attribute.Name & "]. [/krav/16]")
                    errorCounter += 1
                Else
                    CodeNames.Add(UCase(attribute.Name))
                End If
            End If
        Next

        If CodeDefaults.Count <> 0 And CodeNames.Count <> CodeDefaults.Count Then
            Output("Error: Class [«" & kw & "» " & theElement.Name & "] has a mix of codes with [" & CodeDefaults.Count & "] and without [" & CodeNames.Count - CodeDefaults.Count & "] valid initial values. [/krav/kodebruk]")
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