Imports T.R.ZCommonClass

Public Class CmbMstBase
  Inherits CmbBase

#Region "メンバ"

#Region "プライベート"
  Private _CodeFormat As String

  Private _SkipChkCode As Boolean = False

#End Region

#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ(ダミー)
  ''' </summary>
  ''' <remarks>ユーザーコントロールには引数なしのコンストラクタが必要です</remarks>
  Public Sub New()
  End Sub

  Public Sub New(prmCodeFormat As String)
    _CodeFormat = prmCodeFormat
  End Sub

#End Region

#Region "プロパティー"
#Region "パブリック"

  ''' <summary>
  ''' コード存在確認のスキップ有無
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>デフォルト False</remarks>
  Public Property SkipChkCode As Boolean
    Get
      Return _SkipChkCode
    End Get
    Set(value As Boolean)
      _SkipChkCode = value
    End Set
  End Property
#End Region
#End Region

#Region "メソッド"

#Region "プライベート"

  ' コード存在確認
  Private Function ChkCode(prmCode As String) As Boolean
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    If (lcCallBackCreateSql Is Nothing) Then
      Return FALSE
    End If

    tmpDb.GetResult(tmpDt, lcCallBackCreateSql(prmCode))
    Return (1 <= tmpDt.Rows.Count)
  End Function

  Private Sub CmbMstBase_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
    With Me
      If .Text.Length <= 0 Then
        .SelectedIndex = -1
      Else
        If (SkipChkCode()) Then
          ' 表示内容設定
          .Text = .Text
        Else
          If .Text.Length <= Len(_CodeFormat) Then
            ' マスタ検索
            If ChkCode(.Text) Then
              ' 表示内容設定
              .SelectedValue = .Text
              .Text = .Text
            Else
              ' 全選択
              Me.Select(.Text.Length, 0)
              e.Cancel = True
            End If
          End If
        End If
      End If
    End With

  End Sub

#End Region

  ''' <summary>
  ''' 数値とバックスペースのみ入力可
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TTxtDateBase_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    If e.KeyCode = Keys.Escape Then
      If Me.DroppedDown Then
        Me.DroppedDown = False ' リストを閉じる
        e.Handled = True
      End If
    End If
  End Sub

#End Region


End Class
