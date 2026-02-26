Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl
Public Class Form_ResultDetail
  Inherits FormBase

  Public TokuiCd As String
  Public TokuiName As String

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
  Private Sub Form_ResultDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' フォームの最大化ボタンを無効にする
    MaximizeBox = False

    Dim tmpDt As New DataTable
    Dim tmpSumDt As New DataTable
    ' アセンブリの最終更新日時を取得し、フォームのタイトルに表示するテキストを設定
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "実績詳細" & " ( " & updateTime & " ) "

    FormBorderStyle = FormBorderStyle.FixedSingle

    '得意先名設定
    Me.TxtTokuName.Text = TokuiCd & " " & TokuiName

    'コンボボックス初期化
    InitCmb()

    'グリッド初期化
    InitGrid()

    SqlServer.GetResult(tmpSumDt, SqlGetDenpyoSum)
    If tmpSumDt.Rows.Count > 0 Then
      Me.TxtGoukeiKin.Text = tmpSumDt.Rows(0).Item("合計Kingaku")
    End If
  End Sub

  Private Sub InitCmb()
    Dim tmpNohinDayDt As New DataTable

    '納品日コンボボックス初期化
    CmbNohinDay.Items.Clear()
    SqlServer.GetResult(tmpNohinDayDt, GetNohinbiSql)
    CmbNohinDay.Items.Add("")
    For i = 0 To tmpNohinDayDt.Rows.Count - 1
      CmbNohinDay.Items.Add(DateFormatChange(typDateFormat.FORMAT_DATE, tmpNohinDayDt.Rows(i).Item("納品日").ToString))
    Next

  End Sub

  Private Sub InitGrid()

    InitGridDataSource()

    ' ユーザーからのデータ追加を許可しない
    DataGridView1.AllowUserToAddRows = False
    DataGridView1.ReadOnly = True


    ' ヘッダーとセルの内容を中央寄せに設定
    For i As Integer = 0 To tmpDt.Columns.Count - 1
      DataGridView1.Columns(i).Width = 80
    Next

    ' ヘッダーとセルの内容を中央寄せに設定
    For i As Integer = 0 To tmpDt.Columns.Count - 1
      DataGridView1.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      DataGridView1.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    Next

    ' マルチ選択を無効にする
    DataGridView1.MultiSelect = False

    ' 選択モードを全カラム選択に設定
    DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect


  End Sub

  Private Sub InitGridDataSource()
    ''データグリッド初期設定
    SqlServer.GetResult(tmpDt, GetSelectJissekiSql)
    DataGridView1.DataSource = tmpDt
  End Sub


  Private Sub TxtShohinCd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtShohinCd.Validating, TxtGoukeiKin.Validating
    '検索処理
    ''データグリッド初期設定
    InitGridDataSource()
  End Sub

  Private Sub TxtDenNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtDenNo.Validating
    '検索処理
    ''データグリッド初期設定
    InitGridDataSource()
  End Sub

  Private Sub CmbNohinDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNohinDay.SelectedIndexChanged
    '検索処理
    ''データグリッド初期設定
    InitGridDataSource()
  End Sub
  Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    'Form_ResultList.DispGrid(DataGridView1.CurrentRow.Cells(0).Value)

    Me.Close()
  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Me.Close()
  End Sub


  Private Sub CloseButton_Click(sender As Object, e As EventArgs)
    Close()
  End Sub


  Private Function GetSelectJissekiSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT DenNo  伝票番号 "
    sql &= "    ,   GyoNo  GyoNo "
    sql &= "    ,   ShohinCD ShohinCD "
    sql &= "    ,   ShohinNM ShohinNM "
    sql &= "    ,   JutyuSu 受注数 "
    sql &= "    ,   Suryo Suryo "
    sql &= "    ,   Tanka Tanka "
    sql &= "    ,   Kingaku Kingaku "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    sql &= GetSqlWhere()
    sql &= " ORDER BY NohinDay,DenNo,GyoNo "

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetNohinbiSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT NohinDay 納品日 "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    sql &= GetSqlWhere()
    sql &= " GROUP BY NohinDay "
    sql &= " ORDER BY NohinDay desc "

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetSqlWhere() As String
    Dim sql As String = String.Empty
    If Not String.IsNullOrWhiteSpace(TokuiCd) Then
      sql &= " AND TokuiCD = '" & TokuiCd & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.TxtShohinCd.Text) Then
      sql &= " AND ShohinCd LIKE '%" & Me.TxtShohinCd.Text & "%'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.TxtDenNo.Text) Then
      sql &= " AND DenNo LIKE '%" & Me.TxtDenNo.Text & "'"
    End If
    If Not String.IsNullOrWhiteSpace(Me.CmbNohinDay.Text) Then
      sql &= " AND NohinDay = '" & DateFormatChange(typDateFormat.FORMAT_STRING, Me.CmbNohinDay.Text) & "'"
    End If

    Return sql
  End Function

  ''' <summary>
  ''' 得意先マスタを検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetDenpyoSum() As String

    Dim sql As String = String.Empty
    sql &= " SELECT	Sum(CONVERT(numeric,UriageKin)) 合計Kingaku "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    sql &= " AND TokuiCd = '" & Me.TokuiCd & "'"
    sql &= " GROUP BY DenNo "

    Return sql

  End Function


End Class