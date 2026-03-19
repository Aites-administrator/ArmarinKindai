Imports System.ComponentModel
Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.clsCodeLengthSetting


Public Class NohinPrint
  Inherits FormBase

  Private ReadOnly tmpDb As New ClsSqlServer
  Private Const PRG_TITLE As String = "納品書検索発行"
  Private beforeValue As String = ""
  Private beforeControl As Control = Nothing


  Dim tmpDt As New DataTable

  '初回フラグ
  Private BlnFirst As Boolean = True
  '得意先コンボボックス
  Private lastCmbMstCustomer As String
  '売上区分コンボボックス
  Private lastCmbMstUriKbn As String
  '伝区コンボボックス
  Private lastCmbMstDenku As String
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  ' １つ目のプロセスＩＤ
  Private Shared ryoProcesID_01 As System.Diagnostics.Process

  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property


  Private Sub NohinPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      '再発行タイプ
      Dim ReportType As String = ReadSettingIniFile("REPRINT_TYPE", "VALUE")

      ' フォームの最大化ボタンを無効にする
      MaximizeBox = False

      ' アセンブリの最終更新日時を取得し、フォームのタイトルに表示するテキストを設定
      Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
      Text = PRG_TITLE & " ( " & updateTime & " ) "


      '得意先コード設定値
      lastCmbMstCustomer = CmbMstCustomer1From.Text
      '売り区分コード設定値
      lastCmbMstUriKbn = CmbMstUriKbn1.Text
      '伝区コード設定値
      lastCmbMstDenku = CmbMstDenku1.Text


      ' データベースアクセスのための一時的なオブジェクトを作成
      Dim tmpDb As New ClsSqlServer
      Dim tmpDt As New DataTable

      ' フォームのボーダースタイルを固定サイズに設定
      FormBorderStyle = FormBorderStyle.FixedSingle

      ' 行ヘッダーを非表示にする
      DataGridView1.RowHeadersVisible = False

      Dim dtNow As DateTime = DateTime.Now
      TxtKakouDayFrom.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)
      TxtKakouDayTo.Text = New Date(dtNow.Year, dtNow.Month, dtNow.Day)
      TxtKakouDayFrom.Focus()
      RdoUketsukeDesc.Checked = True
      RdoAll.Checked = True

      DispGrid()

      ' コンボボックスの選択肢を設定する関数を呼び出し
      CmbMstCustomer1From.SelectedIndex = -1
      CmbMstCustomerValidating(CmbMstCustomer1From, TxtTokuNameFrom)
      CmbMstCustomer1To.SelectedIndex = -1
      CmbMstCustomerValidating(CmbMstCustomer1To, TxtTokuNameTo)
      CmbMstDenkuValidating(CmbMstDenku1, TxtDenkuName)
      CmbMstUriKbnValidating(CmbMstUriKbn1, TxtUriKbnName)
      CmbMstTanto1.SelectedIndex = -1
      CmbMstTantoValidating(CmbMstTanto1, TxtTanto)

      ' マルチ選択を無効にする
      DataGridView1.MultiSelect = False

      ' 選択モードを全カラム選択に設定
      DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

      ' DataGridView全体のフォントを変更する場合
      DataGridView1.Font = New Font("Segoe UI", 10, FontStyle.Regular)

      DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False
      DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill


      '納品書発行ボタン制御
      If ReportType = ClsCommonGlobalData.REPORT_TYPE_NONE Then
        BtnPrint.Enabled = False
      Else
        BtnPrint.Enabled = True
      End If

      BlnFirst = False
    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, PRG_TITLE, typMsgBox.MSG_ERROR)

    End Try


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
        If BtnPrint.Enabled Then
          ' 発行
          ClickPrintButton()

        End If

      Case Keys.F2
        ' 修正
        ClickEditButton()
      Case Keys.F3
        ' 削除
        ClickDeleteButton()

      Case Keys.F10
        ' 終了
        Close()
    End Select

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

  Public Sub DispGrid()
    Dim tmpDt As New DataTable
    Dim tmpSumDt As New DataTable
    Dim tmpCnt As Integer = 0

    Try
      SqlServer.GetResult(tmpDt, SqlGetJisseki)

      DataGridView1.DataSource = tmpDt

      If tmpDt.Rows.Count = 0 Then
        Me.TxtMeisaiSu.Text = ""
        Me.TxtGoukeiKin.Text = ""
      End If

      For Each col As DataGridViewColumn In DataGridView1.Columns
        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
      Next

      '不要列削除
      'DataGridView1.Columns("請求日").Visible = False
      DataGridView1.Columns("加工日").Visible = False
      DataGridView1.Columns("伝区").Visible = False
      DataGridView1.Columns("伝票区分").Visible = False
      DataGridView1.Columns("PCA").Visible = False
      DataGridView1.Columns("担当者").Visible = False

      SetColumnLocation(DataGridView1)
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try


  End Sub

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetJisseki() As String
    Dim tmpRdoPrint As Integer = 0

    If RdoPrint.Checked Then
      tmpRdoPrint = 1
    Else
      tmpRdoPrint = 0
    End If


    Dim sql As String = String.Empty
    sql &= " SELECT	Format(UketukeDay,'yyyy/MM/dd') 加工日 "
    sql &= "	,	NohinDay 納品日 "
    sql &= "	,	Isnull(DenNo2,DenNo) 伝票番号 "
    sql &= "	,	TokuiCd 得意先コード "
    sql &= "	,	TokuiNM 得意先名 "
    sql &= "	,	COUNT(GyoNo) 明細数 "
    sql &= "	,	Sum(convert(Integer,UriageKin)) 合計金額  "
    sql &= "	,	UTantoCD 担当者 "
    sql &= "	,	PCAFLG PCA "
    'sql &= "	,	SeikyuDay 請求日  "
    sql &= "	,	Denku 伝区  "
    sql &= "	,	DenKBN 伝票区分  "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    If Not String.IsNullOrWhiteSpace(Me.TxtDenNo.Text) Then
      sql &= " AND Isnull(DenNo2,DenNo) = '" & Me.TxtDenNo.Text & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.TxtKakouDayFrom.Text) Then
      sql &= " AND CONVERT(DATE, NohinDay, 112) >= '" & Me.TxtKakouDayFrom.Text & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.TxtKakouDayTo.Text) Then
      sql &= " AND CONVERT(DATE, NohinDay, 112) <= '" & Me.TxtKakouDayTo.Text & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.CmbMstCustomer1From.Text) Then
      sql &= " AND TokuiCd >= '" & Me.CmbMstCustomer1From.Text & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.CmbMstCustomer1To.Text) Then
      sql &= " AND TokuiCd <= '" & Me.CmbMstCustomer1To.Text & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.CmbMstTanto1.Text) Then
      sql &= " AND UTantoCD = '" & Me.CmbMstTanto1.Text & "'"
    End If
    If Me.RdoAll.Checked = False Then
      sql &= " AND NohinPRTFLG = '" & tmpRdoPrint & "'"
    End If

    sql &= " GROUP BY "
    sql &= "         Format(UketukeDay,'yyyy/MM/dd')  "
    sql &= "     ,    NohinDay "
    'sql &= "     ,    SeikyuDay "
    sql &= "     ,    denku "
    sql &= "     ,    DenKBN "
    sql &= "     ,    Isnull(DenNo2,DenNo) "
    sql &= "     ,    TokuiCD "
    sql &= "     ,    TokuiNM "
    sql &= "     ,    UTantoCD "
    sql &= "     ,    PCAFLG "
    sql &= " ORDER BY "
    If Me.RdoUketsukeAsc.Checked Then
      sql &= " NohinDay, "
    End If

    If Me.RdoUketsukeDesc.Checked Then
      sql &= " NohinDay Desc, "
    End If

    If Me.RdoTokuiAsc.Checked Then
      sql &= " TokuiCD, "
    End If

    If Me.RdoTokuiDesc.Checked Then
      sql &= " TokuiCD DESC, "
    End If
    sql &= " Isnull(DenNo2, DenNo) desc "
    Return sql

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
  Private Function SqlDelDenpyo() As String

    Dim sql As String = String.Empty
    sql &= " DELETE FROM TRN_JISSEKI"
    sql &= " WHERE ISNULL(DenNo2,DenNo) = '" & DataGridView1.CurrentRow.Cells("伝票番号").Value & "'"

    Return sql

  End Function


  Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
    '表示列値設定
    'Me.TxtSeikyuDay.Text = DateFormatChange(typDateFormat.FORMAT_DATE, DataGridView1.Rows(e.RowIndex).Cells("請求日").Value)
    Me.TxtNohinDay.Text = DateFormatChange(typDateFormat.FORMAT_DATE, DataGridView1.Rows(e.RowIndex).Cells("納品日").Value)
    Me.CmbMstDenku1.Text = DataGridView1.Rows(e.RowIndex).Cells("伝区").Value
    CmbMstDenkuValidating(CmbMstDenku1, TxtDenkuName)
    Me.TxtMeisaiSu.Text = DataGridView1.Rows(e.RowIndex).Cells("明細数").Value
    Me.TxtGoukeiKin.Text = DataGridView1.Rows(e.RowIndex).Cells("合計金額").Value

  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
    ClickEditButton()
  End Sub

  Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    If e.RowIndex >= 0 Then
      ClickEditButton()
    End If

  End Sub


  Private Sub ClickEditButton()
    Dim child As New Results.Form_ResultList(Me)
    If DataGridView1.CurrentRow Is Nothing Then
      ComMessageBox("修正する伝票を選択してください。", "確認", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
      Exit Sub
    End If

    child.PrpDenNo = DataGridView1.CurrentRow.Cells("伝票番号").Value
    Me.Enabled = False
    child.Show()
    Me.ActiveControl = Me.TxtKakouDayFrom

    DispGrid()

  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
    Me.Close()
  End Sub

  Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
    ClickPrintButton()

  End Sub

  Private Sub ClickPrintButton()

    Dim ClsPrintingProcess As New ClsPrintingProcess.ClsPrintingProcess()
    Dim tmpRdoPrint As Integer = 0
    Dim tmpWhereList As New Dictionary(Of String, String)
    Dim ReportName As String = String.Empty
    Dim ReportType As String = ReadSettingIniFile("REPRINT_TYPE", "VALUE")
    Dim ReportWkTable As String = "WK_NOHIN"

    Try
      If RdoPrint.Checked Then
        tmpRdoPrint = 1
      Else
        tmpRdoPrint = 0
      End If

      If RdoAll.Checked Then
        tmpRdoPrint = -1
      End If

      tmpWhereList.Add("ISNULL(DenNO2,DenNO) = ", "'" & DataGridView1.CurrentRow.Cells("伝票番号").Value & "'")
      If tmpRdoPrint <> -1 Then
        tmpWhereList.Add("NohinPRTFLG =", "'" & tmpRdoPrint & "'")
      End If

      ReportName = If(ReportType = ClsCommonGlobalData.REPORT_TYPE_SHUKKA, "R_SHUKKA", "R_NOHIN")

      ClsPrintingProcess.PrintProcess(ClsCommonGlobalData.PRINT_PREVIEW, ReportWkTable, ReportName, tmpWhereList)

      '画面更新
      DispGrid()
    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try


  End Sub

  Private Sub CmbMstTanto1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstTanto1.Validating
    If CmbMstTantoValidating(CmbMstTanto1, TxtTanto) Then
      e.Cancel = True
    End If

    If beforeValue <> CmbMstTanto1.Text Then
      DispGrid()
    End If
  End Sub

  Private Sub CmbMstCustomer1From_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstCustomer1From.Validating
    If CmbMstCustomerValidating(CmbMstCustomer1From, TxtTokuNameFrom) Then
      e.Cancel = True
    End If

    If Not (String.IsNullOrWhiteSpace(CmbMstCustomer1From.Text) OrElse String.IsNullOrWhiteSpace(CmbMstCustomer1To.Text)) Then
      If Integer.Parse(CmbMstCustomer1From.Text) > Integer.Parse(CmbMstCustomer1To.Text) Then
        ComMessageBox("終了コードが開始コードより小さくなっています。正しい範囲をご指定ください。", PRG_TITLE, typMsgBox.MSG_WARNING)
        e.Cancel = True
        Exit Sub
      End If
    End If

    If beforeValue <> CmbMstCustomer1From.Text Then
      DispGrid()
    End If
  End Sub

  Private Sub CmbMstCustomer1To_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstCustomer1To.Validating
    If CmbMstCustomerValidating(CmbMstCustomer1To, TxtTokuNameTo) Then
      e.Cancel = True
    End If

    If Not (String.IsNullOrWhiteSpace(CmbMstCustomer1From.Text) OrElse String.IsNullOrWhiteSpace(CmbMstCustomer1To.Text)) Then
      If Integer.Parse(CmbMstCustomer1From.Text) > Integer.Parse(CmbMstCustomer1To.Text) Then
        ComMessageBox("終了コードが開始コードより小さくなっています。正しい範囲をご指定ください。", PRG_TITLE, typMsgBox.MSG_WARNING)
        e.Cancel = True
        Exit Sub

      End If

    End If

    If beforeValue <> CmbMstCustomer1To.Text Then
      DispGrid()
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

  Private Sub ValidatingForDispGrid() Handles RdoAll.CheckedChanged, RdoPrint.CheckedChanged,
                                                RdoNotPrint.CheckedChanged, RdoUketsukeAsc.CheckedChanged, RdoUketsukeDesc.CheckedChanged,
                                                RdoTokuiAsc.CheckedChanged, RdoTokuiDesc.CheckedChanged
    DispGrid()
  End Sub

  Private Sub TxtDenNo_Validating() Handles TxtDenNo.Validating, RdoAll.CheckedChanged, RdoPrint.CheckedChanged,
                                                RdoNotPrint.CheckedChanged, RdoUketsukeAsc.CheckedChanged, RdoUketsukeDesc.CheckedChanged,
                                                RdoTokuiAsc.CheckedChanged, RdoTokuiDesc.CheckedChanged
    If Not String.IsNullOrWhiteSpace(TxtDenNo.Text) Then
      TxtDenNo.Text = TxtDenNo.Text.PadLeft(DENPYO_NUMBER_LENGTH, "0"c)
    End If

    If beforeValue <> TxtDenNo.Text Then
      DispGrid()
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

  Private Sub DateTimeFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKakouDayFrom.KeyPress, TxtNohinDay.KeyPress, TxtSeikyuDay.KeyPress, TxtKakouDayTo.KeyPress
    If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ControlChars.Back Or e.KeyChar = "/"c) Then
      e.Handled = True
    End If
  End Sub

  ''' <summary>
  ''' 数値とバックスペースのみ入力可
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNumericBase_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDenNo.KeyPress, TxtMeisaiSu.KeyPress, TxtGoukeiKin.KeyPress

    ' 数値とバックスペースのみ入力可
    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
      '押されたキーが 0～9でない場合は、イベントをキャンセルする
      e.Handled = True
    End If

  End Sub

  Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
    ClickDeleteButton()
  End Sub

  Private Sub ClickDeleteButton()
    Try
      If DataGridView1.CurrentRow Is Nothing Then
        ComMessageBox("削除する伝票を選択してください。", "確認", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
        Exit Sub
      End If

      If ComMessageBox("伝票を削除しますか？" _
                              , PRG_TITLE _
                              , typMsgBox.MSG_WARNING _
                              , typMsgBoxButton.BUTTON_OKCANCEL) = typMsgBoxResult.RESULT_OK Then

        SqlServer.Execute(SqlDelDenpyo)
        ComMessageBox("伝票を削除しました。" _
                              , PRG_TITLE _
                              , typMsgBox.MSG_WARNING _
                              , typMsgBoxButton.BUTTON_OK)
        DispGrid()
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
    End Try

  End Sub

  Private Sub TxtKakouDayFrom_Validating(sender As Object, e As CancelEventArgs) Handles TxtKakouDayFrom.Validating, TxtKakouDayTo.Validating
    Dim tb As TextBox = CType(sender, TextBox)

    Dim inputText As String = tb.Text.Replace("/", "").Trim()


    If Not String.IsNullOrWhiteSpace(inputText) Then
      If DateTypeCheck(inputText) Then
        tb.Text = DateTxt2DateTxt(inputText)
      Else
        ComMessageBox("正しい日付形式を入力してください。", PRG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error)
        TxtKakouDayFrom.SelectAll()
        e.Cancel = True
        Exit Sub
      End If

      If DateTypeCheck(TxtKakouDayFrom.Text) AndAlso DateTypeCheck(TxtKakouDayTo.Text) Then
        If Date.Parse(TxtKakouDayFrom.Text) > Date.Parse(TxtKakouDayTo.Text) Then
          ComMessageBox("終了日が開始日より前になっています。日付の範囲を見直してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
          e.Cancel = True
          Exit Sub
        End If
      End If

      If String.IsNullOrWhiteSpace(TxtKakouDayFrom.Text) _
        OrElse String.IsNullOrWhiteSpace(TxtKakouDayTo.Text) Then
        Exit Sub
      End If

      Dim dtFrom As Date = DateTime.Parse(TxtKakouDayFrom.Text)
      Dim dtTo As Date = DateTime.Parse(TxtKakouDayTo.Text)
      If dtTo > dtFrom.AddYears(5) Then
        ComMessageBox("日付の範囲は5年未満で設定してください。", PRG_TITLE, typMsgBox.MSG_WARNING)

        e.Cancel = True
        Exit Sub
      End If

    End If

    If beforeValue <> tb.Text Then
      DispGrid()
    End If
  End Sub

  Private Sub TxtNohinDay_Validating(sender As Object, e As CancelEventArgs) Handles TxtNohinDay.Validating
    If BlnFirst Then
      Exit Sub
    End If

    Dim inputText As String = TxtNohinDay.Text.Replace("/", "").Trim()

    If Not String.IsNullOrWhiteSpace(inputText) Then

      If DateTypeCheck(inputText) Then
        TxtNohinDay.Text = DateTxt2DateTxt(inputText)
      Else
        MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        TxtNohinDay.SelectAll()
        e.Cancel = True
        Exit Sub
      End If

    End If

  End Sub



  Private Sub TxtSeikyuDay_Validating(sender As Object, e As CancelEventArgs) Handles TxtSeikyuDay.Validating
    If BlnFirst Then
      Exit Sub
    End If

    Dim inputText As String = TxtSeikyuDay.Text.Replace("/", "").Trim()
    If Not String.IsNullOrWhiteSpace(inputText) Then

      If DateTypeCheck(inputText) Then
        TxtSeikyuDay.Text = DateTxt2DateTxt(inputText)
      Else
        MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        TxtSeikyuDay.SelectAll()
        e.Cancel = True
        Exit Sub
      End If
    End If
  End Sub

  Private Sub NohinPrint_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    DispGrid()
  End Sub

  Private Sub Control_Enter(sender As Object, e As EventArgs) Handles CmbMstCustomer1From.Enter, CmbMstCustomer1To.Enter, CmbMstTanto1.Enter, TxtKakouDayFrom.Enter, TxtKakouDayTo.Enter, TxtDenNo.Enter
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

  Private Sub SetColumnLocation(prmDataGridView1 As DataGridView)

    With DataGridView1
      .Columns("明細数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns("合計金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End With

    With DataGridView1
      .Columns("得意先コード").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("担当者").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End With

  End Sub
End Class
