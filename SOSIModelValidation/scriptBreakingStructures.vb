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