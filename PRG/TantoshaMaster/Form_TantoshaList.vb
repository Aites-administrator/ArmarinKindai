Imports Common
Imports Common.ClsFunction

Public Class Form_TantoshaList
    '入力モード
    '新規：1
    '修正：2
    ReadOnly InputMode As Integer
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
    Private Sub Form_PackingMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaximizeBox = False
        Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Text = "担当者マスタ一覧" & " ( " & updateTime & " ) "
        FormBorderStyle = FormBorderStyle.FixedSingle
        PackingDetail.RowHeadersVisible = False
        MaximizeBox = False

        'ユーザーからのデータ追加を不可にしておく
        PackingDetail.AllowUserToAddRows = False

        SqlServer.GetResult(tmpDt, GetAllSelectSql)
        PackingDetail.DataSource = tmpDt
        'PackingDetail.Columns(0).HeaderText = "コード"
        'PackingDetail.Columns(1).HeaderText = "名称"
        'PackingDetail.Columns(2).HeaderText = "重量(ｇ)"
        'PackingDetail.Columns(4).HeaderText = "メモ"

        ''カラムの幅指定
        'PackingDetail.Columns(0).Width = 100
        'PackingDetail.Columns(1).Width = 210
        'PackingDetail.Columns(2).Width = 140
        'PackingDetail.Columns(3).Visible = False
        'PackingDetail.Columns(5).Visible = False

        ''ヘッダーの整列設定
        'For i As Integer = 0 To 5
        '    PackingDetail.Columns(i).DefaultCellStyle.Alignment =
        ' DataGridViewContentAlignment.MiddleCenter
        '    PackingDetail.Columns(i).HeaderCell.Style.Alignment =
        'DataGridViewContentAlignment.MiddleCenter
        'Next

        'SelectPackingMaster()

        ''マルチ選択不可
        'PackingDetail.MultiSelect = False

        ''選択モード設定(全カラム)
        'PackingDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ''検索結果が存在する場合、先頭行選択
        'If PackingDetail.Rows.Count > 0 Then
        '    For i As Integer = 0 To PackingDetail.Rows.Count - 1
        '        If PackingDetail.Rows(i).Cells(3).Value = False Then
        '            PackingDetail.CurrentCell = PackingDetail.Rows(i).Cells(0)
        '            PackingDetail.Rows(i).Selected = True
        '            Exit For
        '        End If
        '    Next
        '    GetRowCount(False)
        'Else
        '    PackingDetail.Text = "0件"
        'End If

        PackingDetail.ReadOnly = True
        ' 選択モードを全カラム選択に設定
        PackingDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub
    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        Form_TantoshaDetail.InputMode = 1
        Form_TantoshaDetail.ShowDialog()
    End Sub
    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        '詳細画面の項目値セット
        SetListData()
        Form_TantoshaDetail.InputMode = 2
        Form_TantoshaDetail.ShowDialog()
    End Sub
    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        DeleteScaleMaster()
    End Sub
    Private Sub PackingDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles PackingDetail.CellClick
    'If PackingDetail.CurrentRow.Cells(3).Value Then
    '    DeleteButton.Text = "削除取消"
    'Else
    '    DeleteButton.Text = "削除"
    'End If
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
    Private Sub PackingDetail_DoubleClick(sender As Object, e As EventArgs) Handles PackingDetail.DoubleClick
        '詳細画面の項目値セット
        SetListData()
        Form_TantoshaDetail.InputMode = 2
        Form_TantoshaDetail.ShowDialog()
    End Sub
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Close()
    End Sub

  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = PackingDetail.CurrentRow.Index
    Form_TantoshaDetail.CodeTextValue = PackingDetail.Rows(i).Cells(0).Value
    Form_TantoshaDetail.NameTextValue = PackingDetail.Rows(i).Cells(1).Value
  End Sub


  Public Sub SelectPackingMaster()
    'Dim sql As String = String.Empty
    'sql = GetAllSelectSql()
    Try
      SqlServer.GetResult(tmpDt, GetAllSelectSql())
      PackingDetail.DataSource = tmpDt
      PackingDetail.Refresh()

      'With tmpDb
      '    SqlServer.GetResult(tmpDt, sql)
      '    If tmpDt.Rows.Count = 0 Then
      '        MessageBox.Show("風袋マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '    Else
      '        WriteDetail(tmpDt, PackingDetail)
      '    End If
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
    'If PackingDetail.Rows.Count > 0 Then
    '    For i As Integer = 0 To PackingDetail.Rows.Count - 1
    '        If PackingDetail.Rows(i).Cells(3).Value = True Then
    '            PackingDetail.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray
    '        End If
    '    Next
    'End If
  End Sub

  Private Function GetAllSelectSql() As String

        Dim sql As String = String.Empty

        sql &= " SELECT"
        sql &= "     CODE 担当者コード"
        sql &= "  ,  NAME 担当者名"
        sql &= " FROM"
        sql &= "     MST_TANTO"
        sql &= " ORDER BY  "
        sql &= "     CODE "
        Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql

    End Function

    Private Sub DeleteScaleMaster()
        Dim sql As String = String.Empty
        Dim rowSelectionCode As String = String.Empty
        Dim DeleteRowFlg As Boolean = PackingDetail.CurrentRow.Cells(3).Value
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
                        SelectPackingMaster()
                        If DeletedDisplayCheckBox.Checked Then
                            GetRowCount(True)
                        Else
                            GetRowCount(False)
                        End If
                        RefreshText()
                    Else
                        ' 削除失敗
                        MessageBox.Show("風袋マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Dim wkPACKING_CODE As String
        Dim CurrentRow As Integer = PackingDetail.SelectedCells(0).RowIndex
        wkPACKING_CODE = PackingDetail.Rows(CurrentRow).Cells(0).Value

        Dim tmpdate As DateTime = CDate(ComGetProcTime())

        sql &= " UPDATE MST_PACKING"
        sql &= "    SET DELETE_FLG = '" & DeleteFlg & "'"
        sql &= "       ,UPDATE_DATE = '" & tmpdate & "'"
        sql &= " WHERE PACKING_CODE = '" & wkPACKING_CODE & "' "
        Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function

    Private Sub DisPlayDeleteRow(RowDisplayFlg As Boolean)
        '表示・非表示
        If PackingDetail.Rows.Count > 0 Then
            For i As Integer = 0 To PackingDetail.Rows.Count - 1
                If RowDisplayFlg Then
                    If PackingDetail.Rows(i).Cells(3).Value = True Then
                        PackingDetail.Rows(i).Visible = True
                    End If
                Else
                    If PackingDetail.Rows(i).Cells(3).Value = True Then
                        PackingDetail.Rows(i).Visible = False
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub GetRowCount(RowDisplayFlg As Boolean)
        Dim rowC As Integer = 0
        If RowDisplayFlg Then
            rowC = PackingDetail.Rows.Count
        Else
            For i As Integer = 0 To PackingDetail.Rows.Count - 1
                If PackingDetail.Rows(i).Cells(3).Value = False Then
                    rowC += 1
                End If
            Next
        End If
        RowCount.Text = rowC.ToString + "件"
    End Sub
    Private Sub RefreshText()
        If AllRowDisplayFlg Then
            For i As Integer = 0 To PackingDetail.Rows.Count - 1
                PackingDetail.Rows(i).Selected = True
                PackingDetail.FirstDisplayedScrollingRowIndex = i
                PackingDetail.CurrentCell = PackingDetail.Rows(i).Cells(0)
                Exit For
            Next
        Else
            For i As Integer = 0 To PackingDetail.Rows.Count - 1
                If PackingDetail.Rows(i).Cells(3).Value = False Then
                    PackingDetail.Rows(i).Selected = True
                    PackingDetail.FirstDisplayedScrollingRowIndex = i
                    PackingDetail.CurrentCell = PackingDetail.Rows(i).Cells(0)
                    Exit For
                End If
            Next
        End If
    End Sub
End Class
