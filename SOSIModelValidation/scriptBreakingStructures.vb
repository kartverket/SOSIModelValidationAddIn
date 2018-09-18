Partial Public Class ModelValidation

    'Function name: scriptBreakingStructuresInModel
    'Author: 		Åsmund Tjora
    'Date: 			20170511 
    'Purpose: 		Check that the model does not contain structures that will break script operations (e.g. cause infinite loops)
    'Parameter: 	the package where the script runs
    'Return value:	false if no script-breaking structures in model are found, true if parts of the model may break the script.
    'Sub functions and subs:	inHeritanceLoop, inheritanceLoopCheck
    Function scriptBreakingStructuresInModel(thePackage)
        Dim retVal
        retVal = False
        Dim currentElement As EA.Element
        Dim elements As EA.Collection

        'Package Dependency Loop Check
        currentElement = thePackage.Element
        'Note:  Dependency loops will not cause script to hang
        'retVal=retVal or dependencyLoop(currentElement)

        'Inheritance Loop Check
        elements = thePackage.elements
        Dim i
        For i = 0 To elements.Count - 1
            currentElement = elements.GetAt(i)
            If (currentElement.Type = "Class") Then
                retVal = retVal Or inheritanceLoop(currentElement)
            End If
        Next
        scriptBreakingStructuresInModel = retVal
    End Function

    'Function name: dependencyLoop
    'Author: 		Åsmund Tjora
    'Date: 			20170511 
    'Purpose: 		Check that dependency structure does not form loops.  Return true if no loops are found, return false if loops are found
    'Parameter: 	Package element where check originates
    'Return value:	false if no loops are found, true if loops are found.
    Function dependencyLoop(thePackageElement)
        Dim retVal
        Dim checkedPackagesList
        checkedPackagesList = CreateObject("System.Collections.ArrayList")
        retVal = dependencyLoopCheck(thePackageElement, checkedPackagesList)
        If retVal Then
            Output("Error:  The dependency structure originating in [«" & thePackageElement.StereoType & "» " & thePackageElement.name & "] contains dependency loops [/req/uml/integration]")
            Output("          See the list above for the packages that are part of a loop.")
            Output("          Ignore this error for dependencies between packages outside the control of the current project.")
            errorCounter = errorCounter + 1
        End If
        dependencyLoop = retVal
    End Function

    Function dependencyLoopCheck(thePackageElement, dependantCheckedPackagesList)
        Dim retVal
        Dim localRetVal
        Dim dependee As EA.Element
        Dim connector As EA.Connector

        ' Generate a copy of the input list.  
        ' The operations done on the list should not be visible by the dependant in order to avoid false positive when there are common dependees.
        Dim checkedPackagesList
        checkedPackagesList = CreateObject("System.Collections.ArrayList")
        Dim ElementID
        For Each ElementID In dependantCheckedPackagesList
            checkedPackagesList.Add(ElementID)
        Next

        retVal = False
        checkedPackagesList.Add(thePackageElement.ElementID)
        For Each connector In thePackageElement.Connectors
            localRetVal = False
            If connector.Type = "Usage" Or connector.Type = "Package" Or connector.Type = "Dependency" Then
                If thePackageElement.ElementID = connector.ClientID Then
                    dependee = theRepository.GetElementByID(connector.SupplierID)
                    Dim checkedPackageID
                    For Each checkedPackageID In checkedPackagesList
                        If checkedPackageID = dependee.ElementID Then localRetVal = True
                    Next
                    If localRetVal Then
                        Output("         Package [«" & dependee.Stereotype & "» " & dependee.Name & "] is part of a dependency loop")
                    Else
                        localRetVal = dependencyLoopCheck(dependee, checkedPackagesList)
                    End If
                    retVal = retVal Or localRetVal
                End If
            End If
        Next

        dependencyLoopCheck = retVal
    End Function

    'Function name: inheritanceLoop
    'Author: 		Åsmund Tjora
    'Date: 			20170221 
    'Purpose: 		Check that inheritance structure does not form loops.  Return true if no loops are found, return false if loops are found
    'Parameter: 	Class element where check originates
    'Return value:	false if no loops are found, true if loops are found.
    Function inheritanceLoop(theClass)
        Dim retVal
        Dim checkedClassesList
        checkedClassesList = CreateObject("System.Collections.ArrayList")
        retVal = inheritanceLoopCheck(theClass, checkedClassesList)
        If retVal Then
            Output("Error: Class hierarchy originating in [«" & theClass.Stereotype & "» " & theClass.Name & "] contains inheritance loops.")
        End If
        inheritanceLoop = retVal
    End Function

    'Function name:	inheritanceLoopCheck
    'Author:		Åsmund Tjora
    'Date:			20170221
    'Purpose		Internal workings of function inhertianceLoop.  Register the class ID, compare list of ID's with superclass ID, recursively call itself for superclass.  
    '				Return "true" if class already has been registered (i.e. is a superclass of itself) 

    Function inheritanceLoopCheck(theClass, subCheckedClassesList)
        Dim retVal
        Dim superClass As EA.Element
        Dim connector As EA.Connector

        ' Generate a copy of the input list.  
        'The operations done on the list should not be visible by the subclass in order to avoid false positive at multiple inheritance
        Dim checkedClassesList
        checkedClassesList = CreateObject("System.Collections.ArrayList")
        Dim ElementID
        For Each ElementID In subCheckedClassesList
            checkedClassesList.Add(ElementID)
        Next

        retVal = False
        checkedClassesList.Add(theClass.ElementID)
        For Each connector In theClass.Connectors
            If connector.Type = "Generalization" Then
                If theClass.ElementID = connector.ClientID Then
                    superClass = theRepository.GetElementByID(connector.SupplierID)
                    Dim checkedClassID
                    For Each checkedClassID In checkedClassesList
                        If checkedClassID = superClass.ElementID Then retVal = True
                    Next
                    If retVal Then
                        Output("Error: Class [«" & superClass.Stereotype & "» " & superClass.Name & "] is a generalization of itself")
                    Else
                        retVal = inheritanceLoopCheck(superClass, checkedClassesList)
                    End If
                End If
            End If
        Next

        inheritanceLoopCheck = retVal
    End Function


End Class