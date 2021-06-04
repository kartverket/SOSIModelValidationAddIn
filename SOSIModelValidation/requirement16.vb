Partial Public Class ModelValidation

    'Sub name:      requirement16
    'Author: 		Kent Jonsrud
    'Date: 			2018-10-17, 2019-03-12, 2019-06-19
    'Date:          2021-03-06 test only role names for classes
    'Purpose: 		'/krav/15 Iso 19103 Requirement 16 - legal non-whitespaced (NC)Name case-insesnitively unique within its namespace
    'Parameter: 	the element that has a name, and is a namespace for names
    'Requirement class:     requirement16
    'Conformance class:     from iso 19103

    Sub requirement16(theThing As EA.Package)
        Call requirement16onPackage(theThing)
    End Sub
    Sub requirement16(theThing As EA.Element)
        Call requirement16onElement(theThing)
    End Sub
    Sub requirement16(theThing As EA.Attribute)
        'duplicate tagged values with same name and value?
        Call requirement16onAttribute(theThing)
    End Sub
    Sub requirement16(theThing As EA.Method)
        Call requirement16onMethod(theThing)
    End Sub
    Sub requirement16(theThing As EA.Connector)
        Call requirement16onConnector(theThing)
    End Sub
    Sub requirement16(theClass As EA.Element, theConn As EA.Connector, theThing As EA.ConnectorEnd)
        Call requirement16onConnectorEnd(theClass, theConn, theThing)
    End Sub
    Sub requirement16onPackage(thePackage)
        Dim PackageNames As New System.Collections.ArrayList
        'PackageNames.Clear()
        For Each package In thePackage.Packages
            If PackageNames.IndexOf(UCase(package.Name), 0) <> -1 Then
                Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] has non-unique subpackage names [" & package.Name & "]. [/krav/16]")
                errorCounter += 1
            Else
                PackageNames.Add(UCase(package.Name))
            End If
        Next
        Dim ClassNames As New System.Collections.ArrayList
        'ClassNames.Clear()
        For Each element In thePackage.Elements
            If element.Type = "Class" Or element.Type = "DataType" Or element.Type = "Enumeration" Or element.Type = "Interface" Then
                If element.Name <> "" Then
                    If Not isNCName(element.Name) Then
                        Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] has illegal class name [" & element.Name & "]. [/krav/16]")
                        errorCounter += 1
                    End If
                    If ClassNames.IndexOf(UCase(element.Name), 0) <> -1 Then
                        Output("Error: Package [«" & thePackage.Element.Stereotype & "» " & thePackage.Name & "] has non-unique class names [" & element.Name & "]. [/krav/16]")
                        errorCounter += 1
                    Else
                        ClassNames.Add(UCase(element.Name))
                    End If
                End If
            End If
        Next
    End Sub
    Sub requirement16onElement(theElement)
        Dim PropertyNames As New System.Collections.ArrayList
        If Not isNCName(theElement.Name) Then
            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has illegal name. [/krav/16]")
            errorCounter += 1
        End If
        'PropertyNames.Clear()
        For Each attribute In theElement.Attributes
            If PropertyNames.IndexOf(UCase(attribute.Name), 0) <> -1 Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique attribute property names [" & attribute.Name & "]. [/krav/16]")
                errorCounter += 1
            Else
                PropertyNames.Add(UCase(attribute.Name))
            End If
        Next
        For Each connector In theElement.Connectors
            If connector.Type <> "Generalization" And connector.Type <> "Realisation" Then

                If connector.ClientEnd.Role <> "" And connector.SupplierID = theElement.ElementID And theRepository.GetElementByID(connector.ClientID).Type = "Class" Then
                    If PropertyNames.IndexOf(UCase(connector.ClientEnd.Role), 0) <> -1 Then
                        Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique role property names [" & connector.ClientEnd.Role & "]. [/krav/16]")
                        errorCounter += 1
                    Else
                        PropertyNames.Add(UCase(connector.ClientEnd.Role))
                    End If
                End If
                If connector.SupplierEnd.Role <> "" And connector.ClientID = theElement.ElementID And theRepository.GetElementByID(connector.SupplierID).Type = "Class" Then
                    If PropertyNames.IndexOf(UCase(connector.SupplierEnd.Role), 0) <> -1 Then
                        Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique role property names [" & connector.SupplierEnd.Role & "]. [/krav/16]")
                        errorCounter += 1
                    Else
                        PropertyNames.Add(UCase(connector.SupplierEnd.Role))
                    End If
                End If
            End If
        Next
        'operations may be polymorphic

        'PropertyNames.Clear()
        For Each constraint In theElement.Constraints
            If PropertyNames.IndexOf(UCase(constraint.Name), 0) <> -1 Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has non-unique constraint property names [" & constraint.Name & "]. [/krav/16]")
                errorCounter += 1
            Else
                PropertyNames.Add(UCase(constraint.Name))
            End If
        Next
    End Sub
    Sub requirement16onAttribute(theAttribute)
        If Not isNCName(theAttribute.Name) Then
            Output("Error: Class [«" & theRepository.GetElementByID(theAttribute.ParentID).Stereotype & "» " & theRepository.GetElementByID(theAttribute.ParentID).Name & "] has illegal attribute name [" & theAttribute.Name & "]. [/krav/16]")
            errorCounter += 1
        End If
    End Sub
    Sub requirement16onMethod(theMethod)
        If Not isNCName(theMethod.Name) Then
            Output("Error: Class [«" & theRepository.GetElementByID(theMethod.ParentID).Stereotype & "» " & theRepository.GetElementByID(theMethod.ParentID).Name & "] has illegal operation name [" & theMethod.Name & "]. [/krav/16]")
            errorCounter += 1
        End If
    End Sub
    Sub requirement16onConnector(theConnector)
        If Not isNCName(theConnector.Name) Then
            Output("Error: Source end class [«" & theRepository.GetElementByID(theConnector.ClientID).Stereotype & "» " & theRepository.GetElementByID(theConnector.ClientID).Name & "] has illegal Association name [" & theConnector.Name & "]. [/krav/16]")
            errorCounter += 1
        End If
    End Sub
    Sub requirement16onConnectorEnd(theClass, theConn, theConnectorEnd)
        If Not isNCName(theConnectorEnd.Role) Then
            Output("Error: Class [«" & theClass.Stereotype & "» " & theClass.Name & "] has illegal role property name [" & theConnectorEnd.Role & "]. [/krav/16]")
            'Output("Error: Connector [" & theRepository.GetElementByID(theConnectorEnd.ConnectorID).Name & "] has illegal role property names [" & theConnectorEnd.Role & "]. [/krav/16]")
            errorCounter += 1
        End If
    End Sub
    Function isNCName(streng)
        Dim tegn, i, u
        u = True
        For i = 1 To Len(streng)
            tegn = Mid(streng, i, 1)
            If tegn = " " Or tegn = "," Or tegn = """" Or tegn = "#" Or tegn = "$" Or tegn = "%" Or tegn = "&" Or tegn = "(" Or tegn = ")" Or tegn = "*" Then
                u = False
            End If
            If tegn = "+" Or tegn = "/" Or tegn = ":" Or tegn = ";" Or tegn = "<" Or tegn = ">" Or tegn = "?" Or tegn = "@" Or tegn = "[" Or tegn = "\" Then
                u = False
            End If
            If tegn = "]" Or tegn = "^" Or tegn = "`" Or tegn = "{" Or tegn = "|" Or tegn = "}" Or tegn = "~" Or tegn = "'" Or tegn = "´" Or tegn = "¨" Then
                u = False
            End If
            If tegn < " " Then
                u = False
            End If
        Next
        tegn = Mid(streng, 1, 1)
        'if not globalLogLevelIsVarning then
        If tegn = "1" Or tegn = "2" Or tegn = "3" Or tegn = "4" Or tegn = "5" Or tegn = "6" Or tegn = "7" Or tegn = "8" Or tegn = "9" Or tegn = "0" Or tegn = "-" Or tegn = "." Then
            u = False
        End If
        'end if
        isNCName = u
    End Function
    Function isNWName(streng)
        Dim tegn, i, u
        u = True
        For i = 1 To Len(streng)
            tegn = Mid(streng, i, 1)
            If tegn = " " Or tegn = "," Or tegn = """" Or tegn = "#" Or tegn = "$" Or tegn = "%" Or tegn = "&" Or tegn = "(" Or tegn = ")" Or tegn = "*" Then
                u = False
            End If
            If tegn = "+" Or tegn = "/" Or tegn = ":" Or tegn = ";" Or tegn = "<" Or tegn = ">" Or tegn = "?" Or tegn = "@" Or tegn = "[" Or tegn = "\" Then
                u = False
            End If
            If tegn = "]" Or tegn = "^" Or tegn = "`" Or tegn = "{" Or tegn = "|" Or tegn = "}" Or tegn = "~" Or tegn = "'" Or tegn = "´" Or tegn = "¨" Then
                u = False
            End If
            If tegn < " " Then
                u = False
            End If
        Next
        isNWName = u
    End Function
End Class