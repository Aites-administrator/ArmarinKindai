Imports Common
Imports Common.ClsFunction
Public Class Form_CorporateDetail
  Public InputMode As Integer
  Public codeTextValue As String
  Public NameTextValue As String
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
  Private Sub Form_CorporateDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "大分類マスタ詳細" & " ( " & updateTime & " ) "
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
        Insertcorporatemaster()
      Case 2
        '更新メソッド呼出し
        Updatecorporatemaster()
    End Select
  End Sub
  Private Sub CodeText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CodeText.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub
  Private Sub SetInitialProperty()
    CodeText.Text = codeTextValue
    NameText.Text = NameTextValue

    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        'コード入力不可
        CodeText.Enabled = True
      Case 2
        'コード入力不可
        CodeText.Enabled = False
        '名称入力不可
        NameText.Enabled = True
    End Select
  End Sub
  Function CheckValue()
    Dim CheckResult As Boolean = True
    '必須項目チェック
    If String.IsNullOrEmpty(CodeText.Text) = True Then
      MessageBox.Show("コードを入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If
    '重複チェック
    If Form_CorporateList.CorporateDetail.Rows.Count > 0 Then
      For i As Integer = 0 To Form_CorporateList.CorporateDetail.Rows.Count - 1
        If CodeText.Text.Equals(Form_CorporateList.CorporateDetail.Rows(i).Cells(0).Value) Then
          MessageBox.Show("既に登録されているコードです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          CheckResult = False
          CodeText.Focus()
          Exit For
        End If
      Next
    End If
    Return CheckResult
  End Function
  Private Sub Insertcorporatemaster()
    Dim sql As String = String.Empty
    Dim rowselectioncode As String = String.Empty
    rowselectioncode = CodeText.Text
    With tmpDb
      Try
        sql = Getinsertsql()
        Dim confirmation As String
        confirmation = MessageBox.Show("登録します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' sql実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("登録処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' 一覧画面データ更新
            Form_CorporateList.SelectCorporateMaster()
            '' 一覧画面件数更新
            'Form_CorporateList.GetRowCount(Form_CorporateList.DeletedDisplayCheckBox.Checked)
            Close()
          Else
            ' 削除失敗
            MessageBox.Show("大分類マスタの登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

  Private Function Getinsertsql() As String
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())
    sql &= " INSERT INTO MST_CHOKUSO("
    sql &= "     CODE"
    sql &= " ,    NAME"
    sql &= " )"
    sql &= " values("
    sql &= "     '" & CodeText.Text & "'"
    sql &= " ,    '" & NameText.Text & "'"
    sql &= " )"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub Updatecorporatemaster()
    Dim sql As String = String.Empty
    Dim rowselectioncode As String = String.Empty
    rowselectioncode = CodeText.Text
    With tmpDb
      Try
        sql = Getupdatesql()
        Dim confirmation As String
        confirmation = MessageBox.Show("更新します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' sql実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("更新処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim CurrentRow As Integer = Form_CorporateList.CorporateDetail.CurrentRow.Index
            Form_CorporateList.SelectCorporateMaster()
            Form_CorporateList.CorporateDetail.Rows(CurrentRow).Selected = True
            Form_CorporateList.CorporateDetail.FirstDisplayedScrollingRowIndex = CurrentRow
            Form_CorporateList.CorporateDetail.CurrentCell = Form_CorporateList.CorporateDetail.Rows(CurrentRow).Cells(0)
            Close()
          Else
            ' 削除失敗
            MessageBox.Show("大分類マスタの更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
  Private Function Getupdatesql() As String
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())
    sql &= " UPDATE MST_CHOKUSO"
    sql &= "    SET NAME = '" & NameText.Text & "'"
    sql &= " WHERE CODE = '" & CodeText.Text & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
End Class