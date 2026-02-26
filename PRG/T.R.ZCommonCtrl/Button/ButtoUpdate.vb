Public Class ButtonUpdate
  Inherits BtnBase

  ' アプリケーション終了ボタン

#Region "コンストラクタ"

  Public Sub New()

    '画像を設定
    Me.Image = My.Resources.ButtonUpdate
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("ボタン押下で更新します。")
  End Sub

#End Region

End Class