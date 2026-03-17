Imports System.IO
Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl


Public Class OutExcel
  Inherits FormBase

  Private Const PRG_TITLE As String = "CSV出力"
  Private ReadOnly ResultCsvPath As String = ReadSettingIniFile("RESULT_CSV_PATH", "VALUE")
  Private beforeValue As String = ""
  Private beforeControl As Control = Nothing

  '得意先コンボボックス
  Private lastCmbMstCustomer As String
  '商品コンボボックス
  Private lastCmbMstItem As String
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



  Private Sub OutExcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      Dim dtNow As DateTime = DateTime.Now
      TxtUKakouDayFrom.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)
      TxtUKakouDayTo.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)

      '得意先コード設定値
      lastCmbMstCustomer = CmbMstCustomer1From.Text
      '得意先コード設定値
      lastCmbMstItem = CmbMstItem1From.Text
      ' コンボボックスの選択肢を設定する関数を呼び出し
      CmbMstCustomerValidating(CmbMstCustomer1From, TxtTokuNameFrom)
      CmbMstItemValidating(CmbMstItem1From, TxtItemNameFrom)
      ' コンボボックスの選択肢を設定する関数を呼び出し
      CmbMstCustomerValidating(CmbMstCustomer1To, TxtTokuNameTo)
      CmbMstItemValidating(CmbMstItem1To, TxtItemNameTo)

      ' DataGridView全体のフォントを変更する場合
      DataGridView1.Font = New Font("Segoe UI", 10, FontStyle.Regular)

      DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False
      DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

      RdoCross.Checked = True

      DispGrid()
      TxtUKakouDayFrom.Focus()

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)
    End Try
  End Sub

  ''' <summary>
  ''' フォームキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Try
    Select Case e.KeyCode
      Case Keys.F1
        ' CSV出力
        ClickOutExcelButton()

      Case Keys.F10
        ' 終了
        Close()
    End Select

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try

  End Sub


  Private Sub CmbMstCustomer1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstCustomer1From.Validating
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstCustomerValidating(CmbMstCustomer1From, TxtTokuNameFrom)
      If beforeValue <> CmbMstCustomer1From.Text Then
        DispGrid()
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try
  End Sub


  Private Sub CmbMstCustomer1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMstCustomer1From.SelectedIndexChanged
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstCustomerValidating(CmbMstCustomer1From, TxtTokuNameFrom)

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try

  End Sub

  Private Sub CmbMstCustomer1To_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstCustomer1To.Validating
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstCustomerValidating(CmbMstCustomer1To, TxtTokuNameTo)
      If beforeValue <> CmbMstCustomer1To.Text Then
        DispGrid()
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try
  End Sub


  Private Sub CmbMstCustomer1To_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMstCustomer1To.SelectedIndexChanged
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstCustomerValidating(CmbMstCustomer1To, TxtTokuNameTo)

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try

  End Sub


  Private Sub CmbMstItem1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstItem1From.Validating
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstItemValidating(CmbMstItem1From, TxtItemNameFrom)
      If beforeValue <> CmbMstItem1From.Text Then
        DispGrid()
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try
  End Sub
  Private Sub CmbMstItem1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMstItem1From.SelectedIndexChanged, CmbMstItem1To.SelectedIndexChanged
    Try
        ' コンボボックスの選択肢を設定する関数を呼び出し
        CmbMstItemValidating(CmbMstItem1From, TxtItemNameFrom)

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try

    End Sub

  Private Sub CmbMstItem1To_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstItem1To.Validating
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstItemValidating(CmbMstItem1To, TxtItemNameTo)
      If beforeValue <> CmbMstItem1To.Text Then
        DispGrid()
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try
  End Sub
  Private Sub CmbMstItem1To_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMstItem1To.SelectedIndexChanged
    Try
    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstItemValidating(CmbMstItem1To, TxtItemNameTo)

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try

  End Sub
  Private Sub DateTimeFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUKakouDayFrom.KeyPress
    If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ControlChars.Back Or e.KeyChar = "/"c) Then
      e.Handled = True
    End If
  End Sub

  Private Sub DateTimeFrom_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtUKakouDayFrom.Validating
    Try
    If ActiveControl.Name <> "DateTimeFrom" And ActiveControl.Name <> "CloseButton" Then

      Dim inputText As String = TxtUKakouDayFrom.Text.Replace("/", "").Trim()

      If DateTypeCheck(inputText) Then
        TxtUKakouDayFrom.Text = DateTxt2DateTxt(inputText)
      Else
        MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        TxtUKakouDayFrom.SelectAll()
        e.Cancel = True
        Exit Sub
      End If

        If beforeValue <> TxtUKakouDayFrom.Text Then
          DispGrid()
        End If
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try


  End Sub

  Private Sub TxtUKakouDayTo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtUKakouDayTo.Validating
    Try
      If ActiveControl.Name <> "DateTimeTo" And ActiveControl.Name <> "CloseButton" Then

        Dim inputText As String = TxtUKakouDayTo.Text.Replace("/", "").Trim()

        If DateTypeCheck(inputText) Then
          TxtUKakouDayTo.Text = DateTxt2DateTxt(inputText)
        Else
          MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          TxtUKakouDayTo.SelectAll()
          e.Cancel = True
          Exit Sub
        End If

        If beforeValue <> TxtUKakouDayTo.Text Then
          DispGrid()
        End If
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try


  End Sub

  Private Sub BtnOutExcel_Click(sender As Object, e As EventArgs) Handles BtnOutExcel.Click
    Try
      ClickOutExcelButton()

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try
  End Sub

  Private Sub ClickOutExcelButton()
    Try
    Dim FileDateTime As String = DateTime.Now.ToString("yyyyMMddHHmmss")
    Dim fileName As String = ResultCsvPath & "Results_" & FileDateTime & ".CSV"
    Dim tmpWhereList As New Dictionary(Of String, String)

    Select Case True
      Case RdoCross.Checked
        ExportToCSV(DataGridView1, fileName, ResultCsvPath)

        'Dim macroFilePath As String = "C:\AUTOPRT\集計マクロ.xlsm"
        'Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(macroFilePath)
      Case RdoA4.Checked
          'Dim ClsPrintingProcess As New ClsPrintingProcess.ClsPrintingProcess()

          'tmpWhereList.Add("UketukeDay >=", Me.TxtUKakouDayFrom.Text)
          'tmpWhereList.Add("UketukeDay <", DateTime.Parse(Me.TxtUKakouDayFrom.Text).AddDays(1).ToString("yyyy/MM/dd"))

          'If Not String.IsNullOrWhiteSpace(CmbMstCustomer1From.Text) Then
          '  tmpWhereList.Add("TokuiCd", CmbMstCustomer1From.Text)
          'End If
          'If Not String.IsNullOrWhiteSpace(CmbMstItem1From.Text) Then
          '  tmpWhereList.Add("ShohinCD", CmbMstItem1From.Text)
          'End If

          'ClsPrintingProcess.PrintProcess(ClsCommonGlobalData.PRINT_NON_PREVIEW, "WK_LAYOUT", "R_LAYOUT", tmpWhereList)


      End Select

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_WARNING)

    End Try


  End Sub


  Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
    Me.Close()
  End Sub

  ''' <summary>
  ''' 得意先が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstCustomer"></param>
  ''' <param name="prmTxtLabelCustomer"></param>
  Private Function CmbMstCustomerValidating(prmCmbMstCustomer As ComboBox, prmTxtLabelCustomer As TextBox)
    Dim rtn As Boolean = False
    If lastCmbMstCustomer Is Nothing Then
      Return rtn
      Exit Function
    End If

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
    Dim tmpDb As New ClsSqlServer

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
        Call ComWriteErrLog(ex)   ' Error出力（＋画面表示）
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

  ''' <summary>
  ''' 得意先が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstItem"></param>
  ''' <param name="prmTxtLabelItem"></param>
  Private Function CmbMstItemValidating(prmCmbMstItem As ComboBox, prmTxtLabelItem As TextBox)
    Dim rtn As Boolean = False
    If lastCmbMstItem Is Nothing Then
      Return rtn
      Exit Function
    End If
    If (lastCmbMstItem.Equals(prmCmbMstItem.Text) = False) Then
      lastCmbMstItem = prmCmbMstItem.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstItem.Text) Then
      ' 得意先コード
      prmCmbMstItem.Text = String.Empty
      prmTxtLabelItem.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      If (GetSHCode(prmCmbMstItem.Text, tmpDt)) Then
        ' 得意先コード
        prmCmbMstItem.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabelItem.Text = tmpDt.Rows(0)("Name").ToString
      Else
        ComMessageBox("商品が存在しません。" _
                                , PRG_TITLE _
                                , typMsgBox.MSG_WARNING _
                                , typMsgBoxButton.BUTTON_OK)
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
  Private Function GetSHCode(prmCode As String,
                                     ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New ClsSqlServer

    ' 実行
    With tmpDb
      Try

        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetSHCode(prmCode))
        If (prmDt.Rows.Count = 0) Then
          ret = False
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex)   ' Error出力（＋画面表示）
        ret = False
      End Try

    End With

    Return ret


  End Function

  Public Sub DispGrid()
    Try
    Dim tmpDt As New DataTable
    Dim tmpSumDt As New DataTable
    Dim tmpCnt As Integer = 0

    SqlServer.GetResult(tmpDt, SqlGetJisseki)

    If tmpDt.Rows.Count = 0 Then
      DataGridView1.Rows.Clear()

      Exit Sub
    End If

    'DataGridView1.DataSource = tmpDt
    SetColumnAdd()

    For Each tmpRow As DataRow In tmpDt.Rows
      DataGridView1.Rows.Add()

        DataGridView1.Rows(tmpCnt).Cells("伝票番号").Value = tmpRow("伝票番号").ToString
        DataGridView1.Rows(tmpCnt).Cells("行番号").Value = tmpRow("行番号").ToString
        DataGridView1.Rows(tmpCnt).Cells("納品日").Value = tmpRow("納品日").ToString
        DataGridView1.Rows(tmpCnt).Cells("得意先コード").Value = tmpRow("得意先コード").ToString
        DataGridView1.Rows(tmpCnt).Cells("得意先名").Value = tmpRow("得意先名").ToString
        DataGridView1.Rows(tmpCnt).Cells("直送先コード").Value = tmpRow("直送先コード").ToString
        DataGridView1.Rows(tmpCnt).Cells("直送先名").Value = tmpRow("直送先名").ToString
        DataGridView1.Rows(tmpCnt).Cells("商品コード").Value = tmpRow("商品コード").ToString
        DataGridView1.Rows(tmpCnt).Cells("商品名").Value = tmpRow("商品名").ToString
        DataGridView1.Rows(tmpCnt).Cells("尾数").Value = tmpRow("尾数").ToString 'なし
        DataGridView1.Rows(tmpCnt).Cells("重量").Value = tmpRow("重量").ToString 'なし
        DataGridView1.Rows(tmpCnt).Cells("単位").Value = tmpRow("単位").ToString
        DataGridView1.Rows(tmpCnt).Cells("単価").Value = tmpRow("単価").ToString
        DataGridView1.Rows(tmpCnt).Cells("金額").Value = tmpRow("金額").ToString
        DataGridView1.Rows(tmpCnt).Cells("原産地名").Value = tmpRow("原産地名").ToString
        DataGridView1.Rows(tmpCnt).Cells("生簀No").Value = tmpRow("生簀No").ToString
        'DataGridView1.Rows(tmpCnt).Cells("加工日").Value = tmpRow("加工日").ToString
        'DataGridView1.Rows(tmpCnt).Cells("請求日").Value = tmpRow("請求日").ToString
        'DataGridView1.Rows(tmpCnt).Cells("先方担当者名").Value = tmpRow("先方担当者名").ToString
        'DataGridView1.Rows(tmpCnt).Cells("部門コード").Value = tmpRow("部門コード").ToString
        'DataGridView1.Rows(tmpCnt).Cells("担当者コード").Value = tmpRow("担当コード").ToString
        'DataGridView1.Rows(tmpCnt).Cells("摘要コード").Value = tmpRow("摘要コード").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("摘要名").Value = tmpRow("摘要名").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("分類コード").Value = tmpRow("分類コード").ToString
        'DataGridView1.Rows(tmpCnt).Cells("区").Value = tmpRow("区").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("入数").Value = tmpRow("入数").ToString
        'DataGridView1.Rows(tmpCnt).Cells("定貫フラグ").Value = tmpRow("箱数").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("売上金額").Value = tmpRow("金額").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("原単価").Value = tmpRow("原単価").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("原価金額").Value = tmpRow("原価金額").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("粗利益").Value = tmpRow("粗利益").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("外税額").Value = tmpRow("外税額").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("内税額").Value = tmpRow("内税額").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("税区分").Value = tmpRow("税区分").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("個体識別番号３").Value = tmpRow("個体識別番号３").ToString
        'DataGridView1.Rows(tmpCnt).Cells("標準価格").Value = tmpRow("標準価格").ToString
        'DataGridView1.Rows(tmpCnt).Cells("同時入荷区分").Value = tmpRow("同時入荷区分").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("売単価").Value = tmpRow("売単価").ToString
        'DataGridView1.Rows(tmpCnt).Cells("売価金額").Value = tmpRow("売価金額").ToString 'なし
        'DataGridView1.Rows(tmpCnt).Cells("規格・型番").Value = tmpRow("規格・型番").ToString 'なし
        tmpCnt += 1

      Next

    For Each col As DataGridViewColumn In DataGridView1.Columns
      col.Width = 125
    Next
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try




  End Sub

  Private Sub DataClear()
    Try
    Dim dtNow As DateTime = DateTime.Now
    TxtUKakouDayFrom.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)

    DataGridView1.Rows.Clear()

    Me.CmbMstCustomer1From.SelectedIndex = 0
    CmbMstCustomerValidating(CmbMstCustomer1From, TxtTokuNameFrom)
    Me.TxtUKakouDayFrom.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)

    Catch ex As Exception
      Throw New Exception(ex.Message)

    End Try
  End Sub

  Private Sub SetColumnAdd()
    DataGridView1.Columns.Clear()
    DataGridView1.Columns.Add(SetColumn("伝票番号"))
    DataGridView1.Columns.Add(SetColumn("行番号"))
    DataGridView1.Columns.Add(SetColumn("納品日"))
    DataGridView1.Columns.Add(SetColumn("得意先コード"))
    DataGridView1.Columns.Add(SetColumn("得意先名"))
    DataGridView1.Columns.Add(SetColumn("直送先コード"))
    DataGridView1.Columns.Add(SetColumn("直送先名"))
    DataGridView1.Columns.Add(SetColumn("商品コード"))
    DataGridView1.Columns.Add(SetColumn("商品名"))
    DataGridView1.Columns.Add(SetColumn("尾数"))
    DataGridView1.Columns.Add(SetColumn("重量"))
    DataGridView1.Columns.Add(SetColumn("単位"))
    DataGridView1.Columns.Add(SetColumn("単価"))
    DataGridView1.Columns.Add(SetColumn("金額"))
    DataGridView1.Columns.Add(SetColumn("原産地名"))
    DataGridView1.Columns.Add(SetColumn("生簀No"))
    'DataGridView1.Columns.Add(SetColumn("加工日"))
    'DataGridView1.Columns.Add(SetColumn("請求日"))
    'DataGridView1.Columns.Add(SetColumn("先方担当者名"))
    'DataGridView1.Columns.Add(SetColumn("部門コード"))
    'DataGridView1.Columns.Add(SetColumn("担当者コード"))
    'DataGridView1.Columns.Add(SetColumn("摘要コード"))
    'DataGridView1.Columns.Add(SetColumn("摘要名"))
    'DataGridView1.Columns.Add(SetColumn("分類コード"))
    'DataGridView1.Columns.Add(SetColumn("区"))
    'DataGridView1.Columns.Add(SetColumn("入数"))
    'DataGridView1.Columns.Add(SetColumn("定貫フラグ"))
    'DataGridView1.Columns.Add(SetColumn("売上金額"))
    'DataGridView1.Columns.Add(SetColumn("原単価"))
    'DataGridView1.Columns.Add(SetColumn("原価金額"))
    'DataGridView1.Columns.Add(SetColumn("粗利益"))
    'DataGridView1.Columns.Add(SetColumn("外税額"))
    'DataGridView1.Columns.Add(SetColumn("内税額"))
    'DataGridView1.Columns.Add(SetColumn("税区分"))
    'DataGridView1.Columns.Add(SetColumn("個体識別番号３"))
    'DataGridView1.Columns.Add(SetColumn("標準価格"))
    'DataGridView1.Columns.Add(SetColumn("同時入荷区分"))
    'DataGridView1.Columns.Add(SetColumn("売単価"))
    'DataGridView1.Columns.Add(SetColumn("売価金額"))
    'DataGridView1.Columns.Add(SetColumn("規格・型番"))
  End Sub

  Private Function SetColumn(prmColumnName As String) As DataGridViewTextBoxColumn
    Try
    Dim textColumn As New DataGridViewTextBoxColumn()
    textColumn.DataPropertyName = prmColumnName
    '名前とヘッダーを設定する
    textColumn.Name = prmColumnName
    textColumn.HeaderText = prmColumnName
      Return textColumn

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Function

  Private Sub ExportToCSV(dgv As DataGridView, fileName As String, filePath As String)
    Try
      If dgv.Rows.Count = 0 Then
        ComMessageBox("データがありません。", PRG_TITLE, typMsgBox.MSG_WARNING)
        Exit Sub
      End If

    ' ファイルを上書きして書き込みモードで開く
    Using writer As New StreamWriter(fileName, False, System.Text.Encoding.UTF8)
      ' ヘッダーを書き込む
      For Each column As DataGridViewColumn In dgv.Columns
        writer.Write(column.HeaderText & ",")
      Next
      writer.WriteLine()

      ' データを書き込む
      Console.WriteLine(dgv.Columns.Count)
      For Each row As DataGridViewRow In dgv.Rows
        For Each cell As DataGridViewCell In row.Cells
          ' セルの値をCSV形式で書き込む
          writer.Write(cell.Value.ToString() & ",")
        Next
        writer.WriteLine()
      Next
    End Using

    MessageBox.Show("CSVファイルが出力されました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)

    Process.Start(filePath)
    Catch ex As Exception
      Throw New Exception(ex.Message)

    End Try

  End Sub


  ''' <summary>
  ''' 商品マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <param name="prmSHCode">得意先コード</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetSHCode(prmSHCode As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT ShohinCD As Code"
    sql &= "    ,   ShohinNM As Name"
    sql &= " FROM MST_SHOHIN "
    sql &= " WHERE 1 = 1 "
    sql &= " AND ShohinCD = '" & prmSHCode & "'"
    sql &= " ORDER BY ShohinCD "

    Return sql

  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetJisseki(Optional prmGyoNo As String = "") As String
    Dim dtFrom As DateTime = DateTime.Parse(Me.TxtUKakouDayFrom.Text)
    Dim dtTo As DateTime = DateTime.Parse(Me.TxtUKakouDayTo.Text)
    Dim sql As String = String.Empty

    sql &= " SELECT	ISNULL(DenNo2,DenNo) 伝票番号 "
    sql &= "	,	GyoNo 行番号 "
    sql &= "	,	NohinDay 納品日 "
    sql &= "	,	TokuiCd 得意先コード "
    sql &= "	,	TokuiNM 得意先名 "
    sql &= "	,	TyokuCD 直送先コード "
    sql &= "	,	TyokuNM 直送先名 "
    sql &= "	,	ShohinCD 商品コード "
    sql &= "	,	ShohinNM 商品名 "
    sql &= "	,	Iro 尾数 "
    sql &= "	,	Suryo 重量 "
    sql &= "	,	Tani 単位 "
    sql &= "	,	Tanka 単価 "
    sql &= "	,	Kingaku 金額 "
    sql &= "	,	Biko 原産地名 "
    sql &= "	,	Memo1 生簀No "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    If Not String.IsNullOrWhiteSpace(dtFrom.ToString) Then
      sql &= " AND NohinDay >= '" & dtFrom.ToString("yyyyMMdd") & "'"
    End If
    If Not String.IsNullOrWhiteSpace(dtTo.ToString) Then
      sql &= " AND NohinDay <= '" & dtTo.ToString("yyyyMMdd") & "'"
    End If
    If Not String.IsNullOrWhiteSpace(CmbMstCustomer1From.Text) Then
      sql &= " AND TokuiCd >= '" & CmbMstCustomer1From.Text & "' "
    End If
    If Not String.IsNullOrWhiteSpace(CmbMstCustomer1To.Text) Then
      sql &= " AND TokuiCd <= '" & CmbMstCustomer1To.Text & "' "
    End If
    If Not String.IsNullOrWhiteSpace(CmbMstItem1From.Text) Then
      sql &= " AND ShohinCD >= '" & CmbMstItem1From.Text & "' "
    End If
    If Not String.IsNullOrWhiteSpace(CmbMstItem1To.Text) Then
      sql &= " AND ShohinCD <= '" & CmbMstItem1To.Text & "' "
    End If
    sql &= " ORDER BY ISNULL(DenNo2,DenNo),ISNULL(GyoNo2,GyoNo) "

    Call WriteExecuteLog("OutExcel", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)

    Return sql

  End Function

  Private Sub Control_Enter(sender As Object, e As EventArgs) Handles CmbMstCustomer1From.Enter, CmbMstCustomer1To.Enter, TxtUKakouDayFrom.Enter, TxtUKakouDayTo.Enter, CmbMstItem1From.Enter, CmbMstItem1To.Enter
    Dim ctrl = DirectCast(sender, Control)
    beforeControl = ctrl

    Select Case True
      Case TypeOf ctrl Is TextBox
        beforeValue = DirectCast(ctrl, TextBox).Text

      Case TypeOf ctrl Is ComboBox
        beforeValue = DirectCast(ctrl, ComboBox).Text

      Case TypeOf ctrl Is CheckBox
        beforeValue = DirectCast(ctrl, CheckBox).Checked.ToString()

      Case TypeOf ctrl Is DateTimePicker
        beforeValue = DirectCast(ctrl, DateTimePicker).Value.ToString()

      Case Else
        beforeValue = ctrl.Text
    End Select
  End Sub
End Class
