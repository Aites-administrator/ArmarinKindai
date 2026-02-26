Imports System.Xml
Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsPcaInputInfoTms
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PcaApp As New clsCommonPCA()
#End Region
#End Region

#Region "コンストラクタ"
  Public Sub New(prmUserId As String _
                 , prmPassWord As String _
                 , prmProgramId As String _
                 , prmProgramName As String _
                 , prmDataAreaName As String)
    With _PcaApp
      .UserID = prmUserId
      .PassWord = prmPassWord
      .ProgramId = prmProgramId
      .ProgramName = prmProgramName
      .DataAreaName = prmDataAreaName
    End With

  End Sub

#End Region

#Region "メソッド"
#Region "パブリック"

  ''' <summary>
  ''' 伝票入力用得意先情報取得
  ''' </summary>
  ''' <param name="prmCustomerCode"></param>
  ''' <returns></returns>
  Public Function GetData(prmCustomerCode As String) As List(Of clsBEInputInfoTms)
    Dim tmpInputInfoTmsList As New List(Of clsBEInputInfoTms)
    Dim document As New XmlDocument
    Dim tmpRoot As XmlNodeList

    Try
      document.LoadXml(_PcaApp.GetData("InputInfoTms?Code=" & prmCustomerCode))
      tmpRoot = document.GetElementsByTagName("BEInputInfoTms")

      For Each tmpXmlElement As XmlElement In tmpRoot
        Dim tmpInputInfoTms As New clsBEInputInfoTms()

        For Each tmpCn As XmlNode In tmpXmlElement
          tmpInputInfoTms.AddRowData(tmpCn.Name, tmpCn.InnerText)
        Next

        tmpInputInfoTmsList.Add(tmpInputInfoTms)
      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("売上伝票用得意先情報の取得に失敗しました。")

    End Try

    Return tmpInputInfoTmsList
  End Function
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
