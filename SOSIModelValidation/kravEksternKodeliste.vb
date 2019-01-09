Partial Public Class ModelValidation

    'Sub Name: kravEksternKodeliste
    'Author:   Sara Henriksen, Kent Jonsrud
    'Date:     15.08.16, 2019-01-08
    'Purpose:  check codeList for 'asDictionary' tag with value 'true', if found, check if tag codeList exist is not empty
    'Requirement class: /krav/eksternKodeliste - Eksterne kodelister (med tagged value asDictionary = true) skal ha en tagged value codeList med verdi som er en http-URI som identifiserer hvor man kan få tilgang til kodene.
    'Conformance class: SOSI regler for UML-modellring 5.0 part nnn

    Sub kravEksternKodeliste(theElement)

        If getTaggedValueOnElement(theElement, "asDictionary") = "true" Then
            Dim httpURIvalue
            httpURIvalue = getTaggedValueOnElement(theElement, "codeList")
            If LCase(Mid(httpURIvalue, 1, 7)) = "http://" Then
                If LCase(Mid(httpURIvalue, Len(httpURIvalue) - 3, Len(httpURIvalue))) = ".xml" Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has tagged value asDictionary = true and a httpURI in tagged value codeList, but the URI [" & httpURIvalue & "] should not contain the file extension (.xml). [/krav/eksternKodeliste]")
                    errorCounter = errorCounter + 1
                End If
                ' TBD: test whether the URI when used as a URL will produce a respons containing a document in a known codelist format
            Else
                If logLevel = "Warning" Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has tagged value asDictionary = true, but [" & httpURIvalue & "] is not a valid http-URI in tagged value codeList. [/krav/eksternKodeliste]")
                    warningCounter = warningCounter + 1
                End If

            End If
        End If
    End Sub

    Function getTaggedValueOnElement(theElement, taggedValueName)
        'iterate tagged Values and return found value, warning on multiple tags with same name (and same/different value).
        getTaggedValueOnElement = ""
        Dim value = ""
        Dim taggedValuesCounter
        Dim currentExistingTaggedValue As EA.TaggedValue
        For taggedValuesCounter = 0 To theElement.TaggedValues.Count - 1
            currentExistingTaggedValue = theElement.TaggedValues.GetAt(taggedValuesCounter)
            'check if the tagged value exists
            If currentExistingTaggedValue.Name = taggedValueName Then
                If value <> "" Then
                    If logLevel = "Warning" Then
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has more than one tagged value named [" & taggedValueName & "]")
                        warningCounter = warningCounter + 1
                    End If
                End If
                value = currentExistingTaggedValue.Value
            End If
        Next
        getTaggedValueOnElement = value
    End Function

End Class
