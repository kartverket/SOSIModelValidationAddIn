Partial Public Class ModelValidation
    'Sub name:      kravNavning
    'Author: 		Magnus Karge
    'Date: 			20190109
    'Ruleset:       SOSI
    'Purpose: 		several sub procedures to check if an element's name is written correctly [krav/navning]:
    '				Here only the redirection to subs in the checkName file happens where the logic is implemented.
    'Calls: 		checkNameOf* in file checkName.vb



    Sub kravNavning(theElement As EA.Package)
        Call checkNameOfPackage(theElement, "krav/navning")
    End Sub

    Sub kravNavning(theElement As EA.Element)
        Call checkNameOfClass(theElement, "krav/navning")
    End Sub

    Sub kravNavning(theElement As EA.Attribute)
        Call checkNameOfAttribute(theElement, "krav/navning")
    End Sub

    Sub kravNavning(theElement As EA.Connector)
        Call checkNameOfConnector(theElement, "krav/navning")
    End Sub


End Class

