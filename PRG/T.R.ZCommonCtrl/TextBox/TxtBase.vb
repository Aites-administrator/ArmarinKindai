Public Class TxtBase
  Inherits TextBox

#Region "メンバ"

#Region "プライベート"
  ' フォーカス取得フラグ
  ' マウスでのフォーカス移動時の全選択に使用
  Private _OnFocus As Boolean

  ' メッセージ出力ラベル
  Private _msgLabel As Label
  ' メッセージ出力ラベルテキスト
  Private _msgLabelText As String

  ''' <summary>
  ''' 入力可能最大文字数
  ''' </summary>
  Private _MaxChar As Integer

  ''' <summary>
  ''' 最終入力テキスト
  ''' </summary>
  Private _LastText As String
#End Region

#Region "パブリック"

  Delegate Sub CallBackValidated(sender As Object, e As EventArgs)
  Public lcCallBackValidated As CallBackValidated = Nothing

  Delegate Sub CallBackSetText()
  Public lcCallBackSetText As CallBackSetText = Nothing

#End Region

#End Region

#Region "プロパティー"

#Region "パブリック"
  ''' <summary>
  ''' 最終入力テキスト
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>
  '''   更新前に最終に入力されていたテキスト
  ''' </remarks>
  Public ReadOnly Property LastText As String
    Get
      Return _LastText
    End Get
  End Property

  Public Overrides Property Text As String
    Get
      Return MyBase.Text
    End Get
    Set(value As String)
      MyBase.Text = value

      If lcCallBackSetText IsNot Nothing Then
        Call lcCallBackSetText()
      End If

      If _LastText <> value Then
        ' 最終入力テキスト更新
        _LastText = value
      End If
    End Set
  End Property

#End Region

#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()
    Me.New(0)
  End Sub

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  ''' <param name="prmMaxChar">入力可能最大文字数</param>
  Public Sub New(prmMaxChar As Integer)
    _OnFocus = False
    _MaxChar = prmMaxChar
    _LastText = String.Empty
  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
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

  Public Function GetMaxChar() As Integer
    Return _MaxChar
  End Function

  Public Sub SetMaxChar(prmMaxChar As Integer)
    _MaxChar = prmMaxChar
  End Sub
#End Region

#End Region

#Region "イベントプロシージャー"
  Private Sub TxtBase_MousUp(sender As Object, e As EventArgs) Handles Me.MouseUp
    If _OnFocus Then
      _OnFocus = False
      sender.SelectAll()
    End If
  End Sub

  Private Sub TxtBase_OnEnter(sender As Object, e As EventArgs) Handles Me.Enter
    sender.SelectAll()
    _OnFocus = True

    'メッセージラベルの定義が未設定の場合
    If _msgLabel Is Nothing Then
      Exit Sub
    Else
      'メッセージラベルへのメッセージの表示
      _msgLabel.Text = _msgLabelText
    End If

  End Sub

  ''' <summary>
  ''' キー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtBase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    ' 入力可能最大文字数が設定されている場合は最大文字数までの入力を可能とする
    If Me.Text.Length >= _MaxChar AndAlso _MaxChar > 0 AndAlso Me.SelectedText.Length < _MaxChar Then
      If e.KeyChar <> ControlChars.Back Then
        e.Handled = True
      End If
    End If

  End Sub

  ''' <summary>
  ''' 更新後処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub EditTxtValidated(sender As Object, e As EventArgs) Handles Me.Validated
    Dim tmpValue As String = DirectCast(sender, TextBox).Text

    If lcCallBackValidated IsNot Nothing Then
      Call lcCallBackValidated(sender, e)
    End If

    If _LastText <> tmpValue Then
      ' 最終入力テキスト更新
      _LastText = tmpValue

    End If

  End Sub

#End Region

End Class
