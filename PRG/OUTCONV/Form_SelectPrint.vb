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
Imports T.R.ZCommonClass.clsCodeLengthSetting


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
  '得意先コンボボックス
  Private lastCmbMstCustomer As String
  '納品日
  Private lastNohinDate As String = String.Empty
  '得意先コード
  Private lastCustCd As String = String.Empty

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
    sql &= " SELECT IIF(TRN_JISSEKI.ShukaPRTFLG = 0,'未完了','完了') AS POST_STAT "
    sql &= "      , NohinDay "
    sql &= "      , TokuiCD "
    sql &= "      , TokuiNM "
    sql &= "      , IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2) DenNo "
    sql &= " FROM TRN_JISSEKI "
    sql &= SqlWhereText()
    sql &= " GROUP BY "
    sql &= "   NohinDay "
    sql &= " , TokuiCD "
    sql &= " , TokuiNM "
    sql &= " , IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2)  "
    sql &= " , TRN_JISSEKI.ShukaPRTFLG "
    sql &= " ORDER BY "
    sql &= "   NohinDay "
    sql &= " , IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2)  "

    Return sql
  End Function


  Private Function SqlSelAggregateData(prmDenNo As String)
    Dim sql As String = String.Empty

    sql &= "SELECT * "
    sql &= "FROM TRN_JISSEKI "
    sql &= "WHERE DENNO2 = '" & prmDenNo & "'"

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
      .Add(New clsDGVColumnSetting("納品日", "NohinDay", argTextAlignment:=typAlignment.MiddleCenter))
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
    sql &= " ORDER BY IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2)  "
    sql &= "     ,    IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.GyoNo ,TRN_JISSEKI.GyoNo2) "

    Return sql
  End Function

  ''' <summary>
  ''' 出荷明細一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("一括発行", "POST_STAT", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("行番号", "GyoNo", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("納品日", "NohinDay", argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("発送先コード", "TyokuCD", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("発送先名", "TyokuNM", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=220))
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
  ''' 確認メッセージ表示
  ''' </summary>
  ''' <param name="prmDbCutJ"></param>
  Private Sub ShowConfirmMessage()

    Try
      '発行済みが含まれるかのメッセージ表示
      For Each tmpGridData As Dictionary(Of String, String) In Controlz(DG2V2.Name).GetAllData()
        If tmpGridData("SelecterCol") = "〇" Then
          If tmpGridData("POST_STAT") = "完了" Then
            ComMessageBox("既に発行すみの納品書は破棄してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
            Exit Sub
          End If
        End If

      Next


    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("集約伝票の作成に失敗しました。" & vbCrLf & ex.Message)
    End Try


  End Sub

  ''' <summary>
  ''' 集約伝票作成
  ''' </summary>
  ''' <param name="prmDbCutJ"></param>
  Private Sub CreateShuyakuDenpyo(prmDbCutJ As T.R.ZCommonClass.clsSqlServer _
                             , ByVal prmDenpyoList As List(Of String))

    Try
      ' URIAGEデータ作成（WORKテーブルへの書き込み）
      Me.lblInformation.Text = "集約伝票を作成しています。しばらくお待ち下さい。"
      Call CreatePostData(prmDbCutJ, prmDenpyoList)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("集約伝票の作成に失敗しました。" & vbCrLf & ex.Message)
    End Try


  End Sub

  ''' <summary>
  ''' 送信データ作成
  ''' </summary>
  ''' <remarks>URIAGEテーブルにPCAに投げるデータを登録する</remarks>
  Private Sub CreatePostData(tmpDbCutJ As T.R.ZCommonClass.clsSqlServer _
                             , ByVal prmDenpyoList As List(Of String)
                             )
    Dim tmpSlipNumber As Long
    Dim tmpLineNumber As Long
    Dim tmpGridList As New List(Of Dictionary(Of String, String))
    Dim tmpTokuiCd As String = String.Empty
    Dim tmpHassoCD As String = String.Empty

    tmpGridList = Controlz(DG2V2.Name).GetAllData()
    'TODO 得意先コード、発送先コード、商品コードで並べる
    tmpGridList = tmpGridList _
    .OrderBy(Function(x) x("TokuiCD")) _
    .ThenBy(Function(x) x("TyokuCD")) _
    .ThenBy(Function(x) x("ShohinCD")) _
    .ToList()

    ' 伝票番号取得 
    'tmpSlipNumber = AssignNumber(tmpDbCutJ)
    'tmpLineNumber = 1

    For Each tmpGridData As Dictionary(Of String, String) In tmpGridList

      'TODO 得意先、発送先が変わったら、採番する。それまで行Noは更新
      If tmpTokuiCd <> tmpGridData("TokuiCD") _
        OrElse tmpHassoCD <> tmpGridData("TyokuCD") Then
        tmpSlipNumber = AssignNumber(tmpDbCutJ)
        tmpLineNumber = 1

        prmDenpyoList.Add(tmpSlipNumber.ToString.PadLeft(DENPYO_NUMBER_LENGTH, "0"c))
        tmpTokuiCd = tmpGridData("TokuiCD")
        tmpHassoCD = tmpGridData("TyokuCD")
      End If

      Dim UpdGridData As New Dictionary(Of String, String)

      ' 選択データのみ対象
      If tmpGridData("SelecterCol") <> "〇" Then
        Continue For
      End If

      UpdGridData = tmpGridData
      UpdGridData.Add("DenNo2", tmpSlipNumber)
      UpdGridData.Add("GyoNo2", tmpLineNumber)

      tmpDbCutJ.Execute(SqlUpdateJisseki(UpdGridData))

      tmpLineNumber += 1

    Next

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

  ''' <summary>
  ''' 集約伝票確認
  ''' </summary>
  ''' <param name="tmpSrcData">挿入データ</param>
  ''' <returns>伝票番号</returns>
  Private Function ChkAggregateData(prmDenNo As String) As Long
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer
    Dim tmpDt As New DataTable

    Try
      tmpDb.GetResult(tmpDt, SqlSelAggregateData(prmDenNo))

      Return tmpDt.Rows.Count
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("集約伝票データ確認処理に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return 0
  End Function

  ''' <summary>
  ''' 集約伝票確認
  ''' </summary>
  ''' <param name="tmpSrcData">挿入データ</param>
  ''' <returns>伝票番号</returns>
  Private Sub GetAggregateDenpyoResult(ByRef prmAggregateDataList As List(Of String))
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer
    Dim tmpDt As New DataTable

    Try
      For Each tmpGridData As Dictionary(Of String, String) In Controlz(DG2V1.Name).GetAllData()
        ' 選択データのみ対象
        If tmpGridData("SelecterCol") <> "〇" Then
          Continue For
        End If

        'すでに存在するならスルーする
        If prmAggregateDataList.Contains(tmpGridData("DenNo")) Then
          Continue For
        End If

        '集約伝票かどうかのチェック
        If ChkAggregateData(tmpGridData("DenNo")) <> 0 Then
          '集約伝票のときリストに追加
          prmAggregateDataList.Add(tmpGridData("DenNo"))
        End If
      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("集約伝票確認処理に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' JISSEKIテーブル挿入クエリの作成
  ''' </summary>
  ''' <param name="tmpSrcData">挿入データ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdateJisseki(tmpSrcData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    sql &= " UPDATE TRN_JISSEKI "
    sql &= " SET DenNo2 = '" & tmpSrcData("DenNo2").PadLeft(DENPYO_NUMBER_LENGTH, "0"c) & "'"
    sql &= " , GyoNo2 = '" & tmpSrcData("GyoNo2").PadLeft(GYO_NUMBER_LENGTH, "0"c) & "'"
    sql &= " , ShukaPRTFLG = 1 "
    sql &= " WHERE DenNo = '" & tmpSrcData("DenNo") & "'"
    sql &= " AND GyoNo = " & tmpSrcData("GyoNo")

    Return sql
  End Function

  ''' <summary>
  ''' 出荷明細一覧取得SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelItemDetail() As String
    Dim sql As String = String.Empty

    sql &= " SELECT IIF(TRN_JISSEKI.ShukaPRTFLG = 0,'未完了','完了') AS POST_STAT "
    sql &= "      , IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.DenNo ,TRN_JISSEKI.DenNo2) DenNo "
    sql &= "      , IIF(TRN_JISSEKI.DenNo2 is null,TRN_JISSEKI.GyoNo ,TRN_JISSEKI.GyoNo2) GyoNo " '後で併せる必要ある
    sql &= "      , NohinDay "
    sql &= "      , TyokuCD "
    sql &= "      , TyokuNM "
    sql &= "      , TokuiCD "
    sql &= "      , TokuiNM "
    sql &= "      , TRN_JISSEKI.ShohinCD "
    sql &= "      , TRN_JISSEKI.ShohinNM "
    sql &= "      , TRN_JISSEKI.Suryo "
    sql &= "      , TRN_JISSEKI.UriTanka "
    sql &= "      , TRN_JISSEKI.UriageKin "
    sql &= " FROM TRN_JISSEKI "
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
      sql &= " AND TokuiCD = '" & Me.CmbMstCustomer1.SelectedValue.ToString() & "'"
    End If

    Return sql
  End Function

  ''' <summary>
  ''' 取消し処理
  ''' </summary>
  ''' <param name="tmpSrcData">挿入データ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdateJissekiTorikeshi(prmDenNo As String) As String
    Dim sql As String = String.Empty
    sql &= " UPDATE TRN_JISSEKI "
    sql &= " SET DenNo2 = null"
    sql &= " , GyoNo2 = null"
    sql &= " , ShukaPRTFLG = 0 "
    sql &= " WHERE DenNo2 = '" & prmDenNo & "'"

    Return sql
  End Function

  ''' <summary>
  ''' 得意先が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstCustomer"></param>
  ''' <param name="prmTxtLabelCustomer"></param>
  Private Function CmbMstCustomerValidating(prmCmbMstCustomer As ComboBox, prmTxtLabelCustomer As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMstCustomer.Equals(prmCmbMstCustomer.Text) = False) Then
      lastCmbMstCustomer = prmCmbMstCustomer.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstCustomer.Text) Then
      ' 得意先コード
      prmCmbMstCustomer.Text = String.Empty
      prmTxtLabelCustomer.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      If (GetTKCode(prmCmbMstCustomer.Text, tmpDt)) Then
        ' 得意先コード
        prmCmbMstCustomer.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabelCustomer.Text = tmpDt.Rows(0)("Name").ToString
      Else
        ComMessageBox("得意先が存在しません。" _
                                , PRG_TITLE _
                                , typMsgBox.MSG_WARNING _
                                , typMsgBoxButton.BUTTON_OK)
        prmCmbMstCustomer.Text = String.Empty
        prmTxtLabelCustomer.Text = String.Empty
        rtn = True
      End If
    End If

    Return rtn
  End Function

  ''' <summary>
  ''' 得意先マスタ検索処理
  ''' </summary>
  ''' <param name="prmCode"></param>
  ''' <param name="prmDt"></param>
  ''' <returns></returns>
  Private Function GetTKCode(prmCode As String,
                                     ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer

    ' 実行
    With tmpDb
      Try

        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetTKCode(prmCode))
        If (prmDt.Rows.Count = 0) Then
          ret = False
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        ret = False
      End Try

    End With

    Return ret


  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <param name="prmTKCode">得意先コード</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetTKCode(prmTKCode As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT TokuiCd As Code"
    sql &= "    ,   TokuiNM1 As Name"
    sql &= "    ,   TokuiNM2 As TokuiNM2"
    sql &= "    ,   TokuiKana As TokuiKN"
    sql &= "    ,   ZipCD As TokuiZipCD"
    sql &= "    ,   Add1 As TokuiAdd1"
    sql &= "    ,   Add2 As TokuiAdd2"
    sql &= "    ,   TelNo As TokuiTel"
    sql &= "    ,   SenpoTanto As SenpoTantoNM"
    sql &= " FROM MST_TOKUISAKI "
    sql &= " WHERE 1 = 1 "
    sql &= " AND TokuiCd = '" & prmTKCode & "'"
    sql &= " ORDER BY TokuiCd "

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
  Private Sub Form_SelectPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' IPC通信起動
    'InitIPC(PRG_ID)

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

    '得意先コード設定値
    CmbMstCustomer1.SelectedIndex = -1
    lastCmbMstCustomer = CmbMstCustomer1.Text

    CmbMstCustomerValidating(CmbMstCustomer1, TxtMstCustomer1)

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
        ' 発行画面
        With btnPostPca
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

    CmbMstCustomerValidating(CmbMstCustomer1, TxtMstCustomer1)
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
  ''' 集約発行ボタン押下時処理
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
    Dim tmpDenpyoList As New List(Of String)
    Dim tmpWhereList As New Dictionary(Of String, List(Of String))
    Try

      If typMsgBoxResult.RESULT_YES _
             = ComMessageBox("選択した伝票を集約した納品書を発行します。よろしいですか？" _
                              , PRG_TITLE _
                              , typMsgBox.MSG_NORMAL _
                              , typMsgBoxButton.BUTTON_YESNO) Then
        tmpDbCutJ.TrnStart()

        If Controlz(DG2V2.Name).SelectCount = 0 Then
          Throw New Exception("変換対象が選択されていません。")
        End If

        '確認メッセージ表示チェック

        ' 集約伝票作成
        Call CreateShuyakuDenpyo(tmpDbCutJ, tmpDenpyoList)

        tmpDbCutJ.TrnCommit()

        ReportName = If(ReportType = ClsCommonGlobalData.REPORT_TYPE_SHUKKA, "R_SHUKKA", "R_NOHIN")

        tmpWhereList.Add("DenNO2", tmpDenpyoList)
        '印刷処理
        ClsPrintingProcess.PrintProcess(ClsCommonGlobalData.PRINT_PREVIEW, ReportWkTable, ReportName, tmpWhereList)

        Call ComMessageBox("納品書を発行しました。", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)


        ' ------------------------------------
        '              再表示
        ' ------------------------------------
        With Controlz(Me.DG2V1.Name)            ' 得意先一覧
          .AutoSearch = True
          .SrcSql = CreateGrid2Src1()
          .ClearSelectedList()
          .AutoSearch = False
        End With

        With Controlz(Me.DG2V2.Name)            ' 明細一覧
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
  ''' 取消しボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnShowLogForm_Click(sender As Object, e As EventArgs) Handles btnShowLogForm.Click
    Dim tmpAggregateDataList As New List(Of String)
    Dim tmpDb As New T.R.ZCommonClass.clsSqlServer

    Try
      '選択した集約伝票のチェック
      GetAggregateDenpyoResult(tmpAggregateDataList)

      'リストが空なら何もしない
      If tmpAggregateDataList.Count = 0 Then
        MessageBox.Show("集約伝票が選択されていません。")
        Exit Sub
      End If

      '集約伝票を表示し、〇〇の集約を取消します。よろしいですか？を表示する。
      '伝票番号を改行区切りでまとめる
      Dim denListText As String = String.Join(vbCrLf, tmpAggregateDataList)

      '確認メッセージ
      Dim msg As String =
        "以下の伝票の集約を取り消します。" & vbCrLf &
        denListText & vbCrLf &
        "よろしいですか？"

      If ComMessageBox(msg, "確認", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_YESNO) = DialogResult.No Then
        Exit Sub
      End If

      '取消し処理を実行
      For Each tmpDenNo In tmpAggregateDataList
        tmpDb.Execute(SqlUpdateJissekiTorikeshi(tmpDenNo))
      Next

      Call ComMessageBox("取消処理が完了しました。", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)

      ' ------------------------------------
      '              再表示
      ' ------------------------------------
      With Controlz(Me.DG2V1.Name)            ' 得意先一覧
        .AutoSearch = True
        .SrcSql = CreateGrid2Src1()
        .ClearSelectedList()
        .AutoSearch = False
      End With

      With Controlz(Me.DG2V2.Name)            ' 明細一覧
        .AutoSearch = True
        .SrcSql = CreateGrid2Src2()
        .ClearSelectedList()
        .AutoSearch = False
      End With


    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 終了ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click

    Close()


  End Sub

#End Region

#End Region

End Class
