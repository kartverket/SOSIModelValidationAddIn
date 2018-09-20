Partial Public Class ModelValidation

    ' sub name kravEnkelArv
    ' Author: Åsmund Tjora, based on modelvalidation script code (sub findMultipleInheritance) by Sara Henriksen
    ' Date:  2018-09-19
    ' Ruleset:  SOSI
    ' Requirement: /krav/enkelArv
    ' Parameter:  Element (class) to check

    Sub kravEnkelArv(currentElement As EA.Element)

        Dim connectors As EA.Collection
        connectors = currentElement.Connectors
        Dim currentConnector As EA.Connector
        Dim numberOfSuperClasses = 0

        For Each currentConnector In connectors
            If currentConnector.Type = "Generalization" And currentConnector.ClientID = currentElement.ElementID Then
                numberOfSuperClasses += 1
            End If
        Next

        If numberOfSuperClasses > 1 Then
            Output("Error: Class [«" + currentElement.Stereotype + "» " + currentElement.Name + "] has multiple inheritance. [/krav/enkelArv]")
            errorCounter += 1
        End If

    End Sub

End Class