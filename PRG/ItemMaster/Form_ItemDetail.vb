Imports Common
Imports Common.ClsFunction
Public Class Form_ItemDetail

  '新規:１ 、変更:２
  Public InputMode As Integer

  Public ItemCodeTextValue As String
  Public ItemNameTextValue As String
  Public ItemNameKanaTextValue As String
  Public IrisuTextValue As String
  Public TaniTextValue As String
  Public ZeiKbnTextValue As String

  Public HyojunTankaTextValue As String
  Public GenkaTextValue As String
  Public NouhinTankaTextValue As String
  Public TyuBunruiTextValue As String

  Public UnitWeightTextValue As String
  Public UnitWeightUnitTextValue As String
  Public DateWeightTextValue As String
  'Public UpperUnitWeightTextValue As Decimal
  Public UpperUnitWeightUnitTextValue As String
  Public LowerUnitWeightTextValue As Decimal
  Public LowerUnitWeightUnitTextValue As String

  Public PackingTextValue As Decimal
  Public PackingUnitTextValue As String

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
  Private Sub Form_ItemDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "商品マスタ詳細" & " ( " & updateTime & " ) "
    FormBorderStyle = FormBorderStyle.FixedSingle

    SetPackingComboBox()
    SetUnitComboBox()

    SetInitialProperty()
  End Sub
  Private Sub SetPackingComboBox()
    'Dim PackingData As DataTable = GetPackingMasterData()

    'PackingComboBox.Items.Clear()

    'If PackingData.Rows.Count = 0 Then
    '  MessageBox.Show("風袋マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Else
    '  For Each row As DataRow In PackingData.Rows
    '    PackingComboBox.Items.Add(row(0))
    '  Next
    'End If
  End Sub
  Private Function GetPackingMasterData() As DataTable
    Dim PackingData As New DataTable
    Try
      With tmpDb
        SqlServer.GetResult(PackingData, GetSelectPackingMaster)
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try

    Return PackingData
  End Function
  Private Function GetSelectPackingMaster() As String
    Dim sql As String = String.Empty
    'sql &= " SELECT PackingNo"
    sql &= " SELECT PackingWeight"
    sql &= " FROM MST_Packing "
    'sql &= " WHERE DELETE_FLG = 0 "
    sql &= " ORDER BY PackingNo"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub SetUnitComboBox()
    '' 共通のアイテムリスト
    'Dim items As String() = {"mg", "g", "kg", "t"}

    '' コンボボックスのアイテムをクリア
    'UnitWeightUnitComboBox.Items.Clear()
    'UpperUnitWeightUnitComboBox.Items.Clear()
    'LowerUnitWeightUnitComboBox.Items.Clear()
    'PackingUnitComboBox.Items.Clear()

    '' アイテムをコンボボックスに追加
    'For Each item As String In items
    '  UnitWeightUnitComboBox.Items.Add(item)
    '  UpperUnitWeightUnitComboBox.Items.Add(item)
    '  LowerUnitWeightUnitComboBox.Items.Add(item)
    '  PackingUnitComboBox.Items.Add(item)
    'Next

    'PackingComboBox.Items.Add(10.0)
  End Sub

  Private Sub SetInitialProperty()
    'UnitWeightUnitComboBox.Enabled = True
    'UpperUnitWeightUnitComboBox.Enabled = True
    'LowerUnitWeightUnitComboBox.Enabled = True
    'PackingComboBox.Enabled = True
    'PackingUnitComboBox.Enabled = True

    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        TxtItemCode.Enabled = True
      Case 2
        TxtItemCode.Enabled = False

        TxtItemCode.Text = ItemCodeTextValue.ToString()
        TxtItemName.Text = ItemNameTextValue
        TxtItemNameKana.Text = ItemNameKanaTextValue
        TxtIrisu.Text = IrisuTextValue
        TxtTani.Text = TaniTextValue
        TxtZeiKbn.Text = ZeiKbnTextValue

        TxtHyojunTanka.Text = HyojunTankaTextValue
        TxtGenka.Text = GenkaTextValue
        TxtNouhinTanka.Text = NouhinTankaTextValue
        TxtTyuBunrui.Text = TyuBunruiTextValue

        'UnitWeightText.Text = UnitWeightTextValue
        'UpperUnitWeightText.Text = DateWeightTextValue
        'LowerUnitWeightText.Text = LowerUnitWeightTextValue

        'UnitWeightUnitComboBox.SelectedItem = UnitWeightUnitTextValue
        'UpperUnitWeightUnitComboBox.SelectedItem = UpperUnitWeightUnitTextValue
        'LowerUnitWeightUnitComboBox.SelectedItem = LowerUnitWeightUnitTextValue

        'PackingComboBox.SelectedItem = PackingTextValue
        'PackingUnitComboBox.SelectedItem = PackingUnitTextValue
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
        InsertItemMaster()
      Case 2
        '更新メソッド呼出し
        UpdateItemMaster()
    End Select
  End Sub

  Function CheckValue()
    Dim CheckResult As Boolean = True
    '必須項目チェック
    If String.IsNullOrEmpty(TxtItemCode.Text) = True Then
      MessageBox.Show("商品コードを入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If
    '重複チェック
    If Form_ItemList.ItemDetail.Rows.Count > 0 Then
      For i As Integer = 0 To Form_ItemList.ItemDetail.Rows.Count - 1
        If TxtItemCode.Text.Equals(Form_ItemList.ItemDetail.Rows(i).Cells(0).Value) Then
          MessageBox.Show("既に登録されている商品コードです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          CheckResult = False
          TxtItemCode.Focus()
          Exit For
        End If
      Next
    End If
    Return CheckResult
  End Function
  Private Sub InsertItemMaster()
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
            Form_ItemList.SelectItemMaster()
            ' 一覧画面件数更新
            'Form_ItemList.GetRowCount(Form_ItemList.DeletedDisplayCheckBox.Checked)

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
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    ' 各フィールドの型に応じた変換
    Dim tmpItemNo As String = TxtItemCode.Text
    Dim tmpItemName As String = TxtItemName.Text
    Dim tmpIrisu As String = TxtIrisu.Text
    Dim tmpTani As String = TxtTani.Text
    Dim tmpZeiKbn As String = TxtZeiKbn.Text
    Dim tmpHyojunTanka As String = TxtHyojunTanka.Text
    Dim tmpGenka As String = TxtGenka.Text
    Dim tmpNouhinTanka As String = TxtNouhinTanka.Text
    Dim tmpTyuBunrui As String = If(String.IsNullOrWhiteSpace(TxtTyuBunrui.Text), "0", TxtTyuBunrui.Text)

    sql &= " INSERT INTO MST_SHOHIN("
    sql &= "     ShohinCD"
    sql &= " ,    ShohinNM"
    sql &= " ,    ShohinKana"
    sql &= " ,    SysKBN"
    sql &= " ,    SMstKBN"
    sql &= " ,    ZaiKanri"
    sql &= " ,    JituKanri"
    sql &= " ,    Irisu"
    sql &= " ,    Tani"
    sql &= " ,    Iro"
    sql &= " ,    Size"
    sql &= " ,    ShohinKBN1"
    sql &= " ,    ShohinKBN2"
    sql &= " ,    ShohinKBN3"
    sql &= " ,    ZeiKBN"
    sql &= " ,    ZeikomiKBN"
    sql &= " ,    SKetaT"
    sql &= " ,    SKetaS"
    sql &= " ,    HyojunKakaku"
    sql &= " ,    Genka"
    sql &= " ,    Baika1"
    sql &= " ,    Baika2"
    sql &= " ,    Baika3"
    sql &= " ,    Baika4"
    sql &= " ,    Baika5"
    sql &= " ,    SokoCD"
    sql &= " ,    SSiireCD"
    sql &= " ,    ZaiTanka"
    sql &= " ,    SiireTanka"
    sql &= " ,    JANCD"
    sql &= " ,    ShoKubun"
    sql &= " ,    TDATE"
    sql &= " ,    KDATE"
    sql &= " )"
    sql &= " VALUES("
    sql &= "     " & tmpItemNo & ""
    sql &= "  ,   '" & tmpItemName & "'"
    sql &= "  ,   '" & tmpItemName & "'"
    sql &= "  ,    0"
    sql &= "  ,   0"
    sql &= "  ,   0"
    sql &= "  ,   0"
    sql &= "  ,   " & tmpIrisu & ""
    sql &= "  ,   '" & tmpTani & "'"
    sql &= "  ,   ''"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    '" & tmpZeiKbn & "'"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= "  ,   '" & tmpHyojunTanka & "'"
    sql &= "  ,   '" & tmpGenka & "'"
    sql &= "  ,   '" & tmpNouhinTanka & "'"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    ''"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    0"
    sql &= " ,    '" & tmpTyuBunrui & "'"
    sql &= "  ,   '" & tmpdate & "'"
    sql &= "  ,   '" & tmpdate & "'"
    sql &= " )"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub UpdateItemMaster()
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
            Dim CurrentRow As Integer = Form_ItemList.ItemDetail.CurrentRow.Index
            Form_ItemList.SelectItemMaster()
            'Form_ItemList.ItemDetail.Rows(CurrentRow).Selected = True
            'Form_ItemList.ItemDetail.FirstDisplayedScrollingRowIndex = CurrentRow
            ''選択している行の行番号の取得
            'Form_ItemList.ItemDetail.CurrentCell = Form_ItemList.ItemDetail.Rows(CurrentRow).Cells(0)
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
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    ' 各フィールドの型に応じた変換
    Dim tmpItemNo As String = TxtItemCode.Text
    Dim tmpItemName As String = TxtItemName.Text
    Dim tmpIrisu As String = TxtIrisu.Text
    Dim tmpTani As String = TxtTani.Text
    Dim tmpZeiKbn As String = TxtZeiKbn.Text
    Dim tmpHyojunTanka As String = TxtHyojunTanka.Text
    Dim tmpGenka As String = TxtGenka.Text
    Dim tmpNouhinTanka As String = TxtNouhinTanka.Text
    Dim tmpTyuBunrui As String = If(String.IsNullOrWhiteSpace(TxtTyuBunrui.Text), "0", TxtTyuBunrui.Text)

    sql &= " UPDATE MST_SHOHIN"
    sql &= "    SET ShohinNM = '" & tmpItemName & "'"
    sql &= "    ,    Irisu = '" & tmpIrisu & "'"
    sql &= "    ,    Tani = '" & tmpTani & "'"
    sql &= "    ,    ZeiKBN = " & tmpZeiKbn & ""
    sql &= "    ,    HyojunKakaku = " & tmpHyojunTanka & ""
    sql &= "    ,    Genka = " & tmpGenka & ""
    sql &= "    ,    Baika1 = " & tmpNouhinTanka & ""
    sql &= "    ,    ShoKubun = " & tmpTyuBunrui & ""
    sql &= "    ,    KDATE = '" & tmpdate & "'"
    sql &= " WHERE ShohinCD = '" & tmpItemNo & "'"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub UpperUnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs)
    ' テキストボックスに入力された文字が数字、小数点、バックスペースでない場合は入力を無効化する
    If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "." OrElse Char.IsControl(e.KeyChar)) Then
      e.Handled = True
    End If

    ' 小数点の場合、既に小数点が含まれているか、すでに小数点が含まれていて、小数点が2つ以上入力された場合は無効化する
    'If e.KeyChar = "." Then
    '  If UpperUnitWeightText.Text.Contains(".") OrElse UpperUnitWeightText.Text.Length = 0 Then
    '    e.Handled = True
    '  End If
    'End If

    '' 入力された文字数が7桁以上の場合は無効化する
    'If UpperUnitWeightText.Text.Replace(".", "").Length >= 7 AndAlso e.KeyChar <> ControlChars.Back Then
    '  e.Handled = True
    'End If
  End Sub

  Private Sub LowerUnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs)
    ' テキストボックスに入力された文字が数字、小数点、バックスペースでない場合は入力を無効化する
    If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "." OrElse Char.IsControl(e.KeyChar)) Then
      e.Handled = True
    End If

    '' 小数点の場合、既に小数点が含まれているか、すでに小数点が含まれていて、小数点が2つ以上入力された場合は無効化する
    'If e.KeyChar = "." Then
    '  If UpperUnitWeightText.Text.Contains(".") OrElse UpperUnitWeightText.Text.Length = 0 Then
    '    e.Handled = True
    '  End If
    'End If

    '' 入力された文字数が7桁以上の場合は無効化する
    'If UpperUnitWeightText.Text.Replace(".", "").Length >= 7 AndAlso e.KeyChar <> ControlChars.Back Then
    '  e.Handled = True
    'End If
  End Sub

  Private Sub CallCodeText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtItemCode.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub UnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub SafetyFactorText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNouhinTanka.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub TargetQtyText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTyuBunrui.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub UpperLimitText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtIrisu.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub StandardText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTani.KeyPress
    ''キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    'If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
    '  e.Handled = True
    'End If
  End Sub

  Private Sub LowerLimitText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtZeiKbn.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub SubtotalTargetQtyText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtHyojunTanka.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub SubtotalTargetCntText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtGenka.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
End Class