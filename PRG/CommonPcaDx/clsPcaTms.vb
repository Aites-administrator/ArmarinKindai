Imports System.Xml
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonCon.DbConnectData

''' <summary>
''' 得意先マスタ操作クラス
''' </summary>
Public Class clsPcaTms
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  ''' <summary>
  ''' PCA API操作クラス
  ''' </summary>
  Private _PcaApp As New clsCommonPCA()

  ''' <summary>
  ''' 送信データ
  ''' </summary>
  Private _PostData As Dictionary(Of String, String)

  ''' <summary>
  ''' 得意先マスタデータリスト
  ''' </summary>
  Private _TmsDetailList As New List(Of clsPcaTmsD)
#End Region
#End Region

#Region "プロパティー"

#Region "プライベート"

  ''' <summary>
  ''' 登録内容をXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Private ReadOnly Property XML As String
    Get
      Dim tmpXml As String = String.Empty

      tmpXml &= "<ArrayOfBEMasterTms>"

      For Each tmpTmsD As clsPcaTmsD In _TmsDetailList
        ' 商品明細追加
        tmpXml &= "<BEMasterTms>"
        tmpXml &= tmpTmsD.XML
        tmpXml &= "</BEMasterTms>"
      Next

      tmpXml &= "</ArrayOfBEMasterTms>"

      Return tmpXml
    End Get
  End Property
#End Region

#End Region

#Region "コンストラクタ"
  Public Sub New()

    With _PcaApp
      .ProgramId = PCAAPI_PG_ID
      .ProgramName = PCAAPI_PG_NAME
      .DataAreaName = PCAAPI_DATAAREANAME
      .UserID = PCAAPI_USERID
      .PassWord = PCAAPI_PASSWORD
      .Version = PCA_API_VERSION
    End With

    Call ComSetDictionaryVal(_PostData, "BEVersion", _PcaApp.Version)
  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' 得意先マスタ作成
  ''' </summary>
  Public Sub Create()
    Try
      _PcaApp.Create("MasterTms", Me.XML)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("得意先マスタの登録に失敗しました。")
    Finally
      For Each tmpTmsD As clsPcaTmsD In _TmsDetailList
        tmpTmsD.Dispose()
      Next
      _TmsDetailList.Clear()
    End Try
  End Sub

  ''' <summary>
  ''' 得意先マスタ更新
  ''' </summary>
  Public Sub Update()
    Try
      _PcaApp.Modify("MasterTms", Me.XML)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("得意先マスタの登録に失敗しました。")
    Finally
      For Each tmpSmsD As clsPcaTmsD In _TmsDetailList
        tmpSmsD.Dispose()
      Next
      _TmsDetailList.Clear()
    End Try
  End Sub

  ''' <summary>
  ''' 得意先マスタ取得
  ''' </summary>
  ''' <returns></returns>
  Public Function GetData(prmSearchCondition As clsPcaTmsD) As List(Of clsPcatmsD)
    Dim tmpTmsDList As New List(Of clsPcaTmsD)
    Dim tmpXml As String = prmSearchCondition.XML
    Dim tmpRetVal As String = String.Empty
    Dim document As New XmlDocument
    Dim tmpRoot As XmlNodeList

    tmpXml = "<BEMasterTmsCondition>" & tmpXml & "</BEMasterTmsCondition>"

    Try
      document.LoadXml(_PcaApp.GetData("MasterTms", tmpXml))

      tmpRoot = document.GetElementsByTagName("BEMasterTms")

      For Each tmpXmlElement As XmlElement In tmpRoot
        Dim tmpTmsDetail As New clsPcaTmsD

        For Each tmpCn As XmlNode In tmpXmlElement
          tmpTmsDetail.AddRowData(tmpCn.Name, tmpCn.InnerText)
        Next

        tmpTmsDList.Add(tmpTmsDetail)
      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      prmSearchCondition.Dispose()
      Throw New Exception("PCA得意先マスタの取得に失敗しました。")
    Finally
    End Try


    Return tmptmsDList

  End Function

  ''' <summary>
  ''' 得意先明細追加
  ''' </summary>
  ''' <param name="prmTmsD">得意先明細</param>
  Public Sub AddDetail(prmTmsD As clsPcaTmsD)
    _TmsDetailList.Add(prmTmsD)
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
