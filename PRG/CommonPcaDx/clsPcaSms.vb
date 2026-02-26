Imports System.Xml
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonCon.DbConnectData

''' <summary>
''' 商品マスタ操作クラス
''' </summary>
Public Class clsPcaSms
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PcaApp As New clsCommonPCA()
  Private _PostData As Dictionary(Of String, String)
  Private _SmsDetailList As New List(Of clsPcaSmsD)
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

      tmpXml &= "<ArrayOfBEMasterSms>"

      For Each tmpSmsD As clsPcaSmsD In _SmsDetailList
        ' 商品明細追加
        tmpXml &= "<BEMasterSms>"
        tmpXml &= tmpSmsD.XML
        tmpXml &= "</BEMasterSms>"
      Next

      tmpXml &= "</ArrayOfBEMasterSms>"

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
  ''' 商品マスタ作成
  ''' </summary>
  Public Sub Create()
    Try
      _PcaApp.Create("MasterSms", Me.XML)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      'Call ComWriteLog(Me.XML, "d:\errr.text")
      Throw New Exception("商品マスタの登録に失敗しました。")
    Finally
      For Each tmpSmsD As clsPcaSmsD In _SmsDetailList
        tmpSmsD.Dispose()
      Next
      _SmsDetailList.Clear()
    End Try
  End Sub

  ''' <summary>
  ''' 商品マスタ更新
  ''' </summary>
  Public Sub Update()
    Try
      _PcaApp.Modify("MasterSms", Me.XML)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("商品マスタの登録に失敗しました。")
    Finally
      For Each tmpSmsD As clsPcaSmsD In _SmsDetailList
        tmpSmsD.Dispose()
      Next
      _SmsDetailList.Clear()
    End Try
  End Sub

  ''' <summary>
  ''' 商品マスタ取得
  ''' </summary>
  ''' <returns></returns>
  Public Function GetData(prmSearchCondition As clsPcaSmsD) As List(Of clsPcaSmsD)
    Dim tmpSmsDList As New List(Of clsPcaSmsD)
    Dim tmpXml As String = prmSearchCondition.XML
    Dim tmpRetVal As String = String.Empty
    Dim document As New XmlDocument
    Dim tmpRoot As XmlNodeList

    tmpXml = "<BEMasterSmsCondition>" & tmpXml & "</BEMasterSmsCondition>"

    Try
      document.LoadXml(_PcaApp.GetData("MasterSms", tmpXml))

      tmpRoot = document.GetElementsByTagName("BEMasterSms")

      For Each tmpXmlElement As XmlElement In tmpRoot
        Dim tmpSmsDetail As New clsPcaSmsD

        For Each tmpCn As XmlNode In tmpXmlElement
          tmpSmsDetail.AddRowData(tmpCn.Name, tmpCn.InnerText)
        Next

        tmpSmsDList.Add(tmpSmsDetail)
      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      prmSearchCondition.Dispose()
      Throw New Exception("PCA商品マスタの取得に失敗しました。")
    End Try


    Return tmpSmsDList

  End Function

  ''' <summary>
  ''' 商品明細追加
  ''' </summary>
  ''' <param name="prmSmsD">商品明細</param>
  Public Sub AddDetail(prmSmsD As clsPcaSmsD)
    _SmsDetailList.Add(prmSmsD)
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
