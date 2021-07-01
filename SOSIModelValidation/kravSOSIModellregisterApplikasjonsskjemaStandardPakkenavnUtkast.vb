Partial Public Class ModelValidation

    ' Author: Åsmund Tjora	
    ' Purpose: check that packages with "Utkast" as part of the package name also has "Utkast" as SOSI_modellstatus tag and that package with the "Utkast"
    ' SOSI_modellstatus tag also has "Utkast" as part of the name. 
    ' Date: 2017-01-10 (original script), 2018-10-15 (add-in)

    Sub kravSOSIModellregisterApplikasjonskjemaStandardPakkenavnUtkast(thePackage As EA.Package)
        Dim utkastInName, utkastInTag As Boolean
        Dim modellstatusExists As Boolean
        Dim modellstatusTag = ""

        ' Check utkastInName by comparing length of package name and package name where "Utkast" (any capitalization) is removed
        utkastInName = Not (Len(thePackage.Name) = Len(Replace(UCase(thePackage.Name), "UTKAST", "")))

        ' Check if SOSI_modellstatus tag exists and if it has the value "utkast"
        utkastInTag = False
        modellstatusExists = False
        Dim currentTaggedValue As EA.TaggedValue
        For Each currentTaggedValue In thePackage.Element.TaggedValues
            If currentTaggedValue.Name = "SOSI_modellstatus" Then
                modellstatusExists = True
                If currentTaggedValue.Value = "utkast" Or currentTaggedValue.Value = "utkastOgSkjult" Then utkastInTag = True
                modellstatusTag = currentTaggedValue.Value
            End If
        Next

        ' If package has "Utkast" in name and the SOSI_modellstatus tag exist, report error if tag does not have the value "utkast"
        ' If package has "Utkast" in name, report error if the SOSI_modellstatus tag does not exist
        ' If package has SOSI_modellstatus tag set to "utkast" report error if "Utkast" is not a part of the package name
        If utkastInName Then
            If modellstatusExists Then
                If modellstatusTag = "" Then
                    Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has Utkast as part of the name, but the tag [SOSI_modellstatus] has no value. Expected value [utkast] or [utkastOgSkjult]. [/krav/SOSI-modellregister/applikasjonsskjema/standard/pakkenavn/utkast]")
                    errorCounter += 1
                ElseIf Not utkastInTag Then
                    Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has Utkast as part of the name, but the tag [SOSI_modellstatus] has the value [" & modellstatusTag & "]. Expected value [utkast] or [utkastOgSkjult]. [/krav/SOSI-modellregister/applikasjonsskjema/standard/pakkenavn/utkast]")
                    errorCounter += 1
                End If
            Else
                Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has Utkast as part of the name, but the tag [SOSI_modellstatus] is missing. [/krav/SOSI-modellregister/applikasjonsskjema/standard/pakkenavn/utkast]")
                errorCounter += 1
            End If
        Else
            If utkastInTag Then
                Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has [SOSI_modellstatus] tag with [" + modellstatusTag + "] value, but Utkast is not part of the package name. [/krav/SOSI-modellregister/applikasjonsskjema/standard/pakkenavn/utkast]")
                errorCounter += 1
            End If
        End If

        ' If package has "Utkast" but with wrong capitalization, return warning
        If utkastInName And logLevel = "Warning" Then
            If Not (Len(Replace(thePackage.Name, "Utkast", "")) = Len(Replace(UCase(thePackage.Name), "UTKAST", ""))) Then
                Output("Warning: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "]. Unexpected upper/lower case of the Utkast part of the name. [/krav/SOSI-modellregister/applikasjonsskjema/standard/pakkenavn/utkast]")
                warningCounter += 1
            End If
        End If

    End Sub

End Class