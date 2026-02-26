Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbDateBase
  Inherits CmbBase

#Region "メンバ"

#Region "プライベート"
  ''' <summary>
  ''' 入力可能最大文字数
  ''' </summary>
  Private _MaxChar As Integer
#End Region

#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()
    '入力可能最大文字数設定
    _MaxChar = "2020/12/31".Length()
  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"

  ' コンボボックスソース抽出用
  Private Function CreateSql(Optional prmCode As String = "") As String
    Return lcCallBackCreateSql(prmCode)
  End Function

#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' キー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 数字8桁とバックスペースのみ入力可
  ''' </remarks>
  Private Sub CmbDateBase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    If Me.Text.Length >= _MaxChar And Me.SelectedText.Length <= 0 Then
      If e.KeyChar <> ControlChars.Back Then
        e.Handled = True
      End If
    Else
      If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) _
        AndAlso e.KeyChar <> "/"c _
        AndAlso e.KeyChar <> ControlChars.Back Then
        e.Handled = True
      End If
    End If

  End Sub

  Private Sub CmbDateBase_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
    Dim tmpDateText As String = String.Empty
    With Me
      If .Text.Length <= 0 Then
        If .AvailableBlank = False Then
          e.Cancel = True
        End If
      Else
        Try
          ' 入力された値を日付形式に変換
          tmpDateText = ComCreateDateText(.Text)
          .Text = ComCreateDateText(.Text)
          .SelectedValue = .Text

          ' 一覧に存在しない日付を入力された場合は、選択項目の先頭に追加する
          If .SelectedValue Is Nothing Then
            With DirectCast(.DataSource, DataTable)
              Dim tmpNewItem As DataRow = .NewRow
              tmpNewItem(0) = tmpDateText
              .Rows.InsertAt(tmpNewItem, 0)
              .AcceptChanges()
            End With
            .SelectedIndex = 0
          End If
        Catch ex As Exception
          e.Cancel = True
        End Try
      End If
    End With

  End Sub

  ''' <summary>
  ''' コンボボックスが件数0件の場合、Indexエラーを回避する
  ''' </summary>
  ''' <returns></returns>
  Public Overrides Property SelectedIndex() As Integer

    Get

      '処理
      Return MyBase.SelectedIndex

    End Get

    Set(ByVal Value As Integer)

      If (Me.Items.Count <> 0) Then
        '処理
        MyBase.SelectedIndex = Value
      End If

    End Set

  End Property

#End Region

End Class
