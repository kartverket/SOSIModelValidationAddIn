Partial Public Class ModelValidation

    'Sub name:      requirement12
    ' Author: Magnus Karge, Kent Jonsrud
    ' Date: 20170110, 2019-01-25, 2019-03-12
    ' Purpose:  sub procedure to check if a given dataType element's (element with stereotype DataType or of type DataType) associations are
    '			compositions and the composition is on the correct end (datatypes must only be targets of compositions)
    '
    Sub requirement12(theElement, conn, connEnd)
        Dim elementOnOppositeSide As EA.Element
        elementOnOppositeSide = theRepository.GetElementByID(conn.ClientID)
        If elementOnOppositeSide.Name <> connEnd.End Then
            elementOnOppositeSide = theRepository.GetElementByID(conn.SupplierID)
        End If
        Output("Debug: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ association role [" & connEnd.Role & "] elementOnOppositeSide.Name = " & elementOnOppositeSide.Name & "] [/krav/12]")

        'check if the elementOnOppositeSide has stereotype "dataType" and this side's end is no composition and not elements both sides of the association are datatypes
        If (UCase(elementOnOppositeSide.Stereotype) = UCase("dataType")) And Not (conn.ClientEnd.Aggregation = 2) And conn.Type <> "Dependency" Then
            Output("Error: Class [«" & elementOnOppositeSide.Stereotype & "» " & elementOnOppositeSide.Name & "] has association to class [" & theElement.Name & "] that is not a composition on " & theElement.Name & "-side. [/krav/12]")
            errorCounter += 1
        End If
    End Sub

End Class