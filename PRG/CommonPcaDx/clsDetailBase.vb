Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsDetailBase
  Implements IDisposable


#Region "メンバ"
#Region "プライベート"
  Private _PostData As New Dictionary(Of String, String)
#End Region
#End Region

#Region "メソッド"
  Public Sub AddRowData(prmKey As String _
                      , prmVal As String)
    Call ComSetDictionaryVal(_PostData, prmKey, prmVal)

  End Sub

  Public Function GetRowData(prmTarget As String) As String
    If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey(prmTarget) Then
      Return _PostData(prmTarget)
    Else
      Return String.Empty
    End If
  End Function
#End Region

#Region "プロパティー"

  ''' <summary>
  ''' 設定データをXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property XML As String
    Get
      Return ComDic2XmlText(_PostData)
    End Get
  End Property

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
      _PostData.Clear()
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  Protected Overrides Sub Finalize()
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(False)
    MyBase.Finalize()
  End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
