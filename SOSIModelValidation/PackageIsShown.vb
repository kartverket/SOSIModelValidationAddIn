Partial Public Class ModelValidation


    ' PackageIsShown
    ' returns true if package or any of its superpackages exist in packageDependenciesShownElementIDList
    ' returns false if package or any of its superpackages does not exist in packageDependenciesShownElementIDList
    Function PackageIsShown(Package As EA.Package) As Boolean
        PackageIsShown = False

        If packageDependenciesShownElementIDList.Contains(Package.Element.ElementID) Then
            PackageIsShown = True
        Else
            Dim parentID = Package.ParentID
            If Not parentID = 0 Then
                Dim parentPackage As EA.Package
                parentPackage = theRepository.GetPackageByID(parentID)
                If Not parentPackage.IsModel Then
                    PackageIsShown = PackageIsShown(parentPackage)
                End If
            End If
        End If


    End Function


End Class