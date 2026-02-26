Imports System.Xml
Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 商品情報取得操作クラス
''' </summary>
Public Class clsPcaInputInfoSms
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PcaApp As New clsCommonPCA()
  Private _PostData As Dictionary(Of String, String)
#End Region
#End Region

#Region "コンストラクタ"
  Public Sub New(prmUserId As String _
                 , prmPassWord As String _
                 , prmProgramId As String _
                 , prmProgramName As String _
                 , prmDataAreaName As String _
                 , prmVersion As String)
    MyClass.New(prmVersion)

    With _PcaApp
      .UserID = prmUserId
      .PassWord = prmPassWord
      .ProgramId = prmProgramId
      .ProgramName = prmProgramName
      .DataAreaName = prmDataAreaName
    End With

  End Sub

  Public Sub New(prmVersion As String)

    Call ComSetDictionaryVal(_PostData, "BEVersion", prmVersion)
  End Sub

#End Region

#Region "メソッド"
#Region "パブリック"

  ''' <summary>
  ''' 売上伝票用商品情報取得
  ''' </summary>
  ''' <param name="prmCondition">商品検索条件構造体クラス</param>
  Public Function GetData(prmCondition As clsPcaInputInfoSmsCondition) As List(Of clsBEInputInfoSms)
    Dim tmpInputInfoSmsList As New List(Of clsBEInputInfoSms)
    Dim document As New XmlDocument
    Dim tmpRoot As XmlNodeList

    Try
      document.LoadXml(_PcaApp.GetData("InputInfoSms", prmCondition.XML))
      tmpRoot = document.GetElementsByTagName("BEInputInfoSms")

      For Each tmpXmlElement As XmlElement In tmpRoot
        Dim tmpInputInfoSms As New clsBEInputInfoSms()

        For Each tmpCn As XmlNode In tmpXmlElement
          tmpInputInfoSms.AddRowData(tmpCn.Name, tmpCn.InnerText)
        Next

        tmpInputInfoSmsList.Add(tmpInputInfoSms)
      Next


    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("売上伝票用商品情報の取得に失敗しました。")
    Finally
    End Try
    Return tmpInputInfoSmsList
  End Function

  Public Sub AddRowData(prmKey As String _
                      , prmVal As String)
    Call ComSetDictionaryVal(_PostData, prmKey, prmVal)

  End Sub

#End Region
#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
        If _PcaApp IsNot Nothing Then
          _PcaApp.Disconnect()
          _PcaApp.Dispose()
        End If


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
