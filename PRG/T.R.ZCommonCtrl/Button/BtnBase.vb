''' <summary>
''' Btn操作クラス
''' </summary>
''' 
Public Class BtnBase

  Inherits Button

#Region "プライベート"
  ' メッセージ出力ラベル
  Private _msgLabel As Label
  ' メッセージ出力ラベルテキスト
  Private _msgLabelText As String


#End Region

#Region "コンストラクタ"
  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()

    'イメージがコントロールのテキストの上部に表示されるように指定します。
    Me.TextImageRelation = TextImageRelation.ImageAboveText

  End Sub


#End Region

#Region "イベントプロシージャー"
  Private Sub BtnBase_OnEnter(sender As Object, e As EventArgs) Handles Me.Enter

    'メッセージラベルの定義が未設定の場合
    If _msgLabel Is Nothing Then
      Exit Sub
    Else
      'メッセージラベルへのメッセージの表示
      _msgLabel.Text = _msgLabelText
    End If

  End Sub

  ''' <summary>
  ''' メッセージラベルの定義
  ''' </summary>
  ''' <param name="msgLabel">メッセージを表示するラベル情報</param>
  Public Sub SetMsgLabel(msgLabel As Label)

    _msgLabel = msgLabel

  End Sub

  ''' <summary>
  ''' メッセージラベルへのメッセージ表示
  ''' </summary>
  ''' <param name="msg">メッセージ</param>
  Public Sub SetMsgLabelText(msg As String)

    _msgLabelText = msg

  End Sub
#End Region

End Class
