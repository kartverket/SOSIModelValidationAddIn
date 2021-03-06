﻿Public Class Main
    Public Class Main

        Const menuHeader = "-&SOSI Model Validation"
        Const menuValidate = "&Run SOSI Model Validation"
        Const menuAbout = "&About"

        ' Check existence of Add-In
        Public Function EA_Connect(Repository As EA.Repository)
            Return "a string"
        End Function

        ' Menu setup
        Public Function EA_GetMenuItems(Repository As EA.Repository, Location As String, MenuName As String)
            Select Case MenuName
                Case ""
                    Return menuHeader
                Case menuHeader
                    Dim subMenus = New String() {menuValidate, menuAbout}
                    Return subMenus
                Case Else
                    Return ""
            End Select
        End Function

        ' Return true if project is  open
        Function IsProjectOpen(Repository As EA.Repository)
            Try
                Dim c As EA.Collection = Repository.Models
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ' Enable/Disable menu items
        Public Sub EA_GetMenuState(Repository As EA.Repository, Location As String, MenuName As String, ItemName As String, ByRef IsEnabled As Boolean, ByRef IsCheckd As Boolean)
            If (IsProjectOpen(Repository)) Then
                Select Case ItemName
                    Case menuValidate
                        IsEnabled = True
                    Case menuAbout
                        IsEnabled = True
                    Case Else
                        IsEnabled = False
                End Select
            Else
                Select Case ItemName
                    Case menuAbout
                        IsEnabled = True
                    Case Else
                        IsEnabled = False
                End Select
            End If
        End Sub

        ' Menu selection
        Public Sub EA_MenuClick(Repository As EA.Repository, Location As String, MenuName As String, ItemName As String)
            Select Case ItemName
                Case menuValidate
                    'Dim modelVal As Object
                    'modelVal = New ModelValidation
                    'Call modelVal.ModelValidation(Repository)
                Case menuAbout
                    ShowAbout()
            End Select
        End Sub

        Private Sub ShowAbout()
            'Dim About = New AboutWindow
            System.Windows.Forms.MessageBox.Show("SOSI Model Validation" + vbCrLf + "Kartverket 2018")
        End Sub
    End Class


End Class
