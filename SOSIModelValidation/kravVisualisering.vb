Partial Public Class ModelValidation

    'Sub name:      kravVisualisering
    'Author: 		Kent Jonsrud
    'Date: 			2020-08-31 - 2020-09-02
    'Purpose: 		'/krav/visualisering
    'Parameter: 	an element or a package
    'Requirement class:     n/a
    'Conformance class:     Regler for UML-modellering versjon 5.1:2020 - Basisregler



    Sub kravVisualisering(theThing As EA.Element)
        Call kravVisualiseringOnElement(theThing)
    End Sub
    Sub kravVisualisering(theThing As EA.Package)
        Call kravVisualiseringOnPackage(theThing)
    End Sub
    Sub kravVisualiseringOnElement(theElement)
        'Alle klasser, egenskaper, assosiasjoner, assosiasjonsroller, operasjoner og restriksjoner 
        'i et applikasjonsskjema skal vises i minst ett av de følgende diagrammene:
        '- hoveddiagram
        '- oversiktsdiagram
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
                If diaoList.GetByIndex(j) = diagramList.GetKey(i) Then
                    'the diagram object is owned by the current diagram
                    If diaeList.GetByIndex(j) = theElement.ElementID Then
                        'the element is shown by this diagram object
                        message = ""

                        f = fullyShownInDiagram(theElement, theDiagram, testNonNavigable, testRestrictions, message)
                        If f Then
                            fullyShown = True
                        Else
                            DiagramResponses.Add("Diagram " & dname & " is incomplete because" & message)
                        End If
                        '                   Else
                        '                   DiagramResponses.Add("Diagram " & dname & " is not named as Hoveddiagram or Oversiktsdiagram " & message)
                        '               End If
                    End If
                End If
            Next
        Next 'diagram

        If Not fullyShown Then
            Output("Error: Class [«" & kw & "» " & theElement.Name & "] is not showing all its own and inherited properties in a diagram named as Hoveddiagram or Oversiktsdiagram. [/krav/visualisering]")
            'dump list of diagrams with what they are missing
            For i = 0 To DiagramResponses.Count - 1
                Output("      Error reasons: " & DiagramResponses(i))
            Next
            errorCounter += 1
        End If

    End Sub

    Sub kravVisualiseringOnPackage(thePackage)
        'Avhengigheter mellom pakker skal vises i minst ett: 
        '    - pakkeavhengighetsdiagram
        'Dersom en realiserer pakker eller klasser i andre standarder skal dette vises i form av: 
        '   - pakkerealiseringsdiagram
        '  - realiseringsdiagram

        'Finn pakkeavhengighetsrelasjon, test om denne vises i et diagram

        'Finn pakkerealiseringsrelasjon, test om denne vises i et diagram
        'Finn klasserealiseringsrelasjon, test om denne vises i et diagram
        '
    End Sub
End Class