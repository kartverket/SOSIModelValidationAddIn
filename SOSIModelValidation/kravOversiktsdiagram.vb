Partial Public Class ModelValidation

    ' sub name kravOversiktsdiagram
    ' Author: Åsmund Tjora
    ' Date:  2018-10-16, Original script code 2017-01-11
    ' Ruleset:  SOSI
    ' Requirement: /krav/oversiktsdiagram
    ' Parameter:  Element (package) to check

    Sub kravOversiktsdiagram(thePackage As EA.Package)
        Dim diagrams As EA.Collection
        diagrams = thePackage.Diagrams
        Dim hoveddiagramCounter = 0
        Dim oversiktsdiagramCounter = 0
        Dim currentDiagram As EA.Diagram

        For Each currentDiagram In diagrams
            If UCase(Mid(currentDiagram.Name, 1, 12)) = "HOVEDDIAGRAM" Then hoveddiagramCounter += 1
            If UCase(Mid(currentDiagram.Name, 1, 16)) = "OVERSIKTSDIAGRAM" Then oversiktsdiagramCounter += 1
        Next

        If ((hoveddiagramCounter > 1) And (oversiktsdiagramCounter = 0)) Then
            Output("Error: Package [ «" & thePackage.StereotypeEx & "» " & thePackage.Name & "] has more than one diagram with names starting with Hoveddiagram, but no diagram with name starting with Oversiktsdiagram [/krav/oversiktsdiagram]")
            errorCounter += 1
        End If
    End Sub

End Class