Partial Public Class ModelValidation

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Script Name: kravFlerspråklighetpakke
    ' Author: Sara Henriksen (original version), Åsmund Tjora, Tore Johnsen
    ' Date: 26.07.16 (original version), 20.01.17 (release 1.1), 02.02.17, enhanced 23.08.18
    '       18.10.2018 -  /krav/flersprålighet/pakke moved from script to AddIn
    ' Purpose: Check if the ApplicationSchema-package got a tag named "language" and  check if the value is empty or not. 
    ' sub procedure to check if the package has the provided tag and a value with correct structure
    ' @param[in]: thePackage (Package)
    ' Conformity class: Basisregler for UML

    Sub kravFlerspråklighetpakke(thePackage)

        Dim theElement As EA.Element
        theElement = thePackage.Element
        Dim taggedValueName
        taggedValueName = "language"

        If taggedValueName = "language" Then
            If UCase(theElement.Stereotype) = UCase("applicationSchema") Then

                Dim packageTaggedValues As EA.Collection
                packageTaggedValues = theElement.TaggedValues

                Dim taggedValueLanguageMissing
                taggedValueLanguageMissing = True
                'iterate trough the tagged values 
                Dim packageTaggedValuesCounter
                For packageTaggedValuesCounter = 0 To packageTaggedValues.Count - 1
                    Dim currentTaggedValue As EA.TaggedValue
                    currentTaggedValue = packageTaggedValues.GetAt(packageTaggedValuesCounter)

                    'check if the provided tagged value exist
                    If (currentTaggedValue.Name = "language") And Not (currentTaggedValue.Value = "") Then
                        'check if the value is no or en, if not, retrun a warning 
                        If Not Mid(StrReverse(currentTaggedValue.Value), 1, 2) = "ne" And Not Mid(StrReverse(currentTaggedValue.Value), 1, 2) = "on" Then
                            If logLevel = "Warning" Then
                                Output("Warning: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & currentTaggedValue.Name & "] has a value """ & currentTaggedValue.Value & """  which is neither ""no"" nor ""en"". [/krav/flerspråklighet/pakke]")
                                warningCounter += 1
                            End If
                        End If
                        taggedValueLanguageMissing = False
                        Exit For
                    End If
                    If currentTaggedValue.Name = "language" And currentTaggedValue.Value = "" Then
                        Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & currentTaggedValue.Name & "] is missing a value. [/krav/flerspråklighet/pakke]")
                        errorCounter += 1
                        taggedValueLanguageMissing = False
                        Exit For
                    End If
                Next
                If taggedValueLanguageMissing Then
                    Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] is missing a [language] tag. [/krav/flerspråklighet/pakke]")
                    errorCounter += 1
                End If
            End If
        End If


    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------


End Class
