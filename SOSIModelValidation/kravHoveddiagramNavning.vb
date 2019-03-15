Partial Public Class ModelValidation
    'Sub name:      kravHoveddiagramNavning
    'Author: 		Sara Henriksen (adjusted to VB by Magnus Karge)
    'Date: 			20190314
    'Ruleset:       SOSI
    'Purpose: 		generate error message if no diagrams named 'Hoveddiagram' have been found


    Sub kravHoveddiagramNavning(startPackage As EA.Package)
        If Not foundHoveddiagram Then
            Output("Error: Neither package [" & startPackage.Name & "] nor any of it's subpackages has a diagram with a name starting with 'Hoveddiagram' [/krav/hoveddiagram/navning]")
            errorCounter += 1

        End If
    End Sub


End Class

