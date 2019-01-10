Partial Public Class ModelValidation


    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name: 		PopulatePackageIDList
    'Author: 		Åsmund Tjora
    'Date: 			20170223
    'Purpose: 		Populate the packageIDList variable. 
    'Parameters:	rootPackage  The package to be added to the list and investigated for subpackages
    ' 
    Sub PopulatePackageIDList(rootPackage)
        Dim subPackageList As EA.Collection
        Dim subPackage As EA.Package
        subPackageList = rootPackage.Packages

        packageIDList.Add(rootPackage.PackageID)
        For Each subPackage In subPackageList
            PopulatePackageIDList(subPackage)
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------

    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name: 		PopulateClassifierIDList
    'Author: 		Åsmund Tjora
    'Date: 			20170228
    'Purpose: 		Populate the classifierIDList variable. 
    'Parameters:	rootPackage  The package to be added to the list and investigated for subpackages

    Sub PopulateClassifierIDList(rootPackage)
        Dim containedElementList As EA.Collection
        Dim containedElement As EA.Element
        Dim subPackageList As EA.Collection
        Dim subPackage As EA.Package
        containedElementList = rootPackage.Elements
        subPackageList = rootPackage.Packages

        For Each containedElement In containedElementList
            classifierIDList.Add(containedElement.ElementID)
        Next
        For Each subPackage In subPackageList
            PopulateClassifierIDList(subPackage)
        Next
    End Sub
    '-------------------------------------------------------------END--------------------------------------------------------------------------------------------


    'Sub name:  PopulatePackageDependenciesElementIDList
    'Author:    Åsmund Tjora
    'Date:      20190109 (date of original sub in script is unknown)
    'Purpose:   Populate the packageDependenciesElementIDList
    Sub PopulatePackageDependenciesElementIDList(thePackageElement)
        Dim connectorList As EA.Collection
        Dim packageConnector As EA.Connector
        Dim dependee As EA.Element
        connectorList = thePackageElement.Connectors
        For Each packageConnector In connectorList
            If packageConnector.Type = "Usage" Or packageConnector.Type = "Package" Or packageConnector.Type = "Dependency" Then
                If thePackageElement.ElementID = packageConnector.ClientID Then
                    dependee = theRepository.GetElementByID(packageConnector.SupplierID)
                    packageDependenciesElementIDList.Add(dependee.ElementID)
                End If
            End If
        Next
    End Sub
End Class
