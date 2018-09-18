Partial Public Class ModelValidation


    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name: 		PopulatePackageIDList
    'Author: 		Åsmund Tjora
    'Date: 			20170223
    'Purpose: 		Populate the globalPackageIDList variable. 
    'Parameters:	rootPackage  The package to be added to the list and investigated for subpackages
    ' 
    Sub PopulatePackageIDList(rootPackage)
        Dim subPackageList As EA.Collection
        Dim subPackage As EA.Package
        subPackageList = rootPackage.Packages

        globalPackageIDList.Add(rootPackage.PackageID)
        For Each subPackage In subPackageList
            PopulatePackageIDList(subPackage)
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name: 		PopulateClassifierIDList
    'Author: 		Åsmund Tjora
    'Date: 			20170228
    'Purpose: 		Populate the globalListAllClassifierIDsInApplicationSchema variable. 
    'Parameters:	rootPackage  The package to be added to the list and investigated for subpackages

    Sub PopulateClassifierIDList(rootPackage)
        Dim containedElementList As EA.Collection
        Dim containedElement As EA.Element
        Dim subPackageList As EA.Collection
        Dim subPackage As EA.Package
        containedElementList = rootPackage.Elements
        subPackageList = rootPackage.Packages

        For Each containedElement In containedElementList
            globalListAllClassifierIDsInApplicationSchema.Add(containedElement.ElementID)
        Next
        For Each subPackage In subPackageList
            PopulateClassifierIDList(subPackage)
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------

End Class
