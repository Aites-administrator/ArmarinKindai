Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Common
Imports Common.ClsFunction
Imports Microsoft.VisualBasic.FileIO
Imports MainMenu.Form_Top
Imports T.R.ZCommonClass.clsCodeLengthSetting


Module Module_Download
  Private Const KEIRYO_COLUMN_ID As Integer = 0
  Private Const JISSEKI_COLUMN_ID As Integer = 1
  Private Const GROUP_TYPE_OFF As Integer = 0
  Private Const GROUP_TYPE_ON As Integer = 1
  Private Const FILE_NAME_BASE As String = "40TRAN"
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
    Dim tmpFileName As String = FILE_NAME_BASE
    Try
      Dim ClsSetMessage As New SetMessage("")

      Console.WriteLine("*******************************************************************************")
      Console.WriteLine("***    *****     *****     *****    *****     ****** ********    ******     ***")
      Console.WriteLine("*** ********** ******* *** ***** ********** ******* * ******* *** ******* *****")
      Console.WriteLine("***    ******* *******     *****    ******* ****** *** ******    ******** *****")
      Console.WriteLine("*** ********** ******* ************ ******* ******     ****** *** ******* *****")
      Console.WriteLine("*** ********** ******* *********    ******* ***** ***** ***** **** ****** *****")
      Console.WriteLine("*******************************************************************************")
      Console.WriteLine("実績受信処理開始")
      Console.WriteLine("******************************")

      ClsSetMessage = New SetMessage("実績受信処理開始")
      Dim Para_ScaleNumber As String = String.Empty
      If ScaleNumber.Length > 0 Then
        For i As Integer = 0 To ScaleNumber.Length - 1
          If i = 0 Then
            Para_ScaleNumber = ScaleNumber(i)
          Else
            Para_ScaleNumber = Para_ScaleNumber + "','" + ScaleNumber(i)
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
        tmpFileName = FILE_NAME_BASE & FileNameDigits & Integer.Parse(UnitNumberArray(j)).ToString()

        Console.WriteLine(IpAddressArray(j) & "号機の実績受信処理 START")
        Console.WriteLine("******************************")
        DownloadPath = FtpDownloadPath & "/" & UnitNumberArray(j) & "/" & tmpFileName & ".CSV"
        BackupPath = FtpBackupPath & "/" & UnitNumberArray(j) & "/" & tmpFileName & "_" & dtNow.ToString("yyMMddHHmmss") & ".CSV"
        URL = "ftp://localhost" & "/" & UnitNumberArray(j) & "/" & tmpFileName & ".CSV"
        DownloadFtp(DownloadPath, BackupPath, URL, UnitNumberArray(j))
        System.Threading.Thread.Sleep(2000)
        'ログ登録　＆ 削除ファイル送信
        If ErrorJudFlg Then
          InsertTRNLOG(UnitNumberArray(j), "NG", "", "実績受信失敗")
          Console.WriteLine("実績受信処理失敗")
          Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
          Console.WriteLine("******************************")
          'ClsSetMessage.SetMessage("実績受信処理失敗")
        Else
          InsertTRNLOG(UnitNumberArray(j), "OK", "", "実績受信成功")
          Console.WriteLine("実績受信処理成功")
          Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
          Console.WriteLine("******************************")
          System.Threading.Thread.Sleep(2000)
          'ClsSetMessage.SetMessage("実績受信処理成功")
        End If
        Console.WriteLine(UnitNumberArray(j) & "号機の実績受信処理 END")
        Console.WriteLine("******************************")
      Next
      Console.WriteLine("*******************************************************************************")
      Console.WriteLine("***    *****     *****     ******    ***** *** *****    ***********************")
      Console.WriteLine("*** ********** ******* *** ****** ********  ** ***** *** **********************")
      Console.WriteLine("***    ******* *******     ******   ****** * * ***** *** **********************")
      Console.WriteLine("*** ********** ******* ********** ******** **  ***** *** **********************")
      Console.WriteLine("*** ********** ******* **********    ***** *** *****    ***********************")
      Console.WriteLine("*******************************************************************************")
      Console.WriteLine("実績受信処理終了")
      Console.WriteLine("******************************")
      Console.WriteLine("このウィンドウを閉じるには、任意のキーを押してください...")
      'ClsSetMessage.SetMessage("実績受信処理終了")

      'Console.ReadKey()
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Sub
  Private Sub DownloadFtp(DownloadPath As String, BackupPath As String, URL As String, UnitNumber As String)
    Dim FILE_NAME As String = Strings.Right(URL, CutFileNameDigits)
    Dim tmpCreateDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    Dim BkFolderFileName As String = GetAddBkFolder(DownloadPath)    'bkフォルダ取得
    Dim tmpRcvFilePath As String = String.Empty

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
      Console.WriteLine(BackupPath & "に実績CSV作成処理完了")
      Console.WriteLine("******************************")

      'IZ対応
      tmpRcvFilePath = Application.StartupPath _
                      & "\tmp\FTP_RCV_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".csv"
      Call MoveToBackUpLoadFile(DownloadPath, tmpRcvFilePath)
      Call ModHeaderText(tmpRcvFilePath)    ' テンポラリファイルのヘッダーに連番追加

      SqlServer.TrnStart()
      '実績CSVを計量テーブルに登録
      InsertResultTable(tmpRcvFilePath, UnitNumber, tmpCreateDate)
      'Console.WriteLine("計量テーブルにデータ更新")
      'Console.WriteLine("******************************")
      '実績CSVを実績テーブルに登録
      InsertJissekiTable(tmpRcvFilePath, UnitNumber, tmpCreateDate)
      SqlServer.TrnCommit()
      Console.WriteLine("実績テーブルにデータ更新")
      Console.WriteLine("******************************")
      System.IO.File.Delete(DownloadPath)
      System.IO.File.Delete(tmpRcvFilePath)


      InsertTRNLOG(UnitNumber, "", FILE_NAME, ftpRes.StatusCode & " " & ftpRes.StatusDescription)
      ErrorJudFlg = False
    Catch ex As Exception
      Call MoveErrCsv(tmpRcvFilePath) ' 読込エラーのCSVを退避
      SqlServer.TrnRollBack()
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ex.Message)
      ErrorJudFlg = True
      Environment.Exit(999)
      Throw New Exception(ex.Message)
      Console.WriteLine(ex.Message)
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
    End Try
  End Sub

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
      Call ComWriteErrLog("Module_MasterDownload",
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
      Call ComWriteErrLog("Module_MasterDownload",
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
      sql &= "     AND IP_ADDRESS IN('" & Para_ScaleNumber & "')"
    End If
    sql &= " ORDER BY  "
    sql &= "     UNIT_NUMBER"
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstColumnSet(prmColumnId As Integer) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     COLUMN_NAME"
    sql &= ",    COLUMN_NO"
    sql &= " FROM"
    sql &= "     MST_COLUMN_SET"
    sql &= " WHERE"
    sql &= "     COLUMN_ID = " & prmColumnId
    sql &= " AND COLUMN_NO is not null"

    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  '集計なし用SQL

  Private Function GetMstGroupKeiryo(prmCreateDate As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     MACHINE_NUMBER"
    sql &= ",    JISSEKI_DATE"
    sql &= ",    MIN(JISSEKI_TIME) JISSEKI_TIME"
    sql &= ",    BUMON_NUMBER"
    sql &= ",    YOBI_NUMBER"
    sql &= ",    YOBI_NAME"
    sql &= ",    SHOP_NUMBER"
    sql &= ",    KEIRYO_FLG"
    sql &= ",    CASE KEIRYO_FLG"
    sql &= "     WHEN 0"
    sql &= "     THEN SUM(JYURYO)"
    sql &= "     ELSE MAX(JYURYO)"
    sql &= "     END  JYURYO"
    sql &= ",    TANKA"
    sql &= ",    CASE KEIRYO_FLG"
    sql &= "     WHEN 0"
    sql &= "     THEN SUM(KINGAKU)"
    sql &= "     ELSE MAX(KINGAKU)"
    sql &= "     END  KINGAKU"
    sql &= ",    CASE KEIRYO_FLG"
    sql &= "     WHEN 0"
    sql &= "     THEN SUM(HANBAI_KINGAKU)"
    sql &= "     ELSE MAX(HANBAI_KINGAKU)"
    sql &= "     END  HANBAI_KINGAKU"
    sql &= ",    CASE KEIRYO_FLG"
    sql &= "     WHEN 0"
    sql &= "     THEN SUM(KOSUU)"
    sql &= "     ELSE MAX(KOSUU)"
    sql &= "     END  KOSUU"
    sql &= ",     SUM(JISSEKI_KOSUU) JISSEKI_KOSUU "
    sql &= ",     KOTAINO1 "
    sql &= ",     KOTAINO2 "
    sql &= ",     KOTAINO3 "
    sql &= ",    FREE1_CD "
    sql &= ",    FREE1_NM "
    sql &= ",    FREE2_CD "
    sql &= ",    FREE2_NM "
    sql &= ",    FREE3_CD "
    sql &= ",    FREE3_NM "
    sql &= ",    FREE4_CD "
    sql &= ",    FREE4_NM "
    sql &= ",    FREE5_CD "
    sql &= ",    FREE5_NM "
    sql &= ",    LOT_NUMBER "
    sql &= " FROM"
    sql &= "     TRN_KEIRYO"
    sql &= " WHERE "
    sql &= "     CREATE_DATE = '" & prmCreateDate & "'"
    sql &= " GROUP BY "
    sql &= "     MACHINE_NUMBER"
    sql &= " ,   JISSEKI_DATE"
    sql &= " ,   BUMON_NUMBER"
    sql &= " ,   YOBI_NUMBER"
    sql &= ",    YOBI_NAME"
    sql &= " ,   SHOP_NUMBER"
    sql &= " ,   KEIRYO_FLG"
    sql &= " ,   TANKA"
    sql &= " ,   TEIGAKU_KIGOU"
    sql &= " ,   KOTAINO1 "
    sql &= " ,   KOTAINO2 "
    sql &= " ,   KOTAINO3 "
    sql &= ",    FREE1_CD "
    sql &= ",    FREE1_NM "
    sql &= ",    FREE2_CD "
    sql &= ",    FREE2_NM "
    sql &= ",    FREE3_CD "
    sql &= ",    FREE3_NM "
    sql &= ",    FREE4_CD "
    sql &= ",    FREE4_NM "
    sql &= ",    FREE5_CD "
    sql &= ",    FREE5_NM "
    sql &= ",    LOT_NUMBER "
    sql &= " ORDER BY  "
    sql &= "     JISSEKI_DATE,SHOP_NUMBER,YOBI_NUMBER"
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  '集計用SQL
  Private Function GetMstNonGroupKeiryo(prmCreateDate As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     MACHINE_NUMBER"
    sql &= ",    JISSEKI_DATE"
    sql &= ",    JISSEKI_TIME "
    sql &= ",    BUMON_NUMBER"
    sql &= ",    YOBI_NUMBER"
    sql &= ",    YOBI_NAME"
    sql &= ",    SHOP_NUMBER"
    sql &= ",     JYURYO"
    sql &= ",    TANKA"
    sql &= ",    KINGAKU"
    sql &= ",    HANBAI_KINGAKU"
    sql &= ",    KEIRYO_FLG"
    sql &= ",    KOSUU"
    sql &= ",    JISSEKI_KOSUU "
    sql &= ",    KOTAINO1 "
    sql &= ",    KOTAINO2 "
    sql &= ",    KOTAINO3 "
    sql &= ",    FREE1_CD "
    sql &= ",    FREE1_NM "
    sql &= ",    FREE2_CD "
    sql &= ",    FREE2_NM "
    sql &= ",    FREE3_CD "
    sql &= ",    FREE3_NM "
    sql &= ",    FREE4_CD "
    sql &= ",    FREE4_NM "
    sql &= ",    FREE5_CD "
    sql &= ",    FREE5_NM "
    sql &= ",    LOT_NUMBER "
    sql &= " FROM"
    sql &= "     TRN_KEIRYO"
    sql &= " WHERE "
    sql &= "     CREATE_DATE = '" & prmCreateDate & "'"
    sql &= " ORDER BY  "
    sql &= "     JISSEKI_DATE,SHOP_NUMBER,YOBI_NUMBER"
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
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
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
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
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
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
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function UpdDenNo(prmDenNo As String) As String
    Dim sql As String = String.Empty
    sql &= " UPDATE TBL_DENNO"
    sql &= " SET  DenNo = '" & prmDenNo & "'"
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetDenpyoNoSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     DenNo "
    sql &= " FROM"
    sql &= "     TBL_DENNO "
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function


  Private Sub InsertResultTable(DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)
    Dim dt As New DataTable("TRN_KEIRYO")
    Dim HeaderRow As String() = Nothing
    SqlServer.GetResult(tmpDt, GetMstColumnSet(KEIRYO_COLUMN_ID))

    ' データベースのカラム名を定義
    Dim columnNames As New List(Of String)
    For Each tmpRow In tmpDt.Rows
      columnNames.Add(tmpRow("COLUMN_NAME"))
    Next

    ' データテーブルにカラムを追加
    For Each columnName As String In columnNames
      dt.Columns.Add(New DataColumn(columnName, GetType(String)))
    Next

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
              tmpCreateTmpDr(tmpDr("COLUMN_NAME")) = CsvList.Values(i)
            End If
          Next
        Next

        ' 実行時の時刻を追加
        '実績データ作成
        tmpJissekiDic.Add(GetKeiryoInsertDictionary(tmpCreateTmpDr, prmCreateDate))

        Dim tmpDataRow As DataRow = ComDic2Dt(tmpJissekiDic).Rows(0)

        For Each tmpColumn As DataColumn In tmpDataRow.Table.Columns
          If False = dt.Columns.Contains(tmpColumn.ColumnName) Then
            dt.Columns.Add(New DataColumn(tmpColumn.ColumnName, GetType(String)))
          End If
        Next

        dt.ImportRow(tmpDataRow)
      End While
    End Using

    ' データベースに挿入
    Dim sql As String = String.Empty
    For Each dr As DataRow In dt.Rows
      sql = GetInsertSql("TRN_KEIRYO", dr)
      With SqlServer
        Try
          If .Execute(sql) = 1 Then
            ' 更新成功
            InsertTRNLOG(UnitNumber, "", "", "実績登録完了")
          Else
            ' 削除失敗
            Throw New Exception("実績管理の登録処理に失敗しました。")
          End If
        Catch ex As Exception
          Call ComWriteErrLog("Module_MasterDownload",
                        System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
          InsertTRNLOG(UnitNumber, "", "", ex.Message)
        End Try
      End With
    Next
  End Sub

  Private Sub InsertJissekiTable(DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)
    Try
      Dim dt As New DataTable("TRN_Jisseki")
      Dim tmpKeiryoDt As New DataTable
      Dim tmpDenpyoNo As String = "0"
      Dim tmpGyoNo As Integer = 0
      Dim tmpBeforeNohinDay As String = "0"
      Dim tmpBeforeTokuiCd As String = "0"
      Dim HeaderRow As String() = Nothing
      Dim column As New DataColumn
      Dim sqlKeiryo As String = If(GROUP_TYPE_ON = ReadSettingIniFile("GROUP_TYPE", "VALUE") _
                                , GetMstGroupKeiryo(prmCreateDate) _
                                , GetMstNonGroupKeiryo(prmCreateDate))

      SqlServer.GetResult(tmpKeiryoDt, sqlKeiryo)
      SqlServer.GetResult(tmpDt, GetMstColumnSet(JISSEKI_COLUMN_ID))

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
      For Each tmpDr In tmpKeiryoDt.Rows
        'DataRow新規追加
        Dim tmpCreateTmpDr As DataRow = dt.NewRow()
        '実績データ
        Dim tmpJissekiDic As New List(Of Dictionary(Of String, String))


        '伝票番号、行番号採番
        GetDenpyoNo(tmpDenpyoNo, tmpGyoNo, ChkSaiban(tmpDr, tmpBeforeNohinDay, tmpBeforeTokuiCd, tmpGyoNo))

        '実績データ作成

        tmpJissekiDic.Add(GetInsertDictionary(tmpDr, tmpDenpyoNo, tmpGyoNo))

        '' 実行時の時刻を追加
        'dr("create_date") = DateTime.Now.ToString("yyyy-MM-dd")
        'dr("update_date") = DateTime.Now.ToString("yyyy-MM-dd")
        Dim tmpDataRow As DataRow = ComDic2Dt(tmpJissekiDic).Rows(0)

        For Each tmpColumn As DataColumn In tmpDataRow.Table.Columns
          If False = dt.Columns.Contains(tmpColumn.ColumnName) Then
            dt.Columns.Add(New DataColumn(tmpColumn.ColumnName, GetType(String)))
          End If
        Next

        dt.ImportRow(tmpDataRow)

      Next

      ' データベースに挿入
      Dim sql As String = String.Empty
      For Each dr As DataRow In dt.Rows
        sql = GetInsertSql("TRN_JISSEKI", dr)
        With SqlServer
          Try
            If .Execute(sql) = 1 Then
              ' 更新成功
              InsertTRNLOG(UnitNumber, "", "", "実績登録完了")
            Else
              ' 削除失敗
              Throw New Exception("実績管理の登録処理に失敗しました。")
            End If
          Catch ex As Exception
            Call ComWriteErrLog("Module_MasterDownload",
                          System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
            InsertTRNLOG(UnitNumber, "", "", ex.Message)
          End Try
        End With
      Next
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Sub

  Function GetInsertSql(prmTableName As String, dr As DataRow) As String
    Dim sql As String = "INSERT INTO " & prmTableName & "("
    Dim values As String = "VALUES ("
    Dim tmpColumnz As New List(Of String)

    For Each tmpColumn As DataColumn In dr.Table.Columns
      tmpColumnz.Add(tmpColumn.ColumnName)
    Next

    ' カラム名と値をセット
    For i As Integer = 0 To tmpColumnz.Count - 1
      sql &= tmpColumnz(i)

      Select Case tmpColumnz(i).ToUpper
        Case "JISSEKI_DATE".ToUpper
          values &= "'" & AdjustmentDateValue(dr(i).ToString().Replace("'", "''")) & "'"
        Case "JISSEKI_TIME".ToUpper
          values &= "'" & AdjustmentTimeValue(dr(i).ToString().Replace("'", "''")) & "'"
        Case "JYURYO".ToUpper
          ' 重量は端数処理を行う
          values &= "'" & AdjustmentWeightValue(dr(i).ToString().Replace("'", "''")) & "'"
        Case Else
          values &= "'" & dr(i).ToString().Replace("'", "''") & "'"
      End Select


      If i < tmpColumnz.Count - 1 Then
        sql &= ", "
        values &= ", "
      End If
    Next

    sql &= ") " & values & ")"
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

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
        Call ComWriteErrLog("Module_MasterDownload",
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
    Call WriteExecuteLog("Module_MasterDownload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetInsertDictionary(prmDataRow As DataRow, prmDenpyoNo As String, prmGyoNo As Integer) As Dictionary(Of String, String)

    'TRN_JISSEKIのカラム名で取得する必要がある。
    Try
      Dim rtnDic As New Dictionary(Of String, String)
      Dim tmpTokuisakiCd As String = prmDataRow.Item("SHOP_NUMBER").ToString.PadLeft(CUSTOMER_CODE_LENGTH, "0"c)
      Dim tmpShohinCd As String = prmDataRow.Item("YOBI_NUMBER").ToString.PadLeft(ITEM_CODE_LENGTH, "0"c)

      ' 商品名は得意先商品が設定されている場合はその名称
      ' 設定されていない場合は計量器から受信した名称を使用
      Dim tmpShohinNm As String = prmDataRow.Item("YOBI_NAME").ToString

      Dim tmpHiduke As String = prmDataRow.Item("JISSEKI_DATE").ToString
      tmpHiduke = tmpHiduke.Substring(0, 4) + "/" + tmpHiduke.Substring(4, 2) + "/" + tmpHiduke.Substring(6, 2)
      Dim tmpJikan As String = prmDataRow.Item("JISSEKI_DATE").ToString.PadLeft(4, "0"c)
      tmpJikan = tmpJikan.Substring(0, 2) + ":" + tmpJikan.Substring(2, 2)

      'データ操作関連
      Dim tmpTDt As New DataTable
      Dim tmpSDt As New DataTable
      Dim tmpTSDt As New DataTable
      Dim tmpZeiDt As New DataTable
      Dim tmpTDr As DataRow
      Dim tmpSDr As DataRow
      Dim tmpTSDr As DataRow
      'Dim tmpZeiDr As DataRow

      '得意先取得
      SqlServer.GetResult(tmpTDt, GetMstTokuisaki(tmpTokuisakiCd))
      If (tmpTDt.Rows.Count = 0) Then
        Throw New Exception("得意先マスタを取得できませんでした。")
      Else
        tmpTDr = tmpTDt.Rows(0)
      End If

      '商品取得
      SqlServer.GetResult(tmpSDt, GetMstShohin(tmpShohinCd))
      If (tmpSDt.Rows.Count = 0) Then
        Throw New Exception("商品マスタを取得できませんでした。")
      Else
        tmpSDr = tmpSDt.Rows(0)
      End If

      '得意先商品取得
      SqlServer.GetResult(tmpTSDt, GetMstTokuisakiShohin(tmpTokuisakiCd, tmpShohinCd))
      If (tmpTSDt.Rows.Count = 0) Then
        '<TODO > 00000の得意先を入れる対応を行ったが、商品マスタで管理するのはありか。 
        SqlServer.GetResult(tmpTSDt, GetMstTokuisakiShohin("000000", tmpShohinCd))
        If (tmpTSDt.Rows.Count = 0) Then
          tmpTSDr = Nothing
        Else
          tmpTSDr = tmpTSDt.Rows(0)
        End If
      Else
        tmpTSDr = tmpTSDt.Rows(0)
        ' 商品名は得意先商品が設定されている場合は、その名称を使用する
        tmpShohinNm = tmpTSDr("ShohinNM").ToString
      End If

      '数量、単価、金額関連
      Dim tmpTanka As String = If(tmpTSDr Is Nothing, If(tmpSDr("Baika1").ToString, "0"), tmpTSDr("Baika").ToString)
      Dim tmpSuryo As String = prmDataRow("JYURYO").ToString
      Dim tmpKingaku As String = CalculateKingaku(tmpTanka, tmpSuryo).ToString
      Dim tmpGenka As String = "0"
      Dim tmpGenKingaku As String = (Decimal.Parse(tmpGenka) * Decimal.Parse(tmpSuryo)).ToString
      'Dim tmpBaika As String = If(tmpSDr("HyojunKakaku").ToString, "0")
      'Dim tmpBaiKingaku As String = If(prmDataRow("KEIRYO_FLG").ToString = "0", Decimal.Parse(tmpBaika) * prmDataRow("JYURYO").ToString, Decimal.Parse(tmpBaika) * Decimal.Parse(tmpSuryo))
      Dim tmpBaika As String = prmDataRow("TANKA").ToString
      Dim tmpBaiKingaku As String = prmDataRow("KINGAKU").ToString

      rtnDic("Denku") = "0"
      rtnDic("UketukeDay") = tmpHiduke
      rtnDic("NohinDay") = Now.ToString("yyyyMMdd")
      rtnDic("SeikyuDay") = Now.ToString("yyyyMMdd")
      rtnDic("DenNo") = prmDenpyoNo
      rtnDic("GyoNo") = prmGyoNo
      rtnDic("TokuiCD") = tmpTokuisakiCd
      rtnDic("TokuiNM") = prmDataRow("FREE1_NM").ToString
      'rtnDic("TokuiNM2") = tmpTDr("TokuiNM2").ToString
      'rtnDic("TokuiKN") = tmpTDr("TokuiKana").ToString
      'rtnDic("TokuiZipCD") = tmpTDr("ZipCD").ToString
      'rtnDic("TokuiAdd1") = tmpTDr("Add1").ToString
      'rtnDic("TokuiAdd2") = tmpTDr("Add2").ToString
      'rtnDic("TokuiTel") = tmpTDr("TelNo").ToString
      rtnDic("TyokuCD") = prmDataRow("FREE2_CD").ToString.PadLeft(TYOKUSO_CODE_LENGTH, "0"c)
      rtnDic("TyokuNM") = prmDataRow("FREE2_NM").ToString
      'rtnDic("SenpoTantoNM") = tmpTDr("SenpoTanto").ToString
      rtnDic("BumonCD") = prmDataRow("BUMON_NUMBER").ToString.PadLeft(2, "0"c)
      rtnDic("UTantoCD") = "99"
      rtnDic("TekiyoCD") = "0"
      'rtnDic("TekiyoNM") = ""
      'rtnDic("BunruiCD") = ""
      rtnDic("DenKBN") = "0"
      rtnDic("ShohinCD") = tmpShohinCd
      rtnDic("ShohinNM") = tmpShohinNm
      rtnDic("ShohinKN") = tmpSDr("ShohinKana").ToString
      rtnDic("SMstKBN") = tmpSDr("SMstKBN").ToString
      rtnDic("Ku") = "0"
      rtnDic("SokoCD") = "0"
      rtnDic("Irisu") = prmDataRow("JISSEKI_KOSUU").ToString
      rtnDic("Hakosu") = prmDataRow("KEIRYO_FLG").ToString
      rtnDic("Suryo") = tmpSuryo
      rtnDic("Tani") = "kg"
      rtnDic("Tanka") = tmpTanka
      rtnDic("UriageKin") = tmpKingaku
      rtnDic("GenTanka") = tmpGenka
      rtnDic("GenkaGaku") = tmpGenKingaku
      rtnDic("Arari") = Decimal.Parse(tmpKingaku) - Decimal.Parse(tmpGenKingaku)
      rtnDic("ZeiKBN") = tmpSDr("ZeiKBN").ToString
      rtnDic("ZeikomiKBN") = tmpSDr("ZeikomiKBN").ToString
      rtnDic("Biko") = prmDataRow("FREE4_NM").ToString
      rtnDic("HyojunKKKu") = tmpBaika
      rtnDic("DojiNyukaKBN") = "1"
      rtnDic("UriTanka") = tmpBaika
      rtnDic("Baikagaku") = tmpBaiKingaku
      'rtnDic("Kikaku") = ""
      rtnDic("Iro") = prmDataRow("FREE3_NM").ToString
      'rtnDic("Size") = ""
      rtnDic("JutyuSu") = "0"
      rtnDic("Kingaku") = tmpKingaku
      rtnDic("Gouki") = prmDataRow("MACHINE_NUMBER").ToString
      rtnDic("ShukaPRTFLG") = "0"
      rtnDic("NohinPRTFLG") = "0"
      rtnDic("PCAFLG") = "0"
      'rtnDic("JANCD") = ""
      rtnDic("SakuseiDay") = Now.ToString
      rtnDic("OpenFLG") = "0"
      rtnDic("HoryuFLG") = "0"
      'rtnDic("SShohinCD") = ""
      'rtnDic("ShatenCD") = tmpTDr("ShatenCD").ToString
      'rtnDic("TorihikisakiCD") = tmpTDr("TorihikiCD").ToString
      rtnDic("Memo1") = prmDataRow("LOT_NUMBER").ToString
      rtnDic("Memo2") = prmDataRow("KOTAINO2").ToString
      'rtnDic("Hoka1") = ""
      'rtnDic("Hoka2") = ""
      'rtnDic("TokuiKubun") = tmpTDr("TokuiKubun").ToString
      rtnDic("ShoKubun") = tmpSDr("ShoKubun").ToString
      rtnDic("UketukeDay2") = tmpHiduke & " " & tmpJikan

      Return rtnDic
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Function

  Private Function GetKeiryoInsertDictionary(prmDataRow As DataRow, prmCreateDate As String) As Dictionary(Of String, String)

    'TRN_JISSEKIのカラム名で取得する必要がある。

    Dim rtnDic As New Dictionary(Of String, String)
    For Each tmpDc As DataColumn In prmDataRow.Table.Columns
      rtnDic(tmpDc.ColumnName) = prmDataRow.Item(tmpDc.ColumnName).ToString
    Next
    rtnDic("CREATE_DATE") = prmCreateDate
    rtnDic("UPDATE_DATE") = prmCreateDate

    'rtnDic("MACHINE_NUMBER") = prmDataRow.Item("MACHINE_NUMBER").ToString
    'rtnDic("JISSEKI_DATE") = prmDataRow.Item("JISSEKI_DATE").ToString
    'rtnDic("JISSEKI_TIME") = prmDataRow.Item("JISSEKI_TIME").ToString
    'rtnDic("BUMON_NUMBER") = prmDataRow.Item("BUMON_NUMBER").ToString
    'rtnDic("YOBI_NUMBER") = prmDataRow.Item("YOBI_NUMBER").ToString
    'rtnDic("TANKA_NUMBER") = prmDataRow.Item("TANKA_NUMBER").ToString
    'rtnDic("SHOP_NUMBER") = prmDataRow.Item("SHOP_NUMBER").ToString
    'rtnDic("JISSEKI_KUBUN") = prmDataRow.Item("JISSEKI_KUBUN").ToString
    'rtnDic("KEIRYO_FLG") = prmDataRow.Item("KEIRYO_FLG").ToString
    'rtnDic("JYURYO") = prmDataRow.Item("JYURYO").ToString
    'rtnDic("TANKA") = prmDataRow.Item("TANKA").ToString
    'rtnDic("KINGAKU") = prmDataRow.Item("KINGAKU").ToString
    'rtnDic("M_TOKKA_FLG") = prmDataRow.Item("M_TOKKA_FLG").ToString
    'rtnDic("M_TOKKA_DATA") = prmDataRow.Item("M_TOKKA_DATA").ToString
    'rtnDic("HANBAI_KINGAKU") = prmDataRow.Item("HANBAI_KINGAKU").ToString
    'rtnDic("TEI_KINGAKU") = prmDataRow.Item("TEI_KINGAKU").ToString
    'rtnDic("KOSUU") = prmDataRow.Item("KOSUU").ToString
    'rtnDic("TEIGAKU_KIGOU") = prmDataRow.Item("TEIGAKU_KIGOU").ToString
    'rtnDic("JISSEKI_KOSUU") = prmDataRow.Item("JISSEKI_KOSUU").ToString
    'rtnDic("TEI_JYURYO") = prmDataRow.Item("TEI_JYURYO").ToString
    'rtnDic("SYOHIN_CODE") = prmDataRow.Item("SYOHIN_CODE").ToString
    'rtnDic("HOUSOU_MODE") = prmDataRow.Item("HOUSOU_MODE").ToString
    'rtnDic("SEISAN_MODE") = prmDataRow.Item("SEISAN_MODE").ToString
    'rtnDic("KOTAINO1") = prmDataRow.Item("KOTAINO1").ToString
    'rtnDic("KOTAINO2") = prmDataRow.Item("KOTAINO2").ToString
    'rtnDic("KOTAINO3") = prmDataRow.Item("KOTAINO3").ToString
    'rtnDic("LOT_NUMBER") = prmDataRow.Item("LOT_NUMBER").ToString
    'rtnDic("LOT_NUMBER2") = prmDataRow.Item("LOT_NUMBER2").ToString
    'rtnDic("LOT_NUMBER3") = prmDataRow.Item("LOT_NUMBER3").ToString
    'rtnDic("TRAY_NUMBER") = prmDataRow.Item("TRAY_NUMBER").ToString
    'rtnDic("CREATE_DATE") = prmCreateDate
    'rtnDic("UPDATE_DATE") = prmCreateDate

    Return rtnDic
  End Function


  Private Function ChkSaiban(prmDr As DataRow, ByRef prmBeforeNohinDay As String, ByRef prmTokuiCd As String, prmGyoNo As Integer) As Boolean
    Dim rtn As Boolean = False

    If (prmBeforeNohinDay <> prmDr("JISSEKI_DATE").ToString _
                OrElse prmTokuiCd <> prmDr("SHOP_NUMBER").ToString _
                OrElse prmGyoNo = 6) Then
      rtn = True
    End If

    prmBeforeNohinDay = prmDr("JISSEKI_DATE").ToString
    prmTokuiCd = prmDr("SHOP_NUMBER").ToString
    Return rtn
  End Function

  Private Sub GetDenpyoNo(ByRef prmDenpyoNo As String, ByRef prmGyoNo As Integer, prmChkSaiban As Boolean)
    Dim tmpDt As New DataTable
    Dim tmpBeforeDenpyoNo As String = prmDenpyoNo
    '伝票番号取得
    SqlServer.GetResult(tmpDt, GetDenpyoNoSql)
    prmDenpyoNo = tmpDt.Rows(0).Item("DenNo").ToString

    If prmChkSaiban Then
      prmDenpyoNo = (Integer.Parse(prmDenpyoNo) + 1).ToString.PadLeft(DENPYO_NUMBER_LENGTH, "0"c)
    End If

    If (tmpBeforeDenpyoNo <> prmDenpyoNo) Then
      prmGyoNo = 1
    Else
      prmGyoNo = prmGyoNo + 1
    End If
    If prmDenpyoNo.Length > DENPYO_NUMBER_LENGTH Then
      prmDenpyoNo = "1".PadLeft(DENPYO_NUMBER_LENGTH, "0"c)
      prmGyoNo = 1
    End If

    '伝票番号テーブル更新
    SqlServer.Execute(UpdDenNo(prmDenpyoNo))

  End Sub


  ''' <summary>
  ''' 計量値調整
  ''' </summary>
  ''' <param name="prmTargetData">調整対象の計量値</param>
  ''' <returns>調整後の値</returns>
  ''' <remarks>
  '''   計量値が30kg未満
  '''     小数点第二位が 1～4は0に調整
  '''     小数点第二位が 5～9は5に調整
  '''   計量値が30kg以上
  '''     小数点第二位 2,4は0に調整
  '''     小数点第二位 6,8は5に調整
  '''     
  ''' 計量器の仕様として
  '''   30kg未満は小数点第二位は0～9が発生
  '''   30kg以上は小数点第二位は0と偶数のみ発生
  ''' の為、処理としては30kg未満・以上でロジックの変更はありません
  ''' </remarks>
  Private Function AdjustmentWeightValue(prmTargetData As String) As String
    Dim tmpWeight As String = prmTargetData
    Dim tmpPointPos As Integer = prmTargetData.IndexOf(".")
    Dim tmpAdjustmentValue As String = ""

    If tmpPointPos > 0 Then
      If 5 <= Integer.Parse(prmTargetData.Substring(tmpPointPos + 2, 1)) Then
        ' 小数点第二位が5以上は5に調整
        tmpAdjustmentValue = "5"
      Else
        ' 小数点第二位が5未満は0に調整
        tmpAdjustmentValue = "0"
      End If

      tmpWeight = prmTargetData.Substring(0, prmTargetData.Length - tmpPointPos) _
                  & tmpAdjustmentValue
    End If

    Return tmpWeight

  End Function

  Private Function AdjustmentDateValue(prmTargetData As String) As String
    Return DateTime.Parse(prmTargetData).ToString("yyyyMMdd")
  End Function

  Private Function AdjustmentTimeValue(prmTargetData As String) As String
    Return DateTime.Parse(prmTargetData).ToString("HHmm")
  End Function

End Module
