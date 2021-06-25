Partial Public Class ModelValidation


    'Sub name:      krav18
    'Author: 		Kent Jonsrud
    'Date: 			2020-08-31 - 2020-09-02
    'Purpose: 		'/krav/18
    'Parameter: 	an element
    'Requirement class:     n/a
    'Conformance class:     Regler for UML-modellering versjon 5.1:2020 - Basisregler

    Sub krav18(theElement)
        'Alle klasser og assosiasjoner skal i minst ett diagram vise alle arvede
        'og alle egne egenskaper, roller, operasjoner og restriksjoner.

        'Requirement 18.
        'All classifiers shall be documented In a “context diagram” where all attributes, operations
        'And all relationships that are navigable from the central classifier are displayed.

        '(Diagrams may be owned by any other package!)
        Dim theDiagram As EA.Diagram
        Dim dname, message, f
        Dim fullyShown = False
        Dim DiagramResponses As New System.Collections.ArrayList
        Dim testNonNavigable = True
        Dim testRestrictions = True
        Dim kw = theElement.Stereotype
        If kw = "" And theElement.Type = "Enumeration" Then
            kw = "enumeration"
        End If

        'for each diagram owned by this application schema
        For i = 0 To diagramList.Count - 1
            dname = diagramList.GetByIndex(i)
            theDiagram = theRepository.GetDiagramByID(diagramList.GetKey(i))

            For j = 0 To diaoList.Count - 1
                'If diaoList.ContainsKey(diagramList.GetKey(i)) Then
                If diaoList.GetByIndex(j) = diagramList.GetKey(i) Then
                    'the diagram object is owned by the current diagram
                    If diaeList.GetByIndex(j) = theElement.ElementID Then
                        'the element is shown by this diagram object
                        message = ""
                        'If UCase(Mid(dname, 1, 12)) = "HOVEDDIAGRAM" Or UCase(Mid(dname, 1, 16)) = "OVERSIKTSDIAGRAM" Then

                        f = fullyShownInDiagram(theElement, theDiagram, testNonNavigable, testRestrictions, message)
                        If f Then
                            fullyShown = True
                        Else
                            DiagramResponses.Add("Diagram " & dname & " is incomplete because" & message)
                        End If
                        'Else
                        'DiagramResponses.Add("Diagram " & dname & " is not named as Hoveddiagram or Oversiktsdiagram " & message)
                        'End If
                    End If
                End If
            Next
        Next 'diagram

        If Not fullyShown Then
            Output("Error: Class [«" & kw & "» " & theElement.Name & "] is not showing all own and inherited properties in one diagram. [/krav/18]")
            'dump list of diagrams with what they are missing
            For i = 0 To DiagramResponses.Count - 1
                Output("      Error reasons: " & DiagramResponses(i))
            Next
            errorCounter += 1
        End If

    End Sub
End Class