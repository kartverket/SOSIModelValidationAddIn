Partial Public Class ModelValidation

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Script Name: kravSOSIModellregisterApplikasjonsskjemaStatus 
    ' Author: Sara Henriksen, Tore Johnsen
    ' Date: 25.07.16 - enhanced 23.08.18
    '       19.10.2018 - moved from script to AddIn
    ' Purpose: Check if the ApplicationSchema-package got a tagged value named "SOSI_modellstatus" and checks if it is a valid value 
    ' /krav/SOSI-modellregister/applikasjonsskjema/status
    ' sub procedure to check if the tagged value with the provided name exist, and checks if the value is valid or not 
    ' (valid values: utkast, gyldig, utkastOgSkjult, foreslått, erstattet, tilbaketrukket og ugyldig). 
    '@param[in]: thePackage (Package)

    Sub kravSOSIModellregisterApplikasjonsskjemaStatus(thePackage)

        Dim theElement
        theElement = thePackage.Element
        Dim taggedValueName
        taggedValueName = "SOSI_modellstatus"

        If UCase(theElement.Stereotype) = UCase("applicationSchema") Then

            If Not theElement Is Nothing And Len(taggedValueName) > 0 Then

                'check if the element has a tagged value with the provided name
                Dim taggedValueSOSIModellstatusMissing
                taggedValueSOSIModellstatusMissing = True
                Dim currentExistingTaggedValue As EA.TaggedValue
                Dim taggedValuesCounter

                For taggedValuesCounter = 0 To theElement.TaggedValues.Count - 1
                    currentExistingTaggedValue = theElement.TaggedValues.GetAt(taggedValuesCounter)

                    If currentExistingTaggedValue.Name = taggedValueName Then
                        'check if the value of the tag is one of the approved values. 
                        If currentExistingTaggedValue.Value = "utkast" Or currentExistingTaggedValue.Value = "gyldig" Or currentExistingTaggedValue.Value = "utkastOgSkjult" Or currentExistingTaggedValue.Value = "foreslått" Or currentExistingTaggedValue.Value = "erstattet" Or currentExistingTaggedValue.Value = "tilbaketrukket" Or currentExistingTaggedValue.Value = "ugyldig" Then

                            taggedValueSOSIModellstatusMissing = False
                        ElseIf currentExistingTaggedValue.Value = "" Then
                            Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [SOSI_modellstatus] is missing a value. [/krav/SOSI-modellregister/applikasjonsskjema/status]")
                            errorCounter += 1
                            taggedValueSOSIModellstatusMissing = False
                        Else
                            Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [SOSI_modellstatus] has a value """ & currentExistingTaggedValue.Value & """. Valid values are: ""utkast"", ""utkastOgSkjult"", ""foreslått"", ""gyldig"", ""erstattet"", ""tilbaketrukket"" or ""ugyldig"". [/krav/SOSI-modellregister/applikasjonsskjema/status]")
                            errorCounter += 1
                            taggedValueSOSIModellstatusMissing = False
                        End If
                    End If
                Next

                'if the tag doesen't exist, return an error-message 
                If taggedValueSOSIModellstatusMissing Then
                    Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] is missing a [SOSI_modellstatus] tag. [krav/SOSI-modellregister/applikansjonsskjema/status]")
                    errorCounter += 1
                End If
            End If
        End If
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------


End Class
