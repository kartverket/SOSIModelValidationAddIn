Partial Public Class ModelValidation

    'Sub name:      requirement11
    ' Author: Magnus Karge, Kent Jonsrud
    ' Date: 20170110, 2019-01-25, 2019-03-12
    ' Purpose:  sub procedure to check if the given association has role names on navigable ends
    '			(navigable ends shall have role names)
    '
    Sub requirement11(theElement, conn, connEnd)
        Output("Debug: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ association role [" & connEnd.Role & "] [/krav/11]")
        If connEnd.Navigable = "Navigable" And connEnd.Role = "" And conn.Type <> "Dependency" Then
            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has no role name on navigable end close to class " & connEnd.End & ". [/krav/11]")
            '? Output("Error: Association between class [«" & theElement.Stereotype & "» " & theElement.Name & "] and class [«" & "elementOnOppositeSide.Stereotype" & "» " & "elementOnOppositeSide.Name" & "] has no role name on navigable end on " & theElement.Name & "-side. [/krav/11]")
            errorCounter += 1
        End If
    End Sub

End Class