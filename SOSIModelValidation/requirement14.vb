Partial Public Class ModelValidation

    ' -----------------------------------------------------------START-------------------------------------------------------------------------------------------
    ' Sub Name: requirement14 - inherit from same stereotype
    ' Author:   Tore Johnsen
    ' Date:     2016-08-22
    ' Purpose:  Checks that there is no inheritance between classes with unequal stereotypes.
    '		    /krav/14
    ' @param[in]: currentElement

    Sub requirement14(currentElement)

        Dim connectors As EA.Collection
        connectors = currentElement.Connectors
        Dim connectorsCounter

        For connectorsCounter = 0 To connectors.Count - 1
            Dim currentConnector As EA.Connector
            currentConnector = connectors.GetAt(connectorsCounter)
            Dim targetElementID
            targetElementID = currentConnector.SupplierID
            Dim elementOnOppositeSide As EA.Element

            If currentConnector.Type = "Generalization" Then
                elementOnOppositeSide = theRepository.GetElementByID(targetElementID)

                If UCase(elementOnOppositeSide.Stereotype) <> UCase(currentElement.Stereotype) Then
                    Output("Error: Class [" & elementOnOppositeSide.Name & "] has a stereotype that is not the same as the stereotype of [" & currentElement.Name & "]. A class can only inherit from a class with the same stereotype. [/krav/14]")
                    errorCounter = errorCounter + 1
                End If
            End If
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------

End Class
