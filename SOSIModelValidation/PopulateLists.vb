Partial Public Class ModelValidation


    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name: 		PopulatePackageIDList
    'Author: 		Åsmund Tjora
    'Date: 			20170223
    'Purpose: 		Populate the packageIDList variable. 
    '               The list shall contain all packageIDs of root package and its subpackages
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
    '           The list shall contain all elementIDs of packages that the current package is dependent on

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

    'Sub name:  PopulateExternalReferencedElementIDList
    'Author:    Magnus Karge (original script version), Åsmund Tjora (current version)
    'Date:      20170228 (original script version), 20192301 (current version)
    'Purpose:   Populate the externalReferencedElementIDList
    '           The list shall contain all elementIDs of elements in external packages

    Sub PopulateExternalReferencedElementIDList(thePackage As EA.Package)
        Dim currentElement As EA.Element
        Dim currentAttribute As EA.Attribute
        Dim currentConnector As EA.Connector
        Dim currentPackage As EA.Package

        ' Navigate all elements in the package
        For Each currentElement In thePackage.Elements
            ' Add externally defined types for attributes to list
            For Each currentAttribute In currentElement.Attributes
                If Not currentAttribute.ClassifierID = 0 And Not classifierIDList.Contains(currentAttribute.ClassifierID) Then
                    If Not externalReferencedElementIDList.Contains(currentAttribute.ClassifierID) Then
                        externalReferencedElementIDList.Add(currentAttribute.ClassifierID)
                    End If
                End If
            Next
            ' Add external connected elements to list, ignoring realisiation connections
            For Each currentConnector In currentElement.Connectors
                If currentElement.ElementID = currentConnector.ClientID And Not currentConnector.Type = "Realisation" And Not classifierIDList.Contains(currentConnector.SupplierID) Then
                    If Not externalReferencedElementIDList.Contains(currentConnector.SupplierID) Then
                        externalReferencedElementIDList.Add(currentConnector.SupplierID)
                    End If
                End If
            Next


        Next
        ' Recurse for all subpackages
        For Each currentPackage In thePackage.Packages
            PopulateExternalReferencedElementIDList(currentPackage)
        Next
    End Sub

End Class

