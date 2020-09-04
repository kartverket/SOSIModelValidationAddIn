Partial Public Class ModelValidation

    'Sub name:      requirement15
    'Author: 		Kent Jonsrud
    'Date: 			2018-09-20, 10-18, 2019-03-12
    'Purpose: 		'/krav/15 Iso 19103 Requirement 15 - warning if not a standardised stereotype
    'Parameter: 	the element that has a stereotype
    'Requirement class:     requirement15
    'Conformance class:     from iso 19103 and 19109 clause 8.2.2 (see req/UML/Profile)
    'TBD:           do we really need separate tests for different rulesets on attributes and roles?

    Sub requirement15(theThing As EA.Package)
        Call requirement15onPackage(theThing)
    End Sub
    Sub requirement15(theThing As EA.Element)
        Call requirement15onClass(theThing)
    End Sub
    Sub requirement15(theElement As EA.Element, theThing As EA.Attribute)
        Call requirement15onAttr(theRepository.GetElementByID(theThing.ParentID), theThing)
    End Sub
    Sub requirement15(theElement As EA.Element, theThing As EA.Connector)
        'Call requirement15onAssoc(theRepository.GetElementByID(theThing.DiagramID), theThing)
        Call requirement15onAssoc(theElement, theThing)
    End Sub
    Sub requirement15(theElement As EA.Element, theThing As EA.ConnectorEnd)
        'Call requirement15onRole(theRepository.GetElementByID(theThing.ParentID), theThing)
        Call requirement15onRole(theElement, theThing)
    End Sub
    Sub requirement15onPackage(thePackage)
        If ruleSet = "SOSI" Or ruleSet = "19109" Then
            If thePackage.Element.Stereotype = "" Or UCase(thePackage.Element.Stereotype) = "APPLICATIONSCHEMA" Or UCase(thePackage.Element.Stereotype) = "LEAF" Then
            Else
                If logLevel = "Warning" Then
                    Output("Warning: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has unknown stereotype. [/krav/15]")
                    warningCounter += 1
                End If
            End If
        End If
        If ruleSet = "19103" Then
            If thePackage.Element.Stereotype = "" Or UCase(thePackage.Element.Stereotype) = "LEAF" Then
            Else
                If logLevel = "Warning" Then
                    Output("Warning: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has unknown stereotype. [/krav/15]")
                    warningCounter += 1
                End If
            End If
        End If
    End Sub

    Sub requirement15onClass(theElement)
        If ruleSet = "SOSI" Then
            If UCase(theElement.Stereotype) = "FEATURETYPE" Or UCase(theElement.Stereotype) = "DATATYPE" Or UCase(theElement.Stereotype) = "UNION" Or UCase(theElement.Stereotype) = "CODELIST" Or UCase(theElement.Stereotype) = "ENUMERATION" Or UCase(theElement.Stereotype) = "ESTIMATED" Or UCase(theElement.Stereotype) = "MESSAGETYPE" Or theElement.Type = "Enumeration" Then
            Else
                If logLevel = "Warning" Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype. [/krav/15]")
                    warningCounter += 1
                End If
            End If
        End If
        If ruleSet = "19109" Then
            If UCase(theElement.Stereotype) = "FEATURETYPE" Or UCase(theElement.Stereotype) = "DATATYPE" Or UCase(theElement.Stereotype) = "UNION" Or UCase(theElement.Stereotype) = "CODELIST" Or UCase(theElement.Stereotype) = "ENUMERATION" Or UCase(theElement.Stereotype) = "ESTIMATED" Or theElement.Type = "Enumeration" Then
            Else
                If logLevel = "Warning" Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype. [/krav/15]")
                    warningCounter += 1
                End If
            End If
        End If
        If ruleSet = "19103" Then
            If UCase(theElement.Stereotype) = "DATATYPE" Or UCase(theElement.Stereotype) = "UNION" Or UCase(theElement.Stereotype) = "CODELIST" Or UCase(theElement.Stereotype) = "ENUMERATION" Or UCase(theElement.Stereotype) = "INTERFACE" Or theElement.Type = "Enumeration" Then
            Else
                If logLevel = "Warning" Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype. [/krav/15]")
                    warningCounter += 1
                End If
            End If
        End If

    End Sub

    Sub requirement15onAttr(theElement, attr)
        If attr.Stereotype <> "" And LCase(attr.Stereotype) <> "estimated" And LCase(attr.Stereotype) <> "propertytype" Then
            'INSPIRE:	if attr.Stereotype <> "" and LCase(attr.Stereotype) <> "estimated" and LCase(attr.Stereotype) <> "propertytype" and LCase(attr.Stereotype) <> "voidable" and LCase(attr.Stereotype) <> "lifecycleinfo" then
            If logLevel = "Warning" Then
                Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype. «" & attr.Stereotype & "» on attribute [" & attr.Name & "]. [/krav/15]")
                warningCounter += 1
            End If
        End If
    End Sub

    Sub requirement15onAssoc(theElement, conn)
        'Associations with unknown stereotypes, especially the old norse style «topo»
        Dim rolename
        Dim badStereotype = ""
        rolename = ""
        If theElement.ElementID = conn.ClientID Then
            rolename = conn.SupplierEnd.Role
        End If
        If theElement.ElementID = conn.SupplierID Then

            roleName = conn.ClientEnd.Role
            badStereotype = conn.ClientEnd.Stereotype
        End If
        '(ignoring all association roles without name!)
        If roleName <> "" Then
            If badStereotype <> "" And LCase(badStereotype) <> "estimated" And LCase(badStereotype) <> "propertytype" Then
                'INSPIRE:		if badStereotype <> "" and LCase(badStereotype) <> "estimated" and LCase(badStereotype) <> "propertytype" and LCase(badStereotype) <> "voidable" then
                If logLevel = "Warning" Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype «" & badStereotype & "» on role name [" & roleName & "]. [/krav/15]")
                    warningCounter += 1
                End If
            End If

        End If
        If conn.Stereotype <> "" Then
            If LCase(conn.Stereotype) = "topo" And ruleSet = "SOSI" Then
                If rolename <> "" Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found illegal stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "] by role [" & rolename & "]. Recommended to use the script <endreTopoAssosiasjonTilRestriksjon>. [/krav/15]")
                Else
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found illegal stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "]. Recommended to use the script <endreTopoAssosiasjonTilRestriksjon>. [/krav/15]")
                End If
                errorCounter += 1
            Else
                If logLevel = "Warning" Then
                    If rolename <> "" Then
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found unknown stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "] by role [" & rolename & "]. [/krav/15]")
                    Else
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found unknown stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "]. [/krav/15]")
                    End If
                    warningCounter += 1
                End If
            End If
        End If

    End Sub
    Sub requirement15onRole(theElement, connEnd)
        Dim badStereotype

        badStereotype = connEnd.Stereotype

        '(ignoring all association roles without name!)
        If connEnd.Role <> "" Then
            If badStereotype <> "" And LCase(badStereotype) <> "estimated" And LCase(badStereotype) <> "propertytype" Then
                'INSPIRE:		if badStereotype <> "" and LCase(badStereotype) <> "estimated" and LCase(badStereotype) <> "propertytype" and LCase(badStereotype) <> "voidable" then
                If logLevel = "Warning" Then
                    Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] as unknown stereotype «" & badStereotype & "» on role name [" & connEnd.Role & "]. [/krav/15]")
                    warningCounter += 1
                End If
            End If
        End If

    End Sub
End Class