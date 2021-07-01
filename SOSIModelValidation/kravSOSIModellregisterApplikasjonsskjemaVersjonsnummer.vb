Partial Public Class ModelValidation

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Script Name: kravSOSIModellregisterApplikasjonskjemaVersjonsnummer
    ' Author: Sara Henriksen, Åsmund Tjora, Tore Johnsen, Magnus Karge
    ' Purpose: check if the package name ends with a version number. The version number could be a date or a serial number. Returns an error if the version 
    ' number contains anything other than 0-2 dots or numbers. 
    ' Packages under development should have the text "Utkast" as the final element, after the version number. 
    ' Date: 25.08.16 (original version) 10.01.17 (Updated version)
    '       19.10.2018 - moved from script to AddIn
    '       29.06.2021 (Magnus Karge) - adjusted to v5.1 of reference standard (Regler for UML-modellering) and improved error message including following changes
    '                       1. runs the test only if SOSI_modellstatus = gyldig (new element in standard)
    '                       2. adjusted error message in order to provide support for writing a correct package name with version number
    ' Conformity class: SOSI-modellregister


    Sub kravSOSIModellregisterApplikasjonskjemaVersjonsnummer(thePackage)

        'get SOSI_modellstatus
        Dim theElement
        theElement = thePackage.Element
        Dim sosiModellstatus = ""
        Dim currentExistingTaggedValue As EA.TaggedValue

        For Each currentExistingTaggedValue In theElement.TaggedValues
            If currentExistingTaggedValue.Name = "SOSI_modellstatus" Then
                sosiModellstatus = currentExistingTaggedValue.Value
            End If
        Next

        If UCase(thePackage.Element.Stereotype) = "APPLICATIONSCHEMA" And sosiModellstatus = "gyldig" Then
            'find the last part of the package name, after "-" 
            Dim startContent, endContent, stringContent, cleanContent

            'remove any "Utkast" part of the name 
            cleanContent = Replace(UCase(thePackage.Name), "UTKAST", "")

            endContent = Len(cleanContent)

            startContent = InStr(cleanContent, "-")

            stringContent = Mid(cleanContent, startContent + 1, endContent)
            Dim versionNumberInPackageName
            versionNumberInPackageName = False
            'count number of dots, only allowed to use max two. 
            Dim dotCounter
            dotCounter = 0

            'check that the package name contains a "-", and thats it is just number(s) and "." after. 
            If InStr(thePackage.Name, "-") Then
                'if the string is numeric or it has dots, set the valueOk true 
                If InStr(stringContent, ".") Or IsNumeric(stringContent) Then
                    Dim i, tegn
                    For i = 1 To Len(stringContent)
                        tegn = Mid(stringContent, i, 1)
                        If tegn = "." Then
                            dotCounter += 1
                        End If
                    Next
                    'count number of dots. If it's more than 2 return an error. 
                    If dotCounter < 3 Then
                        versionNumberInPackageName = True
                    Else
                        versionNumberInPackageName = False
                    End If
                End If
            End If

            'check the string for letters and symbols. If the package name contains one of the following, then return an error. 
            If InStr(UCase(stringContent), "A") Or InStr(UCase(stringContent), "B") Or InStr(UCase(stringContent), "C") Or InStr(UCase(stringContent), "D") Or InStr(UCase(stringContent), "E") Or InStr(UCase(stringContent), "F") Or InStr(UCase(stringContent), "G") Or InStr(UCase(stringContent), "H") Or InStr(UCase(stringContent), "I") Or InStr(UCase(stringContent), "J") Or InStr(UCase(stringContent), "K") Or InStr(UCase(stringContent), "L") Then
                versionNumberInPackageName = False
            End If
            If InStr(UCase(stringContent), "M") Or InStr(UCase(stringContent), "N") Or InStr(UCase(stringContent), "O") Or InStr(UCase(stringContent), "P") Or InStr(UCase(stringContent), "Q") Or InStr(UCase(stringContent), "R") Or InStr(UCase(stringContent), "S") Or InStr(UCase(stringContent), "T") Or InStr(UCase(stringContent), "U") Or InStr(UCase(stringContent), "V") Or InStr(UCase(stringContent), "W") Or InStr(UCase(stringContent), "X") Then
                versionNumberInPackageName = False
            End If
            If InStr(UCase(stringContent), "Y") Or InStr(UCase(stringContent), "Z") Or InStr(UCase(stringContent), "Æ") Or InStr(UCase(stringContent), "Ø") Or InStr(UCase(stringContent), "Å") Then
                versionNumberInPackageName = False
            End If
            If InStr(stringContent, ",") Or InStr(stringContent, "!") Or InStr(stringContent, "@") Or InStr(stringContent, "%") Or InStr(stringContent, "&") Or InStr(stringContent, """") Or InStr(stringContent, "#") Or InStr(stringContent, "$") Or InStr(stringContent, "'") Or InStr(stringContent, "(") Or InStr(stringContent, ")") Or InStr(stringContent, "*") Or InStr(stringContent, "+") Or InStr(stringContent, "/") Then
                versionNumberInPackageName = False
            End If
            If InStr(stringContent, ":") Or InStr(stringContent, ";") Or InStr(stringContent, ">") Or InStr(stringContent, "<") Or InStr(stringContent, "=") Then
                versionNumberInPackageName = False
            End If

            If versionNumberInPackageName = False Then
                Output("Error: Package [" & thePackage.Name & "] does either not have a name ending with a valid version number or the name does not match the required format: <name>-<version number>. [/krav/SOSI-modellregister/applikasjonsskjema/versjonsnummer]")
                errorCounter += 1
            End If
        End If
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------


End Class
