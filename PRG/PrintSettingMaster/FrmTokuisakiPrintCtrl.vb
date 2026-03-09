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

    Dim colCd As New DataGridViewTextBoxColumn()
    colCd.Name = "TOKUISAKI_CD"
    colCd.HeaderText = "得意先コード"
    colCd.DataPropertyName = "TokuiCD"
    colCd.ReadOnly = True
    colCd.Width = 120
    dgvList.Columns.Add(colCd)

    Dim colNm As New DataGridViewTextBoxColumn()
    colNm.Name = "TOKUISAKI_NM"
    colNm.HeaderText = "得意先名"
    colNm.DataPropertyName = "TokuiNM1"
    colNm.ReadOnly = True
    colNm.Width = 260
    dgvList.Columns.Add(colNm)

    Dim colFlg As New DataGridViewCheckBoxColumn()
    colFlg.Name = "INSTANT_PRINT_FLG"
    colFlg.HeaderText = "即時印刷"
    colFlg.DataPropertyName = "INSTANT_PRINT_FLG"
    colFlg.Width = 80
    dgvList.Columns.Add(colFlg)

    dgvList.AllowUserToAddRows = False
    dgvList.AllowUserToDeleteRows = False
    dgvList.RowHeadersVisible = False
    dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    dgvList.MultiSelect = False
    dgvList.EditMode = DataGridViewEditMode.EditOnEnter
  End Sub

  Private Sub SearchData()
    dgvList.EndEdit()

    Dim tokuisakiCd As String = txtTokuisakiCd.Text.Trim()
    Dim tokuisakiNm As String = txtTokuisakiNm.Text.Trim()

    _dtList = _service.GetList(tokuisakiCd, tokuisakiNm, _sqlServer)
    dgvList.DataSource = _dtList
  End Sub

  Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
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
      End If
    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

End Class