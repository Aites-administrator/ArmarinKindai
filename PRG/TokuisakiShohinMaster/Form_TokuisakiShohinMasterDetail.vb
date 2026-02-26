Imports System.ComponentModel
Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl

Public Class Form_TokuisakiShohinMasterDetail
  Inherits FormBase

  Public InputMode As Integer
  Public TokuisakiCodeTextValue As String
  Public ItemCodeTextValue As String
  Public ItemNameTextValue As String
  Public ItemNameKanaTextValue As String
  Public IrisuTextValue As String
  Public TaniTextValue As String
  Public HyojunTankaTextValue As String
  Public NouhinTankaTextValue As String
  Public TokuisakiTankaTextValue As String
  Private Const PRG_TITLE As String = "得意先商品マスタ"

  ReadOnly tmpDb As New ClsSqlServer
  ReadOnly tmpDt As New DataTable
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  '得意先コンボボックス
  Private lastCmbMstCustomer As String
  '商品コンボボックス
  Private lastCmbMstItem As String
  ''' <summary>
  ''' 実績一覧フォーム
  ''' </summary>
  Private Shadows parentForm As Form_TokuisakiShohinMasterList

  ''' <summary>
  ''' 親フォームを連携するためのコンストラクタ
  ''' </summary>
  ''' <param name="parent">親フォーム</param>
  ''' <returns></returns>
  Public Sub New(parent As Form_TokuisakiShohinMasterList)

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
  Private Sub Form_ScaleDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False

    '得意先コード設定値
    lastCmbMstCustomer = CmbMstCustomer1.Text
    '得意先コード設定値
    lastCmbMstItem = CmbMstItem1.Text

    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "得意先商品マスタ詳細" & " ( " & updateTime & " ) "
    SetInitialProperty()
    FormBorderStyle = FormBorderStyle.FixedSingle
    CmbMstCustomerValidating(CmbMstCustomer1, TxtTokuName)
    CmbMstItemValidating(CmbMstItem1, TxtItemName)


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
        ' 登録処理
        ClickCreateButton()

      Case Keys.F10
        ' 終了
        MeClose()
    End Select

  End Sub


  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
    ClickCreateButton()
  End Sub

  Private Sub ClickCreateButton()
    If String.IsNullOrWhiteSpace(CmbMstCustomer1.Text) Then
      ComMessageBox("得意先が選択されていません。", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
      Exit Sub
    End If

    If String.IsNullOrWhiteSpace(CmbMstItem1.Text) Then
      ComMessageBox("商品が選択されていません。", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
      Exit Sub
    End If

    Select Case InputMode
      Case 1
        If CheckValue() = False Then
          Exit Sub
        End If
        '新規登録メソッド呼出し
        InsertScaleMaster()
      Case 2
        '更新メソッド呼出し
        UpdateScaleMaster()
    End Select
  End Sub
  Private Sub UnitNumberText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
  Private Sub IpAddressText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtHyojunTanka.KeyPress, TxtIrisu.KeyPress, TxtNouhinTanka.KeyPress, TxtTokuisakiTanka.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back Or e.KeyChar = ".") Then
      e.Handled = True
    End If
  End Sub

  Private Sub TxtItemKana_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtItemKana.KeyPress
    ' 半角カナのASCIIコード範囲
    Dim halfWidthKanaChars As String = "ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ"

    ' 半角カナ以外をブロック
    If Not Char.IsControl(e.KeyChar) AndAlso Not halfWidthKanaChars.Contains(e.KeyChar) Then
      e.Handled = True ' 入力を無効化
    End If
  End Sub
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    MeClose()
  End Sub

  Private Sub MeClose()
    parentForm.Enabled = True
    Close()
  End Sub

  Private Sub SetInitialProperty()
    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        CmbMstCustomer1.Text = ""
        CmbMstItem1.Text = ""
        CmbMstCustomer1.Enabled = True
        CmbMstItem1.Enabled = True

        Me.ActiveControl = CmbMstCustomer1
      Case 2
        CmbMstCustomer1.Enabled = False
        CmbMstItem1.Enabled = False

        CmbMstCustomer1.SelectedValue = TokuisakiCodeTextValue
        CmbMstItem1.Text = ItemCodeTextValue
        TxtItemName.Text = ItemNameTextValue
        TxtItemKana.Text = ItemNameKanaTextValue
        TxtIrisu.Text = IrisuTextValue
        TxtTani.Text = TaniTextValue
        TxtHyojunTanka.Text = HyojunTankaTextValue
        TxtNouhinTanka.Text = NouhinTankaTextValue
        TxtTokuisakiTanka.Text = TokuisakiTankaTextValue

        Me.ActiveControl = TxtItemKana
    End Select
  End Sub
  Function CheckValue()
    Dim CheckResult As Boolean = True
    ''必須項目チェック①
    'If String.IsNullOrEmpty(UnitNumberText.Text) = True Then
    '  MessageBox.Show("号機番号を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '  CheckResult = False
    '  UnitNumberText.Focus()
    'End If
    ''必須項目チェック②
    'If String.IsNullOrEmpty(IpAddressText.Text) = True Then
    '  MessageBox.Show("IPアドレスを入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '  CheckResult = False
    '  IpAddressText.Focus()
    'End If
    ''重複チェック
    'If Form_TokuisakiShohinMasterList.ScaleDetail.Rows.Count > 0 Then
    '  For i As Integer = 0 To Form_TokuisakiShohinMasterList.ScaleDetail.Rows.Count - 1
    '    If UnitNumberText.Text.Equals(Form_TokuisakiShohinMasterList.ScaleDetail.Rows(i).Cells(0).Value) Then
    '      MessageBox.Show("既に登録されている号機番号です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '      CheckResult = False
    '      UnitNumberText.Focus()
    '      Exit For
    '    End If
    '  Next
    'End If
    Return CheckResult
  End Function

  Private Sub InsertScaleMaster()
    Dim sql As String = String.Empty
    Dim tmpDt As New DataTable
    Dim rowSelectionCode As String = String.Empty
    'rowSelectionCode = UnitNumberText.Text
    With tmpDb
      Try
        If GetTkShCode(Me.CmbMstCustomer1.SelectedValue, Me.CmbMstItem1.SelectedValue, tmpDt) Then
          ComMessageBox("既に登録されています。", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
          Exit Sub
        End If
        sql = GetInsertSql()
        Dim confirmation As String
        confirmation = MessageBox.Show("登録します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("登録処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' 一覧画面データ更新
            'Form_TokuisakiShohinMasterList.SelectScaleMaster()
            ' 一覧画面件数更新
            'Form_TokuisakiShohinMasterList.GetRowCount(Form_TokuisakiShohinMasterList.DeletedDisplayCheckBox.Checked)

            MeClose()
          Else
            ' 登録失敗
            MessageBox.Show("得意先商品マスタの登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        Else
          Exit Sub
        End If
      Catch ex As Exception
        Call ComWriteErrLog([GetType]().Name,
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        Throw New Exception(ex.Message)
      End Try
    End With
  End Sub
  Private Function GetInsertSql() As String
    Dim tmpTokuisakiCode As String = CmbMstCustomer1.SelectedValue
    Dim tmpItemCode As String = CmbMstItem1.SelectedValue
    Dim tmpItemName As String = TxtItemName.Text
    Dim tmpItemKana As String = TxtItemKana.Text
    Dim tmpTani As String = TxtTani.Text
    Dim tmpIrisu As String = If(String.IsNullOrWhiteSpace(TxtIrisu.Text), "0", TxtIrisu.Text)
    Dim tmpHyojunTanka As String = If(String.IsNullOrWhiteSpace(TxtHyojunTanka.Text), "0", TxtHyojunTanka.Text)
    Dim tmpNouhinTanka As String = If(String.IsNullOrWhiteSpace(TxtNouhinTanka.Text), "0", TxtNouhinTanka.Text)
    Dim tmpTokuisakiTanka As String = If(String.IsNullOrWhiteSpace(TxtTokuisakiTanka.Text), "0", TxtTokuisakiTanka.Text)

    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())
    sql &= " INSERT INTO MST_TOKUISAKI_SHOHIN("
    sql &= "     TokuiCD"
    sql &= " ,    ShohinCD"
    sql &= " ,    ShohinNM"
    sql &= " ,    ShohinKana"
    sql &= " ,    HyojunKakaku"
    sql &= " ,    SShohinCD"
    sql &= " ,    JANCD"
    sql &= " ,    FormatNo"
    sql &= " ,    Suryo"
    sql &= " ,    Tanka"
    sql &= " ,    Tani"
    sql &= " ,    Baika"
    sql &= " ,    TorokuFLG"
    sql &= " ,    TokuiKubun"
    sql &= " ,    ShoKubun"
    sql &= " ,    Irisu"
    sql &= " ,    JutyuSu"
    sql &= " ,    ChokusoCD"
    sql &= " ,    SMstKBN"
    sql &= " ,    Genka"
    sql &= " ,    SokoCD"
    sql &= " ,    TDATE"
    sql &= " ,    KDATE"
    sql &= " )"
    sql &= " VALUES("
    sql &= "     '" & tmpTokuisakiCode & "'"
    sql &= " ,    '" & tmpItemCode & "'"
    sql &= " ,    '" & tmpItemName & "'"
    sql &= " ,    '" & tmpItemKana & "'"
    sql &= " ,    '" & tmpHyojunTanka & "'"
    sql &= " ,    ''"
    sql &= " ,    ''"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    " & tmpTokuisakiTanka
    sql &= " ,    '" & tmpTani & "'"
    sql &= " ,    " & tmpNouhinTanka
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    " & tmpIrisu
    sql &= " ,    0"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    '" & tmpdate & "'"
    sql &= " ,    '" & tmpdate & "'"
    sql &= " )"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub UpdateScaleMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    'rowSelectionCode = UnitNumberText.Text
    With tmpDb
      Try
        sql = GetUpdateSql()
        ' SQL実行結果が1件か？
        Dim confirmation As String
        confirmation = MessageBox.Show("更新します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("更新処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim CurrentRow As Integer = Form_TokuisakiShohinMasterList.ScaleDetail.CurrentRow.Index
            Form_TokuisakiShohinMasterList.SelectScaleMaster()
            Form_TokuisakiShohinMasterList.ScaleDetail.Rows(CurrentRow).Selected = True
            Form_TokuisakiShohinMasterList.ScaleDetail.FirstDisplayedScrollingRowIndex = CurrentRow
            Form_TokuisakiShohinMasterList.ScaleDetail.CurrentCell = Form_TokuisakiShohinMasterList.ScaleDetail.Rows(CurrentRow).Cells(0)
            MeClose()
          Else
            ' 更新失敗
            MessageBox.Show("計量器マスタの更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        Else
          Exit Sub
        End If
      Catch ex As Exception
        Call ComWriteErrLog([GetType]().Name,
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        Throw New Exception(ex.Message)
      End Try
    End With
  End Sub
  Private Function GetUpdateSql() As String
    Dim sql As String = String.Empty
    Dim tmpTokuisakiCode As String = CmbMstCustomer1.SelectedValue
    Dim tmpItemCode As String = CmbMstItem1.SelectedValue
    Dim tmpItemName As String = TxtItemName.Text
    Dim tmpItemKana As String = TxtItemKana.Text
    Dim tmpTani As String = TxtTani.Text
    Dim tmpIrisu As String = If(String.IsNullOrWhiteSpace(TxtIrisu.Text), "0", TxtIrisu.Text)
    Dim tmpHyojunTanka As String = If(String.IsNullOrWhiteSpace(TxtHyojunTanka.Text), "0", TxtHyojunTanka.Text)
    Dim tmpNouhinTanka As String = If(String.IsNullOrWhiteSpace(TxtNouhinTanka.Text), "0", TxtNouhinTanka.Text)
    Dim tmpTokuisakiTanka As String = If(String.IsNullOrWhiteSpace(TxtTokuisakiTanka.Text), "0", TxtTokuisakiTanka.Text)

    Dim tmpdate As DateTime = CDate(ComGetProcTime())
    sql &= " UPDATE MST_TOKUISAKI_SHOHIN "
    sql &= "    SET ShohinNM = '" & tmpItemName & "'"
    sql &= "       ,ShohinKana = '" & tmpItemKana & "'"
    sql &= "       ,HyojunKakaku = '" & tmpHyojunTanka & "'"
    sql &= "       ,Tani = '" & tmpTani & "'"
    sql &= "       ,Irisu = '" & tmpIrisu & "'"
    sql &= "       ,Tanka = '" & tmpTokuisakiTanka & "'"
    sql &= "       ,Baika = '" & tmpNouhinTanka & "'"
    sql &= "       ,KDATE = '" & tmpdate & "'"
    sql &= " WHERE TokuiCD = '" & tmpTokuisakiCode & "' "
    sql &= " AND ShohinCD = '" & tmpItemCode & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

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
  ''' 商品が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMstItem"></param>
  ''' <param name="prmTxtLabelItem"></param>
  Private Function CmbMstItemValidating(prmCmbMstItem As ComboBox, prmTxtLabelItem As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMstCustomer.Equals(prmCmbMstItem.Text) = False) Then
      lastCmbMstCustomer = prmCmbMstItem.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMstItem.Text) Then
      ' 商品コード
      prmCmbMstItem.Text = String.Empty
      prmTxtLabelItem.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      Dim tmpTSDt As New DataTable
      If (GetShCode(prmCmbMstItem.Text, tmpDt)) Then


        If GetTkShCode(Me.CmbMstCustomer1.Text, prmCmbMstItem.Text, tmpTSDt) Then
          If String.IsNullOrWhiteSpace(prmTxtLabelItem.Text) Then
            ' 商品コード
            prmCmbMstItem.Text = tmpDt.Rows(0)("Code").ToString
            prmTxtLabelItem.Text = tmpDt.Rows(0)("Name").ToString
          Else
            ' 商品コード
            prmCmbMstItem.Text = tmpDt.Rows(0)("Code").ToString
            prmTxtLabelItem.Text = tmpTSDt.Rows(0)("ShohinNm").ToString
          End If
        Else
          ' 商品コード
          prmCmbMstItem.Text = tmpDt.Rows(0)("Code").ToString
          prmTxtLabelItem.Text = tmpDt.Rows(0)("Name").ToString
        End If
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
  ''' 商品マスタ検索処理
  ''' </summary>
  ''' <param name="prmCode"></param>
  ''' <param name="prmDt"></param>
  ''' <returns></returns>
  Private Function GetShCode(prmCode As String,
                                     ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New ClsSqlServer

    ' 実行
    With tmpDb
      Try

        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetShCode(prmCode))
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
  ''' 得意先商品マスタ検索処理
  ''' </summary>
  ''' <param name="prmCode"></param>
  ''' <param name="prmDt"></param>
  ''' <returns></returns>
  Private Function GetTkShCode(prmTkCode As String, prmShCode As String,
                                     ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New ClsSqlServer

    ' 実行
    With tmpDb
      Try
        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetTkShCode(prmTkCode, prmShCode))
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
  ''' 商品マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <param name="prmTKCode">得意先コード</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetShCode(prmShCode As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT ShohinCD As Code"
    sql &= "    ,   ShohinNM As Name"
    sql &= "    ,   ShohinKana  As Name2"
    sql &= " FROM MST_SHOHIN "
    sql &= " WHERE 1 = 1 "
    sql &= " AND ShohinCD = '" & prmShCode & "'"
    sql &= " ORDER BY ShohinCD "

    Return sql

  End Function

  ''' <summary>
  ''' 得意先商品マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <param name="prmTKCode">得意先コード</param>
  ''' <param name="prmShCode">商品コード</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetTkShCode(prmTKCode As String, prmShCode As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT TokuiCd As TkCode"
    sql &= "    ,   ShohinCD As ChCode "
    sql &= "    ,   ShohinNM As ShohinNM "
    sql &= " FROM MST_TOKUISAKI_SHOHIN "
    sql &= " WHERE 1 = 1 "
    sql &= " AND TokuiCd = '" & prmTKCode & "'"
    sql &= " AND ShohinCD = '" & prmShCode & "'"
    sql &= " ORDER BY ShohinCD "

    Return sql

  End Function

  Private Sub CmbMstCustomer1_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstCustomer1.Validating
    If CmbMstCustomerValidating(CmbMstCustomer1, TxtTokuName) Then
      e.Cancel = True
    End If
  End Sub

  Private Sub CmbMstItem1_Validating(sender As Object, e As CancelEventArgs) Handles CmbMstItem1.Validating
    If CmbMstItemValidating(CmbMstItem1, TxtItemName) Then
      e.Cancel = True
    End If

  End Sub
End Class