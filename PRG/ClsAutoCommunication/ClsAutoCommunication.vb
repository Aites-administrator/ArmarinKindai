Imports System.IO
Imports Common
Imports Common.ClsFunction
Imports System.IO.Pipes
Imports System.Text
Imports System.Threading
Imports T.R.ZCommonClass.clsGlobalData
Public Class ClsAutoCommunication

  Private Shared watcher As System.IO.FileSystemWatcher = Nothing
  Private Shared Concat_ScaleNumber As String = String.Empty
  Private Shared PrintableCheck As Boolean = True
  Public Shared Sub AutoCommunication(prmPrintableCheck As Boolean)
    PrintableCheck = prmPrintableCheck
    If Not (watcher Is Nothing) Then
      Return
    End If



    watcher = New System.IO.FileSystemWatcher
    '監視するディレクトリを指定
    watcher.IncludeSubdirectories = True
    watcher.Path = ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE")
    '最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
    watcher.NotifyFilter = System.IO.NotifyFilters.LastAccess Or
        System.IO.NotifyFilters.LastWrite Or
        System.IO.NotifyFilters.FileName Or
        System.IO.NotifyFilters.DirectoryName


    ComWriteLog(Now.ToString & "ファイル監視:" & System.IO.NotifyFilters.FileName, "..\file.log")

    'すべてのファイルを監視
    watcher.Filter = "*.csv"

    'イベントハンドラの追加
    AddHandler watcher.Created, AddressOf watcher_Changed

    '監視を開始する
    watcher.EnableRaisingEvents = True
    Console.WriteLine("監視を開始しました。")


  End Sub

  'イベントハンドラ
  Private Shared Sub watcher_Changed(ByVal source As System.Object,
    ByVal e As System.IO.FileSystemEventArgs)
    Dim BkFolderFileName As String = GetAddBkFolder(e.FullPath)    'bkフォルダ取得
    Dim BkFolder As String = Path.GetDirectoryName(BkFolderFileName)    'bkフォルダ取得
    Dim FtpFolderName As String = Path.GetFileName(Path.GetDirectoryName(e.FullPath))    'Ftpフォルダ取得
    Dim FtpFolder As String = Path.GetDirectoryName(e.FullPath)    'Ftpフォルダ取得

    Try

      If e.FullPath.Contains("TRAN") Then
        If e.FullPath.Contains("\bk") Then
          Exit Sub
        End If

        '常駐が待機停止中のとき、ファイルを退避しておく。
        SendMessage("GET_TEXT")
        If Not PrintableCheck Then
          System.IO.File.Move(e.FullPath, BkFolderFileName)
          Exit Sub
        End If

        Select Case e.ChangeType
          Case System.IO.WatcherChangeTypes.Created
            'ファイル取込処理 

            If Process.GetProcessesByName("Nohin").Count = 0 _
                     AndAlso Process.GetProcessesByName("Result").Count = 0 _
                     AndAlso SendMessage("GET_TEXT") = "待機中" Then
              SendMessage("受信中")

              CallProcess("DownLoad", FtpFolderName)

              'バックアップファイルからFTP監視フォルダへ移動(計量器指定)
              MoveBackupTOImportPath(BkFolder, FtpFolderName)

              'Dim tmpBkFileName As String = TRAN_FILE_NAME & "*.csv"
              'Dim tmpFileName As String = TRAN_FILE_NAME & ReadSettingIniFile("FILENAME_DIGITS", "VALUE") & Integer.Parse(FtpFolderName) & ".csv"
              'If Dir(BkFolder & "\" & tmpBkFileName) <> "" Then
              '  System.IO.File.Move(BkFolder & "\" & Dir(BkFolder & "\" & tmpBkFileName), FtpFolder & "\" & tmpFileName)
              'End If
            End If

        End Select
      End If



    Catch ex As Exception
      'ダウンロード実績ファイルコピー
      MoveToBackUpLoadFile(e.FullPath, BkFolderFileName)
      System.IO.File.Delete(e.FullPath)
      SendMessage(ex.Message)
      ComWriteErrLog(ex)
    Finally

      If SendMessage("GET_TEXT") = "受信中" Then
        SendMessage("待機中")
      End If
    End Try

  End Sub

  Private Shared Function SendMessage(msg As String) As String
    Dim rtn As String = String.Empty
    'メインメニューテキスト更新用
    Dim pipeClient As New NamedPipeClientStream(".", "MyPipe", PipeDirection.InOut)
    Try
      Console.WriteLine("接続前")
      pipeClient.Connect() ' サーバー側（受信）が立ち上がっていればOK
      Console.WriteLine("接続完了")

      Dim writer As New StreamWriter(pipeClient, Encoding.UTF8),
                  reader As New StreamReader(pipeClient, Encoding.UTF8)

      Thread.Sleep(100)


      Console.WriteLine("送信開始")
      writer.WriteLine(msg)
      writer.Flush()
      Console.WriteLine("送信完了: [" & msg & "]")

      Dim response As String = reader.ReadLine()
      Console.WriteLine("📥 クライアント: 応答受信 → [" & response & "]")
      rtn = response
      If response = "待機中" Then
        PrintableCheck = True
      Else
        PrintableCheck = False
      End If

      writer.WriteLine("<END>")

    Catch ex As Exception
      ComWriteErrLog(ex)
      Console.WriteLine("❌ クライアント側エラー: " & ex.Message)
    Finally
      If pipeClient.IsConnected Then
        pipeClient.Dispose()
        Console.WriteLine("とじました: ")
      End If
    End Try
    Return rtn
  End Function

  Private Shared Sub watcher_Renamed(ByVal source As System.Object,
    ByVal e As System.IO.RenamedEventArgs)
    Console.WriteLine(("ファイル 「" + e.FullPath +
        "」の名前が変更されました。"))
  End Sub

  'Private Sub CallProcess(ProcessMode As String, Concat_ScaleNumber As String)
  Private Shared Sub CallProcess(ProcessMode As String, Concat_ScaleNumber As String)
    Try
      Dim CustomerMasterDownloadPath As String = ReadSettingIniFile("CUSTOMER_MASTER_DOWNLOAD_PATH", "VALUE")
      Dim ShippingMasterDownloadPath As String = ReadSettingIniFile("SHIPPING_MASTER_DOWNLOAD_PATH", "VALUE")
      Dim ItemMasterDownloadPath As String = ReadSettingIniFile("ITEM_MASTER_DOWNLOAD_PATH", "VALUE")
      Dim DownloadPath As String = ReadSettingIniFile("DOWNLOAD_PATH", "VALUE")
      Dim UploadPath As String = ReadSettingIniFile("UPLOAD_PATH", "VALUE")
      Dim ClsPrintingProcess As New ClsPrintingProcess.ClsPrintingProcess()
      Dim ReportType As String = ReadSettingIniFile("REPORT_TYPE", "VALUE")

      Select Case ProcessMode
        Case "ShippingMasterDownload"
          Dim DownloadExe As New ProcessStartInfo With {
                  .FileName = ShippingMasterDownloadPath,
                  .Arguments = Concat_ScaleNumber,
                      .CreateNoWindow = False,
                      .UseShellExecute = False
                  }
          Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(DownloadExe)
          p.WaitForExit()
          SelectFtpResult()
        Case "CustomerMasterDownload"

          Dim DownloadExe As New ProcessStartInfo With {
                  .FileName = CustomerMasterDownloadPath,
                  .Arguments = Concat_ScaleNumber,
                      .CreateNoWindow = False,
                      .UseShellExecute = False
                  }
          'ComMessageBox("開始", "テスト", typMsgBox.MSG_NORMAL)
          Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(DownloadExe)
          p.WaitForExit()
          'ComMessageBox("実績受信終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
          SelectFtpResult()
        Case "ItemMasterDownload"

          Dim DownloadExe As New ProcessStartInfo With {
                  .FileName = ItemMasterDownloadPath,
                  .Arguments = Concat_ScaleNumber,
                      .CreateNoWindow = False,
                      .UseShellExecute = False
                  }
          'ComMessageBox("開始", "テスト", typMsgBox.MSG_NORMAL)
          Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(DownloadExe)
          p.WaitForExit()
          'ComMessageBox("実績受信終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
          SelectFtpResult()
        Case "DownLoad"
          Dim ReportName As String = String.Empty
          Dim ReportWkTable As String = "WK_NOHIN"

          ReportName = If(ReportType = ClsCommonGlobalData.REPORT_TYPE_SHUKKA, "R_SHUKKA", "R_NOHIN")

          Dim DownloadExe As New ProcessStartInfo With {
                  .FileName = DownloadPath,
                  .Arguments = Concat_ScaleNumber,
                      .CreateNoWindow = True,
                      .UseShellExecute = False
                  }
          'ComMessageBox("開始", "テスト", typMsgBox.MSG_NORMAL)
          Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(DownloadExe)
          p.WaitForExit()

          If p.ExitCode <> 0 Then
            Throw New Exception("取込失敗")
          End If
          'ComMessageBox("実績受信終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
          Dim tmpWhereList As New Dictionary(Of String, String)
          tmpWhereList.Add("NohinPRTFLG =", "'0'")
          tmpWhereList.Add("INSTANT_PRINT_FLG IS NOT ", "NULL")

          '即時発行機能のため、条件追記
          If Not ReportType = ClsCommonGlobalData.REPORT_TYPE_NONE _
            OrElse tmpWhereList.ContainsKey("INSTANT_PRINT_FLG IS NOT ") Then
            ClsPrintingProcess.PrintProcess(ClsCommonGlobalData.PRINT_NON_PREVIEW, ReportWkTable, ReportName, tmpWhereList)
          End If
          SelectFtpResult()
        Case "Upload"
          '                    .CreateNoWindow = True,
          Dim UploadExe As New ProcessStartInfo With {
                      .FileName = UploadPath,
                      .Arguments = Concat_ScaleNumber,
          .UseShellExecute = False
                  }
          'ファイルを開いて終了まで待機する
          Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(UploadExe)
          p.WaitForExit()
          ComMessageBox("マスタ送信終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
          SelectFtpResult()
      End Select
    Catch ex As Exception
      SendMessage(ex.Message)
      Throw New Exception(ex.Message)

    End Try

  End Sub

  Public Shared Sub GetMaster(ProcessMode As String, Concat_ScaleNumber As String)
    CallProcess(ProcessMode, Concat_ScaleNumber)
  End Sub


  Public Shared Sub SelectFtpResult()
    Dim sql As String = String.Empty
    'sql = GetResultSelectSql()
    'Try
    '    With tmpDb
    '        SqlServer.GetResult(tmpDt, sql)
    '        If tmpDt.Rows.Count = 0 Then
    '            MessageBox.Show("計量器マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Else
    '            WriteDetail(tmpDt, ResultDetail, CheckboxExistFlg)
    '            SetAutomaticCheck()
    '        End If
    '    End With
    'Catch ex As Exception
    '    Call ComWriteErrLog([GetType]().Name,
    '                      System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
    '    Throw New Exception(ex.Message)
    'Finally
    '    tmpDt.Dispose()
    'End Try
  End Sub

  Private Shared Function SetConcat_ScaleNumber() As String
    Dim InitialCheckFlg = True
    'For i As Integer = 0 To ResultDetail.Rows.Count - 1
    '    If ResultDetail.Rows(i).Cells(0).Value = True Then
    '        If InitialCheckFlg Then
    '            Concat_ScaleNumber = ResultDetail.Rows(i).Cells(1).Value
    '            InitialCheckFlg = False
    '        Else
    '            Concat_ScaleNumber = Concat_ScaleNumber + " " + ResultDetail.Rows(i).Cells(1).Value
    '        End If
    '    End If
    'Next
    Return Concat_ScaleNumber
  End Function

  Private Shared Sub MoveToBackUpLoadFile(DownloadPath As String, BackupPath As String)
    ' コピー先ディレクトリを取得
    Dim destinationDirectory As String = Path.GetDirectoryName(BackupPath)

    ' ディレクトリが存在しない場合は作成
    If Not Directory.Exists(destinationDirectory) Then
      Directory.CreateDirectory(destinationDirectory)
    End If

    System.IO.File.Copy(DownloadPath, BackupPath)
  End Sub

End Class
