Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl

Public Class Form_TokuisakiShohinMasterList
  Inherits FormBase
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
  Private Sub Form_ScaleMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "得意先商品マスタ一覧" & " ( " & updateTime & " ) "
    ScaleDetail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    'ユーザーからのデータ追加を不可にしておく
    ScaleDetail.AllowUserToAddRows = False

    SqlServer.GetResult(tmpDt, GetAllSelectSql)
    ScaleDetail.DataSource = tmpDt


    ScaleDetail.ReadOnly = True
    ' 選択モードを全カラム選択に設定
    ScaleDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    ScaleDetail.DefaultCellStyle.WrapMode = DataGridViewTriState.False
    ScaleDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    ScaleDetail.Columns("登録日時").Width = 150


    If ScaleDetail.Rows.Count = 0 Then
      UpdateButton.Enabled = False
    Else
      UpdateButton.Enabled = True
    End If

    '初期表示
    AllRowDisplayFlg = False
    DisPlayDeleteRow(AllRowDisplayFlg)
    GetRowCount(AllRowDisplayFlg)



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
        ' 新規登録
        ClickCreateButton()

      Case Keys.F2
        ' 更新
        ClickUpdateButton()

      Case Keys.F3
        ' 削除
        ClickDeleteButton()

      Case Keys.F10
        ' 終了
        Close()
    End Select

  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    ClickCreateButton()
  End Sub

  Private Sub ClickCreateButton()
    Dim child As New Form_TokuisakiShohinMasterDetail(Me)

    child.InputMode = 1
    child.CmbMstCustomer1.Text = ""
    child.CmbMstItem1.Text = ""
    Me.Enabled = False
    child.Show()
    Me.ActiveControl = Me.CloseButton

    SelectScaleMaster()


    If ScaleDetail.Rows.Count = 0 Then
      UpdateButton.Enabled = False
    Else
      UpdateButton.Enabled = True
    End If


  End Sub

  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    ClickUpdateButton()
  End Sub

  Private Sub ClickUpdateButton()
    Dim child As New Form_TokuisakiShohinMasterDetail(Me)
    If ScaleDetail.CurrentRow Is Nothing Then
      ComMessageBox("修正したい行を選択してください。", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
      Exit Sub
    End If

    '詳細画面の項目値セット
    SetListData(child)
    child.InputMode = 2
    Me.Enabled = False
    child.Show()
    Me.ActiveControl = Me.CloseButton

    If ScaleDetail.Rows.Count = 0 Then
      UpdateButton.Enabled = False
    Else
      UpdateButton.Enabled = True
    End If

  End Sub
  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    ClickDeleteButton()
  End Sub

  Private Sub ClickDeleteButton()
    If ScaleDetail.CurrentRow Is Nothing Then
      ComMessageBox("削除したい行を選択してください。", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
      Exit Sub
    End If

    DeleteScaleMaster()

    If ScaleDetail.Rows.Count = 0 Then
      UpdateButton.Enabled = False
    Else
      UpdateButton.Enabled = True
    End If
  End Sub

  Private Sub ScaleDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ScaleDetail.CellClick
    'If ScaleDetail.CurrentRow.Cells(2).Value Then
    '    DeleteButton.Text = "削除取消"
    'Else
    '    DeleteButton.Text = "削除"
    'End If
  End Sub
  Private Sub DeletedDisplayCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles DeletedDisplayCheckBox.CheckedChanged
    If DeletedDisplayCheckBox.Checked Then
      AllRowDisplayFlg = True
      DisPlayDeleteRow(AllRowDisplayFlg)
      GetRowCount(AllRowDisplayFlg)
    Else
      AllRowDisplayFlg = False
      DisPlayDeleteRow(AllRowDisplayFlg)
      GetRowCount(AllRowDisplayFlg)
    End If
  End Sub
  Private Sub ScaleDetail_DoubleClick(sender As Object, e As EventArgs) Handles ScaleDetail.DoubleClick
    Dim child As New Form_TokuisakiShohinMasterDetail(Me)
    '詳細画面の項目値セット
    SetListData(child)
    child.InputMode = 2
    child.Show()
    Me.ActiveControl = Me.CloseButton
  End Sub
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub SetListData(prmChild As Form_TokuisakiShohinMasterDetail)

    '選択している行の行番号の取得
    Dim i As Integer = ScaleDetail.CurrentRow.Index
    prmChild.TokuisakiCodeTextValue = ScaleDetail.Rows(i).Cells("得意先コード").Value
    prmChild.ItemCodeTextValue = ScaleDetail.Rows(i).Cells("商品コード").Value
    prmChild.ItemNameTextValue = ScaleDetail.Rows(i).Cells("商品名").Value
    prmChild.ItemNameKanaTextValue = ScaleDetail.Rows(i).Cells("商品カナ").Value
    'prmChild.HyojunTankaTextValue = ScaleDetail.Rows(i).Cells("定金額").Value
    'prmChild.TaniTextValue = ScaleDetail.Rows(i).Cells("単位").Value
    'prmChild.IrisuTextValue = ScaleDetail.Rows(i).Cells("入数").Value
    prmChild.NouhinTankaTextValue = ScaleDetail.Rows(i).Cells("単価").Value
    'prmChild.TokuisakiTankaTextValue = ScaleDetail.Rows(i).Cells("単価").Value

  End Sub
  Public Sub SelectScaleMaster()
    'Dim sql As String = String.Empty
    'sql = GetAllSelectSql()
    Try
      SqlServer.GetResult(tmpDt, GetAllSelectSql())
      ScaleDetail.DataSource = tmpDt
      DisPlayDeleteRow(AllRowDisplayFlg)
      GetRowCount(AllRowDisplayFlg)
      ScaleDetail.Refresh()


      'With tmpDb
      '  SqlServer.GetResult(tmpDt, sql)
      '  If tmpDt.Rows.Count = 0 Then
      '    MessageBox.Show("計量器マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      '  Else
      '    WriteDetail(tmpDt, ScaleDetail)
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
    'If ScaleDetail.Rows.Count > 0 Then
    '  For i As Integer = 0 To ScaleDetail.Rows.Count - 1
    '    If ScaleDetail.Rows(i).Cells(2).Value = True Then
    '      ScaleDetail.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray
    '    End If
    '  Next
    'End If
  End Sub
  Private Function GetAllSelectSql() As String

    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "        TokuiCD 得意先コード "
    sql &= "    ,	ShohinCD 商品コード "
    sql &= "    ,	ShohinNM 商品名 "
    sql &= "    ,	ShohinKana 商品カナ "
    'sql &= "    ,	Tani 単位 "
    'sql &= "    ,	Irisu 入数 "
    'sql &= "    ,	HyojunKakaku 定金額 "
    sql &= "    ,	Baika 単価 "
    'sql &= "    ,	Tanka 単価 "
    sql &= "    ,	TDATE 登録日時 "
    sql &= " FROM"
    sql &= "     MST_TOKUISAKI_SHOHIN"
    'sql &= " WHERE "
    'sql &= "    TokuiCD <> '000000' "
    sql &= " ORDER BY  "
    sql &= "     TokuiCD,ShohinCD "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub DeleteScaleMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    'Dim DeleteRowFlg As Boolean = ScaleDetail.CurrentRow.Cells(2).Value
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
            SelectScaleMaster()
            'If DeletedDisplayCheckBox.Checked Then
            '  GetRowCount(True)
            'Else
            '  GetRowCount(False)
            'End If
            'RefreshText()
          Else
            ' 削除失敗
            MessageBox.Show("計量マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Dim CurrentRow As Integer = ScaleDetail.SelectedCells(0).RowIndex
    Dim wkTokuiCD As String = ScaleDetail.Rows(CurrentRow).Cells(0).Value
    Dim wkShohinCD As String = ScaleDetail.Rows(CurrentRow).Cells(1).Value

    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    sql &= " DELETE FROM MST_TOKUISAKI_SHOHIN"
    'sql &= "    SET DELETE_FLG = '" & DeleteFlg & "'"
    'sql &= "       ,UPDATE_DATE = '" & tmpdate & "'"
    sql &= " WHERE TokuiCd = '" & wkTokuiCD & "' "
    sql &= " AND ShohinCD = '" & wkShohinCD & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub DisPlayDeleteRow(RowDisplayFlg As Boolean)
    '表示・非表示
    If ScaleDetail.Rows.Count > 0 Then
      For i As Integer = 0 To ScaleDetail.Rows.Count - 1
        If RowDisplayFlg Then
          If ScaleDetail.Rows(i).Cells(0).Value = "000000" Then
            ScaleDetail.Rows(i).Visible = True
          End If
        Else
          If ScaleDetail.Rows(i).Cells(0).Value = "000000" Then
            ScaleDetail.CurrentCell = Nothing
            ScaleDetail.Rows(i).Visible = False
          End If
        End If
      Next
    End If
  End Sub
  Public Sub GetRowCount(RowDisplayFlg As Boolean)
    Dim rowC As Integer = 0
    If RowDisplayFlg Then
      rowC = ScaleDetail.Rows.Count
    Else
      For i As Integer = 0 To ScaleDetail.Rows.Count - 1
        If ScaleDetail.Rows(i).Cells(0).Value <> "000000" Then
          rowC += 1
        End If
      Next
    End If
    RowCount.Text = rowC.ToString + "件"
  End Sub
  Private Sub RefreshText()
    If AllRowDisplayFlg Then
      For i As Integer = 0 To ScaleDetail.Rows.Count - 1
        ScaleDetail.Rows(i).Selected = True
        ScaleDetail.FirstDisplayedScrollingRowIndex = i
        ScaleDetail.CurrentCell = ScaleDetail.Rows(i).Cells(0)
        Exit For
      Next
    Else
      For i As Integer = 0 To ScaleDetail.Rows.Count - 1
        If ScaleDetail.Rows(i).Cells(2).Value = False Then
          ScaleDetail.Rows(i).Selected = True
          ScaleDetail.FirstDisplayedScrollingRowIndex = i
          ScaleDetail.CurrentCell = ScaleDetail.Rows(i).Cells(0)
          Exit For
        End If
      Next
    End If
  End Sub

End Class
