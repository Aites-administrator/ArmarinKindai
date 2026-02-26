Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsSqlServer
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonCon.DbConnectData
Imports ClsPrintingProcess
Imports CommonPcaDx
Imports Common


Public Class Form_SelectPrint
  Implements IDgvForm02
  ' 
  ' VisualStudioのツール>コード スニペット マネージャーの​インポートボタンで登録します
  '

#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "SelectPrint"
  Private Const PRG_TITLE As String = "選択一括発行画面"
  Private Const TBL_NAME As String = "TBL_NEW_DENNO"
#End Region
#End Region

#Region "変数定義"
#Region "プライベート"
  Private _DeliveryDate As String = Nothing
#End Region
#End Region


#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_SelectPrint, AddressOf ComRedisplay)
  End Sub
#End Region

#Region "データグリッドビュー２つの画面用操作関連共通"
  ' 得意先一覧DataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView
  ' 出荷明細一覧DataGridViewオブジェクト格納先
  Private DG2V2 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm02() Implements IDgvForm02.InitForm02

    ' 得意先一覧DataGridViewオブジェクトの設定
    DG2V1 = Me.DataGridView1
    ' 出荷明細一覧DataGridViewオブジェクトの設定
    DG2V2 = Me.DataGridView2

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1(), New clsDataGridSelecter(New List(Of String)({"DenNo"})))

    ' 得意先一覧設定
    With DG2V1

      With Controlz(.Name)
        ' 固定列設定
        '.FixedRow = 2

        ' １つ目のDataGridViewオブジェクトの検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])

        ' １つ目のDataGridViewオブジェクトの編集可能列設定
        .EditColumnList = CreateGrid2EditCol1()
      End With
    End With

    Call InitGrid(DG2V2 _
                  , CreateGrid2Src2() _
                  , CreateGrid2Layout2() _
                  , New clsDataGridSelecter(New List(Of String)({"NohinDay", "DenNo"}) _
                                            , prmSelectingCondition:=New Dictionary(Of String, String)() From {{"POST_STAT", "未完了"}}))

    ' 出荷明細一覧設定
    With DG2V2

      With Controlz(.Name)
        ' 固定列設定
        '.FixedRow = 2

        ' ２つ目のDataGridViewオブジェクトの検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])

        ' ２つ目のDataGridViewオブジェクトの編集可能列設定
        .EditColumnList = CreateGrid2EditCol2()
      End With
    End With

  End Sub

  ''' <summary>
  ''' 得意先一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1
    Dim sql As String = String.Empty


    '    sql &= " SELECT IIF(NKBN = 1 ,  IIF(NDEN = 0  , '未完了' ,  '完了') , IIF(DEN = 0 , '未完了' , '完了')) as POST_STAT "
    sql &= " SELECT IIF(URIAGE.DENPYOUNO is null,'未完了','完了') AS POST_STAT "
    sql &= "      , NohinDay "
    sql &= "      , TokuiCD "
    sql &= "      , TokuiNM "
    sql &= "      , IIF(URIAGE.DENPYOUNO is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2) DenNo "
    sql &= " FROM TRN_JISSEKI "
    sql &= " LEFT JOIN URIAGE "
    sql &= " ON  FORMAT(URIAGE.DENPYOUNO,'000000') = TRN_JISSEKI.DenNo2 "
    sql &= " AND URIAGE.GYOBAN = TRN_JISSEKI.GyoNo "
    sql &= SqlWhereText()
    sql &= " GROUP BY "
    sql &= "   NohinDay "
    sql &= " , TokuiCD "
    sql &= " , TokuiNM "
    sql &= " , IIF(URIAGE.DENPYOUNO is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2)  "
    sql &= " , URIAGE.DENPYOUNO  "
    sql &= " ORDER BY "
    sql &= "   NohinDay "
    sql &= " , IIF(URIAGE.DENPYOUNO is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2)  "

    Return sql
  End Function

  ''' <summary>
  ''' 得意先一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret

      .Add(New clsDGVColumnSetting("一括発行", "POST_STAT", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("出荷日", "NohinDay", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("伝票番号", "DenNo", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("得意先コード", "TokuiCD", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("得意先名", "TokuiNM", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=220))

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 得意先一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  ''' <remarks>編集可能列無し</remarks>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 出荷明細一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Dim sql As String = String.Empty

    sql &= SqlSelItemDetail()
    sql &= " ORDER BY IIF(URIAGE.DENPYOUNO is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2)  "
    sql &= "     ,    TRN_JISSEKI.GyoNo "

    Return sql
  End Function

  ''' <summary>
  ''' 出荷明細一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("PCA送信", "POST_STAT", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("行番号", "GyoNo", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("出荷日", "NohinDay", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("得意先コード", "TokuiCD", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("得意先名", "TokuiNM", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=220))
      .Add(New clsDGVColumnSetting("商品コード", "ShohinCD", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("商品名", "ShohinNM", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=180))
      .Add(New clsDGVColumnSetting("数量", "Suryo", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#0.0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("単価", "UriTanka", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("金額", "UriageKin", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("伝票No.", "DenNo", argTextAlignment:=typAlignment.MiddleRight))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 出荷明細一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid2EditCol2() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol2
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
    End With

    Return ret
  End Function
#End Region

#Region "メソッド"

#Region "プライベート"

#Region "データ件数取得"
  ''' <summary>
  ''' 未送信データ件数取得
  ''' </summary>
  ''' <returns>未送信データ件数</returns>
  Private Function GetUnSendCount() As Long
    Dim ret As Long = 0

    For Each tmpRow As Dictionary(Of String, String) In Me.Controlz(DG2V2.Name).GetAllData
      ret += Long.Parse(IIf(tmpRow("POST_STAT").ToString() = "未完了", "1", "0").ToString())
    Next

    Return ret
  End Function

  ''' <summary>
  ''' 送信予定件数表示
  ''' </summary>
  Private Sub DspPlanedCount()

    With Controlz(DG2V2.Name)
      If .SelectCount > 0 Then
        Me.lblPostCount.Text = .SelectCount().ToString() & "行のデータがPCA売上伝票として送信されます。"
      Else
        Me.lblPostCount.Text = ""
      End If

    End With

  End Sub
#End Region

#Region "PCAデータ送信関連"

  ''' <summary>
  ''' 売上データ送信処理
  ''' </summary>
  ''' <param name="prmDbCutJ"></param>
  ''' <param name="prmDbUriageWork"></param>
  Private Sub DataPost(prmDbCutJ As T.R.ZCommonClass.clsSqlServer _
                             , prmDbUriageWork As T.R.ZCommonClass.clsSqlServer _
                             , ByVal prmWhereList As Dictionary(Of String, String))

    Dim tmpSiireCd As New List(Of String)

    Try
      ' URIAGEデータ作成（WORKテーブルへの書き込み）
      Me.lblInformation.Text = "伝票ﾃﾞｰﾀを作成しています。しばらくお待ち下さい。"
      Call CreatePostData(prmDbCutJ, prmDbUriageWork, tmpSiireCd)

      'WK_URIAGE → URIAGE
      Call WkUriage2Uriage(prmDbCutJ, prmDbUriageWork)

      ' PCA送信
      Me.lblInformation.Text = "PCAへの受渡しをしています。しばらくお待ち下さい。"
      Call SendPca(prmDbUriageWork, tmpSiireCd, prmWhereList)
      Me.lblInformation.Text = "PCAへの受渡しは、正常に作成されました。"

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("売上データの送信に失敗しました。" & vbCrLf & ex.Message)
    End Try


  End Sub

  ''' <summary>
  ''' PCAデータ送信処理
  ''' </summary>
  ''' <param name="prmDbUriageWork"></param>
  Private Sub SendPca(prmDbUriageWork As T.R.ZCommonClass.clsSqlServer, tmpSiireCdList As List(Of String), ByRef prmWhereList As Dictionary(Of String, String))
    Dim tmpDt As New DataTable
    Dim tmpDenNo As String = String.Empty
    Dim tmpTokuisakiC As String = String.Empty
    Dim tmpSiireC As String = String.Empty
    Dim tmpZeiritsu As Decimal = Decimal.MinValue
    Dim tmpGenZeiritsu As Decimal = Decimal.MinValue
    Dim tmpUriZeiSbt As Decimal = Decimal.MinValue
    Dim tmpGenZeiSbt As Decimal = Decimal.MinValue
    Dim PcaDataBase As New clsPcaDb
    Dim tmpSmsData As New Dictionary(Of String, String)
    Dim cnt As Integer = 0

    Dim tmpPcaSYK As New clsPcaSYK(PCAAPI_USERID, PCAAPI_PASSWORD, PCAAPI_PG_ID, PCAAPI_PG_NAME, PCAAPI_DATAAREANAME)
    Dim tmpSiireKbn As Boolean = True

    Try
      GetUriageData(prmDbUriageWork, tmpDt)

      If tmpDt.Rows.Count > 0 Then

        For Each tmpDataRow As DataRow In tmpDt.Rows
          'tmpSiireCd = tmpSiireCdList(cnt)

          ' 伝票番号が変更されたら伝票ヘッダーを作成
          If tmpDenNo <> tmpDataRow("DENPYOUNO").ToString() Then
            tmpDenNo = tmpDataRow("DENPYOUNO").ToString()

            'tmpPcaSYK.AddHeader(CreateUriageHeader(tmpDataRow))
          End If

          'tmpSmsData = PcaDataBase.GetZeiSbt(tmpDataRow("SYOHINC").ToString())
          '' 得意先が変更されたら税率を再取得
          'If tmpTokuisakiC <> tmpDataRow("TOKUISAKIC").ToString() Then
          '  tmpTokuisakiC = tmpDataRow("TOKUISAKIC").ToString()
          '  tmpZeiritsu = PcaDataBase.GetTaxRate(tmpTokuisakiC, tmpDataRow("SYOHINC").ToString(), False, tmpDataRow("URIAGEBI").ToString)
          '  'tmpGenZeiritsu = PcaDataBase.GetTaxRate(tmpSiireCd, tmpDataRow("SYOHINC").ToString(), tmpSiireKbn, tmpDataRow("URIAGEBI").ToString)
          'End If

          ' 明細を伝票に追加
          'tmpPcaSYK.AddDetail(CreateUriageDetail(tmpDataRow, tmpZeiritsu, tmpGenZeiritsu, tmpSmsData))

          cnt += 1
        Next



        prmWhereList.Add("DenNO2 = ", tmpDenNo.PadLeft(6, "0"c))

        ' 送信
        'tmpPcaSYK.Create()
      End If




    Catch ex As Exception
      Dim msg As String = ex.Message
      Call ComWriteErrLog(ex)
      Throw New Exception("PCAデータ送信に失敗しました。" & vbCrLf & msg)
    End Try


  End Sub

  'Private Function GetTaxRate(prmTokuisakiC As String) As Decimal
  '  Dim tmpDb As New clsSqlServer
  '  Dim tmpDt As New DataTable
  '  Dim ret As Decimal = 8

  '  Try
  '    tmpDb.GetResult(tmpDt, "SELECT * FROM THENKAN WHERE TKCODE=" & prmTokuisakiC)
  '    If tmpDt.Rows.Count > 0 Then
  '      ret = Decimal.Parse(tmpDt.Rows(0)("ZeiRitu"))
  '    Else
  '      ret = 8
  '    End If
  '  Catch ex As Exception
  '    Call ComWriteErrLog(ex)
  '    Throw New Exception("税率の取得に失敗しました")
  '  End Try

  '  Return ret
  'End Function


  ''' <summary>
  ''' 商品情報取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelSMS() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM SMS "
    sql &= "  INNER JOIN SMSP ON SMS.sms_scd = SMSP.smsp_scd "

    Return sql
  End Function

  Private Function SqlSelShenkan() As String
    Dim sql As String = String.Empty

    sql &= " SELECT HENKAN "
    sql &= " FROM SHENKAN "

    Return sql
  End Function


  ''' <summary>
  ''' URIAGEワークテーブルの内容をURIAGEテーブルに書き込む
  ''' </summary>
  ''' <param name="prmDbCutJ">URIAGEテーブル更新用DB接続</param>
  ''' <param name="prmDbUriageWork">URIAGEワークテーブル抽出用DB接続</param>
  Private Sub WkUriage2Uriage(prmDbCutJ As T.R.ZCommonClass.clsSqlServer _
                             , prmDbUriageWork As T.R.ZCommonClass.clsSqlServer)
    Dim tmpDt As New DataTable
    Dim tmpUriageList As New List(Of Dictionary(Of String, String))

    Try
      prmDbUriageWork.GetResult(tmpDt, "SELECT * FROM #WK_URIAGE ORDER BY URIAGEBI,TOKUISAKIC")
      If tmpDt.Rows.Count > 0 Then
        tmpUriageList = ComDt2Dic(tmpDt)

        For Each tmpDic As Dictionary(Of String, String) In tmpUriageList
          prmDbCutJ.Execute(SqlInsUriageFromWk(tmpDic))
          prmDbCutJ.Execute(SqlUpdateJisseki(tmpDic))
        Next

      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("URIAGEテーブルの更新に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try


  End Sub

  ''' <summary>
  ''' PCA売上伝票ヘッダー作成
  ''' </summary>
  ''' <param name="prmPostData"></param>
  ''' <returns></returns>
  Private Function CreateUriageHeader(prmPostData As DataRow) As clsPcaSYKH
    Dim ret As New clsPcaSYKH

    With ret
      .伝区 = prmPostData("DENKU").ToString()
      .売上日 = Date.Parse(prmPostData("URIAGEBI")).ToString("yyyyMMdd")
      .請求日 = Date.Parse(prmPostData("SEIKYUBI")).ToString("yyyyMMdd")
      .伝票No = prmPostData("DENPYOUNO").ToString()
      .得意先コード = prmPostData("TOKUISAKIC").ToString()
      .部門コード = prmPostData("BUMONC").ToString()
      '      .担当者コード = prmPostData("TANTOUC").ToString()
      .摘要コード = prmPostData("TEKIYOUC").ToString()
    End With

    Return ret
  End Function

  ''' <summary>
  ''' PCA売上伝票明細作成
  ''' </summary>
  ''' <param name="prmPostData"></param>
  ''' <returns></returns>
  Private Function CreateUriageDetail(prmPostData As DataRow _
                                    , prmZeiRitu As Decimal _
                                    , tmpGenZeiritsu As Decimal _
                                    , prmSmsData As Dictionary(Of String, String)) As clsPcaSYKD
    Dim ret As New clsPcaSYKD

    With ret
      .商品コード = prmPostData("SYOHINC").ToString()
      .マスター区分 = prmPostData("MASKUBUN").ToString()
      .税区分 = "" 'prmSmsData("smsp_tax")
      .税込区分 = "" 'prmSmsData("smsp_komi")
      .商品名 = prmPostData("HINMEI").ToString()
      .規格_型番 = prmPostData("KIKAKU").ToString()
      .色 = Decimal.Parse(prmPostData("IRO")).ToString("g4")
      .サイズ = prmPostData("SIZE").ToString()
      .倉庫 = prmPostData("SOUKO").ToString()
      .区 = prmPostData("KU").ToString()
      .入数 = prmPostData("IRISU").ToString()
      .箱数 = prmPostData("HAKOSU").ToString()
      .単位 = prmPostData("TANNI").ToString()
      .単価 = prmPostData("TANKA").ToString()
      .数量小数桁 = "1" 'GetSuryoKeta(prmPostData("SYOHINC").ToString())
      .原単価 = prmPostData("GENTAN").ToString()

      .金額 = prmPostData("Kingaku").ToString()
      .原価金額 = prmPostData("GENKAGAKU").ToString()
      .備考 = prmPostData("BIKOU").ToString().PadLeft(10, "0"c)
      .税率 = prmZeiRitu.ToString()

      If prmPostData("ZEIKOMI").ToString() = "0" Then
        .外税額 = prmPostData("SOTOZEI").ToString()
      ElseIf prmPostData("ZEIKOMI").ToString() = "1" Then
        .内税額 = prmPostData("UchiZei").ToString()
      End If

      .商品項目1 = prmPostData("SKOUMOKU1").ToString()
      .商品名２ = prmPostData("KAKU_NAME").ToString()
      .商品項目2 = prmPostData("SKOUMOKU2").ToString()
      .商品項目3 = prmPostData("SKOUMOKU3").ToString()

      .売上項目1 = prmPostData("UKOUMOKU1").ToString()
      .売上項目2 = prmPostData("UKOUMOKU2").ToString()
      .売上項目3 = prmPostData("UKOUMOKU3").ToString()


      .数量 = prmPostData("SURYO").ToString()


      .粗利益 = GetGrossProfit(prmPostData, prmZeiRitu)

      .売上税種別 = "" 'prmSmsData("sms_kontaxkind")
      .原価税種別 = "" 'prmSmsData("sms_kantaxkind")
      .原価税率 = "" 'tmpGenZeiritsu.ToString()

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 粗利額取得
  ''' </summary>
  ''' <param name="prmPostData"></param>
  ''' <returns></returns>
  Private Function GetGrossProfit(prmPostData As DataRow _
                                  , prmZeiRitu As Decimal) As String
    Dim ret As String = String.Empty
    Dim tmpUriz As Decimal = 0
    Dim tmpGenz As Decimal = 0
    Dim tmpKingaku As Decimal = Decimal.Parse(prmPostData("Kingaku").ToString())
    Dim tmpGenkagaku As Decimal = Decimal.Parse(prmPostData("GENKAGAKU").ToString())

    If prmPostData("ZEIKOMI").ToString() <> "1" Then
      ret = tmpKingaku - Decimal.Parse(prmPostData("GENKAGAKU").ToString())
    Else
      tmpUriz = Fix(tmpKingaku / (1 + (prmZeiRitu / 100)) + 0.999)
      tmpGenz = Fix(tmpGenkagaku / (1 + (prmZeiRitu / 100)) + 0.999)
      ret = (tmpUriz - tmpGenz).ToString()
    End If

    Return ret
  End Function

  ''' <summary>
  ''' PCA商品マスタの商品数量桁を取得する
  ''' </summary>
  ''' <param name="tmpSYOHINC">対象の商品コード</param>
  ''' <returns>数量桁数</returns>
  Private Function GetSuryoKeta(tmpSYOHINC As String) As String
    Dim tmpDb As New clsPcaDb
    Dim tmpDt As New DataTable
    Dim ret As String = String.Empty

    Try
      tmpDb.GetResult(tmpDt, "SELECT * FROM SMS WHERE sms_scd ='" & tmpSYOHINC.PadLeft(clsPcaDb.ITEM_CODE_LENGTH, "0"c) & "'")
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0)("sms_sketa").ToString()
      Else
        Throw New Exception("商品コード不正:商品コード[" & tmpSYOHINC & "]はマスターに存在しません。")
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("数量桁取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' #WK_URIAGEを取得
  ''' </summary>
  ''' <param name="prmDbUriageWork"></param>
  ''' <param name="prmDt"></param>
  Private Sub GetUriageData(prmDbUriageWork As T.R.ZCommonClass.clsSqlServer _
                            , ByRef prmDt As DataTable)

    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM #WK_URIAGE "
    sql &= " ORDER BY DENPYOUNO DESC "
    sql &= "        , GYOBAN "
    sql &= "        , MTOKUISAKIC "
    sql &= "        , MSYOHINC "

    Try
      prmDbUriageWork.GetResult(prmDt, sql)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("#WK_URIAGEの取得に失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' 送信データ作成
  ''' </summary>
  ''' <remarks>URIAGEテーブルにPCAに投げるデータを登録する</remarks>
  Private Sub CreatePostData(tmpDbCutJ As T.R.ZCommonClass.clsSqlServer _
                             , tmpDbUriageWork As T.R.ZCommonClass.clsSqlServer _
                             , ByVal SiireCd As List(Of String)
                             )
    Dim tmpPostData As New Dictionary(Of String, String)
    Dim tmpMSETCODE As Integer = 0

    Dim tmpSlipNumber As Long = Long.MinValue
    Dim tmpLineNumber As Long = Long.MinValue

    '上場コード
    Dim tmpJyouJyou As String = String.Empty

    ' WK_URIAGE作成
    Call CreateWkTable(tmpDbUriageWork)

    ' 伝票番号取得 TODO New伝票番号を取得
    tmpSlipNumber = AssignNumber(tmpDbCutJ, TBL_NAME)
    'tmpSlipNumber = AssignNumber(tmpDbCutJ)

    ' 伝票行番号初期値設定（=1）
    tmpLineNumber = 1

    For Each tmpGridData As Dictionary(Of String, String) In Controlz(DG2V2.Name).GetAllData()

      ' 選択データのみ対象
      If tmpGridData("SelecterCol") <> "〇" Then
        Continue For
      End If

      '---------------------------------------------------
      '   集計単位が変更されたらURIAGEテーブルへ書き込み
      '---------------------------------------------------
      If ChkNewLine(tmpMSETCODE, tmpGridData, tmpPostData, tmpDbCutJ, tmpSlipNumber) Then

        ' URIAGEテーブルへデータ書き込み
        Call WriteUriageTbl(tmpDbUriageWork, tmpDbCutJ, tmpPostData)

        ' 伝票番号・行番号制御
        Call LineControl(tmpDbCutJ, tmpGridData, tmpPostData, tmpSlipNumber, tmpLineNumber)

        ' データクリア
        tmpPostData.Clear()
      End If

      '---------------------------------------------------
      '               伝票番号関連処理
      '---------------------------------------------------
      ComSetDictionaryVal(tmpPostData, "WK_DenCnt", tmpSlipNumber)   ' 伝票番号設定
      ComSetDictionaryVal(tmpPostData, "GYONO", tmpLineNumber)       ' 行番号設定

      'SiireCd.Add(GetSrCode(tmpGridData).ToString())
      ' 在庫データの伝票番号更新
      'Call UpDateCutJ(tmpDbCutJ, tmpGridData, tmpSlipNumber, tmpLineNumber)

      '---------------------------------------------------
      '               伝票データ作成
      '---------------------------------------------------

      ' 出荷明細一覧のデータを送信データ用配列に保持
      Call GridData2PostData(tmpGridData, tmpPostData)

      '' PCA得意先マスタより端数処理方式を取得
      'Call GetPcaSetting(tmpPostData)

      '' 商品基本データの取得
      'Call GetItemBaseData(tmpGridData, tmpPostData)

      '' 得意先別商品データ取得
      'Call GetItemData4Customer(tmpGridData, tmpPostData)

      '' 商品名に左右文字を追記
      'Call SetPartsSideText(tmpGridData, tmpPostData)

      '' 数量・原価計算
      'Call AddSuryoGenka(Decimal.Parse(tmpGridData("JYURYO")), tmpPostData)

      '' 集計処理(粗利・内税・外税・税込金額・税区分取得)
      'Call TallieUp(tmpPostData)

      'If tmpPostData("PSETFLG") = "0" Then
      '  tmpMSETCODE = Integer.Parse(tmpPostData("SETCODE"))
      'Else
      '  tmpMSETCODE = 0
      'End If

    Next

    ' 最終データをURIAGEテーブルへ書き込み
    'If Decimal.Parse(tmpPostData("SURYO")) <> 0 Then
    Call WriteUriageTbl(tmpDbUriageWork, tmpDbCutJ, tmpPostData)
    'End If


  End Sub

  ''' <summary>
  ''' 伝票番号・行番号制御
  ''' </summary>
  ''' <param name="prmGridData"></param>
  ''' <param name="prmPostData"></param>
  ''' <param name="prmSlipNumber"></param>
  ''' <param name="prmLineNumber"></param>
  Private Sub LineControl(prmDb As T.R.ZCommonClass.clsSqlServer _
                          , prmGridData As Dictionary(Of String, String) _
                          , prmPostData As Dictionary(Of String, String) _
                          , ByRef prmSlipNumber As Long _
                          , ByRef prmLineNumber As Long)

    Try
      ' 売上日・得意先・買戻し時に伝票区分（通常 or 赤）が変更になったか
      ' 最大行数を越えた場合は伝票番号を採番する
      If prmGridData("NohinDay") <> prmPostData("URIAGEBI") _
                        Or prmGridData("TokuiCD") <> prmPostData("TOKUISAKIC") Then

        ' 伝票番号取得
        prmSlipNumber = AssignNumber(prmDb, TBL_NAME)
        ' 伝票行番号初期化
        prmLineNumber = 1
      Else
        '次行番号へ
        prmLineNumber += 1
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("伝票番号（行番号）制御に失敗しました")
    End Try

  End Sub

  ''' <summary>
  ''' URIAGEテーブルへのデータ書き込み
  ''' </summary>
  ''' <param name="prmDbUriageWork"></param>
  ''' <param name="prmDbCutJ"></param>
  ''' <param name="prmPostData"></param>
  Private Sub WriteUriageTbl(prmDbUriageWork As T.R.ZCommonClass.clsSqlServer _
                            , prmDbCutJ As T.R.ZCommonClass.clsSqlServer _
                            , ByRef prmPostData As Dictionary(Of String, String)
                          )

    'If prmPostData("SABAKI") = "0" Or prmPostData("SETKBN") = "0" Or prmPostData("EDAKBN") = "0" Then
    ' URIAGEテーブルへ書き込み
    Call Dic2Db(prmDbUriageWork, prmPostData)
    'ElseIf prmPostData("MSYOHINC") = "10000" Then
    '  ' URIAGEテーブルへ書き込み
    '  ComSetDictionaryVal(prmPostData, "FLG2", "2")
    '  Call Dic2Db(prmDbUriageWork, prmPostData)
    'Else
    '  ' 枝重量取得処理（詳細不明）
    '  Call UnKnownProc(prmDbCutJ, prmPostData)
    '  ' URIAGEテーブルへ書き込み
    '  Call Dic2Db(prmDbUriageWork, prmPostData)

    'End If

  End Sub

  ''' <summary>
  ''' CUTJへの伝票番号の更新を行う
  ''' </summary>
  ''' <param name="tmpDb"></param>
  ''' <param name="prmGridSrc"></param>
  ''' <param name="prmSlipNumber"></param>
  ''' <param name="prmLineNumber"></param>
  Private Sub UpDateCutJ(tmpDb As T.R.ZCommonClass.clsSqlServer _
                      , prmGridSrc As Dictionary(Of String, String) _
                      , prmSlipNumber As Long _
                      , prmLineNumber As Long)
    Try
      If 1 <> tmpDb.Execute(SqlUpdCutJ(prmGridSrc, prmSlipNumber, prmLineNumber)) Then
        Throw New Exception("出荷データの更新に失敗しました。他のユーザーによって変更されている可能性があります。")
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("出荷データに伝票番号を更新できませんでした。")
    End Try

  End Sub

  ''' <summary>
  ''' 出荷明細一覧のデータを送信データ用配列に保持する
  ''' </summary>
  ''' <param name="tmpDicFrom">出荷明細一覧データ</param>
  ''' <param name="tmpDicDst">送信データ用配列</param>
  Private Sub GridData2PostData(tmpDicFrom As Dictionary(Of String, String) _
                                , ByRef tmpDicDst As Dictionary(Of String, String))

    ComSetDictionaryVal(tmpDicDst, "SETCODE", "")
    ComSetDictionaryVal(tmpDicDst, "URIAGEBI", tmpDicFrom("NohinDay"))
    ComSetDictionaryVal(tmpDicDst, "SEIKYUBI", tmpDicFrom("NohinDay"))
    ComSetDictionaryVal(tmpDicDst, "TOKUISAKIMEI", tmpDicFrom("TokuiNM"))
    ComSetDictionaryVal(tmpDicDst, "TOKUISAKIC", tmpDicFrom("TokuiCD"))
    ComSetDictionaryVal(tmpDicDst, "TANKA", tmpDicFrom("UriTanka"))
    ComSetDictionaryVal(tmpDicDst, "Kingaku", tmpDicFrom("UriageKin"))
    ComSetDictionaryVal(tmpDicDst, "KU", "0")
    ComSetDictionaryVal(tmpDicDst, "GENSANCHI", "")
    ComSetDictionaryVal(tmpDicDst, "HINSYU", "")
    ComSetDictionaryVal(tmpDicDst, "FLG2", "2")
    ComSetDictionaryVal(tmpDicDst, "ZEIKUBUN", "")
    ComSetDictionaryVal(tmpDicDst, "ZEIRITU", "")
    ComSetDictionaryVal(tmpDicDst, "TANTOUC", "0")
    ComSetDictionaryVal(tmpDicDst, "MAXGYO", "")
    ComSetDictionaryVal(tmpDicDst, "KOTAINO", "")
    ComSetDictionaryVal(tmpDicDst, "EDABAN", "")
    ComSetDictionaryVal(tmpDicDst, "JYURYOK", "")
    ComSetDictionaryVal(tmpDicDst, "BRAND_NAME", "")
    ComSetDictionaryVal(tmpDicDst, "JYOUJYOU", "")
    ComSetDictionaryVal(tmpDicDst, "KAKU_CODE", "0")
    ComSetDictionaryVal(tmpDicDst, "KAKU_NAME", "")

    ComSetDictionaryVal(tmpDicDst, "HINMEI", tmpDicFrom("ShohinNM"))
    ComSetDictionaryVal(tmpDicDst, "SYOHINC", tmpDicFrom("ShohinCD"))
    ComSetDictionaryVal(tmpDicDst, "SETKBN", "0")
    ComSetDictionaryVal(tmpDicDst, "MSYOHINC", tmpDicFrom("ShohinCD"))
    ComSetDictionaryVal(tmpDicDst, "BICODE", tmpDicFrom("ShohinCD"))
    ComSetDictionaryVal(tmpDicDst, "SURYO", tmpDicFrom("Suryo"))
    ComSetDictionaryVal(tmpDicDst, "DenNo", tmpDicFrom("DenNo"))
    ComSetDictionaryVal(tmpDicDst, "GyoNo", tmpDicFrom("GyoNo"))

  End Sub

  ''' <summary>
  ''' 商品基本データ取得
  ''' </summary>
  ''' <param name="tmpDicFrom">出荷明細一覧データ</param>
  ''' <param name="tmpDicDst">送信データ用配列</param>
  Private Sub GetItemBaseData(tmpDicFrom As Dictionary(Of String, String) _
                                , ByRef tmpDicDst As Dictionary(Of String, String))

    ' 原単価は単品・セットの区別なく取得
    'ComSetDictionaryVal(tmpDicDst, "GENTAN", tmpDicFrom("GENKA"))
    Try

      'If tmpDicFrom("LOG_SETCODE") = "0" Then
      ' 単品出荷
      ComSetDictionaryVal(tmpDicDst, "PSETFLG", "0")
      ComSetDictionaryVal(tmpDicDst, "HINMEI", tmpDicFrom("ShohinNM"))

      ComSetDictionaryVal(tmpDicDst, "SYOHINC", tmpDicFrom("ShohinCD"))
      ComSetDictionaryVal(tmpDicDst, "SETKBN", "0")
      ComSetDictionaryVal(tmpDicDst, "MSYOHINC", tmpDicFrom("ShohinCD"))
      ComSetDictionaryVal(tmpDicDst, "BICODE", tmpDicFrom("ShohinCD"))

      'Else
      '  ' セット出荷
      '  Dim tmpDb As New clsSqlServer()
      '  Dim tmpDt As New DataTable

      '  Try
      '    tmpDb.GetResult(tmpDt, "SELECT * FROM SHOHIN WHERE SHCODE = " & tmpDicFrom("LOG_SETCODE"))
      '    If tmpDt.Rows.Count <= 0 Then
      '      Throw New Exception("セットコードが不正です。" & tmpDicFrom("LOG_SETCODE") & "は SHOHINに存在しません。")
      '    Else
      '      ComSetDictionaryVal(tmpDicDst, "PSETFLG", tmpDt.Rows(0)("PSETFLG").ToString())
      '      ComSetDictionaryVal(tmpDicDst, "HINMEI", tmpDt.Rows(0)("HINMEI").ToString())
      '    End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("商品基本データ取得に失敗しました。")
    Finally
      'tmpDt.Dispose()
      '  tmpDb.Dispose()
    End Try

    '  ComSetDictionaryVal(tmpDicDst, "SYOHINC", tmpDicFrom("LOG_SETCODE"))
    '  'ComSetDictionaryVal(tmpDicDst, "SETKBN", IIf(tmpDicFrom("BICODE") = EDANIKU_CODE.ToString(), "0", "1"))
    '  ComSetDictionaryVal(tmpDicDst, "MSYOHINC", (Long.Parse(tmpDicFrom("BICODE")) + 10000).ToString())
    '  ComSetDictionaryVal(tmpDicDst, "BICODE", tmpDicFrom("BICODE"))
    'End If

  End Sub

  ''' <summary>
  ''' 得意先別商品データ取得
  ''' </summary>
  ''' <param name="tmpDicFrom"></param>
  ''' <param name="tmpDicDst"></param>
  Private Sub GetItemData4Customer(tmpDicFrom As Dictionary(Of String, String) _
                                , ByRef tmpDicDst As Dictionary(Of String, String))
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer
    Dim tmpDt As New DataTable

    ' 商品変換テーブル取得
    Try

      ' 得意先指定で商品変換テーブルを取得
      GetShenkanTbl(tmpDt, tmpDicFrom("UTKCODE"), tmpDicDst("SETKBN"), tmpDicDst("SYOHINC"))

      If tmpDt.Rows.Count <= 0 Then
        ' 得意先未指定で商品変換テーブルを取得
        GetShenkanTbl(tmpDt, "0", tmpDicDst("SETKBN"), tmpDicDst("SYOHINC"))
      End If

      If tmpDt.Rows.Count >= 1 Then

        If Integer.Parse(tmpDicDst("PSETFLG")) > 0 Then
          ComSetDictionaryVal(tmpDicDst, "SYOHINC", tmpDt.Rows(0)("HENKAN").ToString())
          ComSetDictionaryVal(tmpDicDst, "FLG2", "3")
        End If

        If tmpDt.Rows(0)("HINMEI").ToString().IndexOf("枝肉") >= 0 Then
          ComSetDictionaryVal(tmpDicDst, "FLG2", "2")
        End If

      End If

      If Integer.Parse(tmpDicDst("PSETFLG")) > 0 Then
        ' 単品セット出荷時

        ComSetDictionaryVal(tmpDicDst, "SETKBN", "0")

        ' 得意先指定で商品変換テーブルを取得
        tmpDt.Clear()
        GetShenkanTbl(tmpDt, tmpDicFrom("UTKCODE"), "0", tmpDicFrom("BICODE"))

        If tmpDt.Rows.Count <= 0 Then
          ' 得意先未指定で商品変換テーブルを取得
          GetShenkanTbl(tmpDt, "0", "0", tmpDicFrom("BICODE"))
        End If

      End If

      ' 入数加算
      If Integer.Parse(tmpDicDst("SETKBN")) > 0 Then
        ComSetDictionaryVal(tmpDicDst, "IRISU", "1")
      Else
        If tmpDicDst.ContainsKey("IRISU") = False Then
          tmpDicDst.Add("IRISU", "0")
        End If
        ComSetDictionaryVal(tmpDicDst, "IRISU", (ComBlank2Zero(tmpDicDst("IRISU") + 1).ToString()))
      End If

      ' 得意先別商品情報取得
      If tmpDt.Rows.Count <= 0 Then
        ' 得意先別商品情報無し
        ComSetDictionaryVal(tmpDicDst, "TANNI2", " ")
        ComSetDictionaryVal(tmpDicDst, "Keta", "100")
        ComSetDictionaryVal(tmpDicDst, "SABAKI", "0")
        ComSetDictionaryVal(tmpDicDst, "SUB_CODE", "0")
        ComSetDictionaryVal(tmpDicDst, "EDAKBN", "0")
      Else
        ' 得意先別商品情報あり
        If Integer.Parse(tmpDicDst("PSETFLG")) = 0 Then
          ComSetDictionaryVal(tmpDicDst, "SYOHINC", tmpDt.Rows(0)("HENKAN").ToString())
        End If
        ComSetDictionaryVal(tmpDicDst, "HINMEI", tmpDt.Rows(0)("HINMEI").ToString())
        ComSetDictionaryVal(tmpDicDst, "TANNI2", tmpDt.Rows(0)("TANNI").ToString())
        ComSetDictionaryVal(tmpDicDst, "Keta", tmpDt.Rows(0)("Keta").ToString())
        ComSetDictionaryVal(tmpDicDst, "SABAKI", tmpDt.Rows(0)("SABAKI").ToString())
        ComSetDictionaryVal(tmpDicDst, "SUB_CODE", tmpDt.Rows(0)("SUB_CODE").ToString())
        ComSetDictionaryVal(tmpDicDst, "IRISU", tmpDt.Rows(0)("IRISU").ToString())
        ComSetDictionaryVal(tmpDicDst, "EDAKBN", tmpDt.Rows(0)("EDAKBN").ToString())
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("得意先別商品データ取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' 商品名に左右文字を追記する
  ''' </summary>
  ''' <param name="tmpDicFrom"></param>
  ''' <param name="tmpDicDst"></param>
  Private Sub SetPartsSideText(tmpDicFrom As Dictionary(Of String, String) _
                                , ByRef tmpDicDst As Dictionary(Of String, String))

    ' 左右の区別無く集計する為、左右どちらも含まれる場合は表記しない
    ComSetDictionaryVal(tmpDicDst, "SAYU", tmpDicFrom("SAYUKUBUN"))

    If tmpDicDst.ContainsKey("gSAYU") = False Then
      tmpDicDst.Add("gSAYU", tmpDicDst("SAYU"))
    End If

    If tmpDicDst("gSAYU") <> tmpDicDst("SAYU") Then
      ComSetDictionaryVal(tmpDicDst, "gSAYU", (Integer.Parse(tmpDicDst("gSAYU")) + Integer.Parse(tmpDicDst("SAYU"))).ToString())
    End If

    Select Case tmpDicDst("gSAYU")
      'Case PARTS_SIDE_LEFT.ToString()
      '  ComSetDictionaryVal(tmpDicDst, "SIZE", " 左")
      'Case PARTS_SIDE_RIGHT.ToString()
      '  ComSetDictionaryVal(tmpDicDst, "SIZE", " 右")
      Case Else
        ComSetDictionaryVal(tmpDicDst, "SIZE", " ")
    End Select

  End Sub

  ''' <summary>
  ''' 集計処理
  ''' </summary>
  ''' <param name="tmpDicDst"></param>
  ''' <remarks>
  '''  原価、原単価、売価、粗利額を集計する
  ''' </remarks>
  Private Sub TallieUp(ByRef tmpDicDst As Dictionary(Of String, String))
    Dim tmpGosa As Decimal

    ' 計算補正値取得
    If Decimal.Parse(tmpDicDst("SURYO")) >= 0 Then
      tmpGosa = 0.01
    Else
      tmpGosa = -0.01
    End If

    ' 売価計算
    Dim tmpTanka As Decimal = Decimal.Parse(tmpDicDst("TANKA"))
    Dim tmpSuryo As Decimal = Decimal.Parse(tmpDicDst("SURYO"))
    Dim tmpKingaku As Decimal
    Select Case tmpDicDst("khasu")
      Case "0"
        tmpKingaku = Fix(tmpTanka * tmpSuryo + tmpGosa)
      Case "1"
        tmpKingaku = Fix(tmpTanka * tmpSuryo + 0.999)
      Case "2"
        tmpKingaku = Math.Truncate(tmpTanka * tmpSuryo + tmpGosa)
    End Select
    ComSetDictionaryVal(tmpDicDst, "Kingaku", tmpKingaku.ToString())

    ' 粗利・税額（内税/外税）計算
    Dim tmpGENKAGAKU As Decimal = Decimal.Parse(tmpDicDst("GENKAGAKU"))
    Dim tmpZei As Decimal = 0
    Dim tmpSotozei As Decimal = 0
    Dim tmpZeiRitu As Decimal = Decimal.Parse(tmpDicDst("ZEIRITU"))
    Dim tmpUchiZei As Decimal = 0
    Dim tmpARARI As Decimal = 0
    Dim tmpZEIKOMI As Decimal
    Dim tmpZEIKU As Decimal


    Select Case tmpDicDst("ZEIKUBUN")
      Case "0"

        tmpZei = tmpZeiRitu / 100
        tmpARARI = tmpKingaku - tmpGENKAGAKU
        Select Case tmpDicDst("Hasuu")
          Case "0"
            tmpSotozei = Fix(tmpKingaku * tmpZei + tmpGosa)
          Case "1"
            tmpSotozei = Fix(tmpKingaku * tmpZei + 0.999)
          Case "2"
            tmpSotozei = Math.Truncate(tmpKingaku * tmpZei + tmpGosa)
        End Select

        tmpUchiZei = 0
        tmpZEIKOMI = 0
        tmpZEIKU = 2

      Case "1"
        Select Case tmpDicDst("Hasuu")
          Case "0"
            tmpUchiZei = Fix((tmpKingaku / (100 + tmpZeiRitu)) * tmpZeiRitu + tmpGosa)
          Case "1"
            tmpUchiZei = Fix((tmpKingaku / (100 + tmpZeiRitu)) * tmpZeiRitu + 0.999)
          Case "2"
            tmpUchiZei = Math.Truncate((tmpKingaku / (100 + tmpZeiRitu)) * tmpZeiRitu + tmpGosa)
        End Select

        tmpARARI = (tmpKingaku - tmpUchiZei) - (tmpGENKAGAKU - Fix((tmpGENKAGAKU / (100 + tmpZeiRitu)) * tmpZeiRitu + tmpGosa))
        tmpSotozei = 0
        tmpZEIKOMI = 1
        tmpZEIKU = 2

      Case "2"
        tmpARARI = tmpKingaku - tmpGENKAGAKU
        tmpSotozei = 0
        tmpUchiZei = 0
        tmpZEIKOMI = 0
        tmpZEIKU = 0
    End Select

    ComSetDictionaryVal(tmpDicDst, "ARARI", tmpARARI.ToString())
    ComSetDictionaryVal(tmpDicDst, "Sotozei", tmpSotozei.ToString())
    ComSetDictionaryVal(tmpDicDst, "UchiZei", tmpUchiZei.ToString())
    ComSetDictionaryVal(tmpDicDst, "ZEIKOMI", tmpZEIKOMI.ToString())
    ComSetDictionaryVal(tmpDicDst, "ZEIKU", tmpZEIKU.ToString())


  End Sub

  ''' <summary>
  ''' 数量・原価計算
  ''' </summary>
  ''' <param name="tmpJyuryo"></param>
  ''' <param name="tmpDicDst"></param>
  ''' <remarks>数量・原価を計算</remarks>
  Private Sub AddSuryoGenka(tmpJyuryo As Decimal _
                                , ByRef tmpDicDst As Dictionary(Of String, String))

    Dim tmpSuryo As Decimal
    Dim tmpBunbo As Decimal
    Dim tmpGosa As Decimal
    Dim tmpKeta As Decimal

    Dim tmpGenkagaku As Decimal
    Dim tmpGentan As Decimal


    '-------------------
    '     初期設定
    '-------------------
    tmpKeta = Decimal.Parse(tmpDicDst("Keta"))        ' GetPcaSettingでPCAの得意先マスタより取得
    tmpBunbo = 1000 \ tmpKeta

    If tmpJyuryo >= 0 Then
      tmpGosa = 0.01
    Else
      tmpGosa = -0.01
    End If

    '-------------------
    '     数量計算
    '-------------------
    If tmpDicDst.ContainsKey("SURYO") Then
      tmpSuryo = Decimal.Parse(tmpDicDst("SURYO"))
    Else
      tmpSuryo = 0
    End If

    tmpSuryo += (Fix(tmpJyuryo \ tmpKeta + tmpGosa) / tmpBunbo)
    ComSetDictionaryVal(tmpDicDst, "SURYO", tmpSuryo.ToString())

    '-------------------
    '     原価額計算
    '-------------------
    If tmpDicDst.ContainsKey("GENKAGAKU") Then
      tmpGenkagaku = Decimal.Parse(tmpDicDst("GENKAGAKU"))
    Else
      tmpGenkagaku = 0
    End If
    tmpGentan = Decimal.Parse(tmpDicDst("GENTAN"))

    tmpGenkagaku += Fix(tmpGentan * (Fix(tmpJyuryo \ tmpKeta + tmpGosa) / tmpBunbo) + tmpGosa)
    ComSetDictionaryVal(tmpDicDst, "GENKAGAKU", tmpGenkagaku.ToString())

  End Sub

  ''' <summary>
  ''' 端数処理方法取得
  ''' </summary>
  ''' <param name="tmpDicDst"></param>
  ''' <remarks>PCAの得意先マスタより取得</remarks>
  Private Sub GetPcaSetting(ByRef tmpDicDst As Dictionary(Of String, String))
    Dim tmpDb As New clsPcaDb
    Dim tmpDt As New DataTable

    Try
      With tmpDb

        .GetResult(tmpDt, "SELECT * FROM TMS WHERE tms_tcd = '" & tmpDicDst("TOKUISAKIC").PadLeft(clsPcaDb.CUSTOMER_CODE_LENGTH, "0"c) & "'")

        If tmpDt.Rows.Count > 0 Then
          ComSetDictionaryVal(tmpDicDst, "khasu", tmpDt.Rows(0)("tms_khasu").ToString())
          ComSetDictionaryVal(tmpDicDst, "Hasuu", tmpDt.Rows(0)("tms_hasu").ToString())
        Else
          ComSetDictionaryVal(tmpDicDst, "khasu", "0")
          ComSetDictionaryVal(tmpDicDst, "Hasuu", "0")
        End If
      End With

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA得意先マスタの取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

  End Sub


  ''' <summary>
  ''' 商品変換テーブル取得
  ''' </summary>
  ''' <param name="tmpDataTable">商品変換テーブル</param>
  ''' <param name="tmpTKCode">得意先コード</param>
  ''' <param name="tmpSetKbn">セット区分</param>
  ''' <param name="tmpSCode">商品コード(得意先コード or セットコード)</param>
  Private Sub GetShenkanTbl(ByRef tmpDataTable As DataTable _
                                , tmpTKCode As String _
                                , tmpSetKbn As String _
                                , tmpSCode As String)
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM SHENKAN "
    sql &= " WHERE TKCODE = " & tmpTKCode
    sql &= "   AND SETKBN = " & tmpSetKbn
    sql &= "   AND SCODE = " & tmpSCode

    Try
      tmpDb.GetResult(tmpDataTable, sql)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("商品変換テーブルの取得に失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' 明細行変更確認
  ''' </summary>
  ''' <returns></returns>
  Private Function ChkNewLine(tmpMSETCODE As Integer _
                              , tmpDicRead As Dictionary(Of String, String) _
                              , tmpDicLast As Dictionary(Of String, String) _
                              , tmpDbCutJ As T.R.ZCommonClass.clsSqlServer _
                              , ByRef tmpSlipNumber As Long) As Boolean
    Dim ret As Boolean = False

    'If tmpMSETCODE > 0 Then

    '  ' セット出庫
    '  If tmpDicRead("LOG_URIAGEBI") <> tmpDicLast("URIAGEBI") _
    '                    Or tmpDicRead("UTKCODE") <> tmpDicLast("TOKUISAKIC") _
    '                    Or tmpDicRead("SETCD") <> tmpDicLast("SETCODE") Then

    '    ret = True

    '  End If

    'Else
    ' 単品出庫

    ' 初回読込時(tmpPostData.Keys.Count = 0)はブレイクさせない
    If tmpDicLast.Keys.Count > 0 Then
      If tmpDicRead("NohinDay") <> tmpDicLast("URIAGEBI") _
                        Or tmpDicRead("TokuiCD") <> tmpDicLast("TOKUISAKIC") _
                        Or tmpDicRead("ShohinCD") <> tmpDicLast("SYOHINC") _
                         Or tmpDicRead("GyoNo") <> tmpDicLast("GYONO") Then

        ret = True
      End If
    End If
    'End If


    Return ret
  End Function

  ''' <summary>
  ''' 画面で選択された情報をURIAGEテーブルへ保存
  ''' </summary>
  ''' <param name="tmpSrcData"></param>
  Private Sub Dic2Db(tmpDb As T.R.ZCommonClass.clsSqlServer _
                     , tmpSrcData As Dictionary(Of String, String))

    Try
      tmpDb.Execute(SqlInsUriage(tmpSrcData))
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("売上ログテーブルへの書き込みに失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' URIAGEテーブルワーク作成
  ''' </summary>
  Private Sub CreateWkTable(tmpDb As T.R.ZCommonClass.clsSqlServer)

    Try
      tmpDb.Execute(SqlCreateWorkTbl())
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("一時テーブルの作成に失敗しました")
    End Try

  End Sub

  ''' <summary>
  ''' 伝票番号の採番を行う
  ''' </summary>
  ''' <returns>伝票番号</returns>
  Private Function AssignNumber(prmDb As T.R.ZCommonClass.clsSqlServer, Optional prmDenNoTable As String = "TBL_DENNO") As Long
    Dim ret As Long = Long.MinValue
    Dim tmpDt As New DataTable

    Try
      With prmDb
        .GetResult(tmpDt, " SELECT * FROM " & prmDenNoTable)

        If tmpDt.Rows.Count <= 0 Then
          Throw New Exception("伝票番号管理テーブル不正")
        Else
          If Long.Parse(tmpDt.Rows(0)("DENNO")) + 1 > 999999 Then
            ret = 500000
          Else
            ret = Long.Parse(tmpDt.Rows(0)("DENNO")) + 1
          End If
        End If

        ' 伝票番号更新
        .Execute("UPDATE " & prmDenNoTable & " SET DENNO =" & ret.ToString())

      End With

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("伝票番号の採番に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return ret
  End Function


  Private Sub UnKnownProc(tmpDbCutJ As T.R.ZCommonClass.clsSqlServer _
                          , ByRef prmPostData As Dictionary(Of String, String))

    Dim tmpGJYURYO As Long = 0
    Dim tmpLJYURYO As Long = 0
    Dim tmpRJYURYO As Long = 0
    Dim tmpWJYURYO As Long = 0
    Dim tmpGENTAN As Long = 0

    Try
      ' 実績テーブル(CUTJ)より左右別枝重量合計取得
      Using tmpDt As New DataTable
        tmpDbCutJ.GetResult(tmpDt, SqlSelCutJ(prmPostData))

        If tmpDt.Rows.Count > 0 Then
          tmpLJYURYO = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("LeftSide")))
          tmpRJYURYO = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("RightSide")))
          tmpGJYURYO = tmpLJYURYO + tmpRJYURYO
        End If

      End Using



      ' 枝情報テーブル(EDAB)より枝重量・仕入原価取得

      Using tmpDt As New DataTable
        tmpDbCutJ.GetResult(tmpDt, SelSelEdab(prmPostData))

        If tmpDt.Rows.Count <= 0 Then
          ' 枝情報無し
          ComSetDictionaryVal(prmPostData, "FLG2", "2")
        Else
          Dim tmpJyuryo01 As Long = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("JYURYO1").ToString()))
          Dim tmpJyuryo02 As Long = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("JYURYO2").ToString()))
          Dim tmpJyuryo As Long = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("JYURYO").ToString()))

          Dim tmpSIIREGAKU1 As Long = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("SIIREGAKU1").ToString()))
          Dim tmpSIIREGAKU2 As Long = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("SIIREGAKU2").ToString()))
          Dim tmpSIIREGAKU As Long = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("SIIREGAKU").ToString()))

          Select Case prmPostData("gSAYU")
            'Case PARTS_SIDE_LEFT.ToString()
            '  tmpWJYURYO = tmpJyuryo01 - tmpLJYURYO
            '  tmpGENTAN = tmpSIIREGAKU1
            'Case PARTS_SIDE_RIGHT.ToString()
            '  tmpWJYURYO = tmpJyuryo02 - tmpRJYURYO
            '  tmpGENTAN = tmpSIIREGAKU2
            Case Else
              If tmpJyuryo > 0 Then
                tmpWJYURYO = tmpJyuryo
              Else
                tmpWJYURYO = tmpJyuryo01 + tmpJyuryo02
              End If
              tmpWJYURYO = tmpWJYURYO - tmpGJYURYO
              tmpGENTAN = tmpSIIREGAKU
          End Select

          If prmPostData("KU") = "1" Then
            ' 返品時は重量をマイナス
            tmpWJYURYO = tmpWJYURYO * -1
          End If

          ComSetDictionaryVal(prmPostData, "JYURYO", tmpWJYURYO.ToString())

          ' 原単価更新
          If tmpGENTAN = 0 Then
            If tmpSIIREGAKU > 0 Then
              tmpGENTAN = tmpSIIREGAKU
            ElseIf tmpSIIREGAKU1 > 0 Then
              tmpGENTAN = tmpSIIREGAKU1
            ElseIf tmpSIIREGAKU2 > 0 Then
              tmpGENTAN = tmpSIIREGAKU2
            End If
          End If
          ComSetDictionaryVal(prmPostData, "GENTAN", tmpGENTAN.ToString())

          ' 枝別精算表からの原価取得
          Call SetEdaSeiGenka(prmPostData)

          ' 数量・原価額更新
          prmPostData("SURYO") = "0"
          prmPostData("GENKAGAKU") = "0"
          Call AddSuryoGenka(tmpWJYURYO, prmPostData)
          ' 集計処理(粗利・内税・外税・税込金額・税区分取得)
          Call TallieUp(prmPostData)

        End If
      End Using

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("不明処理エラー")
    End Try

  End Sub

  Private Sub SetEdaSeiGenka(ByRef prmPostData As Dictionary(Of String, String))
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer
    Dim tmpDt As New DataTable


    Try
      tmpDb.GetResult(tmpDt, SqlSelSeisan(prmPostData))
      If tmpDt.Rows.Count > 0 Then
        ComSetDictionaryVal(prmPostData, "GENTAN", tmpDt.Rows(0)("STANKA").ToString())
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("枝別精算表原価の取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

  End Sub

  Private Function GetSrCode(ByRef prmPostData As Dictionary(Of String, String)) As String
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer
    Dim tmpDt As New DataTable
    Dim ret As String = ""

    Try
      tmpDb.GetResult(tmpDt, SqlSelCutJSrCode(prmPostData))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0)("SRCODE").ToString()
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("仕入先コードの取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try
    Return ret
  End Function

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 任意個体の枝別精算表情報を取得するSQL文の作成
  ''' </summary>
  ''' <param name="prmPostData"></param>
  ''' <returns>作成したいSQL文</returns>
  Private Function SqlSelSeisan(prmPostData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM SEISAN "
    sql &= " WHERE FORMAT(convert(int, KOTAINO),'0000000000') = '" & prmPostData("KOTAINO").PadLeft(10, "0"c) & "'"
    sql &= "  AND EDABAN = " & prmPostData("EDABAN")

    Return sql
  End Function

  ''' <summary>
  ''' 任意個体の枝情報を取得するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SelSelEdab(prmPostData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM EDAB "
    sql &= " WHERE KUBUN <> 9 "
    sql &= "  AND KOTAINO = " & prmPostData("KOTAINO")
    sql &= "  AND EBCODE = " & prmPostData("EDABAN")

    Return sql
  End Function

  ''' <summary>
  ''' CUTJより左右別重量を取得するSQL文の作成
  ''' </summary>
  ''' <param name="prmPostData"></param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''  対象の得意先に出荷済で、今回送信対象外の左右別枝重量合計を求める
  ''' </remarks>
  Private Function SqlSelCutJ(prmPostData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    'sql &= " SELECT  IsNull(SUM(case when SAYUKUBUN = " & PARTS_SIDE_LEFT.ToString() & " then JYURYO else 0 end) ,0) As LeftSide "
    'sql &= "       , IsNull(SUM(case when SAYUKUBUN = " & PARTS_SIDE_RIGHT.ToString() & " then JYURYO else 0 end) ,0) as RightSide "
    'sql &= " From CUTJ  WITH(nolock) "
    'sql &= " WHERE NSZFLG = 2 "
    'sql &= "   And SYUKKABI <= '" & prmPostData("URIAGEBI") & "'"
    'sql &= "   AND KOTAINO = " & prmPostData("KOTAINO")
    'sql &= "   AND UTKCODE = " & prmPostData("TOKUISAKIC")
    'sql &= "   AND BICODE NOT IN ( " & String.Join(",", clsGlobalData.listWageCode) & ") "

    'If prmPostData("KU") = "0" Then
    '  sql &= " AND JYURYO > 0 "
    '  sql &= " AND DENNO <> " & prmPostData("WK_DenCnt")
    'Else
    '  sql &= " AND JYURYO < 0 "
    '  sql &= " AND NDENNO <> " & prmPostData("WK_DenCnt")
    'End If

    Return sql
  End Function

  Private Function SqlUpdCutJ(prmSrcData As Dictionary(Of String, String) _
                              , prmSlipNumber As Long _
                              , prmLineNumber As Long) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    If prmSrcData("NKUBUN") = "1" Then
      sql &= " SET NDENNO = " & prmSlipNumber.ToString()
      sql &= "   , NGYONO = " & prmLineNumber.ToString()
    Else
      sql &= " SET DENNO = " & prmSlipNumber.ToString()
      sql &= "   , GYONO = " & prmLineNumber.ToString()
    End If
    sql &= "     , KDATE ='" & ComGetProcTime() & "'"
    sql &= SqlWhereText()
    sql &= "   AND EBCODE =    " & prmSrcData("EBCODE")
    sql &= "   AND KDATE =   ' " & prmSrcData("KDATE") & "'"
    sql &= "   AND BICODE =    " & prmSrcData("BICODE")
    sql &= "   AND TOOSINO =   " & prmSrcData("TOOSINO")
    sql &= "   AND KAKOUBI = ' " & prmSrcData("KAKOUBI") & "'"
    sql &= "   AND SAYUKUBUN = " & prmSrcData("SAYUKUBUN")
    sql &= "   AND TKCODE =    " & prmSrcData("TKCODE")
    sql &= "   AND NKUBUN =    " & prmSrcData("NKUBUN")
    sql &= "   AND SIRIALNO =    " & prmSrcData("SIRIALNO")

    Return sql
  End Function

  Private Function SqlSelCutJSrCode(prmSrcData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= " SELECT SRCODE "
    sql &= " FROM CUTJ  "
    sql &= " WHERE SYUKKABI =   ' " & prmSrcData("SYUKKABI") & "'"
    sql &= " AND UTKCODE =    " & prmSrcData("UTKCODE")
    sql &= " AND BICODE =    " & prmSrcData("BICODE")

    Return sql
  End Function

  ''' <summary>
  ''' URIAGEテンポラリテーブル作成SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlCreateWorkTbl() As String
    Dim sql As String = String.Empty

    sql &= "  DROP TABLE IF EXISTS #WK_URIAGE;"

    sql &= " CREATE TABLE #WK_URIAGE( "
    sql &= "  [DENKU] [numeric](2, 0) NULL, "
    sql &= "  [NENGOU] [numeric](2, 0) NULL, "
    sql &= "  [URIAGEBI] [date] NULL, "
    sql &= "  [SEIKYUBI] [date] NULL, "
    sql &= "  [DENPYOUNO] [numeric](8, 0) NULL, "
    sql &= "  [GYOBAN] [numeric](2, 0) NULL, "
    sql &= "  [TOKUISAKIC] [nvarchar](14) NULL, "
    sql &= "  [TOKUISAKIMEI] [nvarchar](50) NULL, "
    sql &= "  [CHOKUSOUC] [nvarchar](14) NULL, "
    sql &= "  [SENPOUTANTOU] [nvarchar](30) NULL, "
    sql &= "  [BUMONC] [numeric](6, 0) NULL, "
    sql &= "  [TANTOUC] [numeric](6, 0) NULL, "
    sql &= "  [TEKIYOUC] [numeric](6, 0) NULL, "
    sql &= "  [TEKIYOUMEI] [nvarchar](40) NULL, "
    sql &= "  [BUNRUI] [nvarchar](5) NULL, "
    sql &= "  [DENPYOKU] [nvarchar](2) NULL, "
    sql &= "  [SYOHINC] [nvarchar](14) NULL, "
    sql &= "  [MASKUBUN] [numeric](2, 0) NULL, "
    sql &= "  [HINMEI] [nvarchar](50) NULL, "
    sql &= "  [KU] [numeric](2, 0) NULL, "
    sql &= "  [SOUKO] [nvarchar](4) NULL, "
    sql &= "  [IRISU] [decimal](10, 2) NULL, "
    sql &= "  [HAKOSU] [decimal](10, 2) NULL, "
    sql &= "  [SURYO] [decimal](10, 2) NULL, "
    sql &= "  [TANNI] [nvarchar](10) NULL, "
    sql &= "  [TANKA] [numeric](6, 0) NULL, "
    sql &= "  [KINGAKU] [decimal](18, 0) NULL, "
    sql &= "  [GENTAN] [numeric](6, 0) NULL, "
    sql &= "  [GENKAGAKU] [decimal](18, 0) NULL, "
    sql &= "  [ARARI] [decimal](18, 0) NULL, "
    sql &= "  [SOTOZEI] [decimal](18, 0) NULL, "
    sql &= "  [UCHIZEI] [decimal](18, 0) NULL, "
    sql &= "  [ZEIKU] [numeric](2, 0) NULL, "
    sql &= "  [ZEIKOMI] [numeric](2, 0) NULL, "
    sql &= "  [BIKOUKU] [numeric](2, 0) NULL, "
    sql &= "  [BIKOU] [nvarchar](20) NULL, "
    sql &= "  [HYOJYUN] [decimal](18, 0) NULL, "
    sql &= "  [NYUKA] [numeric](2, 0) NULL, "
    sql &= "  [URITAN] [decimal](18, 0) NULL, "
    sql &= "  [BAIKAKIN] [decimal](18, 0) NULL, "
    sql &= "  [KIKAKU] [nvarchar](40) NULL, "
    sql &= "  [IRO] [nvarchar](9) NULL, "
    sql &= "  [SIZE] [nvarchar](7) NULL, "
    sql &= "  [SIKI] [numeric](6, 0) NULL, "
    sql &= "  [SKOUMOKU1] [decimal](18, 0) NULL, "
    sql &= "  [SKOUMOKU2] [decimal](18, 0) NULL, "
    sql &= "  [SKOUMOKU3] [decimal](18, 0) NULL, "
    sql &= "  [UKOUMOKU1] [decimal](18, 0) NULL, "
    sql &= "  [UKOUMOKU2] [decimal](18, 0) NULL, "
    sql &= "  [UKOUMOKU3] [decimal](18, 0) NULL, "
    sql &= "  [UPDATE] [datetime] NULL, "
    sql &= "  [MTOKUISAKIC] [numeric](6, 0) NULL, "
    sql &= "  [MSYOHINC] [numeric](6, 0) NULL, "
    sql &= "  [FLG] [numeric](2, 0) NULL, "
    sql &= "  [FLG2] [numeric](2, 0) NULL, "
    sql &= "  [JYOUJYOU] [nvarchar](5) NULL, "
    sql &= "  [BRAND_NAME] [nvarchar](30) NULL, "
    sql &= "  [KAKU_NAME] [nvarchar](20) NULL, "
    sql &= "  [DenNo] [nvarchar](6) NULL, "
    sql &= "  [GyoNo] [nvarchar](3) NULL "
    sql &= " )"


    Return sql
  End Function


  ''' <summary>
  ''' JISSEKIテーブル挿入クエリの作成
  ''' </summary>
  ''' <param name="tmpSrcData">挿入データ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdateJisseki(tmpSrcData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    sql &= " UPDATE TRN_JISSEKI "
    sql &= " SET DenNo2 = '" & tmpSrcData("DENPYOUNO").PadLeft(6, "0"c) & "'"
    sql &= " WHERE DenNo = '" & tmpSrcData("DenNo") & "'"
    sql &= " AND GyoNo = " & tmpSrcData("GyoNo")

    Return sql
  End Function

  ''' <summary>
  ''' URIAGEテーブル挿入クエリの作成
  ''' </summary>
  ''' <param name="tmpSrcData">挿入データ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsUriageFromWk(tmpSrcData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    Dim tmpVal As String = String.Empty
    Dim tmpDst As String = String.Empty

    ' 項目名 Updateは予約語の為、[Update]に変換する
    tmpSrcData.Add("[UPDATE]", "'" & tmpSrcData("UPDATE") & "'")
    tmpSrcData.Remove("UPDATE")

    For Each tmpKey As String In tmpSrcData.Keys

      ' 上場番号、ブランド名、格付名はURIAGEテーブルに保持しない（三弘食品特殊仕様の為）
      If tmpKey = "JYOUJYOU" OrElse tmpKey = "BRAND_NAME" OrElse tmpKey = "KAKU_NAME" OrElse tmpKey = "DenNo" OrElse tmpKey = "GyoNo" Then  Continue For

      tmpDst &= tmpKey & ","

      If tmpKey = "URIAGEBI" _
        Or tmpKey = "SEIKYUBI" _
        Or tmpKey = "TANNI" _
        Or tmpKey = "BIKOU" _
        Or tmpKey = "KIKAKU" _
        Or tmpKey = "IRO" _
        Or tmpKey = "SIZE" _
        Or tmpKey = "TOKUISAKIMEI" _
        Or tmpKey = "HINMEI" _
        Or tmpKey = "CHOKUSOUC" _
        Or tmpKey = "SENPOUTANTOU" _
        Or tmpKey = "TEKIYOUMEI" _
        Or tmpKey = "BUNRUI" _
        Or tmpKey = "DENPYOKU" _
        Or tmpKey = "SKOUMOKU1" Then

        tmpVal &= "'" & tmpSrcData(tmpKey) & "',"
      Else
        tmpVal &= tmpSrcData(tmpKey) & ","
      End If
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO URIAGE(" & tmpDst & ")"
    sql &= "             VALUES(" & tmpVal & ")"

    Return sql

  End Function


  ''' <summary>
  ''' URIAGEテーブル作成SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsUriage(tmpSrcData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    Dim tmpKeyValue As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    With tmpKeyValue
      .Add("URIAGEBI", "'" & tmpSrcData("URIAGEBI") & "'")
      .Add("SEIKYUBI", "'" & tmpSrcData("URIAGEBI") & "'")
      .Add("DENPYOUNO", "'" & tmpSrcData("WK_DenCnt").PadLeft(6, "0"c) & "'")
      .Add("GYOBAN", tmpSrcData("GYONO"))
      .Add("TOKUISAKIC", "'" & tmpSrcData("TOKUISAKIC") & "'")
      .Add("TOKUISAKIMEI", "'" & tmpSrcData("TOKUISAKIMEI") & "'")
      .Add("TANTOUC", "'" & tmpSrcData("TANTOUC") & "'")
      .Add("SYOHINC", "'" & tmpSrcData("SYOHINC") & "'")
      .Add("HINMEI", "'" & tmpSrcData("HINMEI") & "'")
      .Add("KU", tmpSrcData("KU"))
      .Add("SURYO", If(String.IsNullOrWhiteSpace(tmpSrcData("SURYO")), "0", tmpSrcData("SURYO")))
      .Add("TANNI", "'kg'")
      .Add("TANKA", tmpSrcData("TANKA"))
      .Add("Kingaku", tmpSrcData("Kingaku"))
      .Add("ARARI", "0")
      .Add("SOTOZEI", "0")
      .Add("UchiZei", "0")
      .Add("ZEIKOMI", "0")
      .Add("BIKOU", "'" & "" & "'")
      .Add("KIKAKU", "'" & Strings.Left(tmpSrcData("GENSANCHI") _
                         & "　" & tmpSrcData("HINSYU") _
                         & "　" & tmpSrcData("BRAND_NAME"), 36) & "'")
      .Add("IRO", "'" & "0" & "'")
      .Add("Size", "'" & "" & "'")
      .Add("SKOUMOKU2", "'" & "0" & "'")
      .Add("MTOKUISAKIC", tmpSrcData("TOKUISAKIC"))
      .Add("MSYOHINC", tmpSrcData("MSYOHINC"))
      .Add("FLG2", "'" & tmpSrcData("FLG2") & "'")
      .Add("SKOUMOKU3", "'" & "0" & "'")
      .Add("ZEIKU", "0")
      .Add("[Update]", "'" & ComGetProcTime() & "'")
      .Add("GENTAN", "0")
      .Add("GENKAGAKU", "0")

      '.Add("BRAND_NAME", "'" & tmpSrcData("BRAND_NAME") & "'")
      '.Add("JYOUJYOU", "'" & tmpSrcData("JYOUJYOU") & "'")

      .Add("DENKU", "0")
      .Add("NENGOU", "1")
      .Add("CHOKUSOUC", "''")
      .Add("SENPOUTANTOU", "' '")
      .Add("BUMONC", "0")
      .Add("TEKIYOUC", "0")
      .Add("TEKIYOUMEI", "'" & tmpSrcData("JYOUJYOU") & "'")
      .Add("BUNRUI", "' '")
      .Add("DENPYOKU", "' '")
      .Add("MASKUBUN", "0")
      .Add("SOUKO", "0")
      .Add("IRISU", "0")
      .Add("HAKOSU", "0")
      .Add("BIKOUKU", "0")
      .Add("HYOJYUN", "0")
      .Add("NYUKA", "1")
      .Add("URITAN", "0")
      .Add("BAIKAKIN", "0")
      .Add("SIKI", "0")
      .Add("SKOUMOKU1", tmpSrcData("GyoNo").PadLeft(3, "0"c))
      .Add("UKOUMOKU1", "0")
      .Add("UKOUMOKU2", "0")
      .Add("UKOUMOKU3", "0")
      .Add("DenNo", "'" & tmpSrcData("DenNo").PadLeft(6, "0"c) & "'")
      .Add("GyoNo", "'" & tmpSrcData("GyoNo").PadLeft(3, "0"c) & "'")
      .Add("FLG", "1")
      '.Add("KAKU_NAME", "'" & If(tmpSrcData("KAKU_CODE") = 0, "", tmpSrcData("KAKU_NAME")) & "'")

    End With

    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyValue(tmpKey) & ","
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO #WK_URIAGE( " & tmpDst & ")"
    sql &= "             VALUES(" & tmpVal & ")"

    Return sql
  End Function

  ''' <summary>
  ''' 未送信データ件数取得SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelGetUnSendCount() As String
    Dim sql As String = String.Empty

    sql &= " SELECT count(*) AS UnSendCount "
    sql &= " FROM (" & SqlSelItemDetail() & ") as SRC "
    sql &= " WHERE SRC.POST_STAT = '未完了' "

    Return sql
  End Function

  ''' <summary>
  ''' 出荷明細一覧取得SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelItemDetail() As String
    Dim sql As String = String.Empty

    sql &= " SELECT IIF(URIAGE.DENPYOUNO is null,'未完了','完了') AS POST_STAT "
    sql &= "      , IIF(URIAGE.DENPYOUNO is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2) DenNo "
    sql &= "      , IIF(URIAGE.DENPYOUNO is null,TRN_JISSEKI.GyoNo ,URIAGE.GYOBAN) GyoNo " '後で併せる必要ある
    sql &= "      , NohinDay "
    sql &= "      , TokuiCD "
    sql &= "      , TokuiNM "
    sql &= "      , TRN_JISSEKI.ShohinCD "
    sql &= "      , TRN_JISSEKI.ShohinNM "
    sql &= "      , TRN_JISSEKI.Suryo "
    sql &= "      , TRN_JISSEKI.UriTanka "
    sql &= "      , TRN_JISSEKI.UriageKin "
    sql &= " FROM TRN_JISSEKI "
    sql &= " LEFT JOIN TBL_DENNO_RELATION "
    sql &= " ON TBL_DENNO_RELATION.OldDenNo = TRN_JISSEKI.DenNo "
    sql &= " AND TBL_DENNO_RELATION.OldGyoNO = TRN_JISSEKI.GyoNo  "
    sql &= " LEFT JOIN URIAGE "
    sql &= " ON FORMAT(URIAGE.DENPYOUNO, '000000') = TBL_DENNO_RELATION.NewDenNO "
    sql &= " AND URIAGE.GYOBAN = TBL_DENNO_RELATION.NewGyoNO　"
    'sql &= " LEFT JOIN URIAGE "
    'sql &= " ON FORMAT(URIAGE.DENPYOUNO,'000000') = TRN_JISSEKI.DenNo2 "
    'sql &= " AND URIAGE.SKOUMOKU1 = TRN_JISSEKI.GyoNo "

    sql &= SqlWhereText()

    Return sql
  End Function

  ''' <summary>
  ''' 売り上げ変換対象データ抽出条件
  ''' </summary>
  ''' <returns>SQL文（Where句）</returns>
  Private Function SqlWhereText() As String
    Dim sql As String = String.Empty
    Dim tmpTargetDate As String = Me.CmbDateProcessing1.SelectedValue.ToString()

    If tmpTargetDate.Equals(String.Empty) Then
      tmpTargetDate = ComGetProcDate()
    End If

    tmpTargetDate = tmpTargetDate.Replace("/", "")

    sql &= " WHERE PCAFLG = 0 "
    sql &= "  And NohinDay = '" & tmpTargetDate & "'"
    If Me.CmbMstCustomer1.SelectedValue IsNot Nothing Then
      sql &= " AND TokuiCD = " & Me.CmbMstCustomer1.SelectedValue.ToString()
    End If

    Return sql
  End Function

#End Region

#End Region

#End Region

#Region "イベントプロシージャー"

#Region "フォーム"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_EdaSyuko_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' IPC通信起動
    InitIPC(PRG_ID)

    Me.Text = PRG_TITLE

    ' 出荷日のコンボボックスを先頭に設定
    Me.CmbDateProcessing1.SelectedIndex = 0
    _DeliveryDate = CmbDateProcessing1.SelectedValue.ToString
    ' 画面初期化
    Call InitForm02()

    ' 抽出条件変更イベント設定
    AddHandler CmbDateProcessing1.Validated, AddressOf SearchConditionChanged
    AddHandler CmbDateProcessing1.SelectedIndexChanged, AddressOf SearchConditionChanged
    AddHandler CmbMstCustomer1.Validated, AddressOf SearchConditionChanged
    AddHandler CmbMstCustomer1.SelectedIndexChanged, AddressOf SearchConditionChanged

    ' 得意先一覧イベント設定
    With Controlz(Me.DG2V1.Name)
      .lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick  ' ダブルクリック時処理
      .lcCallBackReLoadData = AddressOf ReLoadDataCustomerList   ' 再表示時処理
    End With

    ' 出荷明細一覧イベント設定
    With Controlz(Me.DG2V2.Name)
      .lcCallBackReLoadData = AddressOf ReLoadDataItemDetail   ' 再表示時処理
    End With

    ' 一覧表示
    Me.Controlz(Me.DG2V1.Name).ShowList()
    Me.Controlz(Me.DG2V2.Name).ShowList()

    ' 並び替えOFF
    ' 表示順と売上伝票作成のロジックが同期している為、並び替えは出来ません
    Controlz(DG2V2.Name).SortActive = False

    ' メッセージラベル設定
    Me.SetMsgLbl(Me.lblInformation)

    ' 非表示 → 表示時処理設定
    MyBase.lcCallBackShowFormLc = AddressOf ReStartPrg

    Me.ActiveControl = Me.CmbDateProcessing1
  End Sub

  ''' <summary>
  ''' フォームキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      Case Keys.F1
        ' 売上明細データ作成
        With btnPostPca
          .Focus()
          .PerformClick()
        End With
      Case Keys.F5
        ' 売上作成ログ表示
        With btnShowLogForm
          .Focus()
          .PerformClick()
        End With
      Case Keys.F12
        ' 終了
        With btnEnd
          .Focus()
          .PerformClick()
        End With
    End Select

  End Sub

  ''' <summary>
  ''' 画面再表示時処理
  ''' </summary>
  ''' <remarks>
  ''' 非表示→表示時に実行
  ''' FormLoad時に設定
  ''' </remarks>
  Private Sub ReStartPrg()
    With Me.CmbDateProcessing1
      .InitCmb()
      .SelectedIndex = 0
    End With

    With Me.CmbMstCustomer1
      .InitCmb()
      .SelectedIndex = -1
    End With

    Controlz(DG2V1.Name).ShowList()
    Controlz(DG2V2.Name).ShowList()

  End Sub

#End Region

#Region "コンボボックス"

  ''' <summary>
  ''' 抽出条件変更時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  '''  出荷日・得意先コンボボックスが変更されたタイミングで一覧の再表示を行う
  ''' </remarks>
  Private Sub SearchConditionChanged(sender As Object, e As EventArgs)
    Static SrcSqlCunstomerList As String
    Static SrcSqlItemDetailList As String

    If (_DeliveryDate <> Me.CmbDateProcessing1.SelectedValue.ToString()) Then
      _DeliveryDate = Me.CmbDateProcessing1.SelectedValue.ToString()
    End If

    ' 得意先一覧再表示
    If SrcSqlCunstomerList <> CreateGrid2Src1() Then
      SrcSqlCunstomerList = CreateGrid2Src1()
      With Controlz(Me.DG2V1.Name)
        .ClearSelectedList()
        .AutoSearch = True
        .SrcSql = SrcSqlCunstomerList
        .AutoSearch = False
      End With
    End If

    ' 出荷明細一覧再表示
    If SrcSqlItemDetailList <> CreateGrid2Src2() Then
      SrcSqlItemDetailList = CreateGrid2Src2()
      With Controlz(Me.DG2V2.Name)
        .ClearSelectedList()
        .AutoSearch = True
        .SrcSql = SrcSqlItemDetailList
        .AutoSearch = False
      End With
    End If

  End Sub


#End Region

#Region "データグリッド"

  ''' <summary>
  ''' 得意先一覧ダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DgvCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
    Dim tmpCurrentData As Dictionary(Of String, String) = Me.Controlz(Me.DG2V1.Name).SelectedRow
    Dim tmpEqList As New Dictionary(Of String, String)

    tmpEqList.Add("DenNo", tmpCurrentData("DenNo"))
    tmpEqList.Add("POST_STAT", "未完了")

    If tmpCurrentData("SelecterCol") <> "" Then
      ' 一覧が選択された
      ' 得意先が一致し、未完了のデータ全てにチェックを付ける
      Controlz(DG2V2.Name).SetRowSelectMark(tmpEqList)
    Else
      ' 一覧の選択が解除された
      ' 得意先が一致し、未完了のデータ全てのチェックを外す
      Controlz(DG2V2.Name).UnSetRowSelectMark(tmpEqList)
    End If

    ' 送信予定件数表示
    Call DspPlanedCount()

  End Sub

  ''' <summary>
  ''' 得意先一覧再表示時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate"></param>
  ''' <param name="DataCount"></param>
  Private Sub ReLoadDataCustomerList(sender As DataGridView, LastUpdate As Date, DataCount As Long, DataJuryo As Decimal, DataKingaku As Decimal)
    Me.lblCustomerListStat.Text = LastUpdate.ToString("yyyy/MM/dd  HH:mm:ss ") & "現在 " & DataCount & "件"
  End Sub

  ''' <summary>
  ''' 出荷明細一覧再表示時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate"></param>
  ''' <param name="DataCount"></param>
  Private Sub ReLoadDataItemDetail(sender As DataGridView, LastUpdate As Date, DataCount As Long, DataJuryo As Decimal, DataKingaku As Decimal)
    Me.lblItemDetailListStat.Text = LastUpdate.ToString("yyyy/MM/dd  HH:mm:ss ") & "現在 " & DataCount & "件"

    Try
      ' 未送信件数表示
      Me.lblUnSendCount.Text = "未送信明細は、" & GetUnSendCount() & "件です。"
      ' 送信予定件数表示
      Call DspPlanedCount()
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

#End Region

#Region "ボタン"
  ''' <summary>
  ''' 売上明細データ作成ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPostPca_Click(sender As Object, e As EventArgs) Handles btnPostPca.Click
    Dim tmpDbCutJ As New T.R.ZCommonClass.clsSqlServer
    Dim tmpDbUriage As New T.R.ZCommonClass.clsSqlServer
    Dim ReportWkTable As String = "WK_NOHIN"
    Dim ReportName As String = String.Empty
    Dim ClsPrintingProcess As New ClsPrintingProcess.ClsPrintingProcess()
    '再発行タイプ
    Dim ReportType As String = ReadSettingIniFile("REPRINT_TYPE", "VALUE")
    Dim tmpWhereList As New Dictionary(Of String, String)
    Try

      If typMsgBoxResult.RESULT_YES _
             = ComMessageBox("売上データ受渡をしますか。操作を取りやめる時は[いいえ]を選択します" _
                              , PRG_TITLE _
                              , typMsgBox.MSG_NORMAL _
                              , typMsgBoxButton.BUTTON_YESNO) Then
        tmpDbCutJ.TrnStart()

        If Controlz(DG2V2.Name).SelectCount = 0 Then
          Throw New Exception("変換対象が選択されていません。")
        End If

        ' PCA売上伝票作成
        Call DataPost(tmpDbCutJ, tmpDbUriage, tmpWhereList)

        tmpDbCutJ.TrnCommit()

        ReportName = If(ReportType = ClsCommonGlobalData.REPORT_TYPE_SHUKKA, "R_SHUKKA", "R_NOHIN")

        ClsPrintingProcess.PrintProcess(ClsCommonGlobalData.PRINT_PREVIEW, ReportWkTable, ReportName, tmpWhereList)


        Call ComMessageBox("伝票の受渡が成功しました。", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)

        ' ------------------------------------
        '              再表示
        ' ------------------------------------
        With Controlz(Me.DG2V1.Name)            ' 得意先一覧
          .AutoSearch = True
          .SrcSql = CreateGrid2Src1()
          .ClearSelectedList()
          .AutoSearch = False
        End With

        With Controlz(Me.DG2V2.Name)            ' 出荷明細
          .AutoSearch = True
          .SrcSql = CreateGrid2Src2()
          .ClearSelectedList()
          .AutoSearch = False
        End With

      End If

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex, False)
      tmpDbCutJ.TrnRollBack()
    Finally
      tmpDbCutJ.Dispose()
      tmpDbUriage.Dispose()
    End Try
  End Sub

  ''' <summary>
  ''' 売上作成ログ表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnShowLogForm_Click(sender As Object, e As EventArgs) Handles btnShowLogForm.Click
    Using tmpFrm As New Form_OutConvLog
      tmpFrm.ShowDialog()
    End Using
  End Sub

  ''' <summary>
  ''' 終了ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click

    With Controlz(DG2V1.Name)
      .AutoSearch = False
      .ClearSelectedList()
      .ResetPosition()
    End With

    With Controlz(DG2V2.Name)
      .AutoSearch = False
      .ClearSelectedList()
      .ResetPosition()
    End With

    Me.CmbDateProcessing1.SelectedIndex = 0
    Me.CmbMstCustomer1.SelectedIndex = -1

    Me.Hide()
  End Sub

#End Region

#End Region

End Class
