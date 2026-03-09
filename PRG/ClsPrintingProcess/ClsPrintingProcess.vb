Imports Common
Imports Common.ClsFunction
Imports Common.ClsCommonGlobalData

Public Class ClsPrintingProcess
  ' ワークテーブル名

  ' １つ目のプロセスＩＤ
  Private Shared ryoProcesID_01 As System.Diagnostics.Process
  ' プロセスＩＤ
  Private Shared procesID As System.Diagnostics.Process
  ' 納品書用ワークテーブル
  Private tmpNouhinshoDT As New DataTable
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  Public Sub New()
  End Sub


  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property

  ''' <summary>
  ''' ACCESSファイルを開く
  ''' </summary>
  ''' <param name="printPreview">プレビューフラグ</param>
  ''' <param name="strReportName">レポートファイル名</param>
  ''' <param name="prmWaitFlag">待機フラグ</param>
  ''' <returns>
  ''' True :ファイルオープン成功
  ''' False:ファイルオープン失敗
  ''' </returns>
  Public Shared Function AccessRun(printPreview As Integer, strReportName As String, Optional prmWaitFlag As Boolean = False) As Boolean
    Try

      ' Threadオブジェクトを作成する
      Dim MultiProgram_run = New System.Threading.Thread(AddressOf DoRyoSomething01)
      ' １つ目のスレッドを開始する
      MultiProgram_run.Start(New prmReport(printPreview.ToString, strReportName))

      If (prmWaitFlag) Then
        MultiProgram_run.Join()
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Return False
    End Try

    Return True

  End Function

  ''' <summary>
  ''' １つ目の印刷スレッド
  ''' </summary>
  ''' <param name="arg"></param>
  Private Shared Sub DoRyoSomething01(arg As Object)

    Try
      Dim prm As prmReport = DirectCast(arg, prmReport)
      Dim myPath As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, ClsCommonGlobalData.REPORT_FILENAME)

      Dim strPrintPrwview As String
      If (prm.printPreview.Equals("1")) Then
        strPrintPrwview = "1"
      Else
        strPrintPrwview = "0"
      End If

      'ファイルを開く
      ryoProcesID_01 = System.Diagnostics.Process.Start(myPath, " /runtime /cmd " & strPrintPrwview & prm.strReportName)
      If ryoProcesID_01 IsNot Nothing Then
        '終了するまで待機する
        ryoProcesID_01.WaitForExit()
        ryoProcesID_01 = Nothing
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
    End Try

  End Sub

  ''' <summary>
  ''' ACCESSファイルを開く
  ''' </summary>
  ''' <param name="printPreview">プレビューフラグ</param>
  ''' <param name="strReportName">レポートファイル名</param>
  ''' <returns>
  ''' True :ファイルオープン成功
  ''' False:ファイルオープン失敗
  ''' </returns>
  Public Shared Function ComAccessRun(printPreview As Integer, strReportName As String) As Boolean
    Try

      ' Threadオブジェクトを作成する
      Dim MultiProgram_run = New System.Threading.Thread(AddressOf DoSomething01)
      ' １つ目のスレッドを開始する
      MultiProgram_run.Start(New prmReport(printPreview.ToString, strReportName))

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Return False
    End Try

    Return True

  End Function


  ''' <summary>
  ''' 印刷スレッド
  ''' </summary>
  ''' <param name="arg"></param>
  Private Shared Sub DoSomething01(arg As Object)

    Dim prm As prmReport = DirectCast(arg, prmReport)
    Dim myPath As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, ClsCommonGlobalData.REPORT_FILENAME)

    Dim strPrintPrwview As String
    If (prm.printPreview.Equals("1")) Then
      strPrintPrwview = "1"
    Else
      strPrintPrwview = "0"
    End If

    'ファイルを開く
    procesID = System.Diagnostics.Process.Start(myPath, " /runtime /cmd " & strPrintPrwview & prm.strReportName)
    If procesID IsNot Nothing Then
      '終了するまで待機する
      procesID.WaitForExit()
      procesID = Nothing
    End If

  End Sub

  ''' <summary>
  ''' プロセスの終了
  ''' </summary>
  Public Shared Sub ProcessKill()

    If procesID IsNot Nothing Then
      ' 起動した１つ目のプロセスの終了
      procesID.Kill()
      procesID = Nothing
    End If

  End Sub

  ''' <summary>
  ''' プロセス状態確認
  ''' </summary>
  ''' <returns></returns>
  Public Shared Function ProcessStatus() As Boolean

    Dim ret As Boolean = False

    If procesID IsNot Nothing Then
      ret = True
    End If

    Return ret

  End Function

  Public Overloads Sub PrintProcess(prmPreview As Integer, prmTableName As String, prmReportName As String, Optional ByRef prmWhereList As Dictionary(Of String, String) = Nothing)
    Dim tmpDt As New DataTable
    Try
      '対象データ取得
      SqlServer.GetResult(tmpDt, SqlGetPrintData(prmWhereList))

      '印刷処理
      If Not AccessPrint(prmPreview, prmTableName, prmReportName, tmpDt) Then
        Throw New Exception("印刷処理に失敗しました。")
      End If

      For Each tmpRow As DataRow In tmpDt.Rows
        SqlServer.Execute(SqlUpdPrintFlg(tmpRow, prmWhereList))
      Next
    Catch ex As Exception
      ComWriteErrLog(ex)
    End Try

  End Sub

  Public Overloads Sub PrintProcess(prmPreview As Integer, prmTableName As String, prmReportName As String, Optional ByRef prmWhereList As Dictionary(Of String, List(Of String)) = Nothing)
    Dim tmpDt As New DataTable
    Try
      '対象データ取得
      SqlServer.GetResult(tmpDt, SqlGetPrintData(prmWhereList))

      '印刷処理
      If Not AccessPrint(prmPreview, prmTableName, prmReportName, tmpDt) Then
        Throw New Exception("印刷処理に失敗しました。")
      End If

      For Each tmpRow As DataRow In tmpDt.Rows
        SqlServer.Execute(SqlUpdPrintFlg(tmpRow, prmWhereList))
      Next
    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try

  End Sub

  Private Function AccessPrint(prmPreview As Integer, prmTableName As String, prmReportName As String, tmpDt As DataTable) As Boolean
    Dim rtn As Boolean = True

    Try
      If (tmpDt.Rows.Count = 0) Then
        Throw New Exception
      End If

      'ワークテーブル作成
      UpdateReportNohinSet(tmpDt, prmTableName)

      '印刷処理
      AccessRun(prmPreview, prmReportName, True)

    Catch ex As Exception
      rtn = False
    End Try
    Return rtn
  End Function

  ''' <summary>
  ''' 量目表（セット）ワークテーブル削除と新規作成
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function UpdateReportNohinSet(prmDt As DataTable, prmTableName As String) As Boolean

    Dim tmpDb As New ClsReport(ClsCommonGlobalData.REPORT_FILENAME)
    Dim dt As DateTime = DateTime.Parse(ComGetProcTime())
    Dim UpdEndFlg As Boolean = False
    Dim tmpDt As New DataTable

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        .Execute("DELETE FROM " & prmTableName)

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("量目表（セット）ワークテーブルの削除に失敗しました")

      End Try

      Try
        Dim sql As String
        ' トランザクション開始
        .TrnStart()

        ' データテーブルから追加SQL文を作成
        For Each row As DataRow In prmDt.Rows
          sql = SqlInsNohin(prmTableName, row, dt)
          If String.IsNullOrWhiteSpace(sql) = False Then
            .Execute(sql)
          End If
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("量目表（セット）ワークテーブルの書き込みに失敗しました")
      End Try

      .Dispose()

    End With


    Return True

  End Function



  Private Overloads Function SqlGetPrintData(Optional ByRef prmWhereList As Dictionary(Of String, String) = Nothing) As String
    Dim sql As String = String.Empty
    Dim ReportType As String = ReadSettingIniFile("REPORT_TYPE", "VALUE")

    sql &= "SELECT	trn_jisseki.NohinDay "
    sql &= "	,	ISNULL(trn_jisseki.DenNO2,trn_jisseki.DenNO) DenNo "
    sql &= "	,	ISNULL(trn_jisseki.GyoNo2,trn_jisseki.GyoNo2)GyoNo "
    sql &= "	,	trn_jisseki.TokuiCD "
    sql &= "	,	trn_jisseki.TokuiNm "
    sql &= "	,	trn_jisseki.ShohinCD "
    sql &= "	,	trn_jisseki.ShohinNM "
    sql &= "	,	trn_jisseki.Iro IRISU "
    sql &= "	,	trn_jisseki.Suryo "
    sql &= "	,	trn_jisseki.Tanka "
    sql &= "	,	trn_jisseki.UriageKin "

    'If ReportType = REPORT_TYPE_SHUKKA Then
    '  sql &= "	,	ISNULL(MST_TOKUISAKI_SHOHIN.Baika,ISNULL(TOKUISAKI0.Baika,0)) UriTanka "
    'Else
    '  sql &= "	,	ISNULL(MST_TOKUISAKI_SHOHIN.Tanka,ISNULL(TOKUISAKI0.Tanka,0)) UriTanka "
    'End If

    'If ReportType = REPORT_TYPE_SHUKKA Then
    '  sql &= "	,	Floor(iif(trn_jisseki.Suryo = '',trn_jisseki.irisu,trn_jisseki.Suryo) "
    '  sql &= "	    * iif(trn_jisseki.Suryo = '',ISNULL(MST_TOKUISAKI_SHOHIN.Baika,ISNULL(TOKUISAKI0.Baika,0)),ISNULL(MST_TOKUISAKI_SHOHIN.Baika,ISNULL(TOKUISAKI0.Baika,0)) /100)) UriageKin "
    'Else
    '  sql &= "	,	Floor(iif(trn_jisseki.Suryo = '',trn_jisseki.irisu,trn_jisseki.Suryo) "
    '  sql &= "	    * iif(trn_jisseki.Suryo = '',ISNULL(MST_TOKUISAKI_SHOHIN.Tanka,ISNULL(TOKUISAKI0.Tanka,0)),ISNULL(MST_TOKUISAKI_SHOHIN.Tanka,ISNULL(TOKUISAKI0.Tanka,0)) /100)) UriageKin "
    'End If
    sql &= "	,	'8%' Zeiritsu "
    sql &= "	,	trn_jisseki.Biko "
    sql &= "	,	trn_jisseki.TyokuCD "
    sql &= "	,	trn_jisseki.TyokuNM "
    sql &= "FROM trn_jisseki "
    sql &= "LEFT JOIN MST_TOKUISAKI_SHOHIN "
    sql &= "ON MST_TOKUISAKI_SHOHIN.TokuiCD = trn_jisseki.TokuiCD "
    sql &= "AND MST_TOKUISAKI_SHOHIN.ShohinCD  = trn_jisseki.ShohinCD "
    sql &= "LEFT JOIN MST_TOKUISAKI_SHOHIN TOKUISAKI0 "
    sql &= "ON TOKUISAKI0.TokuiCD = 0 "
    sql &= "AND TOKUISAKI0.ShohinCD  =  trn_jisseki.ShohinCD "
    sql &= "WHERE 1=1 "
    For Each strValue As KeyValuePair(Of String, String) In prmWhereList
      sql &= "AND " & strValue.Key & " '" & strValue.Value & "'"
    Next
    sql &= "ORDER BY trn_jisseki.DenNO,trn_jisseki.GyoNo "

    Return sql
  End Function

  Private Overloads Function SqlGetPrintData(Optional ByRef prmWhereList As Dictionary(Of String, List(Of String)) = Nothing) As String
    Dim sql As String = String.Empty
    Dim ReportType As String = ReadSettingIniFile("REPORT_TYPE", "VALUE")

    sql &= "SELECT	trn_jisseki.NohinDay "
    sql &= "	,	trn_jisseki.DenNO2 DenNo "
    sql &= "	,	trn_jisseki.GyoNo2 GyoNo "
    sql &= "	,	trn_jisseki.TokuiCD "
    sql &= "	,	trn_jisseki.TokuiNm "
    sql &= "	,	trn_jisseki.ShohinCD "
    sql &= "	,	trn_jisseki.ShohinNM "
    sql &= "	,	trn_jisseki.Iro IRISU "
    sql &= "	,	trn_jisseki.Suryo "
    sql &= "	,	trn_jisseki.Tanka "
    sql &= "	,	trn_jisseki.UriageKin "
    sql &= "	,	'8%' Zeiritsu "
    sql &= "	,	trn_jisseki.Biko "
    sql &= "	,	trn_jisseki.TyokuCD "
    sql &= "	,	trn_jisseki.TyokuNM "
    sql &= "FROM trn_jisseki "
    sql &= "LEFT JOIN MST_TOKUISAKI_SHOHIN "
    sql &= "ON MST_TOKUISAKI_SHOHIN.TokuiCD = trn_jisseki.TokuiCD "
    sql &= "AND MST_TOKUISAKI_SHOHIN.ShohinCD  = trn_jisseki.ShohinCD "
    sql &= "LEFT JOIN MST_TOKUISAKI_SHOHIN TOKUISAKI0 "
    sql &= "ON TOKUISAKI0.TokuiCD = 0 "
    sql &= "AND TOKUISAKI0.ShohinCD  =  trn_jisseki.ShohinCD "
    sql &= "WHERE 1=1 "
    If prmWhereList IsNot Nothing Then
      For Each tmpValue As KeyValuePair(Of String, List(Of String)) In prmWhereList

        Dim key As String = tmpValue.Key
        Dim vals As List(Of String) = tmpValue.Value

        If vals.Count = 1 Then
          sql &= $" AND {key} '{vals(0)}'"
        Else
          Dim inList = String.Join(",", vals.Select(Function(v) $"'{v}'"))
          sql &= $" AND {key} IN ({inList})"
        End If

      Next
    End If

    sql &= "ORDER BY trn_jisseki.DenNO,trn_jisseki.GyoNo "

    Return sql
  End Function

  ''' <summary>
  ''' 量目表テーブル追加SQL文作成
  ''' </summary>
  ''' <param name="tblName">テーブル名</param>
  ''' <param name="tmpRow">設定値</param>
  ''' <param name="dt">更新日付</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsNohin(tblName As String,
                                    tmpRow As DataRow,
                                    dt As DateTime) As String

    Dim sql As String = String.Empty


    sql &= " INSERT INTO " & tblName
    sql &= "                   ( NOUHIN_DAY "                      '01:                       
    sql &= "                   , DEN_NO "                   '02:
    sql &= "                   , GYO_NO "                      '03:
    sql &= "                   , TOKUI_CD "                      '04:  
    sql &= "                   , TOKUI_NM "                      '05:
    sql &= "                   , SHOHIN_CD "                    '06:
    sql &= "                   , SHOHIN_NM "                      '07:
    sql &= "                   , IRISU "                      '08:
    sql &= "                   , SURYO "                      '09:
    sql &= "                   , TANKA "                      '10:
    sql &= "                   , URIAGE_KIN "                       '11:
    sql &= "                   , ZEIRITSU "                       '12:
    sql &= "                   , BIKO "                       '13:
    sql &= "                   , HASSO_CD "                       '14:
    sql &= "                   , HASSO_NM "                       '15:
    sql &= "                   , KDATE "                       '16:
    sql &= ") VALUES("

    '納品日
    If String.IsNullOrWhiteSpace(tmpRow("NohinDay").ToString) Then
      sql &= "Null,"                                          '01:
    Else
      sql &= "'" & DateFormatChange(typDateFormat.FORMAT_DATE, tmpRow("NohinDay").ToString) & "'" & ","    '01:
    End If

    '伝票No
    If String.IsNullOrWhiteSpace(tmpRow("DenNO").ToString) Then
      sql &= "0,"                                          '02:
    Else
      sql &= "'" & tmpRow("DenNO").ToString & "'" & ","                   '02:
    End If

    '行No
    If String.IsNullOrWhiteSpace(tmpRow("GyoNo").ToString) Then
      sql &= "NULL,"                                          '03:
    Else
      sql &= "'" & tmpRow("GyoNo").ToString & "'" & ","       '03:
    End If

    '得意先コード
    If String.IsNullOrWhiteSpace(tmpRow("TokuiCD").ToString) Then
      sql &= "0,"                                          '04:
    Else
      sql &= "'" & tmpRow("TokuiCD").ToString & "'" & ","                   '04:
    End If

    '得意先名
    If String.IsNullOrWhiteSpace(tmpRow("TokuiNm").ToString) Then
      sql &= "NULL,"                                          '05:
    Else
      sql &= "'" & tmpRow("TokuiNm").ToString & "'" & ","     '05:
    End If

    '商品コード
    If String.IsNullOrWhiteSpace(tmpRow("ShohinCD").ToString) Then
      sql &= "0,"                                          '06:
    Else
      sql &= "'" & tmpRow("ShohinCD").ToString & "'" & ","                   '06:
    End If

    '商品名
    If String.IsNullOrWhiteSpace(tmpRow("ShohinNM").ToString) Then
      sql &= "NULL,"                                          '07:
    Else
      sql &= "'" & tmpRow("ShohinNM").ToString & "'" & ","                   '07:
    End If

    '入り数
    If String.IsNullOrWhiteSpace(tmpRow("Irisu").ToString) Then
      sql &= "0,"                                          '08:
    Else
      sql &= "'" & tmpRow("Irisu").ToString & "'" & ","       '08:
    End If

    '数量
    If String.IsNullOrWhiteSpace(tmpRow("Suryo").ToString) Then
      sql &= "0,"                                          '09:
    Else
      sql &= "'" & tmpRow("Suryo").ToString & "'" & ","       '09:
    End If

    '単価
    If String.IsNullOrWhiteSpace(tmpRow("Tanka").ToString) Then
      sql &= "0,"                                          '10:
    Else
      sql &= "'" & tmpRow("Tanka").ToString & "'" & ","                   '10:
    End If

    '売上金額
    If String.IsNullOrWhiteSpace(tmpRow("UriageKin").ToString) Then
      sql &= "0,"                                          '11:
    Else
      sql &= "'" & tmpRow("UriageKin").ToString & "'" & ","                   '11:
    End If

    '税率
    'TODO 本来なら税率を商品マスタから取得して設定する。
    sql &= "'" & tmpRow("Zeiritsu").ToString & "'" & ","                  '12:                        '12:


    '備考
    If String.IsNullOrWhiteSpace(tmpRow("Biko").ToString) Then
      sql &= "NULL,"                                          '13:
    Else
      sql &= "'" & tmpRow("Biko").ToString & "'" & ","                   '13:
    End If

    '直送先コード
    If String.IsNullOrWhiteSpace(tmpRow("TyokuCd").ToString) Then
      sql &= "0,"                                          '14:
    Else
      sql &= "'" & tmpRow("TyokuCd").ToString & "'" & ","                   '14:
    End If

    '直送先名
    If String.IsNullOrWhiteSpace(tmpRow("TyokuNM").ToString) Then
      sql &= "NULL,"                                          '15:
    Else
      sql &= "'" & tmpRow("TyokuNM").ToString & "'" & ","                   '15:
    End If
    sql &= "'" & dt.ToString & "')"       '16:

    Console.WriteLine(sql)

    Return sql

  End Function

  Private Overloads Function SqlUpdPrintFlg(prmDb As DataRow, Optional ByRef prmWhereList As Dictionary(Of String, String) = Nothing) As String
    Dim sql As String = String.Empty

    sql &= " Update TRN_JISSEKI "
    sql &= " SET NohinPRTFLG = 1"
    sql &= " WHERE 1 = 1 "
    If Not prmWhereList Is Nothing _
      AndAlso prmWhereList.Count > 0 Then
      For Each strValue As KeyValuePair(Of String, String) In prmWhereList
        sql &= "AND " & strValue.Key & " '" & strValue.Value & "'"
      Next
    Else
      sql &= " AND DenNo = '" & prmDb.Item("DenNO").ToString & "'"
      sql &= " AND GyoNo = '" & prmDb.Item("GyoNO").ToString & "'"

    End If

    Return sql
  End Function

  Private Overloads Function SqlUpdPrintFlg(prmDb As DataRow, Optional ByRef prmWhereList As Dictionary(Of String, List(Of String)) = Nothing) As String
    Dim sql As String = String.Empty

    sql &= " Update TRN_JISSEKI "
    sql &= " SET NohinPRTFLG = 1"
    sql &= " WHERE 1 = 1 "
    If prmWhereList IsNot Nothing Then
      For Each tmpValue As KeyValuePair(Of String, List(Of String)) In prmWhereList

        Dim key As String = tmpValue.Key
        Dim vals As List(Of String) = tmpValue.Value

        If vals.Count = 1 Then
          sql &= $" AND {key} '{vals(0)}'"
        Else
          Dim inList = String.Join(",", vals.Select(Function(v) $"'{v}'"))
          sql &= $" AND {key} IN ({inList})"
        End If

      Next
    End If
    Return sql
  End Function
End Class
