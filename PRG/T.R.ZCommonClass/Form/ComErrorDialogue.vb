Public Class ComErrorDialogue
  '--------------------------------
  '     汎用エラーダイアログ
  '--------------------------------


#Region "メンバ"
  ''' <summary>
  ''' エラーメッセージ
  ''' </summary>
  Private _ErrorMsg As String
#End Region

#Region "メソッド"

  ''' <summary>
  ''' 起動時処理
  ''' </summary>
  ''' <param name="prmErrorMsg">表示メッセージ</param>
  ''' <returns></returns>
  Public Function ShowErrorMsg(prmErrorMsg As String, prmTitle As String) As DialogResult
    Dim ret As DialogResult = MessageBoxButtons.OK

    Me.Text = prmTitle
    _ErrorMsg = prmErrorMsg

    Me.ShowDialog()

    Return ret
  End Function

#End Region

#Region "イベントプロシージャー"
#Region "フォーム関連"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ComErrorDialogue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.txtErrorMsg.Text = Me._ErrorMsg
    Me.ActiveControl = Me.txtErrorMsg
  End Sub



#End Region

#Region "ボタン関連"
  ''' <summary>
  ''' OKボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    Me.Close()
  End Sub
#End Region
#End Region

End Class