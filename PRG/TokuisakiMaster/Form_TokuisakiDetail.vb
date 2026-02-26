Imports Common
Imports Common.ClsFunction
Public Class Form_TokuisakiDetail
  '新規:１ 、変更:２
  Public InputMode As Integer

  Public TokuisakiCodeTextValue As Integer
  Public TokuisakiNameTextValue As String
  Public TokuisakiNameKanaTextValue As String
  Public Jusho1TextValue As String
  Public Jusho2TextValue As String
  Public YubinNoTextValue As String
  Public TelNoTextValue As String
  Public SeikyuSmbTextValue As String
  Public ShohizeTuchiTextValue As String
  Public GroupCdTextValue As String

  ReadOnly tmpDb As New ClsSqlServer
  ReadOnly tmpDt As New DataTable
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

  Private Sub Form_ManufacturerDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "得意先マスタ詳細" & " ( " & updateTime & " ) "
    FormBorderStyle = FormBorderStyle.FixedSingle
    SetInitialProperty()
  End Sub

  Private Sub SetInitialProperty()
    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        TxtTokuisakiCode.Enabled = True
      Case 2
        TxtTokuisakiCode.Enabled = False
        TxtTokuisakiCode.Text = TokuisakiCodeTextValue.ToString()
        TxtTokuisakiName.Text = TokuisakiNameTextValue
        TxtJusho1.Text = Jusho1TextValue
        TxtJusho2.Text = Jusho2TextValue
        TxtYubinNo.Text = YubinNoTextValue
        TxtTelNo.Text = TelNoTextValue
        TxtSeikyuSmb.Text = SeikyuSmbTextValue
        TxtShohiZeiTuchi.Text = ShohizeTuchiTextValue
        TxtGroupCode.Text = GroupCdTextValue
    End Select
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
    Select Case InputMode
      Case 1
        If CheckValue() = False Then
          Exit Sub
        End If
        '新規登録メソッド呼出し
        InsertManufacturerMaster()
      Case 2
        '更新メソッド呼出し
        UpdateManufacturerMaster()
    End Select
  End Sub
  Function CheckValue()
    Dim CheckResult As Boolean = True
    '必須項目チェック
    If String.IsNullOrEmpty(TxtTokuisakiCode.Text) = True Then
      MessageBox.Show("製造者コードを入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If

    If String.IsNullOrEmpty(TxtTokuisakiName.Text) = True Then
      MessageBox.Show("製造者名を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If
    '重複チェック
    If Form_TokuisakiList.ManufacturerDetail.Rows.Count > 0 Then
      For i As Integer = 0 To Form_TokuisakiList.ManufacturerDetail.Rows.Count - 1
        If TxtTokuisakiCode.Text.Equals(Form_TokuisakiList.ManufacturerDetail.Rows(i).Cells(0).Value) Then
          MessageBox.Show("既に登録されている製造者コードです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          CheckResult = False
          TxtTokuisakiCode.Focus()
          Exit For
        End If
      Next
    End If
    Return CheckResult
  End Function

  Private Sub InsertManufacturerMaster()
    Dim sql As String = String.Empty
    'Dim rowSelectionCode As String = String.Empty
    'rowSelectionCode = CodeText.Text
    With tmpDb
      Try
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
            Form_TokuisakiList.SelectManufacturerMaster()
            ' 一覧画面件数更新
            'Form_ManufacturerList.GetRowCount(Form_ManufacturerList.DeletedDisplayCheckBox.Checked)

            Close()
          Else
            ' 登録失敗
            MessageBox.Show("商品マスタの登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Dim sql As String = String.Empty
    Dim tmpTokuisakiCode As Integer = TxtTokuisakiCode.Text
    Dim tmpTokuisakinName As String = TxtTokuisakiName.Text
    Dim tmpJusho1 As String = TxtJusho1.Text
    Dim tmpJusho2 As String = TxtJusho2.Text
    Dim tmpYubinNo As String = TxtYubinNo.Text
    Dim tmpTelNo As String = TxtTelNo.Text
    Dim tmpSeikyuSmb As String = If(String.IsNullOrWhiteSpace(TxtSeikyuSmb.Text), "0", TxtSeikyuSmb.Text)
    Dim tmpShohiZeiTuchi As String = If(String.IsNullOrWhiteSpace(TxtShohiZeiTuchi.Text), "0", TxtShohiZeiTuchi.Text)
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    sql &= " INSERT INTO MST_TOKUISAKI ("
    sql &= "     TokuiCD"
    sql &= " ,   TokuiNM1"
    sql &= " ,   TokuiNM2"
    sql &= " ,   TokuiKana"
    sql &= " ,   SenpoTanto"
    sql &= " ,   TMstKBN"
    sql &= " ,   SeikyuCD"
    sql &= " ,   TJituKanri"
    sql &= " ,   Add1"
    sql &= " ,   Add2"
    sql &= " ,   ZipCD"
    sql &= " ,   TelNo"
    sql &= " ,   FaxNo"
    sql &= " ,   TokuiKBN1"
    sql &= " ,   TokuiKBN2"
    sql &= " ,   TokuiKBN3"
    sql &= " ,   TekiyoUriNo"
    sql &= " ,   TekiyoKakeritu"
    sql &= " ,   TekiyoZeikan"
    sql &= " ,   SyuTantoCD"
    sql &= " ,   SeikyuSimebi"
    sql &= " ,   ShohizeiHasu"
    sql &= " ,   ShohizeiTuti"
    sql &= " ,   Kaisyu1"
    sql &= " ,   Kaisyu2"
    sql &= " ,   KaisyuKyokai"
    sql &= " ,   KaisyuYoteibi"
    sql &= " ,   KaisyuHou"
    sql &= " ,   YosinGendo"
    sql &= " ,   KurikosiZan"
    sql &= " ,   NohinYosi"
    sql &= " ,   NohinShamei"
    sql &= " ,   SeikyuYosi"
    sql &= " ,   SeikyuShamei"
    sql &= " ,   KantyoKBN"
    sql &= " ,   Keisho"
    sql &= " ,   ShatenCD"
    sql &= " ,   TorihikiCD"
    sql &= " ,   TokuiKubun"
    sql &= " ,   DENP_KBN"
    sql &= " ,   TDATE"
    sql &= " ,   KDATE"
    sql &= " )"
    sql &= " VALUES("
    sql &= "      '" & tmpTokuisakiCode & "'"
    sql &= " ,    '" & tmpTokuisakinName & "'"
    sql &= " ,    ''"
    sql &= " ,    ''"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    '" & tmpJusho1 & "'"
    sql &= " ,    '" & tmpJusho2 & "'"
    sql &= " ,    '" & tmpTelNo & "'"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    '" & tmpSeikyuSmb & "'"
    sql &= " ,    0"
    sql &= " ,    '" & tmpShohiZeiTuchi & "'"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    ''"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    ''"
    sql &= " ,    '" & tmpdate & "'"
    sql &= " ,    '" & tmpdate & "'"
    sql &= " )"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub UpdateManufacturerMaster()
    Dim sql As String = String.Empty
    'Dim rowSelectionCode As String = String.Empty
    'rowSelectionCode = CodeText.Text
    With tmpDb
      Try
        sql = GetUpdateSql()
        Dim confirmation As String
        confirmation = MessageBox.Show("更新します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("更新処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim CurrentRow As Integer = Form_TokuisakiList.ManufacturerDetail.CurrentRow.Index
            Form_TokuisakiList.SelectManufacturerMaster()
            Form_TokuisakiList.ManufacturerDetail.Rows(CurrentRow).Selected = True
            Form_TokuisakiList.ManufacturerDetail.FirstDisplayedScrollingRowIndex = CurrentRow
            '選択している行の行番号の取得
            Form_TokuisakiList.ManufacturerDetail.CurrentCell = Form_TokuisakiList.ManufacturerDetail.Rows(CurrentRow).Cells(0)
            Close()
          Else
            ' 更新失敗
            MessageBox.Show("商品マスタの更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Dim tmpTokuisakiCode As Integer = TxtTokuisakiCode.Text
    Dim tmpTokuisakinName As String = TxtTokuisakiName.Text
    Dim tmpJusho1 As String = TxtJusho1.Text
    Dim tmpJusho2 As String = TxtJusho2.Text
    Dim tmpYubinNo As String = TxtYubinNo.Text
    Dim tmpTelNo As String = TxtTelNo.Text
    Dim tmpSeikyuSmb As String = If(String.IsNullOrWhiteSpace(TxtSeikyuSmb.Text), "0", TxtSeikyuSmb.Text)
    Dim tmpShohiZeiTuchi As String = If(String.IsNullOrWhiteSpace(TxtShohiZeiTuchi.Text), "0", TxtShohiZeiTuchi.Text)
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    sql &= " UPDATE MST_TOKUISAKI SET"
    sql &= "     TokuiNM1 = '" & tmpTokuisakinName & "'"
    sql &= " ,    Add1 = '" & tmpJusho1 & "'"
    sql &= " ,    Add2 = '" & tmpJusho2 & "'"
    sql &= " ,    ZipCD = '" & tmpYubinNo & "'"
    sql &= " ,    TelNo = '" & tmpTelNo & "'"
    sql &= " ,    SeikyuSimebi = '" & tmpSeikyuSmb & "'"
    sql &= " ,    ShohizeiTuti = '" & tmpShohiZeiTuchi & "'"
    sql &= " ,    KDATE = '" & tmpdate & "'"
    sql &= " WHERE TokuiCD = '" & tmpTokuisakiCode & "'"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CodeText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTokuisakiCode.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
End Class