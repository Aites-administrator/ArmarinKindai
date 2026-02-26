Imports System.Net.NetworkInformation
Imports System.Windows.Forms
Imports Common
Imports Common.ClsFunction

Module Module_Upload
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

  ReadOnly FtpUploadPath As String = ReadSettingIniFile("FTP_UPLOAD_PATH", "VALUE")
  ReadOnly FtpDownloadPath As String = ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE")
  ReadOnly FtpAnsPath As String = ReadSettingIniFile("FTP_BACKUP_PATH", "VALUE")

  ReadOnly FileNameDigits As String = ReadSettingIniFile("FILENAME_DIGITS", "VALUE")
  ReadOnly CutFileNameDigits As String = ReadSettingIniFile("CUT_FILENAME_DIGITS", "VALUE")

  Dim UnitNumberString As String
  Dim IpAddressString As String
  Dim UnitNumberArray() As String
  Dim IpAddressArray() As String
  Dim PathName As String
  Dim TableName As String
  Dim DefText As String
  Dim ErrorJudFlg As Boolean
  Dim AnsErrorJudFlg As Boolean

  Sub Main(ScaleNumber() As String)
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("***    *****     *****     *****    *****     ****** ********    ******     ***")
    Console.WriteLine("*** ********** ******* *** ***** ********** ******* * ******* *** ******* *****")
    Console.WriteLine("***    ******* *******     *****    ******* ****** *** ******    ******** *****")
    Console.WriteLine("*** ********** ******* ************ ******* ******     ****** *** ******* *****")
    Console.WriteLine("*** ********** ******* *********    ******* ***** ***** ***** **** ****** *****")
    Console.WriteLine("*******************************************************************************")
    'ネットワーク情報取得
    GetNetworkInfo()
    Console.WriteLine("******************************")
    Console.WriteLine("マスタ送信処理開始")
    Console.WriteLine("******************************")
    Dim Para_ScaleNumber As String = String.Empty
    'Dim DownloadPath1 As String
    'Dim DownloadPath2 As String
    If ScaleNumber.Length > 0 Then
      For i As Integer = 0 To ScaleNumber.Length - 1
        If i = 0 Then
          Para_ScaleNumber = ScaleNumber(i)
        Else
          Para_ScaleNumber = Para_ScaleNumber + "," + ScaleNumber(i)
        End If
      Next
      Console.WriteLine("送信対象号機番号 ：" & Para_ScaleNumber)
      Console.WriteLine("******************************")
    End If

    '「計量器マスタ」取得
    SelectScaleMaster(Para_ScaleNumber)
    ''「商品マスタ」CSV作成
    CreateItemMasterCSV()
    ''「テナントマスタ」のCSVデータ作成
    'CreateManufacturerMasterCSV()
    ''「実績」のDEF作成
    'CreateResultsCSV()

    System.Threading.Thread.Sleep(1000)
    '引数定義
    Dim UploadPath As String
    Dim URL As String
    '計量器毎にループ(（マスタ）
    For j As Integer = 0 To UnitNumberArray.Length - 1
      '正常処理終了フラグ初期化★★★
      ErrorJudFlg = False
      AnsErrorJudFlg = False
      Console.WriteLine(UnitNumberArray(j) & "号機のマスタ送信処理 START")
      Console.WriteLine("******************************")

      'ファイル毎（DEF、CSV）にループ
      For k As Integer = 0 To 1
        Select Case k
          Case 0
            UploadPath = FtpUploadPath & "40MAS1" & FileNameDigits & UnitNumberArray(j) & ".DEF"
            URL = "ftp://" & IpAddressArray(j) & "/" & "40MAS1" & FileNameDigits & UnitNumberArray(j) & ".DEF"
            UploadFtp(UploadPath, URL, UnitNumberArray(j))
                        'System.Threading.Thread.Sleep(1000)
          Case 1
            UploadPath = FtpUploadPath & "40MAS1" & FileNameDigits & UnitNumberArray(j) & ".CSV"
            URL = "ftp://" & IpAddressArray(j) & "/" & "40MAS1" & FileNameDigits & UnitNumberArray(j) & ".CSV"
            UploadFtp(UploadPath, URL, UnitNumberArray(j))
            'System.Threading.Thread.Sleep(1000)
            'Case 2
            '    UploadPath = FtpUploadPath & "40MAS2" & FileNameDigits & UnitNumberArray(j) & ".DEF"
            '    URL = "ftp://" & IpAddressArray(j) & "/" & "40MAS2" & FileNameDigits & UnitNumberArray(j) & ".DEF"
            '    UploadFtp(UploadPath, URL, UnitNumberArray(j))
            '    'System.Threading.Thread.Sleep(1000)
            'Case 3
            '    UploadPath = FtpUploadPath & "40MAS2" & FileNameDigits & UnitNumberArray(j) & ".CSV"
            '    URL = "ftp://" & IpAddressArray(j) & "/" & "40MAS2" & FileNameDigits & UnitNumberArray(j) & ".CSV"
            '    UploadFtp(UploadPath, URL, UnitNumberArray(j))
            '    'System.Threading.Thread.Sleep(1000)
            'Case 4
            '    '実績ファイルDEF送信
            '    UploadPath = FtpUploadPath & "40TRAN" & FileNameDigits & UnitNumberArray(j) & ".DEF"
            '    URL = "ftp://" & IpAddressArray(j) & "/" & "40TRAN" & FileNameDigits & UnitNumberArray(j) & ".DEF"
            '    UploadFtp(UploadPath, URL, UnitNumberArray(j))
            '    'System.Threading.Thread.Sleep(1000)
        End Select

        If ErrorJudFlg Then
          Exit For
        Else
          System.Threading.Thread.Sleep(2000)
        End If

      Next
      'ログ登録　＆ アンサーファイル受信
      If ErrorJudFlg Then
        InsertTRNLOG(UnitNumberArray(j), "NG", "", "マスタ送信失敗")
        Console.WriteLine("マスタ送信処理失敗")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
      Else
        InsertTRNLOG(UnitNumberArray(j), "OK", "", "マスタ送信成功")

        'DownloadPath1 = FtpAnsPath & UnitNumberArray(j) & "/401A" & FileNameDigits & UnitNumberArray(j) & ".CSV"
        'DownloadPath2 = FtpAnsPath & UnitNumberArray(j) & "/402A" & FileNameDigits & UnitNumberArray(j) & ".CSV"
        'URL = "ftp://" & IpAddressArray(j) & "/401A" & FileNameDigits & UnitNumberArray(j) & ".CSV"
        'AnsDownloadFtp(DownloadPath1, URL, UnitNumberArray(j))
        'System.Threading.Thread.Sleep(2000)
        'URL = "ftp://" & IpAddressArray(j) & "/402A" & FileNameDigits & UnitNumberArray(j) & ".CSV"
        'AnsDownloadFtp(DownloadPath2, URL, UnitNumberArray(j))
        'System.Threading.Thread.Sleep(2000)
        'If AnsErrorJudFlg Then
        '  InsertTRNLOG(UnitNumberArray(j), "", "", "アンサーファイル受信失敗")
        '  Console.WriteLine("アンサーファイル受信処理失敗")
        '  Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        'Else
        '  InsertTRNLOG(UnitNumberArray(j), "", "", "アンサーファイル受信成功")
        '  Console.WriteLine("アンサーファイル受信処理成功")
        '  Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        '  Console.WriteLine("******************************")
        '  Console.WriteLine("マスタ送信処理成功")
        '  Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        '  Console.WriteLine("******************************")
        'End If
      End If
      Console.WriteLine(UnitNumberArray(j) & "号機のマスタ送信処理 END")
      Console.WriteLine("******************************")
    Next
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("***    *****     *****     ******    ***** *** *****    ***********************")
    Console.WriteLine("*** ********** ******* *** ****** ********  ** ***** *** **********************")
    Console.WriteLine("***    ******* *******     ******   ****** * * ***** *** **********************")
    Console.WriteLine("*** ********** ******* ********** ******** **  ***** *** **********************")
    Console.WriteLine("*** ********** ******* **********    ***** *** *****    ***********************")
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("マスタ送信処理終了")
    Console.WriteLine("******************************")
    Console.WriteLine("このウィンドウを閉じるには、任意のキーを押してください...")
    Console.ReadKey()
  End Sub

  Private Sub UploadFtp(UploadPath As String, URL As String, UnitNumber As String)
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

      InsertTRNLOG(UnitNumber, "", FILE_NAME, "FTPログイン成功")
      Console.WriteLine("FTPログイン成功")
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
      'アップロードするファイルを開く
      Dim fs As New System.IO.FileStream(UploadPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
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
      ftpRes.Close()
      Console.WriteLine("送信完了")
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ftpRes.StatusCode & " " & ftpRes.StatusDescription)
    Catch ex As Exception
      '正常処理終了フラグ変更★★★
      Call ComWriteErrLog("Module_Upload",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      ErrorJudFlg = True
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ex.Message)
      Console.ForegroundColor = ConsoleColor.Red
      Console.WriteLine(ex.Message)
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.ForegroundColor = ConsoleColor.Gray
      Console.WriteLine("******************************")
    End Try
  End Sub

  Private Sub AnsDownloadFtp(DownloadPath As String, URL As String, UnitNumber As String)
    '★ファイルの名前命名によって桁数が違う
    Dim FILE_NAME As String = Strings.Right(URL, CutFileNameDigits)
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

      InsertTRNLOG(UnitNumber, "", FILE_NAME, "FTPログイン成功")
      Console.WriteLine("FTPログイン成功")
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
      'ファイルをダウンロードするためのStreamを取得
      Dim resStrm As System.IO.Stream = ftpRes.GetResponseStream()
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
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ftpRes.StatusCode & " " & ftpRes.StatusDescription)
    Catch ex As Exception
      AnsErrorJudFlg = True
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ex.Message)
      Call ComWriteErrLog("Module_Upload",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Console.WriteLine(ex.Message)
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
      Console.WriteLine("計量器マスタ取得")
    Catch ex As Exception
      Call ComWriteErrLog("Module_Upload",
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
      InsertTRNLOG("", "", "", "計量器マスタ取得失敗")
      Console.WriteLine("計量器マスタ取得失敗")
    Finally
      tmpDt.Dispose()
    End Try
  End Sub
  Private Function GetMstScaleSelectSql(Para_ScaleNumber As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     unit_number,"
    sql &= "     ip_address"
    sql &= " FROM"
    sql &= "     MST_Scale"
    sql &= " WHERE"
    sql &= "     delete_flg = 0"
    If Para_ScaleNumber.Length <> 0 Then
      sql &= "     AND unit_number IN(" & Para_ScaleNumber & ")"
    End If
    sql &= " ORDER BY  "
    sql &= "     unit_number"
    Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql

  End Function
  Private Sub CreateItemMasterCSV()
    PathName = "40MAS1"
    TableName = "MST_SHOHIN"
    DefText = "呼出コード:40001,品番:40002,単重:40003,単重単位:40004,安全計数:40005,目標個数:40006,風袋:40007,風袋単位:40008,上限値:40009,基準値:40011,下限値:40013,小計目標値:40028,小計目標回数:40030,品名:40031,単重上限値:40201,単重上限値単位:40202,単重下限値:40203,単重下限値単位:40204
"
    CreateCsv(PathName, TableName, DefText)
  End Sub
  Private Sub CreateManufacturerMasterCSV()
    PathName = "40MAS2"
    TableName = "MST_Manufacturer"
    DefText = "製造者コード:40051,製造者名:40052"
    CreateCsv(PathName, TableName, DefText)
  End Sub
  Private Sub CreateResultsCSV()
    PathName = "40TRAN"
    TableName = "TRN_Results"
    DefText = "日付:40101,時刻:40102,端末機ナンバー:40103,呼出コード:40001,品番:40002,品名:40031,単重:40003,単重単位:40004,個数:40104,風袋:40007,風袋単位:40008,重量:40105,重量単位:40106,製造者コード:40051,製造者名:40052,ロット1:40107,ロット2:40108,区分:40111,単重上限値:40201,単重上限値単位:40202,単重下限値:40203,単重下限値単位:40204,風袋１重量:40375,風袋１重量単位:40376,風袋２重量:40378,風袋２重量単位:40379,風袋２の掛け数:40242,風袋１№:40240,風袋１名称:40377,風袋２№:40241,風袋２名称:40380,担当者№:40361,担当者名称:40362,フリー１№:40244,フリー１名称:40312,フリー２№:40245,フリー２名称:40322,フリー３№:40246,フリー３名称:40332,フリー４№:40247,フリー４名称:40342,フリー５№:40248,フリー５名称:40352,加工日:40220,加工時刻:40221,有効日:40228,有効時刻:40229,作業指示№:40400,明細№:40401,指示数:40405,実績数:40406,作業指示名称:40402"
    CreateCsv(PathName, TableName, DefText)
  End Sub


  Private Sub CreateCsv(PathName As String, TableName As String, DefText As String)
    Dim CsvPath As String
    Dim DefPath As String
    Dim isWriteHeader As Boolean = True
    Dim sql As String = String.Empty
    Dim OutputDb As New DataTable
    Dim OutputDt As New DataTable

    For j As Integer = 0 To UnitNumberArray.Length - 1
      CsvPath = FtpUploadPath & PathName & UnitNumberArray(j) & ".CSV"
      DefPath = FtpUploadPath & PathName & UnitNumberArray(j) & ".DEF"
      'CSVファイル出力時に使うEncoding
      '「Shift_JIS」を使用
      Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
      '書き込むファイルを開く
      Dim wrCsv As New System.IO.StreamWriter(CsvPath, False, encoding)
      Dim wrDef As New System.IO.StreamWriter(DefPath, False, encoding)
      wrDef.Write(DefText)
      wrDef.Close()

      If System.IO.File.Exists(DefPath) Then
        Select Case TableName
          Case "MST_SHOHIN"
            InsertTRNLOG(UnitNumberArray(j), "", "", "商品DEFファイル作成")
            'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の商品DEFファイル作成")
            sql = GetItemMasterSelectSql()
          Case "MST_Manufacturer"
            InsertTRNLOG(UnitNumberArray(j), "", "", "製造者DEFファイル作成")
            'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の製造者DEFファイル作成")
            sql = GetManufacturerMasterSelectSql()
          Case "TRN_Results"
            InsertTRNLOG(UnitNumberArray(j), "", "", "実績DEFファイル作成")
            'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の製造者DEFファイル作成")
            sql = GetManufacturerMasterSelectSql()
        End Select
      Else
        Select Case TableName
          Case "MST_SHOHIN"
            InsertTRNLOG(UnitNumberArray(j), "", "", "商品DEFファイル作成失敗")
            'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の商品DEFファイル作成失敗")
            sql = GetItemMasterSelectSql()
          Case "MST_Manufacturer"
            InsertTRNLOG(UnitNumberArray(j), "", "", "製造者DEFファイル作成失敗")
            'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の製造者DEFファイル作成失敗")
            sql = GetManufacturerMasterSelectSql()
        End Select
      End If

      Try
        With OutputDb
          SqlServer.GetResult(OutputDt, sql)
          If OutputDt.Rows.Count = 0 Then
            Select Case TableName
              Case "MST_SHOHIN"
                InsertTRNLOG(UnitNumberArray(j), "", "", "商品マスタ照会失敗")
                                'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の商品マスタ照会失敗")
              Case "MST_Manufacturer"
                InsertTRNLOG(UnitNumberArray(j), "", "", "製造者マスタ照会失敗")
                'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の製造者マスタ照会失敗")
            End Select
          Else
            Dim colCount As Integer = OutputDt.Columns.Count
            Dim lastColIndex As Integer = colCount - 1
            Dim i As Integer
            'ヘッダを書き込む
            If isWriteHeader Then
              For i = 0 To colCount - 1
                'ヘッダの取得
                Dim field As String = OutputDt.Columns(i).Caption
                '"で囲み書き込む
                field = EncloseDoubleQuotes(field)
                wrCsv.Write(field)
                'カンマ付与
                If lastColIndex > i Then
                  wrCsv.Write(","c)
                End If
              Next
              '改行
              wrCsv.Write(vbCrLf)
            End If
            'レコードを書き込む
            Dim row As DataRow
            For Each row In OutputDt.Rows
              For i = 0 To colCount - 1
                'フィールドの取得
                Dim field As String = row(i).ToString()
                '"で囲み書き込む
                field = EncloseDoubleQuotes(field)
                wrCsv.Write(field)
                'カンマ付与
                If lastColIndex > i Then
                  wrCsv.Write(","c)
                End If
              Next
              '改行
              wrCsv.Write(vbCrLf)
            Next
            '閉じる
            wrCsv.Close()

            If System.IO.File.Exists(DefPath) Then
              Select Case TableName
                Case "MST_SHOHIN"
                  InsertTRNLOG(UnitNumberArray(j), "", "", "商品CSVファイル作成")
                  'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の商品CSVファイル作成")
                  sql = GetItemMasterSelectSql()
                Case "MST_Manufacturer"
                  InsertTRNLOG(UnitNumberArray(j), "", "", "製造者CSVファイル作成")
                  'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の製造者CSVファイル作成")
                  sql = GetManufacturerMasterSelectSql()
              End Select
            Else
              Select Case TableName
                Case "MST_SHOHIN"
                  InsertTRNLOG(UnitNumberArray(j), "", "", "商品CSVファイル作成失敗")
                  'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の商品CSVファイル作成失敗")
                  sql = GetItemMasterSelectSql()
                Case "MST_Manufacturer"
                  InsertTRNLOG(UnitNumberArray(j), "", "", "製造者CSVファイル作成失敗")
                  'Console.WriteLine("号機番号 ： " & UnitNumberArray(j) & "の製造者CSVファイル作成失敗")
                  sql = GetManufacturerMasterSelectSql()
              End Select
            End If
          End If
        End With
      Catch ex As Exception
        InsertTRNLOG(UnitNumberArray(j), "", "", ex.Message)
        Call ComWriteErrLog("Module_Upload",
                  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        Throw New Exception(ex.Message)
      Finally
        OutputDt.Dispose()
      End Try
    Next
  End Sub
  Private Function EncloseDoubleQuotes(field As String) As String
    Return "" & field & ""
  End Function
  Private Function GetItemMasterSelectSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT "
    sql &= "     ShohinCD, "
    sql &= "     ShohinNM, "
    sql &= "     ShohinKana, "
    sql &= "     SysKBN, "
    sql &= "     SMstKBN, "
    sql &= "     ZaiKanri, "
    sql &= "     JituKanri, "
    sql &= "     Irisu, "
    sql &= "     Tani, "
    sql &= "     Iro, "
    sql &= "     Size, "
    sql &= "     ShohinKBN1, "
    sql &= "     ShohinKBN2, "
    sql &= "     ShohinKBN3, "
    sql &= "     ZeiKBN, "
    sql &= "     ZeikomiKBN, "
    sql &= "     SKetaT, "
    sql &= "     SKetaS, "
    sql &= "     HyojunKakaku, "
    sql &= "     Genka, "
    sql &= "     Baika1, "
    sql &= "     Baika2, "
    sql &= "     Baika3, "
    sql &= "     Baika4, "
    sql &= "     Baika5, "
    sql &= "     SokoCD, "
    sql &= "     SSiireCD, "
    sql &= "     ZaiTanka, "
    sql &= "     SiireTanka, "
    sql &= "     JANCD, "
    sql &= "     ShoKubun, "
    sql &= "     TDATE, "
    sql &= "     KDATE "
    sql &= " FROM "
    sql &= "     MST_SHOHIN "
    sql &= " ORDER BY "
    sql &= "     ShohinCD "
    Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Function GetManufacturerMasterSelectSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     Manufacturer_Code,"
    sql &= "     Manufacturer_Name"
    sql &= " FROM"
    sql &= "     MST_Manufacturer"
    sql &= " WHERE "
    sql &= "     delete_flg = 0 "
    Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub InsertTRNLOG(UNIT_NUMBER As String, RESULT As String, FILE_NAME As String, NOTE As String)
    Dim sql As String = String.Empty
    sql = GetInsertSql(UNIT_NUMBER, RESULT, FILE_NAME, NOTE)
    With tmpDb
      Try
        If .Execute(sql) = 1 Then
          ' 更新成功
          .TrnCommit()
        Else
          ' 削除失敗
          Throw New Exception("ログの作成処理に失敗しました")
        End If
      Catch ex As Exception
        Call ComWriteErrLog("Module_Upload",
                                  Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        InsertTRNLOG(UNIT_NUMBER, "", "", ex.Message)
        Console.WriteLine("ログ作成失敗")
        Console.WriteLine("号機番号 ： " & UNIT_NUMBER & "ファイル名 : " & FILE_NAME)
      End Try
    End With
  End Sub

  Private Function GetInsertSql(UNIT_NUMBER As String, RESULT As String, FILE_NAME As String, NOTE As String)
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())
    Dim PROCESS_DATE As String = tmpdate.ToString("yyyy-MM-dd")
    Dim PROCESS_TIME As String = tmpdate.ToString("HH:mm:ss.ss")
    Dim UPLOAD_TIME As String = tmpdate.ToString("yyyy-MM-dd HH:mm:ss.ss ")

    sql &= " INSERT INTO TRN_LOG("
    sql &= "             PROCESS_DATE,"
    sql &= "             MACHINE_NO,"
    sql &= "             PROCESS_TIME,"
    sql &= "             FILE_NAME,"
    sql &= "             MASTER_SEND_TIME,"
    sql &= "             MASTER_RESULT,"
    sql &= "             NOTE,"
    sql &= "             CREATE_DATE,"
    sql &= "             UPDATE_DATE"
    sql &= " )"
    sql &= " VALUES("
    sql &= "     '" & PROCESS_DATE & "',"
    sql &= "     '" & UNIT_NUMBER & "',"
    sql &= "     '" & PROCESS_TIME & "',"
    sql &= "     '" & FILE_NAME & "',"
    sql &= "     '" & UPLOAD_TIME & "',"
    sql &= "     '" & RESULT & "',"
    sql &= "     '" & NOTE.Replace("'", "") & "',"
    sql &= "     '" & tmpdate & "',"
    sql &= "     '" & tmpdate & "'"
    sql &= " )"
    Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub GetNetworkInfo()
    'すべてのネットワークインターフェイスを取得する
    Dim nis As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
    Dim ni As NetworkInterface
    For Each ni In nis
      'ネットワーク接続しているか調べる
      If ni.OperationalStatus = OperationalStatus.Up AndAlso
                ni.NetworkInterfaceType <> NetworkInterfaceType.Loopback AndAlso
                ni.NetworkInterfaceType <> NetworkInterfaceType.Tunnel Then
        'ネットワークインターフェイスの情報を表示する
        Console.WriteLine("接続名:{0}", ni.Name)
        Console.WriteLine("説明:{0}", ni.Description)
        Console.WriteLine("種類:{0}", ni.NetworkInterfaceType)
        Console.WriteLine("速度:{0}", ni.Speed)
        Console.WriteLine("MAC(物理)アドレス:{0}", ni.GetPhysicalAddress())

        '構成情報、アドレス情報を取得する
        Dim ipips As IPInterfaceProperties = ni.GetIPProperties()
        If Not (ipips Is Nothing) Then
          For Each ip As UnicastIPAddressInformation In ipips.UnicastAddresses
            Console.WriteLine("ユニキャストアドレス:{0}", ip.Address)
            Console.WriteLine("IPv4マスク:{0}", ip.IPv4Mask)
          Next ip
          For Each ip As System.Net.IPAddress In ipips.DnsAddresses
            Console.WriteLine("DNS:{0}", ip.ToString())
          Next ip
          For Each ip As System.Net.IPAddress In ipips.DhcpServerAddresses
            Console.WriteLine("DHCP:{0}", ip.ToString())
          Next ip
          For Each ip As GatewayIPAddressInformation In ipips.GatewayAddresses
            Console.WriteLine("Gateway:{0}", ip.Address)
          Next ip
          For Each ip As System.Net.IPAddress In ipips.WinsServersAddresses
            Console.WriteLine("WINS:{0}", ip.ToString())
          Next ip
        End If

        'IPv4の統計情報を表示する
        If ni.Supports(NetworkInterfaceComponent.IPv4) Then
          Dim ipv4 As IPv4InterfaceStatistics = ni.GetIPv4Statistics()
          Console.WriteLine("受信バイト数:{0}", ipv4.BytesReceived)
          Console.WriteLine("送信バイト数:{0}", ipv4.BytesSent)
          Console.WriteLine("受信したユニキャストパケット数:{0}",
                        ipv4.UnicastPacketsReceived)
          Console.WriteLine("送信したユニキャストパケット数:{0}",
                        ipv4.UnicastPacketsSent)
        End If
      End If
    Next ni
  End Sub
End Module
