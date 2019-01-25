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
    'Author: 		Åsmund Tjora, Magnus Karge
    'Date: 			20170228
    'Purpose: 		To populate the following list variables: classifierIDList, featureTypeNamesList, featureTypeElementIDsList
    'Parameters:	package  The package to be examined for existing classifiers

    Sub PopulateClassifierLists(package)
        Dim containedElementList As EA.Collection
        Dim containedElement As EA.Element
        Dim subPackageList As EA.Collection
        Dim subPackage As EA.Package
        containedElementList = package.Elements
        subPackageList = package.Packages

        For Each containedElement In containedElementList
            classifierIDList.Add(containedElement.ElementID)
            'add name and elementID of featureType (elements with stereotype <<featureType>>) to the related array variables in order to check if the names are unique
            If UCase(containedElement.Stereotype) = "FEATURETYPE" Then
                featureTypeNamesList.Add(containedElement.Name)
                featureTypeElementIDsList.Add(containedElement.ElementID)
            End If
        Next
        For Each subPackage In subPackageList
            PopulateClassifierLists(subPackage)
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
