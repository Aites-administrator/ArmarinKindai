Imports System.Data
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class FrmTokuisakiPrintCtrl

  Private _service As TokuisakiPrintCtrlService
  Private _dtList As DataTable
  Private _sqlServer As New clsSqlServer

  Private Sub FrmTokuisakiPrintCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      _service = New TokuisakiPrintCtrlService()

      InitializeForm()
      InitializeGrid()
      SearchData()

    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub InitializeForm()
    Me.Text = "得意先即時印刷設定マスタ"
    Me.StartPosition = FormStartPosition.CenterScreen
    Me.KeyPreview = True
  End Sub

  Private Sub InitializeGrid()
    dgvList.AutoGenerateColumns = False
    dgvList.Columns.Clear()
    dgvList.DefaultCellStyle.Font = New Font("Segoe UI", 15)
    dgvList.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 15)
    dgvList.RowTemplate.Height = 50

    Dim colCd As New DataGridViewTextBoxColumn()
    colCd.Name = "TOKUISAKI_CD"
    colCd.HeaderText = "得意先コード"
    colCd.DataPropertyName = "TokuiCD"
    colCd.ReadOnly = True
    colCd.Width = 160
    colCd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    dgvList.Columns.Add(colCd)

    Dim colNm As New DataGridViewTextBoxColumn()
    colNm.Name = "TOKUISAKI_NM"
    colNm.HeaderText = "得意先名"
    colNm.DataPropertyName = "TokuiNM1"
    colNm.ReadOnly = True
    colNm.Width = 400
    dgvList.Columns.Add(colNm)

    Dim colFlg As New DataGridViewCheckBoxColumn()
    colFlg.Name = "INSTANT_PRINT_FLG"
    colFlg.HeaderText = "即時印刷"
    colFlg.DataPropertyName = "INSTANT_PRINT_FLG"
    colFlg.Width = 100
    dgvList.Columns.Add(colFlg)

    dgvList.AllowUserToAddRows = False
    dgvList.AllowUserToDeleteRows = False
    dgvList.RowHeadersVisible = False
    dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    dgvList.MultiSelect = False
    dgvList.EditMode = DataGridViewEditMode.EditOnEnter
    dgvList.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

  End Sub

  Private Sub SearchData()
    dgvList.EndEdit()

    Dim tokuisakiCd As String = txtTokuisakiCd.Text.Trim()
    Dim tokuisakiNm As String = txtTokuisakiNm.Text.Trim()

    _dtList = _service.GetList(tokuisakiCd, tokuisakiNm, _sqlServer)
    dgvList.DataSource = _dtList
  End Sub

  Private Sub btnSearch_Click(sender As Object, e As EventArgs)
    Try
      SearchData()
    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
    Try
      txtTokuisakiCd.Text = ""
      txtTokuisakiNm.Text = ""
      SearchData()
    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    Try
      If _dtList Is Nothing Then
        MessageBox.Show("保存対象のデータがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return
      End If

      dgvList.EndEdit()

      Dim count As Integer = _service.Save(_dtList, _sqlServer)

      MessageBox.Show(count.ToString() & "件更新しました。", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub FrmTokuisakiPrintCtrl_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then
        If dgvList.Focused OrElse dgvList.IsCurrentCellInEditMode Then
          Return
        End If

        e.SuppressKeyPress = True
        SearchData()
      Else
        Select Case e.KeyCode
          Case Keys.F5
            Me.btnSave.PerformClick()
          Case Keys.F6
            Me.btnClear.PerformClick()
          Case Keys.F10
            Me.btnClose.PerformClick()
        End Select
      End If

    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub MyApplication_Shutdown(ByVal sender As Object,
            ByVal e As System.EventArgs) Handles Me.FormClosing
    _sqlServer.Dispose()
  End Sub

  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

    If False = HasUnsavedData() _
        OrElse typMsgBoxResult.RESULT_YES = ComMessageBox("未保存のデータあります。破棄しますか？" _
                            , Me.Text _
                            , typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_YESNO) Then
      Me.Close()
    End If

  End Sub

  ''' <summary>
  ''' 未保存データが存在するか確認
  ''' </summary>
  Private Function HasUnsavedData() As Boolean

    Dim result As Boolean = False

    Try

      If _dtList IsNot Nothing Then

        dgvList.EndEdit()

        For Each row As DataRow In _dtList.Rows

          Dim currentFlg As Boolean = Convert.ToBoolean(row("INSTANT_PRINT_FLG"))
          Dim originalFlg As Boolean = Convert.ToBoolean(row("ORIGINAL_FLG"))

          If currentFlg <> originalFlg Then
            result = True
            Exit For
          End If

        Next

      End If

    Catch ex As Exception
      Throw
    End Try

    Return result

  End Function
End Class