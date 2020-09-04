Partial Public Class ModelValidation

    'sub InitTreeView
    'initialize TreeView to contain packages, classes, attributes, connectors
    Sub InitTreeView(rootPackage As EA.Package)
        Dim thisNode As System.Windows.Forms.TreeNode
        Dim key As String
        Dim name As String

        key = rootPackage.Element.ElementGUID
        If rootPackage.Element.Stereotype = "" Then
            name = "Package " + rootPackage.Name
        Else
            name = "Package «" + rootPackage.Element.Stereotype + "» " + rootPackage.Name
        End If


        validationWindow.TreeView.BeginUpdate()
        validationWindow.TreeView.Nodes.Clear()

        thisNode = validationWindow.TreeView.Nodes.Add(key, name)
        TreeViewPackageAdd(rootPackage, thisNode, True)
        validationWindow.TreeView.EndUpdate()
    End Sub


    Sub TreeViewPackageAdd(package As EA.Package, parentNode As System.Windows.Forms.TreeNode, isRoot As Boolean)
        Dim subPackageList As EA.Collection
        Dim subPackage As EA.Package
        Dim thisNode As System.Windows.Forms.TreeNode
        subPackageList = package.Packages

        Dim key As String
        Dim name As String

        If isRoot Then
            'skip adding data for root package
            thisNode = parentNode
        Else
            key = package.Element.ElementGUID
            If package.Element.Stereotype = "" Then
                name = "Package " + package.Name
            Else
                name = "Package «" + package.Element.Stereotype + "» " + package.Name
            End If

            thisNode = parentNode.Nodes.Add(key, name)

        End If

        For Each subPackage In subPackageList
            TreeViewPackageAdd(subPackage, thisNode, False)
        Next

        Dim elementList As EA.Collection
        Dim element As EA.Element
        elementList = package.Elements

        For Each element In elementList
            TreeViewClassAdd(element, thisNode)
        Next


    End Sub

    Sub TreeViewClassAdd(element As EA.Element, parentNode As System.Windows.Forms.TreeNode)
        Dim attributeList As EA.Collection
        Dim attribute As EA.Attribute
        Dim operationList As EA.Collection
        Dim operation As EA.Method

        Dim thisNode As System.Windows.Forms.TreeNode
        Dim key
        Dim name

        key = element.ElementGUID
        If element.Stereotype = "" Then
            name = "Class " + element.Name
        Else
            name = "Class «" + element.Stereotype + "» " + element.Name
        End If

        thisNode = parentNode.Nodes.Add(key, name)

        attributeList = element.Attributes
        For Each attribute In attributeList
            key = attribute.AttributeGUID
            If attribute.Type = "" Then
                name = "Attribute " + attribute.Name
            Else
                name = "Attribute " + attribute.Name + " : " + attribute.Type
            End If
            thisNode.Nodes.Add(key, name)
        Next

        operationList = element.Methods
        For Each operation In operationList
            key = operation.MethodGUID
            name = "Operation " + operation.Name + "(" + operation.Parameters.ToString() + ")"
            thisNode.Nodes.Add(key, name)
        Next

    End Sub



End Class