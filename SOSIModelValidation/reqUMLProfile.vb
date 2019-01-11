Partial Public Class ModelValidation

    'Sub name:      reqUMLProfile
    'Author: 		Kent Jonsrud
    'Date: 			2018-09-20, 10-18
    'Purpose: 		/req/uml/profile from iso 19109 - check for valid well known types for all attributes (GM_Surface etc.), builds on iso 19103 Requirement 22 and 25.
    'Parameter: 	the property element that uses a type
    'Requirement class:     /req/uml/profile (and 25 and 22)
    'Conformance class:     from iso 19109 part nnn
    Sub reqUMLProfileNorsk(theElement, attr)

        If attr.ClassifierID = 0 Then
            'Attribute not connected to a datatype class, check if the attribute has a iso TC 211 well known type
            If NationalTypes.IndexOf(attr.Type, 0) = -1 Then
                If ProfileTypes.IndexOf(attr.Type, 0) = -1 Then
                    If ExtensionTypes.IndexOf(attr.Type, 0) = -1 Then
                        If CoreTypes.IndexOf(attr.Type, 0) = -1 Then
                            Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown type for attribute [" & attr.Name & " : " & attr.Type & "]. [norsk /req/uml/profile]")
                            errorCounter += 1
                        End If
                    End If
                End If
            End If
        End If

    End Sub
    Sub reqUMLProfile(theElement, attr)

        If attr.ClassifierID = 0 Then
            'Attribute not connected to a datatype class, check if the attribute has a iso TC 211 well known type
            If ProfileTypes.IndexOf(attr.Type, 0) = -1 Then
                If ExtensionTypes.IndexOf(attr.Type, 0) = -1 Then
                    If CoreTypes.IndexOf(attr.Type, 0) = -1 Then
                        Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown type for attribute [" & attr.Name & " : " & attr.Type & "]. [/req/uml/profile]")
                        errorCounter += 1
                    End If
                End If
            End If
        End If

    End Sub
    Sub requirement25(theElement, attr)

        If attr.ClassifierID = 0 Then
            'Attribute not connected to a datatype class, check if the attribute has a iso TC 211 well known type
            'If ProfileTypes.IndexOf(attr.Type, 0) = -1 Then
            If ExtensionTypes.IndexOf(attr.Type, 0) = -1 Then
                If CoreTypes.IndexOf(attr.Type, 0) = -1 Then
                    Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown type for attribute [" & attr.Name & " : " & attr.Type & "]. [requirement25]")
                    errorCounter += 1
                End If
            End If
            'End If
        End If

    End Sub
    Sub requirement22(theElement, attr)

        If attr.ClassifierID = 0 Then
            'Attribute not connected to a datatype class, check if the attribute has a iso TC 211 well known type
            'If ProfileTypes.IndexOf(attr.Type, 0) = -1 Then
            '   If ExtensionTypes.IndexOf(attr.Type, 0) = -1 Then
            If CoreTypes.IndexOf(attr.Type, 0) = -1 Then
                Output("Error: Class [«" & theElement.Stereotype & "» " & theElement.Name & "] has unknown type for attribute [" & attr.Name & " : " & attr.Type & "]. [requirement22]")
                errorCounter += 1
            End If
            '   End If
            'End If
        End If

    End Sub



    Sub reqUMLProfileLoad()

        'iso 19103:2015 Core types
        CoreTypes.Add("Date")
        CoreTypes.Add("Time")
        CoreTypes.Add("DateTime")
        CoreTypes.Add("CharacterString")
        CoreTypes.Add("Number")
        CoreTypes.Add("Decimal")
        CoreTypes.Add("Integer")
        CoreTypes.Add("Real")
        CoreTypes.Add("Boolean")
        CoreTypes.Add("Vector")

        CoreTypes.Add("Bit")
        CoreTypes.Add("Digit")
        CoreTypes.Add("Sign")

        CoreTypes.Add("NameSpace")
        CoreTypes.Add("GenericName")
        CoreTypes.Add("LocalName")
        CoreTypes.Add("ScopedName")
        CoreTypes.Add("TypeName")
        CoreTypes.Add("MemberName")

        CoreTypes.Add("Any")

        CoreTypes.Add("Record")
        CoreTypes.Add("RecordType")
        CoreTypes.Add("Field")
        CoreTypes.Add("FieldType")

        'iso 19103:2015 Annex-C types
        ExtensionTypes.Add("LanguageString")

        ExtensionTypes.Add("Anchor")
        ExtensionTypes.Add("FileName")
        ExtensionTypes.Add("MediaType")
        ExtensionTypes.Add("URI")

        ExtensionTypes.Add("UnitOfMeasure")
        ExtensionTypes.Add("UomArea")
        ExtensionTypes.Add("UomLenght")
        ExtensionTypes.Add("UomAngle")
        ExtensionTypes.Add("UomAcceleration")
        ExtensionTypes.Add("UomAngularAcceleration")
        ExtensionTypes.Add("UomAngularSpeed")
        ExtensionTypes.Add("UomSpeed")
        ExtensionTypes.Add("UomCurrency")
        ExtensionTypes.Add("UomVolume")
        ExtensionTypes.Add("UomTime")
        ExtensionTypes.Add("UomScale")
        ExtensionTypes.Add("UomWeight")
        ExtensionTypes.Add("UomVelocity")

        ExtensionTypes.Add("Measure")
        ExtensionTypes.Add("Length")
        ExtensionTypes.Add("Distance")
        ExtensionTypes.Add("Speed")
        ExtensionTypes.Add("Angle")
        ExtensionTypes.Add("Scale")
        ExtensionTypes.Add("TimeMeasure")
        ExtensionTypes.Add("Area")
        ExtensionTypes.Add("Volume")
        ExtensionTypes.Add("Currency")
        ExtensionTypes.Add("Weight")
        ExtensionTypes.Add("AngularSpeed")
        ExtensionTypes.Add("DirectedMeasure")
        ExtensionTypes.Add("Velocity")
        ExtensionTypes.Add("AngularVelocity")
        ExtensionTypes.Add("Acceleration")
        ExtensionTypes.Add("AngularAcceleration")

        'well known and often used spatial types from iso 19107:2003
        ProfileTypes.Add("DirectPosition")
        ProfileTypes.Add("GM_Object")
        ProfileTypes.Add("GM_Primitive")
        ProfileTypes.Add("GM_Complex")
        ProfileTypes.Add("GM_Aggregate")
        ProfileTypes.Add("GM_Point")
        ProfileTypes.Add("GM_Curve")
        ProfileTypes.Add("GM_Surface")
        ProfileTypes.Add("GM_Solid")
        ProfileTypes.Add("GM_MultiPoint")
        ProfileTypes.Add("GM_MultiCurve")
        ProfileTypes.Add("GM_MultiSurface")
        ProfileTypes.Add("GM_MultiSolid")
        ProfileTypes.Add("GM_CompositePoint")
        ProfileTypes.Add("GM_CompositeCurve")
        ProfileTypes.Add("GM_CompositeSurface")
        ProfileTypes.Add("GM_CompositeSolid")
        ProfileTypes.Add("TP_Object")
        'ProfileTypes.Add("TP_Primitive")
        ProfileTypes.Add("TP_Complex")
        ProfileTypes.Add("TP_Node")
        ProfileTypes.Add("TP_Edge")
        ProfileTypes.Add("TP_Face")
        ProfileTypes.Add("TP_Solid")
        ProfileTypes.Add("TP_DirectedNode")
        ProfileTypes.Add("TP_DirectedEdge")
        ProfileTypes.Add("TP_DirectedFace")
        ProfileTypes.Add("TP_DirectedSolid")
        ProfileTypes.Add("GM_OrientableCurve")
        ProfileTypes.Add("GM_OrientableSurface")
        ProfileTypes.Add("GM_PolyhedralSurface")
        ProfileTypes.Add("GM_TriangulatedSurface")
        ProfileTypes.Add("GM_Tin")

        'well known and often used coverage types from iso 19123:2007
        ProfileTypes.Add("CV_Coverage")
        ProfileTypes.Add("CV_DiscreteCoverage")
        ProfileTypes.Add("CV_DiscretePointCoverage")
        ProfileTypes.Add("CV_DiscreteGridPointCoverage")
        ProfileTypes.Add("CV_DiscreteCurveCoverage")
        ProfileTypes.Add("CV_DiscreteSurfaceCoverage")
        ProfileTypes.Add("CV_DiscreteSolidCoverage")
        ProfileTypes.Add("CV_ContinousCoverage")
        ProfileTypes.Add("CV_ThiessenPolygonCoverage")
        'ExtensionTypes.Add("CV_ContinousQuadrilateralGridCoverageCoverage")
        ProfileTypes.Add("CV_ContinousQuadrilateralGridCoverage")
        ProfileTypes.Add("CV_HexagonalGridCoverage")
        ProfileTypes.Add("CV_TINCoverage")
        ProfileTypes.Add("CV_SegmentedCurveCoverage")

        'well known and often used temporal types from iso 19108:2006/2002?
        ProfileTypes.Add("TM_Instant")
        ProfileTypes.Add("TM_Period")
        ProfileTypes.Add("TM_Node")
        ProfileTypes.Add("TM_Edge")
        ProfileTypes.Add("TM_TopologicalComplex")

        'well known and often used observation related types from OM_Observation in iso 19156:2011
        ProfileTypes.Add("TM_Object")
        ProfileTypes.Add("DQ_Element")
        ProfileTypes.Add("NamedValue")

        'well known and often used quality element types from iso 19157:2013
        ProfileTypes.Add("DQ_AbsoluteExternalPositionalAccurracy")
        ProfileTypes.Add("DQ_RelativeInternalPositionalAccuracy")
        ProfileTypes.Add("DQ_AccuracyOfATimeMeasurement")
        ProfileTypes.Add("DQ_TemporalConsistency")
        ProfileTypes.Add("DQ_TemporalValidity")
        ProfileTypes.Add("DQ_ThematicClassificationCorrectness")
        ProfileTypes.Add("DQ_NonQuantitativeAttributeCorrectness")
        ProfileTypes.Add("DQ_QuanatitativeAttributeAccuracy")

        'well known and often used metadata element types from iso 19115-1:200x and iso 19139:2x00x
        ProfileTypes.Add("PT_FreeText")
        ProfileTypes.Add("LocalisedCharacterString")
        ProfileTypes.Add("MD_Resolution")
        'ProfileTypes.Add("CI_Citation")
        'ProfileTypes.Add("CI_Date")


        'other lesser known Norwegian legacy geometry types
        NationalTypes.Add("Punkt")
        NationalTypes.Add("Kurve")
        NationalTypes.Add("Flate")
        NationalTypes.Add("Sverm")


    End Sub
End Class