Partial Public Class ModelValidation

    'Sub name:      requirement11
    ' Author: Magnus Karge, Kent Jonsrud
    ' Date: 20170110, 2019-01-25, 2019-03-12, 2020-07-08
    ' Purpose:  sub procedure to check if the given association has role names on navigable ends
    '			(navigable ends shall have role names)
    '
    Sub requirement11(theElement, conn, connEnd)
        If connEnd.Navigable = "Navigable" And connEnd.Role = "" And conn.Type <> "Dependency" Then
            Dim classAtNavigableEndWithoutRoleName As EA.Element
            If connEnd.Equals(conn.ClientEnd) Then
                classAtNavigableEndWithoutRoleName = theRepository.GetElementByID(conn.ClientID)
            Else
                classAtNavigableEndWithoutRoleName = theRepository.GetElementByID(conn.SupplierID)
            End If
            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has no role name on navigable association end close to class " & "[«" & classAtNavigableEndWithoutRoleName.Stereotype & "» " & classAtNavigableEndWithoutRoleName.Name & "]. [/krav/11]")
            '? Output("Error: Association between class [«" & theElement.Stereotype & "» " & theElement.Name & "] and class [«" & "elementOnOppositeSide.Stereotype" & "» " & "elementOnOppositeSide.Name" & "] has no role name on navigable end on " & theElement.Name & "-side. [/krav/11]")
            errorCounter += 1
            End If
    End Sub

End Class