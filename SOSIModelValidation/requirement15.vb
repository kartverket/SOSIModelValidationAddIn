Partial Public Class ModelValidation

    'Sub name:      requirement15
    'Author: 		Kent Jonsrud
    'Date: 			2018-09-20
    'Purpose: 		'/krav/15 Iso 19103 Requirement 15 - warning if not a standardised stereotype
    'Parameter: 	the element that has a stereotype
    'Requirement class:     requirement15
    'Conformance class:     from iso 19103 and 19109? part nnn

    Sub requirement15onPackage(thePackage)
        If thePackage.Element.Stereotype = "" Or UCase(thePackage.Element.Stereotype) = "APPLICATIONSCHEMA" Or UCase(thePackage.Element.Stereotype) = "LEAF" Then
        Else
            If logLevel = "Warning" Then
                Output("Warning: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Element.Name & "] has unknown stereotype. [/krav/15]")
                warningCounter = warningCounter + 1
            End If
        End If
    End Sub

    Sub requirement15onClass(theElement)
        If UCase(theElement.Stereotype) = "FEATURETYPE" Or UCase(theElement.Stereotype) = "DATATYPE" Or UCase(theElement.Stereotype) = "UNION" Or UCase(theElement.Stereotype) = "CODELIST" Or UCase(theElement.Stereotype) = "ENUMERATION" Or UCase(theElement.Stereotype) = "ESTIMATED" Or UCase(theElement.Stereotype) = "MESSAGETYPE" Or UCase(theElement.Stereotype) = "INTERFACE" Or theElement.Type = "Enumeration" Then
        Else
            If logLevel = "Warning" Then
                Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype. [/krav/15]")
                warningCounter = warningCounter + 1
            End If
        End If
    End Sub

    Sub requirement15onAttr(theElement, attr)
        If attr.Stereotype <> "" And LCase(attr.Stereotype) <> "estimated" And LCase(attr.Stereotype) <> "propertytype" Then
            'INSPIRE:	if attr.Stereotype <> "" and LCase(attr.Stereotype) <> "estimated" and LCase(attr.Stereotype) <> "propertytype" and LCase(attr.Stereotype) <> "voidable" and LCase(attr.Stereotype) <> "lifecycleinfo" then
            If logLevel = "Warning" Then
                Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown stereotype. «" & attr.Stereotype & "» on attribute [" & attr.Name & "]. [/krav/15]")
                warningCounter = warningCounter + 1
            End If
        End If
    End Sub

    Sub requirement15onRole(theElement, conn)
        Dim roleName, badStereotype

        roleName = ""
        badStereotype = ""
        If theElement.ElementID = conn.ClientID Then
            roleName = conn.SupplierEnd.Role
            badStereotype = conn.SupplierEnd.Stereotype
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
                    warningCounter = warningCounter + 1
                End If
            End If
        End If


        'Associations with unknown stereotypes, especially the old norse style «topo»
        If conn.Stereotype <> "" Then
            If LCase(conn.Stereotype) = "topo" Then
                If roleName <> "" Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found illegal stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "] by role [" & roleName & "]. Recommended to use the script <endreTopoAssosiasjonTilRestriksjon>. [/krav/15]")
                Else
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found illegal stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "]. Recommended to use the script <endreTopoAssosiasjonTilRestriksjon>. [/krav/15]")
                End If
                errorCounter = errorCounter + 1
            Else
                If logLevel = "Warning" Then
                    If roleName <> "" Then
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found unknown stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "] by role [" & roleName & "]. [/krav/15]")
                    Else
                        Output("Warning: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has found unknown stereotype «" & conn.Stereotype & "» on association named [" & conn.Name & "]. [/krav/15]")
                    End If
                    warningCounter = warningCounter + 1
                End If
            End If
        End If

    End Sub
End Class