Partial Public Class ModelValidation

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Script Name: kravTaggedValueSpråk
    ' Author: Sara Henriksen (original version), Åsmund Tjora, Tore Johnsen
    ' Date: 26.07.16 (original version), 20.01.17 (release 1.1), 02.02.17, enhanced 23.08.18
    '       18.10.2018 - Moved /krav/taggedValueSpråk from script to AddIn
    ' Purpose: Check if the ApplicationSchema-package got a tag named "language" and  check if the value is empty or not. 
    ' Check that designation tags have correct structure: "{name}"@{language}, and that there is at least one English ("{name}"@en) designation for ApplicationSchema packages
    ' Check that definition tags have correct structure: "{name}"@{language}, and that there is at least one English ("{name}"@en) definition for ApplicationSchema packages
    ' /krav/taggedValueSpråk	
    ' sub procedure to check if the package has the provided tags with a value with correct structure
    ' @param[in]: thePackage (Package)
    ' Conformity class: UML-applikasjonsskjema

    Sub kravTaggedValueSpråk(thePackage)

        checkTVLanguageAndDesignation(thePackage.Element, "language")
        checkTVLanguageAndDesignation(thePackage.Element, "designation")
        checkTVLanguageAndDesignation(thePackage.Element, "definition")

    End Sub





    Sub checkTVLanguageAndDesignation(theElement, taggedValueName)

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
                                Output("Warning: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & currentTaggedValue.Name & "] has a value """ & currentTaggedValue.Value & """  which is neither ""no"" nor ""en"". [/krav/taggedValueSpråk]")
                                warningCounter += 1
                            End If
                        End If
                        taggedValueLanguageMissing = False
                        Exit For
                    End If
                    If currentTaggedValue.Name = "language" And currentTaggedValue.Value = "" Then
                        Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & currentTaggedValue.Name & "] is missing a value. [/krav/taggedValueSpråk]")
                        errorCounter += 1
                        taggedValueLanguageMissing = False
                        Exit For
                    End If
                Next
                If taggedValueLanguageMissing Then
                    Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] is missing a [language] tag. [/krav/taggedValueSpråk]")
                    errorCounter += 1
                End If
            End If
        End If

        If taggedValueName = "designation" Or taggedValueName = "definition" Then

            If Not theElement Is Nothing And Len(taggedValueName) > 0 Then

                'check if the element has a tagged value with the provided name
                Dim currentExistingTaggedValue1 As EA.TaggedValue
                Dim valueExists
                Dim enDesignation
                Dim checkQuoteMark
                Dim checkAtMark
                Dim taggedValuesCounter1
                valueExists = False
                enDesignation = False
                For taggedValuesCounter1 = 0 To theElement.TaggedValues.Count - 1
                    currentExistingTaggedValue1 = theElement.TaggedValues.GetAt(taggedValuesCounter1)

                    'check if the tagged value exists, and checks if the value starts with " and ends with "@{language}, if not, return an error. 
                    If currentExistingTaggedValue1.Name = taggedValueName Then
                        valueExists = True
                        checkQuoteMark = False
                        checkAtMark = False

                        If Not Len(currentExistingTaggedValue1.Value) = 0 Then

                            If (InStr(currentExistingTaggedValue1.Value, "@en") <> 0) Then
                                enDesignation = True
                            End If

                            If (Mid(currentExistingTaggedValue1.Value, 1, 1) = """") Then
                                checkQuoteMark = True
                            End If
                            If (InStr(currentExistingTaggedValue1.Value, """@") <> 0) Then
                                checkAtMark = True
                            End If

                            If Not (checkAtMark And checkQuoteMark) Then
                                Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & taggedValueName & "] has an invalid value.  Expected value ""{" & taggedValueName & "}""@{language code} [/krav/taggedValueSpråk]")
                                errorCounter += 1
                            End If

                            'Check if the value contains  illegal quotation marks, gives an Warning-message  
                            Dim startContent, endContent, designationContent

                            startContent = InStr(currentExistingTaggedValue1.Value, """")
                            endContent = Len(currentExistingTaggedValue1.Value) - InStr(StrReverse(currentExistingTaggedValue1.Value), """") - 1
                            If endContent < 0 Then endContent = 0
                            designationContent = Mid(currentExistingTaggedValue1.Value, startContent + 1, endContent)

                            If InStr(designationContent, """") Then
                                If logLevel = "Warning" Then
                                    Output("Warning: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & taggedValueName & "] has a value [" & currentExistingTaggedValue1.Value & "] that contains illegal use of quotation marks.")
                                    warningCounter += 1
                                End If
                            End If
                        Else
                            Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & taggedValueName & "] is missing a value. [/krav/taggedValueSpråk]")
                            errorCounter += 1
                        End If
                    End If
                Next
                If UCase(theElement.Stereotype) = UCase("applicationSchema") Then
                    If Not valueExists Then
                        Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] is missing a [" & taggedValueName & "] tag [/krav/taggedValueSpråk]")
                        errorCounter += 1
                    Else
                        If Not enDesignation Then
                            Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [" & taggedValueName & "] is missing a value for English. Expected value ""{English " & taggedValueName & "}""@en [/krav/taggedValueSpråk]")
                            errorCounter += 1
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------


End Class
