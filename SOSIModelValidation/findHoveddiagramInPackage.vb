Partial Public Class ModelValidation
    'Sub name:      findHoveddiagramInPackage
    'Author: 		Magnus Karge, Sara Henriksen
    'Date: 			20190314
    'Ruleset:       SOSI
    'Purpose: to check if the applicationSchema-package has more than one diagram with a name starting with "Hoveddiagram", if so, returns an error if the 
    ' name of the Diagram is nothing more than "Hoveddiagram". Returns at most one error for the start package and its subpackages, with the number of wrong-named diagrams for the package.
    ' /krav/hoveddiagram/detaljering/navning 
    ' sub procedure to check if the given package and its subpackages has more than one diagram with the provided name, if so, return and error if 
    ' the name of the Diagram is nothing more than "Hoveddiagram".
    '@param[in]: package (EA.package) The package potentially containing diagrams with the provided name 'Hoveddiagram'


    Sub findHoveddiagramInPackage(thePackage As EA.Package)
        Dim diagrams As EA.Collection
        diagrams = thePackage.Diagrams


        'find all diagrams in the package 
        Dim i
        For i = 0 To diagrams.Count - 1
            Dim currentDiagram As EA.Diagram
            currentDiagram = diagrams.GetAt(i)

            'if the package got less than one diagram with a name starting with "Hoveddiagram", then return an error 
            If UCase(Mid((currentDiagram.Name), 1, 12)) = "HOVEDDIAGRAM" And Len(currentDiagram.Name) = 12 Then
                numberOfHoveddiagram += 1

            End If

            'count diagrams named 'Hovediagram'
            If UCase(Mid((currentDiagram.Name), 1, 12)) = "HOVEDDIAGRAM" And Len(currentDiagram.Name) > 12 Then
                numberOfHoveddiagramWithAdditionalInformationInTheName += 1

            End If
        Next
    End Sub


End Class

