Partial Public Class ModelValidation

    'Sub name:      kravFlerspråklighetEelement
    'Author: 		Kent Jonsrud (?)
    'Date: 			2019?
    'Date: 			2021-06-29 not report on empty tagged values
    'Purpose: 		/krav/flerspråklighet/element



    Sub kravFlerspråklighetElement(theElement As EA.Element)
        Call testMultilingualElement(theElement, "description")
        Call testMultilingualElement(theElement, "designation")
        Call testMultilingualElement(theElement, "definition")
    End Sub

    Sub kravFlerspråklighetElement(theElement As EA.Attribute)
        Call testMultilingualAttribute(theElement, "description")
        Call testMultilingualAttribute(theElement, "designation")
        Call testMultilingualAttribute(theElement, "definition")
    End Sub

    Sub kravFlerspråklighetElement(theElement As EA.Method)
        Call testMultilingualOperation(theElement, "description")
        Call testMultilingualOperation(theElement, "designation")
        Call testMultilingualOperation(theElement, "definition")
    End Sub

    Sub kravFlerspråklighetElement(theElement As EA.ConnectorEnd)
        Call testMultilingualConnectorEnd(theElement, "description")
        Call testMultilingualConnectorEnd(theElement, "designation")
        Call testMultilingualConnectorEnd(theElement, "definition")
    End Sub

    Sub testMultilingualElement(theElement As EA.Element, taggedValueName As String)
        Dim currentElement As EA.Element
        Dim currentTaggedValue As EA.TaggedValue
        If Not theElement Is Nothing And Len(taggedValueName) > 0 Then
            For Each currentTaggedValue In theElement.TaggedValues
                If currentTaggedValue.Name = taggedValueName And Len(currentTaggedValue.Value) > 0 Then
                    If Not (InStr(currentTaggedValue.Value, """@") >= 2 And InStr(currentTaggedValue.Value, """") = 1) Then
                        currentElement = theElement
                        If ruleSet = "SOSI" Then
                            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] tag [" + currentTaggedValue.Name + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [/krav/flerspråklighet/element]")
                            errorCounter += 1
                        ElseIf ruleSet = "19109" Then
                            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] tag [" + currentTaggedValue.Name + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [ISO19109:2015 /req/multi-lingual/feature]")
                            errorCounter += 1
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Sub testMultilingualAttribute(theAttribute As EA.Attribute, taggedValueName As String)
        Dim currentElement As EA.Element
        Dim currentTaggedValue As EA.AttributeTag
        If Not theAttribute Is Nothing And Len(taggedValueName) > 0 Then
            For Each currentTaggedValue In theAttribute.TaggedValues
                If currentTaggedValue.Name = taggedValueName And Len(currentTaggedValue.Value) > 0 Then
                    If Not (InStr(currentTaggedValue.Value, """@") >= 2 And InStr(currentTaggedValue.Value, """") = 1) Then
                        currentElement = theRepository.GetElementByID(theAttribute.ParentID)
                        If ruleSet = "SOSI" Then
                            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] attribute [" + theAttribute.Name + "] tag [" + currentTaggedValue.Name + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [/krav/flerspråklighet/element]")
                            errorCounter += 1
                        ElseIf ruleSet = "19109" Then
                            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] attribute [" + theAttribute.Name + "] tag [" + currentTaggedValue.Name + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [ISO19109:2015 /req/multi-lingual/feature]")
                            errorCounter += 1
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Sub testMultilingualOperation(theOperation As EA.Method, taggedValueName As String)
        Dim currentElement As EA.Element
        Dim currentTaggedValue As EA.MethodTag
        If Not theOperation Is Nothing And Len(taggedValueName) > 0 Then
            For Each currentTaggedValue In theOperation.TaggedValues
                If currentTaggedValue.Name = taggedValueName And Len(currentTaggedValue.Value) > 0 Then
                    If Not (InStr(currentTaggedValue.Value, """@") >= 2 And InStr(currentTaggedValue.Value, """") = 1) Then
                        currentElement = theRepository.GetElementByID(theOperation.ParentID)
                        If ruleSet = "SOSI" Then
                            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] operation [" + theOperation.Name + "] tag [" + currentTaggedValue.Name + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [/krav/flerspråklighet/element]")
                            errorCounter += 1
                        ElseIf ruleSet = "19109" Then
                            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] operation [" + theOperation.Name + "] tag [" + currentTaggedValue.Name + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [ISO19109:2015 /req/multi-lingual/feature]")
                            errorCounter += 1
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Sub testMultilingualConnectorEnd(theConnectorEnd As EA.ConnectorEnd, taggedValueName As String)
        Dim currentTaggedValue As EA.RoleTag
        If Not theConnectorEnd Is Nothing And Len(taggedValueName) > 0 Then
            For Each currentTaggedValue In theConnectorEnd.TaggedValues
                If currentTaggedValue.Tag = taggedValueName And Len(currentTaggedValue.Value) > 0 Then
                    If Not (InStr(currentTaggedValue.Value, """@") >= 2 And InStr(currentTaggedValue.Value, """") = 1) Then
                        If ruleSet = "SOSI" Then
                            Output("Error: Role [" + theConnectorEnd.Role + "] tag [" + currentTaggedValue.Tag + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [/krav/flerspråklighet/element]")
                            errorCounter += 1
                        ElseIf ruleSet = "19109" Then
                            Output("Error: Role [" + theConnectorEnd.Role + "] tag [" + currentTaggedValue.Tag + "] has a value [" + currentTaggedValue.Value + "] with wrong structure.  Expected structure: ""{Name}""@{language}. [ISO19109:2015 /req/multi-lingual/feature]")
                            errorCounter += 1
                        End If
                    End If
                End If
            Next
        End If
    End Sub


End Class