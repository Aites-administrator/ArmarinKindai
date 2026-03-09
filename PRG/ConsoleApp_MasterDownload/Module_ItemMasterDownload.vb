Imports System.Windows.Forms
Imports Common
Imports Common.ClsFunction
Imports MainMenu.Form_Top
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Module Module_ItemMasterDownload
  Private Const ITEM_COLUMN_ID As Integer = 2
  Private Const ITEM_TMP_COLUMN_ID As Integer = 4
  Private Const CUSTOMER_ITEM_COLUMN_ID As Integer = 5
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

  ReadOnly FileNameDigits As String = ReadSettingIniFile("FILENAME_DIGITS", "VALUE")
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
    Console.WriteLine("商品マスタ受信処理開始")
    Console.WriteLine("******************************")

    ClsSetMessage = New SetMessage("商品マスタ受信処理開始")
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
    Dim DownloadOrgPath As String
    Dim DownloadPath As String
    Dim BackupPath As String
    'Dim UpLoadFile As String
    Dim URL As String

    '計量器毎にループ（実績）
    For j As Integer = 0 To UnitNumberArray.Length - 1
      Console.WriteLine(UnitNumberArray(j) & "号機の商品マスタ受信処理 START")
      Console.WriteLine("******************************")
      DownloadOrgPath = FtpDownloadPath & "/" & UnitNumberArray(j) & "/" & "01ITEM_org" & ".CSV"
      DownloadPath = FtpDownloadPath & "/" & UnitNumberArray(j) & "/" & "01ITEM" & ".CSV"
      'DownloadPath = FtpDownloadPath & UnitNumberArray(j) & "/40TRAN" & FileNameDigits & UnitNumberArray(j) & ".CSV"
      BackupPath = FtpBackupPath & "/" & UnitNumberArray(j) & "/" & "01ITEM" & "_" & dtNow.ToString("yyMMddHHmmss") & ".CSV"
      'BackupPath = FtpBackupPath & UnitNumberArray(j) & "/40TRAN" & FileNameDigits & UnitNumberArray(j) & "_" & dtNow.ToString("yyMMddHHmmss") & ".CSV"
      URL = "ftp://localhost" & "/" & UnitNumberArray(j) & "/" & "01ITEM" & ".CSV"
      DownloadFtp(DownloadOrgPath, DownloadPath, BackupPath, URL, UnitNumberArray(j))
      System.Threading.Thread.Sleep(2000)
      'ログ登録　＆ 削除ファイル送信
      If ErrorJudFlg Then
        InsertTRNLOG(UnitNumberArray(j), "NG", "", "商品マスタ受信失敗")
        Console.WriteLine("商品マスタ受信処理失敗")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
        'ClsSetMessage.SetMessage("実績受信処理失敗")
      Else
        InsertTRNLOG(UnitNumberArray(j), "OK", "", "商品マスタ受信成功")
        Console.WriteLine("商品マスタ受信処理成功")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
        'UpLoadFile = FtpDelete & "/40TRAN" & FileNameDigits & UnitNumberArray(j) & ".DEL"
        'URL = "ftp://" & IpAddressArray(j) & "/40TRAN" & FileNameDigits & UnitNumberArray(j) & ".DEL"
        'DelUploadFtp(UpLoadFile, URL, UnitNumberArray(j))
        System.Threading.Thread.Sleep(2000)
        'ClsSetMessage.SetMessage("実績受信処理成功")
      End If
      Console.WriteLine(UnitNumberArray(j) & "号機の商品マスタ受信処理 END")
      Console.WriteLine("******************************")
    Next
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("***    *****     *****     ******    ***** *** *****    ***********************")
    Console.WriteLine("*** ********** ******* *** ****** ********  ** ***** *** **********************")
    Console.WriteLine("***    ******* *******     ******   ****** * * ***** *** **********************")
    Console.WriteLine("*** ********** ******* ********** ******** **  ***** *** **********************")
    Console.WriteLine("*** ********** ******* **********    ***** *** *****    ***********************")
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("商品マスタ受信処理終了")
    Console.WriteLine("******************************")
    Console.WriteLine("このウィンドウを閉じるには、任意のキーを押してください...")
    'ClsSetMessage.SetMessage("実績受信処理終了")

    'Console.ReadKey()
  End Sub
  Private Sub DownloadFtp(DownloadOrgPath As String, DownloadPath As String, BackupPath As String, URL As String, UnitNumber As String)
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
      Console.WriteLine(BackupPath & "に商品マスタCSV作成処理完了")
      Console.WriteLine("******************************")
      ''実績CSVを計量テーブルに登録
      ''InsertResultTable(DownloadPath, UnitNumber, tmpCreateDate)
      ''Console.WriteLine("計量テーブルにデータ更新")
      ''Console.WriteLine("******************************")
      ''商品マスタCSVを実績テーブルに登録
      InsertItemCsvTable(DownloadOrgPath, DownloadPath, UnitNumber, tmpCreateDate)
      Console.WriteLine("商品CSVテーブルにデータ更新")
      Console.WriteLine("******************************")
      System.IO.File.Delete(DownloadPath)
      ''商品マスタCSVを実績テーブルに登録
      InsertItemTable(DownloadPath, UnitNumber, tmpCreateDate)
      Console.WriteLine("商品テーブルにデータ更新")
      Console.WriteLine("******************************")
      System.IO.File.Delete(DownloadPath)
      ''得意先商品マスタCSVを実績テーブルに登録
      InsertCustomerItemTable(DownloadPath, UnitNumber, tmpCreateDate)
      Console.WriteLine("得意先商品テーブルにデータ更新")
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
    ControlCodeList.Add("<ISD CD=15/>")
    ControlCodeList.Add("<ISD CD=16/>")

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


  Private Sub InsertItemCsvTable(DownloadOrgPath As String, DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)
    Dim dt As New DataTable("TBL_ITEMCSV")
    Dim HeaderRow As String() = Nothing
    Dim cnt As Integer = 0

    Try
      tmpDb.TrnStart()
      SqlServer.GetResult(tmpDt, GetMstColumnSet(ITEM_TMP_COLUMN_ID))

      ' データベースのカラム名を定義
      Dim columnNames As New List(Of String)
      For Each tmpRow In tmpDt.Rows
        columnNames.Add(tmpRow("COLUMN_NAME"))
      Next

      ' データテーブルにカラムを追加
      For Each columnName As String In columnNames
        dt.Columns.Add(New DataColumn(columnName, GetType(String)))
      Next

      ' ファイル読み込みと加工
      ' 元のファイルを直接編集する処理

      File.Copy(DownloadPath, DownloadOrgPath, True) ' 第3引数で上書きを許可


      Dim lines As String() = File.ReadAllLines(DownloadOrgPath, System.Text.Encoding.GetEncoding("Shift-JIS"))
      Using sw As New StreamWriter(DownloadPath, False, System.Text.Encoding.GetEncoding("Shift-JIS"))
        For Each line As String In lines
          ' フィールドをクォーテーションで囲む処理
          Dim processedLine As String = ControlCodeEscape(line).Replace("""", "")
          sw.WriteLine(processedLine)
        Next
      End Using

      ' CSVファイルからデータを読み込み
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
          '実績データ
          Dim tmpJissekiDic As New List(Of Dictionary(Of String, String))

          For i As Integer = 0 To row.Length - 1
            Dim tmpRow() As DataRow = tmpDt.Select("COLUMN_NO = '" & CsvList.Keys(i) & "'")

            For Each tmpDr In tmpRow
              If tmpCreateTmpDr(tmpDr("COLUMN_NAME")) Is DBNull.Value Then
                tmpCreateTmpDr(tmpDr("COLUMN_NAME")) = ControlCodeEscape(CsvList.Values(i))
              End If
            Next
          Next

          ' 実行時の時刻を追加
          '実績データ作成
          tmpJissekiDic.Add(GetItemCsvInsertDictionary(tmpCreateTmpDr, prmCreateDate))

          Dim tmpDataRow As DataRow = ComDic2Dt(tmpJissekiDic).Rows(0)
          Console.WriteLine(cnt)
          cnt += 1
          If cnt = 0 Then
            cnt += 1

          End If
          dt.ImportRow(tmpDataRow)
        End While
      End Using

      tmpDb.Execute("delete from TBL_ITEMCSV ")
      ' データベースに挿入
      Dim sql As String = String.Empty
      For Each dr As DataRow In dt.Rows
        sql = GetInsertSql("TBL_ITEMCSV", columnNames, dr)
        With tmpDb
          If .Execute(sql) = 1 Then
            ' 更新成功
            InsertTRNLOG(UnitNumber, "", "", "商品CSV登録完了")
          Else
            ' 削除失敗
            Throw New Exception("商品CSVの登録処理に失敗しました。")
          End If
        End With
      Next
      tmpDb.TrnCommit()
    Catch ex As Exception
      Call ComWriteErrLog("Module_MasterDownload",
                       System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      InsertTRNLOG(UnitNumber, "", "", ex.Message)
      tmpDb.TrnRollBack()
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Private Sub InsertItemTable(DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)

    Dim dt As New DataTable("MST_SHOHIN")
    Dim tmpItemDt As New DataTable
    Dim tmpDenpyoNo As String = "0"
    Dim tmpGyoNo As Integer = 0
    Dim tmpBeforeNohinDay As String = "0"
    Dim tmpBeforeTokuiCd As String = "0"
    Dim HeaderRow As String() = Nothing
    Dim column As New DataColumn
    Dim sqlItem As String = GetMstGroupItem()

    Try

      tmpDb.TrnStart()
      SqlServer.GetResult(tmpItemDt, sqlItem)
      SqlServer.GetResult(tmpDt, GetMstColumnSet(ITEM_COLUMN_ID))


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
      'Forで計量データをループする
      For Each tmpDr In tmpItemDt.Rows
        'DataRow新規追加
        Dim tmpCreateTmpDr As DataRow = dt.NewRow()
        '実績データ
        Dim tmpJissekiDic As New List(Of Dictionary(Of String, String))

        '実績データ作成
        tmpJissekiDic.Add(GetInsertDictionary(tmpDr))

        '' 実行時の時刻を追加
        'dr("create_date") = DateTime.Now.ToString("yyyy-MM-dd")
        'dr("update_date") = DateTime.Now.ToString("yyyy-MM-dd")
        Dim tmpDataRow As DataRow = ComDic2Dt(tmpJissekiDic).Rows(0)
        dt.ImportRow(tmpDataRow)

      Next

      tmpDb.Execute("delete from MST_SHOHIN ")

      ' データベースに挿入
      Dim sql As String = String.Empty
      For Each dr As DataRow In dt.Rows
        sql = GetInsertSql("MST_SHOHIN", columnNames, dr)
        With tmpDb
          If .Execute(sql) = 1 Then
            ' 更新成功
            InsertTRNLOG(UnitNumber, "", "", "商品登録完了")
          Else
            ' 削除失敗
            Throw New Exception("商品の登録処理に失敗しました。")
          End If
        End With
      Next

      tmpDb.TrnCommit()

    Catch ex As Exception
      Call ComWriteErrLog("Module_MasterDownload",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      InsertTRNLOG(UnitNumber, "", "", ex.Message)
      tmpDb.TrnRollBack()
      Throw New Exception(ex.Message)

    End Try
  End Sub

  Private Sub InsertCustomerItemTable(DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)

    Dim dt As New DataTable("MST_TOKUISAKI_SHOHIN")
    Dim tmpItemDt As New DataTable
    Dim tmpDenpyoNo As String = "0"
    Dim tmpGyoNo As Integer = 0
    Dim tmpBeforeNohinDay As String = "0"
    Dim tmpBeforeTokuiCd As String = "0"
    Dim HeaderRow As String() = Nothing
    Dim column As New DataColumn
    Dim sqlItem As String = GetMstCustomerItem()

    Try

      tmpDb.TrnStart()
      SqlServer.GetResult(tmpItemDt, sqlItem)
      SqlServer.GetResult(tmpDt, GetMstColumnSet(CUSTOMER_ITEM_COLUMN_ID))


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
      'Forで計量データをループする
      For Each tmpDr In tmpItemDt.Rows
        'DataRow新規追加
        Dim tmpCreateTmpDr As DataRow = dt.NewRow()
        '実績データ
        Dim tmpJissekiDic As New List(Of Dictionary(Of String, String))

        '実績データ作成
        tmpJissekiDic.Add(GetMstCustomerItemInsertDictionary(tmpDr))

        Dim tmpDataRow As DataRow = ComDic2Dt(tmpJissekiDic).Rows(0)
        dt.ImportRow(tmpDataRow)

      Next

      ' データベースに挿入
      Dim sql As String = String.Empty
      For Each dr As DataRow In dt.Rows
        sql = GetInsertSql("MST_TOKUISAKI_SHOHIN", columnNames, dr)
        With tmpDb
          If .Execute(sql) = 1 Then
            ' 更新成功
            InsertTRNLOG(UnitNumber, "", "", "商品登録完了")
          Else
            ' 削除失敗
            Throw New Exception("商品の登録処理に失敗しました。")
          End If
        End With
      Next

      tmpDb.TrnCommit()

    Catch ex As Exception
      Call ComWriteErrLog("Module_MasterDownload",
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

  '集計用SQL
  Private Function GetMstGroupItem() As String
    Dim sql As String = String.Empty
    sql &= " SELECT "
    sql &= "    YOBI "
    sql &= "    , MAX(SHOHINNAME) as SHOHINNAME "
    sql &= "    , MAX(TEIGAKUKOSUU) as TEIGAKUKOSUU "
    sql &= "    , MAX(TEIKINGAKU) as TEIKINGAKU "
    sql &= "    , MAX([1]) as TANKA1 "
    sql &= "    , MAX([2]) as TANKA2 "
    sql &= "    , MAX([3]) as TANKA3 "
    sql &= "    , MAX([4]) as TANKA4 "
    sql &= "    , MAX([5]) as TANKA5 "
    sql &= " FROM"
    sql &= "     TBL_ITEMCSV PIVOT(  "
    sql &= "         MAX(TANKA) FOR TANKANO IN ([1], [2], [3], [4], [5]) "
    sql &= "     ) AS TableA "
    sql &= " GROUP BY "
    sql &= "     YOBI "
    sql &= " ORDER BY  "
    sql &= "     CAST(yobi as int)"
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  '集計用SQL
  Private Function GetMstCustomerItem() As String
    Dim sql As String = String.Empty
    Dim tmpTokuisaki As String = "000000"

    sql &= " SELECT "
    sql &= "    '" & tmpTokuisaki & "' 	AS	TokuiCD	"
    sql &= "    , MST_SHOHIN.ShohinCD 	AS	ShohinCD	"
    sql &= "    , MST_SHOHIN.ShohinNM 	AS	ShohinNM	"
    sql &= "    , MST_SHOHIN.ShohinKana AS	ShohinKana	"
    sql &= "    , MST_SHOHIN.HyojunKakaku AS	HyojunKakaku	"
    sql &= "    , MST_SHOHIN.ShohinCD 	AS	SShohinCD	"
    sql &= "    , MST_SHOHIN.JANCD 			AS	JANCD	"
    sql &= "    , '' 				AS	FormatNo	"
    sql &= "    , 0 				AS	Suryo	"
    sql &= "    , MST_SHOHIN.Baika1 		AS	Tanka	"
    sql &= "    , MST_SHOHIN.Tani 			AS	Tani	"
    sql &= "    , MST_SHOHIN.Baika1 		AS	Baika	"
    sql &= "    , 0 				AS	TorokuFLG	"
    sql &= "    , 0 				AS	TokuiKubun	"
    sql &= "    , 0 				AS	ShoKubun	"
    sql &= "    , MST_SHOHIN.Irisu 			AS	Irisu	"
    sql &= "    , 0 				AS	JutyuSu	"
    sql &= "    , '' 				AS	ChokusoCD	"
    sql &= "    , 0 				AS	SMstKBN	"
    sql &= "    , MST_SHOHIN.Genka 			AS	Genka	"
    sql &= "    , MST_SHOHIN.SokoCD 		AS	SokoCD	"
    sql &= "    , getdate() 				AS	TDATE	"
    sql &= "    , getdate() 				AS	KDATE	"
    sql &= " FROM"
    sql &= "     MST_SHOHIN "
    sql &= " LEFT JOIN ( "
    sql &= "      SELECT  * "
    sql &= "      FROM    MST_TOKUISAKI_SHOHIN "
    sql &= "      WHERE   TokuiCD = '" & tmpTokuisaki & "'"
    sql &= " ) MST_TOKUISAKI_SHOHIN "
    sql &= " ON MST_TOKUISAKI_SHOHIN.ShohinCD = MST_SHOHIN.ShohinCD "
    sql &= " WHERE MST_TOKUISAKI_SHOHIN.TokuiCD IS NULL "

    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function


  Private Function GetInsertDictionary(prmDataRow As DataRow) As Dictionary(Of String, String)

    'TRN_JISSEKIのカラム名で取得する必要がある。

    Dim rtnDic As New Dictionary(Of String, String)

    'データ操作関連
    Dim tmpTDt As New DataTable
    Dim tmpSDt As New DataTable
    Dim tmpTSDt As New DataTable
    Dim tmpZeiDt As New DataTable

    rtnDic("ShohinCD") = prmDataRow.Item("YOBI").ToString.PadLeft(6, "0"c)
    rtnDic("ShohinNM") = prmDataRow.Item("SHOHINNAME").ToString
    rtnDic("ShohinKana") = ""
    rtnDic("SysKBN") = ""
    rtnDic("SMstKBN") = ""
    rtnDic("ZaiKanri") = ""
    rtnDic("JituKanri") = ""
    rtnDic("Irisu") = prmDataRow.Item("TEIGAKUKOSUU").ToString
    rtnDic("Tani") = ""
    rtnDic("Size") = ""
    rtnDic("ShohinKBN1") = "0"
    rtnDic("ShohinKBN2") = "0"
    rtnDic("ShohinKBN3") = "0"
    rtnDic("ZeiKBN") = "0"
    rtnDic("ZeikomiKBN") = "0"
    rtnDic("SKetaT") = "0"
    rtnDic("SKetaS") = "0"
    rtnDic("HyojunKakaku") = prmDataRow.Item("TEIKINGAKU").ToString
    rtnDic("Genka") = "0"
    rtnDic("Baika1") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TANKA1").ToString), "0", prmDataRow.Item("TANKA1").ToString)
    rtnDic("Baika2") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TANKA2").ToString), "0", prmDataRow.Item("TANKA2").ToString)
    rtnDic("Baika3") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TANKA3").ToString), "0", prmDataRow.Item("TANKA3").ToString)
    rtnDic("Baika4") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TANKA4").ToString), "0", prmDataRow.Item("TANKA4").ToString)
    rtnDic("Baika5") = If(String.IsNullOrWhiteSpace(prmDataRow.Item("TANKA5").ToString), "0", prmDataRow.Item("TANKA5").ToString)
    rtnDic("SokoCD") = "0"
    rtnDic("SSiireCD") = ""
    rtnDic("ZaiTanka") = "0"
    rtnDic("SiireTanka") = "0"
    rtnDic("JANCD") = ""
    rtnDic("ShoKubun") = "0"

    Return rtnDic
  End Function

  Private Function GetItemCsvInsertDictionary(prmDataRow As DataRow, prmCreateDate As String) As Dictionary(Of String, String)

    'TRN_JISSEKIのカラム名で取得する必要がある。

    Dim rtnDic As New Dictionary(Of String, String)

    rtnDic("SEIGYOKBN") = prmDataRow.Item("SEIGYOKBN").ToString
    rtnDic("BUMON") = prmDataRow.Item("BUMON").ToString
    rtnDic("YOBI") = prmDataRow.Item("YOBI").ToString
    rtnDic("SHOHIN") = prmDataRow.Item("SHOHIN").ToString
    rtnDic("KAKODAYINJI") = prmDataRow.Item("KAKODAYINJI").ToString
    rtnDic("KAKOJIINJI") = prmDataRow.Item("KAKOJIINJI").ToString
    rtnDic("KAKOJISEL") = prmDataRow.Item("KAKOJISEL").ToString
    rtnDic("KAKOBI") = prmDataRow.Item("KAKOBI").ToString
    rtnDic("KAKOTIME") = prmDataRow.Item("KAKOTIME").ToString
    rtnDic("SHOHIDAYINJI") = prmDataRow.Item("SHOHIDAYINJI").ToString
    rtnDic("SHOHIJIINJI") = prmDataRow.Item("SHOHIJIINJI").ToString
    rtnDic("SHOHIKIKAN") = prmDataRow.Item("SHOHIKIKAN").ToString
    rtnDic("SHOHITIME") = prmDataRow.Item("SHOHITIME").ToString
    rtnDic("OMOTE") = prmDataRow.Item("OMOTE").ToString
    rtnDic("URA") = prmDataRow.Item("URA").ToString
    rtnDic("EYECATCH") = prmDataRow.Item("EYECATCH").ToString
    rtnDic("LABEL") = prmDataRow.Item("LABEL").ToString
    rtnDic("OMOTEFORMAT") = prmDataRow.Item("OMOTEFORMAT").ToString
    rtnDic("URAFORMAT") = prmDataRow.Item("URAFORMAT").ToString
    rtnDic("TENKANO") = prmDataRow.Item("TENKANO").ToString
    rtnDic("HOZONONDO") = prmDataRow.Item("HOZONONDO").ToString
    rtnDic("HOZONHOHO") = prmDataRow.Item("HOZONHOHO").ToString
    rtnDic("SANTINAME") = prmDataRow.Item("SANTINAME").ToString
    rtnDic("FREE1") = prmDataRow.Item("FREE1").ToString
    rtnDic("FREE2") = prmDataRow.Item("FREE2").ToString
    rtnDic("FREE3") = prmDataRow.Item("FREE3").ToString
    rtnDic("FREE4") = prmDataRow.Item("FREE4").ToString
    rtnDic("FREE5") = prmDataRow.Item("FREE5").ToString
    rtnDic("KAKOMOTOSHOP") = prmDataRow.Item("KAKOMOTOSHOP").ToString
    rtnDic("OMOTEBAR") = prmDataRow.Item("OMOTEBAR").ToString
    rtnDic("HAKKOMODE") = prmDataRow.Item("HAKKOMODE").ToString
    rtnDic("AUTOKENTI") = prmDataRow.Item("AUTOKENTI").ToString
    rtnDic("KINKENNO") = prmDataRow.Item("KINKENNO").ToString
    rtnDic("URAHAKKO") = prmDataRow.Item("URAHAKKO").ToString
    rtnDic("HOSOSOKUDO") = prmDataRow.Item("HOSOSOKUDO").ToString
    rtnDic("OMOTESHURUI") = prmDataRow.Item("OMOTESHURUI").ToString
    rtnDic("KOTAILOT") = prmDataRow.Item("KOTAILOT").ToString
    rtnDic("CALORIENO") = prmDataRow.Item("CALORIENO").ToString
    rtnDic("MORITAKASAAUTOKENTI") = prmDataRow.Item("MORITAKASAAUTOKENTI").ToString
    rtnDic("PRINTER") = prmDataRow.Item("PRINTER").ToString
    rtnDic("SHOHINNAME") = prmDataRow.Item("SHOHINNAME").ToString
    rtnDic("TANKANO") = prmDataRow.Item("TANKANO").ToString
    rtnDic("KEIRYOMODE") = prmDataRow.Item("KEIRYOMODE").ToString
    rtnDic("TANKA") = prmDataRow.Item("TANKA").ToString
    rtnDic("TEIKINGAKU") = prmDataRow.Item("TEIKINGAKU").ToString
    rtnDic("MTOKKAMODE") = prmDataRow.Item("MTOKKAMODE").ToString
    rtnDic("MTOKKAKINGAKU") = prmDataRow.Item("MTOKKAKINGAKU").ToString
    rtnDic("TEIGAKUKOSUU") = prmDataRow.Item("TEIGAKUKOSUU").ToString
    rtnDic("TEIGAKUKIGO") = prmDataRow.Item("TEIGAKUKIGO").ToString
    rtnDic("TEIJURYO") = prmDataRow.Item("TEIJURYO").ToString
    rtnDic("JOUGEN") = prmDataRow.Item("JOUGEN").ToString
    rtnDic("KAGEN") = prmDataRow.Item("KAGEN").ToString
    rtnDic("HUTAI") = prmDataRow.Item("HUTAI").ToString
    rtnDic("CODE") = prmDataRow.Item("CODE").ToString
    rtnDic("FLG") = prmDataRow.Item("FLG").ToString
    rtnDic("POSFLG") = prmDataRow.Item("POSFLG").ToString
    rtnDic("POSCODE") = prmDataRow.Item("POSCODE").ToString
    rtnDic("REJICODE") = prmDataRow.Item("REJICODE").ToString
    rtnDic("POPNO") = prmDataRow.Item("POPNO").ToString
    rtnDic("COMMENT") = prmDataRow.Item("COMMENT").ToString
    rtnDic("IMAGENO") = prmDataRow.Item("IMAGENO").ToString
    rtnDic("IMAGE2NO") = prmDataRow.Item("IMAGE2NO").ToString
    rtnDic("TORAYNO") = prmDataRow.Item("TORAYNO").ToString
    rtnDic("HOSOMODE") = prmDataRow.Item("HOSOMODE").ToString
    rtnDic("KYOKYUSOKUDO") = prmDataRow.Item("KYOKYUSOKUDO").ToString
    rtnDic("MORITAKASA") = prmDataRow.Item("MORITAKASA").ToString
    rtnDic("HARITUKEHOKO") = prmDataRow.Item("HARITUKEHOKO").ToString
    rtnDic("HARITUKEMODE") = prmDataRow.Item("HARITUKEMODE").ToString
    rtnDic("YOKIHOSO") = prmDataRow.Item("YOKIHOSO").ToString
    rtnDic("JUSTPRICE") = prmDataRow.Item("JUSTPRICE").ToString
    rtnDic("TORAYOSAE") = prmDataRow.Item("TORAYOSAE").ToString
    rtnDic("TANKAGYAKUENZAN") = prmDataRow.Item("TANKAGYAKUENZAN").ToString


    Return rtnDic
  End Function

  Private Function GetMstCustomerItemInsertDictionary(prmDataRow As DataRow) As Dictionary(Of String, String)

    'TRN_JISSEKIのカラム名で取得する必要がある。

    Dim rtnDic As New Dictionary(Of String, String)

    'データ操作関連
    Dim tmpTDt As New DataTable
    Dim tmpSDt As New DataTable
    Dim tmpTSDt As New DataTable
    Dim tmpZeiDt As New DataTable

    rtnDic("TokuiCD") = prmDataRow.Item("TokuiCD").ToString
    rtnDic("ShohinCD") = prmDataRow.Item("ShohinCD").ToString
    rtnDic("ShohinNM") = prmDataRow.Item("ShohinNM").ToString
    rtnDic("ShohinKana") = prmDataRow.Item("ShohinKana").ToString
    rtnDic("HyojunKakaku") = prmDataRow.Item("HyojunKakaku").ToString
    rtnDic("SShohinCD") = prmDataRow.Item("SShohinCD").ToString
    rtnDic("JANCD") = prmDataRow.Item("JANCD").ToString
    rtnDic("FormatNo") = prmDataRow.Item("FormatNo").ToString
    rtnDic("Suryo") = prmDataRow.Item("Suryo").ToString
    rtnDic("Tanka") = prmDataRow.Item("Tanka").ToString
    rtnDic("Tani") = prmDataRow.Item("Tani").ToString
    rtnDic("Baika") = prmDataRow.Item("Baika").ToString
    rtnDic("TorokuFLG") = prmDataRow.Item("TorokuFLG").ToString
    rtnDic("TokuiKubun") = prmDataRow.Item("TokuiKubun").ToString
    rtnDic("ShoKubun") = prmDataRow.Item("ShoKubun").ToString
    rtnDic("Irisu") = prmDataRow.Item("Irisu").ToString
    rtnDic("JutyuSu") = prmDataRow.Item("JutyuSu").ToString
    rtnDic("ChokusoCD") = prmDataRow.Item("ChokusoCD").ToString
    rtnDic("SMstKBN") = prmDataRow.Item("SMstKBN").ToString
    rtnDic("Genka") = prmDataRow.Item("Genka").ToString
    rtnDic("SokoCD") = prmDataRow.Item("SokoCD").ToString
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

  Function ParseQuotedCSV(line As String) As String()
    ' クォーテーションで囲まれたフィールドを解析
    Dim result As New List(Of String)()
    Dim currentField As String = ""
    Dim inQuotes As Boolean = False

    For Each c As Char In line
      If c = """"c Then
        inQuotes = Not inQuotes ' クォーテーションの開始・終了を判定
      ElseIf c = ","c AndAlso Not inQuotes Then
        result.Add(currentField)
        currentField = ""
      Else
        currentField &= c
      End If
    Next

    result.Add(currentField)
    Return result.ToArray()
  End Function
End Module
