Partial Public Class ModelValidation
    'Sub name:      checkPackageForHoveddiagram
    ' Author: Sara Henriksen
    ' Date: 03.08.16
    ' Purpose: Check if an application-schema has less than one diagram named "Hoveddiagram", if so, return an error
    ' /krav/hoveddiagram/navning abc
    'sub procedure to check if the given package got one or more diagrams with a name starting with "Hoveddiagram", if not, returns an error 
    '@param[in]: package (EA.package) The package containing diagrams potentially with one or more names without "Hoveddiagram".

    Sub checkPackageForHoveddiagram(thePackage As EA.Package)

        Dim diagrams As EA.Collection
        diagrams = thePackage.Diagrams
        'check all digrams in the package 
        Dim i
        For i = 0 To diagrams.Count - 1
            Dim currentDiagram As EA.Diagram
            currentDiagram = diagrams.GetAt(i)
            'set foundHoveddiagram true if any diagrams have been found with a name starting with "Hoveddiagram"
            If Mid((currentDiagram.Name), 1, 12) = "Hoveddiagram" Then
                foundHoveddiagram = True
            End If
        Next
    End Sub

End Class

