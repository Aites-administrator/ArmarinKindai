Imports System.Windows.Forms
Imports Common
Imports Common.ClsFunction
Imports MainMenu.Form_Top
Imports MainMenu.Form_MasterMente
Imports Microsoft.VisualBasic.FileIO

Module Module_CustomerMasterDownload
  Private Const CUSTOMER_COLUMN_ID As Integer = 3
  Private ReadOnly tmpDb As New ClsSqlServer

  Dim tmpDt As New DataTable


  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property

  ReadOnly FtpId As String = ReadSettingIniFile("FTP_ID", "VALUE")
  ReadOnly FtpPw As String = ReadSettingIniFile("FTP_PW", "VALUE")
  ReadOnly FtpDownloadPath As String = ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE")
  ReadOnly FtpBackupPath As String = ReadSettingIniFile("FTP_BACKUP_PATH", "VALUE")
  ReadOnly FtpDelete As String = ReadSettingIniFile("FTP_DELETE", "VALUE")

  ReadOnly CutFileNameDigits As String = ReadSettingIniFile("CUT_FILENAME_DIGITS", "VALUE")

  Dim UnitNumberString As String
  Dim IpAddressString As String
  Dim UnitNumberArray() As String
  Dim IpAddressArray() As String
  Dim ErrorJudFlg As Boolean = False

  Sub Main(ScaleNumber() As String)
    Dim ClsSetMessage As New SetMessage("")

    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("***    *****     *****     *****    *****     ****** ********    ******     ***")
    Console.WriteLine("*** ********** ******* *** ***** ********** ******* * ******* *** ******* *****")
    Console.WriteLine("***    ******* *******     *****    ******* ****** *** ******    ******** *****")
    Console.WriteLine("*** ********** ******* ************ ******* ******     ****** *** ******* *****")
    Console.WriteLine("*** ********** ******* *********    ******* ***** ***** ***** **** ****** *****")
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("得意先マスタ受信処理開始")
    Console.WriteLine("******************************")

    ClsSetMessage = New SetMessage("得意先マスタ受信処理開始")
    Dim Para_ScaleNumber As String = String.Empty
    If ScaleNumber.Length > 0 Then
      For i As Integer = 0 To ScaleNumber.Length - 1
        If i = 0 Then
          Para_ScaleNumber = ScaleNumber(i)
        Else
          Para_ScaleNumber = Para_ScaleNumber + "," + ScaleNumber(i)
        End If
      Next
      Console.WriteLine("対象号機番号 ：" & Para_ScaleNumber)
      Console.WriteLine("******************************")
    End If


    '「計量器マスタ」取得
    SelectScaleMaster(Para_ScaleNumber)
    '引数定義
    Dim dtNow As DateTime = DateTime.Now
    Dim DownloadPath As String
    Dim BackupPath As String
    'Dim UpLoadFile As String
    Dim URL As String

    '計量器毎にループ（実績）
    For j As Integer = 0 To UnitNumberArray.Length - 1
      Console.WriteLine(UnitNumberArray(j) & "号機の得意先マスタ受信処理 START")
      Console.WriteLine("******************************")
      DownloadPath = FtpDownloadPath & "/" & UnitNumberArray(j) & "/" & "01ADDR" & ".CSV"
      BackupPath = FtpBackupPath & "/" & UnitNumberArray(j) & "/" & "01ADDR" & "_" & dtNow.ToString("yyMMddHHmmss") & ".CSV"
      URL = "ftp://localhost" & "/" & UnitNumberArray(j) & "/" & "01ADDR" & ".CSV"
      DownloadFtp(DownloadPath, BackupPath, URL, UnitNumberArray(j))
      System.Threading.Thread.Sleep(2000)
      'ログ登録　＆ 削除ファイル送信
      If ErrorJudFlg Then
        InsertTRNLOG(UnitNumberArray(j), "NG", "", "得意先マスタ受信失敗")
        Console.WriteLine("得意先マスタ受信処理失敗")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
        'ClsSetMessage.SetMessage("実績受信処理失敗")
      Else
        InsertTRNLOG(UnitNumberArray(j), "OK", "", "得意先マスタ受信成功")
        Console.WriteLine("得意先マスタ受信処理成功")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
        System.Threading.Thread.Sleep(2000)
        'ClsSetMessage.SetMessage("実績受信処理成功")
      End If
      Console.WriteLine(UnitNumberArray(j) & "号機の得意先マスタ受信処理 END")
      Console.WriteLine("******************************")
    Next
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("***    *****     *****     ******    ***** *** *****    ***********************")
    Console.WriteLine("*** ********** ******* *** ****** ********  ** ***** *** **********************")
    Console.WriteLine("***    ******* *******     ******   ****** * * ***** *** **********************")
    Console.WriteLine("*** ********** ******* ********** ******** **  ***** *** **********************")
    Console.WriteLine("*** ********** ******* **********    ***** *** *****    ***********************")
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("得意先マスタ受信処理終了")
    Console.WriteLine("******************************")
    Console.WriteLine("このウィンドウを閉じるには、任意のキーを押してください...")
    'ClsSetMessage.SetMessage("実績受信処理終了")

    'Console.ReadKey()
  End Sub
  Private Sub DownloadFtp(DownloadPath As String, BackupPath As String, URL As String, UnitNumber As String)
    Dim FILE_NAME As String = Strings.Right(URL, CutFileNameDigits)
    Dim tmpCreateDate As String = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
    Try
      'ダウンロードするファイルのURI
      Dim URI As New Uri(URL)
      'FtpWebRequestの作成
      Dim ftpReq As System.Net.FtpWebRequest =
                CType(System.Net.WebRequest.Create(URI), System.Net.FtpWebRequest)
      'ログインユーザー名とパスワードを設定
      ftpReq.Credentials = New System.Net.NetworkCredential(FtpId, FtpPw)
      'MethodにWebRequestMethods.Ftp.DownloadFile("RETR")を設定
      ftpReq.Method = System.Net.WebRequestMethods.Ftp.DownloadFile
      '要求の完了後に接続を閉じる
      ftpReq.KeepAlive = False
      'ASCIIモードで転送する
      ftpReq.UseBinary = False
      'PASSIVEモードを無効にする
      ftpReq.UsePassive = False
      Dim ftpRes As System.Net.FtpWebResponse =
                CType(ftpReq.GetResponse(), System.Net.FtpWebResponse)
      'ファイルをダウンロードするためのStreamを取得
      Dim resStrm As System.IO.Stream = ftpRes.GetResponseStream()

      InsertTRNLOG(UnitNumber, "", FILE_NAME, "FTPログイン成功")
      Console.WriteLine("FTPログイン成功")
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
      'ダウンロードしたファイルを書き込むためのFileStreamを作成
      Dim fs As New System.IO.FileStream(
                DownloadPath, System.IO.FileMode.Create, System.IO.FileAccess.Write)
      'ダウンロードしたデータを書き込む
      Dim buffer(1023) As Byte
      While True
        Dim readSize As Integer = resStrm.Read(buffer, 0, buffer.Length)
        If readSize = 0 Then
          Exit While
        End If
        fs.Write(buffer, 0, readSize)
      End While
      fs.Close()
      resStrm.Close()

      ftpRes.Close()
      Console.WriteLine("受信完了")
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
      InsertTRNLOG(UnitNumber, "", FILE_NAME, "FTP処理終了")

      'ダウンロード実績ファイルコピー
      MoveToBackUpLoadFile(DownloadPath, BackupPath)
      Console.WriteLine(BackupPath & "に得意先マスタCSV作成処理完了")
      Console.WriteLine("******************************")
      ''実績CSVを計量テーブルに登録
      ''InsertResultTable(DownloadPath, UnitNumber, tmpCreateDate)
      ''Console.WriteLine("計量テーブルにデータ更新")
      ''Console.WriteLine("******************************")
      ''得意先マスタCSVを実績テーブルに登録
      InsertItemTable(DownloadPath, UnitNumber, tmpCreateDate)
      Console.WriteLine("得意先マスタテーブルにデータ更新")
      Console.WriteLine("******************************")
      System.IO.File.Delete(DownloadPath)

      ' TODO 印刷処理追加予定


      InsertTRNLOG(UnitNumber, "", FILE_NAME, ftpRes.StatusCode & " " & ftpRes.StatusDescription)
      ErrorJudFlg = False
    Catch ex As Exception
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ex.Message)
      ErrorJudFlg = True
      Console.WriteLine(ex.Message)
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
    End Try
  End Sub

  Private Function ControlCodeEscape(prmStr As String) As String
    Dim tmpStr As String = prmStr
    Dim ControlCodeList As New List(Of String)

    ControlCodeList.Add("<NS/>")
    ControlCodeList.Add("<NL/>")
    ControlCodeList.Add("<NLH/>")
    ControlCodeList.Add("<NT/>")
    ControlCodeList.Add("<SL/>")
    ControlCodeList.Add("<SLH/>")
    ControlCodeList.Add("<SS/>")
    ControlCodeList.Add("<SSH/>")
    ControlCodeList.Add("<ST/>")
    ControlCodeList.Add("<B/>")
    ControlCodeList.Add("<R/>")
    ControlCodeList.Add("<U/>")
    ControlCodeList.Add("<F/>")
    ControlCodeList.Add("<ISD CD = ""15"" />")
    ControlCodeList.Add("<ISD CD = ""16"" />")

    For Each ControlCode In ControlCodeList
      tmpStr = tmpStr.Replace(ControlCode, "")
    Next

    Return tmpStr
  End Function


  Private Sub DelUploadFtp(UpLoadFile As String, URL As String, UnitNumber As String)
    Dim FILE_NAME As String = Strings.Right(URL, CutFileNameDigits)
    Try
      'アップロード先のURI
      Dim URI As New Uri(URL)
      'FtpWebRequestの作成
      Dim ftpReq As System.Net.FtpWebRequest = CType(System.Net.WebRequest.Create(URI), System.Net.FtpWebRequest)
      'ログインユーザー名とパスワードを設定
      ftpReq.Credentials = New System.Net.NetworkCredential(FtpId, FtpPw)
      'MethodにWebRequestMethods.Ftp.UploadFile("STOR")を設定
      ftpReq.Method = System.Net.WebRequestMethods.Ftp.UploadFile
      '要求の完了後に接続を閉じる
      ftpReq.KeepAlive = False
      'ASCIIモードで転送する
      ftpReq.UseBinary = False
      'PASVモードを無効にする
      ftpReq.UsePassive = False
      'ファイルをアップロードするためのStreamを取得
      Dim reqStrm As System.IO.Stream = ftpReq.GetRequestStream()
      'アップロードするファイルを開く
      Dim fs As New System.IO.FileStream(UpLoadFile, System.IO.FileMode.Open, System.IO.FileAccess.Read)
      'アップロードStreamに書き込む
      Dim buffer(1023) As Byte
      While True
        Dim readSize As Integer = fs.Read(buffer, 0, buffer.Length)
        If readSize = 0 Then
          Exit While
        End If
        reqStrm.Write(buffer, 0, readSize)
      End While
      fs.Close()
      reqStrm.Close()

      'FtpWebResponseを取得
      Dim ftpRes As System.Net.FtpWebResponse = CType(ftpReq.GetResponse(), System.Net.FtpWebResponse)
      '閉じる
      ftpRes.Close()
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ftpRes.StatusCode & " " & ftpRes.StatusDescription)
    Catch ex As Exception
      '正常処理終了フラグ変更★★★
      Call ComWriteErrLog("Module_Download",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ex.Message)
    End Try
  End Sub
  Public Sub SelectScaleMaster(Para_ScaleNumber As String)
    Dim sql As String = String.Empty

    sql = GetMstScaleSelectSql(Para_ScaleNumber)
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)
        If tmpDt.Rows.Count = 0 Then
          MessageBox.Show("計量器マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
          For i As Integer = 0 To tmpDt.Rows.Count - 1
            If i = 0 Then
              UnitNumberString = tmpDt.Rows(i)(0)
              IpAddressString = tmpDt.Rows(i)(1)
            Else
              UnitNumberString = UnitNumberString + "," + tmpDt.Rows(i)(0)
              IpAddressString = IpAddressString + "," + tmpDt.Rows(i)(1)
            End If
          Next
        End If
      End With
      UnitNumberArray = UnitNumberString.Split(","c)
      IpAddressArray = IpAddressString.Split(","c)
      InsertTRNLOG("", "", "", "計量器マスタ取得")
    Catch ex As Exception
      Call ComWriteErrLog("Module_Download",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
      InsertTRNLOG("", "", "", "計量器マスタ取得失敗")
    Finally
      tmpDt.Dispose()
    End Try
  End Sub
  Private Function GetMstScaleSelectSql(Para_ScaleNumber As String) As String


    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     UNIT_NUMBER,"
    sql &= "     IP_ADDRESS"
    sql &= " FROM"
    sql &= "     MST_SCALE"
    sql &= " WHERE"
    sql &= "     DELETE_FLG = 0"
    If Para_ScaleNumber.Length <> 0 Then
      sql &= "     AND UNIT_NUMBER IN(" & Para_ScaleNumber & ")"
    End If
    sql &= " ORDER BY  "
    sql &= "     UNIT_NUMBER"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstColumnSet(prmColumnId As Integer) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     COLUMN_NAME"
    sql &= ",    COLUMN_NO"
    sql &= " FROM"
    sql &= "     MST_COLUMN_SET"
    sql &= " WHERE COLUMN_ID = " & prmColumnId


    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstKeiryo(prmCreateDate As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     MACHINE_NUMBER"
    sql &= ",    JISSEKI_DATE"
    sql &= ",    JISSEKI_TIME"
    sql &= ",    BUMON_NUMBER"
    sql &= ",    YOBI_NUMBER"
    sql &= ",    TANKA_NUMBER"
    sql &= ",    SHOP_NUMBER"
    sql &= ",    JISSEKI_KUBUN"
    sql &= ",    KEIRYO_FLG"
    sql &= ",    JYURYO"
    sql &= ",    TANKA"
    sql &= ",    KINGAKU"
    sql &= ",    M_TOKKA_FLG"
    sql &= ",    M_TOKKA_DATA"
    sql &= ",    HANBAI_KINGAKU"
    sql &= ",    TEI_KINGAKU"
    sql &= ",    KOSUU"
    sql &= ",    TEIGAKU_KIGOU"
    sql &= ",    JISSEKI_KOSUU"
    sql &= ",    TEI_JYURYO"
    sql &= ",    SYOHIN_CODE"
    sql &= ",    HOUSOU_MODE"
    sql &= ",    SEISAN_MODE"
    sql &= ",    KOTAINO1"
    sql &= ",    KOTAINO2"
    sql &= ",    KOTAINO3"
    sql &= ",    LOT_NUMBER"
    sql &= ",    LOT_NUMBER2"
    sql &= ",    LOT_NUMBER3"
    sql &= ",    TRAY_NUMBER"
    sql &= " FROM"
    sql &= "     TRN_KEIRYO"
    sql &= " WHERE "
    sql &= "     CREATE_DATE = '" & prmCreateDate & "'"
    sql &= " ORDER BY  "
    sql &= "     JISSEKI_DATE,SHOP_NUMBER,YOBI_NUMBER,BUMON_NUMBER+YOBI_NUMBER"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstTokuisaki(prmTokuiCd As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     TokuiCD"
    sql &= ",    TokuiNM1"
    sql &= ",    TokuiNM2"
    sql &= ",    TokuiKana"
    sql &= ",    SenpoTanto"
    sql &= ",    TMstKBN"
    sql &= ",    SeikyuCD"
    sql &= ",    TJituKanri"
    sql &= ",    Add1"
    sql &= ",    Add2"
    sql &= ",    ZipCD"
    sql &= ",    TelNo"
    sql &= ",    FaxNo"
    sql &= ",    TokuiKBN1"
    sql &= ",    TokuiKBN2"
    sql &= ",    TokuiKBN3"
    sql &= ",    TekiyoUriNo"
    sql &= ",    TekiyoKakeritu"
    sql &= ",    TekiyoZeikan"
    sql &= ",    SyuTantoCD"
    sql &= ",    SeikyuSimebi"
    sql &= ",    ShohizeiHasu"
    sql &= ",    ShohizeiTuti"
    sql &= ",    Kaisyu1"
    sql &= ",    Kaisyu2"
    sql &= ",    KaisyuKyokai"
    sql &= ",    KaisyuYoteibi"
    sql &= ",    KaisyuHou"
    sql &= ",    YosinGendo"
    sql &= ",    KurikosiZan"
    sql &= ",    NohinYosi"
    sql &= ",    NohinShamei"
    sql &= ",    SeikyuYosi"
    sql &= ",    SeikyuShamei"
    sql &= ",    KantyoKBN"
    sql &= ",    Keisho"
    sql &= ",    ShatenCD"
    sql &= ",    TorihikiCD"
    sql &= ",    TokuiKubun"
    sql &= ",    DENP_KBN"
    sql &= " FROM"
    sql &= "     MST_TOKUISAKI"
    sql &= " WHERE "
    sql &= "     TokuiCD = '" & prmTokuiCd & "'"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstShohin(prmShohinCd As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     ShohinCD"
    sql &= ",     ShohinNM"
    sql &= ",     ShohinKana"
    sql &= ",     SysKBN"
    sql &= ",     SMstKBN"
    sql &= ",     ZaiKanri"
    sql &= ",     JituKanri"
    sql &= ",     Irisu"
    sql &= ",     Tani"
    sql &= ",     Iro"
    sql &= ",     Size"
    sql &= ",     ShohinKBN1"
    sql &= ",     ShohinKBN2"
    sql &= ",     ShohinKBN3"
    sql &= ",     ZeiKBN"
    sql &= ",     ZeikomiKBN"
    sql &= ",     SKetaT"
    sql &= ",     SKetaS"
    sql &= ",     HyojunKakaku"
    sql &= ",     ISNULL(Genka,0) Genka"
    sql &= ",     ISNULL(Baika1,0) Baika1"
    sql &= ",     ISNULL(Baika2,0) Baika2"
    sql &= ",     ISNULL(Baika3,0) Baika3"
    sql &= ",     ISNULL(Baika4,0) Baika4"
    sql &= ",     ISNULL(Baika5,0) Baika5"
    sql &= ",     SokoCD"
    sql &= ",     SSiireCD"
    sql &= ",     ISNULL(ZaiTanka,0) ZaiTanka"
    sql &= ",     ISNULL(SiireTanka,0) SiireTanka"
    sql &= ",     JANCD"
    sql &= ",     ShoKubun"
    sql &= ",     TDATE"
    sql &= ",     KDATE"
    sql &= " FROM"
    sql &= "     MST_SHOHIN "
    sql &= " WHERE "
    sql &= "     ShohinCD = '" & prmShohinCd & "'"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstTokuisakiShohin(prmTokuiCd As String, prmShohinCd As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     TokuiCD"
    sql &= ",    ShohinCD"
    sql &= ",    ShohinNM"
    sql &= ",    ShohinKana"
    sql &= ",    ISNULL(HyojunKakaku,0) HyojunKakaku"
    sql &= ",    SShohinCD"
    sql &= ",    JANCD"
    sql &= ",    FormatNo"
    sql &= ",    Suryo"
    sql &= ",    ISNULL(Tanka,0) Tanka"
    sql &= ",    Tani"
    sql &= ",    ISNULL(Baika,0) Baika"
    sql &= ",    TorokuFLG"
    sql &= ",    TokuiKubun"
    sql &= ",    ShoKubun"
    sql &= ",    Irisu"
    sql &= ",    JutyuSu"
    sql &= ",    ChokusoCD"
    sql &= ",    SMstKBN"
    sql &= ",    ISNULL(Genka,0) Genka"
    sql &= ",    SokoCD"
    sql &= ",    TDATE"
    sql &= " FROM"
    sql &= "     MST_TOKUISAKI_SHOHIN "
    sql &= " WHERE "
    sql &= "     TokuiCD = '" & prmTokuiCd & "'"
    sql &= " AND "
    sql &= "     ShohinCD = '" & prmShohinCd & "'"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function UpdDenNo(prmDenNo As String) As String
    Dim sql As String = String.Empty
    sql &= " UPDATE TBL_DENNO"
    sql &= " SET  DenNo = '" & prmDenNo & "'"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetDenpyoNoSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     DenNo "
    sql &= " FROM"
    sql &= "     TBL_DENNO "
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function


  Private Sub InsertItemTable(DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)
    Dim dt As New DataTable("MST_TOKUISAKI")
    Dim tmpBeforeNohinDay As String = "0"
    Dim tmpBeforeTokuiCd As String = "0"
    Dim HeaderRow As String() = Nothing
    Dim column As New DataColumn
    Dim tmpShohinDic As New List(Of Dictionary(Of String, String))

    Try

      tmpDb.TrnStart()

      SqlServer.GetResult(tmpDt, GetMstColumnSet(CUSTOMER_COLUMN_ID))

      ' データベースのカラム名を定義
      Dim columnNames As New List(Of String)
      For Each tmpRow In tmpDt.Rows
        columnNames.Add(tmpRow("COLUMN_NAME"))
      Next

      ' データテーブルにカラムを追加
      For Each columnName As String In columnNames
        dt.Columns.Add(New DataColumn(columnName, GetType(String)))
      Next


      'CSV読込
      Using textParser As New TextFieldParser(DownloadPath, System.Text.Encoding.GetEncoding("Shift-JIS"))
        textParser.TextFieldType = FieldType.Delimited
        textParser.SetDelimiters(",")

        ' ヘッダー部を取得
        If Not textParser.EndOfData Then
          HeaderRow = textParser.ReadFields()
        End If

        'CSVデータ内容取得処理
        While Not textParser.EndOfData
          Dim row As String() = textParser.ReadFields() 'CSVデータ
          Dim CsvList As New Dictionary(Of String, String)
          'CSVデータ保持 CsvList(ヘッダ、値)
          For i = 0 To HeaderRow.Count - 1
            CsvList(Strings.Mid(HeaderRow(i), InStr(HeaderRow(i), ":") + 1)) = row(i)
          Next

          'DataRow新規追加
          Dim tmpCreateTmpDr As DataRow = dt.NewRow()
          tmpShohinDic = New List(Of Dictionary(Of String, String))


          For i As Integer = 0 To row.Length - 1
            Dim tmpRow() As DataRow = tmpDt.Select("COLUMN_NO = '" & CsvList.Keys(i) & "'")

            For Each tmpDr In tmpRow
              If tmpCreateTmpDr(tmpDr("COLUMN_NAME")) Is DBNull.Value Then
                tmpCreateTmpDr(tmpDr("COLUMN_NAME")) = ControlCodeEscape(CsvList.Values(i))
              End If
            Next
          Next

          '単価更新 or 実績データ作成
          tmpShohinDic.Add(GetInsertDictionary(tmpCreateTmpDr))
          '' 実行時の時刻を追加
          'dr("create_date") = DateTime.Now.ToString("yyyy-MM-dd")
          'dr("update_date") = DateTime.Now.ToString("yyyy-MM-dd")
          dt.ImportRow(ComDic2Dt(tmpShohinDic).Rows(0))

        End While
      End Using

      tmpDb.Execute("delete from MST_TOKUISAKI ")

      ' データベースに挿入
      Dim sql As String = String.Empty
      For Each dr As DataRow In dt.Rows
        sql = GetInsertSql("MST_TOKUISAKI", columnNames, dr)
        With tmpDb
          .Execute("DELETE FROM MST_TOKUISAKI WHERE TokuiCD = '" & dr("TokuiCD").ToString.PadLeft(6, "0"c) & "'")
          If .Execute(sql) = 1 Then
            ' 更新成功
            InsertTRNLOG(UnitNumber, "", "", "マスタ登録完了")
          Else
            ' 削除失敗
            Throw New Exception("マスタ管理の登録処理に失敗しました。")
          End If
        End With
      Next

      tmpDb.TrnCommit()


    Catch ex As Exception
      Call ComWriteErrLog("Module_Download",
                            System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      InsertTRNLOG(UnitNumber, "", "", ex.Message)
      tmpDb.TrnRollBack()
      Throw New Exception(ex.Message)

    End Try


  End Sub

  Function GetInsertSql(prmTableName As String, columnNames As List(Of String), dr As DataRow) As String
    Dim sql As String = "INSERT INTO " & prmTableName & "("
    Dim values As String = "VALUES ("

    ' カラム名と値をセット
    For i As Integer = 0 To columnNames.Count - 1
      sql &= columnNames(i)
      values &= "'" & dr(i).ToString().Replace("'", "''") & "'"

      If i < columnNames.Count - 1 Then
        sql &= ", "
        values &= ", "
      End If
    Next

    sql &= ") " & values & ")"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub MoveToBackUpLoadFile(DownloadPath As String, BackupPath As String)
    System.IO.File.Copy(DownloadPath, BackupPath)
  End Sub
  Private Sub InsertTRNLOG(UNIT_NUMBER As String, RESULT As String, FILE_NAME As String, NOTE As String)
    Dim sql As String = String.Empty
    sql = GetInsertTRNLOGSql(UNIT_NUMBER, RESULT, FILE_NAME, NOTE)
    With tmpDb
      Try
        If .Execute(sql) = 1 Then
          ' 更新成功
          .TrnCommit()
        Else
          ' 削除失敗
          Throw New Exception("ログの登録処理に失敗しました。")
        End If
      Catch ex As Exception
        Call ComWriteErrLog("Module_Download",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        InsertTRNLOG("", "", "", ex.Message & " " & "(ログ登録失敗)")
      End Try
    End With
  End Sub
  Private Function GetInsertTRNLOGSql(UNIT_NUMBER As String, RESULT As String, FILE_NAME As String, NOTE As String)
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())
    Dim PROCESS_DATE As String = tmpdate.ToString("yyyy-MM-dd")
    Dim PROCESS_TIME As String = tmpdate.ToString("HH:mm:ss.ss")
    Dim ACHIEVEMENT_RECEIVE_TIME As String = tmpdate.ToString("yyyy-MM-dd HH:mm:ss.ss")

    sql &= " INSERT INTO TRN_LOG("
    sql &= "             PROCESS_DATE,"
    sql &= "             MACHINE_NO,"
    sql &= "             PROCESS_TIME,"
    sql &= "             FILE_NAME,"
    sql &= "             ACHIEVEMENT_RECEIVE_TIME,"
    sql &= "             ACHIEVEMENT_RESULT,"
    sql &= "             NOTE,"
    sql &= "             CREATE_DATE,"
    sql &= "             UPDATE_DATE"
    sql &= " )"
    sql &= " VALUES("
    sql &= "     '" & PROCESS_DATE & "',"
    sql &= "     '" & UNIT_NUMBER & "',"
    sql &= "     '" & PROCESS_TIME & "',"
    sql &= "     '" & FILE_NAME & "',"
    sql &= "     '" & ACHIEVEMENT_RECEIVE_TIME & "',"
    sql &= "     '" & RESULT & "',"
    sql &= "     '" & NOTE.Replace("'", "") & "',"
    sql &= "     '" & tmpdate & "',"
    sql &= "     '" & tmpdate & "'"
    sql &= " )"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetInsertDictionary(prmDataRow As DataRow) As Dictionary(Of String, String)

    'TRN_JISSEKIのカラム名で取得する必要がある。

    Dim rtnDic As New Dictionary(Of String, String)
    'Dim tmpTokuisakiCd As String = prmDataRow.Item("TokuiCD").ToString.PadLeft(6, "0"c) ''6桁前0埋め
    'Dim tmpShohinCd As String = prmDataRow.Item("ShohinCD").ToString.PadLeft(6, "0"c) ''6桁前0埋め
    'Dim tmpHiduke As String = prmDataRow.Item("NohinDay").ToString
    'tmpHiduke = tmpHiduke.Substring(0, 4) + "/" + tmpHiduke.Substring(4, 2) + "/" + tmpHiduke.Substring(6, 2)
    'Dim tmpJikan As String = prmDataRow.Item("NohinDay").ToString.PadLeft(4, "0"c)
    'tmpJikan = tmpJikan.Substring(0, 2) + ":" + tmpJikan.Substring(2, 2)

    'データ操作関連
    Dim tmpTDt As New DataTable
    Dim tmpSDt As New DataTable
    Dim tmpTSDt As New DataTable
    Dim tmpZeiDt As New DataTable
    'Dim tmpZeiDr As DataRow

    ''得意先取得
    'SqlServer.GetResult(tmpTDt, GetMstTokuisaki(tmpTokuisakiCd))
    'If (tmpTDt.Rows.Count = 0) Then
    '  Throw New Exception("得意先マスタを取得できませんでした。")
    'Else
    '  tmpTDr = tmpTDt.Rows(0)
    'End If

    ''商品取得
    'SqlServer.GetResult(tmpSDt, GetMstShohin(tmpShohinCd))
    'If (tmpSDt.Rows.Count = 0) Then
    '  Throw New Exception("商品マスタを取得できませんでした。")
    'Else
    '  tmpSDr = tmpSDt.Rows(0)
    'End If

    rtnDic("TokuiCD") = prmDataRow.Item("TokuiCD").ToString.PadLeft(6, "0"c)
    rtnDic("TokuiNM1") = prmDataRow.Item("TokuiNM1").ToString
    rtnDic("TokuiNM2") = prmDataRow.Item("TokuiNM2").ToString
    rtnDic("TokuiKana") = prmDataRow.Item("TokuiKana").ToString
    rtnDic("SenpoTanto") = prmDataRow.Item("SenpoTanto").ToString
    rtnDic("TMstKBN") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TMstKBN").ToString), "0", prmDataRow.Item("TMstKBN").ToString)
    rtnDic("SeikyuCD") = prmDataRow.Item("SeikyuCD").ToString
    rtnDic("TJituKanri") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TJituKanri").ToString), "0", prmDataRow.Item("TJituKanri").ToString)
    rtnDic("Add1") = prmDataRow.Item("Add1").ToString
    rtnDic("Add2") = prmDataRow.Item("Add2").ToString
    rtnDic("ZipCD") = prmDataRow.Item("ZipCD").ToString
    rtnDic("TelNo") = prmDataRow.Item("TelNo").ToString
    rtnDic("FaxNo") = prmDataRow.Item("FaxNo").ToString
    rtnDic("TokuiKBN1") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TokuiKBN1").ToString), "0", prmDataRow.Item("TokuiKBN1").ToString)
    rtnDic("TokuiKBN2") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TokuiKBN2").ToString), "0", prmDataRow.Item("TokuiKBN2").ToString)
    rtnDic("TokuiKBN3") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TokuiKBN3").ToString), "0", prmDataRow.Item("TokuiKBN3").ToString)
    rtnDic("TekiyoUriNo") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TekiyoUriNo").ToString), "0", prmDataRow.Item("TekiyoUriNo").ToString)
    rtnDic("TekiyoKakeritu") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TekiyoKakeritu").ToString), "0", prmDataRow.Item("TekiyoKakeritu").ToString)
    rtnDic("TekiyoZeikan") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TekiyoZeikan").ToString), "0", prmDataRow.Item("TekiyoZeikan").ToString)
    rtnDic("SyuTantoCD") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("SyuTantoCD").ToString), "0", prmDataRow.Item("SyuTantoCD").ToString)
    rtnDic("SeikyuSimebi") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("SeikyuSimebi").ToString), "0", prmDataRow.Item("SeikyuSimebi").ToString)
    rtnDic("ShohizeiHasu") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("ShohizeiHasu").ToString), "0", prmDataRow.Item("ShohizeiHasu").ToString)
    rtnDic("ShohizeiTuti") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("ShohizeiTuti").ToString), "0", prmDataRow.Item("ShohizeiTuti").ToString)
    rtnDic("Kaisyu1") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("Kaisyu1").ToString), "0", prmDataRow.Item("Kaisyu1").ToString)
    rtnDic("Kaisyu2") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("Kaisyu2").ToString), "0", prmDataRow.Item("Kaisyu2").ToString)
    rtnDic("KaisyuKyokai") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("KaisyuKyokai").ToString), "0", prmDataRow.Item("KaisyuKyokai").ToString)
    rtnDic("KaisyuYoteibi") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("KaisyuYoteibi").ToString), "0", prmDataRow.Item("KaisyuYoteibi").ToString)
    rtnDic("KaisyuHou") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("KaisyuHou").ToString), "0", prmDataRow.Item("KaisyuHou").ToString)
    rtnDic("YosinGendo") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("YosinGendo").ToString), "0", prmDataRow.Item("YosinGendo").ToString)
    rtnDic("KurikosiZan") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("KurikosiZan").ToString), "0", prmDataRow.Item("KurikosiZan").ToString)
    rtnDic("NohinYosi") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("NohinYosi").ToString), "0", prmDataRow.Item("NohinYosi").ToString)
    rtnDic("NohinShamei") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("NohinShamei").ToString), "0", prmDataRow.Item("NohinShamei").ToString)
    rtnDic("SeikyuYosi") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("SeikyuYosi").ToString), "0", prmDataRow.Item("SeikyuYosi").ToString)
    rtnDic("SeikyuShamei") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("SeikyuShamei").ToString), "0", prmDataRow.Item("SeikyuShamei").ToString)
    rtnDic("KantyoKBN") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("KantyoKBN").ToString), "0", prmDataRow.Item("KantyoKBN").ToString)
    rtnDic("Keisho") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("Keisho").ToString), "0", prmDataRow.Item("Keisho").ToString)
    rtnDic("ShatenCD") = prmDataRow.Item("ShatenCD").ToString
    rtnDic("TorihikiCD") = prmDataRow.Item("TorihikiCD").ToString
    rtnDic("TokuiKubun") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TokuiKubun").ToString), "0", prmDataRow.Item("TokuiKubun").ToString)
    rtnDic("DENP_KBN") = prmDataRow.Item("DENP_KBN").ToString
    rtnDic("TDATE") = prmDataRow.Item("TDATE").ToString
    rtnDic("KDATE") = prmDataRow.Item("KDATE").ToString

    Return rtnDic
  End Function


  Private Function ChkSaiban(prmDr As DataRow, ByRef prmBeforeNohinDay As String, ByRef prmTokuiCd As String, prmGyoNo As Integer) As Boolean
    Dim rtn As Boolean = False

    If (prmBeforeNohinDay <> prmDr("NohinDay").ToString _
                OrElse prmTokuiCd <> prmDr("TokuiCD").ToString _
                OrElse prmGyoNo = 6) Then
      rtn = True
    End If

    prmBeforeNohinDay = prmDr("NohinDay").ToString
    prmTokuiCd = prmDr("TokuiCD").ToString
    Return rtn
  End Function

  Private Sub GetDenpyoNo(ByRef prmDenpyoNo As String, ByRef prmGyoNo As Integer, prmChkSaiban As Boolean)
    Dim tmpDt As New DataTable
    Dim tmpBeforeDenpyoNo As String = prmDenpyoNo
    '伝票番号取得
    SqlServer.GetResult(tmpDt, GetDenpyoNoSql)
    prmDenpyoNo = tmpDt.Rows(0).Item("DenNo").ToString

    If prmChkSaiban Then
      prmDenpyoNo = (Integer.Parse(prmDenpyoNo) + 1).ToString.PadLeft(6, "0"c)
    End If

    If (tmpBeforeDenpyoNo <> prmDenpyoNo) Then
      prmGyoNo = 1
    Else
      prmGyoNo = prmGyoNo + 1
    End If
    If prmDenpyoNo > 999999 Then
      prmDenpyoNo = "000001"
      prmGyoNo = 1
    End If

    '伝票番号テーブル更新
    SqlServer.Execute(UpdDenNo(prmDenpyoNo))

  End Sub
End Module
