Partial Public Class ModelValidation
    'Sub name:      anbefaling3
    'Author: 		Tore Johnsen
    'Date: 			20200831
    'Date: 			2021-11-14 warnings on classes with keyword enumeration will be shown with stereotype «enumeration». Kent Jonsrud
    'Purpose: 	    Check for codes with same name, and definition.




    Sub anbefaling3(theElement As EA.Element)

        Dim mainAtt As EA.Attribute
        Dim secondaryAtt As EA.Attribute
        Dim counterName
        Dim counterNotes
        Dim duplicatedCodesString
        Dim duplicatedNotesString
        Dim stereo

        For Each mainAtt In theElement.Attributes
            duplicatedCodesString = ""
            duplicatedNotesString = ""
            counterName = False
            counterNotes = False

            For Each secondaryAtt In theElement.Attributes

                If UCase(mainAtt.Name) = UCase(secondaryAtt.Name) Then
                    If mainAtt.AttributeID <> secondaryAtt.AttributeID Then
                        duplicatedCodesString = duplicatedCodesString + " " + "[" & secondaryAtt.Name & "]"
                        counterName = True
                    End If
                End If

				If mainAtt.Notes <> "" then
					If Trim(UCase(mainAtt.Notes)) = Trim(UCase(secondaryAtt.Notes)) Then
						If mainAtt.AttributeID <> secondaryAtt.AttributeID Then
							duplicatedNotesString = duplicatedNotesString + " " + "[" & secondaryAtt.Name & "]"
							counterNotes = True
						End If
					End If
				End If

            Next
            stereo = theElement.Stereotype
            If stereo = "" And theElement.Type = "Enumeration" Then stereo = "enumeration"
            If counterName Then
                Output("Waning: Class [«" & stereo & "» " & theElement.Name & "] - attribute: [" & mainAtt.Name & "] has the same codename as" & duplicatedCodesString & ". [/anbefaling/3]")
                warningCounter += 1
            End If

            If counterNotes Then
                Output("Waning: Class [«" & stereo & "» " & theElement.Name & "] - attribute: [" & mainAtt.Name & "] has the same definition as" & duplicatedNotesString & ". [/anbefaling/3]")
                warningCounter += 1
            End If

        Next

    End Sub


End Class
