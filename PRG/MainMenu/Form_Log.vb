Imports Common.ClsFunction
Public Class Form_Log
  Private Sub Form_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    FormBorderStyle = FormBorderStyle.FixedSingle
  End Sub

  ''' <summary>
  ''' フォームキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      Case Keys.F1
        ' 計量器通信
        OpenForm("OTH05")

      Case Keys.F2
        ' ログ出力
        OpenForm("OTH04")
      Case Keys.F10
        ' 終了
        Close()
    End Select

  End Sub

  Private Sub LogDisplayButton_Click(sender As Object, e As EventArgs) Handles LogDisplayButton.Click
    OpenForm("OTH04")
  End Sub

  Private Sub RealtimeConfirmation_Button_Click(sender As Object, e As EventArgs) Handles RealtimeConfirmation_Button.Click
    Form_Top.LblMessage.Text = "待機中"
    OpenForm("OTH05")
    'OpenForm("OTH11")
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub
End Class