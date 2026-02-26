Imports Common.ClsFunction
Imports T.R.ZCommonCtrl

Public Class Form_Results
  Inherits FormBase

    Private Sub AchievementCreateButton_Click(sender As Object, e As EventArgs) Handles AchievementCreateButton.Click
        'Form_InputPassword.ShowDialog()
        OpenForm("OTH07")
    End Sub
    Private Sub BtnOutExcel_Click(sender As Object, e As EventArgs) Handles BtnOutExcel.Click
        OpenForm("OTH12")
    End Sub

    Private Sub Form_AchievementMente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaximizeBox = False
        FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Close()
    End Sub

End Class