Imports Common
Imports Common.ClsFunction
Public Class Form_TantoshaDetail
    Public InputMode As Integer
    Public CodeTextValue As String
    Public NameTextValue As String
    Public WeightTextValue As String
    Public MemoTextValue As String

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
    Private Sub Form_PackingDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaximizeBox = False
        Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "担当者マスタ詳細" & " ( " & updateTime & " ) "
    SetInitialProperty()
        FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub
    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        Select Case InputMode
            Case 1
                If CheckValue() = False Then
                    Exit Sub
                End If
                '新規登録メソッド呼出し
                InsertPackingMaster()
            Case 2
                '更新メソッド呼出し
                UpdatePackingMaster()
        End Select
    End Sub
  Private Sub CodeText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCode.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
  Private Sub WeightText_TextChanged(sender As Object, e As EventArgs)
    'If IsNumeric(WeightText.Text) = True Then
    '  If CDec(WeightText.Text) * 100 < Math.Ceiling(CDec(WeightText.Text) * 100) Then
    '    WeightText.Text = WeightText.Text.Remove(WeightText.Text.Length - 1, 1)
    '    WeightText.SelectionStart = WeightText.Text.Length
    '  End If
    'End If
    'If CountChar(WeightText.Text, ".") > 1 Then
    '  WeightText.Text = WeightText.Text.Remove(WeightText.Text.Length - 1, 1)
    '  WeightText.SelectionStart = WeightText.Text.Length
    'End If
  End Sub
  Private Sub WeightText_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
    'Dim wkWeight As Double
    'If ActiveControl.Name <> "WeightText" And ActiveControl.Name <> "CloseButton" Then
    '  If String.IsNullOrEmpty(WeightText.Text) = False Then
    '    'If CountChar(WeightText.Text, ".") > 1 Then
    '    '    MessageBox.Show("重量に正しい数値を入力してください。。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    WeightText.Focus()
    '    '    Exit Sub
    '    'End If

    '    wkWeight = Double.Parse(WeightText.Text)
    '    'If wkWeight >= 1000 Then
    '    '    MessageBox.Show("重量の最大値を超えました。" & vbCrLf & "999.99kg以下を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    WeightText.Focus()
    '    '    Exit Sub
    '    'End If
    '    WeightText.Text = wkWeight.ToString("0.00")
    '  End If
    'End If
  End Sub
  Private Sub WeightText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back Or e.KeyChar = ".") Then
      e.Handled = True
    End If
  End Sub
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub
  Private Sub SetInitialProperty()
    TxtCode.Text = CodeTextValue
    TxtName.Text = NameTextValue
    'WeightText.Text = WeightTextValue
    'MemoText.Text = MemoTextValue

    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        'コード入力不可
        TxtCode.Enabled = True

      Case 2
        'コード入力不可
        TxtCode.Enabled = False
        '名称入力不可
        TxtName.Enabled = True
    End Select
  End Sub
  Function CheckValue()
    Dim CheckResult As Boolean = True
    '必須項目チェック
    If String.IsNullOrEmpty(TxtCode.Text) = True Then
      MessageBox.Show("コードを入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If
    '重複チェック
    If Form_TantoshaList.PackingDetail.Rows.Count > 0 Then
      For i As Integer = 0 To Form_TantoshaList.PackingDetail.Rows.Count - 1
        If TxtCode.Text.Equals(Form_TantoshaList.PackingDetail.Rows(i).Cells(0).Value) Then
          MessageBox.Show("既に登録されているコードです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          CheckResult = False
          TxtCode.Focus()
          Exit For
        End If
      Next
    End If
    Return CheckResult
  End Function
  Private Sub InsertPackingMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    rowSelectionCode = TxtCode.Text
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
            Form_TantoshaList.SelectPackingMaster()
            ' 一覧画面件数更新
            Form_TantoshaList.GetRowCount(Form_TantoshaList.DeletedDisplayCheckBox.Checked)

            Close()
          Else
            ' 登録失敗
            MessageBox.Show("風袋マスタの登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    sql &= " INSERT INTO MST_TANTO("
    sql &= "     CODE"
    sql &= " ,    NAME"
    sql &= " )"
    sql &= " VALUES("
    sql &= "     '" & TxtCode.Text & "'"
    sql &= "  ,   '" & TxtName.Text & "'"
    sql &= " )"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub UpdatePackingMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    rowSelectionCode = TxtCode.Text
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
            Dim CurrentRow As Integer = Form_TantoshaList.PackingDetail.CurrentRow.Index
            Form_TantoshaList.SelectPackingMaster()
            Form_TantoshaList.PackingDetail.Rows(CurrentRow).Selected = True
            Form_TantoshaList.PackingDetail.FirstDisplayedScrollingRowIndex = CurrentRow
            Form_TantoshaList.PackingDetail.CurrentCell = Form_TantoshaList.PackingDetail.Rows(CurrentRow).Cells(0)
            Close()
          Else
            ' 削除失敗
            MessageBox.Show("風袋マスタの更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    sql &= " UPDATE MST_TANTO"
    sql &= "    SET NAME = '" & TxtName.Text & "'"
    sql &= " WHERE CODE = '" & TxtCode.Text & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
        Return sql
    End Function
End Class