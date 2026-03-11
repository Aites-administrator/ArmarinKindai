Imports System.Windows.Forms
Imports Common
Imports Common.ClsFunction
Imports MainMenu.Form_Top
Imports Microsoft.VisualBasic.FileIO
Imports T.R.ZCommonClass.clsCodeLengthSetting

Module Modul_ShippingMasterDownload

  Private Const SHIPPING_COLUMN_ID As Integer = 6
  Private Const FILE_NAME_BASE As String = "40FRE2"
  Private ReadOnly tmpDb As New ClsSqlServer
  Dim tmpDt As New DataTable
  Dim tmpRcvFilePath As String = String.Empty

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

  ReadOnly CutFileNameDigits As String = ReadSettingIniFile("CUT_FILENAME_DIGITS", "VALUE")

  Dim UnitNumberArray() As String
  Dim ErrorJudFlg As Boolean = False

  Sub Main(ScaleNumber() As String)
    Dim ClsSetMessage As New SetMessage("")
    Dim tmpFileName As String = FILE_NAME_BASE

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
    SelectScaleMaster(Para_ScaleNumber, SqlServer, UnitNumberArray)
    '引数定義
    Dim dtNow As DateTime = DateTime.Now
    Dim DownloadPath As String
    Dim BackupPath As String
    'Dim UpLoadFile As String
    Dim URL As String

    '計量器毎にループ（実績）
    For j As Integer = 0 To UnitNumberArray.Length - 1
      tmpFileName = CreateDownloadFileName(FILE_NAME_BASE, UnitNumberArray(j))
      Console.WriteLine(UnitNumberArray(j) & "号機の発送先マスタ受信処理 START")
      Console.WriteLine("******************************")
      DownloadPath = FtpDownloadPath & "/" & UnitNumberArray(j) & "/" & tmpFileName & ".CSV"
      BackupPath = FtpBackupPath & "/" & UnitNumberArray(j) & "/" & tmpFileName & "_" & dtNow.ToString("yyMMddHHmmss") & ".CSV"
      URL = "ftp://localhost" & "/" & UnitNumberArray(j) & "/" & tmpFileName & ".CSV"
      Call DownloadFtp(DownloadPath, BackupPath, URL, UnitNumberArray(j))
      System.Threading.Thread.Sleep(2000)
      'ログ登録　＆ 削除ファイル送信
      If ErrorJudFlg Then
        InsertTRNLOG(UnitNumberArray(j), "NG", "", "発送先マスタ受信失敗", SqlServer, "Module_CustomerMasterDownload")
        Console.WriteLine("発送先マスタ受信処理失敗")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
      Else
        InsertTRNLOG(UnitNumberArray(j), "OK", "", "発送先マスタ受信成功", SqlServer, "Module_CustomerMasterDownload")
        Console.WriteLine("発送先マスタ受信処理成功")
        Console.WriteLine("号機番号 ： " & UnitNumberArray(j))
        Console.WriteLine("******************************")
        System.Threading.Thread.Sleep(2000)
      End If
      Console.WriteLine(UnitNumberArray(j) & "号機の発送先マスタ受信処理 END")
      Console.WriteLine("******************************")
    Next
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("***    *****     *****     ******    ***** *** *****    ***********************")
    Console.WriteLine("*** ********** ******* *** ****** ********  ** ***** *** **********************")
    Console.WriteLine("***    ******* *******     ******   ****** * * ***** *** **********************")
    Console.WriteLine("*** ********** ******* ********** ******** **  ***** *** **********************")
    Console.WriteLine("*** ********** ******* **********    ***** *** *****    ***********************")
    Console.WriteLine("*******************************************************************************")
    Console.WriteLine("発送先マスタ受信処理終了")
    Console.WriteLine("******************************")
    Console.WriteLine("このウィンドウを閉じるには、任意のキーを押してください...")

  End Sub

  Private Sub DownloadFtp(DownloadPath As String, BackupPath As String, URL As String, UnitNumber As String)
    Dim FILE_NAME As String = Strings.Right(URL, CutFileNameDigits)
    Dim tmpCreateDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
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

      InsertTRNLOG(UnitNumber, "", FILE_NAME, "FTPログイン成功", SqlServer, "Module_CustomerMasterDownload")
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
      InsertTRNLOG(UnitNumber, "", FILE_NAME, "FTP処理終了", SqlServer, "Module_CustomerMasterDownload")

      'ダウンロード実績ファイルコピー
      MoveToBackUpLoadFile(DownloadPath, BackupPath)
      Console.WriteLine(BackupPath & "に発送先マスタCSV作成処理完了")
      Console.WriteLine("******************************")

      'IZ対応
      tmpRcvFilePath = Application.StartupPath _
                      & "\tmp\FTP_RCV_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".csv"
      Call MoveToBackUpLoadFile(DownloadPath, tmpRcvFilePath)
      Call ModHeaderText(tmpRcvFilePath)    ' テンポラリファイルのヘッダーに連番追加

      ''発送先マスタCSVを実績テーブルに登録
      InsertShippingTable(tmpRcvFilePath, UnitNumber, tmpCreateDate)
      Console.WriteLine("発送先マスタテーブルにデータ更新")
      Console.WriteLine("******************************")
      System.IO.File.Delete(DownloadPath)
      System.IO.File.Delete(tmpRcvFilePath)

      InsertTRNLOG(UnitNumber, "", FILE_NAME, ftpRes.StatusCode & " " & ftpRes.StatusDescription, SqlServer, "Module_CustomerMasterDownload")
      ErrorJudFlg = False
    Catch ex As Exception
      Call MoveErrCsv(tmpRcvFilePath) ' 読込エラーのCSVを退避
      InsertTRNLOG(UnitNumber, "", FILE_NAME, ex.Message, SqlServer, "Module_CustomerMasterDownload")
      ErrorJudFlg = True
      Console.WriteLine(ex.Message)
      Console.WriteLine("号機番号 ： " & UnitNumber & ", " & "ファイル名 : " & FILE_NAME)
      Console.WriteLine("******************************")
    End Try
  End Sub

  Private Sub InsertShippingTable(DownloadPath As String, UnitNumber As Integer, prmCreateDate As String)
    Dim dt As New DataTable("MST_CHOKUSO")
    Dim tmpBeforeNohinDay As String = "0"
    Dim tmpBeforeTokuiCd As String = "0"
    Dim HeaderRow As String() = Nothing
    Dim column As New DataColumn
    Dim tmpShippingDic As New List(Of Dictionary(Of String, String))

    Try

      tmpDb.TrnStart()

      SqlServer.GetResult(tmpDt, GetMstColumnSet(SHIPPING_COLUMN_ID))

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
          tmpShippingDic = New List(Of Dictionary(Of String, String))


          For i As Integer = 0 To row.Length - 1
            Dim tmpRow() As DataRow = tmpDt.Select("COLUMN_NO = '" & CsvList.Keys(i) & "'")

            For Each tmpDr In tmpRow
              If tmpCreateTmpDr(tmpDr("COLUMN_NAME")) Is DBNull.Value Then
                tmpCreateTmpDr(tmpDr("COLUMN_NAME")) = ControlCodeEscape(CsvList.Values(i))
              End If
            Next
          Next

          '単価更新 or 実績データ作成
          tmpShippingDic.Add(GetInsertDictionary(tmpCreateTmpDr))
          '実行時の時刻を追加
          Dim tmpDataRow As DataRow = ComDic2Dt(tmpShippingDic).Rows(0)

          For Each tmpColumn As DataColumn In tmpDataRow.Table.Columns
            If False = dt.Columns.Contains(tmpColumn.ColumnName) Then
              dt.Columns.Add(New DataColumn(tmpColumn.ColumnName, GetType(String)))
            End If
          Next

          dt.ImportRow(tmpDataRow)

        End While
      End Using

      tmpDb.Execute("delete from MST_CHOKUSO ")

      ' データベースに挿入
      Dim sql As String = String.Empty
      For Each dr As DataRow In dt.Rows
        sql = GetInsertSql("MST_CHOKUSO", dr)
        If tmpDb.Execute(sql) = 1 Then
          ' 更新成功
          InsertTRNLOG(UnitNumber, "", "", "マスタ登録完了", SqlServer, "Module_CustomerMasterDownload")
        Else
          ' 削除失敗
          Throw New Exception("マスタ管理の登録処理に失敗しました。")
        End If
      Next

      tmpDb.TrnCommit()


    Catch ex As Exception
      tmpDb.TrnRollBack()
      Call ComWriteErrLog("Module_CustomerMasterDownload",
                            System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      InsertTRNLOG(UnitNumber, "", "", ex.Message, SqlServer, "Module_CustomerMasterDownload")
      Throw New Exception(ex.Message)

    End Try


  End Sub

  Private Function GetInsertDictionary(prmDataRow As DataRow) As Dictionary(Of String, String)

    Dim tmpCreateDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

    Dim rtnDic As New Dictionary(Of String, String)
    For Each tmpDc As DataColumn In prmDataRow.Table.Columns
      If tmpDc.ColumnName.ToUpper = "CODE".ToUpper Then
        rtnDic(tmpDc.ColumnName) = prmDataRow.Item(tmpDc.ColumnName).ToString.PadLeft(TYOKUSO_CODE_LENGTH, "0"c)
      Else
        rtnDic(tmpDc.ColumnName) = prmDataRow.Item(tmpDc.ColumnName).ToString
      End If
    Next

    Return rtnDic
  End Function
End Module
