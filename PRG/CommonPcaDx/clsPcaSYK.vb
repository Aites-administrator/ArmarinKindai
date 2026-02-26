Imports System.Xml
Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 売上伝票操作クラス
''' </summary>
Public Class clsPcaSYK
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PcaApp As New clsCommonPCA()
  Private _PostData As Dictionary(Of String, String)
  Private _SykHeader As New List(Of clsPcaSYKH)
  Private _SykDetailList As New Dictionary(Of clsPcaSYKH, List(Of clsPcaSYKD))
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

      tmpXml &= "<ArrayOfBEInputSYK>"

      For Each tmpSykh As clsPcaSYKH In _SykHeader
        ' 伝票ヘッダー追加
        tmpXml &= "<BEInputSYK>"
        tmpXml &= ComDic2XmlText(_PostData)
        tmpXml &= "<InputSYKH>" & tmpSykh.XML & "</InputSYKH>"

        ' 伝票明細追加
        tmpXml &= "<InputSYKDList>"
        For Each tmpSykd As clsPcaSYKD In _SykDetailList(tmpSykh)
          tmpXml &= "<BEInputSYKD>" & tmpSykd.XML & "</BEInputSYKD>"
        Next
        tmpXml &= "</InputSYKDList>"
        tmpXml &= "</BEInputSYK>"
      Next

      tmpXml &= "</ArrayOfBEInputSYK>"

      Return tmpXml
    End Get
  End Property
#End Region

#Region "パブリック"
  ''' <summary>
  ''' 売上伝票一覧
  ''' </summary>
  ''' <returns>クラス内に登録されている売上伝票の一覧</returns>
  Public ReadOnly Property SalesList As Dictionary(Of clsPcaSYKH, List(Of clsPcaSYKD))
    Get
      Return _SykDetailList
    End Get
  End Property

#End Region


#End Region

#Region "コンストラクタ"
  Public Sub New(prmUserId As String _
                 , prmPassWord As String _
                 , prmProgramId As String _
                 , prmProgramName As String _
                 , prmDataAreaName As String)
    MyClass.New

    With _PcaApp
      .UserID = prmUserId
      .PassWord = prmPassWord
      .ProgramId = prmProgramId
      .ProgramName = prmProgramName
      .DataAreaName = prmDataAreaName
    End With

  End Sub

  Public Sub New()
    Call ComSetDictionaryVal(_PostData, "BEVersion", "800")
  End Sub
#End Region

#Region "メソッド"
#Region "パブリック"

  ''' <summary>
  ''' 売上伝票作成
  ''' </summary>
  Public Sub Create()
    Try
      Call SortDetail()       ' 明細行並び替え
      Call SetInputInfoSms()  ' 伝票作成情報を取得し明細にセットする
      _PcaApp.Create("InputSYK?CalcTotal=True&CalcTax=True&CalcDetailTax=True&TransactionScope=Whole", Me.XML)
      'ComWriteLog(Me.XML, "d:\pcaapipost.xml")
    Catch ex As Exception
      Dim msg As String = ex.Message

      ' 改行コードより後ろを取得
      If msg.IndexOf(vbCrLf) > 0 Then
        msg = msg.Substring(msg.IndexOf(vbCrLf) + Len(vbCrLf))
      End If

      ' 改行コードより前を取得
      If msg.IndexOf(vbCrLf) > 0 Then
        msg = msg.Substring(0, msg.IndexOf(vbCrLf) - Len(vbCrLf) + 1)
      End If

      Call ComWriteErrLog(ex)
      Throw New Exception(msg)
    Finally
      For Each tmpSYkH As clsPcaSYKH In _SykHeader
        For Each tmpSykD As clsPcaSYKD In _SykDetailList(tmpSYkH)
          tmpSykD.Dispose()
        Next
        tmpSYkH.Dispose()
      Next
      _SykDetailList.Clear()
      _SykHeader.Clear()
    End Try
  End Sub

  Public Sub Modify()
    Try
      Call SetInputInfoSms()  ' 伝票作成情報を取得し明細にセットする
      Call EraseUnchangeableData()
      _PcaApp.Modify("InputSYK?CalcTotal=True&CalcTax=True&CalcDetailTax=True&TransactionScope=Whole", Me.XML)
      'ComWriteLog(Me.XML, "d:\pcaapipost.xml")
    Catch ex As Exception
      Dim msg As String = ex.Message

      ' 改行コードより後ろを取得
      If msg.IndexOf(vbCrLf) > 0 Then
        msg = msg.Substring(msg.IndexOf(vbCrLf) + Len(vbCrLf))
      End If

      ' 改行コードより前を取得
      If msg.IndexOf(vbCrLf) > 0 Then
        msg = msg.Substring(0, msg.IndexOf(vbCrLf) - Len(vbCrLf) + 1)
      End If

      Call ComWriteErrLog(ex)
      Throw New Exception(msg)
    Finally
      For Each tmpSYkH As clsPcaSYKH In _SykHeader
        For Each tmpSykD As clsPcaSYKD In _SykDetailList(tmpSYkH)
          tmpSykD.Dispose()
        Next
        tmpSYkH.Dispose()
      Next
      _SykDetailList.Clear()
      _SykHeader.Clear()
    End Try
  End Sub
  ''' <summary>
  ''' 売上伝票明細行追加
  ''' </summary>
  ''' <param name="prmSykd">売上伝票明細</param>
  Public Sub AddDetail(prmSykd As clsPcaSYKD)
    _SykDetailList(_SykHeader.Last()).Add(prmSykd)
  End Sub

  ''' <summary>
  ''' 売上伝票ヘッダー追加
  ''' </summary>
  ''' <param name="prmSykH"></param>
  Public Sub AddHeader(prmSykH As clsPcaSYKH)
    Me._SykHeader.Add(prmSykH)
    Me._SykDetailList.Add(prmSykH, New List(Of clsPcaSYKD))
  End Sub

  ''' <summary>
  ''' 売上伝票検索
  ''' </summary>
  ''' <param name="prmSearchCondition">検索条件クラス</param>
  ''' <returns>売上伝票操作クラス</returns>
  Public Function GetData(prmSearchCondition As clsBEInputSYKCondition) As clsPcaSYK

    Dim ret As New clsPcaSYK(_PcaApp.UserID _
                            , _PcaApp.PassWord _
                            , _PcaApp.ProgramId _
                            , _PcaApp.ProgramName _
                            , _PcaApp.DataAreaName)

    Dim tmpXml As String = prmSearchCondition.XML
    Dim tmpRetVal As String = String.Empty
    Dim document As New XmlDocument

    Dim tmpRoot As XmlNodeList

    Try
      document.LoadXml(_PcaApp.GetData("InputSYK", tmpXml))

      tmpRoot = document.GetElementsByTagName("BEInputSYK")

      For Each tmpXmlElement As XmlElement In tmpRoot


        '------------------------------------
        '         伝票ヘッダー作成
        '------------------------------------
        Dim tmpSYKH As New clsPcaSYKH()

        For Each tmpHeader As XmlElement In tmpXmlElement.GetElementsByTagName("InputSYKH")
          For Each tmpVal As XmlNode In tmpHeader
            tmpSYKH.AddRowData(tmpVal.Name, tmpVal.InnerText.ToString())
          Next
        Next

        ret.AddHeader(tmpSYKH)

        '------------------------------------
        '         伝票明細作成
        '------------------------------------
        For Each tmpDetail As XmlElement In tmpXmlElement.GetElementsByTagName("BEInputSYKD")
          Dim tmpSYKD As New clsPcaSYKD
          For Each tmpVal As XmlNode In tmpDetail
            tmpSYKD.AddRowData(tmpVal.Name, tmpVal.InnerText.ToString())
          Next

          ret.AddDetail(tmpSYKD)
        Next

      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("売上伝票の取得に失敗しました。")
    End Try


    Return ret

  End Function

  ''' <summary>
  ''' 内部データより伝票番号に一致する売上伝票オブジェクトを取得する
  ''' </summary>
  ''' <param name="prmSlipNumber">対象の伝票番号</param>
  ''' <returns>売上伝票オブジェクト</returns>
  ''' <remarks>APIからでは無く、オブジェクト内部に保持しているデータより取得</remarks>
  Public Function GetSlipByNumber(prmSlipNumber As String) As clsPcaSYK
    Dim ret As New clsPcaSYK

    For Each tmpSykH As clsPcaSYKH In Me.SalesList.Keys
      If tmpSykH.伝票No = prmSlipNumber Then
        ret.AddHeader(tmpSykH)
        For Each tmpSykD As clsPcaSYKD In Me.SalesList(tmpSykH)
          ret.AddDetail(tmpSykD)
        Next
        Exit For
      End If
    Next

    Return ret
  End Function
#End Region

#Region "プライベート"
  Private Sub EraseUnchangeableData()
    For Each tmpSykH As clsPcaSYKH In Me.SalesList.Keys
      tmpSykH.AddRowData("TorokuUser", "")
      tmpSykH.AddRowData("TorokuProgram", "")
      tmpSykH.AddRowData("Torokubi", "")
      tmpSykH.AddRowData("KosinUser", "")
      tmpSykH.AddRowData("KosinProgram", "")
      For Each tmpSykD As clsPcaSYKD In Me.SalesList(tmpSykH)
        tmpSykD.AddRowData("HeaderId", "")
        tmpSykD.AddRowData("Edaban", "")
      Next
    Next
  End Sub


  ''' <summary>
  ''' 伝票作成情報を追記する
  ''' </summary>
  ''' <remarks>
  ''' 以下の項目を[伝票入力用商品情報]から取得し設定する
  '''・税率
  '''・原価税率
  '''・売上税種別
  '''・原価税種別
  '''・税区分
  '''・税込区分
  '''・原価税込区分
  '''・数量少数桁
  '''[伝票入力用得意先情報]から取得した端数処理設定を使用し金額を計算・設定する
  ''' </remarks>
  Private Sub SetInputInfoSms()
    Dim tmpPcaInputInforSMS = New clsPcaInputInfoSms(_PcaApp.UserID _
                                                          , _PcaApp.PassWord _
                                                          , _PcaApp.ProgramId _
                                                          , _PcaApp.ProgramName _
                                                          , _PcaApp.DataAreaName _
                                                          , "500")
    Dim tmpCondition = New clsPcaInputInfoSmsCondition()

    Try
      ' 売上日、得意先コード、商品コードより伝票作成情報を取得し明細にセットする
      For Each tmpSykH As clsPcaSYKH In _SykDetailList.Keys
        Dim tmpInputInfoTMS = GetInputInfoTms(tmpSykH.得意先コード)

        tmpCondition.伝票日付 = tmpSykH.売上日
        tmpCondition.得意先コード = tmpSykH.得意先コード
        For index As Integer = 0 To _SykDetailList(tmpSykH).Count - 1
          With _SykDetailList(tmpSykH)(index)
            ' 新規、もしくは更新伝票への行追加なら各種情報を取得
            If .明細SEQ = "" OrElse .明細SEQ = "0" Then
              tmpCondition.商品コード = _SykDetailList(tmpSykH)(index).商品コード
              Dim tmpInputInfoSmsList = tmpPcaInputInforSMS.GetData(tmpCondition)

              .税率 = tmpInputInfoSmsList(0).税率
              .原価税率 = tmpInputInfoSmsList(0).原価税率
              .売上税種別 = tmpInputInfoSmsList(0).税種別
              .原価税種別 = tmpInputInfoSmsList(0).原価税種別
              .税区分 = tmpInputInfoSmsList(0).税区分
              .税込区分 = tmpInputInfoSmsList(0).税込区分
              .原価税込区分 = tmpInputInfoSmsList(0).原価税込区分
              '.数量小数桁 = tmpInputInfoSmsList(0).数量小数桁
              .金額 = CalcDetailTotal(_SykDetailList(tmpSykH)(index), tmpInputInfoTMS)
            End If
          End With
        Next

      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("伝票作成情報の設定に失敗しました。")
    End Try
  End Sub

  Private Function GetInputInfoTms(prmCustomerCode As String) As clsBEInputInfoTms
    Dim tmpInputInfoTMS As New clsBEInputInfoTms
    Dim tmpPcaInputInforTMS = New clsPcaInputInfoTms(_PcaApp.UserID _
                                                          , _PcaApp.PassWord _
                                                          , _PcaApp.ProgramId _
                                                          , _PcaApp.ProgramName _
                                                          , _PcaApp.DataAreaName)

    Try
      tmpInputInfoTMS = tmpPcaInputInforTMS.GetData(prmCustomerCode)(0)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("得意先情報の取得に失敗しました。")
    Finally
      tmpPcaInputInforTMS.Dispose()
    End Try

    Return tmpInputInfoTMS
  End Function

  Private Function CalcDetailTotal(prmSYKD As clsPcaSYKD _
                                  , prmInputInfoTms As clsBEInputInfoTms) As String
    Dim tmpGosa As Decimal

    ' 計算補正値取得
    If Decimal.Parse(prmSYKD.数量) >= 0 Then
      tmpGosa = 0.01
    Else
      tmpGosa = -0.01
    End If

    ' 売価計算
    Dim tmpTanka As Decimal = Decimal.Parse(prmSYKD.単価)
    Dim tmpSuryo As Decimal = Decimal.Parse(prmSYKD.数量)
    Dim tmpKingaku As Decimal
    Select Case prmInputInfoTms.金額端数
      Case "0"
        tmpKingaku = Fix(tmpTanka * tmpSuryo + tmpGosa)
      Case "1"
        tmpKingaku = Fix(tmpTanka * tmpSuryo + 0.999)
      Case "2"
        tmpKingaku = Math.Truncate(tmpTanka * tmpSuryo + tmpGosa)
    End Select

    Return tmpKingaku.ToString()
  End Function

  ''' <summary>
  ''' 明細ソート
  ''' </summary>
  ''' <remarks>
  '''   色（枝番）、売上項目3（食肉標準コード）、商品コード、サイズ（左右）の昇順に明細行を並び替える
  '''   ※京都協同管理特別仕様
  ''' </remarks>
  Private Sub SortDetail()

    For Each tmpSykH As clsPcaSYKH In _SykDetailList.Keys
      Dim tmpLoopEnd = _SykDetailList(tmpSykH).Count - 2
      Dim bShift = True

      While bShift
        bShift = False
        For tmpStartIdx = 0 To tmpLoopEnd
          For idx As Integer = tmpStartIdx To tmpLoopEnd
            With _SykDetailList(tmpSykH)
              Dim tmpEdabanCurrent = .ElementAt(idx).色.PadLeft(8, "0")
              Dim tmpEdabanNext = .ElementAt(idx + 1).色.PadLeft(8, "0")
              If tmpEdabanCurrent > tmpEdabanNext Then
                Call ShiftDetail(_SykDetailList(tmpSykH), idx, (idx + 1))
                bShift = True
              ElseIf tmpEdabanCurrent = tmpEdabanNext Then
                If .ElementAt(idx).売上項目3 > .ElementAt(idx + 1).売上項目3 Then
                  Call ShiftDetail(_SykDetailList(tmpSykH), idx, (idx + 1))
                  bShift = True
                ElseIf .ElementAt(idx).売上項目3 = .ElementAt(idx + 1).売上項目3 Then
                  If .ElementAt(idx).商品コード > .ElementAt(idx + 1).商品コード Then
                    Call ShiftDetail(_SykDetailList(tmpSykH), idx, (idx + 1))
                    bShift = True
                  ElseIf .ElementAt(idx).商品コード = .ElementAt(idx + 1).商品コード Then
                    If .ElementAt(idx).サイズ > .ElementAt(idx + 1).サイズ Then
                      Call ShiftDetail(_SykDetailList(tmpSykH), idx, (idx + 1))
                      bShift = True
                    End If

                  End If

                End If
              End If
            End With
          Next
        Next
      End While
    Next

    ' 商品項目3は並び替えにのみ使用の為、送信前に削除
    ' 枝番0は伝票に表示しない
    For Each tmpSykH As clsPcaSYKH In _SykDetailList.Keys
      For index As Integer = 0 To _SykDetailList(tmpSykH).Count - 1
        With _SykDetailList(tmpSykH)(index)
          .売上項目3 = ""
          If (.色 = "0") Then
            .色 = ""
          End If
        End With
      Next
    Next

  End Sub

  Private Sub ShiftDetail(prmTargetList As List(Of clsPcaSYKD), prmIdxA As Long, prmIdxB As Long)
    Dim tmpSortBuff = prmTargetList(prmIdxA)

    prmTargetList(prmIdxA) = prmTargetList(prmIdxB)
    prmTargetList(prmIdxB) = tmpSortBuff

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

        If _SykHeader IsNot Nothing Then
          For Each tmpSYkH As clsPcaSYKH In _SykHeader
            For Each tmpSykD As clsPcaSYKD In _SykDetailList(tmpSYkH)
              tmpSykD.Dispose()
            Next
            tmpSYkH.Dispose()
          Next
          _SykHeader = Nothing
          _SykDetailList = Nothing
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
