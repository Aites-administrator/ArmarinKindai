Public Class TxtNumericBase
  Inherits TxtBase

  ' 数値入力専用テキストボックス

#Region "メンバ"
#Region "プライベート"
  Private _InputPoint As Boolean
#End Region
#End Region

#Region "コンストラクタ"
  Public Sub New()
    Me.New(10)
  End Sub

  Public Sub New(prmMaxChar As Integer _
                 , Optional prmInputPoint As Boolean = False)
    Me.ImeMode = ImeMode.Alpha        ' IMEモード設定(半角英数字)
    MyBase.SetMaxChar(prmMaxChar)     ' 入力可能最大文字数設定
    _InputPoint = prmInputPoint
    Me.TextAlign = HorizontalAlignment.Right
  End Sub

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' キー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNumericBase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    ' 数値とバックスペースのみ入力可
    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) _
      AndAlso e.KeyChar <> ControlChars.Back _
      AndAlso (_InputPoint = False OrElse e.KeyChar <> "."c OrElse SelectionStart > 3) Then
      e.Handled = True
    End If

  End Sub


#End Region

End Class
