Imports System.ComponentModel
Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.clsCodeLengthSetting

Public Class Form_ResultList
  Inherits FormBase

  Private ReadOnly tmpDb As New ClsSqlServer
  Private Const PRG_TITLE As String = "納品書一覧画面"

  Property PrpDenNo As String = String.Empty

  Dim tmpDt As New DataTable

  '修正前件数
  Public tmpBeforeCnt As Integer
  '修正後件数
  Public tmpAfterCnt As Integer
  '得意先コンボボックス
  Private lastCmbMstCustomer As String = ""
  '売上区分コンボボックス
  Private lastCmbMstUriKbn As String = ""
  '伝区コンボボックス
  Private lastCmbMstDenku As String = ""
  '直送先コンボボックス
  Private lastCmbMstChoku As String = ""
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  ' １つ目のプロセスＩＤ
  Private Shared ryoProcesID_01 As System.Diagnostics.Process
  ' 更新済フラグ
  Public FlgUpdate As Boolean = False
  ''' <summary>
  ''' フォーム内のコントロール
  ''' </summary>
  Private originalValues As New Dictionary(Of String, String)
  ''' <summary>
  ''' 実績一覧フォーム
  ''' </summary>
  Private Shadows parentForm As Form

  ''' <summary>
  ''' 親フォームを連携するためのコンストラクタ
  ''' </summary>
  ''' <param name="parent">親フォーム</param>
  ''' <returns></returns>
  Public Sub New(parent As Form)

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()
    '親フォームを取得
    parentForm = parent

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

  End Sub


  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property
  Private Sub ResultList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' フォームの最大化ボタンを無効にする
    MaximizeBox = False

    Me.MainMenuStrip = New MenuStrip()
    MainMenuStrip = Nothing

    ' アセンブリの最終更新日時を取得し、フォームのタイトルに表示するテキストを設定
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = PRG_TITLE & " ( " & updateTime & " ) "

    'DataClear()

    '得意先コード設定値
    lastCmbMstCustomer = CmbMstCustomer1.Text
    '得意先コード設定値
    lastCmbMstUriKbn = CmbMstUriKbn1.Text
    '得意先コード設定値
    lastCmbMstDenku = CmbMstDenku1.Text
    '直送先コード設定値
    lastCmbMstChoku = CmbMstChoku1.Text

    ' データベースアクセスのための一時的なオブジェクトを.作成
    Dim tmpDb As New ClsSqlServer
    Dim tmpDt As New DataTable

    ' フォームのボーダースタイルを固定サイズに設定
    FormBorderStyle = FormBorderStyle.FixedSingle

    ' 行ヘッダーを非表示にする
    DataGridView1.RowHeadersVisible = False

    Dim dtNow As DateTime = DateTime.Now
    'TxtUketsukeDay.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)
    'DateTimeTo.Text = New Date(dtNow.Year, dtNow.Month, 1).AddMonths(1).AddDays(-1)

    ' コンボボックスの選択肢を設定する関数を呼び出し
    CmbMstCustomerValidating(CmbMstCustomer1, TxtTokuName)
    CmbMstDenkuValidating(CmbMstDenku1, TxtDenkuName)
    CmbMstUriKbnValidating(CmbMstUriKbn1, TxtUriKbnName)
    CmbMstTantoValidating(CmbMstTanto1, TxtTanto)
    CmbMstChokuValidating(CmbMstChoku1, TxtChokuName)

    ' マルチ選択を無効にする
    DataGridView1.MultiSelect = False
    'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    ' 選択モードを全カラム選択に設定
    DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    ' DataGridView全体のフォントを変更する場合
    DataGridView1.Font = New Font("Segoe UI", 10, FontStyle.Regular)

    'DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False
    'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    ' データを読み込むための関数を呼び出し
    SelectResults()

    ' コンボボックスの幅を調整する関数を呼び出し
    ChangeComboBoxWidth()

    DispGrid(PrpDenNo)

    ' 各コントロールの初期値を保存
    For Each ctrl In Me.Controls
      If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
        originalValues(ctrl.Name) = ctrl.Text
      End If
    Next

  End Sub

  Private Sub Form_ResultList_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    Me.ActiveControl = Me.TxtNohinDay
  End Sub

  '仮チェック
  Private Function IsModified() As Boolean
    For Each ctrl In Me.Controls
      If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
        Dim currentText = ctrl.Text
        If originalValues.ContainsKey(ctrl.Name) Then
          If originalValues(ctrl.Name) <> currentText Then
            Return True ' 変更あり
          End If
        End If
      End If
    Next
    Return False ' すべて元のまま
  End Function

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
          ' 明細追加
          ClickAddGyoButton()

        Case Keys.F2
          ' 修正
          If DataGridView1.SelectedRows.Count > 0 Then
            SubDisplaySetGyo()
          Else
            ComMessageBox("修正したい行を選択してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
          End If

        Case Keys.F3
          ' 登録
          ClickCreateButton()
        Case Keys.F10
          ' 終了
          ClickCloseButton()
      End Select

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_ERROR)
    End Try

  End Sub


  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    ClickCreateButton()
  End Sub

  Private Sub ClickCreateButton()
    Dim InsertData As New Dictionary(Of String, String)
    Dim tmpJissekiDt As New DataTable
    Dim tmpTokuiDt As New DataTable
    Dim tmpShohinDt As New DataTable
    Dim tmpBeforeNohinDay As String = "0"
    Dim tmpBeforeTokuiCd As String = "0"
    Dim child As New ItemAddForm(Me) ' 親フォームを渡す
    Dim cnt As Integer = 1

    Try
      '入力チェック
      If Not CheckInput() Then
        Exit Sub
      End If


      '最終採番をする。
      For Each tmpRow As DataGridViewRow In DataGridView1.Rows
        tmpRow.Cells("行No").Value = cnt
        cnt += 1

      Next

      tmpAfterCnt = DataGridView1.Rows.Count

      '日本語名ではだめ！修正必要
      '全項目いる！！！
      For Each DataRow As DataGridViewRow In DataGridView1.Rows
        SqlServer.GetResult(tmpTokuiDt, SqlGetTKCode(Me.CmbMstCustomer1.Text))
        SqlServer.GetResult(tmpShohinDt, SqlGetShohinCode(DataRow.Cells("商品コード").Value))
        '伝票番号、GyoNo採番
        If (Me.TxtDenNo.Text = "") Then
          GetDenpyoNo(Me.TxtDenNo.Text, DataRow.Cells("行No").Value, ChkSaiban(DataRow, tmpBeforeNohinDay, tmpBeforeTokuiCd, DataRow.Cells("行No").Value))
        End If

        InsertData("UketukeDay") = Me.TxtNohinDay.Text
        InsertData("NohinDay") = DateFormatChange(typDateFormat.FORMAT_STRING, Me.TxtNohinDay.Text)
        InsertData("Denku") = Me.CmbMstDenku1.Text
        'InsertData("SeikyuDay") = ""
        InsertData("DenNo") = Me.TxtDenNo.Text
        InsertData("GyoNo") = DataRow.Cells("行No").Value
        InsertData("TokuiCD") = Me.CmbMstCustomer1.Text
        InsertData("TokuiNM") = Me.TxtTokuName.Text
        InsertData("TokuiNM2") = tmpTokuiDt.Rows(0).Item("TokuiNM2").ToString
        InsertData("TokuiKN") = tmpTokuiDt.Rows(0).Item("TokuiKN").ToString
        InsertData("TokuiZipCD") = tmpTokuiDt.Rows(0).Item("TokuiZipCD").ToString
        InsertData("TokuiAdd1") = tmpTokuiDt.Rows(0).Item("TokuiAdd1").ToString
        InsertData("TokuiAdd2") = tmpTokuiDt.Rows(0).Item("TokuiAdd2").ToString
        InsertData("TokuiTel") = Me.TxtTokuiTel.Text
        InsertData("TyokuCD") = Me.CmbMstChoku1.Text
        InsertData("TyokuNM") = Me.TxtChokuName.Text
        InsertData("SenpoTantoNM") = tmpTokuiDt.Rows(0).Item("SenpoTantoNm").ToString
        'InsertData("BumonCD") = Me.TxtBumonCd.Text
        'InsertData("UTantoCD") = Me.CmbMstTanto1.Text
        'InsertData("TekiyoCD") = ""
        'InsertData("TekiyoNM") = Me.TxtTekiyo.Text
        'InsertData("BunruiCD") = Me.TxtBunruiCd.Text
        'InsertData("DenKBN") = Me.TxtDenpyoKbn.Text
        InsertData("ShohinCD") = DataRow.Cells("商品コード").Value
        InsertData("ShohinNM") = DataRow.Cells("商品名").Value
        InsertData("ShohinKN") = tmpShohinDt.Rows(0).Item("ShohinKN").ToString
        InsertData("SMstKBN") = tmpShohinDt.Rows(0).Item("SMstKBN").ToString
        InsertData("Ku") = Me.CmbMstUriKbn1.Text
        'InsertData("SokoCD") = ""
        InsertData("Irisu") = "0" 'DataRow.Cells("個数").Value
        InsertData("Hakosu") = DataRow.Cells("定貫タイプ").Value
        InsertData("Suryo") = DataRow.Cells("数量").Value
        InsertData("Tani") = DataRow.Cells("単位").Value
        InsertData("Tanka") = DataRow.Cells("単価").Value
        InsertData("UriageKin") = DataRow.Cells("金額").Value
        InsertData("GenTanka") = "0"
        InsertData("GenkaGaku") = "0"
        InsertData("Arari") = "0"
        InsertData("Sotozei") = ""
        InsertData("Utizei") = ""
        InsertData("ZeiKBN") = tmpShohinDt.Rows(0).Item("ZeiKBN").ToString
        InsertData("ZeikomiKBN") = tmpShohinDt.Rows(0).Item("ZeikomiKBN").ToString
        InsertData("Biko") = DataRow.Cells("原産地").Value
        InsertData("HyojunKKKu") = ""
        InsertData("DojiNyukaKBN") = "0"
        InsertData("UriTanka") = "0"
        InsertData("Baikagaku") = "0"
        InsertData("Kikaku") = "0"
        InsertData("Iro") = DataRow.Cells("尾数").Value 'tmpShohinDt.Rows(0).Item("Iro").ToString
        InsertData("Size") = tmpShohinDt.Rows(0).Item("Size").ToString
        InsertData("JutyuSu") = ""
        InsertData("Kingaku") = DataRow.Cells("金額").Value
        'InsertData("ShukaPRTFLG") = "0"
        InsertData("NohinPRTFLG") = "0"
        'InsertData("PCAFLG") = "0"
        'InsertData("JANCD") = ""
        'InsertData("SakuseiDay") = ""
        'InsertData("OpenFLG") = ""
        'InsertData("HoryuFLG") = ""
        'InsertData("ShatenCD") = ""
        'InsertData("TorihikisakiCD") = ""
        InsertData("Memo1") = DataRow.Cells("生簀ロット番号").Value
        'InsertData("Memo2") = DataRow.Cells("メモ").Value
        InsertData("Hoka1") = Me.TxtHoka1.Text
        InsertData("Hoka2") = Me.TxtHoka2.Text
        InsertData("TokuiKubun") = "0"
        InsertData("ShoKubun") = "0"
        If (Me.TxtDenNo.Text = "") Then
          InsertData("UketukeDay2") = DateTime.Now.ToString("yyyyMMdd") & " " & DateTime.Now.ToString("HH:mm")
        End If



        SqlServer.GetResult(tmpJissekiDt, SqlGetJisseki(DataRow.Cells("行No").Value))
        Dim WhereSql As String = " WHERE ISNULL(DenNO2,DenNO) = '" & Me.TxtDenNo.Text & "' AND ISNULL(GyoNO2,GyoNO) = '" & DataRow.Cells("行No").Value & "'"
        Dim sql As String = If(tmpJissekiDt.Rows.Count = 0, CreateInsSql("TRN_JISSEKI", InsertData), CreateUpdSql("TRN_JISSEKI", InsertData) & WhereSql)

        SqlServer.Execute(sql)


      Next

      '不要行削除
      If tmpBeforeCnt > tmpAfterCnt Then
        SqlServer.Execute(SqlDelDenpyo(tmpAfterCnt))
      End If

      ComMessageBox("更新しました。" _
                        , PRG_TITLE _
                        , typMsgBox.MSG_NORMAL _
                        , typMsgBoxButton.BUTTON_OK)

      parentForm.Enabled = True
      Close()

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Sub

  Private Sub ClickCloseButton()
    If Not FlgUpdate Then
      'ヘッダ情報更新
      FlgUpdate = IsModified()
    End If

    If FlgUpdate Then
      If ComMessageBox("編集中です。登録しますか？", PRG_TITLE, typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_YESNO) = typMsgBoxResult.RESULT_NO Then
        parentForm.Enabled = True
        Close()
      Else
        ClickCreateButton()
      End If
    Else

      Close()
    End If

  End Sub

  Private Sub SetScaleNumberComboBox()
    'Try
    '    ' 計量マスタからデータを取得
    '    Dim scaleData As DataTable = GetDataFromScaleNumber()

    '    ' 計量マスタからデータが取得できなかった場合
    '    If scaleData.Rows.Count = 0 Then
    '        ' エラーメッセージを表示して終了
    '        MessageBox.Show("計量マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Else
    '        ' ComboBoxのアイテムをクリア
    '        CmbBunruiCd.Items.Clear()

    '        ' 空の項目をComboBoxに追加
    '        CmbBunruiCd.Items.Add("")

    '        ' 計量マスタから取得したデータをComboBoxに追加
    '        For Each row As DataRow In scaleData.Rows
    '            CmbBunruiCd.Items.Add(row(0).ToString())
    '        Next
    '    End If
    'Catch ex As Exception
    '    ' エラーログを書き込んで例外をスロー
    '    ComWriteErrLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
    '    Throw New Exception(ex.Message)
    'End Try
  End Sub

  Private Function GetDataFromScaleNumber() As DataTable
    ' データベース接続用の一時的なオブジェクトを作成
    Dim tmpDb As New ClsSqlServer

    ' データを格納するための一時的なデータテーブルを作成
    Dim tmpDt As New DataTable

    Try
      ' SQLクエリを実行して、計量マスタからデータをデータテーブルに取得
      SqlServer.GetResult(tmpDt, GetSelectScaleMaster)

      ' 取得したデータテーブルを返す
      Return tmpDt
    Catch ex As Exception
      ' エラーログを書き込んで例外をスロー
      ComWriteErrLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      ' 一時的なデータテーブルを解放
      tmpDt.Dispose()
    End Try
  End Function


  Private Function GetSelectScaleMaster() As String
    Dim sql As String = String.Empty

    sql &= " SELECT unit_number "
    sql &= " FROM MST_SCALE "
    sql &= " WHERE delete_flg = 0 "
    sql &= " ORDER BY unit_number "

    Call WriteExecuteLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub ChangeComboBoxWidth()
    'AdjustComboBoxWidth(FromTenantCode_ComboBox)
    'AdjustComboBoxWidth(ToTenantCode_ComboBox)
    'AdjustComboBoxWidth(FromShohinCD_ComboBox)
    'AdjustComboBoxWidth(ToShohinCD_ComboBox)
  End Sub
  Private Sub AdjustComboBoxWidth(comboBox As ComboBox)
    Dim maxSize As Integer = 0
    For Each item As String In comboBox.Items
      maxSize = Math.Max(maxSize, TextRenderer.MeasureText(item, comboBox.Font).Width)
    Next
    maxSize += 20

    If comboBox.DropDownWidth < maxSize Then
      comboBox.DropDownWidth = maxSize
    End If
  End Sub

  Public Sub SelectResults()
    Dim tmpDb As New ClsSqlServer
    Dim tmpDt As New DataTable
    Try
      'With tmpDb
      '    SqlServer.GetResult(tmpDt, GetSelectSql)
      '    WriteDetail(tmpDt, ResultDetail)
      '    If tmpDt.Rows.Count = 0 Then
      '        UpdateButton.Enabled = False
      '        DeleteButton.Enabled = False
      '        'If FirstFlag = True Then
      '        '    FirstFlag = False
      '        'Else
      '        '    MessageBox.Show("該当データが存在しません。", "結果", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '        'End If
      '        'RowCount.Text = "0件"
      '    Else
      '        WriteDetail(tmpDt, ResultDetail)
      '        UpdateButton.Enabled = True
      '        DeleteButton.Enabled = True
      '        'RowCount.Text = ResultDetail.Rows.Count.ToString + "件"
      '        'For i As Integer = 0 To ResultDetail.Rows.Count - 1
      '        '    If ResultDetail.Rows(i).Cells(10).Value = "〇" Then
      '        '        ResultDetail.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
      '        '    End If
      '        'Next
      '    End If
      'End With
    Catch ex As Exception
      Call ComWriteErrLog(Me.GetType().Name,
                                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    ClickCloseButton()
  End Sub

  Private Sub Form_ResultList_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    parentForm.Enabled = True
  End Sub

  Private Sub DateTimeTo_KeyPress(sender As Object, e As KeyPressEventArgs)
    If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ControlChars.Back Or e.KeyChar = "/"c) Then
      e.Handled = True
    End If
  End Sub
  Private Sub DateTimeFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNohinDay.KeyPress, TxtNohinDay2.KeyPress, TxtSeikyuDay.KeyPress
    If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ControlChars.Back Or e.KeyChar = "/"c) Then
      e.Handled = True
    End If
  End Sub

  ''' <summary>
  ''' 数値とバックスペースのみ入力可
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtTelBase_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTokuiTel.KeyPress, TxtTyokuTel.KeyPress

    ' 数値とバックスペースのみ入力可
    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "-"c Then
      '押されたキーが 0～9でない場合は、イベントをキャンセルする
      e.Handled = True
    End If

  End Sub



  ''' <summary>
  ''' 数値とバックスペースのみ入力可
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNumericBase_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBunruiCd.KeyPress, TxtDenNo.KeyPress, TxtDenpyoKbn.KeyPress, TxtBumonCd.KeyPress, TxtMeisaiSu.KeyPress, TxtGoukeiKin.KeyPress, TxtBaikaKei.KeyPress

    ' 数値とバックスペースのみ入力可
    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
      '押されたキーが 0～9でない場合は、イベントをキャンセルする
      e.Handled = True
    End If

  End Sub



  Private Sub DateTimeFrom_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtNohinDay.Validating
    If ActiveControl.Name <> "DateTimeFrom" And ActiveControl.Name <> "CloseButton" Then

      Dim inputText As String = TxtNohinDay.Text.Replace("/", "").Trim()

      If String.IsNullOrWhiteSpace(inputText) Then
        Exit Sub
      End If

      If DateTypeCheck(inputText) Then
        TxtNohinDay.Text = DateTxt2DateTxt(inputText)
      Else
        MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        TxtNohinDay.SelectAll()
        e.Cancel = True
      End If
    End If
  End Sub

  Private Sub DateTimeTo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
    If ActiveControl.Name <> "DateTimeTo" And ActiveControl.Name <> "CloseButton" Then
      'Dim inputText As String = DateTimeTo.Text.Replace("/", "").Trim()

      'If DateTypeCheck(inputText) Then
      '    DateTimeTo.Text = DateTxt2DateTxt(inputText)
      'Else
      '    MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '    DateTimeTo.SelectAll()
      '    e.Cancel = True
      'End If
    End If
  End Sub

  ''' <summary>
  ''' テキストボックス検証イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtBox_Validating(sender As Object, e As CancelEventArgs) Handles TxtTokuName.Validating, TxtTekiyo.Validating, TxtHoka1.Validating, TxtHoka2.Validating
    Dim tb As TextBox = CType(sender, TextBox)
    If tb.Text.Contains("'") Then
      tb.Text = tb.Text.Replace("'", "")
    End If

  End Sub


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
        Me.TxtTokuiTel.Text = tmpDt.Rows(0)("TokuiTel").ToString
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
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        ret = False
      End Try

    End With

    Return ret


  End Function

  ''' <summary>
  ''' 売上区分が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstDenku"></param>
  ''' <param name="prmTxtLabelDenku"></param>
  Private Function CmbMstUriKbnValidating(prmCmbMstDenku As ComboBox, prmTxtLabelDenku As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMstUriKbn.Equals(prmCmbMstDenku.Text) = False) Then
      lastCmbMstUriKbn = prmCmbMstDenku.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstDenku.Text) Then
      ' 得意先コード
      prmCmbMstDenku.Text = String.Empty
      prmTxtLabelDenku.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      If (GetMstCode("MST_URI_KBN", prmCmbMstDenku.Text, tmpDt)) Then
        ' 得意先コード
        prmCmbMstDenku.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabelDenku.Text = tmpDt.Rows(0)("Name").ToString
      Else
        ComMessageBox("売上区分が存在しません。" _
                                , PRG_TITLE _
                                , typMsgBox.MSG_WARNING _
                                , typMsgBoxButton.BUTTON_OK)
        rtn = True
      End If
    End If

    Return rtn
  End Function

  ''' <summary>
  ''' 伝区が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstDenku"></param>
  ''' <param name="prmTxtLabelDenku"></param>
  Private Function CmbMstDenkuValidating(prmCmbMstDenku As ComboBox, prmTxtLabelDenku As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMstDenku.Equals(prmCmbMstDenku.Text) = False) Then
      lastCmbMstDenku = prmCmbMstDenku.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstDenku.Text) Then
      ' 得意先コード
      prmCmbMstDenku.Text = String.Empty
      prmTxtLabelDenku.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      If (GetMstCode("MST_DENKU", prmCmbMstDenku.Text, tmpDt)) Then
        ' 得意先コード
        prmCmbMstDenku.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabelDenku.Text = tmpDt.Rows(0)("Name").ToString
      Else
        ComMessageBox("伝区が存在しません。" _
                                , PRG_TITLE _
                                , typMsgBox.MSG_WARNING _
                                , typMsgBoxButton.BUTTON_OK)
        rtn = True
      End If
    End If

    Return rtn
  End Function


  ''' <summary>
  ''' 伝区が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstDenku"></param>
  ''' <param name="prmTxtLabelDenku"></param>
  Private Function CmbMstTantoValidating(prmCmbMstDenku As ComboBox, prmTxtLabelDenku As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMstDenku.Equals(prmCmbMstDenku.Text) = False) Then
      lastCmbMstDenku = prmCmbMstDenku.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstDenku.Text) Then
      ' 得意先コード
      prmCmbMstDenku.Text = String.Empty
      prmTxtLabelDenku.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      If (GetMstCode("MST_TANTO", prmCmbMstDenku.Text, tmpDt)) Then
        ' 得意先コード
        prmCmbMstDenku.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabelDenku.Text = tmpDt.Rows(0)("Name").ToString
      Else
        ComMessageBox("担当者が存在しません。" _
                                , PRG_TITLE _
                                , typMsgBox.MSG_WARNING _
                                , typMsgBoxButton.BUTTON_OK)
        rtn = True
      End If
    End If

    Return rtn
  End Function



  ''' <summary>
  ''' 直送先が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstChoku"></param>
  ''' <param name="prmTxtLabelChoku"></param>
  Private Function CmbMstChokuValidating(prmCmbMstChoku As ComboBox, prmTxtLabelChoku As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMstChoku.Equals(prmCmbMstChoku.Text) = False) Then
      lastCmbMstChoku = prmCmbMstChoku.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstChoku.Text) Then
      ' 直送先コード
      prmCmbMstChoku.Text = String.Empty
      prmTxtLabelChoku.Text = String.Empty
    ElseIf prmCmbMstChoku.Text = "0".PadLeft(TYOKUSO_CODE_LENGTH, "0"c) Then
      ' 直送先コード
      prmCmbMstChoku.Text = String.Empty
      prmTxtLabelChoku.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      If (GetMstCode("MST_CHOKUSO", prmCmbMstChoku.Text, tmpDt)) Then
        ' 得意先コード
        prmCmbMstChoku.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabelChoku.Text = tmpDt.Rows(0)("Name").ToString
      Else
        ComMessageBox("直送先が存在しません。" _
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
  Private Function GetMstCode(prmMstTableName As String, prmCode As String,
                                     ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New ClsSqlServer

    ' 実行
    With tmpDb
      Try

        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetDenkuCode(prmMstTableName, prmCode))
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

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <param name="prmShohinCode">ShohinCD</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetShohinCode(prmShohinCode As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT	ShohinCD As Code"
    sql &= "    ,	ShohinNM As Name"
    sql &= "    ,	ShohinKana As ShohinKN "
    sql &= "    ,	SMstKBN AS SMstKBN "
    sql &= "    ,	Genka AS Genka "
    sql &= "    ,	ZeiKBN AS ZeiKBN "
    sql &= "    ,	ZeikomiKBN AS ZeikomiKBN "
    sql &= "    ,	Iro AS Iro "
    sql &= "    ,	Size AS Size "
    sql &= " FROM MST_SHOHIN "
    sql &= " WHERE 1 = 1 "
    sql &= " AND ShohinCD = '" & prmShohinCode & "'"
    sql &= " ORDER BY ShohinCD "

    Return sql

  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <param name="prmMstTableName">テーブル名</param>
  ''' <param name="prmCode">コード</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetDenkuCode(prmMstTableName As String, prmCode As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT CODE As Code"
    sql &= "    ,   NAME As Name"
    sql &= " FROM  " & prmMstTableName
    sql &= " WHERE 1 = 1 "
    sql &= " AND CODE = '" & prmCode & "'"
    sql &= " ORDER BY CODE "

    Return sql

  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetJisseki(Optional prmGyoNo As String = "") As String

    Dim sql As String = String.Empty
    sql &= " SELECT	ISNULL(DenNo2,DenNo) 伝票番号 "
    sql &= "	,	ISNULL(GyoNo2,GyoNo) GyoNo "
    sql &= "	,	Format(UketukeDay,'yyyy/MM/dd') 納品日 "
    sql &= "	,	NohinDay 納品日 "
    sql &= "	,	SeikyuDay 請求日 "
    sql &= "	,	Ku 売上区分 "
    sql &= "	,	TokuiCd 得意先コード "
    sql &= "	,	TokuiNM 得意先名 "
    sql &= "	,	IsNull(TokuiTel,'') 得意先Tel "
    sql &= "	,	Denku 伝区 "
    sql &= "	,	DenKBN 伝票区分 "
    sql &= "	,	ISNULL(BunruiCD,'') 分類コード "
    sql &= "	,	TyokuCD 直送先コード "
    sql &= "	,	ShohinCD ShohinCD "
    sql &= "	,	ShohinNM ShohinNM "
    sql &= "	,	Iro Iro"
    sql &= "	,	Irisu Irisu"
    sql &= "	,	Tani Tani "
    sql &= "	,	JutyuSu 受注数 "
    sql &= "	,	Hakosu Hakosu "
    sql &= "	,	Suryo Suryo "
    sql &= "	,	Tanka Tanka "
    sql &= "	,	UriageKin Kingaku "
    sql &= "	,	HyojunKKKu 標準価格 "
    sql &= "	,	UriTanka 売Tanka  "
    sql &= "	,	SokoCD 倉庫コード "
    sql &= "	,	Biko Biko "
    sql &= "	,	Memo1 Memo1 "
    sql &= "	,	Memo2 Memo2 "
    sql &= "	,	ISNULL(BumonCD,'') 部門コード"
    sql &= "	,	UTantoCD 担当コード "
    sql &= "	,	ISNULL(TekiyoNM,'') 摘要 "
    sql &= "	,	ISNULL(Hoka1,'') 他１ "
    sql &= "	,	ISNULL(Hoka2,'') 他２ "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    sql &= " AND ISNULL(DenNo2,DenNo) = '" & Me.TxtDenNo.Text & "'"
    If prmGyoNo <> "" Then
      sql &= " AND ISNULL(GyoNo2,GyoNo) = " & prmGyoNo & " "
    End If
    sql &= " ORDER BY ISNULL(DenNo2,DenNo),ISNULL(GyoNo2,GyoNo) "

    Return sql

  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetDenpyoSum() As String

    Dim sql As String = String.Empty
    sql &= " SELECT	Count(ISNULL(GyoNO2,GyoNO)) 明細数 "
    sql &= "	,	SUM(CONVERT(numeric,UriTanka)) 売価合計 "
    sql &= "	,	Sum(CONVERT(numeric,UriageKin)) 合計Kingaku "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    sql &= " AND ISNULL(DenNo2,DenNo) = '" & Me.TxtDenNo.Text & "'"
    sql &= " GROUP BY ISNULL(DenNo2,DenNo) "

    Return sql

  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelDenpyo(prmMaxGyoNo As Integer) As String

    Dim sql As String = String.Empty
    sql &= " DELETE FROM TRN_JISSEKI "
    sql &= " WHERE ISNULL(DenNo2,DenNo) = '" & Me.TxtDenNo.Text & "'"
    sql &= " AND ISNULL(GyoNo2,GyoNo) > " & prmMaxGyoNo

    Return sql

  End Function
  Private Function UpdDenNo(prmDenNo As String) As String
    Dim sql As String = String.Empty
    sql &= " UPDATE TBL_DENNO"
    sql &= " SET  ISNULL(DenNo2,DenNo) = '" & prmDenNo & "'"
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetDenpyoNoSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     ISNULL(DenNo2,DenNo) "
    sql &= " FROM"
    sql &= "     TBL_DENNO "
    Call WriteExecuteLog("Module_Download", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function


  Private Sub BtnAddGyo_Click(sender As Object, e As EventArgs) Handles BtnAddGyo.Click
    ClickAddGyoButton()
  End Sub

  Private Sub ClickAddGyoButton()
    Try
      Dim child As New ItemAddForm(Me) ' 親フォームを渡す
      Me.Enabled = False
      child.Show()
      Me.BeginInvoke(Sub()
                       Me.ActiveControl = Me.TxtNohinDay
                     End Sub)


    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Private Sub BtnGyoEdit_Click(sender As Object, e As EventArgs) Handles BtnGyoEdit.Click
    Try
      If DataGridView1.SelectedRows.Count > 0 Then
        SubDisplaySetGyo()
      Else
        ComMessageBox("修正したい行を選択してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_ERROR)
    End Try
  End Sub


  Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    If e.RowIndex >= 0 Then
      SubDisplaySetGyo()
    End If
  End Sub

  Private Sub SubDisplaySetGyo()
    Try
      Dim child As New ItemAddForm(Me) ' 親フォームを渡す
      child.DispItem(Me.TxtDenNo.Text, DataGridView1.CurrentRow.Cells("行No").Value, DataGridView1.CurrentCell.RowIndex, DataGridView1)
      Me.Enabled = False
      child.Show()
      Me.BeginInvoke(Sub()
                       Me.ActiveControl = Me.TxtNohinDay
                     End Sub)

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Sub

  Private Sub CmbMstCustomer1_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstCustomer1.Validating
    If CmbMstCustomerValidating(CmbMstCustomer1, TxtTokuName) Then
      e.Cancel = True
    End If
  End Sub

  Private Sub CmbMstDenku1_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstDenku1.Validating
    If CmbMstDenkuValidating(CmbMstDenku1, TxtDenkuName) Then
      e.Cancel = True
    End If

  End Sub

  Private Sub CmbMstUriKbn1_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstUriKbn1.Validating
    If CmbMstUriKbnValidating(CmbMstUriKbn1, TxtUriKbnName) Then
      e.Cancel = True
    End If
  End Sub

  Private Sub CmbMstChoku_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstChoku1.Validating
    If CmbMstChokuValidating(CmbMstChoku1, TxtChokuName) Then
      e.Cancel = True
    End If
  End Sub

  Private Sub CmbMstTanto1_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstTanto1.Validating
    If CmbMstTantoValidating(CmbMstTanto1, TxtTanto) Then
      e.Cancel = True
    End If

  End Sub

  Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
    If e.KeyCode = Keys.Delete Then
      If DataGridView1.CurrentRow IsNot Nothing Then
        Dim result As DialogResult = MessageBox.Show("本当にこの行を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
          DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
          'ヘッダ情報更新
          FlgUpdate = True

        End If
      End If
    End If
  End Sub

  Public Sub DispGrid(prmDenNo As String)
    Dim tmpDt As New DataTable
    Dim tmpSumDt As New DataTable
    Dim tmpCnt As Integer = 0
    Me.TxtDenNo.Text = prmDenNo

    SqlServer.GetResult(tmpDt, SqlGetJisseki)
    SqlServer.GetResult(tmpSumDt, SqlGetDenpyoSum)

    DataClear()
    If tmpDt.Rows.Count = 0 Then
      Exit Sub
    End If

    'DataGridView1.DataSource = tmpDt
    If DataGridView1.Columns.Count = 0 Then
      SetColumnAdd()
    End If

    For Each tmpRow As DataRow In tmpDt.Rows
      DataGridView1.Rows.Add()

      DataGridView1.Rows(tmpCnt).Cells("行No").Value = tmpRow("GyoNo").ToString
      DataGridView1.Rows(tmpCnt).Cells("商品コード").Value = tmpRow("ShohinCD").ToString
      DataGridView1.Rows(tmpCnt).Cells("商品名").Value = tmpRow("ShohinNM").ToString
      DataGridView1.Rows(tmpCnt).Cells("尾数").Value = tmpRow("Iro").ToString 'tmpRow("Irisu").ToString
      DataGridView1.Rows(tmpCnt).Cells("単位").Value = tmpRow("Tani").ToString
      DataGridView1.Rows(tmpCnt).Cells("定貫タイプ").Value = tmpRow("Hakosu").ToString
      DataGridView1.Rows(tmpCnt).Cells("数量").Value = tmpRow("Suryo").ToString
      DataGridView1.Rows(tmpCnt).Cells("単価").Value = tmpRow("Tanka").ToString
      DataGridView1.Rows(tmpCnt).Cells("金額").Value = tmpRow("Kingaku").ToString
      DataGridView1.Rows(tmpCnt).Cells("生簀ロット番号").Value = tmpRow("Memo1").ToString
      'DataGridView1.Rows(tmpCnt).Cells("メモ").Value = tmpRow("Memo2").ToString
      DataGridView1.Rows(tmpCnt).Cells("原産地").Value = tmpRow("Biko").ToString
      DataGridView1.Columns("定貫タイプ").Visible = False
      tmpCnt += 1

    Next

    '表示列値設定
    'Me.TxtNohinDay.Text = tmpDt.Rows(0).Item("加工日")
    Me.TxtNohinDay.Text = tmpDt.Rows(0).Item("納品日")
    Me.TxtNohinDay.Text = SetDateText(Me.TxtNohinDay)
    Me.CmbMstCustomer1.Text = tmpDt.Rows(0).Item("得意先コード")
    Me.TxtTokuiTel.Text = tmpDt.Rows(0).Item("得意先Tel")
    Me.TxtTokuName.Text = tmpDt.Rows(0).Item("得意先名")
    Me.TxtBunruiCd.Text = tmpDt.Rows(0).Item("分類コード")
    Me.CmbMstDenku1.Text = tmpDt.Rows(0).Item("伝区")
    CmbMstDenkuValidating(CmbMstDenku1, TxtDenkuName)
    Me.TxtDenpyoKbn.Text = tmpDt.Rows(0).Item("伝票区分")
    Me.TxtBumonCd.Text = tmpDt.Rows(0).Item("部門コード")
    Me.CmbMstTanto1.Text = tmpDt.Rows(0).Item("担当コード")
    CmbMstTantoValidating(CmbMstTanto1, TxtTanto)
    Me.CmbMstUriKbn1.Text = tmpDt.Rows(0).Item("売上区分")
    CmbMstUriKbnValidating(CmbMstUriKbn1, TxtUriKbnName)
    Me.CmbMstChoku1.Text = tmpDt.Rows(0).Item("直送先コード")
    CmbMstChokuValidating(CmbMstChoku1, TxtChokuName)

    Me.TxtTekiyo.Text = tmpDt.Rows(0).Item("摘要")
    Me.TxtHoka1.Text = tmpDt.Rows(0).Item("他１")
    Me.TxtHoka2.Text = tmpDt.Rows(0).Item("他２")

    Me.TxtMeisaiSu.Text = tmpSumDt.Rows(0).Item("明細数")
    Me.TxtBaikaKei.Text = tmpSumDt.Rows(0).Item("売価合計")
    Me.TxtGoukeiKin.Text = tmpSumDt.Rows(0).Item("合計Kingaku")

    tmpBeforeCnt = DataGridView1.Rows.Count
  End Sub

  Private Sub DataClear()

    Dim dtNow As DateTime = DateTime.Now
    TxtNohinDay.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)

    DataGridView1.Rows.Clear()

    Me.CmbMstCustomer1.SelectedIndex = -1
    Me.TxtTokuiTel.Text = ""
    CmbMstCustomerValidating(CmbMstCustomer1, TxtTokuName)
    Me.TxtNohinDay.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)
    Me.TxtSeikyuDay.Text = ""
    Me.TxtNohinDay2.Text = ""
    Me.TxtBunruiCd.Text = ""
    Me.CmbMstUriKbn1.SelectedIndex = -1
    CmbMstUriKbnValidating(CmbMstUriKbn1, TxtUriKbnName)
    Me.CmbMstDenku1.SelectedIndex = -1
    CmbMstDenkuValidating(CmbMstDenku1, TxtDenkuName)
    Me.TxtDenpyoKbn.Text = ""
    Me.TxtBumonCd.Text = ""
    Me.CmbMstTanto1.SelectedIndex = -1
    CmbMstTantoValidating(CmbMstTanto1, TxtTanto)
    Me.CmbMstChoku1.SelectedIndex = -1
    CmbMstChokuValidating(CmbMstChoku1, TxtChokuName)
    Me.TxtTekiyo.Text = ""
    Me.TxtHoka1.Text = ""
    Me.TxtHoka2.Text = ""

    Me.TxtMeisaiSu.Text = ""
    Me.TxtBaikaKei.Text = ""
    Me.TxtGoukeiKin.Text = ""


  End Sub

  Private Sub SetColumnAdd()
    With DataGridView1
      .Rows.Clear()
      .Columns.Add(SetColumn("行No"))
      .Columns.Add(SetColumn("商品コード"))
      .Columns.Add(SetColumn("商品名"))
      .Columns.Add(SetColumn("定貫タイプ"))
      .Columns.Add(SetColumn("数量"))
      .Columns.Add(SetColumn("尾数"))
      .Columns.Add(SetColumn("単位"))
      .Columns.Add(SetColumn("単価"))
      .Columns.Add(SetColumn("金額"))
      .Columns.Add(SetColumn("生簀ロット番号"))
      .Columns.Add(SetColumn("原産地"))
      '.Columns.Add(SetColumn("メモ"))
    End With
  End Sub

  Private Function SetColumn(prmColumnName As String) As DataGridViewTextBoxColumn
    Dim textColumn As New DataGridViewTextBoxColumn()
    textColumn.DataPropertyName = prmColumnName
    '名前とヘッダーを設定する
    textColumn.Name = prmColumnName
    textColumn.HeaderText = prmColumnName

    Return textColumn
  End Function

  Private Sub TxtDenNo_Validating(sender As Object, e As CancelEventArgs) Handles TxtDenNo.Validating

    DispGrid(Me.TxtDenNo.Text)
  End Sub


  Private Function ChkSaiban(prmDr As DataGridViewRow, prmBeforeNohinDay As String, prmTokuiCd As String, prmGyoNo As Integer) As Boolean
    Dim rtn As Boolean = False

    If (prmBeforeNohinDay <> Me.TxtNohinDay2.ToString _
                    OrElse prmTokuiCd <> prmDr.Cells("得意先コード").ToString _
                    OrElse prmGyoNo = 6) Then
      rtn = True
    End If

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
    If prmDenpyoNo > 999999 Then
      prmDenpyoNo = "1".PadLeft(CUSTOMER_CODE_LENGTH, "0"c)
      prmGyoNo = 1
    End If

    '伝票番号テーブル更新
    SqlServer.Execute(UpdDenNo(prmDenpyoNo))

  End Sub

  Public Sub SetDenNoAndUketsukeDayFocus(prmDenNo As String)
    TxtDenNo.Text = prmDenNo
  End Sub

  Public Function SetDataGridView() As DataGridView
    Return DataGridView1
  End Function

  Private Sub TxtTokuiTel_TextChanged(sender As Object, e As EventArgs) Handles TxtTokuiTel.TextChanged
    Dim tb As TextBox = CType(sender, TextBox)
    ' 数字とハイフン以外を除去
    Dim filteredText As String = New String(tb.Text.Where(Function(c) Char.IsDigit(c) OrElse c = "-"c).ToArray())
    tb.Text = filteredText


  End Sub

  ''' <summary>
  ''' 入力チェック
  ''' </summary>
  ''' <returns></returns>
  Private Function CheckInput() As Boolean
    Dim rtn As Boolean = True

    If String.IsNullOrWhiteSpace(TxtNohinDay.Text) Then
      ComMessageBox("納品日を入力してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      TxtNohinDay.Focus()
      rtn = False
      Return rtn
      Exit Function
    End If

    If String.IsNullOrWhiteSpace(CmbMstCustomer1.Text) Then
      ComMessageBox("得意先を選択してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      CmbMstCustomer1.Focus()
      rtn = False
      Return rtn
      Exit Function
    End If


    Return rtn
  End Function

  Private Sub Form_ResultList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
    ' 一度だけ動作するようフラグで管理（複数回呼ばれるため）
    Static done As Boolean
    If Not done Then
      done = True
      Me.BeginInvoke(Sub()
                       Me.ActiveControl = Me.TxtNohinDay
                     End Sub)
    End If
  End Sub

  Private Function SetDateText(prmDateTextBox As TextBox) As String
    Dim year As String = ""
    Dim month As String = ""
    Dim day As String = ""
    If prmDateTextBox.Text.Length = 8 Then

      year = prmDateTextBox.Text.Substring(0, 4)
      month = prmDateTextBox.Text.Substring(4, 2)
      day = prmDateTextBox.Text.Substring(6, 2)
      prmDateTextBox.Text = year & "/" & month & "/" & day
    End If

    Return prmDateTextBox.Text
  End Function

End Class
