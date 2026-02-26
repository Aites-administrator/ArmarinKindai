Imports System.Xml
Imports T.R.ZCommonClass.clsCommonFnc


''' <summary>
''' 売上伝票検索条件構造体クラス
''' </summary>
Public Class clsBEInputSYKCondition
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PostData As New Dictionary(Of String, String)
#End Region
#End Region

#Region "プロパティー"

#Region "パブリック"

  ''' <summary>
  ''' 設定データをXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property XML As String
    Get
      Dim tmpXml As String = ""
      tmpXml &= "<BEInputSYKCondition>"
      tmpXml &= ComDic2XmlText(_PostData)
      tmpXml &= "</BEInputSYKCondition>"

      Return tmpXml
    End Get
  End Property

  ''' <summary>
  ''' 売上日 (単体指定)
  ''' </summary>
  Public WriteOnly Property 売上日 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Uriagebi", value)
    End Set
  End Property

  ''' <summary>
  ''' 得意先コード（単体）
  ''' </summary>
  Public WriteOnly Property 得意先コード As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TokuisakiCode", value)
    End Set
  End Property

#End Region

#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
      End If

      ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
      ' TODO: 大きなフィールドを null に設定します。
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  'Protected Overrides Sub Finalize()
  '    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    ' GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
