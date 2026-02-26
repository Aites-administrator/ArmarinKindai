Imports Common.ClsFunction
Public Class Form_OutPut

  Private Sub Form_OutPut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        ' 納品書発行
        OpenForm("OTH10")
      Case Keys.F2
        ' CSV出力
        OpenForm("OTH12")

      Case Keys.F10
        ' 終了
        Close()
    End Select

  End Sub


  Private Sub DetailOutputButton_Click(sender As Object, e As EventArgs) Handles DetailOutputButton.Click
    OpenForm("OTH10")
  End Sub
  Private Sub MonthlyReportOutputButton_Click(sender As Object, e As EventArgs) Handles MonthlyReportOutputButton.Click
    'OpenForm("OTH02")
    OpenForm("OTH11")
  End Sub

  Private Sub PackingLabelPrintButton_Click(sender As Object, e As EventArgs) Handles PackingLabelPrintButton.Click
    OpenForm("OTH12")
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()

  End Sub

  Private Sub Form_OutPut_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    If Process.GetProcessesByName("Nohin").Count = 0 _
             AndAlso Process.GetProcessesByName("Result").Count = 0 Then
    Else
      ComMessageBox("実績確認検索画面を終了してから閉じてください。", "実績確認", typMsgBox.MSG_WARNING)
      e.Cancel = True
    End If
  End Sub
End Class