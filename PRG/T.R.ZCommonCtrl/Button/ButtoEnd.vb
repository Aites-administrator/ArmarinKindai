Imports T.R.ZCommonClass.clsCommonFnc

Public Class ButtonEnd
  Inherits BtnBase

  ' アプリケーション終了ボタン

#Region "コンストラクタ"
  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()

    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("ボタン押下で終了します。")

  End Sub

  Protected Overrides Sub InitLayout()

    '画像を設定
    If Me.Height <= 63 Then
      Me.Image = My.Resources.ButtonEndSmall
    Else
      Me.Image = My.Resources.ButtonEnd
    End If

  End Sub

  ''' <summary>
  '''  ボタンがクリックされたときに呼び出されるOnClickメソッドのオーバーライド。
  ''' </summary>
  ''' <param name="e"></param>
  Protected Overrides Sub OnClick(ByVal e As System.EventArgs)

    MyBase.OnClick(e)

    ' プロセス終了
    ProcessKill()

  End Sub


#End Region

End Class