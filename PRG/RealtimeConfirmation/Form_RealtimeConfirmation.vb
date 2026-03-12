Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports Common
Imports Common.ClsFunction
Imports Common.ClsCommonGlobalData
Imports T.R.ZCommonCon.DbConnectData

Imports CommonPcaDx

Public Class Form_RealtimeConfirmation

    Dim CheckboxExistFlg As New Boolean
    Dim Concat_ScaleNumber As String = String.Empty
    Dim PathName As String
    Dim TableName As String
    Dim DefText As String

    Private ReadOnly ImportPath As String = ReadSettingIniFile("IMPORT_PATH", "VALUE")
  Private ReadOnly TenantFileName As String = ReadSettingIniFile("TENANT_FILENAME", "VALUE")

  Private ReadOnly tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    ' SQLサーバー操作オブジェクト
    Private _SqlServer As clsSqlServer
    Private ReadOnly Property SqlServer As clsSqlServer
        Get
            If _SqlServer Is Nothing Then
                _SqlServer = New clsSqlServer
            End If
            Return _SqlServer
        End Get
    End Property
  Private Sub Form_RealtimeConfirmation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'ユーザーからのデータ追加を不可にしておく
    MaximizeBox = False
    CheckboxExistFlg = True
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "通信結果" & " ( " & updateTime & " ) "
    ResultDetail.RowHeadersVisible = False
    ResultDetail.AllowUserToAddRows = False
    MaximizeBox = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    Dim checkBox_Trans As New DataGridViewCheckBoxColumn
    ResultDetail.Columns.Add(checkBox_Trans)
    checkBox_Trans.Width = 65

    ResultDetail.ColumnCount = 4

    ResultDetail.Columns(0).HeaderText = "選択"
    ResultDetail.Columns(1).HeaderText = "号機No"
    ResultDetail.Columns(2).HeaderText = "実績受信"
    ResultDetail.Columns(3).HeaderText = "結果"
    'ResultDetail.Columns(4).HeaderText = "マスタ送信"
    'ResultDetail.Columns(5).HeaderText = "結果"
    'カラムの幅指定
    ResultDetail.Columns(1).Width = 100
    ResultDetail.Columns(2).Width = 160
    ResultDetail.Columns(3).Width = 90
    'ResultDetail.Columns(4).Width = 160
    'ResultDetail.Columns(5).Width = 90

    'ヘッダーの整列設定
    For i As Integer = 0 To 3
      ResultDetail.Columns(i).DefaultCellStyle.Alignment =
               DataGridViewContentAlignment.MiddleCenter
      ResultDetail.Columns(i).HeaderCell.Style.Alignment =
              DataGridViewContentAlignment.MiddleCenter
    Next

    '選択モード設定(全カラム)
    ResultDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    '照会メソッド呼出し
    SelectFtpResult()
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
        ' 実績受信
        ClickGetJissekiButton()

      Case Keys.F10
        ' 終了
        Close()
    End Select

  End Sub

  Private Sub ResultDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ResultDetail.CellClick
        If e.RowIndex >= 0 Then
            ' 左端（0セル目）がクリックされた時のみ
            If e.ColumnIndex = 0 Then
                ' 未チェック状態ならチェックに、逆なら未チェックに
                If CBool(ResultDetail.CurrentRow.Cells(0).Value) Then
                    ResultDetail.CurrentRow.Cells(0).Value = False
                Else
                    ResultDetail.CurrentRow.Cells(0).Value = True
                End If
            End If
        End If
    End Sub
    Private Sub SendButton_Click(sender As Object, e As EventArgs) Handles SendButton.Click
        If CheckSelectRow() = False Then
            MessageBox.Show("行を選択して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            CallProcess("Upload", SetConcat_ScaleNumber)
        End If
    End Sub
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Public Sub SelectFtpResult()
        Dim sql As String = String.Empty
        sql = GetResultSelectSql()
        Try
            With tmpDb
                SqlServer.GetResult(tmpDt, sql)
                If tmpDt.Rows.Count = 0 Then
                    MessageBox.Show("計量器マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    WriteDetail(tmpDt, ResultDetail, CheckboxExistFlg)
                    SetAutomaticCheck()
                End If
            End With
        Catch ex As Exception
            Call ComWriteErrLog([GetType]().Name,
                                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw New Exception(ex.Message)
        Finally
            tmpDt.Dispose()
        End Try
    End Sub
    Public Sub SetAutomaticCheck()
        For i As Integer = 0 To ResultDetail.Rows.Count - 1
            If ResultDetail.Rows(i).Cells(3).Value = "NG" Then
                ResultDetail(3, i).Style.BackColor = Color.DarkRed
                ResultDetail(3, i).Style.SelectionBackColor = Color.DarkRed
            End If

      'If ResultDetail.Rows(i).Cells(5).Value = "NG" Then
      '    ResultDetail(5, i).Style.BackColor = Color.DarkRed
      '    ResultDetail(5, i).Style.SelectionBackColor = Color.DarkRed
      'End If

      ResultDetail.Rows(i).Cells(0).Value = True
        Next
    End Sub
    Private Function GetResultSelectSql() As String

        Dim sql As String = String.Empty

        sql &= " WITH AC_TRN_LOG AS("
        sql &= "     SELECT"
        sql &= "         MACHINE_NO,"
        sql &= "         PROCESS_DATE,"
        sql &= "         ACHIEVEMENT_RECEIVE_TIME,"
        sql &= "         ACHIEVEMENT_RESULT"
        sql &= "     FROM"
        sql &= "         TRN_LOG A"
        sql &= "     WHERE"
        sql &= "         ACHIEVEMENT_RECEIVE_TIME = ("
        sql &= "             SELECT"
        sql &= "                 MAX(ACHIEVEMENT_RECEIVE_TIME)"
        sql &= "             FROM"
        sql &= "                 TRN_LOG AS B"
        sql &= "             WHERE"
        sql &= "                 A.MACHINE_NO = B.MACHINE_NO"
        sql &= "             And B.ACHIEVEMENT_RESULT In ('OK', 'NG') "
        sql &= "         )"
        sql &= "         AND ACHIEVEMENT_RESULT IN ('OK', 'NG') "
        sql &= " ),"
        sql &= " MST_TRN_LOG As("
        sql &= "     SELECT"
        sql &= "         MACHINE_NO,"
        sql &= "         PROCESS_DATE,"
        sql &= "         MASTER_SEND_TIME,"
        sql &= "         MASTER_RESULT"
        sql &= "     FROM"
        sql &= "         TRN_LOG A"
        sql &= "     WHERE"
        sql &= "         MASTER_SEND_TIME = ("
        sql &= "             SELECT"
        sql &= "                 MAX(MASTER_SEND_TIME)"
        sql &= "             FROM"
        sql &= "                 TRN_LOG AS B"
        sql &= "             WHERE"
        sql &= "                 A.MACHINE_NO = B.MACHINE_NO"
        sql &= "             AND B.MASTER_RESULT IN ('OK', 'NG') "
        sql &= "         )"
        sql &= "         AND MASTER_RESULT IN ('OK', 'NG') "
        sql &= " )"
        sql &= " SELECT"
        sql &= "     UNIT_NUMBER As 号機番号,"
        sql &= "     SUBSTRING(ACHIEVEMENT_RECEIVE_TIME, 1, 19) As 実績受信日時,"
    sql &= "     ACHIEVEMENT_RESULT AS 実績受信結果"
    'sql &= "     SUBSTRING(MASTER_SEND_TIME, 1, 19) As マスタ送信日時,"
    '    sql &= "     MASTER_RESULT As マスタ送信結果"
    sql &= " FROM"
        sql &= "     MST_SCALE"
        sql &= "     LEFT JOIN"
        sql &= "         AC_TRN_LOG"
        sql &= "     ON  MST_SCALE.UNIT_NUMBER = AC_TRN_LOG.MACHINE_NO"
        sql &= "     LEFT JOIN"
        sql &= "         MST_TRN_LOG"
        sql &= "     ON  MST_SCALE.UNIT_NUMBER = MST_TRN_LOG.MACHINE_NO"
        sql &= " WHERE"
        sql &= "     DELETE_FLG = 0"
        sql &= " ORDER BY"
        sql &= "     MST_SCALE.UNIT_NUMBER"
        Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function
  Private Sub ReceiveButton_Click(sender As Object, e As EventArgs) Handles ReceiveButton.Click
    ClickGetJissekiButton()
  End Sub

  Private Sub ClickGetJissekiButton()
    Dim BkFolder As String = ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE") & "\" & SetConcat_ScaleNumber() & "\bk"    'bkフォルダ取得

    If CheckSelectRow() = False Then
      MessageBox.Show("行を選択して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Else
      CallProcess("DownLoad", SetConcat_ScaleNumber)

      Dim tmpBkFileName As String = TRAN_FILE_NAME & "*.csv"
      Dim tmpFileName As String = TRAN_FILE_NAME & ReadSettingIniFile("FILENAME_DIGITS", "VALUE") & Integer.Parse(SetConcat_ScaleNumber()) & ".csv"

      'ファイル取込処理 
      If Process.GetProcessesByName("Nohin").Count = 0 _
                   AndAlso Process.GetProcessesByName("Result").Count = 0 Then

        'バックアップファイルからFTP監視フォルダへ移動(計量器指定)
        MoveBackupTOImportPath(BkFolder, SetConcat_ScaleNumber())

        'If Dir(BkFolder & "\" & tmpBkFileName) <> "" Then
        '  System.IO.File.Move(BkFolder & "\" & Dir(BkFolder & "\" & tmpBkFileName), ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE") & "\" & SetConcat_ScaleNumber() & "\" & tmpFileName)
        'End If
      End If

    End If

  End Sub

  Function CheckSelectRow() As Boolean
        Dim result As Boolean = False
        For i As Integer = 0 To ResultDetail.Rows.Count - 1
            If ResultDetail.Rows(i).Cells(0).Value = True Then
                result = True
                Exit For
            End If
        Next
        Return result
    End Function

    Function SetConcat_ScaleNumber() As String
        Dim InitialCheckFlg = True
        For i As Integer = 0 To ResultDetail.Rows.Count - 1
            If ResultDetail.Rows(i).Cells(0).Value = True Then
                If InitialCheckFlg Then
                    Concat_ScaleNumber = ResultDetail.Rows(i).Cells(1).Value
                    InitialCheckFlg = False
                Else
                    Concat_ScaleNumber = Concat_ScaleNumber + " " + ResultDetail.Rows(i).Cells(1).Value
                End If
            End If
        Next
        Return Concat_ScaleNumber
    End Function
    Private Sub CallProcess(ProcessMode As String, Concat_ScaleNumber As String)

    Dim DownloadPath As String = ReadSettingIniFile("DOWNLOAD_PATH", "VALUE")
    Dim UploadPath As String = ReadSettingIniFile("UPLOAD_PATH", "VALUE")

        Select Case ProcessMode
            Case "DownLoad"
                Dim DownloadExe As New ProcessStartInfo With {
                        .FileName = DownloadPath,
                        .Arguments = Concat_ScaleNumber,
                            .UseShellExecute = False
                        }
                Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(DownloadExe)
                p.WaitForExit()
                MessageBox.Show("実績受信終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
                SelectFtpResult()
            Case "Upload"
                '商品登録
                PcaInsShohin()

                '得意先登録
                PcaInsTokuisaki()
                MessageBox.Show("マスタ送信終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
                SelectFtpResult()
        End Select
    End Sub

    Private Sub PcaInsShohin()

        Dim strDate As DateTime = Now
        Dim tmpPcaInsSms As New clsPcaSms()
        Dim tmpPcaUpdSms As New clsPcaSms()
        Dim tmpPcaSmsCount As New clsPcaSms()
        Dim tmpPcaSmsList As New List(Of clsPcaSmsD)
        Dim tmpDt As New DataTable
        Dim tmpUpdFlg As Boolean = False

        Try
            SqlServer.GetResult(tmpDt, GetMstItemSql)

            For Each tmpDr As DataRow In tmpDt.Rows
                Dim tmpPcaSmsD As New clsPcaSmsD()
                Dim tmpPcaSmsSearchCondition As New clsPcaSmsD()

                tmpPcaSmsSearchCondition.商品コード = tmpDr.Item("ShohinCD").ToString
                tmpPcaSmsList = tmpPcaSmsCount.GetData(tmpPcaSmsSearchCondition)

                tmpPcaSmsD.商品コード = tmpDr.Item("ShohinCD").ToString
                tmpPcaSmsD.商品名 = tmpDr.Item("ShohinNM").ToString
                tmpPcaSmsD.システム区分 = tmpDr.Item("SysKBN").ToString
                tmpPcaSmsD.マスター区分 = tmpDr.Item("SMstKBN").ToString
                tmpPcaSmsD.在庫管理 = tmpDr.Item("ZaiKanri").ToString
                tmpPcaSmsD.実績管理 = tmpDr.Item("JituKanri").ToString
                tmpPcaSmsD.入数 = tmpDr.Item("Irisu").ToString
                tmpPcaSmsD.単位 = tmpDr.Item("Tani").ToString
                tmpPcaSmsD.色 = tmpDr.Item("Iro").ToString
                tmpPcaSmsD.サイズ = tmpDr.Item("Size").ToString
                tmpPcaSmsD.商品区分1 = tmpDr.Item("ShohinKBN1").ToString
                tmpPcaSmsD.商品区分2 = tmpDr.Item("ShohinKBN2").ToString
                tmpPcaSmsD.商品区分3 = tmpDr.Item("ShohinKBN3").ToString
                tmpPcaSmsD.税区分 = tmpDr.Item("ZeiKBN").ToString
                tmpPcaSmsD.売上税込区分 = tmpDr.Item("ZeikomiKBN").ToString
                tmpPcaSmsD.単価小数桁 = tmpDr.Item("SKetaT").ToString
                tmpPcaSmsD.数量小数桁 = tmpDr.Item("SKetaS").ToString
                tmpPcaSmsD.標準価格 = tmpDr.Item("HyojunKakaku").ToString
                tmpPcaSmsD.原価 = tmpDr.Item("Genka").ToString
                tmpPcaSmsD.売価1 = tmpDr.Item("Baika1").ToString
                tmpPcaSmsD.売価2 = tmpDr.Item("Baika2").ToString
                tmpPcaSmsD.売価3 = tmpDr.Item("Baika3").ToString
                tmpPcaSmsD.売価4 = tmpDr.Item("Baika4").ToString
                tmpPcaSmsD.売価5 = tmpDr.Item("Baika5").ToString
                tmpPcaSmsD.倉庫コード = tmpDr.Item("SokoCD").ToString
                tmpPcaSmsD.主仕入先 = tmpDr.Item("SSiireCD").ToString
                tmpPcaSmsD.在庫単価 = tmpDr.Item("ZaiTanka").ToString
                tmpPcaSmsD.仕入単価 = tmpDr.Item("SiireTanka").ToString

                If tmpPcaSmsList.Count = 0 Then
                    tmpPcaInsSms.AddDetail(tmpPcaSmsD)
                Else
                    tmpPcaSmsD.更新Date = tmpPcaSmsList(0).更新Date
                    tmpPcaUpdSms.AddDetail(tmpPcaSmsD)
                End If

            Next


            '商品更新
            tmpPcaUpdSms.Update()
            '商品追加
            tmpPcaInsSms.Create()
        Catch ex As Exception
            ComWriteErrLog(ex)
            Throw New Exception(ex.Message)
        Finally
            tmpPcaSmsList.Clear()
            tmpPcaUpdSms.Dispose()
            tmpPcaInsSms.Dispose()

        End Try
    End Sub

    Private Sub PcaInsTokuisaki()

        Dim strDate As DateTime = Now
        Dim tmpPcaInsTms As New clsPcaTms()
        Dim tmpPcaUpdTms As New clsPcaTms()
        Dim tmpPcaTmsCount As New clsPcaTms()
        Dim tmpPcaTmsList As New List(Of clsPcaTmsD)
        Dim tmpDt As New DataTable
        Dim tmpUpdFlg As Boolean = False

        Try
            SqlServer.GetResult(tmpDt, GetMstCustomerSql)

            For Each tmpDr As DataRow In tmpDt.Rows
                Dim tmpPcaTmsD As New clsPcaTmsD()
                Dim tmpPcaTmsSearchCondition As New clsPcaTmsD()

                tmpPcaTmsSearchCondition.得意先コード = tmpDr.Item("TokuiCD").ToString
                tmpPcaTmsList = tmpPcaTmsCount.GetData(tmpPcaTmsSearchCondition)

                tmpPcaTmsD.得意先コード = tmpDr.Item("TokuiCD").ToString
                tmpPcaTmsD.得意先名1 = tmpDr.Item("TokuiNM1").ToString
                tmpPcaTmsD.得意先名2 = tmpDr.Item("TokuiNM2").ToString
                tmpPcaTmsD.先方担当者名 = tmpDr.Item("SenpoTanto").ToString
                tmpPcaTmsD.請求先コード = tmpDr.Item("SeikyuCD").ToString
                tmpPcaTmsD.実績管理 = tmpDr.Item("TJituKanri").ToString
                tmpPcaTmsD.住所1 = tmpDr.Item("Add1").ToString
                tmpPcaTmsD.住所2 = tmpDr.Item("Add1").ToString
                tmpPcaTmsD.郵便番号 = tmpDr.Item("ZipCD").ToString
                tmpPcaTmsD.相手先TEL = tmpDr.Item("TelNo").ToString
                tmpPcaTmsD.相手先FAX = tmpDr.Item("FaxNo").ToString
                tmpPcaTmsD.得意先区分1 = tmpDr.Item("TokuiKBN1").ToString
                tmpPcaTmsD.得意先区分2 = tmpDr.Item("TokuiKBN2").ToString
                tmpPcaTmsD.得意先区分3 = tmpDr.Item("TokuiKBN3").ToString
                tmpPcaTmsD.適用売価No = tmpDr.Item("TekiyoUriNo").ToString
                tmpPcaTmsD.掛率 = tmpDr.Item("TekiyoKakeritu").ToString
                tmpPcaTmsD.税換算 = tmpDr.Item("TekiyoZeikan").ToString
                'tmpPcaTmsD.主担当者 = tmpDr.Item("SyuTantoCD").ToString
                tmpPcaTmsD.請求締日 = tmpDr.Item("SeikyuSimebi").ToString
                tmpPcaTmsD.消費税端数 = tmpDr.Item("ShohizeiHasu").ToString
                tmpPcaTmsD.消費税通知 = tmpDr.Item("ShohizeiTuti").ToString
                tmpPcaTmsD.回収種別1 = tmpDr.Item("Kaisyu1").ToString
                tmpPcaTmsD.回収種別2 = tmpDr.Item("Kaisyu2").ToString
                tmpPcaTmsD.回収日 = tmpDr.Item("KaisyuYoteibi").ToString
                tmpPcaTmsD.回収方法 = tmpDr.Item("KaisyuHou").ToString
                tmpPcaTmsD.与信限度額 = tmpDr.Item("YosinGendo").ToString
                tmpPcaTmsD.繰越残高_売掛金残高 = tmpDr.Item("KurikosiZan").ToString
                'tmpPcaTmsD.納品書用紙 = tmpDr.Item("NohinYosi").ToString
                tmpPcaTmsD.納品書社名 = tmpDr.Item("NohinShamei").ToString
                'tmpPcaTmsD.請求書用紙 = tmpDr.Item("SeikyuYosi").ToString
                tmpPcaTmsD.請求書社名 = tmpDr.Item("SeikyuShamei").ToString
                tmpPcaTmsD.社店コード = tmpDr.Item("ShatenCD").ToString
                tmpPcaTmsD.伝票区分 = tmpDr.Item("TokuiKubun").ToString

                If tmpPcaTmsList.Count = 0 Then
                    tmpPcaInsTms.AddDetail(tmpPcaTmsD)
                Else
                    tmpPcaTmsD.更新Date = tmpPcaTmsList(0).更新Date
                    tmpPcaTmsD.残高更新Date = tmpPcaTmsList(0).残高更新Date
                    tmpPcaUpdTms.AddDetail(tmpPcaTmsD)
                End If

            Next


            '商品更新
            tmpPcaUpdTms.Update()
            '商品追加
            tmpPcaInsTms.Create()
        Catch ex As Exception
            ComWriteErrLog(ex)
            Throw New Exception(ex.Message)
        Finally
            tmpPcaTmsList.Clear()
            tmpPcaUpdTms.Dispose()
            tmpPcaInsTms.Dispose()

        End Try
    End Sub

    Private Function DuplicateCheck(PR_MACHINE_NO As String, PR_PROCESS_DATE As String, PR_PROCESS_TIME As String, PR_TENANT_CODE As String, PR_GARBAGE_CODE As String, PR_MEASURING_WEIGHT As String)
        Dim Result As Boolean
        Dim sql As String = String.Empty
        sql = GetDuplicateSelectSql(PR_MACHINE_NO, PR_PROCESS_DATE, PR_PROCESS_TIME, PR_TENANT_CODE, PR_GARBAGE_CODE, PR_MEASURING_WEIGHT)
        Try
            With tmpDb
                SqlServer.GetResult(tmpDt, sql)
                If tmpDt.Rows(0).Item("ROWC") = 0 Then
                    Result = False
                Else
                    Result = True
                End If
            End With
        Catch ex As Exception
            Call ComWriteErrLog([GetType]().Name,
                                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw New Exception(ex.Message)
        Finally
            tmpDt.Dispose()
        End Try
        Return Result
    End Function
    Private Function GetDuplicateSelectSql(PR_MACHINE_NO As String, PR_PROCESS_DATE As String, PR_PROCESS_TIME As String, PR_TENANT_CODE As String, PR_GARBAGE_CODE As String, PR_MEASURING_WEIGHT As String)
        Dim sql As String = String.Empty

        sql &= " Select"
        sql &= "     COUNT(*) As ROWC"
        sql &= " FROM"
        sql &= "     RESULTS"
        sql &= " WHERE"
        sql &= "     MACHINE_NO = '" & PR_MACHINE_NO & "' "
        sql &= " AND PROCESS_DATE = '" & PR_PROCESS_DATE & "' "
        sql &= " AND PROCESS_TIME = '" & PR_PROCESS_TIME & "' "
        sql &= " AND TENANT_CODE = '" & PR_TENANT_CODE & "' "
        sql &= " AND GARBAGE_CODE = '" & PR_GARBAGE_CODE & "' "
        sql &= " AND MEASURING_WEIGHT = '" & PR_MEASURING_WEIGHT & "' "
        Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function
    Function GetInsertSql(columnNames() As String, dr As DataRow) As String
        Dim sql As String = "INSERT INTO TRN_Results ("
        Dim values As String = "VALUES ("

        ' カラム名と値をセット
        For i As Integer = 0 To columnNames.Length - 1
            sql &= columnNames(i)
            values &= "'" & dr(i).ToString().Replace("'", "''") & "'"

            If i < columnNames.Length - 1 Then
                sql &= ", "
                values &= ", "
            End If
        Next

        sql &= ") " & values & ")"
        Call WriteExecuteLog("Download_USB", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function

  Private Sub CreateManufacturerMasterCSV(ScaleNumber As String, UsbPath As String)
    PathName = TenantFileName
    TableName = "MST_Manufacturer"
    DefText = "製造者コード:40051,製造者名:40052"
    CreateCsv(PathName, TableName, DefText, ScaleNumber, UsbPath)
  End Sub
  Private Sub CreateCsv(PathName As String, TableName As String, DefText As String, ScaleNumber As String, UsbPath As String)
        Dim CsvPath As String
        Dim DefPath As String
        Dim isWriteHeader As Boolean = True
        Dim sql As String = String.Empty
        Dim OutputDb As New DataTable
        Dim OutputDt As New DataTable

        CsvPath = UsbPath & "\" & PathName & ScaleNumber & ".CSV"
        DefPath = UsbPath & "\" & PathName & ScaleNumber & ".DEF"
        'CSVファイル出力時に使うEncoding
        '「Shift_JIS」を使用
        Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        '書き込むファイルを開く
        Dim wrCsv As New System.IO.StreamWriter(CsvPath, False, encoding)
        Dim wrDef As New System.IO.StreamWriter(DefPath, False, encoding)
        wrDef.Write(DefText)
        wrDef.Close()

        Select Case TableName
            Case "MST_Item"
                sql = GetItemMasterSelectSql()
            Case "MST_Manufacturer"
                sql = GetManufacturerMasterSelectSql()
        End Select

        Try
            With OutputDb
                SqlServer.GetResult(OutputDt, sql)
                If OutputDt.Rows.Count = 0 Then
                    Select Case TableName
                        Case "GarbageTypeMaster"
                            MessageBox.Show("品目マスタのデータがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Case "TenantMaster"
                            MessageBox.Show("部署マスタのデータがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                    Select Case TableName
                        Case "MST_Item"
                            sql = GetItemMasterSelectSql()
                        Case "MST_Manufacturer"
                            sql = GetManufacturerMasterSelectSql()
                    End Select

                End If
            End With
        Catch ex As Exception
            Call ComWriteErrLog([GetType]().Name,
                                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw New Exception(ex.Message)
        Finally
            OutputDt.Dispose()
        End Try
    End Sub
    Private Function EncloseDoubleQuotes(field As String) As String
        Return "" & field & ""
    End Function
    Private Function GetItemMasterSelectSql() As String
        Dim sql As String = String.Empty
        sql &= " SELECT "
        sql &= "     call_code, "
        sql &= "     item_no, "
        sql &= "     unit_weight, "
        sql &= "     unit_weight_unit, "
        sql &= "     safety_factor, "
        sql &= "     target_qty, "
        sql &= "     packing, "
        sql &= "     packing_unit, "
        sql &= "     upper_limit, "
        sql &= "     standard_value, "
        sql &= "     lower_limit, "
        sql &= "     subtotal_target_qty, "
        sql &= "     subtotal_target_cnt, "
        sql &= "     item_name, "
        sql &= "     upper_unit_weight, "
        sql &= "     upper_unit_weight_unit, "
        sql &= "     lower_unit_weight, "
        sql &= "     lower_unit_weight_unit, "
        sql &= "     delete_flg, "
        sql &= "     create_date, "
        sql &= "     update_date "
        sql &= " FROM "
        sql &= "     MST_Item "
        sql &= " WHERE "
        sql &= "     delete_flg = 0 "
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

    Private Function GetMstItemSql() As String
        Dim sql As String = String.Empty
        sql &= " SELECT"
        sql &= "     ShohinCD "
        sql &= "     ,ShohinNM "
        sql &= "     ,ShohinKana "
        sql &= "     ,SysKBN "
        sql &= "     ,SMstKBN "
        sql &= "     ,ZaiKanri "
        sql &= "     ,JituKanri "
        sql &= "     ,Irisu "
        sql &= "     ,Tani "
        sql &= "     ,Iro "
        sql &= "     ,Size "
        sql &= "     ,ShohinKBN1 "
        sql &= "     ,ShohinKBN2 "
        sql &= "     ,ShohinKBN3 "
        sql &= "     ,ZeiKBN "
        sql &= "     ,ZeikomiKBN "
        sql &= "     ,SKetaT "
        sql &= "     ,SKetaS "
        sql &= "     ,HyojunKakaku "
        sql &= "     ,Genka "
        sql &= "     ,Baika1 "
        sql &= "     ,Baika2 "
        sql &= "     ,Baika3 "
        sql &= "     ,Baika4 "
        sql &= "     ,Baika5 "
        sql &= "     ,SokoCD "
        sql &= "     ,SSiireCD "
        sql &= "     ,ZaiTanka "
        sql &= "     ,SiireTanka "
        sql &= "     ,JANCD "
        sql &= "     ,ShoKubun "
        sql &= "     ,TDATE "
        sql &= "     ,KDATE "
        sql &= " FROM"
        sql &= "     MST_SHOHIN"
        Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function

    Private Function GetMstCustomerSql() As String
        Dim sql As String = String.Empty
        sql &= " SELECT"
        sql &= "     TokuiCD "
        sql &= "     ,TokuiNM1 "
        sql &= "     ,TokuiNM2 "
        sql &= "     ,TokuiKana "
        sql &= "     ,SenpoTanto "
        sql &= "     ,TMstKBN "
        sql &= "     ,SeikyuCD "
        sql &= "     ,TJituKanri "
        sql &= "     ,Add1 "
        sql &= "     ,Add2 "
        sql &= "     ,ZipCD "
        sql &= "     ,TelNo "
        sql &= "     ,FaxNo "
        sql &= "     ,TokuiKBN1 "
        sql &= "     ,TokuiKBN2 "
        sql &= "     ,TokuiKBN3 "
        sql &= "     ,TekiyoUriNo "
        sql &= "     ,CONVERT(int,TekiyoKakeritu * 10) AS TekiyoKakeritu "
        sql &= "     ,TekiyoZeikan "
        sql &= "     ,SyuTantoCD "
        sql &= "     ,SeikyuSimebi "
        sql &= "     ,ShohizeiHasu "
        sql &= "     ,ShohizeiTuti "
        sql &= "     ,Kaisyu1 "
        sql &= "     ,Kaisyu2 "
        sql &= "     ,KaisyuKyokai "
        sql &= "     ,KaisyuYoteibi "
        sql &= "     ,KaisyuHou "
        sql &= "     ,YosinGendo "
        sql &= "     ,KurikosiZan "
        sql &= "     ,NohinYosi "
        sql &= "     ,NohinShamei "
        sql &= "     ,SeikyuYosi "
        sql &= "     ,SeikyuShamei "
        sql &= "     ,KantyoKBN "
        sql &= "     ,Keisho "
        sql &= "     ,ShatenCD "
        sql &= "     ,TorihikiCD "
        sql &= "     ,TokuiKubun "
        sql &= "     ,DENP_KBN "
        sql &= " FROM"
        sql &= "     MST_TOKUISAKI"
        Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function
End Class
