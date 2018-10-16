Partial Public Class ModelValidation

    ' Script Name: checkValueOfTVVersion
    ' Author: Sara Henriksen, Tore Johnsen
    ' Date: 25.07.16 - Enhanced 23.08.18
    ' Purpose: To check if the value of the version-tag (tagged values) for an ApplicationSchema-package is empty or not. 
    ' req/uml/packaging
    ' sub procedure to check if the tagged value with the provided name exist in the ApplicationSchema, and if the value is emty it returns an Error-message. 
    ' @param[in]: theElement (Element Class) and TaggedValueName (String) 
    Sub reqUmlPackaging(theElement, taggedValueName)

        If UCase(theElement.stereotype) = UCase("applicationSchema") Then

            If Not theElement Is Nothing And Len(taggedValueName) > 0 Then

                'check if the element has a tagged value with the provided name
                Dim taggedValueVersionMissing
                taggedValueVersionMissing = True
                Dim taggedValueVersionCase
                taggedValueVersionCase = False

                Dim currentExistingTaggedValue As EA.TaggedValue
                Dim taggedValuesCounter
                For taggedValuesCounter = 0 To theElement.TaggedValues.Count - 1
                    currentExistingTaggedValue = theElement.TaggedValues.GetAt(taggedValuesCounter)

                    'check if the taggedvalue exists, and if so, checks if the value is empty or not. An empty value will give an error-message. 
                    If currentExistingTaggedValue.Name = taggedValueName Then
                        'remove spaces before and after a string, if the value only contains blanks  the value is empty
                        currentExistingTaggedValue.Value = Trim(currentExistingTaggedValue.Value)
                        If Len(currentExistingTaggedValue.Value) = 0 Then
                            Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] \ tag [version] is missing a value. [req/uml/packaging]")
                            errorCounter = errorCounter + 1
                            taggedValueVersionMissing = False
                        Else
                            taggedValueVersionMissing = False
                            'Session.Output("[" &theElement.Name& "] has version tag:  " &currentExistingTaggedValue.Value)
                        End If
                    End If
                Next
                'if tagged value version lacks for the package, return an error 
                If taggedValueVersionMissing Then
                    Output("Error: Package [«" & theElement.Stereotype & "» " & theElement.Name & "] is missing a [version] tag. [req/uml/packaging]")
                    errorCounter = errorCounter + 1
                End If
            End If
        End If
    End Sub

End Class
