Partial Public Class ModelValidation

    'Sub name:      requirement10
    ' Author: Magnus Karge, Kent Jonsrud
    ' Date: 20170110, 2019-01-25, 2019-03-12
    ' Purpose:  sub procedure to check if the given association properties fulfill the requirements regarding
    '			multiplicity on navigable ends (navigable ends shall have multiplicity)

    Sub requirement10(theElement, conn, connEnd)
        If connEnd.Navigable = "Navigable" And connEnd.Cardinality = "" And conn.Type <> "Dependency" Then
            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ association role [" & connEnd.Role & "] on navigable end has no multiplicity. [/krav/10]")
            '? Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ association role [" & connEnd.Name & "] in association [" & conn.Name & "] has no multiplicity. [/krav/10]")
            errorCounter += 1
        End If
        'If connEnd.targetEndNavigable = "Navigable" And connEnd.targetEndCardinality = "" And conn.Type <> "Dependency" Then
        'Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] \ association role [" & connEnd.Role & "] has no multiplicity. [/krav/10]")
        'errorCounter += 1
        'End If
    End Sub

End Class