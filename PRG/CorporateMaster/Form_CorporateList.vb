Imports Common
Imports Common.ClsFunction
Public Class Form_CorporateList
    Dim AllRowDisplayFlg As Boolean = False
    ReadOnly tmpDb As New ClsSqlServer
    Dim tmpDt As New DataTable
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
    Private Sub Form_CorporateMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaximizeBox = False
        Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "直送先一覧" & " ( " & updateTime & " ) "
    FormBorderStyle = FormBorderStyle.FixedSingle
        CorporateDetail.RowHeadersVisible = False
        MaximizeBox = False

    'ユーザーからのデータ追加を不可にしておく
    CorporateDetail.AllowUserToAddRows = False
    SqlServer.GetResult(tmpDt, GetAllSelectSql)
    CorporateDetail.DataSource = tmpDt

        'CorporateDetail.ColumnCount = 5

        'CorporateDetail.Columns(0).HeaderText = "コード"
        'CorporateDetail.Columns(1).HeaderText = "名称"
        'CorporateDetail.Columns(2).Visible = False
        'CorporateDetail.Columns(3).HeaderText = "メモ"
        'CorporateDetail.Columns(4).Visible = False

        ''カラムの幅指定
        'CorporateDetail.Columns(0).Width = 120
        'CorporateDetail.Columns(1).Width = 300
        'CorporateDetail.Columns(3).Width = 70

        ''ヘッダーの整列設定
        'For i As Integer = 0 To 4
        '    CorporateDetail.Columns(i).DefaultCellStyle.Alignment =
        ' DataGridViewContentAlignment.MiddleCenter
        '    CorporateDetail.Columns(i).HeaderCell.Style.Alignment =
        'DataGridViewContentAlignment.MiddleCenter
        'Next

        ''照会メソッド呼出し
        'SelectCorporateMaster()

        ''マルチ選択不可
        'CorporateDetail.MultiSelect = False

        ''選択モード設定(全カラム)
        'CorporateDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ''検索結果が存在する場合、先頭行選択
        'If CorporateDetail.Rows.Count > 0 Then
        '    For i As Integer = 0 To CorporateDetail.Rows.Count - 1
        '        If CorporateDetail.Rows(i).Cells(2).Value = False Then
        '            CorporateDetail.CurrentCell = CorporateDetail.Rows(i).Cells(0)
        '            CorporateDetail.Rows(i).Selected = True
        '            Exit For
        '        End If
        '    Next
        '    GetRowCount(False)
        'Else
        '    CorporateDetail.Text = "0件"
        'End If

        CorporateDetail.ReadOnly = True
        ' 選択モードを全カラム選択に設定
        CorporateDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub
    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        Form_CorporateDetail.InputMode = 1
        Form_CorporateDetail.ShowDialog()
    End Sub
    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        '詳細画面の項目値セット
        SetListData()
        Form_CorporateDetail.InputMode = 2
        Form_CorporateDetail.ShowDialog()
    End Sub
    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        DeleteCorporateMaster()
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

    Private Sub CorporateDetail_DoubleClick(sender As Object, e As EventArgs) Handles CorporateDetail.DoubleClick
        '詳細画面の項目値セット
        SetListData()
        Form_CorporateDetail.InputMode = 2
        Form_CorporateDetail.ShowDialog()
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Close()
    End Sub
    Public Sub SelectCorporateMaster()
    'Dim sql As String = String.Empty
    'sql = GetAllSelectSql()
    Try
      SqlServer.GetResult(tmpDt, GetAllSelectSql())
      CorporateDetail.DataSource = tmpDt
      CorporateDetail.Refresh()

      'With tmpDb
      '    SqlServer.GetResult(tmpDt, sql)
      '    If tmpDt.Rows.Count = 0 Then
      '        MessageBox.Show("大分類マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '    Else
      '        WriteDetail(tmpDt, CorporateDetail)
      '    End If
      'End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                              Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
        End Try

    'DisPlayDeleteRow(AllRowDisplayFlg)

    ''行色付
    'If CorporateDetail.Rows.Count > 0 Then
    '    For i As Integer = 0 To CorporateDetail.Rows.Count - 1
    '        If CorporateDetail.Rows(i).Cells(2).Value = True Then
    '            CorporateDetail.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray
    '        End If
    '    Next
    'End If
  End Sub
  Private Function GetAllSelectSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     CODE 直送先コード"
    sql &= "  ,  NAME 直送先名"
    sql &= " FROM"
    sql &= "     MST_CHOKUSO"
    sql &= " ORDER BY  "
    sql &= "     CODE "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql

  End Function

  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = CorporateDetail.CurrentRow.Index
    Form_CorporateDetail.CodeTextValue = CorporateDetail.Rows(i).Cells(0).Value
    Form_CorporateDetail.NameTextValue = CorporateDetail.Rows(i).Cells(1).Value
  End Sub
  Private Sub DeleteCorporateMaster()
        Dim sql As String = String.Empty
        Dim rowSelectionCode As String = String.Empty
        Dim DeleteRowFlg As Boolean = CorporateDetail.CurrentRow.Cells(2).Value
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
                        SelectCorporateMaster()
                        If DeletedDisplayCheckBox.Checked Then
                            GetRowCount(True)
                        Else
                            GetRowCount(False)
                        End If
                    Else
                        ' 削除失敗
                        MessageBox.Show("大分類マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    Exit Sub
                End If
                RefreshText()
            Catch ex As Exception
                Call ComWriteErrLog([GetType]().Name,
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
                Throw New Exception(ex.Message)
            End Try
        End With
    End Sub
    Private Function GetDeleteSql(DeleteFlg As Boolean) As String
        Dim sql As String = String.Empty
        Dim wkCorporate_Code As String
        Dim CurrentRow As Integer = CorporateDetail.SelectedCells(0).RowIndex
        wkCorporate_Code = CorporateDetail.Rows(CurrentRow).Cells(0).Value
        Dim tmpdate As DateTime = CDate(ComGetProcTime())
        sql &= " UPDATE MST_CORPORATE"
        sql &= "    SET DELETE_FLG = '" & DeleteFlg & "'"
        sql &= "       ,UPDATE_DATE = '" & tmpdate & "'"
        sql &= " WHERE CORPORATE_CODE = '" & wkCorporate_Code & "' "
        Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function

    Private Sub DisPlayDeleteRow(RowDisplayFlg As Boolean)
        '表示・非表示
        If CorporateDetail.Rows.Count > 0 Then
            For i As Integer = 0 To CorporateDetail.Rows.Count - 1
                If RowDisplayFlg Then
                    If CorporateDetail.Rows(i).Cells(2).Value = True Then
                        CorporateDetail.Rows(i).Visible = True
                    End If
                Else
                    If CorporateDetail.Rows(i).Cells(2).Value = True Then
                        CorporateDetail.Rows(i).Visible = False
                    End If
                End If
            Next
        End If
    End Sub
    Public Sub GetRowCount(RowDisplayFlg As Boolean)
        Dim rowC As Integer = 0
        If RowDisplayFlg Then
            rowC = CorporateDetail.Rows.Count
        Else
            For i As Integer = 0 To CorporateDetail.Rows.Count - 1
                If CorporateDetail.Rows(i).Cells(2).Value = False Then
                    rowC += 1
                End If
            Next
        End If
        RowCount.Text = rowC.ToString + "件"
    End Sub

    Private Sub RefreshText()
        If AllRowDisplayFlg Then
            For i As Integer = 0 To CorporateDetail.Rows.Count - 1
                CorporateDetail.Rows(i).Selected = True
                CorporateDetail.FirstDisplayedScrollingRowIndex = i
                CorporateDetail.CurrentCell = CorporateDetail.Rows(i).Cells(0)
                Exit For
            Next
        Else
            For i As Integer = 0 To CorporateDetail.Rows.Count - 1
                If CorporateDetail.Rows(i).Cells(2).Value = False Then
                    CorporateDetail.Rows(i).Selected = True
                    CorporateDetail.FirstDisplayedScrollingRowIndex = i
                    CorporateDetail.CurrentCell = CorporateDetail.Rows(i).Cells(0)
                    Exit For
                End If
            Next
        End If
    End Sub
End Class
