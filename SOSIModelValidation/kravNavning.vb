Partial Public Class ModelValidation
    'Sub name:      kravNavning
    'Author: 		Magnus Karge.
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

    Sub kravNavning(theConnector As EA.Connector)
        Call checkNameOfConnector(theConnector, "krav/navning")
    End Sub

    Sub kravNavning(theOperation As EA.Method)
        Call checkNameOfOperation(theOperation, "krav/navning")
    End Sub

    Sub kravNavning(theConnector As EA.Connector, owningElement As EA.Element)
        Dim targetEndName
        targetEndName = theConnector.SupplierEnd.Role
        Dim targetElementID
        targetElementID = theConnector.SupplierID
        Dim sourceEndName
        sourceEndName = theConnector.ClientEnd.Role
        Dim elementOnOppositeSide As EA.Element
        elementOnOppositeSide = theRepository.GetElementByID(targetElementID)
        Call checkNameOfRole(owningElement, sourceEndName, targetEndName, elementOnOppositeSide, "krav/navning")
    End Sub


End Class

