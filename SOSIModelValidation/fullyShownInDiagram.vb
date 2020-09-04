Partial Public Class ModelValidation

    'Function name:         fullyShownInDiagram
    'Author: 		        Kent Jonsrud
    'Date: 			        2020-09-02
    'Purpose: 		        basic functions for testing if a class is shown fully in a diagram
    'Parameter: 	        an element, a diagram, returns a message and a true/false
    'Requirement class:     n/a
    'Conformance class:     n/a


    Function fullyShownInDiagram(theElement, theDiagram, testNonNavigable, testRestrictions, ByRef message)
        Dim conn As EA.Connector
        Dim supertype As EA.Element
        fullyShownInDiagram = True

        If elementShownInDiagram(theElement, theDiagram) Then
            For Each conn In theElement.Connectors
                If conn.Type = "Generalization" Then
                    If theElement.ElementID = conn.ClientID Then
                        supertype = theRepository.GetElementByID(conn.SupplierID)
                        message = message & " parent class:" & supertype.Name
                        If Not fullyShownInDiagram(supertype, theDiagram, testNonNavigable, testRestrictions, message) Then
                            fullyShownInDiagram = False
                        End If
                    End If
                End If
            Next
            If InStr(1, theDiagram.ExtendedStyle, "UseAlias=1") <> 0 And theElement.Alias <> "" Then
                message = message & " diagram uses Alias name, real name is " & theElement.Name
                fullyShownInDiagram = False
            End If
            If Not showingAttributes(theElement, theDiagram, message) Then
                fullyShownInDiagram = False
            Else
                If Not showingRoles(theElement, theDiagram, message) Then
                    fullyShownInDiagram = False
                Else
                    If Not showingOperations(theElement, theDiagram, message) Then
                        fullyShownInDiagram = False
                    Else
                        If testRestrictions And Not showingConstraints(theElement, theDiagram, message) Then
                            fullyShownInDiagram = False
                        Else
                            ' all is good here so far
                        End If
                    End If
                End If
            End If

        Else
            message = message & " -- class:" & theElement.Name & " is not shown in diagram " & theDiagram.name & " -- "
        End If
    End Function
    Function showingAttributes(theElement, theDiagram, ByRef message)
        showingAttributes = True
        If theElement.Attributes.Count <> 0 And InStr(1, theDiagram.ExtendedStyle, "HideAtts=1") <> 0 Then
            message = message & " diagram has turned off attribute visibility"
            showingAttributes = False
        Else
            'custom attribute visibility?
            ' TBD
        End If

    End Function
    Function showingRoles(theElement, theDiagram, ByRef message)
        showingRoles = True

        If theElement.Connectors.Count <> 0 And InStr(1, theDiagram.ExtendedStyle, "HideRel=1") <> 0 Then
            message = message & " diagram has turned off relationship visibility"
            showingRoles = False
        Else
            ' find connectors and see whether the roles (oposite of the class) are shown in this diagram
            ' TBD
        End If

    End Function
    Function showingOperations(theElement, theDiagram, ByRef message)
        showingOperations = True

        If theElement.Methods.Count <> 0 And InStr(1, theDiagram.ExtendedStyle, "HideOps=1") <> 0 Then
            message = message + " diagram has turned off operation visibility"
            showingOperations = False
        Else
            'custom operation visibility?
            ' TBD
        End If

    End Function
    Function showingConstraints(theElement, theDiagram, ByRef message)
        showingConstraints = True

        If theElement.Constraints.Count <> 0 And InStr(1, theDiagram.ExtendedStyle, "ShowCons=0") <> 0 Then
            message = message + " diagram has turned off constraint compartment visibility"
            showingConstraints = False
        Else
            'custom Constraints visibility?
            ' TBD
        End If

    End Function
    Function elementShownInDiagram(theElement, theDiagram)
        Dim j, eID, dID
        elementShownInDiagram = False
        For j = 0 To diaoList.Count - 1
            dID = diaoList.GetByIndex(j)
            If dID = theDiagram.DiagramID Then
                'the diagram object is owned by the current diagram
                eID = diaeList.GetByIndex(j)
                If eID = theElement.ElementID Then
                    'the element is shown by this diagram object
                    elementShownInDiagram = True
                End If
            End If
        Next
    End Function
    Function connectorShownInDiagram(theConnector, theDiagram)
        Dim j, cID, dID
        connectorShownInDiagram = False
        For j = 0 To diaoList.Count - 1
            dID = dialList.GetByIndex(j)
            If dID = theDiagram.DiagramID Then
                'the diagram list is owned by the current diagram
                cID = diacList.GetByIndex(j)
                If cID = theConnector.ConnectorID Then
                    'the connector is shown by this diagram link
                    connectorShownInDiagram = True
                End If
            End If
        Next
    End Function
    Sub gatherDiagamsInPackage(thePackage)

        Dim diagram As EA.Diagram
        Dim diagramObject As EA.DiagramObject
        Dim diagramLink As EA.DiagramLink
        diagramList.Clear()
        diaoList.Clear()
        diaeList.Clear()
        dialList.Clear()
        diacList.Clear()
        For Each diagram In thePackage.diagrams
            ' list of diagrams in this package
            diagramList.Add(diagram.DiagramID, diagram.Name)
            For Each diagramObject In diagram.DiagramObjects
                ' list of elements in diagrams in this package
                diaoList.Add(diagramObject.InstanceID, diagram.DiagramID)
                diaeList.Add(diagramObject.InstanceID, diagramObject.ElementID)
            Next
            For Each diagramLink In diagram.DiagramLinks
                ' list of connectors in diagrams in this package
                If diagramLink.InstanceID = 0 Then
                    Output("Debug: possible inconsistencies caused by pending changes to diagram, diagramLink.InstanceID [«" & diagramLink.InstanceID & "] diagram.DiagramID [" & diagram.DiagramID & "]")
                Else
                    dialList.Add(diagramLink.InstanceID, diagram.DiagramID)
                End If
                ' list of connectors in diagrams in this package
                If diagramLink.InstanceID = 0 Then
                    Output("Debug: builds diacList [«" & diagramLink.InstanceID & "] connectorid [" & diagramLink.ConnectorID & "] diagramid [" & diagram.DiagramID & "]")
                Else
                    diacList.Add(diagramLink.InstanceID, diagramLink.ConnectorID)
                End If
            Next
        Next

        Dim subP As EA.Package
        For Each subP In thePackage.packages
            gatherDiagamsInPackage(subP)
        Next

    End Sub
End Class