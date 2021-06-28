Partial Public Class ModelValidation

    'Function name:         fullyShownInDiagram
    'Author: 		        Kent Jonsrud
    'Date: 			        2020-09-02
    'Date:                  2021-03-06 fixed big bug clearing lists in a recursive routine
    'Date:                  2021-06-03 relaxed testing of association visibility for this version 
    'Date:                  TBD details on constraints
    'Date:                  TBD details on association ends
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
        If theElement.Attributes.Count <> 0 And InStr(1, theDiagram.ExtendedStyle, "HideAtts=1") <> 0 And Not elementAttrShownInDiagram(theElement, theDiagram) Then
            message = message & " class and diagram has turned off attribute visibility"
            showingAttributes = False
        Else
            'custom attribute visibility?
            '       If Not elementAttrShownInDiagram(theElement, theDiagram) Then
            '           message = message & " class has turned off attribute visibility"
            '           showingAttributes = False
            '       End If
            ' TBD
        End If

    End Function
    Function showingRoles(theElement, theDiagram, ByRef message)
        showingRoles = True

        If theElement.Connectors.Count <> 0 Then
            If InStr(1, theDiagram.ExtendedStyle, "HideRel=1") <> 0 Then
                message = message & " diagram has turned off relationship visibility"
                showingRoles = False
            Else
                For Each connector In theElement.Connectors
                    If connectorShownInDiagram(connector, theDiagram) Then
                    Else
                        message = message & " association between role [" & connector.ClientEnd.Role & "] and role [" & connector.SupplierEnd.Role & "] has turned off some of its visibility"
                        showingRoles = False
                        Exit Function
                    End If
                Next
            End If
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

        If theElement.Constraints.Count <> 0 And InStr(1, theDiagram.ExtendedStyle, "ShowCons=0") <> 0 And Not elementConstraintsShownInDiagram(theElement, theDiagram) Then
            message = message + " class and diagram has turned off constraint visibility"
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
    Function elementAttrShownInDiagram(theElement, theDiagram)
        Dim j, eID, dID, duid
        elementAttrShownInDiagram = False
        For j = 0 To diaoList.Count - 1
            dID = diaoList.GetByIndex(j)
            If dID = theDiagram.DiagramID Then
                'the diagram object is owned by the current diagram
                eID = diaeList.GetByIndex(j)
                If eID = theElement.ElementID Then
                    'the element is shown by this diagram object, but are its attributes shown?
                    Dim diaobj As EA.DiagramObject
                    For Each diaobj In theDiagram.DiagramObjects
                        If diaobj.InstanceID = diaoList.GetKey(j) Then
                            duid = Mid(diaobj.Style, InStr(1, diaobj.Style, "DUID=") + 5, 8)  ' TBD: should be read not 8 chars but until next ;
                            If InStr(1, theDiagram.StyleEX, duid & "=Att=1:") <> 0 Then
                                elementAttrShownInDiagram = True
                            End If
                        End If
                    Next
                End If
            End If
        Next
    End Function
    Function connectorShownInDiagram(theConnector, theDiagram)
        Dim j, cID, dID
        'connectorShownInDiagram = False
        'FIXME
        connectorShownInDiagram = True
        For j = 0 To dialList.Count - 1
            dID = dialList.GetByIndex(j)
            If dID = theDiagram.DiagramID Then
                'the diagram list is owned by the current diagram
                cID = diacList.GetByIndex(j)
                If cID = theConnector.ConnectorID Then
                    'the connector is shown by this diagram link
                    Dim dialink As EA.DiagramLink
                    For Each dialink In theDiagram.DiagramLinks
                        If dialink.InstanceID = diacList.GetKey(j) Then
                            If Not dialink.IsHidden And Not dialink.HiddenLabels Then
                                connectorShownInDiagram = True
                            End If
                        End If
                    Next

                    'connectorShownInDiagram = True
                End If
            End If
        Next
    End Function
    Function elementConstraintsShownInDiagram(theElement, theDiagram)
        Dim j, eID, dID
        elementConstraintsShownInDiagram = False
        For j = 0 To diaoList.Count - 1
            dID = diaoList.GetByIndex(j)
            If dID = theDiagram.DiagramID Then
                'the diagram object is owned by the current diagram
                eID = diaeList.GetByIndex(j)
                If eID = theElement.ElementID Then
                    'the element is shown by this diagram object, but are its attributes shown?
                    Dim diaobj As EA.DiagramObject
                    For Each diaobj In theDiagram.DiagramObjects
                        If diaobj.InstanceID = diaoList.GetKey(j) Then
                            If InStr(1, diaobj.Style, "Constraint=1") <> 0 Then
                                elementConstraintsShownInDiagram = True
                            End If
                        End If
                    Next
                End If
            End If
        Next
    End Function
    Sub gatherDiagamsInPackageClear()
        diagramList.Clear()
        diaoList.Clear()
        diaeList.Clear()
        dialList.Clear()
        diacList.Clear()

    End Sub

    Sub gatherDiagamsInPackage(thePackage)

        Dim diagram As EA.Diagram
        Dim diagramObject As EA.DiagramObject
        Dim diagramLink As EA.DiagramLink

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
                Else
                    dialList.Add(diagramLink.InstanceID, diagram.DiagramID)
                End If
                ' list of connectors in diagrams in this package
                If diagramLink.InstanceID = 0 Then
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