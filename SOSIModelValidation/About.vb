Public Class About



    Public Sub SetVersion(Version As String, Year As String)
        Label1.Text() = "SOSI Model Validation version " + Version
        Label2.Text() = "Kartverket " + Year
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://www.kartverket.no")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/kartverket/SOSIModelValidationAddIn")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        System.Diagnostics.Process.Start("mailto:standardiseringssekretariatet@kartverket.no")
    End Sub
End Class