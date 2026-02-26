Imports Common
Imports Common.ClsFunction
Public Class Form_TokuisakiList
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

  Private Sub Form_ManufacturerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "得意先マスタ一覧" & " ( " & updateTime & " ) "
    'CheckboxExistFlg = True
    'CheckBoxCtlButton.Text = "全選択"
    'AllCheckBoxFlg = True
    ManufacturerDetail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    ''CheckBox列を追加する
    'Dim checkBox_Trans As New DataGridViewCheckBoxColumn
    'Dim checkBox_Delete As New DataGridViewCheckBoxColumn
    'ManufacturerDetail.Columns.Add(checkBox_Trans)
    ''カラムのヘッダー名指定
    'checkBox_Trans.Width = 90

    ManufacturerDetail.AllowUserToAddRows = False

    SqlServer.GetResult(tmpDt, GetAllSelectSql())
    ManufacturerDetail.DataSource = tmpDt

        'ManufacturerDetail.ColumnCount = 3
        '' Columns(0)のヘッダーテキストを設定
        ''ManufacturerDetail.Columns(0).HeaderText = "選択"

        '' 残りのヘッダーテキストを設定
        'ManufacturerDetail.Columns(0).HeaderText = "製造者コード"
        'ManufacturerDetail.Columns(1).HeaderText = "製造者名"

        '' 削除フラグの列を非表示にする
        'ManufacturerDetail.Columns(2).Visible = False

        '' カラムの幅指定
        'ManufacturerDetail.Columns(0).Width = 100
        'ManufacturerDetail.Columns(1).Width = 100

        ''カラムの整列設定
        'For i As Integer = 0 To 1
        '    ManufacturerDetail.Columns(i).DefaultCellStyle.Alignment =
        '    DataGridViewContentAlignment.MiddleCenter
        'Next

        ''ヘッダーの整列設定
        'For i As Integer = 0 To 1
        '    ManufacturerDetail.Columns(i).HeaderCell.Style.Alignment =
        '    DataGridViewContentAlignment.MiddleCenter
        'Next

        'SelectManufacturerMaster()

        ManufacturerDetail.ReadOnly = True
        ' 選択モードを全カラム選択に設定
        ManufacturerDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
  End Sub

  Public Sub SelectManufacturerMaster()
    'Dim sql As String = String.Empty
    'sql = GetAllSelectSql()
    Try
      SqlServer.GetResult(tmpDt, GetAllSelectSql())
      ManufacturerDetail.DataSource = tmpDt
      ManufacturerDetail.Refresh()

      'With tmpDb
      '  SqlServer.GetResult(tmpDt, sql)

      '  If tmpDt.Rows.Count = 0 Then
      '    MessageBox.Show("製造者マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '  Else
      '    WriteDetail(tmpDt, ManufacturerDetail)
      '  End If
      'End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try

    'DisPlayDeleteRow(AllRowDisplayFlg)

    ''行色付
    'If ManufacturerDetail.Rows.Count > 0 Then
    '  For i As Integer = 0 To ManufacturerDetail.Rows.Count - 1
    '    If ManufacturerDetail.Rows(i).Cells(2).Value = True Then
    '      ManufacturerDetail.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray
    '    End If
    '  Next
    'End If
    ''検索結果が存在する場合、先頭行選択
    'If ManufacturerDetail.Rows.Count > 0 Then
    '  For i As Integer = 0 To ManufacturerDetail.Rows.Count - 1
    '    If ManufacturerDetail.Rows(i).Cells(2).Value = False Then
    '      ManufacturerDetail.CurrentCell = ManufacturerDetail.Rows(i).Cells(0)
    '      ManufacturerDetail.Rows(i).Selected = True
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
    sql &= "        TokuiCD 得意先コード "
    sql &= "    ,   TokuiNM1 得意先名 "
    sql &= "    ,	Add1 住所１ "
    sql &= "    ,	Add2 住所２ "
    sql &= "    ,	ZipCD 郵便番号 "
    sql &= "    ,	TelNo 電話番号"
    sql &= "    ,	TekiyoKakeritu 摘要掛率 "
    sql &= "    ,	SyuTantoCD 主担当コード "
    sql &= "    ,	SeikyuSimebi 請求締日 "
    sql &= " FROM"
    sql &= "     MST_TOKUISAKI_ "
    sql &= " ORDER BY"
    sql &= "     TokuiCD"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_TokuisakiDetail.InputMode = 1
    Form_TokuisakiDetail.ShowDialog()
  End Sub

  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_TokuisakiDetail.InputMode = 2
    Form_TokuisakiDetail.ShowDialog()
  End Sub

  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = ManufacturerDetail.CurrentRow.Index
    Form_TokuisakiDetail.TokuisakiCodeTextValue = ManufacturerDetail.Rows(i).Cells(0).Value
    Form_TokuisakiDetail.TokuisakiNameTextValue = ManufacturerDetail.Rows(i).Cells(1).Value
    Form_TokuisakiDetail.Jusho1TextValue = ManufacturerDetail.Rows(i).Cells(2).Value
    Form_TokuisakiDetail.Jusho2TextValue = ManufacturerDetail.Rows(i).Cells(3).Value
    Form_TokuisakiDetail.YubinNoTextValue = ManufacturerDetail.Rows(i).Cells(4).Value
    Form_TokuisakiDetail.TelNoTextValue = ManufacturerDetail.Rows(i).Cells(5).Value
    Form_TokuisakiDetail.SeikyuSmbTextValue = ManufacturerDetail.Rows(i).Cells(8).Value
  End Sub

  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteManufacturerMaster()
  End Sub
  Private Sub DeleteManufacturerMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    'Dim DeleteRowFlg As Boolean = ManufacturerDetail.CurrentRow.Cells(2).Value
    Dim confirmation As String
    Dim msg1 As String
    Dim msg2 As String
    With tmpDb
      Try
        'If DeleteRowFlg Then
        '  sql = GetDeleteSql(False)
        '  msg1 = "削除取消します。" & vbCrLf & "よろしいでしょうか。"
        '  msg2 = "削除取消処理完了しました。"
        'Else
        sql = GetDeleteSql(True)
          msg1 = "削除します。" & vbCrLf & "よろしいでしょうか。"
          msg2 = "削除処理完了しました。"
        'End If

        confirmation = MessageBox.Show(msg1, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show(msg2, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelectManufacturerMaster()
            If DeletedDisplayCheckBox.Checked Then
              GetRowCount(True)
            Else
              GetRowCount(False)
            End If
            RefreshText()
          Else
            ' 削除失敗
            MessageBox.Show("得意先商品マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
  Private Sub RefreshText()
    If AllRowDisplayFlg Then
      For i As Integer = 0 To ManufacturerDetail.Rows.Count - 1
        ManufacturerDetail.Rows(i).Selected = True
        ManufacturerDetail.FirstDisplayedScrollingRowIndex = i
        ManufacturerDetail.CurrentCell = ManufacturerDetail.Rows(i).Cells(0)
        Exit For
      Next
    Else
      For i As Integer = 0 To ManufacturerDetail.Rows.Count - 1
        If ManufacturerDetail.Rows(i).Cells(2).Value = False Then
          ManufacturerDetail.Rows(i).Selected = True
          ManufacturerDetail.FirstDisplayedScrollingRowIndex = i
          ManufacturerDetail.CurrentCell = ManufacturerDetail.Rows(i).Cells(0)
          Exit For
        End If
      Next
    End If
  End Sub
  Private Function GetDeleteSql(DeleteFlg As Boolean) As String
    Dim sql As String = String.Empty
    Dim currentRow As Integer = ManufacturerDetail.SelectedCells(0).RowIndex
    Dim codeInt As Integer = ManufacturerDetail.Rows(currentRow).Cells(0).Value
    Dim codeInt2 As Integer = ManufacturerDetail.Rows(currentRow).Cells(0).Value
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    sql &= " DELETE FROM MST_TOKUISAKI_SHOHIN"
    'sql &= "    SET delete_flg = '" & DeleteFlg & "'"
    'sql &= "       ,update_date = '" & tmpdate & "'"
    sql &= " WHERE TokuiCD = '" & codeInt & "' "
    sql &= " AND ShohinCD = '" & codeInt2 & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

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
    If ManufacturerDetail.Rows.Count > 0 Then
      For i As Integer = 0 To ManufacturerDetail.Rows.Count - 1
        If RowDisplayFlg Then
          If ManufacturerDetail.Rows(i).Cells(2).Value = True Then
            ManufacturerDetail.Rows(i).Visible = True
          End If
        Else
          If ManufacturerDetail.Rows(i).Cells(2).Value = True Then
            ManufacturerDetail.Rows(i).Visible = False
          End If
        End If
      Next
    End If
  End Sub
  Public Sub GetRowCount(RowDisplayFlg As Boolean)
    Dim rowC As Integer = 0
    If RowDisplayFlg Then
      rowC = ManufacturerDetail.Rows.Count
    Else
      For i As Integer = 0 To ManufacturerDetail.Rows.Count - 1
        If ManufacturerDetail.Rows(i).Cells(2).Value = False Then
          rowC += 1
        End If
      Next
    End If
    RowCount.Text = rowC.ToString + "件"
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub
End Class
