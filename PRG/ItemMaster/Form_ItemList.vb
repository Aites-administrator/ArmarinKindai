Imports Common
Imports Common.ClsFunction
Public Class Form_ItemList
  Dim AllRowDisplayFlg As Boolean = False
  Private CheckboxExistFlg As New Boolean
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

  Private Sub Form_ItemList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "商品マスタ一覧" & " ( " & updateTime & " ) "
    'CheckboxExistFlg = True
    'CheckBoxCtlButton.Text = "全選択"
    'AllCheckBoxFlg = True
    ItemDetail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    'CheckBox列を追加する
    'Dim checkBox_Trans As New DataGridViewCheckBoxColumn
    'Dim checkBox_Delete As New DataGridViewCheckBoxColumn
    'ItemDetail.Columns.Add(checkBox_Trans)
    ''カラムのヘッダー名指定
    'checkBox_Trans.Width = 90

    ItemDetail.AllowUserToAddRows = False

    SqlServer.GetResult(tmpDt, GetAllSelectSql())
    ItemDetail.DataSource = tmpDt

        'ItemDetail.ColumnCount = 19

        '' Columns(0)のヘッダーテキストを設定
        ''ItemDetail.Columns(0).HeaderText = "選択"

        '' 残りのヘッダーテキストを設定
        'ItemDetail.Columns(0).HeaderText = "呼出コード"
        'ItemDetail.Columns(1).HeaderText = "品番"
        'ItemDetail.Columns(2).HeaderText = "単重"
        'ItemDetail.Columns(3).HeaderText = "単重単位"
        'ItemDetail.Columns(4).HeaderText = "安全計数"
        'ItemDetail.Columns(5).HeaderText = "目標個数"
        'ItemDetail.Columns(6).HeaderText = "風袋"
        'ItemDetail.Columns(7).HeaderText = "風袋単位"
        'ItemDetail.Columns(8).HeaderText = "上限値"
        'ItemDetail.Columns(9).HeaderText = "基準値"
        'ItemDetail.Columns(10).HeaderText = "下限値"
        'ItemDetail.Columns(11).HeaderText = "小計目標値"
        'ItemDetail.Columns(12).HeaderText = "小計目標回数"
        'ItemDetail.Columns(13).HeaderText = "品名"
        'ItemDetail.Columns(14).HeaderText = "単重上限値"
        'ItemDetail.Columns(15).HeaderText = "単重上限値単位"
        'ItemDetail.Columns(16).HeaderText = "単重下限値"
        'ItemDetail.Columns(17).HeaderText = "単重下限値単位"
        '' 削除フラグの列を非表示にする
        'ItemDetail.Columns(18).Visible = False

        '' カラムの幅指定
        'ItemDetail.Columns(0).Width = 100
        'ItemDetail.Columns(1).Width = 100
        'ItemDetail.Columns(2).Width = 100
        'ItemDetail.Columns(3).Width = 100
        'ItemDetail.Columns(4).Width = 100
        'ItemDetail.Columns(5).Width = 100
        'ItemDetail.Columns(6).Width = 100
        'ItemDetail.Columns(7).Width = 100
        'ItemDetail.Columns(8).Width = 100
        'ItemDetail.Columns(9).Width = 100
        'ItemDetail.Columns(10).Width = 100
        'ItemDetail.Columns(11).Width = 100
        'ItemDetail.Columns(12).Width = 100
        'ItemDetail.Columns(13).Width = 100
        'ItemDetail.Columns(14).Width = 100
        'ItemDetail.Columns(15).Width = 100
        'ItemDetail.Columns(16).Width = 100
        'ItemDetail.Columns(17).Width = 100

        ''カラムの整列設定
        'For i As Integer = 0 To 17
        '    ItemDetail.Columns(i).DefaultCellStyle.Alignment =
        '    DataGridViewContentAlignment.MiddleCenter
        'Next

        ''ヘッダーの整列設定
        'For i As Integer = 0 To 17
        '    ItemDetail.Columns(i).HeaderCell.Style.Alignment =
        '    DataGridViewContentAlignment.MiddleCenter
        'Next

        'SelectItemMaster()

        ItemDetail.ReadOnly = True
        ' 選択モードを全カラム選択に設定
        ItemDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
  End Sub

  Public Sub SelectItemMaster()
    Try
      SqlServer.GetResult(tmpDt, GetAllSelectSql())
      ItemDetail.DataSource = tmpDt
      ItemDetail.Refresh()

      'With tmpDb
      '  SqlServer.GetResult(tmpDt, sql)

      '  If tmpDt.Rows.Count = 0 Then
      '    MessageBox.Show("商品マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '    'RowCount.Text = "0件"
      '  Else
      '    WriteDetail(tmpDt, ItemDetail)
      '    UpdateButton.Enabled = True
      '    DeleteButton.Enabled = True
      '    'RowCount.Text = ItemDetail.Rows.Count.ToString + "件"
      '  End If
      'End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      'tmpDt.Dispose()
    End Try

    'DisPlayDeleteRow(AllRowDisplayFlg)

    ''行色付
    'If ItemDetail.Rows.Count > 0 Then
    '  For i As Integer = 0 To ItemDetail.Rows.Count - 1
    '    If ItemDetail.Rows(i).Cells(18).Value = True Then
    '      ItemDetail.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray
    '    End If
    '  Next
    'End If

    ''検索結果が存在する場合、先頭行選択
    'If ItemDetail.Rows.Count > 0 Then
    '  For i As Integer = 0 To ItemDetail.Rows.Count - 1
    '    If ItemDetail.Rows(i).Cells(18).Value = False Then
    '      ItemDetail.CurrentCell = ItemDetail.Rows(i).Cells(0)
    '      ItemDetail.Rows(i).Selected = True
    '      Exit For
    '    End If
    '  Next

    '  'CheckBoxCtlButton.Visible = True
    '  GetRowCount(False)
    'Else
    '  RowCount.Text = "0件"
    '  'CheckBoxCtlButton.Visible = False
    'End If
  End Sub

  Private Function GetAllSelectSql() As String

    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "        ShohinCD 商品コード "
    sql &= "     ,	ShohinNM 商品名 "
    sql &= "     ,	ShohinKana 商品カナ "
    sql &= "     ,	Irisu 入数 "
    sql &= "     ,	tani 単位 "
    sql &= "     ,	ZeiKBN 税区分 "
    sql &= "     ,	ZeikomiKBN 税込区分 "
    sql &= "     ,	HyojunKakaku 標準価格 "
    sql &= "     ,	genka 原価 "
    sql &= "     ,	Baika1 売価１ "
    sql &= "     ,	Baika2 売価２ "
    sql &= "     ,	Baika3 売価３ "
    sql &= "     ,	Baika4 売価４ "
    sql &= "     ,	Baika5 売価５ "
    sql &= "     ,	tdate 登録日時 "
    sql &= "     ,	KDATE 更新日時 "
    sql &= " FROM"
    sql &= "     MST_SHOHIN "
    sql &= " ORDER BY"
    sql &= "     ShohinCD"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_ItemDetail.InputMode = 1
    Form_ItemDetail.ShowDialog()
  End Sub
  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_ItemDetail.InputMode = 2
    Form_ItemDetail.ShowDialog()
  End Sub
  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = ItemDetail.CurrentRow.Index
    Form_ItemDetail.ItemCodeTextValue = ItemDetail.Rows(i).Cells(0).Value
    Form_ItemDetail.ItemNameTextValue = ItemDetail.Rows(i).Cells(1).Value
    Form_ItemDetail.ItemNameKanaTextValue = ItemDetail.Rows(i).Cells(2).Value
    Form_ItemDetail.IrisuTextValue = ItemDetail.Rows(i).Cells(3).Value
    Form_ItemDetail.TaniTextValue = ItemDetail.Rows(i).Cells(4).Value
    Form_ItemDetail.ZeiKbnTextValue = ItemDetail.Rows(i).Cells(5).Value
    Form_ItemDetail.HyojunTankaTextValue = ItemDetail.Rows(i).Cells(7).Value
    Form_ItemDetail.NouhinTankaTextValue = ItemDetail.Rows(i).Cells(9).Value
    Form_ItemDetail.GenkaTextValue = ItemDetail.Rows(i).Cells(8).Value
    'Form_ItemDetail.IrisuTextValue = ItemDetail.Rows(i).Cells(8).Value
    'Form_ItemDetail.TaniTextValue = ItemDetail.Rows(i).Cells(9).Value
    'Form_ItemDetail.ZeiKbnTextValue = ItemDetail.Rows(i).Cells(10).Value
    'Form_ItemDetail.HyojunTankaTextValue = ItemDetail.Rows(i).Cells(11).Value
    'Form_ItemDetail.GenkaTextValue = ItemDetail.Rows(i).Cells(12).Value
    'Form_ItemDetail.ItemNameKanaTextValue = ItemDetail.Rows(i).Cells(13).Value
    'Form_ItemDetail.DateWeightTextValue = ItemDetail.Rows(i).Cells(14).Value
    'Form_ItemDetail.UpperUnitWeightUnitTextValue = ItemDetail.Rows(i).Cells(15).Value
    'Form_ItemDetail.LowerUnitWeightTextValue = ItemDetail.Rows(i).Cells(16).Value
    'Form_ItemDetail.LowerUnitWeightUnitTextValue = ItemDetail.Rows(i).Cells(17).Value
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteItemMaster()
  End Sub

  Private Sub DeleteItemMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    Dim DeleteRowFlg As Boolean = ItemDetail.CurrentRow.Cells(18).Value
    Dim confirmation As String
    Dim msg1 As String
    Dim msg2 As String
    With tmpDb
      Try
        If DeleteRowFlg Then
          sql = GetDeleteSql(False)
          msg1 = "削除取消します。" & vbCrLf & "よろしいでしょうか。"
          msg2 = "削除取消処理完了しました。"
        Else
          sql = GetDeleteSql(True)
          msg1 = "削除します。" & vbCrLf & "よろしいでしょうか。"
          msg2 = "削除処理完了しました。"
        End If

        confirmation = MessageBox.Show(msg1, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show(msg2, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelectItemMaster()
            If DeletedDisplayCheckBox.Checked Then
              GetRowCount(True)
            Else
              GetRowCount(False)
            End If
            RefreshText()
          Else
            ' 削除失敗
            MessageBox.Show("商品マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
  Private Function GetDeleteSql(DeleteFlg As Boolean) As String
    Dim sql As String = String.Empty
    Dim currentRow As Integer = ItemDetail.SelectedCells(0).RowIndex
    Dim callCodeInt As Integer = ItemDetail.Rows(currentRow).Cells(0).Value
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    sql &= " UPDATE MST_Item"
    sql &= "    SET delete_flg = '" & DeleteFlg & "'"
    sql &= "       ,update_date = '" & tmpdate & "'"
    sql &= " WHERE call_code = '" & callCodeInt & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub RefreshText()
    If AllRowDisplayFlg Then
      For i As Integer = 0 To ItemDetail.Rows.Count - 1
        ItemDetail.Rows(i).Selected = True
        ItemDetail.FirstDisplayedScrollingRowIndex = i
        ItemDetail.CurrentCell = ItemDetail.Rows(i).Cells(0)
        Exit For
      Next
    Else
      For i As Integer = 0 To ItemDetail.Rows.Count - 1
        If ItemDetail.Rows(i).Cells(18).Value = False Then
          ItemDetail.Rows(i).Selected = True
          ItemDetail.FirstDisplayedScrollingRowIndex = i
          ItemDetail.CurrentCell = ItemDetail.Rows(i).Cells(0)
          Exit For
        End If
      Next
    End If
  End Sub

  Private Sub DeletedDisplayCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles DeletedDisplayCheckBox.CheckedChanged
    If DeletedDisplayCheckBox.Checked Then
      AllRowDisplayFlg = True
      DisPlayDeleteRow(AllRowDisplayFlg)
      GetRowCount(True)
    Else
      AllRowDisplayFlg = False
      DisPlayDeleteRow(AllRowDisplayFlg)
      GetRowCount(False)
    End If
  End Sub
  Private Sub DisPlayDeleteRow(RowDisplayFlg As Boolean)
    '表示・非表示
    If ItemDetail.Rows.Count > 0 Then
      For i As Integer = 0 To ItemDetail.Rows.Count - 1
        If RowDisplayFlg Then
          If ItemDetail.Rows(i).Cells(18).Value = True Then
            ItemDetail.Rows(i).Visible = True
          End If
        Else
          If ItemDetail.Rows(i).Cells(18).Value = True Then
            ItemDetail.Rows(i).Visible = False
          End If
        End If
      Next
    End If
  End Sub
  Public Sub GetRowCount(RowDisplayFlg As Boolean)
    Dim rowC As Integer = 0
    If RowDisplayFlg Then
      rowC = ItemDetail.Rows.Count
    Else
      For i As Integer = 0 To ItemDetail.Rows.Count - 1
        If ItemDetail.Rows(i).Cells(18).Value = False Then
          rowC += 1
        End If
      Next
    End If
    RowCount.Text = rowC.ToString + "件"
  End Sub
End Class
