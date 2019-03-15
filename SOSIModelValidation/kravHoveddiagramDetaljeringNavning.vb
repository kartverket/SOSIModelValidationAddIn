Partial Public Class ModelValidation
    'Sub name:      kravHoveddiagramDetaljeringNavning
    'Author: 		Sara Henriksen (adjusted to VB by Magnus Karge)
    'Date: 			20190314
    'Ruleset:       SOSI
    'Purpose: 		generate error message if no diagrams named 'Hoveddiagram' have been found




    Sub kravHoveddiagramDetaljeringNavning(startPackage As EA.Package)
        'error-message for /krav/hoveddiagram/detaljering/navning (sub: FindHoveddiagramsInAS)
        'if the applicationSchema package got more than one diagram named "Hoveddiagram", then return an error 
        If numberOfHoveddiagram > 1 Or (numberOfHoveddiagram = 1 And numberOfHoveddiagramWithAdditionalInformationInTheName > 0) Then
            Dim sumOfHoveddiagram
            sumOfHoveddiagram = numberOfHoveddiagram + numberOfHoveddiagramWithAdditionalInformationInTheName
            Output("Error: Package [" & startPackage.Name & "] (or any of its subpackages) has " & sumOfHoveddiagram & " diagrams named 'Hoveddiagram' and " & numberOfHoveddiagram & " of them named exactly 'Hoveddiagram'. When there are multiple diagrams of that type additional information is expected in all of the diagram names. [/krav/hoveddiagram/detaljering/navning]")
            errorCounter += 1

        End If
    End Sub


End Class

