Partial Public Class ModelValidation


    '------------------------------------------------------------START-------------------------------------------------------------------------------------------
    'Sub name: 		PopulatePackageIDList
    'Author: 		Åsmund Tjora
    'Date: 			20170223
    'Date:          2021-06-04 added exception for all well known types, Kent Jonsrud
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
    'Date:      20170228 (original script version), 20190123 (current version)
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
                    If Not externalReferencedElementIDList.Contains(currentAttribute.ClassifierID) And Not isWellKnownType(currentAttribute.Type) Then
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

    'Sub name:  PopulatePackageDependenciesShownIDList
    'Author:    Åsmund Tjora
    'Date:      20190312
    'Purpose:   Populate the packageDependenciesShownIDList variable
    '           The list shall contain all IDs of package dependencies shown in the model's package diagrams
    '           Give warning if package diagram contains hidden links

    Sub PopulatePackageDependenciesShownElementIDList(thePackage As EA.Package)
        Dim thePackageElementID
        Dim packageDiagramIDList As New System.Collections.ArrayList
        Dim diagramID
        Dim diagram As EA.Diagram
        Dim linkList As EA.Collection
        Dim diagramLink As EA.DiagramLink
        Dim modelLink As EA.Connector
        Dim supplier As EA.Element
        Dim client As EA.Element

        thePackageElementID = thePackage.Element.ElementID
        PopulatePackageDiagramIDList(thePackage, packageDiagramIDList)

        For Each diagramID In packageDiagramIDList
            diagram = theRepository.GetDiagramByID(diagramID)
            linkList = diagram.DiagramLinks

            For Each diagramLink In linkList
                modelLink = theRepository.GetConnectorByID(diagramLink.ConnectorID)
                If modelLink.Type = "Package" Or modelLink.Type = "Usage" Or modelLink.Type = "Dependency" Then
                    If modelLink.ClientID = thePackageElementID Then
                        packageDependenciesShownElementIDList.Add(modelLink.SupplierID)
                        If diagramLink.IsHidden And logLevel = "Warning" Then
                            supplier = theRepository.GetElementByID(modelLink.SupplierID)
                            client = theRepository.GetElementByID(modelLink.ClientID)
                            Output("Warning: Diagram [" & diagram.Name & "] contains hidden dependency link between elements " & supplier.Name & " and " & client.Name & ".")
                            warningCounter += 1
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    ' Find all package diagrams.  Recurse for all subpackages.
    ' Used by PopulatePackageDiagramIDList sub.
    Sub PopulatePackageDiagramIDList(thePackage As EA.Package, ByRef packageDiagramIDList As System.Collections.ArrayList)
        Dim diagramList As EA.Collection
        diagramList = thePackage.Diagrams
        Dim subPackageList As EA.Collection
        subPackageList = thePackage.Packages
        Dim diagram As EA.Diagram
        Dim subPackage As EA.Package
        For Each diagram In diagramList
            packageDiagramIDList.Add(diagram.DiagramID)
        Next
        For Each subPackage In subPackageList
            PopulatePackageDiagramIDList(subPackage, packageDiagramIDList)
        Next
    End Sub

End Class

